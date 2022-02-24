using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoProductoConfiguration : IEntityTypeConfiguration<MovimientoProducto>
    {
        public void Configure(EntityTypeBuilder<MovimientoProducto> builder)
        {
            builder.ToTable("movimientoproducto");
            builder.HasKey(p => p.IdMovimiento);
        }
    }
}