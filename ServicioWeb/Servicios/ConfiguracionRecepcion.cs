namespace LeandroSoftware.ServicioWeb.Servicios
{
    public class ConfiguracionRecepcion
    {
        public ConfiguracionRecepcion(string strCuentaIvaAcreditable, string strClaveIvaAcreditable, string strCuentaGastoNoAcreditable, string strClaveGastoNoAcreditable, string strCorreoNotificacionErrores, string strCorreoCuentaRecepcion)
        {
            CuentaIvaAcreditable = strCuentaIvaAcreditable;
            ClaveIvaAcreditable = strClaveIvaAcreditable;
            CuentaGastoNoAcreditable = strCuentaGastoNoAcreditable;
            ClaveGastoNoAcreditable = strClaveGastoNoAcreditable;
            CorreoNotificacionErrores = strCorreoNotificacionErrores;
            CorreoCuentaRecepcion = strCorreoCuentaRecepcion;
        }

        public string CuentaIvaAcreditable { get; set; }
        public string ClaveIvaAcreditable { get; set; }
        public string CuentaGastoNoAcreditable { get; set; }
        public string ClaveGastoNoAcreditable { get; set; }
        public string CorreoNotificacionErrores { get; set; }
        public string CorreoCuentaRecepcion { get; set; }
    }
}
