using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("role")]
    public partial class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string Nombre { get; set; }
        public string MenuPadre { get; set; }
        public string MenuItem { get; set; }
        public string Descripcion { get; set; }
    }
}
