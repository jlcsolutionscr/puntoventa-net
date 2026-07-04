namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleMovimientoCuentaPorPagar
    {
        public int IdMovCxP { get; set; }
        public int IdCxP { get; set; }
        public string NombrePropietario { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal Monto { get; set; }
        public CuentaPorPagar CuentaPorPagar { get; set; }
    }
}