namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DesglosePagoDevolucionProveedor
    {
        public int IdConsecutivo { get; set; }
        public int IdDevolucion { get; set; }
        public int IdFormaPago { get; set; }
        public int IdReferencia { get; set; }
        public string DescripcionCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string NroMovimiento { get; set; }
        public decimal MontoLocal { get; set; }
    }
}
