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
            builder.Ignore(p => p.NombrePropietario);
            builder.HasOne(p => p.CuentaPorCobrar).WithMany().HasForeignKey(p => p.IdCxC);
            builder.HasMany(p => p.DesglosePagoMovimientoCuentaPorCobrar).WithOne().HasForeignKey(p => p.IdConsecutivo);
        }
    }
}