using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CantFEMensualEmpresaConfiguration : IEntityTypeConfiguration<CantFEMensualEmpresa>
    {
        public void Configure(EntityTypeBuilder<CantFEMensualEmpresa> builder)
        {
            builder.ToTable("cantfemensualempresa");
            builder.HasKey(p => new { p.IdEmpresa, p.IdMes, p.IdAnio });
        }
    }
}
