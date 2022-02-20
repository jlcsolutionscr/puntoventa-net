using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class AsientoConfiguration : IEntityTypeConfiguration<Asiento>
    {
        public void Configure(EntityTypeBuilder<Asiento> builder)
        {
            builder.ToTable("asiento");
            builder.HasKey(p => p.IdAsiento);
            builder.Ignore(p => p.Total);
        }
    }
}