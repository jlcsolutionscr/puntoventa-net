using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("puntoventa")]
    public class PuntoventaController : ControllerBase
    {
        private static IHostEnvironment _environment;
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static ICorreoService _servicioCorreo;
        private static string _strCorreoNotificacionErrores;
        private static Empresa? empresa;
        private static int intIdEmpresa;
        private static int intIdSucursal;

        public PuntoventaController(
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
            _strCorreoNotificacionErrores = configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value;
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
                throw ex;
            }
        }

        [HttpGet("enviarhistoricoerrores")]
        public void EnviarHistoricoErrores()
        {
            try
            {
                string[] directoryEntries = Directory.GetFileSystemEntries(_environment.ContentRootPath, "errorlog-??-??-????.txt");
                foreach (string str in directoryEntries)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(str);
                    if (bytes.Length > 0)
                    {
                        JArray jarrayObj = new JArray();
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = str,
                            ["contenido"] = Convert.ToBase64String(bytes)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        _servicioCorreo.SendEmail(new string[] { _strCorreoNotificacionErrores }, new string[] { }, "Archivo log con errores de procesamiento", "Adjunto archivo con errores de procesamiento anteriores a la fecha actual.", false, jarrayObj);
                    }
                    System.IO.File.Delete(str);
                }
            }
            catch (Exception ex)
            {
                JArray jarrayObj = new JArray();
                _servicioCorreo.SendEmail(new string[] { _strCorreoNotificacionErrores }, new string[] { }, "Error al enviar el historico de archivo con errores", "Se produjo el siguiente error: " + ex.Message, false, jarrayObj);
            }
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
                throw ex;
            }
        }

        [HttpGet("obtenerlistadotiqueteordenserviciopendiente")]
        public string ObtenerListadoTiqueteOrdenServicioPendiente(int idempresa, int idsucursal)
        {
            try
            {
                IList<ClsTiquete> listadoTiqueteOrdenServicio = _servicioFacturacion.ObtenerListadoTiqueteOrdenServicio(intIdEmpresa, intIdSucursal, false, false);
                string strRespuesta = "";
                if (listadoTiqueteOrdenServicio.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoTiqueteOrdenServicio);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("cambiarestadoaimpresotiqueteordenservicio")]
        public void CambiarEstadoAImpresoTiqueteOrdenServicio(int idtiquete)
        {
            try
            {
                _servicioFacturacion.ActualizarEstadoTiqueteOrdenServicio(idtiquete, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("descargaractualizacion")]
        public Stream DescargarActualizacion()
        {
            try
            {
                string strVersion = _servicioMantenimiento.ObtenerUltimaVersionApp().Replace('.', '-');
                string downloadFilePath = Path.Combine(_environment.ContentRootPath, "appupdates/" + strVersion + "/puntoventaJLC.msi");
                FileStream content = System.IO.File.Open(downloadFilePath, FileMode.Open);
                return System.IO.File.OpenRead(downloadFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoempresasadministrador")]
        public string ObtenerListadoEmpresasAdministrador()
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresaAdministrador = _servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
                string strRespuesta = "";
                if (listadoEmpresaAdministrador.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresaAdministrador);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoempresas")]
        public string ObtenerListadoEmpresasPorTerminal(string dispositivo)
        {
            try
            {
                IList<LlaveDescripcion> listadoEmpresaPorDispositivo = _servicioMantenimiento.ObtenerListadoEmpresasPorTerminal(dispositivo);
                string strRespuesta = "";
                if (listadoEmpresaPorDispositivo.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoEmpresaPorDispositivo);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("validarcredenciales")]
        public string ValidarCredenciales(string usuario, string clave, int idempresa, string dispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                empresa = _servicioMantenimiento.ValidarCredenciales(usuario, strClaveFormateada, idempresa, dispositivo);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = JsonConvert.SerializeObject(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("validarcredencialesweb")]
        public string ValidarCredencialesWeb(string usuario, string clave, string identificacion)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                Empresa empresa = _servicioMantenimiento.ValidarCredenciales(usuario, strClaveFormateada, identificacion);
                string strRespuesta = "";
                if (empresa != null)
                    strRespuesta = JsonConvert.SerializeObject(empresa);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("obtenerlistadoterminalesdisponibles")]
        public string ObtenerListadoTerminalesDisponibles(string usuario, string clave, string id, int tipodispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                IList<EquipoRegistrado> listadoSucursales = (List<EquipoRegistrado>)_servicioMantenimiento.ObtenerListadoTerminalesDisponibles(usuario, strClaveFormateada, id, tipodispositivo);
                string strRespuesta = "";
                if (listadoSucursales.Count > 0)
                    strRespuesta = JsonConvert.SerializeObject(listadoSucursales);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("registrarterminal")]
        public void RegistrarTerminal(string usuario, string clave, string id, int sucursal, int terminal, int tipodispositivo, string dispositivo)
        {
            try
            {
                string strClaveFormateada = clave.Replace(" ", "+");
                _servicioMantenimiento.RegistrarTerminal(usuario, strClaveFormateada, id, sucursal, terminal, tipodispositivo, dispositivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}