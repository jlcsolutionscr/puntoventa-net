using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleapartado")]
    public partial class DetalleApartado
    {
        [Key, Column(Order = 0), ForeignKey("Apartado")]
        public int IdApartado { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PorcDescuento { get; set; }

        public Apartado Apartado { get; set; }
        public Producto Producto { get; set; }
    }
}
