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
    public hl7parser()
    {
      
    }

    public DatosPeticion getPeticion(string peticion)
    {
        DatosPeticion nuevaPeticion = new DatosPeticion();
        System.Diagnostics.Debug.WriteLine("PETICION: " + peticion.ToString());
        var mensaje = NextLevelSeven.Core.Message.Build(@peticion.ToString());
        var segmentos = mensaje[1];
        // first segment in a message (returns IElement)
        var mshSegment = mensaje[1];




        // get the first PID segment
        var pidSegment = mensaje.Segments.OfType("PID").First();
        var pid1segment = pidSegment.Field(1).Component(1);
        var pid31segment = pidSegment.Field(3).Component(1);
        var pid32segment = pidSegment.Field(3).Component(2);
        var pid51segment = pidSegment.Field(5).Component(1);
        var pid52segment = pidSegment.Field(5).Component(2);
        var pid53segment = pidSegment.Field(5).Component(3);
        var pid7segment = pidSegment.Field(7).Component(1);
        var pid8egment = pidSegment.Field(8).Component(1);
        



        var pv1Segment = mensaje.Segments.OfType("PV1").First();
        var orcSegment = mensaje.Segments.OfType("ORC").First();
        var obrSegment = mensaje.Segments.OfType("OBR").First();
        var SpmSegment = mensaje.Segments.OfType("SPM").First();

        /*JULIO PETICION*/

        System.Diagnostics.Debug.WriteLine("SEGMENTO53 " + pid53segment);


        return nuevaPeticion;


    }

}