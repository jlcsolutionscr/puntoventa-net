using Microsoft.AspNetCore.Mvc;
using log4net;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.ServicioWeb.EstructuraDatos;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("recepcion")]
    public class RecepcionController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IFacturacionService _servicioFacturacion;
        private static string _strLogoPath;
        private static string _strCorreoNotificacionErrores;

        public RecepcionController(IConfiguration configuration, IHostEnvironment environment, IFacturacionService servicioFacturacion)
        {
            _strLogoPath = Path.Combine(environment.ContentRootPath, "images/Logo.png");
            _servicioFacturacion = servicioFacturacion;
            _strCorreoNotificacionErrores = configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value;
        }

        [HttpPost("recibirrespuestahacienda")]
        public void RecibirRespuestaHacienda([FromBody] RespuestaHaciendaDTO mensaje)
        {
            byte[] bytLogo = System.IO.File.ReadAllBytes(_strLogoPath);
            _servicioFacturacion.ProcesarRespuestaHacienda(mensaje, _strCorreoNotificacionErrores, bytLogo);
        }
    }
}