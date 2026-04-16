using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("producto");
            builder.HasKey(p => p.IdProducto);
            builder.Ignore(p => p.IndExistencia);
            builder.Ignore(p => p.Existencias);
            builder.HasOne(p => p.Linea).WithMany().HasForeignKey(p => p.IdLinea);
            builder.HasMany(p => p.MovimientoProducto).WithOne().HasForeignKey(p => p.IdProducto);
        }
    }
}