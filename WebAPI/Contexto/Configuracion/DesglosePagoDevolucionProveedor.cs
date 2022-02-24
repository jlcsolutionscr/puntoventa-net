using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoDevolucionProveedorConfiguration : IEntityTypeConfiguration<DesglosePagoDevolucionProveedor>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoDevolucionProveedor> builder)
        {
            builder.ToTable("desglosepagodevolucionproveedor");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}