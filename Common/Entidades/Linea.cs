using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Linea
    {
        public int IdEmpresa { get; set; }
        public int IdLinea { get; set; }
        public string Descripcion { get; set; }
        public List<LineaPorSucursal> LineaPorSucursal { get; set; }
    }
}
