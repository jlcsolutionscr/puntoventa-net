using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("puntodeservicio")]
    public partial class PuntoDeServicio
    {
        [Key]
        public int IdPunto { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        [ForeignKey("IdEmpresa, IdSucursal")]
        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}
