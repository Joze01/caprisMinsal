using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
/// <summary>
/// Descripción breve de conexion
/// </summary>
public class conexion
{

    //mi conexion:
    SqlConnection con = new SqlConnection("server=localhost; database=openf; integrated security = true");

    //procedimiento que abre la conexion sqlsever
    public void conectar()
    {
        try
        {
            con.Open();
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    //procedimiento que cierra la conexion sqlserver
    public void desconectar()
    {
        con.Close();
    }

    //funcion que devuelve la conexion sqlserver
    public SqlConnection getConexion()
    {
        return con;
    }

    public Boolean nuevaPeticion(DatosPeticion datos)
    {
        this.conectar();
        SqlCommand command = new SqlCommand();
        command.Connection = getConexion();
        command.CommandText = "INSERT INTO HIS2LIS (Orden, FSolicitud, Origen, Servicio, Doctor, Libre, Identificacion, Nombre, Apell1, Apell2, FNac, Sexo, Codigo) " +
            "VALUES (@Orden,@FSolicitud,@Origen,@Servicio,@Doctor,@libre,@Identificacion,@Nombre,@Apell1,@Apell2,@FNac,@Sexo,@Codigo)";
        command.Parameters.Add(new SqlParameter("@Orden", datos.Orden));
        command.Parameters.Add(new SqlParameter("@FSolicitud", datos.Fsolicitud));
        command.Parameters.Add(new SqlParameter("@Origen", datos.Origin));
        command.Parameters.Add(new SqlParameter("@Servicio", datos.Servicio));
        command.Parameters.Add(new SqlParameter("@Doctor", datos.Doctor));
        command.Parameters.Add(new SqlParameter("@libre",1));
        command.Parameters.Add(new SqlParameter("@Identificacion", datos.Identificacion1));
        command.Parameters.Add(new SqlParameter("@Nombre", datos.Nombre));
        command.Parameters.Add(new SqlParameter("@Apell1", datos.Apellido1));
        command.Parameters.Add(new SqlParameter("@Apell2", datos.Apellido2));
        command.Parameters.Add(new SqlParameter("@FNac", datos.FNac1));
        command.Parameters.Add(new SqlParameter("@Sexo", datos.Sexo));
        command.Parameters.Add(new SqlParameter("@Codigo", datos.Codigo));
        int afectadas = command.ExecuteNonQuery();

        if (afectadas > 0)
        {
            return true;
        }





        return false;
    }




    public conexion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //




    }
}