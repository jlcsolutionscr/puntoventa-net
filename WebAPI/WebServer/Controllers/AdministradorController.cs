using Microsoft.AspNetCore.Mvc;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.Common.DatosComunes;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("administrador")]
    public class AdministradorController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IHostEnvironment _environment;
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static ICorreoService _servicioCorreo;
        private static ConfiguracionGeneral configuracionGeneral;
        private static ConfiguracionRecepcion configuracionRecepcion;
        private string strLogoPath;
        private byte[] bytLogo;

        public AdministradorController(
            IConfiguration configuration,
            IHostEnvironment environment,
            IMantenimientoService servicioMantenimiento,
            IFacturacionService servicioFacturacion,
            ICorreoService servicioCorreo
        )
        {
            _environment = environment;
            _servicioMantenimiento = servicioMantenimiento;
            _servicioFacturacion = servicioFacturacion;
            _servicioCorreo = servicioCorreo;
            strLogoPath = Path.Combine(environment.ContentRootPath, "images/Logo.png");
            configuracionGeneral = new ConfiguracionGeneral
            (
                configuration.GetSection("appSettings").GetSection("strConsultaIEURL").Value,
                configuration.GetSection("appSettings").GetSection("strSoapOperation").Value,
                configuration.GetSection("appSettings").GetSection("strServicioComprobantesURL").Value,
                configuration.GetSection("appSettings").GetSection("strClientId").Value,
                configuration.GetSection("appSettings").GetSection("strServicioTokenURL").Value,
                configuration.GetSection("appSettings").GetSection("strComprobantesCallbackURL").Value,
                configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value
            );
            configuracionRecepcion = new ConfiguracionRecepcion
            (
                configuration.GetSection("appSettings").GetSection("pop3IvaAccount").Value,
                configuration.GetSection("appSettings").GetSection("pop3IvaPass").Value,
                configuration.GetSection("appSettings").GetSection("pop3GastoAccount").Value,
                configuration.GetSection("appSettings").GetSection("pop3GastoPass").Value
            );
        }

        [HttpPost("actualizararchivoaplicacion")]
        public void ActualizarArchivoAplicacion([FromBody] Stream fileStream)
        {
            byte[] bytContenido;
            using (MemoryStream ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                bytContenido = ms.ToArray();
            }
            string strUpdateAppPath = Path.Combine(_environment.ContentRootPath, "appupdates");
            string[] strVersionArray = _servicioMantenimiento.ObtenerUltimaVersionApp().Split('.');
            string strNewFolderPath = Path.Combine(strUpdateAppPath, strVersionArray[0] + "-" + strVersionArray[1] + "-" + strVersionArray[2] + "-" + strVersionArray[3]);
            Directory.CreateDirectory(strNewFolderPath);
            string strFilePath = Path.Combine(strNewFolderPath, "puntoventaJLC.msi");
            System.IO.File.WriteAllBytes(strFilePath, bytContenido);
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

        [HttpGet("validarcredencialesadmin")]
        public string ValidarCredencialesAdmin(string usuario, string clave)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                Usuario usuarioEntity = _servicioMantenimiento.ValidarCredencialesAdmin(usuario, strClaveFormateada);
                string strRespuesta = "";
                if (usuarioEntity != null)
                    strRespuesta = JsonConvert.SerializeObject(usuarioEntity);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoempresa")]
        public string ObtenerListadoEmpresa()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadosucursales")]
        public string ObtenerListadoSucursales(int idempresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoSucursales = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoSucursales(idempresa);
                string strRespuesta = "";
                if (listadoSucursales.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoSucursales);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoterminales")]
        public string ObtenerListadoTerminales(int idempresa, int idsucursal)
        {
            try
            {
                IList<LlaveDescripcion> listadoTerminales = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoTerminales(idempresa, idsucursal);
                string strRespuesta = "";
                if (listadoTerminales.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoTerminales);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerempresa")]
        public string ObtenerEmpresa(int idempresa)
        {
            try
            {
                Empresa empresa = _servicioMantenimiento.ObtenerEmpresa(idempresa);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = JsonConvert.SerializeObject(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlogotipoempresa")]
        public string ObtenerLogotipoEmpresa(int idempresa)
        {
            try
            {
                string logotipo = _servicioMantenimiento.ObtenerLogotipoEmpresa(idempresa);
                string strRespuesta = "";
                if (logotipo != null)
                    strRespuesta = JsonConvert.SerializeObject(logotipo);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoreporteporempresa")]
        public string ObtenerListadoReportePorEmpresa(int idempresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = _servicioMantenimiento.ObtenerListadoReportePorEmpresa(idempresa);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoroleporempresa")]
        public string ObtenerListadoRolePorEmpresa(int idempresa)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = _servicioMantenimiento.ObtenerListadoRolePorEmpresa(idempresa, true);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenersucursalporempresa")]
        public string ObtenerSucursalPorEmpresa(int idempresa, int idsucursal)
        {
            try
            {
                SucursalPorEmpresa sucursal = _servicioMantenimiento.ObtenerSucursalPorEmpresa(idempresa, idsucursal);
                string strRespuesta = "";
                if (sucursal != null)
                    strRespuesta = JsonConvert.SerializeObject(sucursal);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerterminalporsucursal")]
        public string ObtenerTerminalPorSucursal(int idempresa, int idsucursal, int idterminal)
        {
            try
            {
                TerminalPorSucursal terminal = _servicioMantenimiento.ObtenerTerminalPorSucursal(idempresa, idsucursal, idterminal);
                string strRespuesta = "";
                if (terminal != null)
                    strRespuesta = JsonConvert.SerializeObject(terminal);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadotipoidentificacion")]
        public string ObtenerListadoTipoIdentificacion()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoTipoIdentificacion();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadocatalogoreportes")]
        public string ObtenerListadoCatalogoReportes()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoCatalogoReportes();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoroles")]
        public string ObtenerListadoRoles()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoRoles();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoprovincias")]
        public string ObtenerListadoProvincias()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoProvincias();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadocantones")]
        public string ObtenerListadoCantones(int idprovincia)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoCantones(idprovincia);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadodistritos")]
        public string ObtenerListadoDistritos(int idprovincia, int idcanton)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoDistritos(idprovincia, idcanton);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadobarrios")]
        public string ObtenerListadoBarrios(int idprovincia, int idcanton, int iddistrito)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresas = (List<LlaveDescripcion>)_servicioMantenimiento.ObtenerListadoBarrios(idprovincia, idcanton, iddistrito);
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("agregarempresa")]
        public string AgregarEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                Empresa empresa = JsonConvert.DeserializeObject<Empresa>(strEntidad);
                string strIdEmpresa = _servicioMantenimiento.AgregarEmpresa(empresa);
                return strIdEmpresa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarempresa")]
        public void ActualizarEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                Empresa empresa = JsonConvert.DeserializeObject<Empresa>(strEntidad);
                _servicioMantenimiento.ActualizarEmpresa(empresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarlistadoreportes")]
        public void ActualizarListadoReporte(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strListado = parametrosJO.Property("Datos").Value.ToString();
                List<ReportePorEmpresa> listado = JsonConvert.DeserializeObject<List<ReportePorEmpresa>>(strListado);
                _servicioMantenimiento.ActualizarReportePorEmpresa(intIdEmpresa, listado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarlistadoroles")]
        public void ActualizarListadoRoles(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strListado = parametrosJO.Property("Datos").Value.ToString();
                List<RolePorEmpresa> listado = JsonConvert.DeserializeObject<List<RolePorEmpresa>>(strListado);
                _servicioMantenimiento.ActualizarRolePorEmpresa(intIdEmpresa, listado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarlogoempresa")]
        public void ActualizarLogoEmpresa(string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strLogotipo = parametrosJO.Property("Datos").Value.ToString();
                _servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("removerlogoempresa")]
        public void RemoverLogoEmpresa(int idempresa)
        {
            try
            {
                _servicioMantenimiento.ActualizarLogoEmpresa(idempresa, "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarcertificadoempresa")]
        public void ActualizarCertificadoEmpresa([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intIdEmpresa = int.Parse(parametrosJO.Property("Id").Value.ToString());
                string strCertificado = parametrosJO.Property("Datos").Value.ToString();
                _servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("agregarsucursalporempresa")]
        public void AgregarSucursalPorEmpresa([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                SucursalPorEmpresa sucursal = JsonConvert.DeserializeObject<SucursalPorEmpresa>(strEntidad);
                _servicioMantenimiento.AgregarSucursalPorEmpresa(sucursal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("actualizarsucursalporempresa")]
        public void ActualizarSucursalPorEmpresa([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                SucursalPorEmpresa sucursal = JsonConvert.DeserializeObject<SucursalPorEmpresa>(strEntidad);
                _servicioMantenimiento.ActualizarSucursalPorEmpresa(sucursal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("agregarterminalporsucursal")]
        public void AgregarTerminalPorSucursal([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                TerminalPorSucursal terminal = JsonConvert.DeserializeObject<TerminalPorSucursal>(strEntidad);
                _servicioMantenimiento.AgregarTerminalPorSucursal(terminal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarterminalporsucursal")]
        public void ActualizarTerminalPorSucursal([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                string strEntidad = parametrosJO.Property("Entidad").Value.ToString();
                TerminalPorSucursal terminal = JsonConvert.DeserializeObject<TerminalPorSucursal>(strEntidad);
                _servicioMantenimiento.ActualizarTerminalPorSucursal(terminal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("eliminarregistrosporempresa")]
        public void EliminarRegistrosPorEmpresa(int idempresa)
        {
            try
            {
                _servicioMantenimiento.EliminarRegistrosPorEmpresa(idempresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadodocumentospendientes")]
        public string ObtenerListadoDocumentosElectronicosPendientes()
        {
            try
            {
                IList<DocumentoDetalle> listadoEmpresas = (List<DocumentoDetalle>)_servicioFacturacion.ObtenerListadoDocumentosElectronicosPendientes();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("procesarpendientes")]
        public void ProcesarDocumentosElectronicosPendientes()
        {
            Task.Run(() => RunSyncProcesarPendientes());
        }

        private void RunSyncProcesarPendientes()
        {
            try
            {
                bytLogo = System.IO.File.ReadAllBytes(strLogoPath);
                _servicioFacturacion.ProcesarDocumentosElectronicosPendientes(configuracionGeneral, bytLogo);
                _servicioFacturacion.ProcesarCorreoRecepcion(configuracionGeneral, configuracionRecepcion);
            }
            catch (Exception ex)
            {
                JArray jarrayObj = new JArray();
                _servicioCorreo.SendEmail(new string[] { configuracionGeneral.CorreoNotificacionErrores }, new string[] { }, "Error en el procesamiento de documentos pendientes", "Ocurrio un error en el procesamiento de documentos pendientes: " + ex.Message, false, jarrayObj);
            }
        }

        [HttpGet("limpiarregistrosinvalidos")]
        public void LimpiarRegistrosInvalidos()
        {
            Task.Run(() => _servicioMantenimiento.EliminarRegistroAutenticacionInvalidos());
        }

        [HttpGet("obtenerultimaversionapp")]
        public string ObtenerUltimaVersionApp()
        {
            try
            {
                return _servicioMantenimiento.ObtenerUltimaVersionApp();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("obtenerlistadoparametros")]
        public string ObtenerListadoParametros()
        {
            try
            {
                IList<ParametroSistema> listadoEmpresas = _servicioMantenimiento.ObtenerListadoParametros();
                string strRespuesta = "";
                if (listadoEmpresas.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresas);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("actualizarparametrosistema")]
        public void ActualizarParametroSistema([FromBody] string strDatos)
        {
            try
            {
                JObject parametrosJO = JObject.Parse(strDatos);
                int intId = int.Parse(parametrosJO.Property("IdParametro").Value.ToString());
                string strValor = parametrosJO.Property("Valor").Value.ToString();
                _servicioMantenimiento.ActualizarParametroSistema(intId, strValor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}