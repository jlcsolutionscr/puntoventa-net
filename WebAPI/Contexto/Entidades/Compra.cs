using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CompraConfiguration : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("compra");
            builder.HasKey(p => p.IdCompra);
            builder.Ignore(p => p.Total);
            builder.Ignore(p => p.NombreProveedor);
        }
    }
}