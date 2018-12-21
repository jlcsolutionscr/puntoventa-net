using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Servicios;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core.Servicios;
using log4net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using LeandroSoftware.Core.CommonTypes;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class PuntoventaWCF : IPuntoventaWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICorreoService servicioEnvioCorreo;
        private IMantenimientoService servicioMantenimiento;
        private IFacturacionService servicioFacturacion;
        private ICompraService servicioCompra;
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

        public PuntoventaWCF()
        {
            unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            // unityContainer.RegisterInstance("configuracionData", configuracion, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<ICompraService, CompraService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioCompra = unityContainer.Resolve<ICompraService>();
            try
            {
                ComprobanteElectronicoService.ObtenerTipoCambioVenta(configuracion.ConsultaIndicadoresEconomicosURL, configuracion.OperacionSoap, DateTime.Now, unityContainer);
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el tipo de cambio del dolar: ", ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void Ejecutar(RequestDTO datos)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                JObject parametrosJO;
                int intIdEmpresa;
                int intIdDocumento;
                switch (datos.NombreMetodo)
                {
                    case "ActualizarEmpresa":
                        Empresa empresa = null;
                        empresa = serializer.Deserialize<Empresa>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarEmpresa(empresa);
                        break;
                    case "ActualizarLogoEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strLogotipo = parametrosJO.Property("Logotipo").Value.ToString();
                        servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, strLogotipo);
                        break;
                    case "ActualizarCertificadoEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strCertificado = parametrosJO.Property("Certificado").Value.ToString();
                        servicioMantenimiento.ActualizarCertificadoEmpresa(intIdEmpresa, strCertificado);
                        break;
                    case "ActualizarBancoAdquiriente":
                        BancoAdquiriente bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarBancoAdquiriente(bancoAdquiriente);
                        break;
                    case "EliminarBancoAdquiriente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdBancoAdquiriente = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        servicioMantenimiento.EliminarBancoAdquiriente(intIdBancoAdquiriente);
                        break;
                    case "ActualizarCliente":
                        Cliente cliente = serializer.Deserialize<Cliente>(datos.DatosPeticion);
                        servicioFacturacion.ActualizarCliente(cliente);
                        break;
                    case "EliminarCliente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        servicioFacturacion.EliminarCliente(intIdCliente);
                        break;
                    case "ActualizarLinea":
                        Linea linea = serializer.Deserialize<Linea>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarLinea(linea);
                        break;
                    case "EliminarLinea":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        servicioMantenimiento.EliminarLinea(intIdLinea);
                        break;
                    case "ActualizarProveedor":
                        Proveedor proveedor = serializer.Deserialize<Proveedor>(datos.DatosPeticion);
                        servicioCompra.ActualizarProveedor(proveedor);
                        break;
                    case "EliminarProveedor":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        servicioCompra.EliminarProveedor(intIdProveedor);
                        break;
                    case "ActualizarUsuario":
                        Usuario usuario = serializer.Deserialize<Usuario>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarUsuario(usuario, appSettings["AppThumptprint"]);
                        break;
                    case "EliminarUsuario":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioMantenimiento.EliminarUsuario(intIdUsuario);
                        break;
                    case "EnviarDocumentoElectronicoPendiente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarDocumentoElectronicoPendiente(intIdDocumento, configuracion);
                        break;
                    case "EnviarNotificacionDocumentoElectronico":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        servicioFacturacion.EnviarNotificacionDocumentoElectronico(intIdDocumento, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al procesar petición sin respuesta: " + datos.NombreMetodo, ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Empresa empresa;
                Usuario usuario;
                BancoAdquiriente bancoAdquiriente;
                Cliente cliente;
                Linea linea;
                Proveedor proveedor;
                DocumentoElectronico documento;
                JObject parametrosJO;
                int intIdEmpresa;
                string strIdentificacion;
                int intIdProvincia;
                int intIdCanton;
                int intIdDistrito;
                int intIdDocumento;
                int intNumeroPagina;
                int intFilasPorPagina;
                int intTotalLista;
                string strRespuesta = "";
                string strDescripcion;
                string strNombre;
                switch (datos.NombreMetodo)
                {
                    case "ValidarCredenciales":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                        string strUsuario = parametrosJO.Property("Usuario").Value.ToString();
                        string strClave = parametrosJO.Property("Clave").Value.ToString();
                        usuario = servicioMantenimiento.ValidarCredenciales(strIdentificacion, strUsuario, strClave, appSettings["AppThumptprint"]);
                        if (usuario != null)
                        {
                            foreach (RolePorUsuario role in usuario.RolePorUsuario)
                                role.Usuario = null;
                            strRespuesta = serializer.Serialize(usuario);
                        }
                        break;
                    case "ObtenerListaTipoProducto":
                        IList<TipoProducto> listadoTipoProducto = (List<TipoProducto>)servicioMantenimiento.ObtenerListaTipoProducto();
                        if (listadoTipoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoProducto);
                        break;
                    case "ObtenerListaRoles":
                        IList<Role> listadoRoles = (List<Role>)servicioMantenimiento.ObtenerListaRoles();
                        if (listadoRoles.Count > 0)
                            strRespuesta = serializer.Serialize(listadoRoles);
                        break;
                    case "ObtenerListaEmpresas":
                        IList<Empresa> listadoEmpresa = (List<Empresa>)servicioMantenimiento.ObtenerListaEmpresas();
                        if (listadoEmpresa.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEmpresa);
                        break;
                    case "ObtenerEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        empresa = servicioMantenimiento.ObtenerEmpresa(intIdEmpresa);
                        if (empresa != null)
                        {
                            strRespuesta = serializer.Serialize(empresa);
                        }
                        break;
                    case "AgregarEmpresa":
                        empresa = serializer.Deserialize<Empresa>(datos.DatosPeticion);
                        Empresa nuevaEmpresa = servicioMantenimiento.AgregarEmpresa(empresa);
                        strRespuesta = nuevaEmpresa.IdEmpresa.ToString();
                        break;
                    case "ObtenerListaTipoIdentificacion":
                        IList<TipoIdentificacion> listadoTipoIdentificacion = (List<TipoIdentificacion>)servicioMantenimiento.ObtenerListaTipoIdentificacion();
                        if (listadoTipoIdentificacion.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoIdentificacion);
                        break;
                    case "ObtenerListaModulos":
                        IList<Modulo> listadoModulos = (List<Modulo>)servicioMantenimiento.ObtenerListaModulos();
                        if (listadoModulos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoModulos);
                        break;
                    case "ObtenerListaReportes":
                        IList<CatalogoReporte> listadoReportes = (List<CatalogoReporte>)servicioMantenimiento.ObtenerListaReportes();
                        if (listadoReportes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoReportes);
                        break;
                    case "ObtenerListaProvincias":
                        IList<Provincia> listadoProvincias = (List<Provincia>)servicioMantenimiento.ObtenerListaProvincias();
                        if (listadoProvincias.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProvincias);
                        break;
                    case "ObtenerListaCantones":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        IList<Canton> listadoCantones = (List<Canton>)servicioMantenimiento.ObtenerListaCantones(intIdProvincia);
                        if (listadoCantones.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCantones);
                        break;
                    case "ObtenerListaDistritos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        IList<Distrito> listadoDistritos = (List<Distrito>)servicioMantenimiento.ObtenerListaDistritos(intIdProvincia, intIdCanton);
                        if (listadoDistritos.Count > 0)
                            strRespuesta = serializer.Serialize(listadoDistritos);
                        break;
                    case "ObtenerListaBarrios":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdProvincia = int.Parse(parametrosJO.Property("IdProvincia").Value.ToString());
                        intIdCanton = int.Parse(parametrosJO.Property("IdCanton").Value.ToString());
                        intIdDistrito = int.Parse(parametrosJO.Property("IdDistrito").Value.ToString());
                        IList<Barrio> listadoBarrios = (List<Barrio>)servicioMantenimiento.ObtenerListaBarrios(intIdProvincia, intIdCanton, intIdDistrito);
                        if (listadoBarrios.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBarrios);
                        break;
                    case "ObtenerListaVendedores":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        IList<Vendedor> listadoVendedores = (List<Vendedor>)servicioMantenimiento.ObtenerListaVendedores(intIdEmpresa, strNombre);
                        if (listadoVendedores.Count > 0)
                            strRespuesta = serializer.Serialize(listadoVendedores);
                        break;
                    case "ObtenerListaTipodePrecio":
                        IList<TipodePrecio> listadoTipodePrecio = (List<TipodePrecio>)servicioMantenimiento.ObtenerListaTipodePrecio();
                        if (listadoTipodePrecio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipodePrecio);
                        break;
                    case "ObtenerListaBancoAdquiriente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<BancoAdquiriente> listadoBancoAdquiriente = (List<BancoAdquiriente>)servicioMantenimiento.ObtenerListaBancoAdquiriente(intIdEmpresa, strDescripcion);
                        if (listadoBancoAdquiriente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoBancoAdquiriente);
                        break;
                    case "ObtenerBancoAdquiriente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdBanco = int.Parse(parametrosJO.Property("IdBancoAdquiriente").Value.ToString());
                        bancoAdquiriente = servicioMantenimiento.ObtenerBancoAdquiriente(intIdBanco);
                        if (bancoAdquiriente != null)
                            strRespuesta = serializer.Serialize(bancoAdquiriente);
                        break;
                    case "AgregarBancoAdquiriente":
                        bancoAdquiriente = serializer.Deserialize<BancoAdquiriente>(datos.DatosPeticion);
                        BancoAdquiriente nuevoBanco = servicioMantenimiento.AgregarBancoAdquiriente(bancoAdquiriente);
                        strRespuesta = nuevoBanco.IdBanco.ToString();
                        break;
                    case "ObtenerTotalListaClientes":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        intTotalLista = servicioFacturacion.ObtenerTotalListaClientes(intIdEmpresa, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListaClientes":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        IList<Cliente> listadoCliente = (List<Cliente>)servicioFacturacion.ObtenerListaClientes(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoCliente.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCliente);
                        break;
                    case "ObtenerCliente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCliente = int.Parse(parametrosJO.Property("IdCliente").Value.ToString());
                        cliente = servicioFacturacion.ObtenerCliente(intIdCliente);
                        if (cliente != null)
                            strRespuesta = serializer.Serialize(cliente);
                        break;
                    case "ValidaIdentificacionCliente":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                        cliente = servicioFacturacion.ValidaIdentificacionCliente(intIdEmpresa, strIdentificacion);
                        if (cliente != null)
                            strRespuesta = serializer.Serialize(cliente);
                        break;
                    case "AgregarCliente":
                        cliente = serializer.Deserialize<Cliente>(datos.DatosPeticion);
                        Cliente nuevoCliente = servicioFacturacion.AgregarCliente(cliente);
                        strRespuesta = nuevoCliente.IdCliente.ToString();
                        break;
                    case "ObtenerListaLineas":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<Linea> listadoLinea = (List<Linea>)servicioMantenimiento.ObtenerListaLineas(intIdEmpresa, strDescripcion);
                        if (listadoLinea.Count > 0)
                            strRespuesta = serializer.Serialize(listadoLinea);
                        break;
                    case "ObtenerLinea":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        linea = servicioMantenimiento.ObtenerLinea(intIdLinea);
                        if (linea != null)
                            strRespuesta = serializer.Serialize(linea);
                        break;
                    case "AgregarLinea":
                        linea = serializer.Deserialize<Linea>(datos.DatosPeticion);
                        Linea nuevaLinea = servicioMantenimiento.AgregarLinea(linea);
                        strRespuesta = nuevaLinea.IdLinea.ToString();
                        break;
                    case "ObtenerTotalListaProveedores":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        intTotalLista = servicioCompra.ObtenerTotalListaProveedores(intIdEmpresa, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListaProveedores":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        IList<Proveedor> listadoProveedor = (List<Proveedor>)servicioCompra.ObtenerListaProveedores(intIdEmpresa, intNumeroPagina, intFilasPorPagina, strNombre);
                        if (listadoProveedor.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProveedor);
                        break;
                    case "ObtenerProveedor":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdProveedor = int.Parse(parametrosJO.Property("IdProveedor").Value.ToString());
                        proveedor = servicioCompra.ObtenerProveedor(intIdProveedor);
                        if (proveedor != null)
                            strRespuesta = serializer.Serialize(proveedor);
                        break;
                    case "AgregarProveedor":
                        proveedor = serializer.Deserialize<Proveedor>(datos.DatosPeticion);
                        Proveedor nuevoProveedor = servicioCompra.AgregarProveedor(proveedor);
                        strRespuesta = nuevoProveedor.IdProveedor.ToString();
                        break;
                    case "ObtenerListaUsuarios":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        string strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        IList<Usuario> listadoUsuario = (List<Usuario>)servicioMantenimiento.ObtenerListaUsuarios(intIdEmpresa, strCodigo);
                        if (listadoUsuario.Count > 0)
                            strRespuesta = serializer.Serialize(listadoUsuario);
                        break;
                    case "ObtenerUsuario":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        usuario = servicioMantenimiento.ObtenerUsuario(intIdUsuario, appSettings["AppThumptprint"]);
                        if (usuario != null)
                            strRespuesta = serializer.Serialize(usuario);
                        break;
                    case "AgregarUsuario":
                        usuario = serializer.Deserialize<Usuario>(datos.DatosPeticion);
                        Usuario nuevoUsuario = servicioMantenimiento.AgregarUsuario(usuario, appSettings["AppThumptprint"]);
                        strRespuesta = nuevoUsuario.IdUsuario.ToString();
                        break;
                    case "ObtenerListaDocumentosElectronicosPendientes":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoElectronico> listadoPendientes = servicioFacturacion.ObtenerListaDocumentosElectronicosPendientes(intIdEmpresa);
                        if (listadoPendientes.Count > 0)
                            strRespuesta = serializer.Serialize(listadoPendientes);
                        break;
                    case "ObtenerListaDocumentosElectronicosEnviados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoElectronico> listadoEnviados = servicioFacturacion.ObtenerListaDocumentosElectronicosEnviados(intIdEmpresa);
                        if (listadoEnviados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEnviados);
                        break;
                    case "ObtenerTotalDocumentosElectronicosProcesados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intTotalDocumentosProcesados = servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa);
                        strRespuesta = intTotalDocumentosProcesados.ToString();
                        break;
                    case "ObtenerListaDocumentosElectronicosProcesados":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intPagina = int.Parse(parametrosJO.Property("Pagina").Value.ToString());
                        int intCantDoc = int.Parse(parametrosJO.Property("Cantidad").Value.ToString());
                        IList<DocumentoElectronico> listadoProcesados = (List<DocumentoElectronico>)servicioFacturacion.ObtenerListaDocumentosElectronicosProcesados(intIdEmpresa, intPagina, intCantDoc);
                        if (listadoProcesados.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProcesados);
                        break;
                    case "ObtenerDocumentoElectronico":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerDocumentoElectronico(intIdDocumento);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                    case "ObtenerRespuestaDocumentoElectronicoEnviado":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdDocumento = int.Parse(parametrosJO.Property("IdDocumento").Value.ToString());
                        documento = servicioFacturacion.ObtenerRespuestaDocumentoElectronicoEnviado(intIdDocumento, configuracion);
                        if (documento != null)
                            strRespuesta = serializer.Serialize(documento);
                        break;
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                log.Error("Error al procesar petición con respuesta: " + datos.NombreMetodo, ex);
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            try
            {
                servicioFacturacion.ProcesarRespuestaHacienda(mensaje, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
            }
            catch (Exception ex)
            {
                log.Error("Error al recibir mensaje de respuesta de Hacienda: ", ex);
            }
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
