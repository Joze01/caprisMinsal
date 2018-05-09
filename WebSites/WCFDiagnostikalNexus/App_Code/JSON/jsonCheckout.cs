using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de jsonCheckout
/// </summary>
public class jsonCheckout
{

    private string mensaje;
    private Boolean estado;
    public jsonCheckout()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public jsonCheckout(string mensaje, bool estado)
    {
        this.mensaje = mensaje;
        this.estado = estado;
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