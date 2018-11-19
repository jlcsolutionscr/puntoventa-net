using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.FacturaElectronicaHacienda.Dominio.Entidades
{
    [Table("barrio")]
    public partial class Barrio
    {
        [Key, Column(Order = 0)]
        public int IdProvincia { get; set; }
        [Key, Column(Order = 1)]
        public int IdCanton { get; set; }
        [Key, Column(Order = 2)]
        public int IdDistrito { get; set; }
        [Key, Column(Order = 3)]
        public int IdBarrio { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("IdProvincia, IdCanton, IdDistrito")]
        public Distrito Distrito { get; set; }
    }
}
