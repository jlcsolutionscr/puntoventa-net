using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoCuentaPorCobrarConfiguration : IEntityTypeConfiguration<MovimientoCuentaPorCobrar>
    {
        public void Configure(EntityTypeBuilder<MovimientoCuentaPorCobrar> builder)
        {
            builder.ToTable("movimientocuentaporcobrar");
            builder.HasKey(p => p.IdMovCxC);
            builder.HasMany(p => p.DetalleMovimientoCuentaPorCobrar).WithOne().HasForeignKey(p => p.IdMovCxC);
            builder.HasMany(p => p.DesglosePagoMovimientoCuentaPorCobrar).WithOne().HasForeignKey(p => p.IdMovCxC);
        }
    }
}