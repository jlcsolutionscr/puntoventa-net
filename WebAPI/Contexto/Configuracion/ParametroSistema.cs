using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ParametroSistemaConfiguration : IEntityTypeConfiguration<ParametroSistema>
    {
        public void Configure(EntityTypeBuilder<ParametroSistema> builder)
        {
            builder.ToTable("parametrosistema");
            builder.HasKey(p => p.IdParametro);
        }
    }
}