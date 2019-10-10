using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.Core.CommonTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LeandroSoftware.Core.ClienteWCF
{
    public static class ClienteFEWCF
    {
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioPuntoventaURL = ConfigurationManager.AppSettings["ServicioPuntoventaURL"];
        private static string strServicioRecepcionURL = ConfigurationManager.AppSettings["ServicioRecepcionURL"];
        private static HttpClient httpClient = new HttpClient();

        private static async Task Ejecutar(RequestDTO peticion, string servicioURL, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strContent = serializer.Serialize(peticion);
                StringContent contentJson = new StringContent(strContent, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutar", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                    throw new Exception(httpResponse.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<string> EjecutarConsulta(RequestDTO peticion, string servicioURL, string strToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                string strContent = serializer.Serialize(peticion);
                StringContent contentJson = new StringContent(strContent, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutarconsulta", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(httpResponse.ReasonPhrase);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                ResponseDTO response = serializer.Deserialize<ResponseDTO>(responseContent);
                return response.DatosRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<EquipoRegistrado>> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTerminalesDisponibles",
                DatosPeticion = "{Usuario: '" + strUsuario + "', Clave: '" + strClave + "', Identificacion: '" + strIdentificacion + "', TipoDispositivo: " + intTipoDispositivo + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<EquipoRegistrado> listado = new List<EquipoRegistrado>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<EquipoRegistrado>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasAdministrador()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoEmpresasAdministrador",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEmpresasPorTerminal(string strIdDispositivo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoEmpresasPorTerminal",
                DatosPeticion = "{Dispositivo: '" + strIdDispositivo + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Empresa empresa = null;
            if (respuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<List<SucursalPorEmpresa>> ObtenerListadoSucursalPorEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoSucursalPorEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<SucursalPorEmpresa> listado = new List<SucursalPorEmpresa>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<SucursalPorEmpresa>>(respuesta);
            return listado;
        }

        public static async Task<SucursalPorEmpresa> ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerSucursalPorEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            SucursalPorEmpresa sucursal = null;
            if (respuesta != "")
                sucursal = serializer.Deserialize<SucursalPorEmpresa>(respuesta);
            return sucursal;
        }

        public static async Task<List<TerminalPorSucursal>> ObtenerListadoTerminalPorSucursal(int intIdEmpresa, int intIdSucursal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTerminalPorSucursal",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TerminalPorSucursal> listado = new List<TerminalPorSucursal>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<TerminalPorSucursal>>(respuesta);
            return listado;
        }

        public static async Task<TerminalPorSucursal> ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTerminalPorSucursal",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTerminal: " + intIdTerminal + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            TerminalPorSucursal terminal = null;
            if (respuesta != "")
                terminal = serializer.Deserialize<TerminalPorSucursal>(respuesta);
            return terminal;
        }

        public static async Task<Empresa> ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ValidarCredenciales",
                DatosPeticion = "{Usuario: '" + strUsuario + "', Clave: '" + strClave + "', IdEmpresa: " + intIdEmpresa + ", ValorRegistro: '" + strValorRegistro + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Empresa empresa = null;
            if (respuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta);
            return empresa;
        }

        public static async Task<decimal> ObtenerTipoCambioDolar()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTipoCambioDolar",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            decimal decTipoCambioDolar = 0;
            if (respuesta != "")
                decTipoCambioDolar = serializer.Deserialize<decimal>(respuesta);
            return decTipoCambioDolar;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoIdentificacion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoIdentificacion",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoModulos()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoModulos",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCatalogoReportes()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCatalogoReportes",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProvincias()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoProvincias",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);;
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCantones(int intIdProvincia)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoProducto()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoProducto",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoExoneracion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoExoneracion",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoImpuesto()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoImpuesto",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoUnidad()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoUnidad",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoFormaPagoEgreso()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoFormaPagoEgreso",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoFormaPagoFactura()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoFormaPagoFactura",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoRoles()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoRoles",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipodePrecio()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipodePrecio",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoTipoMoneda()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoTipoMoneda",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVenta()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCondicionVenta",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoFactura()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCondicionVentaYFormaPagoFactura",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCondicionVentaYFormaPagoCompra()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCondicionVentaYFormaPagoCompra",
                DatosPeticion = ""
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentas>> ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intIdTipoPago, int intIdBancoAdquiriente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorCliente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", isNulo: '" + bolNulo + "', IdTipoPago: " + intIdTipoPago + ", IdBancoAdquiriente: " + intIdBancoAdquiriente + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentas> listado = new List<ReporteVentas>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentas>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorVendedor>> ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorVendedor",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdVendedor: " + intIdVendedor + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorVendedor> listado = new List<ReporteVentasPorVendedor>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorVendedor>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCompras>> ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intFormaPago)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteComprasPorProveedor",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + ", FormaPago: " + intFormaPago + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCompras> listado = new List<ReporteCompras>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCompras>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorCobrar>> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteCuentasPorCobrarClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCuentasPorCobrar> listado = new List<ReporteCuentasPorCobrar>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorCobrar>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorPagar>> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteCuentasPorPagarProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCuentasPorPagar> listado = new List<ReporteCuentasPorPagar>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorPagar>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxC>> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosCxCClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosCxC> listado = new List<ReporteMovimientosCxC>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxC>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxP>> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosCxPProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosCxP> listado = new List<ReporteMovimientosCxP>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxP>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosBanco>> ObtenerReporteMovimientosBanco(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosBanco",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosBanco> listado = new List<ReporteMovimientosBanco>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosBanco>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteEstadoResultados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteEstadoResultados>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleEgreso>> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteDetalleEgreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdCuentaEgreso: " + intIdCuentaEgreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDetalleEgreso> listado = new List<ReporteDetalleEgreso>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleEgreso>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleIngreso>> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteDetalleIngreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdCuentaIngreso: " + intIdCuentaIngreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDetalleIngreso> listado = new List<ReporteDetalleIngreso>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleIngreso>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaResumen>> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorLineaResumen",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorLineaResumen> listado = new List<ReporteVentasPorLineaResumen>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaResumen>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaDetalle>> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorLineaDetalle",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdLinea: " + intIdLinea + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorLineaDetalle> listado = new List<ReporteVentasPorLineaDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteFacturasElectronicasEmitidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteNotasCreditoElectronicasEmitidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteFacturasElectronicasRecibidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteNotasCreditoElectronicasRecibidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteResumenDocumentosElectronicos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteEstadoResultados>>(respuesta);
            return listado;
        }

        public static async Task<CierreCaja> GenerarDatosCierreCaja(int intIdEmpresa, string strFechaCierre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "GenerarDatosCierreCaja",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaCierre: '" + strFechaCierre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CierreCaja cierre = null;
            if (respuesta != "")
                cierre = serializer.Deserialize<CierreCaja>(respuesta);
            return cierre;
        }

        public static async Task<CierreCaja> GuardarDatosCierreCaja(CierreCaja cierre)
        {
            string strDatos = serializer.Serialize(cierre);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "GuardarDatosCierreCaja",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CierreCaja nuevoCierre = null;
            if (respuesta != "")
                nuevoCierre = serializer.Deserialize<CierreCaja>(respuesta);
            return nuevoCierre;
        }

        public static async Task ActualizarUltimaVersionApp(string strVersion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarUltimaVersionApp",
                DatosPeticion = "{Version: '" + strVersion + "'}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task AbortarCierreCaja(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AbortarCierreCaja",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<ReporteCierreDeCaja>> ObtenerReporteCierreDeCaja(int intIdCierre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteCierreDeCaja",
                DatosPeticion = "{IdCierre: " + intIdCierre + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCierreDeCaja> listado = new List<ReporteCierreDeCaja>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<ReporteCierreDeCaja>>(respuesta);
            return listado;
        }

        public static async Task<ParametroImpuesto> ObtenerParametroImpuesto(int intIdImpuesto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerParametroImpuesto",
                DatosPeticion = "{IdImpuesto: " + intIdImpuesto + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            ParametroImpuesto parametroImpuesto = null;
            if (respuesta != "")
                parametroImpuesto = serializer.Deserialize<ParametroImpuesto>(respuesta);
            return parametroImpuesto;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoBancoAdquiriente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            string strDatos = serializer.Serialize(bancoAdquiriente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarBancoAdquiriente",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            string strDatos = serializer.Serialize(bancoAdquiriente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarBancoAdquiriente",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<BancoAdquiriente> ObtenerBancoAdquiriente(int intIdBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerBancoAdquiriente",
                DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            BancoAdquiriente bancoAdquiriente = null;
            if (respuesta != "")
                bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(respuesta);
            return bancoAdquiriente;
        }

        public static async Task EliminarBancoAdquiriente(int intIdBanco)
    {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarBancoAdquiriente",
                DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoClientes(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaClientes(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<string> AgregarCliente(Cliente cliente)
        {
            string strDatos = serializer.Serialize(cliente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCliente",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarCliente(Cliente cliente)
        {
            string strDatos = serializer.Serialize(cliente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCliente",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Cliente> ObtenerCliente(int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCliente",
                DatosPeticion = "{IdCliente: " + intIdCliente + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Cliente cliente = null;
            if (respuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta);
            return cliente;
        }

        public static async Task EliminarCliente(int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCliente",
                DatosPeticion = "{IdCliente: " + intIdCliente + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Cliente> ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ValidaIdentificacionCliente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Identificacion: '" + strIdentificacion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Cliente cliente = null;
            if (respuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta);
            return cliente;
        }


        public static async Task<List<LlaveDescripcion>> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoLineas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoLineasDeProducto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoLineasDeProducto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarEmpresa(Empresa empresa)
        {
            string strDatos = serializer.Serialize(empresa);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarEmpresa",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarEmpresa(Empresa empresa)
        {
            string strDatos = serializer.Serialize(empresa);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarEmpresa",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarEmpresaConDetalle(Empresa empresa)
        {
            string strDatos = serializer.Serialize(empresa);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarEmpresaConDetalle",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task RegistrarTerminal(string strCodigoUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "RegistrarTerminal",
                DatosPeticion = "{Usuario: '" + strCodigoUsuario + "', Clave: '" + strClave + "', Identificacion: '" + strIdentificacion + "', IdSucursal: " + intIdSucursal + ", IdTerminal: " + intIdTerminal + ",TipoDispositivo: " + intTipoDispositivo + ",Dispositivo: '" + strDispositivoId + "'}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            string strDatos = serializer.Serialize(sucursal);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarSucursalPorEmpresa",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            string strDatos = serializer.Serialize(sucursal);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarSucursalPorEmpresa",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task AgregarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            string strDatos = serializer.Serialize(terminal);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarTerminalPorSucursal",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            string strDatos = serializer.Serialize(terminal);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarTerminalPorSucursal",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarLogoEmpresa(int intIdEmpresa, string strLogotipo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarLogoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Logotipo: '" + strLogotipo + "'}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task RemoverLogoEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "RemoverLogoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCertificadoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Certificado: '" + strCertificado + "'}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoLineasDeServicio(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoLineasDeServicio",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarLinea(Linea linea)
        {
            string strDatos = serializer.Serialize(linea);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarLinea",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarLinea(Linea linea)
        {
            string strDatos = serializer.Serialize(linea);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarLinea",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Linea> ObtenerLinea(int intIdLinea)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerLinea",
                DatosPeticion = "{IdLinea: " + intIdLinea + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Linea linea = null;
            if (respuesta != "")
                linea = serializer.Deserialize<Linea>(respuesta);
            return linea;
        }

        public static async Task EliminarLinea(int intIdLinea)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarLinea",
                DatosPeticion = "{IdLinea: " + intIdLinea + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProveedores(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
        
            return intCantidad;
        }

        public static async Task<string> AgregarProveedor(Proveedor proveedor)
        {
            string strDatos = serializer.Serialize(proveedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarProveedor",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarProveedor(Proveedor proveedor)
        {
            string strDatos = serializer.Serialize(proveedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarProveedor",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Proveedor> ObtenerProveedor(int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProveedor",
                DatosPeticion = "{IdProveedor: " + intIdProveedor + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Proveedor proveedor = null;
            if (respuesta != "")
                proveedor = serializer.Deserialize<Proveedor>(respuesta);
            return proveedor;
        }

        public static async Task EliminarProveedor(int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarProveedor",
                DatosPeticion = "{IdProveedor: " + intIdProveedor + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaProductos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IncluyeServicios: '" + bolIncluyeServicios + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoProductos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoProductos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IncluyeServicios: '" + bolIncluyeServicios + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarProducto(Producto producto)
        {
            string strDatos = serializer.Serialize(producto);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarProducto",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarProducto(Producto producto)
        {
            string strDatos = serializer.Serialize(producto);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarProducto",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Producto> ObtenerProducto(int intIdProducto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProducto",
                DatosPeticion = "{IdProducto: " + intIdProducto + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Producto producto = null;
            if (respuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProductoPorCodigo",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Producto producto = null;
            if (respuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta);
            return producto;
        }

        public static async Task EliminarProducto(int intIdProducto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarProducto",
                DatosPeticion = "{IdProducto: " + intIdProducto + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoUsuarios",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarUsuario(Usuario usuario)
        {
            string strDatos = serializer.Serialize(usuario);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarUsuario",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarUsuarioPorEmpresa",
                DatosPeticion = "{IdUsuario: " + intIdUsuario + ", IdEmpresa: " + intIdEmpresa + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarUsuario(Usuario usuario)
        {
            string strDatos = serializer.Serialize(usuario);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarUsuario",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Usuario> ActualizarClaveUsuario(int intIdUsuario, string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarClaveUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario + ", Clave: '" + strClave + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Usuario usuario = null;
            if (respuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta);
            return usuario;
        }

        public static async Task<Usuario> ObtenerUsuario(int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Usuario usuario = null;
            if (respuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta);
            return usuario;
        }

        public static async Task EliminarUsuario(int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasEgreso(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCuentasEgreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarCuentaEgreso(CuentaEgreso cuentaEgreso)
        {
            string strDatos = serializer.Serialize(cuentaEgreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCuentaEgreso",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarCuentaEgreso(CuentaEgreso cuentaEgreso)
        {
            string strDatos = serializer.Serialize(cuentaEgreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCuentaEgreso",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<CuentaEgreso> ObtenerCuentaEgreso(int intIdCuentaEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCuentaEgreso",
                DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CuentaEgreso cuentaEgreso = null;
            if (respuesta != "")
                cuentaEgreso = serializer.Deserialize<CuentaEgreso>(respuesta);
            return cuentaEgreso;
        }

        public static async Task EliminarCuentaEgreso(int intIdCuentaEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCuentaEgreso",
                DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoCuentasBanco(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoCuentasBanco",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarCuentaBanco(CuentaBanco cuentaBanco)
        {
            string strDatos = serializer.Serialize(cuentaBanco);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCuentaBanco",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarCuentaBanco(CuentaBanco cuentaBanco)
        {
            string strDatos = serializer.Serialize(cuentaBanco);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCuentaBanco",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<CuentaBanco> ObtenerCuentaBanco(int intIdCuentaBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCuentaBanco",
                DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CuentaBanco cuentaBanco = null;
            if (respuesta != "")
                cuentaBanco = serializer.Deserialize<CuentaBanco>(respuesta);
            return cuentaBanco;
        }

        public static async Task EliminarCuentaBanco(int intIdCuentaBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCuentaBanco",
                DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoVendedores(int intIdEmpresa, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoVendedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task<string> AgregarVendedor(Vendedor vendedor)
        {
            string strDatos = serializer.Serialize(vendedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarVendedor",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarVendedor(Vendedor vendedor)
        {
            string strDatos = serializer.Serialize(vendedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarVendedor",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Vendedor> ObtenerVendedor(int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerVendedor",
                DatosPeticion = "{IdVendedor: " + intIdVendedor + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task<Vendedor> ObtenerVendedorPorDefecto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerVendedorPorDefecto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Vendedor vendedor = null;
            if (respuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta);
            return vendedor;
        }

        public static async Task EliminarVendedor(int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarVendedor",
                DatosPeticion = "{IdVendedor: " + intIdVendedor + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaEgresos(int intIdEmpresa, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaEgresos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoEgresos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoEgresos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AnularEgreso(int intIdEgreso, int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AnularEgreso",
                DatosPeticion = "{IdEgreso: " + intIdEgreso + ", IdUsuario: " + intIdUsuario + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Egreso> ObtenerEgreso(int intIdEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerEgreso",
                DatosPeticion = "{IdEgreso: " + intIdEgreso + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Egreso egreso = null;
            if (respuesta != "")
                egreso = serializer.Deserialize<Egreso>(respuesta);
            return egreso;
        }

        public static async Task<string> AgregarEgreso(Egreso egreso)
        {
            string strDatos = serializer.Serialize(egreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarEgreso",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta;
        }

        public static async Task ActualizarEgreso(Egreso egreso)
        {
            string strDatos = serializer.Serialize(egreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarEgreso",
                DatosPeticion = strDatos
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura = 0, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaFacturas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<LlaveDescripcion>> ObtenerListadoFacturas(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdFactura = 0, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoFacturas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "'}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<LlaveDescripcion>>(respuesta);
            return listado;
        }

        public static async Task AnularFactura(int intIdFactura, int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AnularFactura",
                DatosPeticion = "{IdFactura: " + intIdFactura + ", IdUsuario: " + intIdUsuario + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Factura> ObtenerFactura(int intIdFactura)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerFactura",
                DatosPeticion = "{IdFactura: " + intIdFactura + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Factura factura = null;
            if (respuesta != "")
                factura = serializer.Deserialize<Factura>(respuesta);
            return factura;
        }

        public static async Task<Factura> AgregarFactura(Factura factura)
        {
            string strDatos = serializer.Serialize(factura);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarFactura",
                DatosPeticion = strDatos
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Factura nuevaFactura = null;
            if (respuesta != "")
                nuevaFactura = serializer.Deserialize<Factura>(respuesta);
            return nuevaFactura;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronico(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronico",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronicoPorClave(string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronicoPorClave",
                DatosPeticion = "{Clave: " + strClave + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task GeneraMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "GeneraMensajeReceptor",
                DatosPeticion = "{Datos: '" + strDatos + "', IdEmpresa: " + intIdEmpresa + ", Sucursal: " + intSucursal + ", Terminal: " + intTerminal + ", Estado: " + intEstado + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalDocumentosElectronicosProcesados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta);
            return intCantidad;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoDocumentosElectronicosProcesados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task<List<DocumentoDetalle>> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListadoDocumentosElectronicosEnProceso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<DocumentoDetalle> listado = new List<DocumentoDetalle>();
            if (respuesta != "")
                listado = serializer.Deserialize<List<DocumentoDetalle>>(respuesta);
            return listado;
        }

        public static async Task ProcesarDocumentosElectronicosPendientes(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ProcesarDocumentosElectronicosPendientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task EnviarDocumentoElectronicoPendiente(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EnviarDocumentoElectronicoPendiente",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static async Task<DocumentoElectronico> ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerRespuestaDocumentoElectronicoEnviado",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
            };
            string respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta);
            return documento;
        }

        public static async Task EnviarNotificacion(int intIdDocumento, string strCorreoReceptor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EnviarNotificacionDocumentoElectronico",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + ", CorreoReceptor: '" + strCorreoReceptor + "'}"
            };
            await Ejecutar(peticion, strServicioPuntoventaURL, "");
        }

        public static void ProcesarRespuesta(RespuestaHaciendaDTO respuesta)
        {
            string jsonRequest = "{\"clave\": \"" + respuesta.Clave + "\"," +
                "\"fecha\": \"" + respuesta.Fecha + "\"," +
                "\"ind-estado\": \"" + respuesta.IndEstado + "\"," +
                "\"respuesta-xml\": \"" + respuesta.RespuestaXml + "\"}";
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(strServicioRecepcionURL + "/recibirrespuestahacienda");
            Task<HttpResponseMessage> task1 = httpClient.PostAsync(uri, stringContent);
            if (!task1.Result.IsSuccessStatusCode)
            {
                string strErrorMessage = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                throw new Exception("Error al consumir el servicio web de recepción de respuestas: " + strErrorMessage);
            }
        }

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            HttpResponseMessage httpResponse = await httpClient.GetAsync(strServicioPuntoventaURL + "/obtenerultimaversionapp");
            if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
            if (httpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            string response = serializer.Deserialize<string>(responseContent);
            return response;
        }
    }
}
