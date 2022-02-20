using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Compra
    {
        public Compra()
        {
            DetalleCompra = new HashSet<DetalleCompra>();
            DesglosePagoCompra = new HashSet<DesglosePagoCompra>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdCompra { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public decimal TipoDeCambioDolar { get; set; }
        public int IdProveedor { get; set; }
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        public string Observaciones { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Impuesto - Descuento; } }
        public int IdCxP { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public int IdOrdenCompra { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }

        public Usuario Usuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<DetalleCompra> DetalleCompra { get; set; }
        public ICollection<DesglosePagoCompra> DesglosePagoCompra { get; set; }
    }
}
