using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LeandroSoftware.Common.Dominio.Entidades;

namespace LeandroSoftwarbuilder.ServicioWeb.Dominio.Entidades
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("empresa");
            builder.HasKey(p => p.IdEmpresa);
            builder.Ignore(p => p.Usuario);
            builder.Ignore(p => p.EquipoRegistrado);
            builder.Ignore(p => p.ListadoTipoIdentificacion);
            builder.Ignore(p => p.ListadoFormaPagoCliente);
            builder.Ignore(p => p.ListadoFormaPagoEmpresa);
            builder.Ignore(p => p.ListadoTipoProducto);
            builder.Ignore(p => p.ListadoTipoImpuesto);
            builder.Ignore(p => p.ListadoTipoMoneda);
            builder.Ignore(p => p.ListadoCondicionVenta);
            builder.Ignore(p => p.ListadoTipoExoneracion);
            builder.Ignore(p => p.ListadoNombreInstExoneracion);
            builder.Ignore(p => p.ListadoTipoPrecio);
            builder.HasOne(p => p.Barrio).WithMany().HasForeignKey(p => new { p.IdProvincia, p.IdCanton, p.IdDistrito, p.IdBarrio });
            builder.HasOne(p => p.PlanFacturacion).WithMany().HasForeignKey(p => p.TipoContrato);
            builder.HasMany(p => p.ReportePorEmpresa).WithOne().HasForeignKey(p => p.IdEmpresa);
            builder.HasMany(p => p.RolePorEmpresa).WithOne().HasForeignKey(p => p.IdEmpresa);
            builder.HasMany(p => p.SucursalPorEmpresa).WithOne().HasForeignKey(p => p.IdEmpresa);
            builder.HasMany(p => p.ActividadEconomicaEmpresa).WithOne().HasForeignKey(p => p.IdEmpresa);
        }
    }
}