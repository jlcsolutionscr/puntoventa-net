using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleDevolucionClienteConfiguration : IEntityTypeConfiguration<DetalleDevolucionCliente>
    {
        public void Configure(EntityTypeBuilder<DetalleDevolucionCliente> builder)
        {
            builder.ToTable("detalledevolucioncliente");
            builder.HasKey(p => new { p.IdDevolucion, p.IdProducto });
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}