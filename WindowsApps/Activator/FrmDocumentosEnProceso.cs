using LeandroSoftware.ClienteWCF;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmDocumentosEnProceso : Form
    {
        private IList listadoDocumentosPendientes;
        private void EstablecerPropiedadesDataGrid()
        {
            DataGridViewTextBoxColumn dvcId = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcConsecutivo = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcClave = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcFecha = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcEstado = new DataGridViewTextBoxColumn();
            dgvDatos.Columns.Clear();
            dgvDatos.AutoGenerateColumns = false;
            dvcId.HeaderText = "Id";
            dvcId.DataPropertyName = "IdDocumento";
            dvcId.Width = 0;
            dvcId.Visible = false;
            dgvDatos.Columns.Add(dvcId);
            dvcConsecutivo.HeaderText = "Consecutivo";
            dvcConsecutivo.DataPropertyName = "Consecutivo";
            dvcConsecutivo.Width = 150;
            dgvDatos.Columns.Add(dvcConsecutivo);
            dvcClave.HeaderText = "Clave";
            dvcClave.DataPropertyName = "ClaveNumerica";
            dvcClave.Width = 370;
            dgvDatos.Columns.Add(dvcClave);
            dvcFecha.HeaderText = "Fecha";
            dvcFecha.DataPropertyName = "Fecha";
            dvcFecha.Width = 150;
            dgvDatos.Columns.Add(dvcFecha);
            dvcEstado.HeaderText = "Estado";
            dvcEstado.DataPropertyName = "EstadoEnvio";
            dvcEstado.Width = 80;
            dgvDatos.Columns.Add(dvcEstado);
        }

        public async Task ActualizarDatos()
        {
            try
            {
                picLoader.Visible = true;
                listadoDocumentosPendientes = await Administrador.ObtenerListadoDocumentosElectronicosPendientes(FrmMenu.strToken);
                dgvDatos.DataSource = listadoDocumentosPendientes;
                if (listadoDocumentosPendientes.Count == 0)
                {
                    MessageBox.Show("No existen documentos electrónicos pendientes de procesar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                picLoader.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            dgvDatos.Refresh();
        }

        public FrmDocumentosEnProceso()
        {
            InitializeComponent();
        }

        private async void DocumentosEnProceso_Load(object sender, EventArgs e)
        {
            EstablecerPropiedadesDataGrid();
            await ActualizarDatos();
        }

        private async void BtnProcesar_Click(object sender, EventArgs e)
        {
            btnProcesar.Enabled = false;
            try
            {
                await Administrador.ProcesarDocumentosElectronicosPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
