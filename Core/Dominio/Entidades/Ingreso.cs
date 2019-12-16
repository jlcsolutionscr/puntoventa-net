using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("ingreso")]
    public partial class Ingreso
    {
        public Ingreso()
        {
            DesglosePagoIngreso = new HashSet<DesglosePagoIngreso>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdIngreso { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCuenta { get; set; }
        public string Detalle { get; set; }
        public string RecibidoDe { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<DesglosePagoIngreso> DesglosePagoIngreso { get; set; }
    }
}
