using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("parametrosistema")]
    public partial class ParametroSistema
    {
        [Key]
        public int IdParametro { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
    }
}
