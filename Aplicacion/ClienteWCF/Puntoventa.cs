using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.Core.TiposComunes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LeandroSoftware.ClienteWCF
{
    public static class Puntoventa
    {
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioPuntoventaURL = ConfigurationManager.AppSettings["ServicioURL"];
        private static HttpClient httpClient = new HttpClient();

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerultimaversionapp");
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string response = serializer.Deserialize<string>(responseContent);
            return response;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasAdministrador()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoempresasadmin");
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string respuesta = serializer.Deserialize<string>(responseContent);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasPorTerminal(string strIdDispositivo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoempresas?dispositivo=" + strIdDispositivo);
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string respuesta = serializer.Deserialize<string>(responseContent);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<Empresa> ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/validarcredenciales?usuario=" + strUsuario + "&clave=" + strClave + "&idempresa=" + intIdEmpresa + "&dispositivo=" + strValorRegistro);
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string respuesta = serializer.Deserialize<string>(responseContent);
            Empresa empresa = null;
            if (respuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<List<EquipoRegistrado>> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoterminalesdisponibles?usuario=" + strUsuario + "&clave=" + strClave + "&id=" + strIdentificacion + "&tipodispositivo=" + intTipoDispositivo);
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string respuesta = serializer.Deserialize<string>(responseContent);
            List<EquipoRegistrado> listado = new List<EquipoRegistrado>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<EquipoRegistrado>>(respuesta);
            return listado;
        }

        public static async Task RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/registrarterminal?usuario=" + strUsuario + "&clave=" + strClave + "&id=" + strIdentificacion + "&sucursal=" + intIdSucursal + "&terminal=" + intIdTerminal + "&tipodispositivo=" + intTipoDispositivo + "&dispositivo=" + strDispositivoId);
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
        }

        private static async Task Ejecutar(string strDatos, string servicioURL, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strContent = serializer.Serialize(strDatos);
                StringContent contentJson = new StringContent(strContent, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutar", contentJson);
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

        private static async Task<string> EjecutarConsulta(string strDatos, string servicioURL, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strContent = serializer.Serialize(strDatos);
                StringContent contentJson = new StringContent(strContent, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutarconsulta", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string strError = serializer.Deserialize<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                string strRespuesta = serializer.Deserialize<string>(responseContent);
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Empresa empresa = null;
            if (respuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<byte[]> ObtenerLogotipoEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerLogotipoEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            string logotipo = null;
            if (respuesta != "")
                logotipo = serializer.Deserialize<string>(respuesta);
            return Convert.FromBase64String(logotipo);
        }

        public static async Task<List<SucursalPorEmpresa>> ObtenerListadoSucursalPorEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoSucursalPorEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<SucursalPorEmpresa> listado = new List<SucursalPorEmpresa>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<SucursalPorEmpresa>>(respuesta);
            return listado;
        }

        public static async Task<SucursalPorEmpresa> ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerSucursalPorEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            SucursalPorEmpresa sucursal = null;
            if (respuesta != "")
                sucursal = serializer.Deserialize<SucursalPorEmpresa>(respuesta);
            return sucursal;
        }

        public static async Task<List<TerminalPorSucursal>> ObtenerListadoTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTerminalPorSucursal', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<TerminalPorSucursal> listado = new List<TerminalPorSucursal>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<TerminalPorSucursal>>(respuesta);
            return listado;
        }

        public static async Task<TerminalPorSucursal> ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTerminalPorSucursal', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTerminal: " + intIdTerminal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            TerminalPorSucursal terminal = null;
            if (respuesta != "")
                terminal = serializer.Deserialize<TerminalPorSucursal>(respuesta);
            return terminal;
        }

        public static async Task<decimal> ObtenerTipoCambioDolar(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTipoCambioDolar'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            decimal decTipoCambioDolar = 0;
            if (respuesta != "")
                decTipoCambioDolar = serializer.Deserialize<decimal>(respuesta);
            return decTipoCambioDolar;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoIdentificacion(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoIdentificacion'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoModulos(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoModulos'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCatalogoReportes(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCatalogoReportes'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProvincias(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProvincias'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta); ;
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCantones(int intIdProvincia, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCantones', Parametros: {IdProvincia: " + intIdProvincia + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDistritos', Parametros: {IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoBarrios', Parametros: {IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoProducto(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoProducto'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoExoneracion(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoExoneracion'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoImpuesto(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoImpuesto'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoFormaPagoEgreso(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoFormaPagoEgreso'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoFormaPagoFactura(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoFormaPagoFactura'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoRoles(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoRoles'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipodePrecio(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipodePrecio'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoMoneda(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoMoneda'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVenta(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCondicionVenta'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoFactura(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCondicionVentaYFormaPagoFactura'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoCompra(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCondicionVentaYFormaPagoCompra'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentas>> ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intIdTipoPago, int intIdBancoAdquiriente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", isNulo: '" + bolNulo + "', IdTipoPago: " + intIdTipoPago + ", IdBancoAdquiriente: " + intIdBancoAdquiriente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteVentas> listado = new List<ReporteVentas>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentas>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorVendedor>> ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorVendedor', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdVendedor: " + intIdVendedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteVentasPorVendedor> listado = new List<ReporteVentasPorVendedor>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorVendedor>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCompras>> ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intFormaPago, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteComprasPorProveedor', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + ", isNulo: '" + bolNulo + "', FormaPago: " + intFormaPago + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCompras> listado = new List<ReporteCompras>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCompras>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorCobrar>> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCuentasPorCobrarClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCuentasPorCobrar> listado = new List<ReporteCuentasPorCobrar>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorCobrar>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorPagar>> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCuentasPorPagarProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCuentasPorPagar> listado = new List<ReporteCuentasPorPagar>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorPagar>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxC>> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosCxCClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteMovimientosCxC> listado = new List<ReporteMovimientosCxC>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxC>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxP>> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosCxPProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteMovimientosCxP> listado = new List<ReporteMovimientosCxP>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxP>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosBanco>> ObtenerReporteMovimientosBanco(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosBanco', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteMovimientosBanco> listado = new List<ReporteMovimientosBanco>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosBanco>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteEstadoResultados', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteEstadoResultados>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleEgreso>> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDetalleEgreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdCuentaEgreso: " + intIdCuentaEgreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalleEgreso> listado = new List<ReporteDetalleEgreso>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleEgreso>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleIngreso>> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDetalleIngreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdCuentaIngreso: " + intIdCuentaIngreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalleIngreso> listado = new List<ReporteDetalleIngreso>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleIngreso>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaResumen>> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorLineaResumen', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteVentasPorLineaResumen> listado = new List<ReporteVentasPorLineaResumen>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaResumen>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaDetalle>> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorLineaDetalle', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdLinea: " + intIdLinea + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteVentasPorLineaDetalle> listado = new List<ReporteVentasPorLineaDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteFacturasElectronicasEmitidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteNotasCreditoElectronicasEmitidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteFacturasElectronicasRecibidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteNotasCreditoElectronicasRecibidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteResumenMovimiento>> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteResumenDocumentosElectronicos', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteResumenMovimiento> listado = new List<ReporteResumenMovimiento>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteResumenMovimiento>>(respuesta);
            return listado;
        }

        public static async Task<CierreCaja> GenerarDatosCierreCaja(int intIdEmpresa, string strFechaCierre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GenerarDatosCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + ", FechaCierre: '" + strFechaCierre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CierreCaja cierre = null;
            if (respuesta != "")
                cierre = serializer.Deserialize<CierreCaja>(respuesta);
            return cierre;
        }

        public static async Task GuardarDatosCierreCaja(CierreCaja cierre, string strToken)
        {
            string strEntidad = serializer.Serialize(cierre);
            string strDatos = "{NombreMetodo: 'GuardarDatosCierreCaja', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AbortarCierreCaja(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AbortarCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<ReporteCierreDeCaja>> ObtenerReporteCierreDeCaja(int intIdCierre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCierreDeCaja', Parametros: {IdCierre: " + intIdCierre + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCierreDeCaja> listado = new List<ReporteCierreDeCaja>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCierreDeCaja>>(respuesta);
            return listado;
        }

        public static async Task<ParametroImpuesto> ObtenerParametroImpuesto(int intIdImpuesto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerParametroImpuesto', Parametros: {IdImpuesto: " + intIdImpuesto + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            ParametroImpuesto parametroImpuesto = null;
            if (respuesta != "")
                parametroImpuesto = serializer.Deserialize<ParametroImpuesto>(respuesta);
            return parametroImpuesto;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strToken, string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoBancoAdquiriente', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente, string strToken)
        {
            string strEntidad = serializer.Serialize(bancoAdquiriente);
            string strDatos = "{NombreMetodo: 'AgregarBancoAdquiriente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente, string strToken)
        {
            string strEntidad = serializer.Serialize(bancoAdquiriente);
            string strDatos = "{NombreMetodo: 'ActualizarBancoAdquiriente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<BancoAdquiriente> ObtenerBancoAdquiriente(int intIdBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerBancoAdquiriente', Parametros: {IdBancoAdquiriente: " + intIdBanco + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            BancoAdquiriente bancoAdquiriente = null;
            if (respuesta != "")
                bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(respuesta);
            return bancoAdquiriente;
        }

        public static async Task EliminarBancoAdquiriente(int intIdBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarBancoAdquiriente', Parametros: {IdBancoAdquiriente: " + intIdBanco + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoClientes(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaClientes(int intIdEmpresa, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task AgregarCliente(Cliente cliente, string strToken)
        {
            string strEntidad = serializer.Serialize(cliente);
            string strDatos = "{NombreMetodo: 'AgregarCliente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCliente(Cliente cliente, string strToken)
        {
            string strEntidad = serializer.Serialize(cliente);
            string strDatos = "{NombreMetodo: 'ActualizarCliente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Cliente> ObtenerCliente(int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCliente', Parametros: {IdCliente: " + intIdCliente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Cliente cliente = null;
            if (respuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta);
            return cliente;
        }

        public static async Task EliminarCliente(int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCliente', Parametros: {IdCliente: " + intIdCliente + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Cliente> ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ValidaIdentificacionCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", Identificacion: '" + strIdentificacion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Cliente cliente = null;
            if (respuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta);
            return cliente;
        }


        public static async Task<List<LlaveDescripcion>> ObtenerListadoLineas(int intIdEmpresa, string strToken, string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoLineas', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task ActualizarEmpresa(Empresa empresa, string strToken)
        {
            string strEntidad = serializer.Serialize(empresa);
            string strDatos = "{NombreMetodo: 'ActualizarEmpresa', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarEmpresaConDetalle(Empresa empresa, string strToken)
        {
            string strEntidad = serializer.Serialize(empresa);
            string strDatos = "{NombreMetodo: 'ActualizarEmpresaConDetalle', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal, string strToken)
        {
            string strEntidad = serializer.Serialize(sucursal);
            string strDatos = "{NombreMetodo: 'ActualizarSucursalPorEmpresa', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarTerminalPorSucursal(TerminalPorSucursal terminal, string strToken)
        {
            string strEntidad = serializer.Serialize(terminal);
            string strDatos = "{NombreMetodo: 'ActualizarTerminalPorSucursal', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarLogoEmpresa(int intIdEmpresa, string strLogotipo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarLogoEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + ", Logotipo: '" + strLogotipo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task RemoverLogoEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'RemoverLogoEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarCertificadoEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + ", Certificado: '" + strCertificado + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AgregarLinea(Linea linea, string strToken)
        {
            string strEntidad = serializer.Serialize(linea);
            string strDatos = "{NombreMetodo: 'AgregarLinea', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarLinea(Linea linea, string strToken)
        {
            string strEntidad = serializer.Serialize(linea);
            string strDatos = "{NombreMetodo: 'ActualizarLinea', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Linea> ObtenerLinea(int intIdLinea, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerLinea', Parametros: {IdLinea: " + intIdLinea + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Linea linea = null;
            if (respuesta != "")
                linea = serializer.Deserialize<Linea>(respuesta);
            return linea;
        }

        public static async Task EliminarLinea(int intIdLinea, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarLinea', Parametros: {IdLinea: " + intIdLinea + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProveedores(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);

            return intCantidad;
        }

        public static async Task AgregarProveedor(Proveedor proveedor, string strToken)
        {
            string strEntidad = serializer.Serialize(proveedor);
            string strDatos = "{NombreMetodo: 'AgregarProveedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarProveedor(Proveedor proveedor, string strToken)
        {
            string strEntidad = serializer.Serialize(proveedor);
            string strDatos = "{NombreMetodo: 'ActualizarProveedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Proveedor> ObtenerProveedor(int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProveedor', Parametros: {IdProveedor: " + intIdProveedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Proveedor proveedor = null;
            if (respuesta != "")
                proveedor = serializer.Deserialize<Proveedor>(respuesta);
            return proveedor;
        }

        public static async Task EliminarProveedor(int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarProveedor', Parametros: {IdProveedor: " + intIdProveedor + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, string strToken, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaProductos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IncluyeServicios: '" + bolIncluyeServicios + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProductos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, bool bolIncluyeServicios, string strToken, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProductos', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IncluyeServicios: '" + bolIncluyeServicios + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarProducto(Producto producto, string strToken)
        {
            string strEntidad = serializer.Serialize(producto);
            string strDatos = "{NombreMetodo: 'AgregarProducto', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarProducto(Producto producto, string strToken)
        {
            string strEntidad = serializer.Serialize(producto);
            string strDatos = "{NombreMetodo: 'ActualizarProducto', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Producto> ObtenerProducto(int intIdProducto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProducto', Parametros: {IdProducto: " + intIdProducto + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProductoPorCodigo', Parametros: {IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta);
            return producto;
        }

        public static async Task EliminarProducto(int intIdProducto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarProducto', Parametros: {IdProducto: " + intIdProducto + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoUsuarios', Parametros: {IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarUsuario(Usuario usuario, string strToken)
        {
            string strEntidad = serializer.Serialize(usuario);
            string strDatos = "{NombreMetodo: 'AgregarUsuario', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarUsuario(Usuario usuario, string strToken)
        {
            string strEntidad = serializer.Serialize(usuario);
            string strDatos = "{NombreMetodo: 'ActualizarUsuario', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Usuario> ActualizarClaveUsuario(int intIdUsuario, string strClave, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarClaveUsuario', Parametros: {IdUsuario: " + intIdUsuario + ", Clave: '" + strClave + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Usuario usuario = null;
            if (respuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta);
            return usuario;
        }

        public static async Task<Usuario> ObtenerUsuario(int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerUsuario', Parametros: {IdUsuario: " + intIdUsuario + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Usuario usuario = null;
            if (respuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta);
            return usuario;
        }

        public static async Task EliminarUsuario(int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarUsuario', Parametros: {IdUsuario: " + intIdUsuario + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strToken, string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasEgreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarCuentaEgreso(CuentaEgreso cuentaEgreso, string strToken)
        {
            string strEntidad = serializer.Serialize(cuentaEgreso);
            string strDatos = "{NombreMetodo: 'AgregarCuentaEgreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCuentaEgreso(CuentaEgreso cuentaEgreso, string strToken)
        {
            string strEntidad = serializer.Serialize(cuentaEgreso);
            string strDatos = "{NombreMetodo: 'ActualizarCuentaEgreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaEgreso> ObtenerCuentaEgreso(int intIdCuentaEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaEgreso', Parametros: {IdCuentaEgreso: " + intIdCuentaEgreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaEgreso cuentaEgreso = null;
            if (respuesta != "")
                cuentaEgreso = serializer.Deserialize<CuentaEgreso>(respuesta);
            return cuentaEgreso;
        }

        public static async Task EliminarCuentaEgreso(int intIdCuentaEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCuentaEgreso', Parametros: {IdCuentaEgreso: " + intIdCuentaEgreso + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasBanco(int intIdEmpresa, string strToken, string strDescripcion = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasBanco', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarCuentaBanco(CuentaBanco cuentaBanco, string strToken)
        {
            string strEntidad = serializer.Serialize(cuentaBanco);
            string strDatos = "{NombreMetodo: 'AgregarCuentaBanco', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCuentaBanco(CuentaBanco cuentaBanco, string strToken)
        {
            string strEntidad = serializer.Serialize(cuentaBanco);
            string strDatos = "{NombreMetodo: 'ActualizarCuentaBanco', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaBanco> ObtenerCuentaBanco(int intIdCuentaBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaBanco', Parametros: {IdCuentaBanco: " + intIdCuentaBanco + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaBanco cuentaBanco = null;
            if (respuesta != "")
                cuentaBanco = serializer.Deserialize<CuentaBanco>(respuesta);
            return cuentaBanco;
        }

        public static async Task EliminarCuentaBanco(int intIdCuentaBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCuentaBanco', Parametros: {IdCuentaBanco: " + intIdCuentaBanco + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoVendedores(int intIdEmpresa, string strToken, string strNombre = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoVendedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarVendedor(Vendedor vendedor, string strToken)
        {
            string strEntidad = serializer.Serialize(vendedor);
            string strDatos = "{NombreMetodo: 'AgregarVendedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarVendedor(Vendedor vendedor, string strToken)
        {
            string strEntidad = serializer.Serialize(vendedor);
            string strDatos = "{NombreMetodo: 'ActualizarVendedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Vendedor> ObtenerVendedor(int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerVendedor', Parametros: {IdVendedor: " + intIdVendedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task<Vendedor> ObtenerVendedorPorDefecto(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerVendedorPorDefecto', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task EliminarVendedor(int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarVendedor', Parametros: {IdVendedor: " + intIdVendedor + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaEgresos(int intIdEmpresa, string strToken, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaEgresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEgresos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strToken, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoEgresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AnularEgreso(int intIdEgreso, int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularEgreso', Parametros: {IdEgreso: " + intIdEgreso + ", IdUsuario: " + intIdUsuario + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Egreso> ObtenerEgreso(int intIdEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerEgreso', Parametros: {IdEgreso: " + intIdEgreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Egreso egreso = null;
            if (respuesta != "")
                egreso = serializer.Deserialize<Egreso>(respuesta);
            return egreso;
        }

        public static async Task<string> AgregarEgreso(Egreso egreso, string strToken)
        {
            string strEntidad = serializer.Serialize(egreso);
            string strDatos = "{NombreMetodo: 'AgregarEgreso', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return serializer.Deserialize<string>(strId);
        }

        public static async Task ActualizarEgreso(Egreso egreso, string strToken)
        {
            string strEntidad = serializer.Serialize(egreso);
            string strDatos = "{NombreMetodo: 'ActualizarEgreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaFacturas(int intIdEmpresa, string strToken, int intIdFactura = 0, string strNombre = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaFacturas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoFacturas(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strToken, int intIdFactura = 0, string strNombre = "")
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoFacturas', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task AnularFactura(int intIdFactura, int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularFactura', Parametros: {IdFactura: " + intIdFactura + ", IdUsuario: " + intIdUsuario + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Factura> ObtenerFactura(int intIdFactura, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerFactura', Parametros: {IdFactura: " + intIdFactura + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Factura factura = null;
            if (respuesta != "")
                factura = serializer.Deserialize<Factura>(respuesta);
            return factura;
        }

        public static async Task<string> AgregarFactura(Factura factura, string strToken)
        {
            string strEntidad = serializer.Serialize(factura);
            string strDatos = "{NombreMetodo: 'AgregarFactura', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return serializer.Deserialize<string>(strId);
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronico(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerDocumentoElectronico', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronicoPorClave(string strClave, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerDocumentoElectronicoPorClave', Parametros: {Clave: " + strClave + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task GeneraMensajeReceptor(string strMensaje, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GeneraMensajeReceptor', Parametros: {Datos: '" + strMensaje + "', IdEmpresa: " + intIdEmpresa + ", Sucursal: " + intSucursal + ", Terminal: " + intTerminal + ", Estado: " + intEstado + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDocumentosElectronicosEnProceso', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task EnviarDocumentoElectronicoPendiente(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EnviarDocumentoElectronicoPendiente', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<DocumentoElectronico> ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerRespuestaDocumentoElectronicoEnviado', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task EnviarNotificacion(int intIdDocumento, string strCorreoReceptor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EnviarNotificacionDocumentoElectronico', Parametros: {IdDocumento: " + intIdDocumento + ", CorreoReceptor: '" + strCorreoReceptor + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }
    }
}
