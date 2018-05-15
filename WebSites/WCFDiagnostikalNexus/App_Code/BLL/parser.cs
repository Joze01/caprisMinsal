using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de parser
/// </summary>
public class parser
{
    conexion con = new conexion();
    public parser()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /*
    public Boolean guardarPeticion(string mensaje)
    {
        Boolean resultado = false;
        Peticion nuevaPeticion = JsonConvert.DeserializeObject<Peticion>(mensaje);

        Boolean integridad = true;

        System.Diagnostics.Debug.WriteLine("ACESO DESDE FUERA" + mensaje);
        string md5 = MD5Hash(nuevaPeticion.Mensaje);
        System.Diagnostics.Debug.WriteLine(nuevaPeticion.Token);
        System.Diagnostics.Debug.WriteLine("MD5: "+md5);
        System.Diagnostics.Debug.WriteLine(nuevaPeticion.Md5);
        System.Diagnostics.Debug.WriteLine(nuevaPeticion.Mensaje);

        if (md5 == nuevaPeticion.Md5) {
            integridad = true;
        }
        DatosPeticion datos = new DatosPeticion();

        //con.nuevaPeticion(datos)

        if (true && integridad)
        {
            return true;
        }
        
        return true;
    }
    */

    public  string MD5Hash(string dato)
    {

            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(dato));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            return sb.ToString();
            /*
            StringBuilder hash = new StringBuilder();
         MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
         byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes("123"));

         for (int i = 0; i < bytes.Length; i++)
         {
             hash.Append(bytes[i].ToString("x2"));
              return hash.ToString();
         }
        
       using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
       {
           byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes();
           byte[] hashBytes = md5.ComputeHash(inputBytes);

           // Convert the byte array to hexadecimal string
           StringBuilder sb = new StringBuilder();
           for (int i = 0; i < hashBytes.Length; i++)
           {
               sb.Append(hashBytes[i].ToString("X2"));
           }
           return sb.ToString();
       }

       */
       

    }
}