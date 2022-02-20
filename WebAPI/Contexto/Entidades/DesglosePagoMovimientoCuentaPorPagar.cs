using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoMovimientoCuentaPorPagarConfiguration : IEntityTypeConfiguration<DesglosePagoMovimientoCuentaPorPagar>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoMovimientoCuentaPorPagar> builder)
        {
            builder.ToTable("desglosepagomovimientocuentaporpagar");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}
