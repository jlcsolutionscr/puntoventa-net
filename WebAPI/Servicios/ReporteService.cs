﻿using Microsoft.Reporting.NETCore;
using System.Globalization;
using System.Xml;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using LeandroSoftware.ServicioWeb.Utilitario;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IReporteService
    {
        IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoFactura();
        IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoCompra();
        List<ReporteDetalle> ObtenerReporteProformas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo);
        List<ReporteDetalle> ObtenerReporteApartados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo);
        List<ReporteDetalle> ObtenerReporteOrdenesServicio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo);
        List<ReporteDetalle> ObtenerReporteVentasPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago);
        List<ReporteDetalle> ObtenerReporteDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo);
        List<ReporteVentasPorVendedor> ObtenerReporteVentasPorVendedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdVendedor);
        List<ReporteDetalle> ObtenerReporteComprasPorProveedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago);
        List<ReporteCuentas> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, int intIdSucursal, int intIdCliente, bool bolActivas);
        List<ReporteCuentas> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, int intIdSucursal, int intIdProveedor, bool bolActivas);
        List<ReporteGrupoDetalle> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente);
        List<ReporteGrupoDetalle> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor);
        List<ReporteMovimientosBanco> ObtenerReporteMovimientosBanco(int intIdCuenta, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<ReporteEstadoResultados> ObtenerReporteEstadoResultados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<ReporteGrupoDetalle> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdSucursal, int idCuentaEgreso, string strFechaInicial, string strFechaFinal);
        List<ReporteGrupoDetalle> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdSucursal, int idCuentaIngreso, string strFechaInicial, string strFechaFinal);
        List<DescripcionValor> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<ReporteGrupoLineaDetalle> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdSucursal, int intIdLinea, string strFechaInicial, string strFechaFinal);
        List<ReporteProductoTransitorio> ObtenerReporteVentasProductoTransitorio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<DescripcionValor> ObtenerReporteCierreDeCaja(int intIdCierre);
        List<ReporteInventario> ObtenerReporteInventario(int intIdEmpresa, int intIdSucursal, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion);
        List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0);
        List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa, int intIdSucursal);
        List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal);
        List<ReporteEgreso> ObtenerReporteEgreso(int intIdEgreso);
        List<ReporteIngreso> ObtenerReporteIngreso(int intIdIngreso);
        List<ReporteDocumentoElectronico> ObtenerReporteDocumentosElectronicosEmitidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<ReporteDocumentoElectronico> ObtenerReporteDocumentosElectronicosRecibidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<ReporteResumenMovimiento> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        List<LlaveDescripcionValor> ObtenerReporteComparativoVentasPorPeriodo(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        void EnviarReporteVentasGenerales(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteVentasAnuladas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteResumenMovimientos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteDetalleIngresos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteDetalleEgresos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteDocumentosEmitidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteDocumentosRecibidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
        void EnviarReporteResumenMovimientosElectronicos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte);
    }

    public class ReporteService : IReporteService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory? _serviceScopeFactory;
        private static ICorreoService? _servicioCorreo;
        private static IConfiguracionGeneral? _config;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";
        private static Assembly assembly = Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Common.dll");

        public ReporteService(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory, ICorreoService servicioCorreo, IConfiguracionGeneral config)
        {
            try
            {
                _logger = logger;
                _serviceScopeFactory = serviceScopeFactory;
                _servicioCorreo = servicioCorreo;
                _config = config;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al inicializar el servicio: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error al inicializar el servicio de Reportería. Por favor consulte con su proveedor.");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoFactura()
        {
            IList<LlaveDescripcion> listado = null;
            try
            {
                listado = new List<LlaveDescripcion>();
                var condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoEfectivo, "Ventas de contado en efectivo");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario, "Ventas de contado mediante transferencia o depósito bancario");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoCheque, "Ventas de contado mediante cheque");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoTarjeta, "Ventas de contado mediante pago con tarjeta");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.Credito, "Ventas de crédito");
                listado.Add(condicionVenta);
                return listado;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al obtener el listado de formas de pago para facturación: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoCompra()
        {
            IList<LlaveDescripcion> listado = null;
            try
            {
                listado = new List<LlaveDescripcion>();
                var condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoEfectivo, "Compras de contado en efectivo");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario, "Compras de contado mediante transferencia o depósito bancario");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.ContadoCheque, "Compras de contado mediante cheque");
                listado.Add(condicionVenta);
                condicionVenta = new LlaveDescripcion(StaticReporteCondicionVentaFormaPago.Credito, "Compras de crédito");
                listado.Add(condicionVenta);
                return listado;
            }
            catch (Exception ex)
            {
                if (_logger != null) _logger.LogError("Error al obtener el listado de formas de pago para facturación: ", ex);
                if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                else throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
            }
        }

        public List<ReporteDetalle> ObtenerReporteProformas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    var detalleVentas = dbContext.ProformaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                        .Select(x => new { x.IdCliente, x.Nulo, x.ConsecProforma, x.Fecha, x.NombreCliente, x.Impuesto, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto });
                    foreach (var value in detalleVentas)
                    {
                        ReporteDetalle reporteLinea = new ReporteDetalle
                        {
                            Id = value.ConsecProforma,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Nombre = value.NombreCliente,
                            NoDocumento = "",
                            Impuesto = value.Impuesto,
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de proformas: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalle> ObtenerReporteApartados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    var detalleVentas = dbContext.ApartadoRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                        .Select(x => new { x.IdCliente, x.Nulo, x.ConsecApartado, x.Fecha, x.NombreCliente, x.Impuesto, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto });
                    foreach (var value in detalleVentas)
                    {
                        ReporteDetalle reporteLinea = new ReporteDetalle
                        {
                            Id = value.ConsecApartado,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Nombre = value.NombreCliente,
                            NoDocumento = "",
                            Impuesto = value.Impuesto,
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de apartados: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de apartados. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalle> ObtenerReporteOrdenesServicio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    var detalleVentas = dbContext.OrdenServicioRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                        .Select(x => new { x.IdCliente, x.Nulo, x.ConsecOrdenServicio, x.Fecha, x.NombreCliente, x.Impuesto, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto });
                    foreach (var value in detalleVentas)
                    {
                        ReporteDetalle reporteLinea = new ReporteDetalle
                        {
                            Id = value.ConsecOrdenServicio,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Nombre = value.NombreCliente,
                            NoDocumento = "",
                            Impuesto = value.Impuesto,
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de proformas: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalle> ObtenerReporteVentasPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    if (intTipoPago == -1)
                    {
                        var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                            .Select(x => new { x.IdCliente, x.Nulo, x.IdCondicionVenta, x.ConsecFactura, x.Fecha, x.NombreCliente, x.Impuesto, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto });
                        if (intIdCliente > 0)
                            detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                        foreach (var value in detalleVentas)
                        {
                            ReporteDetalle reporteLinea = new ReporteDetalle
                            {
                                Id = value.ConsecFactura,
                                Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                Nombre = value.NombreCliente,
                                NoDocumento = "",
                                Impuesto = value.Impuesto,
                                Total = value.Total
                            };
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito || !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Select(x => new { x.IdCliente, x.Nulo, x.IdCondicionVenta, x.ConsecFactura, x.Fecha, x.NombreCliente, x.Impuesto, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleVentas = detalleVentas.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdCliente > 0)
                                detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                            foreach (var value in detalleVentas)
                            {
                                ReporteDetalle reporteLinea = new ReporteDetalle
                                {
                                    Id = value.ConsecFactura,
                                    Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                    Nombre = value.NombreCliente,
                                    NoDocumento = "",
                                    Impuesto = value.Impuesto,
                                    Total = value.Total
                                };
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.DesglosePagoFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.ConsecFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.Impuesto, Total = z.y.MontoLocal, z.x.TotalCosto });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoEfectivo)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Efectivo);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTarjeta)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Tarjeta);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoCheque)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Cheque);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario);
                            }
                            if (intIdCliente > 0)
                                detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                            foreach (var value in detalleVentas)
                            {
                                ReporteDetalle reporteLinea = new ReporteDetalle
                                {
                                    Id = value.ConsecFactura,
                                    Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                    Nombre = value.NombreCliente,
                                    NoDocumento = "",
                                    Impuesto = value.Impuesto,
                                    Total = value.Total
                                };
                                listaReporte.Add(reporteLinea);
                            }
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Ventas por Cliente: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalle> ObtenerReporteDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    var detalleVentas = dbContext.DevolucionClienteRepository.Include("Factura").Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo);
                    if (intIdCliente > 0)
                        detalleVentas = detalleVentas.Where(x => x.Factura.IdCliente == intIdCliente);
                    foreach (var value in detalleVentas)
                    {
                        ReporteDetalle reporteLinea = new ReporteDetalle
                        {
                            Id = value.IdDevolucion,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Nombre = value.Factura.NombreCliente,
                            NoDocumento = "",
                            Impuesto = value.Impuesto,
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de devoluciones de clientes: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de devoluciones de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteVentasPorVendedor> ObtenerReporteVentasPorVendedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdVendedor)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteVentasPorVendedor> listaReporte = new List<ReporteVentasPorVendedor>();
                    var detalleVentas = dbContext.FacturaRepository.Include("Vendedor").Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == false)
                        .Select(x => new { x.IdVendedor, x.Vendedor.Nombre, x.Nulo, x.ConsecFactura, x.Fecha, NombreCliente = x.Cliente.Nombre, Total = x.Excento + x.Gravado + x.Exonerado + x.Impuesto, x.TotalCosto, x.Impuesto });
                    if (intIdVendedor > 0)
                        detalleVentas = detalleVentas.Where(x => x.IdVendedor == intIdVendedor);
                    foreach (var value in detalleVentas)
                    {
                        ReporteVentasPorVendedor reporteLinea = new ReporteVentasPorVendedor
                        {
                            NombreVendedor = value.Nombre,
                            IdFactura = value.ConsecFactura,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            NombreCliente = value.NombreCliente,
                            NoDocumento = "",
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Ventas por Vendedor: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalle> ObtenerReporteComprasPorProveedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalle> listaReporte = new List<ReporteDetalle>();
                    if (intTipoPago == -1)
                    {
                        var detalleCompras = dbContext.CompraRepository.Include("Proveedor").Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                            .Select(x => new { x.IdProveedor, x.Nulo, x.IdCondicionVenta, x.IdCompra, x.Fecha, NombreProveedor = x.Proveedor.Nombre, x.NoDocumento, x.Impuesto, Total = x.Excento + x.Gravado + x.Impuesto });
                        if (intIdProveedor > 0)
                            detalleCompras = detalleCompras.Where(x => x.IdProveedor == intIdProveedor);
                        foreach (var value in detalleCompras)
                        {
                            ReporteDetalle reporteLinea = new ReporteDetalle
                            {
                                Id = value.IdCompra,
                                Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                Nombre = value.NombreProveedor,
                                NoDocumento = value.NoDocumento,
                                Impuesto = value.Impuesto,
                                Total = value.Total
                            };
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito || !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleCompras = dbContext.CompraRepository.Include("Proveedor").Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Select(x => new { x.IdProveedor, x.Nulo, x.IdCondicionVenta, x.IdCompra, x.Fecha, NombreProveedor = x.Proveedor.Nombre, x.NoDocumento, x.Impuesto, Total = x.Excento + x.Gravado + x.Impuesto });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleCompras = detalleCompras.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdProveedor > 0)
                                detalleCompras = detalleCompras.Where(x => x.IdProveedor == intIdProveedor);
                            foreach (var value in detalleCompras)
                            {
                                ReporteDetalle reporteLinea = new ReporteDetalle
                                {
                                    Id = value.IdCompra,
                                    Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                    Nombre = value.NombreProveedor,
                                    NoDocumento = value.NoDocumento,
                                    Impuesto = value.Impuesto,
                                    Total = value.Total
                                };
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            var detalleCompras = dbContext.CompraRepository.Include("Proveedor").Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.y.IdFormaPago, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = z.y.MontoLocal });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoEfectivo)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Efectivo);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTarjeta)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Tarjeta);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoCheque)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Cheque);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario);
                            }
                            if (intIdProveedor > 0)
                                detalleCompras = detalleCompras.Where(x => x.IdProveedor == intIdProveedor);
                            foreach (var value in detalleCompras)
                            {
                                ReporteDetalle reporteLinea = new ReporteDetalle
                                {
                                    Id = value.IdCompra,
                                    Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                                    Nombre = value.NombreProveedor,
                                    NoDocumento = value.NoDocumento,
                                    Impuesto = value.Impuesto,
                                    Total = value.Total
                                };
                                listaReporte.Add(reporteLinea);
                            }
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Compras por Proveedor: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCuentas> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, int intIdSucursal, int intIdCliente, bool bolActivas)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteCuentas> listaReporte = new List<ReporteCuentas>();
                    var detalleCxCClientes = dbContext.CuentaPorCobrarRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, x => x.IdPropietario, y => y.IdCliente, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxC, z.x.Referencia, z.x.Fecha, z.x.Plazo, z.x.Total, z.x.Saldo });
                    if (bolActivas)
                        detalleCxCClientes = detalleCxCClientes.Where(x => x.Saldo > 0);
                    else
                        detalleCxCClientes = detalleCxCClientes.Where(x => x.Saldo <= 0);
                    if (intIdCliente > 0)
                        detalleCxCClientes = detalleCxCClientes.Where(x => x.IdPropietario == intIdCliente);
                    foreach (var value in detalleCxCClientes)
                    {
                        ReporteCuentas reporteLinea = new ReporteCuentas
                        {
                            IdPropietario = value.IdPropietario,
                            Nombre = value.Nombre,
                            IdCuenta = value.IdCxC,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Plazo = value.Plazo,
                            FechaVence = value.Fecha.AddDays(value.Plazo).ToString("dd/MM/yyyy"),
                            Descripcion = "Cuenta por cobrar de factura " + value.Referencia,
                            Referencia = value.Referencia,
                            Total = value.Total,
                            Saldo = value.Saldo
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Cuentas por Cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCuentas> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, int intIdSucursal, int intIdProveedor, bool bolActivas)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteCuentas> listaReporte = new List<ReporteCuentas>();
                    var detalleCxPProveedores = dbContext.CuentaPorPagarRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, x => x.IdPropietario, y => y.IdProveedor, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxP, z.x.Fecha, z.x.Plazo, z.x.Referencia, z.x.Total, z.x.Saldo });
                    if (bolActivas)
                        detalleCxPProveedores = detalleCxPProveedores.Where(x => x.Saldo > 0);
                    else
                        detalleCxPProveedores = detalleCxPProveedores.Where(x => x.Saldo <= 0);
                    if (intIdProveedor > 0)
                        detalleCxPProveedores = detalleCxPProveedores.Where(x => x.IdPropietario == intIdProveedor);
                    foreach (var value in detalleCxPProveedores)
                    {
                        ReporteCuentas reporteLinea = new ReporteCuentas
                        {
                            IdPropietario = value.IdPropietario,
                            Nombre = value.Nombre,
                            IdCuenta = value.IdCxP,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Plazo = value.Plazo,
                            FechaVence = value.Fecha.AddDays(value.Plazo).ToString("dd/MM/yyyy"),
                            Descripcion = "Cuenta por pagar de compra " + value.Referencia,
                            Referencia = value.Referencia,
                            Total = value.Total,
                            Saldo = value.Saldo
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Cuentas por Pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteGrupoDetalle> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteGrupoDetalle> listaReporte = new List<ReporteGrupoDetalle>();
                    var cxcClientes = dbContext.CuentaPorCobrarRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Nulo == false && a.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, a => a.IdPropietario, b => b.IdCliente, (a, b) => new { a, b })
                        .Join(dbContext.MovimientoCuentaPorCobrarRepository, a => a.a.IdCxC, d => d.IdCxC, (b, c) => new { b, c }).Where(s => s.c.IdSucursal == intIdSucursal && !s.c.Nulo && s.c.Fecha >= datFechaInicial && s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.a.IdPropietario, d.b.a.Referencia, d.b.b.Nombre, d.b.a.IdCxC, d.b.a.Total, d.b.a.Saldo, d.c.IdMovCxC, d.c.Fecha, d.c.Tipo, d.c.Monto });
                    if (intIdCliente > 0)
                        cxcClientes = cxcClientes.Where(a => a.IdPropietario == intIdCliente);
                    foreach (var value in cxcClientes)
                    {
                        ReporteGrupoDetalle reporteLinea = new ReporteGrupoDetalle
                        {
                            Descripcion = value.Nombre,
                            Id = value.IdMovCxC,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Detalle = "Cuenta por cobrar de factura " + value.Referencia,
                            Total = value.Monto
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Movimientos de Cuentas por Cobrar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteGrupoDetalle> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteGrupoDetalle> listaReporte = new List<ReporteGrupoDetalle>();
                    var cxpProveedores = dbContext.CuentaPorPagarRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Nulo == false && a.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, a => a.IdPropietario, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.MovimientoCuentaPorPagarRepository, a => a.a.IdCxP, d => d.IdCxP, (b, c) => new { b, c }).Where(s => s.c.IdSucursal == intIdSucursal && !s.c.Nulo && s.c.Fecha >= datFechaInicial && s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.a.IdPropietario, d.b.a.Referencia, d.b.b.Nombre, d.c.IdCxP, d.b.a.Total, d.b.a.Saldo, d.c.IdMovCxP, d.c.Fecha, d.c.Recibo, d.c.Tipo, d.c.Monto });
                    if (intIdProveedor > 0)
                        cxpProveedores = cxpProveedores.Where(a => a.IdPropietario == intIdProveedor);
                    foreach (var value in cxpProveedores)
                    {
                        ReporteGrupoDetalle reporteLinea = new ReporteGrupoDetalle
                        {
                            Descripcion = value.Nombre,
                            Id = value.IdMovCxP,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Detalle = "Cuenta por pagar de compra " + value.Referencia,
                            Total = value.Monto
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Movimientos de Cuentas por Pagar: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosBanco> ObtenerReporteMovimientosBanco(int intIdCuenta, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosBanco> listaReporte = new List<ReporteMovimientosBanco>();
                    var movimientoBanco = dbContext.MovimientoBancoRepository.Where(s => s.IdCuenta == intIdCuenta && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaBancoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .Join(dbContext.TipoMovimientoBancoRepository, a => a.x.IdTipo, b => b.IdTipoMov, (a, b) => new { a, b })
                        .Select(z => new { z.a.x.IdMov, z.a.y.IdCuenta, NombreCuenta = z.a.y.Descripcion, z.a.x.Fecha, z.a.x.Numero, z.a.x.Beneficiario, z.a.x.Descripcion, z.b.DebeHaber, DescTipo = z.a.y.Descripcion, Total = z.a.x.Monto, z.a.x.SaldoAnterior })
                        .OrderBy(x => x.IdMov).ThenBy(x => x.Fecha);
                    foreach (var value in movimientoBanco)
                    {
                        ReporteMovimientosBanco reporteLinea = new ReporteMovimientosBanco
                        {
                            IdMov = value.IdMov,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            IdCuenta = value.IdCuenta,
                            NombreCuenta = value.NombreCuenta,
                            SaldoAnterior = value.SaldoAnterior,
                            Numero = value.Numero,
                            Beneficiario = value.Beneficiario,
                            Descripcion = value.Descripcion,
                            Tipo = value.DescTipo
                        };
                        if (value.DebeHaber.Equals("D"))
                        {
                            reporteLinea.Debito = value.Total;
                            reporteLinea.Credito = 0;
                            reporteLinea.Saldo = value.SaldoAnterior - value.Total;
                        }
                        else
                        {
                            reporteLinea.Debito = 0;
                            reporteLinea.Credito = value.Total;
                            reporteLinea.Saldo = value.SaldoAnterior + value.Total;
                        }
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Movimientos Bancarios: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de movimientos bancarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteEstadoResultados> ObtenerReporteEstadoResultados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteEstadoResultados> listaReporte = new List<ReporteEstadoResultados>();
                    var grupoFacturas = dbContext.FacturaRepository.Join(dbContext.DesglosePagoFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa && s.x.Nulo == false && s.x.Fecha >= datFechaInicial && s.x.Fecha <= datFechaFinal)
                        .GroupBy(x => x.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });
                    foreach (var eachFactura in grupoFacturas)
                    {
                        string strTipo = "";
                        if (eachFactura.tipopago == StaticFormaPago.Efectivo)
                            strTipo = " DE CONTADO";
                        else if (eachFactura.tipopago == StaticFormaPago.Cheque)
                            strTipo = " CON CHEQUE";
                        else if (eachFactura.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                            strTipo = " CON DEPOSITO BANCARIO";
                        else if (eachFactura.tipopago == StaticFormaPago.Tarjeta)
                            strTipo = " CON TARJETA";
                        else
                            strTipo = " OTRAS FORMAS DE PAGO";
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("VENTAS" + strTipo, "Ingresos", eachFactura.total);
                        listaReporte.Add(reporteLinea);
                    }
                    var ingreso = dbContext.IngresoRepository.Where(w => w.IdEmpresa == intIdEmpresa && w.Nulo == false && w.Fecha >= datFechaInicial && w.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .GroupBy(a => a.y.Descripcion)
                        .Select(a => new { Total = a.Sum(b => b.x.Monto), Desc = a.Key });
                    foreach (var value in ingreso)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados(value.Desc.ToUpper(), "Ingresos", value.Total);
                        listaReporte.Add(reporteLinea);
                    }
                    var abonosCxC = dbContext.MovimientoCuentaPorCobrarRepository.Join(dbContext.DesglosePagoMovimientoCuentaPorCobrarRepository, x => x.IdMovCxC, y => y.IdMovCxC, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa && s.x.Nulo == false && s.x.Fecha >= datFechaInicial && s.x.Fecha <= datFechaFinal)
                        .GroupBy(x => x.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });
                    foreach (var eachAbono in abonosCxC)
                    {
                        string strTipo = "";
                        if (eachAbono.tipopago == StaticFormaPago.Efectivo)
                            strTipo = " DE CONTADO";
                        else if (eachAbono.tipopago == StaticFormaPago.Cheque)
                            strTipo = " CON CHEQUE";
                        else if (eachAbono.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                            strTipo = " CON DEPOSITO BANCARIO";
                        else if (eachAbono.tipopago == StaticFormaPago.Tarjeta)
                            strTipo = " CON TARJETA";
                        else
                            strTipo = " OTRAS FORMAS DE PAGO";
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("ABONO CXC" + strTipo, "Ingresos", eachAbono.total);
                        listaReporte.Add(reporteLinea);
                    }
                    if (grupoFacturas.Count() == 0 && ingreso.Count() == 0 && abonosCxC.Count() == 0)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("NO HAY REGISTROS", "Ingresos", 0);
                        listaReporte.Add(reporteLinea);
                    }
                    var grupoCompras = dbContext.CompraRepository.Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa && s.x.Nulo == false && s.x.Fecha >= datFechaInicial && s.x.Fecha <= datFechaFinal)
                        .GroupBy(g => g.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });
                    if (grupoCompras.Count() > 0)
                    {
                        foreach (var eachCompra in grupoCompras)
                        {
                            string strTipo = "";
                            if (eachCompra.tipopago == StaticFormaPago.Efectivo)
                                strTipo = " DE CONTADO";
                            else if (eachCompra.tipopago == StaticFormaPago.Cheque)
                                strTipo = " CON CHEQUE";
                            else if (eachCompra.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                                strTipo = " CON DEPOSITO BANCARIO";
                            else if (eachCompra.tipopago == StaticFormaPago.Tarjeta)
                                strTipo = " CON TARJETA";
                            else
                                strTipo = " OTRAS FORMAS DE PAGO";
                            ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("COMPRAS" + strTipo, "Egresos", eachCompra.total);
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    var egreso = dbContext.EgresoRepository.Where(w => w.IdEmpresa == intIdEmpresa && w.Nulo == false && w.Fecha >= datFechaInicial && w.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .GroupBy(a => a.y.Descripcion)
                        .Select(a => new { Total = a.Sum(b => b.x.Monto), Desc = a.Key });
                    foreach (var value in egreso)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados(value.Desc.ToUpper(), "Egresos", value.Total);
                        listaReporte.Add(reporteLinea);
                    }
                    var abonosCxP = dbContext.MovimientoCuentaPorPagarRepository.Join(dbContext.DesglosePagoMovimientoCuentaPorPagarRepository, x => x.IdMovCxP, y => y.IdMovCxP, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa && s.x.Nulo == false && s.x.Fecha >= datFechaInicial && s.x.Fecha <= datFechaFinal)
                        .GroupBy(x => x.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });
                    foreach (var eachAbono in abonosCxP)
                    {
                        string strTipo = "";
                        if (eachAbono.tipopago == StaticFormaPago.Efectivo)
                            strTipo = " DE CONTADO";
                        else if (eachAbono.tipopago == StaticFormaPago.Cheque)
                            strTipo = " CON CHEQUE";
                        else if (eachAbono.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                            strTipo = " CON DEPOSITO BANCARIO";
                        else if (eachAbono.tipopago == StaticFormaPago.Tarjeta)
                            strTipo = " CON TARJETA";
                        else
                            strTipo = " OTRAS FORMAS DE PAGO";
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("ABONO CXP" + strTipo, "Egresos", eachAbono.total);
                        listaReporte.Add(reporteLinea);
                    }
                    if (grupoCompras.Count() == 0 && egreso.Count() == 0 && abonosCxP.Count() == 0)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados("NO HAY REGISTROS", "Egresos", 0);
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Estado de Resultados: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de estado de resultados. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteGrupoDetalle> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdSucursal, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteGrupoDetalle> listaReporte = new List<ReporteGrupoDetalle>();
                    var egreso = dbContext.EgresoRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdEgreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in egreso)
                    {
                        if (intIdCuentaEgreso > 0 && value.IdCuenta != intIdCuentaEgreso)
                            continue;
                        ReporteGrupoDetalle reporteLinea = new ReporteGrupoDetalle
                        {
                            Id = value.IdEgreso,
                            Descripcion = value.Descripcion,
                            Detalle = value.Detalle,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Detalle de Egresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteGrupoDetalle> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdSucursal, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteGrupoDetalle> listaReporte = new List<ReporteGrupoDetalle>();
                    var ingreso = dbContext.IngresoRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdIngreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in ingreso)
                    {
                        if (intIdCuentaIngreso > 0 && value.IdCuenta != intIdCuentaIngreso)
                            continue;
                        ReporteGrupoDetalle reporteLinea = new ReporteGrupoDetalle
                        {
                            Id = value.IdIngreso,
                            Descripcion = value.Descripcion,
                            Detalle = value.Detalle,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Detalle de ingresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<DescripcionValor> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<DescripcionValor> listaReporte = new List<DescripcionValor>();
                    var ventasResumen = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .GroupBy(x => x.y.Descripcion)
                        .Select(sf => new { NombreLinea = sf.Key, Total = sf.Sum(z => (z.x.x.y.Cantidad - z.x.x.y.CantDevuelto) * z.x.x.y.PrecioVenta * (1 + (z.x.x.y.PorcentajeIVA / 100))) });
                    foreach (var value in ventasResumen)
                    {
                        DescripcionValor reporteLinea = new DescripcionValor
                        {
                            Descripcion = value.NombreLinea,
                            Valor = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Resumen de Ventas por Línea: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea resumido. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteGrupoLineaDetalle> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdSucursal, int intIdLinea, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteGrupoLineaDetalle> listaReporte = new List<ReporteGrupoLineaDetalle>();
                    var ventasDetalle = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y });
                    if (intIdLinea > 0)
                        ventasDetalle = ventasDetalle.Where(x => x.y.IdLinea == intIdLinea);
                    var listado = ventasDetalle.GroupBy(x => new { NombreLinea = x.y.Descripcion, x.x.y.Codigo, x.x.x.y.Descripcion })
                        .Select(sf => new { sf.Key.NombreLinea, sf.Key.Codigo, Cantidad = sf.Sum(z => z.x.x.y.Cantidad - z.x.x.y.CantDevuelto), sf.Key.Descripcion, Total = sf.Sum(z => (z.x.x.y.Cantidad - z.x.x.y.CantDevuelto) * z.x.x.y.PrecioVenta * (1 + (z.x.x.y.PorcentajeIVA / 100))) });
                    foreach (var value in listado)
                    {
                        ReporteGrupoLineaDetalle reporteLinea = new ReporteGrupoLineaDetalle
                        {
                            NombreLinea = value.NombreLinea,
                            Codigo = value.Codigo,
                            Cantidad = value.Cantidad,
                            Descripcion = value.Descripcion,
                            Total = value.Total
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Detalle de Ventas por Línea: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea detallado. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteProductoTransitorio> ObtenerReporteVentasProductoTransitorio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteProductoTransitorio> listaReporte = new List<ReporteProductoTransitorio>();
                    var ventasDetalle = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Where(x => x.y.Tipo == 4);
                    foreach (var value in ventasDetalle)
                    {
                        ReporteProductoTransitorio reporteLinea = new ReporteProductoTransitorio
                        {
                            IdFact = value.x.x.ConsecFactura,
                            Fecha = value.x.x.Fecha.ToString("dd/MM/yyyy"),
                            IdProducto = value.y.IdProducto,
                            Descripcion = value.x.y.Descripcion
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de detalle de ventas de producto transitorio: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte detallado de ventas de producto transitorio. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<DescripcionValor> ObtenerReporteCierreDeCaja(int intIdCierre)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<DescripcionValor> listaReporte = new List<DescripcionValor>();
                    var datosCierre = dbContext.CierreCajaRepository.Where(a => a.IdCierre == intIdCierre).FirstOrDefault();
                    Empresa empresa = dbContext.EmpresaRepository.AsNoTracking().Include("RolePorEmpresa").Where(x => x.IdEmpresa == datosCierre.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    decimal decTotalIngresosEfectivo = datosCierre.AdelantosApartadoEfectivo + datosCierre.AdelantosOrdenEfectivo + datosCierre.VentasEfectivo + datosCierre.PagosCxCEfectivo + datosCierre.IngresosEfectivo;
                    decimal decTotalEgresosEfectivo = datosCierre.ComprasEfectivo + datosCierre.PagosCxPEfectivo + datosCierre.EgresosEfectivo;
                    decimal decTotalFondoCaja = datosCierre.FondoInicio + decTotalIngresosEfectivo - decTotalEgresosEfectivo;
                    DescripcionValor reporteLinea = new DescripcionValor("Fondo de inicio de caja", datosCierre.FondoInicio);
                    listaReporte.Add(reporteLinea);
                    if (empresa.RolePorEmpresa.Find(x => x.IdRole == 201) != null)
                    {
                        reporteLinea = new DescripcionValor("Adelantos órdenes de servicio en efectivo", datosCierre.AdelantosOrdenEfectivo);
                        listaReporte.Add(reporteLinea);
                    }
                    if (empresa.RolePorEmpresa.Find(x => x.IdRole == 202) != null)
                    {
                        reporteLinea = new DescripcionValor("Adelantos apartados en efectivo", datosCierre.AdelantosApartadoEfectivo);
                        listaReporte.Add(reporteLinea);
                    }
                    reporteLinea = new DescripcionValor("Ventas de bienes y/o servicios en efectivo", datosCierre.VentasEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Pagos a cuentas por cobrar en efectivo", datosCierre.PagosCxCEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Registro de ingresos en efectivo", datosCierre.IngresosEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Total de ingresos en efectivo", decTotalIngresosEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Compras de bienes y/o servicios en efectivo", datosCierre.ComprasEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Pagos a cuentas por pagar en efectivo", datosCierre.PagosCxPEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Registro de egresos en efectivo", datosCierre.EgresosEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Total de egresos en efectivo", decTotalEgresosEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Total de efectivo en caja", decTotalFondoCaja);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Monto para retirar de fondo de caja", datosCierre.RetiroEfectivo);
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new DescripcionValor("Monto de próximo inicio de caja", decTotalFondoCaja - datosCierre.RetiroEfectivo);
                    listaReporte.Add(reporteLinea);
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de cierre de Caja: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de cierre de caja. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteInventario> ObtenerReporteInventario(int intIdEmpresa, int intIdSucursal, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteInventario> listaReporte = new List<ReporteInventario>();
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (bolFiltraActivos)
                        listaProductos = listaProductos.Where(x => x.Activo);
                    if (!bolIncluyeServicios)
                        listaProductos = listaProductos.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    else if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    else if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (bolFiltraExistencias)
                    {
                        var listado = listaProductos.Join(dbContext.ExistenciaPorSucursalRepository, x => x.IdProducto, y => y.IdProducto, (x, y) => new { x, y }).Where(x => x.y.IdEmpresa == intIdEmpresa && x.y.IdSucursal == intIdSucursal && x.y.Cantidad > 0).OrderBy(x => x.x.Codigo).ToList();
                        foreach (var value in listado)
                        {
                            ReporteInventario item = new ReporteInventario(value.x.IdProducto, value.x.Codigo, value.x.Descripcion, value.y.Cantidad, value.x.PrecioCosto, value.x.PrecioVenta1);
                            listaReporte.Add(item);
                        }
                    }
                    else
                    {
                        var listado = listaProductos.ToList();
                        foreach (var value in listado)
                        {
                            var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdProducto == value.IdProducto).FirstOrDefault();
                            decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                            ReporteInventario item = new ReporteInventario(value.IdProducto, value.Codigo, value.Descripcion, decCantidad, value.PrecioCosto, value.PrecioVenta1);
                            listaReporte.Add(item);
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Inventario: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de Inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosContables> listaReporte = new List<ReporteMovimientosContables>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, b => b.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial && x.d.Fecha <= datFechaFinal && x.d.Nulo == false)
                        .GroupBy(x => x.c.a.Descripcion)
                        .Select(a => new { TotalDebito = a.Sum(b => b.c.b.Debito), TotalCredito = a.Sum(b => b.c.b.Credito), Descripcion = a.Key });
                    foreach (var value in listaCuentas)
                    {
                        ReporteMovimientosContables reporteLinea = new ReporteMovimientosContables
                        {
                            Descripcion = value.Descripcion,
                            SaldoDebe = value.TotalDebito,
                            SaldoHaber = value.TotalCredito
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de movimientos contables: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de movimientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteBalanceComprobacion> listaReporte = new List<ReporteBalanceComprobacion>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReporteBalanceComprobacion reporteLinea = new ReporteBalanceComprobacion
                        {
                            IdCuenta = value.IdCuenta,
                            Descripcion = value.Descripcion
                        };
                        if (intMes > 0 && intAnnio > 0)
                            decSaldo = dbContext.SaldoMensualContableRepository.Where(x => x.Mes == intMes && x.Annio == intAnnio && x.IdCuenta == value.IdCuenta).Select(a => a.SaldoFinMes).FirstOrDefault();
                        else
                            decSaldo = value.SaldoActual;
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            reporteLinea.SaldoDebe = decSaldo;
                            reporteLinea.SaldoHaber = 0;
                        }
                        else
                        {
                            reporteLinea.SaldoDebe = 0;
                            reporteLinea.SaldoHaber = decSaldo;
                        }
                        if (decSaldo != 0)
                            listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa, int intIdSucursal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReportePerdidasyGanancias> listaReporte = new List<ReportePerdidasyGanancias>();
                    DateTime datFechaActual = Validador.ObtenerFechaHoraCostaRica();
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true && x.SaldoActual != 0 && x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReportePerdidasyGanancias reporteLinea = new ReportePerdidasyGanancias
                        {
                            Descripcion = value.Descripcion,
                            IdTipoCuenta = value.IdTipoCuenta
                        };
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                            reporteLinea.DescGrupo = "Cuentas de Egresos";
                        else
                            reporteLinea.DescGrupo = "Cuentas de Ingresos";
                        decSaldo = value.SaldoActual;
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            reporteLinea.SaldoDebe = decSaldo;
                            reporteLinea.SaldoHaber = 0;
                        }
                        else
                        {
                            reporteLinea.SaldoDebe = 0;
                            reporteLinea.SaldoHaber = decSaldo;
                        }
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalleMovimientosCuentasDeBalance> listaReporte = new List<ReporteDetalleMovimientosCuentasDeBalance>();
                    var cuentaDeBalance = dbContext.CatalogoContableRepository.Where(a => a.IdCuenta == intIdCuentaGrupo).FirstOrDefault();
                    int annioSaldoAnterior = datFechaInicial.Year;
                    int mesSaldoAnterior = datFechaInicial.Month - 1;
                    if (mesSaldoAnterior == 1)
                    {
                        annioSaldoAnterior -= 1;
                        mesSaldoAnterior = 12;
                    }
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdCuentaGrupo == intIdCuentaGrupo)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial && x.d.Fecha <= datFechaFinal && x.d.Nulo == false)
                        .OrderBy(x => x.d.IdAsiento)
                        .Select(a => new { a.c.a.IdCuenta, a.c.a.Descripcion, a.c.b.SaldoAnterior, a.d.Fecha, a.d.Detalle, a.c.b.Debito, a.c.b.Credito }).OrderBy(a => a.IdCuenta).ThenBy(a => a.Fecha).ToList();
                    foreach (var value in listaCuentas)
                    {
                        ReporteDetalleMovimientosCuentasDeBalance reporteLinea = new ReporteDetalleMovimientosCuentasDeBalance
                        {
                            DescCuentaBalance = cuentaDeBalance.Descripcion,
                            Descripcion = value.Descripcion,
                            SaldoInicial = value.SaldoAnterior,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Detalle = value.Detalle,
                            Debito = value.Debito,
                            Credito = value.Credito
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de detalle del balance de comprobación: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle del balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteEgreso> ObtenerReporteEgreso(int intIdEgreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteEgreso> listaReporte = new List<ReporteEgreso>();
                    var datosEgreso = dbContext.EgresoRepository.Where(a => a.IdEgreso == intIdEgreso);
                    foreach (var value in datosEgreso)
                    {
                        ReporteEgreso reporteLinea = new ReporteEgreso
                        {
                            IdEgreso = value.IdEgreso,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            Detalle = value.Detalle,
                            Beneficiario = value.Beneficiario,
                            Monto = value.Monto,
                            MontoEnLetras = Utilitario.Validador.NumeroALetras((double)value.Monto)
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Egreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de Egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteIngreso> ObtenerReporteIngreso(int intIdIngreso)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReporteIngreso> listaReporte = new List<ReporteIngreso>();
                    var datosIngreso = dbContext.IngresoRepository.Where(a => a.IdIngreso == intIdIngreso);
                    foreach (var value in datosIngreso)
                    {
                        ReporteIngreso reporteLinea = new ReporteIngreso
                        {
                            IdIngreso = value.IdIngreso,
                            Fecha = value.Fecha.ToString("dd/MM/yyyy"),
                            RecibidoDe = value.RecibidoDe,
                            Detalle = value.Detalle,
                            Monto = value.Monto,
                            MontoEnLetras = Utilitario.Validador.NumeroALetras((double)value.Monto)
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Ingreso: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de Ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteDocumentosElectronicosEmitidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasEmitidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.IdSucursal == intIdSucursal && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 1, 2, 3, 4 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasEmitidas)
                    {
                        string strNombreReceptor = documento.NombreReceptor;
                        string strIdentificacion = documento.IdentificacionReceptor;
                        XmlDocument documentoXml = new XmlDocument();
                        MemoryStream ms = new MemoryStream(documento.DatosDocumento);
                        documentoXml.Load(ms);
                        XmlNode resumenFacturaNode = documentoXml.GetElementsByTagName("ResumenFactura").Item(0);
                        decimal decTotalImpuesto = resumenFacturaNode["TotalImpuesto"] != null && resumenFacturaNode["TotalImpuesto"].ChildNodes.Count > 0 ? decimal.Parse(resumenFacturaNode["TotalImpuesto"].InnerText) : 0;
                        decimal decTotal = decimal.Parse(resumenFacturaNode["TotalComprobante"].InnerText);
                        string strMoneda = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? resumenFacturaNode["CodigoTipoMoneda"]["CodigoMoneda"].InnerText : "CRC";
                        if (strMoneda != "CRC")
                        {
                            decimal decTipoCambio = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? decimal.Parse(resumenFacturaNode["CodigoTipoMoneda"]["TipoCambio"].InnerText) : 1;
                            decTotal = decTotal * decTipoCambio;
                            decTotalImpuesto = decTotalImpuesto * decTipoCambio;
                        }
                        ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico
                        {
                            TipoDocumento = documento.IdTipoDocumento == 1 ? "FACTURA ELECTRONICA" : documento.IdTipoDocumento == 2 ? "NOTA DE DEBITO" : documento.IdTipoDocumento == 3 ? "NOTA DE CREDITO" : "TIQUETE ELECTRONICO",
                            ClaveNumerica = documento.ClaveNumerica,
                            Consecutivo = documento.Consecutivo,
                            Fecha = documento.Fecha.ToString("dd/MM/yyyy"),
                            Nombre = strNombreReceptor,
                            Identificacion = strIdentificacion,
                            Moneda = strMoneda,
                            Impuesto = decTotalImpuesto,
                            Total = decTotal
                        };
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte.OrderBy(x => x.TipoDocumento).ThenBy(x => x.Fecha).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de documentos emitidos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos emitidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteDocumentosElectronicosRecibidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasRecibidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.IdSucursal == intIdSucursal && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 5, 8 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasRecibidas)
                    {
                        if (documento.DatosDocumentoOri != null || documento.IdTipoDocumento == 8)
                        {
                            XmlDocument documentoXml = new XmlDocument();
                            MemoryStream ms = new MemoryStream(documento.IdTipoDocumento == 8 ? documento.DatosDocumento : documento.DatosDocumentoOri);
                            string strIdentificacion = documento.IdentificacionEmisor;
                            documentoXml.Load(ms);
                            decimal decTotalImpuesto = 0;
                            decimal decTotal = 0;
                            string strCodigoMoneda = "CRC";
                            decimal decTipoDeCambio = 1;
                            string strNombreEmisor = "INFORMACION NO DISPONIBLE";
                            if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                                strNombreEmisor = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalImpuesto").Count > 0)
                                decTotalImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("TotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                                strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                            if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                                decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (strCodigoMoneda != "CRC")
                            {
                                decTotal = decTotal * decTipoDeCambio;
                                decTotalImpuesto = decTotalImpuesto * decTipoDeCambio;
                            }
                            ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico
                            {
                                TipoDocumento = documentoXml.DocumentElement.Name == "FacturaElectronica" ? "FACTURA ELECTRONICA" : documentoXml.DocumentElement.Name == "NotaCreditoElectronica" ? "NOTA DE CREDITO" : documentoXml.DocumentElement.Name == "FacturaElectronicaCompra" ? "FACTURA ELEC. DE COMPRA" : "NOTA DE DEBITO",
                                ClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,
                                Consecutivo = documentoXml.GetElementsByTagName("NumeroConsecutivo").Item(0).InnerText,
                                Fecha = documento.Fecha.ToString("dd/MM/yyyy"),
                                Nombre = strNombreEmisor,
                                Moneda = strCodigoMoneda,
                                Identificacion = strIdentificacion,
                                Impuesto = decTotalImpuesto,
                                Total = decTotal
                            };
                            listaReporte.Add(reporteLinea);
                        }
                        else
                        {
                            XmlDocument documentoXml = new XmlDocument();
                            MemoryStream ms = new MemoryStream(documento.DatosDocumento);
                            documentoXml.Load(ms);
                            decimal decTotalImpuesto = 0;
                            decimal decTotal = 0;
                            string strCodigoMoneda = "CRC";
                            string strNombreEmisor = "INFORMACION NO DISPONIBLE";
                            string strIdentificacion = documento.IdentificacionEmisor;
                            if (documentoXml.GetElementsByTagName("TotalFactura").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalFactura").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("MontoTotalImpuesto").Count > 0)
                                decTotalImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("MontoTotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico
                            {
                                TipoDocumento = "FACTURA ELECTRONICA",
                                ClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,
                                Fecha = documento.Fecha.ToString("dd/MM/yyyy"),
                                Nombre = strNombreEmisor,
                                Identificacion = strIdentificacion,
                                Moneda = strCodigoMoneda,
                                Impuesto = decTotalImpuesto,
                                Total = decTotal
                            };
                            listaReporte.Add(reporteLinea);
                        }

                    }
                    return listaReporte.OrderBy(x => x.TipoDocumento).ThenBy(x => x.Fecha).ToList();
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de documentos recibidos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos recibidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteResumenMovimiento> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteResumenMovimiento> listaReporte = new List<ReporteResumenMovimiento>();
                    var grupoFacturasEmitidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa && a.IdSucursal == intIdSucursal && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 1, 3, 4 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    decimal decTotalVentaBienesTasa1 = 0;
                    decimal decTotalVentaBienesTasa2 = 0;
                    decimal decTotalVentaBienesTasa4 = 0;
                    decimal decTotalVentaBienesTasa8 = 0;
                    decimal decTotalVentaBienesTasa13 = 0;
                    decimal decTotalVentaBienesExento = 0;
                    decimal decTotalVentaServiciosTasa1 = 0;
                    decimal decTotalVentaServiciosTasa2 = 0;
                    decimal decTotalVentaServiciosTasa4 = 0;
                    decimal decTotalVentaServiciosTasa8 = 0;
                    decimal decTotalVentaServiciosTasa13 = 0;
                    decimal decTotalVentaServiciosExento = 0;
                    foreach (var documento in grupoFacturasEmitidas)
                    {
                        XmlDocument documentoXml = new XmlDocument();
                        MemoryStream ms = new MemoryStream(documento.DatosDocumento);
                        documentoXml.Load(ms);
                        decimal decVentaServiciosTasa1 = 0;
                        decimal decVentaServiciosTasa2 = 0;
                        decimal decVentaServiciosTasa4 = 0;
                        decimal decVentaServiciosTasa8 = 0;
                        decimal decVentaServiciosTasa13 = 0;
                        decimal decVentaServiciosExento = 0;
                        decimal decVentaBienesTasa1 = 0;
                        decimal decVentaBienesTasa2 = 0;
                        decimal decVentaBienesTasa4 = 0;
                        decimal decVentaBienesTasa8 = 0;
                        decimal decVentaBienesTasa13 = 0;
                        decimal decVentaBienesExento = 0;
                        decimal decTipoDeCambio = 1;
                        string strCodigoMoneda = "CRC";
                        foreach (XmlNode lineaDetalle in documentoXml.GetElementsByTagName("LineaDetalle"))
                        {
                            decimal decMontoPorLinea = decimal.Parse(lineaDetalle["MontoTotal"].InnerText, CultureInfo.InvariantCulture);
                            if (lineaDetalle["Impuesto"] != null)
                            {
                                if (lineaDetalle["Impuesto"]["Exoneracion"] != null)
                                {
                                    int porcentaje = int.Parse(lineaDetalle["Impuesto"]["Exoneracion"]["PorcentajeExoneracion"].InnerText, CultureInfo.InvariantCulture);
                                    decMontoPorLinea = decMontoPorLinea * (100 - porcentaje) / 100;
                                }
                                string strTarifa = lineaDetalle["Impuesto"]["Tarifa"].InnerText;
                                int intTarifa = -1;
                                try
                                {
                                    intTarifa = int.Parse(strTarifa, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"));
                                }
                                catch (Exception)
                                {
                                    throw new Exception("No se logro convertir el string de la tarifa " + strTarifa + " en un numero entero");
                                }
                                if (lineaDetalle["UnidadMedida"].InnerText == "Os" || lineaDetalle["UnidadMedida"].InnerText == "Sp" || lineaDetalle["UnidadMedida"].InnerText == "Spe" || lineaDetalle["UnidadMedida"].InnerText == "St")
                                {
                                    switch (intTarifa)
                                    {
                                        case 1:
                                            decVentaServiciosTasa1 += decMontoPorLinea;
                                            break;
                                        case 2:
                                            decVentaServiciosTasa2 += decMontoPorLinea;
                                            break;
                                        case 4:
                                            decVentaServiciosTasa4 += decMontoPorLinea;
                                            break;
                                        case 8:
                                            decVentaServiciosTasa8 += decMontoPorLinea;
                                            break;
                                        case 13:
                                            decVentaServiciosTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (intTarifa)
                                    {
                                        case 1:
                                            decVentaBienesTasa1 += decMontoPorLinea;
                                            break;
                                        case 2:
                                            decVentaBienesTasa2 += decMontoPorLinea;
                                            break;
                                        case 4:
                                            decVentaBienesTasa4 += decMontoPorLinea;
                                            break;
                                        case 8:
                                            decVentaBienesTasa8 += decMontoPorLinea;
                                            break;
                                        case 13:
                                            decVentaBienesTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (lineaDetalle["UnidadMedida"].InnerText == "Os" || lineaDetalle["UnidadMedida"].InnerText == "Sp" || lineaDetalle["UnidadMedida"].InnerText == "Spe" || lineaDetalle["UnidadMedida"].InnerText == "St")
                                    decVentaServiciosExento += decMontoPorLinea;
                                else
                                    decVentaBienesExento += decMontoPorLinea;
                            }
                        }
                        if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                            decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                        if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                            strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                        if (documentoXml.DocumentElement.Name != "NotaCreditoElectronica")
                        {
                            decTotalVentaBienesTasa1 += decVentaBienesTasa1 * decTipoDeCambio;
                            decTotalVentaBienesTasa2 += decVentaBienesTasa2 * decTipoDeCambio;
                            decTotalVentaBienesTasa4 += decVentaBienesTasa4 * decTipoDeCambio;
                            decTotalVentaBienesTasa8 += decVentaBienesTasa8 * decTipoDeCambio;
                            decTotalVentaBienesTasa13 += decVentaBienesTasa13 * decTipoDeCambio;
                            decTotalVentaBienesExento += decVentaBienesExento * decTipoDeCambio;
                            decTotalVentaServiciosTasa1 += decVentaServiciosTasa1 * decTipoDeCambio;
                            decTotalVentaServiciosTasa2 += decVentaServiciosTasa2 * decTipoDeCambio;
                            decTotalVentaServiciosTasa4 += decVentaServiciosTasa4 * decTipoDeCambio;
                            decTotalVentaServiciosTasa8 += decVentaServiciosTasa8 * decTipoDeCambio;
                            decTotalVentaServiciosTasa13 += decVentaServiciosTasa13 * decTipoDeCambio;
                            decTotalVentaServiciosExento += decVentaServiciosExento * decTipoDeCambio;
                        }
                        else
                        {
                            decTotalVentaBienesTasa1 -= decVentaBienesTasa1 * decTipoDeCambio;
                            decTotalVentaBienesTasa2 -= decVentaBienesTasa2 * decTipoDeCambio;
                            decTotalVentaBienesTasa4 -= decVentaBienesTasa4 * decTipoDeCambio;
                            decTotalVentaBienesTasa8 -= decVentaBienesTasa8 * decTipoDeCambio;
                            decTotalVentaBienesTasa13 -= decVentaBienesTasa13 * decTipoDeCambio;
                            decTotalVentaBienesExento -= decVentaBienesExento * decTipoDeCambio;
                            decTotalVentaServiciosTasa1 -= decVentaServiciosTasa1 * decTipoDeCambio;
                            decTotalVentaServiciosTasa2 -= decVentaServiciosTasa2 * decTipoDeCambio;
                            decTotalVentaServiciosTasa4 -= decVentaServiciosTasa4 * decTipoDeCambio;
                            decTotalVentaServiciosTasa8 -= decVentaServiciosTasa8 * decTipoDeCambio;
                            decTotalVentaServiciosTasa13 -= decVentaServiciosTasa13 * decTipoDeCambio;
                            decTotalVentaServiciosExento -= decVentaServiciosExento * decTipoDeCambio;
                        }
                    }
                    ReporteResumenMovimiento reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Ventas de bienes o mercancias",
                        Exento = decTotalVentaBienesExento,
                        Tasa1 = decTotalVentaBienesTasa1,
                        Tasa2 = decTotalVentaBienesTasa2,
                        Tasa4 = decTotalVentaBienesTasa4,
                        Tasa8 = decTotalVentaBienesTasa8,
                        Tasa13 = decTotalVentaBienesTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Ventas de servicios",
                        Exento = decTotalVentaServiciosExento,
                        Tasa1 = decTotalVentaServiciosTasa1,
                        Tasa2 = decTotalVentaServiciosTasa2,
                        Tasa4 = decTotalVentaServiciosTasa4,
                        Tasa8 = decTotalVentaServiciosTasa8,
                        Tasa13 = decTotalVentaServiciosTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    var grupoFacturasRecibidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa && a.IdSucursal == intIdSucursal && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 5, 8 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    decimal decTotalCompraBienesIvaTasa1 = 0;
                    decimal decTotalCompraBienesIvaTasa2 = 0;
                    decimal decTotalCompraBienesIvaTasa4 = 0;
                    decimal decTotalCompraBienesIvaTasa8 = 0;
                    decimal decTotalCompraBienesIvaTasa13 = 0;
                    decimal decTotalCompraBienesIvaExento = 0;
                    decimal decTotalCompraServiciosIvaTasa1 = 0;
                    decimal decTotalCompraServiciosIvaTasa2 = 0;
                    decimal decTotalCompraServiciosIvaTasa4 = 0;
                    decimal decTotalCompraServiciosIvaTasa8 = 0;
                    decimal decTotalCompraServiciosIvaTasa13 = 0;
                    decimal decTotalCompraServiciosIvaExento = 0;
                    decimal decTotalCompraBienesGastoTasa1 = 0;
                    decimal decTotalCompraBienesGastoTasa2 = 0;
                    decimal decTotalCompraBienesGastoTasa4 = 0;
                    decimal decTotalCompraBienesGastoTasa8 = 0;
                    decimal decTotalCompraBienesGastoTasa13 = 0;
                    decimal decTotalCompraBienesGastoExento = 0;
                    decimal decTotalCompraServiciosGastoTasa1 = 0;
                    decimal decTotalCompraServiciosGastoTasa2 = 0;
                    decimal decTotalCompraServiciosGastoTasa4 = 0;
                    decimal decTotalCompraServiciosGastoTasa8 = 0;
                    decimal decTotalCompraServiciosGastoTasa13 = 0;
                    decimal decTotalCompraServiciosGastoExento = 0;
                    foreach (var documento in grupoFacturasRecibidas)
                    {
                        if (documento.DatosDocumentoOri != null || documento.IdTipoDocumento == 8)
                        {
                            XmlDocument documentoXml = new XmlDocument();
                            MemoryStream ms = new MemoryStream(documento.IdTipoDocumento == 8 ? documento.DatosDocumento : documento.DatosDocumentoOri);
                            documentoXml.Load(ms);
                            decimal decTotalOtrosCargos = 0;
                            decimal decCompraServiciosTasa1 = 0;
                            decimal decCompraServiciosTasa2 = 0;
                            decimal decCompraServiciosTasa4 = 0;
                            decimal decCompraServiciosTasa8 = 0;
                            decimal decCompraServiciosTasa13 = 0;
                            decimal decCompraServiciosExento = 0;
                            decimal decCompraBienesTasa1 = 0;
                            decimal decCompraBienesTasa2 = 0;
                            decimal decCompraBienesTasa4 = 0;
                            decimal decCompraBienesTasa8 = 0;
                            decimal decCompraBienesTasa13 = 0;
                            decimal decCompraBienesExento = 0;
                            decimal decTipoDeCambio = 1;
                            string strCodigoMoneda = "CRC";
                            foreach (XmlElement lineaDetalle in documentoXml.GetElementsByTagName("LineaDetalle"))
                            {
                                decimal decMontoPorLinea = decimal.Parse(lineaDetalle["SubTotal"].InnerText, CultureInfo.InvariantCulture);
                                if (lineaDetalle["Impuesto"] != null)
                                {
                                    foreach (XmlNode impuestoDetalle in lineaDetalle.GetElementsByTagName("Impuesto"))
                                    {
                                        if (impuestoDetalle["Exoneracion"] != null)
                                        {
                                            int porcentaje = int.Parse(impuestoDetalle["Exoneracion"]["PorcentajeExoneracion"].InnerText, CultureInfo.InvariantCulture);
                                            decMontoPorLinea = decMontoPorLinea * (100 - porcentaje) / 100;
                                        }
                                        string strTarifa = impuestoDetalle["Tarifa"].InnerText.Replace(" ", string.Empty);
                                        int intTarifa = -1;
                                        try
                                        {
                                            intTarifa = int.Parse(strTarifa, NumberStyles.Integer | NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"));
                                        }
                                        catch (Exception)
                                        {
                                            throw new Exception("No se logro convertir el string de la tarifa " + strTarifa + " en un numero entero");
                                        }
                                        if (lineaDetalle["UnidadMedida"].InnerText == "Os" || lineaDetalle["UnidadMedida"].InnerText == "Sp" || lineaDetalle["UnidadMedida"].InnerText == "Spe" || lineaDetalle["UnidadMedida"].InnerText == "St")
                                        {
                                            switch (intTarifa)
                                            {
                                                case 0:
                                                    decCompraServiciosExento += decMontoPorLinea;
                                                    break;
                                                case 1:
                                                    decCompraServiciosTasa1 += decMontoPorLinea;
                                                    break;
                                                case 2:
                                                    decCompraServiciosTasa2 += decMontoPorLinea;
                                                    break;
                                                case 4:
                                                    decCompraServiciosTasa4 += decMontoPorLinea;
                                                    break;
                                                case 8:
                                                    decCompraServiciosTasa8 += decMontoPorLinea;
                                                    break;
                                                case 13:
                                                    decCompraServiciosTasa13 += decMontoPorLinea;
                                                    break;
                                                default:
                                                    throw new Exception("La tarifa de impuesto " + strTarifa + " de la linea de detalle no esta parametrizada");
                                            }
                                        }
                                        else
                                        {
                                            switch (intTarifa)
                                            {
                                                case 0:
                                                    decCompraBienesExento += decMontoPorLinea;
                                                    break;
                                                case 1:
                                                    decCompraBienesTasa1 += decMontoPorLinea;
                                                    break;
                                                case 2:
                                                    decCompraBienesTasa2 += decMontoPorLinea;
                                                    break;
                                                case 4:
                                                    decCompraBienesTasa4 += decMontoPorLinea;
                                                    break;
                                                case 8:
                                                    decCompraBienesTasa8 += decMontoPorLinea;
                                                    break;
                                                case 13:
                                                    decCompraBienesTasa13 += decMontoPorLinea;
                                                    break;
                                                default:
                                                    throw new Exception("La tarifa de impuesto " + strTarifa + " de la linea de detalle no esta parametrizada");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (lineaDetalle["UnidadMedida"].InnerText == "Os" || lineaDetalle["UnidadMedida"].InnerText == "Sp" || lineaDetalle["UnidadMedida"].InnerText == "Spe" || lineaDetalle["UnidadMedida"].InnerText == "St")
                                        decCompraServiciosExento += decMontoPorLinea;
                                    else
                                        decCompraBienesExento += decMontoPorLinea;
                                }
                            }
                            if (documentoXml.GetElementsByTagName("OtrosCargos").Count > 0)
                            {
                                XmlNode otrosCargos = documentoXml.GetElementsByTagName("OtrosCargos").Item(0);
                                decTotalOtrosCargos = decimal.Parse(otrosCargos["MontoCargo"].InnerText, CultureInfo.InvariantCulture);
                            }
                            if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                                strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                            if (strCodigoMoneda != "CRC")
                                if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                                    decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.DocumentElement.Name != "NotaCreditoElectronica")
                            {
                                if (documento.IdTipoDocumento == 8 || documento.EsIvaAcreditable == "S")
                                {
                                    decTotalCompraBienesIvaTasa1 += decCompraBienesTasa1 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa2 += decCompraBienesTasa2 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa4 += decCompraBienesTasa4 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa8 += decCompraBienesTasa8 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa13 += decCompraBienesTasa13 * decTipoDeCambio;
                                    decTotalCompraBienesIvaExento += decCompraBienesExento * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa1 += decCompraServiciosTasa1 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa2 += decCompraServiciosTasa2 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa4 += decCompraServiciosTasa4 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa8 += decCompraServiciosTasa8 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa13 += decCompraServiciosTasa13 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaExento += decCompraServiciosExento * decTipoDeCambio;
                                    decTotalCompraServiciosIvaExento += decTotalOtrosCargos * decTipoDeCambio;
                                }
                                else
                                {
                                    decTotalCompraBienesGastoTasa1 += decCompraBienesTasa1 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa2 += decCompraBienesTasa2 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa4 += decCompraBienesTasa4 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa8 += decCompraBienesTasa8 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa13 += decCompraBienesTasa13 * decTipoDeCambio;
                                    decTotalCompraBienesGastoExento += decCompraBienesExento * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa1 += decCompraServiciosTasa1 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa2 += decCompraServiciosTasa2 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa4 += decCompraServiciosTasa4 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa8 += decCompraServiciosTasa8 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa13 += decCompraServiciosTasa13 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoExento += decCompraServiciosExento * decTipoDeCambio;
                                    decTotalCompraServiciosGastoExento += decTotalOtrosCargos * decTipoDeCambio;
                                }
                            }
                            else
                            {
                                if (documento.IdTipoDocumento == 8 || documento.EsIvaAcreditable == "S")
                                {
                                    decTotalCompraBienesIvaTasa1 -= decCompraBienesTasa1 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa2 -= decCompraBienesTasa2 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa4 -= decCompraBienesTasa4 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa8 -= decCompraBienesTasa8 * decTipoDeCambio;
                                    decTotalCompraBienesIvaTasa13 -= decCompraBienesTasa13 * decTipoDeCambio;
                                    decTotalCompraBienesIvaExento -= decCompraBienesExento * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa1 -= decCompraServiciosTasa1 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa2 -= decCompraServiciosTasa2 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa4 -= decCompraServiciosTasa4 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa8 -= decCompraServiciosTasa8 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaTasa13 -= decCompraServiciosTasa13 * decTipoDeCambio;
                                    decTotalCompraServiciosIvaExento -= decCompraServiciosExento * decTipoDeCambio;
                                    decTotalCompraServiciosIvaExento -= decTotalOtrosCargos * decTipoDeCambio;
                                }
                                else
                                {
                                    decTotalCompraBienesGastoTasa1 -= decCompraBienesTasa1 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa2 -= decCompraBienesTasa2 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa4 -= decCompraBienesTasa4 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa8 -= decCompraBienesTasa8 * decTipoDeCambio;
                                    decTotalCompraBienesGastoTasa13 -= decCompraBienesTasa13 * decTipoDeCambio;
                                    decTotalCompraBienesGastoExento -= decCompraBienesExento * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa1 -= decCompraServiciosTasa1 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa2 -= decCompraServiciosTasa2 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa4 -= decCompraServiciosTasa4 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa8 -= decCompraServiciosTasa8 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoTasa13 -= decCompraServiciosTasa13 * decTipoDeCambio;
                                    decTotalCompraServiciosGastoExento -= decCompraServiciosExento * decTipoDeCambio;
                                    decTotalCompraServiciosGastoExento -= decTotalOtrosCargos * decTipoDeCambio;
                                }
                            }
                        }
                        else
                        {
                            XmlDocument documentoXml = new XmlDocument();
                            MemoryStream ms = new MemoryStream(documento.DatosDocumento);
                            documentoXml.Load(ms);
                            decimal decImpuestoPorLinea = 0;
                            decimal decTotalPorLinea = 0;
                            if (documentoXml.GetElementsByTagName("TotalFactura").Count > 0)
                                decTotalPorLinea = decimal.Parse(documentoXml.GetElementsByTagName("TotalFactura").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("MontoTotalImpuesto").Count > 0)
                                decImpuestoPorLinea = decimal.Parse(documentoXml.GetElementsByTagName("MontoTotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (decImpuestoPorLinea > 0)
                            {
                                decTotalCompraBienesIvaTasa13 += decTotalPorLinea - decImpuestoPorLinea;
                            }
                        }
                    }
                    reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Compras de bienes o mercancias IVA acreditable",
                        Exento = decTotalCompraBienesIvaExento,
                        Tasa1 = decTotalCompraBienesIvaTasa1,
                        Tasa2 = decTotalCompraBienesIvaTasa2,
                        Tasa4 = decTotalCompraBienesIvaTasa4,
                        Tasa8 = decTotalCompraBienesIvaTasa8,
                        Tasa13 = decTotalCompraBienesIvaTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Compras de servicios IVA acreditable",
                        Exento = decTotalCompraServiciosIvaExento,
                        Tasa1 = decTotalCompraServiciosIvaTasa1,
                        Tasa2 = decTotalCompraServiciosIvaTasa2,
                        Tasa4 = decTotalCompraServiciosIvaTasa4,
                        Tasa8 = decTotalCompraServiciosIvaTasa8,
                        Tasa13 = decTotalCompraServiciosIvaTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Compras de bienes o mercancias sin IVA acreditable",
                        Exento = decTotalCompraBienesGastoExento,
                        Tasa1 = decTotalCompraBienesGastoTasa1,
                        Tasa2 = decTotalCompraBienesGastoTasa2,
                        Tasa4 = decTotalCompraBienesGastoTasa4,
                        Tasa8 = decTotalCompraBienesGastoTasa8,
                        Tasa13 = decTotalCompraBienesGastoTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento
                    {
                        Descripcion = "Compras de servicios sin IVA acreditable",
                        Exento = decTotalCompraServiciosGastoExento,
                        Tasa1 = decTotalCompraServiciosGastoTasa1,
                        Tasa2 = decTotalCompraServiciosGastoTasa2,
                        Tasa4 = decTotalCompraServiciosGastoTasa4,
                        Tasa8 = decTotalCompraServiciosGastoTasa8,
                        Tasa13 = decTotalCompraServiciosGastoTasa13
                    };
                    listaReporte.Add(reporteLinea);
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcionValor> ObtenerReporteComparativoVentasPorPeriodo(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            List<LlaveDescripcionValor> listado = new List<LlaveDescripcionValor>() { };
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.IdSucursal == intIdSucursal && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == false)
                        .GroupBy(x => new { Month = x.Fecha.Month, Year = x.Fecha.Year })
                        .Select(x => new { Total = x.Sum(f => f.Excento + f.Gravado + f.Exonerado + f.Impuesto), Annio = x.Key.Year, Mes = x.Key.Month })
                        .OrderBy(x => x.Annio).ThenBy(x => x.Mes).ToList();
                    foreach (var value in detalleVentas)
                    {
                        LlaveDescripcionValor reporteLinea = new LlaveDescripcionValor
                        {
                            Id = value.Annio,
                            Descripcion = ObtenerNombreDelMes(value.Mes),
                            Valor = value.Total
                        };
                        listado.Add(reporteLinea);
                    }
                    return listado;
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte comparativo de ventas por periodo: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte comparativo de ventas por periodo. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteVentasGenerales(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null || _servicioCorreo == null) throw new Exception("Service factory or email service not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDetalle> dstDatos = ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, 0, false, 0);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[6];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Reporte de Ventas Generales");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[5] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteVentasGenerales." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de ventas generales por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de ventas generales: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas generales. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteVentasAnuladas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDetalle> dstDatos = ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, 0, true, 0);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[6];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Reporte de Ventas Anuladas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[5] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteVentasAnuladas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de ventas anuladas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de ventas anuladas: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de ventas anuladas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteResumenMovimientos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteEstadoResultados> dstDatos = ObtenerReporteEstadoResultados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[4] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptResumenMovimientos.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteResumenDeMovimientos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de resumen de movimientos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de resumen de movimientos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de resumen de movimientos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteDetalleIngresos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteGrupoDetalle> dstDatos = ObtenerReporteDetalleIngreso(intIdEmpresa, intIdSucursal, 0, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[4] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalleIngresos.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteDetalleDeIngresos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte detallado de ingresos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de detalle de ingresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteDetalleEgresos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteGrupoDetalle> dstDatos = ObtenerReporteDetalleEgreso(intIdEmpresa, intIdSucursal, 0, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[4] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalleEgresos.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteDetalleDeEgresos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte detallado de egresos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al procesar el reporte de detalle de egresos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al ejecutar el reporte de detalle de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteDocumentosEmitidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteDocumentosElectronicosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[6];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Emitidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[5] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptComprobanteElectronico.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteFacturasElectronicasEmitidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de facturas electrónicas emitidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al enviar el reporte de listado facturas electrónicas emitidas: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al enviar el reporte de listado facturas electrónicas emitidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteDocumentosRecibidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteDocumentosElectronicosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[6];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Recibidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[5] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptComprobanteElectronico.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteFacturasElectronicasRecibidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de facturas electrónicas recibidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al enviar el reporte de listado facturas electrónicas recibidas: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al enviar el reporte de listado facturas electrónicas recibidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteResumenMovimientosElectronicos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strFormatoReporte)
        {
            if (_serviceScopeFactory == null) throw new Exception("Service factory not set");
            using (var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteResumenMovimiento> dstDatos = ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaFinal);
                    parameters[4] = new ReportParameter("pSucursal", sucursal.NombreSucursal);
                    Stream stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptResumenComprobanteElectronico.rdlc");
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, stream, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteResumenMovimientosElectronicos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendNotificationEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte resumen de movimientos comprobantes electrónicos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    if (_logger != null) _logger.LogError("Error al enviar el reporte de resumen de documentos electrónicos: ", ex);
                    if (_config?.EsModoDesarrollo ?? false) throw ex.InnerException ?? ex;
                    else throw new Exception("Se produjo un error al enviar el reporte de resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        byte[] GenerarContenidoReporte(string strFormatoReporte, Stream stmDefinicionReporte, ReportDataSource rds, ReportParameter[] parameters)
        {
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(stmDefinicionReporte);
            report.DataSources.Add(rds);
            report.SetParameters(parameters);
            return report.Render(strFormatoReporte);
        }

        private string ObtenerNombreDelMes(int intId)
        {
            switch (intId)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "No Válido";
            }
        }
    }
}