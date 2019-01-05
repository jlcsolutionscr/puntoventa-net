using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using LeandroSoftware.Core;
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using log4net;
using Unity;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IReporteService
    {
        IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoFactura();
        IList<CondicionVentaYFormaPago> ObtenerListaCondicionVentaYFormaPagoCompra();
        DataTable ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago, int intIdBancoAdquiriente);
        DataTable ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor);
        DataTable ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago);
        DataTable ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente);
        DataTable ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor);
        DataTable ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente);
        DataTable ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor);
        DataTable ObtenerReporteMovimientosBanco(int intIdCuenta, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteDetalleEgreso(int intIdEmpresa, int idCuentaEgreso, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteDetalleIngreso(int intIdEmpresa, int idCuentaIngreso, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteCierreDeCaja(int intIdCierre);
        DataTable ObtenerReporteProforma(int intIdProforma);
        DataTable ObtenerReporteOrdenServicio(int intIdOrdenServicio);
        DataTable ObtenerReporteOrdenCompra(int intIdOrdenCompra);
        DataTable ObtenerReporteInventario(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion);
        DataTable ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0);
        DataTable ObtenerReportePerdidasyGanancias(int intIdEmpresa);
        DataTable ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal);
        DataTable ObtenerReporteEgreso(int intIdEgreso);
        DataTable ObtenerReporteIngreso(int intIdIngreso);
    }

    public class ReporteService : IReporteService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        public DataTable ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intTipoPago, int intIdBancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("Ventas");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdFactura", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("NoDocumento", typeof(string));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Total", typeof(decimal));

                    if (intTipoPago == -1)
                    {
                        var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                            .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.NoDocumento, z.x.PorcentajeIVA, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
                        foreach (var value in detalleVentas)
                        {
                            drNewRow = dtDataTable.NewRow();
                            drNewRow["FechaDesde"] = datFechaInicial;
                            drNewRow["FechaHasta"] = datFechaFinal;
                            drNewRow["IdFactura"] = value.IdFactura;
                            drNewRow["Fecha"] = value.Fecha;
                            drNewRow["Nombre"] = value.NombreCliente;
                            drNewRow["NoDocumento"] = value.NoDocumento;
                            drNewRow["Impuesto"] = value.Impuesto;
                            drNewRow["Total"] = value.Total;
                            dtDataTable.Rows.Add(drNewRow);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito | !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdCliente, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdFactura, z.x.Fecha, NombreCliente = z.x.Cliente.Nombre, z.x.NoDocumento, z.x.PorcentajeIVA, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleVentas = detalleVentas.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleVentas = detalleVentas.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdCliente > 0)
                                detalleVentas = detalleVentas.Where(x => x.IdCliente == intIdCliente);
                            foreach (var value in detalleVentas)
                            {
                                drNewRow = dtDataTable.NewRow();
                                drNewRow["FechaDesde"] = datFechaInicial;
                                drNewRow["FechaHasta"] = datFechaFinal;
                                drNewRow["IdFactura"] = value.IdFactura;
                                drNewRow["Fecha"] = value.Fecha;
                                drNewRow["Nombre"] = value.NombreCliente;
                                drNewRow["NoDocumento"] = value.NoDocumento;
                                drNewRow["Impuesto"] = value.Impuesto;
                                drNewRow["Total"] = value.Total;
                                dtDataTable.Rows.Add(drNewRow);
                            }
                        }
                        else
                        {
                            var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ClienteRepository, x => x.IdCliente, y => y.IdCliente, (x, y) => new { x, y })
                                .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                                .Select(z => new { z.x.x.IdCliente, z.x.x.Nulo, z.x.x.IdCondicionVenta, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.NoDocumento, z.x.x.PorcentajeIVA, z.x.x.Impuesto, Total = z.y.MontoLocal, z.x.x.TotalCosto });
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
                                drNewRow = dtDataTable.NewRow();
                                drNewRow["FechaDesde"] = datFechaInicial;
                                drNewRow["FechaHasta"] = datFechaFinal;
                                drNewRow["IdFactura"] = value.IdFactura;
                                drNewRow["Fecha"] = value.Fecha;
                                drNewRow["Nombre"] = value.NombreCliente;
                                drNewRow["NoDocumento"] = value.NoDocumento;
                                drNewRow["Impuesto"] = value.Impuesto;
                                drNewRow["Total"] = value.Total;
                                dtDataTable.Rows.Add(drNewRow);
                            }
                        }
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ventas por Cliente: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("Ventas");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("NombreVendedor", typeof(string));
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdFactura", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("VentaPorMayor", typeof(string));
                    dtDataTable.Columns.Add("NombreCliente", typeof(string));
                    dtDataTable.Columns.Add("NoDocumento", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));

                    var detalleVentas = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == false)
                        .Join(dbContext.VendedorRepository, x => x.IdVendedor, y => y.IdVendedor, (x, y) => new { x, y })
                        .Join(dbContext.DesglosePagoFacturaRepository, x => x.x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Select(z => new { z.x.x.IdVendedor, z.x.y.Nombre, z.x.x.Nulo, z.y.IdFormaPago, z.y.IdCuentaBanco, z.x.x.IdFactura, z.x.x.Fecha, NombreCliente = z.x.x.Cliente.Nombre, z.x.x.NoDocumento, z.x.x.PorcentajeIVA, Total = z.y.MontoLocal, z.x.x.TotalCosto, z.x.x.Impuesto });
                    if (intIdVendedor > 0)
                        detalleVentas = detalleVentas.Where(x => x.IdVendedor == intIdVendedor);

                    foreach (var value in detalleVentas)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["NombreVendedor"] = value.Nombre;
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["IdFactura"] = value.IdFactura;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["NombreCliente"] = value.NombreCliente;
                        drNewRow["NoDocumento"] = value.NoDocumento;
                        drNewRow["Total"] = value.Total;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ventas por Vendedor: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intTipoPago)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("Compras");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdCompra", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("NoDocumento", typeof(string));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Total", typeof(decimal));

                    if (intTipoPago == -1)
                    {
                        var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                            .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                            .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.PorcentajeIVA, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
                        foreach (var value in detalleCompras)
                        {
                            drNewRow = dtDataTable.NewRow();
                            drNewRow["FechaDesde"] = datFechaInicial;
                            drNewRow["FechaHasta"] = datFechaFinal;
                            drNewRow["IdCompra"] = value.IdCompra;
                            drNewRow["Fecha"] = value.Fecha;
                            drNewRow["Nombre"] = value.NombreProveedor;
                            drNewRow["NoDocumento"] = value.NoDocumento;
                            drNewRow["Impuesto"] = value.Impuesto;
                            drNewRow["Total"] = value.Total;
                            dtDataTable.Rows.Add(drNewRow);
                        }
                    }
                    else
                    {
                        var pagosEfectivo = new[] { StaticReporteCondicionVentaFormaPago.ContadoEfectivo, StaticReporteCondicionVentaFormaPago.ContadoTarjeta, StaticReporteCondicionVentaFormaPago.ContadoCheque, StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario };
                        if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito | !pagosEfectivo.Contains(intTipoPago))
                        {
                            var detalleCompras = dbContext.CompraRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Nulo == bolNulo)
                                .Join(dbContext.ProveedorRepository, x => x.IdProveedor, y => y.IdProveedor, (x, y) => new { x, y })
                                .Select(z => new { z.x.IdProveedor, z.x.Nulo, z.x.IdCondicionVenta, z.x.IdCompra, z.x.Fecha, NombreProveedor = z.x.Proveedor.Nombre, z.x.NoDocumento, z.x.PorcentajeIVA, z.x.Impuesto, Total = (z.x.Excento + z.x.Grabado + z.x.Impuesto) });
                            if (intTipoPago == StaticReporteCondicionVentaFormaPago.Credito)
                                detalleCompras = detalleCompras.Where(x => x.IdCondicionVenta == StaticCondicionVenta.Credito);
                            else
                                detalleCompras = detalleCompras.Where(x => !pagosEfectivo.Contains(x.IdCondicionVenta));
                            if (intIdProveedor > 0)
                                detalleCompras = detalleCompras.Where(x => x.IdProveedor == intIdProveedor);
                            foreach (var value in detalleCompras)
                            {
                                drNewRow = dtDataTable.NewRow();
                                drNewRow["FechaDesde"] = datFechaInicial;
                                drNewRow["FechaHasta"] = datFechaFinal;
                                drNewRow["IdCompra"] = value.IdCompra;
                                drNewRow["Fecha"] = value.Fecha;
                                drNewRow["Nombre"] = value.NombreProveedor;
                                drNewRow["NoDocumento"] = value.NoDocumento;
                                drNewRow["Impuesto"] = value.Impuesto;
                                drNewRow["Total"] = value.Total;
                                dtDataTable.Rows.Add(drNewRow);
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
                                drNewRow = dtDataTable.NewRow();
                                drNewRow["FechaDesde"] = datFechaInicial;
                                drNewRow["FechaHasta"] = datFechaFinal;
                                drNewRow["IdCompra"] = value.IdCompra;
                                drNewRow["Fecha"] = value.Fecha;
                                drNewRow["Nombre"] = value.NombreProveedor;
                                drNewRow["NoDocumento"] = value.NoDocumento;
                                drNewRow["Impuesto"] = value.Impuesto;
                                drNewRow["Total"] = value.Total;
                                dtDataTable.Rows.Add(drNewRow);
                            }
                        }
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Compras por Proveedor: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("CuentasPorCobrar");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdPropietario", typeof(int));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("IdCxC", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Referencia", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Saldo", typeof(decimal));

                    var detalleCxCClientes = dbContext.CuentaPorCobrarRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Saldo > 0 & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, x => x.IdPropietario, y => y.IdCliente, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxC, z.x.Descripcion, z.x.Referencia, z.x.Fecha, z.x.Total, z.x.Saldo });

                    if (intIdCliente > 0)
                        detalleCxCClientes = detalleCxCClientes.Where(x => x.IdPropietario == intIdCliente);

                    foreach (var value in detalleCxCClientes)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdPropietario"] = value.IdPropietario;
                        drNewRow["Nombre"] = value.Nombre;
                        drNewRow["IdCxC"] = value.IdCxC;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Referencia"] = value.Referencia;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Saldo"] = value.Saldo;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Cuentas por Cobrar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("CuentasPorPagar");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdPropietario", typeof(int));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("IdCxP", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Saldo", typeof(decimal));

                    var detalleCxPProveedores = dbContext.CuentaPorPagarRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Saldo > 0 & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal & s.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, x => x.IdPropietario, y => y.IdProveedor, (x, y) => new { x, y })
                        .Select(z => new { z.x.IdPropietario, z.y.Nombre, z.x.IdCxP, z.x.Fecha, z.x.Descripcion, z.x.Referencia, z.x.Total, z.x.Saldo });

                    if (intIdProveedor > 0)
                        detalleCxPProveedores = detalleCxPProveedores.Where(x => x.IdPropietario == intIdProveedor);

                    foreach (var value in detalleCxPProveedores)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdPropietario"] = value.IdPropietario;
                        drNewRow["Nombre"] = value.Nombre;
                        drNewRow["IdCxP"] = value.IdCxP;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Descripcion"] = value.Descripcion + " Fact: " + value.Referencia;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Saldo"] = value.Saldo;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Cuentas por Pagar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("MovimientosCxC");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdPropietario", typeof(int));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("IdCxC", typeof(int));
                    dtDataTable.Columns.Add("DescCxC", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Saldo", typeof(decimal));
                    dtDataTable.Columns.Add("IdMovCxC", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Debito", typeof(decimal));
                    dtDataTable.Columns.Add("Credito", typeof(decimal));

                    var cxcClientes = dbContext.CuentaPorCobrarRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Nulo == false & a.Tipo == StaticTipoCuentaPorCobrar.Clientes)
                        .Join(dbContext.ClienteRepository, a => a.IdPropietario, b => b.IdCliente, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorCobrarRepository, a => a.a.IdCxC, d => d.IdCxC, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorCobrarRepository, a => a.c.IdMovCxC, d => d.IdMovCxC, (b, c) => new { b, c }).Where(s => !s.c.Nulo & s.c.Fecha >= datFechaInicial & s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.b.a.IdPropietario, DescCxC = d.b.b.a.Descripcion, d.b.b.b.Nombre, d.b.c.IdCxC, d.b.b.a.Total, d.b.b.a.Saldo, d.c.IdMovCxC, d.c.Fecha, d.c.Descripcion, d.c.Tipo, d.b.c.Monto });

                    if (intIdCliente > 0)
                        cxcClientes = cxcClientes.Where(a => a.IdPropietario == intIdCliente);

                    foreach (var value in cxcClientes)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdPropietario"] = value.IdPropietario;
                        drNewRow["Nombre"] = value.Nombre;
                        drNewRow["IdCxC"] = value.IdCxC;
                        drNewRow["DescCxC"] = value.DescCxC;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Saldo"] = value.Saldo;
                        drNewRow["IdMovCxC"] = value.IdMovCxC;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Descripcion"] = value.Descripcion;
                        if (value.Tipo == 3)
                            drNewRow["Credito"] = value.Monto;
                        else
                            drNewRow["Debito"] = value.Monto;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Movimientos de Cuentas por Cobrar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por cobrar. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("MovimientosCxP");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdProveedor", typeof(int));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("IdCxP", typeof(int));
                    dtDataTable.Columns.Add("DescCxP", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Saldo", typeof(decimal));
                    dtDataTable.Columns.Add("IdMovCxP", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Recibo", typeof(string));
                    dtDataTable.Columns.Add("Debito", typeof(decimal));
                    dtDataTable.Columns.Add("Credito", typeof(decimal));

                    var cxpProveedores = dbContext.CuentaPorPagarRepository.Where(a => a.IdEmpresa == intIdEmpresa & a.Nulo == false & a.Tipo == StaticTipoCuentaPorPagar.Proveedores)
                        .Join(dbContext.ProveedorRepository, a => a.IdPropietario, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.DesgloseMovimientoCuentaPorPagarRepository, a => a.a.IdCxP, d => d.IdCxP, (b, c) => new { b, c })
                        .Join(dbContext.MovimientoCuentaPorPagarRepository, a => a.c.IdMovCxP, d => d.IdMovCxP, (b, c) => new { b, c }).Where(s => !s.c.Nulo & s.c.Fecha >= datFechaInicial & s.c.Fecha <= datFechaFinal)
                        .Select(d => new { d.b.b.a.IdPropietario, DescCxP = d.b.b.a.Descripcion, d.b.b.b.Nombre, d.b.c.IdCxP, d.b.b.a.Total, d.b.b.a.Saldo, d.c.IdMovCxP, d.c.Fecha, d.c.Descripcion, d.c.Recibo, d.c.Tipo, d.b.c.Monto });

                    if (intIdProveedor > 0)
                        cxpProveedores = cxpProveedores.Where(a => a.IdPropietario == intIdProveedor);

                    foreach (var value in cxpProveedores)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdProveedor"] = value.IdPropietario;
                        drNewRow["Nombre"] = value.Nombre;
                        drNewRow["IdCxP"] = value.IdCxP;
                        drNewRow["DescCxP"] = value.DescCxP;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Saldo"] = value.Saldo;
                        drNewRow["IdMovCxP"] = value.IdMovCxP;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Recibo"] = value.Recibo;
                        if (value.Tipo == 3)
                            drNewRow["Credito"] = value.Monto;
                        else
                            drNewRow["Debito"] = value.Monto;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Movimientos de Cuentas por Pagar: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos de cuentas por pagar. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteMovimientosBanco(int intIdCuenta, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("MovimientoBanco");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdMov", typeof(int));
                    dtDataTable.Columns.Add("IdCuenta", typeof(int));
                    dtDataTable.Columns.Add("NombreCuenta", typeof(string));
                    dtDataTable.Columns.Add("SaldoAnterior", typeof(decimal));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Numero", typeof(string));
                    dtDataTable.Columns.Add("Beneficiario", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Tipo", typeof(string));
                    dtDataTable.Columns.Add("Debito", typeof(decimal));
                    dtDataTable.Columns.Add("Credito", typeof(decimal));
                    dtDataTable.Columns.Add("Saldo", typeof(decimal));

                    var movimientoBanco = dbContext.MovimientoBancoRepository.Where(s => s.IdCuenta == intIdCuenta & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaBancoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .Join(dbContext.TipoMovimientoBancoRepository, a => a.x.IdTipo, b => b.IdTipoMov, (a, b) => new { a, b })
                        .Select(z => new { z.a.x.IdMov, z.a.y.IdCuenta, NombreCuenta = z.a.y.Descripcion, z.a.x.Fecha, z.a.x.Numero, z.a.x.Beneficiario, z.a.x.Descripcion, z.b.DebeHaber, DescTipo = z.a.y.Descripcion, Total = z.a.x.Monto, z.a.x.SaldoAnterior })
                        .OrderBy(x => x.IdMov).ThenBy(x => x.Fecha);
                    foreach (var value in movimientoBanco)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["IdMov"] = value.IdMov;
                        drNewRow["IdCuenta"] = value.IdCuenta;
                        drNewRow["NombreCuenta"] = value.NombreCuenta;
                        drNewRow["SaldoAnterior"] = value.SaldoAnterior;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Numero"] = value.Numero;
                        drNewRow["Beneficiario"] = value.Beneficiario;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Tipo"] = value.DescTipo;
                        if (value.DebeHaber.Equals("D"))
                        {
                            drNewRow["Debito"] = value.Total;
                            drNewRow["Credito"] = 0;
                            drNewRow["Saldo"] = value.SaldoAnterior - value.Total;
                        }
                        else
                        {
                            drNewRow["Debito"] = 0;
                            drNewRow["Credito"] = value.Total;
                            drNewRow["Saldo"] = value.SaldoAnterior + value.Total;
                        }
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Movimientos Bancarios: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos bancarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("EstadoResultado");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("NombreTipoRegistro", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Valor", typeof(decimal));

                    var grupoCxC = dbContext.FacturaRepository.Join(dbContext.DesglosePagoFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa & s.x.Nulo == false & s.x.Fecha >= datFechaInicial & s.x.Fecha <= datFechaFinal)
                        .GroupBy(x => x.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });

                    foreach (var eachCxC in grupoCxC)
                    {
                        string strTipo = "";
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["NombreTipoRegistro"] = "Ingresos";
                        if (eachCxC.tipopago == StaticFormaPago.Efectivo)
                            strTipo = " de contado";
                        else if (eachCxC.tipopago == StaticFormaPago.Cheque)
                            strTipo = " con cheque";
                        else if (eachCxC.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                            strTipo = " con depósito bancario";
                        else if (eachCxC.tipopago == StaticFormaPago.Tarjeta)
                            strTipo = " con tarjeta";
                        else
                            strTipo = " otras formas de pago";
                        drNewRow["Descripcion"] = "Ventas" + strTipo;
                        drNewRow["Valor"] = eachCxC.total;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    if (grupoCxC.Count() == 0)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["NombreTipoRegistro"] = "Ingresos";
                        drNewRow["Descripcion"] = "No hay registros";
                        drNewRow["Valor"] = 0;
                        dtDataTable.Rows.Add(drNewRow);
                    }

                    var cxp = dbContext.CompraRepository.Join(dbContext.DesglosePagoCompraRepository, x => x.IdCompra, y => y.IdCompra, (x, y) => new { x, y })
                        .Where(s => s.x.IdEmpresa == intIdEmpresa & s.x.Nulo == false & s.x.Fecha >= datFechaInicial & s.x.Fecha <= datFechaFinal)
                        .GroupBy(g => g.y.IdFormaPago)
                        .Select(sf => new { tipopago = sf.Key, total = sf.Sum(a => a.y.MontoLocal) });
                    if (cxp.Count() > 0)
                    {
                        foreach (var eachCxP in cxp)
                        {
                            string strTipo = "";
                            drNewRow = dtDataTable.NewRow();
                            drNewRow["FechaDesde"] = datFechaInicial;
                            drNewRow["FechaHasta"] = datFechaFinal;
                            drNewRow["NombreTipoRegistro"] = "Egresos";
                            if (eachCxP.tipopago == StaticFormaPago.Efectivo)
                                strTipo = " de contado";
                            else if (eachCxP.tipopago == StaticFormaPago.Cheque)
                                strTipo = " con cheque";
                            else if (eachCxP.tipopago == StaticFormaPago.TransferenciaDepositoBancario)
                                strTipo = " con depósito bancario";
                            else if (eachCxP.tipopago == StaticFormaPago.Tarjeta)
                                strTipo = " con tarjeta";
                            else
                                strTipo = " con otras formas de pago";
                            drNewRow["Descripcion"] = "Compras" + strTipo;
                            drNewRow["Valor"] = eachCxP.total;
                            dtDataTable.Rows.Add(drNewRow);
                        }
                    }

                    var egreso = dbContext.EgresoRepository.Where(w => w.IdEmpresa == intIdEmpresa & w.Nulo == false & w.Fecha >= datFechaInicial & w.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, x => x.IdCuenta, y => y.IdCuenta, (x, y) => new { x, y })
                        .GroupBy(a => a.y.Descripcion)
                        .Select(a => new { Total = a.Sum(b => b.x.Monto), Desc = a.Key });

                    foreach (var value in egreso)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["NombreTipoRegistro"] = "Egresos";
                        drNewRow["Descripcion"] = value.Desc;
                        drNewRow["Valor"] = value.Total;
                        dtDataTable.Rows.Add(drNewRow);
                    }

                    if (cxp.Count() == 0 & egreso.Count() == 0)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["NombreTipoRegistro"] = "Egresos";
                        drNewRow["Descripcion"] = "No hay registros";
                        drNewRow["Valor"] = 0;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Estado de Resultados: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de estado de resultados. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("Egreso");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdMov", typeof(int));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Detalle", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));

                    var egreso = dbContext.EgresoRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaEgresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdEgreso, z.b.Descripcion, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in egreso)
                    {
                        if (intIdCuentaEgreso > 0 & value.IdCuenta != intIdCuentaEgreso)
                            continue;
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["IdMov"] = value.IdEgreso;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Detalle"] = value.Detalle;
                        drNewRow["Total"] = value.Total;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de Egresos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle de egresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("Egreso");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("IdMov", typeof(int));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Detalle", typeof(string));
                    dtDataTable.Columns.Add("Total", typeof(decimal));

                    var ingreso = dbContext.IngresoRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.CuentaIngresoRepository, a => a.IdCuenta, b => b.IdCuenta, (a, b) => new { a, b })
                        .Select(z => new { z.a.IdCuenta, z.a.IdIngreso, z.b.Descripcion, z.a.Detalle, Total = z.a.Monto });
                    foreach (var value in ingreso)
                    {
                        if (intIdCuentaIngreso > 0 & value.IdCuenta != intIdCuentaIngreso)
                            continue;
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["IdMov"] = value.IdIngreso;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Detalle"] = value.Detalle;
                        drNewRow["Total"] = value.Total;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de ingresos: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle de ingresos. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("CuentasPorCobrar");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("Codigo", typeof(string));
                    dtDataTable.Columns.Add("IdLinea", typeof(int));
                    dtDataTable.Columns.Add("NombreLinea", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(int));
                    dtDataTable.Columns.Add("PrecioVenta", typeof(decimal));
                    dtDataTable.Columns.Add("Excento", typeof(decimal));
                    dtDataTable.Columns.Add("Grabado", typeof(decimal));
                    dtDataTable.Columns.Add("Descuento", typeof(decimal));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Costo", typeof(decimal));

                    var ventasResumen = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.x.Excento, z.x.x.x.Grabado, z.x.x.x.Impuesto, z.x.x.x.Descuento, Costo = z.x.x.y.Cantidad * z.x.x.y.PrecioCosto });

                    foreach (var value in ventasResumen)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["Codigo"] = value.Codigo;
                        drNewRow["IdLinea"] = value.IdLinea;
                        drNewRow["NombreLinea"] = value.NombreLinea;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioVenta"] = value.PrecioVenta;
                        drNewRow["Excento"] = value.Excento;
                        drNewRow["Grabado"] = value.Grabado;
                        drNewRow["Descuento"] = value.Descuento;
                        drNewRow["Impuesto"] = value.Impuesto;
                        drNewRow["Costo"] = value.Costo;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Resumen de Ventas por Línea: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea resumido. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("CuentasPorCobrar");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FechaDesde", typeof(string));
                    dtDataTable.Columns.Add("FechaHasta", typeof(string));
                    dtDataTable.Columns.Add("Codigo", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("IdLinea", typeof(int));
                    dtDataTable.Columns.Add("NombreLinea", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(int));
                    dtDataTable.Columns.Add("PrecioVenta", typeof(decimal));
                    dtDataTable.Columns.Add("Excento", typeof(decimal));
                    dtDataTable.Columns.Add("Grabado", typeof(decimal));
                    dtDataTable.Columns.Add("Descuento", typeof(decimal));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));

                    var ventasDetalle = dbContext.FacturaRepository.Where(s => s.IdEmpresa == intIdEmpresa & s.Nulo == false & s.Fecha >= datFechaInicial & s.Fecha <= datFechaFinal)
                        .Join(dbContext.DetalleFacturaRepository, x => x.IdFactura, y => y.IdFactura, (x, y) => new { x, y })
                        .Join(dbContext.ProductoRepository, x => x.y.IdProducto, y => y.IdProducto, (x, y) => new { x, y })
                        .Join(dbContext.LineaRepository, x => x.y.IdLinea, y => y.IdLinea, (x, y) => new { x, y })
                        .Select(z => new { z.x.y.Codigo, z.x.y.Descripcion, z.x.y.IdLinea, NombreLinea = z.y.Descripcion, z.x.x.y.Cantidad, z.x.x.y.PrecioVenta, z.x.x.x.Excento, z.x.x.x.Grabado, z.x.x.x.Impuesto, z.x.x.x.Descuento });

                    if (intIdLinea > 0)
                        ventasDetalle = ventasDetalle.Where(a => a.IdLinea == intIdLinea);

                    foreach (var value in ventasDetalle)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FechaDesde"] = datFechaInicial;
                        drNewRow["FechaHasta"] = datFechaFinal;
                        drNewRow["Codigo"] = value.Codigo;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["IdLinea"] = value.IdLinea;
                        drNewRow["NombreLinea"] = value.NombreLinea;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioVenta"] = value.PrecioVenta;
                        drNewRow["Excento"] = value.Excento;
                        drNewRow["Grabado"] = value.Grabado;
                        drNewRow["Descuento"] = value.Descuento;
                        drNewRow["Impuesto"] = value.Impuesto;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Detalle de Ventas por Línea: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de ventas por línea detallado. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteCierreDeCaja(int intIdCierre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("CierreCaja");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("FondoInicio", typeof(decimal));
                    dtDataTable.Columns.Add("VentasContado", typeof(decimal));
                    dtDataTable.Columns.Add("VentasPorMayor", typeof(decimal));
                    dtDataTable.Columns.Add("VentasDetalle", typeof(decimal));
                    dtDataTable.Columns.Add("VentasCredito", typeof(decimal));
                    dtDataTable.Columns.Add("VentasTarjeta", typeof(decimal));
                    dtDataTable.Columns.Add("OtrasVentas", typeof(decimal));
                    dtDataTable.Columns.Add("RetencionIVA", typeof(decimal));
                    dtDataTable.Columns.Add("ComisionVT", typeof(decimal));
                    dtDataTable.Columns.Add("Liquidacion", typeof(decimal));
                    dtDataTable.Columns.Add("IngresoCxCEfectivo", typeof(decimal));
                    dtDataTable.Columns.Add("IngresoCxCTarjeta", typeof(decimal));
                    dtDataTable.Columns.Add("DevolucionesProveedores", typeof(decimal));
                    dtDataTable.Columns.Add("OtrosIngresos", typeof(decimal));
                    dtDataTable.Columns.Add("ComprasContado", typeof(decimal));
                    dtDataTable.Columns.Add("ComprasCredito", typeof(decimal));
                    dtDataTable.Columns.Add("OtrasCompras", typeof(decimal));
                    dtDataTable.Columns.Add("EgresoCxPEfectivo", typeof(decimal));
                    dtDataTable.Columns.Add("DevolucionesClientes", typeof(decimal));
                    dtDataTable.Columns.Add("OtrosEgresos", typeof(decimal));

                    var datosCierre = dbContext.CierreCajaRepository.Where(a => a.IdCierre == intIdCierre)
                        .Select(d => new { d.FondoInicio, d.VentasPorMayor, d.VentasDetalle, d.VentasContado, d.VentasCredito, d.VentasTarjeta, d.OtrasVentas, d.RetencionIVA, d.ComisionVT, d.LiquidacionTarjeta, d.IngresoCxCEfectivo, d.IngresoCxCTarjeta, d.DevolucionesProveedores, d.OtrosIngresos, d.ComprasContado, d.ComprasCredito, d.OtrasCompras, d.EgresoCxPEfectivo, d.DevolucionesClientes, d.OtrosEgresos });

                    foreach (var value in datosCierre)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["FondoInicio"] = value.FondoInicio;
                        drNewRow["VentasPorMayor"] = value.VentasPorMayor;
                        drNewRow["VentasDetalle"] = value.VentasDetalle;
                        drNewRow["VentasContado"] = value.VentasContado;
                        drNewRow["VentasCredito"] = value.VentasCredito;
                        drNewRow["VentasTarjeta"] = value.VentasTarjeta;
                        drNewRow["OtrasVentas"] = value.OtrasVentas;
                        drNewRow["RetencionIVA"] = value.RetencionIVA;
                        drNewRow["ComisionVT"] = value.ComisionVT;
                        drNewRow["Liquidacion"] = value.LiquidacionTarjeta;
                        drNewRow["IngresoCxCEfectivo"] = value.IngresoCxCEfectivo;
                        drNewRow["IngresoCxCTarjeta"] = value.IngresoCxCTarjeta;
                        drNewRow["DevolucionesProveedores"] = value.DevolucionesProveedores;
                        drNewRow["OtrosIngresos"] = value.OtrosIngresos;
                        drNewRow["ComprasContado"] = value.ComprasContado;
                        drNewRow["ComprasCredito"] = value.ComprasCredito;
                        drNewRow["OtrasCompras"] = value.OtrasCompras;
                        drNewRow["EgresoCxPEfectivo"] = value.EgresoCxPEfectivo;
                        drNewRow["DevolucionesClientes"] = value.DevolucionesClientes;
                        drNewRow["OtrosEgresos"] = value.OtrosEgresos;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de cierre de Caja: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de cierre de caja. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteProforma(int intIdProforma)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Proforma");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdProforma", typeof(int));
                    dtDataTable.Columns.Add("NombreCliente", typeof(string));
                    dtDataTable.Columns.Add("SubTotal", typeof(decimal));
                    dtDataTable.Columns.Add("Descuento", typeof(decimal));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("TipoPago", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(decimal));
                    dtDataTable.Columns.Add("PrecioVenta", typeof(decimal));
                    dtDataTable.Columns.Add("TotalLinea", typeof(decimal));

                    var datosCierre = dbContext.ProformaRepository.Where(a => a.IdProforma == intIdProforma)
                        .Join(dbContext.DetalleProformaRepository, b => b.IdProforma, b => b.IdProforma, (a, b) => new { a, b })
                        .Select(c => new { c.a.IdProforma, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Grabado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Grabado + c.a.Impuesto, c.a.Fecha, c.a.TipoPago, Descripcion = c.b.Producto.Codigo + " " + c.b.Producto.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });

                    foreach (var value in datosCierre)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdProforma"] = value.IdProforma;
                        drNewRow["NombreCliente"] = value.NombreCliente;
                        drNewRow["SubTotal"] = value.SubTotal;
                        drNewRow["Descuento"] = value.Descuento;
                        drNewRow["Impuesto"] = value.Impuesto;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Fecha"] = value.Fecha;
                        if (value.TipoPago == 1)
                            drNewRow["TipoPago"] = "Contado";
                        else
                            drNewRow["TipoPago"] = "Crédito";
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioVenta"] = value.PrecioVenta;
                        drNewRow["TotalLinea"] = value.TotalLinea;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Proforma: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteOrdenServicio(int intIdOrdenServicio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("OrdenServicio");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdOrden", typeof(int));
                    dtDataTable.Columns.Add("NombreCliente", typeof(string));
                    dtDataTable.Columns.Add("SubTotal", typeof(decimal));
                    dtDataTable.Columns.Add("Descuento", typeof(decimal));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Placa", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(decimal));
                    dtDataTable.Columns.Add("PrecioVenta", typeof(decimal));
                    dtDataTable.Columns.Add("TotalLinea", typeof(decimal));

                    var datosCierre = dbContext.OrdenServicioRepository.Where(a => a.IdOrden == intIdOrdenServicio)
                        .Join(dbContext.DetalleOrdenServicioRepository, b => b.IdOrden, b => b.IdOrden, (a, b) => new { a, b })
                        .Select(c => new { c.a.IdOrden, NombreCliente = c.a.Cliente.Nombre, SubTotal = c.a.Excento + c.a.Grabado + c.a.Impuesto + c.a.Descuento, c.a.Descuento, c.a.Impuesto, Total = c.a.Excento + c.a.Grabado + c.a.Impuesto, c.a.Fecha, c.a.Placa, Descripcion = c.b.Producto.Codigo + " " + c.b.Descripcion, c.b.Cantidad, c.b.PrecioVenta, TotalLinea = c.b.Cantidad * c.b.PrecioVenta });

                    foreach (var value in datosCierre)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdOrden"] = value.IdOrden;
                        drNewRow["NombreCliente"] = value.NombreCliente;
                        drNewRow["SubTotal"] = value.SubTotal;
                        drNewRow["Descuento"] = value.Descuento;
                        drNewRow["Impuesto"] = value.Impuesto;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Placa"] = value.Placa;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioVenta"] = value.PrecioVenta;
                        drNewRow["TotalLinea"] = value.TotalLinea;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ordenes de Servicio: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ordenes de Servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteOrdenCompra(int intIdOrdenCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("OrdenCompra");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdOrden", typeof(int));
                    dtDataTable.Columns.Add("Nombre", typeof(string));
                    dtDataTable.Columns.Add("SubTotal", typeof(decimal));
                    dtDataTable.Columns.Add("Descuento", typeof(decimal));
                    dtDataTable.Columns.Add("Impuesto", typeof(decimal));
                    dtDataTable.Columns.Add("Total", typeof(decimal));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("TipoPago", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(decimal));
                    dtDataTable.Columns.Add("PrecioCosto", typeof(decimal));
                    dtDataTable.Columns.Add("TotalLinea", typeof(decimal));

                    var datosCierre = dbContext.OrdenRepository.Where(a => a.IdOrdenCompra == intIdOrdenCompra)
                        .Join(dbContext.ProveedorRepository, b => b.IdProveedor, b => b.IdProveedor, (a, b) => new { a, b })
                        .Join(dbContext.DetalleOrdenCompraRepository, c => c.a.IdOrdenCompra, d => d.IdOrdenCompra, (c, d) => new { c, d })
                        .Select(e => new { e.c.a.IdOrdenCompra, e.c.b.Nombre, SubTotal = e.c.a.Excento + e.c.a.Grabado + e.c.a.Impuesto + e.c.a.Descuento, e.c.a.Descuento, e.c.a.Impuesto, Total = e.c.a.Excento + e.c.a.Grabado + e.c.a.Impuesto, e.c.a.Fecha, e.c.a.TipoPago, Descripcion = e.d.Producto.Codigo + " " + e.d.Producto.Descripcion, e.d.Cantidad, e.d.PrecioCosto, TotalLinea = e.d.Cantidad * e.d.PrecioCosto });

                    foreach (var value in datosCierre)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdOrden"] = value.IdOrdenCompra;
                        drNewRow["Nombre"] = value.Nombre;
                        drNewRow["SubTotal"] = value.SubTotal;
                        drNewRow["Descuento"] = value.Descuento;
                        drNewRow["Impuesto"] = value.Impuesto;
                        drNewRow["Total"] = value.Total;
                        drNewRow["Fecha"] = value.Fecha;
                        if (value.TipoPago == 1)
                            drNewRow["TipoPago"] = "Contado";
                        else
                            drNewRow["TipoPago"] = "Crédito";
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioCosto"] = value.PrecioCosto;
                        drNewRow["TotalLinea"] = value.TotalLinea;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ordenes de Compra: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ordenes de Compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteInventario(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Proforma");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdProducto", typeof(int));
                    dtDataTable.Columns.Add("Codigo", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("Cantidad", typeof(decimal));
                    dtDataTable.Columns.Add("PrecioCosto", typeof(decimal));
                    dtDataTable.Columns.Add("PrecioVenta", typeof(string));

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
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdProducto"] = value.IdProducto;
                        drNewRow["Codigo"] = value.Codigo;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["Cantidad"] = value.Cantidad;
                        drNewRow["PrecioCosto"] = value.PrecioCosto;
                        drNewRow["PrecioVenta"] = value.PrecioVenta1;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Inventario: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteMovimientosContables(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("MovimientosContables");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("SaldoDebe", typeof(decimal));
                    dtDataTable.Columns.Add("SaldoHaber", typeof(decimal));

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
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["SaldoDebe"] = value.TotalDebito;
                        drNewRow["SaldoHaber"] = value.TotalCredito;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de movimientos contables: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de movimientos contables. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteBalanceComprobacion(int intIdEmpresa, int intMes = 0, int intAnnio = 0)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Balance");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdCuenta", typeof(int));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("SaldoDebe", typeof(decimal));
                    dtDataTable.Columns.Add("SaldoHaber", typeof(decimal));

                    DateTime datFechaActual = DateTime.Now;
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdCuenta"] = value.IdCuenta;
                        drNewRow["Descripcion"] = value.Descripcion;
                        if (intMes > 0 & intAnnio > 0)
                            decSaldo = dbContext.SaldoMensualContableRepository.Where(x => x.Mes == intMes & x.Annio == intAnnio & x.IdCuenta == value.IdCuenta).Select(a => a.SaldoFinMes).FirstOrDefault();
                        else
                            decSaldo = value.SaldoActual;
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            drNewRow["SaldoDebe"] = decSaldo;
                            drNewRow["SaldoHaber"] = 0;
                        }
                        else
                        {
                            drNewRow["SaldoDebe"] = 0;
                            drNewRow["SaldoHaber"] = decSaldo;
                        }
                        if (decSaldo != 0)
                            dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReportePerdidasyGanancias(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Balance");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("IdTipoCuenta", typeof(int));
                    dtDataTable.Columns.Add("DescGrupo", typeof(string));
                    dtDataTable.Columns.Add("SaldoDebe", typeof(decimal));
                    dtDataTable.Columns.Add("SaldoHaber", typeof(decimal));

                    DateTime datFechaActual = DateTime.Now;
                    var listaCuentas = dbContext.CatalogoContableRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.EsCuentaBalance == true & x.SaldoActual != 0 & x.IdClaseCuenta == StaticClaseCuentaContable.Resultado)
                        .OrderBy(x => x.Nivel_1).ThenBy(x => x.Nivel_2).ThenBy(x => x.Nivel_3).ThenBy(x => x.Nivel_4).ThenBy(x => x.Nivel_5).ThenBy(x => x.Nivel_6).ThenBy(x => x.Nivel_7).ToList();

                    foreach (CatalogoContable value in listaCuentas)
                    {
                        decimal decSaldo = 0;
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["IdTipoCuenta"] = value.IdTipoCuenta;
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                            drNewRow["DescGrupo"] = "Cuentas de Egresos";
                        else
                            drNewRow["DescGrupo"] = "Cuentas de Ingresos";
                        decSaldo = value.SaldoActual;
                        if (value.TipoCuentaContable.TipoSaldo == StaticTipoDebitoCredito.Debito)
                        {
                            drNewRow["SaldoDebe"] = decSaldo;
                            drNewRow["SaldoHaber"] = 0;
                        }
                        else
                        {
                            drNewRow["SaldoDebe"] = 0;
                            drNewRow["SaldoHaber"] = decSaldo;
                        }
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteDetalleMovimientosCuentasDeBalance(int intIdEmpresa, int intIdCuentaGrupo, string strFechaInicial, string strFechaFinal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DateTime datFechaInicial = DateTime.Parse(strFechaInicial);
                    DateTime datFechaFinal = DateTime.Parse(strFechaFinal + " 23:59:59");
                    DataTable dtDataTable = new DataTable("DetalleBalance");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("DescCuentaBalance", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("SaldoInicial", typeof(decimal));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Detalle", typeof(string));
                    dtDataTable.Columns.Add("Debito", typeof(decimal));
                    dtDataTable.Columns.Add("Credito", typeof(decimal));

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
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["DescCuentaBalance"] = cuentaDeBalance.Descripcion;
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["SaldoInicial"] = value.SaldoAnterior;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Detalle"] = value.Detalle;
                        drNewRow["Debito"] = value.Debito;
                        drNewRow["Credito"] = value.Credito;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de detalle del balance de comprobación: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de detalle del balance de comprobación. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteEgreso(int intIdEgreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Egreso");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdEgreso", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("Detalle", typeof(string));
                    dtDataTable.Columns.Add("Beneficiario", typeof(string));
                    dtDataTable.Columns.Add("Monto", typeof(decimal));
                    dtDataTable.Columns.Add("MontoEnLetras", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("MontoLocal", typeof(decimal));

                    var datosEgreso = dbContext.EgresoRepository.Where(a => a.IdEgreso == intIdEgreso)
                        .Join(dbContext.DesglosePagoEgresoRepository, b => b.IdEgreso, b => b.IdEgreso, (a, b) => new { a, b })
                        .Join(dbContext.FormaPagoRepository, c => c.b.IdFormaPago, d => d.IdFormaPago, (c, d) => new { c, d })
                        .Select(c => new { c.c.a.IdEgreso, c.c.a.Fecha, c.c.a.Detalle, c.c.a.Beneficiario, c.c.a.Monto, c.d.Descripcion, c.c.b.MontoLocal });

                    foreach (var value in datosEgreso)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdEgreso"] = value.IdEgreso;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["Detalle"] = value.Detalle;
                        drNewRow["Beneficiario"] = value.Beneficiario;
                        drNewRow["Monto"] = value.Monto;
                        drNewRow["MontoEnLetras"] = Utilitario.NumeroALetras((double)value.Monto);
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["MontoLocal"] = value.MontoLocal;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Egreso: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Egreso. Por favor consulte con su proveedor.");
                }
            }
        }

        public DataTable ObtenerReporteIngreso(int intIdIngreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DataTable dtDataTable = new DataTable("Ingreso");
                    DataRow drNewRow;
                    dtDataTable.Columns.Add("IdIngreso", typeof(int));
                    dtDataTable.Columns.Add("Fecha", typeof(string));
                    dtDataTable.Columns.Add("RecibidoDe", typeof(string));
                    dtDataTable.Columns.Add("Detalle", typeof(string));
                    dtDataTable.Columns.Add("Monto", typeof(decimal));
                    dtDataTable.Columns.Add("MontoEnLetras", typeof(string));
                    dtDataTable.Columns.Add("Descripcion", typeof(string));
                    dtDataTable.Columns.Add("MontoLocal", typeof(decimal));

                    var datosIngreso = dbContext.IngresoRepository.Where(a => a.IdIngreso == intIdIngreso)
                        .Join(dbContext.DesglosePagoIngresoRepository, b => b.IdIngreso, b => b.IdIngreso, (a, b) => new { a, b })
                        .Join(dbContext.FormaPagoRepository, c => c.b.IdFormaPago, d => d.IdFormaPago, (c, d) => new { c, d })
                        .Select(c => new { c.c.a.IdIngreso, c.c.a.Fecha, c.c.a.RecibidoDe, c.c.a.Detalle, c.c.a.Monto, c.d.Descripcion, c.c.b.MontoLocal });

                    foreach (var value in datosIngreso)
                    {
                        drNewRow = dtDataTable.NewRow();
                        drNewRow["IdIngreso"] = value.IdIngreso;
                        drNewRow["Fecha"] = value.Fecha;
                        drNewRow["RecibidoDe"] = value.RecibidoDe;
                        drNewRow["Detalle"] = value.Detalle;
                        drNewRow["Monto"] = value.Monto;
                        drNewRow["MontoEnLetras"] = Utilitario.NumeroALetras((double)value.Monto);
                        drNewRow["Descripcion"] = value.Descripcion;
                        drNewRow["MontoLocal"] = value.MontoLocal;
                        dtDataTable.Rows.Add(drNewRow);
                    }
                    return dtDataTable;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el reporte de Ingreso: ", ex);
                    throw new Exception("Se produjo un error al ejecutar el reporte de Ingreso. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}