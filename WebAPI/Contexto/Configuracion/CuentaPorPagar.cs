using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CuentaPorPagarConfiguration : IEntityTypeConfiguration<CuentaPorPagar>
    {
        public void Configure(EntityTypeBuilder<CuentaPorPagar> builder)
        {
            builder.ToTable("cuentaporpagar");
            builder.HasKey(p => p.IdCxP);
        }
    }
}