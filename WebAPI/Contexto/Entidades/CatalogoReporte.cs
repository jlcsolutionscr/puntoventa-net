using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CatalogoReporteConfiguration : IEntityTypeConfiguration<CatalogoReporte>
    {
        public void Configure(EntityTypeBuilder<CatalogoReporte> builder)
        {
            builder.ToTable("catalogoreporte");
            builder.HasKey(p => p.IdReporte);
        }
    }
}