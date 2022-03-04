using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Clave { get; set; }
        public decimal PorcMaxDescuento { get; set; }
        public bool PermiteRegistrarDispositivo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Token;

        public List<RolePorUsuario> RolePorUsuario { get; set; }
    }
}
