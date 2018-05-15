using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de peticionHematologia
/// </summary>
public class PeticionEntrante
{
    /// <summary>
    /// Generales de la trama msh
    /// </summary>
    private string msh3_sendingApplication; //aplicacion que envia
    private string msh4_sendingFacility; //Lugar de procedencia de peticion
    private string msh5_receivingApplication;  //Aplicacion que recibe
    private string msh6_recivingFacilty; //
    private string msh7_dateTimeMessage;
    //Tipo de peticion cabecera segun la cabecera msh9> informacion del tipo 
    private string msh9_1_messageCode; //deberian ser OML ya que todos son de laboratorio.
    private string msh9_2_triggerEvent; //Poro lo general es O21 ya que es orden de laboratio. 
    private string msh10_messageControlID;//ID de control para el mensaje.    CORRESPONDE A LA PLANTILLA
    private string msh11_processingID;//D es debugging
    private string msh12_versionId; //2.5.1 
    private string msh15_acceptAcknowledgmentType; //aceptarreconocimiento de tipo AL= siempre
    private string msh16_applicationAcknowledgmentType; //reconocimineto de aplicaicon AL=always

    /// <summary>
    /// Segmento del paciente pid
    /// </summary>
    private string pid1_pdi;//Id de PDI (interno de la peticion)
    //PID3> informacion de paciente
    private string pid3_1_idNumber;
    private string pid3_4_assigningAutority;
    private string pid5_1_familyName;//Apellidos
    private string pid5_2_givenName;//nombre
    private string pid5_3_secondName;//
    //datos extras pid
    private string pid7_datetimeBirth;//fecha de nacimiento
    private string pid8_AdministrativeSex; //CONSULTAR A JULIO llega  1 y espera letras. 

    /// <summary>
    /// Segmento de la visita.
    /// </summary>
    private string pv1_idNumber;
    private string pv2_patientClass;
    private string pv3_assignedPatientLocation;


    /// <summary>
    /// Common Order 
    /// </summary>
    private string orc1_orderControl; //NW means new and Xo means  change order
    private string orc2_placerOrderNumer;
    private string orc4_placerGroupNumer;
    private string orc9_datimeTransaction;
    //Informacion para Ordering Provider orc-12
    private string orc12_1_idNumber;//id provider
    private string orc12_2_familyName;//apelido
    private string orc12_3_givenName;//nombres
    //Informacion de ingreso. orc_13
    private string orc13_1_pointOfCare;
    private string orc13_9_locationDescription;
    //Informacion de la organizacion de ingreso;
    private string orc17_1_identifier;
    private string orc17_2_text;
    //Informacion del lugar de procedencia. orc21
    private string orc21_1_orginizationName;
    private string orc21_3_IdNumber;


    /// <summary>
    /// Peticion de Observacion OBR
    /// </summary>
    private List<Peticion_obr> listaORB;

