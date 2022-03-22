using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class LineaPorSucursalConfiguration : IEntityTypeConfiguration<LineaPorSucursal>
    {
        public void Configure(EntityTypeBuilder<LineaPorSucursal> builder)
        {
            builder.ToTable("lineaporsucursal");
            builder.HasKey(p => new { p.IdEmpresa, p.IdSucursal, p.IdLinea });
            builder.HasOne(p => p.SucursalPorEmpresa).WithMany().HasForeignKey(p => new { p.IdEmpresa, p.IdSucursal });
        }
    }
}
