using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("movimientoordenservicio")]
    public partial class MovimientoOrdenServicio
    {
        public MovimientoOrdenServicio()
        {
            DesglosePagoMovimientoOrdenServicio = new HashSet<DesglosePagoMovimientoOrdenServicio>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        [Key]
        public int IdMovOrden { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("OrdenServicio")]
        public int IdOrden { get; set; }
        public short Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }

        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public OrdenServicio OrdenServicio { get; set; }
        public ICollection<DesglosePagoMovimientoOrdenServicio> DesglosePagoMovimientoOrdenServicio { get; set; }
    }
}
