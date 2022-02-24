using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoApartadoConfiguration : IEntityTypeConfiguration<DesglosePagoApartado>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoApartado> builder)
        {
            builder.ToTable("desglosepagoapartado");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}