using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.Parametros;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;
using System.Globalization;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICuentaPorProcesarService
    {
        CuentaPorCobrar ObtenerCuentaPorCobrar(int intIdCxC);
        int ObtenerTotalListaCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, string strReferencia, string strNombrePropietario);
        List<CuentaPorProcesar> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario);
        int ObtenerTotalListaMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdMov, string strFechaFinal);
        List<IdFechaDescripcion> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdMov, string strFechaFinal);
        string AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento);
        void AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMovimiento);
        int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo);
        CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP);
        int ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, string strReferencia, string strNombrePropietario);
        List<CuentaPorProcesar> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario);
        int ObtenerTotalListaMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdMov, string strFechaFinal);
        List<IdFechaDescripcion> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdMov, string strFechaFinal);
        string AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento);
        void AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMovimiento);
        int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo);
        List<EfectivoDetalle> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado);
        void AplicarMovimientoApartado(MovimientoApartado movimiento);
        void AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoApartado ObtenerMovimientoApartado(int intIdMovimiento);
        List<EfectivoDetalle> ObtenerListadoMovimientosOrdenServicio(int intIdEmpresa, int intIdSucursal, int intIdOrden);
        void AplicarMovimientoOrdenServicio(MovimientoOrdenServicio movimiento);
        void AnularMovimientoOrdenServicio(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoOrdenServicio ObtenerMovimientoOrdenServicio(int intIdMovimiento);
    }

    public class CuentaPorProcesarService : ICuentaPorProcesarService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory? _serviceScopeFactory;
        private static IConfiguracionGeneral? _config;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public CuentaPorProcesarService(ILoggerManager logger, IServiceScopeFactory pServiceScopeFactory, IConfiguracionGeneral config)
        {
            try
            {
                _logger = logger;
                _serviceScopeFactory = pServiceScopeFactory;
                _config = config;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al inicializar el servicio: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error al inicializar el servicio de Cuentas por Cobrar. Por favor consulte con su proveedor.");
            }
        }

        public CuentaPorCobrar ObtenerCuentaPorCobrar(int intIdCxC)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorCobrarRepository.FirstOrDefault(x => x.IdCxC == intIdCxC);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al inicializar el servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener la Cuenta por Cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.CuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Nulo == false);
                    if (bolPendientes)
                        listado = listado.Where(x => x.Saldo > 0);
                    if (strReferencia != "")
                        listado = listado.Where(x => x.Referencia.Contains(strReferencia));
                    if (strNombrePropietario != "")
                    {
                        int[] intIdPropietario = dbContext.ClienteRepository.Where(x => x.Nombre.Contains(strNombrePropietario)).Select(x => x.IdCliente).ToArray();
                        listado = listado.Where(x => intIdPropietario.Contains(x.IdPropietario));
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<CuentaPorProcesar> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<CuentaPorProcesar>();
                try
                {
                    var listado = dbContext.CuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Nulo == false);
                    if (bolPendientes)
                        listado = listado.Where(x => x.Saldo > 0);
                    if (strReferencia != "")
                        listado = listado.Where(x => x.Referencia.Contains(strReferencia));
                    if (strNombrePropietario != "")
                    {
                        int[] intIdPropietario = dbContext.ClienteRepository.Where(x => x.Nombre.Contains(strNombrePropietario)).Select(x => x.IdCliente).ToArray();
                        listado = listado.Where(x => intIdPropietario.Contains(x.IdPropietario));
                    }
                    var lista = listado.OrderBy(x => x.Fecha).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    foreach (var value in lista)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdCliente == value.IdPropietario).FirstOrDefault();
                        if (cliente == null) throw new BusinessException("El propietario con Id " + value.IdPropietario + " no existe registrado en la empresa que envía la petición.");
                        CuentaPorProcesar item = new CuentaPorProcesar(value.IdCxC, value.Fecha.ToString("dd/MM/yyyy"), cliente.Nombre, value.Referencia, value.Total, value.Saldo);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdMov, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdMov > 0) listado = listado.Where(x => x.IdMovCxC == intIdMov);
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<IdFechaDescripcion> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdMov, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<IdFechaDescripcion>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdMov > 0) listado = listado.Where(x => x.IdMovCxC == intIdMov);
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    listado = listado.OrderByDescending(x => x.IdMovCxC).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        IdFechaDescripcion item = new IdFechaDescripcion(value.IdMovCxC, value.Fecha.ToString("dd/MM/yyyy"), value.Observaciones);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                ParametroContable efectivoPorLiquidarParam = null;
                ParametroContable tarjetasPorLiquidarParam = null;
                ParametroContable ivaPorPagarParam = null;
                ParametroContable gastoComisionParam = null;
                ParametroContable cuentaPorCobrarClientesParam = null;
                ParametroContable bancoParam = null;
                Asiento asiento = null;
                MovimientoBanco movimientoBanco = null;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.TipoContrato < StaticTipoContrato.PlanEmpresarial2) throw new BusinessException("El plan de servicios contratado no permite registrar la transacción. Por favor consulte con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoPorLiquidarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("Efectivo")).FirstOrDefault();
                        tarjetasPorLiquidarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("CuentasPorCobrarTarjeta")).FirstOrDefault();
                        cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("CuentasPorCobrarClientes")).FirstOrDefault();
                        if (efectivoPorLiquidarParam == null || tarjetasPorLiquidarParam == null || ivaPorPagarParam == null || gastoComisionParam == null || cuentaPorCobrarClientesParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorCobrarRepository.Add(movimiento);
                    foreach (var detalle in movimiento.DetalleMovimientoCuentaPorCobrar)
                    {
                        CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(detalle.IdCxC);
                        if (cxc == null) throw new BusinessException("La cuenta por cobrar asignada al movimiento no existe");
                        cxc.Saldo -= detalle.Monto;
                        dbContext.NotificarModificacion(cxc);
                    }
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorCobrar)
                    {
                        if (!new int[] { StaticFormaPago.Efectivo, StaticFormaPago.Tarjeta }.Contains(desglosePago.IdFormaPago))
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdReferencia);
                            if (cuentaBanco == null)
                                throw new BusinessException("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = movimiento.IdUsuario;
                            movimientoBanco.Fecha = movimiento.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeEntrante;
                                movimientoBanco.Descripcion = "Recepción de cheque bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoEntrante;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    if (empresa.Contabiliza)
                    {
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = movimiento.IdEmpresa,
                            Fecha = movimiento.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de abono a cuenta por cobrar recibo nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        decimal decTotalMovimiento = 0;
                        foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorCobrar)
                        {
                            decTotalMovimiento += desglosePago.MontoLocal;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento++,
                                    IdCuenta = efectivoPorLiquidarParam.IdCuenta,
                                    Debito = desglosePago.MontoLocal,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoPorLiquidarParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento++,
                                    IdCuenta = tarjetasPorLiquidarParam.IdCuenta,
                                    Debito = desglosePago.MontoLocal,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(tarjetasPorLiquidarParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            else
                            {
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("CuentaDeBancos") && x.IdProducto == desglosePago.IdReferencia).FirstOrDefault();
                                if (bancoParam == null)
                                    throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdReferencia + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento++,
                                    IdCuenta = bancoParam.IdCuenta,
                                    Debito = desglosePago.MontoLocal,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                        }
                        detalleAsiento = new DetalleAsiento
                        {
                            Linea = intLineaDetalleAsiento++,
                            IdCuenta = cuentaPorCobrarClientesParam.IdCuenta,
                            Credito = decTotalMovimiento,
                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentaPorCobrarClientesParam.IdCuenta).SaldoActual
                        };
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        movimiento.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(movimiento);
                        asiento.Detalle += movimiento.IdMovCxC;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        movimientoBanco.Descripcion += movimiento.IdMovCxC;
                        dbContext.NotificarModificacion(movimientoBanco);
                        movimiento.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(movimiento);
                    }
                    dbContext.Commit();
                    return movimiento.IdMovCxC.ToString();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al aplicar el movimiento de una cuenta por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DetalleMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
                    if (movimiento == null) throw new BusinessException("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    movimiento.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(movimiento);
                    foreach (var detalle in movimiento.DetalleMovimientoCuentaPorCobrar)
                    {
                        CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(detalle.IdCxC);
                        if (cxc == null) throw new BusinessException("La cuenta por cobrar asignada al movimiento no existe");
                        cxc.Saldo += detalle.Monto;
                        dbContext.NotificarModificacion(cxc);
                    }
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(movimiento.IdAsiento, dbContext);
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
                    if (_logger != null) _logger.LogError("Error al anular el movimiento de una cuenta por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMovimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DetalleMovimientoCuentaPorCobrar.CuentaPorCobrar").Include("DesglosePagoMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
                    return movimiento;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al obtener el movimiento de una cuenta por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.IdPropietario == intIdPropietario && a.Nulo == false && EF.Functions.DateDiffDay(a.Fecha, Validador.ObtenerFechaHoraCostaRica()) > a.Plazo).Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cantidad de cuentas por cobrar vencidas de un cliente: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cantidad de cuentas por cobrar vencidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el saldo total de cuentas por cobrar activas de un cliente: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el saldo acumulado de cuentas por cobrar vigentes. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.FirstOrDefault(x => x.IdCxP == intIdCxP);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cuenta por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Nulo == false);
                    if (bolPendientes)
                        listado = listado.Where(x => x.Saldo > 0);
                    if (strReferencia != "")
                        listado = listado.Where(x => x.Referencia.Contains(strReferencia));
                    if (strNombrePropietario != "")
                    {
                        int[] intIdPropietario = dbContext.ProveedorRepository.Where(x => x.Nombre.Contains(strNombrePropietario)).Select(x => x.IdProveedor).ToArray();
                        listado = listado.Where(x => intIdPropietario.Contains(x.IdPropietario));
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<CuentaPorProcesar> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdSucursal, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<CuentaPorProcesar>();
                try
                {
                    var listado = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (bolPendientes)
                        listado = listado.Where(x => x.Saldo > 0);
                    if (strReferencia != "")
                        listado = listado.Where(x => x.Referencia.Contains(strReferencia));
                    if (strNombrePropietario != "")
                    {
                        int[] intIdPropietario = dbContext.ProveedorRepository.Where(x => x.Nombre.Contains(strNombrePropietario)).Select(x => x.IdProveedor).ToArray();
                        listado = listado.Where(x => intIdPropietario.Contains(x.IdPropietario));
                    }
                    var lista = listado.OrderBy(x => x.Fecha).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    foreach (var value in lista)
                    {
                        Proveedor proveedor = dbContext.ProveedorRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdProveedor == value.IdPropietario).FirstOrDefault();
                        if (proveedor == null) throw new BusinessException("El propietario con Id " + value.IdPropietario + " no existe registrado en la empresa que envía la petición.");
                        CuentaPorProcesar item = new CuentaPorProcesar(value.IdCxP, value.Fecha.ToString("dd/MM/yyyy"), proveedor.Nombre, value.Referencia, value.Total, value.Saldo);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (BusinessException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdMov, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdMov > 0) listado = listado.Where(x => x.IdMovCxP == intIdMov);
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos de cuentas por pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<IdFechaDescripcion> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdMov, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<IdFechaDescripcion>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && !x.Nulo);
                    if (intIdMov > 0) listado = listado.Where(x => x.IdMovCxP == intIdMov);
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    listado = listado.OrderByDescending(x => x.IdMovCxP).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        IdFechaDescripcion item = new IdFechaDescripcion(value.IdMovCxP, value.Fecha.ToString("dd/MM/yyyy"), value.Observaciones);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos de cuentas por pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                ParametroContable efectivoPorLiquidarParam = null;
                ParametroContable cuentaPorPagarProveedoresParam = null;
                ParametroContable bancoParam = null;
                Asiento asiento = null;
                MovimientoBanco movimientoBanco = null;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.TipoContrato < StaticTipoContrato.PlanEmpresarial2) throw new BusinessException("El plan de servicios contratado no permite registrar la transacción. Por favor consulte con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoPorLiquidarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("Efectivo")).FirstOrDefault();
                        cuentaPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("CuentasPorPagarProveedores")).FirstOrDefault();
                        if (efectivoPorLiquidarParam == null || cuentaPorPagarProveedoresParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorPagarRepository.Add(movimiento);
                    foreach (var detalle in movimiento.DetalleMovimientoCuentaPorPagar)
                    {
                        CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(detalle.IdCxP);
                        if (cxp == null) throw new BusinessException("La cuenta por pagar asignada al movimiento no existe");
                        cxp.Saldo -= detalle.Monto;
                        dbContext.NotificarModificacion(cxp);
                    }
                    decimal decPagoTotal = 0;
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorPagar)
                    {
                        decPagoTotal += desglosePago.MontoLocal;
                        if (desglosePago.IdFormaPago != StaticFormaPago.Efectivo)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdReferencia);
                            if (cuentaBanco == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = movimiento.IdUsuario;
                            movimientoBanco.Fecha = movimiento.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeSaliente;
                                movimientoBanco.Descripcion = "Emisión de cheque bancario por abono a cuentas por pagar recibo nro. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoSaliente;
                                movimientoBanco.Descripcion = "Emisión de transferencia por abono a cuentas por pagar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = "";
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    if (empresa.Contabiliza)
                    {
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = movimiento.IdEmpresa,
                            Fecha = movimiento.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Movimiento de abono a cuenta por pagar recibo nro. "
                        };
                        DetalleAsiento detalleAsiento = new DetalleAsiento
                        {
                            Linea = intLineaDetalleAsiento++,
                            IdCuenta = cuentaPorPagarProveedoresParam.IdCuenta,
                            Debito = decPagoTotal,
                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentaPorPagarProveedoresParam.IdCuenta).SaldoActual
                        };
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorPagar)
                        {
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento++,
                                    IdCuenta = efectivoPorLiquidarParam.IdCuenta,
                                    Credito = desglosePago.MontoLocal,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoPorLiquidarParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            else
                            {

                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == TipoParametroContableClase.ObtenerId("CuentaDeBancos") && x.IdProducto == desglosePago.IdReferencia).FirstOrDefault();
                                if (bancoParam == null)
                                    throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdReferencia + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento++,
                                    IdCuenta = bancoParam.IdCuenta,
                                    Credito = desglosePago.MontoLocal,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        movimiento.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(movimiento);
                        asiento.Detalle += movimiento.IdMovCxP;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        movimiento.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(movimiento);
                        movimientoBanco.Descripcion += movimiento.IdMovCxP;
                        dbContext.NotificarModificacion(movimientoBanco);
                    }
                    dbContext.Commit();
                    return movimiento.IdMovCxP.ToString();
                }
                catch (BusinessException)
                {
                    dbContext.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al aplicar el movimiento de una cuenta por pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DetalleMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                    if (movimiento == null) throw new BusinessException("El movimiento de cuenta por pagar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    movimiento.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(movimiento);
                    foreach (var detalle in movimiento.DetalleMovimientoCuentaPorPagar)
                    {
                        CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(detalle.IdCxP);
                        if (cxp == null) throw new BusinessException("La cuenta por pagar asignada al movimiento no existe");
                        cxp.Saldo += detalle.Monto;
                        dbContext.NotificarModificacion(cxp);
                    }
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(movimiento.IdAsiento, dbContext);
                    }
                    if (movimiento.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                        servicioAuxiliarBancario.AnularMovimientoBanco(movimiento.IdMovBanco, intIdUsuario, "Anulación de registro de movimiento CxP " + movimiento.IdMovCxP, dbContext);
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
                    if (_logger != null) _logger.LogError("Error al anular el movimiento de una cuenta por pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMovimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DetalleMovimientoCuentaPorPagar.CuentaPorPagar").Include("DesglosePagoMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                    return movimiento;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al obtener el movimiento de una cuenta por pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.Where(a => a.IdPropietario == intIdPropietario && a.Nulo == false && EF.Functions.DateDiffDay(a.Fecha, Validador.ObtenerFechaHoraCostaRica()) > a.Plazo).Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cantidad de cuentas por pagar vencidas de un proveedor: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cantidad de cuentas por cobrar vencidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.Where(a => a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el saldo total de cuentas por pagar activas de un proveedor: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar la transacción. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<EfectivoDetalle> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoApartadoRepository.Include("Apartado").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdApartado == intIdApartado && !x.Nulo).OrderByDescending(x => x.IdMovApartado);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovApartado, value.Fecha.ToString("dd/MM/yyyy"), "Abono sobre apartado nro " + +value.Apartado.ConsecApartado, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos del apartado: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos del apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoApartado(MovimientoApartado movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                MovimientoBanco movimientoBanco = null;
                try
                {
                    movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.TipoContrato < StaticTipoContrato.PlanEmpresarial2) throw new BusinessException("El plan de servicios contratado no permite registrar la transacción. Por favor consulte con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoApartadoRepository.Add(movimiento);
                    Apartado apartado = dbContext.ApartadoRepository.Find(movimiento.IdApartado);
                    if (apartado == null) throw new BusinessException("El registro de apartado asociado al movimiento no existe");
                    apartado.MontoAdelanto += movimiento.Monto;
                    dbContext.NotificarModificacion(apartado);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoApartado)
                    {
                        if (!new int[] { StaticFormaPago.Efectivo, StaticFormaPago.Tarjeta }.Contains(desglosePago.IdFormaPago))
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdReferencia);
                            if (cuentaBanco == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = movimiento.IdUsuario;
                            movimientoBanco.Fecha = movimiento.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeEntrante;
                                movimientoBanco.Descripcion = "Recepción de cheque bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoEntrante;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    dbContext.Commit();
                    if (movimientoBanco != null)
                    {
                        movimientoBanco.Descripcion += movimiento.IdMovApartado;
                        dbContext.NotificarModificacion(movimientoBanco);
                        movimiento.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(movimiento);
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
                    if (_logger != null) _logger.LogError("Error al aplicar el movimiento al registro de apartado: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error aplicando el movimiento al registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoApartado movimiento = dbContext.MovimientoApartadoRepository.FirstOrDefault(x => x.IdMovApartado == intIdMovimiento);
                    if (movimiento == null) throw new BusinessException("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    movimiento.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(movimiento);
                    Apartado apartado = dbContext.ApartadoRepository.Find(movimiento.IdApartado);
                    if (apartado == null) throw new BusinessException("El registro de apartado asociado al movimiento no existe");
                    apartado.MontoAdelanto -= movimiento.Monto;
                    dbContext.NotificarModificacion(apartado);
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
                    if (_logger != null) _logger.LogError("Error al anular el movimiento del registro de apartado: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el movimiento del registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoApartado ObtenerMovimientoApartado(int intIdMovimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoApartado movimiento = dbContext.MovimientoApartadoRepository.Include("Apartado").Include("DesglosePagoMovimientoApartado").FirstOrDefault(x => x.IdMovApartado == intIdMovimiento);
                    return movimiento;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al obtener el movimiento del registro de apartado: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener el movimiento del registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<EfectivoDetalle> ObtenerListadoMovimientosOrdenServicio(int intIdEmpresa, int intIdSucursal, int intIdOrden)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoOrdenServicioRepository.Include("OrdenServicio").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdOrden == intIdOrden && !x.Nulo).OrderByDescending(x => x.IdMovOrden);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovOrden, value.Fecha.ToString("dd/MM/yyyy"), "Abono sobre orden de servicio nro " + +value.OrdenServicio.ConsecOrdenServicio, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos de la orden de servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoOrdenServicio(MovimientoOrdenServicio movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                MovimientoBanco movimientoBanco = null;
                try
                {
                    movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.TipoContrato < StaticTipoContrato.PlanEmpresarial2) throw new BusinessException("El plan de servicios contratado no permite registrar la transacción. Por favor consulte con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoOrdenServicioRepository.Add(movimiento);
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(movimiento.IdOrden);
                    if (ordenServicio == null) throw new BusinessException("El registro de orden de servicio asociado al movimiento no existe");
                    ordenServicio.MontoAdelanto += movimiento.Monto;
                    dbContext.NotificarModificacion(ordenServicio);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoOrdenServicio)
                    {
                        if (!new int[] { StaticFormaPago.Efectivo, StaticFormaPago.Tarjeta }.Contains(desglosePago.IdFormaPago))
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdReferencia);
                            if (cuentaBanco == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = movimiento.IdUsuario;
                            movimientoBanco.Fecha = movimiento.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeEntrante;
                                movimientoBanco.Descripcion = "Recepción de cheque bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoEntrante;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    dbContext.Commit();
                    if (movimientoBanco != null)
                    {
                        movimientoBanco.Descripcion += movimiento.IdMovOrden;
                        dbContext.NotificarModificacion(movimientoBanco);
                        movimiento.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(movimiento);
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
                    if (_logger != null) _logger.LogError("Error al aplicar el movimiento a la orden de servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error aplicando el movimiento a la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoOrdenServicio(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoOrdenServicio movimiento = dbContext.MovimientoOrdenServicioRepository.FirstOrDefault(x => x.IdMovOrden == intIdMovimiento);
                    if (movimiento == null) throw new BusinessException("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    movimiento.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(movimiento);
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(movimiento.IdOrden);
                    if (ordenServicio == null) throw new BusinessException("El registro de apartado asociado al movimiento no existe");
                    ordenServicio.MontoAdelanto -= movimiento.Monto;
                    dbContext.NotificarModificacion(ordenServicio);
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
                    if (_logger != null) _logger.LogError("Error al anular el movimiento del registro de orden de servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error anulando el movimiento del registro de orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoOrdenServicio ObtenerMovimientoOrdenServicio(int intIdMovimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    MovimientoOrdenServicio movimiento = dbContext.MovimientoOrdenServicioRepository.Include("OrdenServicio").Include("DesglosePagoMovimientoOrdenServicio").FirstOrDefault(x => x.IdMovOrden == intIdMovimiento);
                    return movimiento;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al obtener el movimiento del registro de orden de servicio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al obtener el movimiento del registro de orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}