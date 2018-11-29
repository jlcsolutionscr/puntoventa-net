using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("detalleordencompra")]
    public partial class DetalleOrdenCompra
    {
        [Key, Column(Order = 0), ForeignKey("Orden")]
        public int IdOrdenCompra { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }

        public OrdenCompra Orden { get; set; }
        public Producto Producto { get; set; }
    }
}
