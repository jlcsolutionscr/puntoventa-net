namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class RolePorUsuario
    {
        public int IdUsuario { get; set; }
        public int IdRole { get; set; }
        public Role Role { get; set; }
    }
}