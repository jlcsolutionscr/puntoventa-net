using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("parametroexoneracion")]
    public partial class ParametroExoneracion
    {
        [Key]
        public int IdTipoExoneracion { get; set; }
        public string Descripcion { get; set; }
    }
}
