using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("tipoparametrocontable")]
    public partial class TipoParametroContable
    {
        [Key]
        public int IdTipo { get; set; }
        public string Descripcion { get; set; }
        public bool MultiCuenta { get; set; }
    }
}
