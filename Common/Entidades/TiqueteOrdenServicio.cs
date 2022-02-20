namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class TiqueteOrdenServicio
    {
        public int IdTiquete { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public string Impresora { get; set; }
        public byte[] Lineas { get; set; }
        public bool Impreso { get; set; }

        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}