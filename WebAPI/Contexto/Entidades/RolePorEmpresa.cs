using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class RolePorEmpresaConfiguration : IEntityTypeConfiguration<RolePorEmpresa>
    {
        public void Configure(EntityTypeBuilder<RolePorEmpresa> builder)
        {
            builder.ToTable("roleporempresa");
            builder.HasKey(p => new { p.IdEmpresa, p.IdRole });
        }
    }
}