using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoBancoConfiguration : IEntityTypeConfiguration<MovimientoBanco>
    {
        public void Configure(EntityTypeBuilder<MovimientoBanco> builder)
        {
            builder.ToTable("movimientobanco");
            builder.HasKey(p => p.IdMov);
        }
    }
}