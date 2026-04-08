using LeandroSoftware.Common.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class NotificacionConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable("notificacion");
            builder.HasKey(p => p.Id);
        }
    }
}