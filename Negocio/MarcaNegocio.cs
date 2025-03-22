using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listaMarca()
        {
            List<Marca> listaMarca = new List<Marca>();
            ConexionSql.consulta("SELECT * from MARCAS");
            try
            {
                ConexionSql.iniciarLectura();

                while (ConexionSql.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = ConexionSql.Lector.GetInt32(0);
                    aux.Descripcion = ConexionSql.Lector.GetString(1);

                    listaMarca.Add(aux);
                }
                return listaMarca;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ConexionSql.cerrarConexion();
            }
        }
    }
}
