using LeandroSoftware.AccesoDatos.ClienteWCF;
using LeandroSoftware.PuntoVenta.Datos;
using LeandroSoftware.PuntoVenta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Windows.Forms;

namespace Migracion
{
    public partial class Migracion : Form
    {
        private static string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;

        public Migracion()
        {
            InitializeComponent();
        }

        private async void btnProcesar_Click(object sender, EventArgs e)
        {
            rtbSalida.AppendText("INICIO DEL PROCESO DE MIGRACION\n");
            rtbSalida.AppendText("Agregando registros de banco adquiriente:\n");
            IDictionary<int, int> bancosDict = new Dictionary<int, int>();
            using (IDbContext dbContext = new LeandroContext(connString))
            {
                List<BancoAdquiriente> bancoAdquirientes = dbContext.BancoAdquirienteRepository.ToList();
                foreach (BancoAdquiriente banco in bancoAdquirientes)
                {
                    LeandroSoftware.AccesoDatos.Dominio.Entidades.BancoAdquiriente nuevoBanco = new LeandroSoftware.AccesoDatos.Dominio.Entidades.BancoAdquiriente();
                    nuevoBanco.IdEmpresa = banco.IdEmpresa;
                    nuevoBanco.Descripcion = banco.Descripcion;
                    nuevoBanco.PorcentajeComision = banco.PorcentajeComision;
                    nuevoBanco.PorcentajeRetencion = banco.PorcentajeRetencion;
                    string strIdBanco = await PuntoventaWCF.AgregarBancoAdquiriente(nuevoBanco);
                    bancosDict.Add(banco.IdBanco, int.Parse(strIdBanco));
                    rtbSalida.AppendText("Migración de banco adquiriente IdLocal: " + banco.IdBanco + " IdServer: " + strIdBanco + "\n");
                }

            }
            rtbSalida.AppendText("FIN DEL PROCESO DE MIGRACION");
        }
    }
}
