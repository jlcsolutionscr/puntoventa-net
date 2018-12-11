using System;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmSeguridad : Form
    {
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            if (TxtUsuario.Text != "activator")
            {
                MessageBox.Show("Usuario incorrecto.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtUsuario.Text = "";
                TxtClave.Text = "";
                TxtUsuario.Focus();
            }
            else
            {
                if (TxtClave.Text == "A09c02t81i$")
                {
                    Close();
                }
                else
                {
                    MessageBox.Show("Clave incorrecta.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtClave.Text = "";
                    TxtClave.Focus();
                }
            }
        }

        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public FrmSeguridad()
        {
            InitializeComponent();
        }
    }
}
