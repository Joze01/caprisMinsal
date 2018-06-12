using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Repuesta
/// </summary>
public class Repuesta
{
    string msh_1_fielSeparador;
    string msh_2_EncodeingCaracters;
    string msh_3_sendingApplication;
    string msh_4_1_namespaceId;
    string msh_4_2_UniversalID;
    string msh_5_ReceivingApplication; //SIAPS
    string msh_6_ReceivingFacility;
    string msh_7_DateTimeOfMessage;
    string msh_9_1_MessageCode; //OUL
    string msh_9_2_TriggerEvent; //R22
    string msh_10_MessageControlId;
    string msh_11_ProcessingID;
    string msh_12_VersionId;
    string msh_15_acceptAcknowledgemeType;//AL
    string msh_16_ApplicationAcknowledgmeType;//AL

    string orc_1_codigoDeControl;
    string orc_2_IdSolicitudSiaps;
    string orc_5_EstatusOrden;
    string orc_9_FechaDeEnvio;
    string orc_12_1_CodigoProfesional;
    string orc_12_2_NombreProfesional;




    List<Repuesta_Orb> listadDeRespuestas = new List<Repuesta_Orb>();

    public Repuesta()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Msh_1_fielSeparador
    {
        get
        {
            return msh_1_fielSeparador;
        }

        set
        {
            msh_1_fielSeparador = value;
        }
    }

    public string Msh_2_EncodeingCaracters
    {
        get
        {
            return msh_2_EncodeingCaracters;
        }

        set
        {
            msh_2_EncodeingCaracters = value;
        }
    }

    public string Msh_3_sendingApplication
    {
        get
        {
            return msh_3_sendingApplication;
        }

        set
        {
            msh_3_sendingApplication = value;
        }
    }

    public string Msh_4_1_namespaceId
    {
        get
        {
            return msh_4_1_namespaceId;
        }

        set
        {
            msh_4_1_namespaceId = value;
        }
    }

    public string Msh_4_2_UniversalID
    {
        get
        {
            return msh_4_2_UniversalID;
        }

        set
        {
            msh_4_2_UniversalID = value;
        }
    }

    public string Msh_5_ReceivingApplication
    {
        get
        {
            return msh_5_ReceivingApplication;
        }

        set
        {
            msh_5_ReceivingApplication = value;
        }
    }

    public string Msh_6_ReceivingFacility
    {
        get
        {
            return msh_6_ReceivingFacility;
        }

        set
        {
            msh_6_ReceivingFacility = value;
        }
    }

    public string Msh_7_DateTimeOfMessage
    {
        get
        {
            return msh_7_DateTimeOfMessage;
        }

        set
        {
            msh_7_DateTimeOfMessage = value;
        }
    }

    public string Msh_9_1_MessageCode
    {
        get
        {
            return msh_9_1_MessageCode;
        }

        set
        {
            msh_9_1_MessageCode = value;
        }
    }

    public string Msh_9_2_TriggerEvent
    {
        get
        {
            return msh_9_2_TriggerEvent;
        }

        set
        {
            msh_9_2_TriggerEvent = value;
        }
    }

    public string Msh_10_MessageControlId
    {
        get
        {
            return msh_10_MessageControlId;
        }

        set
        {
            msh_10_MessageControlId = value;
        }
    }

    public string Msh_11_ProcessingID
    {
        get
        {
            return msh_11_ProcessingID;
        }

        set
        {
            msh_11_ProcessingID = value;
        }
    }

    public string Msh_12_VersionId
    {
        get
        {
            return msh_12_VersionId;
        }

        set
        {
            msh_12_VersionId = value;
        }
    }

    public string Msh_15_acceptAcknowledgemeType
    {
        get
        {
            return msh_15_acceptAcknowledgemeType;
        }

        set
        {
            msh_15_acceptAcknowledgemeType = value;
        }
    }

    public string Msh_16_ApplicationAcknowledgmeType
    {
        get
        {
            return msh_16_ApplicationAcknowledgmeType;
        }

        set
        {
            msh_16_ApplicationAcknowledgmeType = value;
        }
    }

    public List<Repuesta_Orb> ListadDeRespuestas
    {
        get
        {
            return listadDeRespuestas;
        }

        set
        {
            listadDeRespuestas = value;
        }
    }

    public string Orc_1_codigoDeControl
    {
        get
        {
            return orc_1_codigoDeControl;
        }

        set
        {
            orc_1_codigoDeControl = value;
        }
    }

    public string Orc_2_IdSolicitudSiaps
    {
        get
        {
            return orc_2_IdSolicitudSiaps;
        }

        set
        {
            orc_2_IdSolicitudSiaps = value;
        }
    }

    public string Orc_5_EstatusOrden
    {
        get
        {
            return orc_5_EstatusOrden;
        }

        set
        {
            orc_5_EstatusOrden = value;
        }
    }

    public string Orc_9_FechaDeEnvio
    {
        get
        {
            return orc_9_FechaDeEnvio;
        }

        set
        {
            orc_9_FechaDeEnvio = value;
        }
    }

    public string Orc_12_1_CodigoProfesional
    {
        get
        {
            return orc_12_1_CodigoProfesional;
        }

        set
        {
            orc_12_1_CodigoProfesional = value;
        }
    }

    public string Orc_12_2_NombreProfesional
    {
        get
        {
            return orc_12_2_NombreProfesional;
        }

        set
        {
            orc_12_2_NombreProfesional = value;
        }
    }
}