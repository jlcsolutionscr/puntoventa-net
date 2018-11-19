using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("provincia")]
    public partial class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public string Descripcion { get; set; }
    }
}
