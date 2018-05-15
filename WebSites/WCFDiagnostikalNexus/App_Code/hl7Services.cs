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
        System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA, SE RECIBIO Y DECODIFICO ");


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
                parseadorhl7.getPeticion(peticion.Mensaje);
                res = new jsonAcceptMessage(true, "Ok");
                return JsonConvert.SerializeObject(res);
                /*if (parseadorhl7.isValid(peticion.Mensaje, peticion.Checksum)) { 
                
                }*/
            }

           
             }
        res = new jsonAcceptMessage(false, "No Hubo recepcion");
        return JsonConvert.SerializeObject(res);
    }
    //***************************************************************************************END Ingeso de datos*********************************************************
    [WebMethod]
    public String decodificarHl7() {
        hl7parser parseadorhl7 = new hl7parser();
       /* string peticion = @"MSH|^~\&|SIAP|MINSAL|IOLIS|TECNODIAGNOSTICA - VITEK 2 Compact|201705021121||OML^O21|1|D|2.5.1|||AL|AL|||||
PID|1||911-16^^^30||JAIME AVILES^CESAR^EDUARDO ||201705031121|1
PV1|1|2|MINSAL-Hospitalización
ORC|NW|112||1|||||201705021200|||716^CERON RIVERA^ADA NOHEMY^|55^^^^^^^^Cirugía Hombres 1||||1^Ministerio de Salud||||Hospital Nacional Santa Tecla LI San Rafael^^30
OBR|1|1069||298^HEMOCULTIVO^^M19|||201705021112||||||||1069SPM|1|1069||1^Sangre||||^^|||||||||201705021212^";
*/

        string peticion= @"MSH|^~\u005Cu005C&|SIAP|MINSAL|DiagnostikalServer|DIAGNOSTIKA CAPRIS|201805091010||OML^O21|1|D|2.5.1|||AL|AL||||| 
PID|1||1-18^^^43||PANIAGUA GOMEZ^KENNETH^ANTONIO ||2004-04-01|1 
PV1|1|1|MINSAL-Consulta Externa 
ORC|NW|28||3|||||201805090411|||49^DIAZ ALARCÓN^JULIO ERNESTO^|46^^^^^^^^MINSAL-Cirugía Oral||||1^Ministerio de Salud||||Hospital Nacional San Vicente SV   Santa Gertrudis^^43
OBR|1|42||493^COLESTEROL^^Q10|||201805090959|||1|||||42 
OBR|2|41||487^ACIDO ÚRICO^^Q4|||201805090959|||1|||||41 SPM|1|42||1^Sangre||||^^|||||||||201805090959 
SPM|2|41||1^Sangre||||^^|||||||||201805090959";
        parseadorhl7.getPeticion(peticion);
        
        return "OK";
    }


    [WebMethod]
    public string acceptMessage2Respuesta()
    {
        openfDBManager managerDBOpenf = new openfDBManager();
        String respuesta = "";
        respuesta = managerDBOpenf.RespuestaQuimica();


        return respuesta;
    }
}
