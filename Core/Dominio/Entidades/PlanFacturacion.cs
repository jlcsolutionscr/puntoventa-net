using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("planfacturacion")]
    public partial class PlanFacturacion
    {
        [Key]
        public int IdPlan { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDocumentos { get; set; }
    }
}
