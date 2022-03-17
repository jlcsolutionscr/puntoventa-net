using LeandroSoftware.Common.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LeandroSoftware.ServicioWeb.Contexto
{
    public interface ILeandroContext : IDisposable
    {
        DbSet<ActividadEconomicaEmpresa> ActividadEconomicaEmpresaRepository { get; set; }
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
        DbSet<ClasificacionProducto> ClasificacionProductoRepository { get; set; }
        DbSet<Cliente> ClienteRepository { get; set; }
        DbSet<CredencialesHacienda> CredencialesHaciendaRepository { get; set; }
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
        DbSet<Ingreso> IngresoRepository { get; set; }
        DbSet<Linea> LineaRepository { get; set; }
        DbSet<LineaPorSucursal> LineaPorSucursalRepository { get; set; }
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
        DbSet<ParametroSistema> ParametroSistemaRepository { get; set; }
        DbSet<Producto> ProductoRepository { get; set; }
        DbSet<Proforma> ProformaRepository { get; set; }
        DbSet<Proveedor> ProveedorRepository { get; set; }
        DbSet<Provincia> ProvinciaRepository { get; set; }
        DbSet<PuntoDeServicio> PuntoDeServicioRepository { get; set; }
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
        DbSet<TipoMovimientoBanco> TipoMovimientoBancoRepository { get; set; }
        DbSet<TipoParametroContable> TipoParametroContableRepository { get; set; }
        DbSet<TiqueteOrdenServicio> TiqueteOrdenServicioRepository { get; set; }
        DbSet<Traslado> TrasladoRepository { get; set; }
        DbSet<Usuario> UsuarioRepository { get; set; }
        DbSet<Vendedor> VendedorRepository { get; set; }

        void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class;
        void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class;
        IDbContextTransaction GetDatabaseTransaction();
        void ExecuteProcedure(string procedureName, object[] objParameters);
        void Commit();
        void RollBack();
    }
}
