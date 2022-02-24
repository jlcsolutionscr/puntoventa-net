using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class SaldoMensualContableConfiguration : IEntityTypeConfiguration<SaldoMensualContable>
    {
        public void Configure(EntityTypeBuilder<SaldoMensualContable> builder)
        {
            builder.ToTable("saldomensualcontable");
            builder.HasKey(p => new { p.IdCuenta, p.Mes, p.Annio });
        }
    }
}