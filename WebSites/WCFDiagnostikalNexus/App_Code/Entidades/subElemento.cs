using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de subElemento
/// </summary>
public class subElemento
{
    string tParam;
    string codigo;
    string nombre;
    string sexo;
    string edad;


    public subElemento()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string TParam
    {
        get
        {
            return tParam;
        }

        set
        {
            tParam = value;
        }
    }

    public string Codigo
    {
        get
        {
            return codigo;
        }

        set
        {
            codigo = value;
        }
    }

    public string Nombre
    {
        get
        {
            return nombre;
        }

        set
        {
            nombre = value;
        }
    }

    public string Sexo
    {
        get
        {
            return sexo;
        }

        set
        {
            sexo = value;
        }
    }

    public string Edad
    {
        get
        {
            return edad;
        }

        set
        {
            edad = value;
        }
    }
}