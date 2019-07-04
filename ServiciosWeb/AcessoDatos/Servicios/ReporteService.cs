using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using LeandroSoftware.Core;
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.Core.Dominio.Entidades;
using log4net;
using Unity;
using System.Globalization;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using LeandroSoftware.AccesoDatos.TiposDatos;
using System.IO;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IReporteService
    {
        IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoFactura();
        IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoCompra();
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
        List<ReporteEstadoResultados> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
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

        public IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoFactura()
        {
            IList<CondicionVentaYFormaPago> listado = null;
            try
            {
                listado = new List<CondicionVentaYFormaPago>();
                var condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoEfectivo, "Ventas de contado en efectivo");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario, "Ventas de contado mediante transferencia o depósito bancario");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoCheque, "Ventas de contado mediante cheque");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoTarjeta, "Ventas de contado mediante pago con tarjeta");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.Credito, "Ventas de crédito");
                listado.Add(condicionVenta);
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
            }
            return listado;
        }

        public IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoCompra()
        {
            IList<CondicionVentaYFormaPago> listado = null;
            try
            {
                listado = new List<CondicionVentaYFormaPago>();
                var condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoEfectivo, "Compras de contado en efectivo");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario, "Compras de contado mediante transferencia o depósito bancario");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.ContadoCheque, "Compras de contado mediante cheque");
                listado.Add(condicionVenta);
                condicionVenta = new CondicionVentaYFormaPago(StaticReporteCondicionVentaFormaPago.Credito, "Compras de crédito");
                listado.Add(condicionVenta);
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
            }
            return listado;
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
                        var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                            .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto - z.x.Descuento) });
                        foreach (var value in detalleVentas)
                        {
                            ReporteVentas reporteLinea = new ReporteVentas();
                            reporteLinea.IdFactura = value.IdFactura;
                            reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                            reporteLinea.Nombre = value.NombreCliente;
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
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto - z.x.Descuento) });
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
                                reporteLinea.NoDocumento = value.NoDocumento;
                                reporteLinea.Impuesto = value.Impuesto;
                                reporteLinea.Total = value.Total;
                                listaReporte.Add(reporteLinea);
                            }
                        }
                        else
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                                .Select(z => new { z.x.x.IdCliente, z.x.x.Nulo, z.x.x.IdCondicionVenta, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.NoDocumento, z.x.x.Impuesto, Total = z.y.MontoLocal, z.x.x.TotalCosto });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoEfectivo)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Efectivo);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTarjeta)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Tarjeta);
                                if (intIdBancoAdquiriente > 0)
                                    detalleVentas = detalleVentas.Where(x => x.IdCuentaBanco == intIdBancoAdquiriente);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoCheque)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Cheque);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario)
                            {
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario);
                            }
                            if (intIdCliente > 0)
                                detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                            foreach (var value in detalleVentas)
                            {
                                ReporteVentas reporteLinea = new ReporteVentas();
                                reporteLinea.IdFactura = value.IdFactura;
                                reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                                reporteLinea.Nombre = value.NombreCliente;
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
                    var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == false)
                        .Join(dbContext.VendedorRepository, x => x.IdVendedor, y => y.IdVendedor, (x, y) => new { x, y })
                        .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Select(z => new { z.x.x.IdVendedor, z.x.y.Nombre, z.x.x.Nulo, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.NoDocumento, Total = z.y.MontoLocal, z.x.x.TotalCosto, z.x.x.Impuesto });
                    if (intIdVendedor > 0)
                        detalleVentas = detalleVentas.Where(x => x.IdVendedor == intIdVendedor);
                    foreach (var value in detalleVentas)
                    {
                        ReporteVentasPorVendedor reporteLinea = new ReporteVentasPorVendedor();
                        reporteLinea.NombreVendedor = value.Nombre;
                        reporteLinea.IdFactura = value.IdFactura;
                        reporteLinea.Fecha = value.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.NombreCliente = value.NombreCliente;
                        reporteLinea.NoDocumento = value.NoDocumento;
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
                        var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                            .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
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
                            var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
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
                            var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                                .Join(dbContext.DesglosePagoCompraRepository, x => x.x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                                .Select(z => new { z.x.x.IdProveedor, z.x.x.Nulo, z.y.IdFormaPago, z.x.x.IdCondicionVenta, z.x.x.IdCompra, z.x.x.Fecha, NombreProveedor = z.x.y.Nombre, z.x.x.NoDocumento, z.x.x.Impuesto, Total = z.y.MontoLocal });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoEfectivo)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Efectivo);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTarjeta)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Tarjeta);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoCheque)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.Cheque);
                            }
                            else if (intTipoPago == StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario)
                            {
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Contado & x.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario);
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
                    var detalleCxCClientes = dbContext.CuentaPorCobrarRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Saldo > 0 & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Tipo == StaticTipoCuentaPorCobrar.Clientes)
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
                    var detalleCxPProveedores = dbContext.CuentaPorPagarRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Saldo > 0 & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Tipo == StaticTipoCuentaPorPagar.Proveedores)
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
                    var cxcClientes = dbContext.CuentaPorCobrarRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Nulo == false & a.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, a => a.IdPropietario, b => b.IdCliente, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorCobrarRepository, a => a.a.IdCxC, d => d.IdCxC, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorCobrarRepository, a => a.c.IdMovCxC, d => d.IdMovCxC, (b, c) => new { b, c }).Where(s => !s.c.Nulo & s.c.Fecha >= datFechaInicial & s.c.Fecha <= datFechaFinal)
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
                    var cxpProveedores = dbContext.CuentaPorPagarRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Nulo == false & a.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, a => a.IdPropietario, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorPagarRepository, a => a.a.IdCxP, d => d.IdCxP, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorPagarRepository, a => a.c.IdMovCxP, d => d.IdMovCxP, (b, c) => new { b, c }).Where(s => !s.c.Nulo & s.c.Fecha >= datFechaInicial & s.c.Fecha <= datFechaFinal)
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
                    var movimientoBanco = dbContext.MovimientoBancoRepository.Where(s => s.IdCuenta == intIdCuenta & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
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
                        .Where(s => s.x.IdEmpresa == intIdEmpresa & s.x.Nulo == false & s.x.Fecha >= datFechaInicial & s.x.Fecha <= datFechaFinal)
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
                    var ingreso = dbContext.IngresoRepository.Where(w => w.IdEmpresa == intIdEmpresa & w.Nulo == false & w.Fecha >= datFechaInicial & w.Fecha <= datFechaFinal)
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
                    if (grupoFacturas.Count() == 0 & ingreso.Count() == 0)
                    {
                        ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                        reporteLinea.NombreTipoRegistro = "Ingresos";
                        reporteLinea.Descripcion = "No hay registros";
                        reporteLinea.Valor = 0;
                        listaReporte.Add(reporteLinea);
                    }
                    var grupoCompras = dbContext.CompraRepository.Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa & s.x.Nulo == false & s.x.Fecha >= datFechaInicial & s.x.Fecha <= datFechaFinal)
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
                    var egreso = dbContext.EgresoRepository.Where(w => w.IdEmpresa == intIdEmpresa & w.Nulo == false & w.Fecha >= datFechaInicial & w.Fecha <= datFechaFinal)
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

                    if (grupoCompras.Count() == 0 & egreso.Count() == 0)
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
                    var egreso = dbContext.EgresoRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdEgreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in egreso)
                    {
                        if (intIdCuentaEgreso > 0 & value.IdCuenta != intIdCuentaEgreso)
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
                    var ingreso = dbContext.IngresoRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdIngreso, z.b.Descripcion, z.a.Fecha, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in ingreso)
                    {
                        if (intIdCuentaIngreso > 0 & value.IdCuenta != intIdCuentaIngreso)
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
                    var ventasResumen = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.y.PorcentajeIVA, z.x.x.x.Excento, z.x.x.x.Grabado, z.x.x.x.Impuesto, z.x.x.x.Descuento, Costo = z.x.x.y.Cantidad * z.x.x.y.PrecioCosto });
                    foreach (var value in ventasResumen)
                    {
                        ReporteVentasPorLineaResumen reporteLinea = new ReporteVentasPorLineaResumen();
                        reporteLinea.Codigo = value.Codigo;
                        reporteLinea.IdLinea = value.IdLinea;
                        reporteLinea.NombreLinea = value.NombreLinea;
                        reporteLinea.Cantidad = value.Cantidad;
                        reporteLinea.PrecioVenta = value.PrecioVenta;
                        reporteLinea.Excento = value.Excento;
                        reporteLinea.Grabado = value.Grabado;
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
                    var ventasDetalle = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.Descripcion, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.y.PorcentajeIVA, z.x.x.x.Excento, z.x.x.x.Grabado, z.x.x.x.Impuesto, z.x.x.x.Descuento });
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
                        reporteLinea.Grabado = value.Grabado;
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
                        .Select(c => new { c.a.IdProforma, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Grabado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Grabado + c.a.Impuesto, c.a.Fecha, c.a.TipoPago, Descripcion = c.b.Producto.Codigo + " " + c.b.Producto.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });
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
                        .Select(c => new { c.a.IdOrden, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Grabado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Grabado + c.a.Impuesto, c.a.Fecha, c.a.Placa, Descripcion = c.b.Producto.Codigo + " " + c.b.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });
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
                        .Select(e => new { e.c.a.IdOrdenCompra, e.c.b.Nombre, SubTotal = e.c.a.Excento + e.c.a.Grabado + e.c.a.Impuesto + e.c.a.Descuento, e.c.a.Descuento, e.c.a.Impuesto, Total = e.c.a.Excento + e.c.a.Grabado + e.c.a.Impuesto, e.c.a.Fecha, e.c.a.TipoPago, Descripcion = e.d.Producto.Codigo + " " + e.d.Producto.Descripcion, e.d.Cantidad, e.d.PrecioCosto, TotalLinea = e.d.Cantidad * e.d.PrecioCosto });
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
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.Tipo == StaticTipoProducto.Producto);
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
                        .Where(x => x.d.Fecha >= datFechaInicial & x.d.Fecha <= datFechaFinal & x.d.Nulo == false)
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
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();
                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        ReporteBalanceComprobacion reporteLinea = new ReporteBalanceComprobacion();
                        reporteLinea.IdCuenta = value.IdCuenta;
                        reporteLinea.Descripcion = value.Descripcion;
                        if (intMes > 0 & intAnnio > 0)
                            decSaldo = dbContext.SaldoMensualContableRepository.Where(x => x.Mes == intMes & x.Annio == intAnnio & x.IdCuenta == value.IdCuenta).Select(a => a.SaldoFinMes).FirstOrDefault();
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
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true & x.SaldoActual != 0 & x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
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
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.IdCuentaGrupo == intIdCuentaGrupo)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7)
                        .Join(dbContext.DetalleAsientoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Join(dbContext.AsientoRepository, c => c.b.IdAsiento, d => d.IdAsiento, (c, d) => new { c, d })
                        .Where(x => x.d.Fecha >= datFechaInicial & x.d.Fecha <= datFechaFinal & x.d.Nulo == false)
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
                    var datosFacturasEmitidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 1 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasEmitidas)
                    {
                        string strNombreReceptor = "Cliente de contado";
                        XmlSerializer serializer = new XmlSerializer(typeof(FacturaElectronica));
                        FacturaElectronica facturaElectronica;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            facturaElectronica = (FacturaElectronica)serializer.Deserialize(memStream);
                        if (facturaElectronica.Receptor != null)
                        {
                            strNombreReceptor = facturaElectronica.Receptor.Nombre;
                        }
                        decimal decTotalImpuesto = facturaElectronica.ResumenFactura.TotalImpuestoSpecified ? facturaElectronica.ResumenFactura.TotalImpuesto : 0;
                        string strMoneda = facturaElectronica.ResumenFactura.CodigoMonedaSpecified ? facturaElectronica.ResumenFactura.CodigoMoneda.ToString() : "CRC";
                        decimal decTotal = facturaElectronica.ResumenFactura.TotalComprobante;
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
                    var datosFacturasEmitidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 3 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasEmitidas)
                    {
                        string strNombreReceptor = "Cliente de contado";
                        XmlSerializer serializer = new XmlSerializer(typeof(NotaCreditoElectronica));
                        NotaCreditoElectronica notaCreditoElectronica;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            notaCreditoElectronica = (NotaCreditoElectronica)serializer.Deserialize(memStream);
                        if (notaCreditoElectronica.Receptor != null)
                        {
                            strNombreReceptor = notaCreditoElectronica.Receptor.Nombre;
                        }
                        decimal decTotalImpuesto = notaCreditoElectronica.ResumenFactura.TotalImpuestoSpecified ? notaCreditoElectronica.ResumenFactura.TotalImpuesto : 0;
                        string strMoneda = notaCreditoElectronica.ResumenFactura.CodigoMonedaSpecified ? notaCreditoElectronica.ResumenFactura.CodigoMoneda.ToString() : "CRC";
                        decimal decTotal = notaCreditoElectronica.ResumenFactura.TotalComprobante;
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

        public List<ReporteDocumentoElectronico> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteDocumentoElectronico> listaReporte = new List<ReporteDocumentoElectronico>();
                    var datosFacturasRecibidas = dbContext.DocumentoElectronicoRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 5 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado);
                    foreach (var documento in datosFacturasRecibidas)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(MensajeReceptor));
                        MensajeReceptor mensajeReceptor;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            mensajeReceptor = (MensajeReceptor)serializer.Deserialize(memStream);
                        string strNombreEmisor = mensajeReceptor.NumeroCedulaEmisor;
                        decimal decTotalImpuesto = mensajeReceptor.MontoTotalImpuestoSpecified ? mensajeReceptor.MontoTotalImpuesto : 0;
                        string strMoneda = "CRC";
                        decimal decTotal = mensajeReceptor.TotalFactura;
                        ReporteDocumentoElectronico reporteLinea = new ReporteDocumentoElectronico();
                        reporteLinea.ClaveNumerica = mensajeReceptor.Clave;
                        reporteLinea.Fecha = documento.Fecha.ToString("dd/MM/yyyy");
                        reporteLinea.Nombre = strNombreEmisor;
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

        public List<ReporteEstadoResultados> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                    DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                    List<ReporteEstadoResultados> listaReporte = new List<ReporteEstadoResultados>();
                    var grupoFacturasEmitidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 1 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    decimal decTotalVentasExcentas = 0;
                    decimal decTotalVentasGrabadas = 0;
                    decimal decTotalImpuestoVentas = 0;
                    foreach (var documento in grupoFacturasEmitidas)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(FacturaElectronica));
                        FacturaElectronica facturaElectronica;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            facturaElectronica = (FacturaElectronica)serializer.Deserialize(memStream);
                        decimal decTotalPorLineaExcento = facturaElectronica.ResumenFactura.TotalExento;
                        decimal decTotalPorLineaGrabado = facturaElectronica.ResumenFactura.TotalGravado;
                        decimal decImpuestoPorLinea = facturaElectronica.ResumenFactura.TotalImpuestoSpecified ? facturaElectronica.ResumenFactura.TotalImpuesto : 0;
                        decTotalVentasExcentas += decTotalPorLineaExcento;
                        decTotalVentasGrabadas += decTotalPorLineaGrabado;
                        decTotalImpuestoVentas += decImpuestoPorLinea;
                    }

                    var grupoNotasCreditoEmitidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 3 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    foreach (var documento in grupoNotasCreditoEmitidas)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(NotaCreditoElectronica));
                        NotaCreditoElectronica notaCreditoElectronica;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            notaCreditoElectronica = (NotaCreditoElectronica)serializer.Deserialize(memStream);
                        decimal decTotalPorLineaExcento = notaCreditoElectronica.ResumenFactura.TotalExento;
                        decimal decTotalPorLineaGrabado = notaCreditoElectronica.ResumenFactura.TotalGravado;
                        decimal decImpuestoPorLinea = notaCreditoElectronica.ResumenFactura.TotalImpuestoSpecified ? notaCreditoElectronica.ResumenFactura.TotalImpuesto : 0;
                        decTotalVentasExcentas -= decTotalPorLineaExcento;
                        decTotalVentasGrabadas -= decTotalPorLineaGrabado;
                        decTotalImpuestoVentas -= decImpuestoPorLinea;
                    }
                    ReporteEstadoResultados reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Ventas excentas por facturas electrónicas emitidas";
                    reporteLinea.Descripcion = "Ventas excentas por facturas electrónicas emitidas";
                    reporteLinea.Valor = decTotalVentasExcentas;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Ventas grabadas por facturas electrónicas emitidas";
                    reporteLinea.Descripcion = "Ventas grabadas por facturas electrónicas emitidas";
                    reporteLinea.Valor = decTotalVentasGrabadas;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Impuesto sobre facturas electrónicas emitidas";
                    reporteLinea.Descripcion = "Impuesto sobre facturas electrónicas emitidas";
                    reporteLinea.Valor = decTotalImpuestoVentas;
                    listaReporte.Add(reporteLinea);

                    var grupoFacturasRecibidas = dbContext.DocumentoElectronicoRepository
                        .Where(a => a.IdEmpresa == intIdEmpresa & a.Fecha >= datFechaInicial & a.Fecha <= datFechaFinal & a.IdTipoDocumento == 5 & a.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado).ToList();
                    decimal decTotalComprasExcentas = 0;
                    decimal decTotalComprasGravadas = 0;
                    decimal decTotalImpuestoCompras = 0;
                    foreach (var documento in grupoFacturasRecibidas)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(MensajeReceptor));
                        MensajeReceptor mensajeReceptor;
                        using (MemoryStream memStream = new MemoryStream(documento.DatosDocumento))
                            mensajeReceptor = (MensajeReceptor)serializer.Deserialize(memStream);
                        decimal decTotalPorLinea = mensajeReceptor.TotalFactura;
                        decimal decImpuestoPorLinea = mensajeReceptor.MontoTotalImpuesto;
                        if (decImpuestoPorLinea > 0)
                        {
                            decTotalComprasGravadas += (decTotalPorLinea - decImpuestoPorLinea);
                            decTotalImpuestoCompras += decImpuestoPorLinea;
                        }
                        else
                        {
                            decTotalComprasExcentas += decTotalPorLinea;
                        }
                    }
                    reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Compras exentas por documentos electrónicos aceptados";
                    reporteLinea.Descripcion = "Compras exentas por documentos electrónicos aceptados";
                    reporteLinea.Valor = decTotalComprasExcentas;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Compras gravadas por documentos electrónicos aceptados";
                    reporteLinea.Descripcion = "Compras gravadas por documentos electrónicos aceptados";
                    reporteLinea.Valor = decTotalComprasGravadas;
                    listaReporte.Add(reporteLinea);
                    reporteLinea = new ReporteEstadoResultados();
                    reporteLinea.NombreTipoRegistro = "Impuesto sobre documentos electrónicos aceptados";
                    reporteLinea.Descripcion = "Impuesto sobre documentos electrónicos aceptados";
                    reporteLinea.Valor = decTotalImpuestoCompras;
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
    }
}