using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TipoParametroContableConfiguration : IEntityTypeConfiguration<TipoParametroContable>
    {
        public void Configure(EntityTypeBuilder<TipoParametroContable> builder)
        {
            builder.ToTable("tipoparametrocontable");
            builder.HasKey(p => p.IdTipo);
        }
    }
}