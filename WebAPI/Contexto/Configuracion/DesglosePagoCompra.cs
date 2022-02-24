using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoCompraConfiguration : IEntityTypeConfiguration<DesglosePagoCompra>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoCompra> builder)
        {
            builder.ToTable("desglosepagocompra");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}