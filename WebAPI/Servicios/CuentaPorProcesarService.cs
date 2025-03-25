using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICuentaPorProcesarService
    {
        CuentaPorCobrar ObtenerCuentaPorCobrar(int intIdCxC);
        int ObtenerTotalListaCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario);
        List<CuentaPorProcesar> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario);
        List<EfectivoDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdCuenta);
        void AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento);
        void AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMovimiento);
        int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo);
        CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP);
        int ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario);
        List<CuentaPorProcesar> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario);
        List<EfectivoDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdCuenta);
        void AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento);
        void AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMovimiento);
        int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo);
        List<LlaveDescripcion> ObtenerListadoApartadosConSaldo(int intIdEmpresa);
        List<EfectivoDetalle> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado);
        void AplicarMovimientoApartado(MovimientoApartado movimiento);
        void AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoApartado ObtenerMovimientoApartado(int intIdMovimiento);
        List<LlaveDescripcion> ObtenerListadoOrdenesServicioConSaldo(int intIdEmpresa);
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

        public int ObtenerTotalListaCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.CuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Tipo == intIdTipo && x.Nulo == false);
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

        public List<CuentaPorProcesar> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<CuentaPorProcesar>();
                try
                {
                    var listado = dbContext.CuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Tipo == intIdTipo && x.Nulo == false);
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<EfectivoDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorCobrarRepository.Include("CuentaPorCobrar").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCxC == intIdCuenta && !x.Nulo).OrderByDescending(x => x.IdMovCxC);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovCxC, value.Fecha.ToString("dd/MM/yyyy"), "Abono sobre cuenta por cobrar nro " + value.CuentaPorCobrar.Referencia, value.Monto);
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

        public void AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                decimal decTotalIngresosTarjeta = 0;
                decimal decTotalImpuestoRetenido = 0;
                decimal decTotalGastoComisionTarjeta = 0;
                ParametroContable efectivoParam = null;
                ParametroContable cuentaPorCobrarTarjetaParam = null;
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
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.GastoComisionTarjeta).FirstOrDefault();
                        cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarClientes).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorCobrarTarjetaParam == null || ivaPorPagarParam == null || gastoComisionParam == null || cuentaPorCobrarClientesParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorCobrarRepository.Add(movimiento);
                    CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(movimiento.IdCxC);
                    if (cxc == null)
                        throw new BusinessException("La cuenta por cobrar asignada al movimiento no existe");
                    cxc.Saldo -= movimiento.Monto;
                    dbContext.NotificarModificacion(cxc);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorCobrar)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
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
                        foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorCobrar)
                        {
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = efectivoParam.IdCuenta;
                                detalleAsiento.Debito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                BancoAdquiriente bancoAdquiriente = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                                decTotalIngresosTarjeta = Math.Round(desglosePago.MontoLocal / (1 + (bancoAdquiriente.PorcentajeRetencion + bancoAdquiriente.PorcentajeComision) / 100), 2, MidpointRounding.AwayFromZero);
                                decTotalImpuestoRetenido = Math.Round(decTotalIngresosTarjeta * (bancoAdquiriente.PorcentajeRetencion / 100), 2, MidpointRounding.AwayFromZero);
                                decTotalGastoComisionTarjeta = Math.Round(desglosePago.MontoLocal - decTotalIngresosTarjeta - decTotalImpuestoRetenido, 2, MidpointRounding.AwayFromZero);
                                if (decTotalIngresosTarjeta > 0)
                                {
                                    detalleAsiento = new DetalleAsiento();
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento.Linea = intLineaDetalleAsiento;
                                    detalleAsiento.IdCuenta = cuentaPorCobrarTarjetaParam.IdCuenta;
                                    detalleAsiento.Debito = decTotalIngresosTarjeta;
                                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                }
                                if (decTotalImpuestoRetenido > 0)
                                {
                                    detalleAsiento = new DetalleAsiento();
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento.Linea = intLineaDetalleAsiento;
                                    detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                                    detalleAsiento.Debito = decTotalImpuestoRetenido;
                                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                }
                                if (decTotalGastoComisionTarjeta > 0)
                                {
                                    detalleAsiento = new DetalleAsiento();
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento.Linea = intLineaDetalleAsiento;
                                    detalleAsiento.IdCuenta = gastoComisionParam.IdCuenta;
                                    detalleAsiento.Debito = decTotalGastoComisionTarjeta;
                                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                }
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                if (bancoParam == null)
                                    throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = bancoParam.IdCuenta;
                                detalleAsiento.Debito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                        }
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = cuentaPorCobrarClientesParam.IdCuenta;
                        detalleAsiento.Credito = movimiento.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
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
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
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
                    CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(movimiento.IdCxC);
                    if (cxc == null) throw new BusinessException("La cuenta por cobrar asignada al movimiento no existe");
                    cxc.Saldo += movimiento.Monto;
                    dbContext.NotificarModificacion(cxc);
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(movimiento.IdAsiento, dbContext);
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
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("CuentaPorCobrar").Include("DesglosePagoMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
                    movimiento.NombrePropietario = "Información no disponible";
                    Cliente cliente = dbContext.ClienteRepository.Find(movimiento.CuentaPorCobrar.IdPropietario);
                    if (cliente != null) movimiento.NombrePropietario = cliente.Nombre;
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
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false && EF.Functions.DateDiffDay(a.Fecha, Validador.ObtenerFechaHoraCostaRica()) > a.Plazo).Count();
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
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
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

        public int ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Tipo == intIdTipo && x.Nulo == false);
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

        public List<CuentaPorProcesar> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int numPagina, int cantRec, string strReferencia, string strNombrePropietario)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<CuentaPorProcesar>();
                try
                {
                    var listado = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Tipo == intIdTipo && !x.Nulo);
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
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas por cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<EfectivoDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorPagarRepository.Include("CuentaPorPagar").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdCxP == intIdCuenta && !x.Nulo).OrderByDescending(x => x.IdMovCxP);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovCxP, value.Fecha.ToString("dd/MM/yyyy"), "Abono con recibo " + value.Recibo + " sobre cuenta por pagar nro " + value.CuentaPorPagar.Referencia, value.Monto);
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

        public void AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                movimiento.Fecha = Validador.ObtenerFechaHoraCostaRica();
                ParametroContable efectivoParam = null;
                ParametroContable cuentaPorPagarProveedoresParam = null;
                ParametroContable bancoParam = null;
                Asiento asiento = null;
                MovimientoBanco movimientoBanco = null;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorPagarProveedores).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorPagarProveedoresParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorPagarRepository.Add(movimiento);
                    CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(movimiento.IdCxP);
                    if (cxp == null) throw new BusinessException("La cuenta por Pagar asignada al movimiento no existe");
                    cxp.Saldo -= movimiento.Monto;
                    dbContext.NotificarModificacion(cxp);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorPagar)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
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
                            movimientoBanco.Beneficiario = desglosePago.Beneficiario;
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
                            Detalle = "Movimiento de abono a cuenta por Pagar recibo nro. "
                        };
                        DetalleAsiento detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = cuentaPorPagarProveedoresParam.IdCuenta;
                        detalleAsiento.Debito = movimiento.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorPagar)
                        {
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = efectivoParam.IdCuenta;
                                detalleAsiento.Credito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                if (bancoParam == null)
                                    throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = bancoParam.IdCuenta;
                                detalleAsiento.Credito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
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
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                    if (movimiento == null) throw new BusinessException("El movimiento de cuenta por Pagar no existe");
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
                    CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(movimiento.IdCxP);
                    if (cxp == null) throw new BusinessException("La cuenta por Pagar asignada al movimiento no existe");
                    cxp.Saldo += movimiento.Monto;
                    dbContext.NotificarModificacion(cxp);
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger, _config);
                        servicioContabilidad.ReversarAsientoContable(movimiento.IdAsiento, dbContext);
                    }
                    if (movimiento.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService(_logger, _config);
                        servicioAuxiliarBancario.ReversarMovimientoBanco(movimiento.IdMovBanco, intIdUsuario, "Anulación de registro de movimiento CxP " + movimiento.IdMovCxP, dbContext);
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
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("CuentaPorPagar").Include("DesglosePagoMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                    movimiento.NombrePropietario = "Información no disponible";
                    Proveedor proveedor = dbContext.ProveedorRepository.Find(movimiento.CuentaPorPagar.IdPropietario);
                    if (proveedor != null) movimiento.NombrePropietario = proveedor.Nombre;
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
                    return dbContext.CuentaPorPagarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false && EF.Functions.DateDiffDay(a.Fecha, Validador.ObtenerFechaHoraCostaRica()) > a.Plazo).Count();
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
                    return dbContext.CuentaPorPagarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el saldo total de cuentas por pagar activas de un proveedor: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar la transacción. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoApartadosConSaldo(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ApartadoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Aplicado == false && x.Excento + x.Gravado + x.Exonerado + x.Impuesto - x.MontoAdelanto > 0 && !x.Nulo).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdApartado, "Apartado " + value.IdApartado + " de " + value.NombreCliente);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de apartados pendientes: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de apartados pendientes. Por favor consulte con su proveedor.");
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
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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

        public List<LlaveDescripcion> ObtenerListadoOrdenesServicioConSaldo(int intIdEmpresa)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.OrdenServicioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Aplicado == false && x.Excento + x.Gravado + x.Exonerado + x.Impuesto - x.MontoAdelanto > 0 && !x.Nulo).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdOrden, "Orden servicio " + value.IdOrden + " de " + value.NombreCliente);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de ordenes de servicio pendientes: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de ordenes de servicio pendientes. Por favor consulte con su proveedor.");
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
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = movimiento.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
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