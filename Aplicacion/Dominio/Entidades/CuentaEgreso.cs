using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("cuentaegreso")]
    public partial class CuentaEgreso
    {
        public CuentaEgreso()
        {
            Egreso = new HashSet<Egreso>();
        }

        public int IdEmpresa { get; set; }
        [Key]
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Egreso> Egreso { get; set; }
    }
}
