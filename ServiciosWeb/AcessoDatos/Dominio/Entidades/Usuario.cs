using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            RolePorUsuario = new HashSet<RolePorUsuario>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Clave { get; set; }
        [NotMapped]
        public string ClaveSinEncriptar { get; set; }
        public bool Modifica { get; set; }
        public bool AutorizaCredito { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<RolePorUsuario> RolePorUsuario { get; set; }
    }
}
