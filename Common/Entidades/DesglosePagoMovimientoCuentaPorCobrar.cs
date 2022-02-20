namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DesglosePagoMovimientoCuentaPorCobrar
    {
        public int IdConsecutivo { get; set; }
        public int IdMovCxC { get; set; }
        public int IdFormaPago { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCuentaBanco { get; set; }
        public string DescripcionCuenta { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal TipoDeCambio { get; set; }

        public MovimientoApartado MovimientoCuentaPorCobrar { get; set; }
    }
}
