using LeandroSoftware.Common.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeandroSoftware.ServicioWeb.Dominio.Entidades
{
    public class AjusteInventarioConfiguration : IEntityTypeConfiguration<AjusteInventario>
    {
        public void Configure(EntityTypeBuilder<AjusteInventario> builder)
        {
            builder.ToTable("ajusteinventario");
            builder.HasKey(p => p.IdAjuste);
        }
    }
}
