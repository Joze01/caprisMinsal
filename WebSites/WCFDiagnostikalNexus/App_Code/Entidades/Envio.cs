﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Envio
/// </summary>
public class Envio
{
    string mensaje;
    int indice;
    public Envio()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
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

    public int Indice
    {
        get
        {
            return indice;
        }

        set
        {
            indice = value;
        }
    }
}