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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            var formExist = Application.OpenForms["frmRoles"] as frmRoles;
            if (formExist != null)
            {
                formExist.Focus();
            }
            else
            {
                frmRoles vRoles = new frmRoles();
                vRoles.MdiParent = this;
                vRoles.Show();
            }            
        }

        private void pruebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form vRoles = new Form();
            vRoles.MdiParent = this;
            vRoles.Show();
        }
    }
}
