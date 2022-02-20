using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class PlanFacturacionConfiguration : IEntityTypeConfiguration<PlanFacturacion>
    {
        public void Configure(EntityTypeBuilder<PlanFacturacion> builder)
        {
            builder.ToTable("planfacturacion");
            builder.HasKey(p => p.IdPlan);
        }
    }
}