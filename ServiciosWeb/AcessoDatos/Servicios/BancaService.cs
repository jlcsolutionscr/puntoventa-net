using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using LeandroSoftware.Core.CommonTypes;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using Unity;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IBancaService
    {
        CuentaBanco AgregarCuentaBanco(CuentaBanco cuentaBanco);
        void ActualizarCuentaBanco(CuentaBanco cuentaBanco);
        void EliminarCuentaBanco(int intIdCuenta);
        CuentaBanco ObtenerCuentaBanco(int intIdCuenta);
        IEnumerable<CuentaBanco> ObtenerListaCuentasBanco(int intIdEmpresa, string strDescripcion = "");
        IEnumerable<TipoMovimientoBanco> ObtenerTipoMovimientoBanco();
        MovimientoBanco AgregarMovimientoBanco(MovimientoBanco movimiento);
        MovimientoBanco AgregarMovimientoBanco(IDbContext dbContext, MovimientoBanco movimiento);
        void ActualizarMovimientoBanco(MovimientoBanco movimiento);
        void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario);
        void AnularMovimientoBanco(IDbContext dbContext, int intIdMovimiento, int intIdUsuario);
        MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento);
        int ObtenerTotalListaMovimientos(int intIdEmpresa, string strDescripcion = "");
        IEnumerable<MovimientoBanco> ObtenerListaMovimientos(int intIdEmpresa, int numPagina, int cantRec, string strDescripcion = "");
    }

    public class BancaService : IBancaService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BancaService()
        {
        }

        public BancaService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio del auxiliar bancario. Por favor consulte con su proveedor..");
            }
        }

        public CuentaBanco AgregarCuentaBanco(CuentaBanco cuentaBanco)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al agregar la cuenta bancaría: ", ex);
                    throw new Exception("Se produjo un error agregando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
            return cuentaBanco;
        }

        public void ActualizarCuentaBanco(CuentaBanco cuentaBanco)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al actualizar la cuenta bancaría: ", ex);
                    throw new Exception("Se produjo un error actualizando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public void EliminarCuentaBanco(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(intIdCuenta);
                    if (cuentaBanco == null)
                        throw new Exception("La cuenta bancaria por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaBancoRepository.Remove(cuentaBanco);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
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
                    log.Error("Error al eliminar la cuenta bancaría: ", ex);
                    throw new Exception("Se produjo un error eliminando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public CuentaBanco ObtenerCuentaBanco(int intIdCuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CuentaBancoRepository.Find(intIdCuenta);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la cuenta bancaría: ", ex);
                    throw new Exception("Se produjo un error consultando la cuenta bancaria. Por favor consulte con su proveedor..");
                }
            }
        }

        public IEnumerable<CuentaBanco> ObtenerListaCuentasBanco(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCuentas = dbContext.CuentaBancoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaCuentas = listaCuentas.Where(x => x.Descripcion.Contains(strDescripcion));
                    return listaCuentas.OrderBy(x => x.IdCuenta).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cuentas bancaría: ", ex);
                    throw new Exception("Se produjo un error obteniendo el listado de cuentas bancarias. Por favor consulte con su proveedor..");
                }
            }
        }

        public IEnumerable<TipoMovimientoBanco> ObtenerTipoMovimientoBanco()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoMovimientoBancoRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de movimiento: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }

        public MovimientoBanco AgregarMovimientoBanco(MovimientoBanco movimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                    if (cuenta == null)
                        throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    TipoMovimientoBanco tipo = dbContext.TipoMovimientoBancoRepository.Find(movimiento.IdTipo);
                    if (tipo == null)
                        throw new Exception("El tipo de movimiento no existe.");
                    movimiento.SaldoAnterior = cuenta.Saldo;
                    if (tipo.DebeHaber == StaticTipoDebitoCredito.Debito)
                        cuenta.Saldo -= movimiento.Monto;
                    else
                        cuenta.Saldo += movimiento.Monto;
                    dbContext.MovimientoBancoRepository.Add(movimiento);
                    dbContext.NotificarModificacion(cuenta);
                    dbContext.Commit();
                    return movimiento;
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el movimiento bancario: ", ex);
                    throw new Exception("Se produjo un error agregando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public MovimientoBanco AgregarMovimientoBanco(IDbContext dbContext, MovimientoBanco movimiento)
        {
            try
            {
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null)
                    throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                if (empresa == null)
                    throw new Exception("La empresa asignada a la transacción no existe.");
                if (empresa.CierreEnEjecucion)
                    throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                TipoMovimientoBanco tipo = dbContext.TipoMovimientoBancoRepository.Find(movimiento.IdTipo);
                if (tipo == null)
                    throw new Exception("El tipo de movimiento no existe.");
                movimiento.SaldoAnterior = cuenta.Saldo;
                if (tipo.DebeHaber == StaticTipoDebitoCredito.Debito)
                    cuenta.Saldo -= movimiento.Monto;
                else
                    cuenta.Saldo += movimiento.Monto;
                dbContext.MovimientoBancoRepository.Add(movimiento);
                dbContext.NotificarModificacion(cuenta);
                return movimiento;
            }
            catch (BusinessException ex)
            {
                dbContext.RollBack();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error("Error al agregar el movimiento bancario: ", ex);
                throw;
            }
        }

        public void ActualizarMovimientoBanco(MovimientoBanco movimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                    if (cuenta == null)
                        throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al actualizar el movimiento bancario: ", ex);
                    throw new Exception("Se produjo un error actualizando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    MovimientoBanco movimiento = dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                    if (movimiento == null)
                        throw new Exception("El movimiento por eliminar no existe.");
                    CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                    if (cuenta == null)
                        throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    movimiento.Nulo = true;
                    movimiento.IdAnuladoPor = intIdUsuario;
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
                    log.Error("Error al anular el movimiento bancario: ", ex);
                    throw new Exception("Se produjo un error anulando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public void AnularMovimientoBanco(IDbContext dbContext, int intIdMovimiento, int intIdUsuario)
        {
            try
            {
                MovimientoBanco movimiento = dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                if (movimiento == null)
                    throw new Exception("El movimiento por eliminar no existe.");
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null)
                    throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                if (empresa == null)
                    throw new Exception("La empresa asignada a la transacción no existe.");
                if (empresa.CierreEnEjecucion)
                    throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                movimiento.Nulo = true;
                movimiento.IdAnuladoPor = intIdUsuario;
                dbContext.NotificarModificacion(movimiento);
                cuenta.Saldo -= movimiento.Monto;
                dbContext.NotificarModificacion(cuenta);
            }
            catch (BusinessException ex)
            {
                dbContext.RollBack();
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error("Error al anular el movimiento bancario: ", ex);
                throw;
            }
        }

        public MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el movimiento bancario: ", ex);
                    throw new Exception("Se produjo un error consultando el movimiento bancario. Por favor consulte con su proveedor..");
                }
            }
        }

        public int ObtenerTotalListaMovimientos(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                            .Where(a => !a.a.Nulo && a.b.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaMovimientos = listaMovimientos.Where(a => a.a.Descripcion.Contains(strDescripcion));
                    return listaMovimientos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de movimientos bancarios: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }

        public IEnumerable<MovimientoBanco> ObtenerListaMovimientos(int intIdEmpresa, int numPagina, int cantRec, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Where(a => !a.a.Nulo && a.b.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaMovimientos = listaMovimientos.Where(a => a.a.Descripcion.Contains(strDescripcion));
                    return listaMovimientos.OrderByDescending(x => x.a.IdMov).Skip((numPagina - 1) * cantRec).Take(cantRec).Select(a => a.a).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de movimientos bancarios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de movimientos bancarios. Por favor consulte con su proveedor..");
                }
            }
        }
    }
}