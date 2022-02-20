using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ClaseCuentaContableConfiguration : IEntityTypeConfiguration<ClaseCuentaContable>
    {
        public void Configure(EntityTypeBuilder<ClaseCuentaContable> builder)
        {
            builder.ToTable("clasecuentacontable");
            builder.HasKey(p => p.IdClaseCuenta);
        }
    }
}