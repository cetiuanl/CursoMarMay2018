using CN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeDepot
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            dgvRoles.DataSource = Rol.traerTodos(true);
        }

        private void limpiarDatos()
        {
            txtNombre.Clear();
            txtDescripcion.Text = "";
            txtIdRol.Text = "0";
        }
        
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            int idRol;
            int.TryParse(txtIdRol.Text, out idRol);
            
            try
            {
                Rol oRol = new Rol(nombre, idRol, descripcion);
                oRol.guardar();
                MessageBox.Show("Proceso exitoso!");
                cargarDatos();
                limpiarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvRoles_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index < 0)
            {
                return;
            }
            string nombre = e.Row.Cells["nombre"].Value.ToString();

            string descripcion = "";
            if (e.Row.Cells["descripcion"].Value != null)
            {
                descripcion = e.Row.Cells["descripcion"].Value.ToString();
            }
            
            string idRol = e.Row.Cells["idRol"].Value.ToString();
            //string strEsActivo = e.Row.Cells["esActivo"].Value.ToString();            

            txtIdRol.Text = idRol;            
            txtNombre.Text = nombre;
            txtDescripcion.Text = descripcion;
            //chkEsActivo.Checked = (strEsActivo == "True");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int idRol;
            if (int.TryParse(txtIdRol.Text, out idRol))
            {
                try
                {
                    Rol.desactivar(idRol);
                    MessageBox.Show("Proceso exitoso!");
                    cargarDatos();
                    limpiarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
        }
    }
}
