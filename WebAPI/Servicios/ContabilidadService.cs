using System.Data;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using Microsoft.EntityFrameworkCore;

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
        void ReversarAsientoContable(int intIdAsiento);
        void ActualizarAsiento(Asiento asiento);
        void AnularAsiento(int intIdAsiento, int intIdUsuario);
        Asiento ObtenerAsiento(int intIdAsiento);
        int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "");
        IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "");
        void MayorizarCuenta(int intIdCuenta, string strTipoMov, decimal dblMonto);
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
        private static ILeandroContext dbContext;

        public ContabilidadService(ILeandroContext pContext)
        {
            try
            {
                dbContext = pContext;
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Contabilidad. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarCuentaContable(CatalogoContable cuenta)
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
                //_logger.LogError("Error al agregar la cuenta contable: ", ex);
                throw new Exception("Se produjo un error agregando la cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public void ActualizarCuentaContable(CatalogoContable cuenta)
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
                //_logger.LogError("Error al actualizar la cuenta contable: ", ex);
                throw new Exception("Se produjo un error actualizando la cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public void EliminarCuentaContable(int intIdCuenta)
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
                //_logger.LogError("Validación al eliminar la cuenta contable: ", uex);
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
                //_logger.LogError("Error al eliminar la cuenta contable: ", ex);
                throw new Exception("Se produjo un error eliminando la cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public CatalogoContable ObtenerCuentaContable(int intIdCuenta)
        {
            try
            {
                return dbContext.CatalogoContableRepository.Find(intIdCuenta);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener la cuenta contable: ", ex);
                throw new Exception("Se produjo un error consultando la cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasContables(int intIdEmpresa, string strDescripcion = "")
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
                //_logger.LogError("Error al obtener el listado de cuentas contables: ", ex);
                throw new Exception("Se produjo un error cosultando el listado de cuentas contables. Por favor consulte con su proveedor.");
            }
        }

        private bool esCuentaMadre(int intIdCuenta)
        {
            try
            {
                CatalogoContable cuenta = dbContext.CatalogoContableRepository.Find(intIdCuenta);
                return (cuenta.IdCuentaGrupo == null);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al evaluar si la cuenta indicada es cuenta madre: ", ex);
                throw new Exception("Se produjo un error verificando la cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public ParametroContable AgregarParametroContable(ParametroContable parametro)
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
                //_logger.LogError("Error al agregar el parámetro contable: ", ex);
                throw new Exception("Se produjo un error agregando el parámetro contable. Por favor consulte con su proveedor.");
            }
            return parametro;
        }

        public void ActualizarParametroContable(ParametroContable parametro)
        {
            try
            {
                dbContext.NotificarModificacion(parametro);
                dbContext.Commit();
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                //_logger.LogError("Error al actualizar el parámetro contable: ", ex);
                throw new Exception("Se produjo un error actualizando el parámetro contable. Por favor consulte con su proveedor.");
            }
        }

        public void EliminarParametroContable(int intIdParametro)
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
                //_logger.LogError("Error al eliminar el parámetro contable: ", ex);
                throw new Exception("Se produjo un error eliminando el parámetro contable. Por favor consulte con su proveedor.");
            }
        }

        public ParametroContable ObtenerParametroContable(int intIdParametro)
        {
            try
            {
                ParametroContable parametro = dbContext.ParametroContableRepository.Find(intIdParametro);
                return parametro;
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el parámetro contable: ", ex);
                throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
            }
        }

        public TipoParametroContable ObtenerTipoParametroContable(int intIdTipo)
        {
            try
            {
                TipoParametroContable tipoParametro = dbContext.TipoParametroContableRepository.Find(intIdTipo);
                return tipoParametro;
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el parámetro contable: ", ex);
                throw new Exception("Se produjo un error consultando el parámetro contable. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaParametrosContables(string strDescripcion = "")
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
                //_logger.LogError("Error al obtener el listado de parámetros contables: ", ex);
                throw new Exception("Se produjo un error consultando el listado de parámetros contables. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<TipoCuentaContable> ObtenerTiposCuentaContable()
        {
            try
            {
                return dbContext.TipoCuentaContableRepository.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el tipo de cuenta contable: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de cuenta contable. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<TipoParametroContable> ObtenerTiposParametroContable()
        {
            try
            {
                return dbContext.TipoParametroContableRepository.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el tipo de parámetro contable: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de parámetro contable. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ClaseCuentaContable> ObtenerClaseCuentaContable()
        {
            try
            {
                return dbContext.ClaseCuentaContableRepository.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de clases de cuentas contables: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de clases de cuentas contables. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasPrimerOrden(int intIdEmpresa)
        {
            try
            {
                return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento == false).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables de primer orden: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables de primer orden. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasParaMovimientos(int intIdEmpresa)
        {
            try
            {
                return dbContext.CatalogoContableRepository.Where(c => c.IdEmpresa == intIdEmpresa && c.PermiteMovimiento).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para movimientos: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para movimientos. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeProducto(int intIdEmpresa)
        {
            try
            {
                return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.LineaDeProductos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para líneas de producto: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de producto. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaLineasDeServicio(int intIdEmpresa)
        {
            try
            {
                return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.LineaDeServicios }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para líneas de servicio: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para líneas de servicio. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaBancos(int intIdEmpresa)
        {
            try
            {
                return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeBancos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para cuentas bancarias: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para cuentas bancarías. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaEgresos(int intIdEmpresa)
        {
            try
            {
                return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeEgresos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<ParametroContable> ObtenerListaCuentasParaIngresos(int intIdEmpresa)
        {
            try
            {
                return dbContext.ParametroContableRepository.Include("CatalogoContable").Where(c => c.CatalogoContable.IdEmpresa == intIdEmpresa && c.CatalogoContable.PermiteMovimiento && new[] { StaticTipoParametroContable.CuentaDeIngresos }.Contains(c.IdTipo)).OrderBy(x => x.CatalogoContable.Nivel_1).ThenBy(x => x.CatalogoContable.Nivel_2).ThenBy(x => x.CatalogoContable.Nivel_3).ThenBy(x => x.CatalogoContable.Nivel_4).ThenBy(x => x.CatalogoContable.Nivel_5).ThenBy(x => x.CatalogoContable.Nivel_6).ThenBy(x => x.CatalogoContable.Nivel_7).ToList();
                }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para cuentas de egreso: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables para egresos. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<CatalogoContable> ObtenerListaCuentasDeBalance(int intIdEmpresa)
        {
            try
            {
                return dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true).OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el listado de cuentas contables para cuentas de PyG: ", ex);
                throw new Exception("Se produjo un error consultando el listado de cuentas contables de perdias y ganancias. Por favor consulte con su proveedor.");
            }
        }

        public Asiento AgregarAsiento(Asiento asiento)
        {
            if(asiento.TotalDebito != asiento.TotalCredito)
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
                //_logger.LogError("Error al agregar el asiento contable: ", ex);
                throw new Exception("Se produjo un error agregando la información del asiento contable. Por favor consulte con su proveedor.");
            }
            return asiento;
        }

        public void ReversarAsientoContable(int intIdAsiento)
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
                IContabilidadService servicioContabilidad = new ContabilidadService(dbContext);
                servicioContabilidad.AgregarAsiento(asientoDeReversion);
            }
            catch (Exception ex)
            {
                dbContext.RollBack();
                //_logger.LogError("Error al reversar asiento contable: ", ex);
                throw new Exception("Se produjo un error reversando el asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public void ActualizarAsiento(Asiento asiento)
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
                //_logger.LogError("Error al actualizar el asiento contable: ", ex);
                throw new Exception("Se produjo un error actualizando la información del asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public void AnularAsiento(int intIdAsiento, int intIdUsuario)
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
                //_logger.LogError("Error al anular el asiento contable: ", ex);
                throw new Exception("Se produjo un error anulando el asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public Asiento ObtenerAsiento(int intIdAsiento)
        {
            try
            {
                return dbContext.AsientoRepository.Include("DetalleAsiento.CatalogoContable.TipoCuentaContable").FirstOrDefault(x => x.IdAsiento == intIdAsiento);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al obtener el asiento contable: ", ex);
                throw new Exception("Se produjo un error consultando la información del asiento contable. Por favor consulte con su proveedor.");
            }
        }

        public int ObtenerTotalListaAsientos(int intIdEmpresa, int intIdAsiento = 0, string strDetalle = "")
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
                //_logger.LogError("Error al obtener el total del listado de asientos contables: ", ex);
                throw new Exception("Se produjo un error consultando el total del listado de asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<Asiento> ObtenerListaAsientos(int intIdEmpresa, int numPagina, int cantRec, int intIdAsiento = 0, string strDetalle = "")
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
                //_logger.LogError("Error al obtener el listado de asientos contables: ", ex);
                throw new Exception("Se produjo un error consultando el listado de asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void MayorizarCuenta(int intIdCuenta, string strTipoMov, decimal dblMonto)
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

        public void ProcesarCierreMensual(int intIdEmpresa)
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
                //_logger.LogError("Error al ejecutar el cierre mensual contable: ", ex);
                throw new Exception("Se produjo un error ejecutando el cierre mensual contable. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeFacturas()
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
                        ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IngresosPorVentas).FirstOrDefault();
                        costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CostosDeVentas).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarClientes).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.GastoComisionTarjeta).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.OtraCondicionVenta).FirstOrDefault();
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
                                int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.IdLinea));
                                if (intExiste >= 0)
                                    dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + (producto.PrecioCosto * detalleFactura.Cantidad);
                                else
                                {
                                    DataRow data = dtbInventarios.NewRow();
                                    data["IdLinea"] = producto.IdLinea;
                                    data["Total"] = producto.PrecioCosto * detalleFactura.Cantidad;
                                    dtbInventarios.Rows.Add(data);
                                }
                            }
                            else
                            {
                                decTotalServicios += decTotalPorLinea;
                                int intExiste = dtbIngresosPorServicios.Rows.IndexOf(dtbIngresosPorServicios.Rows.Find(producto.IdLinea));
                                if (intExiste >= 0)
                                    dtbIngresosPorServicios.Rows[intExiste]["Total"] = (decimal)dtbIngresosPorServicios.Rows[intExiste]["Total"] + decTotalPorLinea;
                                else
                                {
                                    DataRow data = dtbIngresosPorServicios.NewRow();
                                    data["IdLinea"] = detalleFactura.Producto.IdLinea;
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
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.LineaDeServicios && x.IdProducto == intIdLinea).FirstOrDefault();
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
                                lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
                                if (lineaParam == null)
                                    throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                                detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                                detalleAsiento.Credito = (decimal)data["Total"];
                                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                                asiento.DetalleAsiento.Add(detalleAsiento);
                                asiento.TotalCredito += detalleAsiento.Credito;
                            }
                        }
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al agregar el registro de facturación: ", ex);
                        throw new Exception("Se produjo un error guardando la información de la factura. Por favor consulte con su proveedor.");
                    }
                }
                dbContext.Commit();
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de las facturas: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeCompras()
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
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentasPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorPagarProveedores).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.OtraCondicionVenta).FirstOrDefault();
                        if (efectivoParam == null || cuentasPorPagarProveedoresParam == null || ivaPorPagarParam == null || otraCondicionVentaParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                        decTotalImpuesto = compra.Impuesto;
                        decSubTotalCompra = compra.Excento + compra.Gravado + compra.Descuento;
                        foreach (var detalleCompra in compra.DetalleCompra)
                        {
                            Producto producto = detalleCompra.Producto;
                            if (producto == null)
                                throw new Exception("El producto asignado al detalle de la compra no existe");
                            if (producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                                throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                            else if (producto.Tipo == StaticTipoProducto.Producto)
                            {
                                decimal decTotalPorLinea = detalleCompra.PrecioCosto * detalleCompra.Cantidad;
                                decTotalPorLinea = Math.Round(decTotalPorLinea - (compra.Descuento / decSubTotalCompra * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                decTotalInventario += decTotalPorLinea;
                                int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.IdLinea));
                                if (intExiste >= 0)
                                    dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + decTotalPorLinea;
                                else
                                {
                                    DataRow data = dtbInventarios.NewRow();
                                    data["IdLinea"] = producto.IdLinea;
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
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
                            if (lineaParam == null)
                                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                            detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                            detalleAsiento.Debito = (decimal)data["Total"];
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al agregar el registro de compra: ", ex);
                        throw new Exception("Se produjo un error agregando la información de la compra. Por favor consulte con su proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de las compras: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeEgresos()
        {
            try
            {
                var egresos = dbContext.EgresoRepository.Where(x => x.Nulo == false).ToList();
                foreach (Egreso egreso in egresos)
                {
                    ParametroContable efectivo = null;
                    ParametroContable egresoParam = null;
                    Asiento asiento = null;
                    try
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa);
                        if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
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
                        egresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeEgresos && x.IdProducto == cuentaEgreso.IdCuenta).FirstOrDefault();
                        if (egresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaEgreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = egresoParam.IdCuenta;
                        detalleAsiento.Debito = egreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = efectivo.IdCuenta;
                        detalleAsiento.Credito = egreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al agregar el registro de egreso: ", ex);
                        throw new Exception("Se produjo un error agregando la información del egreso. Por favor consulte con su proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de los egresos: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeIngresos()
        {
            try
            {
                var ingresos = dbContext.IngresoRepository.Where(x => x.Nulo == false).ToList();
                foreach (Ingreso ingreso in ingresos)
                {
                    ParametroContable efectivo = null;
                    ParametroContable ingresoParam = null;
                    Asiento asiento = null;
                    try
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Find(ingreso.IdEmpresa);
                        if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        efectivo = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
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
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        detalleAsiento.IdCuenta = efectivo.IdCuenta;
                        detalleAsiento.Debito = ingreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        ingresoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeIngresos && x.IdProducto == cuentaIngreso.IdCuenta).FirstOrDefault();
                        if (ingresoParam == null)
                            throw new BusinessException("No existe parametrización contable para la cuenta de ingresos " + cuentaIngreso.IdCuenta + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = ingresoParam.IdCuenta;
                        detalleAsiento.Credito = ingreso.Monto;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al agregar el registro de ingreso: ", ex);
                        throw new Exception("Se produjo un error agregando la información del ingreso. Por favor consulte con su proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de los ingresos: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeAbonosCxC()
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
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.GastoComisionTarjeta).FirstOrDefault();
                        cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarClientes).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorCobrarTarjetaParam == null || ivaPorPagarParam == null || gastoComisionParam == null || cuentaPorCobrarClientesParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                        movimiento.IdAsiento = 0;
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
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al aplicar el movimiento de una cuenta por cobrar: ", ex);
                        throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por cobrar. Por favor consulte con su proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de los ingresos: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void GenerarAsientosdeAbonosCxP()
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
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentaPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorPagarProveedores).FirstOrDefault();
                        if (efectivoParam == null || cuentaPorPagarProveedoresParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                        movimiento.IdAsiento = 0;
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
                        AgregarAsiento(asiento);
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
                        //_logger.LogError("Error al aplicar el movimiento de una cuenta por pagar: ", ex);
                        throw new Exception("Se produjo un error aplicando el movimiento de la cuenta por pagar. Por favor consulte con su proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al general los asientos contables de los ingresos: ", ex);
                throw new Exception("Se produjo un error generando los asientos contables. Por favor consulte con su proveedor.");
            }
        }

        public void AjustarSaldosCuentasdeMayor()
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
                //_logger.LogError("Error al realizar el ajuste de saldos contables: ", ex);
                throw new Exception("Se produjo un error ejecutando el ajuste de saldos contables. Por favor consulte con su proveedor.");
            }
        }
    }
}