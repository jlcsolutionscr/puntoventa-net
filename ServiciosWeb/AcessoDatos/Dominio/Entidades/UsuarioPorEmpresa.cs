using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("usuarioporempresa")]
    public partial class UsuarioPorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
    }
}
