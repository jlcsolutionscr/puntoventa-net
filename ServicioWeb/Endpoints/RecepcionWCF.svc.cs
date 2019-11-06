using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.Core.Servicios;
using LeandroSoftware.Core.TiposComunes;
using System;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class RecepcionWCF : IRecepcionWCF, IDisposable
    {
        IUnityContainer unityContainer;
        private static ICorreoService servicioEnvioCorreo;
        private IFacturacionService servicioFacturacion;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private static string strCorreoNotificacionErrores = appSettings["strCorreoNotificacionErrores"].ToString();
        private static string strCorreoEnvio = appSettings["facturaEmailFrom"].ToString();

        public RecepcionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["smtpEmailHost"], appSettings["smtpEmailPort"], appSettings["smtpEmailAccount"], appSettings["smtpEmailPass"], appSettings["smtpSSLHost"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            servicioFacturacion.ProcesarRespuestaHacienda(mensaje, servicioEnvioCorreo, strCorreoEnvio, strCorreoNotificacionErrores);
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
