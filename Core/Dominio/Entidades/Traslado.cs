using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("traslado")]
    public partial class Traslado
    {
        public Traslado()
        {
            DetalleTraslado = new HashSet<DetalleTraslado>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdTraslado { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdSucursalDestino { get; set; }
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public int IdAsiento { get; set; }
        public bool Aplicado { get; set; }
        public int? IdAplicadoPor { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<DetalleTraslado> DetalleTraslado { get; set; }
    }
}
