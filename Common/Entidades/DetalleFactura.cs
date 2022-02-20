namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleFactura
    {
        public int IdConsecutivo { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PorcDescuento { get; set; }
        public decimal CantDevuelto { get; set; }

        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }
}
