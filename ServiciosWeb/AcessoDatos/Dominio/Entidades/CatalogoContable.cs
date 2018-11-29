using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.AccesoDatos.Dominio.Entidades
{
    [Table("catalogocontable")]
    public partial class CatalogoContable
    {
        public CatalogoContable()
        {
            SaldoMensualContable = new HashSet<SaldoMensualContable>();
            ParametroContable = new HashSet<ParametroContable>();
        }
        
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        [Key]
        public int IdCuenta { get; set; }
        public string Nivel_1 { get; set; }
        public string Nivel_2 { get; set; }
        public string Nivel_3 { get; set; }
        public string Nivel_4 { get; set; }
        public string Nivel_5 { get; set; }
        public string Nivel_6 { get; set; }
        public string  Nivel_7 { get; set; }
        [NotMapped]
        public string CuentaContable { get { return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}", Nivel_1, Nivel_2, Nivel_3, Nivel_4, Nivel_5, Nivel_6, Nivel_7) ?? ""; } }
        [ForeignKey("CatalogoContableGrupo")]
        public int? IdCuentaGrupo { get; set; }
        public bool EsCuentaBalance { get; set; }
        public string Descripcion { get; set; }
        [NotMapped]
        public string DescripcionCompleta { get { return string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6} {7}", Nivel_1, Nivel_2, Nivel_3, Nivel_4, Nivel_5, Nivel_6, Nivel_7, Descripcion) ?? ""; } }
        [ForeignKey("TipoCuentaContable")]
        public int IdTipoCuenta { get; set; }
        [ForeignKey("ClaseCuentaContable")]
        public int IdClaseCuenta { get; set; }
        [NotMapped]
        public string TipoSaldo { get { if(TipoCuentaContable == null) return ""; else return TipoCuentaContable.Descripcion; } }
        public bool PermiteMovimiento { get; set; }
        public bool PermiteSobrejiro { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }

        public Empresa Empresa { get; set; }
        public CatalogoContable CatalogoContableGrupo { get; set; }
        public TipoCuentaContable TipoCuentaContable { get; set; }
        public ClaseCuentaContable ClaseCuentaContable { get; set; }
        public ICollection<SaldoMensualContable> SaldoMensualContable { get; set; }
        public ICollection<ParametroContable> ParametroContable { get; set; }
    }
}
