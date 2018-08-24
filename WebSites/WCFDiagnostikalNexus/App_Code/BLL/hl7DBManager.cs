﻿using System;
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

    public Boolean guardarPeticion(String mensaje, int examenes, int orden, string idSiaps, int area) {
        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();
        DateTime fechaActualCodigo = DateTime.Now;
        String query= "INSERT INTO transacciones(peticion,pruebas, orden, siapsid) VALUES (@Ppeticion, @Pexamenes, @Porden, @Psiapsid)";
        cmd =new SqlCommand(query, cone);
        cmd.Parameters.AddWithValue("@Ppeticion", mensaje);
        cmd.Parameters.AddWithValue("@Pexamenes", examenes);
        string ordenFinal = fechaActualCodigo.ToString("yyMMdd") + idSiaps + area;
        cmd.Parameters.AddWithValue("@Porden", ordenFinal);
        cmd.Parameters.AddWithValue("@Psiapsid", ordenFinal);
        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }
        cone.Close();
        return false;
    }



    //EXTRAE CADA PETICION YA ALMACENADA PARA PROCESARLA Y ENVIARLA
    public List<transacciones> obtenerPendientes()
    {

        List<transacciones> listaPendiente = new List<transacciones>();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=0 or estado=1 or estado=2";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            transacciones transaccion = new transacciones();
            transaccion.Indice1 = int.Parse(reader["Indice"].ToString());
            transaccion.Peticion = reader["peticion"].ToString();
            transaccion.Estado = 0;
            transaccion.Fecha = reader.GetDateTime(4);
            transaccion.Pruebas = int.Parse(reader["pruebas"].ToString());
            transaccion.Orden = reader["orden"].ToString();
            transaccion.Siapsid = reader["siapsid"].ToString();

            listaPendiente.Add(transaccion);
        }

        cone.Close();
        return listaPendiente;
    }

    public int cantidadResultados(int ordern)
    {
        int numeroRespuestas = 0;
        
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select pruebas from transacciones where orden="+ ordern;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            numeroRespuestas = int.Parse(reader["pruebas"].ToString());
        }

        cone.Close();
        return numeroRespuestas;
        
    }

    public Boolean actualizarCompletas(int id,string mensaje,int ordenn) {

        openfDBManager managerOpenF = new openfDBManager();
        String query = "";
        if (managerOpenF.cantidadRespuestas(ordenn) == this.cantidadResultados(ordenn)) {
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =3 WHERE Indice=" + id;
        }
        else
        {
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =1 WHERE Indice=" + id;
        }

        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();


        cmd = new SqlCommand(query, cone);
        cmd.Parameters.AddWithValue("@PRespuesta", mensaje);
      
        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }

        cone.Close();
        return false;
    }

    public Boolean actualizarEnviadas(int id)
    {
        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();

        String query = "UPDATE transacciones SET estado =2 WHERE Indice=" + id;
        cmd = new SqlCommand(query, cone);
  

        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }

        cone.Close();
        return false;
    }

    public Boolean isCompleta(int indice) {
        List<transacciones> listaCompletas = new List<transacciones>();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=3 and indice="+indice;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return true;
        }
        cone.Close();
        return false;
        
    }

    public List<transacciones> ObtenerCompletos() {
        List<transacciones> listaCompletas = new List<transacciones>();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=1 or estado=3";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            transacciones transaccion = new transacciones();
            transaccion.Indice1 = int.Parse(reader["Indice"].ToString());
            transaccion.Peticion = reader["peticion"].ToString();
            transaccion.Respuesta = reader["Respuesta"].ToString();
            transaccion.Estado = 2;
            transaccion.Fecha = reader.GetDateTime(4);
            transaccion.Pruebas = int.Parse(reader["pruebas"].ToString());
            transaccion.Orden = reader["orden"].ToString();
            transaccion.Siapsid = reader["siapsid"].ToString();

            listaCompletas.Add(transaccion);
            actualizarCompletas(transaccion.Indice1, transaccion.Respuesta,int.Parse(transaccion.Siapsid));
        }


        cone.Close();
        return listaCompletas;
    }


}