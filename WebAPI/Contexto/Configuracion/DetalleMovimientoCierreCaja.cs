using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DetalleMovimientoCierreCajaConfiguration : IEntityTypeConfiguration<DetalleMovimientoCierreCaja>
    {
        public void Configure(EntityTypeBuilder<DetalleMovimientoCierreCaja> builder)
        {
            builder.ToTable("detallemovimientocierrecaja");
            builder.HasKey(p => p.Consecutivo);
        }
    }
}