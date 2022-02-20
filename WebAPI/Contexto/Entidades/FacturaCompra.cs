using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class FacturaCompraElectronicoConfiguration : IEntityTypeConfiguration<FacturaCompra>
    {
        public void Configure(EntityTypeBuilder<FacturaCompra> builder)
        {
            builder.ToTable("facturacompra");
            builder.HasKey(p => p.IdFactCompra);
            builder.Ignore(p => p.Total);
        }
    }
}