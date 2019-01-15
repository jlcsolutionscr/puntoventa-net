using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core.CustomClasses;
using LeandroSoftware.Puntoventa.CommonTypes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LeandroSoftware.AccesoDatos.ClienteWCF
{
    public static class PuntoventaWCF
    {
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioPuntoventaURL = appSettings["ServicioPuntoventaURL"].ToString();
        private static HttpClient httpClient = new HttpClient();

        private static async Task Ejecutar(string jsonObject, string servicioURL, string strToken)
        {
            try
            {
                StringContent contentJson = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutar", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<string> EjecutarConsulta(string jsonObject, string servicioURL, string strToken)
        {
            try
            {
                StringContent contentJson = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                if (strToken != "")
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", strToken);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(servicioURL + "/ejecutarconsulta", contentJson);
                if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Empresa> ObtenerEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Empresa empresa = null;
            if (strRespuesta != "")
                empresa = serializer.Deserialize<Empresa>(strRespuesta);
            return empresa;
        }

        public static async Task<Usuario> ValidarCredenciales(string strIdentificacion, string strUsuario, string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ValidarCredenciales",
                DatosPeticion = "{Identificacion: '" + strIdentificacion + "', Usuario: '" + strUsuario + "', Clave: '" + strClave + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Usuario usuario = null;
            if (strRespuesta != "")
                usuario = serializer.Deserialize<Usuario>(strRespuesta);
            return usuario;
        }

        public static async Task<decimal> ObtenerTipoCambioDolar()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTipoCambioDolar",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            decimal decTipoCambioDolar = 0;
            if (strRespuesta != "")
                decTipoCambioDolar = serializer.Deserialize<decimal>(strRespuesta);
            return decTipoCambioDolar;
        }

        public static async Task<List<TipoIdentificacion>> ObtenerListaTipoIdentificacion()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoIdentificacion",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<TipoIdentificacion> listado = new List<TipoIdentificacion>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<TipoIdentificacion>>(strRespuesta);
            return listado;
        }

        public static async Task<List<Modulo>> ObtenerlistaModulos()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaModulos",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Modulo> listado = new List<Modulo>();
            if (strRespuesta != "")
                listado = serializer.Deserialize<List<Modulo>>(strRespuesta);
            return listado;
        }

        public static async Task<List<CatalogoReporte>> ObtenerListaReportes()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaReportes",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<CatalogoReporte> listado = new List<CatalogoReporte>();
            if (strRespuesta != "")
                listado = serializer.Deserialize<List<CatalogoReporte>>(strRespuesta);
            return listado;
        }

        public static async Task<List<Provincia>> ObtenerListaProvincias()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProvincias",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<Provincia> listado = new List<Provincia>();
            if (strRespuesta != "")
                listado = serializer.Deserialize<List<Provincia>>(strRespuesta);;;
            return listado;
        }

        public static async Task<List<Canton>> ObtenerListaCantones(int intIdProvincia)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + "}"
            };
        string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<Canton> listado = new List<Canton>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Canton>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<Distrito>> ObtenerListaDistritos(int intIdProvincia, int intIdCanton)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + ", IdCanton: " + intIdCanton.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Distrito> listado = new List<Distrito>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Distrito>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<Barrio>> ObtenerListaBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia.ToString() + ", IdCanton: " + intIdCanton.ToString() + ", IdDistrito: " + intIdDistrito.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<Barrio> listado = new List<Barrio>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Barrio>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<TipoProducto>> ObtenerListaTipoProducto()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoProducto",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<TipoProducto> listado = new List<TipoProducto>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<TipoProducto>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<TipoUnidad>> ObtenerListaTipoUnidad()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoUnidad",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<TipoUnidad> listado = new List<TipoUnidad>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<TipoUnidad>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<FormaPago>> ObtenerListaFormaPagoEgreso()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFormaPagoEgreso",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<FormaPago> listado = new List<FormaPago>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<FormaPago>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<FormaPago>> ObtenerListaFormaPagoFactura()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFormaPagoFactura",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<FormaPago> listado = new List<FormaPago>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<FormaPago>>(strRespuesta);;
            return listado;
        }

        public static async Task<List<Role>> ObtenerListaRoles()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaRoles",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Role> listado = new List<Role>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Role>>(strRespuesta);
            return listado;
        }

        public static async Task<List<TipodePrecio>> ObtenerListaTipodePrecio()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipodePrecio",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<TipodePrecio> listado = new List<TipodePrecio>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<TipodePrecio>>(strRespuesta);
            return listado;
        }

        public static async Task<List<TipoMoneda>> ObtenerListaTipoMoneda()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoMoneda",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<TipoMoneda> listado = new List<TipoMoneda>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<TipoMoneda>>(strRespuesta);
            return listado;
        }

        public static async Task<List<CondicionVenta>> ObtenerListaCondicionVenta()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCondicionVenta",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<CondicionVenta> listado = new List<CondicionVenta>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<CondicionVenta>>(strRespuesta);
            return listado;
        }

        public static async Task<List<BancoAdquiriente>> ObtenerListaBancoAdquiriente(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBancoAdquiriente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<BancoAdquiriente> listado = new List<BancoAdquiriente>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<BancoAdquiriente>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            string strDatos = serializer.Serialize(bancoAdquiriente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarBancoAdquiriente",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<BancoAdquiriente> ObtenerBancoAdquiriente(int intIdBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerBancoAdquiriente",
                DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            BancoAdquiriente bancoAdquiriente = null;
            if (strRespuesta != "")
                bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(strRespuesta);
            return bancoAdquiriente;
        }

        public static async Task EliminarBancoAdquiriente(int intIdBanco)
    {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarBancoAdquiriente",
                DatosPeticion = "{IdBancoAdquiriente: " + intIdBanco.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<Cliente>> ObtenerListaClientes(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Cliente> listado = new List<Cliente>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Cliente>>(strRespuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaClientes(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaClientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarCliente(Cliente cliente)
        {
            string strDatos = serializer.Serialize(cliente);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCliente",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Cliente> ObtenerCliente(int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCliente",
                DatosPeticion = "{IdCliente: " + intIdCliente.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Cliente cliente = null;
            if (strRespuesta != "")
                cliente = serializer.Deserialize<Cliente>(strRespuesta);
            return cliente;
        }

        public static async Task EliminarCliente(int intIdCliente)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCliente",
                DatosPeticion = "{IdCliente: " + intIdCliente.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Cliente> ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ValidaIdentificacionCliente",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Identificacion: '" + strIdentificacion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Cliente cliente = null;
            if (strRespuesta != "")
                cliente = serializer.Deserialize<Cliente>(strRespuesta);
            return cliente;
        }


        public static async Task<List<Linea>> ObtenerListaLineas(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<Linea> listado = new List<Linea>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Linea>>(strRespuesta);
            return listado;
        }

        public static async Task<List<Linea>> ObtenerListaLineasDeProducto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineasDeProducto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Linea> listado = new List<Linea>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Linea>>(strRespuesta);
            return listado;
        }

        public static async Task<List<Empresa>> ObtenerListaEmpresas()
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEmpresas",
                DatosPeticion = ""
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Empresa> listado = new List<Empresa>();
            if (strRespuesta != "")
                listado = serializer.Deserialize<List<Empresa>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarEmpresa(Empresa empresa)
        {
            string strDatos = serializer.Serialize(empresa);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarEmpresa",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarLogoEmpresa(int intIdEmpresa, string strLogotipo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarLogoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Logotipo: '" + strLogotipo + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task RemoverLogoEmpresa(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "RemoverLogoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCertificadoEmpresa",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Certificado: '" + strCertificado + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<Linea>> ObtenerListaLineasDeServicio(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaLineasDeServicio",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Linea> listado = new List<Linea>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Linea>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarLinea(Linea linea)
        {
            string strDatos = serializer.Serialize(linea);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarLinea",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Linea> ObtenerLinea(int intIdLinea)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerLinea",
                DatosPeticion = "{IdLinea: " + intIdLinea.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Linea linea = null;
            if (strRespuesta != "")
                linea = serializer.Deserialize<Linea>(strRespuesta);
            return linea;
        }

        public static async Task EliminarLinea(int intIdLinea)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarLinea",
                DatosPeticion = "{IdLinea: " + intIdLinea.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<Proveedor>> ObtenerListaProveedores(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Proveedor> listado = new List<Proveedor>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Proveedor>>(strRespuesta);
            return listado;
        }

        public static async Task<int> ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaProveedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
        
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarProveedor(Proveedor proveedor)
        {
            string strDatos = serializer.Serialize(proveedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarProveedor",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Proveedor> ObtenerProveedor(int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProveedor",
                DatosPeticion = "{IdProveedor: " + intIdProveedor.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Proveedor proveedor = null;
            if (strRespuesta != "")
                proveedor = serializer.Deserialize<Proveedor>(strRespuesta);
            return proveedor;
        }

        public static async Task EliminarProveedor(int intIdProveedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarProveedor",
                DatosPeticion = "{IdProveedor: " + intIdProveedor.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaProductos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IncluyeServicios: '" + bolIncluyeServicios.ToString() + "', IdLinea: " + intIdLinea.ToString() + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
            return intCantidad;
        }

        public static async Task<List<Producto>> ObtenerListaProductos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProductos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IncluyeServicios: '" + bolIncluyeServicios.ToString() + "', IdLinea: " + intIdLinea.ToString() + ", Codigo: '" + strCodigo + "', Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Producto> listado = new List<Producto>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Producto>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarProducto(Producto producto)
        {
            string strDatos = serializer.Serialize(producto);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarProducto",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Producto> ObtenerProducto(int intIdProducto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProducto",
                DatosPeticion = "{IdProducto: " + intIdProducto.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Producto producto = null;
            if (strRespuesta != "")
                producto = serializer.Deserialize<Producto>(strRespuesta);
            return producto;
        }

        public static async Task<Producto> ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerProductoPorCodigo",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Codigo: '" + strCodigo + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Producto producto = null;
            if (strRespuesta != "")
                producto = serializer.Deserialize<Producto>(strRespuesta);
            return producto;
        }

        public static async Task EliminarProducto(int intIdProducto)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarProducto",
                DatosPeticion = "{IdProducto: " + intIdProducto.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<Usuario>> ObtenerListaUsuarios(int intIdEmpresa, string strCodigo)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaUsuarios",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Codigo: '" + strCodigo + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Usuario> listado = new List<Usuario>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Usuario>>(strRespuesta);;
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarUsuarioPorEmpresa",
                DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + ", IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task ActualizarUsuario(Usuario usuario)
        {
            string strDatos = serializer.Serialize(usuario);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarUsuario",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Usuario> ActualizarClaveUsuario(int intIdUsuario, string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarClaveUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + ", Clave: '" + strClave + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Usuario usuario = null;
            if (strRespuesta != "")
                usuario = serializer.Deserialize<Usuario>(strRespuesta);
            return usuario;
        }

        public static async Task<Usuario> ObtenerUsuario(int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Usuario usuario = null;
            if (strRespuesta != "")
                usuario = serializer.Deserialize<Usuario>(strRespuesta);
            return usuario;
        }

        public static async Task EliminarUsuario(int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarUsuario",
                DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<CuentaEgreso>> ObtenerListaCuentasEgreso(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCuentasEgreso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             List<CuentaEgreso> listado = new List<CuentaEgreso>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<CuentaEgreso>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarCuentaEgreso(CuentaEgreso cuentaEgreso)
        {
            string strDatos = serializer.Serialize(cuentaEgreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCuentaEgreso",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<CuentaEgreso> ObtenerCuentaEgreso(int intIdCuentaEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCuentaEgreso",
                DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            CuentaEgreso cuentaEgreso = null;
            if (strRespuesta != "")
                cuentaEgreso = serializer.Deserialize<CuentaEgreso>(strRespuesta);
            return cuentaEgreso;
        }

        public static async Task EliminarCuentaEgreso(int intIdCuentaEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCuentaEgreso",
                DatosPeticion = "{IdCuentaEgreso: " + intIdCuentaEgreso.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<CuentaBanco>> ObtenerListaCuentasBanco(int intIdEmpresa, string strDescripcion = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCuentasBanco",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Descripcion: '" + strDescripcion + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<CuentaBanco> listado = new List<CuentaBanco>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<CuentaBanco>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarCuentaBanco(CuentaBanco cuentaBanco)
        {
            string strDatos = serializer.Serialize(cuentaBanco);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarCuentaBanco",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<CuentaBanco> ObtenerCuentaBanco(int intIdCuentaBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerCuentaBanco",
                DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            CuentaBanco cuentaBanco = null;
            if (strRespuesta != "")
                cuentaBanco = serializer.Deserialize<CuentaBanco>(strRespuesta);
            return cuentaBanco;
        }

        public static async Task EliminarCuentaBanco(int intIdCuentaBanco)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarCuentaBanco",
                DatosPeticion = "{IdCuentaBanco: " + intIdCuentaBanco.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<List<Vendedor>> ObtenerListaVendedores(int intIdEmpresa, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaVendedores",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Vendedor> listado = new List<Vendedor>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Vendedor>>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task ActualizarVendedor(Vendedor vendedor)
        {
            string strDatos = serializer.Serialize(vendedor);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarVendedor",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Vendedor> ObtenerVendedor(int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerVendedor",
                DatosPeticion = "{IdVendedor: " + intIdVendedor.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Vendedor vendedor = null;
            if (strRespuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(strRespuesta);
            return vendedor;
        }

        public static async Task<Vendedor> ObtenerVendedorPorDefecto(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerVendedorPorDefecto",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Vendedor vendedor = null;
            if (strRespuesta != "")
                vendedor = serializer.Deserialize<Vendedor>(strRespuesta);
            return vendedor;
        }

        public static async Task EliminarVendedor(int intIdVendedor)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EliminarVendedor",
                DatosPeticion = "{IdVendedor: " + intIdVendedor.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaEgresos(int intIdEmpresa, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaEgresos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IdEgreso: " + intIdEgreso.ToString() + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
            return intCantidad;
        }

        public static async Task<List<Egreso>> ObtenerListaEgresos(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdEgreso = 0, string strBeneficiario = "", string strDetalle = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaEgresos",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IdEgreso: " + intIdEgreso.ToString() + ", Beneficiario: '" + strBeneficiario + "', Detalle: '" + strDetalle + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Egreso> listado = new List<Egreso>();
             if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Egreso>>(strRespuesta);
            return listado;
        }

        public static async Task AnularEgreso(int intIdEgreso, int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AnularEgreso",
                DatosPeticion = "{IdEgreso: " + intIdEgreso.ToString() + ", IdUsuario: " + intIdUsuario.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Egreso> ObtenerEgreso(int intIdEgreso)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerEgreso",
                DatosPeticion = "{IdEgreso: " + intIdEgreso.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Egreso egreso = null;
            if (strRespuesta != "")
                egreso = serializer.Deserialize<Egreso>(strRespuesta);
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
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             return strRespuesta;
        }

        public static async Task ActualizarEgreso(Egreso egreso)
        {
            string strDatos = serializer.Serialize(egreso);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarEgreso",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura = 0, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalListaFacturas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", IdFactura: " + intIdFactura.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
            return intCantidad;
        }

        public static async Task<List<Factura>> ObtenerListaFacturas(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina, int intIdFactura = 0, string strNombre = "")
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaFacturas",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + ", IdFactura: " + intIdFactura.ToString() + ", Nombre: '" + strNombre + "'}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<Factura> listado = new List<Factura>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<Factura>>(strRespuesta);
            return listado;
        }

        public static async Task AnularFactura(int intIdFactura, int intIdUsuario)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AnularFactura",
                DatosPeticion = "{IdFactura: " + intIdFactura.ToString() + ", IdUsuario: " + intIdUsuario.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<Factura> ObtenerFactura(int intIdFactura)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerFactura",
                DatosPeticion = "{IdFactura: " + intIdFactura.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            Factura factura = null;
            if (strRespuesta != "")
                factura = serializer.Deserialize<Factura>(strRespuesta);
            return factura;
        }

        public static async Task<string> AgregarFactura(Factura factura)
        {
            string strDatos = serializer.Serialize(factura);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarFactura",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
             return strRespuesta;
        }

        public static async Task ActualizarFactura(Factura factura)
        {
            string strDatos = serializer.Serialize(factura);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ActualizarFactura",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronico(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronico",
                DatosPeticion = "{IdDocumento: " + intIdDocumento.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            DocumentoElectronico documento = null;
            if (strRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(strRespuesta);
            return documento;
        }

        public static async Task<DocumentoElectronico> ObtenerDocumentoElectronicoPorClave(string strClave)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerDocumentoElectronicoPorClave",
                DatosPeticion = "{Clave: " + strClave + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            DocumentoElectronico documento = null;
            if (strRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(strRespuesta);
            return documento;
        }

        public static async Task GeneraMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "GeneraMensajeReceptor",
                DatosPeticion = "{Datos: '" + strDatos + "', IdEmpresa: " + intIdEmpresa.ToString() + ", Sucursal: " + intSucursal.ToString() + ", Terminal: " + intTerminal.ToString() + ", Estado: " + intEstado.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<int> ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerTotalDocumentosElectronicosProcesados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            int intCantidad = 0;
            if (strRespuesta != "")
                intCantidad = serializer.Deserialize<int>(strRespuesta);
            return intCantidad;
        }

        public static async Task<List<DocumentoElectronico>> ObtenerListaDocumentosElectronicosProcesados(int intIdEmpresa, int intNumeroPagina, int intFilasPorPagina)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDocumentosElectronicosProcesados",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + ", NumeroPagina: " + intNumeroPagina.ToString() + ",FilasPorPagina: " + intFilasPorPagina.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<DocumentoElectronico>>(strRespuesta);
            return listado;
        }

        public static async Task<List<DocumentoElectronico>> ObtenerListaDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDocumentosElectronicosEnProceso",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            if (strRespuesta!= "")
                listado = serializer.Deserialize<List<DocumentoElectronico>>(strRespuesta);
            return listado;
        }

        public static async Task ProcesarDocumentosElectronicosPendientes(int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ProcesarDocumentosElectronicosPendientes",
                DatosPeticion = "{IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task EnviarDocumentoElectronicoPendiente(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EnviarDocumentoElectronicoPendiente",
                DatosPeticion = "{IdDocumento: " + intIdDocumento.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static async Task<DocumentoElectronico> ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "ObtenerRespuestaDocumentoElectronicoEnviado",
                DatosPeticion = "{IdDocumento: " + intIdDocumento.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioPuntoventaURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            DocumentoElectronico documento = null;
            if (strRespuesta != "")
                documento = serializer.Deserialize<DocumentoElectronico>(strRespuesta);
            return documento;
        }

        public static async Task EnviarNotificacion(int intIdDocumento)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "EnviarNotificacionDocumentoElectronico",
                DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            await Ejecutar(strPeticion, strServicioPuntoventaURL, "");
        }

        public static void ProcesarRespuesta(RespuestaHaciendaDTO respuesta)
        {
            string jsonRequest = "{\"clave\": \"" + respuesta.Clave + "\"," +
                "\"fecha\": \"" + respuesta.Fecha + "\"," +
                "\"ind-estado\": \"" + respuesta.IndEstado + "\"," +
                "\"respuesta-xml\": \"" + respuesta.RespuestaXml + "\"}";

            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(strServicioPuntoventaURL + "/recibirrespuestahacienda");
            Task<HttpResponseMessage> task1 = httpClient.PostAsync(uri, stringContent);
            if (!task1.Result.IsSuccessStatusCode)
            {
                string strErrorMessage = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                throw new Exception("Error al consumir el servicio web de factura electrónica: " + strErrorMessage);
            }
        }
    }
}
