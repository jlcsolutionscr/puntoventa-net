using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleOrdenCompraConfiguration : IEntityTypeConfiguration<DetalleOrdenCompra>
    {
        public void Configure(EntityTypeBuilder<DetalleOrdenCompra> builder)
        {
            builder.ToTable("detalleordencompra");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.PrecioVenta);
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}