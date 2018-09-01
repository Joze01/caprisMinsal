using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de conexonhl7
/// </summary>
public class Conexonhl7
{
    //mi conexion:
    SqlConnection con = null;
    SqlDataReader reader = null;
    SqlCommand cmd = null;
    //procedimiento que abre la conexion sqlsever
    public void conectar()
    {
        try
        {
            desconectar();
            con = new SqlConnection("Data Source=localhost;Initial Catalog=openf;aUser id=admin; Password=123");
            con.Open();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());

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



}