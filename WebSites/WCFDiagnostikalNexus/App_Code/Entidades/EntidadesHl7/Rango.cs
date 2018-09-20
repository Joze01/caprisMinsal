using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Rango
/// </summary>
public class Rango
{
    string comentario;
    long idComentario;

    float rangoInferior;
    float rangoSuperior;


    public Rango()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Comentario
    {
        get
        {
            return comentario;
        }

        set
        {
            comentario = value;
        }
    }

    public long IdComentario
    {
        get
        {
            return idComentario;
        }

        set
        {
            idComentario = value;
        }
    }

    public float RangoInferior
    {
        get
        {
            return rangoInferior;
        }

        set
        {
            rangoInferior = value;
        }
    }

    public float RangoSuperior
    {
        get
        {
            return rangoSuperior;
        }

        set
        {
            rangoSuperior = value;
        }
    }
}