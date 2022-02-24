using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CantonConfiguration : IEntityTypeConfiguration<Canton>
    {
        public void Configure(EntityTypeBuilder<Canton> builder)
        {
            builder.ToTable("canton");
            builder.HasKey(p => new { p.IdProvincia, p.IdCanton });
        }
    }
}