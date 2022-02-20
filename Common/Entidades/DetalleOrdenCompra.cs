namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleOrdenCompra
    {
        public int IdConsecutivo { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PrecioVenta { get; set; }

        public OrdenCompra Orden { get; set; }
        public Producto Producto { get; set; }
    }
}