using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class LineaConfiguration : IEntityTypeConfiguration<Linea>
    {
        public void Configure(EntityTypeBuilder<Linea> builder)
        {
            builder.ToTable("linea");
            builder.HasKey(p => p.IdLinea);
            builder.HasMany(p => p.LineaPorSucursal).WithOne().HasForeignKey(p => p.IdLinea);
        }
    }
}