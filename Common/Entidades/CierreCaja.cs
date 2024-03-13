using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CierreCaja
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal FondoInicio { get; set; }
        public decimal AdelantosApartadoEfectivo { get; set; }
        public decimal AdelantosApartadoTarjeta { get; set; }
        public decimal AdelantosApartadoBancos { get; set; }
        public decimal AdelantosOrdenEfectivo { get; set; }
        public decimal AdelantosOrdenTarjeta { get; set; }
        public decimal AdelantosOrdenBancos { get; set; }
        public decimal VentasEfectivo { get; set; }
        public decimal VentasTarjeta { get; set; }
        public decimal VentasBancos { get; set; }
        public decimal IngresosEfectivo { get; set; }
        public decimal PagosCxCEfectivo { get; set; }
        public decimal PagosCxCTarjeta { get; set; }
        public decimal PagosCxCBancos { get; set; }
        public decimal ComprasEfectivo { get; set; }
        public decimal ComprasBancos { get; set; }
        public decimal EgresosEfectivo { get; set; }
        public decimal PagosCxPEfectivo { get; set; }
        public decimal PagosCxPBancos { get; set; }
        public decimal RetencionTarjeta { get; set; }
        public decimal ComisionTarjeta { get; set; }
        public decimal LiquidacionTarjeta { get; set; }
        public decimal VentasCredito { get; set; }
        public decimal ComprasCredito { get; set; }
        public decimal RetiroEfectivo { get; set; }
        public decimal FondoCierre { get; set; }
        public string Observaciones { get; set; }

        public List<DetalleEfectivoCierreCaja> DetalleEfectivoCierreCaja { get; set; }
        public List<DetalleMovimientoCierreCaja> DetalleMovimientoCierreCaja { get; set; }
    }
}
