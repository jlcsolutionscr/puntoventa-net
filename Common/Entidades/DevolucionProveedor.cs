using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DevolucionProveedor
    {
        public int IdEmpresa { get; set; }
        public int IdDevolucion { get; set; }
        public int IdCompra { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Impuesto; } }
        public int IdMovimientoCxP { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }

        public Proveedor Proveedor { get; set; }
        public List<DetalleDevolucionProveedor> DetalleDevolucionProveedor { get; set; }
        public List<DesglosePagoDevolucionProveedor> DesglosePagoDevolucionProveedor { get; set; }
    }
}