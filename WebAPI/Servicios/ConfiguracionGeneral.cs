using Microsoft.Extensions.Configuration;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IConfiguracionGeneral
    {
        public string ConsultaTipoDeCambioDolarURL { get; set; }
        public string ConsultaInformacionContribuyenteURL { get; set; }
        public string ComprobantesElectronicosURL { get; set; }
        public string ClientId { get; set; }
        public string ServicioTokenURL { get; set; }
        public string ServicioWebURL { get; set; }
        public string CallbackURL { get; set; }
        public string CorreoNotificacionErrores { get; set; }
        public bool EsModoDesarrollo { get; set; }
    }
    public class ConfiguracionGeneral: IConfiguracionGeneral
    {
        public ConfiguracionGeneral(IConfiguration configuration)
        {
            ConsultaTipoDeCambioDolarURL = configuration.GetSection("appSettings").GetSection("strConsultaTipoCambioDolarURL").Value;
            ConsultaInformacionContribuyenteURL = configuration.GetSection("appSettings").GetSection("strConsultaContribuyenteURL").Value;
            ComprobantesElectronicosURL = configuration.GetSection("appSettings").GetSection("strServicioComprobantesURL").Value;
            ClientId = configuration.GetSection("appSettings").GetSection("strClientId").Value;
            ServicioTokenURL = configuration.GetSection("appSettings").GetSection("strServicioTokenURL").Value;
            ServicioWebURL = configuration.GetSection("appSettings").GetSection("strServicioWebURL").Value;
            CallbackURL = configuration.GetSection("appSettings").GetSection("strComprobantesCallbackURL").Value;
            CorreoNotificacionErrores = configuration.GetSection("appSettings").GetSection("strCorreoNotificacionErrores").Value;
            EsModoDesarrollo = configuration.GetSection("appSettings").GetSection("strEnvironment").Value == "development";
        }

        public string ConsultaTipoDeCambioDolarURL { get; set; }
        public string ConsultaInformacionContribuyenteURL { get; set; }
        public string ComprobantesElectronicosURL { get; set; }
        public string ClientId { get; set; }
        public string ServicioTokenURL { get; set; }
        public string ServicioWebURL { get; set; }
        public string CallbackURL { get; set; }
        public string CorreoNotificacionErrores { get; set; }
        public bool EsModoDesarrollo { get; set; }
    }
}
