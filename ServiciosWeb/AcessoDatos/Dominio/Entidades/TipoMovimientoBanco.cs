using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("tipomovimientobanco")]
    public partial class TipoMovimientoBanco
    {
        [Key]
        public int IdTipoMov { get; set; }
        public string DebeHaber { get; set; }
        public string Descripcion { get; set; }
    }
}
