using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("movimientocuentaporcobrar")]
    public partial class MovimientoCuentaPorCobrar
    {
        public MovimientoCuentaPorCobrar()
        {
            DesglosePagoMovimientoCuentaPorCobrar = new HashSet<DesglosePagoMovimientoCuentaPorCobrar>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdMovCxC { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public int IdPropietario { get; set; }
        [NotMapped]
        public string NombrePropietario { get; set; }
        public short Tipo { get; set; }
        [ForeignKey("CuentaPorCobrar")]
        public int IdCxC { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public CuentaPorCobrar CuentaPorCobrar { get; set; }
        public ICollection<DesglosePagoMovimientoCuentaPorCobrar> DesglosePagoMovimientoCuentaPorCobrar { get; set; }
    }
}
