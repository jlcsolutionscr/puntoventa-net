using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeandroSoftware.ClienteWCF;
using LeandroSoftware.Core.TiposComunes;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresaListado : Form
    {
        private FrmEmpresa empresaForm;
        private List<LlaveDescripcion> dsDataSet = new List<LlaveDescripcion>();
        private string strToken;

        public FrmEmpresaListado()
        {
            InitializeComponent();
        }

        private async Task CargarListadoEmpresa()
        {
            try
            {
                dsDataSet = await Administrador.ObtenerListadoEmpresa(strToken);
                cboEmpresa.DataSource = dsDataSet;
                btnAgregar.Enabled = true;
                if (dsDataSet.Count > 0) btnEditar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void FrmEmpresaListado_Load(object sender, EventArgs e)
        {
            strToken = FrmMenu.strToken;
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
