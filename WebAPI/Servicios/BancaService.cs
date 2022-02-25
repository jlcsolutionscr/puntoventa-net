using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IBancaService
    {
        void AgregarCuentaBanco(CuentaBanco cuentaBanco);
        void ActualizarCuentaBanco(CuentaBanco cuentaBanco);
        void EliminarCuentaBanco(int intIdCuenta);
        CuentaBanco ObtenerCuentaBanco(int intIdCuenta);
        IList<LlaveDescripcion> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion = "");
        IList<LlaveDescripcion> ObtenerListadoTipoMovimientoBanco();
        string AgregarMovimientoBanco(MovimientoBanco movimiento);
        void ActualizarMovimientoBanco(MovimientoBanco movimiento);
        void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion);
        MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento);
        int ObtenerTotalListaMovimientos(int intIdEmpresa, int intIdSucursal, string strDescripcion = "");
        IList<EfectivoDetalle> ObtenerListadoMovimientos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strDescripcion = "");
    }

    public class BancaService : IBancaService
    {
        private static ILeandroContext dbContext;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BancaService(ILeandroContext pContext)
        {
            try
            {
                dbContext = pContext;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio del auxiliar bancario. Por favor consulte con su proveedor..");
            }
        }

        public void AgregarCuentaBanco(CuentaBanco cuentaBanco)
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
                log.Error("Error al agregar la cuenta bancaría: ", ex);
                throw new Exception("Se produjo un error agregando la cuenta bancaria. Por favor consulte con su proveedor..");
            }
        }

        public void ActualizarCuentaBanco(CuentaBanco cuentaBanco)
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
                log.Error("Error al actualizar la cuenta bancaría: ", ex);
                throw new Exception("Se produjo un error actualizando la cuenta bancaria. Por favor consulte con su proveedor..");
            }
        }

        public void EliminarCuentaBanco(int intIdCuenta)
        {
            try
            {
                CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(intIdCuenta);
                if (cuentaBanco == null) throw new Exception("La cuenta bancaria por eliminar no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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

        public CuentaBanco ObtenerCuentaBanco(int intIdCuenta)
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

        public IList<LlaveDescripcion> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion = "")
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
                log.Error("Error al obtener el listado de cuentas bancaría: ", ex);
                throw new Exception("Se produjo un error obteniendo el listado de cuentas bancarias. Por favor consulte con su proveedor..");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTipoMovimientoBanco()
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
                log.Error("Error al obtener el tipo de movimiento: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de movimientos bancarios. Por favor consulte con su proveedor..");
            }
        }

        public string AgregarMovimientoBanco(MovimientoBanco movimiento)
        {
            try
            {
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null) throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa);
                if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                return movimiento.IdMov.ToString();
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

        public void ActualizarMovimientoBanco(MovimientoBanco movimiento)
        {
            try
            {
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null) throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
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
                log.Error("Error al actualizar el movimiento bancario: ", ex);
                throw new Exception("Se produjo un error actualizando el movimiento bancario. Por favor consulte con su proveedor..");
            }
        }

        public void AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivoAnulacion)
        {
            try
            {
                MovimientoBanco movimiento = dbContext.MovimientoBancoRepository.Find(intIdMovimiento);
                if (movimiento == null) throw new Exception("El movimiento por eliminar no existe.");
                CuentaBanco cuenta = dbContext.CuentaBancoRepository.Find(movimiento.IdCuenta);
                if (cuenta == null) throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
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
                log.Error("Error al anular el movimiento bancario: ", ex);
                throw new Exception("Se produjo un error anulando el movimiento bancario. Por favor consulte con su proveedor..");
            }
        }

        public MovimientoBanco ObtenerMovimientoBanco(int intIdMovimiento)
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

        public int ObtenerTotalListaMovimientos(int intIdEmpresa, int intIdSucursal, string strDescripcion = "")
        {
            try
            {
                var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Where(a => !a.a.Nulo & a.b.IdEmpresa == intIdEmpresa && a.a.IdSucursal == intIdSucursal);
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

        public IList<EfectivoDetalle> ObtenerListadoMovimientos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strDescripcion = "")
        {
            var listaTipoMovimiento = new List<EfectivoDetalle>();
            try
            {
                var listaMovimientos = dbContext.MovimientoBancoRepository.Join(dbContext.CuentaBancoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                    .Where(a => !a.a.Nulo & a.b.IdEmpresa == intIdEmpresa && a.a.IdSucursal == intIdSucursal);
                if (!strDescripcion.Equals(string.Empty))
                    listaMovimientos = listaMovimientos.Where(a => a.a.Descripcion.Contains(strDescripcion));
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
                log.Error("Error al obtener el listado de movimientos bancarios: ", ex);
                throw new Exception("Se produjo un error consultando el listado de movimientos bancarios. Por favor consulte con su proveedor..");
            }
        }
    }
}