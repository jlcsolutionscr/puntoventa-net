namespace LeandroSoftware.ServicioWeb.Servicios
{
    public class ConfiguracionGeneral
    {
        public ConfiguracionGeneral(string strConsultaTipoDeCambioDolarURL, string strConsultaInformacionContribuyenteURL, string strComprobantesElectronicosURL, string strClientId, string strServicioTokenURL, string strComprobantesCallbackURL, string strCorreoNotificacionErrores)
        {
            ConsultaTipoDeCambioDolarURL = strConsultaTipoDeCambioDolarURL;
            ConsultaInformacionContribuyenteURL = strConsultaInformacionContribuyenteURL;
            ComprobantesElectronicosURL = strComprobantesElectronicosURL;
            ClientId = strClientId;
            ServicioTokenURL = strServicioTokenURL;
            CallbackURL = strComprobantesCallbackURL;
            CorreoNotificacionErrores = strCorreoNotificacionErrores;
        }

        public string ConsultaTipoDeCambioDolarURL { get; set; }
        public string ConsultaInformacionContribuyenteURL { get; set; }
        public string ComprobantesElectronicosURL { get; set; }
        public string ClientId { get; set; }
        public string ServicioTokenURL { get; set; }
        public string CallbackURL { get; set; }
        public string CorreoNotificacionErrores { get; set; }
    }
}
