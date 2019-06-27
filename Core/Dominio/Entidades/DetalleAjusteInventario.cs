using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleajusteinventario")]
    public partial class DetalleAjusteInventario
    {
        [Key, Column(Order = 0), ForeignKey("AjusteInventario")]
        public int IdAjuste { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public bool Excento { get; set; }

        public AjusteInventario AjusteInventario { get; set; }
        public Producto Producto { get; set; }
    }
}
