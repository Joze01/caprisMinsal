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
    SqlConnection con = null;
    SqlCommand cmd=null;
    SqlDataReader reader=null;
    String query;
    //procedimiento que abre la conexion sqlsever
    public void conectar()
    {
        try
        {
            desconectar();
            con = new SqlConnection("Data Source=localhost;Initial Catalog=openf;User id=admin; Password=123");
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
        if (con != null) { 
        con.Close();
        }
    }

    //funcion que devuelve la conexion sqlserver
    public SqlConnection getConexion()
    {
        return con;
    }

   

    

    public conexion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}