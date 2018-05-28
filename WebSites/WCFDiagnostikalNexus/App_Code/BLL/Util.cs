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

    public List<string> getComentarioCualitativo(string sexo, int edad, Boolean embarazo, List<float>rangosMax, List<float> rangosMin, string lectura)
    {
        List<string> comentarios = new List<string>();
        string id="";
        string valorcomentario="";
        try
        {
                
                float valor = float.Parse(lectura);
                float rangoUtilMax =0;
                float rangoUtilMin = 0;
                if (sexo == "M")
                {
                    rangoUtilMax = rangosMax[0];
                    rangoUtilMin = rangosMin[0];
                }else if(sexo == "F")
                {
                    rangoUtilMax = rangosMax[1];
                    rangoUtilMin = rangosMin[1];
                }

                if (edad <14)
                {
                    rangoUtilMax = rangosMax[3];
                    rangoUtilMin = rangosMin[3];
                }
                if (embarazo)
                {
                    rangoUtilMax = rangosMax[2];
                    rangoUtilMin = rangosMin[2];
                }

                if(valor>=rangoUtilMin && valor <= rangoUtilMax)
                {
                id="1";
                valorcomentario = "Normal";
                }else {
                id = "3";
                valorcomentario = "Anormal";
            }

        }catch(Exception ex)
        {
            if (lectura == "Negativo")
            {
                id = "3";
                valorcomentario = "Negativo";
            }
            if (lectura == "Positivo")
            {
                id = "4";
                valorcomentario = "Positivo";
            }
            if (lectura == "Muestra Inadecuada")
            {
                id = "5";
                valorcomentario = "Muestra Inadecuada";
            }
            if (lectura == "Otros")
            {
                id = "6";
                valorcomentario = "Otros";
            }
            if (lectura == "Reactivo")
            {
                id = "7";
                valorcomentario = "Reactivo";
            }
            if (lectura == "Indeterminado")
            {
                id = "8";
                valorcomentario = "Indeterminado";
            }
            if (lectura == "No Reactivo")
            {
                id = "9";
                valorcomentario = "No Reactivo";
            }
        }

        comentarios.Add(id);
        comentarios.Add(valorcomentario);
        return comentarios;
    }



}