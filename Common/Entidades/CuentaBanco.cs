using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CuentaBanco
    {
        public CuentaBanco()
        {
            MovimientoBanco = new HashSet<MovimientoBanco>();
        }

        public int IdEmpresa { get; set; }
        public int IdCuenta { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }

        public ICollection<MovimientoBanco> MovimientoBanco { get; set; }
    }
}
