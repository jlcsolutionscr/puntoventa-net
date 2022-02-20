namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class RolePorEmpresa
    {
        public int IdEmpresa { get; set; }
        public int IdRole { get; set; }

        public Role Role { get; set; }
    }
}