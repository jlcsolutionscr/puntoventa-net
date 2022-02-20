using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CuentaIngreso
    {
        public CuentaIngreso()
        {
            Ingreso = new HashSet<Ingreso>();
        }

        public int IdEmpresa { get; set; }
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ingreso> Ingreso { get; set; }
    }
}