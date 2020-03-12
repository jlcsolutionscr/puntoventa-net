using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("movimientocuentaporpagar")]
    public partial class MovimientoCuentaPorPagar
    {
        public MovimientoCuentaPorPagar()
        {
            DesgloseMovimientoCuentaPorPagar = new HashSet<DesgloseMovimientoCuentaPorPagar>();
            DesglosePagoMovimientoCuentaPorPagar = new HashSet<DesglosePagoMovimientoCuentaPorPagar>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdMovCxP { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public int IdPropietario { get; set; }
        public short Tipo { get; set; }
        public short TipoPropietario { get; set; }
        public string Recibo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<DesgloseMovimientoCuentaPorPagar> DesgloseMovimientoCuentaPorPagar { get; set; }
        public ICollection<DesglosePagoMovimientoCuentaPorPagar> DesglosePagoMovimientoCuentaPorPagar { get; set; }
    }
}
