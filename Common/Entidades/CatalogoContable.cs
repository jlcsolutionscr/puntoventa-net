using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CatalogoContable
    {
        public int IdEmpresa { get; set; }
        public int IdCuenta { get; set; }
        public string Nivel_1 { get; set; }
        public string Nivel_2 { get; set; }
        public string Nivel_3 { get; set; }
        public string Nivel_4 { get; set; }
        public string Nivel_5 { get; set; }
        public string Nivel_6 { get; set; }
        public string  Nivel_7 { get; set; }
        public string CuentaContable { get { return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", Nivel_1, Nivel_2, Nivel_3, Nivel_4, Nivel_5, Nivel_6, Nivel_7) ?? ""; } }
        public int? IdCuentaGrupo { get; set; }
        public bool EsCuentaBalance { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCompleta { get { return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6} {7}", Nivel_1, Nivel_2, Nivel_3, Nivel_4, Nivel_5, Nivel_6, Nivel_7, Descripcion) ?? ""; } }
        public int IdTipoCuenta { get; set; }
        public int IdClaseCuenta { get; set; }
        public string TipoSaldo { get { if(TipoCuentaContable == null) return ""; else return TipoCuentaContable.Descripcion; } }
        public bool PermiteMovimiento { get; set; }
        public bool PermiteSobrejiro { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }

        public CatalogoContable CatalogoContableGrupo { get; set; }
        public TipoCuentaContable TipoCuentaContable { get; set; }
        public ClaseCuentaContable ClaseCuentaContable { get; set; }
    }
}
