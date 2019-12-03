using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleproforma")]
    public partial class DetalleProforma
    {
        [Key, Column(Order = 0), ForeignKey("Proforma")]
        public int IdProforma { get; set; }
        [Key, Column(Order = 1), ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Excento { get; set; }
        public decimal PorcentajeIVA { get; set; }

        public Proforma Proforma { get; set; }
        public Producto Producto { get; set; }
    }
}
