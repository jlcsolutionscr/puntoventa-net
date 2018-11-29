using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("detalledevolucioncliente")]
    public partial class DetalleDevolucionCliente
    {
        [Key, Column(Order = 0), ForeignKey("DevolucionCliente")]
        public int IdDevolucion { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal CantDevolucion { get; set; }

        public DevolucionCliente DevolucionCliente { get; set; }
        public Producto Producto { get; set; }
    }
}
