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
    private string obr10_CollectorIdentifier;
    private string obr15_specimenSource;

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

    public string Obr10_CollectorIdentifier
    {
        get
        {
            return obr10_CollectorIdentifier;
        }

        set
        {
            obr10_CollectorIdentifier = value;
        }
    }
}
