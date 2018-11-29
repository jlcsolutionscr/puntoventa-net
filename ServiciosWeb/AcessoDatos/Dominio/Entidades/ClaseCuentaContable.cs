using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("clasecuentacontable")]
    public partial class ClaseCuentaContable
    {
        [Key]
        public int IdClaseCuenta { get; set; }
        public string Descripcion { get; set; }
    }
}
