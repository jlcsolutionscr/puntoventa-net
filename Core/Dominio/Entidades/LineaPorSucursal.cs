using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("lineaporsucursal")]
    public partial class LineaPorSucursal
    {
        [Key, Column(Order = 0)]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdSucursal { get; set; }
        [Key, Column(Order = 2)]
        public int IdLinea { get; set; }

        [ForeignKey("IdEmpresa, IdSucursal")]
        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}
