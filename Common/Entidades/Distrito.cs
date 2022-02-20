namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Distrito
    {
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public string Descripcion { get; set; }

        public Canton Canton { get; set; }
    }
}
