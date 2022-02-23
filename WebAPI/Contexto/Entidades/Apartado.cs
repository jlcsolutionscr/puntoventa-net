using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ApartadoConfiguration : IEntityTypeConfiguration<Apartado>
    {
        public void Configure(EntityTypeBuilder<Apartado> builder)
        {
            builder.ToTable("apartado");
            builder.HasKey(p => p.IdApartado);
            builder.Ignore(p => p.Total);
            builder.HasOne(p => p.Cliente).WithMany().HasForeignKey(p => p.IdCliente);
            builder.HasOne(p => p.Vendedor).WithMany().HasForeignKey(p => p.IdVendedor);
            builder.HasMany(p => p.DetalleApartado).WithOne().HasForeignKey(p => p.IdApartado);
            builder.HasMany(p => p.DesglosePagoApartado).WithOne().HasForeignKey(p => p.IdApartado);
        }
    }
}
