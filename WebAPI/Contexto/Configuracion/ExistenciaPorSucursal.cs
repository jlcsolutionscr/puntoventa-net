using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ExistenciaPorSucursalConfiguration : IEntityTypeConfiguration<ExistenciaPorSucursal>
    {
        public void Configure(EntityTypeBuilder<ExistenciaPorSucursal> builder)
        {
            builder.ToTable("existenciaporsucursal");
            builder.HasKey(p => new { p.IdEmpresa, p.IdSucursal, p.IdProducto });
        }
    }
}