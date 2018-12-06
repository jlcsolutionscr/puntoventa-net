using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Servicios;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core.Servicios;
using log4net;
using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class AccesoDatosWCF : IAccesoDatosWCF
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

        public AccesoDatosWCF()
        {
            unityContainer = new UnityContainer();
            ConnectionStringSettingsCollection connectionStrings = WebConfigurationManager.ConnectionStrings as ConnectionStringSettingsCollection;
            IEnumerator connectionStringsEnum = connectionStrings.GetEnumerator();
            int i = 0;
            string connString = "";
            while (connectionStringsEnum.MoveNext())
            {
                string name = connectionStrings[i].Name;
                if (name == "PuntoventaConn")
                    connString = connectionStrings[i].ConnectionString;
            }
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterInstance("configuracionData", configuracion, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
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

        public string ValidarCredenciales(string strUsuario, string strPassword, string strIdentificacion)
        {
            string strEmpresa = null;
            try
            {
                Empresa empresa = servicioMantenimiento.ObtenerEmpresaPorIdentificacion(strIdentificacion);
                if (empresa == null)
                {
                    throw new WebFaultException<string>("No existe registrada la empresa para la identificación ingresada.", HttpStatusCode.InternalServerError);
                }
                Usuario usuario = servicioMantenimiento.ValidarUsuario(empresa.IdEmpresa, strUsuario, strPassword, appSettings["ApplicationKey"]);
                var serializer = new DataContractJsonSerializer(typeof(Empresa));
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, empresa);
                    strEmpresa = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
            return strEmpresa;
        }

        public void Ejecutar(RequestDTO datos)
        {
            if (datos.NombreMetodo == "AgregarEmpresa")
            {
                Empresa empresa = null;
                var serializer = new DataContractJsonSerializer(typeof(Empresa));
                using (var ms = new MemoryStream(Convert.FromBase64String(datos.DatosPeticion)))
                {
                    empresa = (Empresa)serializer.ReadObject(ms);
                }
                servicioMantenimiento.AgregarEmpresa(empresa);
            }
        }

        public ResponseDTO EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                ResponseDTO respuesta = new ResponseDTO();
                if (datos.NombreMetodo == "ValidarCredenciales")
                {
                    Credenciales credenciales = null;
                    var serializer = new DataContractJsonSerializer(typeof(Credenciales));
                    using (var ms = new MemoryStream(Convert.FromBase64String(datos.DatosPeticion)))
                    {
                        credenciales = (Credenciales)serializer.ReadObject(ms);
                    }
                    string strEmpresa = null;
                    Empresa empresa = servicioMantenimiento.ObtenerEmpresaPorIdentificacion(credenciales.Identificacion);
                    if (empresa == null)
                    {
                        throw new Exception("No existe registrada la empresa para la identificación ingresada.");
                    }
                    Usuario usuario = servicioMantenimiento.ValidarUsuario(empresa.IdEmpresa, credenciales.Usuario, credenciales.Clave, appSettings["ApplicationKey"]);
                    serializer = new DataContractJsonSerializer(typeof(Empresa));
                    using (var ms = new MemoryStream())
                    {
                        serializer.WriteObject(ms, empresa);
                        strEmpresa = Convert.ToBase64String(ms.ToArray());
                    }
                    respuesta.ElementoSimple = false;
                    respuesta.DatosPeticion = strEmpresa;
                }
                if (datos.NombreMetodo == "AgregarEmpresa")
                {
                    Empresa empresa = null;
                    var serializer = new DataContractJsonSerializer(typeof(Empresa));
                    using (var ms = new MemoryStream(Convert.FromBase64String(datos.DatosPeticion)))
                    {
                        empresa = (Empresa)serializer.ReadObject(ms);
                    }
                    Empresa nuevoRegistro = servicioMantenimiento.AgregarEmpresa(empresa);
                    respuesta.ElementoSimple = true;
                    respuesta.DatosPeticion = nuevoRegistro.IdEmpresa.ToString();
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
