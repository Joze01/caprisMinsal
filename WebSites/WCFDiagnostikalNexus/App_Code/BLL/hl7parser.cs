using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NextLevelSeven;
using NextLevelSeven.Core;

/// <summary>
/// Descripción breve de hl7parser
/// </summary>
public class hl7parser
{
    hl7DBManager mangerDBhl7 = new hl7DBManager();
    openfDBManager managerDBopenf = new openfDBManager();
    public hl7parser()
    {
      
    }
    /// <summary>
    /// Mensaje de peticion entrante  y retorna el mensaje decodificado segun el template de la clase PeticionEntrante.
    /// </summary>
    /// <param name="peticion">String de entrada HL7</param>
    /// <returns>Retorna un objeto de tipo entidad de Peticion Entrante.</returns>
    public PeticionEntrante leerPeticion(string peticion)
    {
        PeticionEntrante nuevaPeticion = new PeticionEntrante();
        //System.Diagnostics.Debug.WriteLine("PETICION: " + peticion);
        var mensaje = NextLevelSeven.Core.Message.Build(peticion);
        var segmentos = mensaje[1];
        // first segment in a message (returns IElement)
        var mshSegment = mensaje[1];


        //Llenado de la cabecera MSH
        nuevaPeticion.Msh3_sendingApplication = mshSegment.Field(3).ToString();
        nuevaPeticion.Msh4_sendingFacility = mshSegment.Field(4).ToString();
        nuevaPeticion.Msh5_receivingApplication = mshSegment.Field(5).ToString();
        nuevaPeticion.Msh6_recivingFacilty = mshSegment.Field(6).Component(2).ToString();
        nuevaPeticion.Msh6_1_idSuministrasnte = mshSegment.Field(6).Component(1).ToString();
        nuevaPeticion.Msh7_dateTimeMessage = mshSegment.Field(7).ToString();
        nuevaPeticion.Msh9_1_messageCode = mshSegment.Field(9).Component(1).ToString();
        nuevaPeticion.Msh9_2_triggerEvent = mshSegment.Field(9).Component(2).ToString();
        nuevaPeticion.Msh10_messageControlID = mshSegment.Field(10).ToString();
        nuevaPeticion.Msh11_processingID = mshSegment.Field(11).ToString();
        nuevaPeticion.Msh12_versionId = mshSegment.Field(12).ToString();
        nuevaPeticion.Msh15_acceptAcknowledgmentType = mshSegment.Field(15).ToString();
        nuevaPeticion.Msh16_applicationAcknowledgmentType = mshSegment.Field(16).ToString();


        //Segmento PID (Paciente ID)
        var pidSegment = mensaje.Segments.OfType("PID").First();
        var pid1segment =
        nuevaPeticion.Pid1_pdi = pidSegment.Field(1).Component(1).ToString();
        nuevaPeticion.Pid3_1_idNumber = pidSegment.Field(3).Component(1).ToString();
        nuevaPeticion.Pid3_4_assigningAutority = pidSegment.Field(3).Component(4).ToString();
        nuevaPeticion.Pid5_1_familyName = pidSegment.Field(5).Component(1).ToString();
        nuevaPeticion.Pid5_2_givenName = pidSegment.Field(5).Component(2).ToString();
        nuevaPeticion.Pid5_3_secondName = pidSegment.Field(5).Component(3).ToString();
        nuevaPeticion.Pid7_datetimeBirth = pidSegment.Field(7).ToString();
        nuevaPeticion.Pid8_AdministrativeSex = pidSegment.Field(8).ToString();
        System.Diagnostics.Debug.WriteLine("Id paciente: " + nuevaPeticion.Pid3_1_idNumber);

        System.Diagnostics.Debug.WriteLine("Nombre Paciente: " + nuevaPeticion.Pid5_1_familyName + " " + nuevaPeticion.Pid5_2_givenName + " " + nuevaPeticion.Pid5_3_secondName);
     //Segmento PV (visita)
        var pv1Segment = mensaje.Segments.OfType("PV1").First();
        nuevaPeticion.Pv1_idNumber = pv1Segment.Field(1).ToString();
        nuevaPeticion.Pv2_patientClass = pv1Segment.Field(2).ToString();
        nuevaPeticion.Pv3_assignedPatientLocation = pv1Segment.Field(3).ToString();


        //Segmento ORC
        var orcSegment = mensaje.Segments.OfType("ORC").First();
        nuevaPeticion.Orc1_orderControl = orcSegment.Field(1).ToString();
        nuevaPeticion.Orc2_placerOrderNumer = orcSegment.Field(2).ToString();
        nuevaPeticion.Orc4_placerGroupNumer = orcSegment.Field(4).ToString();
        nuevaPeticion.Orc9_datimeTransaction = orcSegment.Field(9).ToString();
        nuevaPeticion.Orc12_1_idNumber = orcSegment.Field(12).Component(1).ToString();
        nuevaPeticion.Orc13_1_pointOfCare = orcSegment.Field(13).Component(1).ToString();
        nuevaPeticion.Orc17_1_identifier = orcSegment.Field(17).Component(1).ToString();
        nuevaPeticion.Orc17_2_text = orcSegment.Field(17).Component(2).ToString();
        nuevaPeticion.Orc21_1_orginizationName = orcSegment.Field(21).Component(1).ToString();
        nuevaPeticion.Orc21_3_IdNumber = orcSegment.Field(21).Component(3).ToString();

        //Segmento OBR
        var obrSegment = mensaje.Segments.OfType("OBR");

        foreach (var objeto in obrSegment)
        {
            Peticion_obr nuevoObr = new Peticion_obr();
            nuevoObr.Obr1_idOBR = objeto.Field(1).ToString();
            nuevoObr.Obr2_placerOrderNumber = objeto.Field(2).ToString();
            nuevoObr.Obr4_1_Identifier = objeto.Field(4).Component(1).ToString();
            nuevoObr.Obr4_2_text = objeto.Field(4).Component(2).ToString();
            nuevoObr.Obr4_4_AlternateIdentifier = objeto.Field(4).Component(4).ToString();
            nuevoObr.Obr7_ObservationDate = objeto.Field(7).ToString();
            nuevoObr.Obr10_CollectorIdentifier = objeto.Field(10).ToString();
            nuevoObr.Obr15_specimenSource = objeto.Field(15).ToString();
            nuevaPeticion.ListaORB.Add(nuevoObr);
        }

        //var splitsOBR = mensaje.SplitSegments("OBR",true);
        // System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos SPLITOBR:" + splitsOBR.OfType);
        // System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos OBR:"+obrSegment.Count());
        //System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos OBR:" + obrSegment.Count());


        //Lectura de muestras segmento SPM
        var spmSegment = mensaje.Segments.OfType("SPM");
        foreach (var objeto in spmSegment)
        {
            Peticion_spm nuevoSpm = new Peticion_spm();
            nuevoSpm.Spm1_id = objeto.Field(1).ToString();
            nuevoSpm.Spm2_specimenId = objeto.Field(2).ToString();
            nuevoSpm.Spm4_1_identifier = objeto.Field(4).Component(1).ToString();
            nuevoSpm.Spm4_2_text = objeto.Field(4).Component(2).ToString();
            nuevoSpm.Spm17_specimenCollectinDate = objeto.Field(17).ToString();
            nuevaPeticion.Listaspm.Add(nuevoSpm);
        }
        
        return nuevaPeticion;
    }


