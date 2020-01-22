using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("cuentaporcobrar")]
    public partial class CuentaPorCobrar
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdCxC { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMoneda")]
        public int IdTipoMoneda { get; set; }
        public int IdPropietario { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        [NotMapped]
        public string DescReferencia { get { return Descripcion + " Referencia: " + Referencia; } }
        public int NroDocOrig { get; set; }
        public DateTime Fecha { get; set; }
        public int Plazo { get; set; }
        public short Tipo { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public TipoMoneda TipoMoneda { get; set; }
    }
}
