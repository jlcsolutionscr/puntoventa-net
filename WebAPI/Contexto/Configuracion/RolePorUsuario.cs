using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class RolePorUsuarioConfiguration : IEntityTypeConfiguration<RolePorUsuario>
    {
        public void Configure(EntityTypeBuilder<RolePorUsuario> builder)
        {
            builder.ToTable("roleporusuario");
            builder.HasKey(p => new { p.IdUsuario, p.IdRole });
            builder.HasOne(p => p.Role).WithMany().HasForeignKey(p => p.IdRole);
        }
    }
}