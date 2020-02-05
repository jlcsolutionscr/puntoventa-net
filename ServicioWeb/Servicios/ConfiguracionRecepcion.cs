namespace LeandroSoftware.ServicioWeb.Servicios
{
    public class ConfiguracionRecepcion
    {
        public ConfiguracionRecepcion(string strCuentaIvaAcreditable, string strClaveIvaAcreditable, string strCuentaGastoNoAcreditable, string strClaveGastoNoAcreditable)
        {
            CuentaIvaAcreditable = strCuentaIvaAcreditable;
            ClaveIvaAcreditable = strClaveIvaAcreditable;
            CuentaGastoNoAcreditable = strCuentaGastoNoAcreditable;
            ClaveGastoNoAcreditable = strClaveGastoNoAcreditable;
        }

        public string CuentaIvaAcreditable { get; set; }
        public string ClaveIvaAcreditable { get; set; }
        public string CuentaGastoNoAcreditable { get; set; }
        public string ClaveGastoNoAcreditable { get; set; }
    }
}
