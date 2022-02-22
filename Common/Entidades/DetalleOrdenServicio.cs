namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleOrdenServicio
    {
        public int IdConsecutivo { get; set; }
        public int IdOrden { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PorcDescuento { get; set; }
        public Producto Producto { get; set; }
    }
}
