using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("sucursal")]
    public partial class Sucursal
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Empresa Empresa { get; set; }
    }
}
