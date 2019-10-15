using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.Core.TiposComunes;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LeandroSoftware.Core.Utilities;
using System.Collections.Generic;
using System.Text;

namespace LeandroSoftware.ClienteWCF
{
    public static class Administrador
    {
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
        private static string strApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
        private static HttpClient httpClient = new HttpClient();

        public static StringContent serializarEntidad<T>(T entidad)
        {
            string strEntidad = serializer.Serialize("{Entidad: " + serializer.Serialize(entidad) + "}");
            StringContent contentJson = new StringContent(strEntidad, Encoding.UTF8, "application/json");
            return contentJson;
        }

        public static StringContent serializarDatosConId(int id, string strDatos)
        {
            string strEntidad = serializer.Serialize("{Id: " + id + ", Datos: '" + strDatos + "'}");
            StringContent contentJson = new StringContent(strEntidad, Encoding.UTF8, "application/json");
            return contentJson;
        }

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerultimaversionapp");
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string response = serializer.Deserialize<string>(responseContent);
            return response;
        }

        public static async Task ActualizarVersionApp(string strVersion, byte[] bytZipFile, string strToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string strDatos = serializer.Serialize("{Version: '" + strVersion + "'}");
            StringContent contentJson = new StringContent(strDatos, Encoding.UTF8, "application/json");
            if (strToken != "")
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
            HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarversionapp", contentJson);
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/octet-stream";
            client.Headers[HttpRequestHeader.Authorization] = "bearer " + strToken;
            client.UploadData(strServicioURL + "/actualizararchivoaplicacion", bytZipFile);
        }

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
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerempresa?idempresa=" + intIdEmpresa);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                Empresa empresa = null;
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                if (strResponse != "")
                    empresa = serializer.Deserialize<Empresa>(strResponse);
                return empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<SucursalPorEmpresa> ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenersucursalporempresa?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                SucursalPorEmpresa sucursal = null;
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                if (strResponse != "")
                    sucursal = serializer.Deserialize<SucursalPorEmpresa>(strResponse);
                return sucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<TerminalPorSucursal> ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerterminalporsucursal?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal + "&idterminal=" + intIdTerminal);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                TerminalPorSucursal sucursal = null;
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                if (strResponse != "")
                    sucursal = serializer.Deserialize<TerminalPorSucursal>(strResponse);
                return sucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoIdentificacion(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadotipoidentificacion");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCatalogoReportes(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadocatalogoreportes");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProvincias(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoprovincias");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCantones(int intIdProvincia, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadocantones?idprovincia=" + intIdProvincia);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<List<LlaveDescripcion>> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadodistritos?idprovincia=" + intIdProvincia + "&idcanton=" + intIdCanton);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadobarrios?idprovincia=" + intIdProvincia + "&idcanton=" + intIdCanton + "&iddistrito=" + intIdDistrito);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
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

        public static async Task<string> AgregarEmpresa(Empresa empresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(empresa);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/agregarempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                return serializer.Deserialize<string>(responseContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarEmpresa(Empresa empresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(empresa);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarLogoEmpresa(int intIdEmpresa, string strLogotipo, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarDatosConId(intIdEmpresa, strLogotipo);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarlogoempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task RemoverLogoEmpresa(int intIdEmpresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/removerlogoempresaidempresa=" + intIdEmpresa);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarDatosConId(intIdEmpresa, strCertificado);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarcertificadoempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(sucursal);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/agregarsucursalporempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(sucursal);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarsucursalporempresa", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task AgregarTerminalPorSucursal(TerminalPorSucursal terminal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(terminal);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/agregarterminalporsucursal", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarTerminalPorSucursal(TerminalPorSucursal terminal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                StringContent contentJson = serializarEntidad(terminal);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarterminalporsucursal", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosPendientes(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadodocumentospendientes");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strResponse = serializer.Deserialize<string>(responseContent);
                if (strResponse != "")
                    listado = serializer.Deserialize<List<DocumentoDetalle>>(strResponse);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ProcesarDocumentosElectronicosPendientes()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/procesarpendientes");
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}