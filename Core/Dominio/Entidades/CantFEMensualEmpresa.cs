using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("cantfemensualempresa")]
    public class CantFEMensualEmpresa
    {
        [Key, Column(Order = 0)]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdMes { get; set; }
        [Key, Column(Order = 2)]
        public int IdAnio { get; set; }
        public int CantidadDoc { get; set; }
    }
}
