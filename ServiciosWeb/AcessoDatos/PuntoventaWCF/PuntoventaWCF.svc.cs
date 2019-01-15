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
using LeandroSoftware.Puntoventa.CommonTypes;
using LeandroSoftware.Core.CustomClasses;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    public class PuntoventaWCF : IPuntoventaWCF, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ICorreoService servicioEnvioCorreo;
        private IMantenimientoService servicioMantenimiento;
        private IFacturacionService servicioFacturacion;
        private ICompraService servicioCompra;
        private IEgresoService servicioEgreso;
        private IBancaService servicioBanca;
        IUnityContainer unityContainer;
        private static decimal decTipoCambioDolar;
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
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<ICorreoService, CorreoService>(new InjectionConstructor(appSettings["strEmailHost"], appSettings["strEmailPort"], appSettings["strEmailAccount"], appSettings["strEmailPass"], appSettings["strEmailFrom"], appSettings["strSSLHost"]));
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            unityContainer.RegisterType<IFacturacionService, FacturacionService>();
            unityContainer.RegisterType<ICompraService, CompraService>();
            unityContainer.RegisterType<IEgresoService, EgresoService>();
            unityContainer.RegisterType<IBancaService, BancaService>();
            servicioEnvioCorreo = unityContainer.Resolve<ICorreoService>();
            servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
            servicioFacturacion = unityContainer.Resolve<IFacturacionService>();
            servicioCompra = unityContainer.Resolve<ICompraService>();
            servicioEgreso = unityContainer.Resolve<IEgresoService>();
            servicioBanca = unityContainer.Resolve<IBancaService>();
            try
            {
                decTipoCambioDolar = ComprobanteElectronicoService.ObtenerTipoCambioVenta(configuracion.ConsultaIndicadoresEconomicosURL, configuracion.OperacionSoap, DateTime.Now, unityContainer);
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
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                JObject parametrosJO;
                int intIdEmpresa;
                int intIdUsuario;
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
                    case "RemoverLogoEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioMantenimiento.ActualizarLogoEmpresa(intIdEmpresa, "");
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
                    case "ActualizarProducto":
                        Producto producto = serializer.Deserialize<Producto>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarProducto(producto);
                        break;
                    case "EliminarProducto":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        servicioMantenimiento.EliminarProducto(intIdProducto);
                        break;
                    case "ActualizarUsuario":
                        Usuario usuario = serializer.Deserialize<Usuario>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarUsuario(usuario);
                        break;
                    case "AgregarUsuarioPorEmpresa":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioMantenimiento.AgregarUsuarioPorEmpresa(intIdUsuario, intIdEmpresa);
                        break;
                    case "EliminarUsuario":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioMantenimiento.EliminarUsuario(intIdUsuario);
                        break;
                    case "ActualizarCuentaEgreso":
                        CuentaEgreso cuentaEgreso = serializer.Deserialize<CuentaEgreso>(datos.DatosPeticion);
                        servicioEgreso.ActualizarCuentaEgreso(cuentaEgreso);
                        break;
                    case "EliminarCuentaEgreso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCuentaEgreso = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        servicioEgreso.EliminarCuentaEgreso(intIdCuentaEgreso);
                        break;
                    case "ActualizarCuentaBanco":
                        CuentaBanco cuentaBanco = serializer.Deserialize<CuentaBanco>(datos.DatosPeticion);
                        servicioBanca.ActualizarCuentaBanco(cuentaBanco);
                        break;
                    case "EliminarCuentaBanco":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCuentaBanco = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        servicioBanca.EliminarCuentaBanco(intIdCuentaBanco);
                        break;
                    case "ActualizarVendedor":
                        Vendedor vendedor = serializer.Deserialize<Vendedor>(datos.DatosPeticion);
                        servicioMantenimiento.ActualizarVendedor(vendedor);
                        break;
                    case "EliminarVendedor":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        servicioMantenimiento.EliminarVendedor(intIdVendedor);
                        break;
                    case "AnularEgreso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioEgreso.AnularEgreso(intIdEgreso, intIdUsuario);
                        break;
                    case "ActualizarEgreso":
                        Egreso egreso = serializer.Deserialize<Egreso>(datos.DatosPeticion);
                        servicioEgreso.ActualizarEgreso(egreso);
                        break;
                    case "AnularFactura":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        servicioFacturacion.AnularFactura(intIdFactura, intIdUsuario, configuracion);
                        break;
                    case "ActualizarFactura":
                        Factura factura = serializer.Deserialize<Factura>(datos.DatosPeticion);
                        servicioFacturacion.ActualizarFactura(factura);
                        break;
                    case "GeneraMensajeReceptor":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        string strDatos = parametrosJO.Property("Datos").Value.ToString();
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        int intSucursal = int.Parse(parametrosJO.Property("Sucursal").Value.ToString());
                        int intTerminal = int.Parse(parametrosJO.Property("Terminal").Value.ToString());
                        int intEstado = int.Parse(parametrosJO.Property("Estado").Value.ToString());
                        servicioFacturacion.GeneraMensajeReceptor(strDatos, intIdEmpresa, intSucursal, intTerminal, intEstado, configuracion);
                        break;
                    case "ProcesarDocumentosElectronicosPendientes":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        servicioFacturacion.ProcesarDocumentosElectronicosPendientes(intIdEmpresa, servicioEnvioCorreo, configuracion);
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
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public string EjecutarConsulta(RequestDTO datos)
        {
            try
            {
                JavaScriptSerializer serializer = new CustomJavascriptSerializer();
                Empresa empresa;
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
                JObject parametrosJO;
                int intIdEmpresa;
                int intIdProvincia;
                int intIdCanton;
                int intIdDistrito;
                int intIdDocumento;
                int intIdEgreso;
                int intIdFactura;
                int intNumeroPagina;
                int intFilasPorPagina;
                int intTotalLista;
                bool bolIncluyeServicios;
                string strIdentificacion;
                string strCodigo;
                string strDescripcion;
                string strNombre;
                string strBeneficiario;
                string strDetalle;
                string strRespuesta = "";
                switch (datos.NombreMetodo)
                {
                    case "ValidarCredenciales":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        strIdentificacion = parametrosJO.Property("Identificacion").Value.ToString();
                        string strUsuario = parametrosJO.Property("Usuario").Value.ToString();
                        string strClave = parametrosJO.Property("Clave").Value.ToString();
                        usuario = servicioMantenimiento.ValidarCredenciales(strIdentificacion, strUsuario, strClave);
                        if (usuario != null)
                        {
                            foreach (RolePorUsuario role in usuario.RolePorUsuario)
                                role.Usuario = null;
                            strRespuesta = serializer.Serialize(usuario);
                        }
                        break;
                    case "ObtenerTipoCambioDolar":
                        strRespuesta = decTipoCambioDolar.ToString();
                        break;
                    case "ObtenerListaTipodePrecio":
                        IList<TipodePrecio> listadoTipodePrecio = (List<TipodePrecio>)servicioMantenimiento.ObtenerListaTipodePrecio();
                        if (listadoTipodePrecio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipodePrecio);
                        break;
                    case "ObtenerListaTipoProducto":
                        IList<TipoProducto> listadoTipoProducto = (List<TipoProducto>)servicioMantenimiento.ObtenerListaTipoProducto();
                        if (listadoTipoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoProducto);
                        break;
                    case "ObtenerListaTipoUnidad":
                        IList<TipoUnidad> listadoTipoUnidad = (List<TipoUnidad>)servicioMantenimiento.ObtenerListaTipoUnidad();
                        if (listadoTipoUnidad.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoUnidad);
                        break;
                    case "ObtenerListaFormaPagoEgreso":
                        IList<FormaPago> listadoFormaPagoEgreso = (List<FormaPago>)servicioMantenimiento.ObtenerListaFormaPagoEgreso();
                        if (listadoFormaPagoEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoEgreso);
                        break;
                    case "ObtenerListaFormaPagoFactura":
                        IList<FormaPago> listadoFormaPagoFactura = (List<FormaPago>)servicioMantenimiento.ObtenerListaFormaPagoFactura();
                        if (listadoFormaPagoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFormaPagoFactura);
                        break;
                    case "ObtenerListaTipoMoneda":
                        IList<TipoMoneda> listadoTipoMoneda = (List<TipoMoneda>)servicioMantenimiento.ObtenerListaTipoMoneda();
                        if (listadoTipoMoneda.Count > 0)
                            strRespuesta = serializer.Serialize(listadoTipoMoneda);
                        break;
                    case "ObtenerListaCondicionVenta":
                        IList<CondicionVenta> listadoCondicionVenta = (List<CondicionVenta>)servicioMantenimiento.ObtenerListaCondicionVenta();
                        if (listadoCondicionVenta.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCondicionVenta);
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
                    case "ObtenerListaLineasDeProducto":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<Linea> listadoLineaProducto = (List<Linea>)servicioMantenimiento.ObtenerListaLineasDeProducto(intIdEmpresa);
                        if (listadoLineaProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoLineaProducto);
                        break;
                    case "ObtenerListaLineasDeServicio":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<Linea> listadoLineaServicio = (List<Linea>)servicioMantenimiento.ObtenerListaLineasDeServicio(intIdEmpresa);
                        if (listadoLineaServicio.Count > 0)
                            strRespuesta = serializer.Serialize(listadoLineaServicio);
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
                    case "ObtenerTotalListaProductos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        intTotalLista = servicioMantenimiento.ObtenerTotalListaProductos(intIdEmpresa, bolIncluyeServicios, intIdLinea, strCodigo, strDescripcion);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListaProductos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        bolIncluyeServicios = bool.Parse(parametrosJO.Property("IncluyeServicios").Value.ToString());
                        intIdLinea = int.Parse(parametrosJO.Property("IdLinea").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<Producto> listadoProducto = (List<Producto>)servicioMantenimiento.ObtenerListaProductos(intIdEmpresa, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, intIdLinea, strCodigo, strDescripcion);
                        if (listadoProducto.Count > 0)
                            strRespuesta = serializer.Serialize(listadoProducto);
                        break;
                    case "ObtenerProducto":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdProducto = int.Parse(parametrosJO.Property("IdProducto").Value.ToString());
                        producto = servicioMantenimiento.ObtenerProducto(intIdProducto);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "ObtenerProductoPorCodigo":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        producto = servicioMantenimiento.ObtenerProductoPorCodigo(intIdEmpresa, strCodigo);
                        if (producto != null)
                            strRespuesta = serializer.Serialize(producto);
                        break;
                    case "AgregarProducto":
                        producto = serializer.Deserialize<Producto>(datos.DatosPeticion);
                        Producto nuevoProducto = servicioMantenimiento.AgregarProducto(producto);
                        strRespuesta = nuevoProducto.IdProducto.ToString();
                        break;
                    case "ObtenerListaUsuarios":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strCodigo = parametrosJO.Property("Codigo").Value.ToString();
                        IList<Usuario> listadoUsuario = (List<Usuario>)servicioMantenimiento.ObtenerListaUsuarios(intIdEmpresa, strCodigo);
                        if (listadoUsuario.Count > 0)
                            strRespuesta = serializer.Serialize(listadoUsuario);
                        break;
                    case "ObtenerUsuario":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        usuario = servicioMantenimiento.ObtenerUsuario(intIdUsuario);
                        if (usuario != null)
                            strRespuesta = serializer.Serialize(usuario);
                        break;
                    case "AgregarUsuario":
                        usuario = serializer.Deserialize<Usuario>(datos.DatosPeticion);
                        Usuario nuevoUsuario = servicioMantenimiento.AgregarUsuario(usuario);
                        strRespuesta = nuevoUsuario.IdUsuario.ToString();
                        break;
                    case "ActualizarClaveUsuario":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdUsuario = int.Parse(parametrosJO.Property("IdUsuario").Value.ToString());
                        strClave = parametrosJO.Property("Clave").Value.ToString();
                        usuario = servicioMantenimiento.ActualizarClaveUsuario(intIdUsuario, strClave);
                        if (usuario != null)
                            strRespuesta = serializer.Serialize(usuario);
                        break;
                    case "ObtenerListaCuentasEgreso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<CuentaEgreso> listadoCuentaEgreso = (List<CuentaEgreso>)servicioEgreso.ObtenerListaCuentasEgreso(intIdEmpresa, strDescripcion);
                        if (listadoCuentaEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaEgreso);
                        break;
                    case "ObtenerCuentaEgreso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCuentaEgreso = int.Parse(parametrosJO.Property("IdCuentaEgreso").Value.ToString());
                        cuentaEgreso = servicioEgreso.ObtenerCuentaEgreso(intIdCuentaEgreso);
                        if (cuentaEgreso != null)
                            strRespuesta = serializer.Serialize(cuentaEgreso);
                        break;
                    case "AgregarCuentaEgreso":
                        cuentaEgreso = serializer.Deserialize<CuentaEgreso>(datos.DatosPeticion);
                        CuentaEgreso nuevoCuentaEgreso = servicioEgreso.AgregarCuentaEgreso(cuentaEgreso);
                        strRespuesta = nuevoCuentaEgreso.IdCuenta.ToString();
                        break;
                    case "ObtenerListaCuentasBanco":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strDescripcion = parametrosJO.Property("Descripcion").Value.ToString();
                        IList<CuentaBanco> listadoCuentaBanco = (List<CuentaBanco>)servicioBanca.ObtenerListaCuentasBanco(intIdEmpresa, strDescripcion);
                        if (listadoCuentaBanco.Count > 0)
                            strRespuesta = serializer.Serialize(listadoCuentaBanco);
                        break;
                    case "ObtenerCuentaBanco":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdCuentaBanco = int.Parse(parametrosJO.Property("IdCuentaBanco").Value.ToString());
                        cuentaBanco = servicioBanca.ObtenerCuentaBanco(intIdCuentaBanco);
                        if (cuentaBanco != null)
                            strRespuesta = serializer.Serialize(cuentaBanco);
                        break;
                    case "AgregarCuentaBanco":
                        cuentaBanco = serializer.Deserialize<CuentaBanco>(datos.DatosPeticion);
                        CuentaBanco nuevoCuentaBanco = servicioBanca.AgregarCuentaBanco(cuentaBanco);
                        strRespuesta = nuevoCuentaBanco.IdCuenta.ToString();
                        break;
                    case "ObtenerListaVendedores":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        IList<Vendedor> listadoVendedores = (List<Vendedor>)servicioMantenimiento.ObtenerListaVendedores(intIdEmpresa, strNombre);
                        if (listadoVendedores.Count > 0)
                            strRespuesta = serializer.Serialize(listadoVendedores);
                        break;
                    case "ObtenerVendedor":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        int intIdVendedor = int.Parse(parametrosJO.Property("IdVendedor").Value.ToString());
                        vendedor = servicioMantenimiento.ObtenerVendedor(intIdVendedor);
                        if (vendedor != null)
                            strRespuesta = serializer.Serialize(vendedor);
                        break;
                    case "ObtenerVendedorPorDefecto":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        vendedor = servicioMantenimiento.ObtenerVendedorPorDefecto(intIdEmpresa);
                        if (vendedor != null)
                            strRespuesta = serializer.Serialize(vendedor);
                        break;
                    case "AgregarVendedor":
                        vendedor = serializer.Deserialize<Vendedor>(datos.DatosPeticion);
                        Vendedor nuevoVendedor = servicioMantenimiento.AgregarVendedor(vendedor);
                        strRespuesta = nuevoVendedor.IdVendedor.ToString();
                        break;
                    case "ObtenerTotalListaEgresos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        intTotalLista = servicioEgreso.ObtenerTotalListaEgresos(intIdEmpresa, intIdEgreso, strBeneficiario, strDetalle);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListaEgresos":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        strBeneficiario = parametrosJO.Property("Beneficiario").Value.ToString();
                        strDetalle = parametrosJO.Property("Detalle").Value.ToString();
                        IList<Egreso> listadoEgreso = (List<Egreso>)servicioEgreso.ObtenerListaEgresos(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdEgreso, strBeneficiario, strDetalle);
                        if (listadoEgreso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEgreso);
                        break;
                    case "ObtenerEgreso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEgreso = int.Parse(parametrosJO.Property("IdEgreso").Value.ToString());
                        egreso = servicioEgreso.ObtenerEgreso(intIdEgreso);
                        if (egreso != null)
                            strRespuesta = serializer.Serialize(egreso);
                        break;
                    case "AgregarEgreso":
                        egreso = serializer.Deserialize<Egreso>(datos.DatosPeticion);
                        Egreso nuevoEgreso = servicioEgreso.AgregarEgreso(egreso);
                        strRespuesta = nuevoEgreso.IdEgreso.ToString();
                        break;

                    case "ObtenerTotalListaFacturas":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        intTotalLista = servicioFacturacion.ObtenerTotalListaFacturas(intIdEmpresa, intIdFactura, strNombre);
                        strRespuesta = serializer.Serialize(intTotalLista);
                        break;
                    case "ObtenerListaFacturas":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        strNombre = parametrosJO.Property("Nombre").Value.ToString();
                        IList<Factura> listadoFactura = (List<Factura>)servicioFacturacion.ObtenerListaFacturas(intIdEmpresa, intNumeroPagina, intFilasPorPagina, intIdFactura, strNombre);
                        if (listadoFactura.Count > 0)
                            strRespuesta = serializer.Serialize(listadoFactura);
                        break;
                    case "ObtenerFactura":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdFactura = int.Parse(parametrosJO.Property("IdFactura").Value.ToString());
                        factura = servicioFacturacion.ObtenerFactura(intIdFactura);
                        if (factura != null)
                            strRespuesta = serializer.Serialize(factura);
                        break;
                    case "AgregarFactura":
                        factura = serializer.Deserialize<Factura>(datos.DatosPeticion);
                        Factura nuevoFactura = servicioFacturacion.AgregarFactura(factura, configuracion);
                        strRespuesta = nuevoFactura.IdFactura.ToString();
                        break;

                    case "ObtenerListaDocumentosElectronicosEnProceso":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        intIdEmpresa = int.Parse(parametrosJO.Property("IdEmpresa").Value.ToString());
                        IList<DocumentoElectronico> listadoEnProceso = servicioFacturacion.ObtenerListaDocumentosElectronicosEnProceso(intIdEmpresa);
                        if (listadoEnProceso.Count > 0)
                            strRespuesta = serializer.Serialize(listadoEnProceso);
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
                        intNumeroPagina = int.Parse(parametrosJO.Property("NumeroPagina").Value.ToString());
                        intFilasPorPagina = int.Parse(parametrosJO.Property("FilasPorPagina").Value.ToString());
                        IList<DocumentoElectronico> listadoProcesados = (List<DocumentoElectronico>)servicioFacturacion.ObtenerListaDocumentosElectronicosProcesados(intIdEmpresa, intNumeroPagina, intFilasPorPagina);
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
                    case "ObtenerDocumentoElectronicoPorClave":
                        parametrosJO = JObject.Parse(datos.DatosPeticion);
                        strClave = parametrosJO.Property("Clave").Value.ToString();
                        documento = servicioFacturacion.ObtenerDocumentoElectronicoPorClave(strClave);
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
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje)
        {
            servicioFacturacion.ProcesarRespuestaHacienda(mensaje, servicioEnvioCorreo, configuracion.CorreoNotificacionErrores);
        }

        public void Dispose()
        {
            unityContainer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
