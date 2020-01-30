using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
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
        public int IdSucursal { get; set; }
        [Key]
        public int IdProforma { get; set; }
        public int ConsecProforma { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string TextoAdicional { get; set; }
        public string Telefono { get; set; }
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto - Descuento; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }

        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleProforma> DetalleProforma { get; set; }
    }
}
