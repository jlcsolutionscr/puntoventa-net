using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresaListado : Form
    {
        private FrmEmpresa empresaForm;
        private string strServicioPuntoventaURL;
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient();
        private IList<Empresa> dsDataSet = Array.Empty<Empresa>();

        public FrmEmpresaListado()
        {
            InitializeComponent();
        }

        private async Task CargarListadoEmpresa()
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
                dsDataSet = new JavaScriptSerializer().Deserialize<List<Empresa>>(strRespuesta); 
                cboEmpresa.DataSource = dsDataSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void FrmEmpresaListado_Load(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            strServicioPuntoventaURL = appSettings["ServicioPuntoventaURL"];
            cboEmpresa.ValueMember = "IdEmpresa";
            cboEmpresa.DisplayMember = "NombreComercial";
            await CargarListadoEmpresa();
            btnAgregar.Enabled = true;
            if (dsDataSet.Count > 0) btnEditar.Enabled = true;
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            empresaForm = new FrmEmpresa
            {
                strServicioPuntoventaURL = strServicioPuntoventaURL,
                bolEditing = false
            };
            empresaForm.ShowDialog();
            await CargarListadoEmpresa();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            empresaForm = new FrmEmpresa
            {
                strServicioPuntoventaURL = strServicioPuntoventaURL,
                bolEditing = true,
                intIdEmpresa = (int)cboEmpresa.SelectedValue
            };
            empresaForm.ShowDialog();
        }

        private void CboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = (int)cboEmpresa.SelectedValue;
            if (selectedValue > -1)
                btnEditar.Enabled = true;
        }
    }
}
