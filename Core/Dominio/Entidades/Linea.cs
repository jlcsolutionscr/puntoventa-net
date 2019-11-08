using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("linea")]
    public partial class Linea
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdLinea { get; set; }
        public string Descripcion { get; set; }

        public Empresa Empresa { get; set; }
    }
}
