using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoMovimientoOrdenServicioConfiguration : IEntityTypeConfiguration<DesglosePagoMovimientoOrdenServicio>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoMovimientoOrdenServicio> builder)
        {
            builder.ToTable("desglosepagomovimientoordenservicio");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}
