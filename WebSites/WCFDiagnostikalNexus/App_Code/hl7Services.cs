using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


/// <summary>
/// Summary description for hl7Services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class hl7Services : System.Web.Services.WebService
{
    public SecuredTokenWebservice SoapHeader = new SecuredTokenWebservice();

    public hl7Services()
    {


        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    //*******************************************************Ingreso de datos****************************************************************************
    [WebMethod]
    public string checkin(string json_array)
    {
        jsonCheckin res;
        System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA" + json_array);
        string resultado = "{\"Estado\": \"false\",\"mensaje\": \"Permiso denegado\",\"token\": \"\"}";
        if (json_array != null)
        {
            System.Diagnostics.Debug.WriteLine("ingreso a checkin");
            Usuario user = new Usuario();
            user = JsonConvert.DeserializeObject<Usuario>(json_array);

            System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA" + json_array);
            res = new jsonCheckin("", "Permiso Denegado", false);

            resultado = JsonConvert.SerializeObject(res);



            if (user != null)
            {
                SoapHeader.UserName = user.AppUser;
                SoapHeader.Password = user.Password;

                if (SoapHeader == null)
                {
                    res = new jsonCheckin("", "Permiso Denegado", false);
                    resultado = JsonConvert.SerializeObject(res);
                }
                if (string.IsNullOrEmpty(SoapHeader.UserName) || string.IsNullOrEmpty(SoapHeader.Password))
                {
                    res = new jsonCheckin("", "Permiso Denegado", false);
                    resultado = JsonConvert.SerializeObject(res);
                }


                //Check is User credentials Valid
                if (!SoapHeader.IsUserCredentialsValid(SoapHeader.UserName, SoapHeader.Password))
                {
                    res = new jsonCheckin("", "Permiso Denegado", false);
                    resultado = JsonConvert.SerializeObject(res);
                }
                else { 


                // Create and store the AuthenticatedToken before returning it
                string token = Guid.NewGuid().ToString();
                HttpRuntime.Cache.Add(
                    token, //token
                    SoapHeader.UserName, //usuer
                    null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(30),
                    System.Web.Caching.CacheItemPriority.NotRemovable,
                    null
                    );
                    res = new jsonCheckin(token, "OK", true);
                    resultado = JsonConvert.SerializeObject(res);
                    //resultado = @"{'estado': 'true','mensaje': 'OK','token': '" + token + "'}";
                    return resultado;
                }



            }
            else
            {
                res = new jsonCheckin("", "Error", false);
                resultado = JsonConvert.SerializeObject(res);
            }
        }
        else {
            res = new jsonCheckin("", "Dato Nulo", false);
            resultado = JsonConvert.SerializeObject(res);
            return resultado;
        }
       
        return resultado;
    }
    [WebMethod]
    public string checkout(string json_array) {
        Checkout chkinfo = JsonConvert.DeserializeObject<Checkout>(json_array);
        jsonCheckout res;
        if (HttpRuntime.Cache[chkinfo.Token]!="")
        {
            HttpRuntime.Cache.Remove(chkinfo.Token);
            res = new jsonCheckout("OK",true);
            return JsonConvert.SerializeObject(res);

        }


        res = new jsonCheckout("No Hubo Recepcion",false);
        return JsonConvert.SerializeObject(res);
    }
    [WebMethod]
    public string acceptMessage(string json_array)
    {
        parser parseador = new parser();
        jsonAcceptMessage res;
        System.Diagnostics.Debug.WriteLine("JSON: "+json_array);
       
  



        Peticion peticion = JsonConvert.DeserializeObject<Peticion>(json_array);
        hl7parser parseadorhl7 = new hl7parser();
        System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA" + json_array);


        SoapHeader.AuthenticationToken = peticion.Token;
            if (peticion != null) {
                if (SoapHeader == null) {
                res = new jsonAcceptMessage(false, "Permiso Denegado");
                return JsonConvert.SerializeObject(res);
                
                }

                if (!SoapHeader.IsUserCredentialsValid(SoapHeader)) {
                res = new jsonAcceptMessage(false, "Permiso Denegado");
                return JsonConvert.SerializeObject(res);
            }
                else {
                    if (parseador.guardarPeticion(json_array)) {
                    res = new jsonAcceptMessage(true, "OK");
                    string mensajehl7 = "";



                    return JsonConvert.SerializeObject(res);




                }
                }

           
             }
        res = new jsonAcceptMessage(false, "No Hubo recepcion");
        return JsonConvert.SerializeObject(res);
    }
    //***************************************************************************************END Ingeso de datos*********************************************************
    [WebMethod]
    
    public String decodificarHl7() {
        hl7parser parseadorhl7 = new hl7parser();
        string peticion = @"MSH|^~\&|SIAP|MINSAL|IOLIS|TECNODIAGNOSTICA - VITEK 2 Compact|201705021121||OML^O21|1|D|2.5.1|||AL|AL|||||
PID|1||911-16^^^30||JAIME AVILES^CESAR^EDUARDO ||201705031121|1
PV1|1|2|MINSAL-Hospitalización
ORC|NW|112||1|||||201705021200|||716^CERON RIVERA^ADA NOHEMY^|55^^^^^^^^Cirugía Hombres 1||||1^Ministerio de Salud||||Hospital Nacional Santa Tecla LI San Rafael^^30
OBR|1|1069||298^HEMOCULTIVO^^M19|||201705021112||||||||1069SPM|1|1069||1^Sangre||||^^|||||||||201705021212^";

        parseadorhl7.getPeticion(peticion);
        
        return "OK";
    }


    [WebMethod]
    public string acceptMessage2(string json_array)
    {
        parser parseador = new parser();
        jsonAcceptMessage res;
        System.Diagnostics.Debug.WriteLine("JSON: " + json_array);





        Peticion peticion = JsonConvert.DeserializeObject<Peticion>(json_array);
        hl7parser parseadorhl7 = new hl7parser();
        System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA" + json_array);


        SoapHeader.AuthenticationToken = peticion.Token;
        if (peticion != null)
        {
            if (SoapHeader == null)
            {
                res = new jsonAcceptMessage(false, "Permiso Denegado");
                return JsonConvert.SerializeObject(res);

            }

            if (!SoapHeader.IsUserCredentialsValid(SoapHeader))
            {
                res = new jsonAcceptMessage(false, "Permiso Denegado");
                return JsonConvert.SerializeObject(res);
            }
            else
            {
                if (true)
                {
                    res = new jsonAcceptMessage(true, "Ok");



                    //System.Diagnostics.Debug.WriteLine("Datos del parser"+parseadorhl7.getPeticion(peticion.Mensaje));

                    return JsonConvert.SerializeObject(res);




                }
            }


        }
        res = new jsonAcceptMessage(false, "No Hubo recepcion");
        return JsonConvert.SerializeObject(res);
    }
}
