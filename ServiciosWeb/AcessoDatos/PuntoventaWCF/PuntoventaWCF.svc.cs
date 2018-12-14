using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Servicios;
using LeandroSoftware.AccesoDatos.TiposDatos;
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

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class PuntoventaWCF : IPuntoventaWCF
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ICorreoService servicioEnvioCorreo;
        private IMantenimientoService servicioMantenimiento;
        private IFacturacionService servicioFacturacion;
        IUnityContainer unityContainer;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private readonly DatosConfiguracion configuracion = new DatosConfiguracion
        (
            appSettings["strConsultaIEURL"].ToString(),
            appSettings["strSoapOperation"].ToString(),
            appSettings["strServicioComprobantesURL"].ToString(),
            appSettings["strClientId"].ToString(),
            appSettings["strServicioTokenURL"].ToString(),
            appSettings["strComprobantesCallbackURL"].ToString(),
            appSettings["strCorreoNotificacionErrores"].ToString()
        );

        public PuntoventaWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            // unityContainer.RegisterInstance("configuracionData", configuracion, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            try
            {
                ComprobanteElectronicoService.ObtenerTipoCambioVenta(configuracion.ConsultaIndicadoresEconomicosURL, configuracion.OperacionSoap, DateTime.Now, unityContainer);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void Ejecutar(RequestDTO datos)
        {
            try
            {
                JObject parametrosJO;
                int intIdEmpresa;
                int intIdDocumento;
                switch (datos.NombreMetodo)
                {
                    case "ActualizarEmpresa":
                        Empresa empresa = null;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        empresa = serializer.Deserialize<Empresa>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarEmpresa(empresa);
                        break;
                    case "ActualizarLogoEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strLogotipo = parametrosJO.Property("Logotipo").Value.ToString();
                        servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
                        break;
                    case "ActualizarCertificadoEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                        servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
                        break;
                    case "EnviarDocumentoElectronicoPendiente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarDocumentoElectronicoPendiente(intIdDocumento, configuracion);
                        break;
                    case "EnviarNotificacionDocumentoElectronico":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdDocumento, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al procesar petición sin respuesta: " + datos.NombreMetodo, ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Empresa empresa;
                Usuario usuario;
                DocumentoElectronico documento;
                JObject parametrosJO;
                int intIdEmpresa;
                int intIdProvincia;
                int intIdCanton;
                int intIdDistrito;
                int intIdDocumento;
                string strRespuesta = "";
                switch (datos.NombreMetodo)
                {
                    case "ValidarCredenciales":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        string strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                        string strUsuario = parametrosJO.Property("Usuario").Value.ToString();
                        string strClave = parametrosJO.Property("Clave").Value.ToString();
                        empresa = servicioMantenimiento.ObtenerEmpresaPorIdentificacion(strIdentificacion);
                        if (empresa == null) throw new Exception("Empresa no registrada en el sistema.Por favor, pongase en contacto con su proveedor del servicio.");
                        usuario = servicioMantenimiento.ValidarUsuario(empresa.IdEmpresa, strUsuario, strClave, appSettings["AppThumptprint"]);
                        if (usuario != null)
                        {
                            foreach (RolePorUsuario role in usuario.RolePorUsuario)
                                role.Usuario = null;
                            strRespuesta = serializer.Serialize(usuario);
                        }
                        break;
                    case "ObtenerEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        empresa = servicioMantenimiento.ObtenerEmpresa(intIdEmpresa);
                        if (empresa != null)
                        {
                            foreach (DetalleRegistro detalle in empresa.DetalleRegistro)
                                detalle.Empresa = null;
                            foreach (ModuloPorEmpresa modulo in empresa.ModuloPorEmpresa)
                                modulo.Empresa = null;
                            foreach (ReportePorEmpresa reporte in empresa.ReportePorEmpresa)
                                reporte.Empresa = null;
                            strRespuesta = serializer.Serialize(empresa);
                        }
                        break;
                    case "ObtenerListaEmpresas":
                        IList<Empresa> listadoEmpresa = (List<Empresa>)servicioMantenimiento.ObtenerListaEmpresas();
                        if (listadoEmpresa.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEmpresa);
                        break;
                    case "ObtenerListaTipoIdentificacion":
                        IList<TipoIdentificacion> listadoTipoIdentificacion = (List<TipoIdentificacion>)servicioMantenimiento.ObtenerListaTipoIdentificacion();
                        if (listadoTipoIdentificacion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoIdentificacion);
                        break;
                    case "ObtenerListaProvincias":
                        IList<Provincia> listadoProvincias = (List<Provincia>)servicioMantenimiento.ObtenerListaProvincias();
                        if (listadoProvincias.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProvincias);
                        break;
                    case "ObtenerListaCantones":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        IList<Canton> listadoCantones = (List<Canton>)servicioMantenimiento.ObtenerListaCantones(intIdProvincia);
                        if (listadoCantones.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCantones);
                        break;
                    case "ObtenerListaDistritos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        IList<Distrito> listadoDistritos = (List<Distrito>)servicioMantenimiento.ObtenerListaDistritos(intIdProvincia, intIdCanton);
                        if (listadoDistritos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDistritos);
                        break;
                    case "ObtenerListaBarrios":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                        IList<Barrio> listadoBarrios = (List<Barrio>)servicioMantenimiento.ObtenerListaBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                        if (listadoBarrios.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBarrios);
                        break;
                    case "AgregarEmpresa":
                        empresa = serializer.Deserialize<Empresa>(datos.DatosPeticion);
                        Empresa nuevoRegistro = servicioMantenimiento.AgregarEmpresa(empresa);
                        strRespuesta = nuevoRegistro.IdEmpresa.ToString();
                        break;
                    case "ObtenerListaDocumentosElectronicosPendientes":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoElectronico> listadoPendientes = servicioFacturacion.ObtenerListaDocumentosElectronicosPendientes(intIdEmpresa);
                        if (listadoPendientes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoPendientes);
                        break;
                    case "ObtenerListaDocumentosElectronicosEnviados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoElectronico> listadoEnviados = servicioFacturacion.ObtenerListaDocumentosElectronicosEnviados(intIdEmpresa);
                        if (listadoEnviados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEnviados);
                        break;
                    case "ObtenerTotalDocumentosElectronicosProcesados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intTotalDocumentosProcesados = servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa);
                        strRespuesta = intTotalDocumentosProcesados.ToString();
                        break;
                    case "ObtenerListaDocumentosElectronicosProcesados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intPagina = int.Parse(parametrosJO.Property("Pagina").Value.ToString());
                        int intCantDoc = int.Parse(parametrosJO.Property("Cantidad").Value.ToString());
                        IList<DocumentoElectronico> listadoProcesados = (List<DocumentoElectronico>)servicioFacturacion.ObtenerListaDocumentosElectronicosProcesados(intIdEmpresa, intPagina, intCantDoc);
                        if (listadoProcesados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProcesados);
                        break;
                    case "ObtenerDocumentoElectronico":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerDocumentoElectronico(intIdDocumento);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    case "ObtenerRespuestaDocumentoElectronicoEnviado":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerRespuestaDocumentoElectronicoEnviado(intIdDocumento, configuracion);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                log.Error("Error al procesar petición con respuesta: " + datos.NombreMetodo, ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            try
            {
                servicioFacturacion.ProcesarRespuestaHacienda(mensaje, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
            }
            catch (Exception ex)
            {
                log.Error("Error al recibir mensaje de respuesta de Hacienda: ", ex);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
