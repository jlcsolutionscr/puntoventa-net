using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
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
        [ForeignKey("Sucursal")]
        public int IdSucursal { get; set; }
        public int Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        public decimal Total { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        [NotMapped]
        public string NombreSucursal { get { if (Sucursal == null) return ""; else return Sucursal.Nombre; } }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<DetalleTraslado> DetalleTraslado { get; set; }
    }
}
