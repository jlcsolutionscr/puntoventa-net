using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("linea")]
    public partial class Linea
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdLinea { get; set; }
        [ForeignKey("TipoProducto")]
        public int IdTipoProducto { get; set; }
        public string Descripcion { get; set; }
        [NotMapped]
        public string TipoLineaDesc { get { if (TipoProducto == null) return ""; else return TipoProducto.Descripcion; } }

        public Empresa Empresa { get; set; }
        public TipoProducto TipoProducto { get; set; }
    }
}
