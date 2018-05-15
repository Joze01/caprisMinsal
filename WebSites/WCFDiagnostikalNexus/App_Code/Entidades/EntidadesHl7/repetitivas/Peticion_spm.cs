using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Peticion_spm
/// </summary>
public class Peticion_spm
{
    private string spm1_id;
    private string spm2_specimenId;
    private string spm4_1_identifier;
    private string spm4_2_text;
    private string spm17_specimenCollectinDate;



    public Peticion_spm()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Spm1_id
    {
        get
        {
            return spm1_id;
        }

        set
        {
            spm1_id = value;
        }
    }


    public string Spm4_1_identifier
    {
        get
        {
            return spm4_1_identifier;
        }

        set
        {
            spm4_1_identifier = value;
        }
    }

    public string Spm4_2_text
    {
        get
        {
            return spm4_2_text;
        }

        set
        {
            spm4_2_text = value;
        }
    }

    public string Spm17_specimenCollectinDate
    {
        get
        {
            return spm17_specimenCollectinDate;
        }

        set
        {
            spm17_specimenCollectinDate = value;
        }
    }

    public string Spm2_specimenId
    {
        get
        {
            return spm2_specimenId;
        }

        set
        {
            spm2_specimenId = value;
        }
    }
}