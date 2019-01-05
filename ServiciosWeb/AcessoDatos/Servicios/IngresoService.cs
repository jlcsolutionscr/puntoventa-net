using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using Unity;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IIngresoService
    {
        CuentaIngreso AgregarCuentaIngreso(CuentaIngreso cuenta);
        void ActualizarCuentaIngreso(CuentaIngreso cuenta);
        void EliminarCuentaIngreso(int intIdCuenta);
        CuentaIngreso ObtenerCuentaIngreso(int intIdCuenta);
        IEnumerable<CuentaIngreso> ObtenerListaCuentasIngreso(int intIdEmpresa, string strDescripcion = "");
        Ingreso AgregarIngreso(Ingreso ingreso);
        void ActualizarIngreso(Ingreso ingreso);
        void AnularIngreso(int intIdIngreso, int intIdUsuario);
        Ingreso ObtenerIngreso(int intIdIngreso);
        int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "");
        IEnumerable<Ingreso> ObtenerListaIngresos(int intIdEmpresa, int numPagina, int cantRec, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "");
    }

    class IngresoService : IIngresoService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IngresoService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de ingresos. Por favor consulte con su proveedor.");
            }
        }

        public CuentaIngreso AgregarCuentaIngreso(CuentaIngreso cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaIngresoRepository.Add(cuenta);
                    dbContext.Commit();
                    return cuenta;
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la cuenta de ingreso: ", ex);
                    throw new Exception("Se produjo un error agregando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaIngreso(CuentaIngreso cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al actualizar la cuenta de ingreso: ", ex);
                    throw new Exception("Se produjo un error actualizando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaIngreso(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaIngreso cuenta = dbContext.CuentaIngresoRepository.Find(intIdCuenta);
                    if (cuenta == null) throw new Exception("La cuenta de ingreso por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaIngresoRepository.Remove(cuenta);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar la cuenta de ingreso: ", ex);
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
                    log.Error("Error al eliminar la cuenta de ingreso: ", ex);
                    throw new Exception("Se produjo un error eliminando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaIngreso ObtenerCuentaIngreso(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaIngresoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cuenta de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando la cuenta de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CuentaIngreso> ObtenerListaCuentasIngreso(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCuentas = dbContext.CuentaIngresoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaCuentas = listaCuentas.Where(x => x.Descripcion.Contains(strDescripcion));
                    return listaCuentas.OrderBy(x => x.Descripcion).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas de ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public Ingreso AgregarIngreso(Ingreso ingreso)
        {
            decimal decTotalIngresosTarjeta = 0;
            decimal decTotalImpuestoRetenido = 0;
            decimal decTotalGastoComisionTarjeta = 0;
            ParametroContable efectivo = null;
            ParametroContable bancoParam = null;
            ParametroContable ingresoParam = null;
            ParametroContable cuentaPorCobrarTarjetaParam = null;
            ParametroContable gastoComisionParam = null;
            ParametroContable ivaPorPagarParam = null;
            Asiento asiento = null;
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        if (efectivo == null || cuentaPorCobrarTarjetaParam == null || gastoComisionParam == null || ivaPorPagarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    CuentaIngreso cuentaIngreso = dbContext.CuentaIngresoRepository.Find(ingreso.IdCuenta);
                    if (cuentaIngreso == null)
                        throw new Exception("La cuenta de ingreso asignada al registro no existe");
                    ingreso.IdAsiento = 0;
                    ingreso.IdMovBanco = 0;
                    dbContext.IngresoRepository.Add(ingreso);
                    foreach (var desglosePago in ingreso.DesglosePagoIngreso)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = ingreso.IdUsuario;
                            movimientoBanco.Fecha = ingreso.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeEntrante;
                                movimientoBanco.Descripcion = "Recepción de cheque bancario por registro de ingreso. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Recepción de depósito bancario por registro de ingreso. ";
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
                            IdEmpresa = ingreso.IdEmpresa,
                            Fecha = ingreso.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de ingreso nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        foreach (var desglosePago in ingreso.DesglosePagoIngreso)
                        {
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = efectivo.IdCuenta;
                                detalleAsiento.Debito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
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
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                BancoAdquiriente bancoAdquiriente = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                                decTotalGastoComisionTarjeta = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeComision / 100), 2, MidpointRounding.AwayFromZero);
                                decTotalImpuestoRetenido = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeRetencion / 100), 2, MidpointRounding.AwayFromZero);
                                decTotalIngresosTarjeta = Math.Round(desglosePago.MontoLocal - decTotalGastoComisionTarjeta - decTotalImpuestoRetenido, 2, MidpointRounding.AwayFromZero);
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = cuentaPorCobrarTarjetaParam.IdCuenta;
                                detalleAsiento.Debito = decTotalIngresosTarjeta;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
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
                        }
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        ingresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeIngresos && x.IdProducto == cuentaIngreso.IdCuenta).FirstOrDefault();
                        if (ingresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaIngreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = ingresoParam.IdCuenta;
                        detalleAsiento.Credito = ingreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        ingreso.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(ingreso);
                        asiento.Detalle += ingreso.IdIngreso;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        ingreso.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(ingreso);
                        movimientoBanco.Descripcion += ingreso.IdIngreso;
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
                    log.Error("Error al agregar el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error agregando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
            return ingreso;
        }

        public void ActualizarIngreso(Ingreso ingreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(ingreso);
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
                    log.Error("Error al actualizar el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularIngreso(int intIdIngreso, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Ingreso ingreso = dbContext.IngresoRepository.Find(intIdIngreso);
                    if (ingreso == null) throw new Exception("El ingreso por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (ingreso.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    ingreso.Nulo = true;
                    ingreso.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(ingreso);
                    if (ingreso.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, ingreso.IdAsiento);
                    }
                    if (ingreso.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService();
                        servicioAuxiliarBancario.AnularMovimientoBanco(dbContext, ingreso.IdMovBanco, intIdUsuario);
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

        public Ingreso ObtenerIngreso(int intIdIngreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.IngresoRepository.Include("DesglosePagoIngreso.FormaPago").Include("DesglosePagoIngreso.TipoMoneda").FirstOrDefault(x => x.IdIngreso == intIdIngreso);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaIngresos = dbContext.IngresoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
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
                    log.Error("Error al obtener el total del listado de registros de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Ingreso> ObtenerListaIngresos(int intIdEmpresa, int numPagina, int cantRec, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaIngresos = dbContext.IngresoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdIngreso > 0)
                        listaIngresos = listaIngresos.Where(x => x.IdIngreso == intIdIngreso);
                    else
                    {
                        if (!strRecibidoDe.Equals(string.Empty))
                            listaIngresos = listaIngresos.Where(x => x.RecibidoDe.Contains(strRecibidoDe));
                        if (!strDetalle.Equals(string.Empty))
                            listaIngresos = listaIngresos.Where(x => x.Detalle.Contains(strDetalle));
                    }
                    return listaIngresos.OrderByDescending(x => x.IdIngreso).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}