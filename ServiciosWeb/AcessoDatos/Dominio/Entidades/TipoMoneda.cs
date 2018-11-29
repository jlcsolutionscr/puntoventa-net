using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("tipomoneda")]
    public partial class TipoMoneda
    {
        [Key]
        public int IdTipoMoneda { get; set; }
        public string Descripcion { get; set; }
        public decimal TipoCambioCompra { get; set; }
        public decimal TipoCambioVenta { get; set; }
    }
}
