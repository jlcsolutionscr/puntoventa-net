namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class ParametroContable
    {
        public int IdParametro { get; set; }
        public int IdCuenta { get; set; }
        public int IdTipo { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get { if (TipoParametroContable == null) return ""; else return TipoParametroContable.Descripcion; } }
        public string DescCuentaContable { get { if (CatalogoContable == null) return ""; else return CatalogoContable.CuentaContable  + " " + CatalogoContable.Descripcion; } }

        public CatalogoContable CatalogoContable { get; set; }
        public TipoParametroContable TipoParametroContable { get; set; }
    }
}