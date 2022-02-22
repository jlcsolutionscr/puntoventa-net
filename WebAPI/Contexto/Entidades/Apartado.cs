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
        }
    }
}
