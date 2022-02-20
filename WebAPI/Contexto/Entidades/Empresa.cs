using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("empresa");
            builder.HasKey(p => p.IdEmpresa);
            builder.Ignore(p => p.Usuario);
            builder.Ignore(p => p.EquipoRegistrado);
        }
    }
}