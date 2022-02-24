using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ClasificacionProductoConfiguration : IEntityTypeConfiguration<ClasificacionProducto>
    {
        public void Configure(EntityTypeBuilder<ClasificacionProducto> builder)
        {
            builder.ToTable("clasificacionproducto");
            builder.HasKey(p => p.Id);
        }
    }
}