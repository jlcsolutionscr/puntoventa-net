using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("canton")]
    public partial class Canton
    {
        [Key, Column(Order = 0), ForeignKey("Provincia")]
        public int IdProvincia { get; set; }
        [Key, Column(Order = 1)]
        public int IdCanton { get; set; }
        public string Descripcion { get; set; }

        public Provincia Provincia { get; set; }
    }
}
