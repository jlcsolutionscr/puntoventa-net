using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoApartado
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovApartado { get; set; }
        public int IdUsuario { get; set; }
        public int IdApartado { get; set; }
        public short Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoActual { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
        public Apartado Apartado { get; set; }
        public List<DesglosePagoMovimientoApartado> DesglosePagoMovimientoApartado { get; set; }
    }
}