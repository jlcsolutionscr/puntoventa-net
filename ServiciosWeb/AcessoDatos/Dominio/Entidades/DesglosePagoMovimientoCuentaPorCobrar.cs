using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("desglosepagomovimientocuentaporcobrar")]
    public partial class DesglosePagoMovimientoCuentaPorCobrar
    {
        [Key, Column(Order = 0), ForeignKey("MovimientoCuentaPorCobrar")]
        public int IdMovCxC { get; set; }
        [Key, Column(Order = 1), ForeignKey("FormaPago")]
        public int IdFormaPago { get; set; }
        [Key, Column(Order = 2), ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        [Key, Column(Order = 3)]
        public int IdCuentaBanco { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal MontoForaneo { get; set; }

        public MovimientoCuentaPorCobrar MovimientoCuentaPorCobrar { get; set; }
        public FormaPago FormaPago { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
    }
}
