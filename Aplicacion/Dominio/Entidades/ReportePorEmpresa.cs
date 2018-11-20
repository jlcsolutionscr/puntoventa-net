using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("reporteporempresa")]
    public class ReportePorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("CatalogoReporte")]
        public int IdReporte { get; set; }

        public Empresa Empresa { get; set; }
        public CatalogoReporte CatalogoReporte { get; set; }
    }
}
