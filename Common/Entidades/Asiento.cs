using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Asiento
    {
        public int IdEmpresa { get; set; }
        public int IdAsiento { get; set; }
        public int IdUsuario { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal Total { get { return TotalDebito; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }

        public List<DetalleAsiento> DetalleAsiento { get; set; }
    }
}
