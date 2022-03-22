using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleEfectivoCierreCajaConfiguration : IEntityTypeConfiguration<DetalleEfectivoCierreCaja>
    {
        public void Configure(EntityTypeBuilder<DetalleEfectivoCierreCaja> builder)
        {
            builder.ToTable("detalleefectivocierrecaja");
            builder.HasKey(p => new { p.IdCierre, p.Denominacion });
        }
    }
}
