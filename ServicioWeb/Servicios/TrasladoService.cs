using System;
using System.Linq;
using System.Collections.Generic;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;
using System.Data;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ITrasladoService
    {
        IList<LlaveDescripcion> ObtenerListadoSucursalDestino(int intIdEmpresa, int intIdSucursalOrigen);
        string AgregarTraslado(Traslado traslado);
        void AplicarTraslado(int intIdTraslado, int intIdUsuario);
        void AnularTraslado(int intIdTraslado, int intIdUsuario);
        Traslado ObtenerTraslado(int intIdTraslado);
        int ObtenerTotalListaTraslados(int intIdEmpresa, int intIdSucursalOrigen, int intIdTraslado);
        IList<TrasladoDetalle> ObtenerListaTraslados(int intIdEmpresa, int intIdSucursalOrigen, int numPagina, int cantRec, int intIdTraslado);
        IList<TrasladoDetalle> ObtenerListaTrasladosPorAplicar(int intIdEmpresa, int intIdSucursalDestino, int intIdTraslado);
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

        public IList<LlaveDescripcion> ObtenerListadoSucursalDestino(int intIdEmpresa, int intIdSucursalOrigen)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaSucursales = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal != intIdSucursalOrigen);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdSucursal, value.NombreSucursal);
                        listaSucursales.Add(item);
                    }
                    return listaSucursales;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de sucursales: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de sucursales. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarTraslado(Traslado traslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(traslado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    traslado.IdAsiento = 0;
                    dbContext.TrasladoRepository.Add(traslado);
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
            return traslado.IdTraslado.ToString();
        }

        public void AplicarTraslado(int intIdTraslado, int intIdUsuario)
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
                    Traslado traslado = dbContext.TrasladoRepository.Find(intIdTraslado);
                    if (traslado == null) throw new Exception("El registro de traslado por aplicar no existe.");
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
                    traslado.Aplicado = true;
                    traslado.IdAplicadoPor = intIdUsuario;
                    dbContext.NotificarModificacion(traslado);
                    foreach (var detalleTraslado in traslado.DetalleTraslado)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleTraslado.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle del traslado no existe");
                        if (producto.Tipo != StaticTipoProducto.Producto)
                            throw new BusinessException("El tipo de producto por trasladar no puede ser un servicio. Por favor verificar.");
                        ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == traslado.IdSucursalOrigen).FirstOrDefault();
                        if (existencias != null)
                        {
                            existencias.Cantidad -= detalleTraslado.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                        }
                        else
                        {
                            ExistenciaPorSucursal nuevoRegistro = new ExistenciaPorSucursal
                            {
                                IdEmpresa = traslado.IdEmpresa,
                                IdSucursal = traslado.IdSucursalOrigen,
                                IdProducto = detalleTraslado.IdProducto,
                                Cantidad = detalleTraslado.Cantidad * -1
                            };
                            dbContext.ExistenciaPorSucursalRepository.Add(nuevoRegistro);
                        }
                        MovimientoProducto movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = traslado.IdSucursalOrigen,
                            Fecha = DateTime.Now,
                            Referencia = traslado.Referencia.Substring(0, 100),
                            PrecioCosto = detalleTraslado.PrecioCosto,
                            Origen = "Salida por traslado entre sucursales",
                            Tipo = StaticTipoMovimientoProducto.Salida,
                            Cantidad = detalleTraslado.Cantidad
                        };
                        producto.MovimientoProducto.Add(movimiento);
                        existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == traslado.IdSucursalDestino).FirstOrDefault();
                        if (existencias != null)
                        {
                            existencias.Cantidad += detalleTraslado.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                        }
                        else
                        {
                            ExistenciaPorSucursal nuevoRegistro = new ExistenciaPorSucursal
                            {
                                IdEmpresa = traslado.IdEmpresa,
                                IdSucursal = traslado.IdSucursalDestino,
                                IdProducto = detalleTraslado.IdProducto,
                                Cantidad = detalleTraslado.Cantidad
                            };
                            dbContext.ExistenciaPorSucursalRepository.Add(nuevoRegistro);
                        }
                        movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = traslado.IdSucursalDestino,
                            Fecha = DateTime.Now,
                            Referencia = traslado.Referencia.Substring(0,100),
                            PrecioCosto = detalleTraslado.PrecioCosto,
                            Origen = "Ingreso por traslado entre sucursales",
                            Tipo = StaticTipoMovimientoProducto.Entrada,
                            Cantidad = detalleTraslado.Cantidad
                        };
                        producto.MovimientoProducto.Add(movimiento);
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
                        asiento = new Asiento
                        {
                            IdEmpresa = traslado.IdEmpresa,
                            Fecha = traslado.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0
                        };
                        asiento.Detalle = "Registro de traslado de mercancías entre sucursales.";
                        //Detalle asiento sucursal origne
                        DetalleAsiento detalleAsiento = new DetalleAsiento();
                        int intLineaDetalleAsiento = 1;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        trasladosParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Traslados && x.IdProducto == traslado.IdSucursalOrigen).FirstOrDefault();
                        if (trasladosParam == null)
                            throw new BusinessException("No existe parametrización contable para la sucursal origen " + traslado.IdSucursalOrigen + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = trasladosParam.IdCuenta;
                        detalleAsiento.Debito = traslado.Total;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalDebito += detalleAsiento.Debito;
                        foreach (DataRow data in dtbInventarios.Rows)
                        {
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            int intIdLinea = (int)data["IdLinea"];
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
                            if (lineaParam == null)
                                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                            detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                            detalleAsiento.Credito = (decimal)data["Total"];
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        //Detalle asiento sucursal destino
                        detalleAsiento = new DetalleAsiento();
                        intLineaDetalleAsiento = 2;
                        detalleAsiento.Linea = intLineaDetalleAsiento;
                        trasladosParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Traslados && x.IdProducto == traslado.IdSucursalDestino).FirstOrDefault();
                        if (trasladosParam == null)
                            throw new BusinessException("No existe parametrización contable para la sucursal destino " + traslado.IdSucursalOrigen + " y no se puede continuar. Por favor verificar.");
                        detalleAsiento.IdCuenta = trasladosParam.IdCuenta;
                        detalleAsiento.Credito = traslado.Total;
                        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                        asiento.DetalleAsiento.Add(detalleAsiento);
                        asiento.TotalCredito += detalleAsiento.Credito;
                        foreach (DataRow data in dtbInventarios.Rows)
                        {
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            int intIdLinea = (int)data["IdLinea"];
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
                            if (lineaParam == null)
                                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                            detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                            detalleAsiento.Debito = (decimal)data["Total"];
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
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
                    log.Error("Error al aplicar el registro de traslado: ", ex);
                    throw new Exception("Se produjo un error aplicando la información del traslado. Por favor consulte con su proveedor.");
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
                    if (traslado.Nulo) throw new Exception("El registro de traslado ya ha sido anulado.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(traslado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    traslado.Nulo = true;
                    traslado.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(traslado);
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

        public int ObtenerTotalListaTraslados(int intIdEmpresa, int intIdSucursalOrigen, int intIdTraslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaTraslados = dbContext.TrasladoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursalOrigen == intIdSucursalOrigen);
                    if (intIdTraslado > 0)
                        listaTraslados = listaTraslados.Where(x => x.IdTraslado == intIdTraslado);
                    return listaTraslados.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de traslado: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de traslados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<TrasladoDetalle> ObtenerListaTraslados(int intIdEmpresa, int intIdSucursalOrigen, int numPagina, int cantRec, int intIdTraslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTraslado = new List<TrasladoDetalle>();
                try
                {
                    var listado = dbContext.TrasladoRepository.Where(x => !x.Nulo && !x.Aplicado && x.IdEmpresa == intIdEmpresa && x.IdSucursalOrigen == intIdSucursalOrigen);
                    if (intIdTraslado > 0)
                        listado = listado.Where(x => x.IdTraslado == intIdTraslado);
                    listado.OrderByDescending(x => x.IdTraslado).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    foreach (var traslado in listado)
                    {
                        SucursalPorEmpresa sucursalDestino = dbContext.SucursalPorEmpresaRepository.Find(traslado.IdSucursalDestino);
                        TrasladoDetalle item = new TrasladoDetalle(traslado.IdTraslado, traslado.Fecha.ToString("dd/MM/yyyy"), sucursalDestino.NombreSucursal, traslado.Total);
                        listaTraslado.Add(item);
                    }
                    return listaTraslado;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de traslado: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de traslados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<TrasladoDetalle> ObtenerListaTrasladosPorAplicar(int intIdEmpresa, int intIdSucursalDestino, int intIdTraslado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTraslado = new List<TrasladoDetalle>();
                try
                {
                    var listado = dbContext.TrasladoRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursalDestino == intIdSucursalDestino);
                    if (intIdTraslado > 0)
                        listado = listado.Where(x => x.IdTraslado == intIdTraslado);
                    listado.OrderByDescending(x => x.IdTraslado).ToList();
                    foreach (var traslado in listado)
                    {
                        SucursalPorEmpresa sucursalOrigen = dbContext.SucursalPorEmpresaRepository.Find(traslado.IdSucursalOrigen);
                        TrasladoDetalle item = new TrasladoDetalle(traslado.IdTraslado, traslado.Fecha.ToString("dd/MM/yyyy"), sucursalOrigen.NombreSucursal, traslado.Total);
                        listaTraslado.Add(item);
                    }
                    return listaTraslado;
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
