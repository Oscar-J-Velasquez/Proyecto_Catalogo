using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Form3: Form
    {
        Articulo articulo;
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;   
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = $"Código: {articulo.Codigo}";
            lblNombre.Text = $"Nombre del artículo: {articulo.Nombre}";
            lblDescripcion.Text = $"Descripción: {articulo.Descripcion}";
            lblMarca.Text = $"Marca: {articulo.Marca.Descripcion}";
            lblCategoria.Text = $"Categoría: {articulo.Categoria.Descripcion}";            
            lblPrecio.Text = $"Precio: ${articulo.Precio.ToString()}";

            cargarImagen(articulo.ImagenUrl);
        }
        public void cargarImagen(string imagenUrl)
        {
            try
            {
                pbxImagen.Load(imagenUrl);
            }
            catch (Exception)
            {

                pbxImagen.Load("https://demofree.sirv.com/nope-not-here.jpg");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
