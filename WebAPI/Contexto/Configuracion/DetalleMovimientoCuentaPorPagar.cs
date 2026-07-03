using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleMovimientoCuentaPorPagarConfiguration : IEntityTypeConfiguration<DetalleMovimientoCuentaPorPagar>
    {
        public void Configure(EntityTypeBuilder<DetalleMovimientoCuentaPorPagar> builder)
        {
            builder.ToTable("detallemovimientocuentaporpagar");
            builder.Ignore(p => p.NombrePropietario);
            builder.HasKey(p => new { p.IdMovCxP, p.IdCxP });
            builder.HasOne(p => p.MovimientoCuentaPorPagar).WithMany().HasForeignKey(p => p.IdMovCxP);
            builder.HasOne(p => p.CuentaPorPagar).WithMany().HasForeignKey(p => p.IdCxP);
        }
    }
}