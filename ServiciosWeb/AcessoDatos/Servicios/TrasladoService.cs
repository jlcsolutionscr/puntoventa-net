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

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface ITrasladoService
    {
        Sucursal AgregarSucursal(Sucursal sucursal);
        void ActualizarSucursal(Sucursal sucursal);
        void EliminarSucursal(int intIdSucursal);
        Sucursal ObtenerSucursal(int intIdSucursal);
        IEnumerable<Sucursal> ObtenerListaSucursales(int intIdEmpresa, string strNombre = "");
        Traslado AgregarTraslado(Traslado traslado);
        void ActualizarTraslado(Traslado traslado);
        void AnularTraslado(int intIdTraslado, int intIdUsuario);
        Traslado ObtenerTraslado(int intIdTraslado);
        int ObtenerTotalListaTraslados(int intIdEmpresa, int intIdTraslado = 0, string strNombre = "");
        IEnumerable<Traslado> ObtenerListaTraslados(int intIdEmpresa, int numPagina, int cantRec, int intIdTraslado = 0, string strNombre = "");
    }

    public class TrasladoService : ITrasladoService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TrasladoService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Traslados. Por favor consulte con su proveedor.");
            }
        }

        public Sucursal AgregarSucursal(Sucursal sucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(sucursal.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.SucursalRepository.Add(sucursal);
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
                    log.Error("Error al agregar la sucursal: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
            return sucursal;
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(sucursal.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(sucursal);
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
                    log.Error("Error al actualizar la sucursal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarSucursal(int intIdSucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Sucursal sucursal = dbContext.SucursalRepository.Find(intIdSucursal);
                    if (sucursal == null) throw new Exception("La sucursal por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(sucursal.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.SucursalRepository.Remove(sucursal);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar la sucursal: ", ex);
                    throw new BusinessException("No es posible eliminar la sucursal seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar la sucursal: ", ex);
                    throw new Exception("Se produjo un error eliminando la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public Sucursal ObtenerSucursal(int intIdSucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.SucursalRepository.Find(intIdSucursal);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la sucursal: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Sucursal> ObtenerListaSucursales(int intIdEmpresa, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaSucursales = dbContext.SucursalRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listaSucursales = listaSucursales.Where(x => x.Nombre.Contains(strNombre));
                    return listaSucursales.OrderBy(x => x.IdSucursal).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de sucursales: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de sucursales. Por favor consulte con su proveedor.");
                }
            }
        }

        public Traslado AgregarTraslado(Traslado traslado)
        {
            decimal decTotalInventario = 0;
            ParametroContable ivaPorPagarParam = null;
            ParametroContable efectivoParam = null;
            ParametroContable trasladosParam = null;
            ParametroContable lineaParam = null;
            DataTable dtbInventarios = new DataTable();
            dtbInventarios.Columns.Add("IdLinea", typeof(int));
            dtbInventarios.Columns.Add("Total", typeof(decimal));
            dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
            Asiento asiento = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(traslado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        if (ivaPorPagarParam == null || efectivoParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    traslado.IdAsiento = 0;
                    dbContext.TrasladoRepository.Add(traslado);
                    foreach (var detalleTraslado in traslado.DetalleTraslado)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleTraslado.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle del traslado no existe");
                        if (producto.Tipo == StaticTipoProducto.Servicio)
                            throw new BusinessException("El tipo de producto por trasladar no puede ser un servicio. Por favor verificar.");
                        else if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Referencia = traslado.NoDocumento,
                                Cantidad = detalleTraslado.Cantidad,
                                PrecioCosto = detalleTraslado.PrecioCosto
                            };
                            if (traslado.Tipo == 0)
                            {
                                movimiento.Origen = "Registro de traslado entrante";
                                movimiento.Tipo = StaticTipoMovimientoProducto.Entrada;
                                producto.Cantidad += detalleTraslado.Cantidad;
                            }
                            else
                            {
                                movimiento.Origen = "Registro de traslado saliente";
                                movimiento.Tipo = StaticTipoMovimientoProducto.Salida;
                                producto.Cantidad -= detalleTraslado.Cantidad;
                            }
                            producto.MovimientoProducto.Add(movimiento);
                            dbContext.NotificarModificacion(producto);
                            if (empresa.Contabiliza)
                            {
                                decimal decTotalPorLinea = Math.Round(detalleTraslado.PrecioCosto * detalleTraslado.Cantidad, 2, MidpointRounding.AwayFromZero);
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
                    }
                    if (empresa.Contabiliza)
                    {
                        decimal decTotalDiff = decTotalInventario - traslado.Total;
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
                            IdEmpresa = traslado.IdEmpresa,
                            Fecha = traslado.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0
                        };
                        if (traslado.Tipo == 0)
                            asiento.Detalle = "Registro de traslado de mercancía entrante. ";
                        else
                            asiento.Detalle = "Registro de traslado de mercancía saliente. ";
                        DetalleAsiento detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento += 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        trasladosParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Traslados & x.IdProducto == traslado.IdSucursal).FirstOrDefault();
                        if (trasladosParam == null)
                            throw new BusinessException("No existe parametrización contable para la sucursal " + traslado.IdSucursal + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = trasladosParam.IdCuenta;
                        if (traslado.Tipo == 0)
                            detalleAsiento.Credito = traslado.Total;
                        else
                            detalleAsiento.Debito = traslado.Total;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        if (traslado.Tipo == 0)
                            asiento.TotalCredito += detalleAsiento.Credito;
                        else
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
                            if (traslado.Tipo == 0)
                                detalleAsiento.Debito = (decimal)data["Total"];
                            else
                                detalleAsiento.Credito = (decimal)data["Total"];
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            if (traslado.Tipo == 0)
                                asiento.TotalDebito += detalleAsiento.Debito;
                            else
                                asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    if (asiento != null)
                    {
                        traslado.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(traslado);
                        asiento.Detalle += traslado.IdTraslado;
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
                    log.Error("Error al agregar el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
            return traslado;
        }

        public void ActualizarTraslado(Traslado traslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(traslado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(traslado);
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
                    log.Error("Error al actualizar el registro de traslado: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del traslado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularTraslado(int intIdTraslado, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Traslado traslado = dbContext.TrasladoRepository.Include("DetalleTraslado").FirstOrDefault(x => x.IdTraslado == intIdTraslado);
                    if (traslado == null) throw new Exception("El registro de traslado por anular no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(traslado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    traslado.Nulo = true;
                    traslado.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(traslado);
                    foreach (var detalleTraslado in traslado.DetalleTraslado)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleTraslado.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe");
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Referencia = traslado.NoDocumento,
                                Cantidad = detalleTraslado.Cantidad,
                                PrecioCosto = detalleTraslado.PrecioCosto
                            };
                            if (traslado.Tipo == 0)
                            {
                                movimiento.Origen = "Anulación registro de traslado entrante";
                                movimiento.Tipo = StaticTipoMovimientoProducto.Salida;
                                producto.Cantidad -= detalleTraslado.Cantidad;
                            }
                            else
                            {
                                movimiento.Origen = "Anulación registro de traslado saliente";
                                movimiento.Tipo = StaticTipoMovimientoProducto.Entrada;
                                producto.Cantidad += detalleTraslado.Cantidad;
                            }
                            producto.MovimientoProducto.Add(movimiento);
                            dbContext.NotificarModificacion(producto);
                        }
                    }
                    if (traslado.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, traslado.IdAsiento);
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
                    log.Error("Error al anular el registro de traslado: ", ex);
                    throw new Exception("Se produjo un error anulando el traslado. Por favor consulte con su proveedor.");
                }
            }
        }

        public Traslado ObtenerTraslado(int intIdTraslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TrasladoRepository.Include("DetalleTraslado.Producto").FirstOrDefault(x => x.IdTraslado == intIdTraslado);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de traslado: ", ex);
                    throw new Exception("Se produjo un error consultado la información del traslado. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaTraslados(int intIdEmpresa, int intIdTraslado = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaTraslados = dbContext.TrasladoRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdTraslado > 0)
                        listaTraslados = listaTraslados.Where(x => x.IdTraslado == intIdTraslado);
                    else if (!strNombre.Equals(string.Empty))
                        listaTraslados = listaTraslados.Where(x => x.Sucursal.Nombre.Contains(strNombre));
                    return listaTraslados.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de traslado: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de traslados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Traslado> ObtenerListaTraslados(int intIdEmpresa, int numPagina, int cantRec, int intIdTraslado = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaTraslados = dbContext.TrasladoRepository.Include("Sucursal").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdTraslado > 0)
                        listaTraslados = listaTraslados.Where(x => x.IdTraslado == intIdTraslado);
                    else if (!strNombre.Equals(string.Empty))
                        listaTraslados = listaTraslados.Where(x => x.Sucursal.Nombre.Contains(strNombre));
                    return listaTraslados.OrderByDescending(x => x.IdTraslado).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de traslado: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de traslados. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}
