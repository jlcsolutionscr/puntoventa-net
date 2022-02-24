using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class RegistroAutenticacionConfiguration : IEntityTypeConfiguration<RegistroAutenticacion>
    {
        public void Configure(EntityTypeBuilder<RegistroAutenticacion> builder)
        {
            builder.ToTable("registroautenticacion");
            builder.HasKey(p => p.Id);
        }
    }
}