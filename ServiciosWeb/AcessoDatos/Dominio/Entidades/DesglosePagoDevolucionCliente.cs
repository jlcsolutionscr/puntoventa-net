using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("desglosepagodevolucioncliente")]
    public partial class DesglosePagoDevolucionCliente
    {
        [Key, Column(Order = 0), ForeignKey("DevolucionCliente")]
        public int IdDevolucion { get; set; }
        [Key, Column(Order = 1), ForeignKey("FormaPago")]
        public int IdFormaPago { get; set; }
        [Key, Column(Order = 2), ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        [Key, Column(Order = 3), ForeignKey("CuentaBanco")]
        public int IdCuentaBanco { get; set; }
        public string Beneficiario { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal MontoForaneo { get; set; }

        public DevolucionCliente DevolucionCliente { get; set; }
        public FormaPago FormaPago { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public CuentaBanco CuentaBanco { get; set; }
    }
}