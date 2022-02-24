using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoDevolucionClienteConfiguration : IEntityTypeConfiguration<DesglosePagoDevolucionCliente>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoDevolucionCliente> builder)
        {
            builder.ToTable("desglosepagodevolucioncliente");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}