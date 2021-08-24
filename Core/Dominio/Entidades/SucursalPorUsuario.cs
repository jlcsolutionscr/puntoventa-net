using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("sucursalporusuario")]
    public partial class SucursalPorUsuario
    {
        [Key, Column(Order = 0)]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdSucursal { get; set; }
        [Key, Column(Order = 2)]
        public int IdUsuario { get; set; }

        [ForeignKey("IdEmpresa, IdSucursal")]
        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
