using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de transacciones
/// </summary>
public class transacciones
{

    long Indice;
    string peticion;
    string respuesta;
    long estado;
    DateTime fecha;
    long pruebas;
    string orden;
    string siapsid;



    public transacciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public long Indice1
    {
        get
        {
            return Indice;
        }

        set
        {
            Indice = value;
        }
    }

    public string Peticion
    {
        get
        {
            return peticion;
        }

        set
        {
            peticion = value;
        }
    }

    public string Respuesta
    {
        get
        {
            return respuesta;
        }

        set
        {
            respuesta = value;
        }
    }

    public long Estado
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

    public DateTime Fecha
    {
        get
        {
            return fecha;
        }

        set
        {
            fecha = value;
        }
    }

    public long Pruebas
    {
        get
        {
            return pruebas;
        }

        set
        {
            pruebas = value;
        }
    }

    public string Orden
    {
        get
        {
            return orden;
        }

        set
        {
            orden = value;
        }
    }

    public string Siapsid
    {
        get
        {
            return siapsid;
        }

        set
        {
            siapsid = value;
        }
    }
}