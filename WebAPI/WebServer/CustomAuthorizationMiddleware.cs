using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.Seguridad;
using LeandroSoftware.ServicioWeb.Servicios;

namespace WebServer.Middlewares
{
    public class CustomAuthorizationMiddleware
    {
        private string[] adminEndpoints = new string[] {
            "obtenerultimarversionapp",
            "recibirrespuestahacienda",
            "generarnotificacionrestablecerclaveusuario",
            "restablecerclaveusuario",
            "autorizarcorreousuario",
            "limpiarregistrosinvalidos",
            "obtenerultimaversionapp",
            "obtenerlistadotiqueteordenserviciopendiente",
            "cambiarestadoaimpresotiqueteordenservicio",
            "descargaractualizacion",
            "obtenerlistadoempresasadmin",
            "obtenerlistadoempresas",
            "validarcredenciales",
            "validarcredencialesweb",
            "validarcredencialesadmin",
            "obtenerlistadoterminalesdisponibles",
            "registrarterminal",
            "procesarpendientes",
            "validarregistroautenticacion"
        };
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, IHostEnvironment environment, IMantenimientoService servicioMantenimiento
        )
        {
            return BeginInvoke(context, environment, servicioMantenimiento);
        }

        private Task BeginInvoke(HttpContext context, IHostEnvironment environment, IMantenimientoService servicioMantenimiento
        )
        {
            if (environment.IsProduction() && !context.Request.IsHttps) {
                throw new Exception("La petición no se encuentra en un protocolo seguro y no es posible procesar su solicitud");
            }
            string[] strPath = context.Request.Path.ToString().Substring(1).Split('/');
            if (strPath.Length > 1)
            {
                if (strPath[0] == "puntoventa")
                {
                    bool modoMantenimiento = servicioMantenimiento.EnModoMantenimiento();
                    if (modoMantenimiento) throw new Exception("El sistema se encuentra en modo mantenimiento y no es posible procesar su solicitud.");
                }
                if (!adminEndpoints.Contains(strPath[1]))
                {
                    var headers = context.Request.Headers;
                    string strToken = headers["Authorization"];
                    if (strToken == null) throw new Exception("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    strToken = strToken.Substring(7);
                    string strTokenDesencriptado = Encriptador.DesencriptarDatos(strToken);
                    servicioMantenimiento.ValidarRegistroAutenticacion(strTokenDesencriptado, StaticRolePorUsuario.USUARIO_SISTEMA, 12);
                }
            }
            else throw new Exception("La petición solicitada no existe");
            return _next.Invoke(context);
        }
    }

    public static class CustomAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorizationMiddleware>();
        }
    }
}
