using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;
using LeandroSoftware.Common.Dominio.Entidades;
using Newtonsoft.Json;
using LeandroSoftware.Common.Seguridad;
using LeandroSoftware.Common.DatosComunes;

namespace LeandroSoftware.ClienteWCF
{
    public static class Administrador
    {
        private static string strServicioURL = ConfigurationManager.AppSettings["ServicioURL"];
        private static HttpClient httpClient = new HttpClient();

        public static StringContent serializarEntidad<T>(T entidad)
        {
            string strDatos = JsonConvert.SerializeObject("{NombreMetodo: 'ActualizarEmpresa', Entidad: " + JsonConvert.SerializeObject(entidad) + "}");
            StringContent contentJson = new StringContent(strDatos, Encoding.UTF8, "application/json");
            return contentJson;
        }

        public static StringContent serializarDatosConId(int id, string strDatos)
        {
            string strEntidad = JsonConvert.SerializeObject("{Id: " + id + ", Datos: '" + strDatos + "'}");
            StringContent contentJson = new StringContent(strEntidad, Encoding.UTF8, "application/json");
            return contentJson;
        }

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerultimaversionapp");
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string response = await httpResponse.Content.ReadAsStringAsync();
            return response;
        }

        public static async Task ActualizarVersionApp(string strValor, byte[] bytZipFile, string strToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string strDatos = JsonConvert.SerializeObject("{IdParametro: 1, Valor: '" + strValor + "'}");
            StringContent contentJson = new StringContent(strDatos, Encoding.UTF8, "application/json");
            if (strToken != "")
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
            HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarparametrosistema", contentJson);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/octet-stream";
            client.Headers[HttpRequestHeader.Authorization] = "bearer " + strToken;
            client.UploadData(strServicioURL + "/actualizararchivoaplicacion", "POST", bytZipFile);
        }

        public static async Task<List<ParametroSistema>> ObtenerListadoParametros(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoparametros");
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<ParametroSistema> listado = new List<ParametroSistema>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<ParametroSistema>>(response);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ActualizarParametroSistema(int intIdParametro, string strValor, string strToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string strDatos = JsonConvert.SerializeObject("{IdParametro: " + intIdParametro + ", Valor: '" + strValor + "'}");
            StringContent contentJson = new StringContent(strDatos, Encoding.UTF8, "application/json");
            if (strToken != "")
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
            HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarparametrosistema", contentJson);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
        }

        public static async Task<Usuario> ValidarCredencialesAdmin(string strUsuario, string strClave)
        {
            try
            {
                string strEncryptedPassword = Encriptador.EncriptarDatos(strClave);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/validarcredencialesadmin?usuario=" + strUsuario + "&clave=" + strEncryptedPassword);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = httpResponse.Content.ReadAsStringAsync().Result;
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                string response = await httpResponse.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                Empresa empresa = null;
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    empresa = JsonConvert.DeserializeObject<Empresa>(response);
                return empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoReportePorEmpresa(int intIdEmpresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoreporteporempresa?idempresa=" + intIdEmpresa);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoRolePorEmpresa(int intIdEmpresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoroleporempresa?idempresa=" + intIdEmpresa);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
                return listado;
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                SucursalPorEmpresa sucursal = null;
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    sucursal = JsonConvert.DeserializeObject<SucursalPorEmpresa>(response);
                return sucursal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task EliminarRegistrosPorEmpresa(int intIdEmpresa, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/eliminarregistrosporempresa?idempresa=" + intIdEmpresa);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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

        public static async Task<TerminalPorSucursal> ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerterminalporsucursal?idempresa=" + intIdEmpresa + "&idsucursal=" + intIdSucursal + "&idterminal=" + intIdTerminal);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                TerminalPorSucursal sucursal = null;
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    sucursal = JsonConvert.DeserializeObject<TerminalPorSucursal>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoRoles(string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioURL + "/obtenerlistadoroles");
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                string response = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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

        public static async Task ActualizarReportePorEmpresa(int intIdEmpresa, List<ReportePorEmpresa> listado, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strListado = JsonConvert.SerializeObject(listado);
                StringContent contentJson = serializarDatosConId(intIdEmpresa, strListado);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarlistadoreportes", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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

        public static async Task ActualizarRolePorEmpresa(int intIdEmpresa, List<RolePorEmpresa> listado, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strListado = JsonConvert.SerializeObject(listado);
                StringContent contentJson = serializarDatosConId(intIdEmpresa, strListado);
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioURL + "/actualizarlistadoroles", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
                string response = await httpResponse.Content.ReadAsStringAsync();
                if (response != "")
                    listado = JsonConvert.DeserializeObject<List<DocumentoDetalle>>(response);
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
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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