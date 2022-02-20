using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleProformaConfiguration : IEntityTypeConfiguration<DetalleProforma>
    {
        public void Configure(EntityTypeBuilder<DetalleProforma> builder)
        {
            builder.ToTable("detalleproforma");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.Codigo);
        }
    }
}