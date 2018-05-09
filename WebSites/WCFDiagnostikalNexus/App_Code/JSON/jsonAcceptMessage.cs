using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de jsonAcceptMessage
/// </summary>
public class jsonAcceptMessage
{
    private Boolean estado;
    private string mensaje;


    

    public jsonAcceptMessage()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public jsonAcceptMessage(bool estado, string mensaje)
    {
        this.estado = estado;
        this.mensaje = mensaje;
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
}