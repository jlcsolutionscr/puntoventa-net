using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class OrdenServicio
    {
        public OrdenServicio()
        {
            DetalleOrdenServicio = new HashSet<DetalleOrdenServicio>();
            DesglosePagoOrdenServicio = new HashSet<DesglosePagoOrdenServicio>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdOrden { get; set; }
        public int ConsecOrdenServicio { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string FechaEntrega { get; set; }
        public string HoraEntrega { get; set; }
        public string OtrosDetalles { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal MontoAdelanto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Aplicado { get; set; }
        public bool Procesado { get; set; }

        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleOrdenServicio> DetalleOrdenServicio { get; set; }
        public ICollection<DesglosePagoOrdenServicio> DesglosePagoOrdenServicio { get; set; }
    }
}