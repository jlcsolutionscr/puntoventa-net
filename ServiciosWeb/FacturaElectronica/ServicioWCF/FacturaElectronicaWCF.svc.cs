using System;
using System.ServiceModel;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using LeandroSoftware.FacturaElectronicaHacienda.Servicios;
using LeandroSoftware.FacturaElectronicaHacienda.Datos;
using log4net;
using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Net;

namespace LeandroSoftware.FacturaElectronicaHacienda.ServicioWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FacturaElectronicaWCF : IFacturaElectronicaWCF, IDisposable
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

        public FacturaElectronicaWCF()
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

        public List<EmpresaDTO> ConsultarListadoEmpresas()
        {
            try
            {
                return servicioFacturElectronica.ConsultarListadoEmpresas();
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarListadoEmpresas': ", ex);
                throw ex;
            }
        }

        public EmpresaDTO ConsultarEmpresa(string empresa)
        {
            try
            {
                return servicioFacturElectronica.ConsultarEmpresa(empresa);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarEmpresa': ", ex);
                throw ex;
            }
        }

        public string RegistrarEmpresa(EmpresaDTO empresa)
        {
            try
            {
                return servicioFacturElectronica.RegistrarEmpresa(empresa);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'RegistrarEmpresa': ", ex);
                throw ex;
            }
        }

        public void RegistrarDocumentoElectronico(DatosDocumentoElectronicoDTO datos)
        {
            try
            {
                servicioFacturElectronica.RegistrarDocumentoElectronico(datos, configuracion, decTipoCambioDolar);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'RegistrarDocumentoElectronico': ", ex);
                throw ex;
            }
        }

        public void EnviarDocumentoElectronico(DatosDocumentoElectronicoDTO datos)
        {
            try
            {
                servicioFacturElectronica.EnviarDocumentoElectronico(datos, configuracion);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'EnviarDocumentoElectronico': ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.NotAcceptable);
            }
        }

        public DatosDocumentoElectronicoDTO ConsultarDocumentoElectronico(string empresa, string clave, string consecutivo)
        {
            try
            {
                return servicioFacturElectronica.ConsultarDocumentoElectronico(int.Parse(empresa), clave, consecutivo, configuracion);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarEstadoDocumentoElectronico': ", ex);
                throw ex;
            }
        }

        public List<DatosDocumentoElectronicoDTO> ConsultarListadoDocumentosElectronicos(string empresa, string estado)
        {
            try
            {
                return servicioFacturElectronica.ConsultarListadoDocumentosElectronicos(int.Parse(empresa), estado, configuracion);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarListadoComprobantesElectronicos': ", ex);
                throw ex;
            }
        }

        public void EnviarNotificacion(string empresa, string clave, string consecutivo)
        {
            try
            {
                servicioFacturElectronica.EnviarNotificacion(int.Parse(empresa), clave, consecutivo, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarEstadoDocumentoElectronico': ", ex);
                throw ex;
            }
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            try
            {
                servicioFacturElectronica.RecibirRespuestaHacienda(mensaje, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
            }
            catch (Exception ex)
            {
                log.Error("Error al recibir mensaje de respuesta de Hacienda: ", ex);
            }
        }

        public PadronDTO ConsultarPersonaPorIdentificacion(string id)
        {
            try
            {
                return servicioFacturElectronica.ConsultarPadronPorIdentificacion(id);
            }
            catch (Exception ex)
            {
                log.Error("Error al consumir endpoint 'ConsultarPersonaPorIdentificacion': ", ex);
                throw ex;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
