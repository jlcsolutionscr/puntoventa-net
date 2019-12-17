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
        IList<CuentaDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdPropietario);
        void AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento);
        void AnularMovimientoCxC(int intIdMov, int intIdUsuario);
        MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMov);
        int ObtenerCantidadCxCVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorCobrar(int intIdPropietario, int intIdTipo);
        CuentaPorPagar AgregarCuentaPorPagar(CuentaPorPagar cuenta);
        void AnularCuentaPorPagar(int intIdCuentaPorPagar, int intIdUsuario);
        CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP);
        IList<LlaveDescripcion> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdTipo, int intIdPropietario);
        IList<CuentaDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdPropietario);
        void AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento);
        void AnularMovimientoCxP(int intIdMov, int intIdUsuario);
        MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMov);
        int ObtenerCantidadCxPVencidas(int intIdPropietario, int intIdTipo);
        decimal ObtenerSaldoCuentasPorPagar(int intIdPropietario, int intIdTipo);
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
                try
                {
                    return dbContext.CuentaPorCobrarRepository.Find(intIdCxC);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de cuenta por cobrar: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la cuenta por cobrar. Por favor consulte con su proveedor.");
                }
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
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCxC, value.Referencia);
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

        public IList<CuentaDetalle> ObtenerListadoMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<CuentaDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdPropietario == intIdPropietario).OrderByDescending(x => x.IdMovCxC);
                    foreach (var value in listado)
                    {
                        CuentaDetalle item = new CuentaDetalle(value.IdMovCxC, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
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
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public void AnularMovimientoCxC(int intIdMov, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DesgloseMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == intIdMov);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por cobrar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public MovimientoCuentaPorCobrar ObtenerMovimientoCxC(int intIdMov)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                return dbContext.MovimientoCuentaPorCobrarRepository.Include("DesgloseMovimientoCuentaPorCobrar").Include("DesgloseMovimientoCuentaPorCobrar.CuentaPorCobrar").Include("DesglosePagoMovimientoCuentaPorCobrar").Include("DesglosePagoMovimientoCuentaPorCobrar.FormaPago").FirstOrDefault(x => x.IdMovCxC == intIdMov);
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

        public CuentaPorPagar AgregarCuentaPorPagar(CuentaPorPagar cuenta)
        {
            ParametroContable cuentasPorPagarParticularesParam = null;
            ParametroContable efectivo = null;
            Asiento asiento = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        cuentasPorPagarParticularesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarParticulares).FirstOrDefault();
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        if (efectivo == null || cuentasPorPagarParticularesParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    cuenta.IdAsiento = 0;
                    dbContext.CuentaPorPagarRepository.Add(cuenta);
                    if (empresa.Contabiliza)
                    {
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = cuenta.IdEmpresa,
                            Fecha = cuenta.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de cuenta por pagar a particular"
                        };
                        DetalleAsiento detalleAsiento = null;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = cuentasPorPagarParticularesParam.IdCuenta;
                        detalleAsiento.Credito = cuenta.Total;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    cuenta.NroDocOrig = cuenta.IdCxP;
                    dbContext.NotificarModificacion(cuenta);
                    if (asiento != null)
                    {
                        cuenta.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(cuenta);
                        asiento.Detalle = "Registro de cuenta por pagar a particular nro. " + cuenta.IdCxP;
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
                    log.Error("Error al agregar el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error agregando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
            return cuenta;
        }

        public void AnularCuentaPorPagar(int intIdCuentaPorPagar, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaPorPagar cuentaPorPagar = dbContext.CuentaPorPagarRepository.Find(intIdCuentaPorPagar);
                    if (cuentaPorPagar == null) throw new Exception("El ingreso por anular no existe");
                    if (cuentaPorPagar.Saldo > cuentaPorPagar.Total) throw new Exception("La cuenta por pagar ya posee movimientos de abono. No puede ser anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaPorPagar.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    cuentaPorPagar.Nulo = true;
                    cuentaPorPagar.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(cuentaPorPagar);
                    if (cuentaPorPagar.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, cuentaPorPagar.IdAsiento);
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
                    log.Error("Error al anular el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error anulando el ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaPorPagar ObtenerCuentaPorPagar(int intIdCxP)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaPorPagarRepository.Include("DesglosePagoCuentaPorPagar.FormaPago").Include("DesglosePagoCuentaPorPagar.TipoMoneda").FirstOrDefault(x => x.IdCxP == intIdCxP);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de cuenta por pagar: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la cuenta por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<CuentaDetalle> ObtenerListadoMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdPropietario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaMovimientos = new List<CuentaDetalle>();
                try
                {
                    var listado = dbContext.MovimientoCuentaPorPagarRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdPropietario == intIdPropietario).OrderByDescending(x => x.IdMovCxP);
                    foreach (var value in listado)
                    {
                        CuentaDetalle item = new CuentaDetalle(value.IdMovCxP, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
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

        public int ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intTipo, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCuentas = dbContext.CuentaPorPagarRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Tipo == intTipo && x.Nulo == false);
                    return listaCuentas.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de cuentas por pagar a particulares: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de cuentas por pagar a particulares. Por favor consulte con su proveedor.");
                }
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
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCxP, value.Referencia);
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
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public void AnularMovimientoCxP(int intIdMov, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == intIdMov);
                    if (movimiento == null) throw new Exception("El movimiento de cuenta por Pagar no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public MovimientoCuentaPorPagar ObtenerMovimientoCxP(int intIdMov)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                return dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar").Include("DesgloseMovimientoCuentaPorPagar.CuentaPorPagar").Include("DesglosePagoMovimientoCuentaPorPagar").Include("DesglosePagoMovimientoCuentaPorPagar.FormaPago").FirstOrDefault(x => x.IdMovCxP == intIdMov);
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
    }
}