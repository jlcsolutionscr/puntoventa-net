using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("terminalporempresa")]
    public partial class TerminalPorEmpresa
    {
        [Key, Column(Order = 0), ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key, Column(Order = 1)]
        public int IdSucursal { get; set; }
        [Key, Column(Order = 2)]
        public int IdTerminal { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string ValorRegistro { get; set; }
        public string ImpresoraFactura { get; set; }
        public string NombreSucursal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int UltimoDocFE { get; set; }
        public int UltimoDocND { get; set; }
        public int UltimoDocNC { get; set; }
        public int UltimoDocTE { get; set; }
        public int UltimoDocMR { get; set; }
        public int IdTipoDispositivo { get; set; }

        public Empresa Empresa { get; set; }
    }
}
