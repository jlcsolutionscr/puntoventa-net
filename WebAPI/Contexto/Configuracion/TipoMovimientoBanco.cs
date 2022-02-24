using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TipoMovimientoBancoConfiguration : IEntityTypeConfiguration<TipoMovimientoBanco>
    {
        public void Configure(EntityTypeBuilder<TipoMovimientoBanco> builder)
        {
            builder.ToTable("tipomovimientobanco");
            builder.HasKey(p => p.IdTipoMov);
        }
    }
}