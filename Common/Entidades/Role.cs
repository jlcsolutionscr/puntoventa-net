namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Role
    {
        public int IdRole { get; set; }
        public string Nombre { get; set; }
        public string MenuPadre { get; set; }
        public string MenuItem { get; set; }
        public string Descripcion { get; set; }
    }
}