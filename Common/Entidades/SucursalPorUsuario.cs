namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class SucursalPorUsuario
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdUsuario { get; set; }

        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
        public Usuario Usuario { get; set; }
    }
}