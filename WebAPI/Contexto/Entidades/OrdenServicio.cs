using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class OrdenServicioConfiguration : IEntityTypeConfiguration<OrdenServicio>
    {
        public void Configure(EntityTypeBuilder<OrdenServicio> builder)
        {
            builder.ToTable("ordenservicio");
            builder.HasKey(p => p.IdOrden);
            builder.Ignore(p => p.Total);
        }
    }
}