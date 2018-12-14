using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresaListado : Form
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FrmEmpresa empresaForm;
        private string strServicioPuntoventaURL;
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient();

        public FrmEmpresaListado()
        {
            InitializeComponent();
        }

        private async void CargarListadoEmpresa()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEmpresas",
                DatosPeticion = ""
            };
            try
            {
                string strPeticion = new JavaScriptSerializer().Serialize(peticion);
                string strRespuesta = await Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "");
                strRespuesta = new JavaScriptSerializer().Deserialize<string>(strRespuesta);
                IList<Empresa> dsDataSet = Array.Empty<Empresa>();
                dsDataSet = new JavaScriptSerializer().Deserialize<List<Empresa>>(strRespuesta); 
                cboEmpresa.DataSource = dsDataSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void FrmEmpresaListado_Load(object sender, EventArgs e)
        {
            strServicioPuntoventaURL = appSettings["ServicioPuntoventaURL"];
            cboEmpresa.ValueMember = "IdEmpresa";
            cboEmpresa.DisplayMember = "NombreEmpresa";
            CargarListadoEmpresa();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            empresaForm = new FrmEmpresa();
            empresaForm.strServicioPuntoventaURL = strServicioPuntoventaURL;
            empresaForm.bolEditing = false;
            empresaForm.ShowDialog();
            CargarListadoEmpresa();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            empresaForm = new FrmEmpresa();
            empresaForm.strServicioPuntoventaURL = strServicioPuntoventaURL;
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
