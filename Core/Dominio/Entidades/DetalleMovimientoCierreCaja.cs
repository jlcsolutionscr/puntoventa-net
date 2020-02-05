using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("detallemovimientocierrecaja")]
    public partial class DetalleMovimientoCierreCaja
    {
        [Key]
        public int Consecutivo { get; set; }
        [ForeignKey("CierreCaja")]
        public int IdCierre { get; set; }
        public int IdReferencia { get; set; }
        public int Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }

        public CierreCaja CierreCaja { get; set; }
    }
}
