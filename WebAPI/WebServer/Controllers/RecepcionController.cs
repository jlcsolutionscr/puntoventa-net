using Microsoft.AspNetCore.Mvc;
using LeandroSoftware.ServicioWeb.Servicios;
using LeandroSoftware.ServicioWeb.EstructuraDatos;
using LeandroSoftware.ServicioWeb.Contexto;
using System.Text;
using Newtonsoft.Json;

namespace LeandroSoftware.ServicioWeb.WebServer.Controllers
{
    [ApiController]
    [Route("recepcion")]
    public class RecepcionController : ControllerBase
    {
        private static IFacturacionService _servicioFacturacion;
        private static string _strLogoPath;
        private static string _strCorreoNotificacionErrores;

        public RecepcionController(
            IConfiguration configuration,
            IHostEnvironment environment,
            IFacturacionService servicioFacturacion)
        {
            _strLogoPath = Path.Combine(environment.ContentRootPath, "images/Logo.png");
            _servicioFacturacion = servicioFacturacion;
            _strCorreoNotificacionErrores = configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value;
        }

        [HttpPost("recibirrespuestahacienda")]
        public void RecibirRespuestaHacienda()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string bodyStr = reader.ReadToEndAsync().Result;
            RespuestaHaciendaDTO mensaje = JsonConvert.DeserializeObject<RespuestaHaciendaDTO>(bodyStr);
            byte[] bytLogo = System.IO.File.ReadAllBytes(_strLogoPath);
            _servicioFacturacion.ProcesarRespuestaHacienda(mensaje, _strCorreoNotificacionErrores, bytLogo);
        }
    }
}