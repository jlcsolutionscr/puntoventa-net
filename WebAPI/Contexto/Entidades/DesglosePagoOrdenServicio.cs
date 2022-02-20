using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class DesglosePagoOrdenServicioConfiguration : IEntityTypeConfiguration<DesglosePagoOrdenServicio>
    {
        public void Configure(EntityTypeBuilder<DesglosePagoOrdenServicio> builder)
        {
            builder.ToTable("desglosepagoordenservicio");
            builder.HasKey(p => p.IdConsecutivo);
            builder.Ignore(p => p.DescripcionCuenta);
        }
    }
}