namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Canton
    {
        public int IdProvincia { get; set; }
        public int IdCanton { get; set; }
        public string Descripcion { get; set; }

        public Provincia Provincia { get; set; }
    }
}
