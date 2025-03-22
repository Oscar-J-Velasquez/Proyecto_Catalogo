using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace UI
{
    public partial class Form2 : Form
    {
        private Articulo articulo = null;
        public Form2()
        {
            InitializeComponent();
            Text = "Agregar artículo";
        }
        public Form2(Articulo articulo) 
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }
        public void cargarImagen(string urlImagen)
        {
            try
            {
                pbxImagen.Load(urlImagen);
            }
            catch (Exception)
            {

                pbxImagen.Load("https://demofree.sirv.com/nope-not-here.jpg");
            }
        }
        private bool validarDatos(StringBuilder sb)
        {
            bool valido = true;

            if (string.IsNullOrWhiteSpace(tbxCodigo.Text) || tbxCodigo.Text.Contains("@") || tbxCodigo.Text.Contains("."))
            {
                sb.AppendLine("El código ingresado es incorrecto.");
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(tbxNombre.Text) || tbxNombre.Text.Contains("."))
            {
                sb.AppendLine("El nombre ingresado no es válido.");
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(tbxDescripcion.Text) || tbxDescripcion.Text.Contains("@"))
            {
                sb.AppendLine("La descripción ingresada no es válida.");
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(tbxUrlImagen.Text) || tbxUrlImagen.Text.Contains("@"))
            {
                sb.AppendLine("La Url ingresada no es válida.");
                valido = false;
            }
            if (!(decimal.TryParse(tbxPrecio.Text, out decimal precio)))
            {
                sb.AppendLine("El valor ingresado no es válido.");
                valido = false;
            }

            return valido;
        }

        private void Form2_Load(object sender, EventArgs e)
        {            
            cargarImagen(tbxUrlImagen.Text);
            cbxMarca.DataSource = new MarcaNegocio().listaMarca();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
            cbxCategoria.DataSource = new CategoriaNegocio().listaCategoria();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";

            if (articulo != null)
            {
                cargarImagen(articulo.ImagenUrl);
                tbxCodigo.Text = articulo.Codigo;
                tbxNombre.Text = articulo.Nombre;
                tbxDescripcion.Text = articulo.Descripcion;
                cbxMarca.SelectedValue = articulo.Marca.Id;
                cbxCategoria.SelectedValue = articulo.Categoria.Id;
                tbxUrlImagen.Text = articulo.ImagenUrl;
                tbxPrecio.Text = articulo.Precio.ToString();
            }            
        }       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();    
            StringBuilder sb = new StringBuilder();

            if (validarDatos(sb))
            {
                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.ImagenUrl = tbxUrlImagen.Text;
                articulo.Precio = decimal.Parse(tbxPrecio.Text);

                if (articulo.Id != 0)
                {
                    negocio.modificarArticulo(articulo);
                    MessageBox.Show("Modificado exitosamente.");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    negocio.agregarArticulo(articulo);
                    MessageBox.Show("Agregado exitosamente.");
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show(sb.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbxUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxUrlImagen.Text);
        }
    }
}
