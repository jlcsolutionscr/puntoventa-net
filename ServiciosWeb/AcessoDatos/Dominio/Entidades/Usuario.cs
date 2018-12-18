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
            EmpresaPorUsuario = new HashSet<EmpresaPorUsuario>();
        }

        [Key]
        public int IdUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Clave { get; set; }
        [NotMapped]
        public string ClaveSinEncriptar { get; set; }
        public bool Modifica { get; set; }
        public bool AutorizaCredito { get; set; }
        [NotMapped]
        public Empresa Empresa;

        public ICollection<RolePorUsuario> RolePorUsuario { get; set; }
        public ICollection<EmpresaPorUsuario> EmpresaPorUsuario { get; set; }
    }
}
