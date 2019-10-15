using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("devolucioncliente")]
    public partial class DevolucionCliente
    {
        public DevolucionCliente()
        {
            DetalleDevolucionCliente = new HashSet<DetalleDevolucionCliente>();
            DesglosePagoDevolucionCliente = new HashSet<DesglosePagoDevolucionCliente>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdDevolucion { get; set; }
        [ForeignKey("Factura")]
        public int IdFactura { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Gravado + Impuesto; } }
        public int IdMovimientoCxC { get; set; }
        public int IdAsiento { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Procesado { get; set; }
        public string IdDocElectronico { get; set; }
        public string IdDocElectronicoRev { get; set; }
        [NotMapped]
        public string NombreCliente { get { if (Factura == null) return ""; else return Factura.Cliente.Nombre; } }

        public Empresa Empresa { get; set; }
        public Factura Factura { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<DetalleDevolucionCliente> DetalleDevolucionCliente { get; set; }
        public ICollection<DesglosePagoDevolucionCliente> DesglosePagoDevolucionCliente { get; set; }
    }
}
