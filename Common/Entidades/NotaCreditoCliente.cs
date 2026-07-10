using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class NotaCreditoCliente
    {
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int IdNotaCredito { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public int Referencia { get; set; }
        public decimal MontoOriginal { get; set; }
        public decimal Saldo { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public Cliente Cliente { get; set; }
        public List<MovimientoNotaCreditoCliente> MovimientoNotaCreditoCliente { get; set; }
        
    }
}