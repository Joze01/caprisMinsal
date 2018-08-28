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

    /// <summary>
    /// Funcion que recibe una peticion en string y la descompone para guardar el registro en Nexus y en la tabla de transacciones.
    ///
    /// </summary>
    /// <param name="mensaje"> Mensaje Recibido Hl7</param>
    /// <param name="examenes">Cantidad de examanes</param>
    /// <param name="orden"># de orden</param>
    /// <param name="idSiaps">Id de siaps</param>
    /// <param name="area">Area que envia la peticion.</param>
    /// <returns>Retorna True si se completa o false si no lo hace.</returns>
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


    /// <summary>
    /// Obtener las peticiones que estan pendientes de procesar, (Estado=1)
    /// </summary>
    /// <returns>Listado de entidades transaccion.</returns>
    public List<transacciones> obtenerPendientes()
    {

        List<transacciones> listaPendiente = new List<transacciones>();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=0 or estado=1";
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

    /// <summary>
    /// Funcion para obtener cuantos examenes ya tienen respuesta validada.
    /// </summary>
    /// <param name="ordern"># de orden</param>
    /// <returns>Retorna un entero con la cantidad.</returns>
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

    public int cantidadResultadosCompletosOld(int ordern)
    {
        int numeroRespuestas = 0;

        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select completas from transacciones where orden=" + ordern;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            numeroRespuestas = int.Parse(reader["completas"].ToString());
        }

        cone.Close();
        return numeroRespuestas;

    }


    /// <summary>
    /// Funcion para cambiar de estado las peticiones en la tabla de transacciones.
    /// </summary>
    /// <param name="id">Indice en la Tabla</param>
    /// <param name="mensaje">Mensaje de respuesta</param>
    /// <param name="ordenn"># de orden</param>
    /// <returns>retorna false si hay algun fallo. </returns>
    public Boolean actualizarCompletas(int id,string mensaje,int ordenn) {

        openfDBManager managerOpenF = new openfDBManager();
        String query = "";
        int cantidadPruebasReg = this.cantidadResultados(ordenn);
        int cantidadPruebasCom = managerOpenF.cantidadRespuestas(ordenn);
        int cantidadPruebasCompletadasOld = this.cantidadResultadosCompletosOld(ordenn);

        if (cantidadPruebasCom == cantidadPruebasReg && cantidadPruebasCompletadasOld< cantidadPruebasCom) {
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =3 WHERE Indice=" + id ;
        }
        else
        {
            if(cantidadPruebasCompletadasOld < cantidadPruebasCom) { 
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =1 WHERE Indice=" + id;
            }
        }

        if (query == "")
        {
            return false;
        }
        actualizarCantidadProcesadas(cantidadPruebasCom, id);
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

    /// <summary>
    /// Cambio de estado a Enviadas
    /// </summary>
    /// <param name="id">Indice de la tabla.</param>
    /// <returns>Retorna false si falla la conexion</returns>
    public Boolean actualizarEnviadas(int id)
    {
        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();

        String query = "UPDATE transacciones SET estado =2 WHERE Indice=" + id;
        cmd = new SqlCommand(query, cone);
        System.Diagnostics.Debug.WriteLine("MARCADASSSSSSSSS");


        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }

        cone.Close();
        return false;
    }

    /// <summary>
    /// Revisa que una peticion ya este completada (estado=3)
    /// </summary>
    /// <param name="indice"></param>
    /// <returns>Ttrue si el estado es 3</returns>
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

    /// <summary>
    /// Funcion para extraer el listado de peticiones que ya se completaron
    /// </summary>
    /// <returns>Lista de Entidades Transaccion.</returns>
    public List<transacciones> ObtenerCompletos() {
        List<transacciones> listaCompletas = new List<transacciones>();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=3";
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cantidad"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Boolean actualizarCantidadProcesadas(int cantidad,int id) {
        conhl7 = new Conexonhl7();
        int afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();

        String query = "UPDATE transacciones SET completas ="+cantidad+" WHERE Indice=" + id;
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
}