using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TiqueteOrdenServicioConfiguration : IEntityTypeConfiguration<TiqueteOrdenServicio>
    {
        public void Configure(EntityTypeBuilder<TiqueteOrdenServicio> builder)
        {
            builder.ToTable("tiqueteordenservicio");
            builder.HasKey(p => p.IdTiquete);
        }
    }
}