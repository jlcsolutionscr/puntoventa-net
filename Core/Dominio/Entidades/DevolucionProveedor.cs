using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("devolucionproveedor")]
    public partial class DevolucionProveedor
    {
        public DevolucionProveedor()
        {
            DetalleDevolucionProveedor = new HashSet<DetalleDevolucionProveedor>();
            DesglosePagoDevolucionProveedor = new HashSet<DesglosePagoDevolucionProveedor>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdDevolucion { get; set; }
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Impuesto; } }
        public int IdMovimientoCxP { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }
        [NotMapped]
        public string NombreProveedor { get { if (Proveedor == null) return ""; else return Proveedor.Nombre; } }

        public Empresa Empresa { get; set; }
        public Compra Compra { get; set; }
        public Usuario Usuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<DetalleDevolucionProveedor> DetalleDevolucionProveedor { get; set; }
        public ICollection<DesglosePagoDevolucionProveedor> DesglosePagoDevolucionProveedor { get; set; }
    }
}
