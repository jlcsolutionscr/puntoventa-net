using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.Core.CommonTypes;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LeandroSoftware.Core.Utilities;
using System.Collections.Generic;

namespace LeandroSoftware.Activator
{
    public static class ClienteWCF
    {
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
        private static string strApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
        private static HttpClient httpClient = new HttpClient();

        public static async Task<Usuario> ValidarCredenciales(string strUsuario, string strClave)
        {
            try
            {
                string strEncryptedPassword = Utilitario.EncriptarDatos(strClave, strApplicationKey);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/validarcredenciales?usuario=" + strUsuario + "&clave=" + strEncryptedPassword);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new Exception("No se logro establecer la comunicación con el servicio web.");
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                Usuario usuario = serializer.Deserialize<Usuario>(strResponse);
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresa(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoempresa");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("No se logro establecer la comunicación con el servicio web.");
                }
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                if (strResponse != "")
                    listado = serializer.Deserialize<List<LlaveDescripcion>>(strResponse);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}