using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("formapago")]
    public partial class FormaPago
    {
        [Key]
        public int IdFormaPago { get; set; }
        public string Descripcion { get; set; }
    }
}
