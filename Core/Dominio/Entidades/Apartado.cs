using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("apartado")]
    public partial class Apartado
    {
        public Apartado()
        {
            DetalleApartado = new HashSet<DetalleApartado>();
            DesglosePagoApartado = new HashSet<DesglosePagoApartado>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdApartado { get; set; }
        public int ConsecApartado { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string TextoAdicional { get; set; }
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal MontoAdelanto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto - Descuento; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }
        public bool Procesado { get; set; }

        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleApartado> DetalleApartado { get; set; }
        public ICollection<DesglosePagoApartado> DesglosePagoApartado { get; set; }
    }
}
