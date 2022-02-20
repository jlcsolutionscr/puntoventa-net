using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class BancoAdquirienteConfiguration : IEntityTypeConfiguration<BancoAdquiriente>
    {
        public void Configure(EntityTypeBuilder<BancoAdquiriente> builder)
        {
            builder.ToTable("bancoadquiriente");
            builder.HasKey(p => p.IdBanco);
        }
    }
}
