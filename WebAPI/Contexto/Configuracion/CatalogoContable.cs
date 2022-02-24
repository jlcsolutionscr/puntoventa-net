using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CatalogoContableConfiguration : IEntityTypeConfiguration<CatalogoContable>
    {
        public void Configure(EntityTypeBuilder<CatalogoContable> builder)
        {
            builder.ToTable("catalogocontable");
            builder.HasKey(p => p.IdCuenta);
            builder.Ignore(p => p.CuentaContable);
            builder.Ignore(p => p.DescripcionCompleta);
            builder.Ignore(p => p.TipoSaldo);
            builder.HasOne(p => p.CatalogoContableGrupo).WithMany().HasForeignKey(p => p.IdCuentaGrupo);
            builder.HasOne(p => p.TipoCuentaContable).WithMany().HasForeignKey(p => p.IdTipoCuenta);
            builder.HasOne(p => p.ClaseCuentaContable).WithMany().HasForeignKey(p => p.IdClaseCuenta);
        }
    }
}
