using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("tipomoneda")]
    public partial class TipoMoneda
    {
        [Key]
        public int IdTipoMoneda { get; set; }
        public string Descripcion { get; set; }
    }
}
