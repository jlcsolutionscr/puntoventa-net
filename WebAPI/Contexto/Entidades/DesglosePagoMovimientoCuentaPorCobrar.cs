using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoMovimientoCuentaPorCobrarConfiguration : IEntityTypeConfiguration<DesglosePagoMovimientoCuentaPorCobrar>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoMovimientoCuentaPorCobrar> builder)
        {
            builder.ToTable("desglosepagomovimientocuentaporcobrar");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}