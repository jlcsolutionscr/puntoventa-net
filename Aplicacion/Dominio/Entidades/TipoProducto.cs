using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("tipoproducto")]
    public partial class TipoProducto
    {
        [Key]
        public int IdTipoProducto { get; set; }
        public string Descripcion { get; set; }
    }
}
