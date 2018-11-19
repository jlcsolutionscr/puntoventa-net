using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("tipocuentacontable")]
    public partial class TipoCuentaContable
    {
        [Key]
        public int IdTipoCuenta { get; set; }
        public string TipoSaldo { get; set; }
        public string Descripcion { get; set; }
    }
}
