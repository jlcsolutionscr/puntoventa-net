using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("terminalporsucursal")]
    public partial class TerminalPorSucursal
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1), ForeignKey("SucursalPorEmpresa")]
        public int IdSucursal { get; set; }
        [Key, Column(Order = 2)]
        public int IdTerminal { get; set; }
        public string ValorRegistro { get; set; }
        public string ImpresoraFactura { get; set; }
        public int UltimoDocFE { get; set; }
        public int UltimoDocND { get; set; }
        public int UltimoDocNC { get; set; }
        public int UltimoDocTE { get; set; }
        public int UltimoDocMR { get; set; }
        public int IdTipoDispositivo { get; set; }

        public Empresa Empresa { get; set; }
        public SucursalPorEmpresa SucursalPorEmpresa { get; set; }
    }
}
