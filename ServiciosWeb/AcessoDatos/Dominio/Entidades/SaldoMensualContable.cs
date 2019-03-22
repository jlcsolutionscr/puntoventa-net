using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("saldomensualcontable")]
    public partial class SaldoMensualContable
    {
        [Key, Column(Order = 0), ForeignKey("CatalogoContable")]
        public int IdCuenta { get; set; }
        [Key, Column(Order = 1)]
        public int Mes { get; set; }
        [Key, Column(Order = 2)]
        public int Annio { get; set; }
        public decimal SaldoFinMes { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }

        public CatalogoContable CatalogoContable { get; set; }
    }
}
