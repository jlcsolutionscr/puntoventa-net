using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("egreso")]
    public partial class Egreso
    {
        public Egreso()
        {
            DesglosePagoEgreso = new HashSet<DesglosePagoEgreso>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdEgreso { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<DesglosePagoEgreso> DesglosePagoEgreso { get; set; }
    }
}
