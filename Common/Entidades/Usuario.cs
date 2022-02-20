using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            RolePorUsuario = new HashSet<RolePorUsuario>();
            SucursalPorUsuario = new HashSet<SucursalPorUsuario>();
        }

        public int IdUsuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string Clave { get; set; }
        public decimal PorcMaxDescuento { get; set; }
        public bool PermiteRegistrarDispositivo { get; set; }
        public int IdSucursal;
        public Empresa Empresa;
        public string Token;

        public ICollection<RolePorUsuario> RolePorUsuario { get; set; }
        public ICollection<SucursalPorUsuario> SucursalPorUsuario { get; set; }
    }
}
