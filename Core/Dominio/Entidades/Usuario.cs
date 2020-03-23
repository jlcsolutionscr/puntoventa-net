using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            RolePorUsuario = new HashSet<RolePorUsuario>();
            UsuarioPorEmpresa = new HashSet<UsuarioPorEmpresa>();
        }

        [Key]
        public int IdUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Clave { get; set; }
        public decimal PorcMaxDescuento { get; set; }
        public bool PermiteRegistrarDispositivo { get; set; }
        [NotMapped]
        public string Token;

        public ICollection<RolePorUsuario> RolePorUsuario { get; set; }
        public ICollection<UsuarioPorEmpresa> UsuarioPorEmpresa { get; set; }
    }
}
