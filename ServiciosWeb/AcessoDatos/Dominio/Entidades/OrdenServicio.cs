using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("ordenservicio")]
    public partial class OrdenServicio
    {
        public OrdenServicio()
        {
            DetalleOrdenServicio = new HashSet<DetalleOrdenServicio>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdOrden { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string Operarios { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }
        public string EstadoActual { get; set; }
        public decimal Excento { get; set; }
        public decimal Grabado { get; set; }
        public decimal Descuento { get; set; }
        public decimal PorcentajeIVA { get; set; }
        public decimal Impuesto { get; set; }
        [NotMapped]
        public decimal Total { get { return Excento + Grabado + Impuesto; } }
        [NotMapped]
        public string NombreCliente { get { if (Cliente == null) return ""; else return Cliente.Nombre; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public bool Aplicado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleOrdenServicio> DetalleOrdenServicio { get; set; }
    }
}
