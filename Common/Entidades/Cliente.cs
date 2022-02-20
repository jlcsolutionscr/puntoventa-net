using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Cliente
    {
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdVendedor { get; set; }
        public int IdTipoPrecio { get; set; }
        public bool AplicaTasaDiferenciada { get; set; }
        public int IdImpuesto { get; set; }
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string NombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public bool PermiteCredito { get; set; }

        public Vendedor Vendedor { get; set; }
    }
}
