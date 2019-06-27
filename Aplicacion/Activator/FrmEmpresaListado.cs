using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.ClienteWCF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeandroSoftware.Core.CustomClasses;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresaListado : Form
    {
        private FrmEmpresa empresaForm;
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient();
        private static CustomJavascriptSerializer serializer = new CustomJavascriptSerializer();
        private IList<Empresa> dsDataSet = Array.Empty<Empresa>();

        public FrmEmpresaListado()
        {
            InitializeComponent();
        }

        private async Task CargarListadoEmpresa()
        {
            try
            {
                dsDataSet = await PuntoventaWCF.ObtenerListaEmpresas();
                cboEmpresa.DataSource = dsDataSet;
                btnAgregar.Enabled = true;
                btnEditar.Enabled = true;
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
                bolEditing = false
            };
            empresaForm.ShowDialog();
            await CargarListadoEmpresa();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            empresaForm = new FrmEmpresa
            {
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
