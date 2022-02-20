namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleTraslado
    {
        public int IdTraslado { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }

        public Traslado Traslado { get; set; }
        public Producto Producto { get; set; }
    }
}
