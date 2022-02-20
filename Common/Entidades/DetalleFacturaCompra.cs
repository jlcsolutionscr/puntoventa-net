namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleFacturaCompra
    {
        public int IdFactCompra { get; set; }
        public int Linea { get; set; }
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdImpuesto { get; set; }
        public decimal PorcentajeIVA { get; set; }

        public FacturaCompra FacturaCompra { get; set; }
    }
}