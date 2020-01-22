using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("sucursalporempresa")]
    public partial class SucursalPorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool CierreEnEjecucion { get; set; }
        public int ConsecOrdenServicio { get; set; }
        public int ConsecApartado { get; set; }

        public Empresa Empresa { get; set; }
    }
}
