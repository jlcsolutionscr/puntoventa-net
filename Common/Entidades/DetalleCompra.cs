namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleCompra
    {
        public int IdConsecutivo { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioVentaAnt { get; set; }

        public Compra Compra { get; set; }
        public Producto Producto { get; set; }
    }
}