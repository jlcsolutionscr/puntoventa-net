namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleAsiento
    {
        public int IdAsiento { get; set; }
        public int Linea { get; set; }
        public int IdCuenta { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        public decimal SaldoAnterior { get; set; }
        public CatalogoContable CatalogoContable { get; set; }
    }
}