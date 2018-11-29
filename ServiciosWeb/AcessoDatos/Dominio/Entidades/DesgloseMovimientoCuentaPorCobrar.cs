using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("desglosemovimientocuentaporcobrar")]
    public partial class DesgloseMovimientoCuentaPorCobrar
    {
        [Key, Column(Order = 0), ForeignKey("MovimientoCuentaPorCobrar")]
        public int IdMovCxC { get; set; }
        [Key, Column(Order = 1), ForeignKey("CuentaPorCobrar")]
        public int IdCxC { get; set; }
        public decimal Monto { get; set; }

        public MovimientoCuentaPorCobrar MovimientoCuentaPorCobrar { get; set; }
        public CuentaPorCobrar CuentaPorCobrar { get; set; }
    }
}
