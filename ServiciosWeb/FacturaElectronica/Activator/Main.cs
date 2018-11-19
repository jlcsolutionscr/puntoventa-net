using log4net;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Empresa empresaForm;
        private string strServicioFacturaElectronicaURL;
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 256000
        };

        public Main()
        {
            InitializeComponent();
            strServicioFacturaElectronicaURL = appSettings["ServicioFacturaElectronicaURL"];
        }

        private void CargarListadoEmpresa()
        {
            HttpClient client;
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            Uri uri = new Uri(strServicioFacturaElectronicaURL + "/consultarlistadoempresas");
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                EmpresaDTO[] dsDataSet = new JavaScriptSerializer().Deserialize<EmpresaDTO[]>(task1.Result.Content.ReadAsStringAsync().Result);
                cboEmpresa.DataSource = dsDataSet;
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            strServicioFacturaElectronicaURL = ConfigurationManager.AppSettings["ServicioFacturaElectronicaURL"];
            cboEmpresa.ValueMember = "IdEmpresa";
            cboEmpresa.DisplayMember = "NombreEmpresa";
            CargarListadoEmpresa();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            empresaForm = new Empresa();
            empresaForm.strServicioFacturaElectronicaURL = strServicioFacturaElectronicaURL;
            empresaForm.bolEditing = false;
            empresaForm.ShowDialog();
            CargarListadoEmpresa();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            empresaForm = new Empresa();
            empresaForm.strServicioFacturaElectronicaURL = strServicioFacturaElectronicaURL;
            empresaForm.bolEditing = true;
            empresaForm.intIdEmpresa = (int)cboEmpresa.SelectedValue;
            empresaForm.ShowDialog();
        }

        private void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = (int)cboEmpresa.SelectedValue;
            if (selectedValue > -1)
                btnEditar.Enabled = true;
        }
    }
}
