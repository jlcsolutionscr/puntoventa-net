using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("cierrecaja")]
    public partial class CierreCaja
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal? FondoInicio { get; set; }
        public decimal? VentasPorMayor { get; set; }
        public decimal? VentasDetalle { get; set; }
        public decimal? VentasContado { get; set; }
        public decimal? VentasCredito { get; set; }
        public decimal? VentasTarjeta { get; set; }
        public decimal? OtrasVentas { get; set; }
        public decimal? RetencionIVA { get; set; }
        public decimal? ComisionVT { get; set; }
        public decimal? LiquidacionTarjeta { get; set; }
        public decimal? IngresoCxCEfectivo { get; set; }
        public decimal? IngresoCxCTarjeta { get; set; }
        public decimal? DevolucionesProveedores { get; set; }
        public decimal? OtrosIngresos { get; set; }
        public decimal? ComprasContado { get; set; }
        public decimal? ComprasCredito { get; set; }
        public decimal? OtrasCompras { get; set; }
        public decimal? EgresoCxPEfectivo { get; set; }
        public decimal? DevolucionesClientes { get; set; }
        public decimal? OtrosEgresos { get; set; }
        public decimal? FondoCierre { get; set; }

        public Empresa Empresa { get; set; }
    }
}
