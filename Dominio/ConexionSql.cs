using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class ConexionSql
    {
        static SqlConnection conexion;
        static SqlCommand comando;
        static SqlDataReader lector;
        public static SqlCommand Comando { get { return comando; } }
        public static SqlDataReader Lector { get { return lector; } }

        static ConexionSql()
        {
            conexion = new SqlConnection("Data Source=DESKTOP-J6SUQQ6\\SQLEXPRESS01;Initial Catalog=CATALOGO_DB;Integrated Security=True");
        }
        public static void consulta(string consulta)
        {
            comando = new SqlCommand(consulta, conexion);                                    
        }
        public static void iniciarLectura()
        {
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }    
        }
        public static void insertarConsulta()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }        
        public static void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
        public static void agregarParametro(string parametro, object valor)
        {
            comando.Parameters.AddWithValue(parametro, valor);
        }
    }
}
