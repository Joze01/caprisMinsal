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
    private string checksum;

    

    public Peticion()
    {
       
    }

    public Peticion(string token, string mensaje, string checksum)
    {
        this.Token = token;
        this.Mensaje = mensaje;
        this.checksum = checksum;
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

    public string Checksum
    {
        get
        {
            return checksum;
        }

        set
        {
            checksum = value;
        }
    }
}