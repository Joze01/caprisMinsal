using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// CLASE DE MANEJO DE LA LOGICA DE NEGOCIOS DE LA BASE HL7
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
    /// <param name="muestra">SAMPLE</param>
    /// <returns>Retorna True si se completa o false si no lo hace.</returns>
    public Boolean guardarPeticion(String mensaje, long examenes, long orden, string idSiaps, long muestra) {
        conhl7 = new Conexonhl7();
        long afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();
        DateTime fechaActualCodigo = DateTime.Now;
        String query= "INSERT INTO transacciones(peticion,pruebas, orden, siapsid, orc) VALUES (@Ppeticion, @Pexamenes, @Porden, @Psiapsid, @Porc)";
        cmd =new SqlCommand(query, cone);
        cmd.Parameters.AddWithValue("@Ppeticion", mensaje);
        cmd.Parameters.AddWithValue("@Pexamenes", examenes);
        string ordenFinal = fechaActualCodigo.ToString("yyMMdd") + muestra;
        cmd.Parameters.AddWithValue("@Porden", ordenFinal);
        cmd.Parameters.AddWithValue("@Psiapsid", ordenFinal);
        cmd.Parameters.AddWithValue("@Porc", idSiaps);
        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }
        conhl7.desconectar();cone.Close();
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
            transaccion.Indice1 = long.Parse(reader["Indice"].ToString());
            transaccion.Peticion = reader["peticion"].ToString();
            transaccion.Estado = 0;
            transaccion.Fecha = reader.GetDateTime(4);
            transaccion.Pruebas = long.Parse(reader["pruebas"].ToString());
            transaccion.Orden = reader["orden"].ToString();
            transaccion.Siapsid = reader["siapsid"].ToString();

            listaPendiente.Add(transaccion);
        }

        conhl7.desconectar();cone.Close();
        return listaPendiente;
    }

    /// <summary>
    /// Funcion para obtener cuantos examenes ya tienen respuesta validada.
    /// </summary>
    /// <param name="ordern"># de orden</param>
    /// <returns>Retorna un entero con la cantidad.</returns>
    public long cantidadResultados(long ordern)
    {
        long numeroRespuestas = 0;
        
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select pruebas from transacciones where orden="+ ordern;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            numeroRespuestas = long.Parse(reader["pruebas"].ToString());
        }
        
        conhl7.desconectar();cone.Close();;
        return numeroRespuestas;
        
    }

    /// <summary>
    /// Funcion para obtener la cantidad de examenes que ya tienen respuesta.
    /// </summary>
    /// <param name="ordern"># de orden SIAPS</param>
    /// <returns>Retorna de respuesta que tiene cada peticion.</returns>
    public long cantidadResultadosCompletosOld(long ordern)
    {
        long numeroRespuestas = 0;

        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select completas from transacciones where orden=" + ordern;
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            numeroRespuestas = long.Parse(reader["completas"].ToString());
        }

        conhl7.desconectar();cone.Close();
        return numeroRespuestas;

    }


    /// <summary>
    /// Funcion para cambiar de estado las peticiones en la tabla de transacciones.
    /// </summary>
    /// <param name="id">Indice en la Tabla</param>
    /// <param name="mensaje">Mensaje de respuesta</param>
    /// <param name="ordenn"># de orden</param>
    /// <returns>retorna false si hay algun fallo. </returns>
    public Boolean actualizarCompletas(long id,string mensaje,long ordenn) {

        openfDBManager managerOpenF = new openfDBManager();
        String query = "";
        long cantidadPruebasReg = this.cantidadResultados(ordenn);
        long cantidadPruebasCom = managerOpenF.cantidadRespuestas(ordenn);
        long cantidadPruebasCompletadasOld = this.cantidadResultadosCompletosOld(ordenn);

        if (cantidadPruebasCom == cantidadPruebasReg && cantidadPruebasCompletadasOld< cantidadPruebasCom) {
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =3 WHERE Indice=" + id ;
        }
        else
        {
            if(cantidadPruebasCompletadasOld != cantidadPruebasCom) { 
            query = "UPDATE transacciones SET respuesta = @PRespuesta  ,estado =1 WHERE Indice=" + id;
            }
        }

        if (query == "")
        {
            return false;
        }
        actualizarCantidadProcesadas(cantidadPruebasCom, id);
        conhl7 = new Conexonhl7();
        long afectadas = 0;
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

        conhl7.desconectar();cone.Close();
        return false;
    }

    /// <summary>
    /// Cambio de estado a Enviadas
    /// </summary>
    /// <param name="id">Indice de la tabla.</param>
    /// <returns>Retorna false si falla la conexion</returns>
    public Boolean actualizarEnviadas(long id)
    {
        conhl7 = new Conexonhl7();
        long afectadas = 0;
        conhl7.conectar();
        cone = conhl7.getConexion();

        String query = "UPDATE transacciones SET estado =2 WHERE Indice=" + id;
        cmd = new SqlCommand(query, cone);
        System.Diagnostics.Debug.WriteLine("checked: "+id);


        cmd.CommandType = CommandType.Text;
        afectadas = cmd.ExecuteNonQuery();
        if (afectadas > 0)
        {
            return true;
        }
        conhl7.desconectar();
        cone.Close();
        return false;
    }

    /// <summary>
    /// Revisa que una peticion ya este completada (estado=3)
    /// </summary>
    /// <param name="indice"></param>
    /// <returns>Ttrue si el estado es 3</returns>
    public Boolean isCompleta(long indice) {
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
        conhl7.desconectar();cone.Close();
        return false;
    }

    /// <summary>
    /// Funcion para extraer el listado de peticiones que ya se completaron
    /// </summary>
    /// <returns>Lista de Entidades Transaccion.</returns>
    public List<transacciones> ObtenerCompletos() {
        List<transacciones> listaCompletas = new List<transacciones>();
        openfDBManager managerOpenF = new openfDBManager();
        conhl7 = new Conexonhl7();
        conhl7.conectar();
        cone = conhl7.getConexion();
        string query = "select * from transacciones where estado=3 or estado=1 or estado=2";
        cmd = new SqlCommand(query, cone);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            transacciones transaccion = new transacciones();
            transaccion.Indice1 = long.Parse(reader["Indice"].ToString());
            transaccion.Peticion = reader["peticion"].ToString();
            transaccion.Respuesta = reader["Respuesta"].ToString();
            transaccion.Estado = long.Parse(reader["estado"].ToString());
            transaccion.Fecha = reader.GetDateTime(4);
            transaccion.Pruebas = long.Parse(reader["pruebas"].ToString());
            transaccion.Orden = reader["orden"].ToString();
            transaccion.Siapsid = reader["siapsid"].ToString();
            long cantidadPruebasCompletadasOld = this.cantidadResultadosCompletosOld(long.Parse(transaccion.Siapsid));
            long cantidadPruebasCom = managerOpenF.cantidadRespuestas(long.Parse(transaccion.Siapsid));

            if (cantidadPruebasCompletadasOld != cantidadPruebasCom || transaccion.Estado == 3) { 
                listaCompletas.Add(transaccion);
                actualizarCompletas(transaccion.Indice1, transaccion.Respuesta, long.Parse(transaccion.Siapsid));
            }
        }


        conhl7.desconectar();cone.Close();
        return listaCompletas;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cantidad"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Boolean actualizarCantidadProcesadas(long cantidad,long id) {
        conhl7 = new Conexonhl7();
        long afectadas = 0;
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

        conhl7.desconectar();cone.Close();
        return false;


    }
}