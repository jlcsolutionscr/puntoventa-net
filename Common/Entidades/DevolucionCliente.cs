using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DevolucionCliente
    {
        public DevolucionCliente()
        {
            DetalleDevolucionCliente = new HashSet<DetalleDevolucionCliente>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdDevolucion { get; set; }
        public int IdFactura { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public int ConsecFactura { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Impuesto; } }
        public int IdMovimientoCxC { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
        public string IdDocElectronico { get; set; }
        public string IdDocElectronicoRev { get; set; }
        public string NombreCliente { get { if (Factura == null) return ""; else return Factura.Cliente.Nombre; } }

        public Factura Factura { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<DetalleDevolucionCliente> DetalleDevolucionCliente { get; set; }
    }
}