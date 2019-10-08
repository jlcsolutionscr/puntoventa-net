using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeandroSoftware.Core.ClienteWCF;
using LeandroSoftware.Core.CommonTypes;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresaListado : Form
    {
        private FrmEmpresa empresaForm;
        private IList<LlaveDescripcion> dsDataSet = Array.Empty<LlaveDescripcion>();

        public FrmEmpresaListado()
        {
            InitializeComponent();
        }

        private async Task CargarListadoEmpresa()
        {
            try
            {
                dsDataSet = await ClienteFEWCF.ObtenerListadoEmpresasAdministrador();
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
            cboEmpresa.ValueMember = "Id";
            cboEmpresa.DisplayMember = "Descripcion";
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
