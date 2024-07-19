using System.Globalization;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IBancaService
    {
        void AgregarCuentaBanco(CuentaBanco cuentaBanco);
        void ActualizarCuentaBanco(CuentaBanco cuentaBanco);
        void EliminarCuentaBanco(int intIdCuenta);
        CuentaBanco ObtenerCuentaBanco(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion);
        IList<LlaveDescripcion> ObtenerListadoTipoMovimientoBanco();
        ReferenciasEntidad AgregarMovimientoBanco(MovimientoBanco movimiento);
        ReferenciasEntidad AgregarMovimientoBanco(MovimientoBanco movimiento, LeandroContext dbContext);
        void ActualizarMovimientoBanco(MovimientoBanco movimiento);
        void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion, LeandroContext dbContext);
        MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento);
        int ObtenerTotalListaMovimientos(int intIdEmpresa, int intIdSucursal, string strDescripcion, string strFechaFinal);
        IList<EfectivoDetalle> ObtenerListadoMovimientos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strDescripcion, string strFechaFinal);
    }

    public class BancaService : IBancaService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory? _serviceScopeFactory;
        private static IConfiguracionGeneral? _config;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public BancaService(ILoggerManager logger, IConfiguracionGeneral? config)
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
                else throw new Exception("Se produjo un error al inicializar el servicio del auxiliar bancario. Por favor consulte con su proveedor..");
            }
        }

        public BancaService(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory, IConfiguracionGeneral config)
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
                else throw new Exception("Se produjo un error al inicializar el servicio del auxiliar bancario. Por favor consulte con su proveedor..");
            }
        }

        public void AgregarCuentaBanco(CuentaBanco cuentaBanco)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaBancoRepository.Add(cuentaBanco);
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
                    if (_logger != null) _logger.LogError("Error al agregar la cuenta bancaría: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error agregando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public void ActualizarCuentaBanco(CuentaBanco cuentaBanco)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(cuentaBanco);
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
                    if (_logger != null) _logger.LogError("Error al actualizar la cuenta bancaría: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public void EliminarCuentaBanco(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(intIdCuenta);
                    if (cuentaBanco == null) throw new BusinessException("La cuenta bancaria por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.CuentaBancoRepository.Remove(cuentaBanco);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    if (_logger != null) _logger.LogError("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar el banco adquiriente seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    if (_logger != null) _logger.LogError("Error al eliminar la cuenta bancaría: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error eliminando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public CuentaBanco ObtenerCuentaBanco(int intIdCuenta)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CuentaBancoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener la cuenta bancaría: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCuentaBanco = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CuentaBancoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderBy(x => x.IdCuenta);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCuenta, value.Descripcion);
                        listaCuentaBanco.Add(item);
                    }
                    return listaCuentaBanco;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de cuentas bancaría: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error obteniendo el listado de cuentas bancarias. Por favor consulte con su proveedor..");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTipoMovimientoBanco()
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaTipoMovimiento = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TipoMovimientoBancoRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTipoMov, value.Descripcion);
                        listaTipoMovimiento.Add(item);
                    }
                    return listaTipoMovimiento;

                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el tipo de movimiento: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de tipos de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }

        public ReferenciasEntidad AgregarMovimientoBanco(MovimientoBanco movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                return AdicionarMovimientoBanco(movimiento, dbContext);
            }
        }

        public ReferenciasEntidad AgregarMovimientoBanco(MovimientoBanco movimiento, LeandroContext dbContext)
        {
            return AdicionarMovimientoBanco(movimiento, dbContext);
        }


        private ReferenciasEntidad AdicionarMovimientoBanco(MovimientoBanco movimiento, LeandroContext dbContext)
        {
            try
            {
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                TipoMovimientoBanco tipo = dbContext.TipoMovimientoBancoRepository.Find(movimiento.IdTipo);
                if (tipo == null) throw new BusinessException("El tipo de movimiento no existe.");
                movimiento.SaldoAnterior = cuenta.Saldo;
                if (tipo.DebeHaber == StaticTipoDebitoCredito.Debito)
                    cuenta.Saldo -= movimiento.Monto;
                else
                    cuenta.Saldo += movimiento.Monto;
                dbContext.MovimientoBancoRepository.Add(movimiento);
                dbContext.NotificarModificacion(cuenta);
                dbContext.Commit();
                return new ReferenciasEntidad(movimiento.IdMov.ToString());
            }
            catch (BusinessException ex)
            {
                dbContext.RollBack();
                throw ex;
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                if (_logger != null) _logger.LogError("Error al agregar el movimiento bancario: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error agregando el movimiento bancario. Por favor consulte con su proveedor..");
            }
        }

        public void ActualizarMovimientoBanco(MovimientoBanco movimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                    if (cuenta == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(movimiento);
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
                    if (_logger != null) _logger.LogError("Error al actualizar el movimiento bancario: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error actualizando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                InvalidarMovimientoBanco(intIdMovimiento, intIdUsuario, strMotivoAnulacion, dbContext);
            }
        }

        public void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion, LeandroContext dbContext)
        {
            InvalidarMovimientoBanco(intIdMovimiento, intIdUsuario, strMotivoAnulacion, dbContext);
        }

        private void InvalidarMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion, LeandroContext dbContext)
        {
            try
            {
                MovimientoBanco movimiento = dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                if (movimiento == null) throw new BusinessException("El movimiento por eliminar no existe.");
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null) throw new BusinessException("La cuenta bancaria asignada al movimiento no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                movimiento.Nulo = true;
                movimiento.IdAnuladoPor = intIdUsuario;
                movimiento.MotivoAnulacion = strMotivoAnulacion;
                dbContext.NotificarModificacion(movimiento);
                cuenta.Saldo -= movimiento.Monto;
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
                if (_logger != null) _logger.LogError("Error al anular el movimiento bancario: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error anulando el movimiento bancario. Por favor consulte con su proveedor..");
            }
        }

        public MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el movimiento bancario: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public int ObtenerTotalListaMovimientos(int intIdEmpresa, int intIdSucursal, string strDescripcion, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                            .Where(a => !a.a.Nulo & a.b.IdEmpresa == intIdEmpresa && a.a.IdSucursal == intIdSucursal);
                    if (!strDescripcion.Equals(string.Empty))
                        listaMovimientos = listaMovimientos.Where(a => a.a.Descripcion.Contains(strDescripcion));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listaMovimientos = listaMovimientos.Where(a => a.a.Fecha < datFechaFinal);
                    }
                    return listaMovimientos.Count();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el total del listado de movimientos bancarios: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el total del listado de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }

        public IList<EfectivoDetalle> ObtenerListadoMovimientos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strDescripcion, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaTipoMovimiento = new List<EfectivoDetalle>();
                try
                {
                    var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Where(a => !a.a.Nulo & a.b.IdEmpresa == intIdEmpresa && a.a.IdSucursal == intIdSucursal);
                    if (!strDescripcion.Equals(string.Empty))
                        listaMovimientos = listaMovimientos.Where(a => a.a.Descripcion.Contains(strDescripcion));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listaMovimientos = listaMovimientos.Where(a => a.a.Fecha < datFechaFinal);
                    }
                    var lista = listaMovimientos.OrderByDescending(x => x.a.IdMov).Skip((numPagina - 1) * cantRec).Take(cantRec)
                        .Select(z => new { z.a.IdMov, z.a.Fecha, z.a.Descripcion, z.a.Monto }).ToList();
                    foreach (var value in lista)
                    {
                        var item = new EfectivoDetalle(value.IdMov, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion, value.Monto);
                        listaTipoMovimiento.Add(item);
                    }
                    return listaTipoMovimiento;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al obtener el listado de movimientos bancarios: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error consultando el listado de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }
    }
}