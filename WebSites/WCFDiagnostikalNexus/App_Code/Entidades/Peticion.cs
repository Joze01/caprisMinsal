using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Peticion
/// </summary>
public class Peticion
{
    private string token;
    private string mensaje;
    private string md5;

    

    public Peticion()
    {
       
    }

    public Peticion(string token, string mensaje, string md5)
    {
        this.Token = token;
        this.Mensaje = mensaje;
        this.Md5 = md5;
    }

    public string Token
    {
        get
        {
            return token;
        }

        set
        {
            token = value;
        }
    }

    public string Mensaje
    {
        get
        {
            return mensaje;
        }

        set
        {
            mensaje = value;
        }
    }

    public string Md5
    {
        get
        {
            return md5;
        }

        set
        {
            md5 = value;
        }
    }
}