using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class RegistroAutenticacion
    {
        public int IdEmpresa { get; set; }
        public string CodigoUsuario { get; set; }
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Role { get; set; }
    }
}