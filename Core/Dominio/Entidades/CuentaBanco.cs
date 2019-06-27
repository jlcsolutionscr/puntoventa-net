using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("cuentabanco")]
    public partial class CuentaBanco
    {
        public CuentaBanco()
        {
            MovimientoBanco = new HashSet<MovimientoBanco>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdCuenta { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Saldo { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<MovimientoBanco> MovimientoBanco { get; set; }
    }
}
