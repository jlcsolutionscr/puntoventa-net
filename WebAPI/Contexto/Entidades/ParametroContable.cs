using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ParametroContableConfiguration : IEntityTypeConfiguration<ParametroContable>
    {
        public void Configure(EntityTypeBuilder<ParametroContable> builder)
        {
            builder.ToTable("parametrocontable");
            builder.HasKey(p => p.IdParametro);
            builder.Ignore(p => p.Descripcion);
            builder.Ignore(p => p.DescCuentaContable);
            builder.HasOne(p => p.TipoParametroContable).WithMany().HasForeignKey(p => p.IdTipo);
            builder.HasOne(p => p.CatalogoContable).WithMany().HasForeignKey(p => p.IdCuenta);
        }
    }
}