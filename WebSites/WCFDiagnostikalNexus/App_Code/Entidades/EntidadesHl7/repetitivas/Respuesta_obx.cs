using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Respuesta_obx
/// </summary>
public class Respuesta_obx
{
    int obx_1_setID;
    string obx_2_ValueType; //STR , NM
    string obx_3_ObservationIdentifier;
    string obx_3_1_Identifier;
    string obx_3_2_Text;
    string obx_4_ObservationsubId;
    string obx_5_ObservationValue;
    string obx_6_Units;
    string obx_7_ReferenceRange;
    string obx_11_ObservationResultStatus;
    string obx_14_DatimeTimeObservation;

    public Respuesta_obx()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int Obx_1_setID
    {
        get
        {
            return obx_1_setID;
        }

        set
        {
            obx_1_setID = value;
        }
    }

    public string Obx_2_ValueType
    {
        get
        {
            return obx_2_ValueType;
        }

        set
        {
            obx_2_ValueType = value;
        }
    }

    public string Obx_3_ObservationIdentifier
    {
        get
        {
            return obx_3_ObservationIdentifier;
        }

        set
        {
            obx_3_ObservationIdentifier = value;
        }
    }

    public string Obx_4_ObservationsubId
    {
        get
        {
            return obx_4_ObservationsubId;
        }

        set
        {
            obx_4_ObservationsubId = value;
        }
    }

    public string Obx_5_ObservationValue
    {
        get
        {
            return obx_5_ObservationValue;
        }

        set
        {
            obx_5_ObservationValue = value;
        }
    }
}