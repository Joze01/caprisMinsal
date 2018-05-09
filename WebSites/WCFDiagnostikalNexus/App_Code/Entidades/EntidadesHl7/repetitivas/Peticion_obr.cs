using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Peticion_orb
/// </summary>
public class Peticion_obr
{
    private string obr1_idOBR;
    private string obr2_placerOrderNumber;
    //Universal Service Identifier.     obr4
    private string obr4_1_Identifier;
    private string obr4_2_text;
    private string obr4_4_AlternateIdentifier;
    //
    private string obr7_ObservationDate;
    private string obr15_specimenSource;
    private string obr16_orderingProvider;
    private string obr17_orderCallBackPhoneNumber;//PREGUNTAR A JULIO
    //Placer Fiel2 obr19> 
    private string obr19_1_id;
    private string obr19_2_text;

    //result interpreter
    private string obr32_1; //Parece una fecha

    public Peticion_obr()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Obr1_idOBR
    {
        get
        {
            return obr1_idOBR;
        }

        set
        {
            obr1_idOBR = value;
        }
    }

    public string Obr2_placerOrderNumber
    {
        get
        {
            return obr2_placerOrderNumber;
        }

        set
        {
            obr2_placerOrderNumber = value;
        }
    }

    public string Obr4_1_Identifier
    {
        get
        {
            return obr4_1_Identifier;
        }

        set
        {
            obr4_1_Identifier = value;
        }
    }

    public string Obr4_2_text
    {
        get
        {
            return obr4_2_text;
        }

        set
        {
            obr4_2_text = value;
        }
    }

    public string Obr4_4_AlternateIdentifier
    {
        get
        {
            return obr4_4_AlternateIdentifier;
        }

        set
        {
            obr4_4_AlternateIdentifier = value;
        }
    }

    public string Obr7_ObservationDate
    {
        get
        {
            return obr7_ObservationDate;
        }

        set
        {
            obr7_ObservationDate = value;
        }
    }

    public string Obr15_specimenSource
    {
        get
        {
            return obr15_specimenSource;
        }

        set
        {
            obr15_specimenSource = value;
        }
    }

    public string Obr16_orderingProvider
    {
        get
        {
            return obr16_orderingProvider;
        }

        set
        {
            obr16_orderingProvider = value;
        }
    }

    public string Obr17_orderCallBackPhoneNumber
    {
        get
        {
            return obr17_orderCallBackPhoneNumber;
        }

        set
        {
            obr17_orderCallBackPhoneNumber = value;
        }
    }

    public string Obr19_1_id
    {
        get
        {
            return obr19_1_id;
        }

        set
        {
            obr19_1_id = value;
        }
    }

    public string Obr19_2_text
    {
        get
        {
            return obr19_2_text;
        }

        set
        {
            obr19_2_text = value;
        }
    }

    public string Obr32_1
    {
        get
        {
            return obr32_1;
        }

        set
        {
            obr32_1 = value;
        }
    }
}