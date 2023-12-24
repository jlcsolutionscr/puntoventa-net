using Microsoft.Extensions.Configuration;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IConfiguracionRecepcion
    {
        public string CuentaIvaAcreditable { get; set; }
        public string ClaveIvaAcreditable { get; set; }
        public string CuentaGastoNoAcreditable { get; set; }
        public string ClaveGastoNoAcreditable { get; set; }
    }

    public class ConfiguracionRecepcion: IConfiguracionRecepcion
    {
        public ConfiguracionRecepcion(IConfiguration configuration)
        {
            CuentaIvaAcreditable = configuration.GetSection("appSettings").GetSection("pop3IvaAccount").Value;
            ClaveIvaAcreditable = configuration.GetSection("appSettings").GetSection("pop3IvaPass").Value;
            CuentaGastoNoAcreditable = configuration.GetSection("appSettings").GetSection("pop3GastoAccount").Value;
            ClaveGastoNoAcreditable = configuration.GetSection("appSettings").GetSection("pop3GastoPass").Value;
        }

        public string CuentaIvaAcreditable { get; set; }
        public string ClaveIvaAcreditable { get; set; }
        public string CuentaGastoNoAcreditable { get; set; }
        public string ClaveGastoNoAcreditable { get; set; }
    }
}
