using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(p => p.IdUsuario);
            builder.Ignore(p => p.IdSucursal);
            builder.Ignore(p => p.Token);
            builder.HasMany(p => p.RolePorUsuario).WithOne().HasForeignKey(p => p.IdUsuario);
            builder.HasMany(p => p.SucursalPorUsuario).WithOne(p => p.Usuario).HasForeignKey(p => p.IdUsuario);
        }
    }
}