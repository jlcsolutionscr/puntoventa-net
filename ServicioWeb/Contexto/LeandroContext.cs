using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using LeandroSoftware.Core.Dominio.Entidades;

namespace LeandroSoftware.ServicioWeb.Contexto
{
    public interface IDbContext : IDisposable
    {
        DbSet<Asiento> AsientoRepository { get; set; }
        DbSet<AjusteInventario> AjusteInventarioRepository { get; set; }
        DbSet<Apartado> ApartadoRepository { get; set; }
        DbSet<BancoAdquiriente> BancoAdquirienteRepository { get; set; }
        DbSet<Barrio> BarrioRepository { get; set; }
        DbSet<CantFEMensualEmpresa> CantFEMensualEmpresaRepository { get; set; }
        DbSet<Canton> CantonRepository { get; set; }
        DbSet<CatalogoContable> CatalogoContableRepository { get; set; }
        DbSet<CatalogoReporte> CatalogoReporteRepository { get; set; }
        DbSet<CierreCaja> CierreCajaRepository { get; set; }
        DbSet<ClaseCuentaContable> ClaseCuentaContableRepository { get; set; }
        DbSet<Cliente> ClienteRepository { get; set; }
        DbSet<CondicionVenta> CondicionVentaRepository { get; set; }
        DbSet<CuentaBanco> CuentaBancoRepository { get; set; }
        DbSet<CuentaEgreso> CuentaEgresoRepository { get; set; }
        DbSet<CuentaIngreso> CuentaIngresoRepository { get; set; }
        DbSet<CuentaPorCobrar> CuentaPorCobrarRepository { get; set; }
        DbSet<CuentaPorPagar> CuentaPorPagarRepository { get; set; }
        DbSet<ExistenciaPorSucursal> ExistenciaPorSucursalRepository { get; set; }
        DbSet<Factura> FacturaRepository { get; set; }
        DbSet<FacturaCompra> FacturaCompraRepository { get; set; }
        DbSet<Compra> CompraRepository { get; set; }
        DbSet<DesglosePagoApartado> DesglosePagoApartadoRepository { get; set; }
        DbSet<DesglosePagoCompra> DesglosePagoCompraRepository { get; set; }
        DbSet<DesglosePagoDevolucionCliente> DesglosePagoDevolucionClienteRepository { get; set; }
        DbSet<DesglosePagoDevolucionProveedor> DesglosePagoDevolucionProveedorRepository { get; set; }
        DbSet<DesglosePagoFactura> DesglosePagoFacturaRepository { get; set; }
        DbSet<DesglosePagoMovimientoApartado> DesglosePagoMovimientoApartadoRepository { get; set; }
        DbSet<DesglosePagoMovimientoCuentaPorCobrar> DesglosePagoMovimientoCuentaPorCobrarRepository { get; set; }
        DbSet<DesglosePagoMovimientoCuentaPorPagar> DesglosePagoMovimientoCuentaPorPagarRepository { get; set; }
        DbSet<DesglosePagoMovimientoOrdenServicio> DesglosePagoMovimientoOrdenServicioRepository { get; set; }
        DbSet<DesglosePagoOrdenServicio> DesglosePagoOrdenServicioRepository { get; set; }
        DbSet<DetalleAsiento> DetalleAsientoRepository { get; set; }
        DbSet<DetalleApartado> DetalleApartadoRepository { get; set; }
        DbSet<DetalleCompra> DetalleCompraRepository { get; set; }
        DbSet<DetalleDevolucionCliente> DetalleDevolucionClienteRepository { get; set; }
        DbSet<DetalleDevolucionProveedor> DetalleDevolucionProveedorRepository { get; set; }
        DbSet<DetalleFactura> DetalleFacturaRepository { get; set; }
        DbSet<DetalleFacturaCompra> DetalleFacturaCompraRepository { get; set; }
        DbSet<DetalleOrdenCompra> DetalleOrdenCompraRepository { get; set; }
        DbSet<DetalleOrdenServicio> DetalleOrdenServicioRepository { get; set; }
        DbSet<DetalleProforma> DetalleProformaRepository { get; set; }
        DbSet<DetalleTraslado> DetalleTrasladoRepository { get; set; }
        DbSet<DevolucionCliente> DevolucionClienteRepository { get; set; }
        DbSet<DevolucionProveedor> DevolucionProveedorRepository { get; set; }
        DbSet<Distrito> DistritoRepository { get; set; }
        DbSet<DocumentoElectronico> DocumentoElectronicoRepository { get; set; }
        DbSet<Egreso> EgresoRepository { get; set; }
        DbSet<Empresa> EmpresaRepository { get; set; }
        DbSet<FormaPago> FormaPagoRepository { get; set; }
        DbSet<Ingreso> IngresoRepository { get; set; }
        DbSet<Linea> LineaRepository { get; set; }
        DbSet<MovimientoApartado> MovimientoApartadoRepository { get; set; }
        DbSet<MovimientoBanco> MovimientoBancoRepository { get; set; }
        DbSet<MovimientoCuentaPorCobrar> MovimientoCuentaPorCobrarRepository { get; set; }
        DbSet<MovimientoCuentaPorPagar> MovimientoCuentaPorPagarRepository { get; set; }
        DbSet<MovimientoProducto> MovimientoProductoRepository { get; set; }
        DbSet<MovimientoOrdenServicio> MovimientoOrdenServicioRepository { get; set; }
        DbSet<OrdenCompra> OrdenRepository { get; set; }
        DbSet<OrdenServicio> OrdenServicioRepository { get; set; }
        DbSet<Padron> PadronRepository { get; set; }
        DbSet<ParametroContable> ParametroContableRepository { get; set; }
        DbSet<ParametroExoneracion> ParametroExoneracionRepository { get; set; }
        DbSet<ParametroImpuesto> ParametroImpuestoRepository { get; set; }
        DbSet<ParametroSistema> ParametroSistemaRepository { get; set; }
        DbSet<Producto> ProductoRepository { get; set; }
        DbSet<Proforma> ProformaRepository { get; set; }
        DbSet<Proveedor> ProveedorRepository { get; set; }
        DbSet<Provincia> ProvinciaRepository { get; set; }
        DbSet<RegistroAutenticacion> RegistroAutenticacionRepository { get; set; }
        DbSet<RegistroRespuestaHacienda> RegistroRespuestaHaciendaRepository { get; set; }
        DbSet<ReportePorEmpresa> ReportePorEmpresaRepository { get; set; }
        DbSet<Role> RoleRepository { get; set; }
        DbSet<RolePorEmpresa> RolePorEmpresaRepository { get; set; }
        DbSet<RolePorUsuario> RolePorUsuarioRepository { get; set; }
        DbSet<SaldoMensualContable> SaldoMensualContableRepository { get; set; }
        DbSet<SucursalPorEmpresa> SucursalPorEmpresaRepository { get; set; }
        DbSet<TerminalPorSucursal> TerminalPorSucursalRepository { get; set; }
        DbSet<TipoDeCambioDolar> TipoDeCambioDolarRepository { get; set; }
        DbSet<TipoCuentaContable> TipoCuentaContableRepository { get; set; }
        DbSet<TipoIdentificacion> TipoIdentificacionRepository { get; set; }
        DbSet<TipoMoneda> TipoMonedaRepository { get; set; }
        DbSet<TipoMovimientoBanco> TipoMovimientoBancoRepository { get; set; }
        DbSet<TipoParametroContable> TipoParametroContableRepository { get; set; }
        DbSet<TipoProducto> TipoProductoRepository { get; set; }
        DbSet<Traslado> TrasladoRepository { get; set; }
        DbSet<Usuario> UsuarioRepository { get; set; }
        DbSet<UsuarioPorEmpresa> UsuarioPorEmpresaRepository { get; set; }
        DbSet<Vendedor> VendedorRepository { get; set; }

