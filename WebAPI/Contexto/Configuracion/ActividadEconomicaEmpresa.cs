using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ActividadEconomicaEmpresaConfiguration : IEntityTypeConfiguration<ActividadEconomicaEmpresa>
    {
        public void Configure(EntityTypeBuilder<ActividadEconomicaEmpresa> builder)
        {
            builder.ToTable("actividadeconomicaempresa");
            builder.HasKey(p => new { p.IdEmpresa, p.CodigoActividad });
            builder.HasOne<Empresa>().WithMany().HasForeignKey(p => p.IdEmpresa);
        }
    }
}
