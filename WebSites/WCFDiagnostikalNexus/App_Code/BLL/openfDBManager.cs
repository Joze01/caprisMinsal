using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Descripción breve de openfDBManager
/// </summary>
public class openfDBManager
{
    string query;
    conexion conOpenf;
    SqlConnection cone;
    SqlCommand cmd;
    public openfDBManager()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }



    public Boolean nuevaPeticion(PeticionEntrante datos)
    {
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        int afectadas = 0;
        string fechaNacimiento = datos.Pid7_datetimeBirth;
        
        DateTime nacimiento = DateTime.ParseExact(fechaNacimiento+" 00:00:00", "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);


        DateTime fecha_Actual = DateTime.Now;
        int edad = (fecha_Actual - nacimiento).Days/365;

        foreach (Peticion_obr orb_detalle in datos.ListaORB)
        {
            // Example of a UNIX timestamp for 11-29-2013 4:58:30
            double timestamp = Double.Parse("1526759002");
            // Format our new DateTime object to start at the UNIX Epoch
            System.DateTime dateTimeTransaction = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            // Add the timestamp (number of seconds since the Epoch) to be converted
            dateTimeTransaction = dateTimeTransaction.AddSeconds(timestamp);
            dateTimeTransaction.Add(new TimeSpan(0, 0, 0));

            //datos.Orc9_datimeTransaction = dateTimeTransaction.ToString();

            string fechaFormateada = dateTimeTransaction.ToString("yyyy-MM-dd");

            cmd = null;
            //query = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, Sexo, Codigo, Edad) " +
               // "VALUES (@POrdens, '"+ fechaFormateada + "' , @POrigens, @PServicios, @PDoctors, @Plibres, @PIdentificacions, @PNombres, @PApell1s, @PApell2s, @PSexos, @PCodigos, @Pedad)";
            query = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, Sexo, Codigo) " +
                "VALUES (@POrdens, '" + fechaFormateada + " 00:00:00' , @POrigens, @PServicios, @PDoctors, @Plibres, @PIdentificacions, @PNombres, @PApell1s, @PApell2s, @PSexos, @PCodigos)";

            try
            {
                cmd = new SqlCommand(query, cone);
                string[] apellidos = datos.Pid5_1_familyName.Split(' ');


                cmd.Parameters.Add(new SqlParameter("@POrdens", datos.Orc2_placerOrderNumer));
                var salaryParam = new SqlParameter("PFSolicituds", SqlDbType.SmallDateTime);
                salaryParam.Value = dateTimeTransaction.ToShortDateString();
                cmd.Parameters.Add(new SqlParameter("@POrigens", datos.Pv2_patientClass));//Origen de solicitud
                                                                                          // cmd.Parameters.Add(new SqlParameter("@POrigens", datos.Pv2_patientClass));//Origen de solicitud
                //cmd.Parameters.Add(new SqlParameter("@PServicios", datos.Orc13_1_pointOfCare));//Servicio
                cmd.Parameters.Add(new SqlParameter("@PServicios", 1));
                cmd.Parameters.Add(new SqlParameter("@PDoctors", datos.Orc12_1_idNumber));
                cmd.Parameters.Add(new SqlParameter("@Plibres", 1));
                cmd.Parameters.Add(new SqlParameter("@PIdentificacions", datos.Pid3_1_idNumber));//paciente ID
                cmd.Parameters.Add(new SqlParameter("@PNombres", datos.Pid5_2_givenName +" "+ datos.Pid5_3_secondName));
                cmd.Parameters.Add(new SqlParameter("@PApell1s", apellidos[0])); //Hacer split de los apellidos
                cmd.Parameters.Add(new SqlParameter("@PApell2s", apellidos[1]));
                //cmd.Parameters.Add(new SqlParameter("@Pedad", edad));
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }

        }
        if (afectadas > 0)
            {
                return true;
            }

            return true;
    }

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
        return encargadoName;
    }


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




        return examenName;
    }

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




        return examenId;
    }

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




        return resultado;
    }

    public Rango getRangos(int cod,int EdadDias,float lectura) {
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


        return rangosAdecuados;

    } //min y max 

    public string getAbreviado(string id) {
        string abreviatura = "";
        List<resultview> listadoResultados = new List<resultview>();

        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select abreviado from operfil_data where par_codigo=" + id;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            abreviatura = reader["abreviado"].ToString();
        }




        return abreviatura;
    }
    public List<resultview> getResultados(int orden) {
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

        return resultadosLista;
    }



    public List<resultview> getResultadosByEstudio(int orden,string prueba)
    {
        resultview resultado = new resultview();
        List<resultview> resultadosLista = new List<resultview>();
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        string query = "select * from ResultHIS_view where Orden=" + orden+" and Estudio='"+prueba+"'";
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

        return resultadosLista;
    }
}