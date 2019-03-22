using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("parametroimpuesto")]
    public class ParametroImpuesto
    {
        [Key]
        public int IdImpuesto { get; set; }
        public string Descripcion { get; set; }
        public decimal TasaImpuesto { get; set; }
    }
}
