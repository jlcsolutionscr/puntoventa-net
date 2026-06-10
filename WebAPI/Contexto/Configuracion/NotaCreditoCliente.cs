using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class NotaCreditoClienteConfiguration : IEntityTypeConfiguration<NotaCreditoCliente>
    {
        public void Configure(EntityTypeBuilder<NotaCreditoCliente> builder)
        {
            builder.ToTable("notacreditocliente");
            builder.HasKey(p => p.IdNotaCredito);
            builder.HasOne(p => p.Cliente).WithMany().HasForeignKey(p => p.IdCliente);
            builder.HasMany(p => p.MovimientoNotaCreditoCliente).WithOne().HasForeignKey(p => p.IdNotaCredito);
        }
    }
}