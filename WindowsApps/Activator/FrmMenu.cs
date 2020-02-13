using System;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmMenu : Form
    {
        public static string strToken { get; set; }

        public FrmMenu()
        {
            InitializeComponent();
            FrmSeguridad formSeguridad = new FrmSeguridad();
            formSeguridad.ShowDialog(this);
            if (strToken != "")
            {
                tsRegistrarEmpresa.Visible = true;
                tsSubirNuevaVersionApp.Visible = true;
            } else
            {
                Application.Exit();
            }
        }

        private void tsRegistrarEmpresa_Click(object sender, EventArgs e)
        {
            FrmEmpresaListado listadoEmpresas = new FrmEmpresaListado
            {
                MdiParent = this
            };
            listadoEmpresas.Show();
        }

        private void tsSubirNuevaVersionApp_Click(object sender, EventArgs e)
        {
            FrmSubirActualizacion formSubirActualizacion = new FrmSubirActualizacion
            {
                MdiParent = this
            };
            formSubirActualizacion.Show();
        }

        private void tsSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConsultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDocumentosEnProceso formDocumentosEnProceso = new FrmDocumentosEnProceso
            {
                MdiParent = this
            };
            formDocumentosEnProceso.Show();
        }

        private void parametrosDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParametrosSistema formParametrosSistema = new FrmParametrosSistema
            {
                MdiParent = this
            };
            formParametrosSistema.Show();
        }
    }
}
