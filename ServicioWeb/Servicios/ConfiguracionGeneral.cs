﻿namespace LeandroSoftware.ServicioWeb.Servicios
{
    public class ConfiguracionGeneral
    {
        public ConfiguracionGeneral(string strConsultaIndicadoresEconomicosURL, string strOperacionSoap, string strComprobantesElectronicosURL, string strClientId, string strServicioTokenURL, string strComprobantesCallbackURL, string strCorreoNotificacionErrores, string strCorreoCuentaFacturacion)
        {
            ConsultaIndicadoresEconomicosURL = strConsultaIndicadoresEconomicosURL;
            OperacionSoap = strOperacionSoap;
            ComprobantesElectronicosURL = strComprobantesElectronicosURL;
            ClientId = strClientId;
            ServicioTokenURL = strServicioTokenURL;
            CallbackURL = strComprobantesCallbackURL;
            CorreoNotificacionErrores = strCorreoNotificacionErrores;
            CorreoCuentaFacturacion = strCorreoCuentaFacturacion;
        }

        public string ConsultaIndicadoresEconomicosURL { get; set; }
        public string OperacionSoap { get; set; }
        public string ComprobantesElectronicosURL { get; set; }
        public string ClientId { get; set; }
        public string ServicioTokenURL { get; set; }
        public string CallbackURL { get; set; }
        public string CorreoNotificacionErrores { get; set; }
        public string CorreoCuentaFacturacion { get; set; }
    }
}