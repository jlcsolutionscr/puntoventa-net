using Microsoft.EntityFrameworkCore;
using System.Globalization;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IFlujoCajaService
    {
        void AgregarCuentaIngreso(CuentaIngreso cuenta);
        void ActualizarCuentaIngreso(CuentaIngreso cuenta);
        void EliminarCuentaIngreso(int intIdCuenta);
        CuentaIngreso ObtenerCuentaIngreso(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasIngreso(int intIdEmpresa, string strDescripcion);
        string AgregarIngreso(Ingreso ingreso);
        void AnularIngreso(int intIdIngreso, int intIdUsuario, string strMotivoAnulacion);
        Ingreso ObtenerIngreso(int intIdIngreso);
        int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdSucursal, int intIdIngreso, string strRecibidoDe, string strDetalle, string strFechaFinal);
        IList<EfectivoDetalle> ObtenerListadoIngresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdIngreso, string strRecibidoDe, string strDetalle, string strFechaFinal);
        void AgregarCuentaEgreso(CuentaEgreso cuenta);
        void ActualizarCuentaEgreso(CuentaEgreso cuenta);
        void EliminarCuentaEgreso(int intIdCuenta);
        CuentaEgreso ObtenerCuentaEgreso(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion);
        string AgregarEgreso(Egreso egreso);
        void AnularEgreso(int intIdEgreso, int intIdUsuario, string strMotivoAnulacion);
        Egreso ObtenerEgreso(int intIdEgreso);
        int ObtenerTotalListaEgresos(int intIdEmpresa, int intIdSucursal, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal);
        IList<EfectivoDetalle> ObtenerListadoEgresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal);
        CierreCaja GenerarDatosCierreCaja(int intIdEmpresa, int intIdSucursal);
        string GuardarDatosCierreCaja(CierreCaja cierre);
        void AbortarCierreCaja(int intIdEmpresa, int intIdSucursal);
        int ObtenerTotalListaCierreCaja(int intIdEmpresa, int intIdSucursal);
        IList<LlaveDescripcion> ObtenerListadoCierreCaja(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec);
        CierreCaja ObtenerCierreCaja(int intIdCierreCaja);
    }

    public class FlujoCajaService : IFlujoCajaService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory? _serviceScopeFactory;
        private static IConfiguracionGeneral? _config;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public FlujoCajaService(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory, IConfiguracionGeneral config)
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
                else throw new Exception("Se produjo un error al inicializar el servicio de Egresos. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarCuentaIngreso(CuentaIngreso cuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaIngresoRepository.Add(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar la cuenta de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaIngreso(CuentaIngreso cuenta)
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al actualizar la cuenta de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaIngreso(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CuentaIngreso cuenta = dbContext.CuentaIngresoRepository.Find(intIdCuenta);
                    if (cuenta == null) throw new BusinessException("La cuenta de ingreso por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaIngresoRepository.Remove(cuenta);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    if (_logger != null) _logger.LogError("Validación al eliminar la cuenta de ingreso: ", ex);
                    throw new BusinessException("No es posible eliminar la cuenta de ingreso seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al eliminar la cuenta de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error eliminando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaIngreso ObtenerCuentaIngreso(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaIngresoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cuenta de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasIngreso(int intIdEmpresa, string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listadoCuentaIngreso = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CuentaIngresoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderBy(x => x.Descripcion);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCuenta, value.Descripcion);
                        listadoCuentaIngreso.Add(item);
                    }
                    return listadoCuentaIngreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas de ingresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarIngreso(Ingreso ingreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                ParametroContable efectivo = null;
                ParametroContable ingresoParam = null;
                ParametroContable cuentaPorCobrarTarjetaParam = null;
                ParametroContable gastoComisionParam = null;
                ParametroContable ivaPorPagarParam = null;
                Asiento asiento = null;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ingreso.IdEmpresa && x.IdSucursal == ingreso.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.GastoComisionTarjeta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        if (efectivo == null || cuentaPorCobrarTarjetaParam == null || gastoComisionParam == null || ivaPorPagarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    CuentaIngreso cuentaIngreso = dbContext.CuentaIngresoRepository.Find(ingreso.IdCuenta);
                    if (cuentaIngreso == null)
                        throw new BusinessException("La cuenta de ingreso asignada al registro no existe");
                    ingreso.IdAsiento = 0;
                    ingreso.IdMovBanco = 0;
                    dbContext.IngresoRepository.Add(ingreso);
                    if (empresa.Contabiliza)
                    {
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = ingreso.IdEmpresa,
                            Fecha = ingreso.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de ingreso nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = efectivo.IdCuenta;
                        detalleAsiento.Debito = ingreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        ingresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeIngresos & x.IdProducto == cuentaIngreso.IdCuenta).FirstOrDefault();
                        if (ingresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaIngreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = ingresoParam.IdCuenta;
                        detalleAsiento.Credito = ingreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        ingreso.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(ingreso);
                        asiento.Detalle += ingreso.IdIngreso;
                        dbContext.NotificarModificacion(asiento);
                    }
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar el registro de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la información del ingreso. Por favor consulte con su proveedor.");
                }
                return ingreso.IdIngreso.ToString();
            }
        }

        public void AnularIngreso(int intIdIngreso, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Ingreso ingreso = dbContext.IngresoRepository.Find(intIdIngreso);
                    if (ingreso == null) throw new BusinessException("El ingreso por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ingreso.IdEmpresa && x.IdSucursal == ingreso.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (ingreso.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    ingreso.Nulo = true;
                    ingreso.IdAnuladoPor = intIdUsuario;
                    ingreso.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(ingreso);
                    if (ingreso.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(ingreso.IdAsiento, dbContext);
                    }
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al anular el registro de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public Ingreso ObtenerIngreso(int intIdIngreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Ingreso ingreso = dbContext.IngresoRepository.FirstOrDefault(x => x.IdIngreso == intIdIngreso);
                    return ingreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el registro de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdSucursal, int intIdIngreso, string strRecibidoDe, string strDetalle, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaIngresos = dbContext.IngresoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdIngreso > 0)
                        listaIngresos = listaIngresos.Where(x => x.IdIngreso == intIdIngreso);
                    else
                    {
                        if (!strRecibidoDe.Equals(string.Empty))
                            listaIngresos = listaIngresos.Where(x => x.RecibidoDe.Contains(strRecibidoDe));
                        if (!strDetalle.Equals(string.Empty))
                            listaIngresos = listaIngresos.Where(x => x.Detalle.Contains(strDetalle));
                    }
                    return listaIngresos.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el total del listado de registros de ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el total del listado de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoIngresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdIngreso, string strRecibidoDe, string strDetalle, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listadoIngreso = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.IngresoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdIngreso > 0)
                        listado = listado.Where(x => x.IdIngreso == intIdIngreso);
                    else
                    {
                        if (!strRecibidoDe.Equals(string.Empty))
                            listado = listado.Where(x => x.RecibidoDe.Contains(strRecibidoDe));
                        if (!strDetalle.Equals(string.Empty))
                            listado = listado.Where(x => x.Detalle.Contains(strDetalle));
                    }
                    listado = listado.OrderByDescending(x => x.IdIngreso).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdIngreso, value.Fecha.ToString("dd/MM/yyyy"), value.Detalle, value.Monto);
                        listadoIngreso.Add(item);
                    }
                    return listadoIngreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de registros de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarCuentaEgreso(CuentaEgreso cuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaEgresoRepository.Add(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar la cuenta de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaEgreso(CuentaEgreso cuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al actualizar la cuenta de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaEgreso(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(intIdCuenta);
                    if (cuentaEgreso == null) throw new BusinessException("La cuenta de egreso por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaEgreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaEgresoRepository.Remove(cuentaEgreso);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    if (_logger != null) _logger.LogError("Validación al eliminar la cuenta de egreso: ", ex);
                    throw new BusinessException("No es posible eliminar la cuenta de egreso seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al eliminar la cuenta de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error eliminando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaEgreso ObtenerCuentaEgreso(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaEgresoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cuenta de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listadoCuentaEgreso = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CuentaEgresoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderBy(x => x.Descripcion);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCuenta, value.Descripcion);
                        listadoCuentaEgreso.Add(item);
                    }
                    return listadoCuentaEgreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas de egresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuentas de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarEgreso(Egreso egreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                ParametroContable efectivo = null;
                ParametroContable egresoParam = null;
                Asiento asiento = null;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == egreso.IdEmpresa && x.IdSucursal == egreso.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        if (efectivo == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(egreso.IdCuenta);
                    if (cuentaEgreso == null)
                        throw new BusinessException("La cuenta de egreso asignada al registro no existe");
                    egreso.IdAsiento = 0;
                    egreso.IdMovBanco = 0;
                    dbContext.EgresoRepository.Add(egreso);
                    if (empresa.Contabiliza)
                    {
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = egreso.IdEmpresa,
                            Fecha = egreso.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de egreso nro. "
                        };
                        DetalleAsiento detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        egresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeEgresos & x.IdProducto == cuentaEgreso.IdCuenta).FirstOrDefault();
                        if (egresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaEgreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = egresoParam.IdCuenta;
                        detalleAsiento.Debito = egreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = efectivo.IdCuenta;
                        detalleAsiento.Credito = egreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        egreso.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(egreso);
                        asiento.Detalle += egreso.IdEgreso;
                        dbContext.NotificarModificacion(asiento);
                    }
                    dbContext.Commit();
                    return egreso.IdEgreso.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al agregar el registro de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la información del egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularEgreso(int intIdEgreso, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Egreso egreso = dbContext.EgresoRepository.Find(intIdEgreso);
                    if (egreso == null) throw new BusinessException("El egreso por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == egreso.IdEmpresa && x.IdSucursal == egreso.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (egreso.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    egreso.Nulo = true;
                    egreso.IdAnuladoPor = intIdUsuario;
                    egreso.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(egreso);
                    if (egreso.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(egreso.IdAsiento, dbContext);
                    }
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al anular el registro de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public Egreso ObtenerEgreso(int intIdEgreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Egreso egreso = dbContext.EgresoRepository.FirstOrDefault(x => x.IdEgreso == intIdEgreso);
                    return egreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el registro de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la información del egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaEgresos(int intIdEmpresa, int intIdSucursal, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaEgresos = dbContext.EgresoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdEgreso > 0)
                        listaEgresos = listaEgresos.Where(x => x.IdEgreso == intIdEgreso);
                    if (!strBeneficiario.Equals(string.Empty))
                        listaEgresos = listaEgresos.Where(x => x.Beneficiario.Contains(strBeneficiario));
                    if (!strDetalle.Equals(string.Empty))
                        listaEgresos = listaEgresos.Where(x => x.Detalle.Contains(strDetalle));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listaEgresos = listaEgresos.Where(x => x.Fecha < datFechaFinal);
                    }
                    return listaEgresos.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el total del listado de registros de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el total del listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoEgresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listadoEgreso = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.EgresoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdEgreso > 0)
                        listado = listado.Where(x => x.IdEgreso == intIdEgreso);
                    if (!strBeneficiario.Equals(string.Empty))
                        listado = listado.Where(x => x.Beneficiario.Contains(strBeneficiario));
                    if (!strDetalle.Equals(string.Empty))
                        listado = listado.Where(x => x.Detalle.Contains(strDetalle));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha < datFechaFinal);
                    }
                    listado = listado.OrderByDescending(x => x.IdEgreso).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdEgreso, value.Fecha.ToString("dd/MM/yyyy"), value.Detalle, value.Monto);
                        listadoEgreso.Add(item);
                    }
                    return listadoEgreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de registros de egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public CierreCaja GenerarDatosCierreCaja(int intIdEmpresa, int intIdSucursal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    sucursal.CierreEnEjecucion = true;
                    dbContext.Commit();
                    CierreCaja cierre = new CierreCaja
                    {
                        IdEmpresa = intIdEmpresa,
                        IdSucursal = intIdSucursal
                    };
                    List<DetalleMovimientoCierreCaja> listaMovimientos = new List<DetalleMovimientoCierreCaja>();
                    CierreCaja cierreAnterior = cierreAnterior = dbContext.CierreCajaRepository.OrderByDescending(b => b.IdCierre).FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (cierreAnterior != null)
                        cierre.FondoInicio = cierreAnterior.FondoCierre;
                    else
                        cierre.FondoInicio = 0;
                    cierre.AdelantosApartadoEfectivo = 0;
                    cierre.AdelantosApartadoTarjeta = 0;
                    cierre.AdelantosApartadoBancos = 0;
                    cierre.AdelantosOrdenEfectivo = 0;
                    cierre.AdelantosOrdenTarjeta = 0;
                    cierre.AdelantosOrdenBancos = 0;
                    cierre.VentasEfectivo = 0;
                    cierre.VentasTarjeta = 0;
                    cierre.VentasBancos = 0;
                    cierre.PagosCxCEfectivo = 0;
                    cierre.PagosCxCTarjeta = 0;
                    cierre.PagosCxCBancos = 0;
                    cierre.IngresosEfectivo = 0;
                    cierre.ComprasEfectivo = 0;
                    cierre.ComprasBancos = 0;
                    cierre.PagosCxPEfectivo = 0;
                    cierre.PagosCxPBancos = 0;
                    cierre.EgresosEfectivo = 0;
                    cierre.RetencionTarjeta = 0;
                    cierre.ComisionTarjeta = 0;
                    cierre.VentasCredito = 0;
                    cierre.ComprasCredito = 0;
                    cierre.RetiroEfectivo = 0;

                    var facturas = dbContext.FacturaRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCondicionVenta != StaticCondicionVenta.Credito && !x.Procesado).ToList();
                    if (facturas.Count > 0)
                    {
                        var pagosFacturas = facturas.Join(dbContext.DesglosePagoFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                            .Select(y => new { y.x.ConsecFactura, y.x.Fecha, y.x.NombreCliente, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosFacturas)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.VentasEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecFactura,
                                    Tipo = 1,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Facturado a: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.VentasTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecFactura,
                                    Tipo = 2,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Factura: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.VentasBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecFactura,
                                    Tipo = 3,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Factura: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var facturasCredito = dbContext.FacturaRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCondicionVenta == StaticCondicionVenta.Credito && !x.Procesado).ToList();
                    if (facturasCredito.Count > 0)
                    {
                        foreach (var dato in facturasCredito)
                        {
                            cierre.VentasCredito += dato.Total * dato.TipoDeCambioDolar;
                            listaMovimientos.Add(new DetalleMovimientoCierreCaja
                            {
                                IdReferencia = dato.ConsecFactura,
                                Tipo = 4,
                                Fecha = dato.Fecha,
                                Descripcion = "Factura: " + dato.NombreCliente,
                                Total = dato.Total * dato.TipoDeCambioDolar
                            });
                        }
                    }

                    var apartados = dbContext.ApartadoRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (apartados.Count > 0)
                    {
                        var pagosApartados = apartados.Join(dbContext.DesglosePagoApartadoRepository, x => x.IdApartado, y => y.IdApartado, (x, y) => new { x, y })
                            .Select(y => new { y.x.ConsecApartado, y.x.Fecha, y.x.NombreCliente, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosApartados)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.AdelantosApartadoEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecApartado,
                                    Tipo = 5,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Apartado: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.AdelantosApartadoTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecApartado,
                                    Tipo = 6,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Adelanto de apartado: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.AdelantosApartadoBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecApartado,
                                    Tipo = 7,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Apartado: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var movimientosapartado = dbContext.MovimientoApartadoRepository.Include("Apartado").Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (movimientosapartado.Count > 0)
                    {
                        var pagosApartados = movimientosapartado.Join(dbContext.DesglosePagoMovimientoApartadoRepository, x => x.IdMovApartado, y => y.IdMovApartado, (x, y) => new { x, y })
                            .Select(y => new { y.x.IdMovApartado, y.x.Fecha, y.x.Apartado.ConsecApartado, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosApartados)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.AdelantosApartadoEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovApartado,
                                    Tipo = 5,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a apartado " + dato.ConsecApartado,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.AdelantosApartadoTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovApartado,
                                    Tipo = 6,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a apartado " + dato.ConsecApartado,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.AdelantosApartadoBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovApartado,
                                    Tipo = 7,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a apartado " + dato.ConsecApartado,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var ordenes = dbContext.OrdenServicioRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (ordenes.Count > 0)
                    {
                        var pagosOrdenes = ordenes.Join(dbContext.DesglosePagoOrdenServicioRepository, x => x.IdOrden, y => y.IdOrden, (x, y) => new { x, y })
                            .Select(y => new { y.x.ConsecOrdenServicio, y.x.Fecha, y.x.NombreCliente, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosOrdenes)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.AdelantosOrdenEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecOrdenServicio,
                                    Tipo = 8,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Orden de Servicio: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.AdelantosOrdenTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecOrdenServicio,
                                    Tipo = 9,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Orden de Servicio: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.AdelantosOrdenBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.ConsecOrdenServicio,
                                    Tipo = 10,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Orden de Servicio: " + dato.NombreCliente,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var MovimientosordenServicio = dbContext.MovimientoOrdenServicioRepository.Include("OrdenServicio").Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (MovimientosordenServicio.Count > 0)
                    {
                        var pagosOrdenes = MovimientosordenServicio.Join(dbContext.DesglosePagoMovimientoOrdenServicioRepository, x => x.IdMovOrden, y => y.IdMovOrden, (x, y) => new { x, y })
                            .Select(y => new { y.x.IdMovOrden, y.x.Fecha, y.x.OrdenServicio.ConsecOrdenServicio, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosOrdenes)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.AdelantosOrdenEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovOrden,
                                    Tipo = 8,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a orden de servicio " + dato.ConsecOrdenServicio,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.AdelantosOrdenTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovOrden,
                                    Tipo = 9,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a orden de servicio " + dato.ConsecOrdenServicio,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.AdelantosOrdenBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovOrden,
                                    Tipo = 10,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a orden de servicio " + dato.ConsecOrdenServicio,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var movimientosCxC = dbContext.MovimientoCuentaPorCobrarRepository.Include("CuentaPorCobrar").Where(x => x.Tipo == 1 && x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (movimientosCxC.Count > 0)
                    {
                        var pagosCxC = movimientosCxC.Join(dbContext.DesglosePagoMovimientoCuentaPorCobrarRepository, x => x.IdMovCxC, y => y.IdMovCxC, (x, y) => new { x, y })
                            .Select(y => new { y.x.IdMovCxC, y.x.Fecha, y.x.CuentaPorCobrar.Referencia, y.y.IdFormaPago, y.y.IdCuentaBanco, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosCxC)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.PagosCxCEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovCxC,
                                    Tipo = 11,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a cuenta por cobrar: " + dato.Referencia,
                                    Total = dato.MontoLocal
                                });
                            }
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.PagosCxCTarjeta += dato.MontoLocal;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionTarjeta += (dato.MontoLocal * banco.PorcentajeRetencion / 100);
                                cierre.ComisionTarjeta += (dato.MontoLocal * banco.PorcentajeComision / 100);
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovCxC,
                                    Tipo = 12,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a cuenta por cobrar: " + dato.Referencia,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.PagosCxCBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovCxC,
                                    Tipo = 13,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a cuenta por cobrar: " + dato.Referencia,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var ingresosEfectivo = dbContext.IngresoRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (ingresosEfectivo.Count > 0)
                    {
                        foreach (var dato in ingresosEfectivo)
                        {
                            cierre.IngresosEfectivo += dato.Monto;
                            listaMovimientos.Add(new DetalleMovimientoCierreCaja
                            {
                                IdReferencia = dato.IdIngreso,
                                Tipo = 14,
                                Fecha = dato.Fecha,
                                Descripcion = "Ingreso de efectivo: " + dato.Detalle,
                                Total = dato.Monto
                            });
                        }
                    }

                    //var movDevolucionesProveedores = dbContext.DevolucionProveedorRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).Join(dbContext.DesglosePagoDevolucionProveedorRepository, x => x.IdDevolucion, y => y.IdDevolucion, (x, y) => new { x, y })
                    //    .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    //foreach (var dato in movDevolucionesProveedores)
                    //{
                    //    if (dato.Key == StaticFormaPago.Efectivo)
                    //        cierre.DevolucionesProveedores = dato.Total;
                    //}

                    var compras = dbContext.CompraRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCondicionVenta != StaticCondicionVenta.Credito && !x.Procesado).ToList();
                    if (compras.Count > 0)
                    {
                        var pagosCompras = compras.Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                            .Select(y => new { y.x.IdCompra, y.x.Fecha, y.x.NombreProveedor, y.y.IdFormaPago, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosCompras)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.ComprasEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdCompra,
                                    Tipo = 15,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Compra de mercadería: " + dato.NombreProveedor,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.ComprasBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdCompra,
                                    Tipo = 16,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Compra de mercadería: " + dato.NombreProveedor,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var comprasCredito = dbContext.CompraRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCondicionVenta == StaticCondicionVenta.Credito && !x.Procesado).ToList();
                    if (comprasCredito.Count > 0)
                    {
                        foreach (var dato in comprasCredito)
                        {
                            cierre.ComprasCredito += dato.Total * dato.TipoDeCambioDolar;
                            listaMovimientos.Add(new DetalleMovimientoCierreCaja
                            {
                                IdReferencia = dato.IdCompra,
                                Tipo = 17,
                                Fecha = dato.Fecha,
                                Descripcion = "Compra de mercadería: " + dato.NombreProveedor,
                                Total = dato.Total * dato.TipoDeCambioDolar
                            });
                        }
                    }

                    var movimientosCxP = dbContext.MovimientoCuentaPorPagarRepository.Include("CuentaPorPagar").Where(x => x.Tipo == 1 && x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (movimientosCxP.Count > 0)
                    {
                        var pagosCxP = movimientosCxP.Join(dbContext.DesglosePagoMovimientoCuentaPorPagarRepository, x => x.IdMovCxP, y => y.IdMovCxP, (x, y) => new { x, y })
                            .Select(y => new { y.x.IdMovCxP, y.x.Fecha, y.x.CuentaPorPagar.Referencia, y.y.IdFormaPago, MontoLocal = y.y.MontoLocal * y.y.TipoDeCambio });
                        foreach (var dato in pagosCxP)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                cierre.PagosCxPEfectivo += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovCxP,
                                    Tipo = 18,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a cuenta por pagar: " + dato.Referencia,
                                    Total = dato.MontoLocal
                                });
                            }
                            else
                            {
                                cierre.PagosCxPBancos += dato.MontoLocal;
                                listaMovimientos.Add(new DetalleMovimientoCierreCaja
                                {
                                    IdReferencia = dato.IdMovCxP,
                                    Tipo = 19,
                                    Fecha = dato.Fecha,
                                    Descripcion = "Abono a cuenta por pagar: " + dato.Referencia,
                                    Total = dato.MontoLocal
                                });
                            }
                        }
                    }

                    var egresosEfectivo = dbContext.EgresoRepository.Where(x => x.Nulo == false && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Procesado).ToList();
                    if (egresosEfectivo.Count > 0)
                    {
                        foreach (var dato in egresosEfectivo)
                        {
                            cierre.EgresosEfectivo += dato.Monto;
                            listaMovimientos.Add(new DetalleMovimientoCierreCaja
                            {
                                IdReferencia = dato.IdEgreso,
                                Tipo = 20,
                                Fecha = dato.Fecha,
                                Descripcion = "Egreso de efectivo: " + dato.Detalle,
                                Total = dato.Monto
                            });
                        }
                    }
                    cierre.DetalleMovimientoCierreCaja = listaMovimientos;
                    return cierre;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    sucursal.CierreEnEjecucion = false;
                    dbContext.Commit();
                    if (_logger != null) _logger.LogError("Error al general el cierre diario: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error generando la información del cierre diario. Por favor consulte con su proveedor.");
                }
            }
        }

        public string GuardarDatosCierreCaja(CierreCaja cierre)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                using (var dbContextTransaction = dbContext.GetDatabaseTransaction())
                {
                    try
                    {
                        if (cierre.IdCierre > 0) throw new BusinessException("La información del cierre ya fue registrada. No es posible continuar con la transacción.");
                        SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == cierre.IdEmpresa && x.IdSucursal == cierre.IdSucursal);
                        if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        object[] objParameters = new object[2];
                        objParameters.SetValue(cierre.IdEmpresa, 0);
                        objParameters.SetValue(cierre.IdSucursal, 1);
                        dbContext.ExecuteProcedure("MarcaRegistrosProcesados", objParameters);
                        dbContext.CierreCajaRepository.Add(cierre);
                        sucursal.CierreEnEjecucion = false;
                        dbContext.Commit();
                        dbContextTransaction.Commit();
                        return cierre.IdCierre.ToString();
                    }
                    catch (BusinessException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        if (_logger != null) _logger.LogError("Error al actualizar el cierre diario: ", ex);
                        if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                        else throw new Exception("Se produjo un error actualizando la información del cierre diario. Por favor consulte con su proveedor.");
                    }
                }
            }
        }

        public void AbortarCierreCaja(int intIdEmpresa, int intIdSucursal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    sucursal.CierreEnEjecucion = false;
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al finalizar el cierre diario: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error finalizando el proceso de cierre diario. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaCierreCaja(int intIdEmpresa, int intIdSucursal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CierreCajaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal).Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el total del listado de registros de cierres de efectivo: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el total del listado de cierres de efectivo. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCierreCaja(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listadoEgreso = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CierreCajaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    listado = listado.OrderByDescending(x => x.IdCierre).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCierre, value.FechaCierre.ToString());
                        listadoEgreso.Add(item);
                    }
                    return listadoEgreso;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cierres de efectivo procesados: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cierres de efectivo procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public CierreCaja ObtenerCierreCaja(int intIdCierreCaja)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CierreCaja cierre = dbContext.CierreCajaRepository.Include("DetalleMovimientoCierreCaja").Include("DetalleEfectivoCierreCaja").FirstOrDefault(x => x.IdCierre == intIdCierreCaja);
                    return cierre;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el registro de cierre de caja: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la información del cierre de caja. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}