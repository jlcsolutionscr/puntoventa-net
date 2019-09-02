using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleasiento")]
    public partial class DetalleAsiento
    {
        [Key, Column(Order=0),ForeignKey("Asiento")]
        public int IdAsiento { get; set; }
        [Key, Column(Order=1)]
        public int Linea { get; set; }
        [ForeignKey("CatalogoContable")]
        public int IdCuenta { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        public decimal SaldoAnterior { get; set; }

        public Asiento Asiento { get; set; }
        public CatalogoContable CatalogoContable { get; set; }
    }
}
