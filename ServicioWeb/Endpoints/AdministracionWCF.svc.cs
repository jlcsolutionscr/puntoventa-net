using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.Core.Servicios;
using LeandroSoftware.Core.TiposComunes;
using log4net;
using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.ServicioWeb.Contexto;
using Unity;
using Unity.Lifetime;
using Unity.Injection;
using System.Web.Configuration;
using System.Web;
using System.ServiceModel.Web;
using System.Net;
using System.Web.Script.Serialization;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class AdministracionWCF : IAdministracionWCF, IDisposable
    {
        IUnityContainer unityContainer;
        private static ICorreoService servicioEnvioCorreo;
        private static IServerMailService servicioRecepcionCorreo;
        private IFacturacionService servicioFacturacion;
        private IMantenimientoService servicioMantenimiento;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private readonly DatosConfiguracion configuracion = new DatosConfiguracion
        (
            appSettings["strConsultaIEURL"].ToString(),
            appSettings["strSoapOperation"].ToString(),
            appSettings["strServicioComprobantesURL"].ToString(),
            appSettings["strClientId"].ToString(),
            appSettings["strServicioTokenURL"].ToString(),
            appSettings["strComprobantesCallbackURL"].ToString(),
            appSettings["strCorreoNotificacionErrores"].ToString(),
            appSettings["facturaEmailFrom"].ToString(),
            appSettings["recepcionEmailFrom"].ToString()
        );
        private static string strApplicationKey = appSettings["ApplicationKey"].ToString();

        public AdministracionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["smtpEmailHost"], appSettings["smtpEmailPort"], appSettings["smtpEmailAccount"], appSettings["smtpEmailPass"], appSettings["smtpSSLHost"]));
            unityContainer.RegisterType<IServerMailService, ServerMailService>(new InjectionConstructor(appSettings["pop3EmailHost"], appSettings["pop3EmailPort"], appSettings["pop3EmailAccount"], appSettings["pop3EmailPass"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioRecepcionCorreo = unityContainer.Resolve<IServerMailService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
        }

        public string ValidarCredenciales(string strUsuario, string strClave)
        {
            try
            {
                Usuario usuario = servicioMantenimiento.ValidarCredenciales(strUsuario, strClave, strApplicationKey);
                string strRespuesta = "";
                if (usuario != null)
                    strRespuesta = serializer.Serialize(usuario);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoEmpresa()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerEmpresa(int intIdEmpresa)
        {
            try
            {
                Empresa empresa = servicioMantenimiento.ObtenerEmpresa(intIdEmpresa);
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

        public string ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal)
        {
            try
            {
                SucursalPorEmpresa sucursal = servicioMantenimiento.ObtenerSucursalPorEmpresa(intIdEmpresa, intIdSucursal);
                string strRespuesta = "";
                if (sucursal != null)
                    strRespuesta = serializer.Serialize(sucursal);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {
            try
            {
                TerminalPorSucursal terminal = servicioMantenimiento.ObtenerTerminalPorSucursal(intIdEmpresa, intIdSucursal, intIdTerminal);
                string strRespuesta = "";
                if (terminal != null)
                    strRespuesta = serializer.Serialize(terminal);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoTipoIdentificacion()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTipoIdentificacion();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoCatalogoReportes()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoCatalogoReportes();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoProvincias()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoProvincias();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoCantones(int intIdProvincia)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoCantones(intIdProvincia);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoDistritos(intIdProvincia, intIdCanton);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string AgregarEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                Empresa empresa = serializer.Deserialize<Empresa>(strEntidad);
                string strIdEmpresa = servicioMantenimiento.AgregarEmpresa(empresa);
                return strIdEmpresa;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                Empresa empresa = serializer.Deserialize<Empresa>(strEntidad);
                servicioMantenimiento.ActualizarEmpresaConDetalle(empresa);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarLogoEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strLogotipo = parametrosJO.Property("Datos").Value.ToString();
                servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void RemoverLogoEmpresa(int intIdEmpresa)
        {
            try
            {
                servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, "");
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarCertificadoEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strCertificado = parametrosJO.Property("Datos").Value.ToString();
                servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void AgregarSucursalPorEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                SucursalPorEmpresa sucursal = serializer.Deserialize<SucursalPorEmpresa>(strEntidad);
                servicioMantenimiento.AgregarSucursalPorEmpresa(sucursal);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
            
        }

        public void ActualizarSucursalPorEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                SucursalPorEmpresa sucursal = serializer.Deserialize<SucursalPorEmpresa>(strEntidad);
                servicioMantenimiento.ActualizarSucursalPorEmpresa(sucursal);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void AgregarTerminalPorSucursal(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                TerminalPorSucursal terminal = serializer.Deserialize<TerminalPorSucursal>(strEntidad);
                servicioMantenimiento.AgregarTerminalPorSucursal(terminal);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarTerminalPorSucursal(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                TerminalPorSucursal terminal = serializer.Deserialize<TerminalPorSucursal>(strEntidad);
                servicioMantenimiento.ActualizarTerminalPorSucursal(terminal);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoDocumentosElectronicosPendientes()
        {
            try
            {
                IList<DocumentoDetalle> listadoEmpresas = (List<DocumentoDetalle>)servicioFacturacion.ObtenerListadoDocumentosElectronicosPendientes();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = serializer.Serialize(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ProcesarDocumentosElectronicosPendientes()
        {
            servicioFacturacion.ProcesarDocumentosElectronicosPendientes(servicioEnvioCorreo, servicioRecepcionCorreo, configuracion);
        }

        public void LimpiarRegistrosInvalidos()
        {
            servicioMantenimiento.EliminarRegistroAutenticacionInvalidos();
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

        public void ActualizarArchivoAplicacion(Stream fileStream)
        {
            try
            {
                byte[] bytContenido;
                using (MemoryStream ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    bytContenido = ms.ToArray();
                }
                string strUpdateAppPath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "appupdates");
                string[] strVersionArray = servicioMantenimiento.ObtenerUltimaVersionApp().Split('.');
                string strNewFolderPath = Path.Combine(strUpdateAppPath, strVersionArray[0] + "-" + strVersionArray[1] + "-" + strVersionArray[2] + "-" + strVersionArray[3]);
                Directory.CreateDirectory(strNewFolderPath);
                string strFilePath = Path.Combine(strNewFolderPath, "puntoventaJLC.msi");
                File.WriteAllBytes(strFilePath, bytContenido);
                foreach (string strSubDirPath in Directory.GetDirectories(strUpdateAppPath))
                {
                    if (strNewFolderPath != strSubDirPath)
                    {
                        DirectoryInfo appDirectoryInfo = new DirectoryInfo(strSubDirPath);
                        foreach (FileInfo file in appDirectoryInfo.GetFiles())
                            file.Delete();
                        Directory.Delete(strSubDirPath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarVersionApp(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strVersion = parametrosJO.Property("Version").Value.ToString();
                servicioMantenimiento.ActualizarVersionApp(strVersion);
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
