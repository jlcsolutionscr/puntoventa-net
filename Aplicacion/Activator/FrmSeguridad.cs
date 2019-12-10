using LeandroSoftware.ClienteWCF;
using LeandroSoftware.Core.Dominio.Entidades;
using System;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmSeguridad : Form
    {
        private async void CmdAceptar_Click(object sender, EventArgs e)
        {

            try
            {
                CmdAceptar.Enabled = false;
                CmdCancelar.Enabled = false;
                Usuario usuario = await Administrador.ValidarCredencialesAdmin(TxtUsuario.Text, TxtClave.Text);
                if (usuario != null)
                {
                    FrmMenu.strToken = usuario.Token;
                    Close();
                }
                else
                {
                    CmdAceptar.Enabled = true;
                    CmdCancelar.Enabled = true;
                    MessageBox.Show("Clave incorrecta.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtClave.Text = "";
                    TxtClave.Focus();
                }
            }
            catch (Exception ex)
            {
                CmdAceptar.Enabled = true;
                CmdCancelar.Enabled = true;
                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(strError, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public FrmSeguridad()
        {
            InitializeComponent();
        }
    }
}
