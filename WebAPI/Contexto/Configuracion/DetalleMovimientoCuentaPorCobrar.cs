using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleMovimientoCuentaPorCobrarConfiguration : IEntityTypeConfiguration<DetalleMovimientoCuentaPorCobrar>
    {
        public void Configure(EntityTypeBuilder<DetalleMovimientoCuentaPorCobrar> builder)
        {
            builder.ToTable("detallemovimientocuentaporcobrar");
            builder.Ignore(p => p.NombrePropietario);
            builder.HasKey(p => new { p.IdMovCxC, p.IdCxC });
            builder.HasOne(p => p.CuentaPorCobrar).WithMany().HasForeignKey(p => p.IdCxC);
        }
    }
}