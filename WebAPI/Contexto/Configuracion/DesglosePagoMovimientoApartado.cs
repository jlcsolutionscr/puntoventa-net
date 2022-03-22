using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoMovimientoApartadoConfiguration : IEntityTypeConfiguration<DesglosePagoMovimientoApartado>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoMovimientoApartado> builder)
        {
            builder.ToTable("desglosepagomovimientoapartado");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}