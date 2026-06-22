using LeandroSoftware.Common.Parametros;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class ParametroContable
    {
        public int IdParametro { get; set; }
        public int IdCuenta { get; set; }
        public int IdTipo { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get { return TipoParametroContableClase.ObtenerDescripcion(IdTipo); } }
        public string DescCuentaContable { get { if (CatalogoContable == null) return ""; else return CatalogoContable.DescripcionCompleta; } }

        public CatalogoContable CatalogoContable { get; set; }
    }
}