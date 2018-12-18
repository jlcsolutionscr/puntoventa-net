using System;
using System.Windows.Forms;

namespace LeandroSoftware.Activator
{
    public partial class FrmMenu : Form
    {
        public string strCadenaConexion, strMySQLDumpOptions, strThumbprint, strSubjectName, strApplicationKey, strDatabase, strUser, strPassword, strHost;
        public bool bolSeguridad = true;
        // private bool bolArchivoConfig = true;

        public FrmMenu()
        {
            InitializeComponent();
            if (bolSeguridad)
            {
                bolSeguridad = false;
                FrmSeguridad formSeguridad = new FrmSeguridad();
                formSeguridad.ShowDialog();
                if (bolSeguridad)
                {
                    tsRegistrarEmpresa.Visible = true;
                }
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
    }
}
