using System.Data;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IContabilidadService
    {
        void AgregarCuentaContable(CatalogoContable cuenta);
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
        IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeProducto(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeServicio(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListaCuentasParaBancos(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListaCuentasParaEgresos(int intIdEmpresa);
        IEnumerable<ParametroContable> ObtenerListaCuentasParaIngresos(int intIdEmpresa);
        IEnumerable<CatalogoContable> ObtenerListaCuentasDeBalance(int intIdEmpresa);
        Asiento AgregarAsiento(Asiento asiento);
        Asiento AgregarAsiento(Asiento asiento, LeandroContext dbContext);
        void ReversarAsientoContable(int intIdAsiento);
        void ReversarAsientoContable(int intIdAsiento, LeandroContext dbContext);
        void ActualizarAsiento(Asiento asiento);
        void AnularAsiento(int intIdAsiento, int intIdUsuario);
        Asiento ObtenerAsiento(int intIdAsiento);
        int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "");
        IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "");
        void MayorizarCuenta(int intIdCuenta, string strTipoMov, decimal dblMonto);
        void ProcesarCierreMensual(int intIdEmpresa);
        void AjustarSaldosCuentasdeMayor();
    }
    
    public class ContabilidadService: IContabilidadService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory serviceScopeFactory;

        public ContabilidadService(ILoggerManager logger)
        {
            try
            {
                _logger = logger;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public ContabilidadService(ILoggerManager logger, IServiceScopeFactory pServiceScopeFactory)
        {
            try
            {
                _logger = logger;
                serviceScopeFactory = pServiceScopeFactory;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarCuentaContable(CatalogoContable cuenta)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    _logger.LogError("Error al agregar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error agregando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCuentaContable(CatalogoContable cuenta)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al actualizar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error actualizando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCuentaContable(int intIdCuenta)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    if (cuenta == null) throw new Exception("La cuenta contable por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CatalogoContableRepository.Remove(cuenta);
                    dbContext.Commit();
                }
                catch (DbUpdateException uex)
                {
                    _logger.LogError("Validación al eliminar la cuenta contable: ", uex);
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
                    _logger.LogError("Error al eliminar la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error eliminando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public CatalogoContable ObtenerCuentaContable(int intIdCuenta)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la cuenta contable: ", ex);
                    throw new Exception("Se produjo un error consultando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasContables(int intIdEmpresa, string strDescripcion = "")
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de cuentas contables: ", ex);
                    throw new Exception("Se produjo un error cosultando el listado de cuentas contables. Por favor consulte con su proveedor.");
                }
            }
        }

        private bool esCuentaMadre(int intIdCuenta)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                    return (cuenta.IdCuentaGrupo == null);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al evaluar si la cuenta indicada es cuenta madre: ", ex);
                    throw new Exception("Se produjo un error verificando la cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable AgregarParametroContable(ParametroContable parametro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    TipoParametroContable tipo = dbContext.TipoParametroContableRepository.Find(parametro.IdTipo);
                    bool bolError = !tipo.MultiCuenta && dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo).Count() > 0;
                    if (bolError)
                        throw new BusinessException("El tipo de parámetro contable " + tipo.Descripcion + " no soporta la asignación de múltiples cuentas contables");
                    bolError = dbContext.ParametroContableRepository.Where(x => x.IdTipo == parametro.IdTipo && x.IdCuenta == parametro.IdCuenta).Count() > 0;
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
                    _logger.LogError("Error al agregar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error agregando el parámetro contable. Por favor consulte con su proveedor.");
                }
                return parametro;
            }
        }

        public void ActualizarParametroContable(ParametroContable parametro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error actualizando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarParametroContable(int intIdParametro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al eliminar el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error eliminando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroContable ObtenerParametroContable(int intIdParametro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                    return parametro;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public TipoParametroContable ObtenerTipoParametroContable(int intIdTipo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    TipoParametroContable tipoParametro = dbContext.TipoParametroContableRepository.Find(intIdTipo);
                    return tipoParametro;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaParametrosContables(string strDescripcion = "")
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de parámetros contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de parámetros contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoCuentaContable> ObtenerTiposCuentaContable()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.TipoCuentaContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el tipo de cuenta contable: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de cuenta contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoParametroContable> ObtenerTiposParametroContable()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.TipoParametroContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el tipo de parámetro contable: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de parámetro contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ClaseCuentaContable> ObtenerClaseCuentaContable()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ClaseCuentaContableRepository.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de clases de cuentas contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de clases de cuentas contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasPrimerOrden(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento == false).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables de primer orden: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables de primer orden. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaMovimientos(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para movimientos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para movimientos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeProducto(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.LineaDeProductos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para líneas de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeServicio(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.LineaDeServicios }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para líneas de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaBancos(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeBancos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para cuentas bancarias: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para cuentas bancarías. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaEgresos(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeEgresos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaIngresos(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeIngresos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasDeBalance(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de cuentas contables para cuentas de PyG: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cuentas contables de perdias y ganancias. Por favor consulte con su proveedor.");
                }
            }
        }

        public Asiento AgregarAsiento(Asiento asiento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
            catch (BusinessException ex)
            {
                dbContext.RollBack();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                _logger.LogError("Error al agregar el asiento contable: ", ex);
                throw new Exception("Se produjo un error agregando la información del asiento contable. Por favor consulte con su proveedor.");
            }
            return asiento;
        }

        public void ReversarAsientoContable(int intIdAsiento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                AgregarAsiento(asientoDeReversion);
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                _logger.LogError("Error al reversar asiento contable: ", ex);
                throw new Exception("Se produjo un error reversando el asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public void ActualizarAsiento(Asiento asiento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(asiento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    _logger.LogError("Error al actualizar el asiento contable: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularAsiento(int intIdAsiento, int intIdUsuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al anular el asiento contable: ", ex);
                    throw new Exception("Se produjo un error anulando el asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public Asiento ObtenerAsiento(int intIdAsiento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.AsientoRepository.Include("DetalleAsiento.CatalogoContable.TipoCuentaContable").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el asiento contable: ", ex);
                    throw new Exception("Se produjo un error consultando la información del asiento contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "")
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el total del listado de asientos contables: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "")
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de asientos contables: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de asientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public void MayorizarCuenta(int intIdCuenta, string strTipoMov, decimal dblMonto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                        MayorizarCuenta((int)catalogoContable.IdCuentaGrupo, strTipoMov, dblMonto);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ProcesarCierreMensual(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                decimal decTotalEgresos = 0;
                decimal decTotalIngresos = 0;
                ParametroContable perdidaGananciaParam = null;
                Empresa empresa = null;
                try
                {
                    empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    //if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    //empresa.CierreEnEjecucion = true;
                    dbContext.Commit();
                    perdidaGananciaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.PerdidasyGanancias).FirstOrDefault();
                    if (perdidaGananciaParam == null) throw new Exception("La cuenta de perdidas y ganancias no se encuentra parametrizada y no se puede ejecutar el cierre contable. Por favor verificar.");

                    var saldosMensuales = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.SaldoActual != 0)
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

                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true && x.SaldoActual != 0 && x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
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
                    AgregarAsiento(asiento);
                    //empresa.CierreEnEjecucion = false;
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    //if (empresa != null)
                    //{
                    //    empresa.CierreEnEjecucion = false;
                    //    dbContext.Commit();
                    //}
                    _logger.LogError("Error al ejecutar el cierre mensual contable: ", ex);
                    throw new Exception("Se produjo un error ejecutando el cierre mensual contable. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AjustarSaldosCuentasdeMayor()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var cuentas = dbContext.CatalogoContableRepository.Where(x => x.PermiteMovimiento == true && x.SaldoActual > 0).ToList();
                    foreach (CatalogoContable cuenta in cuentas)
                    {
                        if (cuenta.IdCuentaGrupo != null)
                        {
                            if (cuenta.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                                MayorizarCuenta((int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Debito, cuenta.SaldoActual);
                            else
                                MayorizarCuenta((int)cuenta.IdCuentaGrupo, StaticTipoDebitoCredito.Credito, cuenta.SaldoActual);
                        }
                    }
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al realizar el ajuste de saldos contables: ", ex);
                    throw new Exception("Se produjo un error ejecutando el ajuste de saldos contables. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}