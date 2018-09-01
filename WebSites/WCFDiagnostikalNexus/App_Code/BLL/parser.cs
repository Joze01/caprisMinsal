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
    
    public  string MD5Hash(string dato)
    {

            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(dato));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            return sb.ToString();
           

    }
}