namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class BancoAdquiriente
    {
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeRetencion { get; set; }
        public decimal PorcentajeComision { get; set; }
    }
}
