using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICuentaPorProcesarService
    {
        CuentaPorCobrar ObtenerCuentaPorCobrar(int intIdCxC);
        IList<LlaveDescripcion> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdTipo, int intIdPropietario);
        IList<EfectivoDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdPropietario);
        void AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento);
        void AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario);
        MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMovimiento);
        int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo);
        CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP);
        IList<LlaveDescripcion> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdTipo, int intIdPropietario);
        IList<EfectivoDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdPropietario);
        void AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento);
        void AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario);
        MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMovimiento);
        int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo);
        IList<LlaveDescripcion> ObtenerListadoApartadosConSaldo(int intIdEmpresa);
        IList<EfectivoDetalle> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado);
        void AplicarMovimientoApartado(MovimientoApartado movimiento);
        void AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario);
        MovimientoApartado ObtenerMovimientoApartado(int intIdMovimiento);
        IList<LlaveDescripcion> ObtenerListadoOrdenesServicioConSaldo(int intIdEmpresa);
        IList<EfectivoDetalle> ObtenerListadoMovimientosOrdenServicio(int intIdEmpresa, int intIdSucursal, int intIdOrden);
        void AplicarMovimientoOrdenServicio(MovimientoOrdenServicio movimiento);
        void AnularMovimientoOrdenServicio(int intIdMovimiento, int intIdUsuario);
        MovimientoOrdenServicio ObtenerMovimientoOrdenServicio(int intIdMovimiento);
    }

    public class CuentaPorProcesarService : ICuentaPorProcesarService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CuentaPorProcesarService()
        {
        }

        public CuentaPorProcesarService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Cuentas por Cobrar. Por favor consulte con su proveedor.");
            }
        }

        public CuentaPorCobrar ObtenerCuentaPorCobrar(int intIdCxC)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                return dbContext.CuentaPorCobrarRepository.FirstOrDefault(x => x.IdCxC == intIdCxC);
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdTipo, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CuentaPorCobrarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Tipo == intIdTipo && x.IdPropietario == intIdPropietario && x.Nulo == false && x.Saldo > 0).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCxC, value.Descripcion);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas por cobrar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdPropietario == intIdPropietario).OrderByDescending(x => x.IdMovCxC);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovCxC, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de movimientos de cuentas por cobrar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento)
        {
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                        cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorCobrarTarjetaParam == null || ivaPorPagarParam == null || gastoComisionParam == null || cuentaPorCobrarClientesParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorCobrarRepository.Add(movimiento);
                    foreach (DesgloseMovimientoCuentaPorCobrar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorCobrar)
                    {
                        CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(desgloseMovimiento.IdCxC);
                        if (cxc == null)
                            throw new Exception("La cuenta por cobrar asignada al movimiento no existe");
                        cxc.Saldo -= desgloseMovimiento.Monto;
                        dbContext.NotificarModificacion(cxc);
                    }
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorCobrar)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
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
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
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
                    log.Error("Error al aplicar el movimiento de una cuenta por cobrar: ", ex);
                    throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DesgloseMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(movimiento);
                    foreach (DesgloseMovimientoCuentaPorCobrar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorCobrar)
                    {
                        CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(desgloseMovimiento.IdCxC);
                        if (cxc == null)
                            throw new Exception("La cuenta por cobrar asignada al movimiento no existe");
                        cxc.Saldo += desgloseMovimiento.Monto;
                        dbContext.NotificarModificacion(cxc);
                    }
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, movimiento.IdAsiento);
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
                    log.Error("Error al anular el movimiento de una cuenta por cobrar: ", ex);
                    throw new Exception("Se produjo un error anulando el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMovimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DesgloseMovimientoCuentaPorCobrar.CuentaPorCobrar").Include("DesglosePagoMovimientoCuentaPorCobrar.FormaPago").FirstOrDefault(x => x.IdMovCxC == intIdMovimiento);
                foreach (var detalle in movimiento.DesgloseMovimientoCuentaPorCobrar)
                    detalle.CuentaPorCobrar = null;
                return movimiento;
            }
        }

        public int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false && DbFunctions.DiffDays(a.Fecha, DateTime.Now) > a.Plazo).Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cantidad de cuentas por cobrar vencidas de un cliente: ", ex);
                    throw new Exception("Se produjo un error consultando la cantidad de cuentas por cobrar vencidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaPorCobrarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el saldo total de cuentas por cobrar activas de un cliente: ", ex);
                    throw new Exception("Se produjo un error consultando el saldo acumulado de cuentas por cobrar vigentes. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                return dbContext.CuentaPorPagarRepository.FirstOrDefault(x => x.IdCxP == intIdCxP);
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdTipo, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Tipo == intIdTipo && x.IdPropietario == intIdPropietario && x.Nulo == false && x.Saldo > 0).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCxP, value.Descripcion);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas por pagar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorPagarRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdPropietario == intIdPropietario).OrderByDescending(x => x.IdMovCxP);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovCxP, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de movimientos de cuentas por pagar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento)
        {
            ParametroContable efectivoParam = null;
            ParametroContable cuentaPorPagarProveedoresParam = null;
            ParametroContable bancoParam = null;
            Asiento asiento = null;
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentaPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarProveedores).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorPagarProveedoresParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoCuentaPorPagarRepository.Add(movimiento);
                    foreach (DesgloseMovimientoCuentaPorPagar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorPagar)
                    {
                        CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(desgloseMovimiento.IdCxP);
                        if (cxp == null)
                            throw new Exception("La cuenta por Pagar asignada al movimiento no existe");
                        cxp.Saldo -= desgloseMovimiento.Monto;
                        dbContext.NotificarModificacion(cxp);
                    }
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoCuentaPorPagar)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Emisión de transferencia por abono a cuentas por pagar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = desglosePago.Beneficiario;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
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
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                if (bancoParam == null)
                                    throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = bancoParam.IdCuenta;
                                detalleAsiento.Credito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
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
                    log.Error("Error al aplicar el movimiento de una cuenta por pagar: ", ex);
                    throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por Pagar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(movimiento);
                    foreach (DesgloseMovimientoCuentaPorPagar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorPagar)
                    {
                        CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(desgloseMovimiento.IdCxP);
                        if (cxp == null)
                            throw new Exception("La cuenta por Pagar asignada al movimiento no existe");
                        cxp.Saldo += desgloseMovimiento.Monto;
                        dbContext.NotificarModificacion(cxp);
                    }
                    if (movimiento.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, movimiento.IdAsiento);
                    }
                    if (movimiento.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService();
                        servicioAuxiliarBancario.AnularMovimientoBanco(dbContext, movimiento.IdMovBanco, intIdUsuario);
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
                    log.Error("Error al anular el movimiento de una cuenta por pagar: ", ex);
                    throw new Exception("Se produjo un error anulando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMovimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar.CuentaPorPagar").Include("DesglosePagoMovimientoCuentaPorPagar.FormaPago").FirstOrDefault(x => x.IdMovCxP == intIdMovimiento);
                foreach (var detalle in movimiento.DesgloseMovimientoCuentaPorPagar)
                    detalle.CuentaPorPagar = null;
                return movimiento;
            }
        }

        public int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false && DbFunctions.DiffDays(a.Fecha, DateTime.Now) > a.Plazo).Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cantidad de cuentas por pagar vencidas de un proveedor: ", ex);
                    throw new Exception("Se produjo un error consultando la cantidad de cuentas por cobrar vencidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.Where(a => a.Tipo == intIdTipo && a.IdPropietario == intIdPropietario && a.Nulo == false).Sum(a => (decimal?)a.Saldo) ?? 0;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el saldo total de cuentas por pagar activas de un proveedor: ", ex);
                    throw new Exception("Se produjo un error al ejecutar la transacción. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoApartadosConSaldo(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ApartadoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Nulo == false && x.Aplicado == false && x.Excento + x.Gravado + x.Exonerado + x.Impuesto - x.Descuento - x.MontoAdelanto > 0).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdApartado, "Apartado " + value.IdApartado + " de " + value.NombreCliente);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de apartados pendientes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de apartados pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoApartadoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdApartado == intIdApartado).OrderByDescending(x => x.IdMovApartado);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovApartado, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de movimientos del apartado: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de movimientos del apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoApartado(MovimientoApartado movimiento)
        {
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoApartadoRepository.Add(movimiento);
                    Apartado apartado = dbContext.ApartadoRepository.Find(movimiento.IdApartado);
                    if (apartado == null)
                        throw new Exception("El registro de apartado asociado al movimiento no existe");
                    apartado.MontoAdelanto += movimiento.Monto;
                    dbContext.NotificarModificacion(apartado);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoApartado)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
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
                    log.Error("Error al aplicar el movimiento al registro de apartado: ", ex);
                    throw new Exception("Se produjo un error aplicando el movimiento al registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoApartado movimiento = dbContext.MovimientoApartadoRepository.FirstOrDefault(x => x.IdMovApartado == intIdMovimiento);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(movimiento);
                    Apartado apartado = dbContext.ApartadoRepository.Find(movimiento.IdApartado);
                    if (apartado == null)
                        throw new Exception("El registro de apartado asociado al movimiento no existe");
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
                    log.Error("Error al anular el movimiento del registro de apartado: ", ex);
                    throw new Exception("Se produjo un error anulando el movimiento del registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoApartado ObtenerMovimientoApartado(int intIdMovimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                MovimientoApartado movimiento = dbContext.MovimientoApartadoRepository.Include("Apartado").Include("DesglosePagoMovimientoApartado.FormaPago").FirstOrDefault(x => x.IdMovApartado == intIdMovimiento);
                foreach (var detalle in movimiento.DesglosePagoMovimientoApartado)
                    detalle.MovimientoApartado = null;
                return movimiento;
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoOrdenesServicioConSaldo(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCuentas = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.OrdenServicioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Nulo == false && x.Aplicado == false && x.Excento + x.Gravado + x.Exonerado + x.Impuesto - x.Descuento - x.MontoAdelanto > 0).OrderByDescending(x => x.Fecha);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdOrden, "Orden servicio " + value.IdOrden + " de " + value.NombreCliente);
                        listaCuentas.Add(item);
                    }
                    return listaCuentas;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de ordenes de servicio pendientes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de ordenes de servicio pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoMovimientosOrdenServicio(int intIdEmpresa, int intIdSucursal, int intIdOrden)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<EfectivoDetalle>();
                try
                {
                    var listado = dbContext.MovimientoOrdenServicioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdOrden == intIdOrden).OrderByDescending(x => x.IdMovOrden);
                    foreach (var value in listado)
                    {
                        EfectivoDetalle item = new EfectivoDetalle(value.IdMovOrden, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
                        listaMovimientos.Add(item);
                    }
                    return listaMovimientos;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de movimientos de la orden de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de movimientos de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AplicarMovimientoOrdenServicio(MovimientoOrdenServicio movimiento)
        {
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    movimiento.IdAsiento = 0;
                    movimiento.IdMovBanco = 0;
                    dbContext.MovimientoOrdenServicioRepository.Add(movimiento);
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(movimiento.IdOrden);
                    if (ordenServicio == null)
                        throw new Exception("El registro de orden de servicio asociado al movimiento no existe");
                    ordenServicio.MontoAdelanto += movimiento.Monto;
                    dbContext.NotificarModificacion(ordenServicio);
                    foreach (var desglosePago in movimiento.DesglosePagoMovimientoOrdenServicio)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por abono a cuentas por cobrar recibo nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
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
                    log.Error("Error al aplicar el movimiento a la orden de servicio: ", ex);
                    throw new Exception("Se produjo un error aplicando el movimiento a la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularMovimientoOrdenServicio(int intIdMovimiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoOrdenServicio movimiento = dbContext.MovimientoOrdenServicioRepository.FirstOrDefault(x => x.IdMovOrden == intIdMovimiento);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == movimiento.IdEmpresa && x.IdSucursal == movimiento.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (movimiento.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(movimiento);
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(movimiento.IdOrden);
                    if (ordenServicio == null)
                        throw new Exception("El registro de apartado asociado al movimiento no existe");
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
                    log.Error("Error al anular el movimiento del registro de apartado: ", ex);
                    throw new Exception("Se produjo un error anulando el movimiento del registro de apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public MovimientoOrdenServicio ObtenerMovimientoOrdenServicio(int intIdMovimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                MovimientoOrdenServicio movimiento = dbContext.MovimientoOrdenServicioRepository.Include("OrdenServicio").Include("DesglosePagoMovimientoOrdenServicio.FormaPago").FirstOrDefault(x => x.IdMovOrden == intIdMovimiento);
                foreach (var detalle in movimiento.DesglosePagoMovimientoOrdenServicio)
                    detalle.MovimientoOrdenServicio = null;
                return movimiento;
            }
        }
    }
}