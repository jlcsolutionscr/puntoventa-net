using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ReportePorEmpresaConfiguration : IEntityTypeConfiguration<ReportePorEmpresa>
    {
        public void Configure(EntityTypeBuilder<ReportePorEmpresa> builder)
        {
            builder.ToTable("reporteporempresa");
            builder.HasKey(p => new { p.IdEmpresa, p.IdReporte });
        }
    }
}