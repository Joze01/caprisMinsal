using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de conexonhl7
/// </summary>
public class conexonhl7
{
    //mi conexion:
    SqlConnection con = new SqlConnection("server=localhost; database=hl7; integrated security = true");

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
        command.CommandText = "INSERT INTO [hl7].[dbo].[transacciones] (peticion)" +
            "VALUES(@peticion)";



        command.Parameters.Add(new SqlParameter("@Orden", datos.Orden));
        command.Parameters.Add(new SqlParameter("@FSolicitud", datos.Fsolicitud));
        command.Parameters.Add(new SqlParameter("@Origen", datos.Origin));

        int afectadas = command.ExecuteNonQuery();

        if (afectadas > 0)
        {
            return true;
        }





        return false;
    }

}