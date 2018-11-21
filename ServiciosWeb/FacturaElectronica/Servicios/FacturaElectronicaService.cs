using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Unity;
using LeandroSoftware.FacturaElectronicaHacienda.Datos;
using LeandroSoftware.FacturaElectronicaHacienda.Dominio.Entidades;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace LeandroSoftware.FacturaElectronicaHacienda.Servicios
{
    public interface IFacturaElectronicaService
    {
        decimal ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta);
        List<EmpresaDTO> ConsultarListadoEmpresas();
        EmpresaDTO ConsultarEmpresa(string empresa);
        string RegistrarEmpresa(EmpresaDTO empresa);
        void RegistrarDocumentoElectronico(DatosDocumentoElectronicoDTO documentoElectronico, DatosConfiguracion configuracion, decimal decTipoDeCambioDolar);
        void EnviarDocumentoElectronico(DatosDocumentoElectronicoDTO datos, DatosConfiguracion configuracion);
        DatosDocumentoElectronicoDTO ConsultarDocumentoElectronico(int intIdEmpresa, string strClave, string strConsecutivo, DatosConfiguracion configuracion);
        List<DatosDocumentoElectronicoDTO> ConsultarListadoDocumentosElectronicos(int intIdEmpresa, string estado, DatosConfiguracion configuracion);
        void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores);
        PadronDTO ConsultarPadronPorIdentificacion(string strIdentificacion);
    }

    public class FacturaElectronicaService: IFacturaElectronicaService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IUnityContainer localContainer;
        private static HttpClient httpClient = new HttpClient();

        public FacturaElectronicaService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al inicializar el servicio de Factura electrónica. Por favor consulte con su proveedor.");
            }
        }

        private void validarToken(IDbContext dbContext, Empresa empresaLocal, string strServicioTokenURL, string strClientId)
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
                throw ex;
            }
        }

        private async Task<TokenType> ObtenerToken(string strServicioTokenURL, string strClientId, string strUsuario, string strPassword)
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
                throw ex;
            }
        }

        private async Task<TokenType> RefrescarToken(string strServicioTokenURL, string strClientId, string strRefreshToken)
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
                throw ex;
            }
        }

        public decimal ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta)
        {
            try
            {
                TipoDeCambioDolar tipoDeCambio = null;
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    string criteria = fechaConsulta.ToString("dd/MM/yyyy");
                    tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                    if (tipoDeCambio == null)
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("tcIndicador", "318"),
                            new KeyValuePair<string, string>("tcFechaInicio", fechaConsulta.ToString("dd/MM/yyyy")),
                            new KeyValuePair<string, string>("tcFechaFinal", fechaConsulta.ToString("dd/MM/yyyy")),
                            new KeyValuePair<string, string>("tcNombre", "System"),
                            new KeyValuePair<string, string>("tnSubNiveles", "N")
                        });
                        HttpResponseMessage httpResponse = httpClient.PostAsync(strServicioURL + "/ObtenerIndicadoresEconomicos", formContent).Result;

                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(httpResponse.Content.ReadAsStreamAsync().Result);
                            decimal decTipoDeCambio = 0;
                            string strTipoCambioDolar = xmlDoc.GetElementsByTagName("INGC011_CAT_INDICADORECONOMIC").Item(0).ChildNodes.Item(2).InnerText;
                            if (strTipoCambioDolar == "")
                                throw new Exception("El tipo de cambio no puede ser un valor nulo");
                            else
                            {
                                try
                                {
                                    decTipoDeCambio = Math.Round(decimal.Parse(strTipoCambioDolar, CultureInfo.InvariantCulture), 5);
                                    tipoDeCambio = new TipoDeCambioDolar
                                    {
                                        FechaTipoCambio = fechaConsulta.ToString("dd/MM/yyyy"),
                                        ValorTipoCambio = decTipoDeCambio
                                    };
                                    dbContext.TipoDeCambioDolarRepository.Add(tipoDeCambio);
                                    dbContext.Commit();
                                    return decTipoDeCambio;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error parseando el tipo de cambio: " + strTipoCambioDolar + ": " + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                            throw new Exception(responseContent);
                        }
                    }
                    else
                    {
                        return tipoDeCambio.ValorTipoCambio;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpresaDTO> ConsultarListadoEmpresas()
        {
            try
            {
                List<EmpresaDTO> empresaList = new List<EmpresaDTO>();
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    var listado = dbContext.EmpresaRepository.ToList();
                    foreach (var item in listado)
                    {
                        EmpresaDTO empresaDTO = new EmpresaDTO();
                        empresaDTO.IdEmpresa = item.IdEmpresa;
                        empresaDTO.NombreEmpresa = item.NombreEmpresa;
                        empresaDTO.UsuarioATV = item.UsuarioHacienda;
                        empresaDTO.ClaveATV = item.ClaveHacienda;
                        empresaDTO.CorreoNotificacion = item.CorreoNotificacion;
                        empresaDTO.PermiteFacturar = item.PermiteFacturar ? "S" : "N";
                        empresaList.Add(empresaDTO);
                    }
                }
                return empresaList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmpresaDTO ConsultarEmpresa(string empresa)
        {
            try
            {
                EmpresaDTO empresaDTO = new EmpresaDTO();
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    var item = dbContext.EmpresaRepository.Find(int.Parse(empresa));
                    if (item != null)
                    {
                        empresaDTO.IdEmpresa = item.IdEmpresa;
                        empresaDTO.NombreEmpresa = item.NombreEmpresa;
                        empresaDTO.UsuarioATV = item.UsuarioHacienda;
                        empresaDTO.ClaveATV = item.ClaveHacienda;
                        empresaDTO.CorreoNotificacion = item.CorreoNotificacion;
                        empresaDTO.PermiteFacturar = item.PermiteFacturar ? "S" : "N";
                    }
                }
                return empresaDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RegistrarEmpresa(EmpresaDTO empresaDTO)
        {
            try
            {
                Empresa item;
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    if (empresaDTO.IdEmpresa != null)
                    {
                        item = dbContext.EmpresaRepository.Find(empresaDTO.IdEmpresa);
                        item.NombreEmpresa = empresaDTO.NombreEmpresa;
                        item.UsuarioHacienda = empresaDTO.UsuarioATV;
                        item.ClaveHacienda = empresaDTO.ClaveATV;
                        item.CorreoNotificacion = empresaDTO.CorreoNotificacion;
                        item.PermiteFacturar = empresaDTO.PermiteFacturar == "S";
                        dbContext.NotificarModificacion(item);
                    }
                    else
                    {
                        item = new Empresa();
                        item.NombreEmpresa = empresaDTO.NombreEmpresa;
                        item.UsuarioHacienda = empresaDTO.UsuarioATV;
                        item.ClaveHacienda = empresaDTO.ClaveATV;
                        item.CorreoNotificacion = empresaDTO.CorreoNotificacion;
                        item.PermiteFacturar = empresaDTO.PermiteFacturar == "S";
                        dbContext.EmpresaRepository.Add(item);
                    }
                    dbContext.Commit();
                }
                return item.IdEmpresa.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegistrarDocumentoElectronico(DatosDocumentoElectronicoDTO datos, DatosConfiguracion configuracion, decimal decTipoDeCambioDolar)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    Empresa empresaLocal = dbContext.EmpresaRepository.Find(datos.IdEmpresa);
                    if (empresaLocal == null) throw new Exception("Empresa no registrada en el sistema de factura electrónica. Consulte con su proveedor.");
                    if (!empresaLocal.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa. Consulte con su proveedor.");
                    int intMesEnCurso = DateTime.Now.Month;
                    int intAnnioEnCurso = DateTime.Now.Year;
                    CantFEMensualEmpresa cantiFacturasMensual = dbContext.CantFEMensualEmpresaRepository.Where(x => x.IdEmpresa == datos.IdEmpresa && x.IdMes == intMesEnCurso && x.IdAnio == intAnnioEnCurso).FirstOrDefault();
                    XmlDocument documentoXml = new XmlDocument();
                    byte[] bytDatos = Convert.FromBase64String(datos.DatosDocumento);
                    using (MemoryStream ms = new MemoryStream(bytDatos))
                    {
                        documentoXml.Load(ms);
                    }
                    XmlNodeList codigoMonedaNode = documentoXml.GetElementsByTagName("CodigoMoneda");
                    if (codigoMonedaNode.Count > 0)
                        if (documentoXml.GetElementsByTagName("CodigoMoneda").Item(0).InnerText == "USD")
                            documentoXml.GetElementsByTagName("TipoCambio").Item(0).InnerText = decTipoDeCambioDolar.ToString();
                    byte[] mensajeEncoded = Encoding.UTF8.GetBytes(documentoXml.OuterXml);
                    
                    DocumentoElectronico documento = new DocumentoElectronico()
                    {
                        IdEmpresa = datos.IdEmpresa,
                        Fecha = datos.FechaEmision.ToLocalTime(),
                        ClaveNumerica = datos.ClaveNumerica,
                        Consecutivo = datos.Consecutivo,
                        TipoIdentificacionEmisor = datos.TipoIdentificacionEmisor,
                        IdentificacionEmisor = datos.IdentificacionEmisor,
                        TipoIdentificacionReceptor = datos.TipoIdentificacionReceptor,
                        IdentificacionReceptor = datos.IdentificacionReceptor,
                        EsMensajeReceptor = datos.EsMensajeReceptor,
                        EstadoEnvio = "registrado",
                        CorreoNotificacion = datos.CorreoNotificacion,
                        DatosDocumento = mensajeEncoded
                    };
                    dbContext.DocumentoElectronicoRepository.Add(documento);
                    if (cantiFacturasMensual == null)
                    {
                        cantiFacturasMensual = new CantFEMensualEmpresa();
                        cantiFacturasMensual.IdEmpresa = datos.IdEmpresa;
                        cantiFacturasMensual.IdMes = intMesEnCurso;
                        cantiFacturasMensual.IdAnio = intAnnioEnCurso;
                        cantiFacturasMensual.CantidadDoc = 1;
                        dbContext.CantFEMensualEmpresaRepository.Add(cantiFacturasMensual);
                    }
                    else
                    {
                        cantiFacturasMensual.CantidadDoc += 1;
                        dbContext.NotificarModificacion(cantiFacturasMensual);
                    }
                    dbContext.Commit();
                    string strComprobanteXML = Convert.ToBase64String(mensajeEncoded);
                    validarToken(dbContext, empresaLocal, configuracion.ServicioTokenURL, configuracion.ClientId);
                    string JsonObject = "{\"clave\": \"" + datos.ClaveNumerica + "\",\"fecha\": \"" + documento.Fecha.ToString("yyyy-MM-ddTHH:mm:ssss") + "\"," +
                        "\"emisor\": {\"tipoIdentificacion\": \"" + datos.TipoIdentificacionEmisor + "\"," +
                        "\"numeroIdentificacion\": \"" + datos.IdentificacionEmisor + "\"},";
                    if (datos.TipoIdentificacionReceptor.Length > 0)
                    {
                        JsonObject += "\"receptor\": {\"tipoIdentificacion\": \"" + datos.TipoIdentificacionReceptor + "\"," +
                        "\"numeroIdentificacion\": \"" + datos.IdentificacionReceptor + "\"},";
                    }
                    if (configuracion.CallbackURL != "")
                        JsonObject += "\"callbackUrl\": \"" + configuracion.CallbackURL + "\",";
                    if (datos.EsMensajeReceptor == "S")
                        JsonObject += "\"consecutivoReceptor\": \"" + datos.Consecutivo + "\",";
                    JsonObject += "\"comprobanteXml\": \"" + strComprobanteXML + "\"}";
                    try
                    {
                        StringContent contentJson = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                        HttpResponseMessage httpResponse = httpClient.PostAsync(configuracion.ComprobantesElectronicosURL + "/recepcion", contentJson).Result;
                        string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                        if (httpResponse.StatusCode == HttpStatusCode.Accepted)
                        {
                            documento.EstadoEnvio = "enviado";
                            dbContext.NotificarModificacion(documento);
                            dbContext.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarDocumentoElectronico(DatosDocumentoElectronicoDTO datos, DatosConfiguracion configuracion)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    Empresa empresaLocal = dbContext.EmpresaRepository.Find(datos.IdEmpresa);
                    if (empresaLocal == null) throw new Exception("Empresa no registrada en el sistema de factura electrónica. Consulte con su proveedor.");
                    if (!empresaLocal.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa. Consulte con su proveedor.");
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == datos.ClaveNumerica).FirstOrDefault();
                    if (documento == null) throw new Exception("El documento con clave numéerica " + datos.ClaveNumerica + "no se encuentra registrado en el sistema.");
                    if (documento.EstadoEnvio != "registrado") throw new Exception("El documento con clave numérica " + datos.ClaveNumerica + " ya fue enviado a hacienda.");
                    string strComprobanteXML = Convert.ToBase64String(documento.DatosDocumento);
                    validarToken(dbContext, empresaLocal, configuracion.ServicioTokenURL, configuracion.ClientId);
                    string JsonObject = "{\"clave\": \"" + documento.ClaveNumerica + "\",\"fecha\": \"" + documento.Fecha.ToString("yyyy-MM-ddTHH:mm:ssss") + "\"," +
                        "\"emisor\": {\"tipoIdentificacion\": \"" + datos.TipoIdentificacionEmisor + "\"," +
                        "\"numeroIdentificacion\": \"" + datos.IdentificacionEmisor + "\"}," +
                        "\"receptor\": {\"tipoIdentificacion\": \""+ datos.TipoIdentificacionReceptor + "\"," +
                        "\"numeroIdentificacion\": \"" + datos.IdentificacionReceptor + "\"},";
                    if (configuracion.CallbackURL != "")
                        JsonObject += "\"callbackUrl\": \"" + configuracion.CallbackURL + "\",";
                    if (datos.EsMensajeReceptor == "S")
                    {
                        JsonObject += "\"consecutivoReceptor\": \"" + datos.Consecutivo + "\",";
                    }
                    JsonObject += "\"comprobanteXml\": \"" + strComprobanteXML + "\"}";
                    try
                    {
                        StringContent contentJson = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                        HttpResponseMessage httpResponse = httpClient.PostAsync(configuracion.ComprobantesElectronicosURL + "/recepcion", contentJson).Result;
                        string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                        if (httpResponse.StatusCode == HttpStatusCode.Accepted)
                        {
                            documento.EstadoEnvio = "enviado";
                            dbContext.NotificarModificacion(documento);
                            dbContext.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatosDocumentoElectronicoDTO ConsultarDocumentoElectronico(int intIdEmpresa, string strClave, string strConsecutivo, DatosConfiguracion configuracion)
        {
            try
            {
                DatosDocumentoElectronicoDTO respuesta = new DatosDocumentoElectronicoDTO();
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    Empresa empresaLocal = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresaLocal == null) throw new Exception("Empresa no registrada en el sistema de factura electrónica. Consulte con su proveedor.");
                    if (!empresaLocal.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa. Consulte con su proveedor.");
                    DocumentoElectronico documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.ClaveNumerica == strClave && x.Consecutivo == strConsecutivo).FirstOrDefault();
                    if (documentoElectronico != null)
                    {
                        respuesta.IdEmpresa = documentoElectronico.IdEmpresa;
                        respuesta.IdDocumento = documentoElectronico.IdDocumento;
                        respuesta.ClaveNumerica = documentoElectronico.ClaveNumerica;
                        if (documentoElectronico.EstadoEnvio == "enviado")
                        {
                            validarToken(dbContext, empresaLocal, configuracion.ServicioTokenURL, configuracion.ClientId);
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                            HttpResponseMessage httpResponse = httpClient.GetAsync(configuracion.ComprobantesElectronicosURL + "/recepcion/" + documentoElectronico.ClaveNumerica).Result;
                            if (httpResponse.StatusCode == HttpStatusCode.OK)
                            {
                                JObject estadoDocumento = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);
                                string strEstado = estadoDocumento.Property("ind-estado").Value.ToString();
                                if (strEstado != "procesando")
                                {
                                    string strRespuesta = estadoDocumento.Property("respuesta-xml").Value.ToString();
                                    respuesta.RespuestaHacienda = strRespuesta;

                                }
                                respuesta.EstadoEnvio = strEstado;
                            }
                            else
                            {
                                IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                                if (headers.Count > 0)
                                    respuesta.RespuestaHacienda = headers[0];
                                respuesta.EstadoEnvio = "registrado";
                            }
                        }
                        else if (documentoElectronico.EstadoEnvio == "aceptado" || documentoElectronico.EstadoEnvio == "rechazado")
                        {
                            respuesta.EstadoEnvio = documentoElectronico.EstadoEnvio;
                            respuesta.RespuestaHacienda = Convert.ToBase64String(documentoElectronico.Respuesta);
                        }
                        else
                        {
                            respuesta.EstadoEnvio = documentoElectronico.EstadoEnvio;
                        }
                        return respuesta;
                    }
                    else
                    {
                        throw new Exception("el documento electrónico con clave numérica: " + strClave + " no se encuentra registrado en la empresa indicada. Consulte con su proveedor.");
                    }
                }         
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DatosDocumentoElectronicoDTO> ConsultarListadoDocumentosElectronicos(int intIdEmpresa, string strEstado, DatosConfiguracion configuracion)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    Empresa empresaLocal = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresaLocal == null) throw new Exception("Empresa no registrada en el sistema de factura electrónica. Consulte con su proveedor.");
                    if (!empresaLocal.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa. Consulte con su proveedor.");
                    var listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (strEstado != "")
                        listado = listado.Where(x => x.EstadoEnvio == strEstado);
                    List<DatosDocumentoElectronicoDTO> listadoDocumentoElectronico = new List<DatosDocumentoElectronicoDTO>();
                    foreach (DocumentoElectronico item in listado)
                    {
                        DatosDocumentoElectronicoDTO datos = new DatosDocumentoElectronicoDTO();
                        datos.IdEmpresa = item.IdEmpresa;
                        datos.IdDocumento = item.IdDocumento;
                        datos.ClaveNumerica = item.ClaveNumerica;
                        datos.Consecutivo = item.Consecutivo;
                        datos.FechaEmision = item.Fecha;
                        datos.TipoIdentificacionEmisor = item.TipoIdentificacionEmisor;
                        datos.IdentificacionEmisor = item.IdentificacionEmisor;
                        datos.TipoIdentificacionReceptor = item.TipoIdentificacionReceptor;
                        datos.IdentificacionReceptor = item.IdentificacionReceptor;
                        datos.EsMensajeReceptor = item.EsMensajeReceptor;
                        datos.EstadoEnvio = item.EstadoEnvio;
                        BinaryFormatter bf = new BinaryFormatter();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            datos.DatosDocumento = Convert.ToBase64String(item.DatosDocumento, 0, item.DatosDocumento.Length);
                            if (item.Respuesta != null && item.Respuesta.Length > 0)
                                datos.RespuestaHacienda = Convert.ToBase64String(item.Respuesta, 0, item.Respuesta.Length);
                        }
                        listadoDocumentoElectronico.Add(datos);
                    }
                    return listadoDocumentoElectronico;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
        {
            string strClave = "";
            string strConsecutivo = "";
            if (mensaje.Clave.Length > 50)
            {
                strClave = mensaje.Clave.Substring(0, 50);
                strConsecutivo = mensaje.Clave.Substring(51);
            }
            else
                strClave = mensaje.Clave;
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    DocumentoElectronico documentoElectronico = null;
                    if (strConsecutivo.Length > 0)
                        documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave & x.Consecutivo == strConsecutivo).FirstOrDefault();
                    else
                        documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave).FirstOrDefault();
                    if (documentoElectronico == null)
                    {
                        JArray emptyJArray = new JArray();
                        string strBody = "El documento con clave " + mensaje.Clave + " no se encuentra registrado en los registros del cliente.";
                        servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al recibir respuesta de Hacienda.", strBody, false, emptyJArray);
                    }
                    else
                    {
                        string strEstado = mensaje.IndEstado;
                        documentoElectronico.EstadoEnvio = strEstado;
                        if (strEstado == "aceptado" || strEstado == "rechazado")
                        {
                            byte[] bytRespuestaXML = Convert.FromBase64String(mensaje.RespuestaXml);
                            documentoElectronico.Respuesta = bytRespuestaXML;
                            if (strEstado == "aceptado" && documentoElectronico.CorreoNotificacion != "")
                            {
                                string strBody;
                                JArray jarrayObj = new JArray();
                                if (documentoElectronico.EsMensajeReceptor == "N")
                                {
                                    strBody = "Adjunto documento electrónico en formato PDF y XML con clave " + mensaje.Clave + " y la respuesta de Hacienda.";
                                    XmlSerializer serializer = new XmlSerializer(typeof(FacturaElectronica));
                                    MemoryStream memStream = new MemoryStream(documentoElectronico.DatosDocumento);
                                    FacturaElectronica facturaElectronica = (FacturaElectronica)serializer.Deserialize(memStream);
                                    byte[] pdfAttactment = GenerarPDFFacturaElectronica(facturaElectronica, dbContext);
                                    JObject jobDatosAdjuntos1 = new JObject();
                                    jobDatosAdjuntos1["nombre"] = documentoElectronico.ClaveNumerica + ".pdf";
                                    jobDatosAdjuntos1["contenido"] = Convert.ToBase64String(pdfAttactment);
                                    jarrayObj.Add(jobDatosAdjuntos1);
                                }
                                else
                                {
                                    strBody = "Adjunto XML de aceptación de documento electrónico con clave " + mensaje.Clave + " y la respuesta de Hacienda.";
                                }
                                JObject jobDatosAdjuntos2 = new JObject();
                                jobDatosAdjuntos2["nombre"] = documentoElectronico.ClaveNumerica + ".xml";
                                jobDatosAdjuntos2["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento);
                                jarrayObj.Add(jobDatosAdjuntos2);
                                JObject jobDatosAdjuntos3 = new JObject();
                                jobDatosAdjuntos3["nombre"] = "RespuestaHacienda.xml";
                                jobDatosAdjuntos3["contenido"] = Convert.ToBase64String(bytRespuestaXML);
                                jarrayObj.Add(jobDatosAdjuntos3);
                                servicioEnvioCorreo.SendEmail(new string[] { documentoElectronico.CorreoNotificacion }, new string[] { }, "Documento electrónico con clave " + mensaje.Clave, strBody, false, jarrayObj);
                            }
                        }
                        dbContext.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el procesamiento de la respuesta de hacienda para el comprobante con clave: " + mensaje.Clave, ex.Message, false, emptyJArray);
            }
        }

        public PadronDTO ConsultarPadronPorIdentificacion(string strIdentificacion)
        {
            PadronDTO respuesta = new PadronDTO();
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    Padron persona = dbContext.PadronRepository.Find(strIdentificacion);
                    if (persona != null)
                    {
                        respuesta.Identificacion = persona.Identificacion;
                        respuesta.IdProvincia = persona.IdProvincia;
                        respuesta.IdCanton = persona.IdCanton;
                        respuesta.IdDistrito = persona.IdDistrito;
                        respuesta.NombreCompleto = persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido;
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] GenerarPDFFacturaElectronica (FacturaElectronica facturaElectronica, IDbContext dbContext)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Documento electrónico";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Courier New", 10, XFontStyle.Bold);
                string strNombre = facturaElectronica.Emisor.NombreComercial.Length > 0 ? facturaElectronica.Emisor.NombreComercial : facturaElectronica.Emisor.Nombre;
                gfx.DrawString(strNombre.ToUpper(), font, XBrushes.Black, new XRect(0, 40, page.Width, 15), XStringFormats.Center);
                gfx.DrawString("COMPROBANTE ELECTRONICO", font, XBrushes.Black, new XRect(0, 55, page.Width, 15), XStringFormats.Center);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                gfx.DrawString("Consecutivo: ", font, XBrushes.Black, new XRect(20, 100, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.NumeroConsecutivo, font, XBrushes.Black, new XRect(110, 100, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Plazo de crédito: ", font, XBrushes.Black, new XRect(370, 100, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.PlazoCredito != null) gfx.DrawString(facturaElectronica.PlazoCredito, font, XBrushes.Black, new XRect(470, 100, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Clave: ", font, XBrushes.Black, new XRect(20, 112, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Clave, font, XBrushes.Black, new XRect(110, 112, 200, 12), XStringFormats.TopLeft);

                string strCondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(facturaElectronica.CondicionVenta.ToString().Substring(5)));
                gfx.DrawString("Condición de Venta: ", font, XBrushes.Black, new XRect(370, 112, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strCondicionVenta, font, XBrushes.Black, new XRect(470, 112, 80, 12), XStringFormats.TopLeft);

                string strMedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(facturaElectronica.MedioPago[0].ToString().Substring(5)));
                gfx.DrawString("Fecha: ", font, XBrushes.Black, new XRect(20, 124, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.FechaEmision.ToString("dd/MM/yyyy hh:mm:ss"), font, XBrushes.Black, new XRect(110, 124, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Medio de Pago: ", font, XBrushes.Black, new XRect(370, 124, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strMedioPago, font, XBrushes.Black, new XRect(470, 124, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Tipo Documento: ", font, XBrushes.Black, new XRect(20, 136, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString("Factura electrónica", font, XBrushes.Black, new XRect(110, 136, 200, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("DATOS DEL EMISOR", font, XBrushes.Black, new XRect(20, 151, 100, 12), XStringFormats.TopLeft);

                font = new XFont("Arial", 8, XFontStyle.Regular);
                gfx.DrawString("Nombre: ", font, XBrushes.Black, new XRect(20, 163, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Emisor.Nombre, font, XBrushes.Black, new XRect(110, 163, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Identificación: ", font, XBrushes.Black, new XRect(370, 163, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Emisor.Identificacion.Numero, font, XBrushes.Black, new XRect(470, 163, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Nombre comercial: ", font, XBrushes.Black, new XRect(20, 175, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Emisor.Nombre, font, XBrushes.Black, new XRect(110, 175, 400, 12), XStringFormats.TopLeft);

                gfx.DrawString("Correo electrónico: ", font, XBrushes.Black, new XRect(20, 187, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Emisor.CorreoElectronico, font, XBrushes.Black, new XRect(110, 187, 400, 12), XStringFormats.TopLeft);

                gfx.DrawString("Teléfono: ", font, XBrushes.Black, new XRect(20, 199, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.Emisor.Telefono != null) gfx.DrawString(facturaElectronica.Emisor.Telefono.NumTelefono, font, XBrushes.Black, new XRect(110, 199, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Fax: ", font, XBrushes.Black, new XRect(370, 199, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.Emisor.Fax != null) gfx.DrawString(facturaElectronica.Emisor.Fax.NumTelefono, font, XBrushes.Black, new XRect(470, 199, 80, 12), XStringFormats.TopLeft);

                int intProvincia = int.Parse(facturaElectronica.Emisor.Ubicacion.Provincia);
                int intCanton = int.Parse(facturaElectronica.Emisor.Ubicacion.Canton);
                int intDistrito = int.Parse(facturaElectronica.Emisor.Ubicacion.Distrito);
                int intBarrio = int.Parse(facturaElectronica.Emisor.Ubicacion.Barrio);
                string strProvincia = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                gfx.DrawString("Provincia: ", font, XBrushes.Black, new XRect(20, 211, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strProvincia, font, XBrushes.Black, new XRect(110, 211, 200, 12), XStringFormats.TopLeft);
                string strCanton = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                gfx.DrawString("Cantón: ", font, XBrushes.Black, new XRect(370, 211, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strCanton, font, XBrushes.Black, new XRect(470, 211, 80, 12), XStringFormats.TopLeft);

                string strDistrito = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                gfx.DrawString("Distrito: ", font, XBrushes.Black, new XRect(20, 223, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strDistrito, font, XBrushes.Black, new XRect(110, 223, 200, 12), XStringFormats.TopLeft);
                string strBarrio = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                gfx.DrawString("Barrio: ", font, XBrushes.Black, new XRect(370, 223, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strBarrio, font, XBrushes.Black, new XRect(470, 223, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Otras señas: ", font, XBrushes.Black, new XRect(20, 235, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Emisor.Ubicacion.OtrasSenas, font, XBrushes.Black, new XRect(110, 235, 400, 12), XStringFormats.TopLeft);

                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("DATOS DEL CLIENTE", font, XBrushes.Black, new XRect(20, 252, 100, 12), XStringFormats.TopLeft);

                font = new XFont("Arial", 8, XFontStyle.Regular);
                gfx.DrawString("Nombre: ", font, XBrushes.Black, new XRect(20, 264, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Receptor.Nombre, font, XBrushes.Black, new XRect(110, 264, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Identificación: ", font, XBrushes.Black, new XRect(370, 264, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Receptor.Identificacion.Numero, font, XBrushes.Black, new XRect(470, 264, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Nombre comercial: ", font, XBrushes.Black, new XRect(20, 276, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Receptor.Nombre, font, XBrushes.Black, new XRect(110, 276, 400, 12), XStringFormats.TopLeft);

                gfx.DrawString("Correo electrónico: ", font, XBrushes.Black, new XRect(20, 288, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Receptor.CorreoElectronico, font, XBrushes.Black, new XRect(110, 288, 400, 12), XStringFormats.TopLeft);

                gfx.DrawString("Teléfono: ", font, XBrushes.Black, new XRect(20, 300, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.Receptor.Telefono != null) gfx.DrawString(facturaElectronica.Receptor.Telefono.NumTelefono, font, XBrushes.Black, new XRect(110, 300, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Fax: ", font, XBrushes.Black, new XRect(370, 300, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.Receptor.Fax != null) gfx.DrawString(facturaElectronica.Receptor.Fax.NumTelefono, font, XBrushes.Black, new XRect(470, 300, 80, 12), XStringFormats.TopLeft);

                intProvincia = int.Parse(facturaElectronica.Receptor.Ubicacion.Provincia);
                intCanton = int.Parse(facturaElectronica.Receptor.Ubicacion.Canton);
                intDistrito = int.Parse(facturaElectronica.Receptor.Ubicacion.Distrito);
                intBarrio = int.Parse(facturaElectronica.Receptor.Ubicacion.Barrio);
                strProvincia = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                gfx.DrawString("Provincia: ", font, XBrushes.Black, new XRect(20, 312, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strProvincia, font, XBrushes.Black, new XRect(110, 312, 200, 12), XStringFormats.TopLeft);
                strCanton = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                gfx.DrawString("Cantón: ", font, XBrushes.Black, new XRect(370, 312, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strCanton, font, XBrushes.Black, new XRect(470, 312, 80, 12), XStringFormats.TopLeft);

                strDistrito = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                gfx.DrawString("Distrito: ", font, XBrushes.Black, new XRect(20, 324, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strDistrito, font, XBrushes.Black, new XRect(110, 324, 200, 12), XStringFormats.TopLeft);
                strBarrio = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                gfx.DrawString("Barrio: ", font, XBrushes.Black, new XRect(370, 324, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(strBarrio, font, XBrushes.Black, new XRect(470, 324, 80, 12), XStringFormats.TopLeft);

                gfx.DrawString("Otras señas: ", font, XBrushes.Black, new XRect(20, 336, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(facturaElectronica.Receptor.Ubicacion.OtrasSenas, font, XBrushes.Black, new XRect(110, 336, 400, 12), XStringFormats.TopLeft);

                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("DETALLE DE SERVICIOS", font, XBrushes.Black, new XRect(20, 353, 100, 12), XStringFormats.TopLeft);

                gfx.DrawString("Línea", font, XBrushes.Black, new XRect(30, 365, 30, 12), XStringFormats.TopLeft);
                gfx.DrawString("Código", font, XBrushes.Black, new XRect(60, 365, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString("Detalle", font, XBrushes.Black, new XRect(140, 365, 280, 12), XStringFormats.TopLeft);
                gfx.DrawString("Precio Unitario", font, XBrushes.Black, new XRect(432.5, 365, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString("Total", font, XBrushes.Black, new XRect(557.5, 365, 80, 12), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.DarkGray, 28, 376, 582, 376);

                font = new XFont("Arial", 8, XFontStyle.Regular);
                int lineaPos = 365;
                foreach (FacturaElectronicaLineaDetalle linea in facturaElectronica.DetalleServicio)
                {
                    lineaPos += 12;
                    gfx.DrawString(linea.NumeroLinea, font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.Center);
                    gfx.DrawString(linea.Codigo[0].Codigo, font, XBrushes.Black, new XRect(60, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(linea.Detalle, font, XBrushes.Black, new XRect(140, lineaPos, 280, 12), XStringFormats.TopLeft);
                    string strPrecioUnitario = linea.PrecioUnitario.ToString();
                    double intPrecioUnitarioLength = 420 + 80 - (strPrecioUnitario.Length * 4.5);
                    gfx.DrawString(strPrecioUnitario, font, XBrushes.Black, new XRect(intPrecioUnitarioLength, lineaPos, 80, 12), XStringFormats.TopLeft);
                    string strTotal = linea.MontoTotalLinea.ToString();
                    double intTotalLength = 500 + 80 - (strTotal.Length * 4.5);
                    gfx.DrawString(strTotal, font, XBrushes.Black, new XRect(intTotalLength, lineaPos, 80, 12), XStringFormats.TopLeft);
                }
                gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 582, lineaPos + 11);
                lineaPos += 17;
                gfx.DrawString("SubTotal Factura:", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                string strResumenDato = facturaElectronica.ResumenFactura.TotalVenta.ToString();
                double intResumenLength = 500 + 80 - (strResumenDato.Length * 4.5);
                gfx.DrawString(strResumenDato, font, XBrushes.Black, new XRect(intResumenLength, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Descuento:", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.ResumenFactura.TotalDescuentosSpecified)
                    strResumenDato = facturaElectronica.ResumenFactura.TotalDescuentos.ToString();
                if (strResumenDato == "0")
                    strResumenDato = "0.00000";
                intResumenLength = 500 + 80 - (strResumenDato.Length * 4.5);
                gfx.DrawString(strResumenDato, font, XBrushes.Black, new XRect(intResumenLength, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Impuesto:", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                if (facturaElectronica.ResumenFactura.TotalImpuestoSpecified)
                    strResumenDato = facturaElectronica.ResumenFactura.TotalImpuesto.ToString();
                if (strResumenDato == "0")
                    strResumenDato = "0.00000";
                intResumenLength = 500 + 80 - (strResumenDato.Length * 4.5);
                gfx.DrawString(strResumenDato, font, XBrushes.Black, new XRect(intResumenLength, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Total General:", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                strResumenDato = facturaElectronica.ResumenFactura.TotalComprobante.ToString();
                intResumenLength = 500 + 80 - (strResumenDato.Length * 4.5);
                gfx.DrawString(strResumenDato, font, XBrushes.Black, new XRect(intResumenLength, lineaPos, 80, 12), XStringFormats.TopLeft);

                if (facturaElectronica.ResumenFactura.CodigoMonedaSpecified)
                {
                    lineaPos += 32;
                    gfx.DrawString("Codigo Moneda:", font, XBrushes.Black, new XRect(70, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(facturaElectronica.ResumenFactura.CodigoMoneda.ToString(), font, XBrushes.Black, new XRect(150, lineaPos, 80, 12), XStringFormats.TopLeft);
                    if (facturaElectronica.ResumenFactura.TipoCambioSpecified)
                    {
                        lineaPos += 12;
                        strResumenDato = facturaElectronica.ResumenFactura.TipoCambio.ToString();
                        if (strResumenDato == "0")
                            strResumenDato = "0.00000";
                        gfx.DrawString("Tipo de cambio:", font, XBrushes.Black, new XRect(70, lineaPos, 80, 12), XStringFormats.TopLeft);
                        gfx.DrawString(strResumenDato, font, XBrushes.Black, new XRect(150, lineaPos, 80, 12), XStringFormats.TopLeft);
                    }
                }

                string filename = facturaElectronica.Clave + ".pdf";
                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                byte[] bytes = stream.ToArray();
                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
