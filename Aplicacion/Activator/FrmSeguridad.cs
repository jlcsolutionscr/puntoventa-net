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
                Usuario usuario = await ClienteWCF.ValidarCredenciales(TxtUsuario.Text, TxtClave.Text);
                if (usuario != null)
                {
                    FrmMenu.strToken = usuario.Token;
                    Close();
                }
                else
                {
                    MessageBox.Show("Clave incorrecta.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtClave.Text = "";
                    TxtClave.Focus();
                }
            }
            catch (Exception ex)
            {
                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(strError, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
