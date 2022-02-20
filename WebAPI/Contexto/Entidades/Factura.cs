using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.ToTable("factura");
            builder.HasKey(p => p.IdFactura);
            builder.Ignore(p => p.Total);
        }
    }
}