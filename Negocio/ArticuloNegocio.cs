using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulo()
        {
            List<Articulo> lista = new List<Articulo>();
            ConexionSql.consulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, C.Id IdCategoria, C.Descripcion DescripcionCategoria, M.Id IdMarca, M.Descripcion DescripcionMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id");
            try
            {
                ConexionSql.iniciarLectura();
                while (ConexionSql.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)ConexionSql.Lector["Id"];
                    aux.Codigo = (string)ConexionSql.Lector["Codigo"];
                    aux.Nombre = (string)ConexionSql.Lector["Nombre"];
                    aux.Descripcion = (string)ConexionSql.Lector["Descripcion"];
                    aux.Precio = (decimal)ConexionSql.Lector["Precio"];
                    aux.Marca.Id = (int)ConexionSql.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)ConexionSql.Lector["DescripcionMarca"];                    
                    aux.Categoria.Id = (int)ConexionSql.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)ConexionSql.Lector["DescripcionCategoria"];
                    aux.ImagenUrl = (string)ConexionSql.Lector["ImagenUrl"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ConexionSql.cerrarConexion();
            }
        }
        public void agregarArticulo(Articulo articulo)
        {
            try
            {
                ConexionSql.consulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                ConexionSql.agregarParametro("@Codigo", articulo.Codigo);
                ConexionSql.agregarParametro("@Nombre", articulo.Nombre);
                ConexionSql.agregarParametro("@Descripcion", articulo.Descripcion);
                ConexionSql.agregarParametro("@IdMarca", articulo.Marca.Id);
                ConexionSql.agregarParametro("@IdCategoria", articulo.Categoria.Id);
                ConexionSql.agregarParametro("@ImagenUrl", articulo.ImagenUrl);
                ConexionSql.agregarParametro("@Precio", articulo.Precio);
                ConexionSql.insertarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ConexionSql.cerrarConexion();
            }            
        }
        public void modificarArticulo(Articulo articulo)
        {
            try
            {
                ConexionSql.consulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                ConexionSql.agregarParametro("@Codigo", articulo.Codigo);
                ConexionSql.agregarParametro("@Nombre", articulo.Nombre);
                ConexionSql.agregarParametro("@Descripcion", articulo.Descripcion);
                ConexionSql.agregarParametro("@IdMarca", articulo.Marca.Id);
                ConexionSql.agregarParametro("@IdCategoria", articulo.Categoria.Id);
                ConexionSql.agregarParametro("@ImagenUrl", articulo.ImagenUrl);
                ConexionSql.agregarParametro("@Precio", articulo.Precio);
                ConexionSql.agregarParametro("@Id", articulo.Id);
                ConexionSql.insertarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ConexionSql.cerrarConexion();
            }            
        }
        public void eliminarArticulo(Articulo articulo)
        {
            try
            {
                ConexionSql.consulta("delete ARTICULOS where Id = @Id");
                ConexionSql.agregarParametro("@Id", articulo.Id);
                ConexionSql.insertarConsulta();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                ConexionSql.cerrarConexion();
            }
        }
        public void agregarParametro(string parametro, object valor)
        {
            ConexionSql.Comando.Parameters.AddWithValue(parametro, valor);            
        }
    }
}
