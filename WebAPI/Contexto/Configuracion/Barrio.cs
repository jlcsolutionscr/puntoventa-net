using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class BarrioConfiguration : IEntityTypeConfiguration<Barrio>
    {
        public void Configure(EntityTypeBuilder<Barrio> builder)
        {
            builder.ToTable("barrio");
            builder.HasKey(p => new { p.IdProvincia, p.IdCanton, p.IdDistrito, p.IdBarrio });
        }
    }
}
