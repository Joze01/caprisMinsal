﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// CLASE DE LOGICA DE NEGOCIOS DE LA BASE OPENF
/// </summary>
public class openfDBManager
{
    string query;
    conexion conOpenf;
    SqlConnection cone;
    SqlCommand cmd;
    public openfDBManager()
    {
    }
    /// <summary>
    /// Funcion para registrar la peticion es HI2LIS de OPENF
    /// </summary>
    /// <param name="datos">peticin entrante, de tipo entidad PeticionEntrante</param>
    /// <returns>Retorna True or False</returns>
    public Boolean nuevaPeticion(PeticionEntrante datos)
    {
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        long afectadas = 0;
        string fechaNacimiento = datos.Pid7_datetimeBirth;
        
        DateTime nacimiento = DateTime.ParseExact(fechaNacimiento+" 00:00:00", "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);


        DateTime fecha_Actual = DateTime.Now;
        long edad = (fecha_Actual - nacimiento).Days/365;

        foreach (Peticion_obr orb_detalle in datos.ListaORB)
        {
            DateTime fechaActualCodigo = DateTime.Now;
            System.DateTime dateTimeTransaction = DateTime.Now;
            
            string fechaFormateada = dateTimeTransaction.ToString("yyyy-MM-dd");

            cmd = null;
            //query = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, Sexo, Codigo, Edad) " +
               // "VALUES (@POrdens, '"+ fechaFormateada + "' , @POrigens, @PServicios, @PDoctors, @Plibres, @PIdentificacions, @PNombres, @PApell1s, @PApell2s, @PSexos, @PCodigos, @Pedad)";
            query = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, Sexo, Codigo, Edad) " +
                "VALUES (@POrdens, '" + fechaFormateada + " 00:00:00' , @POrigens, @PServicios, @PDoctors, @Plibres, @PIdentificacions, @PNombres, @PApell1s, @PApell2s, @PSexos, @PCodigos, @Pedad)";

            try
            {
                cmd = new SqlCommand(query, cone);
                string[] apellidos = datos.Pid5_1_familyName.Split(' ');

                string ordenFinal = fechaActualCodigo.ToString("yyMMdd") + datos.Orc2_placerOrderNumer + long.Parse(datos.Msh10_messageControlID);

                cmd.Parameters.Add(new SqlParameter("@POrdens", ordenFinal));
                var salaryParam = new SqlParameter("PFSolicituds", SqlDbType.SmallDateTime);
                salaryParam.Value = dateTimeTransaction.ToShortDateString();
                cmd.Parameters.Add(new SqlParameter("@POrigens", datos.Pv2_patientClass));
                cmd.Parameters.Add(new SqlParameter("@PServicios", datos.Orc13_1_pointOfCare));
                cmd.Parameters.Add(new SqlParameter("@PDoctors", datos.Orc12_1_idNumber));
                cmd.Parameters.Add(new SqlParameter("@Plibres", 1));
                cmd.Parameters.Add(new SqlParameter("@PIdentificacions", datos.Pid3_1_idNumber));//paciente ID
                cmd.Parameters.Add(new SqlParameter("@PNombres", datos.Pid5_2_givenName +" "+ datos.Pid5_3_secondName));
                cmd.Parameters.Add(new SqlParameter("@PApell1s", apellidos[0])); //Hacer split de los apellidos
                cmd.Parameters.Add(new SqlParameter("@PApell2s", apellidos[1]));
                cmd.Parameters.Add(new SqlParameter("@Pedad", edad));
                //cmd.Parameters.Add(new SqlParameter("@Pedad", null));
                if (datos.Pid8_AdministrativeSex == "2")
                {
                    cmd.Parameters.Add(new SqlParameter("@PSexos", "F")); //Revisar tabla Masculino = M, Femenino = F
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@PSexos", "M")); //Revisar tabla Masculino = M, Femenino = F
                }
                //cmd.Parameters.Add(new SqlParameter("@PCodigos", orb_detalle.Obr4_4_AlternateIdentifier));
                cmd.Parameters.Add(new SqlParameter("@PCodigos", orb_detalle.Obr4_4_AlternateIdentifier));


                afectadas += cmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine("Afectadas " + afectadas);
                System.Diagnostics.Debug.WriteLine(cmd.CommandText);

                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                conOpenf.desconectar();cone.Close();
            }

        }
        conOpenf.desconectar();cone.Close();
        if (afectadas > 0)
            {
                return true;
            }

            return true;
    }
    /// <summary>
    /// FUncion auxiliar para obtener el nombre de encargado by name.
    /// </summary>
    /// <param name="id">Identificador del empleado</param>
    /// <returns>Retorna True or False si se completa</returns>
    public string getEncargadoName(string id) {
        string encargadoName = "";
        List<resultview> listadoResultados = new List<resultview>();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from ologin where login_name='"+ id+"'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            encargadoName = reader["login_descripcion"].ToString();
        }
        conOpenf.desconectar();cone.Close();
        return encargadoName;
    }
    /// <summary>
    /// Funcion para obtener los nombre del examen
    /// </summary>
    /// <param name="id">Id del examen (Clase name en la tabla operfil)</param>
    /// <returns>retorna el nombre del examen asociado al Id</returns>
    public string getExamenName(string id)
    {
        string examenName = "";
        List<resultview> listadoResultados = new List<resultview>();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from operfil where clase_name='"+id+"'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            examenName = reader["clase_descrip"].ToString();
        }



        conOpenf.desconectar();cone.Close();
        return examenName;
    }
    /// <summary>
    /// Obtiene el Id del examen
    /// </summary>
    /// <param name="cod">Codigo de examen (clase_name en operfil)</param>
    /// <returns></returns>
    public string getExamenId(string cod) {
        string examenId = "";
        List<resultview> listadoResultados = new List<resultview>();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from operfil where clase_name='" + cod+"'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            examenId = reader["clase"].ToString();
        }


        conOpenf.desconectar();cone.Close();

        return examenId;
    }
    /// <summary>
    /// Extrae las unidades de medida para cada elemento del hl7
    /// </summary>
    /// <param name="cod">codigo de examen (par_codigo en operfil)</param>
    /// <returns>Retorna las unidades de medida de cada elemento</returns>
    public string getUnitstest(string cod)
    {
        string resultado = "";
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select unidades from operfil_data where par_codigo=" + cod;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            resultado = reader["unidades"].ToString();
        }
        conOpenf.desconectar();cone.Close();

        return resultado;
    }
    /// <summary>
    /// Obtener los rangos para las respuestas y la validacion de resultado
    /// </summary>
    /// <param name="cod">Codigo del del examen</param>
    /// <param name="EdadDias">Edad de la persona en dias</param>
    /// <param name="lectura">Cantidad leida en la vista de resultados</param>
    /// <returns>Retorna una entidad llamada Rangos con los elementos necesarios</returns>
    public Rango getRangos(long cod,long EdadDias,float lectura) {
        Rango rangosAdecuados=new Rango();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select valor_inferior1, valor_inferior2, valor_inferior3, valor_inferior4, valor_superior1, valor_superior2, valor_superior3, valor_superior4 from operfil_data where par_codigo=" + cod;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        float rangoin1=0;
        float rangoin2=0;
        float rangoin3 =0; 
        float rangoin4 = 0;

        float rangosup1 = 0;
        float rangosup2 = 0;
        float rangosup3 = 0;
        float rangosup4 = 0;
        if (reader.Read())
        {
             rangoin1 = float.Parse(reader["valor_inferior1"].ToString());
             rangoin2 = float.Parse(reader["valor_inferior2"].ToString());
             rangoin3 = float.Parse(reader["valor_inferior3"].ToString());
             rangoin4 = float.Parse(reader["valor_inferior4"].ToString());

             rangosup1 = float.Parse(reader["valor_superior1"].ToString());
             rangosup2 = float.Parse(reader["valor_superior2"].ToString());
             rangosup3 = float.Parse(reader["valor_superior3"].ToString());
             rangosup4 = float.Parse(reader["valor_superior4"].ToString());

        }
        if(EdadDias>=0 && EdadDias <= 54750)
        {
            rangosAdecuados.RangoInferior = rangoin3;
            rangosAdecuados.RangoSuperior = rangosup3;
        }
        if (EdadDias <= 30) { //1
            rangosAdecuados.RangoInferior = rangoin4;
            rangosAdecuados.RangoSuperior = rangoin4;
        }
        if(EdadDias>= 31 && EdadDias <= 4380)//2
        {
            rangosAdecuados.RangoInferior = rangoin2;
            rangosAdecuados.RangoSuperior = rangosup2;
        }
        if (EdadDias >= 4381 && EdadDias<=54750) {
            rangosAdecuados.RangoInferior = rangoin1;
            rangosAdecuados.RangoSuperior = rangosup1;
        }

        if (lectura >= rangosAdecuados.RangoInferior && lectura <= rangosAdecuados.RangoSuperior) {
            rangosAdecuados.Comentario = "Normal";
            rangosAdecuados.IdComentario = 1;
        }
        else
        {
            rangosAdecuados.Comentario = "Anormal";
            rangosAdecuados.IdComentario = 3;
        }

        conOpenf.desconectar();cone.Close();
        return rangosAdecuados;

    } //min y max 
    /// <summary>
    /// Obtener el abreviado del examen
    /// </summary>
    /// <param name="id">codigo del examen</param>
    /// <returns></returns>
    public string getAbreviado(string id) {
        string abreviatura = "";
        List<resultview> listadoResultados = new List<resultview>();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select abreviado from operfil_data where cod_perfil='" + id+"'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            abreviatura = reader["abreviado"].ToString();
        }


        conOpenf.desconectar();cone.Close();

        return abreviatura;
    }
    /// <summary>
    /// Funcion para obtener los resultados por ordenes 
    /// </summary>
    /// <param name="orden"># de orden SIAP </param>
    /// <returns>Retorna una lista de entidades ResultVIew(resultados de la vista) </returns>
    public List<resultview> getResultados(long orden) {
        resultview resultado = new resultview();
        List<resultview> resultadosLista = new List<resultview>();
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from ResultHIS_view where Orden="+orden;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            resultado.Orden = reader["Orden"].ToString();
            resultado.Identificacion = reader["Identificacion"].ToString();
            resultado.Fecha = reader.GetDateTime(2).ToString("yyyyMMddHHmm");
            resultado.Parametro = reader["Parametro"].ToString();
            resultado.Resultado = reader["Resultado"].ToString();
            resultado.Comentario = reader["Comentario"].ToString();
            resultado.Estudio = reader["Estudio"].ToString();
            resultado.Plantilla = reader["Plantilla"].ToString();
            resultado.B_Elemento = reader["B_Elemento"].ToString();
            resultado.B_SubElemento = reader["B_SubElemento"].ToString();
            resultado.E_Elemento = reader["E_Elemento"].ToString();
            resultado.Responsable = reader["Responsable"].ToString();
            resultado.T_from_his = reader["t_from_his"].ToString();
            resultado.T_validado = reader["t_validado"].ToString();
            resultadosLista.Add(resultado);
        }
        conOpenf.desconectar();cone.Close();
        return resultadosLista;
    }
    /// <summary>
    /// funciona para obtener la cantidad de respuesta que ya posee una orden
    /// </summary>
    /// <param name="orden"># de orden SIAP</param>
    /// <returns>retorna un entero</returns>
    public long cantidadRespuestas(long orden)
    {
        long resultado = 0;
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select count(distinct(Estudio)) as cantidad from ResultHIS_view where Orden=" + orden;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            resultado = long.Parse(reader["cantidad"].ToString());
        }
        
        conOpenf.desconectar();cone.Close();
        return resultado;
    }
    /// <summary>
    /// obtener los resultados de cada examen, de la una orden. 
    /// </summary>
    /// <param name="orden"># de orden SIAP</param>
    /// <param name="prueba">Codigo de prueba</param>
    /// <returns>Retorna una lista de entidades de tipo ResultView</returns>
    public List<resultview> getResultadosByEstudio(long orden,string prueba)
    {
      
        List<resultview> resultadosLista = new List<resultview>();
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from ResultHIS_view where Orden=" + orden+" and Estudio='"+prueba+"'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            resultview resultado = new resultview();
            resultado.Orden = reader["Orden"].ToString();
            resultado.Identificacion = reader["Identificacion"].ToString();
            resultado.Fecha = reader.GetDateTime(2).ToString("yyyyMMdd");
            string horaYminuto = DateTime.Now.ToString("HHmm");
            resultado.Fecha = resultado.Fecha + horaYminuto;
            resultado.Parametro = reader["Parametro"].ToString();
            resultado.Resultado = reader["Resultado"].ToString();
            resultado.Comentario = reader["Comentario"].ToString();
            resultado.Estudio = reader["Estudio"].ToString();
            resultado.Plantilla = reader["Plantilla"].ToString();
            resultado.B_Elemento = reader["B_Elemento"].ToString();
            resultado.B_SubElemento = reader["B_SubElemento"].ToString();
            resultado.E_Elemento = reader["E_Elemento"].ToString();
            resultado.Responsable = reader["Responsable"].ToString();
            resultado.T_from_his = reader["t_from_his"].ToString();
            resultado.T_validado = reader["t_validado"].ToString();
            resultadosLista.Add(resultado);
        }
        conOpenf.desconectar();cone.Close();
        return resultadosLista;
    }
    /// <summary>
    /// Funcion para generar el tipo de examen en la trama
    /// </summary>
    /// <param name="codigoExamen"> codigo de examen</param>
    /// <returns>Retorna el string segun el area</returns>
    public string getTipoExame(string codigoExamen)
    {
        string respuesta = "";

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select top 1 olgrupos_descrip from olgrupos inner join ot on ot.t_familia=olgrupos.olgrupos_num where ot.t_perfil_codigo='"+ codigoExamen + "'";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            respuesta = reader["olgrupos_descrip"].ToString();
        }

        if (respuesta == "QUIMICA"||respuesta== "QUIMICA") {
            respuesta = "CH";
        }else if(respuesta== "HEMATOLOGIA")
        {
            respuesta = "HM";
        }
        else
        {
            respuesta = "U";
        }
        conOpenf.desconectar();cone.Close();

        return respuesta;
    }
    /// <summary>
    /// retorna sub elemtnos de una prueba
    /// </summary>
    /// <param name="elementos"> codigo de examen</param>
    /// <param name="tparam"> Tparam de la prueba</param>
    /// <param name="sexo">Sexo de a persona</param>
    /// <param name="edadDias">Edad en dias de la persona</param>
    /// <returns>retorna un objeto de tipo entidad de SubElemento</returns>
    public subElemento getSubElemeto(string elementos,string tparam, string sexo, long edadDias) {
        subElemento resultado = new subElemento();
        long idEdad = 1;
        if(edadDias>=0 && edadDias < 54750)
        {
            idEdad = 4;
        }

        if (edadDias >= 0 && edadDias <31) {
            idEdad = 1;
        }else if(edadDias >= 31 && edadDias < 4381)
        {
            idEdad = 2;
        }else if(edadDias >= 4382 && edadDias < 54750)
        {
            idEdad = 3;
        }

        string query = "";
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        query ="SELECT * FROM siaps_subelemento where id_elemento = '"+elementos+"' and par_codigo ='" + tparam + "' and (genero_id = '" + sexo + "' or genero_id=3) and (edad_id = " + idEdad + " or edad_id = 4) ";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            resultado.Nombre = reader["subelemento_nombre"].ToString();
            resultado.TParam=reader["par_codigo"].ToString();
            resultado.Codigo = reader["id_subelemento"].ToString();
        }


        conOpenf.desconectar();cone.Close();
        
        return resultado;
    }


    public Boolean checkServicio(string id)
    {
        bool result = false;
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select count(*) from oaux_origen where clase_name="+id;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            result = true;
        }
      
        conOpenf.desconectar();cone.Close();
        return result;
    }
    public Boolean checkOrigen(string id)
    {
        bool result = false;
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select count(*) from oaux_origen where clase_name=" + id;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            result = true;
        }
   
        conOpenf.desconectar();cone.Close();
        return result;
    }

    public void newOrigen(string id, string nombre)
    {

    }

    public void newService(string id, string nombre, string origen)
    {

    }

}