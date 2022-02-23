using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TerminalPorSucursalConfiguration : IEntityTypeConfiguration<TerminalPorSucursal>
    {
        public void Configure(EntityTypeBuilder<TerminalPorSucursal> builder)
        {
            builder.ToTable("terminalporsucursal");
            builder.HasKey(p => new { p.IdEmpresa, p.IdSucursal, p.IdTerminal });
            builder.HasOne(p => p.SucursalPorEmpresa).WithMany().HasForeignKey(p => new { p.IdEmpresa, p.IdSucursal });
        }
    }
}