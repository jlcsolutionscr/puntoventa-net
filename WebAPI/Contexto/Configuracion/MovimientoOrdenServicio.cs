using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoOrdenServicioConfiguration : IEntityTypeConfiguration<MovimientoOrdenServicio>
    {
        public void Configure(EntityTypeBuilder<MovimientoOrdenServicio> builder)
        {
            builder.ToTable("movimientoordenservicio");
            builder.HasKey(p => p.IdMovOrden);
            builder.HasOne(p => p.OrdenServicio).WithMany().HasForeignKey(p => p.IdOrden);
            builder.HasMany(p => p.DesglosePagoMovimientoOrdenServicio).WithOne().HasForeignKey(p => p.IdConsecutivo);
        }
    }
}