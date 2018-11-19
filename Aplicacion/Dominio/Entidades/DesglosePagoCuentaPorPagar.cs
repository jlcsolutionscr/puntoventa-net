using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("desglosepagocuentaporpagar")]
    public partial class DesglosePagoCuentaPorPagar
    {
        [Key, Column(Order = 0), ForeignKey("CuentaPorPagar")]
        public int IdCxP { get; set; }
        [Key, Column(Order = 1), ForeignKey("FormaPago")]
        public int IdFormaPago { get; set; }
        [Key, Column(Order = 2), ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public int IdCuentaBanco { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal MontoForaneo { get; set; }

        public CuentaPorPagar CuentaPorPagar { get; set; }
        public FormaPago FormaPago { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
    }
}
