using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("ordencompra")]
    public partial class OrdenCompra
    {
        public OrdenCompra()
        {
            DetalleOrdenCompra = new HashSet<DetalleOrdenCompra>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdOrdenCompra { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        [ForeignKey("CondicionVenta")]
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string NoDocumento { get; set; }
        public short TipoPago { get; set; }
        public decimal Excento { get; set; }
        public decimal Grabado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Grabado + Impuesto; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }
        [NotMapped]
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public CondicionVenta CondicionVenta { get; set; }
        public ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
    }
}
