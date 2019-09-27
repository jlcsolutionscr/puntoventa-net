using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Servicios;
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

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class RecepcionWCF : IRecepcionWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IUnityContainer unityContainer;
        private static ICorreoService servicioEnvioCorreo;
        private IFacturacionService servicioFacturacion;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private readonly string strCorreoNotificacionErrores = appSettings["strCorreoNotificacionErrores"].ToString();

        public RecepcionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
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
                        servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
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

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            servicioFacturacion.ProcesarRespuestaHacienda(mensaje, servicioEnvioCorreo, strCorreoNotificacionErrores);
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
