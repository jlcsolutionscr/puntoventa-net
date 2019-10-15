using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Factura = new HashSet<Factura>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdCliente { get; set; }
        [ForeignKey("TipoIdentificacion")]
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string IdentificacionExtranjero { get; set; }
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public int IdBarrio { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string CorreoElectronico { get; set; }
        [ForeignKey("Vendedor")]
        public int? IdVendedor { get; set; }
        public int IdTipoPrecio { get; set; }
        public bool AplicaTasaDiferenciada { get; set; }
        [ForeignKey("ParametroImpuesto")]
        public int IdImpuesto { get; set; }

        public Empresa Empresa { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        [ForeignKey("IdProvincia, IdCanton, IdDistrito, IdBarrio")]
        public Barrio Barrio { get; set; }
        public Vendedor Vendedor { get; set; }
        public ParametroImpuesto ParametroImpuesto { get; set; }
        public ICollection<Factura> Factura { get; set; }
    }
}
