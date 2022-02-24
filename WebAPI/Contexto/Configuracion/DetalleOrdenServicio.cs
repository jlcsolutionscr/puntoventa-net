using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleOrdenServicioConfiguration : IEntityTypeConfiguration<DetalleOrdenServicio>
    {
        public void Configure(EntityTypeBuilder<DetalleOrdenServicio> builder)
        {
            builder.ToTable("detalleordenservicio");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.Codigo);
            builder.HasOne(p => p.Producto).WithMany().HasForeignKey(p => p.IdProducto);
        }
    }
}