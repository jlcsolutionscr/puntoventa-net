namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleDevolucionProveedor
    {
        public int IdDevolucion { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal CantDevolucion { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public Producto Producto { get; set; }
    }
}