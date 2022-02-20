using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class TipoCuentaContableConfiguration : IEntityTypeConfiguration<TipoCuentaContable>
    {
        public void Configure(EntityTypeBuilder<TipoCuentaContable> builder)
        {
            builder.ToTable("tipocuentacontable");
            builder.HasKey(p => p.IdTipoCuenta);
        }
    }
}