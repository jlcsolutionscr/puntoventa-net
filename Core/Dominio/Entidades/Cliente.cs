using System;
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
        [ForeignKey("ParametroExoneracion")]
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string NombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public bool PermiteCredito { get; set; }

        public Empresa Empresa { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public Vendedor Vendedor { get; set; }
        public ParametroImpuesto ParametroImpuesto { get; set; }
        public ParametroExoneracion ParametroExoneracion { get; set; }
        public ICollection<Factura> Factura { get; set; }
    }
}
