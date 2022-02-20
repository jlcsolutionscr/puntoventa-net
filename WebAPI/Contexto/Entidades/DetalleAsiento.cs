using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleAsientoConfiguration : IEntityTypeConfiguration<DetalleAsiento>
    {
        public void Configure(EntityTypeBuilder<DetalleAsiento> builder)
        {
            builder.ToTable("detalleasiento");
            builder.HasKey(p => new { p.IdAsiento, p.Linea });
        }
    }
}