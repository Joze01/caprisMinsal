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
    public string generarRespuestas()
    {
        openfDBManager managerDBOpenf = new openfDBManager();
        string respuesta = "";
        string jsonRespuesta = "";

        respuesta = @"MSH|^~\\u005Cu005C&|Ls|5^Sci - DiagnotikalNexus|SIAP|MINSAL|201802211712||OUL^R22|3|D|2.5.1|||AL|AL" +
                   "ORC | NW | 27553 ||| CM |||| 201802211712 ||| LAB0001 ^ DPC" +
                   "OBR | 1 | 39030 || 150 ^ GLUCOSA ^ L ^ GLUC ^ GLUCOSA |||| 201802211712 || 1 |||||| LAB0001 ^ HEMATOLOGIA |||||| 201802211712 || HM | F" +
                   "OBX | 1 | ST | 142 | 1 | Normal" +
                   "OBX | 2 | NM | 484 ^ GLUCOSA | Instrumento | 80 | mg / dl | 70 - 110 |||| F ||| 201802211712" +
                   "OBR | 2 | 39031 || 528 ^ DEPURACION DE CREATININA DE 24 HORAS ^ L ^ CRE24 ^ DEP CREA 24H |||| 201802211712 || 2 |||||| LAB0001 ^ HEMATOLOGIA |||||| 201802211712 || HM | F" +
                   "OBX | 1 | ST | 142 | 3 | Anormal" +
                   "OBX | 2 | NM | 100 ^ CREATININA | Instrumento | 0.7 | mg / dL | 0.55 - 1.02 |||| F ||| 201802211712" +
                   "OBX | 3 | NM | 101 ^ CREATININA EN ORINA| Instrumento | 40 | mg / dl | 30 - 125 |||| F ||| 201802211712" +
                   "OBX | 4 | NM | 102 ^ DEPURACION DE CREATININA 24 Hrs | Instrumento | 1.59 | ml / min | 70 - 110 |||| F ||| 201802211712" +
                   "OBX | 5 | NM | 103 ^ VOLUMEN | Instrumento | 40 |||||| F ||| 201802211712";

        //respues=
        //forech ()
        jsonRespuesta = "{\"Respuestas\": [{ \"mensaje\":\""+respuesta+"\"}]}";



        return jsonRespuesta;
    }
}
