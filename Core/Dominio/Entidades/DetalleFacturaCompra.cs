using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detallefacturacompra")]
    public partial class DetalleFacturaCompra
    {
        [Key, Column(Order = 0), ForeignKey("FacturaCompra")]
        public int IdFactCompra { get; set; }
        [Key, Column(Order = 1)]
        public int Linea { get; set; }
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdImpuesto { get; set; }
        public decimal PorcentajeIVA { get; set; }

        public FacturaCompra FacturaCompra { get; set; }
    }
}
