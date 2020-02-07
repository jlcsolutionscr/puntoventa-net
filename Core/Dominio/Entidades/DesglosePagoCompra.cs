using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("desglosepagocompra")]
    public partial class DesglosePagoCompra
    {
        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }
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

        public Compra Compra { get; set; }
        public FormaPago FormaPago { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
    }
}
