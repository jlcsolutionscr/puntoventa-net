using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IFlujoCajaService
    {
        void AgregarCuentaIngreso(CuentaIngreso cuenta);
        void ActualizarCuentaIngreso(CuentaIngreso cuenta);
        void EliminarCuentaIngreso(int intIdCuenta);
        CuentaIngreso ObtenerCuentaIngreso(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasIngreso(int intIdEmpresa, string strDescripcion = "");
        string AgregarIngreso(Ingreso ingreso);
        void ActualizarIngreso(Ingreso ingreso);
        void AnularIngreso(int intIdIngreso, int intIdUsuario);
        Ingreso ObtenerIngreso(int intIdIngreso);
        int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdSucursal, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "");
        IList<FlujoEfectivoDetalle> ObtenerListadoIngresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "");
        void AgregarCuentaEgreso(CuentaEgreso cuenta);
        void ActualizarCuentaEgreso(CuentaEgreso cuenta);
        void EliminarCuentaEgreso(int intIdCuenta);
        CuentaEgreso ObtenerCuentaEgreso(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion);
        string AgregarEgreso(Egreso egreso);
        void ActualizarEgreso(Egreso egreso);
        void AnularEgreso(int intIdEgreso, int intIdUsuario);
        Egreso ObtenerEgreso(int intIdEgreso);
        int ObtenerTotalListaEgresos(int intIdEmpresa, int intIdSucursal, int intIdEgreso, string strBeneficiario, string strDetalle);
        IList<FlujoEfectivoDetalle> ObtenerListadoEgresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdEgreso, string strBeneficiario, string strDetalle);
    }

    public class FlujoCajaService : IFlujoCajaService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FlujoCajaService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Egresos. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarCuentaIngreso(CuentaIngreso cuenta)
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

        public IList<LlaveDescripcion> ObtenerListadoCuentasIngreso(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al obtener el listado de cuentas de ingresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarIngreso(Ingreso ingreso)
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
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                        ingresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeIngresos & x.IdProducto == cuentaIngreso.IdCuenta).FirstOrDefault();
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
            return ingreso.IdIngreso.ToString();
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
                    Ingreso ingreso = dbContext.IngresoRepository.Include("DesglosePagoIngreso.FormaPago").Include("DesglosePagoIngreso.TipoMoneda").FirstOrDefault(x => x.IdIngreso == intIdIngreso);
                    foreach (DesglosePagoIngreso desglosePago in ingreso.DesglosePagoIngreso)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                            desglosePago.DescripcionCuenta = banco.Descripcion;
                        }
                        else
                        {
                            CuentaBanco banco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            desglosePago.DescripcionCuenta = banco.Descripcion;
                        }
                        desglosePago.Ingreso = null;
                    }
                    return ingreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de ingreso: ", ex);
                    throw new Exception("Se produjo un error consultando la información del ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaIngresos(int intIdEmpresa, int intIdSucursal, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaIngresos = dbContext.IngresoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
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

        public IList<FlujoEfectivoDetalle> ObtenerListadoIngresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdIngreso = 0, string strRecibidoDe = "", string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listadoIngreso = new List<FlujoEfectivoDetalle>();
                try
                {
                    var listado = dbContext.IngresoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
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
                        FlujoEfectivoDetalle item = new FlujoEfectivoDetalle(value.IdIngreso, value.Fecha.ToString("dd/MM/yyyy"), value.Detalle, value.Monto);
                        listadoIngreso.Add(item);
                    }
                    return listadoIngreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarCuentaEgreso(CuentaEgreso cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al agregar la cuenta de egreso: ", ex);
                    throw new Exception("Se produjo un error agregando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaEgreso(CuentaEgreso cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa); ;
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
                    log.Error("Error al actualizar la cuenta de egreso: ", ex);
                    throw new Exception("Se produjo un error actualizando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaEgreso(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(intIdCuenta);
                    if (cuentaEgreso == null) throw new Exception("La cuenta de egreso por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaEgreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaEgresoRepository.Remove(cuentaEgreso);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar la cuenta de egreso: ", ex);
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
                    log.Error("Error al eliminar la cuenta de egreso: ", ex);
                    throw new Exception("Se produjo un error eliminando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public CuentaEgreso ObtenerCuentaEgreso(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaEgresoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cuenta de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando la cuenta de egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al obtener el listado de cuentas de egresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarEgreso(Egreso egreso)
        {
            ParametroContable efectivo = null;
            ParametroContable bancoParam = null;
            ParametroContable egresoParam = null;
            Asiento asiento = null;
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        if (efectivo == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(egreso.IdCuenta);
                    if (cuentaEgreso == null)
                        throw new Exception("La cuenta de egreso asignada al registro no existe");
                    egreso.IdAsiento = 0;
                    egreso.IdMovBanco = 0;
                    dbContext.EgresoRepository.Add(egreso);
                    foreach (var desglosePago in egreso.DesglosePagoEgreso)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario || desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = egreso.IdUsuario;
                            movimientoBanco.Fecha = egreso.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeSaliente;
                                movimientoBanco.Descripcion = "Emisión de cheque bancario por registro de pago egreso nro. ";
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Emisión de transferencia por registro pago de egreso nro. ";
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.NotaDebito;
                                movimientoBanco.Descripcion = "Nota de débito por registro de pago de egreso nro. ";
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
                            IdEmpresa = egreso.IdEmpresa,
                            Fecha = egreso.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de egreso nro. "
                        };
                        DetalleAsiento detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        egresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeEgresos & x.IdProducto == cuentaEgreso.IdCuenta).FirstOrDefault();
                        if (egresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaEgreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = egresoParam.IdCuenta;
                        detalleAsiento.Debito = egreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        foreach (var desglosePago in egreso.DesglosePagoEgreso)
                        {
                            if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = efectivo.IdCuenta;
                                detalleAsiento.Credito = desglosePago.MontoLocal;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario || desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                        egreso.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(egreso);
                        asiento.Detalle += egreso.IdEgreso;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        egreso.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(egreso);
                        movimientoBanco.Descripcion += egreso.IdEgreso;
                        dbContext.NotificarModificacion(movimientoBanco);
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
                    log.Error("Error al agregar el registro de egreso: ", ex);
                    throw new Exception("Se produjo un error agregando la información del egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarEgreso(Egreso egreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(egreso);
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
                    log.Error("Error al actualizar el registro de egreso: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularEgreso(int intIdEgreso, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Egreso egreso = dbContext.EgresoRepository.Find(intIdEgreso);
                    if (egreso == null) throw new Exception("El egreso por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (egreso.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    egreso.Nulo = true;
                    egreso.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(egreso);
                    if (egreso.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, egreso.IdAsiento);
                    }
                    if (egreso.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService();
                        servicioAuxiliarBancario.AnularMovimientoBanco(dbContext, egreso.IdMovBanco, intIdUsuario);
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
                    log.Error("Error al anular el registro de egreso: ", ex);
                    throw new Exception("Se produjo un error anulando el egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public Egreso ObtenerEgreso(int intIdEgreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Egreso egreso = dbContext.EgresoRepository.Include("DesglosePagoEgreso.FormaPago").Include("DesglosePagoEgreso.TipoMoneda").FirstOrDefault(x => x.IdEgreso == intIdEgreso);
                    foreach (DesglosePagoEgreso desglosePago in egreso.DesglosePagoEgreso)
                    {
                        CuentaBanco banco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                        desglosePago.DescripcionCuenta = banco.Descripcion;
                        desglosePago.Egreso = null;
                    }
                    return egreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando la información del egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaEgresos(int intIdEmpresa, int intIdSucursal, int intIdEgreso, string strBeneficiario, string strDetalle)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaEgresos = dbContext.EgresoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdEgreso > 0)
                        listaEgresos = listaEgresos.Where(x => x.IdEgreso == intIdEgreso);
                    else
                    {
                        if (!strBeneficiario.Equals(string.Empty))
                            listaEgresos = listaEgresos.Where(x => x.Beneficiario.Contains(strBeneficiario));
                        if (!strDetalle.Equals(string.Empty))
                            listaEgresos = listaEgresos.Where(x => x.Detalle.Contains(strDetalle));
                    }
                    return listaEgresos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<FlujoEfectivoDetalle> ObtenerListadoEgresos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdEgreso, string strBeneficiario, string strDetalle)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listadoEgreso = new List<FlujoEfectivoDetalle>();
                try
                {
                    var listado = dbContext.EgresoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdEgreso > 0)
                        listado = listado.Where(x => x.IdEgreso == intIdEgreso);
                    else
                    {
                        if (!strBeneficiario.Equals(string.Empty))
                            listado = listado.Where(x => x.Beneficiario.Contains(strBeneficiario));
                        if (!strDetalle.Equals(string.Empty))
                            listado = listado.Where(x => x.Detalle.Contains(strDetalle));
                    }
                    listado = listado.OrderByDescending(x => x.IdEgreso).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        FlujoEfectivoDetalle item = new FlujoEfectivoDetalle(value.IdEgreso, value.Fecha.ToString("dd/MM/yyyy"), value.Detalle, value.Monto);
                        listadoEgreso.Add(item);
                    }
                    return listadoEgreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de egresos. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}