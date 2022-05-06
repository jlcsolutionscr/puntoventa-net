using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("puntoventa")]
    public class EjecutarController : ControllerBase
    {
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static ICompraService _servicioCompra;
        private static IFlujoCajaService _servicioFlujoCaja;
        private static IBancaService _servicioBanca;
        private static IReporteService _servicioReportes;
        private static ITrasladoService _servicioTraslado;
        private static ICuentaPorProcesarService _servicioCuentaPorProcesar;
        private static ConfiguracionGeneral configuracionGeneral;
        private static Empresa? empresa;
        private static CredencialesHacienda credenciales;
        private static SucursalPorEmpresa? sucursal;
        private static TerminalPorSucursal? terminal;
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
        private static Proforma? proforma;
        private static OrdenServicio? ordenServicio;
        private static MovimientoCuentaPorCobrar? movimientoCxC;
        private static MovimientoCuentaPorPagar? movimientoCxP;
        private static MovimientoApartado? movimientoApartado;
        private static MovimientoOrdenServicio? movimientoOrdenServicio;
        private static PuntoDeServicio? puntoDeServicio;
        private static int intIdEmpresa;
        private static int intIdSucursal;
        private static int intIdUsuario;
        private static int intIdTipoPago;
        private static int intIdLlave1;
        private static string strLogoPath;
        private static string strUsuario;
        private static string strClave;
        private static string strNombreCertificado;
        private static string strPin;
        private static string strCertificado;
        private static bool bolAplicado;
        private static string strMotivoAnulacion;
        private static string strFechaInicial;
        private static string strFechaFinal;
        private static byte[] bytLogo;
        private static decimal decTipoCambioDolar = 0;

        public EjecutarController(
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
            configuracionGeneral = new ConfiguracionGeneral
            (
                configuration.GetSection("appSettings").GetSection("strConsultaTipoCambioDolarURL").Value,
                configuration.GetSection("appSettings").GetSection("strConsultaContribuyenteURL").Value,
                configuration.GetSection("appSettings").GetSection("strServicioComprobantesURL").Value,
                configuration.GetSection("appSettings").GetSection("strClientId").Value,
                configuration.GetSection("appSettings").GetSection("strServicioTokenURL").Value,
                configuration.GetSection("appSettings").GetSection("strComprobantesCallbackURL").Value,
                configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value
            );
            if (decTipoCambioDolar == 0) decTipoCambioDolar = 1;
        }


        [HttpPost("ejecutar")]
        public void Ejecutar([FromBody] string strDatos)
        {
            JObject datosJO = JObject.Parse(strDatos);
            JObject parametrosJO = null;
            string strNombreMetodo;
            string strEntidad = null;
            string strCorreoReceptor;
            if (datosJO.Property("NombreMetodo") != null)
            {
                strNombreMetodo = datosJO.Property("NombreMetodo").Value.ToString();
            }
            else
            {
                throw new Exception("El mensaje no contiene la información suficiente para ser procesado.");
            }
            if (datosJO.Property("Entidad") != null)
            {
                strEntidad = datosJO.Property("Entidad").Value.ToString();
            }
            else if (datosJO.Property("Parametros") != null)
            {
                parametrosJO = JObject.Parse(datosJO.Property("Parametros").Value.ToString());
            }
            else
            {
                throw new Exception("El mensaje no contiene la información suficiente para ser procesado.");
            }
            switch (strNombreMetodo)
            {
                case "ActualizarParametroDelSistema":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdParametro").Value.ToString());
                    string strValor = parametrosJO.Property("Valor").Value.ToString();
                    _servicioMantenimiento.ActualizarParametroSistema(intIdLlave1, strValor);
                    break;
                case "AbortarCierreCaja":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    _servicioFlujoCaja.AbortarCierreCaja(intIdEmpresa, intIdSucursal);
                    break;
                case "ActualizarEmpresa":
                    empresa = JsonConvert.DeserializeObject<Empresa>(strEntidad);
                    _servicioMantenimiento.ActualizarEmpresa(empresa);
                    break;
                case "ActualizarLogoEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    string strLogotipo = parametrosJO.Property("Logotipo").Value.ToString();
                    _servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
                    break;
                case "RemoverLogoEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    _servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, "");
                    break;
                case "AgregarCredencialesHacienda":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strUsuario = parametrosJO.Property("Usuario").Value.ToString();
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    strNombreCertificado = parametrosJO.Property("NombreCertificado").Value.ToString();
                    strPin = parametrosJO.Property("PinCertificado").Value.ToString();
                    strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                    byte[] bytCertificado = Convert.FromBase64String(strCertificado);
                    credenciales = new CredencialesHacienda();
                    credenciales.IdEmpresa = intIdEmpresa;
                    credenciales.UsuarioHacienda = strUsuario;
                    credenciales.ClaveHacienda = strClave;
                    credenciales.NombreCertificado = strNombreCertificado;
                    credenciales.PinCertificado = strPin;
                    credenciales.Certificado = bytCertificado;
                    _servicioMantenimiento.AgregarCredencialesHacienda(credenciales);
                    break;
                case "ActualizarCredencialesHacienda":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strUsuario = parametrosJO.Property("Usuario").Value.ToString();
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    strNombreCertificado = parametrosJO.Property("NombreCertificado").Value.ToString();
                    strPin = parametrosJO.Property("PinCertificado").Value.ToString();
                    strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                    _servicioMantenimiento.ActualizarCredencialesHacienda(intIdEmpresa, strUsuario, strClave, strNombreCertificado, strPin, strCertificado);
                    break;
                case "ValidarCredencialesHacienda":
                    string strCodigo = parametrosJO.Property("CodigoUsuario").Value.ToString();
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    _servicioMantenimiento.ValidarCredencialesHacienda(strCodigo, strClave, configuracionGeneral);
                    break;
                case "ValidarCertificadoHacienda":
                    strPin = parametrosJO.Property("PinCertificado").Value.ToString();
                    strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                    _servicioMantenimiento.ValidarCertificadoHacienda(strPin, strCertificado);
                    break;
                case "ActualizarSucursalPorEmpresa":
                    sucursal = JsonConvert.DeserializeObject<SucursalPorEmpresa>(strEntidad);
                    _servicioMantenimiento.ActualizarSucursalPorEmpresa(sucursal);
                    break;
                case "ActualizarTerminalPorSucursal":
                    terminal = JsonConvert.DeserializeObject<TerminalPorSucursal>(strEntidad);
                    _servicioMantenimiento.ActualizarTerminalPorSucursal(terminal);
                    break;
                case "AgregarBancoAdquiriente":
                    bancoAdquiriente = JsonConvert.DeserializeObject<BancoAdquiriente>(strEntidad);
                    _servicioMantenimiento.AgregarBancoAdquiriente(bancoAdquiriente);
                    break;
                case "ActualizarBancoAdquiriente":
                    bancoAdquiriente = JsonConvert.DeserializeObject<BancoAdquiriente>(strEntidad);
                    _servicioMantenimiento.ActualizarBancoAdquiriente(bancoAdquiriente);
                    break;
                case "EliminarBancoAdquiriente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                    _servicioMantenimiento.EliminarBancoAdquiriente(intIdLlave1);
                    break;
                case "AgregarCliente":
                    cliente = JsonConvert.DeserializeObject<Cliente>(strEntidad);
                    _servicioFacturacion.AgregarCliente(cliente);
                    break;
                case "ActualizarCliente":
                    cliente = JsonConvert.DeserializeObject<Cliente>(strEntidad);
                    _servicioFacturacion.ActualizarCliente(cliente);
                    break;
                case "EliminarCliente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                    _servicioFacturacion.EliminarCliente(intIdLlave1);
                    break;
                case "AgregarLinea":
                    linea = JsonConvert.DeserializeObject<Linea>(strEntidad);
                    _servicioMantenimiento.AgregarLinea(linea);
                    break;
                case "ActualizarLinea":
                    linea = JsonConvert.DeserializeObject<Linea>(strEntidad);
                    _servicioMantenimiento.ActualizarLinea(linea);
                    break;
                case "EliminarLinea":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                    _servicioMantenimiento.EliminarLinea(intIdLlave1);
                    break;
                case "AgregarProveedor":
                    proveedor = JsonConvert.DeserializeObject<Proveedor>(strEntidad);
                    _servicioCompra.AgregarProveedor(proveedor);
                    break;
                case "ActualizarProveedor":
                    proveedor = JsonConvert.DeserializeObject<Proveedor>(strEntidad);
                    _servicioCompra.ActualizarProveedor(proveedor);
                    break;
                case "EliminarProveedor":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                    _servicioCompra.EliminarProveedor(intIdLlave1);
                    break;
                case "AgregarProducto":
                    producto = JsonConvert.DeserializeObject<Producto>(strEntidad);
                    _servicioMantenimiento.AgregarProducto(producto);
                    break;
                case "ActualizarProducto":
                    producto = JsonConvert.DeserializeObject<Producto>(strEntidad);
                    _servicioMantenimiento.ActualizarProducto(producto);
                    break;
                case "EliminarProducto":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                    _servicioMantenimiento.EliminarProducto(intIdLlave1);
                    break;
                case "AgregarUsuario":
                    usuario = JsonConvert.DeserializeObject<Usuario>(strEntidad);
                    _servicioMantenimiento.AgregarUsuario(usuario);
                    break;
                case "ActualizarUsuario":
                    usuario = JsonConvert.DeserializeObject<Usuario>(strEntidad);
                    _servicioMantenimiento.ActualizarUsuario(usuario);
                    break;
                case "EliminarUsuario":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    _servicioMantenimiento.EliminarUsuario(intIdLlave1);
                    break;
                case "AgregarCuentaEgreso":
                    cuentaEgreso = JsonConvert.DeserializeObject<CuentaEgreso>(strEntidad);
                    _servicioFlujoCaja.AgregarCuentaEgreso(cuentaEgreso);
                    break;
                case "ActualizarCuentaEgreso":
                    cuentaEgreso = JsonConvert.DeserializeObject<CuentaEgreso>(strEntidad);
                    _servicioFlujoCaja.ActualizarCuentaEgreso(cuentaEgreso);
                    break;
                case "EliminarCuentaEgreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                    _servicioFlujoCaja.EliminarCuentaEgreso(intIdLlave1);
                    break;
                case "AgregarCuentaIngreso":
                    cuentaIngreso = JsonConvert.DeserializeObject<CuentaIngreso>(strEntidad);
                    _servicioFlujoCaja.AgregarCuentaIngreso(cuentaIngreso);
                    break;
                case "ActualizarCuentaIngreso":
                    cuentaIngreso = JsonConvert.DeserializeObject<CuentaIngreso>(strEntidad);
                    _servicioFlujoCaja.ActualizarCuentaIngreso(cuentaIngreso);
                    break;
                case "EliminarCuentaIngreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                    _servicioFlujoCaja.EliminarCuentaIngreso(intIdLlave1);
                    break;
                case "AgregarCuentaBanco":
                    cuentaBanco = JsonConvert.DeserializeObject<CuentaBanco>(strEntidad);
                    _servicioBanca.AgregarCuentaBanco(cuentaBanco);
                    break;
                case "ActualizarCuentaBanco":
                    cuentaBanco = JsonConvert.DeserializeObject<CuentaBanco>(strEntidad);
                    _servicioBanca.ActualizarCuentaBanco(cuentaBanco);
                    break;
                case "EliminarCuentaBanco":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                    _servicioBanca.EliminarCuentaBanco(intIdLlave1);
                    break;
                case "AgregarVendedor":
                    vendedor = JsonConvert.DeserializeObject<Vendedor>(strEntidad);
                    _servicioMantenimiento.AgregarVendedor(vendedor);
                    break;
                case "ActualizarVendedor":
                    vendedor = JsonConvert.DeserializeObject<Vendedor>(strEntidad);
                    _servicioMantenimiento.ActualizarVendedor(vendedor);
                    break;
                case "EliminarVendedor":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                    _servicioMantenimiento.EliminarVendedor(intIdLlave1);
                    break;
                case "AnularEgreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFlujoCaja.AnularEgreso(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularIngreso":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFlujoCaja.AnularIngreso(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularFactura":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFacturacion.AnularFactura(intIdLlave1, intIdUsuario, strMotivoAnulacion, configuracionGeneral);
                    break;
                case "AnularDevolucionCliente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFacturacion.AnularDevolucionCliente(intIdLlave1, intIdUsuario, strMotivoAnulacion, configuracionGeneral);
                    break;
                case "AnularCompra":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioCompra.AnularCompra(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularProforma":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFacturacion.AnularProforma(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "ActualizarProforma":
                    proforma = JsonConvert.DeserializeObject<Proforma>(strEntidad);
                    _servicioFacturacion.ActualizarProforma(proforma);
                    break;
                case "AnularApartado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFacturacion.AnularApartado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularOrdenServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioFacturacion.AnularOrdenServicio(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "ActualizarOrdenServicio":
                    ordenServicio = JsonConvert.DeserializeObject<OrdenServicio>(strEntidad);
                    _servicioFacturacion.ActualizarOrdenServicio(ordenServicio);
                    break;
                case "AplicarTraslado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    _servicioTraslado.AplicarTraslado(intIdLlave1, intIdUsuario);
                    break;
                case "AnularTraslado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioTraslado.AnularTraslado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularAjusteInventario":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioMantenimiento.AnularAjusteInventario(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AplicarMovimientoCxC":
                    movimientoCxC = JsonConvert.DeserializeObject<MovimientoCuentaPorCobrar>(strEntidad);
                    _servicioCuentaPorProcesar.AplicarMovimientoCxC(movimientoCxC);
                    break;
                case "AnularMovimientoCxC":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioCuentaPorProcesar.AnularMovimientoCxC(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AplicarMovimientoCxP":
                    movimientoCxP = JsonConvert.DeserializeObject<MovimientoCuentaPorPagar>(strEntidad);
                    _servicioCuentaPorProcesar.AplicarMovimientoCxP(movimientoCxP);
                    break;
                case "AnularMovimientoCxP":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioCuentaPorProcesar.AnularMovimientoCxP(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AplicarMovimientoApartado":
                    movimientoApartado = JsonConvert.DeserializeObject<MovimientoApartado>(strEntidad);
                    _servicioCuentaPorProcesar.AplicarMovimientoApartado(movimientoApartado);
                    break;
                case "AnularMovimientoApartado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioCuentaPorProcesar.AnularMovimientoApartado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AplicarMovimientoOrdenServicio":
                    movimientoOrdenServicio = JsonConvert.DeserializeObject<MovimientoOrdenServicio>(strEntidad);
                    _servicioCuentaPorProcesar.AplicarMovimientoOrdenServicio(movimientoOrdenServicio);
                    break;
                case "AnularMovimientoOrdenServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioCuentaPorProcesar.AnularMovimientoOrdenServicio(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AnularMovimientoBanco":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                    intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                    strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                    _servicioBanca.AnularMovimientoBanco(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                    break;
                case "AgregarPuntoDeServicio":
                    puntoDeServicio = JsonConvert.DeserializeObject<PuntoDeServicio>(strEntidad);
                    _servicioMantenimiento.AgregarPuntoDeServicio(puntoDeServicio);
                    break;
                case "ActualizarPuntoDeServicio":
                    puntoDeServicio = JsonConvert.DeserializeObject<PuntoDeServicio>(strEntidad);
                    _servicioMantenimiento.ActualizarPuntoDeServicio(puntoDeServicio);
                    break;
                case "ActualizarEstadoTiqueteOrdenServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTiquete").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Estado").Value.ToString());
                    _servicioFacturacion.ActualizarEstadoTiqueteOrdenServicio(intIdLlave1, bolAplicado);
                    break;
                case "EliminarPuntoDeServicio":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdPuntoDeServicio").Value.ToString());
                    _servicioMantenimiento.EliminarPuntoDeServicio(intIdLlave1);
                    break;
                case "GenerarMensajeReceptor":
                    strDatos = parametrosJO.Property("Datos").Value.ToString();
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdTerminal").Value.ToString());
                    intIdTipoPago = int.Parse(parametrosJO.Property("IdEstado").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("IvaAcreditable").Value.ToString());
                    _servicioFacturacion.GenerarMensajeReceptor(strDatos, intIdEmpresa, intIdSucursal, intIdLlave1, intIdTipoPago, bolAplicado, configuracionGeneral);
                    break;
                case "EnviarNotificacionDocumentoElectronico":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdLlave1, strCorreoReceptor, configuracionGeneral.CorreoNotificacionErrores, bytLogo);
                    break;
                case "GenerarNotificacionFactura":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.GenerarNotificacionFactura(intIdLlave1, bytLogo);
                    break;
                case "GenerarNotificacionProforma":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                    strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.GenerarNotificacionProforma(intIdLlave1, strCorreoReceptor, bytLogo);
                    break;
                case "ReprocesarDocumentoElectronico":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    _servicioFacturacion.ReprocesarDocumentoElectronico(intIdLlave1, configuracionGeneral);
                    break;
                case "EnviarReportePorCorreoElectronico":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    string strNombreReporte = parametrosJO.Property("NombreReporte").Value.ToString();
                    strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                    strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                    // TODO Use parameter for report type once PDF processing issue is fixed on NetCore 6
                    string strFormatoReporte = "HTML5";
                    switch (strNombreReporte)
                    {
                        case "Ventas en general":
                            _servicioReportes.EnviarReporteVentasGenerales(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Ventas anuladas":
                            _servicioReportes.EnviarReporteVentasAnuladas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Resumen de movimientos":
                            _servicioReportes.EnviarReporteResumenMovimientos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Detalle de ingresos":
                            _servicioReportes.EnviarReporteDetalleIngresos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Detalle de egresos":
                            _servicioReportes.EnviarReporteDetalleEgresos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Documentos electrónicos emitidos":
                            _servicioReportes.EnviarReporteDocumentosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Documentos electrónicos recibidos":
                            _servicioReportes.EnviarReporteDocumentosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        case "Resumen de comprobantes electrónicos":
                            _servicioReportes.EnviarReporteResumenMovimientosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte);
                            break;
                        default:
                            throw new Exception("El mótodo solicitado: '" + strNombreReporte + "' no ha sido implementado, contacte con su proveedor");
                    }
                    break;
                default:
                    throw new Exception("El mótodo solicitado no ha sido implementado: " + strNombreMetodo);
            }
        }
    }
}