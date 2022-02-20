using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DevolucionClienteConfiguration : IEntityTypeConfiguration<DevolucionCliente>
    {
        public void Configure(EntityTypeBuilder<DevolucionCliente> builder)
        {
            builder.ToTable("devolucioncliente");
            builder.HasKey(p => p.IdDevolucion);
            builder.Ignore(p => p.Total);
            builder.Ignore(p => p.NombreCliente);
        }
    }
}