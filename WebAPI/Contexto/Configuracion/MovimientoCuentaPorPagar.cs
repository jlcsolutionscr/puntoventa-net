using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoCuentaPorPagarConfiguration : IEntityTypeConfiguration<MovimientoCuentaPorPagar>
    {
        public void Configure(EntityTypeBuilder<MovimientoCuentaPorPagar> builder)
        {
            builder.ToTable("movimientocuentaporpagar");
            builder.HasKey(p => p.IdMovCxP);
            builder.HasMany(p => p.DetalleMovimientoCuentaPorPagar).WithOne().HasForeignKey(p => p.IdMovCxP);
            builder.HasMany(p => p.DesglosePagoMovimientoCuentaPorPagar).WithOne().HasForeignKey(p => p.IdMovCxP);
        }
    }
}