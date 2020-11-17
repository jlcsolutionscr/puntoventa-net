using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleordenservicio")]
    public partial class DetalleOrdenServicio
    {
        public DetalleOrdenServicio()
        {
        }

        [Key]
        public int IdConsecutivo { get; set; }
        [ForeignKey("OrdenServicio")]
        public int IdOrden { get; set; }
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        [NotMapped]
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal PorcDescuento { get; set; }

        public OrdenServicio OrdenServicio { get; set; }
        public Producto Producto { get; set; }
    }
}
