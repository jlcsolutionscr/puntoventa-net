using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("roleporempresa")]
    public partial class RolePorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("Role")]
        public int IdRole { get; set; }

        public Empresa Empresa { get; set; }
        public Role Role { get; set; }
    }
}
