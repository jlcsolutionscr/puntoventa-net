using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoCuentaPorCobrar
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovCxC { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
        public List<DetalleMovimientoCuentaPorCobrar> DetalleMovimientoCuentaPorCobrar { get; set; }
        public List<DesglosePagoMovimientoCuentaPorCobrar> DesglosePagoMovimientoCuentaPorCobrar { get; set; }
    }
}