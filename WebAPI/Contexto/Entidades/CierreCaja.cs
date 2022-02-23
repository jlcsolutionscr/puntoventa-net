using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CierreCajaConfiguration : IEntityTypeConfiguration<CierreCaja>
    {
        public void Configure(EntityTypeBuilder<CierreCaja> builder)
        {
            builder.ToTable("cierrecaja");
            builder.HasKey(p => p.IdCierre);
            builder.HasMany(p => p.DetalleEfectivoCierreCaja).WithOne().HasForeignKey(p => p.IdCierre);
            builder.HasMany(p => p.DetalleMovimientoCierreCaja).WithOne().HasForeignKey(p => p.IdCierre);
        }
    }
}