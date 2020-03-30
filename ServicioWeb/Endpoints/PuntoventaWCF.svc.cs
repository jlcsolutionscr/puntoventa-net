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

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class PuntoventaWCF : IPuntoventaWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICorreoService servicioEnvioCorreo;
        private IMantenimientoService servicioMantenimiento;
        private IFacturacionService servicioFacturacion;
        private ICompraService servicioCompra;
        private IFlujoCajaService servicioFlujoCaja;
        private IBancaService servicioBanca;
        private IReporteService servicioReportes;
        private IContabilidadService servicioContabilidad;
        private ITrasladoService servicioTraslado;
        private ICuentaPorProcesarService servicioCuentaPorProcesar;
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
            appSettings["strCorreoNotificacionErrores"].ToString()
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
        private static CuentaIngreso cuentaIngreso;
        private static CuentaBanco cuentaBanco;
        private static Vendedor vendedor;
        private static Egreso egreso;
        private static Ingreso ingreso;
        private static Factura factura;
        private static FacturaCompra facturaCompra;
        private static DevolucionCliente devolucionCliente;
        private static Compra compra;
        private static Proforma proforma;
        private static OrdenServicio ordenServicio;
        private static Apartado apartado;
        private static DocumentoElectronico documento;
        private static CierreCaja cierre;
        private static Traslado traslado;
        private static AjusteInventario ajusteInventario;
        private static CuentaPorCobrar cuentaPorCobrar;
        private static MovimientoCuentaPorCobrar movimientoCxC;
        private static CuentaPorPagar cuentaPorPagar;
        private static MovimientoCuentaPorPagar movimientoCxP;
        private static MovimientoApartado movimientoApartado;
        private static MovimientoOrdenServicio movimientoOrdenServicio;
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
        bool bolIncluyeServicios;
        bool bolFiltraActivos;
        bool bolFiltraExistencias;
        bool bolNulo;
        bool bolAplicado;
        string strClave;
        string strIdentificacion;
        string strCodigo;
        string strCodigoProveedor;
        string strDescripcion;
        string strNombre;
        string strBeneficiario;
        string strDetalle;
        string strMotivoAnulacion;
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
            unityContainer.RegisterType<IFlujoCajaService, FlujoCajaService>();
            unityContainer.RegisterType<IBancaService, BancaService>();
            unityContainer.RegisterType<IReporteService, ReporteService>();
            unityContainer.RegisterType<IContabilidadService, ContabilidadService>();
            unityContainer.RegisterType<ITrasladoService, TrasladoService>();
            unityContainer.RegisterType<ICuentaPorProcesarService, CuentaPorProcesarService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioCompra = unityContainer.Resolve<ICompraService>();
            servicioFlujoCaja = unityContainer.Resolve<IFlujoCajaService>();
            servicioBanca = unityContainer.Resolve<IBancaService>();
            servicioReportes = unityContainer.Resolve<IReporteService>();
            servicioContabilidad = unityContainer.Resolve<IContabilidadService>();
            servicioTraslado = unityContainer.Resolve<ITrasladoService>();
            servicioCuentaPorProcesar = unityContainer.Resolve<ICuentaPorProcesarService>();
            try
            {
                bool modoMantenimiento = servicioMantenimiento.EnModoMantenimiento();
                if (modoMantenimiento) throw new Exception("El sistema se encuentra en modo mantenimiento y no es posible acceder por el momento.");
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el estado del modo mantenimiento del sistema: ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
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
                        servicioEnvioCorreo.SendEmail(new string[] { configuracionGeneral.CorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
                    }
                    File.Delete(str);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al enviar los archivos historicos de errores del sistema: ", ex);
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

        public string ObtenerUltimaVersionMobileApp()
        {
            try
            {
                return "{\"Version\": \"" + servicioMantenimiento.ObtenerUltimaVersionMobileApp() + "\"}";
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
                IList<LlaveDescripcion> listadoEmpresaAdministrador = servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
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
                IList<LlaveDescripcion> listadoEmpresaPorDispositivo = servicioMantenimiento.ObtenerListadoEmpresasPorTerminal(strDispositivoId);
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
                        servicioFlujoCaja.GuardarDatosCierreCaja(cierre);
                        break;
                    case "AbortarCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        servicioFlujoCaja.AbortarCierreCaja(intIdEmpresa, intIdSucursal);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        servicioMantenimiento.EliminarBancoAdquiriente(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        servicioFacturacion.EliminarCliente(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        servicioMantenimiento.EliminarLinea(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        servicioCompra.EliminarProveedor(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        servicioMantenimiento.EliminarProducto(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioMantenimiento.EliminarUsuario(intIdLlave1);
                        break;
                    case "AgregarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(strEntidad);
                        servicioFlujoCaja.AgregarCuentaEgreso(cuentaEgreso);
                        break;
                    case "ActualizarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(strEntidad);
                        servicioFlujoCaja.ActualizarCuentaEgreso(cuentaEgreso);
                        break;
                    case "EliminarCuentaEgreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        servicioFlujoCaja.EliminarCuentaEgreso(intIdLlave1);
                        break;
                    case "AgregarCuentaIngreso":
                        cuentaIngreso = serializer.Deserialize<CuentaIngreso>(strEntidad);
                        servicioFlujoCaja.AgregarCuentaIngreso(cuentaIngreso);
                        break;
                    case "ActualizarCuentaIngreso":
                        cuentaIngreso = serializer.Deserialize<CuentaIngreso>(strEntidad);
                        servicioFlujoCaja.ActualizarCuentaIngreso(cuentaIngreso);
                        break;
                    case "EliminarCuentaIngreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                        servicioFlujoCaja.EliminarCuentaIngreso(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        servicioBanca.EliminarCuentaBanco(intIdLlave1);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        servicioMantenimiento.EliminarVendedor(intIdLlave1);
                        break;
                    case "AnularEgreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFlujoCaja.AnularEgreso(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AnularIngreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFlujoCaja.AnularIngreso(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AnularFactura":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFacturacion.AnularFactura(intIdLlave1, intIdUsuario, strMotivoAnulacion, configuracionGeneral);
                        break;
                    case "AnularDevolucionCliente":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFacturacion.AnularDevolucionCliente(intIdLlave1, intIdUsuario, strMotivoAnulacion, configuracionGeneral);
                        break;
                    case "AnularCompra":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioCompra.AnularCompra(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AnularProforma":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFacturacion.AnularProforma(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "ActualizarProforma":
                        proforma = serializer.Deserialize<Proforma>(strEntidad);
                        servicioFacturacion.ActualizarProforma(proforma);
                        break;
                    case "AnularApartado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFacturacion.AnularApartado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AnularOrdenServicio":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioFacturacion.AnularOrdenServicio(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "ActualizarOrdenServicio":
                        ordenServicio = serializer.Deserialize<OrdenServicio>(strEntidad);
                        servicioFacturacion.ActualizarOrdenServicio(ordenServicio);
                        break;
                    case "AplicarTraslado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioTraslado.AplicarTraslado(intIdLlave1, intIdUsuario);
                        break;
                    case "AnularTraslado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioTraslado.AnularTraslado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AnularAjusteInventario":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioMantenimiento.AnularAjusteInventario(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AplicarMovimientoCxC":
                        movimientoCxC = serializer.Deserialize<MovimientoCuentaPorCobrar>(strEntidad);
                        servicioCuentaPorProcesar.AplicarMovimientoCxC(movimientoCxC);
                        break;
                    case "AnularMovimientoCxC":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioCuentaPorProcesar.AnularMovimientoCxC(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AplicarMovimientoCxP":
                        movimientoCxP = serializer.Deserialize<MovimientoCuentaPorPagar>(strEntidad);
                        servicioCuentaPorProcesar.AplicarMovimientoCxP(movimientoCxP);
                        break;
                    case "AnularMovimientoCxP":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioCuentaPorProcesar.AnularMovimientoCxP(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AplicarMovimientoApartado":
                        movimientoApartado = serializer.Deserialize<MovimientoApartado>(strEntidad);
                        servicioCuentaPorProcesar.AplicarMovimientoApartado(movimientoApartado);
                        break;
                    case "AnularMovimientoApartado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioCuentaPorProcesar.AnularMovimientoApartado(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "AplicarMovimientoOrdenServicio":
                        movimientoOrdenServicio = serializer.Deserialize<MovimientoOrdenServicio>(strEntidad);
                        servicioCuentaPorProcesar.AplicarMovimientoOrdenServicio(movimientoOrdenServicio);
                        break;
                    case "AnularMovimientoOrdenServicio":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strMotivoAnulacion = parametrosJO.Property("MotivoAnulacion") != null ? parametrosJO.Property("MotivoAnulacion").Value.ToString() : "";
                        servicioCuentaPorProcesar.AnularMovimientoOrdenServicio(intIdLlave1, intIdUsuario, strMotivoAnulacion);
                        break;
                    case "GenerarMensajeReceptor":
                        strDatos = parametrosJO.Property("Datos").Value.ToString();
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTerminal").Value.ToString());
                        intIdTipoPago = int.Parse(parametrosJO.Property("IdEstado").Value.ToString());
                        bolAplicado = bool.Parse(parametrosJO.Property("IvaAcreditable").Value.ToString());
                        servicioFacturacion.GenerarMensajeReceptor(strDatos, intIdEmpresa, intIdSucursal, intIdLlave1, intIdTipoPago, bolAplicado, configuracionGeneral);
                        break;
                    case "EnviarDocumentoElectronicoPendiente":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarDocumentoElectronicoPendiente(intIdLlave1, configuracionGeneral);
                        break;
                    case "EnviarNotificacionDocumentoElectronico":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        string strCorreoReceptor = parametrosJO.Property("CorreoReceptor").Value.ToString();
                        servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdLlave1, strCorreoReceptor, servicioEnvioCorreo, configuracionGeneral.CorreoNotificacionErrores);
                        break;
                    case "ReprocesarDocumentoElectronico":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.ReprocesarDocumentoElectronico(intIdLlave1, configuracionGeneral);
                        break;
                    case "EnviarReporteVentasGenerales":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteVentasGenerales(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteVentasAnuladas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteVentasAnuladas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteResumenMovimientos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteResumenMovimientos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteDetalleEgresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteDetalleEgresos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteDocumentosEmitidos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteDocumentosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteDocumentosRecibidos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteDocumentosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
                        break;
                    case "EnviarReporteResumenMovimientosElectronicos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        strFormatoReporte = parametrosJO.Property("FormatoReporte").Value.ToString();
                        servicioReportes.EnviarReporteResumenMovimientosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, strFormatoReporte, servicioEnvioCorreo);
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
                    case "ValidarUsuarioHacienda":
                        strCodigo = parametrosJO.Property("CodigoUsuario").Value.ToString();
                        strClave = parametrosJO.Property("Clave").Value.ToString();
                        bool bolValido = servicioMantenimiento.ValidarUsuarioHacienda(strCodigo, strClave, configuracionGeneral);
                        strRespuesta = serializer.Serialize(bolValido);
                        break;
                    case "ObtenerListadoTipodePrecio":
                        IList<LlaveDescripcion> listadoTipodePrecio = servicioMantenimiento.ObtenerListadoTipodePrecio();
                        if (listadoTipodePrecio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipodePrecio);
                        break;
                    case "ObtenerListadoTipoProducto":
                        IList<LlaveDescripcion> listadoTipoProducto = servicioMantenimiento.ObtenerListadoTipoProducto();
                        if (listadoTipoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoProducto);
                        break;
                    case "ObtenerListadoTipoExoneracion":
                        IList<LlaveDescripcion> listadoTipoExoneracion = servicioMantenimiento.ObtenerListadoTipoExoneracion();
                        if (listadoTipoExoneracion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoExoneracion);
                        break;
                    case "ObtenerListadoTipoImpuesto":
                        IList<LlaveDescripcion> listadoTipoImpuesto = servicioMantenimiento.ObtenerListadoTipoImpuesto();
                        if (listadoTipoImpuesto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoImpuesto);
                        break;
                    case "ObtenerListadoFormaPagoCliente":
                        IList<LlaveDescripcion> listadoFormaPagoFactura = servicioMantenimiento.ObtenerListadoFormaPagoCliente();
                        if (listadoFormaPagoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoFactura);
                        break;
                    case "ObtenerListadoFormaPagoEmpresa":
                        IList<LlaveDescripcion> listadoFormaPagoCompra = servicioMantenimiento.ObtenerListadoFormaPagoEmpresa();
                        if (listadoFormaPagoCompra.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoCompra);
                        break;
                    case "ObtenerListadoTipoMoneda":
                        IList<LlaveDescripcion> listadoTipoMoneda = servicioMantenimiento.ObtenerListadoTipoMoneda();
                        if (listadoTipoMoneda.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoMoneda);
                        break;
                    case "ObtenerListadoCondicionVenta":
                        IList<LlaveDescripcion> listadoCondicionVenta = servicioMantenimiento.ObtenerListadoCondicionVenta();
                        if (listadoCondicionVenta.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionVenta);
                        break;
                    case "ObtenerListadoRolesPorEmpresa":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<LlaveDescripcion> listadoRoles = servicioMantenimiento.ObtenerListadoRolePorEmpresa(intIdEmpresa);
                        if (listadoRoles.Count > 0)
                            strRespuesta = serializer.Serialize(listadoRoles);
                        break;
                    case "ObtenerListadoEmpresa":
                        IList<LlaveDescripcion> listadoEmpresa = servicioMantenimiento.ObtenerListadoEmpresa();
                        if (listadoEmpresa.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEmpresa);
                        break;
                    case "ObtenerListadoCondicionVentaYFormaPagoFactura":
                        IList<LlaveDescripcion> listadoCondicionesyFormaPagoFactura = servicioReportes.ObtenerListadoCondicionVentaYFormaPagoFactura();
                        if (listadoCondicionesyFormaPagoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionesyFormaPagoFactura);
                        break;
                    case "ObtenerListadoCondicionVentaYFormaPagoCompra":
                        IList<LlaveDescripcion> listadoCondicionesyFormaPagoCompra = servicioReportes.ObtenerListadoCondicionVentaYFormaPagoCompra();
                        if (listadoCondicionesyFormaPagoCompra.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionesyFormaPagoCompra);
                        break;
                    case "ObtenerReporteProformas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        IList<ReporteDetalle> listadoReporteProformas = servicioReportes.ObtenerReporteProformas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                        if (listadoReporteProformas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteProformas);
                        break;
                    case "ObtenerReporteApartados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        IList<ReporteDetalle> listadoReporteApartados = servicioReportes.ObtenerReporteApartados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                        if (listadoReporteApartados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteApartados);
                        break;
                    case "ObtenerReporteOrdenesServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        IList<ReporteDetalle> listadoReporteOrdenes = servicioReportes.ObtenerReporteOrdenesServicio(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolNulo);
                        if (listadoReporteOrdenes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteOrdenes);
                        break;
                    case "ObtenerReporteVentasPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                        IList<ReporteDetalle> listadoReporteVentas = servicioReportes.ObtenerReporteVentasPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente, bolNulo, intIdTipoPago);
                        if (listadoReporteVentas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentas);
                        break;
                    case "ObtenerReporteVentasPorVendedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        int intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        IList<ReporteVentasPorVendedor> listadoReporteVentasPorVendedor = servicioReportes.ObtenerReporteVentasPorVendedor(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdVendedor);
                        if (listadoReporteVentasPorVendedor.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorVendedor);
                        break;
                    case "ObtenerReporteComprasPorProveedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        bolNulo = bool.Parse(parametrosJO.Property("isNulo").Value.ToString());
                        intIdTipoPago = int.Parse(parametrosJO.Property("IdTipoPago").Value.ToString());
                        IList<ReporteDetalle> listadoReporteCompras = servicioReportes.ObtenerReporteComprasPorProveedor(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdProveedor, bolNulo, intIdTipoPago);
                        if (listadoReporteCompras.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCompras);
                        break;
                    case "ObtenerReporteCuentasPorCobrarClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("Activas").Value.ToString());
                        IList<ReporteCuentas> listadoReporteCuentasPorCobrar = servicioReportes.ObtenerReporteCuentasPorCobrarClientes(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente, bolFiltraActivos);
                        if (listadoReporteCuentasPorCobrar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCuentasPorCobrar);
                        break;
                    case "ObtenerReporteCuentasPorPagarProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("Activas").Value.ToString());
                        IList<ReporteCuentas> listadoReporteCuentasPorPagar = servicioReportes.ObtenerReporteCuentasPorPagarProveedores(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdProveedor, bolFiltraActivos);
                        if (listadoReporteCuentasPorPagar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCuentasPorPagar);
                        break;
                    case "ObtenerReporteMovimientosCxCClientes":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        IList<ReporteGrupoDetalle> listadoReporteMovimientosCxC = servicioReportes.ObtenerReporteMovimientosCxCClientes(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdCliente);
                        if (listadoReporteMovimientosCxC.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosCxC);
                        break;
                    case "ObtenerReporteMovimientosCxPProveedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        IList<ReporteGrupoDetalle> listadoReporteMovimientosCxP = servicioReportes.ObtenerReporteMovimientosCxPProveedores(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdProveedor);
                        if (listadoReporteMovimientosCxP.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosCxP);
                        break;
                    case "ObtenerReporteMovimientosBanco":
                        intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteMovimientosBanco> listadoReporteMovimientosBanco = servicioReportes.ObtenerReporteMovimientosBanco(intIdCuenta, strFechaInicial, strFechaFinal);
                        if (listadoReporteMovimientosBanco.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteMovimientosBanco);
                        break;
                    case "ObtenerReporteEstadoResultados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<DescripcionValor> listadoReporteEstadoResultados = servicioReportes.ObtenerReporteEstadoResultados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                        if (listadoReporteEstadoResultados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteEstadoResultados);
                        break;
                    case "ObtenerReporteDetalleEgreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteGrupoDetalle> listadoReporteDetalleEgreso = servicioReportes.ObtenerReporteDetalleEgreso(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                        if (listadoReporteDetalleEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteDetalleEgreso);
                        break;
                    case "ObtenerReporteDetalleIngreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                        IList<ReporteGrupoDetalle> listadoReporteDetalleIngreso = servicioReportes.ObtenerReporteDetalleIngreso(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                        if (listadoReporteDetalleIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteDetalleIngreso);
                        break;
                    case "ObtenerReporteVentasPorLineaResumen":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteVentasPorLineaResumen> listadoReporteVentasPorLineaResumen = servicioReportes.ObtenerReporteVentasPorLineaResumen(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                        if (listadoReporteVentasPorLineaResumen.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorLineaResumen);
                        break;
                    case "ObtenerReporteVentasPorLineaDetalle":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteVentasPorLineaDetalle> listadoReporteVentasPorLineaDetalle = servicioReportes.ObtenerReporteVentasPorLineaDetalle(intIdEmpresa, intIdSucursal, intIdLlave1, strFechaInicial, strFechaFinal);
                        if (listadoReporteVentasPorLineaDetalle.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteVentasPorLineaDetalle);
                        break;
                    case "ObtenerReporteCierreDeCaja":
                        int intIdCierre = int.Parse(parametrosJO.Property("IdCierre").Value.ToString());
                        IList<DescripcionValor> listadoReporteCierreDeCaja = servicioReportes.ObtenerReporteCierreDeCaja(intIdCierre);
                        if (listadoReporteCierreDeCaja.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteCierreDeCaja);
                        break;
                    case "ObtenerReporteInventario":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString());
                        bolFiltraExistencias = bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<ReporteInventario> listadoReporteInventario = servicioReportes.ObtenerReporteInventario(intIdEmpresa, intIdSucursal, bolFiltraActivos, bolFiltraExistencias, intIdLlave1, strCodigo, strDescripcion);
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
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        IList<ReportePerdidasyGanancias> listadoReportePerdidasyGanancias = servicioReportes.ObtenerReportePerdidasyGanancias(intIdEmpresa, intIdSucursal);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        IList<ReporteEgreso> listadoReporteEgreso = servicioReportes.ObtenerReporteEgreso(intIdLlave1);
                        if (listadoReporteEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteEgreso);
                        break;
                    case "ObtenerReporteIngreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        IList<ReporteIngreso> listadoReporteIngreso = servicioReportes.ObtenerReporteIngreso(intIdLlave1);
                        if (listadoReporteIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteIngreso);
                        break;
                    case "ObtenerReporteDocumentosElectronicosEmitidos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoFacturasEmitidas = servicioReportes.ObtenerReporteDocumentosElectronicosEmitidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                        if (listadoFacturasEmitidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFacturasEmitidas);
                        break;
                    case "ObtenerReporteDocumentosElectronicosRecibidos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteDocumentoElectronico> listadoFacturasRecibidas = servicioReportes.ObtenerReporteDocumentosElectronicosRecibidos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                        if (listadoFacturasRecibidas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFacturasRecibidas);
                        break;
                    case "ObtenerReporteResumenDocumentosElectronicos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<ReporteResumenMovimiento> listadoReporteResumenDocumentosElectronicos = servicioReportes.ObtenerReporteResumenDocumentosElectronicos(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal);
                        if (listadoReporteResumenDocumentosElectronicos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReporteResumenDocumentosElectronicos);
                        break;
                    case "GenerarDatosCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        cierre = servicioFlujoCaja.GenerarDatosCierreCaja(intIdEmpresa, intIdSucursal);
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
                        IList<LlaveDescripcion> listadoSucursales = servicioMantenimiento.ObtenerListadoSucursales(intIdEmpresa);
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
                        IList<LlaveDescripcion> listadoTipoIdentificacion = servicioMantenimiento.ObtenerListadoTipoIdentificacion();
                        if (listadoTipoIdentificacion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoIdentificacion);
                        break;
                    case "ObtenerListadoCatalogoReportes":
                        IList<LlaveDescripcion> listadoReportes = servicioMantenimiento.ObtenerListadoCatalogoReportes();
                        if (listadoReportes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReportes);
                        break;
                    case "ObtenerListadoProvincias":
                        IList<LlaveDescripcion> listadoProvincias = servicioMantenimiento.ObtenerListadoProvincias();
                        if (listadoProvincias.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProvincias);
                        break;
                    case "ObtenerListadoCantones":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        IList<LlaveDescripcion> listadoCantones = servicioMantenimiento.ObtenerListadoCantones(intIdProvincia);
                        if (listadoCantones.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCantones);
                        break;
                    case "ObtenerListadoDistritos":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        IList<LlaveDescripcion> listadoDistritos = servicioMantenimiento.ObtenerListadoDistritos(intIdProvincia, intIdCanton);
                        if (listadoDistritos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDistritos);
                        break;
                    case "ObtenerListadoBarrios":
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                        IList<LlaveDescripcion> listadoBarrios = servicioMantenimiento.ObtenerListadoBarrios(intIdProvincia, intIdCanton, intIdDistrito);
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
                        IList<LlaveDescripcion> listadoBancoAdquiriente = servicioMantenimiento.ObtenerListadoBancoAdquiriente(intIdEmpresa, strDescripcion);
                        if (listadoBancoAdquiriente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBancoAdquiriente);
                        break;
                    case "ObtenerBancoAdquiriente":
                        intIdBanco = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
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
                        IList<LlaveDescripcion> listadoCliente = servicioFacturacion.ObtenerListadoClientes(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoCliente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCliente);
                        break;
                    case "ObtenerCliente":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        cliente = servicioFacturacion.ObtenerCliente(intIdLlave1);
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
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoLinea = servicioMantenimiento.ObtenerListadoLineas(intIdEmpresa, strDescripcion);
                        if (listadoLinea.Count > 0)
                            strRespuesta = serializer.Serialize(listadoLinea);
                        break;
                    case "ObtenerLinea":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        linea = servicioMantenimiento.ObtenerLinea(intIdLlave1);
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
                        IList<LlaveDescripcion> listadoProveedor = servicioCompra.ObtenerListadoProveedores(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoProveedor.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProveedor);
                        break;
                    case "ObtenerProveedor":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        proveedor = servicioCompra.ObtenerProveedor(intIdLlave1);
                        if (proveedor != null)
                            strRespuesta = serializer.Serialize(proveedor);
                        break;
                    case "ObtenerTotalListaProductos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString());
                        bolFiltraExistencias = parametrosJO.Property("FiltraExistencias") != null ? bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString()) : false;
                        intIdLlave1 = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        intTotalLista = servicioMantenimiento.ObtenerTotalListaProductos(intIdEmpresa, intIdSucursal, bolIncluyeServicios, bolFiltraActivos, bolFiltraExistencias, intIdLlave1, strCodigo, strCodigoProveedor, strDescripcion);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoProductos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("FiltraActivos").Value.ToString());
                        bolFiltraExistencias = parametrosJO.Property("FiltraExistencias") != null ? bool.Parse(parametrosJO.Property("FiltraExistencias").Value.ToString()) : false;
                        intIdLlave1 = parametrosJO.Property("IdLinea") != null ? int.Parse(parametrosJO.Property("IdLinea").Value.ToString()) : 0;
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        strCodigoProveedor = parametrosJO.Property("CodigoProveedor") != null ? parametrosJO.Property("CodigoProveedor").Value.ToString() : "";
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<ProductoDetalle> listadoProducto = (List<ProductoDetalle>)servicioMantenimiento.ObtenerListadoProductos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, bolFiltraActivos, bolFiltraExistencias, intIdLlave1, strCodigo, strCodigoProveedor, strDescripcion);
                        if (listadoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProducto);
                        break;
                    case "ObtenerProducto":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProducto(intIdLlave1, intIdSucursal);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoTransitorio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProductoTransitorio(intIdEmpresa);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoPorCodigo":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProductoPorCodigo(intIdEmpresa, strCodigo, intIdSucursal);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoPorCodigoProveedor":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProductoPorCodigoProveedor(intIdEmpresa, strCodigo, intIdSucursal);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerTotalMovimientosPorProducto":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intTotalLista = servicioMantenimiento.ObtenerTotalMovimientosPorProducto(intIdLlave1, intIdSucursal, strFechaInicial, strFechaFinal);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerMovimientosPorProducto":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        IList<MovimientoProducto> listadoMovimientosProducto = servicioMantenimiento.ObtenerMovimientosPorProducto(intIdLlave1, intIdSucursal, intNumeroPagina, intFilasPorPagina, strFechaInicial, strFechaFinal);
                        if (listadoMovimientosProducto != null)
                            strRespuesta = serializer.Serialize(listadoMovimientosProducto);
                        break;
                    case "ObtenerListadoUsuarios":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo") != null ? parametrosJO.Property("Codigo").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoUsuario = servicioMantenimiento.ObtenerListadoUsuarios(intIdEmpresa, strCodigo);
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
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoCuentaEgreso = servicioFlujoCaja.ObtenerListadoCuentasEgreso(intIdEmpresa, strDescripcion);
                        if (listadoCuentaEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaEgreso);
                        break;
                    case "ObtenerCuentaEgreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        cuentaEgreso = servicioFlujoCaja.ObtenerCuentaEgreso(intIdLlave1);
                        if (cuentaEgreso != null)
                            strRespuesta = serializer.Serialize(cuentaEgreso);
                        break;
                    case "ObtenerListadoCuentasIngreso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : ""; ;
                        IList<LlaveDescripcion> listadoCuentaIngreso = servicioFlujoCaja.ObtenerListadoCuentasIngreso(intIdEmpresa, strDescripcion);
                        if (listadoCuentaIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaIngreso);
                        break;
                    case "ObtenerCuentaIngreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaIngreso").Value.ToString());
                        cuentaIngreso = servicioFlujoCaja.ObtenerCuentaIngreso(intIdLlave1);
                        if (cuentaIngreso != null)
                            strRespuesta = serializer.Serialize(cuentaIngreso);
                        break;
                    case "ObtenerListadoCuentasBanco":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion") != null ? parametrosJO.Property("Descripcion").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoCuentaBanco = servicioBanca.ObtenerListadoCuentasBanco(intIdEmpresa, strDescripcion);
                        if (listadoCuentaBanco.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaBanco);
                        break;
                    case "ObtenerCuentaBanco":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        cuentaBanco = servicioBanca.ObtenerCuentaBanco(intIdLlave1);
                        if (cuentaBanco != null)
                            strRespuesta = serializer.Serialize(cuentaBanco);
                        break;
                    case "ObtenerListadoVendedores":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<LlaveDescripcion> listadoVendedores = servicioMantenimiento.ObtenerListadoVendedores(intIdEmpresa, strNombre);
                        if (listadoVendedores.Count > 0)
                            strRespuesta = serializer.Serialize(listadoVendedores);
                        break;
                    case "ObtenerVendedor":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        vendedor = servicioMantenimiento.ObtenerVendedor(intIdLlave1);
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
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        intTotalLista = servicioFlujoCaja.ObtenerTotalListaEgresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoEgresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        IList<EfectivoDetalle> listadoEgreso = servicioFlujoCaja.ObtenerListadoEgresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle);
                        if (listadoEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEgreso);
                        break;
                    case "AgregarEgreso":
                        egreso = serializer.Deserialize<Egreso>(strEntidad);
                        string strIdEgreso = servicioFlujoCaja.AgregarEgreso(egreso);
                        strRespuesta = serializer.Serialize(strIdEgreso);
                        break;
                    case "ObtenerEgreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        egreso = servicioFlujoCaja.ObtenerEgreso(intIdLlave1);
                        if (egreso != null)
                            strRespuesta = serializer.Serialize(egreso);
                        break;
                    case "ObtenerTotalListaIngresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        intTotalLista = servicioFlujoCaja.ObtenerTotalListaIngresos(intIdEmpresa, intIdSucursal, intIdLlave1, strBeneficiario, strDetalle);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoIngresos":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        IList<EfectivoDetalle> listadoIngreso = servicioFlujoCaja.ObtenerListadoIngresos(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strBeneficiario, strDetalle);
                        if (listadoIngreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoIngreso);
                        break;
                    case "AgregarIngreso":
                        ingreso = serializer.Deserialize<Ingreso>(strEntidad);
                        string strIdIngreso = servicioFlujoCaja.AgregarIngreso(ingreso);
                        strRespuesta = serializer.Serialize(strIdIngreso);
                        break;
                    case "ObtenerIngreso":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdIngreso").Value.ToString());
                        ingreso = servicioFlujoCaja.ObtenerIngreso(intIdLlave1);
                        if (ingreso != null)
                            strRespuesta = serializer.Serialize(ingreso);
                        break;
                    case "ObtenerTotalListaFacturas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial") != null ? parametrosJO.Property("FechaInicial").Value.ToString() : "";
                        strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                        intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaFacturas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoFacturas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial") != null ? parametrosJO.Property("FechaInicial").Value.ToString() : "";
                        strFechaFinal = parametrosJO.Property("FechaFinal") != null ? parametrosJO.Property("FechaFinal").Value.ToString() : "";
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdFactura") != null ? int.Parse(parametrosJO.Property("IdFactura").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoFacturas = servicioFacturacion.ObtenerListadoFacturas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
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
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        factura = servicioFacturacion.ObtenerFactura(intIdLlave1);
                        if (factura != null)
                            strRespuesta = serializer.Serialize(factura);
                        break;
                    case "ObtenerTotalListaDevolucionesPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdLlave1 = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaDevolucionesPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoDevolucionesPorCliente":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdDevolucion") != null ? int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoDevolucionClientes = servicioFacturacion.ObtenerListadoDevolucionesPorCliente(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                        if (listadoDevolucionClientes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDevolucionClientes);
                        break;
                    case "AgregarDevolucionCliente":
                        devolucionCliente = serializer.Deserialize<DevolucionCliente>(strEntidad);
                        string strIdDevolucionCliente = servicioFacturacion.AgregarDevolucionCliente(devolucionCliente, configuracionGeneral);
                        strRespuesta = serializer.Serialize(strIdDevolucionCliente);
                        break;
                    case "ObtenerDevolucionCliente":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDevolucion").Value.ToString());
                        devolucionCliente = servicioFacturacion.ObtenerDevolucionCliente(intIdLlave1);
                        if (devolucionCliente != null)
                            strRespuesta = serializer.Serialize(devolucionCliente);
                        break;
                    case "ObtenerTotalListaCompras":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intIdLlave1 = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioCompra.ObtenerTotalListaCompras(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoCompras":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdCompra") != null ? int.Parse(parametrosJO.Property("IdCompra").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<CompraDetalle> listadoCompras = (List<CompraDetalle>)servicioCompra.ObtenerListadoCompras(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                        if (listadoCompras.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCompras);
                        break;
                    case "AgregarCompra":
                        compra = serializer.Deserialize<Compra>(strEntidad);
                        string strIdCompra = servicioCompra.AgregarCompra(compra);
                        strRespuesta = serializer.Serialize(strIdCompra);
                        break;
                    case "ObtenerCompra":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCompra").Value.ToString());
                        compra = servicioCompra.ObtenerCompra(intIdLlave1);
                        if (compra != null)
                            strRespuesta = serializer.Serialize(compra);
                        break;
                    case "ObtenerTotalListaProformas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaProformas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoProformas":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdProforma") != null ? int.Parse(parametrosJO.Property("IdProforma").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoProformas = servicioFacturacion.ObtenerListadoProformas(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                        if (listadoProformas.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProformas);
                        break;
                    case "AgregarProforma":
                        proforma = serializer.Deserialize<Proforma>(strEntidad);
                        string strIdProforma = servicioFacturacion.AgregarProforma(proforma);
                        strRespuesta = serializer.Serialize(strIdProforma);
                        break;
                    case "ObtenerProforma":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdProforma").Value.ToString());
                        proforma = servicioFacturacion.ObtenerProforma(intIdLlave1);
                        if (proforma != null)
                            strRespuesta = serializer.Serialize(proforma);
                        break;
                    case "ObtenerTotalListaOrdenServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaOrdenServicio(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoOrdenServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdOrdenServicio") != null ? int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoOrdenServicio = servicioFacturacion.ObtenerListadoOrdenServicio(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                        if (listadoOrdenServicio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoOrdenServicio);
                        break;
                    case "AgregarOrdenServicio":
                        ordenServicio = serializer.Deserialize<OrdenServicio>(strEntidad);
                        string strIdOrdenServicio = servicioFacturacion.AgregarOrdenServicio(ordenServicio);
                        strRespuesta = serializer.Serialize(strIdOrdenServicio);
                        break;
                    case "ObtenerOrdenServicio":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdOrdenServicio").Value.ToString());
                        ordenServicio = servicioFacturacion.ObtenerOrdenServicio(intIdLlave1);
                        if (ordenServicio != null)
                            strRespuesta = serializer.Serialize(ordenServicio);
                        break;
                    case "ObtenerTotalListaApartados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        intTotalLista = servicioFacturacion.ObtenerTotalListaApartados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intIdLlave1, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoApartados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        strFechaInicial = parametrosJO.Property("FechaInicial").Value.ToString();
                        strFechaFinal = parametrosJO.Property("FechaFinal").Value.ToString();
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = parametrosJO.Property("IdApartado") != null ? int.Parse(parametrosJO.Property("IdApartado").Value.ToString()) : 0;
                        strNombre = parametrosJO.Property("Nombre") != null ? parametrosJO.Property("Nombre").Value.ToString() : "";
                        IList<FacturaDetalle> listadoApartados = servicioFacturacion.ObtenerListadoApartados(intIdEmpresa, intIdSucursal, strFechaInicial, strFechaFinal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1, strNombre);
                        if (listadoApartados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoApartados);
                        break;
                    case "AgregarApartado":
                        apartado = serializer.Deserialize<Apartado>(strEntidad);
                        string strIdApartado = servicioFacturacion.AgregarApartado(apartado);
                        strRespuesta = serializer.Serialize(strIdApartado);
                        break;
                    case "ObtenerApartado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                        apartado = servicioFacturacion.ObtenerApartado(intIdLlave1);
                        if (apartado != null)
                            strRespuesta = serializer.Serialize(apartado);
                        break;
                    case "ObtenerCuentaPorCobrar":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCxC").Value.ToString());
                        cuentaPorCobrar = servicioCuentaPorProcesar.ObtenerCuentaPorCobrar(intIdLlave1);
                        if (cuentaPorCobrar != null)
                            strRespuesta = serializer.Serialize(cuentaPorCobrar);
                        break;
                    case "ObtenerTotalListaCuentasPorCobrar":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                        strClave = parametrosJO.Property("Referencia").Value.ToString();
                        strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                        intTotalLista = servicioCuentaPorProcesar.ObtenerTotalListaCuentasPorCobrar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, strClave, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
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
                        IList<CuentaPorProcesar> listadoCuentasPorCobrar = servicioCuentaPorProcesar.ObtenerListadoCuentasPorCobrar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, intNumeroPagina, intFilasPorPagina, strClave, strNombre);
                        if (listadoCuentasPorCobrar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentasPorCobrar);
                        break;
                    case "ObtenerListaMovimientosCxC":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                        IList<EfectivoDetalle> listadoMovimientosCxC = servicioCuentaPorProcesar.ObtenerListadoMovimientosCxC(intIdEmpresa, intIdSucursal, intIdCuenta);
                        if (listadoMovimientosCxC.Count > 0)
                            strRespuesta = serializer.Serialize(listadoMovimientosCxC);
                        break;
                    case "ObtenerMovimientoCxC":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        movimientoCxC = servicioCuentaPorProcesar.ObtenerMovimientoCxC(intIdLlave1);
                        if (movimientoCxC != null)
                            strRespuesta = serializer.Serialize(movimientoCxC);
                        break;
                    case "ObtenerCuentaPorPagar":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCxP").Value.ToString());
                        cuentaPorPagar = servicioCuentaPorProcesar.ObtenerCuentaPorPagar(intIdLlave1);
                        if (cuentaPorPagar != null)
                            strRespuesta = serializer.Serialize(cuentaPorPagar);
                        break;
                    case "ObtenerTotalListaCuentasPorPagar":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTipo").Value.ToString());
                        bolFiltraActivos = bool.Parse(parametrosJO.Property("Pendientes").Value.ToString());
                        strClave = parametrosJO.Property("Referencia").Value.ToString();
                        strNombre = parametrosJO.Property("NombrePropietario").Value.ToString();
                        intTotalLista = servicioCuentaPorProcesar.ObtenerTotalListaCuentasPorPagar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, strClave, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
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
                        IList<CuentaPorProcesar> listadoCuentasPorPagar = servicioCuentaPorProcesar.ObtenerListadoCuentasPorPagar(intIdEmpresa, intIdSucursal, intIdLlave1, bolFiltraActivos, intNumeroPagina, intFilasPorPagina, strClave, strNombre);
                        if (listadoCuentasPorPagar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentasPorPagar);
                        break;
                    case "ObtenerListaMovimientosCxP":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdCuenta = int.Parse(parametrosJO.Property("IdCuenta").Value.ToString());
                        IList<EfectivoDetalle> listadoMovimientosCxP = servicioCuentaPorProcesar.ObtenerListadoMovimientosCxP(intIdEmpresa, intIdSucursal, intIdCuenta);
                        if (listadoMovimientosCxP.Count > 0)
                            strRespuesta = serializer.Serialize(listadoMovimientosCxP);
                        break;
                    case "ObtenerMovimientoCxP":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        movimientoCxP = servicioCuentaPorProcesar.ObtenerMovimientoCxP(intIdLlave1);
                        if (movimientoCxP != null)
                            strRespuesta = serializer.Serialize(movimientoCxP);
                        break;
                    case "ObtenerListadoApartadosConSaldo":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<LlaveDescripcion> listadoApartadosConSaldo = servicioCuentaPorProcesar.ObtenerListadoApartadosConSaldo(intIdEmpresa);
                        if (listadoApartadosConSaldo.Count > 0)
                            strRespuesta = serializer.Serialize(listadoApartadosConSaldo);
                        break;
                    case "ObtenerListadoMovimientosApartado":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdApartado").Value.ToString());
                        IList<EfectivoDetalle> listadoMovimientosApartado = servicioCuentaPorProcesar.ObtenerListadoMovimientosApartado(intIdEmpresa, intIdSucursal, intIdLlave1);
                        if (listadoMovimientosApartado.Count > 0)
                            strRespuesta = serializer.Serialize(listadoMovimientosApartado);
                        break;
                    case "ObtenerMovimientoApartado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        movimientoApartado = servicioCuentaPorProcesar.ObtenerMovimientoApartado(intIdLlave1);
                        if (movimientoApartado != null)
                            strRespuesta = serializer.Serialize(movimientoApartado);
                        break;
                    case "ObtenerListadoOrdenesServicioConSaldo":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<LlaveDescripcion> listadoOrdenesServicioConSaldo = servicioCuentaPorProcesar.ObtenerListadoOrdenesServicioConSaldo(intIdEmpresa);
                        if (listadoOrdenesServicioConSaldo.Count > 0)
                            strRespuesta = serializer.Serialize(listadoOrdenesServicioConSaldo);
                        break;
                    case "ObtenerListadoMovimientosOrdenServicio":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdOrden").Value.ToString());
                        IList<EfectivoDetalle> listadoMovimientosOrdenServicio = servicioCuentaPorProcesar.ObtenerListadoMovimientosOrdenServicio(intIdEmpresa, intIdSucursal, intIdLlave1);
                        if (listadoMovimientosOrdenServicio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoMovimientosOrdenServicio);
                        break;
                    case "ObtenerMovimientoOrdenServicio":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdMovimiento").Value.ToString());
                        movimientoOrdenServicio = servicioCuentaPorProcesar.ObtenerMovimientoOrdenServicio(intIdLlave1);
                        if (movimientoOrdenServicio != null)
                            strRespuesta = serializer.Serialize(movimientoOrdenServicio);
                        break;
                    case "ObtenerListadoDocumentosElectronicosEnProceso":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoDetalle> listadoEnProceso = servicioFacturacion.ObtenerListadoDocumentosElectronicosEnProceso(intIdEmpresa);
                        if (listadoEnProceso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEnProceso);
                        break;
                    case "ObtenerTotalDocumentosElectronicosProcesados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        int intTotalDocumentosProcesados = servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal);
                        strRespuesta = intTotalDocumentosProcesados.ToString();
                        break;
                    case "ObtenerListadoDocumentosElectronicosProcesados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        IList<DocumentoDetalle> listadoProcesados = servicioFacturacion.ObtenerListadoDocumentosElectronicosProcesados(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina);
                        if (listadoProcesados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProcesados);
                        break;
                    case "ObtenerDocumentoElectronico":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerDocumentoElectronico(intIdLlave1);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    case "ObtenerRespuestaDocumentoElectronicoEnviado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerRespuestaDocumentoElectronicoEnviado(intIdLlave1, configuracionGeneral);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    case "AutorizacionPorcentaje":
                        string strCodigoUsuario = parametrosJO.Property("CodigoUsuario").Value.ToString();
                        strClave = parametrosJO.Property("Clave").Value.ToString();
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        decimal decPorcentaje = servicioMantenimiento.AutorizacionPorcentaje(strCodigoUsuario, strClave, intIdEmpresa);
                        strRespuesta = decPorcentaje.ToString();
                        break;
                    case "ObtenerListadoSucursalDestino":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                        IList<LlaveDescripcion> listadoSucursalesDestino = servicioTraslado.ObtenerListadoSucursalDestino(intIdEmpresa, intIdSucursal);
                        if (listadoSucursalesDestino.Count > 0)
                            strRespuesta = serializer.Serialize(listadoSucursalesDestino);
                        break;
                    case "AgregarTraslado":
                        traslado = serializer.Deserialize<Traslado>(strEntidad);
                        string strIdTraslado = servicioTraslado.AgregarTraslado(traslado);
                        strRespuesta = serializer.Serialize(strIdTraslado);
                        break;
                    case "ObtenerTraslado":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                        traslado = servicioTraslado.ObtenerTraslado(intIdLlave1);
                        if (traslado != null)
                            strRespuesta = serializer.Serialize(traslado);
                        break;
                    case "ObtenerTotalListaTraslados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                        int intTotalTraslados = servicioTraslado.ObtenerTotalListaTraslados(intIdEmpresa, intIdSucursal, bolAplicado, intIdLlave1);
                        strRespuesta = intTotalTraslados.ToString();
                        break;
                    case "ObtenerListadoTraslados":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalOrigen").Value.ToString());
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdTraslado").Value.ToString());
                        IList<TrasladoDetalle> listadoTraslados = servicioTraslado.ObtenerListadoTraslados(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina, intIdLlave1);
                        if (listadoTraslados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTraslados);
                        break;
                    case "ObtenerTotalListaTrasladosPorAplicar":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalDestino").Value.ToString());
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        int intTotalTrasladosPorAplicar = servicioTraslado.ObtenerTotalListaTrasladosPorAplicar(intIdEmpresa, intIdSucursal, bolAplicado);
                        strRespuesta = intTotalTrasladosPorAplicar.ToString();
                        break;
                    case "ObtenerListadoTrasladosPorAplicar":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursalDestino").Value.ToString());
                        bolAplicado = bool.Parse(parametrosJO.Property("Aplicado").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        IList<TrasladoDetalle> listadoTrasladosPorAplicar = servicioTraslado.ObtenerListadoTrasladosPorAplicar(intIdEmpresa, intIdSucursal, bolAplicado, intNumeroPagina, intFilasPorPagina);
                        if (listadoTrasladosPorAplicar.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTrasladosPorAplicar);
                        break;
                    case "AgregarAjusteInventario":
                        ajusteInventario = serializer.Deserialize<AjusteInventario>(strEntidad);
                        string strIdAjuste = servicioMantenimiento.AgregarAjusteInventario(ajusteInventario);
                        strRespuesta = serializer.Serialize(strIdAjuste);
                        break;
                    case "ObtenerAjusteInventario":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                        ajusteInventario = servicioMantenimiento.ObtenerAjusteInventario(intIdLlave1);
                        if (ajusteInventario != null)
                            strRespuesta = serializer.Serialize(ajusteInventario);
                        break;
                    case "ObtenerTotalListaAjusteInventario":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        int intTotalAjusteInventarios = servicioMantenimiento.ObtenerTotalListaAjusteInventario(intIdEmpresa, intIdSucursal, intIdLlave1, strDescripcion);
                        strRespuesta = intTotalAjusteInventarios.ToString();
                        break;
                    case "ObtenerListadoAjusteInventario":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdAjuste").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<AjusteInventarioDetalle> listadoAjusteInventarios = servicioMantenimiento.ObtenerListadoAjusteInventario(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina, intIdLlave1, strDescripcion);
                        if (listadoAjusteInventarios.Count > 0)
                            strRespuesta = serializer.Serialize(listadoAjusteInventarios);
                        break;
                    case "ObtenerTotalListaCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intTotalLista = servicioFlujoCaja.ObtenerTotalListaCierreCaja(intIdEmpresa, intIdSucursal);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListadoCierreCaja":
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdSucursal = int.Parse(parametrosJO.Property("IdSucursal").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        IList<LlaveDescripcion> listadoCierreCaja = servicioFlujoCaja.ObtenerListadoCierreCaja(intIdEmpresa, intIdSucursal, intNumeroPagina, intFilasPorPagina);
                        if (listadoCierreCaja.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCierreCaja);
                        break;
                    case "ObtenerCierreCaja":
                        intIdLlave1 = int.Parse(parametrosJO.Property("IdCierre").Value.ToString());
                        cierre = servicioFlujoCaja.ObtenerCierreCaja(intIdLlave1);
                        if (cierre != null)
                            strRespuesta = serializer.Serialize(cierre);
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
