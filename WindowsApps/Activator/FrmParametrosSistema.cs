using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Windows.Forms;
using LeandroSoftware.ClienteWCF;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftware.Activator
{
    public partial class FrmParametrosSistema : Form
    {
        public FrmParametrosSistema()
        {
            InitializeComponent();
        }

        private async void FrmSubirActualizacion_Load(object sender, EventArgs e)
        {
            try
            {
                List<ParametroSistema> listado = await Administrador.ObtenerListadoParametros(FrmMenu.strToken);
                txtPendientes.Text = listado.FirstOrDefault(x => x.IdParametro == 2).Valor;
                txtRecepcion.Text = listado.FirstOrDefault(x => x.IdParametro == 3).Valor;
                txtModoMantenimiento.Text = listado.FirstOrDefault(x => x.IdParametro == 5).Valor;
                btnActualizar.Enabled = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show("Error: " + ex.InnerException.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Error: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea proceder con la actualización?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    await Administrador.ActualizarParametroSistema(2, txtPendientes.Text, FrmMenu.strToken);
                    await Administrador.ActualizarParametroSistema(3, txtRecepcion.Text, FrmMenu.strToken);
                    await Administrador.ActualizarParametroSistema(5, txtModoMantenimiento.Text, FrmMenu.strToken);
                    MessageBox.Show("Actualización procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (WebException ex)
                {
                    if (ex.InnerException != null)
                        MessageBox.Show("Error: " + ex.InnerException.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        MessageBox.Show("Error: " + response, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }  
                    Close();
                }
            }
        }
    }
}
