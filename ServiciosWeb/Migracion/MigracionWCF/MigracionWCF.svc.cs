using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using log4net;
using System;
using System.Net;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using LeandroSoftware.Core.CustomClasses;
using Newtonsoft.Json.Linq;

namespace LeandroSoftware.Migracion.ServicioWCF
{
    public class MigracionWCF : IMigracionWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IMigracionService servicioMigracion;
        IUnityContainer unityContainer;
        private static System.Collections.Specialized.NameValueCollection appSettings = WebConfigurationManager.AppSettings;
        private readonly DatosConfiguracion configuracion = new DatosConfiguracion
        (
            appSettings["strConsultaIEURL"].ToString(),
            appSettings["strSoapOperation"].ToString(),
            appSettings["strServicioComprobantesURL"].ToString(),
            appSettings["strClientId"].ToString(),
            appSettings["strServicioTokenURL"].ToString(),
            appSettings["strComprobantesCallbackURL"].ToString(),
            appSettings["strCorreoNotificacionErrores"].ToString()
        );
        private readonly string strKey = appSettings["ApplicationKey"].ToString();

        public MigracionWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<IMigracionService, MigracionService>();
            servicioMigracion = unityContainer.Resolve<IMigracionService>();
        }

        public string EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                BancoAdquiriente bancoAdquiriente;
                Cliente cliente;
                Linea linea;
                Proveedor proveedor;
                Producto producto;
                Usuario usuario;
                CuentaEgreso cuentaEgreso;
                CuentaBanco cuentaBanco;
                Vendedor vendedor;
                Egreso egreso;
                Factura factura;
                DocumentoElectronico documento;
                string strRespuesta = "";
                switch (datos.NombreMetodo)
                {
                    case "AgregarBancoAdquiriente":
                        bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(datos.DatosPeticion);
                        BancoAdquiriente nuevoBanco = servicioMigracion.AgregarBancoAdquiriente(bancoAdquiriente);
                        strRespuesta = nuevoBanco.IdBanco.ToString();
                        break;
                    case "AgregarCliente":
                        cliente = serializer.Deserialize<Cliente>(datos.DatosPeticion);
                        Cliente nuevoCliente = servicioMigracion.AgregarCliente(cliente);
                        strRespuesta = nuevoCliente.IdCliente.ToString();
                        break;
                    case "AgregarLinea":
                        linea = serializer.Deserialize<Linea>(datos.DatosPeticion);
                        Linea nuevaLinea = servicioMigracion.AgregarLinea(linea);
                        strRespuesta = nuevaLinea.IdLinea.ToString();
                        break;
                    case "AgregarProveedor":
                        proveedor = serializer.Deserialize<Proveedor>(datos.DatosPeticion);
                        Proveedor nuevoProveedor = servicioMigracion.AgregarProveedor(proveedor);
                        strRespuesta = nuevoProveedor.IdProveedor.ToString();
                        break;
                    case "AgregarProducto":
                        producto = serializer.Deserialize<Producto>(datos.DatosPeticion);
                        Producto nuevoProducto = servicioMigracion.AgregarProducto(producto);
                        strRespuesta = nuevoProducto.IdProducto.ToString();
                        break;
                    case "AgregarUsuario":
                        usuario = serializer.Deserialize<Usuario>(datos.DatosPeticion);
                        Usuario nuevoUsuario = servicioMigracion.AgregarUsuario(usuario, strKey);
                        strRespuesta = nuevoUsuario.IdUsuario.ToString();
                        break;
                    case "AgregarUsuarioPorEmpresa":
                        JObject parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        int intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioMigracion.AgregarUsuarioPorEmpresa(intIdUsuario, intIdEmpresa);
                        strRespuesta = "";
                        break;
                    case "AgregarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(datos.DatosPeticion);
                        CuentaEgreso nuevoCuentaEgreso = servicioMigracion.AgregarCuentaEgreso(cuentaEgreso);
                        strRespuesta = nuevoCuentaEgreso.IdCuenta.ToString();
                        break;
                    case "AgregarCuentaBanco":
                        cuentaBanco = serializer.Deserialize<CuentaBanco>(datos.DatosPeticion);
                        CuentaBanco nuevoCuentaBanco = servicioMigracion.AgregarCuentaBanco(cuentaBanco);
                        strRespuesta = nuevoCuentaBanco.IdCuenta.ToString();
                        break;
                    case "AgregarVendedor":
                        vendedor = serializer.Deserialize<Vendedor>(datos.DatosPeticion);
                        Vendedor nuevoVendedor = servicioMigracion.AgregarVendedor(vendedor);
                        strRespuesta = nuevoVendedor.IdVendedor.ToString();
                        break;
                    case "AgregarEgreso":
                        egreso = serializer.Deserialize<Egreso>(datos.DatosPeticion);
                        Egreso nuevoEgreso = servicioMigracion.AgregarEgreso(egreso);
                        strRespuesta = nuevoEgreso.IdEgreso.ToString();
                        break;
                    case "AgregarFactura":
                        factura = serializer.Deserialize<Factura>(datos.DatosPeticion);
                        Factura nuevoFactura = servicioMigracion.AgregarFactura(factura);
                        strRespuesta = nuevoFactura.IdFactura.ToString();
                        break;
                    case "AgregarDocumentoElectronico":
                        documento = serializer.Deserialize<DocumentoElectronico>(datos.DatosPeticion);
                        DocumentoElectronico nuevoDocumento = servicioMigracion.AgregarDocumentoElectronico(documento, configuracion).Result;
                        strRespuesta = nuevoDocumento.IdDocumento.ToString();
                        break;
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
