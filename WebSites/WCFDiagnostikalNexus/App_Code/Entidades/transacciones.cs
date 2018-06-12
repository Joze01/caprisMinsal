using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de transacciones
/// </summary>
public class transacciones
{

    int Indice;
    string peticion;
    string respuesta;
    int estado;
    DateTime fecha;
    int pruebas;
    string orden;
    string siapsid;



    public transacciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int Indice1
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

    public int Estado
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

    public int Pruebas
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