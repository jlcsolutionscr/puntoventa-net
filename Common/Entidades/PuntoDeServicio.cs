namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class PuntoDeServicio
    {
        public int IdPunto { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}