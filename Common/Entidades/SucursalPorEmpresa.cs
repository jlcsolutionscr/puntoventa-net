namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class SucursalPorEmpresa
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool CierreEnEjecucion { get; set; }
        public int ConsecFactura { get; set; }
        public int ConsecProforma { get; set; }
        public int ConsecOrdenServicio { get; set; }
        public int ConsecApartado { get; set; }
        public Empresa Empresa { get; set; }
    }
}