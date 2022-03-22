namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class ExistenciaPorSucursal
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
    }
}