        void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class;
        void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class;
        DbContextTransaction GetDatabaseTransaction();
        void ExecuteProcedure(string procedureName, object[] objParameters);
        void Commit();
        void RollBack();
    }

    public partial class LeandroContext : DbContext, IDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LeandroContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public LeandroContext(string conectionString)
        {
            Database.Connection.ConnectionString = conectionString;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Asiento> AsientoRepository { get; set; }
        public DbSet<AjusteInventario> AjusteInventarioRepository { get; set; }
        public DbSet<Apartado> ApartadoRepository { get; set; }
        public DbSet<BancoAdquiriente> BancoAdquirienteRepository { get; set; }
        public DbSet<Barrio> BarrioRepository { get; set; }
        public DbSet<CantFEMensualEmpresa> CantFEMensualEmpresaRepository { get; set; }
        public DbSet<Canton> CantonRepository { get; set; }
        public DbSet<CatalogoContable> CatalogoContableRepository { get; set; }
        public DbSet<CatalogoReporte> CatalogoReporteRepository { get; set; }
        public DbSet<CierreCaja> CierreCajaRepository { get; set; }
        public DbSet<ClaseCuentaContable> ClaseCuentaContableRepository { get; set; }
        public DbSet<Cliente> ClienteRepository { get; set; }
        public DbSet<CondicionVenta> CondicionVentaRepository { get; set; }
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
        public DbSet<FormaPago> FormaPagoRepository { get; set; }
        public DbSet<Ingreso> IngresoRepository { get; set; }
        public DbSet<Linea> LineaRepository { get; set; }
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
        public DbSet<ParametroExoneracion> ParametroExoneracionRepository { get; set; }
        public DbSet<ParametroImpuesto> ParametroImpuestoRepository { get; set; }
        public DbSet<ParametroSistema> ParametroSistemaRepository { get; set; }
        public DbSet<Producto> ProductoRepository { get; set; }
        public DbSet<Proforma> ProformaRepository { get; set; }
        public DbSet<Proveedor> ProveedorRepository { get; set; }
        public DbSet<Provincia> ProvinciaRepository { get; set; }
        public DbSet<RegistroAutenticacion> RegistroAutenticacionRepository { get; set; }
        public DbSet<RegistroRespuestaHacienda> RegistroRespuestaHaciendaRepository { get; set; }
        public DbSet<ReportePorEmpresa> ReportePorEmpresaRepository { get; set; }
        public DbSet<Role> RoleRepository { get; set; }
        public DbSet<RolePorEmpresa> RolePorEmpresaRepository { get; set; }
        public DbSet<RolePorUsuario> RolePorUsuarioRepository { get; set; }
        public DbSet<SaldoMensualContable> SaldoMensualContableRepository { get; set; }
        public DbSet<SucursalPorEmpresa> SucursalPorEmpresaRepository { get; set; }
        public DbSet<TerminalPorSucursal> TerminalPorSucursalRepository { get; set; }
        public DbSet<TipoDeCambioDolar> TipoDeCambioDolarRepository { get; set; }
        public DbSet<TipoCuentaContable> TipoCuentaContableRepository { get; set; }
        public DbSet<TipoIdentificacion> TipoIdentificacionRepository { get; set; }
        public DbSet<TipoMoneda> TipoMonedaRepository { get; set; }
        public DbSet<TipoMovimientoBanco> TipoMovimientoBancoRepository { get; set; }
        public DbSet<TipoParametroContable> TipoParametroContableRepository { get; set; }
        public DbSet<TipoProducto> TipoProductoRepository { get; set; }
        public DbSet<Traslado> TrasladoRepository { get; set; }
        public DbSet<Usuario> UsuarioRepository { get; set; }
        public DbSet<UsuarioPorEmpresa> UsuarioPorEmpresaRepository { get; set; }
        public DbSet<Vendedor> VendedorRepository { get; set; }

        public void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class
        {
            Entry<TEntity>(entidad).State = EntityState.Modified;
        }

        public void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class
        {
            Entry<TEntity>(entidad).State = EntityState.Deleted;
        }

        public DbContextTransaction GetDatabaseTransaction()
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
            Database.ExecuteSqlCommand("call " + procedureName + "(" + strParameters + ")");
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
