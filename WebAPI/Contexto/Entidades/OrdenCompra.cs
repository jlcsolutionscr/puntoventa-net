using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class OrdenCompraConfiguration : IEntityTypeConfiguration<OrdenCompra>
    {
        public void Configure(EntityTypeBuilder<OrdenCompra> builder)
        {
            builder.ToTable("ordencompra");
            builder.HasKey(p => p.IdOrdenCompra);
            builder.Ignore(p => p.Total);
            builder.Ignore(p => p.NombreProveedor);
            builder.HasOne(p => p.Proveedor).WithMany().HasForeignKey(p => p.IdProveedor);
            builder.HasMany(p => p.DetalleOrdenCompra).WithOne().HasForeignKey(p => p.IdConsecutivo);
        }
    }
}