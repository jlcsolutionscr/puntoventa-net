using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("existenciaporsucursal")]
    public partial class ExistenciaPorSucursal
    {
        [Key, Column(Order = 0)]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdSucursal { get; set; }
        [Key, Column(Order = 3)]
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
    }
}
