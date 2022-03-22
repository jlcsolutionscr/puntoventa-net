using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CredencialesHaciendaConfiguration : IEntityTypeConfiguration<CredencialesHacienda>
    {
        public void Configure(EntityTypeBuilder<CredencialesHacienda> builder)
        {
            builder.ToTable("credencialeshacienda");
            builder.HasKey(p => p.IdEmpresa);
            builder.HasOne<Empresa>().WithOne().HasForeignKey<CredencialesHacienda>(p => p.IdEmpresa);
        }
    }
}
