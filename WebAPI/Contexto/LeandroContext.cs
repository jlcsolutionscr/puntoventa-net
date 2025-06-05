using LeandroSoftware.Common.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace LeandroSoftware.ServicioWeb.Contexto
{
    public partial class LeandroContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public LeandroContext(DbContextOptions<LeandroContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            Database.SetCommandTimeout(180);
        }

        public DbSet<ActividadEconomicaEmpresa> ActividadEconomicaEmpresaRepository { get; set; }
        public DbSet<Asiento> AsientoRepository { get; set; }
        public DbSet<AjusteInventario> AjusteInventarioRepository { get; set; }
        public DbSet<Apartado> ApartadoRepository { get; set; }
        public DbSet<BancoAdquiriente> BancoAdquirienteRepository { get; set; }
        public DbSet<CantFEMensualEmpresa> CantFEMensualEmpresaRepository { get; set; }
        public DbSet<Canton> CantonRepository { get; set; }
        public DbSet<CatalogoContable> CatalogoContableRepository { get; set; }
        public DbSet<CatalogoReporte> CatalogoReporteRepository { get; set; }
        public DbSet<CierreCaja> CierreCajaRepository { get; set; }
        public DbSet<ClaseCuentaContable> ClaseCuentaContableRepository { get; set; }
        public DbSet<ClasificacionProducto> ClasificacionProductoRepository { get; set; }
        public DbSet<Cliente> ClienteRepository { get; set; }
        public DbSet<CredencialesHacienda> CredencialesHaciendaRepository { get; set; }
        public DbSet<CuentaBanco> CuentaBancoRepository { get; set; }
        public DbSet<CuentaEgreso> CuentaEgresoRepository { get; set; }
        public DbSet<CuentaIngreso> CuentaIngresoRepository { get; set; }
        public DbSet<CuentaPorCobrar> CuentaPorCobrarRepository { get; set; }
        public DbSet<CuentaPorPagar> CuentaPorPagarRepository { get; set; }
        public DbSet<ExistenciaPorSucursal> ExistenciaPorSucursalRepository { get; set; }
        public DbSet<Factura> FacturaRepository { get; set; }
        public DbSet<FacturaCompra> FacturaCompraRepository { get; set; }
        public DbSet<Compra> CompraRepository { get; set; }
        public DbSet<DesglosePagoApartado> DesglosePagoApartadoRepository { get; set; }
        public DbSet<DesglosePagoCompra> DesglosePagoCompraRepository { get; set; }
        public DbSet<DesglosePagoDevolucionCliente> DesglosePagoDevolucionClienteRepository { get; set; }
        public DbSet<DesglosePagoDevolucionProveedor> DesglosePagoDevolucionProveedorRepository { get; set; }
        public DbSet<DesglosePagoFactura> DesglosePagoFacturaRepository { get; set; }
        public DbSet<DesglosePagoMovimientoApartado> DesglosePagoMovimientoApartadoRepository { get; set; }
        public DbSet<DesglosePagoMovimientoCuentaPorCobrar> DesglosePagoMovimientoCuentaPorCobrarRepository { get; set; }
        public DbSet<DesglosePagoMovimientoCuentaPorPagar> DesglosePagoMovimientoCuentaPorPagarRepository { get; set; }
        public DbSet<DesglosePagoMovimientoOrdenServicio> DesglosePagoMovimientoOrdenServicioRepository { get; set; }
        public DbSet<DesglosePagoOrdenServicio> DesglosePagoOrdenServicioRepository { get; set; }
        public DbSet<DetalleAsiento> DetalleAsientoRepository { get; set; }
        public DbSet<DetalleApartado> DetalleApartadoRepository { get; set; }
        public DbSet<DetalleCompra> DetalleCompraRepository { get; set; }
        public DbSet<DetalleDevolucionCliente> DetalleDevolucionClienteRepository { get; set; }
        public DbSet<DetalleDevolucionProveedor> DetalleDevolucionProveedorRepository { get; set; }
        public DbSet<DetalleFactura> DetalleFacturaRepository { get; set; }
        public DbSet<DetalleFacturaCompra> DetalleFacturaCompraRepository { get; set; }
        public DbSet<DetalleOrdenCompra> DetalleOrdenCompraRepository { get; set; }
        public DbSet<DetalleOrdenServicio> DetalleOrdenServicioRepository { get; set; }
        public DbSet<DetalleProforma> DetalleProformaRepository { get; set; }
        public DbSet<DetalleTraslado> DetalleTrasladoRepository { get; set; }
        public DbSet<DevolucionCliente> DevolucionClienteRepository { get; set; }
        public DbSet<DevolucionProveedor> DevolucionProveedorRepository { get; set; }
        public DbSet<Distrito> DistritoRepository { get; set; }
        public DbSet<DocumentoElectronico> DocumentoElectronicoRepository { get; set; }
        public DbSet<Egreso> EgresoRepository { get; set; }
        public DbSet<Empresa> EmpresaRepository { get; set; }
        public DbSet<Ingreso> IngresoRepository { get; set; }
        public DbSet<Linea> LineaRepository { get; set; }
        public DbSet<LineaPorSucursal> LineaPorSucursalRepository { get; set; }
        public DbSet<MovimientoApartado> MovimientoApartadoRepository { get; set; }
        public DbSet<MovimientoBanco> MovimientoBancoRepository { get; set; }
        public DbSet<MovimientoCuentaPorCobrar> MovimientoCuentaPorCobrarRepository { get; set; }
        public DbSet<MovimientoCuentaPorPagar> MovimientoCuentaPorPagarRepository { get; set; }
        public DbSet<MovimientoOrdenServicio> MovimientoOrdenServicioRepository { get; set; }
        public DbSet<MovimientoProducto> MovimientoProductoRepository { get; set; }
        public DbSet<OrdenCompra> OrdenRepository { get; set; }
        public DbSet<OrdenServicio> OrdenServicioRepository { get; set; }
        public DbSet<Padron> PadronRepository { get; set; }
        public DbSet<ParametroContable> ParametroContableRepository { get; set; }
        public DbSet<ParametroSistema> ParametroSistemaRepository { get; set; }
        public DbSet<Producto> ProductoRepository { get; set; }
        public DbSet<Proforma> ProformaRepository { get; set; }
        public DbSet<Proveedor> ProveedorRepository { get; set; }
        public DbSet<Provincia> ProvinciaRepository { get; set; }
        public DbSet<PuntoDeServicio> PuntoDeServicioRepository { get; set; }
        public DbSet<RegistroAutenticacion> RegistroAutenticacionRepository { get; set; }
        public DbSet<ReportePorEmpresa> ReportePorEmpresaRepository { get; set; }
        public DbSet<Role> RoleRepository { get; set; }
        public DbSet<RolePorEmpresa> RolePorEmpresaRepository { get; set; }
        public DbSet<RolePorUsuario> RolePorUsuarioRepository { get; set; }
        public DbSet<SaldoMensualContable> SaldoMensualContableRepository { get; set; }
        public DbSet<SucursalPorEmpresa> SucursalPorEmpresaRepository { get; set; }
        public DbSet<TerminalPorSucursal> TerminalPorSucursalRepository { get; set; }
        public DbSet<TipoCuentaContable> TipoCuentaContableRepository { get; set; }
        public DbSet<TipoMovimientoBanco> TipoMovimientoBancoRepository { get; set; }
        public DbSet<TipoParametroContable> TipoParametroContableRepository { get; set; }
        public DbSet<TiqueteOrdenServicio> TiqueteOrdenServicioRepository { get; set; }
        public DbSet<Traslado> TrasladoRepository { get; set; }
        public DbSet<Usuario> UsuarioRepository { get; set; }
        public DbSet<Vendedor> VendedorRepository { get; set; }

        public void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class
        {
            Entry<TEntity>(entidad).State = EntityState.Modified;
        }

        public void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class
        {
            Entry<TEntity>(entidad).State = EntityState.Deleted;
        }

        public IDbContextTransaction GetDatabaseTransaction()
        {
            return Database.BeginTransaction();
        }

        public void ExecuteProcedure(string procedureName, object[] objParameters)
        {
            string strParameters = "";
            foreach (int parameter in objParameters)
            {
                if (strParameters != "")
                    strParameters += ", ";
                strParameters += parameter;
            }
            Database.ExecuteSqlRaw("call " + procedureName + "(" + strParameters + ")");
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }
    }
}
