using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("provincia")]
    public partial class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public string Descripcion { get; set; }
    }
}
