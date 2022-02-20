using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class SucursalPorUsuarioConfiguration : IEntityTypeConfiguration<SucursalPorUsuario>
    {
        public void Configure(EntityTypeBuilder<SucursalPorUsuario> builder)
        {
            builder.ToTable("sucursalporusuario");
            builder.HasKey(p => new { p.IdEmpresa, p.IdSucursal, p.IdUsuario });
        }
    }
}