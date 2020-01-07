using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("factura")]
    public partial class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
            DesglosePagoFactura = new HashSet<DesglosePagoFactura>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        [Key]
        public int IdFactura { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public decimal TipoDeCambioDolar { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        [ForeignKey("CondicionVenta")]
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string TextoAdicional { get; set; }
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        [ForeignKey("ParametroExoneracion")]
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string NombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAdelanto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto - Descuento; } }
        public int IdCxC { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdProforma { get; set; }
        public int IdApartado { get; set; }
        public decimal TotalCosto { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }
        public string IdDocElectronico { get; set; }
        public string IdDocElectronicoRev { get; set; }

        public Cliente Cliente { get; set; }
        public CondicionVenta CondicionVenta { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public ParametroExoneracion ParametroExoneracion { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleFactura> DetalleFactura { get; set; }
        public ICollection<DesglosePagoFactura> DesglosePagoFactura { get; set; }
    }
}
