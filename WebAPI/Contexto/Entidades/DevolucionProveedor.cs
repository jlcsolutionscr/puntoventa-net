using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DevolucionProveedorConfiguration : IEntityTypeConfiguration<DevolucionProveedor>
    {
        public void Configure(EntityTypeBuilder<DevolucionProveedor> builder)
        {
            builder.ToTable("devolucionproveedor");
            builder.HasKey(p => p.IdDevolucion);
            builder.Ignore(p => p.Total);
            builder.Ignore(p => p.NombreProveedor);
        }
    }
}