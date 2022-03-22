using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CuentaIngresoConfiguration : IEntityTypeConfiguration<CuentaIngreso>
    {
        public void Configure(EntityTypeBuilder<CuentaIngreso> builder)
        {
            builder.ToTable("cuentaingreso");
            builder.HasKey(p => p.IdCuenta);
        }
    }
}