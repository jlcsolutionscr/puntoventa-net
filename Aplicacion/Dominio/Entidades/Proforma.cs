using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("proforma")]
    public partial class Proforma
    {
        public Proforma()
        {
            DetalleProforma = new HashSet<DetalleProforma>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdProforma { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        [ForeignKey("CondicionVenta")]
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public short TipoPago { get; set; }
        public decimal Excento { get; set; }
        public decimal Grabado { get; set; }
        public decimal Descuento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Grabado + Impuesto; } }
        [NotMapped]
        public string NombreCliente { get { if (Cliente == null) return ""; else return Cliente.Nombre; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public CondicionVenta CondicionVenta { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleProforma> DetalleProforma { get; set; }
    }
}
