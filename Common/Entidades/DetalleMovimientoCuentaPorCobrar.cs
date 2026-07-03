using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleMovimientoCuentaPorCobrar
    {
        public int IdMovCxC { get; set; }
        public int IdCxC { get; set; }
        public string NombrePropietario { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal Monto { get; set; }
        public MovimientoCuentaPorCobrar MovimientoCuentaPorCobrar { get; set; }
        public CuentaPorCobrar CuentaPorCobrar { get; set; }
    }
}