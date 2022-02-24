using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CuentaEgresoConfiguration : IEntityTypeConfiguration<CuentaEgreso>
    {
        public void Configure(EntityTypeBuilder<CuentaEgreso> builder)
        {
            builder.ToTable("cuentaegreso");
            builder.HasKey(p => p.IdCuenta);
        }
    }
}