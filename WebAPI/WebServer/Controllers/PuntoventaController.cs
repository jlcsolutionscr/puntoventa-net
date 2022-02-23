using Microsoft.AspNetCore.Mvc;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("puntoventa")]
    public class PuntoventaController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IHostEnvironment _environment;
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static ICompraService _servicioCompra;
        private static IFlujoCajaService _servicioFlujoCaja;
        private static IBancaService _servicioBanca;
        private static IReporteService _servicioReportes;
        private static ITrasladoService _servicioTraslado;
        private static ICuentaPorProcesarService _servicioCuentaPorProcesar;
        private static ICorreoService _servicioEnvioCorreo;
        private ConfiguracionGeneral configuracionGeneral;
        private static Empresa? empresa;
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
        private static int intIdUsuario;
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
        private string strLogoPath;
        private bool bolIncluyeServicios;
        private bool bolFiltraActivos;
        private bool bolFiltraExistencias;
        private bool bolFiltraConDescuento;
        private bool bolNulo;
        private bool bolAplicado;
        private string strClave;
        private string strIdentificacion;
        private string strCodigo;
        private string strCodigoProveedor;
        private string strDescripcion;
        private string strNombre;
        private string strBeneficiario;
        private string strDetalle;
        private string strMotivoAnulacion;
        private string strFechaInicial;
        private string strFechaFinal;
        private byte[] bytLogo;
        private string strRespuesta = "";

        public PuntoventaController(
            IConfiguration configuration,
            IHostEnvironment environment,
            IMantenimientoService servicioMantenimiento,
            IFacturacionService servicioFacturacion,
            ICompraService servicioCompra,
            IFlujoCajaService servicioFlujoCaja,
            IBancaService servicioBanca,
            IReporteService servicioReportes,
            ITrasladoService servicioTraslado,
            ICuentaPorProcesarService servicioCuentaPorProcesar,
            ICorreoService servicioEnvioCorreo
        )
        {
            _environment = environment;
            _servicioMantenimiento = servicioMantenimiento;
            _servicioFacturacion = servicioFacturacion;
            _servicioCompra = servicioCompra;
            _servicioFlujoCaja = servicioFlujoCaja;
            _servicioBanca = servicioBanca;
            _servicioReportes = servicioReportes;
            _servicioTraslado = servicioTraslado;
            _servicioCuentaPorProcesar = servicioCuentaPorProcesar;
            _servicioEnvioCorreo = servicioEnvioCorreo;
            strLogoPath = Path.Combine(environment.ContentRootPath, "images/Logo.png");
            configuracionGeneral = new ConfiguracionGeneral
            (
                configuration.GetSection("appSettings").GetSection("strConsultaIEURL").Value,
                configuration.GetSection("appSettings").GetSection("strSoapOperation").Value,
                configuration.GetSection("appSettings").GetSection("strServicioComprobantesURL").Value,
                configuration.GetSection("appSettings").GetSection("strClientId").Value,
                configuration.GetSection("appSettings").GetSection("strServicioTokenURL").Value,
                configuration.GetSection("appSettings").GetSection("strComprobantesCallbackURL").Value,
                configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value
            );
            try
            {
                bool modoMantenimiento = _servicioMantenimiento.EnModoMantenimiento();
                if (modoMantenimiento) throw new Exception("El sistema se encuentra en modo mantenimiento y no es posible acceder por el momento.");
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el estado del modo mantenimiento del sistema: ", ex);
                throw new Exception("Error al consultar el estado del modo mantenimiento del sistema");
            }
            /*try
            {
                decTipoCambioDolar = _servicioMantenimiento.ObtenerTipoCambioVenta(configuracionGeneral.ConsultaIndicadoresEconomicosURL, configuracionGeneral.OperacionSoap, DateTime.Now, unityContainer);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw new Exception("Error al consultar el tipo de cambio del dolar");
            }*/
        }

        [HttpGet("enviarhistoricoerrores")]
        public void EnviarHistoricoErrores()
        {
            try
            {
                string[] directoryEntries = Directory.GetFileSystemEntries(_environment.ContentRootPath, "errorlog-??-??-????.txt");
                foreach (string str in directoryEntries)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(str);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = str,
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioEnvioCorreo.SendEmail(new string[] { configuracionGeneral.CorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
                    }
                    System.IO.File.Delete(str);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al enviar los archivos historicos de errores del sistema: ", ex);
            }
        }

        [HttpGet("obtenerultimaversionapp")]
        public string ObtenerUltimaVersionApp()
        {
            try
            {
                return _servicioMantenimiento.ObtenerUltimaVersionApp();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadotiqueteordenserviciopendiente")]
        public string ObtenerListadoTiqueteOrdenServicioPendiente(int idempresa, int idsucursal)
        {
            try
            {
                IList<ClsTiquete> listadoTiqueteOrdenServicio = _servicioFacturacion.ObtenerListadoTiqueteOrdenServicio(intIdEmpresa, intIdSucursal, false, false);
                string strRespuesta = "";
                if (listadoTiqueteOrdenServicio.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoTiqueteOrdenServicio);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("cambiarestadoaimpresotiqueteordenservicio")]
        public void CambiarEstadoAImpresoTiqueteOrdenServicio(int idtiquete)
        {
            try
            {
                _servicioFacturacion.ActualizarEstadoTiqueteOrdenServicio(idtiquete, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("descargaractualizacion")]
        public Stream DescargarActualizacion()
        {
            try
            {
                string strVersion = _servicioMantenimiento.ObtenerUltimaVersionApp().Replace('.', '-');
                string downloadFilePath = Path.Combine(_environment.ContentRootPath, "appupdates/" + strVersion + "/puntoventaJLC.msi");
                FileStream content = System.IO.File.Open(downloadFilePath, FileMode.Open);
                return System.IO.File.OpenRead(downloadFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoempresasadministrador")]
        public string ObtenerListadoEmpresasAdministrador()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresaAdministrador = _servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                string strRespuesta = "";
                if (listadoEmpresaAdministrador.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresaAdministrador);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoempresas")]
        public string ObtenerListadoEmpresasPorTerminal(string dispositivo)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresaPorDispositivo = _servicioMantenimiento.ObtenerListadoEmpresasPorTerminal(dispositivo);
                string strRespuesta = "";
                if (listadoEmpresaPorDispositivo.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresaPorDispositivo);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("validarcredenciales")]
        public string ValidarCredenciales(string usuario, string clave, int idempresa, string dispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                empresa = _servicioMantenimiento.ValidarCredenciales(usuario, strClaveFormateada, idempresa, dispositivo);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = JsonConvert.SerializeObject(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("validarcredencialesweb")]
        public string ValidarCredencialesWeb(string usuario, string clave, string identificacion)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                Empresa empresa = _servicioMantenimiento.ValidarCredenciales(usuario, strClaveFormateada, identificacion);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = JsonConvert.SerializeObject(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoterminalesdisponibles")]
        public string ObtenerListadoTerminalesDisponibles(string usuario, string clave, string id, int tipodispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                IList<EquipoRegistrado> listadoSucursales = (List<EquipoRegistrado>)_servicioMantenimiento.ObtenerListadoTerminalesDisponibles(usuario, strClaveFormateada, id, tipodispositivo);
                string strRespuesta = "";
                if (listadoSucursales.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoSucursales);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("registrarterminal")]
        public void RegistrarTerminal(string usuario, string clave, string id, int sucursal, int terminal, int tipodispositivo, string dispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                _servicioMantenimiento.RegistrarTerminal(usuario, strClaveFormateada, id, sucursal, terminal, tipodispositivo, dispositivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ejecutar")]
        public void Ejecutar([FromBody]string strDatos)
        {
                JObject datosJO = JObject.Parse(strDatos);
                JObject parametrosJO = null;
                string strNombreMetodo;
                string strEntidad = null;
                string strFormatoReporte = "PDF";
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
                case "ActualizarCertificadoEmpresa":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    string strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                    _servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
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
                case "EnviarDocumentoElectronicoPendiente":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    _servicioFacturacion.EnviarDocumentoElectronicoPendiente(intIdLlave1, configuracionGeneral);
                    break;
                case "EnviarNotificacionDocumentoElectronico":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdLlave1, strCorreoReceptor, _servicioEnvioCorreo, configuracionGeneral.CorreoNotificacionErrores, bytLogo);
                    break;
                case "GenerarNotificacionFactura":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.GenerarNotificacionFactura(intIdLlave1, _servicioEnvioCorreo, bytLogo);
                    break;
                case "GenerarNotificacionProforma":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                    strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                    bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                    _servicioFacturacion.GenerarNotificacionProforma(intIdLlave1, strCorreoReceptor, _servicioEnvioCorreo, bytLogo);
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
                    strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                    byte[] bytPlantillaReporte;
                    switch (strNombreReporte)
                    {
                        case "Ventas en general":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptVentas.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteVentasGenerales(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Ventas anuladas":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptVentas.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteVentasAnuladas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Resumen de movimientos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptResumenMovimientos.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteResumenMovimientos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Detalle de ingresos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptDetalleIngresos.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteDetalleIngresos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Detalle de egresos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptDetalleEgresos.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteDetalleEgresos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Documentos electrónicos emitidos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptComprobanteElectronico.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteDocumentosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Documentos electrónicos recibidos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptComprobanteElectronico.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteDocumentosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        case "Resumen de comprobantes electrónicos":
                            strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptResumenComprobanteElectronico.rdlc");
                            bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                            _servicioReportes.EnviarReporteResumenMovimientosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, bytPlantillaReporte, _servicioEnvioCorreo);
                            break;
                        default:
                            throw new Exception("El método solicitado: '" + strNombreReporte + "' no ha sido implementado, contacte con su proveedor");
                    }
                    break;
                default:
                    throw new Exception("El método solicitado no ha sido implementado: " + strNombreMetodo);
            }
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
            switch (strNombreMetodo)
            {
                case "GuardarDatosCierreCaja":
                    CierreCaja cierre = JsonConvert.DeserializeObject<CierreCaja>(strEntidad);
                    string strIdCierre = _servicioFlujoCaja.GuardarDatosCierreCaja(cierre);
                    strRespuesta = JsonConvert.SerializeObject(strIdCierre);
                    break;
                case "ObtenerTipoCambioDolar":
                    if (decTipoCambioDolar == 0)
                        decTipoCambioDolar = _servicioMantenimiento.ObtenerTipoCambioVenta(configuracionGeneral.ConsultaIndicadoresEconomicosURL, configuracionGeneral.OperacionSoap, DateTime.Now);
                    strRespuesta = decTipoCambioDolar.ToString();
                    break;
                case "ValidarUsuarioHacienda":
                    strCodigo = parametrosJO.Property("CodigoUsuario").Value.ToString();
                    strClave = parametrosJO.Property("Clave").Value.ToString();
                    bool bolValido = _servicioMantenimiento.ValidarUsuarioHacienda(strCodigo, strClave, configuracionGeneral);
                    strRespuesta = JsonConvert.SerializeObject(bolValido);
                    break;
                case "ObtenerListadoTipodePrecio":
                    IList<LlaveDescripcion> listadoTipodePrecio = _servicioMantenimiento.ObtenerListadoTipodePrecio();
                    if (listadoTipodePrecio.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipodePrecio);
                    break;
                case "ObtenerListadoTipoProducto":
                    strCodigo = parametrosJO.Property("CodigoUsuario").Value.ToString();
                    IList<LlaveDescripcion> listadoTipoProducto = _servicioMantenimiento.ObtenerListadoTipoProducto(strCodigo);
                    if (listadoTipoProducto.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoProducto);
                    break;
                case "ObtenerListadoTipoExoneracion":
                    IList<LlaveDescripcion> listadoTipoExoneracion = _servicioMantenimiento.ObtenerListadoTipoExoneracion();
                    if (listadoTipoExoneracion.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoExoneracion);
                    break;
                case "ObtenerListadoTipoImpuesto":
                    IList<LlaveDescripcionValor> listadoTipoImpuesto = _servicioMantenimiento.ObtenerListadoTipoImpuesto();
                    if (listadoTipoImpuesto.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoImpuesto);
                    break;
                case "ObtenerListadoFormaPagoCliente":
                    IList<LlaveDescripcion> listadoFormaPagoFactura = _servicioMantenimiento.ObtenerListadoFormaPagoCliente();
                    if (listadoFormaPagoFactura.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFormaPagoFactura);
                    break;
                case "ObtenerListadoFormaPagoEmpresa":
                    IList<LlaveDescripcion> listadoFormaPagoCompra = _servicioMantenimiento.ObtenerListadoFormaPagoEmpresa();
                    if (listadoFormaPagoCompra.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFormaPagoCompra);
                    break;
                case "ObtenerListadoTipoMoneda":
                    IList<LlaveDescripcion> listadoTipoMoneda = _servicioMantenimiento.ObtenerListadoTipoMoneda();
                    if (listadoTipoMoneda.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoMoneda);
                    break;
                case "ObtenerListadoCondicionVenta":
                    IList<LlaveDescripcion> listadoCondicionVenta = _servicioMantenimiento.ObtenerListadoCondicionVenta();
                    if (listadoCondicionVenta.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoCondicionVenta);
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
                case "ObtenerListadoTipoIdentificacion":
                    IList<LlaveDescripcion> listadoTipoIdentificacion = _servicioMantenimiento.ObtenerListadoTipoIdentificacion();
                    if (listadoTipoIdentificacion.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoTipoIdentificacion);
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
                    intTotalLista = _servicioBanca.ObtenerTotalListaMovimientos(intIdEmpresa, intIdSucursal, strDescripcion);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoMovimientoBanco":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    IList<EfectivoDetalle> listadoMovimientos = _servicioBanca.ObtenerListadoMovimientos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, strDescripcion);
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
                    intTotalLista = _servicioFlujoCaja.ObtenerTotalListaEgresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoEgresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    IList<EfectivoDetalle> listadoEgreso = _servicioFlujoCaja.ObtenerListadoEgresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle);
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
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    intTotalLista = _servicioFlujoCaja.ObtenerTotalListaIngresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoIngresos":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                    strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                    strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                    IList<EfectivoDetalle> listadoIngreso = _servicioFlujoCaja.ObtenerListadoIngresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle);
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
                    intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strIdentificacion = parametrosJO.Property("Identificacion") != null ? parametrosJO.Property("Identificacion").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaFacturas(intIdEmpresa, intIdSucursal, intIdLlave1, strNombre, strIdentificacion);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoFacturas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    strIdentificacion = parametrosJO.Property("Identificacion") != null ? parametrosJO.Property("Identificacion").Value.ToString() : "";
                    IList<FacturaDetalle> listadoFacturas = _servicioFacturacion.ObtenerListadoFacturas(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre, strIdentificacion);
                    if (listadoFacturas.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoFacturas);
                    break;
                case "AgregarFactura":
                    factura = JsonConvert.DeserializeObject<Factura>(strEntidad);
                    string strIdFactura = _servicioFacturacion.AgregarFactura(factura, configuracionGeneral);
                    strRespuesta = JsonConvert.SerializeObject(strIdFactura);
                    break;
                case "AgregarFacturaCompra":
                    facturaCompra = JsonConvert.DeserializeObject<FacturaCompra>(strEntidad);
                    string strIdFacturaCompra = _servicioFacturacion.AgregarFacturaCompra(facturaCompra, configuracionGeneral);
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
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaDevolucionesPorCliente(intIdEmpresa, intIdSucursal, intIdLlave1, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoDevolucionesPorCliente":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<FacturaDetalle> listadoDevolucionClientes = _servicioFacturacion.ObtenerListadoDevolucionesPorCliente(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                    if (listadoDevolucionClientes.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoDevolucionClientes);
                    break;
                case "AgregarDevolucionCliente":
                    devolucionCliente = JsonConvert.DeserializeObject<DevolucionCliente>(strEntidad);
                    string strIdDevolucionCliente = _servicioFacturacion.AgregarDevolucionCliente(devolucionCliente, configuracionGeneral);
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
                    intTotalLista = _servicioCompra.ObtenerTotalListaCompras(intIdEmpresa, intIdSucursal, intIdLlave1, strClave, strNombre);
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
                    IList<CompraDetalle> listadoCompras = (List<CompraDetalle>)_servicioCompra.ObtenerListadoCompras(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strClave, strNombre);
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
                    intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaProformas(intIdEmpresa, intIdSucursal, bolAplicado, intIdLlave1, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoProformas":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<FacturaDetalle> listadoProformas = _servicioFacturacion.ObtenerListadoProformas(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
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
                    intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaOrdenServicio(intIdEmpresa, intIdSucursal, bolAplicado, intIdLlave1, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoOrdenServicio":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<FacturaDetalle> listadoOrdenServicio = _servicioFacturacion.ObtenerListadoOrdenServicio(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
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
                    intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    intTotalLista = _servicioFacturacion.ObtenerTotalListaApartados(intIdEmpresa, intIdSucursal, bolAplicado, intIdLlave1, strNombre);
                    strRespuesta = JsonConvert.SerializeObject(intTotalLista);
                    break;
                case "ObtenerListadoApartados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<FacturaDetalle> listadoApartados = _servicioFacturacion.ObtenerListadoApartados(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
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
                    int intTotalDocumentosProcesados = _servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal, strNombre);
                    strRespuesta = intTotalDocumentosProcesados.ToString();
                    break;
                case "ObtenerListadoDocumentosElectronicosProcesados":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                    IList<DocumentoDetalle> listadoProcesados = _servicioFacturacion.ObtenerListadoDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, strNombre);
                    if (listadoProcesados.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoProcesados);
                    break;
                case "ObtenerDocumentoElectronico":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    documento = _servicioFacturacion.ObtenerDocumentoElectronico(intIdLlave1);
                    if (documento != null)
                        strRespuesta = JsonConvert.SerializeObject(documento);
                    break;
                case "ObtenerRespuestaDocumentoElectronicoEnviado":
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                    documento = _servicioFacturacion.ObtenerRespuestaDocumentoElectronicoEnviado(intIdLlave1, configuracionGeneral);
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
                case "ObtenerListadoSucursalDestino":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                    IList<LlaveDescripcion> listadoSucursalesDestino = _servicioTraslado.ObtenerListadoSucursalDestino(intIdEmpresa, intIdSucursal);
                    if (listadoSucursalesDestino.Count > 0)
                        strRespuesta = JsonConvert.SerializeObject(listadoSucursalesDestino);
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
                    int intTotalAjusteInventarios = _servicioMantenimiento.ObtenerTotalListaAjusteInventario(intIdEmpresa, intIdSucursal, intIdLlave1, strDescripcion);
                    strRespuesta = intTotalAjusteInventarios.ToString();
                    break;
                case "ObtenerListadoAjusteInventario":
                    intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                    intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                    intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                    intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                    strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                    IList<AjusteInventarioDetalle> listadoAjusteInventarios = _servicioMantenimiento.ObtenerListadoAjusteInventario(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strDescripcion);
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
                case "GenerarReporteCompra":
                    strLogoPath = Path.Combine(_environment.ContentRootPath, "PlantillaReportes/rptCompra.rdlc");
                    int intIdCompra = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                    strCodigoUsuario = parametrosJO.Property("CodigoUsuario").Value.ToString();
                    string strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                    byte[] bytPlantillaReporte = System.IO.File.ReadAllBytes(strLogoPath);
                    byte[] compraReporte = _servicioReportes.GenerarReporteCompra(intIdCompra, strCodigoUsuario, strFormatoReporte, bytPlantillaReporte);
                    if (compraReporte.Length > 0)
                        strRespuesta = JsonConvert.SerializeObject(compraReporte);
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
                    throw new Exception("El método solicitado no ha sido implementado: " + strNombreMetodo);
            }
            return strRespuesta;
        }
    }
}