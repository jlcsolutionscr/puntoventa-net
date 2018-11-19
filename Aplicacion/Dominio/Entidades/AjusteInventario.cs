using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.PuntoVenta.Dominio.Entidades
{
    [Table("ajusteinventario")]
    public partial class AjusteInventario
    {
        public AjusteInventario()
        {
            DetalleAjusteInventario = new HashSet<DetalleAjusteInventario>();
        }

        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdAjuste { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }

        public Empresa Empresa { get; set; }
        public ICollection<DetalleAjusteInventario> DetalleAjusteInventario { get; set; }
    }
}
