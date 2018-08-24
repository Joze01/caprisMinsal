using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Util
/// </summary>
public class Util
{
    public Util()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public List<string> getRangos(int edadDias, string lectura)
    {
        List<string> comentarios = new List<string>();
        string id="";
        string valorcomentario="";
        float rangoUtilMax = 0;
        float rangoUtilMin = 0;
        

        comentarios.Add(id);
        comentarios.Add(valorcomentario);
        comentarios.Add(rangoUtilMin.ToString());
        comentarios.Add(rangoUtilMax.ToString());
        return comentarios;
    }

    public void generarRespuestas() {
        
        openfDBManager managerDBOpenf = new openfDBManager();
        hl7DBManager managerDBOhl7 = new hl7DBManager();
        Repuesta resultadoRepuesta = new Repuesta();
        hl7parser parseadorHl7 = new hl7parser();
        Util utilidades = new Util();
        DateTime now = DateTime.Now;
        string respuesta = "";
        List<transacciones> listaPendientes =new List<transacciones>();
        List<resultview> resultadosObtenido = new List<resultview>();
        Boolean orcCargado = false;
        //now.ToString("yyyyMMddHHmm"); // case sensitive
        resultadoRepuesta.Msh_1_fielSeparador = "|";
        resultadoRepuesta.Msh_2_EncodeingCaracters = @"^~\&";
        resultadoRepuesta.Msh_3_sendingApplication = "Nexus Web Service";
        resultadoRepuesta.Msh_4_1_namespaceId = "7";
        resultadoRepuesta.Msh_4_2_UniversalID = "DIAGNOSTIKAL CAPRIS";
        resultadoRepuesta.Msh_5_ReceivingApplication = "SIAP";
        resultadoRepuesta.Msh_6_ReceivingFacility = "MINSAL";
        resultadoRepuesta.Msh_7_DateTimeOfMessage = now.ToString("yyyyMMddHHmm");
        resultadoRepuesta.Msh_9_1_MessageCode = "OUL";
        resultadoRepuesta.Msh_9_2_TriggerEvent = "R22";
        resultadoRepuesta.Msh_10_MessageControlId = "3";
        resultadoRepuesta.Msh_11_ProcessingID = "D";
        resultadoRepuesta.Msh_12_VersionId = "2.5.1";
        resultadoRepuesta.Msh_15_acceptAcknowledgemeType = "AL";
        resultadoRepuesta.Msh_16_ApplicationAcknowledgmeType = "AL";


        listaPendientes = managerDBOhl7.obtenerPendientes();
        
        foreach (transacciones tranIncompleta in listaPendientes ) {
            int contadorObr = 1;
            orcCargado = false;
            respuesta = "";
            resultadosObtenido = new List<resultview>();
            resultadosObtenido = managerDBOpenf.getResultados(int.Parse(tranIncompleta.Siapsid));
            PeticionEntrante peticionActual = new PeticionEntrante();
            peticionActual = parseadorHl7.leerPeticion(tranIncompleta.Peticion);



            if (resultadosObtenido.Count()>0) { 
                resultadoRepuesta.Orc_1_codigoDeControl = "NW";
                resultadoRepuesta.Orc_2_IdSolicitudSiaps = tranIncompleta.Siapsid;
                resultadoRepuesta.Orc_5_EstatusOrden = "CM";
                resultadoRepuesta.Orc_9_FechaDeEnvio = now.ToString("yyyyMMddHHmm");


                //resultadosObtenido = managerDBOpenf.getResultados(int.Parse(tranIncompleta.Respuesta));

                foreach (Peticion_obr obrPeticion in peticionActual.ListaORB)
                {
                    List<resultview> ResultadosObx = new List<resultview>();
                    ResultadosObx = managerDBOpenf.getResultadosByEstudio(int.Parse(tranIncompleta.Siapsid), obrPeticion.Obr4_4_AlternateIdentifier);
                    if (ResultadosObx.Count() > 0)
                    {
                        string examenIdStudio = ResultadosObx[0].Estudio;
                        string TipoExamenPlantilla = ResultadosObx[0].Plantilla;


                        Repuesta_Orb nuevaObrResult = new Repuesta_Orb();
                        nuevaObrResult.Obr_1_IDOBR = contadorObr;
                        nuevaObrResult.Obr_2_PlacerOrdeNumber = obrPeticion.Obr2_placerOrderNumber;
                        nuevaObrResult.Obr_4_1_Identifier = managerDBOpenf.getExamenId(examenIdStudio);
                        nuevaObrResult.Obr_4_2_Text = managerDBOpenf.getExamenName(examenIdStudio);
                        nuevaObrResult.Obr_4_3_NameOfCodingSystem = "L";
                        if (TipoExamenPlantilla == "A")
                        {
                            nuevaObrResult.Obr_4_4_AlternateIdentifier = managerDBOpenf.getAbreviado(ResultadosObx[0].Estudio);
                        }
                        else
                        {
                            nuevaObrResult.Obr_4_4_AlternateIdentifier = nuevaObrResult.Obr_4_2_Text.Substring(0, 2);
                        }
                        nuevaObrResult.Obr_4_5_AlternateText = nuevaObrResult.Obr_4_2_Text;

                        nuevaObrResult.Obr_8_ObservationEndDateTime = ResultadosObx[0].Fecha.ToString();

                        nuevaObrResult.Obr_10_CollectorIdentifier = obrPeticion.Obr10_CollectorIdentifier;
                        nuevaObrResult.Obr_16_1_IdNumber = peticionActual.Orc12_1_idNumber;
                        nuevaObrResult.Obr_16_2_FamilyName = peticionActual.Orc12_2_familyName;
                        nuevaObrResult.Obr_22_ResultReptStatusChangeDateTime = now.ToString("yyyyMMddHHmm");
                        nuevaObrResult.Obr_24_DiagnosticServiceID = managerDBOpenf.getTipoExame(ResultadosObx[0].Estudio);//REVISAR ESTOOOOOOOOOOOOOOOO  *Revisado
                        nuevaObrResult.Obr_25_ResultStatus = "F";

                        if (!orcCargado)
                        {
                            resultadoRepuesta.Orc_12_1_CodigoProfesional = ResultadosObx[0].Responsable;
                            resultadoRepuesta.Orc_12_2_NombreProfesional = managerDBOpenf.getEncargadoName(ResultadosObx[0].Responsable);
                            string orc = @"ORC|" + resultadoRepuesta.Orc_1_codigoDeControl + "|" + resultadoRepuesta.Orc_2_IdSolicitudSiaps + "|||" + resultadoRepuesta.Orc_5_EstatusOrden + "||||" + resultadoRepuesta.Orc_9_FechaDeEnvio + "|||" + resultadoRepuesta.Orc_12_1_CodigoProfesional + "^" + resultadoRepuesta.Orc_12_2_NombreProfesional + "_z";

                            resultadoRepuesta.Msh_1_fielSeparador = "|";
                            resultadoRepuesta.Msh_2_EncodeingCaracters = @"^~\&";
                            resultadoRepuesta.Msh_3_sendingApplication = "Nexus Web Service";
                            resultadoRepuesta.Msh_4_1_namespaceId = "7";
                            resultadoRepuesta.Msh_4_2_UniversalID = "DIAGNOSTIKAL CAPRIS";
                            resultadoRepuesta.Msh_5_ReceivingApplication = "SIAP";
                            resultadoRepuesta.Msh_6_ReceivingFacility = "MINSAL";
                            resultadoRepuesta.Msh_7_DateTimeOfMessage = now.ToString("yyyyMMddHHmm");
                            resultadoRepuesta.Msh_9_1_MessageCode = "OUL";
                            resultadoRepuesta.Msh_9_2_TriggerEvent = "R22";
                            resultadoRepuesta.Msh_10_MessageControlId = "3";
                            resultadoRepuesta.Msh_11_ProcessingID = "D";
                            resultadoRepuesta.Msh_12_VersionId = "2.5.1";
                            resultadoRepuesta.Msh_15_acceptAcknowledgemeType = "AL";
                            resultadoRepuesta.Msh_16_ApplicationAcknowledgmeType = "AL";

                            resultadoRepuesta.Msh_10_MessageControlId = peticionActual.Msh10_messageControlID;
                            resultadoRepuesta.Msh_4_1_namespaceId = peticionActual.Msh6_1_idSuministrasnte;
                            string msh = @"MSH" + resultadoRepuesta.Msh_1_fielSeparador + "^~\\u005Cu005C&|" + resultadoRepuesta.Msh_3_sendingApplication + "|" + resultadoRepuesta.Msh_4_1_namespaceId + "^" + resultadoRepuesta.Msh_4_2_UniversalID + "|" + resultadoRepuesta.Msh_5_ReceivingApplication + "|" + resultadoRepuesta.Msh_6_ReceivingFacility + "|" + resultadoRepuesta.Msh_7_DateTimeOfMessage + "||" + resultadoRepuesta.Msh_9_1_MessageCode + "^" + resultadoRepuesta.Msh_9_2_TriggerEvent + "|" + resultadoRepuesta.Msh_10_MessageControlId + "|" + resultadoRepuesta.Msh_11_ProcessingID + "|" + resultadoRepuesta.Msh_12_VersionId + "|||" + resultadoRepuesta.Msh_15_acceptAcknowledgemeType + "|" + resultadoRepuesta.Msh_16_ApplicationAcknowledgmeType + "_z";



                            respuesta = msh + orc;
                            orcCargado = true;
                        }

                        //SECCIONES DE LOS OBX
                        Respuesta_obx_cualitativo nuevaObxCualitativo = new Respuesta_obx_cualitativo();
                        nuevaObxCualitativo.Obx_1_IdObx = "1";
                        nuevaObxCualitativo.Obx_2_TipoDato = "ST";
                        nuevaObxCualitativo.Obx_3_IdExamenSolicitado = nuevaObrResult.Obr_4_1_Identifier;

                        int contadorObx = 2;

                        if (TipoExamenPlantilla == "A")
                        {
                            foreach (resultview resultadoAImprimir in ResultadosObx)
                            {
                                Respuesta_obx nuevoObxCuantitativo = new Respuesta_obx();
                                nuevoObxCuantitativo.Obx_1_ObxId = contadorObx.ToString();
                                nuevoObxCuantitativo.Obx_2_ValueType = "NM";
                                nuevoObxCuantitativo.Obx_3_1_Identifier = nuevaObrResult.Obr_4_1_Identifier;
                                nuevoObxCuantitativo.Obx_3_2_text = nuevaObrResult.Obr_4_2_Text;
                                nuevoObxCuantitativo.Obx_4_observationSubid = "Instrumento";
                                nuevoObxCuantitativo.Obx_6_units = managerDBOpenf.getUnitstest(resultadoAImprimir.Parametro);
                                string fechaNacimiento = peticionActual.Pid7_datetimeBirth;
                                DateTime nacimiento = DateTime.ParseExact(fechaNacimiento + " 00:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                DateTime fecha_Actual = DateTime.Now;
                                int edadDias = (fecha_Actual - nacimiento).Days;
                                float resultadoValue = 0;
                                string PrimerCaracter = resultadoAImprimir.Resultado.Substring(0, 1);
                                if (PrimerCaracter == ".")
                                {
                                    resultadoValue = float.Parse("0" + resultadoAImprimir.Resultado);
                                }
                                else
                                {
                                    resultadoValue = float.Parse(resultadoAImprimir.Resultado);
                                }
                                nuevoObxCuantitativo.Obx_5_ObservationValue = resultadoAImprimir.Resultado;

                                Rango rangosObtenidos = managerDBOpenf.getRangos(int.Parse(resultadoAImprimir.Parametro), edadDias, resultadoValue);
                                nuevoObxCuantitativo.Obx_7_rangeReference = rangosObtenidos.RangoInferior.ToString() + " - " + rangosObtenidos.RangoSuperior.ToString();
                                nuevoObxCuantitativo.Obx_11_ObservationResultStatus = "F";
                                nuevoObxCuantitativo.Obx_14_dateofObservation = nuevaObrResult.Obr_8_ObservationEndDateTime;
                                nuevaObrResult.ListObxCuantitativos.Add(nuevoObxCuantitativo);

                                nuevaObxCualitativo.Obx_4_IdDelResultado = rangosObtenidos.IdComentario.ToString();//revisar
                                nuevaObxCualitativo.Obx_5_ResultadoCualitativo = rangosObtenidos.Comentario;//revisar

                                contadorObx++;
                            }

                        }
                        else if (TipoExamenPlantilla == "B" || TipoExamenPlantilla == "E")//PlantillaB y E
                        {
                            foreach (resultview resultadoAImprimir in ResultadosObx)
                            {
                                if (resultadoAImprimir.Parametro != "25070" && resultadoAImprimir.Parametro != "85115" && resultadoAImprimir.Parametro != "40006")
                                {
                                    string fechaNacimiento = peticionActual.Pid7_datetimeBirth;
                                    DateTime nacimiento = DateTime.ParseExact(fechaNacimiento + " 00:00:00", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                    DateTime fecha_Actual = DateTime.Now;
                                    int edadDias = (fecha_Actual - nacimiento).Days;
                                    Respuesta_obx nuevoObxCuantitativo = new Respuesta_obx();
                                    nuevoObxCuantitativo.Obx_1_ObxId = contadorObx.ToString();

                                    Boolean isNumeric = resultadoAImprimir.Resultado.All(char.IsNumber);
                                    if (isNumeric)
                                    {
                                        nuevoObxCuantitativo.Obx_2_ValueType = "NM";
                                        subElemento SubElementoRespuesta = managerDBOpenf.getSubElemeto(resultadoAImprimir.Estudio, resultadoAImprimir.Parametro, peticionActual.Pid8_AdministrativeSex, edadDias);
                                        //COdigo de examen
                                        nuevoObxCuantitativo.Obx_3_1_Identifier = SubElementoRespuesta.Codigo;
                                        //Nombre Examen
                                        nuevoObxCuantitativo.Obx_3_2_text = SubElementoRespuesta.Nombre;
                                        nuevoObxCuantitativo.Obx_4_observationSubid = "Instrumento";
                                        float resultadoValue = 0;
                                        string PrimerCaracter = resultadoAImprimir.B_Elemento.Substring(0, 1);
                                        if (PrimerCaracter == ".")
                                        {
                                            resultadoValue = float.Parse("0" + resultadoAImprimir.Resultado);
                                        }
                                        else
                                        {
                                            resultadoValue = float.Parse(resultadoAImprimir.Resultado);
                                        }
                                        //Resultado
                                        nuevoObxCuantitativo.Obx_5_ObservationValue = resultadoValue.ToString();

                                        nuevoObxCuantitativo.Obx_6_units = managerDBOpenf.getUnitstest(resultadoAImprimir.Parametro);
                                        Rango rangosObtenidos = managerDBOpenf.getRangos(int.Parse(resultadoAImprimir.Parametro), edadDias, float.Parse(resultadoAImprimir.Resultado));
                                        nuevoObxCuantitativo.Obx_7_rangeReference = rangosObtenidos.RangoInferior.ToString() + " - " + rangosObtenidos.RangoSuperior.ToString();
                                        nuevoObxCuantitativo.Obx_11_ObservationResultStatus = "F";
                                        nuevoObxCuantitativo.Obx_14_dateofObservation = nuevaObrResult.Obr_8_ObservationEndDateTime;
                                        nuevaObrResult.ListObxCuantitativos.Add(nuevoObxCuantitativo);
                                    }
                                    else//NO ES NUMERICO
                                    {
                                        nuevoObxCuantitativo.Obx_2_ValueType = "NM";
                                        subElemento SubElementoRespuesta = managerDBOpenf.getSubElemeto(resultadoAImprimir.Estudio, resultadoAImprimir.Parametro, peticionActual.Pid8_AdministrativeSex, edadDias);
                                        //COdigo de examen
                                        nuevoObxCuantitativo.Obx_3_1_Identifier = SubElementoRespuesta.Codigo;
                                        //Nombre Examen
                                        nuevoObxCuantitativo.Obx_3_2_text = SubElementoRespuesta.Nombre;
                                        nuevoObxCuantitativo.Obx_4_observationSubid = "Instrumento";
                                        nuevoObxCuantitativo.Obx_5_ObservationValue = resultadoAImprimir.Resultado;
                                        nuevoObxCuantitativo.Obx_6_units = "";
                                        nuevoObxCuantitativo.Obx_7_rangeReference = "";
                                        nuevoObxCuantitativo.Obx_11_ObservationResultStatus = "F";
                                        nuevoObxCuantitativo.Obx_14_dateofObservation = nuevaObrResult.Obr_8_ObservationEndDateTime;
                                        nuevaObrResult.ListObxCuantitativos.Add(nuevoObxCuantitativo);
                                    }

                                    int idComentario = 1;
                                    ResultadosObx[ResultadosObx.Count - 1].Resultado = ResultadosObx[ResultadosObx.Count - 1].Resultado.Replace(" ", "");
                                    if (ResultadosObx[ResultadosObx.Count - 1].Resultado == "Normal" || ResultadosObx[ResultadosObx.Count - 1].Resultado == "normal")
                                    {
                                        idComentario = 1;
                                    }
                                    else
                                    {
                                        idComentario = 3;
                                    }

                                    nuevaObxCualitativo.Obx_4_IdDelResultado = idComentario.ToString();//revisar
                                    nuevaObxCualitativo.Obx_5_ResultadoCualitativo = ResultadosObx[ResultadosObx.Count - 1].Resultado; //revisar

                                    contadorObx++;
                                }
                            }
                        }


                        //END OBXS

                        //Impresion de obx y obrs
                        nuevaObrResult.Obx_Cualitativo = nuevaObxCualitativo;
                        string obr = @"OBR|" + nuevaObrResult.Obr_1_IDOBR + "|" + nuevaObrResult.Obr_2_PlacerOrdeNumber + "||" + nuevaObrResult.Obr_4_1_Identifier + "^" + nuevaObrResult.Obr_4_2_Text + "^L^" + nuevaObrResult.Obr_4_4_AlternateIdentifier + "^" + nuevaObrResult.Obr_4_5_AlternateText + "||||" + nuevaObrResult.Obr_8_ObservationEndDateTime + "||" + nuevaObrResult.Obr_10_CollectorIdentifier + "||||||" + resultadoRepuesta.Orc_12_1_CodigoProfesional + "^" + resultadoRepuesta.Orc_12_2_NombreProfesional + "||||||" + nuevaObrResult.Obr_22_ResultReptStatusChangeDateTime + "||" + nuevaObrResult.Obr_24_DiagnosticServiceID + "|" + nuevaObrResult.Obr_25_ResultStatus + "_z";
                        respuesta += obr;
                        string obxCuali = @"OBX|" + nuevaObrResult.Obx_Cualitativo.Obx_1_IdObx + "|" + nuevaObrResult.Obx_Cualitativo.Obx_2_TipoDato + "|" + nuevaObrResult.Obx_Cualitativo.Obx_3_IdExamenSolicitado + "|" + nuevaObrResult.Obx_Cualitativo.Obx_4_IdDelResultado + "|" + nuevaObxCualitativo.Obx_5_ResultadoCualitativo + "_z";
                        respuesta += obxCuali;
                        foreach (Respuesta_obx resultObx in nuevaObrResult.ListObxCuantitativos)
                        {
                            string obx = @"OBX|" + resultObx.Obx_1_ObxId + "|" + resultObx.Obx_2_ValueType + "|" + resultObx.Obx_3_1_Identifier + "^" + resultObx.Obx_3_2_text + "|" + resultObx.Obx_4_observationSubid + "|" + resultObx.Obx_5_ObservationValue + "|" + resultObx.Obx_6_units + "|" + resultObx.Obx_7_rangeReference + "||||" + resultObx.Obx_11_ObservationResultStatus + "|||" + resultObx.Obx_14_dateofObservation + "_z";
                            respuesta += obx;
                        }

                        contadorObr++;
                    }//END OBRS

                }//end for no result
            }//end if resultado==null

            if (respuesta != "")
            managerDBOhl7.actualizarCompletas(tranIncompleta.Indice1, respuesta, int.Parse(tranIncompleta.Orden));
            respuesta = "";
            resultadoRepuesta = new Repuesta();
            orcCargado = false;
        }//end foreach Transacciones Incompletas
    }
      


    }

