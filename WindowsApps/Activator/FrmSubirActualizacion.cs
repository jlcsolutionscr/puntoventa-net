using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using LeandroSoftware.ClienteWCF;

namespace LeandroSoftware.Activator
{
    public partial class FrmSubirActualizacion : Form
    {
        private static string strServicioPuntoventaURL = ConfigurationManager.AppSettings["ServicioPuntoventaURL"];
        private static WebClient client = new WebClient();
        private byte[] bytZipFile;

        public FrmSubirActualizacion()
        {
            InitializeComponent();
        }

        private async void FrmSubirActualizacion_Load(object sender, EventArgs e)
        {
            try
            {
                string strVersionActual = await Administrador.ObtenerUltimaVersionApp();
                string[] lstVersion = strVersionActual.Split('.');
                txtVersion.Text = lstVersion[0];
                txtSubVersion.Text = lstVersion[1];
                txtBuild.Text = lstVersion[2];
                txtRevision.Text = lstVersion[3];
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

        private void btnCargar_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "msi";
            ofdAbrirDocumento.Filter = "MSI Files|*.msi;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    txtFileName.Text = ofdAbrirDocumento.FileName;
                    bytZipFile = File.ReadAllBytes(ofdAbrirDocumento.FileName);
                    btnActualizar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar cargar el archivo: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea proceder con la actualización?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string strVersion = txtVersion.Text + "." + txtSubVersion.Text + "." + txtBuild.Text + "." + txtRevision.Text;
                    await Administrador.ActualizarVersionApp(strVersion, bytZipFile, FrmMenu.strToken);
                    MessageBox.Show("Actualización procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                catch (WebException ex)
                {
                    if (ex.InnerException != null)
                        MessageBox.Show("Error: " + ex.InnerException.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
        }
    }
}
