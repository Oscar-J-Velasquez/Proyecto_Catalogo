using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listaCategoria()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            ConexionSql.consulta("SELECT * from CATEGORIAS");
            try
            {
                ConexionSql.iniciarLectura();
                while (ConexionSql.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = ConexionSql.Lector.GetInt32(0);
                    aux.Descripcion = ConexionSql.Lector.GetString(1);

                    listaCategoria.Add(aux);
                }
                return listaCategoria;
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
