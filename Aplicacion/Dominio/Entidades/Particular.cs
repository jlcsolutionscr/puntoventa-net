using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("particular")]
    public partial class Particular
    {
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdParticular { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }

        public Empresa Empresa { get; set; }
    }
}
