using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("tipounidad")]
    public partial class TipoUnidad
    {
        [Key]
        public int IdTipoUnidad { get; set; }
        public string Descripcion { get; set; }
    }
}
