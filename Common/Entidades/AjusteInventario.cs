using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class AjusteInventario
    {
        public AjusteInventario()
        {
            DetalleAjusteInventario = new HashSet<DetalleAjusteInventario>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdAjuste { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }

        public ICollection<DetalleAjusteInventario> DetalleAjusteInventario { get; set; }
    }
}
