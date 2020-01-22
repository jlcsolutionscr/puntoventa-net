using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detalleefectivocierrecaja")]
    public partial class DetalleEfectivoCierreCaja
    {
        [Key, Column(Order = 0), ForeignKey("CierreCaja")]
        public int IdCierre { get; set; }
        [Key, Column(Order = 1)]
        public int Denominacion { get; set; }
        public int Cantidad { get; set; }

        public CierreCaja CierreCaja { get; set; }
    }
}
