using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class CuentaBancoConfiguration : IEntityTypeConfiguration<CuentaBanco>
    {
        public void Configure(EntityTypeBuilder<CuentaBanco> builder)
        {
            builder.ToTable("cuentabanco");
            builder.HasKey(p => p.IdCuenta);
        }
    }
}