using System;
using System.Linq;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using Unity;
using System.Globalization;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IContabilidadService
    {
        CatalogoContable AgregarCuentaContable(CatalogoContable cuenta);
        void ActualizarCuentaContable(CatalogoContable cuenta);
        void EliminarCuentaContable(int intIdCuenta);
        CatalogoContable ObtenerCuentaContable(int intIdCuenta);
        IEnumerable<CatalogoContable> ObtenerListaCuentasContables(int intIdEmpresa, string strDescripcion = "");
        ParametroContable AgregarParametroContable(ParametroContable parametro);
        void ActualizarParametroContable(ParametroContable parametro);
        void EliminarParametroContable(int intIdParametro);
        ParametroContable ObtenerParametroContable(int intIdParametro);
        TipoParametroContable ObtenerTipoParametroContable(int intIdTipo);
        IEnumerable<ParametroContable> ObtenerListaParametrosContables(string strDescripcion = "");
        IEnumerable<TipoCuentaContable> ObtenerTiposCuentaContable();
        IEnumerable<TipoParametroContable> ObtenerTiposParametroContable();
        IEnumerable<ClaseCuentaContable> ObtenerClaseCuentaContable();
        IEnumerable<CatalogoContable> ObtenerListaCuentasPrimerOrden(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaMovimientos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaLineasDeProducto(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaLineasDeServicio(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaBancos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaEgresos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasParaIngresos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasDeBalance(int intIdEmpresa);
        Asiento AgregarAsiento(Asiento asiento);
        Asiento AgregarAsiento(IDbContext dbContext, Asiento asiento);
        void ReversarAsientoContable(IDbContext dbContext, int intIdAsiento);
        void ActualizarAsiento(Asiento asiento);
        void AnularAsiento(int intIdAsiento, int intIdUsuario);
        void AnularAsiento(IDbContext dbContext, int intIdAsiento, int intIdUsuario);
        Asiento ObtenerAsiento(int intIdAsiento);
        int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "");
        IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "");
        void MayorizarCuenta(IDbContext dbContext, int intIdCuenta, string strTipoMov, decimal dblMonto);
        CierreCaja GenerarDatosCierreCaja(int intIdEmpresa, string strFechaCierre);
        CierreCaja GuardarDatosCierreCaja(CierreCaja cierre);
        void AbortarCierreCaja(int intIdEmpresa);
        void ProcesarCierreMensual(int intIdEmpresa);
        void GenerarAsientosdeFacturas();
        void GenerarAsientosdeCompras();
        void GenerarAsientosdeEgresos();
        void GenerarAsientosdeIngresos();
        void GenerarAsientosdeAbonosCxC();
        void GenerarAsientosdeAbonosCxP();
        void AjustarSaldosCuentasdeMayor();
    }
    
    public class ContabilidadService: IContabilidadService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContabilidadService()
        {
        }

        public ContabilidadService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public CatalogoContable AgregarCuentaContable(CatalogoContable cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CatalogoContableRepository.Add(cuenta);
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
                    log.Error("Error al agregar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error agregando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
            return cuenta;
        }

        public void ActualizarCuentaContable(CatalogoContable cuenta)
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
                    log.Error("Error al actualizar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error actualizando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaContable(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    if (cuenta == null) throw new Exception("La cuenta contable por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CatalogoContableRepository.Remove(cuenta);
                    dbContext.Commit();
                }
                catch (DbUpdateException uex)
                {
                    log.Info("Validación al eliminar la cuenta contable: ", uex);
                    throw new BusinessException("No es posible eliminar la cuenta contable seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error eliminando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public CatalogoContable ObtenerCuentaContable(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error consultando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasContables(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCuentas = dbContext.CatalogoContableRepository.Include("TipoCuentaContable").Where(c => c.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaCuentas = listaCuentas.Where(c => c.Descripcion.Contains(strDescripcion));
                    return listaCuentas.OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables: ", ex);
                    throw new Exception("Se produjo un error cosultando el listado de cuentas contables. Por favor consulte con su proveedor.");
                }
            }
        }

        private bool esCuentaMadre(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    return (cuenta.IdCuentaGrupo == null);
                }
                catch (Exception ex)
                {
                    log.Error("Error al evaluar si la cuenta indicada es cuenta madre: ", ex);
                    throw new Exception("Se produjo un error verificando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable AgregarParametroContable(ParametroContable parametro)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    TipoParametroContable tipo = dbContext.TipoParametroContableRepository.Find(parametro.IdTipo);
                    bool bolError = !tipo.MultiCuenta & dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo).Count() > 0;
                    if (bolError)
                        throw new BusinessException("El tipo de parámetro contable " + tipo.Descripcion + " no soporta la asignación de múltiples cuentas contables");
                    bolError = dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo & x.IdCuenta == parametro.IdCuenta).Count() > 0;
                    if (bolError)
                        throw new BusinessException("El tipo de parámetro contable " + tipo.Descripcion + " ya tiene asignada la cuenta contable seleccionada");
                    dbContext.ParametroContableRepository.Add(parametro);
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
                    log.Error("Error al agregar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error agregando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
            return parametro;
        }

        public void ActualizarParametroContable(ParametroContable parametro)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error actualizando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarParametroContable(int intIdParametro)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                    if (parametro == null)
                        throw new Exception("El parámetro contable por eliminar no existe");
                    dbContext.ParametroContableRepository.Remove(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error eliminando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable ObtenerParametroContable(int intIdParametro)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                    return parametro;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public TipoParametroContable ObtenerTipoParametroContable(int intIdTipo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    TipoParametroContable tipoParametro = dbContext.TipoParametroContableRepository.Find(intIdTipo);
                    return tipoParametro;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaParametrosContables(string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaParametros = dbContext.ParametroContableRepository.Include("TipoParametroContable").Include("CatalogoContable").Where(x => x.IdParametro == x.IdParametro);
                    if (!strDescripcion.Equals(string.Empty))
                        listaParametros = listaParametros.Where(x => x.TipoParametroContable.Descripcion.Contains(strDescripcion));
                    return listaParametros.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de parámetros contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de parámetros contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoCuentaContable> ObtenerTiposCuentaContable()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoCuentaContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de cuenta contable: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoParametroContable> ObtenerTiposParametroContable()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoParametroContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ClaseCuentaContable> ObtenerClaseCuentaContable()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ClaseCuentaContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de clases de cuentas contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de clases de cuentas contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasPrimerOrden(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento == false).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables de primer orden: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables de primer orden. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaMovimientos(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para movimientos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para movimientos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaLineasDeProducto(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento & c.ParametroContable.Where(y => y.IdTipo == StaticTipoCuentaContable.LineaDeProductos).Count() > 0).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para líneas de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaLineasDeServicio(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento & c.ParametroContable.Where(y => y.IdTipo == StaticTipoCuentaContable.LineaDeServicios).Count() > 0).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para líneas de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaBancos(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento & c.ParametroContable.Where(y => y.IdTipo == StaticTipoCuentaContable.CuentaDeBancos).Count() > 0).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para cuentas bancarias: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para cuentas bancarías. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaEgresos(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento & c.ParametroContable.Where(y => y.IdTipo == StaticTipoCuentaContable.CuentaDeEgresos).Count() > 0).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaIngresos(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa & c.PermiteMovimiento & c.ParametroContable.Where(y => y.IdTipo == StaticTipoCuentaContable.CuentaDeIngresos).Count() > 0).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasDeBalance(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas contables para cuentas de PyG: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables de perdias y ganancias. Por favor consulte con su proveedor.");
                }
            }
        }

        public Asiento AgregarAsiento(Asiento asiento)
        {
            if(asiento.TotalDebito != asiento.TotalCredito)
                throw new BusinessException("El asiento contable se encuentra descuadrado. Por favor verifique los datos.");
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.AsientoRepository.Add(asiento);
                    foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                    {
                        if (detalleAsiento.CatalogoContable.PermiteMovimiento == false)
                            throw new BusinessException("La cuenta contable " + detalleAsiento.CatalogoContable.Descripcion + " no permite movimientos. Por favor verifique los parámetros del catalogo contable.");
                        if (detalleAsiento.Debito > 0)
                            MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Debito);
                        else
                            MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Credito);
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
                    log.Error("Error al agregar el asiento contable: ", ex);
                    throw new Exception("Se produjo un error agregando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
            return asiento;
        }

        public Asiento AgregarAsiento(IDbContext dbContext, Asiento asiento)
        {
            if (asiento.TotalDebito != asiento.TotalCredito)
                throw new BusinessException("El asiento contable se encuentra descuadrado. Por favor verifique los datos.");
            try
            {
                dbContext.AsientoRepository.Add(asiento);
                foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                {
                    if (detalleAsiento.CatalogoContable.PermiteMovimiento == false)
                        throw new BusinessException("La cuenta contable " + detalleAsiento.CatalogoContable.Descripcion + " no permite movimientos. Por favor verifique los parámetros del catalogo contable.");
                    if (detalleAsiento.Debito > 0)
                        MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Debito);
                    else
                        MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Credito);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al agregar el asiento contable: ", ex);
                throw ex;
            }
            return asiento;
        }

        public void ReversarAsientoContable(IDbContext dbContext, int intIdAsiento)
        {
            try
            {
                Asiento asiento = dbContext.AsientoRepository.Include("DetalleAsiento").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                Asiento asientoDeReversion = new Asiento
                {
                    IdEmpresa = asiento.IdEmpresa,
                    Detalle = "Reversión de " + asiento.Detalle,
                    Fecha = DateTime.Now,
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
                IContabilidadService servicioContabilidad = new ContabilidadService();
                servicioContabilidad.AgregarAsiento(dbContext, asientoDeReversion);
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                log.Error("Error al reversar asiento contable: ", ex);
                throw new Exception("Se produjo un error reversando el asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public void ActualizarAsiento(Asiento asiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(asiento);
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
                    log.Error("Error al actualizar el asiento contable: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularAsiento(int intIdAsiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Asiento asiento = dbContext.AsientoRepository.Include("DetalleAsiento").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                    if (asiento == null)
                        throw new Exception("El asiento contable por anular no existe");
                    if (asiento.Nulo)
                        return;
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                    {
                        if (detalleAsiento.Debito > 0)
                            MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Debito);
                        else
                            MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Credito);
                    }
                    asiento.Nulo = true;
                    asiento.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(asiento);
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
                    log.Error("Error al anular el asiento contable: ", ex);
                    throw new Exception("Se produjo un error anulando el asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularAsiento(IDbContext dbContext, int intIdAsiento, int intIdUsuario)
        {
            try
            {
                Asiento asiento = dbContext.AsientoRepository.Include("DetalleAsiento").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                if (asiento == null)
                    throw new Exception("El asiento contable por anular no existe");
                if (asiento.Nulo)
                    return;
                foreach (DetalleAsiento detalleAsiento in asiento.DetalleAsiento)
                {
                    if (detalleAsiento.Debito > 0)
                        MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Credito, detalleAsiento.Debito);
                    else
                        MayorizarCuenta(dbContext, detalleAsiento.IdCuenta, StaticTipoDebitoCredito.Debito, detalleAsiento.Credito);
                }
                asiento.Nulo = true;
                asiento.IdAnuladoPor = intIdUsuario;
                dbContext.NotificarModificacion(asiento);
            }
            catch (Exception ex)
            {
                log.Error("Error al anular el asiento contable: ", ex);
                throw ex;
            }
        }

        public Asiento ObtenerAsiento(int intIdAsiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.AsientoRepository.Include("DetalleAsiento.CatalogoContable.TipoCuentaContable").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el asiento contable: ", ex);
                    throw new Exception("Se produjo un error consultando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaAsientos = dbContext.AsientoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdAsiento > 0)
                        listaAsientos = listaAsientos.Where(x => x.IdAsiento == intIdAsiento);
                    else if (!strDetalle.Equals(string.Empty))
                        listaAsientos = listaAsientos.Where(x => x.Detalle.Contains(strDetalle));
                    return listaAsientos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de asientos contables: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaAsientos = dbContext.AsientoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdAsiento > 0)
                        listaAsientos = listaAsientos.Where(x => x.IdAsiento == intIdAsiento);
                    else if (!strDetalle.Equals(string.Empty))
                        listaAsientos = listaAsientos.Where(x => x.Detalle.Contains(strDetalle));
                    return listaAsientos.OrderByDescending(x => x.IdAsiento).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de asientos contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void MayorizarCuenta(IDbContext dbContext, int intIdCuenta, string strTipoMov, decimal dblMonto)
        {
            try
            {
                CatalogoContable catalogoContable = dbContext.CatalogoContableRepository.Include("TipoCuentaContable").FirstOrDefault(x => x.IdCuenta == intIdCuenta);
                if (catalogoContable == null)
                    throw new Exception("La cuenta contable por mayorizar no existe");
                if (strTipoMov.Equals(StaticTipoDebitoCredito.Debito))
                    if (catalogoContable.TipoCuentaContable.TipoSaldo.Equals(StaticTipoDebitoCredito.Debito))
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
                    if (catalogoContable.TipoCuentaContable.TipoSaldo.Equals(StaticTipoDebitoCredito.Credito))
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
                    MayorizarCuenta(dbContext, (int)catalogoContable.IdCuentaGrupo, strTipoMov, dblMonto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CierreCaja GenerarDatosCierreCaja(int intIdEmpresa, string strFechaCierre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime dtFechaCierre = DateTime.ParseExact(strFechaCierre, "dd/MM/yyyy", provider);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    empresa.CierreEnEjecucion = true;
                    dbContext.Commit();
                    CierreCaja cierre = new CierreCaja
                    {
                        IdEmpresa = intIdEmpresa,
                        FechaCierre = dtFechaCierre
                    };
                    CierreCaja cierreAnterior = cierreAnterior = dbContext.CierreCajaRepository.OrderByDescending(b => b.IdCierre).FirstOrDefault();
                    if (cierreAnterior != null)
                        cierre.FondoInicio = cierreAnterior.FondoCierre;
                    else
                        cierre.FondoInicio = 0;
                    cierre.VentasContado = 0;
                    cierre.VentasCredito = 0;
                    cierre.VentasTarjeta = 0;
                    cierre.OtrasVentas = 0;
                    cierre.RetencionIVA = 0;
                    cierre.ComisionVT = 0;
                    cierre.LiquidacionTarjeta = 0;
                    cierre.IngresoCxCEfectivo = 0;
                    cierre.IngresoCxCTarjeta = 0;
                    cierre.DevolucionesProveedores = 0;
                    cierre.OtrosIngresos = 0;
                    cierre.ComprasContado = 0;
                    cierre.ComprasCredito = 0;
                    cierre.OtrasCompras = 0;
                    cierre.EgresoCxPEfectivo = 0;
                    cierre.DevolucionesClientes = 0;
                    cierre.OtrosEgresos = 0;
                    var facturasCredito = dbContext.FacturaRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & x.IdCondicionVenta == StaticCondicionVenta.Credito & !x.Procesado).ToList();
                    if (facturasCredito.Count > 0)
                    {
                        decimal decFacturasCredito = facturasCredito.Sum(y => y.Total);
                        cierre.VentasCredito = decFacturasCredito;
                    }
                    var facturas = dbContext.FacturaRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & x.IdCondicionVenta != StaticCondicionVenta.Credito & !x.Procesado).ToList();
                    if (facturas.Count > 0)
                    {
                        var pagosFacturas = facturas.Join(dbContext.DesglosePagoFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                            .GroupBy(x => new { x.y.IdFormaPago,  x.y.IdCuentaBanco }).Select(x => new { IdFormaPago = x.Min(y => y.y.IdFormaPago), IdCuentaBanco = x.Min(y => y.y.IdCuentaBanco), Total = x.Sum(y => y.y.MontoLocal) });
                        foreach (var dato in pagosFacturas)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                                cierre.VentasContado = dato.Total;
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.VentasTarjeta += dato.Total;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionIVA += (dato.Total * banco.PorcentajeRetencion / 100);
                                cierre.ComisionVT += (dato.Total * banco.PorcentajeComision / 100);
                            }
                            else
                                cierre.OtrasVentas += dato.Total;
                        }
                    }
                    var comprasCredito = dbContext.CompraRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & x.IdCondicionVenta == StaticCondicionVenta.Credito & !x.Procesado).ToList();
                    if (comprasCredito.Count > 0)
                    {
                        decimal decFacturasCredito = comprasCredito.Sum(y => y.Total);
                        cierre.ComprasCredito = decFacturasCredito;
                        var pagosCompras = dbContext.CompraRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & x.IdCondicionVenta != StaticCondicionVenta.Credito & !x.Procesado).Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                            .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                        foreach (var dato in pagosCompras)
                        {
                            if (dato.Key == StaticFormaPago.Efectivo)
                                cierre.ComprasContado = dato.Total;
                            else
                                cierre.OtrasCompras += dato.Total;
                        }
                    }

                    var movimientosCxC = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => x.Tipo == 1 & x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).ToList();
                    if (movimientosCxC.Count > 0)
                    {
                        var pagosCxC = movimientosCxC.Join(dbContext.DesglosePagoMovimientoCuentaPorCobrarRepository, x => x.IdMovCxC, y => y.IdMovCxC, (x, y) => new { x, y })
                            .GroupBy(x => new { x.y.IdFormaPago, x.y.IdCuentaBanco }).Select(x => new { IdFormaPago = x.Min(y => y.y.IdFormaPago), IdCuentaBanco = x.Min(y => y.y.IdCuentaBanco), Total = x.Sum(y => y.y.MontoLocal) });
                        foreach (var dato in pagosCxC)
                        {
                            if (dato.IdFormaPago == StaticFormaPago.Efectivo)
                                cierre.IngresoCxCEfectivo = dato.Total;
                            else if (dato.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                cierre.VentasTarjeta += dato.Total;
                                BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(dato.IdCuentaBanco);
                                cierre.RetencionIVA += (dato.Total * banco.PorcentajeRetencion / 100);
                                cierre.ComisionVT += (dato.Total * banco.PorcentajeComision / 100);
                            }
                        }
                    }

                    var movimientosIngresos = dbContext.IngresoRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).Join(dbContext.DesglosePagoIngresoRepository, x => x.IdIngreso, y => y.IdIngreso, (x, y) => new { x, y })
                        .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    foreach (var dato in movimientosIngresos)
                    {
                        if (dato.Key == StaticFormaPago.Efectivo)
                            cierre.OtrosIngresos = dato.Total;
                    }

                    var movDevolucionesProveedores = dbContext.DevolucionProveedorRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).Join(dbContext.DesglosePagoDevolucionProveedorRepository, x => x.IdDevolucion, y => y.IdDevolucion, (x, y) => new { x, y })
                        .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    foreach (var dato in movDevolucionesProveedores)
                    {
                        if (dato.Key == StaticFormaPago.Efectivo)
                            cierre.DevolucionesProveedores = dato.Total;
                    }

                    var pagosCxP = dbContext.MovimientoCuentaPorPagarRepository.Where(x => x.Tipo == 1 & x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).Join(dbContext.DesglosePagoMovimientoCuentaPorPagarRepository, x => x.IdMovCxP, y => y.IdMovCxP, (x, y) => new { x, y })
                        .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    foreach (var dato in pagosCxP)
                    {
                        if (dato.Key == StaticFormaPago.Efectivo)
                            cierre.EgresoCxPEfectivo = dato.Total;
                    }

                    var movimientosEgresos = dbContext.EgresoRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).Join(dbContext.DesglosePagoEgresoRepository, x => x.IdEgreso, y => y.IdEgreso, (x, y) => new { x, y })
                        .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    foreach (var dato in movimientosEgresos)
                    {
                        if (dato.Key == StaticFormaPago.Efectivo)
                            cierre.OtrosEgresos = dato.Total;
                    }
                        
                    var movDevolucionesClientes = dbContext.DevolucionClienteRepository.Where(x => x.Nulo == false & x.IdEmpresa == intIdEmpresa & !x.Procesado).Join(dbContext.DesglosePagoDevolucionClienteRepository, x => x.IdDevolucion, y => y.IdDevolucion, (x, y) => new { x, y })
                        .GroupBy(x => x.y.IdFormaPago).Select(x => new { x.Key, Total = x.Sum(y => y.y.MontoLocal) });
                    foreach (var dato in movDevolucionesClientes)
                    {
                        if (dato.Key == StaticFormaPago.Efectivo)
                            cierre.DevolucionesClientes = dato.Total;
                    }
                    cierre.LiquidacionTarjeta = cierre.VentasTarjeta - cierre.RetencionIVA - cierre.ComisionVT;
                    return cierre;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    empresa.CierreEnEjecucion = false;
                    dbContext.Commit();
                    log.Error("Error al general el cierre diario: ", ex);
                    throw new Exception("Se produjo un error generando la información del cierre diario. Por favor consulte con su proveedor.");
                }
            }
        }

        public CierreCaja GuardarDatosCierreCaja(CierreCaja cierre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                using (var dbContextTransaction = dbContext.GetDatabaseTransaction())
                {
                    try
                    {
                        if (cierre.IdCierre > 0) throw new BusinessException("La información del cierre ya fue registrada. No es posible continuar con la transacción.");
                        Empresa empresa = dbContext.EmpresaRepository.Find(cierre.IdEmpresa);
                        if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        object[] objParameters = new object[1];
                        objParameters.SetValue(cierre.IdEmpresa, 0);
                        dbContext.ExecuteProcedure("MarcaRegistrosProcesados", objParameters);
                        dbContext.CierreCajaRepository.Add(cierre);
                        empresa.CierreEnEjecucion = false;
                        dbContext.Commit();
                        dbContextTransaction.Commit();
                    }
                    catch (BusinessException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback(); 
                        log.Error("Error al actualizar el cierre diario: ", ex);
                        throw new Exception("Se produjo un error actualizando la información del cierre diario. Por favor consulte con su proveedor.");
                    }
                }
            }
            return cierre;
        }

        public void AbortarCierreCaja(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    empresa.CierreEnEjecucion = false;
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("Error al finalizar el cierre diario: ", ex);
                    throw new Exception("Se produjo un error finalizando el proceso de cierre diario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarCierreMensual(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                decimal decTotalEgresos = 0;
                decimal decTotalIngresos = 0;
                ParametroContable perdidaGananciaParam = null;
                Empresa empresa = null;
                try
                {
                    empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    empresa.CierreEnEjecucion = true;
                    dbContext.Commit();
                    perdidaGananciaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.PerdidasyGanancias).FirstOrDefault();
                    if (perdidaGananciaParam == null) throw new Exception("La cuenta de perdidas y ganancias no se encuentra parametrizada y no se puede ejecutar el cierre contable. Por favor verificar.");

                    var saldosMensuales = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.SaldoActual != 0)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    foreach (CatalogoContable value in saldosMensuales)
                    {
                        SaldoMensualContable saldoMensual = new SaldoMensualContable
                        {
                            IdCuenta = value.IdCuenta,
                            Mes = DateTime.Now.Month,
                            Annio = DateTime.Now.Year,
                            SaldoFinMes = value.SaldoActual,
                            TotalDebito = value.TotalDebito,
                            TotalCredito = value.TotalCredito
                        };
                        dbContext.SaldoMensualContableRepository.Add(saldoMensual);
                    }

                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true & x.SaldoActual != 0 & x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    Asiento asiento = null;
                    asiento = new Asiento
                    {
                        IdEmpresa = intIdEmpresa,
                        Fecha = DateTime.Now,
                        TotalCredito = 0,
                        TotalDebito = 0,
                        Detalle = "Empresa cierre perdidas y ganancías"
                    };
                    DetalleAsiento detalleAsiento = null;
                    int intLineaDetalleAsiento = 0;
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            decTotalEgresos += value.SaldoActual;
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            detalleAsiento.IdCuenta = value.IdCuenta;
                            detalleAsiento.Credito = value.SaldoActual;
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                        }
                        else
                        {
                            decTotalIngresos += value.SaldoActual;
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            detalleAsiento.IdCuenta = value.IdCuenta;
                            detalleAsiento.Debito = value.SaldoActual;
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                        }
                    }
                    detalleAsiento = new DetalleAsiento();
                    intLineaDetalleAsiento += 1;
                    detalleAsiento.Linea = intLineaDetalleAsiento;
                    detalleAsiento.IdCuenta = perdidaGananciaParam.IdCuenta;
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
                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    asiento.DetalleAsiento.Add(detalleAsiento);
                    asiento.TotalDebito = decTotalIngresos;
                    asiento.TotalCredito = decTotalEgresos;
                    AgregarAsiento(dbContext, asiento);
                    empresa.CierreEnEjecucion = false;
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (empresa != null)
                    {
                        empresa.CierreEnEjecucion = false;
                        dbContext.Commit();
                    }
                    log.Error("Error al ejecutar el cierre mensual contable: ", ex);
                    throw new Exception("Se produjo un error ejecutando el cierre mensual contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeFacturas()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var facturas = dbContext.FacturaRepository.Where(x => x.Nulo == false).ToList();
                    foreach (Factura factura in facturas)
                    {
                        decimal decTotalMercancia = 0;
                        decimal decTotalServicios = 0;
                        decimal decSubTotalFactura = 0;
                        decimal decTotalImpuesto = 0;
                        decimal decTotalCostoVentas = 0;
                        ParametroContable ingresosVentasParam = null;
                        ParametroContable costoVentasParam = null;
                        ParametroContable ivaPorPagarParam = null;
                        ParametroContable efectivoParam = null;
                        ParametroContable cuentaPorCobrarClientesParam = null;
                        ParametroContable cuentaPorCobrarTarjetaParam = null;
                        ParametroContable gastoComisionParam = null;
                        ParametroContable lineaParam = null;
                        ParametroContable otraCondicionVentaParam = null;
                        DataTable dtbIngresosPorServicios = new DataTable();
                        dtbIngresosPorServicios.Columns.Add("IdLinea", typeof(int));
                        dtbIngresosPorServicios.Columns.Add("Total", typeof(decimal));
                        dtbIngresosPorServicios.PrimaryKey = new DataColumn[] { dtbIngresosPorServicios.Columns[0] };
                        DataTable dtbInventarios = new DataTable();
                        dtbInventarios.Columns.Add("IdLinea", typeof(int));
                        dtbInventarios.Columns.Add("Total", typeof(decimal));
                        dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
                        Asiento asiento = null;
                        try
                        {
                            ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IngresosPorVentas).FirstOrDefault();
                            costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CostosDeVentas).FirstOrDefault();
                            ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                            efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                            cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                            gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                            otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.OtraCondicionVenta).FirstOrDefault();
                            if (ingresosVentasParam == null || costoVentasParam == null || ivaPorPagarParam == null || efectivoParam == null || cuentaPorCobrarClientesParam == null || cuentaPorCobrarTarjetaParam == null || gastoComisionParam == null || otraCondicionVentaParam == null)
                                throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            factura.IdAsiento = 0;
                            decTotalImpuesto += factura.Impuesto;
                            decSubTotalFactura = factura.Total + factura.Descuento;
                            foreach (var detalleFactura in factura.DetalleFactura)
                            {
                                Producto producto = detalleFactura.Producto;
                                decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                                decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                if (!detalleFactura.Excento)
                                    decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (detalleFactura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                                if (producto.Tipo == StaticTipoProducto.Producto)
                                {
                                    decTotalMercancia += decTotalPorLinea;
                                    decTotalCostoVentas += producto.PrecioCosto * detalleFactura.Cantidad;
                                    int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                                    if (intExiste >= 0)
                                        dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + (producto.PrecioCosto * detalleFactura.Cantidad);
                                    else
                                    {
                                        DataRow data = dtbInventarios.NewRow();
                                        data["IdLinea"] = producto.Linea.IdLinea;
                                        data["Total"] = producto.PrecioCosto * detalleFactura.Cantidad;
                                        dtbInventarios.Rows.Add(data);
                                    }
                                }
                                else
                                {
                                    decTotalServicios += decTotalPorLinea;
                                    int intExiste = dtbIngresosPorServicios.Rows.IndexOf(dtbIngresosPorServicios.Rows.Find(producto.Linea.IdLinea));
                                    if (intExiste >= 0)
                                        dtbIngresosPorServicios.Rows[intExiste]["Total"] = (decimal)dtbIngresosPorServicios.Rows[intExiste]["Total"] + decTotalPorLinea;
                                    else
                                    {
                                        DataRow data = dtbIngresosPorServicios.NewRow();
                                        data["IdLinea"] = detalleFactura.Producto.Linea.IdLinea;
                                        data["Total"] = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                                        dtbIngresosPorServicios.Rows.Add(data);
                                    }
                                }
                            }
                            decimal decTotalDiff = decTotalMercancia + decTotalImpuesto + decTotalServicios - factura.Total;
                            if (decTotalDiff != 0)
                            {
                                if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                    throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                                if (decTotalMercancia > 0)
                                    decTotalMercancia -= decTotalDiff;
                                else
                                {
                                    dtbIngresosPorServicios.Rows[0]["Total"] = (decimal)dtbIngresosPorServicios.Rows[0]["Total"] - decTotalDiff;
                                    decTotalServicios -= decTotalDiff;
                                }
                            }
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = factura.IdEmpresa,
                                Fecha = factura.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de venta de mercancía"
                            };
                            DetalleAsiento detalleAsiento = null;
                            if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                            {
                                intLineaDetalleAsiento += 1;
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento,
                                    IdCuenta = cuentaPorCobrarClientesParam.IdCuenta,
                                    Debito = factura.Total,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentaPorCobrarClientesParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            else if (factura.IdCondicionVenta == StaticCondicionVenta.Contado)
                            {
                                foreach (var desglosePago in factura.DesglosePagoFactura)
                                {
                                    if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                                    {
                                        ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                        if (bancoParam == null)
                                            throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = bancoParam.IdCuenta,
                                            Debito = desglosePago.MontoLocal,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalDebito += detalleAsiento.Debito;
                                    }
                                    else if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                                    {
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = efectivoParam.IdCuenta,
                                            Debito = desglosePago.MontoLocal,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalDebito += detalleAsiento.Debito;
                                    }
                                    else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                                    {
                                        BancoAdquiriente bancoAdquiriente = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                                        decimal decTotalGastoComisionTarjeta = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeComision / 100), 2, MidpointRounding.AwayFromZero);
                                        decimal decTotalImpuestoRetenido = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeRetencion / 100), 2, MidpointRounding.AwayFromZero);
                                        decimal decTotalIngresosTarjeta = Math.Round(desglosePago.MontoLocal - decTotalGastoComisionTarjeta - decTotalImpuestoRetenido, 2, MidpointRounding.AwayFromZero);
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = cuentaPorCobrarTarjetaParam.IdCuenta,
                                            Debito = decTotalIngresosTarjeta,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentaPorCobrarTarjetaParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalDebito += detalleAsiento.Debito;
                                        if (decTotalImpuestoRetenido > 0)
                                        {
                                            intLineaDetalleAsiento += 1;
                                            detalleAsiento = new DetalleAsiento
                                            {
                                                Linea = intLineaDetalleAsiento,
                                                IdCuenta = ivaPorPagarParam.IdCuenta,
                                                Debito = decTotalImpuestoRetenido,
                                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(ivaPorPagarParam.IdCuenta).SaldoActual
                                            };
                                            asiento.DetalleAsiento.Add(detalleAsiento);
                                            asiento.TotalDebito += detalleAsiento.Debito;
                                        }
                                        if (decTotalGastoComisionTarjeta > 0)
                                        {
                                            intLineaDetalleAsiento += 1;
                                            detalleAsiento = new DetalleAsiento
                                            {
                                                Linea = intLineaDetalleAsiento,
                                                IdCuenta = gastoComisionParam.IdCuenta,
                                                Debito = decTotalGastoComisionTarjeta,
                                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(gastoComisionParam.IdCuenta).SaldoActual
                                            };
                                            asiento.DetalleAsiento.Add(detalleAsiento);
                                            asiento.TotalDebito += detalleAsiento.Debito;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                intLineaDetalleAsiento += 1;
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento,
                                    IdCuenta = otraCondicionVentaParam.IdCuenta,
                                    Debito = factura.Total,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(otraCondicionVentaParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            if (decTotalMercancia > 0)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = ingresosVentasParam.IdCuenta;
                                detalleAsiento.Credito = decTotalMercancia;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            if (decTotalImpuesto > 0)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                                detalleAsiento.Credito = decTotalImpuesto;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            foreach (DataRow data in dtbIngresosPorServicios.Rows)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                int intIdLinea = (int)data["IdLinea"];
                                lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeServicios & x.IdProducto == intIdLinea).FirstOrDefault();
                                if (lineaParam == null)
                                    throw new BusinessException("No existe parametrización contable para la línea de servicios " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                                detalleAsiento.Credito = (decimal)data["Total"];
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            if (decTotalCostoVentas > 0)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = costoVentasParam.IdCuenta;
                                detalleAsiento.Debito = decTotalCostoVentas;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                                foreach (DataRow data in dtbInventarios.Rows)
                                {
                                    detalleAsiento = new DetalleAsiento();
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento.Linea = intLineaDetalleAsiento;
                                    int intIdLinea = (int)data["IdLinea"];
                                    lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos & x.IdProducto == intIdLinea).FirstOrDefault();
                                    if (lineaParam == null)
                                        throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                                    detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                                    detalleAsiento.Credito = (decimal)data["Total"];
                                    detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalCredito += detalleAsiento.Credito;
                                }
                            }
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                factura.IdAsiento = asiento.IdAsiento;
                                dbContext.NotificarModificacion(factura);
                                asiento.Detalle = "Registro de venta de mercancía de Factura nro. " + factura.IdFactura;
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
                            log.Error("Error al agregar el registro de facturación: ", ex);
                            throw new Exception("Se produjo un error guardando la información de la factura. Por favor consulte con su proveedor.");
                        }
                    }
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de las facturas: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeCompras()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var compras = dbContext.CompraRepository.Where(x => x.Nulo == false).ToList();
                    foreach (Compra compra in compras)
                    {
                        decimal decTotalImpuesto = 0;
                        decimal decSubTotalCompra = 0;
                        decimal decTotalInventario = 0;
                        ParametroContable efectivoParam = null;
                        ParametroContable cuentasPorPagarProveedoresParam = null;
                        ParametroContable ivaPorPagarParam = null;
                        ParametroContable lineaParam = null;
                        ParametroContable otraCondicionVentaParam = null;
                        DataTable dtbInventarios = new DataTable();
                        dtbInventarios.Columns.Add("IdLinea", typeof(int));
                        dtbInventarios.Columns.Add("Total", typeof(decimal));
                        dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
                        Asiento asiento = null;
                        try
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                            if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                            efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            cuentasPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarProveedores).FirstOrDefault();
                            ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                            otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.OtraCondicionVenta).FirstOrDefault();
                            if (efectivoParam == null || cuentasPorPagarProveedoresParam == null || ivaPorPagarParam  == null || otraCondicionVentaParam == null)
                                throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            decTotalImpuesto = compra.Impuesto;
                            decSubTotalCompra = compra.Excento + compra.Grabado + compra.Descuento;
                            foreach (var detalleCompra in compra.DetalleCompra)
                            {
                                Producto producto = detalleCompra.Producto;
                                if (producto == null)
                                    throw new Exception("El producto asignado al detalle de la compra no existe");
                                if (producto.Tipo == StaticTipoProducto.Servicio)
                                    throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                                else if (producto.Tipo == StaticTipoProducto.Producto)
                                {
                                    decimal decTotalPorLinea = detalleCompra.PrecioCosto * detalleCompra.Cantidad;
                                    decTotalPorLinea = Math.Round(decTotalPorLinea - (compra.Descuento / decSubTotalCompra * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                    decTotalInventario += decTotalPorLinea;
                                    int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                                    if (intExiste >= 0)
                                        dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + decTotalPorLinea;
                                    else
                                    {
                                        DataRow data = dtbInventarios.NewRow();
                                        data["IdLinea"] = producto.Linea.IdLinea;
                                        data["Total"] = decTotalPorLinea;
                                        dtbInventarios.Rows.Add(data);
                                    }
                                }
                            }

                            decimal decTotalDiff = decTotalInventario + decTotalImpuesto - compra.Total;
                            if (decTotalDiff != 0)
                            {
                                if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                    throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                                dtbInventarios.Rows[0]["Total"] = (decimal)dtbInventarios.Rows[0]["Total"] - decTotalDiff;
                                decTotalInventario -= decTotalDiff;
                            }
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = compra.IdEmpresa,
                                Fecha = compra.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de compra de mercancía"
                            };
                            DetalleAsiento detalleAsiento = null;
                            if (decTotalImpuesto > 0)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                                detalleAsiento.Debito = decTotalImpuesto;
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            if (compra.IdCondicionVenta == StaticCondicionVenta.Credito)
                            {
                                intLineaDetalleAsiento += 1;
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento,
                                    IdCuenta = cuentasPorPagarProveedoresParam.IdCuenta,
                                    Credito = compra.Total,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentasPorPagarProveedoresParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            else if (compra.IdCondicionVenta == StaticCondicionVenta.Contado)
                            {
                                foreach (var desglosePago in compra.DesglosePagoCompra)
                                {
                                    if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                                    {
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = efectivoParam.IdCuenta,
                                            Credito = desglosePago.MontoLocal,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalCredito += detalleAsiento.Credito;
                                    }
                                    else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                                    {
                                        ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                        if (bancoParam == null)
                                            throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = bancoParam.IdCuenta,
                                            Credito = desglosePago.MontoLocal,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalCredito += detalleAsiento.Credito;
                                    }
                                }
                            }
                            else
                            {
                                intLineaDetalleAsiento += 1;
                                detalleAsiento = new DetalleAsiento
                                {
                                    Linea = intLineaDetalleAsiento,
                                    IdCuenta = otraCondicionVentaParam.IdCuenta,
                                    Credito = compra.Total,
                                    SaldoAnterior = dbContext.CatalogoContableRepository.Find(otraCondicionVentaParam.IdCuenta).SaldoActual
                                };
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                            foreach (DataRow data in dtbInventarios.Rows)
                            {
                                detalleAsiento = new DetalleAsiento();
                                intLineaDetalleAsiento += 1;
                                detalleAsiento.Linea = intLineaDetalleAsiento;
                                int intIdLinea = (int)data["IdLinea"];
                                lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos & x.IdProducto == intIdLinea).FirstOrDefault();
                                if (lineaParam == null)
                                    throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                                detalleAsiento.Debito = (decimal)data["Total"];
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalDebito += detalleAsiento.Debito;
                            }
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                compra.IdAsiento = asiento.IdAsiento;
                                dbContext.NotificarModificacion(compra);
                                asiento.Detalle = "Registro de compra de mercancía nro. " + compra.IdCompra;
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
                            log.Error("Error al agregar el registro de compra: ", ex);
                            throw new Exception("Se produjo un error agregando la información de la compra. Por favor consulte con su proveedor.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de las compras: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeEgresos()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var egresos = dbContext.EgresoRepository.Where(x => x.Nulo == false).ToList();
                    foreach (Egreso egreso in egresos)
                    {
                        ParametroContable efectivo = null;
                        ParametroContable bancoParam = null;
                        ParametroContable egresoParam = null;
                        Asiento asiento = null;
                        try
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa);
                            if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                            efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            if (efectivo == null)
                                throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(egreso.IdCuenta);
                            if (cuentaEgreso == null)
                                throw new Exception("La cuenta de egreso asignada al registro no existe");
                            egreso.IdAsiento = 0;
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = egreso.IdEmpresa,
                                Fecha = egreso.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de egreso"
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
                                else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
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
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                egreso.IdAsiento = asiento.IdAsiento;
                                dbContext.NotificarModificacion(egreso);
                                asiento.Detalle = "Registro de egreso nro. " + egreso.IdEgreso;
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
                            log.Error("Error al agregar el registro de egreso: ", ex);
                            throw new Exception("Se produjo un error agregando la información del egreso. Por favor consulte con su proveedor.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de los egresos: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeIngresos()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var ingresos = dbContext.IngresoRepository.Where(x => x.Nulo == false).ToList();
                    foreach (Ingreso ingreso in ingresos)
                    {
                        ParametroContable efectivo = null;
                        ParametroContable bancoParam = null;
                        ParametroContable ingresoParam = null;
                        Asiento asiento = null;
                        try
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                            if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                            efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            if (efectivo == null) throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            CuentaIngreso cuentaIngreso = dbContext.CuentaIngresoRepository.Find(ingreso.IdCuenta);
                            if (cuentaIngreso == null) throw new Exception("La cuenta de ingreso asignada al registro no existe");
                            ingreso.IdAsiento = 0;
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = ingreso.IdEmpresa,
                                Fecha = ingreso.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de ingreso"
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
                                else if (desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
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
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                ingreso.IdAsiento = asiento.IdAsiento;
                                dbContext.NotificarModificacion(ingreso);
                                asiento.Detalle = "Registro de ingreso nro. " + ingreso.IdIngreso;
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
                }
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de los ingresos: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeAbonosCxC()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var movimientos = dbContext.MovimientoCuentaPorCobrarRepository.Where(x => x.Nulo == false).ToList();
                    foreach (MovimientoCuentaPorCobrar movimiento in movimientos)
                    {
                        decimal decTotalIngresosTarjeta = 0;
                        decimal decTotalImpuestoRetenido = 0;
                        decimal decTotalGastoComisionTarjeta = 0;
                        ParametroContable efectivoParam = null;
                        ParametroContable cuentaPorCobrarTarjetaParam = null;
                        ParametroContable ivaPorPagarParam = null;
                        ParametroContable gastoComisionParam = null;
                        ParametroContable cuentaPorCobrarClientesParam = null;
                        Asiento asiento = null;
                        try
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                            if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                            efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                            ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                            gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                            cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                            if (efectivoParam == null || cuentaPorCobrarTarjetaParam == null || ivaPorPagarParam == null || gastoComisionParam == null || cuentaPorCobrarClientesParam == null)
                                throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            movimiento.IdAsiento = 0;
                            foreach (DesgloseMovimientoCuentaPorCobrar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorCobrar)
                            {
                                CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(desgloseMovimiento.IdCxC);
                                if (cxc == null)
                                    throw new Exception("La cuenta por cobrar asignada al movimiento no existe");
                            }
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = movimiento.IdEmpresa,
                                Fecha = movimiento.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de abono a cuenta por cobrar recibo. "
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
                            }
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            detalleAsiento.IdCuenta = cuentaPorCobrarClientesParam.IdCuenta;
                            detalleAsiento.Credito = movimiento.Monto;
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                movimiento.IdAsiento = asiento.IdAsiento;
                                dbContext.NotificarModificacion(movimiento);
                                asiento.Detalle += movimiento.IdMovCxC;
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
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de los ingresos: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarAsientosdeAbonosCxP()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var movimientos = dbContext.MovimientoCuentaPorPagarRepository.Where(x => x.Nulo == false).ToList();
                    foreach (MovimientoCuentaPorPagar movimiento in movimientos)
                    {
                        ParametroContable efectivoParam = null;
                        ParametroContable cuentaPorPagarProveedoresParam = null;
                        ParametroContable bancoParam = null;
                        Asiento asiento = null;
                        try
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(movimiento.IdEmpresa);
                            if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                            efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                            cuentaPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarProveedores).FirstOrDefault();
                            if (efectivoParam == null || cuentaPorPagarProveedoresParam == null)
                                throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                            movimiento.IdAsiento = 0;
                            foreach (DesgloseMovimientoCuentaPorPagar desgloseMovimiento in movimiento.DesgloseMovimientoCuentaPorPagar)
                            {
                                CuentaPorPagar cxp = dbContext.CuentaPorPagarRepository.Find(desgloseMovimiento.IdCxP);
                                if (cxp == null)
                                    throw new Exception("La cuenta por Pagar asignada al movimiento no existe");
                            }
                            int intLineaDetalleAsiento = 0;
                            asiento = new Asiento
                            {
                                IdEmpresa = movimiento.IdEmpresa,
                                Fecha = movimiento.Fecha,
                                TotalCredito = 0,
                                TotalDebito = 0,
                                Detalle = "Registro de abono a cuentas por Pagar recibo nro. " + movimiento.Recibo
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
                                else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
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
                            AgregarAsiento(dbContext, asiento);
                            dbContext.Commit();
                            if (asiento != null)
                            {
                                movimiento.IdAsiento = asiento.IdAsiento;
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
                            log.Error("Error al aplicar el movimiento de una cuenta por pagar: ", ex);
                            throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al general los asientos contables de los ingresos: ", ex);
                    throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AjustarSaldosCuentasdeMayor()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var cuentas = dbContext.CatalogoContableRepository.Where(x => x.PermiteMovimiento == true & x.SaldoActual > 0).ToList();
                    foreach (CatalogoContable cuenta in cuentas)
                    {
                        if (cuenta.IdCuentaGrupo != null)
                        {
                            if (cuenta.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                                MayorizarCuenta(dbContext, (int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Debito, cuenta.SaldoActual);
                            else
                                MayorizarCuenta(dbContext, (int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Credito, cuenta.SaldoActual);
                        }
                    }
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("Error al realizar el ajuste de saldos contables: ", ex);
                    throw new Exception("Se produjo un error ejecutando el ajuste de saldos contables. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}