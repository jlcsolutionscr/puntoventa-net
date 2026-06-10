using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoNotaCreditoCliente
    {
        public int IdNotaCredito { get; set; }
        public int Consecutivo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int IdFactura { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public NotaCreditoCliente NotaCreditoCliente { get; set; }
    }
}