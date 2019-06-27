using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detallecompra")]
    public partial class DetalleCompra
    {
        [Key, Column(Order = 0), ForeignKey("Compra")]
        public int IdCompra { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }

        public Compra Compra { get; set; }
        public Producto Producto { get; set; }
    }
}
