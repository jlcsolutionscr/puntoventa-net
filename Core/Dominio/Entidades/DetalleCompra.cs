using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detallecompra")]
    public partial class DetalleCompra
    {
        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        [NotMapped]
        public decimal PrecioVenta { get; set; }

        public Compra Compra { get; set; }
        public Producto Producto { get; set; }
    }
}
