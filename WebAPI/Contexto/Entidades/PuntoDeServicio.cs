using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class PuntoDeServicioConfiguration : IEntityTypeConfiguration<PuntoDeServicio>
    {
        public void Configure(EntityTypeBuilder<PuntoDeServicio> builder)
        {
            builder.ToTable("puntodeservicio");
            builder.HasKey(p => p.IdPunto);
        }
    }
}
