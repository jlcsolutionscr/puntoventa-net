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
            DataGridViewTextBoxColumn dvcClave = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcFecha = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcEstado = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcError = new DataGridViewTextBoxColumn();
            dgvDatos.Columns.Clear();
            dgvDatos.AutoGenerateColumns = false;
            dvcId.HeaderText = "Id";
            dvcId.DataPropertyName = "IdDocumento";
            dvcId.Width = 0;
            dvcId.Visible = false;
            dvcFecha.HeaderText = "Fecha";
            dvcFecha.DataPropertyName = "Fecha";
            dvcFecha.Width = 80;
            dgvDatos.Columns.Add(dvcFecha);
            dgvDatos.Columns.Add(dvcId);
            dvcClave.HeaderText = "Clave";
            dvcClave.DataPropertyName = "ClaveNumerica";
            dvcClave.Width = 310;
            dgvDatos.Columns.Add(dvcClave);
            dvcEstado.HeaderText = "Estado";
            dvcEstado.DataPropertyName = "EstadoEnvio";
            dvcEstado.Width = 80;
            dgvDatos.Columns.Add(dvcEstado);
            dvcError.HeaderText = "Error envío";
            dvcError.DataPropertyName = "ErrorEnvio";
            dvcError.Width = 297;
            dgvDatos.Columns.Add(dvcError);
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
