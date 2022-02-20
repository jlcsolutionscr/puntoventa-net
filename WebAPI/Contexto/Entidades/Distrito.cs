using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.ToTable("distrito");
            builder.HasKey(p => new { p.IdProvincia, p.IdCanton, p.IdDistrito });
        }
    }
}