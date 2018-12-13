using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("detalleregistro")]
    public partial class DetalleRegistro
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public string ValorRegistro { get; set; }
        public string ImpresoraFactura { get; set; }
        public bool UsaImpresoraImpacto { get; set; }

        public Empresa Empresa { get; set; }
    }
}
