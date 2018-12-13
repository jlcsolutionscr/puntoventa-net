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
            try
            {
                ComprobanteElectronicoService.ObtenerTipoCambioVenta(configuracion.ConsultaIndicadoresEconomicosURL, configuracion.OperacionSoap, DateTime.Now, unityContainer);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw ex;
            }
        }

        public void Ejecutar(RequestDTO datos)
        {
            try
            {
                JObject parametrosJO;
                int intIdEmpresa;
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
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public ResponseDTO EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                ResponseDTO respuesta = new ResponseDTO();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Empresa empresa;
                JObject parametrosJO;
                int intIdProvincia;
                int intIdCanton;
                int intIdDistrito;
                switch (datos.NombreMetodo)
                {
                    case "ValidarCredenciales":
                        Credenciales credenciales = serializer.Deserialize<Credenciales>(datos.DatosPeticion);
                        empresa = servicioMantenimiento.ObtenerEmpresaPorIdentificacion(credenciales.Identificacion);
                        if (empresa == null)
                        {
                            throw new Exception("No existe registrada la empresa para la identificación ingresada.");
                        }
                        Usuario usuario = servicioMantenimiento.ValidarUsuario(empresa.IdEmpresa, credenciales.Usuario, credenciales.Clave, appSettings["ApplicationKey"]);
                        respuesta.ElementoSimple = false;
                        respuesta.DatosPeticion = serializer.Serialize(empresa);
                        break;
                    case "ObtenerListaEmpresas":
                        IList<Empresa> listadoEmpresa = (List<Empresa>)servicioMantenimiento.ObtenerListaEmpresas();
                        respuesta.ElementoSimple = false;
                        if (listadoEmpresa.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoEmpresa);
                        break;
                    case "ObtenerListaTipoIdentificacion":
                        IList<TipoIdentificacion> listadoTipoIdentificacion = (List<TipoIdentificacion>)servicioMantenimiento.ObtenerListaTipoIdentificacion();
                        respuesta.ElementoSimple = false;
                        if (listadoTipoIdentificacion.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoTipoIdentificacion);
                        break;
                    case "ObtenerListaProvincias":
                        IList<Provincia> listadoProvincias = (List<Provincia>)servicioMantenimiento.ObtenerListaProvincias();
                        respuesta.ElementoSimple = false;
                        if (listadoProvincias.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoProvincias);
                        break;
                    case "ObtenerListaCantones":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        IList<Canton> listadoCantones = (List<Canton>)servicioMantenimiento.ObtenerListaCantones(intIdProvincia);
                        respuesta.ElementoSimple = false;
                        if (listadoCantones.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoCantones);
                        break;
                    case "ObtenerListaDistritos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        IList<Distrito> listadoDistritos = (List<Distrito>)servicioMantenimiento.ObtenerListaDistritos(intIdProvincia, intIdCanton);
                        respuesta.ElementoSimple = false;
                        if (listadoDistritos.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoDistritos);
                        break;
                    case "ObtenerListaBarrios":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                        IList<Barrio> listadoBarrios = (List<Barrio>)servicioMantenimiento.ObtenerListaBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                        respuesta.ElementoSimple = false;
                        if (listadoBarrios.Count > 0)
                            respuesta.DatosPeticion = serializer.Serialize(listadoBarrios);
                        break;
                    case "ConsultarEmpresa":
                        empresa = servicioMantenimiento.ObtenerEmpresa(int.Parse(datos.DatosPeticion));
                        foreach (DetalleRegistro detalle in empresa.DetalleRegistro)
                            detalle.Empresa = null;
                        respuesta.ElementoSimple = false;
                        if (empresa != null)
                            respuesta.DatosPeticion = serializer.Serialize(empresa);
                        break;
                    case "AgregarEmpresa":
                        empresa = serializer.Deserialize<Empresa>(datos.DatosPeticion);
                        Empresa nuevoRegistro = servicioMantenimiento.AgregarEmpresa(empresa);
                        respuesta.ElementoSimple = true;
                        respuesta.DatosPeticion = nuevoRegistro.IdEmpresa.ToString();
                        break;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
