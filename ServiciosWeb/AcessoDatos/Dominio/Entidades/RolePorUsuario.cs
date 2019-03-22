using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("roleporusuario")]
    public partial class RolePorUsuario
    {
        [Key, Column(Order = 0), ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [Key, Column(Order = 1), ForeignKey("Role")]
        public int IdRole { get; set; }

        public Usuario Usuario { get; set; }
        public Role Role { get; set; }
    }
}
