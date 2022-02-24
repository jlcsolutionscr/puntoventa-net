using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class RegistroRespuestaHaciendaConfiguration : IEntityTypeConfiguration<RegistroRespuestaHacienda>
    {
        public void Configure(EntityTypeBuilder<RegistroRespuestaHacienda> builder)
        {
            builder.ToTable("registrorespuestahacienda");
            builder.HasKey(p => p.IdRegistro);
        }
    }
}