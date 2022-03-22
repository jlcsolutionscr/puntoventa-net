using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleAjusteInventarioConfiguration : IEntityTypeConfiguration<DetalleAjusteInventario>
    {
        public void Configure(EntityTypeBuilder<DetalleAjusteInventario> builder)
        {
            builder.ToTable("detalleajusteinventario");
            builder.HasKey(p => new { p.IdAjuste, p.IdProducto });
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}