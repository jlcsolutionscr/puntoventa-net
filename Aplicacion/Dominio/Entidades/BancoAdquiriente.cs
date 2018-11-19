using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("bancoadquiriente")]
    public partial class BancoAdquiriente
    {
        public int IdEmpresa { get; set; }
        [Key]
        public int IdBanco { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeRetencion { get; set; }
        public decimal PorcentajeComision { get; set; }
    }
}
