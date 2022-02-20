using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoApartadoConfiguration : IEntityTypeConfiguration<MovimientoApartado>
    {
        public void Configure(EntityTypeBuilder<MovimientoApartado> builder)
        {
            builder.ToTable("movimientoapartado");
            builder.HasKey(p => p.IdMovApartado);
        }
    }
}