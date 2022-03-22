using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class OrdenCompra
    {
        public int IdEmpresa { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        public short TipoPago { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Impuesto; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }
        public Proveedor Proveedor { get; set; }
        public List<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
    }
}