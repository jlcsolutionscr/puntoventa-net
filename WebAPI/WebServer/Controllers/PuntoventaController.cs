using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Servicios;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("puntoventa")]
    public class PuntoventaController : ControllerBase
    {
        private static IConfiguration _configuration;
        private static IHostEnvironment _environment;
        private static IMantenimientoService _servicioMantenimiento;
        private static IFacturacionService _servicioFacturacion;
        private static Empresa? empresa;
        private static int intIdEmpresa;
        private static int intIdSucursal;

        public PuntoventaController
        (
            IConfiguration configuration,
            IHostEnvironment environment,
            IMantenimientoService servicioMantenimiento,
            IFacturacionService servicioFacturacion
        )
        {
            _configuration = configuration;
            _environment = environment;
            _servicioMantenimiento = servicioMantenimiento;
            _servicioFacturacion = servicioFacturacion;
        }

        [HttpGet("obtenerultimaversionapp")]
        public string ObtenerUltimaVersionApp()
        {
            return _servicioMantenimiento.ObtenerUltimaVersionApp();
        }

        [HttpGet("obtenerlistadotiqueteordenserviciopendiente")]
        public string ObtenerListadoTiqueteOrdenServicioPendiente(int idempresa, int idsucursal)
        {
            IList<ClsTiquete> listadoTiqueteOrdenServicio = _servicioFacturacion.ObtenerListadoTiqueteOrdenServicio(intIdEmpresa, intIdSucursal, false, false);
            string strRespuesta = "";
            if (listadoTiqueteOrdenServicio.Count > 0)
                strRespuesta = JsonConvert.SerializeObject(listadoTiqueteOrdenServicio);
            return strRespuesta;
        }

        [HttpGet("cambiarestadoaimpresotiqueteordenservicio")]
        public void CambiarEstadoAImpresoTiqueteOrdenServicio(int idtiquete)
        {
            _servicioFacturacion.ActualizarEstadoTiqueteOrdenServicio(idtiquete, true);
        }

        [HttpGet("descargaractualizacion")]
        public FileStreamResult DescargarActualizacion()
        {
            string strVersion = _servicioMantenimiento.ObtenerUltimaVersionApp().Replace('.', '-');
            string downloadFilePath = Path.Combine(_environment.ContentRootPath, "appupdates/" + strVersion + "/puntoventaJLC.msi");
            FileStream fileStream = new FileStream(downloadFilePath, FileMode.Open, FileAccess.Read);
            Response.Headers.Add("content-disposition", "attachment; filename=puntoventaJLC.msi");
            return File(fileStream, "application/octet-stream");
        }

        [HttpGet("obtenerlistadoempresasadmin")]
        public string ObtenerListadoEmpresasAdministrador()
        {
            IList<LlaveDescripcion> listadoEmpresaAdministrador = _servicioMantenimiento.ObtenerListadoEmpresasAdministrador();
            string strRespuesta = "";
            if (listadoEmpresaAdministrador.Count > 0)
                strRespuesta = JsonConvert.SerializeObject(listadoEmpresaAdministrador);
            return strRespuesta;
        }

        [HttpGet("obtenerlistadoempresas")]
        public string ObtenerListadoEmpresasPorTerminal(string dispositivo)
        {
            IList<LlaveDescripcion> listadoEmpresaPorDispositivo = _servicioMantenimiento.ObtenerListadoEmpresasPorTerminal(dispositivo);
            string strRespuesta = "";
            if (listadoEmpresaPorDispositivo.Count > 0)
                strRespuesta = JsonConvert.SerializeObject(listadoEmpresaPorDispositivo);
            return strRespuesta;
        }

        [HttpGet("validarcredenciales")]
        public string ValidarCredenciales(string usuario, string clave, int idempresa, string dispositivo)
        {
            empresa = _servicioMantenimiento.ValidarCredenciales(usuario, clave, idempresa, dispositivo);
            string strRespuesta = "";
            if (empresa != null)
                strRespuesta = JsonConvert.SerializeObject(empresa);
            return strRespuesta;
        }

        [HttpGet("validarcredencialesweb")]
        public string ValidarCredencialesWeb(string usuario, string clave, string identificacion)
        {
            Empresa empresa = _servicioMantenimiento.ValidarCredenciales(usuario, clave, identificacion);
            string strRespuesta = "";
            if (empresa != null)
                strRespuesta = JsonConvert.SerializeObject(empresa);
            return strRespuesta;
        }

        [HttpGet("obtenerlistadoterminalesdisponibles")]
        public string ObtenerListadoTerminalesDisponibles(string usuario, string clave, string id, int tipodispositivo)
        {
            IList<EquipoRegistrado> listadoSucursales = (List<EquipoRegistrado>)_servicioMantenimiento.ObtenerListadoTerminalesDisponibles(usuario, clave, id, tipodispositivo);
            string strRespuesta = "";
            if (listadoSucursales.Count > 0)
                strRespuesta = JsonConvert.SerializeObject(listadoSucursales);
            return strRespuesta;
        }

        [HttpGet("registrarterminal")]
        public void RegistrarTerminal(string usuario, string clave, string id, int sucursal, int terminal, int tipodispositivo, string dispositivo)
        {
            _servicioMantenimiento.RegistrarTerminal(usuario, clave, id, sucursal, terminal, tipodispositivo, dispositivo);
        }

        [HttpGet("generarnotificacionrestablecerclaveusuario")]
        public void GenerarNotificacionRestablecerClaveUsuario(string identificacion, string usuario)
        {
            string strServicioWebURL = _configuration.GetSection("appSettings").GetSection("strServicioWebURL").Value;
            _servicioMantenimiento.GenerarNotificacionRestablecerClaveUsuario(strServicioWebURL, identificacion, usuario);
        }

        [HttpGet("restablecerclaveusuario")]
        public void RestablecerClaveUsuario(string session, string clave)
        {
            _servicioMantenimiento.RestablecerClaveUsuario(session, clave);
        }
    }
}