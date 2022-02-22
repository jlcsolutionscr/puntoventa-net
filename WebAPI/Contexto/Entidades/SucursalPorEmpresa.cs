using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class SucursalPorEmpresaConfiguration : IEntityTypeConfiguration<SucursalPorEmpresa>
    {
        public void Configure(EntityTypeBuilder<SucursalPorEmpresa> builder)
        {
            builder.ToTable("sucursalporempresa");
            builder.HasKey(p => new { p.IdEmpresa, p.IdSucursal });
            builder.HasMany(p => p.TerminalPorSucursal).WithOne().HasForeignKey(p => new { p.IdEmpresa, p.IdSucursal, p.IdTerminal });
        }
    }
}