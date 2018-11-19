namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    public class DatosConfiguracion
    {
        public DatosConfiguracion(string strConsultaIndicadoresEconomicosURL, string strOperacionSoap, string strComprobantesElectronicosURL, string strServicioTokenURL, string strComprobantesCallbackURL, string strCorreoNotificacionErrores)
        {
            ConsultaIndicadoresEconomicosURL = strConsultaIndicadoresEconomicosURL;
            OperacionSoap = strOperacionSoap;
            ComprobantesElectronicosURL = strComprobantesElectronicosURL;
            ServicioTokenURL = strServicioTokenURL;
            CallbackURL = strComprobantesCallbackURL;
            CorreoNotificacionErrores = strCorreoNotificacionErrores;
        }

        public string ConsultaIndicadoresEconomicosURL { get; set; }
        public string OperacionSoap { get; set; }
        public string ComprobantesElectronicosURL { get; set; }
        public string ServicioTokenURL { get; set; }
        public string CallbackURL { get; set; }
        public string CorreoNotificacionErrores { get; set; }
    }
}
