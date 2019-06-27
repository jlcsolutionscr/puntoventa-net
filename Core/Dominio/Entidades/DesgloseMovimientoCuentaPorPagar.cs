using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("desglosemovimientocuentaporpagar")]
    public partial class DesgloseMovimientoCuentaPorPagar
    {
        [Key, Column(Order = 0), ForeignKey("MovimientoCuentaPorPagar")]
        public int IdMovCxP { get; set; }
        [Key, Column(Order = 1), ForeignKey("CuentaPorPagar")]
        public int IdCxP { get; set; }
        public decimal Monto { get; set; }

        public MovimientoCuentaPorPagar MovimientoCuentaPorPagar { get; set; }
        public CuentaPorPagar CuentaPorPagar { get; set; }
    }
}
