namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DesglosePagoMovimientoCuentaPorPagar
    {
        public int IdConsecutivo { get; set; }
        public int IdMovCxP { get; set; }
        public int IdFormaPago { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCuentaBanco { get; set; }
        public string DescripcionCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
