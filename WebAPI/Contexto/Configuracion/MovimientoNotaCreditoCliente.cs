using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class MovimientoNotaCreditoClienteConfiguration : IEntityTypeConfiguration<MovimientoNotaCreditoCliente>
    {
        public void Configure(EntityTypeBuilder<MovimientoNotaCreditoCliente> builder)
        {
            builder.ToTable("movimientonotacreditocliente");
            builder.HasKey(p => p.Consecutivo);
            builder.HasOne(p => p.NotaCreditoCliente).WithMany().HasForeignKey(p => p.IdNotaCredito);
        }
    }
}