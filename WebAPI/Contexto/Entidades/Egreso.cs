using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class EgresoConfiguration : IEntityTypeConfiguration<Egreso>
    {
        public void Configure(EntityTypeBuilder<Egreso> builder)
        {
            builder.ToTable("egreso");
            builder.HasKey(p => p.IdEgreso);
        }
    }
}