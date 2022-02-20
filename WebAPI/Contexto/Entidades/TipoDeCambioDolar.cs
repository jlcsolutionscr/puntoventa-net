using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TipoDeCambioDolarConfiguration : IEntityTypeConfiguration<TipoDeCambioDolar>
    {
        public void Configure(EntityTypeBuilder<TipoDeCambioDolar> builder)
        {
            builder.ToTable("tipodecambiodolar");
            builder.HasKey(p => p.FechaTipoCambio);
        }
    }
}