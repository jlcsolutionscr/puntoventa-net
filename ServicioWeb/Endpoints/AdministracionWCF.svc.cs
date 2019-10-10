using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.Core.Servicios;
using LeandroSoftware.Core.CommonTypes;
using log4net;
using System;
using Newtonsoft.Json.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using System.IO;
using System.Web;
using System.Collections.Generic;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using System.Web.Script.Serialization;
using LeandroSoftware.ServicioWeb.Contexto;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class AdministracionWCF : IAdministracionWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IUnityContainer unityContainer;
        private static ICorreoService servicioEnvioCorreo;
        private IFacturacionService servicioFacturacion;
        private IMantenimientoService servicioMantenimiento;
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

        public AdministracionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            try
            {
                string strPath = HttpContext.Current.Server.MapPath("~");
                string[] directoryEntries = Directory.GetFileSystemEntries(strPath, "errorlog.txt??-??-????");

                foreach (string str in directoryEntries)
                {
                    JArray jarrayObj = new JArray();
                    byte[] bytes  = File.ReadAllBytes(str);
                    if (bytes.Length > 0)
                    {
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = str,
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioEnvioCorreo.SendEmail(new string[] { configuracion.CorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
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

        public string ValidarCredenciales(string strUsuario, string strClave)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                Usuario usuario = servicioMantenimiento.ValidarCredenciales(strUsuario, strClave);
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
                IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
                WebHeaderCollection headers = request.Headers;
                string strToken = headers["Authorization"];
                bool bolTokenValido = true;
                if (strToken != "") {
                    strToken = strToken.Substring(6);
                    bolTokenValido = servicioMantenimiento.ValidarRegistroAutenticacion(strToken, StaticRolePorUsuario.ADMINISTRADOR);
                }
                if (bolTokenValido)
                {
                    JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                    IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                    string strRespuesta = "";
                    if (listadoEmpresas.Count > 0)
                        strRespuesta = serializer.Serialize(listadoEmpresas);
                    return strRespuesta;
                } else
                {
                    return "";
                }
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
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
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

        public void ActualizarEmpresa(string strEmpresa)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                Empresa empresa = serializer.Deserialize<Empresa>(strEmpresa);
                servicioMantenimiento.ActualizarEmpresa(empresa);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
}

        public void ProcesarDocumentosPendientes()
        {
            servicioFacturacion.ProcesarDocumentosElectronicosPendientes(servicioEnvioCorreo, configuracion);
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

        public Stream DescargarActualizacion()
        {
            try
            {
                string strVersion = servicioMantenimiento.ObtenerUltimaVersionApp().Replace('.', '-');
                string downloadFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "appupdates/" + strVersion + "/puntoventaJLC.msi");
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                return File.OpenRead(downloadFilePath);
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
