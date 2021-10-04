using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("tiqueteordenservicio")]
    public partial class TiqueteOrdenServicio
    {
        [Key]
        public int IdTiquete { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public string Impresora { get; set; }
        public byte[] Lineas { get; set; }
        public bool Impreso { get; set; }

        [ForeignKey("IdEmpresa, IdSucursal")]
        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}