    public Boolean getPeticion(string peticion)
    {
       
        PeticionEntrante nuevaPeticion = new PeticionEntrante();
        //System.Diagnostics.Debug.WriteLine("PETICION: " + peticion);
        var mensaje = NextLevelSeven.Core.Message.Build(peticion);
        var segmentos = mensaje[1];
        // first segment in a message (returns IElement)
        var mshSegment = mensaje[1];

      
        //Llenado de la cabecera MSH
        nuevaPeticion.Msh3_sendingApplication = mshSegment.Field(3).ToString();
        nuevaPeticion.Msh4_sendingFacility = mshSegment.Field(4).ToString();
        nuevaPeticion.Msh5_receivingApplication = mshSegment.Field(5).ToString();
        nuevaPeticion.Msh6_recivingFacilty = mshSegment.Field(6).ToString();
        nuevaPeticion.Msh6_1_idSuministrasnte = mshSegment.Field(6).Component(1).ToString();
        nuevaPeticion.Msh7_dateTimeMessage = mshSegment.Field(7).ToString();
        nuevaPeticion.Msh9_1_messageCode = mshSegment.Field(9).Component(1).ToString();
        nuevaPeticion.Msh9_2_triggerEvent = mshSegment.Field(9).Component(2).ToString();
        nuevaPeticion.Msh10_messageControlID = mshSegment.Field(10).ToString();
        nuevaPeticion.Msh11_processingID = mshSegment.Field(11).ToString();
        nuevaPeticion.Msh12_versionId = mshSegment.Field(12).ToString();
        nuevaPeticion.Msh15_acceptAcknowledgmentType = mshSegment.Field(15).ToString();
        nuevaPeticion.Msh16_applicationAcknowledgmentType = mshSegment.Field(16).ToString();

        
        //Segmento PID (Paciente ID)
        var pidSegment = mensaje.Segments.OfType("PID").First();
        var pid1segment =
        nuevaPeticion.Pid1_pdi = pidSegment.Field(1).Component(1).ToString();
        nuevaPeticion.Pid3_1_idNumber = pidSegment.Field(3).Component(1).ToString();
        nuevaPeticion.Pid3_4_assigningAutority = pidSegment.Field(3).Component(4).ToString();
        nuevaPeticion.Pid5_1_familyName = pidSegment.Field(5).Component(1).ToString();
        nuevaPeticion.Pid5_2_givenName= pidSegment.Field(5).Component(2).ToString();
        nuevaPeticion.Pid5_3_secondName= pidSegment.Field(5).Component(3).ToString();
        nuevaPeticion.Pid7_datetimeBirth = pidSegment.Field(7).ToString();
        nuevaPeticion.Pid8_AdministrativeSex= pidSegment.Field(8).ToString();
        System.Diagnostics.Debug.WriteLine("Id paciente: " + nuevaPeticion.Pid3_1_idNumber);

        System.Diagnostics.Debug.WriteLine("Nombre Paciente: " + nuevaPeticion.Pid5_1_familyName+" "+nuevaPeticion.Pid5_2_givenName+" "+nuevaPeticion.Pid5_3_secondName);
//        System.Diagnostics.Debug.WriteLine("Id paciente: " + nuevaPeticion.Pid3_1_idNumber);
        //Segmento PV (visita)
        var pv1Segment = mensaje.Segments.OfType("PV1").First();
        nuevaPeticion.Pv1_idNumber = pv1Segment.Field(1).ToString();
        nuevaPeticion.Pv2_patientClass = pv1Segment.Field(2).ToString();
        nuevaPeticion.Pv3_assignedPatientLocation = pv1Segment.Field(3).ToString();


        //Segmento ORC
        var orcSegment = mensaje.Segments.OfType("ORC").First();
        nuevaPeticion.Orc1_orderControl = orcSegment.Field(1).ToString();
        nuevaPeticion.Orc2_placerOrderNumer = orcSegment.Field(2).ToString();
        nuevaPeticion.Orc4_placerGroupNumer = orcSegment.Field(4).ToString();
        nuevaPeticion.Orc9_datimeTransaction = orcSegment.Field(9).ToString();
        nuevaPeticion.Orc12_1_idNumber = orcSegment.Field(12).Component(1).ToString();
        nuevaPeticion.Orc13_1_pointOfCare = orcSegment.Field(13).Component(1).ToString();
        nuevaPeticion.Orc17_1_identifier = orcSegment.Field(17).Component(1).ToString();
        nuevaPeticion.Orc17_2_text = orcSegment.Field(17).Component(2).ToString();
        nuevaPeticion.Orc21_1_orginizationName = orcSegment.Field(21).Component(1).ToString();
        nuevaPeticion.Orc21_3_IdNumber = orcSegment.Field(21).Component(3).ToString();

        //Segmento OBR
        var obrSegment = mensaje.Segments.OfType("OBR");
        
        foreach (var objeto in obrSegment) {
            Peticion_obr nuevoObr = new Peticion_obr();
            nuevoObr.Obr1_idOBR = objeto.Field(1).ToString();
            nuevoObr.Obr2_placerOrderNumber = objeto.Field(2).ToString();
            nuevoObr.Obr4_1_Identifier = objeto.Field(4).Component(1).ToString();
            nuevoObr.Obr4_2_text = objeto.Field(4).Component(2).ToString();
            nuevoObr.Obr4_4_AlternateIdentifier = objeto.Field(4).Component(4).ToString();
            nuevoObr.Obr7_ObservationDate = objeto.Field(7).ToString();
            nuevoObr.Obr10_CollectorIdentifier = objeto.Field(10).ToString();
            nuevoObr.Obr15_specimenSource = objeto.Field(15).ToString();
            nuevaPeticion.ListaORB.Add(nuevoObr);
        }

        //var splitsOBR = mensaje.SplitSegments("OBR",true);
        // System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos SPLITOBR:" + splitsOBR.OfType);
        // System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos OBR:"+obrSegment.Count());
        //System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos OBR:" + obrSegment.Count());


        //Lectura de muestras segmento SPM
        var spmSegment = mensaje.Segments.OfType("SPM");
        foreach (var objeto in spmSegment)
        {
            Peticion_spm nuevoSpm = new Peticion_spm();
            nuevoSpm.Spm1_id = objeto.Field(1).ToString();
            nuevoSpm.Spm2_specimenId = objeto.Field(2).ToString();
            nuevoSpm.Spm4_1_identifier = objeto.Field(4).Component(1).ToString();
            nuevoSpm.Spm4_2_text = objeto.Field(4).Component(2).ToString();
            nuevoSpm.Spm17_specimenCollectinDate = objeto.Field(17).ToString();
            nuevaPeticion.Listaspm.Add(nuevoSpm);
        }

        //  System.Diagnostics.Debug.WriteLine("Cantidad segmegmentos SPM:" + spmSegment.Count());
        mangerDBhl7.guardarPeticion(peticion, obrSegment.Count(),long.Parse(nuevaPeticion.Orc4_placerGroupNumer), nuevaPeticion.Orc2_placerOrderNumer,long.Parse(nuevaPeticion.Orc4_placerGroupNumer)); //guardar peticion en la base del servicio web
        //foreach (Peticion_spm)
        if (managerDBopenf.nuevaPeticion(nuevaPeticion))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Funcion en desuso.
    /// </summary>
    /// <param name="mensaje"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    public Boolean isValid(string mensaje, string md5)
    {
       
        return true;
    }
    


   


    
}