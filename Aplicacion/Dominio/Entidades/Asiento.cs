using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("asiento")]
    public partial class Asiento
    {
        public Asiento()
        {
            DetalleAsiento = new HashSet<DetalleAsiento>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdAsiento { get; set; }
        public int IdUsuario { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
        [NotMapped]
        public decimal Total { get { return TotalDebito; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<DetalleAsiento> DetalleAsiento { get; set; }
    }
}
