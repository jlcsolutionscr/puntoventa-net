namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class TerminalPorSucursal
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public string ValorRegistro { get; set; }
        public string ImpresoraFactura { get; set; }
        public int AnchoLinea { get; set; }
        public int UltimoDocFE { get; set; }
        public int UltimoDocND { get; set; }
        public int UltimoDocNC { get; set; }
        public int UltimoDocTE { get; set; }
        public int UltimoDocMR { get; set; }
        public int UltimoDocFEC { get; set; }
        public int IdTipoDispositivo { get; set; }

        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}