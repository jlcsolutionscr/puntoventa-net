using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleDevolucionProveedorConfiguration : IEntityTypeConfiguration<DetalleDevolucionProveedor>
    {
        public void Configure(EntityTypeBuilder<DetalleDevolucionProveedor> builder)
        {
            builder.ToTable("detalledevolucionproveedor");
            builder.HasKey(p => new { p.IdDevolucion, p.IdProducto });
        }
    }
}