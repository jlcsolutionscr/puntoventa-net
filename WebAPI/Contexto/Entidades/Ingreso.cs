using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class IngresoConfiguration : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            builder.ToTable("ingreso");
            builder.HasKey(p => p.IdIngreso);
        }
    }
}