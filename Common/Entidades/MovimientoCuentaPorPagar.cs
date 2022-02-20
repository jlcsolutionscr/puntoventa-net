using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoCuentaPorPagar
    {
        public MovimientoCuentaPorPagar()
        {
            DesglosePagoMovimientoCuentaPorPagar = new HashSet<DesglosePagoMovimientoCuentaPorPagar>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovCxP { get; set; }
        public int IdUsuario { get; set; }
        public int IdPropietario { get; set; }
        public string NombrePropietario { get; set; }
        public short Tipo { get; set; }
        public int IdCxP { get; set; }
        public short TipoPropietario { get; set; }
        public string Recibo { get; set; }
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

        public Usuario Usuario { get; set; }
        public CuentaPorPagar CuentaPorPagar { get; set; }
        public ICollection<DesglosePagoMovimientoCuentaPorPagar> DesglosePagoMovimientoCuentaPorPagar { get; set; }
    }
}