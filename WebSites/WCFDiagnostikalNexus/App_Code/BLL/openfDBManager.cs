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
        foreach (Peticion_obr orb_detalle in datos.ListaORB)
        {
            cmd = null;
            query = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, FNac, Sexo, Codigo) " +
                "VALUES (@POrdens,@PFSolicituds,@POrigens,@PServicios,@PDoctors,@Plibres,@PIdentificacions,@PNombres,@PApell1s,@PApell2s,@PFNacs,@PSexos,@PCodigos)";
            try
            {
                cmd = new SqlCommand(query, cone);
                cmd.Parameters.Add(new SqlParameter("@POrdens", datos.Orc2_placerOrderNumer));
                cmd.Parameters.Add(new SqlParameter("@PFSolicituds", datos.Orc9_datimeTransaction));
                cmd.Parameters.Add(new SqlParameter("@POrigens", datos.Pv2_patientClass));//Origen de solicitud
                                                                                          // cmd.Parameters.Add(new SqlParameter("@POrigens", datos.Pv2_patientClass));//Origen de solicitud
                cmd.Parameters.Add(new SqlParameter("@PServicios", datos.Orc13_1_pointOfCare));//Servicio
                cmd.Parameters.Add(new SqlParameter("@PDoctors", datos.Orc12_1_idNumber));
                cmd.Parameters.Add(new SqlParameter("@Plibres", 1));
                cmd.Parameters.Add(new SqlParameter("@PIdentificacions", datos.Pid3_1_idNumber));//paciente ID
                cmd.Parameters.Add(new SqlParameter("@PNombres", datos.Pid5_2_givenName + datos.Pid5_3_secondName));
                cmd.Parameters.Add(new SqlParameter("@PApell1s", datos.Pid5_1_familyName)); //Hacer split de los apellidos
                cmd.Parameters.Add(new SqlParameter("@PApell2s", datos.Pid5_1_familyName));
                cmd.Parameters.Add(new SqlParameter("@PFNacs", datos.Pid7_datetimeBirth)); //Revisar formato DD-MM-YYYY
                if (datos.Pid8_AdministrativeSex == "2")
                {
                    cmd.Parameters.Add(new SqlParameter("@PSexos", "F")); //Revisar tabla Masculino = M, Femenino = F
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@PSexos", "M")); //Revisar tabla Masculino = M, Femenino = F
                }
                cmd.Parameters.Add(new SqlParameter("@PCodigos", orb_detalle.Obr4_4_AlternateIdentifier));
                System.Diagnostics.Debug.WriteLine(cmd.CommandText);
                afectadas += cmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine("Afectadas " + afectadas);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

            }

        }
        if (afectadas > 0)
            {
                return true;
            }

            return true;
    }

    public String RespuestaQuimica()
    {
        String respuesta = "";
        conOpenf = new conexion();
        conOpenf.conectar();
        cone = conOpenf.getConexion();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        cmd.CommandText = "SELECT TOP 1 Origen FROM HIS2LIS";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cone;

        reader = cmd.ExecuteReader();
        // Data is accessible through the DataReader object here.
        while (reader.Read())
        {
            respuesta = reader["Origen"].ToString();

        }

        
        return respuesta;
    }
}