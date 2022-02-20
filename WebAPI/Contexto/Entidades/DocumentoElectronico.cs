using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DocumentoElectronicoConfiguration : IEntityTypeConfiguration<DocumentoElectronico>
    {
        public void Configure(EntityTypeBuilder<DocumentoElectronico> builder)
        {
            builder.ToTable("documentoelectronico");
            builder.HasKey(p => p.IdDocumento);
        }
    }
}