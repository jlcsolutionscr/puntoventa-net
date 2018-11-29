using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("detalleordenservicio")]
    public partial class DetalleOrdenServicio
    {
        public DetalleOrdenServicio()
        {
        }

        [Key, Column(Order = 0), ForeignKey("OrdenServicio")]
        public int IdOrden { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal CostoInstalacion { get; set; }

        public OrdenServicio OrdenServicio { get; set; }
        public Producto Producto { get; set; }
    }
}
