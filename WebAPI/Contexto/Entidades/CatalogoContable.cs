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
        }
    }
}
