namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleAjusteInventario
    {
        public int IdAjuste { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public Producto Producto { get; set; }
    }
}
