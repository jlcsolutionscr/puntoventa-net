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
            builder.HasOne(p => p.Empresa).WithMany(p => p.SucursalPorEmpresa).HasForeignKey(p => p.IdEmpresa);
        }
    }
}