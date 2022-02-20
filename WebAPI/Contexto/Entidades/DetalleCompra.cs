using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleCompraConfiguration : IEntityTypeConfiguration<DetalleCompra>
    {
        public void Configure(EntityTypeBuilder<DetalleCompra> builder)
        {
            builder.ToTable("detallecompra");
            builder.HasKey(p => p.IdConsecutivo);
        }
    }
}