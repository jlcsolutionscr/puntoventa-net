using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("ordenservicio")]
    public partial class OrdenServicio
    {
        public OrdenServicio()
        {
            DetalleOrdenServicio = new HashSet<DetalleOrdenServicio>();
            DesglosePagoOrdenServicio = new HashSet<DesglosePagoOrdenServicio>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdOrden { get; set; }
        public int ConsecOrdenServicio { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        [ForeignKey("Vendedor")]
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
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Aplicado { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleOrdenServicio> DetalleOrdenServicio { get; set; }
        public ICollection<DesglosePagoOrdenServicio> DesglosePagoOrdenServicio { get; set; }
    }
}
