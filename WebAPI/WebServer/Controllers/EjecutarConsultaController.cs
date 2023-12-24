using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("puntoventa")]
    public class EjecutarConsultaController : ControllerBase
    {
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static ICompraService _servicioCompra;
        private static IFlujoCajaService _servicioFlujoCaja;
        private static IBancaService _servicioBanca;
        private static IReporteService _servicioReportes;
        private static ITrasladoService _servicioTraslado;
        private static ICuentaPorProcesarService _servicioCuentaPorProcesar;
        private static Empresa? empresa;
        private static CredencialesHacienda credenciales;
        private static BancoAdquiriente? bancoAdquiriente;
        private static Cliente? cliente;
        private static Linea? linea;
        private static Proveedor? proveedor;
        private static Producto? producto;
        private static Usuario? usuario;
        private static CuentaEgreso? cuentaEgreso;
        private static CuentaIngreso? cuentaIngreso;
        private static CuentaBanco? cuentaBanco;
        private static Vendedor? vendedor;
        private static Egreso? egreso;
        private static Ingreso? ingreso;
        private static Factura? factura;
        private static FacturaCompra? facturaCompra;
        private static DevolucionCliente? devolucionCliente;
        private static Compra? compra;
        private static Proforma? proforma;
        private static OrdenServicio? ordenServicio;
        private static Apartado? apartado;
        private static DocumentoElectronico? documento;
        private static Traslado? traslado;
        private static AjusteInventario? ajusteInventario;
        private static CuentaPorCobrar? cuentaPorCobrar;
        private static MovimientoCuentaPorCobrar? movimientoCxC;
        private static CuentaPorPagar? cuentaPorPagar;
        private static MovimientoCuentaPorPagar? movimientoCxP;
        private static MovimientoApartado? movimientoApartado;
        private static MovimientoOrdenServicio? movimientoOrdenServicio;
        private static MovimientoBanco? movimientoBanco;
        private static PuntoDeServicio? puntoDeServicio;
        private static int intIdEmpresa;
        private static int intIdSucursal;
        private static int intIdCuenta;
        private static int intIdProvincia;
        private static int intIdCanton;
        private static int intIdDistrito;
        private static int intIdCliente;
        private static int intIdProveedor;
        private static int intIdTipoPago;
        private static int intIdBanco;
        private static int intIdLlave1;
        private static int intNumeroPagina;
        private static int intFilasPorPagina;
        private static int intTotalLista;
        private static decimal decTipoCambioDolar = 0;
        private static string strLogoPath;
        private static string strClave;
        private static bool bolIncluyeServicios;
        private static bool bolFiltraActivos;
        private static bool bolFiltraExistencias;
        private static bool bolFiltraConDescuento;
        private static bool bolNulo;
        private static bool bolAplicado;
        private static bool bolIncluyeNulos;
        private static string strIdentificacion;
        private static string strCodigo;
        private static string strCodigoProveedor;
        private static string strDescripcion;
        private static string strNombre;
        private static string strBeneficiario;
        private static string strDetalle;
        private static string strFechaInicial;
        private static string strFechaFinal;
        private static byte[] bytLogo;

        private static string strConsultaInformacionContribuyenteURL;

        public EjecutarConsultaController(
            IConfiguration configuration,
            IHostEnvironment environment,
            IMantenimientoService servicioMantenimiento,
            IFacturacionService servicioFacturacion,
            ICompraService servicioCompra,
            IFlujoCajaService servicioFlujoCaja,
            IBancaService servicioBanca,
            IReporteService servicioReportes,
            ITrasladoService servicioTraslado,
            ICuentaPorProcesarService servicioCuentaPorProcesar
        )
        {
            _servicioMantenimiento = servicioMantenimiento;
            _servicioFacturacion = servicioFacturacion;
            _servicioCompra = servicioCompra;
            _servicioFlujoCaja = servicioFlujoCaja;
            _servicioBanca = servicioBanca;
            _servicioReportes = servicioReportes;
            _servicioTraslado = servicioTraslado;
            _servicioCuentaPorProcesar = servicioCuentaPorProcesar;
            strLogoPath = Path.Combine(environment.ContentRootPath, "images/Logo.png");
            strConsultaInformacionContribuyenteURL = configuration.GetSection("appSettings").GetSection("strConsultaContribuyenteURL").Value;
            if (decTipoCambioDolar == 0) decTipoCambioDolar = 1;
        }

        [HttpPost("ejecutarconsulta")]
        public string EjecutarConsulta([FromBody] string strDatos)
        {
            JObject datosJO = JObject.Parse(strDatos);
            JObject parametrosJO = null;
            string strNombreMetodo;
            string strEntidad = "";
            string strCodigoUsuario;
            if (datosJO.Property("NombreMetodo") != null)
                strNombreMetodo = datosJO.Property("NombreMetodo").Value.ToString();
            else
                throw new Exception("El mensaje no contiene la información suficiente para ser procesado.");
            if (datosJO.Property("Entidad") != null)
                strEntidad = datosJO.Property("Entidad").Value.ToString();
            else if (datosJO.Property("Parametros") != null)
                parametrosJO = JObject.Parse(datosJO.Property("Parametros").Value.ToString());
            string strRespuesta = "";
            switch (strNombreMetodo)
            {
                case "GuardarDatosCierreCaja":
                    CierreCaja cierre = JsonConvert.DeserializeObject<CierreCaja>(strEntidad);
                    string strIdCierre = _servicioFlujoCaja.GuardarDatosCierreCaja(cierre);
                    strRespuesta = JsonConvert.SerializeObject(strIdCierre);
                    break;
                case "ObtenerTipoCambioDolar":
                    strRespuesta = decTipoCambioDolar.ToString();
                    break;
                case "ObtenerListadoActividadEconomica":
                    strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                    IList<LlaveDescripcion> listadoActividades = _servicioMantenimiento.ObtenerListadoActividadEconomica(strConsultaInformacionContribuyenteURL, strIdentificacion);
                    if (listadoActividades.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoActividades);
                    break;
                case "ObtenerListadoRolesPorEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    IList<LlaveDescripcion> listadoRoles = _servicioMantenimiento.ObtenerListadoRolePorEmpresa(intIdEmpresa, false);
                    if (listadoRoles.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoRoles);
                    break;
                case "ObtenerListadoTipoMovimientoBanco":
                    IList<LlaveDescripcion> listadoTipoMovimiento = _servicioBanca.ObtenerListadoTipoMovimientoBanco();
                    if (listadoTipoMovimiento.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoMovimiento);
                    break;
                case "ObtenerListadoEmpresa":
                    IList<LlaveDescripcion> listadoEmpresa = _servicioMantenimiento.ObtenerListadoEmpresa();
                    if (listadoEmpresa.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoEmpresa);
                    break;
                case "ObtenerListadoCondicionVentaYFormaPagoFactura":
                    IList<LlaveDescripcion> listadoCondicionesyFormaPagoFactura = _servicioReportes.ObtenerListadoCondicionVentaYFormaPagoFactura();
                    if (listadoCondicionesyFormaPagoFactura.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCondicionesyFormaPagoFactura);
                    break;
                case "ObtenerListadoCondicionVentaYFormaPagoCompra":
                    IList<LlaveDescripcion> listadoCondicionesyFormaPagoCompra = _servicioReportes.ObtenerListadoCondicionVentaYFormaPagoCompra();
                    if (listadoCondicionesyFormaPagoCompra.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCondicionesyFormaPagoCompra);
                    break;
                case "ObtenerTotalListaClasificacionProducto":
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    intTotalLista = _servicioMantenimiento.ObtenerTotalListaClasificacionProducto(strDescripcion);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoClasificacionProducto":
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<ClasificacionProducto> listadoClasificacionProducto = _servicioMantenimiento.ObtenerListadoClasificacionProducto(intNumeroPagina, intFilasPorPagina, strDescripcion);
                    if (listadoClasificacionProducto.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoClasificacionProducto);
                    break;
                case "ObtenerClasificacionProducto":
                    strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                    ClasificacionProducto clasificacionProducto = _servicioMantenimiento.ObtenerClasificacionProducto(strCodigo);
                    if (clasificacionProducto != null)
                        strRespuesta = JsonConvert.SerializeObject(clasificacionProducto);
                    break;
                case "ObtenerReporteProformas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    IList<ReporteDetalle> listadoReporteProformas = _servicioReportes.ObtenerReporteProformas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                    if (listadoReporteProformas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteProformas);
                    break;
                case "ObtenerReporteApartados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    IList<ReporteDetalle> listadoReporteApartados = _servicioReportes.ObtenerReporteApartados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                    if (listadoReporteApartados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteApartados);
                    break;
                case "ObtenerReporteOrdenesServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    IList<ReporteDetalle> listadoReporteOrdenes = _servicioReportes.ObtenerReporteOrdenesServicio(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                    if (listadoReporteOrdenes.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteOrdenes);
                    break;
                case "ObtenerReporteVentasPorCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                    IList<ReporteDetalle> listadoReporteVentas = _servicioReportes.ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente, bolNulo, intIdTipoPago);
                    if (listadoReporteVentas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteVentas);
                    break;
                case "ObtenerReporteDevolucionesPorCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    IList<ReporteDetalle> listadoReporteDevolucionesClientes = _servicioReportes.ObtenerReporteDevolucionesPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente, bolNulo);
                    if (listadoReporteDevolucionesClientes.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteDevolucionesClientes);
                    break;
                case "ObtenerReporteVentasPorVendedor":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    int intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                    IList<ReporteVentasPorVendedor> listadoReporteVentasPorVendedor = _servicioReportes.ObtenerReporteVentasPorVendedor(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdVendedor);
                    if (listadoReporteVentasPorVendedor.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteVentasPorVendedor);
                    break;
                case "ObtenerReporteComprasPorProveedor":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                    bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                    intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                    IList<ReporteDetalle> listadoReporteCompras = _servicioReportes.ObtenerReporteComprasPorProveedor(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdProveedor, bolNulo, intIdTipoPago);
                    if (listadoReporteCompras.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteCompras);
                    break;
                case "ObtenerReporteCuentasPorCobrarClientes":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Activas").Value.ToString());
                    IList<ReporteCuentas> listadoReporteCuentasPorCobrar = _servicioReportes.ObtenerReporteCuentasPorCobrarClientes(intIdEmpresa, intIdSucursal, intIdCliente, bolFiltraActivos);
                    if (listadoReporteCuentasPorCobrar.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteCuentasPorCobrar);
                    break;
                case "ObtenerReporteCuentasPorPagarProveedores":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Activas").Value.ToString());
                    IList<ReporteCuentas> listadoReporteCuentasPorPagar = _servicioReportes.ObtenerReporteCuentasPorPagarProveedores(intIdEmpresa, intIdSucursal, intIdProveedor, bolFiltraActivos);
                    if (listadoReporteCuentasPorPagar.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteCuentasPorPagar);
                    break;
                case "ObtenerReporteMovimientosCxCClientes":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    IList<ReporteGrupoDetalle> listadoReporteMovimientosCxC = _servicioReportes.ObtenerReporteMovimientosCxCClientes(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente);
                    if (listadoReporteMovimientosCxC.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteMovimientosCxC);
                    break;
                case "ObtenerReporteMovimientosCxPProveedores":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                    IList<ReporteGrupoDetalle> listadoReporteMovimientosCxP = _servicioReportes.ObtenerReporteMovimientosCxPProveedores(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdProveedor);
                    if (listadoReporteMovimientosCxP.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteMovimientosCxP);
                    break;
                case "ObtenerReporteMovimientosBanco":
                    intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteMovimientosBanco> listadoReporteMovimientosBanco = _servicioReportes.ObtenerReporteMovimientosBanco(intIdCuenta, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoReporteMovimientosBanco.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteMovimientosBanco);
                    break;
                case "ObtenerReporteEstadoResultados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<DescripcionValor> listadoReporteEstadoResultados = _servicioReportes.ObtenerReporteEstadoResultados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoReporteEstadoResultados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteEstadoResultados);
                    break;
                case "ObtenerReporteDetalleEgreso":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteGrupoDetalle> listadoReporteDetalleEgreso = _servicioReportes.ObtenerReporteDetalleEgreso(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                    if (listadoReporteDetalleEgreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteDetalleEgreso);
                    break;
                case "ObtenerReporteDetalleIngreso":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                    IList<ReporteGrupoDetalle> listadoReporteDetalleIngreso = _servicioReportes.ObtenerReporteDetalleIngreso(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                    if (listadoReporteDetalleIngreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteDetalleIngreso);
                    break;
                case "ObtenerReporteVentasPorLineaResumen":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<DescripcionValor> listadoReporteVentasPorLineaResumen = _servicioReportes.ObtenerReporteVentasPorLineaResumen(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoReporteVentasPorLineaResumen.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteVentasPorLineaResumen);
                    break;
                case "ObtenerReporteVentasPorLineaDetalle":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteGrupoLineaDetalle> listadoReporteVentasPorLineaDetalle = _servicioReportes.ObtenerReporteVentasPorLineaDetalle(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                    if (listadoReporteVentasPorLineaDetalle.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteVentasPorLineaDetalle);
                    break;
                case "ObtenerReporteCierreDeCaja":
                    int intIdCierre = int.Parse(parametrosJO.Property("IdCierre").Value.ToString());
                    IList<DescripcionValor> listadoReporteCierreDeCaja = _servicioReportes.ObtenerReporteCierreDeCaja(intIdCierre);
                    if (listadoReporteCierreDeCaja.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteCierreDeCaja);
                    break;
                case "ObtenerReporteInventario":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString());
                    bolFiltraExistencias = bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString());
                    bolIncluyeServicios = parametrosJO.Property("IncluyeServicios") != null ? bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString()) : true;
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                    strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                    strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<ReporteInventario> listadoReporteInventario = _servicioReportes.ObtenerReporteInventario(intIdEmpresa, intIdSucursal, bolFiltraActivos, bolFiltraExistencias, bolIncluyeServicios, intIdLlave1, strCodigo, strDescripcion);
                    if (listadoReporteInventario.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteInventario);
                    break;
                case "ObtenerReporteMovimientosContables":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteMovimientosContables> listadoReporteMovimientosContables = _servicioReportes.ObtenerReporteMovimientosContables(intIdEmpresa, strFechaInicial, strFechaFinal);
                    if (listadoReporteMovimientosContables.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteMovimientosContables);
                    break;
                case "ObtenerReporteBalanceComprobacion":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    int intMes = int.Parse(parametrosJO.Property("Mes").Value.ToString());
                    int intAnnio = int.Parse(parametrosJO.Property("Annio").Value.ToString());
                    IList<ReporteBalanceComprobacion> listadoReporteBalanceComprobacion = _servicioReportes.ObtenerReporteBalanceComprobacion(intIdEmpresa, intMes, intAnnio);
                    if (listadoReporteBalanceComprobacion.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteBalanceComprobacion);
                    break;
                case "ObtenerReportePerdidasyGanancias":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    IList<ReportePerdidasyGanancias> listadoReportePerdidasyGanancias = _servicioReportes.ObtenerReportePerdidasyGanancias(intIdEmpresa, intIdSucursal);
                    if (listadoReportePerdidasyGanancias.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReportePerdidasyGanancias);
                    break;
                case "ObtenerReporteDetalleMovimientosCuentasDeBalance":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    int intIdCuentaGrupo = int.Parse(parametrosJO.Property("IdCuentaGrupo").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteDetalleMovimientosCuentasDeBalance> listadoReporteDetalleMovimientosCuentasDeBalance = _servicioReportes.ObtenerReporteDetalleMovimientosCuentasDeBalance(intIdEmpresa, intIdCuentaGrupo, strFechaInicial, strFechaFinal);
                    if (listadoReporteDetalleMovimientosCuentasDeBalance.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteDetalleMovimientosCuentasDeBalance);
                    break;
                case "ObtenerReporteEgreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    IList<ReporteEgreso> listadoReporteEgreso = _servicioReportes.ObtenerReporteEgreso(intIdLlave1);
                    if (listadoReporteEgreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteEgreso);
                    break;
                case "ObtenerReporteIngreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                    IList<ReporteIngreso> listadoReporteIngreso = _servicioReportes.ObtenerReporteIngreso(intIdLlave1);
                    if (listadoReporteIngreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteIngreso);
                    break;
                case "ObtenerReporteDocumentosElectronicosEmitidos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteDocumentoElectronico> listadoFacturasEmitidas = _servicioReportes.ObtenerReporteDocumentosElectronicosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoFacturasEmitidas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFacturasEmitidas);
                    break;
                case "ObtenerReporteDocumentosElectronicosRecibidos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteDocumentoElectronico> listadoFacturasRecibidas = _servicioReportes.ObtenerReporteDocumentosElectronicosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoFacturasRecibidas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFacturasRecibidas);
                    break;
                case "ObtenerReporteResumenDocumentosElectronicos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<ReporteResumenMovimiento> listadoReporteResumenDocumentosElectronicos = _servicioReportes.ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoReporteResumenDocumentosElectronicos.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteResumenDocumentosElectronicos);
                    break;
                case "ObtenerReporteComparativoVentasPorPeriodo":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<LlaveDescripcionValor> listadoReporteComparativoVentas = _servicioReportes.ObtenerReporteComparativoVentasPorPeriodo(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                    if (listadoReporteComparativoVentas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReporteComparativoVentas);
                    break;
                case "GenerarDatosCierreCaja":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    cierre = _servicioFlujoCaja.GenerarDatosCierreCaja(intIdEmpresa, intIdSucursal);
                    if (cierre != null)
                        strRespuesta = JsonConvert.SerializeObject(cierre);
                    break;
                case "ObtenerEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    empresa = _servicioMantenimiento.ObtenerEmpresa(intIdEmpresa);
                    if (empresa != null)
                        strRespuesta = JsonConvert.SerializeObject(empresa);
                    break;
                case "ObtenerCredencialesHacienda":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    credenciales = _servicioMantenimiento.ObtenerCredencialesHacienda(intIdEmpresa);
                    if (credenciales != null)
                        strRespuesta = JsonConvert.SerializeObject(credenciales);
                    break;
                case "ObtenerLogotipoEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    string logotipo = _servicioMantenimiento.ObtenerLogotipoEmpresa(intIdEmpresa);
                    if (logotipo != null)
                        strRespuesta = JsonConvert.SerializeObject(logotipo);
                    break;
                case "ObtenerListadoSucursales":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    IList<LlaveDescripcion> listadoSucursales = _servicioMantenimiento.ObtenerListadoSucursales(intIdEmpresa);
                    if (listadoSucursales.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoSucursales);
                    break;
                case "ObtenerSucursalPorEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    SucursalPorEmpresa sucursal = _servicioMantenimiento.ObtenerSucursalPorEmpresa(intIdEmpresa, intIdSucursal);
                    if (sucursal != null)
                        strRespuesta = JsonConvert.SerializeObject(sucursal);
                    break;
                case "ObtenerTerminalPorSucursal":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    int intIdTerminal = int.Parse(parametrosJO.Property("IdTerminal").Value.ToString());
                    TerminalPorSucursal terminal = _servicioMantenimiento.ObtenerTerminalPorSucursal(intIdEmpresa, intIdSucursal, intIdTerminal);
                    if (terminal != null)
                        strRespuesta = JsonConvert.SerializeObject(terminal);
                    break;
                case "ObtenerListadoCatalogoReportes":
                    IList<LlaveDescripcion> listadoReportes = _servicioMantenimiento.ObtenerListadoCatalogoReportes();
                    if (listadoReportes.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoReportes);
                    break;
                case "ObtenerListadoProvincias":
                    IList<LlaveDescripcion> listadoProvincias = _servicioMantenimiento.ObtenerListadoProvincias();
                    if (listadoProvincias.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProvincias);
                    break;
                case "ObtenerListadoCantones":
                    intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                    IList<LlaveDescripcion> listadoCantones = _servicioMantenimiento.ObtenerListadoCantones(intIdProvincia);
                    if (listadoCantones.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCantones);
                    break;
                case "ObtenerListadoDistritos":
                    intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                    intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                    IList<LlaveDescripcion> listadoDistritos = _servicioMantenimiento.ObtenerListadoDistritos(intIdProvincia, intIdCanton);
                    if (listadoDistritos.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoDistritos);
                    break;
                case "ObtenerListadoBarrios":
                    intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                    intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                    intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                    IList<LlaveDescripcion> listadoBarrios = _servicioMantenimiento.ObtenerListadoBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                    if (listadoBarrios.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoBarrios);
                    break;
                case "ObtenerParametroImpuesto":
                    int intIdImpuesto = int.Parse(parametrosJO.Property("IdImpuesto").Value.ToString());
                    LlaveDescripcionValor parametroImpuesto = _servicioMantenimiento.ObtenerParametroImpuesto(intIdImpuesto);
                    if (parametroImpuesto != null)
                        strRespuesta = JsonConvert.SerializeObject(parametroImpuesto);
                    break;
                case "ObtenerListadoBancoAdquiriente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoBancoAdquiriente = _servicioMantenimiento.ObtenerListadoBancoAdquiriente(intIdEmpresa, strDescripcion);
                    if (listadoBancoAdquiriente.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoBancoAdquiriente);
                    break;
                case "ObtenerBancoAdquiriente":
                    intIdBanco = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                    bancoAdquiriente = _servicioMantenimiento.ObtenerBancoAdquiriente(intIdBanco);
                    if (bancoAdquiriente != null)
                        strRespuesta = JsonConvert.SerializeObject(bancoAdquiriente);
                    break;
                case "ObtenerTotalListaClientes":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaClientes(intIdEmpresa, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoClientes":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoCliente = _servicioFacturacion.ObtenerListadoClientes(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                    if (listadoCliente.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCliente);
                    break;
                case "ObtenerCliente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    cliente = _servicioFacturacion.ObtenerCliente(intIdLlave1);
                    if (cliente != null)
                        strRespuesta = JsonConvert.SerializeObject(cliente);
                    break;
                case "ValidaIdentificacionCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                    cliente = _servicioFacturacion.ValidaIdentificacionCliente(intIdEmpresa, strIdentificacion);
                    if (cliente != null)
                        strRespuesta = JsonConvert.SerializeObject(cliente);
                    break;
                case "ObtenerListadoLineas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoLinea = _servicioMantenimiento.ObtenerListadoLineas(intIdEmpresa, strDescripcion);
                    if (listadoLinea.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoLinea);
                    break;
                case "ObtenerLinea":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                    linea = _servicioMantenimiento.ObtenerLinea(intIdLlave1);
                    if (linea != null)
                        strRespuesta = JsonConvert.SerializeObject(linea);
                    break;
                case "ObtenerTotalListaProveedores":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    intTotalLista = _servicioCompra.ObtenerTotalListaProveedores(intIdEmpresa, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoProveedores":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoProveedor = _servicioCompra.ObtenerListadoProveedores(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                    if (listadoProveedor.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProveedor);
                    break;
                case "ObtenerProveedor":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                    proveedor = _servicioCompra.ObtenerProveedor(intIdLlave1);
                    if (proveedor != null)
                        strRespuesta = JsonConvert.SerializeObject(proveedor);
                    break;
                case "ObtenerTotalListaProductos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                    bolFiltraActivos = parametrosJO.Property("FiltraActivos") != null ? bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString()) : false;
                    bolFiltraExistencias = parametrosJO.Property("FiltraExistencias") != null ? bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString()) : false;
                    bolFiltraConDescuento = parametrosJO.Property("FiltraConDescuento") != null ? bool.Parse(parametrosJO.Property("FiltraConDescuento").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                    strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                    strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    intTotalLista = _servicioMantenimiento.ObtenerTotalListaProductos(intIdEmpresa, intIdSucursal, bolIncluyeServicios, bolFiltraActivos, bolFiltraExistencias, bolFiltraConDescuento, intIdLlave1, strCodigo, strCodigoProveedor, strDescripcion);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoProductos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    bolIncluyeServicios = parametrosJO.Property("IncluyeServicios") != null ? bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString()) : true;
                    bolFiltraActivos = parametrosJO.Property("FiltraActivos") != null ? bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString()) : false;
                    bolFiltraExistencias = parametrosJO.Property("FiltraExistencias") != null ? bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString()) : false;
                    bolFiltraConDescuento = parametrosJO.Property("FiltraConDescuento") != null ? bool.Parse(parametrosJO.Property("FiltraConDescuento").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                    strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                    strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<ProductoDetalle> listadoProducto = (List<ProductoDetalle>)_servicioMantenimiento.ObtenerListadoProductos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, bolFiltraActivos, bolFiltraExistencias, bolFiltraConDescuento, intIdLlave1, strCodigo, strCodigoProveedor, strDescripcion);
                    if (listadoProducto.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProducto);
                    break;
                case "ObtenerProducto":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    producto = _servicioMantenimiento.ObtenerProducto(intIdLlave1, intIdSucursal);
                    if (producto != null)
                        strRespuesta = JsonConvert.SerializeObject(producto);
                    break;
                case "ObtenerProductoEspecial":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                    producto = _servicioMantenimiento.ObtenerProductoEspecial(intIdEmpresa, intIdLlave1);
                    if (producto != null)
                        strRespuesta = JsonConvert.SerializeObject(producto);
                    break;
                case "ObtenerProductoPorCodigo":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    producto = _servicioMantenimiento.ObtenerProductoPorCodigo(intIdEmpresa, strCodigo, intIdSucursal);
                    if (producto != null)
                        strRespuesta = JsonConvert.SerializeObject(producto);
                    break;
                case "ObtenerProductoPorCodigoProveedor":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    producto = _servicioMantenimiento.ObtenerProductoPorCodigoProveedor(intIdEmpresa, strCodigo, intIdSucursal);
                    if (producto != null)
                        strRespuesta = JsonConvert.SerializeObject(producto);
                    break;
                case "ObtenerTotalMovimientosPorProducto":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    intTotalLista = _servicioMantenimiento.ObtenerTotalMovimientosPorProducto(intIdLlave1, intIdSucursal, strFechaInicial, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerMovimientosPorProducto":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    IList<MovimientoProducto> listadoMovimientosProducto = _servicioMantenimiento.ObtenerMovimientosPorProducto(intIdLlave1, intIdSucursal, intNumeroPagina, intFilasPorPagina, strFechaInicial, strFechaFinal);
                    if (listadoMovimientosProducto != null)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientosProducto);
                    break;
                case "ObtenerListadoUsuarios":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoUsuario = _servicioMantenimiento.ObtenerListadoUsuarios(intIdEmpresa, strCodigo);
                    if (listadoUsuario.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoUsuario);
                    break;
                case "ObtenerUsuario":
                    int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    usuario = _servicioMantenimiento.ObtenerUsuario(intIdUsuario);
                    if (usuario != null)
                        strRespuesta = JsonConvert.SerializeObject(usuario);
                    break;
                case "ActualizarClaveUsuario":
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    usuario = _servicioMantenimiento.ActualizarClaveUsuario(intIdUsuario, strClave);
                    if (usuario != null)
                        strRespuesta = JsonConvert.SerializeObject(usuario);
                    break;
                case "ObtenerListadoCuentasEgreso":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoCuentaEgreso = _servicioFlujoCaja.ObtenerListadoCuentasEgreso(intIdEmpresa, strDescripcion);
                    if (listadoCuentaEgreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCuentaEgreso);
                    break;
                case "ObtenerCuentaEgreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                    cuentaEgreso = _servicioFlujoCaja.ObtenerCuentaEgreso(intIdLlave1);
                    if (cuentaEgreso != null)
                        strRespuesta = JsonConvert.SerializeObject(cuentaEgreso);
                    break;
                case "ObtenerListadoCuentasIngreso":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : ""; ;
                    IList<LlaveDescripcion> listadoCuentaIngreso = _servicioFlujoCaja.ObtenerListadoCuentasIngreso(intIdEmpresa, strDescripcion);
                    if (listadoCuentaIngreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCuentaIngreso);
                    break;
                case "ObtenerCuentaIngreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                    cuentaIngreso = _servicioFlujoCaja.ObtenerCuentaIngreso(intIdLlave1);
                    if (cuentaIngreso != null)
                        strRespuesta = JsonConvert.SerializeObject(cuentaIngreso);
                    break;
                case "ObtenerListadoCuentasBanco":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoCuentaBanco = _servicioBanca.ObtenerListadoCuentasBanco(intIdEmpresa, strDescripcion);
                    if (listadoCuentaBanco.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCuentaBanco);
                    break;
                case "ObtenerCuentaBanco":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                    cuentaBanco = _servicioBanca.ObtenerCuentaBanco(intIdLlave1);
                    if (cuentaBanco != null)
                        strRespuesta = JsonConvert.SerializeObject(cuentaBanco);
                    break;
                case "AgregarMovimientoBanco":
                    movimientoBanco = JsonConvert.DeserializeObject<MovimientoBanco>(strEntidad);
                    string strIdMov = _servicioBanca.AgregarMovimientoBanco(movimientoBanco);
                    strRespuesta = JsonConvert.SerializeObject(strIdMov);
                    break;
                case "ObtenerMovimientoBanco":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    movimientoBanco = _servicioBanca.ObtenerMovimientoBanco(intIdLlave1);
                    if (movimientoBanco != null)
                        strRespuesta = JsonConvert.SerializeObject(movimientoBanco);
                    break;
                case "ObtenerTotalListaMovimientoBanco":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioBanca.ObtenerTotalListaMovimientos(intIdEmpresa, intIdSucursal, strDescripcion, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoMovimientoBanco":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<EfectivoDetalle> listadoMovimientos = _servicioBanca.ObtenerListadoMovimientos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, strDescripcion, strFechaFinal);
                    if (listadoMovimientos.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientos);
                    break;
                case "ObtenerListadoVendedores":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoVendedores = _servicioMantenimiento.ObtenerListadoVendedores(intIdEmpresa, strNombre);
                    if (listadoVendedores.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoVendedores);
                    break;
                case "ObtenerVendedor":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                    vendedor = _servicioMantenimiento.ObtenerVendedor(intIdLlave1);
                    if (vendedor != null)
                        strRespuesta = JsonConvert.SerializeObject(vendedor);
                    break;
                case "ObtenerVendedorPorDefecto":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    vendedor = _servicioMantenimiento.ObtenerVendedorPorDefecto(intIdEmpresa);
                    if (vendedor != null)
                        strRespuesta = JsonConvert.SerializeObject(vendedor);
                    break;
                case "ObtenerTotalListaEgresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFlujoCaja.ObtenerTotalListaEgresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoEgresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<EfectivoDetalle> listadoEgreso = _servicioFlujoCaja.ObtenerListadoEgresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle, strFechaFinal);
                    if (listadoEgreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoEgreso);
                    break;
                case "AgregarEgreso":
                    egreso = JsonConvert.DeserializeObject<Egreso>(strEntidad);
                    string strIdEgreso = _servicioFlujoCaja.AgregarEgreso(egreso);
                    strRespuesta = JsonConvert.SerializeObject(strIdEgreso);
                    break;
                case "ObtenerEgreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    egreso = _servicioFlujoCaja.ObtenerEgreso(intIdLlave1);
                    if (egreso != null)
                        strRespuesta = JsonConvert.SerializeObject(egreso);
                    break;
                case "ObtenerTotalListaIngresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdIngreso") != null ? int.Parse(parametrosJO.Property("IdIngreso").Value.ToString()) : 0;
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFlujoCaja.ObtenerTotalListaIngresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoIngresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdIngreso") != null ? int.Parse(parametrosJO.Property("IdIngreso").Value.ToString()) : 0;
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<EfectivoDetalle> listadoIngreso = _servicioFlujoCaja.ObtenerListadoIngresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle, strFechaFinal);
                    if (listadoIngreso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoIngreso);
                    break;
                case "AgregarIngreso":
                    ingreso = JsonConvert.DeserializeObject<Ingreso>(strEntidad);
                    string strIdIngreso = _servicioFlujoCaja.AgregarIngreso(ingreso);
                    strRespuesta = JsonConvert.SerializeObject(strIdIngreso);
                    break;
                case "ObtenerIngreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                    ingreso = _servicioFlujoCaja.ObtenerIngreso(intIdLlave1);
                    if (ingreso != null)
                        strRespuesta = JsonConvert.SerializeObject(ingreso);
                    break;
                case "ObtenerTotalListaFacturas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strIdentificacion = parametrosJO.Property("Identificacion") != null ? parametrosJO.Property("Identificacion").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaFacturas(intIdEmpresa, intIdSucursal, bolIncluyeNulos, intIdLlave1, strNombre, strIdentificacion, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoFacturas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strIdentificacion = parametrosJO.Property("Identificacion") != null ? parametrosJO.Property("Identificacion").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<FacturaDetalle> listadoFacturas = _servicioFacturacion.ObtenerListadoFacturas(intIdEmpresa, intIdSucursal, bolIncluyeNulos, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strIdentificacion, strFechaFinal);
                    if (listadoFacturas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFacturas);
                    break;
                case "AgregarFactura":
                    factura = JsonConvert.DeserializeObject<Factura>(strEntidad);
                    string strIdFactura = _servicioFacturacion.AgregarFactura(factura);
                    strRespuesta = JsonConvert.SerializeObject(strIdFactura);
                    break;
                case "AgregarFacturaCompra":
                    facturaCompra = JsonConvert.DeserializeObject<FacturaCompra>(strEntidad);
                    string strIdFacturaCompra = _servicioFacturacion.AgregarFacturaCompra(facturaCompra);
                    strRespuesta = JsonConvert.SerializeObject(strIdFacturaCompra);
                    break;
                case "ObtenerFactura":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                    factura = _servicioFacturacion.ObtenerFactura(intIdLlave1);
                    if (factura != null)
                        strRespuesta = JsonConvert.SerializeObject(factura);
                    break;
                case "ObtenerTotalListaDevolucionesPorCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaDevolucionesPorCliente(intIdEmpresa, intIdSucursal, intIdLlave1, strNombre, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoDevolucionesPorCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<FacturaDetalle> listadoDevolucionClientes = _servicioFacturacion.ObtenerListadoDevolucionesPorCliente(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strFechaFinal);
                    if (listadoDevolucionClientes.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoDevolucionClientes);
                    break;
                case "AgregarDevolucionCliente":
                    devolucionCliente = JsonConvert.DeserializeObject<DevolucionCliente>(strEntidad);
                    string strIdDevolucionCliente = _servicioFacturacion.AgregarDevolucionCliente(devolucionCliente);
                    strRespuesta = JsonConvert.SerializeObject(strIdDevolucionCliente);
                    break;
                case "ObtenerDevolucionCliente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                    devolucionCliente = _servicioFacturacion.ObtenerDevolucionCliente(intIdLlave1);
                    if (devolucionCliente != null)
                        strRespuesta = JsonConvert.SerializeObject(devolucionCliente);
                    break;
                case "ObtenerTotalListaCompras":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                    strClave = parametrosJO.Property("RefFactura") != null ? parametrosJO.Property("RefFactura").Value.ToString() : "";
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioCompra.ObtenerTotalListaCompras(intIdEmpresa, intIdSucursal, intIdLlave1, strClave, strNombre, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoCompras":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                    strClave = parametrosJO.Property("RefFactura") != null ? parametrosJO.Property("RefFactura").Value.ToString() : "";
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<CompraDetalle> listadoCompras = (List<CompraDetalle>)_servicioCompra.ObtenerListadoCompras(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strClave, strNombre, strFechaFinal);
                    if (listadoCompras.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCompras);
                    break;
                case "AgregarCompra":
                    compra = JsonConvert.DeserializeObject<Compra>(strEntidad);
                    string strIdCompra = _servicioCompra.AgregarCompra(compra);
                    strRespuesta = JsonConvert.SerializeObject(strIdCompra);
                    break;
                case "ObtenerCompra":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                    compra = _servicioCompra.ObtenerCompra(intIdLlave1);
                    if (compra != null)
                        strRespuesta = JsonConvert.SerializeObject(compra);
                    break;
                case "ObtenerTotalListaProformas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaProformas(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intIdLlave1, strNombre, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoProformas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<FacturaDetalle> listadoProformas = _servicioFacturacion.ObtenerListadoProformas(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strFechaFinal);
                    if (listadoProformas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProformas);
                    break;
                case "AgregarProforma":
                    proforma = JsonConvert.DeserializeObject<Proforma>(strEntidad);
                    string strIdProforma = _servicioFacturacion.AgregarProforma(proforma);
                    strRespuesta = JsonConvert.SerializeObject(strIdProforma);
                    break;
                case "ObtenerProforma":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                    proforma = _servicioFacturacion.ObtenerProforma(intIdLlave1);
                    if (proforma != null)
                        strRespuesta = JsonConvert.SerializeObject(proforma);
                    break;
                case "ObtenerTotalListaOrdenServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaOrdenServicio(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intIdLlave1, strNombre, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoOrdenServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<FacturaDetalle> listadoOrdenServicio = _servicioFacturacion.ObtenerListadoOrdenServicio(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strFechaFinal);
                    if (listadoOrdenServicio.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoOrdenServicio);
                    break;
                case "AgregarOrdenServicio":
                    ordenServicio = JsonConvert.DeserializeObject<OrdenServicio>(strEntidad);
                    string strIdOrdenServicio = _servicioFacturacion.AgregarOrdenServicio(ordenServicio);
                    strRespuesta = JsonConvert.SerializeObject(strIdOrdenServicio);
                    break;
                case "ObtenerOrdenServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                    ordenServicio = _servicioFacturacion.ObtenerOrdenServicio(intIdLlave1);
                    if (ordenServicio != null)
                        strRespuesta = JsonConvert.SerializeObject(ordenServicio);
                    break;
                case "ObtenerTotalListaApartados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaApartados(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intIdLlave1, strNombre, strFechaFinal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoApartados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    bolIncluyeNulos = parametrosJO.Property("IncluyeNulos") != null ? bool.Parse(parametrosJO.Property("IncluyeNulos").Value.ToString()) : false;
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<FacturaDetalle> listadoApartados = _servicioFacturacion.ObtenerListadoApartados(intIdEmpresa, intIdSucursal, bolAplicado, bolIncluyeNulos, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strFechaFinal);
                    if (listadoApartados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoApartados);
                    break;
                case "AgregarApartado":
                    apartado = JsonConvert.DeserializeObject<Apartado>(strEntidad);
                    string strIdApartado = _servicioFacturacion.AgregarApartado(apartado);
                    strRespuesta = JsonConvert.SerializeObject(strIdApartado);
                    break;
                case "ObtenerApartado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                    apartado = _servicioFacturacion.ObtenerApartado(intIdLlave1);
                    if (apartado != null)
                        strRespuesta = JsonConvert.SerializeObject(apartado);
                    break;
                case "ObtenerCuentaPorCobrar":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCxC").Value.ToString());
                    cuentaPorCobrar = _servicioCuentaPorProcesar.ObtenerCuentaPorCobrar(intIdLlave1);
                    if (cuentaPorCobrar != null)
                        strRespuesta = JsonConvert.SerializeObject(cuentaPorCobrar);
                    break;
                case "ObtenerTotalListaCuentasPorCobrar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                    strClave = parametrosJO.Property("Referencia").Value.ToString();
                    strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                    intTotalLista = _servicioCuentaPorProcesar.ObtenerTotalListaCuentasPorCobrar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, strClave, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoCuentasPorCobrar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Pendientes").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strClave = parametrosJO.Property("Referencia").Value.ToString();
                    strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                    IList<CuentaPorProcesar> listadoCuentasPorCobrar = _servicioCuentaPorProcesar.ObtenerListadoCuentasPorCobrar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, intNumeroPagina, intFilasPorPagina, strClave, strNombre);
                    if (listadoCuentasPorCobrar.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCuentasPorCobrar);
                    break;
                case "ObtenerListaMovimientosCxC":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                    IList<EfectivoDetalle> listadoMovimientosCxC = _servicioCuentaPorProcesar.ObtenerListadoMovimientosCxC(intIdEmpresa, intIdSucursal, intIdCuenta);
                    if (listadoMovimientosCxC.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientosCxC);
                    break;
                case "ObtenerMovimientoCxC":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    movimientoCxC = _servicioCuentaPorProcesar.ObtenerMovimientoCxC(intIdLlave1);
                    if (movimientoCxC != null)
                        strRespuesta = JsonConvert.SerializeObject(movimientoCxC);
                    break;
                case "ObtenerCuentaPorPagar":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCxP").Value.ToString());
                    cuentaPorPagar = _servicioCuentaPorProcesar.ObtenerCuentaPorPagar(intIdLlave1);
                    if (cuentaPorPagar != null)
                        strRespuesta = JsonConvert.SerializeObject(cuentaPorPagar);
                    break;
                case "ObtenerTotalListaCuentasPorPagar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Pendientes").Value.ToString());
                    strClave = parametrosJO.Property("Referencia").Value.ToString();
                    strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                    intTotalLista = _servicioCuentaPorProcesar.ObtenerTotalListaCuentasPorPagar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, strClave, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoCuentasPorPagar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Pendientes").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strClave = parametrosJO.Property("Referencia").Value.ToString();
                    strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                    IList<CuentaPorProcesar> listadoCuentasPorPagar = _servicioCuentaPorProcesar.ObtenerListadoCuentasPorPagar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, intNumeroPagina, intFilasPorPagina, strClave, strNombre);
                    if (listadoCuentasPorPagar.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCuentasPorPagar);
                    break;
                case "ObtenerListaMovimientosCxP":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                    IList<EfectivoDetalle> listadoMovimientosCxP = _servicioCuentaPorProcesar.ObtenerListadoMovimientosCxP(intIdEmpresa, intIdSucursal, intIdCuenta);
                    if (listadoMovimientosCxP.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientosCxP);
                    break;
                case "ObtenerMovimientoCxP":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    movimientoCxP = _servicioCuentaPorProcesar.ObtenerMovimientoCxP(intIdLlave1);
                    if (movimientoCxP != null)
                        strRespuesta = JsonConvert.SerializeObject(movimientoCxP);
                    break;
                case "ObtenerListadoApartadosConSaldo":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    IList<LlaveDescripcion> listadoApartadosConSaldo = _servicioCuentaPorProcesar.ObtenerListadoApartadosConSaldo(intIdEmpresa);
                    if (listadoApartadosConSaldo.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoApartadosConSaldo);
                    break;
                case "ObtenerListadoMovimientosApartado":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                    IList<EfectivoDetalle> listadoMovimientosApartado = _servicioCuentaPorProcesar.ObtenerListadoMovimientosApartado(intIdEmpresa, intIdSucursal, intIdLlave1);
                    if (listadoMovimientosApartado.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientosApartado);
                    break;
                case "ObtenerMovimientoApartado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    movimientoApartado = _servicioCuentaPorProcesar.ObtenerMovimientoApartado(intIdLlave1);
                    if (movimientoApartado != null)
                        strRespuesta = JsonConvert.SerializeObject(movimientoApartado);
                    break;
                case "ObtenerListadoOrdenesServicioConSaldo":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    IList<LlaveDescripcion> listadoOrdenesServicioConSaldo = _servicioCuentaPorProcesar.ObtenerListadoOrdenesServicioConSaldo(intIdEmpresa);
                    if (listadoOrdenesServicioConSaldo.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoOrdenesServicioConSaldo);
                    break;
                case "ObtenerListadoMovimientosOrdenServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdOrden").Value.ToString());
                    IList<EfectivoDetalle> listadoMovimientosOrdenServicio = _servicioCuentaPorProcesar.ObtenerListadoMovimientosOrdenServicio(intIdEmpresa, intIdSucursal, intIdLlave1);
                    if (listadoMovimientosOrdenServicio.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoMovimientosOrdenServicio);
                    break;
                case "ObtenerMovimientoOrdenServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    movimientoOrdenServicio = _servicioCuentaPorProcesar.ObtenerMovimientoOrdenServicio(intIdLlave1);
                    if (movimientoOrdenServicio != null)
                        strRespuesta = JsonConvert.SerializeObject(movimientoOrdenServicio);
                    break;
                case "ObtenerListadoDocumentosElectronicosEnProceso":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    IList<DocumentoDetalle> listadoEnProceso = _servicioFacturacion.ObtenerListadoDocumentosElectronicosEnProceso(intIdEmpresa);
                    if (listadoEnProceso.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoEnProceso);
                    break;
                case "ObtenerTotalDocumentosElectronicosProcesados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    int intTotalDocumentosProcesados = _servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal, strNombre, strFechaFinal);
                    strRespuesta = intTotalDocumentosProcesados.ToString();
                    break;
                case "ObtenerListadoDocumentosElectronicosProcesados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<DocumentoDetalle> listadoProcesados = _servicioFacturacion.ObtenerListadoDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, strNombre, strFechaFinal);
                    if (listadoProcesados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProcesados);
                    break;
                case "ObtenerDocumentoElectronico":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    documento = _servicioFacturacion.ObtenerDocumentoElectronico(intIdLlave1);
                    if (documento != null)
                        strRespuesta = JsonConvert.SerializeObject(documento);
                    break;
                case "AutorizacionPorcentaje":
                    strCodigoUsuario = parametrosJO.Property("CodigoUsuario").Value.ToString();
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    decimal decPorcentaje = _servicioMantenimiento.AutorizacionPorcentaje(strCodigoUsuario, strClave, intIdEmpresa);
                    strRespuesta = decPorcentaje.ToString();
                    break;
                case "AgregarTraslado":
                    traslado = JsonConvert.DeserializeObject<Traslado>(strEntidad);
                    string strIdTraslado = _servicioTraslado.AgregarTraslado(traslado);
                    strRespuesta = JsonConvert.SerializeObject(strIdTraslado);
                    break;
                case "ObtenerTraslado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                    traslado = _servicioTraslado.ObtenerTraslado(intIdLlave1);
                    if (traslado != null)
                        strRespuesta = JsonConvert.SerializeObject(traslado);
                    break;
                case "ObtenerTotalListaTraslados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    int intTotalTraslados = _servicioTraslado.ObtenerTotalListaTraslados(intIdEmpresa, intIdSucursal, bolAplicado, intIdLlave1);
                    strRespuesta = intTotalTraslados.ToString();
                    break;
                case "ObtenerListadoTraslados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                    IList<TrasladoDetalle> listadoTraslados = _servicioTraslado.ObtenerListadoTraslados(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1);
                    if (listadoTraslados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTraslados);
                    break;
                case "ObtenerTotalListaTrasladosPorAplicar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalDestino").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    int intTotalTrasladosPorAplicar = _servicioTraslado.ObtenerTotalListaTrasladosPorAplicar(intIdEmpresa, intIdSucursal, bolAplicado);
                    strRespuesta = intTotalTrasladosPorAplicar.ToString();
                    break;
                case "ObtenerListadoTrasladosPorAplicar":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalDestino").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    IList<TrasladoDetalle> listadoTrasladosPorAplicar = _servicioTraslado.ObtenerListadoTrasladosPorAplicar(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina);
                    if (listadoTrasladosPorAplicar.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTrasladosPorAplicar);
                    break;
                case "AgregarAjusteInventario":
                    ajusteInventario = JsonConvert.DeserializeObject<AjusteInventario>(strEntidad);
                    string strIdAjuste = _servicioMantenimiento.AgregarAjusteInventario(ajusteInventario);
                    strRespuesta = JsonConvert.SerializeObject(strIdAjuste);
                    break;
                case "ObtenerAjusteInventario":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                    ajusteInventario = _servicioMantenimiento.ObtenerAjusteInventario(intIdLlave1);
                    if (ajusteInventario != null)
                        strRespuesta = JsonConvert.SerializeObject(ajusteInventario);
                    break;
                case "ObtenerTotalListaAjusteInventario":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    int intTotalAjusteInventarios = _servicioMantenimiento.ObtenerTotalListaAjusteInventario(intIdEmpresa, intIdSucursal, intIdLlave1, strDescripcion, strFechaFinal);
                    strRespuesta = intTotalAjusteInventarios.ToString();
                    break;
                case "ObtenerListadoAjusteInventario":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                    IList<AjusteInventarioDetalle> listadoAjusteInventarios = _servicioMantenimiento.ObtenerListadoAjusteInventario(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strDescripcion, strFechaFinal);
                    if (listadoAjusteInventarios.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoAjusteInventarios);
                    break;
                case "ObtenerTotalListaCierreCaja":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intTotalLista = _servicioFlujoCaja.ObtenerTotalListaCierreCaja(intIdEmpresa, intIdSucursal);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoCierreCaja":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    IList<LlaveDescripcion> listadoCierreCaja = _servicioFlujoCaja.ObtenerListadoCierreCaja(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina);
                    if (listadoCierreCaja.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCierreCaja);
                    break;
                case "ObtenerCierreCaja":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCierre").Value.ToString());
                    cierre = _servicioFlujoCaja.ObtenerCierreCaja(intIdLlave1);
                    if (cierre != null)
                        strRespuesta = JsonConvert.SerializeObject(cierre);
                    break;
                case "ObtenerFacturaPDF":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    byte[] facturaPdf = _servicioFacturacion.GenerarFacturaPDF(intIdLlave1, bytLogo);
                    if (facturaPdf.Length > 0)
                        strRespuesta = JsonConvert.SerializeObject(facturaPdf);
                    break;
                case "ObtenerApartadoPDF":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    byte[] apartadoPdf = _servicioFacturacion.GenerarApartadoPDF(intIdLlave1, bytLogo);
                    if (apartadoPdf.Length > 0)
                        strRespuesta = JsonConvert.SerializeObject(apartadoPdf);
                    break;
                case "ObtenerOrdenServicioPDF":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdOrden").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    byte[] ordenPdf = _servicioFacturacion.GenerarOrdenServicioPDF(intIdLlave1, bytLogo);
                    if (ordenPdf.Length > 0)
                        strRespuesta = JsonConvert.SerializeObject(ordenPdf);
                    break;
                case "ObtenerProformaPDF":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    byte[] proformaPdf = _servicioFacturacion.GenerarProformaPDF(intIdLlave1, bytLogo);
                    if (proformaPdf.Length > 0)
                        strRespuesta = JsonConvert.SerializeObject(proformaPdf);
                    break;
                case "ObtenerListadoPuntoDeServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    IList<LlaveDescripcion> listadoPuntoDeServicio = _servicioMantenimiento.ObtenerListadoPuntoDeServicio(intIdEmpresa, intIdSucursal, bolFiltraActivos, strDescripcion);
                    if (listadoPuntoDeServicio.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoPuntoDeServicio);
                    break;
                case "ObtenerPuntoDeServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdPuntoDeServicio").Value.ToString());
                    puntoDeServicio = _servicioMantenimiento.ObtenerPuntoDeServicio(intIdLlave1);
                    if (puntoDeServicio != null)
                        strRespuesta = JsonConvert.SerializeObject(puntoDeServicio);
                    break;
                case "ObtenerListadoTiqueteOrdenServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolFiltraActivos = bool.Parse(parametrosJO.Property("Impreso").Value.ToString());
                    IList<ClsTiquete> listadoTiqueteOrdenServicio = _servicioFacturacion.ObtenerListadoTiqueteOrdenServicio(intIdEmpresa, intIdSucursal, bolFiltraActivos, true);
                    if (listadoTiqueteOrdenServicio.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTiqueteOrdenServicio);
                    break;
                case "ObtenerDatosReporte":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    string strNombreReporte = parametrosJO.Property("NombreReporte").Value.ToString();
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    switch (strNombreReporte)
                    {
                        case "Ventas en general":
                            IList<ReporteDetalle> listado1 = _servicioReportes.ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, 0, false, 0);
                            if (listado1.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado1);
                            break;
                        case "Ventas anuladas":
                            IList<ReporteDetalle> listado2 = _servicioReportes.ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, 0, true, 0);
                            if (listado2.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado2);
                            break;
                        case "Resumen de movimientos":
                            IList<DescripcionValor> listado3 = _servicioReportes.ObtenerReporteEstadoResultados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                            if (listado3.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado3);
                            break;
                        case "Detalle de ingresos":
                            IList<ReporteGrupoDetalle> listado4 = _servicioReportes.ObtenerReporteDetalleIngreso(intIdEmpresa, intIdSucursal, 0, strFechaInicial, strFechaFinal);
                            if (listado4.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado4);
                            break;
                        case "Detalle de egresos":
                            IList<ReporteGrupoDetalle> listado5 = _servicioReportes.ObtenerReporteDetalleEgreso(intIdEmpresa, intIdSucursal, 0, strFechaInicial, strFechaFinal);
                            if (listado5.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado5);
                            break;
                        case "Documentos electrónicos emitidos":
                            List<ReporteDocumentoElectronico> listado6 = _servicioReportes.ObtenerReporteDocumentosElectronicosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                            if (listado6.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado6);
                            break;
                        case "Documentos electrónicos recibidos":
                            List<ReporteDocumentoElectronico> listado7 = _servicioReportes.ObtenerReporteDocumentosElectronicosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                            if (listado7.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado7);
                            break;
                        case "Resumen de comprobantes electrónicos":
                            List<ReporteResumenMovimiento> listado8 = _servicioReportes.ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                            if (listado8.Count > 0)
                                strRespuesta = JsonConvert.SerializeObject(listado8);
                            break;
                        default:
                            throw new Exception("El reporte solicitado: '" + strNombreReporte + "' no ha sido implementado, contacte con su proveedor");
                    }
                    break;
                default:
                    throw new Exception("El mótodo solicitado no ha sido implementado: " + strNombreMetodo);
            }
            return strRespuesta;
        }
    }
}