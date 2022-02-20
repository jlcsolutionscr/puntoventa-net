using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CuentaEgreso
    {
        public CuentaEgreso()
        {
            Egreso = new HashSet<Egreso>();
        }

        public int IdEmpresa { get; set; }
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Egreso> Egreso { get; set; }
    }
}
