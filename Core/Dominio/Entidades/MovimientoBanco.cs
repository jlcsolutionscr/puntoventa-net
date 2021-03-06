using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("movimientobanco")]
    public partial class MovimientoBanco
    {
        [Key]
        public int IdMov { get; set; }
        public int IdSucursal { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey("TipoMovimientoBanco")]
        public int IdTipo { get; set; }
        [ForeignKey("CuentaBanco")]
        public int IdCuenta { get; set; }
        public string Numero { get; set; }
        public string Beneficiario { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }

        public TipoMovimientoBanco TipoMovimientoBanco { get; set; }
        public CuentaBanco CuentaBanco { get; set; }
    }
}
