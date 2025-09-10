using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Configuration;
using System.Management;
using Newtonsoft.Json;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.DatosComunes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json.Linq;

namespace LeandroSoftware.ClienteWCF
{
    public static class Puntoventa
    {
        private static string strServicioPuntoventaURL = ConfigurationManager.AppSettings["ServicioURL"];
        private static string strServicioHaciendaURL = ConfigurationManager.AppSettings["ServicioHaciendaURL"];
        private static HttpClient httpClient = new HttpClient();

        public static string ObtenerIdentificadorEquipo()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string strVolumeSerial = dsk["VolumeSerialNumber"].ToString();
            string strProcessorId = "";
            string strBoardSerialNumber = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Processor Where DeviceID =\"CPU0\"");
                foreach (ManagementObject mo in mos.Get())
                    strProcessorId = mo["ProcessorId"] != null ? mo["ProcessorId"].ToString() : "N/F-PROCESSOR";
            }
            catch
            {
                strProcessorId = "N/F-PROCESSOR";
            }
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject mo in mos.Get())
                    strBoardSerialNumber = mo["SerialNumber"] != null ? mo["SerialNumber"].ToString() : "N/F-BASEBOARD";
            }
            catch
            {
                strBoardSerialNumber = "N/F-BASEBOARD";
            }
            return strProcessorId + "-" + strVolumeSerial + "-" + strBoardSerialNumber;
        }

        public static decimal ObtenerPrecioRedondeado(decimal decValorRedondeo, decimal decPrecioVenta)
        {
            decimal decPrecioRedondeado = decPrecioVenta;
            string[] arrPrecioConDescuento = decPrecioVenta.ToString().Split('.');
            decimal decDecimales = arrPrecioConDescuento.Length > 1 ? decimal.Parse("0." + arrPrecioConDescuento[1].ToString()) : 0;
            decimal decTotalIncremento = decDecimales > 0 ? 1 - decDecimales : 0;
            decimal decDigitos = decimal.Parse(arrPrecioConDescuento[0].Substring(arrPrecioConDescuento[0].Length - 2)) + decDecimales;
            while ((decDigitos + decTotalIncremento) % decValorRedondeo != 0)
            {
                decTotalIncremento += 1;
            }
            if (decTotalIncremento > 0) decPrecioRedondeado += decTotalIncremento;
            return decPrecioRedondeado;
        }

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerultimaversionapp");
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(JsonConvert.DeserializeObject<string>(strError));
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            return await httpResponse.Content.ReadAsStringAsync();
        }

        public static async Task ActualizarVersionApp(string strValor, byte[] bytZipFile, string strToken)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/octet-stream";
            client.Headers[HttpRequestHeader.Authorization] = "bearer " + strToken;
            client.UploadData(strServicioPuntoventaURL + "/actualizararchivoaplicacion", bytZipFile);
            string strDatos = JsonConvert.SerializeObject("{IdParametro: 1, Valor: '" + strValor + "'}");
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasAdministrador()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoempresasadmin");
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string respuesta = await httpResponse.Content.ReadAsStringAsync();
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasPorTerminal(string strIdDispositivo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoempresas?dispositivo=" + strIdDispositivo);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string respuesta = await httpResponse.Content.ReadAsStringAsync();
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<Empresa> ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/validarcredenciales?usuario=" + strUsuario + "&clave=" + strClave + "&idempresa=" + intIdEmpresa + "&dispositivo=" + strValorRegistro);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string respuesta = await httpResponse.Content.ReadAsStringAsync();
            Empresa empresa = null;
            if (respuesta != "")
                empresa = JsonConvert.DeserializeObject<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<List<EquipoRegistrado>> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerlistadoterminalesdisponibles?usuario=" + strUsuario + "&clave=" + strClave + "&id=" + strIdentificacion + "&tipodispositivo=" + intTipoDispositivo);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception(strError);
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string respuesta = await httpResponse.Content.ReadAsStringAsync();
            List<EquipoRegistrado> listado = new List<EquipoRegistrado>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EquipoRegistrado>>(respuesta);
            return listado;
        }

        public static async Task RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/registrarterminal?usuario=" + strUsuario + "&clave=" + strClave + "&id=" + strIdentificacion + "&sucursal=" + intIdSucursal + "&terminal=" + intIdTerminal + "&tipodispositivo=" + intTipoDispositivo + "&dispositivo=" + strDispositivoId);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
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
                var jsonString = JsonConvert.SerializeObject(strDatos);
                StringContent contentJson = new StringContent(jsonString, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutar", contentJson);
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

        private static async Task<string> EjecutarConsulta(string strDatos, string servicioURL, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var jsonString = JsonConvert.SerializeObject(strDatos);
                StringContent contentJson = new StringContent(jsonString, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutarconsulta", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                    throw new Exception(strError);
                }
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                return await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ValidarCredencialesHacienda(string strCodigoUsuario, string strClave, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ValidarCredencialesHacienda', Parametros: {CodigoUsuario: '" + strCodigoUsuario + "', Clave: '" + strClave + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ValidarCertificadoHacienda(string strPin, string strCertificado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ValidarCertificadoHacienda', Parametros: {PinCertificado: '" + strPin + "', Certificado: '" + strCertificado + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<byte[]> ObtenerFacturaPDF(int intIdFactura, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerFacturaPDF', Parametros: {IdFactura: '" + intIdFactura + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            byte[] archivoPDF = new byte[0];
            if (respuesta != "")
                archivoPDF = JsonConvert.DeserializeObject<byte[]>(respuesta);
            return archivoPDF;
        }

        public static async Task<byte[]> ObtenerApartadoPDF(int intIdApartado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerApartadoPDF', Parametros: {IdApartado: '" + intIdApartado + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            byte[] archivoPDF = new byte[0];
            if (respuesta != "")
                archivoPDF = JsonConvert.DeserializeObject<byte[]>(respuesta);
            return archivoPDF;
        }

        public static async Task<byte[]> ObtenerOrdenServicioPDF(int intIdOrden, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerOrdenServicioPDF', Parametros: {IdOrden: '" + intIdOrden + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            byte[] archivoPDF = new byte[0];
            if (respuesta != "")
                archivoPDF = JsonConvert.DeserializeObject<byte[]>(respuesta);
            return archivoPDF;
        }

        public static async Task<byte[]> ObtenerProformaPDF(int intIdProforma, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProformaPDF', Parametros: {IdProforma: '" + intIdProforma + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            byte[] archivoPDF = new byte[0];
            if (respuesta != "")
                archivoPDF = JsonConvert.DeserializeObject<byte[]>(respuesta);
            return archivoPDF;
        }

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Empresa empresa = null;
            if (respuesta != "")
                empresa = JsonConvert.DeserializeObject<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<byte[]> ObtenerLogotipoEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerLogotipoEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            byte[] logotipo = null;
            if (respuesta != "\"\"")
                logotipo = Convert.FromBase64String(JsonConvert.DeserializeObject<string>(respuesta));
            return logotipo;
        }

        public static async Task<CredencialesHacienda> ObtenerCredencialesHacienda(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCredencialesHacienda', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CredencialesHacienda credenciales = null;
            if (respuesta != "")
                credenciales = JsonConvert.DeserializeObject<CredencialesHacienda>(respuesta);
            return credenciales;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoSucursales(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoSucursales', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<SucursalPorEmpresa> ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerSucursalPorEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            SucursalPorEmpresa sucursal = null;
            if (respuesta != "")
                sucursal = JsonConvert.DeserializeObject<SucursalPorEmpresa>(respuesta);
            return sucursal;
        }

        public static async Task<List<TerminalPorSucursal>> ObtenerListadoTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTerminalPorSucursal', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<TerminalPorSucursal> listado = new List<TerminalPorSucursal>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<TerminalPorSucursal>>(respuesta);
            return listado;
        }

        public static async Task<TerminalPorSucursal> ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTerminalPorSucursal', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTerminal: " + intIdTerminal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            TerminalPorSucursal terminal = null;
            if (respuesta != "")
                terminal = JsonConvert.DeserializeObject<TerminalPorSucursal>(respuesta);
            return terminal;
        }

        public static async Task<decimal> ObtenerTipoCambioDolarHacienda()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioHaciendaURL + "/indicadores/tc/dolar");
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                string strError = JsonConvert.DeserializeObject<string>(httpResponse.Content.ReadAsStringAsync().Result);
                throw new Exception("Error al consultar la informacion del tipo de cambio del dolar en el Ministerio de Hacienda");
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception("Error al consultar la informacion del tipo de cambio del dolar en el Ministerio de Hacienda");
            string strTipoDeCambio = await httpResponse.Content.ReadAsStringAsync();
            JObject datosJO = JObject.Parse(strTipoDeCambio);
            if (datosJO.Property("venta") == null) throw new Exception("Error al consultar el tipo de cambio de venta en el Ministerio de Hacienda");
            JObject ventaJO = JObject.Parse(datosJO.Property("venta").Value.ToString());
            if (ventaJO.Property("valor") == null) throw new Exception("Error al consultar el tipo de cambio de venta en el Ministerio de Hacienda");
            decimal decTipoDeCambioDolar = decimal.Parse(ventaJO.Property("valor").Value.ToString());
            return decTipoDeCambioDolar;
        }

        public static async Task<ContribuyenteHacienda> ObtenerInformacionContribuyente(string strIdentificacion)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioHaciendaURL + "/fe/ae?identificacion=" + strIdentificacion);
            if (httpResponse.StatusCode == HttpStatusCode.SeeOther)
            {
                throw new Exception("Error al consultar la informacion del contribuyente en el Ministerio de Hacienda");
            }
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception("Error al consultar la informacion del contribuyente en el Ministerio de Hacienda");
            string strInformacionContribuyente = await httpResponse.Content.ReadAsStringAsync();
            JObject datosJO = JObject.Parse(strInformacionContribuyente);
            if (datosJO.Property("actividades") == null) throw new Exception("Error al consultar la informacion del contribuyente en el Ministerio de Hacienda");

            JArray actividades = JArray.Parse(datosJO.Property("actividades").Value.ToString());
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            foreach (JObject item in actividades)
            {
                listado.Add(new LlaveDescripcion(int.Parse(item.Property("codigo").Value.ToString()), item.Property("codigo").Value.ToString() + " - " + item.Property("descripcion").Value.ToString()));
            }
            ContribuyenteHacienda cliente = new ContribuyenteHacienda
            {
                Nombre = datosJO.Property("nombre").Value.ToString(),
                ActividadesEconomicas = listado
            };
            return cliente;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoMovimientoBanco(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTipoMovimientoBanco'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCatalogoReportes(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCatalogoReportes'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProvincias(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProvincias'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta); ;
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCantones(int intIdProvincia, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCantones', Parametros: {IdProvincia: " + intIdProvincia + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDistritos', Parametros: {IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoBarrios', Parametros: {IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoRolesPorEmpresa(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoRolesPorEmpresa', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoFactura(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCondicionVentaYFormaPagoFactura'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoCompra(string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCondicionVentaYFormaPagoCompra'}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteProformas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteProformas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', isNulo: '" + bolNulo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteApartados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteApartados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', isNulo: '" + bolNulo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteOrdenesServicio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, bool bolNulo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteOrdenesServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', isNulo: '" + bolNulo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteVentasPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intIdTipoPago, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", isNulo: '" + bolNulo + "', IdTipoPago: " + intIdTipoPago + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDevolucionesPorCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", isNulo: '" + bolNulo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorVendedor>> ObtenerReporteVentasPorVendedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorVendedor', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdVendedor: " + intIdVendedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteVentasPorVendedor> listado = new List<ReporteVentasPorVendedor>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteVentasPorVendedor>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalle>> ObtenerReporteComprasPorProveedor(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intFormaPago, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteComprasPorProveedor', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + ", isNulo: '" + bolNulo + "', IdTipoPago: " + intFormaPago + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDetalle> listado = new List<ReporteDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentas>> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolActivas, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCuentasPorCobrarClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", Activas: '" + bolActivas + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCuentas> listado = new List<ReporteCuentas>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteCuentas>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentas>> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolActivas, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCuentasPorPagarProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + ", Activas: '" + bolActivas + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteCuentas> listado = new List<ReporteCuentas>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteCuentas>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteGrupoDetalle>> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosCxCClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteGrupoDetalle> listado = new List<ReporteGrupoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteGrupoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteGrupoDetalle>> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosCxPProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteGrupoDetalle> listado = new List<ReporteGrupoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteGrupoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosBanco>> ObtenerReporteMovimientosBanco(int intIdCuenta, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteMovimientosBanco', Parametros: {IdCuenta: " + intIdCuenta + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteMovimientosBanco> listado = new List<ReporteMovimientosBanco>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteMovimientosBanco>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteEstadoResultados(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteEstadoResultados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteEstadoResultados>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteGrupoDetalle>> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdSucursal, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDetalleEgreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdCuentaEgreso: " + intIdCuentaEgreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteGrupoDetalle> listado = new List<ReporteGrupoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteGrupoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteGrupoDetalle>> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdSucursal, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDetalleIngreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdCuentaIngreso: " + intIdCuentaIngreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteGrupoDetalle> listado = new List<ReporteGrupoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteGrupoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<DescripcionValor>> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorLineaResumen', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DescripcionValor> listado = new List<DescripcionValor>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<DescripcionValor>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteProductoTransitorio>> ObtenerReporteVentasProductoTransitorio(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasProductoTransitorio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteProductoTransitorio> listado = new List<ReporteProductoTransitorio>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteProductoTransitorio>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteGrupoLineaDetalle>> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdSucursal, int intIdLinea, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteVentasPorLineaDetalle', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdLinea: " + intIdLinea + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteGrupoLineaDetalle> listado = new List<ReporteGrupoLineaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteGrupoLineaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteDocumentosElectronicosEmitidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDocumentosElectronicosEmitidos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteNotasCreditoElectronicasEmitidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteDocumentosElectronicosRecibidos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteDocumentosElectronicosRecibidos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteNotasCreditoElectronicasRecibidas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteResumenMovimiento>> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteResumenDocumentosElectronicos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteResumenMovimiento> listado = new List<ReporteResumenMovimiento>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteResumenMovimiento>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcionValor>> ObtenerReporteComparativoVentasPorPeriodo(int intIdEmpresa, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteComparativoVentasPorPeriodo', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcionValor> listado = new List<LlaveDescripcionValor>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcionValor>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteInventario>> ObtenerReporteInventario(int intIdEmpresa, int intIdSucursal, bool bolFiltraActivos, bool bolFitraExistencias, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteInventario', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FiltraActivos: '" + bolFiltraActivos + "', FiltraExistencias: '" + bolFitraExistencias + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', CodigoProveedor: '" + strCodigoProveedor + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ReporteInventario> listado = new List<ReporteInventario>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ReporteInventario>>(respuesta);
            return listado;
        }

        public static async Task<CierreCaja> GenerarDatosCierreCaja(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GenerarDatosCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CierreCaja cierre = null;
            if (respuesta != "")
                cierre = JsonConvert.DeserializeObject<CierreCaja>(respuesta);
            return cierre;
        }

        public static async Task<string> GuardarDatosCierreCaja(CierreCaja cierre, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cierre);
            string strDatos = "{NombreMetodo: 'GuardarDatosCierreCaja', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AbortarCierreCaja(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AbortarCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<DescripcionValor>> ObtenerReporteCierreDeCaja(int intIdCierre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerReporteCierreDeCaja', Parametros: {IdCierre: " + intIdCierre + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DescripcionValor> listado = new List<DescripcionValor>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<DescripcionValor>>(respuesta);
            return listado;
        }

        public static async Task<LlaveDescripcionValor> ObtenerParametroImpuesto(int intIdImpuesto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerParametroImpuesto', Parametros: {IdImpuesto: " + intIdImpuesto + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            LlaveDescripcionValor parametroImpuesto = null;
            if (respuesta != "")
                parametroImpuesto = JsonConvert.DeserializeObject<LlaveDescripcionValor>(respuesta);
            return parametroImpuesto;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoBancoAdquiriente', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(bancoAdquiriente);
            string strDatos = "{NombreMetodo: 'AgregarBancoAdquiriente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(bancoAdquiriente);
            string strDatos = "{NombreMetodo: 'ActualizarBancoAdquiriente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<BancoAdquiriente> ObtenerBancoAdquiriente(int intIdBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerBancoAdquiriente', Parametros: {IdBancoAdquiriente: " + intIdBanco + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            BancoAdquiriente bancoAdquiriente = null;
            if (respuesta != "")
                bancoAdquiriente = JsonConvert.DeserializeObject<BancoAdquiriente>(respuesta);
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
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaClientes(int intIdEmpresa, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaClientes', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task AgregarCliente(Cliente cliente, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cliente);
            string strDatos = "{NombreMetodo: 'AgregarCliente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCliente(Cliente cliente, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cliente);
            string strDatos = "{NombreMetodo: 'ActualizarCliente', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Cliente> ObtenerCliente(int intIdCliente, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCliente', Parametros: {IdCliente: " + intIdCliente + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Cliente cliente = null;
            if (respuesta != "")
                cliente = JsonConvert.DeserializeObject<Cliente>(respuesta);
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
                cliente = JsonConvert.DeserializeObject<Cliente>(respuesta);
            return cliente;
        }


        public static async Task<List<LlaveDescripcion>> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoLineas', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task ActualizarEmpresa(Empresa empresa, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(empresa);
            string strDatos = "{NombreMetodo: 'ActualizarEmpresa', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(sucursal);
            string strDatos = "{NombreMetodo: 'ActualizarSucursalPorEmpresa', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarTerminalPorSucursal(TerminalPorSucursal terminal, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(terminal);
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

        public static async Task AgregarCredencialesHacienda(int intIdEmpresa, string strUsuario, string strClave, string strNombreCertificado, string strPin, string strCertificado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AgregarCredencialesHacienda', Parametros: {IdEmpresa: " + intIdEmpresa + ", Usuario: '" + strUsuario + "', Clave: '" + strClave + "', NombreCertificado: '" + strNombreCertificado + "', PinCertificado: '" + strPin + "', Certificado: '" + strCertificado + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCredencialesHacienda(int intIdEmpresa, string strUsuario, string strClave, string strNombreCertificado, string strPin, string strCertificado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarCredencialesHacienda', Parametros: {IdEmpresa: " + intIdEmpresa + ", Usuario: '" + strUsuario + "', Clave: '" + strClave + "', NombreCertificado: '" + strNombreCertificado + "', PinCertificado: '" + strPin + "', Certificado: '" + strCertificado + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AgregarLinea(Linea linea, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(linea);
            string strDatos = "{NombreMetodo: 'AgregarLinea', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarLinea(Linea linea, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(linea);
            string strDatos = "{NombreMetodo: 'ActualizarLinea', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Linea> ObtenerLinea(int intIdLinea, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerLinea', Parametros: {IdLinea: " + intIdLinea + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Linea linea = null;
            if (respuesta != "")
                linea = JsonConvert.DeserializeObject<Linea>(respuesta);
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
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaProveedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);

            return intCantidad;
        }

        public static async Task AgregarProveedor(Proveedor proveedor, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(proveedor);
            string strDatos = "{NombreMetodo: 'AgregarProveedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarProveedor(Proveedor proveedor, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(proveedor);
            string strDatos = "{NombreMetodo: 'ActualizarProveedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Proveedor> ObtenerProveedor(int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProveedor', Parametros: {IdProveedor: " + intIdProveedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Proveedor proveedor = null;
            if (respuesta != "")
                proveedor = JsonConvert.DeserializeObject<Proveedor>(respuesta);
            return proveedor;
        }

        public static async Task EliminarProveedor(int intIdProveedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarProveedor', Parametros: {IdProveedor: " + intIdProveedor + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaClasificacionProducto(string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaClasificacionProducto', Parametros: {Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<ClasificacionProducto>> ObtenerListadoClasificacionProducto(int intNumeroPagina, int intFilasPorPagina, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoClasificacionProducto', Parametros: {NumeroPagina: " + intNumeroPagina + ", FilasPorPagina: " + intFilasPorPagina + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ClasificacionProducto> listado = new List<ClasificacionProducto>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ClasificacionProducto>>(respuesta);
            return listado;
        }

        public static async Task<ClasificacionProducto> ObtenerClasificacionProducto(string strCodigo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerClasificacionProducto', Parametros: {Codigo: '" + strCodigo + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            ClasificacionProducto clasificacionProducto = null;
            if (respuesta != "")
                clasificacionProducto = JsonConvert.DeserializeObject<ClasificacionProducto>(respuesta);
            return clasificacionProducto;
        }

        public static async Task<int> ObtenerTotalListaProductos(int intIdEmpresa, int intIdSucursal, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFitraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaProductos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IncluyeServicios: '" + bolIncluyeServicios + "', FiltraActivos: '" + bolFiltraActivos + "', FiltraExistencias: '" + bolFitraExistencias + "', FiltraConDescuento: '" + bolFiltraConDescuento + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', CodigoProveedor: '" + strCodigoProveedor + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<ProductoDetalle>> ObtenerListadoProductos(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFitraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProductos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ", FilasPorPagina: " + intFilasPorPagina + ", IncluyeServicios: '" + bolIncluyeServicios + "', FiltraActivos: '" + bolFiltraActivos + "', FiltraExistencias: '" + bolFitraExistencias + "', FiltraConDescuento: '" + bolFiltraConDescuento + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', CodigoProveedor: '" + strCodigoProveedor + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ProductoDetalle> listado = new List<ProductoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ProductoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalMovimientosPorProducto(int intIdProducto, int intIdSucursal, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalMovimientosPorProducto', Parametros: {IdProducto: " + intIdProducto + ", IdSucursal: " + intIdSucursal + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<MovimientoProducto>> ObtenerMovimientosPorProducto(int intIdProducto, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, string strFechaInicial, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientosPorProducto', Parametros: {IdProducto: " + intIdProducto + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ", FilasPorPagina: " + intFilasPorPagina + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<MovimientoProducto> listado = new List<MovimientoProducto>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<MovimientoProducto>>(respuesta);
            return listado;
        }

        public static async Task AgregarProducto(Producto producto, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(producto);
            string strDatos = "{NombreMetodo: 'AgregarProducto', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarProducto(Producto producto, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(producto);
            string strDatos = "{NombreMetodo: 'ActualizarProducto', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Producto> ObtenerProducto(int intIdProducto, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProducto', Parametros: {IdProducto: " + intIdProducto + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = JsonConvert.DeserializeObject<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoTransitorio(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProductoEspecial', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdTipo: 4}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = JsonConvert.DeserializeObject<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoImpuestoServicio(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProductoEspecial', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdTipo: 5}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = JsonConvert.DeserializeObject<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProductoPorCodigo', Parametros: {IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "', IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = JsonConvert.DeserializeObject<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigoProveedor(int intIdEmpresa, string strCodigo, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProductoPorCodigoProveedor', Parametros: {IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "', IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Producto producto = null;
            if (respuesta != "")
                producto = JsonConvert.DeserializeObject<Producto>(respuesta);
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
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarUsuario(Usuario usuario, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(usuario);
            string strDatos = "{NombreMetodo: 'AgregarUsuario', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarUsuario(Usuario usuario, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(usuario);
            string strDatos = "{NombreMetodo: 'ActualizarUsuario', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Usuario> ActualizarClaveUsuario(int intIdUsuario, string strClave, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarClaveUsuario', Parametros: {IdUsuario: " + intIdUsuario + ", Clave: '" + strClave + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Usuario usuario = null;
            if (respuesta != "")
                usuario = JsonConvert.DeserializeObject<Usuario>(respuesta);
            return usuario;
        }

        public static async Task<Usuario> ObtenerUsuario(int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerUsuario', Parametros: {IdUsuario: " + intIdUsuario + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Usuario usuario = null;
            if (respuesta != "")
                usuario = JsonConvert.DeserializeObject<Usuario>(respuesta);
            return usuario;
        }

        public static async Task EliminarUsuario(int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarUsuario', Parametros: {IdUsuario: " + intIdUsuario + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasEgreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarCuentaEgreso(CuentaEgreso cuentaEgreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaEgreso);
            string strDatos = "{NombreMetodo: 'AgregarCuentaEgreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCuentaEgreso(CuentaEgreso cuentaEgreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaEgreso);
            string strDatos = "{NombreMetodo: 'ActualizarCuentaEgreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaEgreso> ObtenerCuentaEgreso(int intIdCuentaEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaEgreso', Parametros: {IdCuentaEgreso: " + intIdCuentaEgreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaEgreso cuentaEgreso = null;
            if (respuesta != "")
                cuentaEgreso = JsonConvert.DeserializeObject<CuentaEgreso>(respuesta);
            return cuentaEgreso;
        }

        public static async Task EliminarCuentaEgreso(int intIdCuentaEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCuentaEgreso', Parametros: {IdCuentaEgreso: " + intIdCuentaEgreso + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasIngreso(int intIdEmpresa, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasIngreso', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarCuentaIngreso(CuentaIngreso cuentaIngreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaIngreso);
            string strDatos = "{NombreMetodo: 'AgregarCuentaIngreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCuentaIngreso(CuentaIngreso cuentaIngreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaIngreso);
            string strDatos = "{NombreMetodo: 'ActualizarCuentaIngreso', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaIngreso> ObtenerCuentaIngreso(int intIdCuentaIngreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaIngreso', Parametros: {IdCuentaIngreso: " + intIdCuentaIngreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaIngreso cuentaIngreso = null;
            if (respuesta != "")
                cuentaIngreso = JsonConvert.DeserializeObject<CuentaIngreso>(respuesta);
            return cuentaIngreso;
        }

        public static async Task EliminarCuentaIngreso(int intIdCuentaIngreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCuentaIngreso', Parametros: {IdCuentaIngreso: " + intIdCuentaIngreso + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasBanco', Parametros: {IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarCuentaBanco(CuentaBanco cuentaBanco, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaBanco);
            string strDatos = "{NombreMetodo: 'AgregarCuentaBanco', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarCuentaBanco(CuentaBanco cuentaBanco, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(cuentaBanco);
            string strDatos = "{NombreMetodo: 'ActualizarCuentaBanco', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaBanco> ObtenerCuentaBanco(int intIdCuentaBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaBanco', Parametros: {IdCuentaBanco: " + intIdCuentaBanco + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaBanco cuentaBanco = null;
            if (respuesta != "")
                cuentaBanco = JsonConvert.DeserializeObject<CuentaBanco>(respuesta);
            return cuentaBanco;
        }

        public static async Task EliminarCuentaBanco(int intIdCuentaBanco, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarCuentaBanco', Parametros: {IdCuentaBanco: " + intIdCuentaBanco + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<string> AgregarMovimientoBanco(MovimientoBanco movimiento, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(movimiento);
            string strDatos = "{NombreMetodo: 'AgregarMovimientoBanco', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task<MovimientoBanco> ObtenerMovimientoBanco(int intIdMovimiento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientoBanco', Parametros: {IdMovimiento: " + intIdMovimiento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            MovimientoBanco movimientoBanco = null;
            if (respuesta != "")
                movimientoBanco = JsonConvert.DeserializeObject<MovimientoBanco>(respuesta);
            return movimientoBanco;
        }

        public static async Task AnularMovimientoBanco(int intIdMovimiento, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularMovimientoBanco', Parametros: {IdMovimiento: " + intIdMovimiento + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaMovimientoBanco(int intIdEmpresa, int intIdSucursal, string strDescripcion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaMovimientoBanco', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Descripcion: '" + strDescripcion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListadoMovimientoBanco(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, string strDescripcion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoMovimientoBanco', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Descripcion: '" + strDescripcion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoVendedores(int intIdEmpresa, string strNombre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoVendedores', Parametros: {IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarVendedor(Vendedor vendedor, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(vendedor);
            string strDatos = "{NombreMetodo: 'AgregarVendedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarVendedor(Vendedor vendedor, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(vendedor);
            string strDatos = "{NombreMetodo: 'ActualizarVendedor', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<Vendedor> ObtenerVendedor(int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerVendedor', Parametros: {IdVendedor: " + intIdVendedor + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = JsonConvert.DeserializeObject<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task<Vendedor> ObtenerVendedorPorDefecto(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerVendedorPorDefecto', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = JsonConvert.DeserializeObject<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task EliminarVendedor(int intIdVendedor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarVendedor', Parametros: {IdVendedor: " + intIdVendedor + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaEgresos(int intIdEmpresa, int intIdSucursal, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaEgresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListadoEgresos(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, int intIdEgreso, string strBeneficiario, string strDetalle, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoEgresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Egreso> ObtenerEgreso(int intIdEgreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerEgreso', Parametros: {IdEgreso: " + intIdEgreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Egreso egreso = null;
            if (respuesta != "")
                egreso = JsonConvert.DeserializeObject<Egreso>(respuesta);
            return egreso;
        }

        public static async Task<string> AgregarEgreso(Egreso egreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(egreso);
            string strDatos = "{NombreMetodo: 'AgregarEgreso', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularEgreso(int intIdEgreso, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularEgreso', Parametros: {IdEgreso: " + intIdEgreso + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaIngresos(int intIdEmpresa, int intIdSucursal, int intIdIngreso, string strBeneficiario, string strDetalle, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaIngresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdIngreso: " + intIdIngreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListadoIngresos(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, int intIdIngreso, string strBeneficiario, string strDetalle, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoIngresos', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdIngreso: " + intIdIngreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Ingreso> ObtenerIngreso(int intIdIngreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerIngreso', Parametros: {IdIngreso: " + intIdIngreso + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Ingreso egreso = null;
            if (respuesta != "")
                egreso = JsonConvert.DeserializeObject<Ingreso>(respuesta);
            return egreso;
        }

        public static async Task<string> AgregarIngreso(Ingreso egreso, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(egreso);
            string strDatos = "{NombreMetodo: 'AgregarIngreso', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularIngreso(int intIdIngreso, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularIngreso', Parametros: {IdIngreso: " + intIdIngreso + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int intIdFactura, string strNombre, string strIdentificacion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaFacturas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IncluyeNulos: '" + bolIncluyeNulos + "', IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "', Identificacion: '" + strIdentificacion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int intNumeroPagina, int intFilasPorPagina, int intIdFactura, string strNombre, string strIdentificacion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoFacturas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IncluyeNulos: '" + bolIncluyeNulos + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "', Identificacion: '" + strIdentificacion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Factura> ObtenerFactura(int intIdFactura, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerFactura', Parametros: {IdFactura: " + intIdFactura + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Factura factura = null;
            if (respuesta != "")
                factura = JsonConvert.DeserializeObject<Factura>(respuesta);
            return factura;
        }

        public static async Task<string> AgregarFactura(Factura factura, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(factura);
            string strDatos = "{NombreMetodo: 'AgregarFactura', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task<string> AgregarFacturaCompra(FacturaCompra facturaCompra, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(facturaCompra);
            string strDatos = "{NombreMetodo: 'AgregarFacturaCompra', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularFactura(int intIdFactura, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularFactura', Parametros: {IdFactura: " + intIdFactura + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, int intIdDevolucion, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaDevolucionesPorCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdDevolucion: " + intIdDevolucion + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, int intIdDevolucion, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDevolucionesPorCliente', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdDevolucion: " + intIdDevolucion + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<DevolucionCliente> ObtenerDevolucionCliente(int intIdDevolucion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerDevolucionCliente', Parametros: {IdDevolucion: " + intIdDevolucion + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DevolucionCliente devolucion = null;
            if (respuesta != "")
                devolucion = JsonConvert.DeserializeObject<DevolucionCliente>(respuesta);
            return devolucion;
        }

        public static async Task<string> AgregarDevolucionCliente(DevolucionCliente devolucion, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(devolucion);
            string strDatos = "{NombreMetodo: 'AgregarDevolucionCliente', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularDevolucionCliente', Parametros: {IdDevolucion: " + intIdDevolucion + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdProforma, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaProformas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', IdProforma: " + intIdProforma + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intNumeroPagina, int intFilasPorPagina, int intIdProforma, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoProformas', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdProforma: " + intIdProforma + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Proforma> ObtenerProforma(int intIdProforma, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerProforma', Parametros: {IdProforma: " + intIdProforma + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Proforma proforma = null;
            if (respuesta != "")
                proforma = JsonConvert.DeserializeObject<Proforma>(respuesta);
            return proforma;
        }

        public static async Task<string> AgregarProforma(Proforma Proforma, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(Proforma);
            string strDatos = "{NombreMetodo: 'AgregarProforma', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task ActualizarProforma(Proforma proforma, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(proforma);
            string strDatos = "{NombreMetodo: 'ActualizarProforma', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularProforma(int intIdProforma, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularProforma', Parametros: {IdProforma: " + intIdProforma + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, bool bolExcluyeCancelados, int intIdApartado, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaApartados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', ExcluyeCancelados: '" + bolExcluyeCancelados + "', IdApartado: " + intIdApartado + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, bool bolExcluyeCancelados, int intNumeroPagina, int intFilasPorPagina, int intIdApartado, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoApartados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', ExcluyeCancelados: '" + bolExcluyeCancelados + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdApartado: " + intIdApartado + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Apartado> ObtenerApartado(int intIdApartado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerApartado', Parametros: {IdApartado: " + intIdApartado + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Apartado Apartado = null;
            if (respuesta != "")
                Apartado = JsonConvert.DeserializeObject<Apartado>(respuesta);
            return Apartado;
        }

        public static async Task<string> AgregarApartado(Apartado Apartado, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(Apartado);
            string strDatos = "{NombreMetodo: 'AgregarApartado', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularApartado(int intIdApartado, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularApartado', Parametros: {IdApartado: " + intIdApartado + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolFiltraEstado, bool bolAplicado, bool bolIncluyeNulos, bool bolExcluyeCancelados,int intIdOrdenServicio, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaOrdenServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FiltraEstado: '" + bolFiltraEstado + "', Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', ExcluyeCancelados: '" + bolExcluyeCancelados + "', IdOrdenServicio: " + intIdOrdenServicio + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<FacturaDetalle>> ObtenerListadoOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolFiltraEstado, bool bolAplicado, bool bolIncluyeNulos, bool bolExcluyeCancelados, int intNumeroPagina, int intFilasPorPagina, int intIdOrdenServicio, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoOrdenServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FiltraEstado: '" + bolFiltraEstado + "', Aplicado: '" + bolAplicado + "', IncluyeNulos: '" + bolIncluyeNulos + "', ExcluyeCancelados: '" + bolExcluyeCancelados + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdOrdenServicio: " + intIdOrdenServicio + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<FacturaDetalle> listado = new List<FacturaDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<FacturaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<OrdenServicio> ObtenerOrdenServicio(int intIdOrdenServicio, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerOrdenServicio', Parametros: {IdOrdenServicio: " + intIdOrdenServicio + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            OrdenServicio OrdenServicio = null;
            if (respuesta != "")
                OrdenServicio = JsonConvert.DeserializeObject<OrdenServicio>(respuesta);
            return OrdenServicio;
        }

        public static async Task<string> AgregarOrdenServicio(OrdenServicio OrdenServicio, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(OrdenServicio);
            string strDatos = "{NombreMetodo: 'AgregarOrdenServicio', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task ActualizarOrdenServicio(OrdenServicio OrdenServicio, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(OrdenServicio);
            string strDatos = "{NombreMetodo: 'ActualizarOrdenServicio', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularOrdenServicio', Parametros: {IdOrdenServicio: " + intIdOrdenServicio + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaCompras(int intIdEmpresa, int intIdSucursal, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaCompras', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdCompra: " + intIdCompra + ", RefFactura: '" + strRefFactura + "', Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<CompraDetalle>> ObtenerListadoCompras(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCompras', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdCompra: " + intIdCompra + ", RefFactura: '" + strRefFactura + "', Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<CompraDetalle> listado = new List<CompraDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<CompraDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Compra> ObtenerCompra(int intIdCompra, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCompra', Parametros: {IdCompra: " + intIdCompra + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Compra compra = null;
            if (respuesta != "")
                compra = JsonConvert.DeserializeObject<Compra>(respuesta);
            return compra;
        }

        public static async Task<string> AgregarCompra(Compra compra, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(compra);
            string strDatos = "{NombreMetodo: 'AgregarCompra', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularCompra(int intIdCompra, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularCompra', Parametros: {IdCompra: " + intIdCompra + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaTraslados(int intIdEmpresa, int intIdSucursalOrigen, bool bolAplicado, int intIdTraslado, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaTraslados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursalOrigen: " + intIdSucursalOrigen + ", Aplicado: '" + bolAplicado + "', IdTraslado: " + intIdTraslado + ", FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<TrasladoDetalle>> ObtenerListadoTraslados(int intIdEmpresa, int intIdSucursalOrigen, bool bolAplicado, int intNumeroPagina, int intFilasPorPagina, int intIdTraslado, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTraslados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursalOrigen: " + intIdSucursalOrigen + ", Aplicado: '" + bolAplicado + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdTraslado: " + intIdTraslado + ", FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<TrasladoDetalle> listado = new List<TrasladoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<TrasladoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaTrasladosPorAplicar(int intIdEmpresa, int intIdSucursalDestino, bool bolAplicado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaTrasladosPorAplicar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursalDestino: " + intIdSucursalDestino + ", Aplicado: '" + bolAplicado + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<TrasladoDetalle>> ObtenerListadoTrasladosPorAplicar(int intIdEmpresa, int intIdSucursalDestino, bool bolAplicado, int intNumeroPagina, int intFilasPorPagina, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTrasladosPorAplicar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursalDestino: " + intIdSucursalDestino + ", Aplicado: '" + bolAplicado + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<TrasladoDetalle> listado = new List<TrasladoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<TrasladoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<Traslado> ObtenerTraslado(int intIdTraslado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTraslado', Parametros: {IdTraslado: " + intIdTraslado + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            Traslado Traslado = null;
            if (respuesta != "")
                Traslado = JsonConvert.DeserializeObject<Traslado>(respuesta);
            return Traslado;
        }

        public static async Task<string> AgregarTraslado(Traslado Traslado, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(Traslado);
            string strDatos = "{NombreMetodo: 'AgregarTraslado', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AplicarTraslado(int intIdTraslado, int intIdUsuario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AplicarTraslado', Parametros: {IdTraslado: " + intIdTraslado + ", IdUsuario: " + intIdUsuario + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularTraslado(int intIdTraslado, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularTraslado', Parametros: {IdTraslado: " + intIdTraslado + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalListaAjusteInventario(int intIdEmpresa, int intIdSucursal, int intIdAjuste, string strDescripcion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaAjusteInventario', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdAjuste: " + intIdAjuste + ", Descripcion: '" + strDescripcion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<AjusteInventarioDetalle>> ObtenerListadoAjusteInventario(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, int intIdAjuste, string strDescripcion, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoAjusteInventario', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdAjuste: " + intIdAjuste + ", Descripcion: '" + strDescripcion + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<AjusteInventarioDetalle> listado = new List<AjusteInventarioDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<AjusteInventarioDetalle>>(respuesta);
            return listado;
        }

        public static async Task<AjusteInventario> ObtenerAjusteInventario(int intIdAjusteInventario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerAjusteInventario', Parametros: {IdAjuste: " + intIdAjusteInventario + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            AjusteInventario ajusteInventario = null;
            if (respuesta != "")
                ajusteInventario = JsonConvert.DeserializeObject<AjusteInventario>(respuesta);
            return ajusteInventario;
        }

        public static async Task<string> AgregarAjusteInventario(AjusteInventario AjusteInventario, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(AjusteInventario);
            string strDatos = "{NombreMetodo: 'AgregarAjusteInventario', Entidad: " + strEntidad + "}";
            string strId = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            return JsonConvert.DeserializeObject<string>(strId);
        }

        public static async Task AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularAjusteInventario', Parametros: {IdAjuste: " + intIdAjusteInventario + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaPorCobrar> ObtenerCuentaPorCobrar(int intIdCxC, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaPorCobrar', Parametros: {IdCxC: " + intIdCxC + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaPorCobrar cuentaPorCobrar = null;
            if (respuesta != "")
                cuentaPorCobrar = JsonConvert.DeserializeObject<CuentaPorCobrar>(respuesta);
            return cuentaPorCobrar;
        }

        public static async Task<int> ObtenerTotalListaCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaCuentasPorCobrar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTipo: " + intIdTipo + ", Pendientes: '" + bolPendientes + "', Referencia: '" + strReferencia + "', NombrePropietario: '" + strNombrePropietario + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<CuentaPorProcesar>> ObtenerListadoCuentasPorCobrar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int intNumeroPagina, int intFilasPorPagina, string strReferencia, string strNombrePropietario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasPorCobrar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTipo: " + intIdTipo + ", Pendientes: '" + bolPendientes + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Referencia: '" + strReferencia + "', NombrePropietario: '" + strNombrePropietario + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<CuentaPorProcesar> listado = new List<CuentaPorProcesar>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<CuentaPorProcesar>>(respuesta);
            return listado;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListaMovimientosCxC(int intIdEmpresa, int intIdSucursal, int intIdCuenta, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListaMovimientosCxC', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdCuenta: " + intIdCuenta + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<MovimientoCuentaPorCobrar> ObtenerMovimientoCxC(int intIdMovimiento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientoCxC', Parametros: {IdMovimiento: " + intIdMovimiento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            MovimientoCuentaPorCobrar movimientoCxC = null;
            if (respuesta != "")
                movimientoCxC = JsonConvert.DeserializeObject<MovimientoCuentaPorCobrar>(respuesta);
            return movimientoCxC;
        }

        public static async Task AplicarMovimientoCxC(MovimientoCuentaPorCobrar movimiento, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(movimiento);
            string strDatos = "{NombreMetodo: 'AplicarMovimientoCxC', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularMovimientoCxC(int intIdMovimiento, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularMovimientoCxC', Parametros: {IdMovimiento: " + intIdMovimiento + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<CuentaPorPagar> ObtenerCuentaPorPagar(int intIdCxP, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCuentaPorPagar', Parametros: {IdCxP: " + intIdCxP + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CuentaPorPagar cuentaPorPagar = null;
            if (respuesta != "")
                cuentaPorPagar = JsonConvert.DeserializeObject<CuentaPorPagar>(respuesta);
            return cuentaPorPagar;
        }

        public static async Task<int> ObtenerTotalListaCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, string strReferencia, string strNombrePropietario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaCuentasPorPagar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTipo: " + intIdTipo + ", Pendientes: '" + bolPendientes + "', Referencia: '" + strReferencia + "', NombrePropietario: '" + strNombrePropietario + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<CuentaPorProcesar>> ObtenerListadoCuentasPorPagar(int intIdEmpresa, int intIdSucursal, int intIdTipo, bool bolPendientes, int intNumeroPagina, int intFilasPorPagina, string strReferencia, string strNombrePropietario, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCuentasPorPagar', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTipo: " + intIdTipo + ", Pendientes: '" + bolPendientes + "', NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Referencia: '" + strReferencia + "', NombrePropietario: '" + strNombrePropietario + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<CuentaPorProcesar> listado = new List<CuentaPorProcesar>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<CuentaPorProcesar>>(respuesta);
            return listado;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListaMovimientosCxP(int intIdEmpresa, int intIdSucursal, int intIdCuenta, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListaMovimientosCxP', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdCuenta: " + intIdCuenta + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<MovimientoCuentaPorPagar> ObtenerMovimientoCxP(int intIdMovimiento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientoCxP', Parametros: {IdMovimiento: " + intIdMovimiento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            MovimientoCuentaPorPagar movimientoCxP = null;
            if (respuesta != "")
                movimientoCxP = JsonConvert.DeserializeObject<MovimientoCuentaPorPagar>(respuesta);
            return movimientoCxP;
        }

        public static async Task AplicarMovimientoCxP(MovimientoCuentaPorPagar movimiento, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(movimiento);
            string strDatos = "{NombreMetodo: 'AplicarMovimientoCxP', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularMovimientoCxP(int intIdMovimiento, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularMovimientoCxP', Parametros: {IdMovimiento: " + intIdMovimiento + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoApartadosConSaldo(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoApartadosConSaldo', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListadoMovimientosApartado(int intIdEmpresa, int intIdSucursal, int intIdApartado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoMovimientosApartado', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdApartado: " + intIdApartado + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<MovimientoApartado> ObtenerMovimientoApartado(int intIdMovimiento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientoApartado', Parametros: {IdMovimiento: " + intIdMovimiento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            MovimientoApartado movimientoCxC = null;
            if (respuesta != "")
                movimientoCxC = JsonConvert.DeserializeObject<MovimientoApartado>(respuesta);
            return movimientoCxC;
        }

        public static async Task AplicarMovimientoApartado(MovimientoApartado movimiento, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(movimiento);
            string strDatos = "{NombreMetodo: 'AplicarMovimientoApartado', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularMovimientoApartado(int intIdMovimiento, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularMovimientoApartado', Parametros: {IdMovimiento: " + intIdMovimiento + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoOrdenesServicioConSaldo(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoOrdenesServicioConSaldo', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<EfectivoDetalle>> ObtenerListadoMovimientosOrdenServicio(int intIdEmpresa, int intIdSucursal, int intIdOrdenServicio, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoMovimientosOrdenServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdOrden: " + intIdOrdenServicio + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<EfectivoDetalle> listado = new List<EfectivoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<EfectivoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<MovimientoOrdenServicio> ObtenerMovimientoOrdenServicio(int intIdMovimiento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerMovimientoOrdenServicio', Parametros: {IdMovimiento: " + intIdMovimiento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            MovimientoOrdenServicio movimientoCxC = null;
            if (respuesta != "")
                movimientoCxC = JsonConvert.DeserializeObject<MovimientoOrdenServicio>(respuesta);
            return movimientoCxC;
        }

        public static async Task AplicarMovimientoOrdenServicio(MovimientoOrdenServicio movimiento, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(movimiento);
            string strDatos = "{NombreMetodo: 'AplicarMovimientoOrdenServicio', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task AnularMovimientoOrdenServicio(int intIdMovimiento, int intIdUsuario, string strMotivo, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AnularMovimientoOrdenServicio', Parametros: {IdMovimiento: " + intIdMovimiento + ", IdUsuario: " + intIdUsuario + ", MotivoAnulacion: '" + strMotivo + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronico(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerDocumentoElectronico', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = JsonConvert.DeserializeObject<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task<int> ObtenerTotalListaCierreCaja(int intIdEmpresa, int intIdSucursal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalListaCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCierreCaja(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoCierreCaja', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<CierreCaja> ObtenerCierreCaja(int intIdCierre, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerCierreCaja', Parametros: {IdCierre: " + intIdCierre + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            CierreCaja cierre = null;
            if (respuesta != "")
                cierre = JsonConvert.DeserializeObject<CierreCaja>(respuesta);
            return cierre;
        }


        public static async Task GenerarMensajeReceptor(string strMensaje, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado, bool bolIvaAcreditable, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GenerarMensajeReceptor', Parametros: {Datos: '" + strMensaje + "', IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intSucursal + ", IdTerminal: " + intTerminal + ", IdEstado: " + intEstado + ", IvaAcreditable: '" + bolIvaAcreditable + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<int> ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerTotalDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = JsonConvert.DeserializeObject<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, int intNumeroPagina, int intFilasPorPagina, string strNombre, string strFechaFinal, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDocumentosElectronicosProcesados', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", NumeroPagina: " + intNumeroPagina + ", FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "', FechaFinal: '" + strFechaFinal + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoDocumentosElectronicosEnProceso', Parametros: {IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<DocumentoElectronico> ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerRespuestaDocumentoElectronicoEnviado', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = JsonConvert.DeserializeObject<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task<bool> EnviarNotificacion(int intIdDocumento, string strCorreoReceptor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EnviarNotificacionDocumentoElectronico', Parametros: {IdDocumento: " + intIdDocumento + ", CorreoReceptor: '" + strCorreoReceptor + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
            return true;
        }

        public static async Task<bool> GenerarNotificacionProforma(int intIdProforma, string strCorreoReceptor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GenerarNotificacionProforma', Parametros: {IdProforma: " + intIdProforma + ", CorreoReceptor: '" + strCorreoReceptor + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
            return true;
        }

        public static async Task<bool> GenerarNotificacionOrdenServicio(int intIdOrden, string strCorreoReceptor, string strToken)
        {
            string strDatos = "{NombreMetodo: 'GenerarNotificacionOrdenServicio', Parametros: {IdOrden: " + intIdOrden + ", CorreoReceptor: '" + strCorreoReceptor + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
            return true;
        }

        public static async Task<bool> ReprocesarDocumentoElectronico(int intIdDocumento, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ReprocesarDocumentoElectronico', Parametros: {IdDocumento: " + intIdDocumento + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
            return true;
        }

        public static async Task<decimal> AutorizacionPorcentaje(string strCodigoUsuario, string strClave, int intIdEmpresa, string strToken)
        {
            string strDatos = "{NombreMetodo: 'AutorizacionPorcentaje', Parametros: {CodigoUsuario: '" + strCodigoUsuario + "', Clave: '" + strClave + "', IdEmpresa: " + intIdEmpresa + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            decimal decPorcentaje = 0;
            if (respuesta != "")
                decPorcentaje = JsonConvert.DeserializeObject<decimal>(respuesta);
            return decPorcentaje;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoPuntoDeServicio(int intIdEmpresa, int intIdSucursal, bool bolFiltraActivos, string strDescripcion, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoPuntoDeServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", FiltraActivos: '" + bolFiltraActivos + "', Descripcion: '" + strDescripcion + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AgregarPuntoDeServicio(PuntoDeServicio puntoDeServicio, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(puntoDeServicio);
            string strDatos = "{NombreMetodo: 'AgregarPuntoDeServicio', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task ActualizarPuntoDeServicio(PuntoDeServicio puntoDeServicio, string strToken)
        {
            string strEntidad = JsonConvert.SerializeObject(puntoDeServicio);
            string strDatos = "{NombreMetodo: 'ActualizarPuntoDeServicio', Entidad: " + strEntidad + "}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<PuntoDeServicio> ObtenerPuntoDeServicio(int intIdPunto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerPuntoDeServicio', Parametros: {IdPuntoDeServicio: " + intIdPunto + "}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            PuntoDeServicio puntoDeServicio = null;
            if (respuesta != "")
                puntoDeServicio = JsonConvert.DeserializeObject<PuntoDeServicio>(respuesta);
            return puntoDeServicio;
        }

        public static async Task EliminarPuntoDeServicio(int intIdPunto, string strToken)
        {
            string strDatos = "{NombreMetodo: 'EliminarPuntoDeServicio', Parametros: {IdPuntoDeServicio: " + intIdPunto + "}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }

        public static async Task<List<ClsTiquete>> ObtenerListadoTiqueteOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolImpreso, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ObtenerListadoTiqueteOrdenServicio', Parametros: {IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", Impreso: '" + bolImpreso + "'}}";
            string respuesta = await EjecutarConsulta(strDatos, strServicioPuntoventaURL, strToken);
            List<ClsTiquete> listado = new List<ClsTiquete>();
            if (respuesta != "")
                listado = JsonConvert.DeserializeObject<List<ClsTiquete>>(respuesta);
            return listado;
        }

        public static async Task ActualizarEstadoTiqueteOrdenServicio(int intIdTiquete, bool bolEstado, string strToken)
        {
            string strDatos = "{NombreMetodo: 'ActualizarEstadoTiqueteOrdenServicio', Parametros: {IdTiquete: " + intIdTiquete + ", Estado: '" + bolEstado + "'}}";
            await Ejecutar(strDatos, strServicioPuntoventaURL, strToken);
        }
    }
}
