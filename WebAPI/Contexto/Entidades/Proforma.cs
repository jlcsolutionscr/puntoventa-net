using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class ProformaConfiguration : IEntityTypeConfiguration<Proforma>
    {
        public void Configure(EntityTypeBuilder<Proforma> builder)
        {
            builder.ToTable("proforma");
            builder.HasKey(p => p.IdProforma);
            builder.Ignore(p => p.Total);
        }
    }
}