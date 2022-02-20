using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TrasladoConfiguration : IEntityTypeConfiguration<Traslado>
    {
        public void Configure(EntityTypeBuilder<Traslado> builder)
        {
            builder.ToTable("traslado");
            builder.HasKey(p => p.IdTraslado);
            builder.Ignore(p => p.NombreSucursalOrigen);
            builder.Ignore(p => p.NombreSucursalDestino);
        }
    }
}