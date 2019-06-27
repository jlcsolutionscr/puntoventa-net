using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("moduloporempresa")]
    public class ModuloPorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("Modulo")]
        public int IdModulo { get; set; }

        public Empresa Empresa { get; set; }
        public Modulo Modulo { get; set; }
    }
}
