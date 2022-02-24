using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleTrasladoConfiguration : IEntityTypeConfiguration<DetalleTraslado>
    {
        public void Configure(EntityTypeBuilder<DetalleTraslado> builder)
        {
            builder.ToTable("detalletraslado");
            builder.HasKey(p => new { p.IdTraslado, p.IdProducto });
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}