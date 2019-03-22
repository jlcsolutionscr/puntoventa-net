using System;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmMenu : Form
    {
        public bool bolSeguridad = false;

        public FrmMenu()
        {
            InitializeComponent();
            FrmSeguridad formSeguridad = new FrmSeguridad();
            formSeguridad.ShowDialog(this);
            if (bolSeguridad)
            {
                tsRegistrarEmpresa.Visible = true;
                tsSubirNuevaVersionApp.Visible = true;
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
    }
}
