using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace UI
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        
        public Form1()
        {
            InitializeComponent();
            Text = "Catálogo de artículos";
        }      
        private void cargarLista()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.listarArticulo();
            dgvArticulos.DataSource = listaArticulo;
            ocultarColumna();
        }
        private void ocultarColumna()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
        }
        private Articulo getArtSeleccionado()
        {
            Articulo artSeleccionado = null;
            if (dgvArticulos.Rows.Count > 0)
            {                
                if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
                {
                    artSeleccionado = dgvArticulos.CurrentRow.DataBoundItem as Articulo;
                    return artSeleccionado; 
                }
            }
            
            return artSeleccionado;
        }
        private void Form1_Load(object sender, EventArgs e)
        {                              
            cargarLista();            
            cargarImagen(listaArticulo[0].ImagenUrl);
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Marca");
            cbxCampo.Items.Add("Categoría");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            if (form2.ShowDialog() == DialogResult.OK)
            {
                cargarLista();
            }            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (getArtSeleccionado() != null)
            {
                Form2 form2 = new Form2(getArtSeleccionado());

                if (form2.ShowDialog() == DialogResult.OK)
                {
                    cargarLista();
                }
            }
            else
            {
                MessageBox.Show("No hay un artículo seleccionado.");
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (getArtSeleccionado() != null)
            {
                DialogResult resultado = MessageBox.Show("¿Esta seguro de que desea eliminar el artículo seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (resultado == DialogResult.OK)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminarArticulo(getArtSeleccionado());
                    MessageBox.Show("Artículo eliminado exitosamente.");
                    cargarLista();
                }
            }
            else
            {
                MessageBox.Show("No hay un artículo seleccionado.");
            }            
        }
        public void cargarImagen(string urlImagen)
        {
            try
            {
                pbxImagenCatalogo.Load(urlImagen);
            }
            catch (Exception)
            {

                pbxImagenCatalogo.Load("https://demofree.sirv.com/nope-not-here.jpg");
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (getArtSeleccionado() != null)
            {
                cargarImagen(getArtSeleccionado().ImagenUrl);
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (getArtSeleccionado() != null)
            {
                Form3 form3 = new Form3(getArtSeleccionado());
                form3.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay un artículo seleccionado.");
            }
            
        }

        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada = listaArticulo;
            string filtro = tbxFiltro.Text;
            string seleccionado = "";

            if (cbxCampo.SelectedItem != null)
            {
                seleccionado = cbxCampo.SelectedItem.ToString();
            }

            if (filtro.Length >= 3)
            {
                if (seleccionado == "Nombre")
                {
                    listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
                }
                else if (seleccionado == "Marca")
                {
                    listaFiltrada = listaArticulo.FindAll(x => x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()));
                }
                else if (seleccionado == "Categoría")
                {
                    listaFiltrada = listaArticulo.FindAll(x => x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
                }
            }
            else
            {
                listaFiltrada = listaArticulo;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumna();
        }
        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            tbxFiltro.Text = "";
            cbxCampo.SelectedItem = null;
        }
    }
}
