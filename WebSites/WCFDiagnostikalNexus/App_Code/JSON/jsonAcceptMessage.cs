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
    private string Mensaje;


    

    public jsonAcceptMessage()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public jsonAcceptMessage(bool estado, string mensaje)
    {
        this.estado = estado;
        this.Mensaje = mensaje;
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

    public string mensaje
    {
        get
        {
            return Mensaje;
        }

        set
        {
            Mensaje = value;
        }
    }
}