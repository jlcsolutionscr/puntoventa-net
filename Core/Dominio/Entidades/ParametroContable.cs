using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("parametrocontable")]
    public partial class ParametroContable
    {
        [Key]
        public int IdParametro { get; set; }
        [ForeignKey("CatalogoContable")]
        public int IdCuenta { get; set; }
        [ForeignKey("TipoParametroContable")]
        public int IdTipo { get; set; }
        public int IdProducto { get; set; }
        [NotMapped]
        public string Descripcion { get { if (TipoParametroContable == null) return ""; else return TipoParametroContable.Descripcion; } }
        [NotMapped]
        public string DescCuentaContable { get { if (CatalogoContable == null) return ""; else return CatalogoContable.CuentaContable  + " " + CatalogoContable.Descripcion; } }

        public CatalogoContable CatalogoContable { get; set; }
        public TipoParametroContable TipoParametroContable { get; set; }
    }
}
