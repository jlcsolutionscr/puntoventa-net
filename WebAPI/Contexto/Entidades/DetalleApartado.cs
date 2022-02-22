using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleApartadoConfiguration : IEntityTypeConfiguration<DetalleApartado>
    {
        public void Configure(EntityTypeBuilder<DetalleApartado> builder)
        {
            builder.ToTable("detalleapartado");
            builder.HasKey(p => p.IdConsecutivo);
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}