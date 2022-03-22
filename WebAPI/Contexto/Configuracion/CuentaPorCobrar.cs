using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CuentaPorCobrarConfiguration : IEntityTypeConfiguration<CuentaPorCobrar>
    {
        public void Configure(EntityTypeBuilder<CuentaPorCobrar> builder)
        {
            builder.ToTable("cuentaporcobrar");
            builder.HasKey(p => p.IdCxC);
        }
    }
}
