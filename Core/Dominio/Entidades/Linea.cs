using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("linea")]
    public partial class Linea
    {
        public Linea()
        {
            LineaPorSucursal = new HashSet<LineaPorSucursal>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdLinea { get; set; }
        public string Descripcion { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<LineaPorSucursal> LineaPorSucursal { get; set; }
    }
}
