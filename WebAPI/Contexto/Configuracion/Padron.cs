using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class PadronConfiguration : IEntityTypeConfiguration<Padron>
    {
        public void Configure(EntityTypeBuilder<Padron> builder)
        {
            builder.ToTable("padron");
            builder.HasKey(p => p.Identificacion);
        }
    }
}