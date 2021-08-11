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
using System.Threading.Tasks;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class AdministracionWCF : IAdministracionWCF, IDisposable
    {
        IUnityContainer unityContainer;
        private static ICorreoService servicioEnvioCorreo;
        private static IServerMailService servicioRecepcionCorreo;
        private IFacturacionService servicioFacturacion;
        private IMantenimientoService servicioMantenimiento;
        private IReporteService servicioReportes;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        
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
        private readonly ConfiguracionRecepcion configuracionRecepcion = new ConfiguracionRecepcion
        (
            appSettings["pop3IvaAccount"].ToString(),
            appSettings["pop3IvaPass"].ToString(),
            appSettings["pop3GastoAccount"].ToString(),
            appSettings["pop3GastoPass"].ToString()
        );

        public AdministracionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["smtpEmailHost"], appSettings["smtpEmailPort"], appSettings["smtpEmailAccount"], appSettings["smtpEmailPass"], appSettings["smtpSSLHost"]));
            unityContainer.RegisterType<IServerMailService, ServerMailService>(new InjectionConstructor(appSettings["pop3EmailHost"], appSettings["pop3EmailPort"]));
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            unityContainer.RegisterType<IReporteService, ReporteService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioRecepcionCorreo = unityContainer.Resolve<IServerMailService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioReportes = unityContainer.Resolve<IReporteService>();
        }

        public string ValidarCredencialesAdmin(string strUsuario, string strClave)
        {
            try
            {
                string strClaveFormateada = strClave.Replace(" ", "+");
                Usuario usuario = servicioMantenimiento.ValidarCredencialesAdmin(strUsuario, strClaveFormateada);
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

        public string ObtenerListadoSucursales(int intIdEmpresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoSucursales = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoSucursales(intIdEmpresa);
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

        public string ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal)
        {
            try
            {
                IList<LlaveDescripcion> listadoTerminales = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoTerminales(intIdEmpresa, intIdSucursal);
                string strRespuesta = "";
                if (listadoTerminales.Count > 0)
                    strRespuesta = serializer.Serialize(listadoTerminales);
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

        public string ObtenerLogotipoEmpresa(int intIdEmpresa)
        {
            try
            {
                string logotipo = servicioMantenimiento.ObtenerLogotipoEmpresa(intIdEmpresa);
                string strRespuesta = "";
                if (logotipo != null)
                    strRespuesta = serializer.Serialize(logotipo);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string ObtenerListadoReportePorEmpresa(int intIdEmpresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = servicioMantenimiento.ObtenerListadoReportePorEmpresa(intIdEmpresa);
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

        public string ObtenerListadoRolePorEmpresa(int intIdEmpresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = servicioMantenimiento.ObtenerListadoRolePorEmpresa(intIdEmpresa, true);
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

        public string ObtenerListadoRoles()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)servicioMantenimiento.ObtenerListadoRoles();
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

        public string ObtenerDatosReporte(int tipo, int idempresa, int idsucursal, string fechainicial, string fechafinal)
        {
            string strRespuesta = "";
            switch (tipo)
            {
                case 1:
                    {
                        List<ReporteDocumentoElectronico> listado = servicioReportes.ObtenerReporteDocumentosElectronicosEmitidos(idempresa, idsucursal, fechainicial, fechafinal);
                        if (listado.Count > 0)
                            strRespuesta = serializer.Serialize(listado);
                        break;
                    }
                case 2:
                    {
                        List<ReporteDocumentoElectronico> listado = servicioReportes.ObtenerReporteDocumentosElectronicosRecibidos(idempresa, idsucursal, fechainicial, fechafinal);
                        if (listado.Count > 0)
                            strRespuesta = serializer.Serialize(listado);
                        break;
                    }
                case 3:
                    {
                        List<ReporteResumenMovimiento> listado = servicioReportes.ObtenerReporteResumenDocumentosElectronicos(idempresa, idsucursal, fechainicial, fechafinal);
                        if (listado.Count > 0)
                            strRespuesta = serializer.Serialize(listado);
                        break;
                    }
            }
            return strRespuesta;
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
                servicioMantenimiento.ActualizarEmpresa(empresa);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarListadoReporte(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strListado = parametrosJO.Property("Datos").Value.ToString();
                List<ReportePorEmpresa> listado = serializer.Deserialize<List<ReportePorEmpresa>>(strListado);
                servicioMantenimiento.ActualizarReportePorEmpresa(intIdEmpresa, listado);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void ActualizarListadoRoles(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strListado = parametrosJO.Property("Datos").Value.ToString();
                List<RolePorEmpresa> listado = serializer.Deserialize<List<RolePorEmpresa>>(strListado);
                servicioMantenimiento.ActualizarRolePorEmpresa(intIdEmpresa, listado);
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

        public void EliminarRegistrosPorEmpresa(int intIdEmpresa)
        {
            try
            {
                servicioMantenimiento.EliminarRegistrosPorEmpresa(intIdEmpresa);
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
            Task.Run(() => RunSyncProcesarPendientes());
        }

        public void RunSyncProcesarPendientes()
        {
            try
            {
                using (UnityContainer singletonContainer = new UnityContainer())
                {
                    string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
                    singletonContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(connString));
                    singletonContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["smtpEmailHost"], appSettings["smtpEmailPort"], appSettings["smtpEmailAccount"], appSettings["smtpEmailPass"], appSettings["smtpSSLHost"]));
                    singletonContainer.RegisterType<IServerMailService, ServerMailService>(new InjectionConstructor(appSettings["pop3EmailHost"], appSettings["pop3EmailPort"]));
                    singletonContainer.RegisterType<IFacturacionService, FacturacionService>();
                    ICorreoService servicioEnvioCorreo = singletonContainer.Resolve<ICorreoService>();
                    IServerMailService servicioRecepcionCorreo = singletonContainer.Resolve<IServerMailService>();
                    IFacturacionService servicioFacturacion = singletonContainer.Resolve<IFacturacionService>();
                    servicioFacturacion.ProcesarDocumentosElectronicosPendientes(servicioEnvioCorreo, configuracionGeneral);
                    servicioFacturacion.ProcesarCorreoRecepcion(servicioEnvioCorreo, servicioRecepcionCorreo, configuracionGeneral, configuracionRecepcion);
                }
            }
            catch (Exception ex)
            {
                JArray jarrayObj = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { configuracionGeneral.CorreoNotificacionErrores }, new string[] { }, "Error en el procesamiento de documentos pendientes", "Ocurrio un error en el procesamiento de documentos pendientes: " + ex.Message, false, jarrayObj);
            }
        }

        public void LimpiarRegistrosInvalidos()
        {
            Task.Run(() => servicioMantenimiento.EliminarRegistroAutenticacionInvalidos());
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

        public string ObtenerListadoParametros()
        {
            try
            {
                IList<ParametroSistema> listadoEmpresas = servicioMantenimiento.ObtenerListadoParametros();
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

        public void ActualizarParametroSistema(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intId = int.Parse(parametrosJO.Property("IdParametro").Value.ToString());
                string strValor = parametrosJO.Property("Valor").Value.ToString();
                servicioMantenimiento.ActualizarParametroSistema(intId, strValor);
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
