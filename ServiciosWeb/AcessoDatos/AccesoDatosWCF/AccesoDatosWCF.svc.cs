using LeandroSoftware.Core.Servicios;
using log4net;
using System;
using System.Web.Configuration;
using Unity;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class AccesoDatosWCF : IAccesoDatosWCF
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IFacturaElectronicaService servicioFacturElectronica;
        private ICorreoService servicioEnvioCorreo;
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
        private decimal decTipoCambioDolar;

        public AccesoDatosWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, DatabaseContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<IFacturaElectronicaService, FacturaElectronicaService>();
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            servicioFacturElectronica = unityContainer.Resolve<IFacturaElectronicaService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            try
            {
                decTipoCambioDolar = servicioFacturElectronica.ObtenerTipoCambioVenta(configuracion.ConsultaIndicadoresEconomicosURL, configuracion.OperacionSoap, DateTime.Now);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw ex;
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
