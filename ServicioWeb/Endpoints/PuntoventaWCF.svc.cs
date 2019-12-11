using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.Core.Servicios;
using log4net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.CustomClasses;
using System.IO;
using System.Web;
using System.Text;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class PuntoventaWCF : IPuntoventaWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICorreoService servicioEnvioCorreo;
        private IMantenimientoService servicioMantenimiento;
        private IFacturacionService servicioFacturacion;
        private ICompraService servicioCompra;
        private IEgresoService servicioEgreso;
        private IBancaService servicioBanca;
        private IReporteService servicioReportes;
        private IContabilidadService servicioContabilidad;
        IUnityContainer unityContainer;
        private static decimal decTipoCambioDolar;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private readonly ConfiguracionGeneral configuracionGeneral = new ConfiguracionGeneral
        (
            appSettings["strConsultaIEURL"].ToString(),
            appSettings["strSoapOperation"].ToString(),
            appSettings["strServicioComprobantesURL"].ToString(),
            appSettings["strClientId"].ToString(),
            appSettings["strServicioTokenURL"].ToString(),
            appSettings["strComprobantesCallbackURL"].ToString(),
            appSettings["strCorreoNotificacionErrores"].ToString(),
            appSettings["facturaEmailFrom"].ToString()
        );
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static Empresa empresa;
        private static SucursalPorEmpresa sucursal;
        private static TerminalPorSucursal terminal;
        private static BancoAdquiriente bancoAdquiriente;
        private static Cliente cliente;
        private static Linea linea;
        private static Proveedor proveedor;
        private static Producto producto;
        private static Usuario usuario;
        private static CuentaEgreso cuentaEgreso;
        private static CuentaBanco cuentaBanco;
        private static Vendedor vendedor;
        private static Egreso egreso;
        private static Factura factura;
        private static FacturaCompra facturaCompra;
        private static DevolucionCliente devolucionCliente;
        private static Compra compra;
        private static Proforma proforma;
        private static OrdenServicio ordenServicio;
        private static Apartado apartado;
        private static DocumentoElectronico documento;
        private static CierreCaja cierre;
        private static int intIdEmpresa;
        private static int intIdSucursal;
        private static int intIdUsuario;
        private static int intIdProvincia;
        private static int intIdCanton;
        private static int intIdDistrito;
        private static int intIdDocumento;
        private static int intIdLinea;
        private static int intIdProducto;
        private static int intIdCuentaBanco;
        private static int intIdCuentaEgreso;
        private static int intIdCuentaIngreso;
        private static int intIdEgreso;
        private static int intIdIngreso;
        private static int intIdVendedor;
        private static int intIdFactura;
        private static int intIdDevolucion;
        private static int intIdCompra;
        private static int intIdProforma;
        private static int intIdOrdenServicio;
        private static int intIdApartado;
        private static int intNumeroPagina;
        private static int intFilasPorPagina;
        private static int intTotalLista;
        private static int intIdCliente;
        private static int intIdProveedor;
        private static int intIdTipoPago;
        private static int intIdBancoAdquiriente;
        bool bolIncluyeServicios;
        bool bolNulo;
        string strClave;
        string strIdentificacion;
        string strCodigo;
        string strCodigoProveedor;
        string strDescripcion;
        string strNombre;
        string strBeneficiario;
        string strDetalle;
        string strFechaInicial;
        string strFechaFinal;
        string strRespuesta = "";

        public PuntoventaWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterType<LeandroContext>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["smtpEmailHost"], appSettings["smtpEmailPort"], appSettings["smtpEmailAccount"], appSettings["smtpEmailPass"], appSettings["smtpSSLHost"]));
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<ICompraService, CompraService>();
            unityContainer.RegisterType<IEgresoService, EgresoService>();
            unityContainer.RegisterType<IBancaService, BancaService>();
            unityContainer.RegisterType<IReporteService, ReporteService>();
            unityContainer.RegisterType<IContabilidadService, ContabilidadService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioCompra = unityContainer.Resolve<ICompraService>();
            servicioEgreso = unityContainer.Resolve<IEgresoService>();
            servicioBanca = unityContainer.Resolve<IBancaService>();
            servicioReportes = unityContainer.Resolve<IReporteService>();
            servicioContabilidad = unityContainer.Resolve<IContabilidadService>();
            try
            {
                decTipoCambioDolar = ComprobanteElectronicoService.ObtenerTipoCambioVenta(configuracionGeneral.ConsultaIndicadoresEconomicosURL, configuracionGeneral.OperacionSoap, DateTime.Now, unityContainer);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
            try
            {
                string strPath = HttpContext.Current.Server.MapPath("~");
                string[] directoryEntries = Directory.GetFileSystemEntries(strPath, "errorlog.txt??-??-????");
                foreach (string str in directoryEntries)
                {
                    byte[] bytes = File.ReadAllBytes(str);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = str,
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(configuracionGeneral.CorreoCuentaFacturacion, new string[] { configuracionGeneral.CorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
                    }
                    File.Delete(str);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerUltimaVersionApp()
        {
            try
            {
                return servicioMantenimiento.ObtenerUltimaVersionApp();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public Stream DescargarActualizacion()
        {
            try
            {
                string strVersion = servicioMantenimiento.ObtenerUltimaVersionApp().Replace('.', '-');
                string downloadFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "appupdates/" + strVersion + "/puntoventaJLC.msi");
                FileStream content = File.Open(downloadFilePath, FileMode.Open);
                HttpContext.Current.Response.Headers.Add("Content-Length", content.Length.ToString());
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                return content;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoEmpresasAdministrador()
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                IList<LlaveDescripcion> listadoEmpresaAdministrador = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                string strRespuesta = "";
                if (listadoEmpresaAdministrador.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresaAdministrador);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoEmpresasPorTerminal(string strDispositivoId)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                IList<LlaveDescripcion> listadoEmpresaPorDispositivo = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoEmpresasPorTerminal(strDispositivoId);
                string strRespuesta = "";
                if (listadoEmpresaPorDispositivo.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresaPorDispositivo);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                string strClaveFormateada = strClave.Replace(" ", "+");
                empresa = servicioMantenimiento.ValidarCredenciales(strUsuario, strClaveFormateada, intIdEmpresa, strValorRegistro);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = serializer.Serialize(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                string strClaveFormateada = strClave.Replace(" ", "+");
                IList<EquipoRegistrado> listadoSucursales = (List<EquipoRegistrado>)servicioMantenimiento.ObtenerListadoTerminalesDisponibles(strUsuario, strClaveFormateada, strIdentificacion, intTipoDispositivo);
                string strRespuesta = "";
                if (listadoSucursales.Count > 0)
                    strRespuesta = serializer.Serialize(listadoSucursales);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
}

        public void RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivo)
        {
            try
            {
                string strClaveFormateada = strClave.Replace(" ", "+");
                servicioMantenimiento.RegistrarTerminal(strUsuario, strClaveFormateada, strIdentificacion, intIdSucursal, intIdTerminal, intTipoDispositivo, strDispositivo);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void Ejecutar(string strDatos)
        {
            try
            {
                JObject datosJO = JObject.Parse(strDatos);
                JObject parametrosJO = null;
                string strNombreMetodo;
                string strEntidad = null;
                string strFormatoReporte = "PDF";
                if (datosJO.Property("NombreMetodo") != null)
                {
                    strNombreMetodo = datosJO.Property("NombreMetodo").Value.ToString();
                }
                else
                {
                    throw new WebFaultException<string>("El mensaje no contiene la información suficiente para ser procesado.", HttpStatusCode.InternalServerError);
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
                    throw new WebFaultException<string>("El mensaje no contiene la información suficiente para ser procesado.", HttpStatusCode.InternalServerError);
                }
                switch (strNombreMetodo)
                {
                    case "GuardarDatosCierreCaja":
                        CierreCaja cierre = serializer.Deserialize<CierreCaja>(strEntidad);
                        servicioContabilidad.GuardarDatosCierreCaja(cierre);
                        break;
                    case "AbortarCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioContabilidad.AbortarCierreCaja(intIdEmpresa);
                        break;
                    case "ActualizarEmpresa":
                        empresa = serializer.Deserialize<Empresa>(strEntidad);
                        servicioMantenimiento.ActualizarEmpresa(empresa);
                        break;
                    case "ActualizarLogoEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strLogotipo = parametrosJO.Property("Logotipo").Value.ToString();
                        servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
                        break;
                    case "RemoverLogoEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, "");
                        break;
                    case "ActualizarCertificadoEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                        servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
                        break;
                    case "ActualizarSucursalPorEmpresa":
                        sucursal = serializer.Deserialize<SucursalPorEmpresa>(strEntidad);
                        servicioMantenimiento.ActualizarSucursalPorEmpresa(sucursal);
                        break;
                    case "ActualizarTerminalPorSucursal":
                        terminal = serializer.Deserialize<TerminalPorSucursal>(strEntidad);
                        servicioMantenimiento.ActualizarTerminalPorSucursal(terminal);
                        break;
                    case "AgregarBancoAdquiriente":
                        bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(strEntidad);
                        servicioMantenimiento.AgregarBancoAdquiriente(bancoAdquiriente);
                        break;
                    case "ActualizarBancoAdquiriente":
                        bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(strEntidad);
                        servicioMantenimiento.ActualizarBancoAdquiriente(bancoAdquiriente);
                        break;
                    case "EliminarBancoAdquiriente":
                        intIdBancoAdquiriente = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        servicioMantenimiento.EliminarBancoAdquiriente(intIdBancoAdquiriente);
                        break;
                    case "AgregarCliente":
                        cliente = serializer.Deserialize<Cliente>(strEntidad);
                        servicioFacturacion.AgregarCliente(cliente);
                        break;
                    case "ActualizarCliente":
                        cliente = serializer.Deserialize<Cliente>(strEntidad);
                        servicioFacturacion.ActualizarCliente(cliente);
                        break;
                    case "EliminarCliente":
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        servicioFacturacion.EliminarCliente(intIdCliente);
                        break;
                    case "AgregarLinea":
                        linea = serializer.Deserialize<Linea>(strEntidad);
                        servicioMantenimiento.AgregarLinea(linea);
                        break;
                    case "ActualizarLinea":
                        linea = serializer.Deserialize<Linea>(strEntidad);
                        servicioMantenimiento.ActualizarLinea(linea);
                        break;
                    case "EliminarLinea":
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        servicioMantenimiento.EliminarLinea(intIdLinea);
                        break;
                    case "AgregarProveedor":
                        proveedor = serializer.Deserialize<Proveedor>(strEntidad);
                        servicioCompra.AgregarProveedor(proveedor);
                        break;
                    case "ActualizarProveedor":
                        proveedor = serializer.Deserialize<Proveedor>(strEntidad);
                        servicioCompra.ActualizarProveedor(proveedor);
                        break;
                    case "EliminarProveedor":
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        servicioCompra.EliminarProveedor(intIdProveedor);
                        break;
                    case "AgregarProducto":
                        producto = serializer.Deserialize<Producto>(strEntidad);
                        servicioMantenimiento.AgregarProducto(producto);
                        break;
                    case "ActualizarProducto":
                        producto = serializer.Deserialize<Producto>(strEntidad);
                        servicioMantenimiento.ActualizarProducto(producto);
                        break;
                    case "EliminarProducto":
                        intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        servicioMantenimiento.EliminarProducto(intIdProducto);
                        break;
                    case "AgregarUsuario":
                        usuario = serializer.Deserialize<Usuario>(strEntidad);
                        servicioMantenimiento.AgregarUsuario(usuario);
                        break;
                    case "ActualizarUsuario":
                        usuario = serializer.Deserialize<Usuario>(strEntidad);
                        servicioMantenimiento.ActualizarUsuario(usuario);
                        break;
                    case "EliminarUsuario":
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioMantenimiento.EliminarUsuario(intIdUsuario);
                        break;
                    case "AgregarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(strEntidad);
                        servicioEgreso.AgregarCuentaEgreso(cuentaEgreso);
                        break;
                    case "ActualizarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(strEntidad);
                        servicioEgreso.ActualizarCuentaEgreso(cuentaEgreso);
                        break;
                    case "EliminarCuentaEgreso":
                        intIdCuentaEgreso = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        servicioEgreso.EliminarCuentaEgreso(intIdCuentaEgreso);
                        break;
                    case "AgregarCuentaBanco":
                        cuentaBanco = serializer.Deserialize<CuentaBanco>(strEntidad);
                        servicioBanca.AgregarCuentaBanco(cuentaBanco);
                        break;
                    case "ActualizarCuentaBanco":
                        cuentaBanco = serializer.Deserialize<CuentaBanco>(strEntidad);
                        servicioBanca.ActualizarCuentaBanco(cuentaBanco);
                        break;
                    case "EliminarCuentaBanco":
                        intIdCuentaBanco = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        servicioBanca.EliminarCuentaBanco(intIdCuentaBanco);
                        break;
                    case "AgregarVendedor":
                        vendedor = serializer.Deserialize<Vendedor>(strEntidad);
                        servicioMantenimiento.AgregarVendedor(vendedor);
                        break;
                    case "ActualizarVendedor":
                        vendedor = serializer.Deserialize<Vendedor>(strEntidad);
                        servicioMantenimiento.ActualizarVendedor(vendedor);
                        break;
                    case "EliminarVendedor":
                        intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        servicioMantenimiento.EliminarVendedor(intIdVendedor);
                        break;
                    case "AnularEgreso":
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioEgreso.AnularEgreso(intIdEgreso, intIdUsuario);
                        break;
                    case "ActualizarEgreso":
                        egreso = serializer.Deserialize<Egreso>(strEntidad);
                        servicioEgreso.ActualizarEgreso(egreso);
                        break;
                    case "AnularFactura":
                        intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularFactura(intIdFactura, intIdUsuario, configuracionGeneral);
                        break;
                    case "AnularDevolucionCliente":
                        intIdDevolucion = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularDevolucionCliente(intIdDevolucion, intIdUsuario, configuracionGeneral);
                        break;
                    case "AnularCompra":
                        intIdCompra = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioCompra.AnularCompra(intIdCompra, intIdUsuario);
                        break;
                    case "AnularProforma":
                        intIdProforma = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularProforma(intIdProforma, intIdUsuario);
                        break;
                    case "ActualizarProforma":
                        proforma = serializer.Deserialize<Proforma>(strEntidad);
                        servicioFacturacion.ActualizarProforma(proforma);
                        break;
                    case "AnularApartado":
                        intIdApartado = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularApartado(intIdApartado, intIdUsuario);
                        break;
                    case "AnularOrdenServicio":
                        intIdOrdenServicio = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularOrdenServicio(intIdOrdenServicio, intIdUsuario);
                        break;
                    case "ActualizarOrdenServicio":
                        ordenServicio = serializer.Deserialize<OrdenServicio>(strEntidad);
                        servicioFacturacion.ActualizarOrdenServicio(ordenServicio);
                        break;
                    case "EnviarDocumentoElectronicoPendiente":
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarDocumentoElectronicoPendiente(intIdDocumento, configuracionGeneral);
                        break;
                    case "EnviarNotificacionDocumentoElectronico":
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        string strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                        servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdDocumento, strCorreoReceptor, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion, configuracionGeneral.CorreoNotificacionErrores);
                        break;
                    case "EnviarReporteVentasGenerales":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteVentasGenerales(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteVentasAnuladas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteVentasAnuladas(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteResumenMovimientos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteResumenMovimientos(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteDetalleEgresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteDetalleEgresos(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteFacturasEmitidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteFacturasEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteNotasCreditoEmitidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteNotasCreditoEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteFacturasRecibidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteFacturasRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteNotasCreditoRecibidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteNotasCreditoRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    case "EnviarReporteResumenMovimientosElectronicos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteResumenMovimientosElectronicos(intIdEmpresa, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo, configuracionGeneral.CorreoCuentaFacturacion);
                        break;
                    default:
                        throw new Exception("El método solicitado no ha sido implementado: " + strNombreMetodo);
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string EjecutarConsulta(string strDatos)
        {
            try
            {
                JObject datosJO = JObject.Parse(strDatos);
                JObject parametrosJO = null;
                string strNombreMetodo;
                string strEntidad = "";
                if (datosJO.Property("NombreMetodo") != null)
                    strNombreMetodo = datosJO.Property("NombreMetodo").Value.ToString();
                else
                    throw new WebFaultException<string>("El mensaje no contiene la información suficiente para ser procesado.", HttpStatusCode.InternalServerError);
                if (datosJO.Property("Entidad") != null)
                    strEntidad = datosJO.Property("Entidad").Value.ToString();
                else if (datosJO.Property("Parametros") != null)
                    parametrosJO = JObject.Parse(datosJO.Property("Parametros").Value.ToString());
                switch (strNombreMetodo)
                {
                    case "ObtenerTipoCambioDolar":
                        strRespuesta = decTipoCambioDolar.ToString();
                        break;
                    case "ObtenerListadoTipodePrecio":
                        IList<LlaveDescripcion> listadoTipodePrecio = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipodePrecio();
                        if (listadoTipodePrecio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipodePrecio);
                        break;
                    case "ObtenerListadoTipoProducto":
                        IList<LlaveDescripcion> listadoTipoProducto = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoProducto();
                        if (listadoTipoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoProducto);
                        break;
                    case "ObtenerListadoTipoExoneracion":
                        IList<LlaveDescripcion> listadoTipoExoneracion = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoExoneracion();
                        if (listadoTipoExoneracion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoExoneracion);
                        break;
                    case "ObtenerListadoTipoImpuesto":
                        IList<LlaveDescripcion> listadoTipoImpuesto = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoImpuesto();
                        if (listadoTipoImpuesto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoImpuesto);
                        break;
                    case "ObtenerListadoFormaPagoEgreso":
                        IList<LlaveDescripcion> listadoFormaPagoEgreso = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoFormaPagoEgreso();
                        if (listadoFormaPagoEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoEgreso);
                        break;
                    case "ObtenerListadoFormaPagoFactura":
                        IList<LlaveDescripcion> listadoFormaPagoFactura = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoFormaPagoFactura();
                        if (listadoFormaPagoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoFactura);
                        break;
                    case "ObtenerListadoFormaPagoCompra":
                        IList<LlaveDescripcion> listadoFormaPagoCompra = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoFormaPagoCompra();
                        if (listadoFormaPagoCompra.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoCompra);
                        break;
                    case "ObtenerListadoTipoMoneda":
                        IList<LlaveDescripcion> listadoTipoMoneda = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoMoneda();
                        if (listadoTipoMoneda.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoMoneda);
                        break;
                    case "ObtenerListadoCondicionVenta":
                        IList<LlaveDescripcion> listadoCondicionVenta = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoCondicionVenta();
                        if (listadoCondicionVenta.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionVenta);
                        break;
                    case "ObtenerListadoRolesPorEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<LlaveDescripcion> listadoRoles = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoRolePorEmpresa(intIdEmpresa);
                        if (listadoRoles.Count > 0)
                            strRespuesta = serializer.Serialize(listadoRoles);
                        break;
                    case "ObtenerListadoEmpresa":
                        IList<LlaveDescripcion> listadoEmpresa = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoEmpresa();
                        if (listadoEmpresa.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEmpresa);
                        break;
                    case "ObtenerListadoCondicionVentaYFormaPagoFactura":
                        IList<LlaveDescripcion> listadoCondicionesyFormaPagoFactura = (List<LlaveDescripcion>)servicioReportes.ObtenerListadoCondicionVentaYFormaPagoFactura();
                        if (listadoCondicionesyFormaPagoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionesyFormaPagoFactura);
                        break;
                    case "ObtenerListadoCondicionVentaYFormaPagoCompra":
                        IList<LlaveDescripcion> listadoCondicionesyFormaPagoCompra = (List<LlaveDescripcion>)servicioReportes.ObtenerListadoCondicionVentaYFormaPagoCompra();
                        if (listadoCondicionesyFormaPagoCompra.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionesyFormaPagoCompra);
                        break;
                    case "ObtenerReporteVentasPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                        intIdBancoAdquiriente = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        IList<ReporteVentas> listadoReporteVentas = servicioReportes.ObtenerReporteVentasPorCliente(intIdEmpresa, strFechaInicial, strFechaFinal, intIdCliente, bolNulo, intIdTipoPago, intIdBancoAdquiriente);
                        if (listadoReporteVentas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentas);
                        break;
                    case "ObtenerReporteVentasPorVendedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        int intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        IList<ReporteVentasPorVendedor> listadoReporteVentasPorVendedor = servicioReportes.ObtenerReporteVentasPorVendedor(intIdEmpresa, strFechaInicial, strFechaFinal, intIdVendedor);
                        if (listadoReporteVentasPorVendedor.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorVendedor);
                        break;
                    case "ObtenerReporteComprasPorProveedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                        intIdBancoAdquiriente = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        IList<ReporteCompras> listadoReporteCompras = servicioReportes.ObtenerReporteComprasPorProveedor(intIdEmpresa, strFechaInicial, strFechaFinal, intIdProveedor, bolNulo, intIdTipoPago);
                        if (listadoReporteCompras.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCompras);
                        break;
                    case "ObtenerReporteCuentasPorCobrarClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        IList<ReporteCuentasPorCobrar> listadoReporteCuentasPorCobrar = servicioReportes.ObtenerReporteCuentasPorCobrarClientes(intIdEmpresa, strFechaInicial, strFechaFinal, intIdCliente);
                        if (listadoReporteCuentasPorCobrar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCuentasPorCobrar);
                        break;
                    case "ObtenerReporteCuentasPorPagarProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        IList<ReporteCuentasPorPagar> listadoReporteCuentasPorPagar = servicioReportes.ObtenerReporteCuentasPorPagarProveedores(intIdEmpresa, strFechaInicial, strFechaFinal, intIdProveedor);
                        if (listadoReporteCuentasPorPagar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCuentasPorPagar);
                        break;
                    case "ObtenerReporteMovimientosCxCClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        IList<ReporteMovimientosCxC> listadoReporteMovimientosCxC = servicioReportes.ObtenerReporteMovimientosCxCClientes(intIdEmpresa, strFechaInicial, strFechaFinal, intIdCliente);
                        if (listadoReporteMovimientosCxC.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosCxC);
                        break;
                    case "ObtenerReporteMovimientosCxPProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        IList<ReporteMovimientosCxP> listadoReporteMovimientosCxP = servicioReportes.ObtenerReporteMovimientosCxPProveedores(intIdEmpresa, strFechaInicial, strFechaFinal, intIdProveedor);
                        if (listadoReporteMovimientosCxP.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosCxP);
                        break;
                    case "ObtenerReporteMovimientosBanco":
                        int intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteMovimientosBanco> listadoReporteMovimientosBanco = servicioReportes.ObtenerReporteMovimientosBanco(intIdCuenta, strFechaInicial, strFechaFinal);
                        if (listadoReporteMovimientosBanco.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosBanco);
                        break;
                    case "ObtenerReporteEstadoResultados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteEstadoResultados> listadoReporteEstadoResultados = servicioReportes.ObtenerReporteEstadoResultados(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoReporteEstadoResultados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteEstadoResultados);
                        break;
                    case "ObtenerReporteDetalleEgreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdCuentaEgreso = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDetalleEgreso> listadoReporteDetalleEgreso = servicioReportes.ObtenerReporteDetalleEgreso(intIdEmpresa, intIdCuentaEgreso, strFechaInicial, strFechaFinal);
                        if (listadoReporteDetalleEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteDetalleEgreso);
                        break;
                    case "ObtenerReporteDetalleIngreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCuentaIngreso = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                        IList<ReporteDetalleIngreso> listadoReporteDetalleIngreso = servicioReportes.ObtenerReporteDetalleIngreso(intIdEmpresa, intIdCuentaIngreso, strFechaInicial, strFechaFinal);
                        if (listadoReporteDetalleIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteDetalleIngreso);
                        break;
                    case "ObtenerReporteVentasPorLineaResumen":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteVentasPorLineaResumen> listadoReporteVentasPorLineaResumen = servicioReportes.ObtenerReporteVentasPorLineaResumen(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoReporteVentasPorLineaResumen.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorLineaResumen);
                        break;
                    case "ObtenerReporteVentasPorLineaDetalle":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteVentasPorLineaDetalle> listadoReporteVentasPorLineaDetalle = servicioReportes.ObtenerReporteVentasPorLineaDetalle(intIdEmpresa, intIdLinea, strFechaInicial, strFechaFinal);
                        if (listadoReporteVentasPorLineaDetalle.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorLineaDetalle);
                        break;
                    case "ObtenerReporteCierreDeCaja":
                        int intIdCierre = int.Parse(parametrosJO.Property("IdCierre").Value.ToString());
                        IList<ReporteCierreDeCaja> listadoReporteCierreDeCaja = servicioReportes.ObtenerReporteCierreDeCaja(intIdCierre);
                        if (listadoReporteCierreDeCaja.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCierreDeCaja);
                        break;
                    case "ObtenerReporteInventario":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";;
                        IList<ReporteInventario> listadoReporteInventario = servicioReportes.ObtenerReporteInventario(intIdEmpresa, intIdSucursal, intIdLinea, strCodigo, strDescripcion);
                        if (listadoReporteInventario.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteInventario);
                        break;
                    case "ObtenerReporteMovimientosContables":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteMovimientosContables> listadoReporteMovimientosContables = servicioReportes.ObtenerReporteMovimientosContables(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoReporteMovimientosContables.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosContables);
                        break;
                    case "ObtenerReporteBalanceComprobacion":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intMes = int.Parse(parametrosJO.Property("Mes").Value.ToString());
                        int intAnnio = int.Parse(parametrosJO.Property("Annio").Value.ToString());
                        IList<ReporteBalanceComprobacion> listadoReporteBalanceComprobacion = servicioReportes.ObtenerReporteBalanceComprobacion(intIdEmpresa, intMes, intAnnio);
                        if (listadoReporteBalanceComprobacion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteBalanceComprobacion);
                        break;
                    case "ObtenerReportePerdidasyGanancias":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<ReportePerdidasyGanancias> listadoReportePerdidasyGanancias = servicioReportes.ObtenerReportePerdidasyGanancias(intIdEmpresa);
                        if (listadoReportePerdidasyGanancias.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReportePerdidasyGanancias);
                        break;
                    case "ObtenerReporteDetalleMovimientosCuentasDeBalance":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intIdCuentaGrupo = int.Parse(parametrosJO.Property("IdCuentaGrupo").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDetalleMovimientosCuentasDeBalance> listadoReporteDetalleMovimientosCuentasDeBalance = servicioReportes.ObtenerReporteDetalleMovimientosCuentasDeBalance(intIdEmpresa, intIdCuentaGrupo, strFechaInicial, strFechaFinal);
                        if (listadoReporteDetalleMovimientosCuentasDeBalance.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteDetalleMovimientosCuentasDeBalance);
                        break;
                    case "ObtenerReporteEgreso":
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        IList<ReporteEgreso> listadoReporteEgreso = servicioReportes.ObtenerReporteEgreso(intIdEgreso);
                        if (listadoReporteEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteEgreso);
                        break;
                    case "ObtenerReporteIngreso":
                        intIdIngreso = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        IList<ReporteIngreso> listadoReporteIngreso = servicioReportes.ObtenerReporteIngreso(intIdIngreso);
                        if (listadoReporteIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteIngreso);
                        break;
                    case "ObtenerReporteFacturasElectronicasEmitidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoFacturasEmitidas = servicioReportes.ObtenerReporteFacturasElectronicasEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoFacturasEmitidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFacturasEmitidas);
                        break;
                    case "ObtenerReporteNotasCreditoElectronicasEmitidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoNotasCreditoEmitidas = servicioReportes.ObtenerReporteNotasCreditoElectronicasEmitidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoNotasCreditoEmitidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoNotasCreditoEmitidas);
                        break;
                    case "ObtenerReporteFacturasElectronicasRecibidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoFacturasRecibidas = servicioReportes.ObtenerReporteFacturasElectronicasRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoFacturasRecibidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFacturasRecibidas);
                        break;
                    case "ObtenerReporteNotasCreditoElectronicasRecibidas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoNotasCreditoRecibidas = servicioReportes.ObtenerReporteNotasCreditoElectronicasRecibidas(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoNotasCreditoRecibidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoNotasCreditoRecibidas);
                        break;
                    case "ObtenerReporteResumenDocumentosElectronicos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteResumenMovimiento> listadoReporteResumenDocumentosElectronicos = servicioReportes.ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, strFechaInicial, strFechaFinal);
                        if (listadoReporteResumenDocumentosElectronicos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteResumenDocumentosElectronicos);
                        break;
                    case "GenerarDatosCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strFechaCierre = parametrosJO.Property("FechaCierre").Value.ToString();
                        cierre = servicioContabilidad.GenerarDatosCierreCaja(intIdEmpresa, strFechaCierre);
                        if (cierre != null)
                            strRespuesta = serializer.Serialize(cierre);
                        break;
                    case "ObtenerEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        empresa = servicioMantenimiento.ObtenerEmpresa(intIdEmpresa);
                        if (empresa != null)
                            strRespuesta = serializer.Serialize(empresa);
                        break;
                    case "ObtenerLogotipoEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string logotipo = servicioMantenimiento.ObtenerLogotipoEmpresa(intIdEmpresa);
                        if (logotipo != null)
                            strRespuesta = serializer.Serialize(logotipo);
                        break;
                    case "ObtenerListadoSucursales":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<LlaveDescripcion> listadoSucursales = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoSucursales(intIdEmpresa);
                        if (listadoSucursales.Count > 0)
                            strRespuesta = serializer.Serialize(listadoSucursales);
                        break;
                    case "ObtenerSucursalPorEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        SucursalPorEmpresa sucursal = servicioMantenimiento.ObtenerSucursalPorEmpresa(intIdEmpresa, intIdSucursal);
                        if (sucursal != null)
                            strRespuesta = serializer.Serialize(sucursal);
                        break;
                    case "ObtenerTerminalPorSucursal":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        int intIdTerminal = int.Parse(parametrosJO.Property("IdTerminal").Value.ToString());
                        TerminalPorSucursal terminal = servicioMantenimiento.ObtenerTerminalPorSucursal(intIdEmpresa, intIdSucursal, intIdTerminal);
                        if (terminal != null)
                            strRespuesta = serializer.Serialize(terminal);
                        break;
                    case "ObtenerListadoTipoIdentificacion":
                        IList<LlaveDescripcion> listadoTipoIdentificacion = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoIdentificacion();
                        if (listadoTipoIdentificacion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoIdentificacion);
                        break;
                    case "ObtenerListadoCatalogoReportes":
                        IList<LlaveDescripcion> listadoReportes = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoCatalogoReportes();
                        if (listadoReportes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReportes);
                        break;
                    case "ObtenerListadoProvincias":
                        IList<LlaveDescripcion> listadoProvincias = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoProvincias();
                        if (listadoProvincias.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProvincias);
                        break;
                    case "ObtenerListadoCantones":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        IList<LlaveDescripcion> listadoCantones = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoCantones(intIdProvincia);
                        if (listadoCantones.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCantones);
                        break;
                    case "ObtenerListadoDistritos":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        IList<LlaveDescripcion> listadoDistritos = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoDistritos(intIdProvincia, intIdCanton);
                        if (listadoDistritos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDistritos);
                        break;
                    case "ObtenerListadoBarrios":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                        IList<LlaveDescripcion> listadoBarrios = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                        if (listadoBarrios.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBarrios);
                        break;
                    case "ObtenerParametroImpuesto":
                        int intIdImpuesto = int.Parse(parametrosJO.Property("IdImpuesto").Value.ToString());
                        ParametroImpuesto parametroImpuesto = servicioMantenimiento.ObtenerParametroImpuesto(intIdImpuesto);
                        if (parametroImpuesto != null)
                            strRespuesta = serializer.Serialize(parametroImpuesto);
                        break;
                    case "ObtenerListadoBancoAdquiriente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoBancoAdquiriente = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoBancoAdquiriente(intIdEmpresa, strDescripcion);
                        if (listadoBancoAdquiriente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBancoAdquiriente);
                        break;
                    case "ObtenerBancoAdquiriente":
                        int intIdBanco = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        bancoAdquiriente = servicioMantenimiento.ObtenerBancoAdquiriente(intIdBanco);
                        if (bancoAdquiriente != null)
                            strRespuesta = serializer.Serialize(bancoAdquiriente);
                        break;
                    case "ObtenerTotalListaClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaClientes(intIdEmpresa, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoCliente = (List<LlaveDescripcion>)servicioFacturacion.ObtenerListadoClientes(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoCliente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCliente);
                        break;
                    case "ObtenerCliente":
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        cliente = servicioFacturacion.ObtenerCliente(intIdCliente);
                        if (cliente != null)
                            strRespuesta = serializer.Serialize(cliente);
                        break;
                    case "ValidaIdentificacionCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                        cliente = servicioFacturacion.ValidaIdentificacionCliente(intIdEmpresa, strIdentificacion);
                        if (cliente != null)
                            strRespuesta = serializer.Serialize(cliente);
                        break;
                    case "ObtenerListadoLineas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";;
                        IList<LlaveDescripcion> listadoLinea = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoLineas(intIdEmpresa, strDescripcion);
                        if (listadoLinea.Count > 0)
                            strRespuesta = serializer.Serialize(listadoLinea);
                        break;
                    case "ObtenerLinea":
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        linea = servicioMantenimiento.ObtenerLinea(intIdLinea);
                        if (linea != null)
                            strRespuesta = serializer.Serialize(linea);
                        break;
                    case "ObtenerTotalListaProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioCompra.ObtenerTotalListaProveedores(intIdEmpresa, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoProveedor = (List<LlaveDescripcion>)servicioCompra.ObtenerListadoProveedores(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoProveedor.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProveedor);
                        break;
                    case "ObtenerProveedor":
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        proveedor = servicioCompra.ObtenerProveedor(intIdProveedor);
                        if (proveedor != null)
                            strRespuesta = serializer.Serialize(proveedor);
                        break;
                    case "ObtenerTotalListaProductos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        intIdLinea = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        intTotalLista = servicioMantenimiento.ObtenerTotalListaProductos(intIdEmpresa, intIdSucursal, bolIncluyeServicios, intIdLinea, strCodigo, strCodigoProveedor, strDescripcion);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoProductos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        intIdLinea = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<ProductoDetalle> listadoProducto = (List<ProductoDetalle>)servicioMantenimiento.ObtenerListadoProductos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, intIdLinea, strCodigo, strCodigoProveedor, strDescripcion);
                        if (listadoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProducto);
                        break;
                    case "ObtenerProducto":
                        intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProducto(intIdProducto);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoPorCodigo":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        producto = servicioMantenimiento.ObtenerProductoPorCodigo(intIdEmpresa, strCodigo);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoPorCodigoProveedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        producto = servicioMantenimiento.ObtenerProductoPorCodigoProveedor(intIdEmpresa, strCodigo);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerTotalMovimientosPorProducto":
                        intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intTotalLista = servicioMantenimiento.ObtenerTotalMovimientosPorProducto(intIdProducto, intIdSucursal, strFechaInicial, strFechaFinal);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerMovimientosPorProducto":
                        intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<MovimientoProducto> listadoMovimientosProducto = servicioMantenimiento.ObtenerMovimientosPorProducto(intIdProducto, intIdSucursal, intNumeroPagina, intFilasPorPagina, strFechaInicial, strFechaFinal);
                        if (listadoMovimientosProducto != null)
                            strRespuesta = serializer.Serialize(listadoMovimientosProducto);
                        break;
                    case "ObtenerListadoUsuarios":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoUsuario = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoUsuarios(intIdEmpresa, strCodigo);
                        if (listadoUsuario.Count > 0)
                            strRespuesta = serializer.Serialize(listadoUsuario);
                        break;
                    case "ObtenerUsuario":
                        int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        usuario = servicioMantenimiento.ObtenerUsuario(intIdUsuario);
                        if (usuario != null)
                            strRespuesta = serializer.Serialize(usuario);
                        break;
                    case "ActualizarClaveUsuario":
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strClave = parametrosJO.Property("Clave").Value.ToString();
                        usuario = servicioMantenimiento.ActualizarClaveUsuario(intIdUsuario, strClave);
                        if (usuario != null)
                            strRespuesta = serializer.Serialize(usuario);
                        break;
                    case "ObtenerListadoCuentasEgreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";;
                        IList<LlaveDescripcion> listadoCuentaEgreso = (List<LlaveDescripcion>)servicioEgreso.ObtenerListadoCuentasEgreso(intIdEmpresa, strDescripcion);
                        if (listadoCuentaEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaEgreso);
                        break;
                    case "ObtenerCuentaEgreso":
                        intIdCuentaEgreso = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        cuentaEgreso = servicioEgreso.ObtenerCuentaEgreso(intIdCuentaEgreso);
                        if (cuentaEgreso != null)
                            strRespuesta = serializer.Serialize(cuentaEgreso);
                        break;
                    case "ObtenerListadoCuentasBanco":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";;
                        IList<LlaveDescripcion> listadoCuentaBanco = (List<LlaveDescripcion>)servicioBanca.ObtenerListadoCuentasBanco(intIdEmpresa, strDescripcion);
                        if (listadoCuentaBanco.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaBanco);
                        break;
                    case "ObtenerCuentaBanco":
                        int intIdCuentaBanco = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        cuentaBanco = servicioBanca.ObtenerCuentaBanco(intIdCuentaBanco);
                        if (cuentaBanco != null)
                            strRespuesta = serializer.Serialize(cuentaBanco);
                        break;
                    case "ObtenerListadoVendedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoVendedores = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoVendedores(intIdEmpresa, strNombre);
                        if (listadoVendedores.Count > 0)
                            strRespuesta = serializer.Serialize(listadoVendedores);
                        break;
                    case "ObtenerVendedor":
                        intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        vendedor = servicioMantenimiento.ObtenerVendedor(intIdVendedor);
                        if (vendedor != null)
                            strRespuesta = serializer.Serialize(vendedor);
                        break;
                    case "ObtenerVendedorPorDefecto":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        vendedor = servicioMantenimiento.ObtenerVendedorPorDefecto(intIdEmpresa);
                        if (vendedor != null)
                            strRespuesta = serializer.Serialize(vendedor);
                        break;
                    case "ObtenerTotalListaEgresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        intTotalLista = servicioEgreso.ObtenerTotalListaEgresos(intIdEmpresa, intIdEgreso, strBeneficiario, strDetalle);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoEgresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        IList<LlaveDescripcion> listadoEgreso = (List<LlaveDescripcion>)servicioEgreso.ObtenerListadoEgresos(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdEgreso, strBeneficiario, strDetalle);
                        if (listadoEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEgreso);
                        break;
                    case "AgregarEgreso":
                        egreso = serializer.Deserialize<Egreso>(strEntidad);
                        string strIdEgreso = servicioEgreso.AgregarEgreso(egreso);
                        strRespuesta = serializer.Serialize(strIdEgreso);
                        break;
                    case "ObtenerEgreso":
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        egreso = servicioEgreso.ObtenerEgreso(intIdEgreso);
                        if (egreso != null)
                            strRespuesta = serializer.Serialize(egreso);
                        break;
                    case "ObtenerTotalListaFacturas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdFactura = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaFacturas(intIdEmpresa, intIdFactura, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoFacturas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdFactura = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoFacturas = (List<FacturaDetalle>)servicioFacturacion.ObtenerListadoFacturas(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdFactura, strNombre);
                        if (listadoFacturas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFacturas);
                        break;
                    case "AgregarFactura":
                        factura = serializer.Deserialize<Factura>(strEntidad);
                        string strIdFactura = servicioFacturacion.AgregarFactura(factura, configuracionGeneral);
                        strRespuesta = serializer.Serialize(strIdFactura);
                        break;
                    case "AgregarFacturaCompra":
                        facturaCompra = serializer.Deserialize<FacturaCompra>(strEntidad);
                        string strIdFacturaCompra = servicioFacturacion.AgregarFacturaCompra(facturaCompra, configuracionGeneral);
                        strRespuesta = serializer.Serialize(strIdFacturaCompra);
                        break;
                    case "ObtenerFactura":
                        intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        factura = servicioFacturacion.ObtenerFactura(intIdFactura);
                        if (factura != null)
                            strRespuesta = serializer.Serialize(factura);
                        break;
                    case "ObtenerTotalListaDevolucionesPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdDevolucion = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaDevolucionesPorCliente(intIdEmpresa, intIdDevolucion, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoDevolucionesPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdDevolucion = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoDevolucionClientes = servicioFacturacion.ObtenerListadoDevolucionesPorCliente(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdDevolucion, strNombre);
                        if (listadoDevolucionClientes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDevolucionClientes);
                        break;
                    case "AgregarDevolucionCliente":
                        devolucionCliente = serializer.Deserialize<DevolucionCliente>(strEntidad);
                        string strIdDevolucionCliente = servicioFacturacion.AgregarDevolucionCliente(devolucionCliente, configuracionGeneral);
                        strRespuesta = serializer.Serialize(strIdDevolucionCliente);
                        break;
                    case "ObtenerDevolucionCliente":
                        intIdDevolucion = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                        devolucionCliente = servicioFacturacion.ObtenerDevolucionCliente(intIdDevolucion);
                        if (devolucionCliente != null)
                            strRespuesta = serializer.Serialize(devolucionCliente);
                        break;
                    case "ObtenerTotalListaCompras":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdCompra = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioCompra.ObtenerTotalListaCompras(intIdEmpresa, intIdCompra, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoCompras":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdCompra = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<CompraDetalle> listadoCompras = (List<CompraDetalle>)servicioCompra.ObtenerListadoCompras(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdCompra, strNombre);
                        if (listadoCompras.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCompras);
                        break;
                    case "AgregarCompra":
                        compra = serializer.Deserialize<Compra>(strEntidad);
                        string strIdCompra = servicioCompra.AgregarCompra(compra);
                        strRespuesta = serializer.Serialize(strIdCompra);
                        break;
                    case "ObtenerCompra":
                        intIdCompra = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                        compra = servicioCompra.ObtenerCompra(intIdCompra);
                        if (compra != null)
                            strRespuesta = serializer.Serialize(compra);
                        break;
                    case "ObtenerTotalListaProformas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdProforma = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaProformas(intIdEmpresa, intIdProforma, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoProformas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdProforma = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoProformas = (List<FacturaDetalle>)servicioFacturacion.ObtenerListadoProformas(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdProforma, strNombre);
                        if (listadoProformas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProformas);
                        break;
                    case "AgregarProforma":
                        proforma = serializer.Deserialize<Proforma>(strEntidad);
                        string strIdProforma = servicioFacturacion.AgregarProforma(proforma);
                        strRespuesta = serializer.Serialize(strIdProforma);
                        break;
                    case "ObtenerProforma":
                        intIdProforma = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                        proforma = servicioFacturacion.ObtenerProforma(intIdProforma);
                        if (proforma != null)
                            strRespuesta = serializer.Serialize(proforma);
                        break;
                    case "ObtenerTotalListaOrdenServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdOrdenServicio = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaOrdenServicio(intIdEmpresa, intIdOrdenServicio, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoOrdenServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdOrdenServicio = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoOrdenServicio = (List<FacturaDetalle>)servicioFacturacion.ObtenerListadoOrdenServicio(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdOrdenServicio, strNombre);
                        if (listadoOrdenServicio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoOrdenServicio);
                        break;
                    case "AgregarOrdenServicio":
                        ordenServicio = serializer.Deserialize<OrdenServicio>(strEntidad);
                        string strIdOrdenServicio = servicioFacturacion.AgregarOrdenServicio(ordenServicio);
                        strRespuesta = serializer.Serialize(strIdOrdenServicio);
                        break;
                    case "ObtenerOrdenServicio":
                        intIdOrdenServicio = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                        ordenServicio = servicioFacturacion.ObtenerOrdenServicio(intIdOrdenServicio);
                        if (ordenServicio != null)
                            strRespuesta = serializer.Serialize(ordenServicio);
                        break;
                    case "ObtenerTotalListaApartados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdApartado = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaApartados(intIdEmpresa, intIdApartado, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoApartados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdApartado = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoApartados = (List<FacturaDetalle>)servicioFacturacion.ObtenerListadoApartados(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdApartado, strNombre);
                        if (listadoApartados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoApartados);
                        break;
                    case "AgregarApartado":
                        apartado = serializer.Deserialize<Apartado>(strEntidad);
                        string strIdApartado = servicioFacturacion.AgregarApartado(apartado);
                        strRespuesta = serializer.Serialize(strIdApartado);
                        break;
                    case "ObtenerApartado":
                        intIdApartado = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                        apartado = servicioFacturacion.ObtenerApartado(intIdApartado);
                        if (apartado != null)
                            strRespuesta = serializer.Serialize(apartado);
                        break;
                    case "ObtenerListadoDocumentosElectronicosEnProceso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoDetalle> listadoEnProceso = (List<DocumentoDetalle>)servicioFacturacion.ObtenerListadoDocumentosElectronicosEnProceso(intIdEmpresa);
                        if (listadoEnProceso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEnProceso);
                        break;
                    case "ObtenerTotalDocumentosElectronicosProcesados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intTotalDocumentosProcesados = servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa);
                        strRespuesta = intTotalDocumentosProcesados.ToString();
                        break;
                    case "ObtenerListadoDocumentosElectronicosProcesados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        IList<DocumentoDetalle> listadoProcesados = (List<DocumentoDetalle>)servicioFacturacion.ObtenerListadoDocumentosElectronicosProcesados(intIdEmpresa, intNumeroPagina, intFilasPorPagina);
                        if (listadoProcesados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProcesados);
                        break;
                    case "ObtenerDocumentoElectronico":
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerDocumentoElectronico(intIdDocumento);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    case "ObtenerRespuestaDocumentoElectronicoEnviado":
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerRespuestaDocumentoElectronicoEnviado(intIdDocumento, configuracionGeneral);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    default:
                        throw new Exception("El método solicitado no ha sido implementado: " + strNombreMetodo);
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
