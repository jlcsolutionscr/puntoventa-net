using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class OrdenServicioConfiguration : IEntityTypeConfiguration<OrdenServicio>
    {
        public void Configure(EntityTypeBuilder<OrdenServicio> builder)
        {
            builder.ToTable("ordenservicio");
            builder.HasKey(p => p.IdOrden);
            builder.Ignore(p => p.Total);
            builder.HasOne(p => p.Cliente).WithMany().HasForeignKey(p => p.IdCliente);
            builder.HasOne(p => p.Vendedor).WithMany().HasForeignKey(p => p.IdVendedor);
            builder.HasMany(p => p.DetalleOrdenServicio).WithOne().HasForeignKey(p => p.IdOrden);
            builder.HasMany(p => p.DesglosePagoOrdenServicio).WithOne().HasForeignKey(p => p.IdOrden);
        }
    }
}