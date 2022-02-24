using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoFacturaConfiguration : IEntityTypeConfiguration<DesglosePagoFactura>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoFactura> builder)
        {
            builder.ToTable("desglosepagofactura");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}
