using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("compra")]
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompra = new HashSet<DetalleCompra>();
            DesglosePagoCompra = new HashSet<DesglosePagoCompra>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdCompra { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public decimal TipoDeCambioDolar { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        [ForeignKey("CondicionVenta")]
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        public string Observaciones { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado - Descuento + Impuesto; } }
        public int IdCxP { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public int IdOrdenCompra { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }
        [NotMapped]
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public Proveedor Proveedor { get; set; }
        public CondicionVenta CondicionVenta { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; }
        public ICollection<DesglosePagoCompra> DesglosePagoCompra { get; set; }
    }
}
