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
            builder.HasOne(p => p.Barrio).WithMany().HasForeignKey(p => new { p.IdProvincia, p.IdCanton, p.IdDistrito, p.IdBarrio });
            builder.HasOne(p => p.PlanFacturacion).WithMany().HasForeignKey(p => p.TipoContrato);
            builder.HasMany(p => p.ReportePorEmpresa).WithOne().HasForeignKey(p => new { p.IdEmpresa, p.IdReporte });
        }
    }
}