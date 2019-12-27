using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detallefactura")]
    public partial class DetalleFactura
    {
        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("Factura")]
        public int IdFactura { get; set; }
        [ForeignKey("Producto")]
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
