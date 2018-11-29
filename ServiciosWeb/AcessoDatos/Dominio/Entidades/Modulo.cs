using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("modulo")]
    public class Modulo
    {
        [Key]
        public int IdModulo { get; set; }
        public string Descripcion { get; set; }
        public string MenuPadre { get; set; }
    }
}
