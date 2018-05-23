using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Descripción breve de hl7DBManager
/// </summary>
public class hl7DBManager
{
   
    Conexonhl7 conhl7;
    SqlConnection cone;
    SqlCommand cmd;
    public hl7DBManager()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public Boolean guardarPeticion(String mensaje, int examenes, int orden) {
        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();
       
        String query= "INSERT INTO transacciones(peticion,pruebas, orden) VALUES (@Ppeticion, @Pexamenes, @Porden)";
        cmd =new SqlCommand(query, cone);
        cmd.Parameters.AddWithValue("@Ppeticion", mensaje);
        cmd.Parameters.AddWithValue("@Pexamenes", examenes);
        cmd.Parameters.AddWithValue("@Porden", orden);
        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }


        return false;
    }

}