using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("condicionventa")]
    public partial class CondicionVenta
    {
        [Key]
        public int IdCondicionVenta { get; set; }
        public string Descripcion { get; set; }
    }
}
