using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Repuesta_Orb
/// </summary>
public class Repuesta_Orb
{
    /// <summary>
    /// DEBE SER INCREMENTAL
    /// </summary>
    int obr_1_IDOBR;
    string obr_2_PlacerOrdeNumber;//(DetalleSoliciturd)

    string obr_4_1_Identifier;
    string obr_4_2_Text;
    string obr_4_3_NameOfCodingSystem; //L
    string obr_4_4_AlternateIdentifier;
    string obr_4_5_AlternateText;
    string obr_8_ObservationEndDateTime;
    string obr_10_CollectorIdentifier;
    string obr_16_1_IdNumber;
    string obr_16_2_FamilyName;
    string obr_22_ResultReptStatusChangeDateTime;
    string obr_24_DiagnosticServiceID; // HM= hemato, MB= microbiologia, ch=Bioquimica
    string obr_25_ResultStatus; // = F
    Respuesta_obx_cualitativo obx_Cualitativo;
    List<Respuesta_obx> listObxCuantitativos;


    public Repuesta_Orb()
    {
        Obx_Cualitativo = new Respuesta_obx_cualitativo();
        ListObxCuantitativos = new List<Respuesta_obx>();
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int Obr_1_IDOBR
    {
        get
        {
            return obr_1_IDOBR;
        }

        set
        {
            obr_1_IDOBR = value;
        }
    }

    public string Obr_2_PlacerOrdeNumber
    {
        get
        {
            return obr_2_PlacerOrdeNumber;
        }

        set
        {
            obr_2_PlacerOrdeNumber = value;
        }
    }

    public string Obr_4_1_Identifier
    {
        get
        {
            return obr_4_1_Identifier;
        }

        set
        {
            obr_4_1_Identifier = value;
        }
    }

    public string Obr_4_2_Text
    {
        get
        {
            return obr_4_2_Text;
        }

        set
        {
            obr_4_2_Text = value;
        }
    }

    public string Obr_4_3_NameOfCodingSystem
    {
        get
        {
            return obr_4_3_NameOfCodingSystem;
        }

        set
        {
            obr_4_3_NameOfCodingSystem = value;
        }
    }

    public string Obr_4_4_AlternateIdentifier
    {
        get
        {
            return obr_4_4_AlternateIdentifier;
        }

        set
        {
            obr_4_4_AlternateIdentifier = value;
        }
    }

    public string Obr_4_5_AlternateText
    {
        get
        {
            return obr_4_5_AlternateText;
        }

        set
        {
            obr_4_5_AlternateText = value;
        }
    }

    public string Obr_8_ObservationEndDateTime
    {
        get
        {
            return obr_8_ObservationEndDateTime;
        }

        set
        {
            obr_8_ObservationEndDateTime = value;
        }
    }

    public string Obr_10_CollectorIdentifier
    {
        get
        {
            return obr_10_CollectorIdentifier;
        }

        set
        {
            obr_10_CollectorIdentifier = value;
        }
    }

    public string Obr_16_1_IdNumber
    {
        get
        {
            return obr_16_1_IdNumber;
        }

        set
        {
            obr_16_1_IdNumber = value;
        }
    }

    public string Obr_16_2_FamilyName
    {
        get
        {
            return obr_16_2_FamilyName;
        }

        set
        {
            obr_16_2_FamilyName = value;
        }
    }

    public string Obr_22_ResultReptStatusChangeDateTime
    {
        get
        {
            return obr_22_ResultReptStatusChangeDateTime;
        }

        set
        {
            obr_22_ResultReptStatusChangeDateTime = value;
        }
    }

    public string Obr_24_DiagnosticServiceID
    {
        get
        {
            return obr_24_DiagnosticServiceID;
        }

        set
        {
            obr_24_DiagnosticServiceID = value;
        }
    }

    public string Obr_25_ResultStatus
    {
        get
        {
            return obr_25_ResultStatus;
        }

        set
        {
            obr_25_ResultStatus = value;
        }
    }

    public Respuesta_obx_cualitativo Obx_Cualitativo
    {
        get
        {
            return obx_Cualitativo;
        }

        set
        {
            obx_Cualitativo = value;
        }
    }

    public List<Respuesta_obx> ListObxCuantitativos
    {
        get
        {
            return listObxCuantitativos;
        }

        set
        {
            listObxCuantitativos = value;
        }
    }
}