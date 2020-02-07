using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleordencompra")]
    public partial class DetalleOrdenCompra
    {
        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("Orden")]
        public int IdOrdenCompra { get; set; }
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        [NotMapped]
        public decimal PrecioVenta { get; set; }

        public OrdenCompra Orden { get; set; }
        public Producto Producto { get; set; }
    }
}
