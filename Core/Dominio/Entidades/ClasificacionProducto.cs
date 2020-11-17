using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("clasificacionproducto")]
    public partial class ClasificacionProducto
    {
        [Key]
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public int Impuesto { get; set; }
    }
}
