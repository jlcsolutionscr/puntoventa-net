using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LeandroSoftware.Migracion.ClienteWeb
{
    public static class MigracionClient
    {
        private static readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static JavaScriptSerializer serializer = new CustomJavascriptSerializer();
        private static string strServicioMigracionURL = appSettings["ServicioMigracionURL"].ToString();
        private static HttpClient httpClient = new HttpClient();

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

        public static async Task<string> AgregarBancoAdquiriente(BancoAdquiriente datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarBancoAdquiriente",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarCliente(Cliente datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCliente",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarLinea(Linea datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarLinea",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarProveedor(Proveedor datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarProveedor",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarProducto(Producto datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarProducto",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarUsuario(Usuario datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarUsuario",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa)
        {
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarUsuarioPorEmpresa",
                DatosPeticion = "{IdUsuario: " + intIdUsuario.ToString() + ", IdEmpresa: " + intIdEmpresa.ToString() + "}"
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarCuentaEgreso(CuentaEgreso datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCuentaEgreso",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarCuentaBanco(CuentaBanco datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarCuentaBanco",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarVendedor(Vendedor datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarVendedor",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarEgreso(Egreso datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarEgreso",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarFactura(Factura datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarFactura",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }

        public static async Task<string> AgregarDocumentoElectronico(DocumentoElectronico datos)
        {
            string strDatos = serializer.Serialize(datos);
            RequestDTO peticion = new RequestDTO
            {
                NombreMetodo = "AgregarDocumentoElectronico",
                DatosPeticion = strDatos
            };
            string strPeticion = serializer.Serialize(peticion);
            string strRespuesta = await EjecutarConsulta(strPeticion, strServicioMigracionURL, "");
            strRespuesta = serializer.Deserialize<string>(strRespuesta);
            return strRespuesta;
        }
    }
}
