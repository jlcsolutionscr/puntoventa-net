using System.Data;
using System.Linq;
using System.Globalization;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Parametros;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IContabilidadService
    {
        IEnumerable<ClaseCuentaContableElemento> ObtenerListadoClaseCuentaContable();
        ClaseCuentaContableElemento ObtenerClaseCuentaContable(int intIdClase);
        IEnumerable<TipoParametroContable> ObtenerListadoTipoParametroContable();
        TipoParametroContable ObtenerTipoParametroContable(int intIdTipo);
        void AgregarCuentaContable(CatalogoContable cuenta);
        void ActualizarCuentaContable(CatalogoContable cuenta);
        void EliminarCuentaContable(int intIdCuenta);
        CatalogoContable ObtenerCuentaContable(int intIdCuenta);
        IEnumerable<CatalogoContable> ObtenerListadoCuentasContables(int intIdEmpresa, string strDescripcion);
        ParametroContable AgregarParametroContable(ParametroContable parametro);
        void ActualizarParametroContable(ParametroContable parametro);
        void EliminarParametroContable(int intIdParametro);
        ParametroContable ObtenerParametroContable(int intIdParametro);
        IEnumerable<ParametroContable> ObtenerListadoParametrosContables(string strDescripcion);
        IEnumerable<CatalogoContable> ObtenerListadoCuentaDeMayor(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListadoCuentasParaMovimientos(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListadoCuentasParaLineasDeProducto(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListadoCuentasParaLineasDeServicio(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListadoCuentasParaBancos(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListadoCuentasParaEgresos(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListadoCuentasParaIngresos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListadoCuentasDeBalance(int intIdEmpresa);
        Asiento AgregarAsiento(Asiento asiento);
        Asiento AgregarAsiento(Asiento asiento, LeandroContext dbContext);
        void ReversarAsientoContable(int intIdAsiento);
        void ReversarAsientoContable(int intIdAsiento, LeandroContext dbContext);
        void ActualizarAsiento(Asiento asiento);
        void AnularAsiento(int intIdAsiento, int intIdUsuario);
        Asiento ObtenerAsiento(int intIdAsiento);
        int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento, string strDetalle);
        IEnumerable<Asiento> ObtenerListadoAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento, string strDetalle);
        void ProcesarCierreMensual(int intIdEmpresa);
        void AjustarSaldosCuentasdeMayor();
        List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0);
        List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa, int intIdSucursal);
        List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal);
        
    }
    
    public class ContabilidadService: IContabilidadService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory? _serviceScopeFactory;
        private static IConfiguracionGeneral? _config;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public ContabilidadService(ILoggerManager logger, IConfiguracionGeneral? config)
        {
            try
            {
                _logger = logger;
                _config = config;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al inicializar el servicio: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public ContabilidadService(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory, IConfiguracionGeneral config)
        {
            try
            {
                _logger = logger;
                _serviceScopeFactory = serviceScopeFactory;
                _config = config;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al inicializar el servicio: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ClaseCuentaContableElemento> ObtenerListadoClaseCuentaContable()
        {
            return ClaseCuentaContable.ObtenerListado();
        }

        public ClaseCuentaContableElemento ObtenerClaseCuentaContable(int intIdClase)
        {
            return ClaseCuentaContable.Encontrar(intIdClase);
        }

        public IEnumerable<TipoParametroContable> ObtenerListadoTipoParametroContable()
        {
            return TipoParametroContable.ObtenerListado();
        }

        public TipoParametroContable ObtenerTipoParametroContable(int intIdTipo)
        {
            return TipoParametroContable.Encontrar(intIdTipo);
        }

        public void AgregarCuentaContable(CatalogoContable cuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CatalogoContableRepository.Add(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar la cuenta contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaContable(CatalogoContable cuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al actualizar la cuenta contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaContable(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    if (cuenta == null) throw new BusinessException("La cuenta contable por eliminar no existe!");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CatalogoContableRepository.Remove(cuenta);
                    dbContext.Commit();
                }
                catch (DbUpdateException uex)
                {
                    if (_logger != null) _logger.LogError("Validación al eliminar la cuenta contable: ", uex);
                    throw new BusinessException("No es posible eliminar la cuenta contable seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al eliminar la cuenta contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error eliminando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public CatalogoContable ObtenerCuentaContable(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cuenta contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListadoCuentasContables(int intIdEmpresa, string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaCuentas = listaCuentas.Where(c => c.Descripcion.Contains(strDescripcion));
                    return listaCuentas.OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error cosultando el listado de cuentas contables. Por favor consulte con su proveedor.");
                }
            }
        }

        private bool esCuentaMadre(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    return cuenta.IdCuentaGrupo == null;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al evaluar si la cuenta indicada es cuenta madre: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error verificando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable AgregarParametroContable(ParametroContable parametro)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var tipo = TipoParametroContable.Encontrar(parametro.IdTipo);
                    bool bolError = tipo.MultiCuenta && dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo).Count() > 0;
                    if (bolError)
                        throw new BusinessException("El tipo de parámetro contable " + tipo.Descripcion + " no soporta la asignación de múltiples cuentas contables");
                    bolError = dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo && x.IdCuenta == parametro.IdCuenta).Count() > 0;
                    if (bolError)
                        throw new BusinessException("El tipo de parámetro contable " + tipo.Descripcion + " ya tiene asignada la cuenta contable seleccionada");
                    dbContext.ParametroContableRepository.Add(parametro);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar el parámetro contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando el parámetro contable. Por favor consulte con su proveedor.");
                }
                return parametro;
            }
        }

        public void ActualizarParametroContable(ParametroContable parametro)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al actualizar el parámetro contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarParametroContable(int intIdParametro)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                    if (parametro == null) throw new Exception("El parámetro contable por eliminar no existe");
                    dbContext.ParametroContableRepository.Remove(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al eliminar el parámetro contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error eliminando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable ObtenerParametroContable(int intIdParametro)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                    return parametro;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el parámetro contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoParametrosContables(string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaParametros = dbContext.ParametroContableRepository.Include("CatalogoContable").Where(x => x.IdParametro == x.IdParametro);
                    if (!strDescripcion.Equals(string.Empty))
                        listaParametros = listaParametros.Where(x => x.Descripcion.Contains(strDescripcion));
                    return listaParametros.ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de parámetros contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de parámetros contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListadoCuentaDeMayor(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento == false).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables de primer orden: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables de primer orden. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListadoCuentasParaMovimientos(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para movimientos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para movimientos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoCuentasParaLineasDeProducto(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { TipoParametroContable.ObtenerId("LineaDeProductos") }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para líneas de producto: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoCuentasParaLineasDeServicio(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { TipoParametroContable.ObtenerId("LineaDeServicios") }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para líneas de servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoCuentasParaBancos(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { TipoParametroContable.ObtenerId("CuentaDeBancos") }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para cuentas bancarias: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para cuentas bancarías. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoCuentasParaEgresos(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { TipoParametroContable.ObtenerId("CuentaDeEgresos") }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListadoCuentasParaIngresos(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { TipoParametroContable.ObtenerId("CuentaDeIngresos") }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListadoCuentasDeBalance(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de PyG: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas contables de perdias y ganancias. Por favor consulte con su proveedor.");
                }
            }
        }

        public Asiento AgregarAsiento(Asiento asiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                return AdicionarAsiento(asiento, dbContext);
            }
        }

        public Asiento AgregarAsiento(Asiento asiento, LeandroContext dbContext)
        {
            return AdicionarAsiento(asiento, dbContext);
        }

        private Asiento AdicionarAsiento(Asiento asiento, LeandroContext dbContext)
        {
            if (asiento.TotalDebito != asiento.TotalCredito)
                throw new BusinessException("El asiento contable se encuentra descuadrado. Por favor verifique los datos.");
            try
            {
                Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                dbContext.AsientoRepository.Add(asiento);
                foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                {
                    if (detalleAsiento.CatalogoContable.PermiteMovimiento == false)
                        throw new BusinessException("La cuenta contable " + detalleAsiento.CatalogoContable.Descripcion + " no permite movimientos. Por favor verifique los parámetros del catalogo contable.");
                    if (detalleAsiento.Debito > 0)
                        MayorizarCuenta(detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Debito);
                    else
                        MayorizarCuenta(detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Credito);
                }
                dbContext.Commit();
            }
            catch (BusinessException)
            {
                dbContext.RollBack();
                throw;
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                if (_logger != null) _logger.LogError("Error al agregar el asiento contable: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error agregando la información del asiento contable. Por favor consulte con su proveedor.");
            }
            return asiento;
        }

        public void ReversarAsientoContable(int intIdAsiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                InvalidarAsientoContable(intIdAsiento, dbContext);
            }
        }

        public void ReversarAsientoContable(int intIdAsiento, LeandroContext dbContext)
        {
            InvalidarAsientoContable(intIdAsiento, dbContext);
        }

        public void InvalidarAsientoContable(int intIdAsiento, LeandroContext dbContext)
        {
            try
            {
                Asiento asiento = dbContext.AsientoRepository.Include("DetalleAsiento").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                Asiento asientoDeReversion = new Asiento
                {
                    IdEmpresa = asiento.IdEmpresa,
                    Detalle = "Reversión de " + asiento.Detalle,
                    Fecha = Validador.ObtenerFechaHoraCostaRica(),
                    TotalDebito = asiento.TotalCredito,
                    TotalCredito = asiento.TotalDebito
                };
                foreach (var detalle in asiento.DetalleAsiento)
                {
                    DetalleAsiento detalleReversion = new DetalleAsiento
                    {
                        Linea = detalle.Linea,
                        IdCuenta = detalle.IdCuenta,
                        Credito = detalle.Debito,
                        Debito = detalle.Credito
                    };
                    asientoDeReversion.DetalleAsiento.Add(detalleReversion);
                }
                AgregarAsiento(asientoDeReversion);
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                if (_logger != null) _logger.LogError("Error al reversar asiento contable: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error reversando el asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public void ActualizarAsiento(Asiento asiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(asiento);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al actualizar el asiento contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularAsiento(int intIdAsiento, int intIdUsuario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Asiento asiento = dbContext.AsientoRepository.Include("DetalleAsiento").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                    if (asiento == null) throw new BusinessException("El asiento contable por anular no existe");
                    if (asiento.Nulo)
                        return;
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                    {
                        if (detalleAsiento.Debito > 0)
                            MayorizarCuenta(detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Debito);
                        else
                            MayorizarCuenta(detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Credito);
                    }
                    asiento.Nulo = true;
                    asiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(asiento);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al anular el asiento contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public Asiento ObtenerAsiento(int intIdAsiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.AsientoRepository.Include("DetalleAsiento.CatalogoContable").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el asiento contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento, string strDetalle)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaAsientos = dbContext.AsientoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdAsiento > 0)
                        listaAsientos = listaAsientos.Where(x => x.IdAsiento == intIdAsiento);
                    else if (!strDetalle.Equals(string.Empty))
                        listaAsientos = listaAsientos.Where(x => x.Detalle.Contains(strDetalle));
                    return listaAsientos.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el total del listado de asientos contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el total del listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Asiento> ObtenerListadoAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento, string strDetalle)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaAsientos = dbContext.AsientoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdAsiento > 0)
                        listaAsientos = listaAsientos.Where(x => x.IdAsiento == intIdAsiento);
                    else if (!strDetalle.Equals(string.Empty))
                        listaAsientos = listaAsientos.Where(x => x.Detalle.Contains(strDetalle));
                    return listaAsientos.OrderByDescending(x => x.IdAsiento).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de asientos contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarCierreMensual(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                decimal decTotalEgresos = 0;
                decimal decTotalIngresos = 0;
                ParametroContable perdidaGananciaParam = null;
                Empresa empresa = null;
                DateTime horaActual = Validador.ObtenerFechaHoraCostaRica();
                try
                {
                    empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    perdidaGananciaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContable.ObtenerId("PerdidasyGanancias")).FirstOrDefault();
                    if (perdidaGananciaParam == null) throw new BusinessException("La cuenta de perdidas y ganancias no se encuentra parametrizada y no se puede ejecutar el cierre contable. Por favor verificar.");

                    var saldosMensuales = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.SaldoActual != 0)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    foreach (CatalogoContable value in saldosMensuales)
                    {
                        SaldoMensualContable saldoMensual = new SaldoMensualContable
                        {
                            IdCuenta = value.IdCuenta,
                            Mes = horaActual.Month,
                            Annio = horaActual.Year,
                            SaldoFinMes = value.SaldoActual,
                            TotalDebito = value.TotalDebito,
                            TotalCredito = value.TotalCredito
                        };
                        dbContext.SaldoMensualContableRepository.Add(saldoMensual);
                    }

                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true && x.SaldoActual != 0 && x.IdClaseCuenta == 3/*CUENTA DE RESULTADOS*/)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    Asiento asiento = null;
                    asiento = new Asiento
                    {
                        IdEmpresa = intIdEmpresa,
                        Fecha = horaActual,
                        TotalCredito = 0,
                        TotalDebito = 0,
                        Detalle = "Empresa cierre perdidas y ganancías"
                    };
                    DetalleAsiento detalleAsiento = null;
                    int intLineaDetalleAsiento = 0;
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        if (value.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            decTotalEgresos += value.SaldoActual;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento += 1,
                                IdCuenta = value.IdCuenta,
                                Credito = value.SaldoActual,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(value.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                        }
                        else
                        {
                            decTotalIngresos += value.SaldoActual;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento += 1,
                                IdCuenta = value.IdCuenta,
                                Debito = value.SaldoActual,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(value.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                        }
                    }
                    detalleAsiento = new DetalleAsiento
                    {
                        Linea = intLineaDetalleAsiento += 1,
                        IdCuenta = perdidaGananciaParam.IdCuenta
                    };
                    decimal decDiferencia;
                    if (decTotalEgresos > decTotalIngresos)
                    {
                        decDiferencia = decTotalEgresos - decTotalIngresos;
                        decTotalIngresos += decDiferencia;
                        detalleAsiento.Debito = decDiferencia;
                    }
                    else
                    {
                        decDiferencia = decTotalIngresos - decTotalEgresos;
                        decTotalEgresos += decDiferencia;
                        detalleAsiento.Credito = decDiferencia;
                    }
                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(perdidaGananciaParam.IdCuenta).SaldoActual;
                    asiento.DetalleAsiento.Add(detalleAsiento);
                    asiento.TotalDebito = decTotalIngresos;
                    asiento.TotalCredito = decTotalEgresos;
                    AgregarAsiento(asiento);
                    dbContext.Commit();
                }
                catch (BusinessException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al ejecutar el cierre mensual contable: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error ejecutando el cierre mensual contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AjustarSaldosCuentasdeMayor()
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var cuentas = dbContext.CatalogoContableRepository.Where(x => x.PermiteMovimiento == true && x.SaldoActual > 0).ToList();
                    foreach (CatalogoContable cuenta in cuentas)
                    {
                        if (cuenta.IdCuentaGrupo != null)
                        {
                            if (cuenta.TipoSaldo == StaticTipoDebitoCredito.Debito)
                                MayorizarCuenta((int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Debito, cuenta.SaldoActual);
                            else
                                MayorizarCuenta((int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Credito, cuenta.SaldoActual);
                        }
                    }
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al realizar el ajuste de saldos contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error ejecutando el ajuste de saldos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosContables> listaReporte = new List<ReporteMovimientosContables>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, b => b.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial && x.d.Fecha <= datFechaFinal && x.d.Nulo == false)
                        .GroupBy(x => x.c.a.Descripcion)
                        .Select(a => new { TotalDebito = a.Sum(b => b.c.b.Debito), TotalCredito = a.Sum(b => b.c.b.Credito), Descripcion = a.Key });
                    foreach (var value in listaCuentas)
                    {
                        ReporteMovimientosContables reporteLinea = new ReporteMovimientosContables
                        {
                            Descripcion = value.Descripcion,
                            SaldoDebe = value.TotalDebito,
                            SaldoHaber = value.TotalCredito
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de movimientos contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de movimientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteBalanceComprobacion> listaReporte = new List<ReporteBalanceComprobacion>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReporteBalanceComprobacion reporteLinea = new ReporteBalanceComprobacion
                        {
                            IdCuenta = value.IdCuenta,
                            Descripcion = value.Descripcion
                        };
                        if (intMes > 0 && intAnnio > 0)
                            decSaldo = dbContext.SaldoMensualContableRepository.Where(x => x.Mes == intMes && x.Annio == intAnnio && x.IdCuenta == value.IdCuenta).Select(a => a.SaldoFinMes).FirstOrDefault();
                        else
                            decSaldo = value.SaldoActual;
                        if (value.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            reporteLinea.SaldoDebe = decSaldo;
                            reporteLinea.SaldoHaber = 0;
                        }
                        else
                        {
                            reporteLinea.SaldoDebe = 0;
                            reporteLinea.SaldoHaber = decSaldo;
                        }
                        if (decSaldo != 0)
                            listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa, int intIdSucursal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReportePerdidasyGanancias> listaReporte = new List<ReportePerdidasyGanancias>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true && x.SaldoActual != 0 && x.IdClaseCuenta == 3/*CUENTA DE RESULTADOS*/)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReportePerdidasyGanancias reporteLinea = new ReportePerdidasyGanancias
                        {
                            Descripcion = value.Descripcion,
                            IdClaseCuenta = value.IdClaseCuenta
                        };
                        if (value.TipoSaldo == StaticTipoDebitoCredito.Debito)
                            reporteLinea.DescGrupo = "Cuentas de Egresos";
                        else
                            reporteLinea.DescGrupo = "Cuentas de Ingresos";
                        decSaldo = value.SaldoActual;
                        if (value.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            reporteLinea.SaldoDebe = decSaldo;
                            reporteLinea.SaldoHaber = 0;
                        }
                        else
                        {
                            reporteLinea.SaldoDebe = 0;
                            reporteLinea.SaldoHaber = decSaldo;
                        }
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalleMovimientosCuentasDeBalance> listaReporte = new List<ReporteDetalleMovimientosCuentasDeBalance>();
                    var cuentaDeBalance = dbContext.CatalogoContableRepository.Where(a => a.IdCuenta == intIdCuentaGrupo).FirstOrDefault();
                    int annioSaldoAnterior = datFechaInicial.Year;
                    int mesSaldoAnterior = datFechaInicial.Month - 1;
                    if (mesSaldoAnterior == 1)
                    {
                        annioSaldoAnterior -= 1;
                        mesSaldoAnterior = 12;
                    }
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdCuentaGrupo == intIdCuentaGrupo)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial && x.d.Fecha <= datFechaFinal && x.d.Nulo == false)
                        .OrderBy(x => x.d.IdAsiento)
                        .Select(a => new { a.c.a.IdCuenta, a.c.a.Descripcion, a.c.b.SaldoAnterior, a.d.Fecha, a.d.Detalle, a.c.b.Debito, a.c.b.Credito }).OrderBy(a => a.IdCuenta).ThenBy(a => a.Fecha).ToList();
                    foreach (var value in listaCuentas)
                    {
                        ReporteDetalleMovimientosCuentasDeBalance reporteLinea = new ReporteDetalleMovimientosCuentasDeBalance
                        {
                            DescCuentaBalance = cuentaDeBalance.Descripcion,
                            Descripcion = value.Descripcion,
                            SaldoInicial = value.SaldoAnterior,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Detalle = value.Detalle,
                            Debito = value.Debito,
                            Credito = value.Credito
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de detalle del balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle del balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        private void MayorizarCuenta(int intIdCuenta, string strTipoMov, decimal dblMonto)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                CatalogoContable catalogoContable = dbContext.CatalogoContableRepository.Include("TipoCuentaContable").FirstOrDefault(x => x.IdCuenta == intIdCuenta);
                if (catalogoContable == null) throw new Exception("La cuenta contable por mayorizar no existe");
                if (strTipoMov.Equals(StaticTipoDebitoCredito.Debito))
                    if (catalogoContable.TipoSaldo.Equals(StaticTipoDebitoCredito.Debito))
                    {
                        catalogoContable.SaldoActual += dblMonto;
                        catalogoContable.TotalDebito += dblMonto;
                    }
                    else
                    {
                        catalogoContable.SaldoActual -= dblMonto;
                        catalogoContable.TotalDebito += dblMonto;
                    }
                else
                    if (catalogoContable.TipoSaldo.Equals(StaticTipoDebitoCredito.Credito))
                {
                    catalogoContable.SaldoActual += dblMonto;
                    catalogoContable.TotalCredito += dblMonto;
                }
                else
                {
                    catalogoContable.SaldoActual -= dblMonto;
                    catalogoContable.TotalCredito += dblMonto;
                }
                dbContext.NotificarModificacion(catalogoContable);
                if (catalogoContable.IdCuentaGrupo > 0)
                    MayorizarCuenta((int)catalogoContable.IdCuentaGrupo, strTipoMov, dblMonto);
            }
        }
    }
}