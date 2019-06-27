using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("cuentaingreso")]
    public partial class CuentaIngreso
    {
        public CuentaIngreso()
        {
            Ingreso = new HashSet<Ingreso>();
        }

        public int IdEmpresa { get; set; }
        [Key]
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Ingreso> Ingreso { get; set; }
    }
}
