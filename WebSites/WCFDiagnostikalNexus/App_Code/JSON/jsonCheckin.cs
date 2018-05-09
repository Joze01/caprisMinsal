using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de jsonCheckin
/// </summary>
public class jsonCheckin
{
    private string token;
    private string mensaje;
    private Boolean estado;
    public jsonCheckin()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public jsonCheckin(string token, string mensaje, bool estado)
    {
        this.Token = token;
        this.Mensaje = mensaje;
        this.Estado = estado;
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

    public bool Estado
    {
        get
        {
            return estado;
        }

        set
        {
            estado = value;
        }
    }
}