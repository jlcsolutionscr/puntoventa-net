using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleFacturaCompraConfiguration : IEntityTypeConfiguration<DetalleFacturaCompra>
    {
        public void Configure(EntityTypeBuilder<DetalleFacturaCompra> builder)
        {
            builder.ToTable("detallefacturacompra");
            builder.HasKey(p => new { p.IdFactCompra, p.Linea });
        }
    }
}