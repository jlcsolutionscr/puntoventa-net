using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalletraslado")]
    public partial class DetalleTraslado
    {
        [Key, Column(Order = 0), ForeignKey("Traslado")]
        public int IdTraslado { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }

        public Traslado Traslado { get; set; }
        public Producto Producto { get; set; }
    }
}
