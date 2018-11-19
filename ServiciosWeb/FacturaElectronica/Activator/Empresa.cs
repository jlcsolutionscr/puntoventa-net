using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Empresa : Form
    {
        EmpresaDTO empresa;
        public string strServicioFacturaElectronicaURL;
        public bool bolEditing;
        public int intIdEmpresa = -1;
        private static HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 256000
        };

        public Empresa()
        {
            InitializeComponent();
        }

        private void Empresa_Load(object sender, EventArgs e)
        {
            try
            {
                if (bolEditing)
                {
                    Uri uri = new Uri(strServicioFacturaElectronicaURL + "/consultarempresa?empresa=" + intIdEmpresa);
                    Task<HttpResponseMessage> task1 = client.GetAsync(uri);
                    try
                    {
                        task1.Wait();
                        if (!task1.Result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        empresa = new JavaScriptSerializer().Deserialize<EmpresaDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                        txtIdEmpresa.Text = empresa.IdEmpresa.ToString();
                        txtNombreEmpresa.Text = empresa.NombreEmpresa;
                        txtUsuarioATV.Text = empresa.UsuarioATV;
                        txtClaveATV.Text = empresa.ClaveATV;
                        txtCorreoNotificacion.Text = empresa.CorreoNotificacion;
                        chkPermiteFacturar.Checked = empresa.PermiteFacturar == "S";
                    }
                    catch (AggregateException ex)
                    {
                        Exception newEx = ex.Flatten();
                        MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                else
                {
                    empresa = new EmpresaDTO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private bool ValidarCampos()
        {
            if (txtIdEmpresa.Text == "" & txtNombreEmpresa.Text == "" & txtUsuarioATV.Text == "" & txtClaveATV.Text == "")
                return false;
            else
            {
                return true;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Información incompleta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                empresa.NombreEmpresa = txtNombreEmpresa.Text;
                empresa.UsuarioATV = txtUsuarioATV.Text;
                empresa.ClaveATV = txtClaveATV.Text;
                empresa.CorreoNotificacion = txtCorreoNotificacion.Text;
                empresa.PermiteFacturar = chkPermiteFacturar.Checked ? "S" : "N";
                string jsonRequest = new JavaScriptSerializer().Serialize(empresa);
                StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                Uri uri = new Uri(strServicioFacturaElectronicaURL + "/registrarempresa");
                Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
                try
                {
                    task1.Wait();
                    if (!task1.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (txtIdEmpresa.Text == "")
                        txtIdEmpresa.Text = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                }
                catch (AggregateException ex)
                {
                    Exception newEx = ex.Flatten();
                    MessageBox.Show("Error al actualizar el registro: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro" + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Close();
        }
    }
}
