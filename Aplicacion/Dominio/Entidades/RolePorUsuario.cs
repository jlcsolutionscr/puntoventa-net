using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("roleporusuario")]
    public partial class RolePorUsuario
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [Key, Column(Order = 2), ForeignKey("Role")]
        public int IdRole { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Role Role { get; set; }
    }
}
