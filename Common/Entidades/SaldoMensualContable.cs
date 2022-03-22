namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class SaldoMensualContable
    {
        public int IdCuenta { get; set; }
        public int Mes { get; set; }
        public int Annio { get; set; }
        public decimal SaldoFinMes { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
    }
}