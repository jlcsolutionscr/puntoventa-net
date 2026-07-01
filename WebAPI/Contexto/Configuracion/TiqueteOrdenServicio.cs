using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TiqueteDespachoMercanciaConfiguration : IEntityTypeConfiguration<TiqueteDespachoMercancia>
    {
        public void Configure(EntityTypeBuilder<TiqueteDespachoMercancia> builder)
        {
            builder.ToTable("tiquetedespachomercancia");
            builder.HasKey(p => p.IdTiquete);
        }
    }
}