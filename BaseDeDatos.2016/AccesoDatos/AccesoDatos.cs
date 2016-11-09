using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
{
    public class AccesoDatos
    {
        #region Atributos
        private SqlConnection _conexion;
        private SqlCommand _comando;
        #endregion

        #region Constructores
        public AccesoDatos() 
        {
            // CREO UN OBJETO SQLCONECTION establece la ruta de la bd y el nombre de la tabla
            this._conexion = new SqlConnection(Properties.Settings.Default.MiConeccion); 
            // CREO UN OBJETO SQLCOMMAND //almacena comandos (insert, update, delete, select)
            this._comando = new SqlCommand();
            // INDICO EL TIPO DE COMANDO 
            this._comando.CommandType = System.Data.CommandType.Text;
            // ESTABLEZCO LA CONEXION
            this._comando.Connection = this._conexion;
        }
        #endregion

        #region Métodos
        public List<Persona> ObtenerListaPersonas()
        {
            bool TodoOk = false;
            List<Persona> lista = new List<Persona>();

            try
            {
                // LE PASO LA INSTRUCCION SQL
                _comando.CommandText = "SELECT * FROM Personas ORDER BY apellido, nombre";

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND                 
                SqlDataReader oDr = _comando.ExecuteReader(); //instancio el reader, apunta a un elemento con la psibilidad unica de leerlo para delante, nunca vuelve hacia atras

                // MIENTRAS TENGA REGISTROS...
                while (oDr.Read())
                {
                    // ACCEDO POR NOMBRE O POR INDICE //el listado me permite uusarlo en consola y form (no como cw)
                    lista.Add(new Persona(int.Parse(oDr[0].ToString()),oDr["apellido"].ToString(), oDr["nombre"].ToString(), int.Parse(oDr[3].ToString())));                    
                }

                //CIERRO EL DATAREADER
                oDr.Close();

                TodoOk = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (TodoOk)
                    this._conexion.Close();
            }
            return lista;
        }

        public DataTable ObtenerTablaPersonas()
        {
            bool TodoOk = false;
            DataTable tabla = new DataTable("Personas"); //tiene q ser el mismo nombre de la tabla de la base de datos

            try
            {
                // INDICO EL TIPO DE COMANDO
                this._comando.CommandType = System.Data.CommandType.Text;
                // LE PASO LA INSTRUCCION SQL
                this._comando.CommandText = "SELECT * FROM Personas ORDER BY apellido DESC, nombre";

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND                 
                SqlDataReader oDr = this._comando.ExecuteReader();

                // CARGO LA TABLA CON REGISTROS...
                tabla.Load(oDr);

                //CIERRO EL DATAREADER
                oDr.Close();

                TodoOk = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (TodoOk)
                    this._conexion.Close();
            }
            return tabla;
        }

        public bool InsertarPersona(Persona p)
        {
            bool todoOk = false;

            string sql = "INSERT INTO Personas (nombre, apellido, edad) VALUES(";
            sql = sql + "'" + p.Nombre + "','" + p.Apellido + "'," + p.Edad.ToString() + ")";

            try
            {
                // LE PASO LA INSTRUCCION SQL
                this._comando.CommandText = sql;

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND
                this._comando.ExecuteNonQuery();

                todoOk = true;

            }
            catch (Exception)
            {
                todoOk = false;
            }
            finally
            {
                if (todoOk)
                    this._conexion.Close();               
            }
            return todoOk;
 
        }

        public Persona ObtenerPersonaPorApellido(string apellido)
        {
            bool TodoOk = false;
            Persona p = null;

            try
            {
                // LE PASO LA INSTRUCCION SQL
                this._comando.CommandText = "SELECT * FROM Personas WHERE apellido = '" + apellido + "'";
                // ESTABLESCO LA CONEXION
                this._comando.Connection = this._conexion;

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND                 
                SqlDataReader oDr = this._comando.ExecuteReader();

                // SI HAY REGISTROS...               
                if (oDr.Read())
                {
                    // ACCEDO POR NOMBRE O POR INDICE
                    p = new Persona(int.Parse(oDr["id"].ToString()), oDr["apellido"].ToString(),
                        oDr["nombre"].ToString(), int.Parse(oDr["edad"].ToString()));
                }
                //CIERRO EL DATAREADER
                oDr.Close();

                TodoOk = true;
            }

            catch (Exception)
            {
                TodoOk = false;
            }
            finally
            {

                if (TodoOk)
                    this._conexion.Close();

            }

            return p;
        }

        public bool ModificarPersona(Persona p)
        {
            bool todoOk = false;
            string sql = "UPDATE Personas SET nombre = '" + p.Nombre + "', apellido = '";
            sql = sql + p.Apellido + "', edad = " + p.Edad.ToString() + " WHERE id = " + p.ID.ToString();

            try
            {
                // LE PASO LA INSTRUCCION SQL
                this._comando.CommandText = sql;

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND
                this._comando.ExecuteNonQuery();

                todoOk = true;
            }
            catch (Exception)
            {
                todoOk = false;
            }
            finally
            {
                if (todoOk)
                    this._conexion.Close();
            }
            return todoOk;
        }

        public bool EliminarPersona(Persona p)
        {
            bool todoOk = false;

            string sql = "DELETE FROM Personas WHERE id = " + p.ID.ToString();

            try
            {
                // LE PASO LA INSTRUCCION SQL
                this._comando.CommandText = sql;

                // ABRO LA CONEXION A LA BD
                this._conexion.Open();

                // EJECUTO EL COMMAND
                this._comando.ExecuteNonQuery();

                todoOk = true;

            }
            catch (Exception)
            {
                todoOk = false;
            }
            finally
            {
                if (todoOk)
                    this._conexion.Close();
            }
            return todoOk;
        }
        #endregion
    }
}
