namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Barrio
    {
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public int IdBarrio { get; set; }
        public string Descripcion { get; set; }
        public Distrito Distrito { get; set; }
    }
}
