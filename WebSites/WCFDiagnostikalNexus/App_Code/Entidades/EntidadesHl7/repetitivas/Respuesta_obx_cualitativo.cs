using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Respuesta_obx_cualitativo
/// </summary>
public class Respuesta_obx_cualitativo
{
    string obx_1_IdObx;
    string obx_2_TipoDato; //String
    string obx_3_IdExamenSolicitado;
    string obx_4_IdDelResultado;    //1-7 segun catalogo
    string obx_5_ResultadoCualitativo; //Normal, anormal, etc ----- segun catalogo
    public Respuesta_obx_cualitativo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Obx_1_IdObx
    {
        get
        {
            return obx_1_IdObx;
        }

        set
        {
            obx_1_IdObx = value;
        }
    }

    public string Obx_2_TipoDato
    {
        get
        {
            return obx_2_TipoDato;
        }

        set
        {
            obx_2_TipoDato = value;
        }
    }

    public string Obx_3_IdExamenSolicitado
    {
        get
        {
            return obx_3_IdExamenSolicitado;
        }

        set
        {
            obx_3_IdExamenSolicitado = value;
        }
    }

    public string Obx_4_IdDelResultado
    {
        get
        {
            return obx_4_IdDelResultado;
        }

        set
        {
            obx_4_IdDelResultado = value;
        }
    }

    public string Obx_5_ResultadoCualitativo
    {
        get
        {
            return obx_5_ResultadoCualitativo;
        }

        set
        {
            obx_5_ResultadoCualitativo = value;
        }
    }
}