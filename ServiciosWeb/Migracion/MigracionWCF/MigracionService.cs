using LeandroSoftware.AccesoDatos.Datos;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Puntoventa.CommonTypes;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Xml;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LeandroSoftware.Migracion.ServicioWCF
{
    public interface IMigracionService
    {
        BancoAdquiriente AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        Cliente AgregarCliente(Cliente cliente);
        Linea AgregarLinea(Linea linea);
        Proveedor AgregarProveedor(Proveedor cuenta);
        Producto AgregarProducto(Producto producto);
        Usuario AgregarUsuario(Usuario usuario, string strKey);
        void AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa);
        CuentaEgreso AgregarCuentaEgreso(CuentaEgreso cuenta);
        CuentaBanco AgregarCuentaBanco(CuentaBanco cuentaBanco);
        Vendedor AgregarVendedor(Vendedor vendedor);
        Egreso AgregarEgreso(Egreso egreso);
        Factura AgregarFactura(Factura factura);
        Task<DocumentoElectronico> AgregarDocumentoElectronico(DocumentoElectronico documento, DatosConfiguracion datos);
    }

    public class MigracionService : IMigracionService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static HttpClient httpClient = new HttpClient();

        public MigracionService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw ex;
            }
        }

        public BancoAdquiriente AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.BancoAdquirienteRepository.Add(bancoAdquiriente);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de banco adquiriente: ", ex);
                    throw ex;
                }
            }
            return bancoAdquiriente;
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ClienteRepository.Add(cliente);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el cliente: ", ex);
                    throw ex;
                }
            }
            return cliente;
        }

        public Linea AgregarLinea(Linea linea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.LineaRepository.Add(linea);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la línea de producto: ", ex);
                    throw ex;
                }
            }
            return linea;
        }

        public Proveedor AgregarProveedor(Proveedor proveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ProveedorRepository.Add(proveedor);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el proveedor: ", ex);
                    throw ex;
                }
            }
            return proveedor;
        }

        public Producto AgregarProducto(Producto producto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    bool existe = dbContext.ProductoRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.Codigo == producto.Codigo).Count() > 0;
                    if (existe) throw new BusinessException("El código de producto ingresado ya está registrado en la empresa.");
                    dbContext.ProductoRepository.Add(producto);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el producto: ", ex);
                    throw ex;
                }
            }
            return producto;
        }

        public Usuario AgregarUsuario(Usuario usuario, string strKey)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuarioExistente = dbContext.UsuarioRepository.Where(x => x.CodigoUsuario.ToUpper().Contains(usuario.CodigoUsuario.ToUpper())).FirstOrDefault();
                    if (usuarioExistente != null) throw new BusinessException("El código de usuario que desea agregar ya existe.");
                    usuario.Clave = Core.Utilitario.EncriptarDatos(usuario.Clave, strKey);
                    dbContext.UsuarioRepository.Add(usuario);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el usuario: ", ex);
                    throw ex;
                }
            }
            return usuario;
        }

        public void AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    UsuarioPorEmpresa usuarioEmpresa = new UsuarioPorEmpresa
                    {
                        IdUsuario = intIdUsuario,
                        IdEmpresa = intIdEmpresa
                    };
                    dbContext.UsuarioPorEmpresaRepository.Add(usuarioEmpresa);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de usuario por empresa: ", ex);
                    throw ex;
                }
            }
        }

        public CuentaEgreso AgregarCuentaEgreso(CuentaEgreso cuenta)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuenta.IdEmpresa); ;
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaEgresoRepository.Add(cuenta);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la cuenta de egreso: ", ex);
                    throw ex;
                }
            }
            return cuenta;
        }

        public CuentaBanco AgregarCuentaBanco(CuentaBanco cuentaBanco)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cuentaBanco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.CuentaBancoRepository.Add(cuentaBanco);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la cuenta bancaría: ", ex);
                    throw ex;
                }
            }
            return cuentaBanco;
        }

        public Vendedor AgregarVendedor(Vendedor vendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.VendedorRepository.Add(vendedor);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el vendedor: ", ex);
                    throw ex;
                }
            }
            return vendedor;
        }

        public Egreso AgregarEgreso(Egreso egreso)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(egreso.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    CuentaEgreso cuentaEgreso = dbContext.CuentaEgresoRepository.Find(egreso.IdCuenta);
                    if (cuentaEgreso == null) throw new BusinessException("La cuenta de egreso asignada al registro no existe");
                    dbContext.EgresoRepository.Add(egreso);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de egreso: ", ex);
                    throw ex;
                }
            }
            return egreso;
        }

        public Factura AgregarFactura(Factura factura)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.FacturaRepository.Add(factura);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de facturación: ", ex);
                    throw ex;
                }
            }
            return factura;
        }

        public async Task<DocumentoElectronico> AgregarDocumentoElectronico(DocumentoElectronico documento, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.DocumentoElectronicoRepository.Add(documento);
                    dbContext.Commit();
                    if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Procesando)
                    {
                        await EnviarDocumentoElectronico(empresa.IdEmpresa, documento.IdDocumento, datos);
                    }
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de facturación: ", ex);
                    throw ex;
                }
            }
            return documento;
        }

        private static async Task EnviarDocumentoElectronico(int intIdEmpresa, int intIdDocumento, DatosConfiguracion datos)
        {

            try
            {
                string connString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
                localContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
                localContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento != null)
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                        if (empresa != null)
                        {
                            XmlDocument documentoXml = new XmlDocument();
                            using (MemoryStream ms = new MemoryStream(documento.DatosDocumento))
                            {
                                documentoXml.Load(ms);
                            }
                            byte[] mensajeEncoded = Encoding.UTF8.GetBytes(documentoXml.OuterXml);
                            string strComprobanteXML = Convert.ToBase64String(mensajeEncoded);
                            ValidarToken(dbContext, empresa, datos.ServicioTokenURL, datos.ClientId);
                            if (empresa.AccessToken != null)
                            {
                                try
                                {
                                    string JsonObject = "{\"clave\": \"" + documento.ClaveNumerica + "\",\"fecha\": \"" + documento.Fecha.ToString("yyyy-MM-ddTHH:mm:ssss") + "\"," +
                                        "\"emisor\": {\"tipoIdentificacion\": \"" + documento.TipoIdentificacionEmisor + "\"," +
                                        "\"numeroIdentificacion\": \"" + documento.IdentificacionEmisor + "\"},";
                                    if (documento.TipoIdentificacionReceptor.Length > 0)
                                    {
                                        JsonObject += "\"receptor\": {\"tipoIdentificacion\": \"" + documento.TipoIdentificacionReceptor + "\"," +
                                        "\"numeroIdentificacion\": \"" + documento.IdentificacionReceptor + "\"},";
                                    }
                                    if (datos.CallbackURL != "")
                                        JsonObject += "\"callbackUrl\": \"" + datos.CallbackURL + "\",";
                                    if (documento.EsMensajeReceptor == "S")
                                        JsonObject += "\"consecutivoReceptor\": \"" + documento.Consecutivo + "\",";
                                    JsonObject += "\"comprobanteXml\": \"" + strComprobanteXML + "\"}";
                                    StringContent contentJson = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresa.AccessToken);
                                    HttpResponseMessage httpResponse = httpClient.PostAsync(datos.ComprobantesElectronicosURL + "/recepcion", contentJson).Result;
                                    string responseContent = await httpResponse.Content.ReadAsStringAsync();
                                    if (httpResponse.StatusCode == HttpStatusCode.Accepted)
                                    {
                                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                                    }
                                    else
                                    {
                                        if (httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value != null)
                                        {
                                            IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                                            if (headers.Count > 0)
                                            {
                                                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                                                {
                                                    if (headers[0] == "El comprobante [" + documento.ClaveNumerica + "] ya fue recibido anteriormente.")
                                                    {
                                                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                                                    }
                                                    else
                                                    {
                                                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                                        documento.ErrorEnvio = headers[0];
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                            documento.ErrorEnvio = httpResponse.ReasonPhrase;
                                        }
                                    }
                                    dbContext.NotificarModificacion(documento);
                                    dbContext.Commit();
                                }
                                catch (Exception ex)
                                {
                                    string strMensajeError = ex.Message;
                                    if (ex.Message.Length > 500) strMensajeError = ex.Message.Substring(0, 500);
                                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                    documento.ErrorEnvio = strMensajeError;
                                    dbContext.NotificarModificacion(documento);
                                    dbContext.Commit();
                                }
                            }
                            else
                            {
                                documento.ErrorEnvio = "No se logro obtener un token válido para la empresa correspondiente al documento electrónico.";
                                dbContext.NotificarModificacion(documento);
                                dbContext.Commit();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el enviar el documento electrónico: ", ex);
                throw ex;
            }
        }

        private static void ValidarToken(IDbContext dbContext, Empresa empresaLocal, string strServicioTokenURL, string strClientId)
        {
            TokenType nuevoToken = null;
            try
            {
                if (empresaLocal.AccessToken != null)
                {
                    if (empresaLocal.EmitedAt != null)
                    {
                        DateTime horaEmision = DateTime.Parse(empresaLocal.EmitedAt.ToString());
                        if (horaEmision.AddSeconds((int)empresaLocal.ExpiresIn) < DateTime.Now)
                        {
                            if (horaEmision.AddSeconds((int)empresaLocal.RefreshExpiresIn) < DateTime.Now)
                            {
                                nuevoToken = ObtenerToken(strServicioTokenURL, strClientId, empresaLocal.UsuarioHacienda, empresaLocal.ClaveHacienda).Result;
                                empresaLocal.AccessToken = nuevoToken.access_token;
                                empresaLocal.ExpiresIn = nuevoToken.expires_in;
                                empresaLocal.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                                empresaLocal.RefreshToken = nuevoToken.refresh_token;
                                empresaLocal.EmitedAt = nuevoToken.emitedAt;
                                dbContext.NotificarModificacion(empresaLocal);
                                dbContext.Commit();
                            }
                            else
                            {
                                nuevoToken = RefrescarToken(strServicioTokenURL, strClientId, empresaLocal.RefreshToken).Result;
                                empresaLocal.AccessToken = nuevoToken.access_token;
                                empresaLocal.ExpiresIn = nuevoToken.expires_in;
                                empresaLocal.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                                empresaLocal.RefreshToken = nuevoToken.refresh_token;
                                empresaLocal.EmitedAt = nuevoToken.emitedAt;
                                dbContext.NotificarModificacion(empresaLocal);
                                dbContext.Commit();
                            }
                        }
                    }
                }
                else
                {
                    nuevoToken = ObtenerToken(strServicioTokenURL, strClientId, empresaLocal.UsuarioHacienda, empresaLocal.ClaveHacienda).Result;
                    empresaLocal.AccessToken = nuevoToken.access_token;
                    empresaLocal.ExpiresIn = nuevoToken.expires_in;
                    empresaLocal.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                    empresaLocal.RefreshToken = nuevoToken.refresh_token;
                    empresaLocal.EmitedAt = nuevoToken.emitedAt;
                    dbContext.NotificarModificacion(empresaLocal);
                    dbContext.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al validar el token: ", ex);
                throw ex;
            }
        }

        private static async Task<TokenType> ObtenerToken(string strServicioTokenURL, string strClientId, string strUsuario, string strPassword)
        {
            try
            {
                FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", strClientId),
                    new KeyValuePair<string, string>("username", strUsuario),
                    new KeyValuePair<string, string>("password", strPassword)
                });
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioTokenURL + "/token", formContent);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                TokenType objToken = new JavaScriptSerializer().Deserialize<TokenType>(responseContent);
                objToken.emitedAt = DateTime.Now;
                return objToken;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener un token nuevo: ", ex);
                throw ex;
            }
        }

        private static async Task<TokenType> RefrescarToken(string strServicioTokenURL, string strClientId, string strRefreshToken)
        {
            try
            {
                FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("client_id", strClientId),
                    new KeyValuePair<string, string>("refresh_token", strRefreshToken)
                });
                HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioTokenURL + "/token", formContent);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                TokenType objToken = new JavaScriptSerializer().Deserialize<TokenType>(responseContent);
                objToken.emitedAt = DateTime.Now;
                return objToken;
            }
            catch (Exception ex)
            {
                log.Error("Error al refrescar un token existente: ", ex);
                throw ex;
            }
        }
    }
}