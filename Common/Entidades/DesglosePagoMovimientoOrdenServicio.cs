namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DesglosePagoMovimientoOrdenServicio
    {
        public int IdConsecutivo { get; set; }
        public int IdMovOrden { get; set; }
        public int IdFormaPago { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCuentaBanco { get; set; }
        public string DescripcionCuenta { get; set; }
        public string TipoTarjeta { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal TipoDeCambio { get; set; }

        public MovimientoOrdenServicio MovimientoOrdenServicio { get; set; }
    }
}