    /// <summary>
    /// Peticion de Muestras SPM
    /// </summary>
    private List<Peticion_spm> listaspm;
    public PeticionEntrante()
    {
        listaORB = new List<Peticion_obr>();
        listaspm = new List<Peticion_spm>();
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string Msh3_sendingApplication
    {
        get
        {
            return msh3_sendingApplication;
        }

        set
        {
            msh3_sendingApplication = value;
        }
    }

    public string Msh4_sendingFacility
    {
        get
        {
            return msh4_sendingFacility;
        }

        set
        {
            msh4_sendingFacility = value;
        }
    }

    public string Msh5_receivingApplication
    {
        get
        {
            return msh5_receivingApplication;
        }

        set
        {
            msh5_receivingApplication = value;
        }
    }

    public string Msh6_recivingFacilty
    {
        get
        {
            return msh6_recivingFacilty;
        }

        set
        {
            msh6_recivingFacilty = value;
        }
    }

    public string Msh7_dateTimeMessage
    {
        get
        {
            return msh7_dateTimeMessage;
        }

        set
        {
            msh7_dateTimeMessage = value;
        }
    }

    public string Msh9_1_messageCode
    {
        get
        {
            return msh9_1_messageCode;
        }

        set
        {
            msh9_1_messageCode = value;
        }
    }

    public string Msh9_2_triggerEvent
    {
        get
        {
            return msh9_2_triggerEvent;
        }

        set
        {
            msh9_2_triggerEvent = value;
        }
    }

    public string Msh10_messageControlID
    {
        get
        {
            return msh10_messageControlID;
        }

        set
        {
            msh10_messageControlID = value;
        }
    }

    public string Msh11_processingID
    {
        get
        {
            return msh11_processingID;
        }

        set
        {
            msh11_processingID = value;
        }
    }

    public string Msh12_versionId
    {
        get
        {
            return msh12_versionId;
        }

        set
        {
            msh12_versionId = value;
        }
    }

    public string Msh15_acceptAcknowledgmentType
    {
        get
        {
            return msh15_acceptAcknowledgmentType;
        }

        set
        {
            msh15_acceptAcknowledgmentType = value;
        }
    }

    public string Msh16_applicationAcknowledgmentType
    {
        get
        {
            return msh16_applicationAcknowledgmentType;
        }

        set
        {
            msh16_applicationAcknowledgmentType = value;
        }
    }

    public string Pid1_pdi
    {
        get
        {
            return pid1_pdi;
        }

        set
        {
            pid1_pdi = value;
        }
    }

    public string Pid3_1_idNumber
    {
        get
        {
            return pid3_1_idNumber;
        }

        set
        {
            pid3_1_idNumber = value;
        }
    }

    public string Pid3_4_assigningAutority
    {
        get
        {
            return pid3_4_assigningAutority;
        }

        set
        {
            pid3_4_assigningAutority = value;
        }
    }

    public string Pid5_1_familyName
    {
        get
        {
            return pid5_1_familyName;
        }

        set
        {
            pid5_1_familyName = value;
        }
    }

    public string Pid5_2_givenName
    {
        get
        {
            return pid5_2_givenName;
        }

        set
        {
            pid5_2_givenName = value;
        }
    }

    public string Pid5_3_secondName
    {
        get
        {
            return pid5_3_secondName;
        }

        set
        {
            pid5_3_secondName = value;
        }
    }

    public string Pid7_datetimeBirth
    {
        get
        {
            return pid7_datetimeBirth;
        }

        set
        {
            pid7_datetimeBirth = value;
        }
    }

    public string Pid8_AdministrativeSex
    {
        get
        {
            return pid8_AdministrativeSex;
        }

        set
        {
            pid8_AdministrativeSex = value;
        }
    }

    public string Orc1_orderControl
    {
        get
        {
            return orc1_orderControl;
        }

        set
        {
            orc1_orderControl = value;
        }
    }

    public string Orc2_placerOrderNumer
    {
        get
        {
            return orc2_placerOrderNumer;
        }

        set
        {
            orc2_placerOrderNumer = value;
        }
    }

    public string Orc4_placerGroupNumer
    {
        get
        {
            return orc4_placerGroupNumer;
        }

        set
        {
            orc4_placerGroupNumer = value;
        }
    }

    public string Orc9_datimeTransaction
    {
        get
        {
            return orc9_datimeTransaction;
        }

        set
        {
            orc9_datimeTransaction = value;
        }
    }

    public string Orc12_1_idNumber
    {
        get
        {
            return orc12_1_idNumber;
        }

        set
        {
            orc12_1_idNumber = value;
        }
    }

    public string Orc12_2_familyName
    {
        get
        {
            return orc12_2_familyName;
        }

        set
        {
            orc12_2_familyName = value;
        }
    }

    public string Orc12_3_givenName
    {
        get
        {
            return orc12_3_givenName;
        }

        set
        {
            orc12_3_givenName = value;
        }
    }

    public string Orc13_1_pointOfCare
    {
        get
        {
            return orc13_1_pointOfCare;
        }

        set
        {
            orc13_1_pointOfCare = value;
        }
    }

    public string Orc13_9_locationDescription
    {
        get
        {
            return orc13_9_locationDescription;
        }

        set
        {
            orc13_9_locationDescription = value;
        }
    }

    public string Orc17_1_identifier
    {
        get
        {
            return orc17_1_identifier;
        }

        set
        {
            orc17_1_identifier = value;
        }
    }

    public string Orc17_2_text
    {
        get
        {
            return orc17_2_text;
        }

        set
        {
            orc17_2_text = value;
        }
    }

    public string Orc21_1_orginizationName
    {
        get
        {
            return orc21_1_orginizationName;
        }

        set
        {
            orc21_1_orginizationName = value;
        }
    }

    public string Orc21_3_IdNumber
    {
        get
        {
            return orc21_3_IdNumber;
        }

        set
        {
            orc21_3_IdNumber = value;
        }
    }



    public string Pv1_idNumber
    {
        get
        {
            return pv1_idNumber;
        }

        set
        {
            pv1_idNumber = value;
        }
    }

    public string Pv2_patientClass
    {
        get
        {
            return pv2_patientClass;
        }

        set
        {
            pv2_patientClass = value;
        }
    }

    public string Pv3_assignedPatientLocation
    {
        get
        {
            return pv3_assignedPatientLocation;
        }

        set
        {
            pv3_assignedPatientLocation = value;
        }
    }

    public List<Peticion_obr> ListaORB
    {
        get
        {
            return listaORB;
        }

        set
        {
            listaORB = value;
        }
    }

    public List<Peticion_spm> Listaspm
    {
        get
        {
            return listaspm;
        }

        set
        {
            listaspm = value;
        }
    }
}