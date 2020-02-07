using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("desglosepagodevolucioncliente")]
    public partial class DesglosePagoDevolucionCliente
    {
        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("DevolucionCliente")]
        public int IdDevolucion { get; set; }
        [ForeignKey("FormaPago")]
        public int IdFormaPago { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public int IdCuentaBanco { get; set; }
        [NotMapped]
        public string DescripcionCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal TipoDeCambio { get; set; }

        public DevolucionCliente DevolucionCliente { get; set; }
        public FormaPago FormaPago { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
    }
}
