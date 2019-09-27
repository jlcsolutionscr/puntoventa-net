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
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<ResponseDTO> EjecutarConsulta(RequestDTO peticion, string servicioURL, string strToken)
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
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<IdentificacionNombre>> ObtenerListaEmpresasPorIdentificacion(string strListaIdentificacion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEmpresasPorIdentificacion",
                DatosPeticion = "{ListaIdentificacion: '" + strListaIdentificacion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<IdentificacionNombre> listado = new List<IdentificacionNombre>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<IdentificacionNombre>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<string> ObtenerUltimaVersionApp()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerUltimaVersionApp",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            string strUltimaVersionApp = "";
            if (respuesta.DatosRespuesta != "")
                strUltimaVersionApp = serializer.Deserialize<string>(respuesta.DatosRespuesta);
            return strUltimaVersionApp;
        }

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Empresa empresa = null;
            if (respuesta.DatosRespuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta.DatosRespuesta);
            return empresa;
        }

        public static async Task<TerminalPorEmpresa> ObtenerTerminalPorEmpresa(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTerminalPorEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdSucursal: " + intIdSucursal + ", IdTerminal: " + intIdTerminal + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            TerminalPorEmpresa terminal = null;
            if (respuesta.DatosRespuesta != "")
                terminal = serializer.Deserialize<TerminalPorEmpresa>(respuesta.DatosRespuesta);
            return terminal;
        }

        public static async Task<Empresa> ValidarCredenciales(string strIdentificacion, string strValorRegistro, string strUsuario, string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ValidarCredenciales",
                DatosPeticion = "{Identificacion: '" + strIdentificacion + "', ValorRegistro: '" + strValorRegistro + "', Usuario: '" + strUsuario + "', Clave: '" + strClave + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Empresa empresa = null;
            if (respuesta.DatosRespuesta != "")
                empresa = serializer.Deserialize<Empresa>(respuesta.DatosRespuesta);
            return empresa;
        }

        public static async Task<decimal> ObtenerTipoCambioDolar()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTipoCambioDolar",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            decimal decTipoCambioDolar = 0;
            if (respuesta.DatosRespuesta != "")
                decTipoCambioDolar = serializer.Deserialize<decimal>(respuesta.DatosRespuesta);
            return decTipoCambioDolar;
        }

        public static async Task<List<TipoIdentificacion>> ObtenerListaTipoIdentificacion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoIdentificacion",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TipoIdentificacion> listado = new List<TipoIdentificacion>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<TipoIdentificacion>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Modulo>> ObtenerlistaModulos()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaModulos",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Modulo> listado = new List<Modulo>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Modulo>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<CatalogoReporte>> ObtenerListaReportes()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaReportes",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CatalogoReporte> listado = new List<CatalogoReporte>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CatalogoReporte>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Provincia>> ObtenerListaProvincias()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProvincias",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Provincia> listado = new List<Provincia>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Provincia>>(respuesta.DatosRespuesta);;
            return listado;
        }

        public static async Task<List<Canton>> ObtenerListaCantones(int intIdProvincia)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Canton> listado = new List<Canton>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Canton>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Distrito>> ObtenerListaDistritos(int intIdProvincia, int intIdCanton)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Distrito> listado = new List<Distrito>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Distrito>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Barrio>> ObtenerListaBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Barrio> listado = new List<Barrio>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Barrio>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<TipoProducto>> ObtenerListaTipoProducto()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoProducto",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TipoProducto> listado = new List<TipoProducto>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<TipoProducto>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ParametroExoneracion>> ObtenerListaTipoExoneracion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoExoneracion",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ParametroExoneracion> listado = new List<ParametroExoneracion>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ParametroExoneracion>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ParametroImpuesto>> ObtenerListaTipoImpuesto()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoImpuesto",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ParametroImpuesto> listado = new List<ParametroImpuesto>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ParametroImpuesto>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<TipoUnidad>> ObtenerListaTipoUnidad()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoUnidad",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TipoUnidad> listado = new List<TipoUnidad>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<TipoUnidad>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<FormaPago>> ObtenerListaFormaPagoEgreso()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFormaPagoEgreso",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<FormaPago> listado = new List<FormaPago>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<FormaPago>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<FormaPago>> ObtenerListaFormaPagoFactura()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFormaPagoFactura",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<FormaPago> listado = new List<FormaPago>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<FormaPago>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Role>> ObtenerListaRoles()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaRoles",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Role> listado = new List<Role>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Role>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<TipodePrecio>> ObtenerListaTipodePrecio()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipodePrecio",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TipodePrecio> listado = new List<TipodePrecio>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<TipodePrecio>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<TipoMoneda>> ObtenerListaTipoMoneda()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoMoneda",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<TipoMoneda> listado = new List<TipoMoneda>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<TipoMoneda>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<CondicionVenta>> ObtenerListaCondicionVenta()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCondicionVenta",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CondicionVenta> listado = new List<CondicionVenta>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CondicionVenta>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<CondicionVentaYFormaPago>> ObtenerListaCondicionVentaYFormaPagoFactura()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCondicionVentaYFormaPagoFactura",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CondicionVentaYFormaPago> listado = new List<CondicionVentaYFormaPago>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CondicionVentaYFormaPago>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<CondicionVentaYFormaPago>> ObtenerListaCondicionVentaYFormaPagoCompra()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCondicionVentaYFormaPagoCompra",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CondicionVentaYFormaPago> listado = new List<CondicionVentaYFormaPago>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CondicionVentaYFormaPago>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteVentas>> ObtenerReporteVentasPorCliente(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente, bool bolNulo, int intIdTipoPago, int intIdBancoAdquiriente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorCliente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + ", isNulo: '" + bolNulo + "', IdTipoPago: " + intIdTipoPago + ", IdBancoAdquiriente: " + intIdBancoAdquiriente + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentas> listado = new List<ReporteVentas>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteVentas>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorVendedor>> ObtenerReporteVentasPorVendedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorVendedor",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdVendedor: " + intIdVendedor + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorVendedor> listado = new List<ReporteVentasPorVendedor>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorVendedor>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteCompras>> ObtenerReporteComprasPorProveedor(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor, bool bolNulo, int intFormaPago)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteComprasPorProveedor",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + ", FormaPago: " + intFormaPago + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCompras> listado = new List<ReporteCompras>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteCompras>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorCobrar>> ObtenerReporteCuentasPorCobrarClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteCuentasPorCobrarClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCuentasPorCobrar> listado = new List<ReporteCuentasPorCobrar>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorCobrar>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteCuentasPorPagar>> ObtenerReporteCuentasPorPagarProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteCuentasPorPagarProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCuentasPorPagar> listado = new List<ReporteCuentasPorPagar>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteCuentasPorPagar>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxC>> ObtenerReporteMovimientosCxCClientes(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosCxCClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdCliente: " + intIdCliente + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosCxC> listado = new List<ReporteMovimientosCxC>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxC>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosCxP>> ObtenerReporteMovimientosCxPProveedores(int intIdEmpresa, string strFechaInicial, string strFechaFinal, int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosCxPProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "', IdProveedor: " + intIdProveedor + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosCxP> listado = new List<ReporteMovimientosCxP>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosCxP>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteMovimientosBanco>> ObtenerReporteMovimientosBanco(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteMovimientosBanco",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteMovimientosBanco> listado = new List<ReporteMovimientosBanco>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteMovimientosBanco>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteEstadoResultados(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteEstadoResultados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteEstadoResultados>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleEgreso>> ObtenerReporteDetalleEgreso(int intIdEmpresa, int intIdCuentaEgreso, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteDetalleEgreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdCuentaEgreso: " + intIdCuentaEgreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDetalleEgreso> listado = new List<ReporteDetalleEgreso>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleEgreso>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDetalleIngreso>> ObtenerReporteDetalleIngreso(int intIdEmpresa, int intIdCuentaIngreso, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteDetalleIngreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdCuentaIngreso: " + intIdCuentaIngreso + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDetalleIngreso> listado = new List<ReporteDetalleIngreso>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDetalleIngreso>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaResumen>> ObtenerReporteVentasPorLineaResumen(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorLineaResumen",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorLineaResumen> listado = new List<ReporteVentasPorLineaResumen>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaResumen>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteVentasPorLineaDetalle>> ObtenerReporteVentasPorLineaDetalle(int intIdEmpresa, int intIdLinea, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteVentasPorLineaDetalle",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", IdLinea: " + intIdLinea + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteVentasPorLineaDetalle> listado = new List<ReporteVentasPorLineaDetalle>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteVentasPorLineaDetalle>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteFacturasElectronicasEmitidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasEmitidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteNotasCreditoElectronicasEmitidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteFacturasElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteFacturasElectronicasRecibidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteDocumentoElectronico>> ObtenerReporteNotasCreditoElectronicasRecibidas(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteNotasCreditoElectronicasRecibidas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteDocumentoElectronico> listado = new List<ReporteDocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteDocumentoElectronico>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ReporteEstadoResultados>> ObtenerReporteResumenDocumentosElectronicos(int intIdEmpresa, string strFechaInicial, string strFechaFinal)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerReporteResumenDocumentosElectronicos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaInicial: '" + strFechaInicial + "', FechaFinal: '" + strFechaFinal + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteEstadoResultados> listado = new List<ReporteEstadoResultados>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteEstadoResultados>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<CierreCaja> GenerarDatosCierreCaja(int intIdEmpresa, string strFechaCierre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "GenerarDatosCierreCaja",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", FechaCierre: '" + strFechaCierre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CierreCaja cierre = null;
            if (respuesta.DatosRespuesta != "")
                cierre = serializer.Deserialize<CierreCaja>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CierreCaja nuevoCierre = null;
            if (respuesta.DatosRespuesta != "")
                nuevoCierre = serializer.Deserialize<CierreCaja>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ReporteCierreDeCaja> listado = new List<ReporteCierreDeCaja>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ReporteCierreDeCaja>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<ParametroImpuesto> ObtenerParametroImpuesto(int intIdImpuesto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerParametroImpuesto",
                DatosPeticion = "{IdImpuesto: " + intIdImpuesto + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            ParametroImpuesto parametroImpuesto = null;
            if (respuesta.DatosRespuesta != "")
                parametroImpuesto = serializer.Deserialize<ParametroImpuesto>(respuesta.DatosRespuesta);
            return parametroImpuesto;
        }

        public static async Task<List<BancoAdquiriente>> ObtenerListaBancoAdquiriente(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBancoAdquiriente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<BancoAdquiriente> listado = new List<BancoAdquiriente>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<BancoAdquiriente>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            BancoAdquiriente bancoAdquiriente = null;
            if (respuesta.DatosRespuesta != "")
                bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(respuesta.DatosRespuesta);
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

        public static async Task<List<Cliente>> ObtenerListaClientes(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Cliente> listado = new List<Cliente>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Cliente>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaClientes(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Cliente cliente = null;
            if (respuesta.DatosRespuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Cliente cliente = null;
            if (respuesta.DatosRespuesta != "")
                cliente = serializer.Deserialize<Cliente>(respuesta.DatosRespuesta);
            return cliente;
        }


        public static async Task<List<Linea>> ObtenerListaLineas(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Linea> listado = new List<Linea>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Linea>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<Linea>> ObtenerListaLineasDeProducto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineasDeProducto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Linea> listado = new List<Linea>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Linea>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<ListaEmpresa>> ObtenerListaEmpresas()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEmpresas",
                DatosPeticion = ""
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<ListaEmpresa> listado = new List<ListaEmpresa>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<ListaEmpresa>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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

        public static async Task ActualizarTerminalPorEmpresa(TerminalPorEmpresa terminal)
        {
            string strDatos = serializer.Serialize(terminal);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarTerminalPorEmpresa",
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

        public static async Task<List<Linea>> ObtenerListaLineasDeServicio(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineasDeServicio",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Linea> listado = new List<Linea>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Linea>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Linea linea = null;
            if (respuesta.DatosRespuesta != "")
                linea = serializer.Deserialize<Linea>(respuesta.DatosRespuesta);
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

        public static async Task<List<Proveedor>> ObtenerListaProveedores(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Proveedor> listado = new List<Proveedor>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Proveedor>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
        
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Proveedor proveedor = null;
            if (respuesta.DatosRespuesta != "")
                proveedor = serializer.Deserialize<Proveedor>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
            return intCantidad;
        }

        public static async Task<List<Producto>> ObtenerListaProductos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProductos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IncluyeServicios: '" + bolIncluyeServicios + "', IdLinea: " + intIdLinea + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Producto> listado = new List<Producto>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Producto>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Producto producto = null;
            if (respuesta.DatosRespuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta.DatosRespuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProductoPorCodigo",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Producto producto = null;
            if (respuesta.DatosRespuesta != "")
                producto = serializer.Deserialize<Producto>(respuesta.DatosRespuesta);
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

        public static async Task<List<Usuario>> ObtenerListaUsuarios(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaUsuarios",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Codigo: '" + strCodigo + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Usuario> listado = new List<Usuario>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Usuario>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Usuario usuario = null;
            if (respuesta.DatosRespuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta.DatosRespuesta);
            return usuario;
        }

        public static async Task<Usuario> ObtenerUsuario(int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Usuario usuario = null;
            if (respuesta.DatosRespuesta != "")
                usuario = serializer.Deserialize<Usuario>(respuesta.DatosRespuesta);
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

        public static async Task<List<CuentaEgreso>> ObtenerListaCuentasEgreso(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCuentasEgreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CuentaEgreso> listado = new List<CuentaEgreso>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CuentaEgreso>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CuentaEgreso cuentaEgreso = null;
            if (respuesta.DatosRespuesta != "")
                cuentaEgreso = serializer.Deserialize<CuentaEgreso>(respuesta.DatosRespuesta);
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

        public static async Task<List<CuentaBanco>> ObtenerListaCuentasBanco(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCuentasBanco",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Descripcion: '" + strDescripcion + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<CuentaBanco> listado = new List<CuentaBanco>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<CuentaBanco>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            CuentaBanco cuentaBanco = null;
            if (respuesta.DatosRespuesta != "")
                cuentaBanco = serializer.Deserialize<CuentaBanco>(respuesta.DatosRespuesta);
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

        public static async Task<List<Vendedor>> ObtenerListaVendedores(int intIdEmpresa, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaVendedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Vendedor> listado = new List<Vendedor>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Vendedor>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Vendedor vendedor = null;
            if (respuesta.DatosRespuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta.DatosRespuesta);
            return vendedor;
        }

        public static async Task<Vendedor> ObtenerVendedorPorDefecto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerVendedorPorDefecto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Vendedor vendedor = null;
            if (respuesta.DatosRespuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
            return intCantidad;
        }

        public static async Task<List<Egreso>> ObtenerListaEgresos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEgresos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdEgreso: " + intIdEgreso + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Egreso> listado = new List<Egreso>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Egreso>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Egreso egreso = null;
            if (respuesta.DatosRespuesta != "")
                egreso = serializer.Deserialize<Egreso>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            return respuesta.DatosRespuesta;
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
            return intCantidad;
        }

        public static async Task<List<Factura>> ObtenerListaFacturas(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdFactura = 0, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFacturas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + ", IdFactura: " + intIdFactura + ", Nombre: '" + strNombre + "'}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<Factura> listado = new List<Factura>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<Factura>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Factura factura = null;
            if (respuesta.DatosRespuesta != "")
                factura = serializer.Deserialize<Factura>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            Factura nuevaFactura = null;
            if (respuesta.DatosRespuesta != "")
                nuevaFactura = serializer.Deserialize<Factura>(respuesta.DatosRespuesta);
            return nuevaFactura;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronico(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronico",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta.DatosRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta.DatosRespuesta);
            return documento;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronicoPorClave(string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronicoPorClave",
                DatosPeticion = "{Clave: " + strClave + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta.DatosRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            int intCantidad = 0;
            if (respuesta.DatosRespuesta != "")
                intCantidad = serializer.Deserialize<int>(respuesta.DatosRespuesta);
            return intCantidad;
        }

        public static async Task<List<DocumentoElectronico>> ObtenerListaDocumentosElectronicosProcesados(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDocumentosElectronicosProcesados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + ", NumeroPagina: " + intNumeroPagina + ",FilasPorPagina: " + intFilasPorPagina + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<DocumentoElectronico>>(respuesta.DatosRespuesta);
            return listado;
        }

        public static async Task<List<DocumentoElectronico>> ObtenerListaDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDocumentosElectronicosEnProceso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
            };
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            if (respuesta.DatosRespuesta != "")
                listado = serializer.Deserialize<List<DocumentoElectronico>>(respuesta.DatosRespuesta);
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
            ResponseDTO respuesta = await EjecutarConsulta(peticion, strServicioPuntoventaURL, "");
            DocumentoElectronico documento = null;
            if (respuesta.DatosRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(respuesta.DatosRespuesta);
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
    }
}
