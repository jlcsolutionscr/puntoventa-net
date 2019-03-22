using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("detalledevolucionproveedor")]
    public partial class DetalleDevolucionProveedor
    {
        [Key, Column(Order = 0), ForeignKey("DevolucionProveedor")]
        public int IdDevolucion { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }
        public decimal CantDevolucion { get; set; }
        public decimal PorcentajeIVA { get; set; }

        public DevolucionProveedor DevolucionProveedor { get; set; }
        public Producto Producto { get; set; }
    }
}
