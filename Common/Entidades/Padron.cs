namespace LeandroSoftware.Common.Dominio.Entidades
{
    public class Padron
    {
        public string Identificacion { get; set; }
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public int IdDistrito { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
    }
}