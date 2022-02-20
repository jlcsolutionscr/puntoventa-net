using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ProvinciaConfiguration : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.ToTable("provincia");
            builder.HasKey(p => p.IdProvincia);
        }
    }
}