using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("vendedor")]
    public partial class Vendedor
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdVendedor { get; set; }
        public string Nombre { get; set; }
        
        public Empresa Empresa { get; set; }
    }
}
