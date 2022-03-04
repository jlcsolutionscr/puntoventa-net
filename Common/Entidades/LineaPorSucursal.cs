namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class LineaPorSucursal
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdLinea { get; set; }

        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}