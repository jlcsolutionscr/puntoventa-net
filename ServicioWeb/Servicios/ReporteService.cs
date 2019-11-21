﻿using System;
using System.Linq;
using System.Collections.Generic;
using LeandroSoftware.Core. Utilitario;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.Core.Dominio.Entidades;
using log4net;
using Unity;
using System.Globalization;
using System.Xml;
using System.Text;
using Microsoft.Reporting.WebForms;
using LeandroSoftware.Core.Servicios;
using Newtonsoft.Json.Linq;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IReporteService
    {
        IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoFactura();
        IList<LlaveDescripcion> ObtenerListadoCondicionVentaYFormaPagoCompra();
        List<ReporteVentas> ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago, int intIdBancoAdquiriente);
        List<ReporteVentasPorVendedor> ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor);
        List<ReporteCompras> ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago);
        List<ReporteCuentasPorCobrar> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente);
        List<ReporteCuentasPorPagar> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor);
        List<ReporteMovimientosCxC> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente);
        List<ReporteMovimientosCxP> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor);
        List<ReporteMovimientosBanco> ObtenerReporteMovimientosBanco(int intIdCuenta, string strFechaInicial, string strFechaFinal);
        List<ReporteEstadoResultados> ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteDetalleEgreso> ObtenerReporteDetalleEgreso(int intIdEmpresa, int idCuentaEgreso, string strFechaInicial, string strFechaFinal);
        List<ReporteDetalleIngreso> ObtenerReporteDetalleIngreso(int intIdEmpresa, int idCuentaIngreso, string strFechaInicial, string strFechaFinal);
        List<ReporteVentasPorLineaResumen> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteVentasPorLineaDetalle> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal);
        List<ReporteCierreDeCaja> ObtenerReporteCierreDeCaja(int intIdCierre);
        List<ReporteProforma> ObtenerReporteProforma(int intIdProforma);
        List<ReporteOrdenServicio> ObtenerReporteOrdenServicio(int intIdOrdenServicio);
        List<ReporteOrdenCompra> ObtenerReporteOrdenCompra(int intIdOrdenCompra);
        List<ReporteInventario> ObtenerReporteInventario(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion);
        List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0);
        List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa);
        List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal);
        List<ReporteEgreso> ObtenerReporteEgreso(int intIdEgreso);
        List<ReporteIngreso> ObtenerReporteIngreso(int intIdIngreso);
        List<ReporteDocumentoElectronico> ObtenerReporteFacturasElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteDocumentoElectronico> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteDocumentoElectronico> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteDocumentoElectronico> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        List<ReporteResumenMovimiento> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        void EnviarReporteVentasGenerales(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteVentasAnuladas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteResumenMovimientos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteDetalleEgresos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteFacturasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteFacturasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteNotasCreditoEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteNotasCreditoRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);
        void EnviarReporteResumenMovimientosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio);

    }

    public class ReporteService : IReporteService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public ReporteService(IUnityContainer pContainer)
        {
            try
            {
                localContainer = pContainer;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Reportería. Por favor consulte con su proveedor.");
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
                log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
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
                log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
            }
        }

        public List<ReporteVentas> ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago, int intIdBancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteVentas> listaReporte = new List<ReporteVentas>();
                    if (intTipoPago == -1)
                    {
                        var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                            .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.IdDocElectronico, z.x.Impuesto, Total = (z.x.Excento + z.x.Gravado + z.x.Impuesto - z.x.Descuento) });
                        foreach (var value in detalleVentas)
                        {
                            ReporteVentas reporteLinea = new ReporteVentas();
                            reporteLinea.IdFactura = value.IdFactura;
                            reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                            reporteLinea.Nombre = value.NombreCliente;
                            reporteLinea.NoDocumento = value.IdDocElectronico;
                            reporteLinea.Impuesto = value.Impuesto;
                            reporteLinea.Total = value.Total;
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito | !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.IdDocElectronico, z.x.Impuesto, Total = (z.x.Excento + z.x.Gravado + z.x.Impuesto - z.x.Descuento) });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleVentas = detalleVentas.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdCliente > 0)
                                detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                            foreach (var value in detalleVentas)
                            {
                                ReporteVentas reporteLinea = new ReporteVentas();
                                reporteLinea.IdFactura = value.IdFactura;
                                reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = value.NombreCliente;
                                reporteLinea.NoDocumento = value.IdDocElectronico;
                                reporteLinea.Impuesto = value.Impuesto;
                                reporteLinea.Total = value.Total;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                                .Select(z => new { z.x.x.IdCliente, z.x.x.Nulo, z.x.x.IdCondicionVenta, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.IdDocElectronico, z.x.x.Impuesto, Total = z.y.MontoLocal, z.x.x.TotalCosto });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoEfectivo)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Efectivo);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTarjeta)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado && x.IdFormaPago == StaticFormaPago.Tarjeta);
                                if (intIdBancoAdquiriente > 0)
                                    detalleVentas = detalleVentas.Where(x => x.IdCuentaBanco == intIdBancoAdquiriente);
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
                                ReporteVentas reporteLinea = new ReporteVentas();
                                reporteLinea.IdFactura = value.IdFactura;
                                reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = value.NombreCliente;
                                reporteLinea.NoDocumento = value.IdDocElectronico;
                                reporteLinea.Impuesto = value.Impuesto;
                                reporteLinea.Total = value.Total;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ventas por Cliente: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteVentasPorVendedor> ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteVentasPorVendedor> listaReporte = new List<ReporteVentasPorVendedor>();
                    var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == false)
                        .Join(dbContext.VendedorRepository, x => x.IdVendedor, y => y.IdVendedor, (x, y) => new { x, y })
                        .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Select(z => new { z.x.x.IdVendedor, z.x.y.Nombre, z.x.x.Nulo, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.IdDocElectronico, Total = z.y.MontoLocal, z.x.x.TotalCosto, z.x.x.Impuesto });
                    if (intIdVendedor > 0)
                        detalleVentas = detalleVentas.Where(x => x.IdVendedor == intIdVendedor);
                    foreach (var value in detalleVentas)
                    {
                        ReporteVentasPorVendedor reporteLinea = new ReporteVentasPorVendedor();
                        reporteLinea.NombreVendedor = value.Nombre;
                        reporteLinea.IdFactura = value.IdFactura;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.NombreCliente = value.NombreCliente;
                        reporteLinea.NoDocumento = value.IdDocElectronico;
                        reporteLinea.Total = value.Total;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ventas por Vendedor: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCompras> ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteCompras> listaReporte = new List<ReporteCompras>();
                    if (intTipoPago == -1)
                    {
                        var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                            .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Gravado + z.x.Impuesto) });
                        foreach (var value in detalleCompras)
                        {
                            ReporteCompras reporteLinea = new ReporteCompras();
                            reporteLinea.IdCompra = value.IdCompra;
                            reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                            reporteLinea.Nombre = value.NombreProveedor;
                            reporteLinea.NoDocumento = value.NoDocumento;
                            reporteLinea.Impuesto = value.Impuesto;
                            reporteLinea.Total = value.Total;
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito | !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Gravado + z.x.Impuesto) });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleCompras = detalleCompras.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdProveedor > 0)
                                detalleCompras = detalleCompras.Where(x => x.IdProveedor == intIdProveedor);
                            foreach (var value in detalleCompras)
                            {
                                ReporteCompras reporteLinea = new ReporteCompras();
                                reporteLinea.IdCompra = value.IdCompra;
                                reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = value.NombreProveedor;
                                reporteLinea.NoDocumento = value.NoDocumento;
                                reporteLinea.Impuesto = value.Impuesto;
                                reporteLinea.Total = value.Total;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Nulo == bolNulo)
                                .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                                .Join(dbContext.DesglosePagoCompraRepository, x => x.x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                                .Select(z => new { z.x.x.IdProveedor, z.x.x.Nulo, z.y.IdFormaPago, z.x.x.IdCondicionVenta, z.x.x.IdCompra, z.x.x.Fecha, NombreProveedor = z.x.y.Nombre, z.x.x.NoDocumento, z.x.x.Impuesto, Total = z.y.MontoLocal });
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
                                ReporteCompras reporteLinea = new ReporteCompras();
                                reporteLinea.IdCompra = value.IdCompra;
                                reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = value.NombreProveedor;
                                reporteLinea.NoDocumento = value.NoDocumento;
                                reporteLinea.Impuesto = value.Impuesto;
                                reporteLinea.Total = value.Total;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Compras por Proveedor: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCuentasPorCobrar> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteCuentasPorCobrar> listaReporte = new List<ReporteCuentasPorCobrar>();
                    var detalleCxCClientes = dbContext.CuentaPorCobrarRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Saldo > 0 && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, x => x.IdPropietario, y => y.IdCliente, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxC, z.x.Descripcion, z.x.Referencia, z.x.Fecha, z.x.Total, z.x.Saldo });
                    if (intIdCliente > 0)
                        detalleCxCClientes = detalleCxCClientes.Where(x => x.IdPropietario == intIdCliente);
                    foreach (var value in detalleCxCClientes)
                    {
                        ReporteCuentasPorCobrar reporteLinea = new ReporteCuentasPorCobrar();
                        reporteLinea.IdPropietario = value.IdPropietario;
                        reporteLinea.Nombre = value.Nombre;
                        reporteLinea.IdCxC = value.IdCxC;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Referencia = value.Referencia;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Saldo = value.Saldo;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Cuentas por Cobrar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCuentasPorPagar> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteCuentasPorPagar> listaReporte = new List<ReporteCuentasPorPagar>();
                    var detalleCxPProveedores = dbContext.CuentaPorPagarRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Saldo > 0 && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal && s.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, x => x.IdPropietario, y => y.IdProveedor, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxP, z.x.Fecha, z.x.Descripcion, z.x.Referencia, z.x.Total, z.x.Saldo });
                    if (intIdProveedor > 0)
                        detalleCxPProveedores = detalleCxPProveedores.Where(x => x.IdPropietario == intIdProveedor);
                    foreach (var value in detalleCxPProveedores)
                    {
                        ReporteCuentasPorPagar reporteLinea = new ReporteCuentasPorPagar();
                        reporteLinea.IdPropietario = value.IdPropietario;
                        reporteLinea.Nombre = value.Nombre;
                        reporteLinea.IdCxP = value.IdCxP;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Descripcion = value.Descripcion + " Fact: " + value.Referencia;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Saldo = value.Saldo;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Cuentas por Pagar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosCxC> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosCxC> listaReporte = new List<ReporteMovimientosCxC>();
                    var cxcClientes = dbContext.CuentaPorCobrarRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Nulo == false && a.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, a => a.IdPropietario, b => b.IdCliente, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorCobrarRepository, a => a.a.IdCxC, d => d.IdCxC, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorCobrarRepository, a => a.c.IdMovCxC, d => d.IdMovCxC, (b, c) => new { b, c }).Where(s => !s.c.Nulo && s.c.Fecha >= datFechaInicial && s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.b.a.IdPropietario, DescCxC = d.b.b.a.Descripcion, d.b.b.b.Nombre, d.b.c.IdCxC, d.b.b.a.Total, d.b.b.a.Saldo, d.c.IdMovCxC, d.c.Fecha, d.c.Descripcion, d.c.Tipo, d.b.c.Monto });
                    if (intIdCliente > 0)
                        cxcClientes = cxcClientes.Where(a => a.IdPropietario == intIdCliente);
                    foreach (var value in cxcClientes)
                    {
                        ReporteMovimientosCxC reporteLinea = new ReporteMovimientosCxC();
                        reporteLinea.IdPropietario = value.IdPropietario;
                        reporteLinea.Nombre = value.Nombre;
                        reporteLinea.IdCxC = value.IdCxC;
                        reporteLinea.DescCxC = value.DescCxC;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Saldo = value.Saldo;
                        reporteLinea.IdMovCxC = value.IdMovCxC;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Descripcion = value.Descripcion;
                        if (value.Tipo == 3)
                            reporteLinea.Credito = value.Monto;
                        else
                            reporteLinea.Debito = value.Monto;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Movimientos de Cuentas por Cobrar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosCxP> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosCxP> listaReporte = new List<ReporteMovimientosCxP>();
                    var cxpProveedores = dbContext.CuentaPorPagarRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Nulo == false && a.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, a => a.IdPropietario, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorPagarRepository, a => a.a.IdCxP, d => d.IdCxP, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorPagarRepository, a => a.c.IdMovCxP, d => d.IdMovCxP, (b, c) => new { b, c }).Where(s => !s.c.Nulo && s.c.Fecha >= datFechaInicial && s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.b.a.IdPropietario, DescCxP = d.b.b.a.Descripcion, d.b.b.b.Nombre, d.b.c.IdCxP, d.b.b.a.Total, d.b.b.a.Saldo, d.c.IdMovCxP, d.c.Fecha, d.c.Descripcion, d.c.Recibo, d.c.Tipo, d.b.c.Monto });
                    if (intIdProveedor > 0)
                        cxpProveedores = cxpProveedores.Where(a => a.IdPropietario == intIdProveedor);
                    foreach (var value in cxpProveedores)
                    {
                        ReporteMovimientosCxP reporteLinea = new ReporteMovimientosCxP();
                        reporteLinea.IdProveedor = value.IdPropietario;
                        reporteLinea.Nombre = value.Nombre;
                        reporteLinea.IdCxP = value.IdCxP;
                        reporteLinea.DescCxP = value.DescCxP;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Saldo = value.Saldo;
                        reporteLinea.IdMovCxP = value.IdMovCxP;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Recibo = value.Recibo;
                        if (value.Tipo == 3)
                            reporteLinea.Credito = value.Monto;
                        else
                            reporteLinea.Debito = value.Monto;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Movimientos de Cuentas por Pagar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosBanco> ObtenerReporteMovimientosBanco(int intIdCuenta, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosBanco> listaReporte = new List<ReporteMovimientosBanco>();
                    var movimientoBanco = dbContext.MovimientoBancoRepository.Where(s => s.IdCuenta == intIdCuenta && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaBancoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .Join(dbContext.TipoMovimientoBancoRepository, a => a.x.IdTipo, b => b.IdTipoMov, (a, b) => new { a, b })
                        .Select(z => new { z.a.x.IdMov, z.a.y.IdCuenta, NombreCuenta = z.a.y.Descripcion, z.a.x.Fecha, z.a.x.Numero, z.a.x.Beneficiario, z.a.x.Descripcion, z.b.DebeHaber, DescTipo = z.a.y.Descripcion, Total = z.a.x.Monto, z.a.x.SaldoAnterior })
                        .OrderBy(x => x.IdMov).ThenBy(x => x.Fecha);
                    foreach (var value in movimientoBanco)
                    {
                        ReporteMovimientosBanco reporteLinea = new ReporteMovimientosBanco();
                        reporteLinea.IdMov = value.IdMov;
                        reporteLinea.IdCuenta = value.IdCuenta;
                        reporteLinea.NombreCuenta = value.NombreCuenta;
                        reporteLinea.SaldoAnterior = value.SaldoAnterior;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Numero = value.Numero;
                        reporteLinea.Beneficiario = value.Beneficiario;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Tipo = value.DescTipo;
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
                    log.Error("Error al procesar el reporte de Movimientos Bancarios: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos bancarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteEstadoResultados> ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Ingresos";
                        if (eachFactura.tipopago == StaticFormaPago.Efectivo)
                            strTipo = " de contado";
                        else if (eachFactura.tipopago == StaticFormaPago.Cheque)
                            strTipo = " con cheque";
                        else if (eachFactura.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                            strTipo = " con depósito bancario";
                        else if (eachFactura.tipopago == StaticFormaPago.Tarjeta)
                            strTipo = " con tarjeta";
                        else
                            strTipo = " otras formas de pago";
                        reporteLinea.Descripcion = "Ventas" + strTipo;
                        reporteLinea.Valor = eachFactura.total;
                        listaReporte.Add(reporteLinea);
                    }
                    var ingreso = dbContext.IngresoRepository.Where(w => w.IdEmpresa == intIdEmpresa && w.Nulo == false && w.Fecha >= datFechaInicial && w.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .GroupBy(a => a.y.Descripcion)
                        .Select(a => new { Total = a.Sum(b => b.x.Monto), Desc = a.Key });
                    foreach (var value in ingreso)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Ingresos";
                        reporteLinea.Descripcion = value.Desc;
                        reporteLinea.Valor = value.Total;
                        listaReporte.Add(reporteLinea);
                    }
                    if (grupoFacturas.Count() == 0 && ingreso.Count() == 0)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Ingresos";
                        reporteLinea.Descripcion = "No hay registros";
                        reporteLinea.Valor = 0;
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
                            ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                            reporteLinea.NombreTipoRegistro = "Egresos";
                            if (eachCompra.tipopago == StaticFormaPago.Efectivo)
                                strTipo = " de contado";
                            else if (eachCompra.tipopago == StaticFormaPago.Cheque)
                                strTipo = " con cheque";
                            else if (eachCompra.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                                strTipo = " con depósito bancario";
                            else if (eachCompra.tipopago == StaticFormaPago.Tarjeta)
                                strTipo = " con tarjeta";
                            else
                                strTipo = " con otras formas de pago";
                            reporteLinea.Descripcion = "Compras" + strTipo;
                            reporteLinea.Valor = eachCompra.total;
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    var egreso = dbContext.EgresoRepository.Where(w => w.IdEmpresa == intIdEmpresa && w.Nulo == false && w.Fecha >= datFechaInicial && w.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .GroupBy(a => a.y.Descripcion)
                        .Select(a => new { Total = a.Sum(b => b.x.Monto), Desc = a.Key });
                    foreach (var value in egreso)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Egresos";
                        reporteLinea.Descripcion = value.Desc;
                        reporteLinea.Valor = value.Total;
                        listaReporte.Add(reporteLinea);
                    }

                    if (grupoCompras.Count() == 0 && egreso.Count() == 0)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Egresos";
                        reporteLinea.Descripcion = "No hay registros";
                        reporteLinea.Valor = 0;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Estado de Resultados: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de estado de resultados. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalleEgreso> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalleEgreso> listaReporte = new List<ReporteDetalleEgreso>();
                    var egreso = dbContext.EgresoRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdEgreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in egreso)
                    {
                        if (intIdCuentaEgreso > 0 && value.IdCuenta != intIdCuentaEgreso)
                            continue;
                        ReporteDetalleEgreso reporteLinea = new ReporteDetalleEgreso();
                        reporteLinea.IdMov = value.IdEgreso;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Detalle = value.Detalle;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Total = value.Total;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de Egresos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalleIngreso> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDetalleIngreso> listaReporte = new List<ReporteDetalleIngreso>();
                    var ingreso = dbContext.IngresoRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdIngreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in ingreso)
                    {
                        if (intIdCuentaIngreso > 0 && value.IdCuenta != intIdCuentaIngreso)
                            continue;
                        ReporteDetalleIngreso reporteLinea = new ReporteDetalleIngreso();
                        reporteLinea.IdMov = value.IdIngreso;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Detalle = value.Detalle;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Total = value.Total;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de ingresos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteVentasPorLineaResumen> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteVentasPorLineaResumen> listaReporte = new List<ReporteVentasPorLineaResumen>();
                    var ventasResumen = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.y.PorcentajeIVA, z.x.x.x.Excento, z.x.x.x.Gravado, z.x.x.x.Impuesto, z.x.x.x.Descuento, Costo = z.x.x.y.Cantidad * z.x.x.y.PrecioCosto });
                    foreach (var value in ventasResumen)
                    {
                        ReporteVentasPorLineaResumen reporteLinea = new ReporteVentasPorLineaResumen();
                        reporteLinea.Codigo = value.Codigo;
                        reporteLinea.IdLinea = value.IdLinea;
                        reporteLinea.NombreLinea = value.NombreLinea;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioVenta = value.PrecioVenta;
                        reporteLinea.Excento = value.Excento;
                        reporteLinea.Gravado = value.Gravado;
                        reporteLinea.Descuento = value.Descuento;
                        reporteLinea.Impuesto = value.Impuesto;
                        reporteLinea.Costo = value.Costo;
                        reporteLinea.PorcentajeIVA = value.PorcentajeIVA;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Ventas por Línea: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea resumido. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteVentasPorLineaDetalle> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteVentasPorLineaDetalle> listaReporte = new List<ReporteVentasPorLineaDetalle>();
                    var ventasDetalle = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa && s.Nulo == false && s.Fecha >= datFechaInicial && s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.Descripcion, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.y.PorcentajeIVA, z.x.x.x.Excento, z.x.x.x.Gravado, z.x.x.x.Impuesto, z.x.x.x.Descuento });
                    if (intIdLinea > 0)
                        ventasDetalle = ventasDetalle.Where(a => a.IdLinea == intIdLinea);
                    foreach (var value in ventasDetalle)
                    {
                        ReporteVentasPorLineaDetalle reporteLinea = new ReporteVentasPorLineaDetalle();
                        reporteLinea.Codigo = value.Codigo;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.IdLinea = value.IdLinea;
                        reporteLinea.NombreLinea = value.NombreLinea;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioVenta = value.PrecioVenta;
                        reporteLinea.Excento = value.Excento;
                        reporteLinea.Gravado = value.Gravado;
                        reporteLinea.Descuento = value.Descuento;
                        reporteLinea.Impuesto = value.Impuesto;
                        reporteLinea.PorcentajeIVA = value.PorcentajeIVA;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de Ventas por Línea: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea detallado. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteCierreDeCaja> ObtenerReporteCierreDeCaja(int intIdCierre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteCierreDeCaja> listaReporte = new List<ReporteCierreDeCaja>();
                    var datosCierre = dbContext.CierreCajaRepository.Where(a => a.IdCierre == intIdCierre)
                        .Select(d => new { d.FondoInicio, d.VentasContado, d.VentasCredito, d.VentasTarjeta, d.OtrasVentas, d.RetencionIVA, d.ComisionVT, d.LiquidacionTarjeta, d.IngresoCxCEfectivo, d.IngresoCxCTarjeta, d.DevolucionesProveedores, d.OtrosIngresos, d.ComprasContado, d.ComprasCredito, d.OtrasCompras, d.EgresoCxPEfectivo, d.DevolucionesClientes, d.OtrosEgresos });
                    foreach (var value in datosCierre)
                    {
                        ReporteCierreDeCaja reporteLinea = new ReporteCierreDeCaja();
                        reporteLinea.FondoInicio = (decimal)value.FondoInicio;
                        reporteLinea.VentasContado = (decimal)value.VentasContado;
                        reporteLinea.VentasCredito = (decimal)value.VentasCredito;
                        reporteLinea.VentasTarjeta = (decimal)value.VentasTarjeta;
                        reporteLinea.OtrasVentas = (decimal)value.OtrasVentas;
                        reporteLinea.RetencionIVA = (decimal)value.RetencionIVA;
                        reporteLinea.ComisionVT = (decimal)value.ComisionVT;
                        reporteLinea.Liquidacion = (decimal)value.LiquidacionTarjeta;
                        reporteLinea.IngresoCxCEfectivo = (decimal)value.IngresoCxCEfectivo;
                        reporteLinea.IngresoCxCTarjeta = (decimal)value.IngresoCxCTarjeta;
                        reporteLinea.DevolucionesProveedores = (decimal)value.DevolucionesProveedores;
                        reporteLinea.OtrosIngresos = (decimal)value.OtrosIngresos;
                        reporteLinea.ComprasContado = (decimal)value.ComprasContado;
                        reporteLinea.ComprasCredito = (decimal)value.ComprasCredito;
                        reporteLinea.OtrasCompras = (decimal)value.OtrasCompras;
                        reporteLinea.EgresoCxPEfectivo = (decimal)value.EgresoCxPEfectivo;
                        reporteLinea.DevolucionesClientes = (decimal)value.DevolucionesClientes;
                        reporteLinea.OtrosEgresos = (decimal)value.OtrosEgresos;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de cierre de Caja: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cierre de caja. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteProforma> ObtenerReporteProforma(int intIdProforma)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteProforma> listaReporte = new List<ReporteProforma>();
                    var datosCierre = dbContext.ProformaRepository.Where(a => a.IdProforma == intIdProforma)
                        .Join(dbContext.DetalleProformaRepository, b => b.IdProforma, b => b.IdProforma, (a, b) => new { a, b })
                        .Select(c => new { c.a.IdProforma, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Gravado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Gravado + c.a.Impuesto, c.a.Fecha, c.a.TipoPago, Descripcion = c.b.Producto.Codigo + " " + c.b.Producto.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });
                    foreach (var value in datosCierre)
                    {
                        ReporteProforma reporteLinea = new ReporteProforma();
                        reporteLinea.IdProforma = value.IdProforma;
                        reporteLinea.NombreCliente = value.NombreCliente;
                        reporteLinea.SubTotal = value.SubTotal;
                        reporteLinea.Descuento = value.Descuento;
                        reporteLinea.Impuesto = value.Impuesto;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        if (value.TipoPago == 1)
                            reporteLinea.TipoPago = "Contado";
                        else
                            reporteLinea.TipoPago = "Crédito";
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioVenta = value.PrecioVenta;
                        reporteLinea.TotalLinea = value.TotalLinea;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Proforma: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteOrdenServicio> ObtenerReporteOrdenServicio(int intIdOrdenServicio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteOrdenServicio> listaReporte = new List<ReporteOrdenServicio>();
                    var datosCierre = dbContext.OrdenServicioRepository.Where(a => a.IdOrden == intIdOrdenServicio)
                        .Join(dbContext.DetalleOrdenServicioRepository, b => b.IdOrden, b => b.IdOrden, (a, b) => new { a, b })
                        .Select(c => new { c.a.IdOrden, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Gravado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Gravado + c.a.Impuesto, c.a.Fecha, c.a.Placa, Descripcion = c.b.Producto.Codigo + " " + c.b.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });
                    foreach (var value in datosCierre)
                    {
                        ReporteOrdenServicio reporteLinea = new ReporteOrdenServicio();
                        reporteLinea.IdOrden = value.IdOrden;
                        reporteLinea.NombreCliente = value.NombreCliente;
                        reporteLinea.SubTotal = value.SubTotal;
                        reporteLinea.Descuento = value.Descuento;
                        reporteLinea.Impuesto = value.Impuesto;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Placa = value.Placa;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioVenta = value.PrecioVenta;
                        reporteLinea.TotalLinea = value.TotalLinea;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ordenes de Servicio: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ordenes de Servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteOrdenCompra> ObtenerReporteOrdenCompra(int intIdOrdenCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteOrdenCompra> listaReporte = new List<ReporteOrdenCompra>();
                    var datosCierre = dbContext.OrdenRepository.Where(a => a.IdOrdenCompra == intIdOrdenCompra)
                        .Join(dbContext.ProveedorRepository, b => b.IdProveedor, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.DetalleOrdenCompraRepository, c => c.a.IdOrdenCompra, d => d.IdOrdenCompra, (c, d) => new { c, d })
                        .Select(e => new { e.c.a.IdOrdenCompra, e.c.b.Nombre, SubTotal = e.c.a.Excento + e.c.a.Gravado + e.c.a.Impuesto + e.c.a.Descuento, e.c.a.Descuento, e.c.a.Impuesto, Total = e.c.a.Excento + e.c.a.Gravado + e.c.a.Impuesto, e.c.a.Fecha, e.c.a.TipoPago, Descripcion = e.d.Producto.Codigo + " " + e.d.Producto.Descripcion, e.d.Cantidad, e.d.PrecioCosto, TotalLinea = e.d.Cantidad * e.d.PrecioCosto });
                    foreach (var value in datosCierre)
                    {
                        ReporteOrdenCompra reporteLinea = new ReporteOrdenCompra();
                        reporteLinea.IdOrden = value.IdOrdenCompra;
                        reporteLinea.Nombre = value.Nombre;
                        reporteLinea.SubTotal = value.SubTotal;
                        reporteLinea.Descuento = value.Descuento;
                        reporteLinea.Impuesto = value.Impuesto;
                        reporteLinea.Total = value.Total;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        if (value.TipoPago == 1)
                            reporteLinea.TipoPago = "Contado";
                        else
                            reporteLinea.TipoPago = "Crédito";
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioCosto = value.PrecioCosto;
                        reporteLinea.TotalLinea = value.TotalLinea;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ordenes de Compra: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ordenes de Compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteInventario> ObtenerReporteInventario(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteInventario> listaReporte = new List<ReporteInventario>();
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Tipo == StaticTipoProducto.Producto);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    else if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    else if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    var detalle = listaProductos.Select(a => new { a.IdProducto, a.Codigo, a.Descripcion, a.Cantidad, a.PrecioCosto, a.PrecioVenta1 });

                    foreach (var value in detalle)
                    {
                        ReporteInventario reporteLinea = new ReporteInventario();
                        reporteLinea.IdProducto = value.IdProducto;
                        reporteLinea.Codigo = value.Codigo;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioCosto = value.PrecioCosto;
                        reporteLinea.PrecioVenta = value.PrecioVenta1;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Inventario: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteMovimientosContables> ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteMovimientosContables> listaReporte = new List<ReporteMovimientosContables>();
                    DateTime datFechaActual = DateTime.Now;
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, b => b.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial && x.d.Fecha <= datFechaFinal && x.d.Nulo == false)
                        .GroupBy(x => x.c.a.Descripcion)
                        .Select(a => new { TotalDebito = a.Sum(b => b.c.b.Debito), TotalCredito = a.Sum(b => b.c.b.Credito), Descripcion = a.Key });
                    foreach (var value in listaCuentas)
                    {
                        ReporteMovimientosContables reporteLinea = new ReporteMovimientosContables();
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.SaldoDebe = value.TotalDebito;
                        reporteLinea.SaldoHaber = value.TotalCredito;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de movimientos contables: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteBalanceComprobacion> ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteBalanceComprobacion> listaReporte = new List<ReporteBalanceComprobacion>();
                    DateTime datFechaActual = DateTime.Now;
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReporteBalanceComprobacion reporteLinea = new ReporteBalanceComprobacion();
                        reporteLinea.IdCuenta = value.IdCuenta;
                        reporteLinea.Descripcion = value.Descripcion;
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
                    log.Error("Error al procesar el reporte de balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReportePerdidasyGanancias> ObtenerReportePerdidasyGanancias(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReportePerdidasyGanancias> listaReporte = new List<ReportePerdidasyGanancias>();
                    DateTime datFechaActual = DateTime.Now;
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EsCuentaBalance == true && x.SaldoActual != 0 && x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReportePerdidasyGanancias reporteLinea = new ReportePerdidasyGanancias();
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.IdTipoCuenta = value.IdTipoCuenta;
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
                    log.Error("Error al procesar el reporte de balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDetalleMovimientosCuentasDeBalance> ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                        ReporteDetalleMovimientosCuentasDeBalance reporteLinea = new ReporteDetalleMovimientosCuentasDeBalance();
                        reporteLinea.DescCuentaBalance = cuentaDeBalance.Descripcion;
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.SaldoInicial = value.SaldoAnterior;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Detalle = value.Detalle;
                        reporteLinea.Debito = value.Debito;
                        reporteLinea.Credito = value.Credito;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de detalle del balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle del balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteEgreso> ObtenerReporteEgreso(int intIdEgreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteEgreso> listaReporte = new List<ReporteEgreso>();
                    var datosEgreso = dbContext.EgresoRepository.Where(a => a.IdEgreso == intIdEgreso)
                        .Join(dbContext.DesglosePagoEgresoRepository, b => b.IdEgreso, b => b.IdEgreso, (a, b) => new { a, b })
                        .Join(dbContext.FormaPagoRepository, c => c.b.IdFormaPago, d => d.IdFormaPago, (c, d) => new { c, d })
                        .Select(c => new { c.c.a.IdEgreso, c.c.a.Fecha, c.c.a.Detalle, c.c.a.Beneficiario, c.c.a.Monto, c.d.Descripcion, c.c.b.MontoLocal });
                    foreach (var value in datosEgreso)
                    {
                        ReporteEgreso reporteLinea = new ReporteEgreso();
                        reporteLinea.IdEgreso = value.IdEgreso;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Detalle = value.Detalle;
                        reporteLinea.Beneficiario = value.Beneficiario;
                        reporteLinea.Monto = value.Monto;
                        reporteLinea.MontoEnLetras = Utilitario.NumeroALetras((double)value.Monto);
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.MontoLocal = value.MontoLocal;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Egreso: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteIngreso> ObtenerReporteIngreso(int intIdIngreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteIngreso> listaReporte = new List<ReporteIngreso>();
                    var datosIngreso = dbContext.IngresoRepository.Where(a => a.IdIngreso == intIdIngreso)
                        .Join(dbContext.DesglosePagoIngresoRepository, b => b.IdIngreso, b => b.IdIngreso, (a, b) => new { a, b })
                        .Join(dbContext.FormaPagoRepository, c => c.b.IdFormaPago, d => d.IdFormaPago, (c, d) => new { c, d })
                        .Select(c => new { c.c.a.IdIngreso, c.c.a.Fecha, c.c.a.RecibidoDe, c.c.a.Detalle, c.c.a.Monto, c.d.Descripcion, c.c.b.MontoLocal });
                    foreach (var value in datosIngreso)
                    {
                        ReporteIngreso reporteLinea = new ReporteIngreso();
                        reporteLinea.IdIngreso = value.IdIngreso;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.RecibidoDe = value.RecibidoDe;
                        reporteLinea.Detalle = value.Detalle;
                        reporteLinea.Monto = value.Monto;
                        reporteLinea.MontoEnLetras = Utilitario.NumeroALetras((double)value.Monto);
                        reporteLinea.Descripcion = value.Descripcion;
                        reporteLinea.MontoLocal = value.MontoLocal;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ingreso: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ingreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteFacturasElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasEmitidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 1, 4 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasEmitidas)
                    {
                        string strNombreReceptor = "CLIENTE DE CONTADO";
                        string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                        {
                            XmlNode receptorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                            strNombreReceptor = receptorNode["Nombre"] != null && receptorNode["Nombre"].ChildNodes.Count > 0 ? receptorNode["Nombre"].InnerText : "CLIENTE DE CONTADO";
                        }
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
                        ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                        reporteLinea.ClaveNumerica = documento.ClaveNumerica;
                        reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Nombre = strNombreReceptor;
                        reporteLinea.Moneda = strMoneda;
                        reporteLinea.Impuesto = decTotalImpuesto;
                        reporteLinea.Total = decTotal;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Documentos Emitidos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos emitidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasEmitidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && a.IdTipoDocumento == 3 && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasEmitidas)
                    {
                        string strNombreReceptor = "CLIENTE DE CONTADO";
                        string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                        {
                            XmlNode receptorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                            strNombreReceptor = receptorNode["Nombre"] != null && receptorNode["Nombre"].ChildNodes.Count > 0 ? receptorNode["Nombre"].InnerText : "CLIENTE DE CONTADO";
                        }
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
                        ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                        reporteLinea.ClaveNumerica = documento.Consecutivo;
                        reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Nombre = strNombreReceptor;
                        reporteLinea.Moneda = strMoneda;
                        reporteLinea.Impuesto = decTotalImpuesto;
                        reporteLinea.Total = decTotal;
                        listaReporte.Add(reporteLinea);
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Documentos Emitidos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos emitidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasRecibidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && a.IdTipoDocumento == 5 && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasRecibidas)
                    {
                        if (documento.DatosDocumentoOri != null)
                        {
                            string datosXml = Encoding.Default.GetString(documento.DatosDocumentoOri);
                            XmlDocument documentoXml = new XmlDocument();
                            documentoXml.LoadXml(datosXml);
                            if (documentoXml.DocumentElement.Name == "FacturaElectronica")
                            {
                                decimal decTotalImpuesto = 0;
                                decimal decTotal = 0;
                                string strCodigoMoneda = "CRC";
                                decimal decTipoDeCambio = 1;
                                string strNombreEmisor = "";
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
                                ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                                reporteLinea.ClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
                                reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = strNombreEmisor;
                                reporteLinea.Moneda = strCodigoMoneda;
                                reporteLinea.Impuesto = decTotalImpuesto;
                                reporteLinea.Total = decTotal;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                            XmlDocument documentoXml = new XmlDocument();
                            documentoXml.LoadXml(datosXml);
                            decimal decTotalImpuesto = 0;
                            decimal decTotal = 0;
                            string strCodigoMoneda = "CRC";
                            string strNombreEmisor = "";
                            if (documentoXml.GetElementsByTagName("TotalFactura").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalFactura").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("MontoTotalImpuesto").Count > 0)
                                decTotalImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("MontoTotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                            reporteLinea.ClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
                            reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                            reporteLinea.Nombre = strNombreEmisor;
                            reporteLinea.Moneda = strCodigoMoneda;
                            reporteLinea.Impuesto = decTotalImpuesto;
                            reporteLinea.Total = decTotal;
                            listaReporte.Add(reporteLinea);
                        }

                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Documentos Emitidos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos emitidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteDocumentoElectronico> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasRecibidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && a.IdTipoDocumento == 5 && a.DatosDocumentoOri != null && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasRecibidas)
                    {
                        string datosXml = Encoding.Default.GetString(documento.DatosDocumentoOri);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (documentoXml.DocumentElement.Name == "NotaCreditoElectronica")
                        {
                            string strNombreEmisor = "";
                            if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                                strNombreEmisor = emisorNode["Nombre"].InnerText;
                            }
                            decimal decTotalImpuesto = 0;
                            if (documentoXml.GetElementsByTagName("TotalImpuesto").Count > 0)
                                decTotalImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("TotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            decimal decTotal = 0;
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                            string strCodigoMoneda = "CRC";
                            if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                                strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                            decimal decTipoDeCambio = 1;
                            if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                                decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (strCodigoMoneda != "CRC")
                            {
                                decTotal = decTotal * decTipoDeCambio;
                                decTotalImpuesto = decTotalImpuesto * decTipoDeCambio;
                            }
                            ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                            reporteLinea.ClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
                            reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                            reporteLinea.Nombre = strNombreEmisor;
                            reporteLinea.Moneda = strCodigoMoneda;
                            reporteLinea.Impuesto = decTotalImpuesto;
                            reporteLinea.Total = decTotal;
                            listaReporte.Add(reporteLinea);
                        }
                    }
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Documentos Emitidos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de documentos electrónicos emitidos. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<ReporteResumenMovimiento> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteResumenMovimiento> listaReporte = new List<ReporteResumenMovimiento>();
                    var grupoFacturasEmitidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && new[] { 1, 4 }.Any(s => s == a.IdTipoDocumento) && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
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
                        string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
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
                                if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                {
                                    switch (strTarifa)
                                    {
                                        case "1":
                                            decVentaServiciosTasa1 += decMontoPorLinea;
                                            break;
                                        case "2":
                                            decVentaServiciosTasa2 += decMontoPorLinea;
                                            break;
                                        case "4":
                                            decVentaServiciosTasa4 += decMontoPorLinea;
                                            break;
                                        case "8":
                                            decVentaServiciosTasa8 += decMontoPorLinea;
                                            break;
                                        case "13":
                                            decVentaServiciosTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (strTarifa)
                                    {
                                        case "1":
                                            decVentaBienesTasa1 += decMontoPorLinea;
                                            break;
                                        case "2":
                                            decVentaBienesTasa2 += decMontoPorLinea;
                                            break;
                                        case "4":
                                            decVentaBienesTasa4 += decMontoPorLinea;
                                            break;
                                        case "8":
                                            decVentaBienesTasa8 += decMontoPorLinea;
                                            break;
                                        case "13":
                                            decVentaBienesTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                    decVentaServiciosExento += decMontoPorLinea;
                                else
                                    decVentaBienesExento += decMontoPorLinea;
                            }
                        }
                        if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                            decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                        if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                            strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
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

                    var grupoNotasCreditoEmitidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && a.IdTipoDocumento == 3 && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    foreach (var documento in grupoNotasCreditoEmitidas)
                    {
                        string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
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
                                if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                {
                                    switch (strTarifa)
                                    {
                                        case "1":
                                            decVentaServiciosTasa1 += decMontoPorLinea;
                                            break;
                                        case "2":
                                            decVentaServiciosTasa2 += decMontoPorLinea;
                                            break;
                                        case "4":
                                            decVentaServiciosTasa4 += decMontoPorLinea;
                                            break;
                                        case "8":
                                            decVentaServiciosTasa8 += decMontoPorLinea;
                                            break;
                                        case "13":
                                            decVentaServiciosTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (strTarifa)
                                    {
                                        case "1":
                                            decVentaBienesTasa1 += decMontoPorLinea;
                                            break;
                                        case "2":
                                            decVentaBienesTasa2 += decMontoPorLinea;
                                            break;
                                        case "4":
                                            decVentaBienesTasa4 += decMontoPorLinea;
                                            break;
                                        case "8":
                                            decVentaBienesTasa8 += decMontoPorLinea;
                                            break;
                                        case "13":
                                            decVentaBienesTasa13 += decMontoPorLinea;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                    decVentaServiciosExento += decMontoPorLinea;
                                else
                                    decVentaBienesExento += decMontoPorLinea;
                            }
                        }
                        if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                            strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                        if (strCodigoMoneda != "CRC")
                            if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                                decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
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
                    ReporteResumenMovimiento reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Ventas de bienes o mercancias";
                    reporteLinea.Exento = decTotalVentaBienesExento;
                    reporteLinea.Tasa1 = decTotalVentaBienesTasa1;
                    reporteLinea.Tasa2 = decTotalVentaBienesTasa2;
                    reporteLinea.Tasa4 = decTotalVentaBienesTasa4;
                    reporteLinea.Tasa8 = decTotalVentaBienesTasa8;
                    reporteLinea.Tasa13 = decTotalVentaBienesTasa13;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Ventas de servicios";
                    reporteLinea.Exento = decTotalVentaServiciosExento;
                    reporteLinea.Tasa1 = decTotalVentaServiciosTasa1;
                    reporteLinea.Tasa2 = decTotalVentaServiciosTasa2;
                    reporteLinea.Tasa4 = decTotalVentaServiciosTasa4;
                    reporteLinea.Tasa8 = decTotalVentaServiciosTasa8;
                    reporteLinea.Tasa13 = decTotalVentaServiciosTasa13;
                    listaReporte.Add(reporteLinea);
                    var grupoFacturasRecibidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa && a.Fecha >= datFechaInicial && a.Fecha <= datFechaFinal && a.IdTipoDocumento == 5 && a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
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
                        if (documento.DatosDocumentoOri != null)
                        {
                            string datosXml = Encoding.Default.GetString(documento.DatosDocumentoOri);
                            XmlDocument documentoXml = new XmlDocument();
                            documentoXml.LoadXml(datosXml);
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
                            foreach (XmlNode lineaDetalle in documentoXml.GetElementsByTagName("LineaDetalle"))
                            {
                                decimal decMontoPorLinea = decimal.Parse(lineaDetalle["SubTotal"].InnerText, CultureInfo.InvariantCulture);
                                if (lineaDetalle["Impuesto"] != null)
                                {
                                    if (lineaDetalle["Impuesto"]["Exoneracion"] != null)
                                    {
                                        int porcentaje = int.Parse(lineaDetalle["Impuesto"]["Exoneracion"]["PorcentajeExoneracion"].InnerText, CultureInfo.InvariantCulture);
                                        decMontoPorLinea = decMontoPorLinea * (100 - porcentaje) / 100;
                                    }
                                    string strTarifa = lineaDetalle["Impuesto"]["Tarifa"].InnerText;
                                    if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                    {
                                        switch (strTarifa)
                                        {
                                            case "1":
                                            case "1.00":
                                                decCompraServiciosTasa1 += decMontoPorLinea;
                                                break;
                                            case "2":
                                            case "2.00":
                                                decCompraServiciosTasa2 += decMontoPorLinea;
                                                break;
                                            case "4":
                                            case "4.00":
                                                decCompraServiciosTasa4 += decMontoPorLinea;
                                                break;
                                            case "8":
                                            case "8.00":
                                                decCompraServiciosTasa8 += decMontoPorLinea;
                                                break;
                                            case "13":
                                            case "13.00":
                                                decCompraServiciosTasa13 += decMontoPorLinea;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (strTarifa)
                                        {
                                            case "1":
                                            case "1.00":
                                                decCompraBienesTasa1 += decMontoPorLinea;
                                                break;
                                            case "2":
                                            case "2.00":
                                                decCompraBienesTasa2 += decMontoPorLinea;
                                                break;
                                            case "4":
                                            case "4.00":
                                                decCompraBienesTasa4 += decMontoPorLinea;
                                                break;
                                            case "8":
                                            case "8.00":
                                                decCompraBienesTasa8 += decMontoPorLinea;
                                                break;
                                            case "13":
                                            case "13.00":
                                                decCompraBienesTasa13 += decMontoPorLinea;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (lineaDetalle["UnidadMedida"].InnerText == "Sp")
                                        decCompraServiciosExento += decMontoPorLinea;
                                    else
                                        decCompraBienesExento += decMontoPorLinea;
                                }
                            }
                            if (documentoXml.GetElementsByTagName("CodigoMoneda").Count > 0)
                                strCodigoMoneda = documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText;
                            if (strCodigoMoneda != "CRC")
                                if (documentoXml.GetElementsByTagName("TipoCambio").Count > 0)
                                    decTipoDeCambio = decimal.Parse(documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.DocumentElement.Name != "NotaCreditoElectronica")
                            {
                                if (documento.EsIvaAcreditable == "S")
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
                                }
                            }
                            else
                            {
                                if (documento.EsIvaAcreditable == "S")
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
                                }
                            }
                        }
                        else
                        {
                            string datosXml = Encoding.Default.GetString(documento.DatosDocumento);
                            XmlDocument documentoXml = new XmlDocument();
                            documentoXml.LoadXml(datosXml);
                            decimal decImpuestoPorLinea = 0;
                            decimal decTotalPorLinea = 0;
                            if (documentoXml.GetElementsByTagName("TotalFactura").Count > 0)
                                decTotalPorLinea = decimal.Parse(documentoXml.GetElementsByTagName("TotalFactura").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (documentoXml.GetElementsByTagName("MontoTotalImpuesto").Count > 0)
                                decImpuestoPorLinea = decimal.Parse(documentoXml.GetElementsByTagName("MontoTotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                            if (decImpuestoPorLinea > 0)
                            {
                                decTotalCompraBienesIvaTasa13 += (decTotalPorLinea - decImpuestoPorLinea);
                            }
                        }
                    }
                    reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Compras de bienes o mercancias IVA acreditable";
                    reporteLinea.Exento = decTotalCompraBienesIvaExento;
                    reporteLinea.Tasa1 = decTotalCompraBienesIvaTasa1;
                    reporteLinea.Tasa2 = decTotalCompraBienesIvaTasa2;
                    reporteLinea.Tasa4 = decTotalCompraBienesIvaTasa4;
                    reporteLinea.Tasa8 = decTotalCompraBienesIvaTasa8;
                    reporteLinea.Tasa13 = decTotalCompraBienesIvaTasa13;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Compras de servicios IVA acreditable";
                    reporteLinea.Exento = decTotalCompraServiciosIvaExento;
                    reporteLinea.Tasa1 = decTotalCompraServiciosIvaTasa1;
                    reporteLinea.Tasa2 = decTotalCompraServiciosIvaTasa2;
                    reporteLinea.Tasa4 = decTotalCompraServiciosIvaTasa4;
                    reporteLinea.Tasa8 = decTotalCompraServiciosIvaTasa8;
                    reporteLinea.Tasa13 = decTotalCompraServiciosIvaTasa13;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Compras de bienes o mercancias sin IVA acreditable";
                    reporteLinea.Exento = decTotalCompraBienesGastoExento;
                    reporteLinea.Tasa1 = decTotalCompraBienesGastoTasa1;
                    reporteLinea.Tasa2 = decTotalCompraBienesGastoTasa2;
                    reporteLinea.Tasa4 = decTotalCompraBienesGastoTasa4;
                    reporteLinea.Tasa8 = decTotalCompraBienesGastoTasa8;
                    reporteLinea.Tasa13 = decTotalCompraBienesGastoTasa13;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteResumenMovimiento();
                    reporteLinea.Descripcion = "Compras de servicios sin IVA acreditable";
                    reporteLinea.Exento = decTotalCompraServiciosGastoExento;
                    reporteLinea.Tasa1 = decTotalCompraServiciosIvaTasa1;
                    reporteLinea.Tasa2 = decTotalCompraServiciosGastoTasa2;
                    reporteLinea.Tasa4 = decTotalCompraServiciosGastoTasa4;
                    reporteLinea.Tasa8 = decTotalCompraServiciosGastoTasa8;
                    reporteLinea.Tasa13 = decTotalCompraServiciosGastoTasa13;
                    listaReporte.Add(reporteLinea);
                    return listaReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteVentasGenerales(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptVentas.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteVentas> dstDatos = ObtenerReporteVentasPorCliente(intIdEmpresa, strFechaInicial, strFechaFinal, 0, false, 0, 0);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Reporte de Ventas Generales");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteVentasGenerales." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de ventas generales por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteVentasAnuladas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptVentas.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteVentas> dstDatos = ObtenerReporteVentasPorCliente(intIdEmpresa, strFechaInicial, strFechaFinal, 0, true, 0, 0);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Reporte de Ventas Anuladas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteVentasAnuladas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de ventas anuladas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteResumenMovimientos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptResumenMovimientos.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteEstadoResultados> dstDatos = ObtenerReporteEstadoResultados(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteResumenDeMovimientos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de resumen de movimientos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteDetalleEgresos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptDetalleEgresos.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDetalleEgreso> dstDatos = ObtenerReporteDetalleEgreso(intIdEmpresa, 0, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteDetalleDeEgresos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte detallado de egresos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteFacturasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptComprobanteElectronico.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteFacturasElectronicasEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Emitidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteFacturasElectronicasEmitidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de facturas electrónicas emitidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al enviar el reporte de listado facturas electrónicas emitidas: ", ex);
                    throw new Exception("Se produjo un error al enviar el reporte de listado facturas electrónicas emitidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteFacturasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptComprobanteElectronico.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteFacturasElectronicasRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Recibidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteFacturasElectronicasRecibidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de facturas electrónicas recibidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al enviar el reporte de listado facturas electrónicas recibidas: ", ex);
                    throw new Exception("Se produjo un error al enviar el reporte de listado facturas electrónicas recibidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteNotasCreditoEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptComprobanteElectronico.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteNotasCreditoElectronicasEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Notas de Crédito Electrónicas Emitidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteNotasCreditoElectrónicasEmitidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de notas de crédito electrónicas emitidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al enviar el reporte de listado notas de crédito electrónicas emitidas: ", ex);
                    throw new Exception("Se produjo un error al enviar el reporte de listado notas de crédito electrónicas emitidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteNotasCreditoRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptComprobanteElectronico.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteDocumentoElectronico> dstDatos = ObtenerReporteNotasCreditoElectronicasRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[5];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pNombreReporte", "Listado de Notas de Crédito Electrónicas Recibidas");
                    parameters[3] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[4] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteNotasCreditoElectrónicasRecibidas." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte de notas de crédito electrónicas recibidas por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al enviar el reporte de listado notas de crédito electrónicas recibidas: ", ex);
                    throw new Exception("Se produjo un error al enviar el reporte de listado notas de crédito electrónicas recibidas. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EnviarReporteResumenMovimientosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strFormatoReporte, ICorreoService servicioEnvioCorreo, string strCorreoEnvio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    string strPlantillaReporte = "rptResumenComprobanteElectronico.rdlc";
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    string strNombreEmpresa = empresa.NombreComercial != "" ? empresa.NombreComercial : empresa.NombreEmpresa;
                    IList<ReporteResumenMovimiento> dstDatos = ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, strFechaInicial, strFechaFinal);
                    ReportDataSource rds = new ReportDataSource("dstDatos", dstDatos);
                    ReportParameter[] parameters = new ReportParameter[4];
                    parameters[0] = new ReportParameter("pUsuario", "SYSTEM");
                    parameters[1] = new ReportParameter("pEmpresa", strNombreEmpresa);
                    parameters[2] = new ReportParameter("pFechaDesde", strFechaInicial);
                    parameters[3] = new ReportParameter("pFechaHasta", strFechaInicial);
                    byte[] bytes = GenerarContenidoReporte(strFormatoReporte, strPlantillaReporte, rds, parameters);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "ReporteResumenMovimientosElectronicos." + strFormatoReporte.ToLower(),
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(strCorreoEnvio, new string[] { empresa.CorreoNotificacion }, new string[] { }, "JLC Solutions CR - Reporte resumen de movimientos comprobantes electrónicos por rango de fechas", "Adjunto archivo en formato " + strFormatoReporte + " correspondiente al reporte de ventas por cliente para el rango de fechas solicitado.", false, jarrayObj);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al enviar el reporte de resumen de documentos electrónicos: ", ex);
                    throw new Exception("Se produjo un error al enviar el reporte de resumen de documentos electrónicos. Por favor consulte con su proveedor.");
                }
            }
        }

        byte[] GenerarContenidoReporte(string strFormatoReporte, string strPlantillaReporte, ReportDataSource rds, ReportParameter[] parameters)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Clear();
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.ProcessingMode = ProcessingMode.Local;
                    string reportPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "PlantillaReportes/" + strPlantillaReporte;
                    viewer.LocalReport.ReportPath = reportPath;
                    viewer.LocalReport.SetParameters(parameters);
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = viewer.LocalReport.Render(strFormatoReporte, null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    return bytes;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Documentos Electrónicos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte resumen de documentos electrónicos. Por favor consulte con su proveedor.");

                }
            }
        }
    }
}