﻿using System;
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
using log4net;
using LeandroSoftware.Core;
using System.Drawing;
using System.Web.Hosting;

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
                        if (item.Logotipo != null)
                            try
                            {
                                empresaDTO.Logotipo = Convert.ToBase64String(item.Logotipo);
                            }
                            catch (Exception)
                            {
                                empresaDTO.Logotipo = "";
                            }
                        else
                            empresaDTO.Logotipo = "";
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
                        try
                        {
                            item.Logotipo = Convert.FromBase64String(empresaDTO.Logotipo);
                        }
                        catch (Exception)
                        {
                            item.Logotipo = null;
                        }
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
                        try
                        {
                            item.Logotipo = Convert.FromBase64String(empresaDTO.Logotipo);
                        }
                        catch (Exception)
                        {
                            item.Logotipo = null;
                        }
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
                        IdTipoDocumento = datos.IdTipoDocumento,
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
                    if (empresaLocal.AccessToken != null)
                    {
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
                    if (empresaLocal.AccessToken == null) throw new Exception("No se logró obtener un token valido con los parámetros proporcionados. Consulte con su proveedor.");
                    string JsonObject = "{\"clave\": \"" + documento.ClaveNumerica + "\",\"fecha\": \"" + documento.Fecha.ToString("yyyy-MM-ddTHH:mm:ssss") + "\"," +
                        "\"emisor\": {\"tipoIdentificacion\": \"" + datos.TipoIdentificacionEmisor + "\"," +
                        "\"numeroIdentificacion\": \"" + datos.IdentificacionEmisor + "\"}," +
                        "\"receptor\": {\"tipoIdentificacion\": \"" + datos.TipoIdentificacionReceptor + "\"," +
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
                    }
                    else
                    {
                        respuesta.IdEmpresa = intIdEmpresa;
                        respuesta.ClaveNumerica = strClave;
                        respuesta.Consecutivo = strConsecutivo;
                        respuesta.EstadoEnvio = "noexiste";
                    }
                    return respuesta;
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
                    Empresa empresa = null;
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
                        empresa = dbContext.EmpresaRepository.Where(x => x.IdEmpresa == documentoElectronico.IdEmpresa).FirstOrDefault();
                        string strEstado = mensaje.IndEstado;
                        documentoElectronico.EstadoEnvio = strEstado;
                        if (strEstado == "aceptado" || strEstado == "rechazado")
                        {
                            byte[] bytRespuestaXML = Convert.FromBase64String(mensaje.RespuestaXml);
                            documentoElectronico.Respuesta = bytRespuestaXML;
                            try
                            {
                                string strBody;
                                JArray jarrayObj = new JArray();
                                if (documentoElectronico.EsMensajeReceptor == "N")
                                {
                                    if (strEstado == "aceptado" && documentoElectronico.CorreoNotificacion != "")
                                    {
                                        strBody = "Adjunto documento electrónico en formato PDF y XML con clave " + mensaje.Clave + " y la respuesta de aceptación del Ministerio de Hacienda.";
                                        EstructuraPDF datos = new EstructuraPDF();
                                        try
                                        {
                                            Image logoImage;
                                            using (MemoryStream memStream = new MemoryStream(empresa.Logotipo))
                                                logoImage = Image.FromStream(memStream);
                                            datos.Logotipo = logoImage;
                                        }
                                        catch (Exception)
                                        {
                                            datos.Logotipo = null;
                                        }
                                        try
                                        {
                                            string apPath = HostingEnvironment.ApplicationPhysicalPath + "bin\\images\\Logo.png";
                                            Image poweredByImage = Image.FromFile(apPath);
                                            datos.PoweredByLogotipo = poweredByImage;
                                        }
                                        catch (Exception)
                                        {
                                            datos.PoweredByLogotipo = null;
                                        }
                                        if (documentoElectronico.IdTipoDocumento == 1)
                                        {
                                            XmlSerializer serializer = new XmlSerializer(typeof(FacturaElectronica));
                                            FacturaElectronica facturaElectronica;
                                            using (MemoryStream memStream = new MemoryStream(documentoElectronico.DatosDocumento))
                                                facturaElectronica = (FacturaElectronica)serializer.Deserialize(memStream);
                                            
                                            datos.TituloDocumento = "FACTURA ELECTRONICA";
                                            datos.NombreEmpresa = facturaElectronica.Emisor.NombreComercial != null ? facturaElectronica.Emisor.NombreComercial : facturaElectronica.Emisor.Nombre;
                                            datos.Consecutivo = facturaElectronica.NumeroConsecutivo;
                                            datos.PlazoCredito = facturaElectronica.PlazoCredito != null ? facturaElectronica.PlazoCredito : "";
                                            datos.Clave = facturaElectronica.Clave;
                                            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(facturaElectronica.CondicionVenta.ToString().Substring(5)));
                                            datos.Fecha = facturaElectronica.FechaEmision.ToString("dd/MM/yyyy hh:mm:ss");
                                            datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(facturaElectronica.MedioPago[0].ToString().Substring(5)));
                                            datos.NombreEmisor = facturaElectronica.Emisor.Nombre;
                                            datos.NombreComercialEmisor = facturaElectronica.Emisor.NombreComercial;
                                            datos.IdentificacionEmisor = facturaElectronica.Emisor.Identificacion.Numero;
                                            datos.CorreoElectronicoEmisor = facturaElectronica.Emisor.CorreoElectronico;
                                            datos.TelefonoEmisor = facturaElectronica.Emisor.Telefono != null ? facturaElectronica.Emisor.Telefono.NumTelefono.ToString() : "";
                                            datos.FaxEmisor = facturaElectronica.Emisor.Fax != null ? facturaElectronica.Emisor.Fax.NumTelefono.ToString() : "";
                                            int intProvincia = int.Parse(facturaElectronica.Emisor.Ubicacion.Provincia);
                                            int intCanton = int.Parse(facturaElectronica.Emisor.Ubicacion.Canton);
                                            int intDistrito = int.Parse(facturaElectronica.Emisor.Ubicacion.Distrito);
                                            int intBarrio = int.Parse(facturaElectronica.Emisor.Ubicacion.Barrio);
                                            datos.ProvinciaEmisor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                            datos.DireccionEmisor = facturaElectronica.Emisor.Ubicacion.OtrasSenas;
                                            if (facturaElectronica.Receptor != null)
                                            {
                                                datos.PoseeReceptor = true;
                                                datos.NombreReceptor = facturaElectronica.Receptor.Nombre;
                                                datos.NombreComercialReceptor = facturaElectronica.Receptor.NombreComercial != null ? facturaElectronica.Receptor.NombreComercial : "";
                                                datos.IdentificacionReceptor = facturaElectronica.Receptor.Identificacion.Numero;
                                                datos.CorreoElectronicoReceptor = facturaElectronica.Receptor.CorreoElectronico;
                                                datos.TelefonoReceptor = facturaElectronica.Receptor.Telefono != null ? facturaElectronica.Receptor.Telefono.NumTelefono.ToString() : "";
                                                datos.FaxReceptor = facturaElectronica.Receptor.Fax != null ? facturaElectronica.Receptor.Fax.NumTelefono.ToString() : "";
                                                intProvincia = int.Parse(facturaElectronica.Receptor.Ubicacion.Provincia);
                                                intCanton = int.Parse(facturaElectronica.Receptor.Ubicacion.Canton);
                                                intDistrito = int.Parse(facturaElectronica.Receptor.Ubicacion.Distrito);
                                                intBarrio = int.Parse(facturaElectronica.Receptor.Ubicacion.Barrio);
                                                datos.ProvinciaReceptor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                                datos.CantonReceptor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                                datos.DistritoReceptor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                                datos.BarrioReceptor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                                datos.DireccionReceptor = facturaElectronica.Receptor.Ubicacion.OtrasSenas;
                                            }
                                            foreach (FacturaElectronicaLineaDetalle linea in facturaElectronica.DetalleServicio)
                                            {
                                                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio();
                                                detalle.NumeroLinea = linea.NumeroLinea;
                                                detalle.Codigo = linea.Codigo[0].Codigo;
                                                detalle.Detalle = linea.Detalle;
                                                detalle.PrecioUnitario = string.Format("{0:N5}", Convert.ToDouble(linea.PrecioUnitario, CultureInfo.InvariantCulture));
                                                detalle.TotalLinea = string.Format("{0:N5}", Convert.ToDouble(linea.MontoTotalLinea, CultureInfo.InvariantCulture));
                                                datos.DetalleServicio.Add(detalle);
                                            }
                                            datos.SubTotal = string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalVenta, CultureInfo.InvariantCulture));
                                            datos.Descuento = facturaElectronica.ResumenFactura.TotalDescuentosSpecified ? string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalDescuentos, CultureInfo.InvariantCulture)) : "0.00000";
                                            datos.Impuesto = facturaElectronica.ResumenFactura.TotalImpuestoSpecified ? string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalImpuesto, CultureInfo.InvariantCulture)) : "0.00000";
                                            datos.TotalGeneral = string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalComprobante, CultureInfo.InvariantCulture));
                                            datos.CodigoMoneda = facturaElectronica.ResumenFactura.CodigoMonedaSpecified ? facturaElectronica.ResumenFactura.CodigoMoneda.ToString() : "";
                                            datos.TipoDeCambio = facturaElectronica.ResumenFactura.CodigoMonedaSpecified ? facturaElectronica.ResumenFactura.TipoCambio.ToString() : "";
                                        }
                                        else if (documentoElectronico.IdTipoDocumento == 3)
                                        {
                                            XmlSerializer serializer = new XmlSerializer(typeof(NotaCreditoElectronica));
                                            NotaCreditoElectronica notaCreditoElectronica;
                                            using (MemoryStream memStream = new MemoryStream(documentoElectronico.DatosDocumento))
                                                notaCreditoElectronica = (NotaCreditoElectronica)serializer.Deserialize(memStream);
                                            datos.TituloDocumento = "NOTA DE CREDITO ELECTRONICA";
                                            datos.NombreEmpresa = notaCreditoElectronica.Emisor.NombreComercial != null ? notaCreditoElectronica.Emisor.NombreComercial : notaCreditoElectronica.Emisor.Nombre;
                                            datos.Consecutivo = notaCreditoElectronica.NumeroConsecutivo;
                                            datos.PlazoCredito = notaCreditoElectronica.PlazoCredito != null ? notaCreditoElectronica.PlazoCredito : "";
                                            datos.Clave = notaCreditoElectronica.Clave;
                                            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(notaCreditoElectronica.CondicionVenta.ToString().Substring(5)));
                                            datos.Fecha = notaCreditoElectronica.FechaEmision.ToString("dd/MM/yyyy hh:mm:ss");
                                            if (notaCreditoElectronica.MedioPago != null)
                                                datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(notaCreditoElectronica.MedioPago[0].ToString().Substring(5)));
                                            else
                                                datos.MedioPago = "";
                                            datos.NombreEmisor = notaCreditoElectronica.Emisor.Nombre;
                                            datos.NombreComercialEmisor = notaCreditoElectronica.Emisor.NombreComercial;
                                            datos.IdentificacionEmisor = notaCreditoElectronica.Emisor.Identificacion.Numero;
                                            datos.CorreoElectronicoEmisor = notaCreditoElectronica.Emisor.CorreoElectronico;
                                            datos.TelefonoEmisor = notaCreditoElectronica.Emisor.Telefono != null ? notaCreditoElectronica.Emisor.Telefono.NumTelefono.ToString() : "";
                                            datos.FaxEmisor = notaCreditoElectronica.Emisor.Fax != null ? notaCreditoElectronica.Emisor.Fax.NumTelefono.ToString() : "";
                                            int intProvincia = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Provincia);
                                            int intCanton = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Canton);
                                            int intDistrito = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Distrito);
                                            int intBarrio = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Barrio);
                                            datos.ProvinciaEmisor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                            datos.DireccionEmisor = notaCreditoElectronica.Emisor.Ubicacion.OtrasSenas;
                                            if (notaCreditoElectronica.Receptor != null)
                                            {
                                                datos.PoseeReceptor = true;
                                                datos.NombreReceptor = notaCreditoElectronica.Receptor.Nombre;
                                                datos.NombreComercialReceptor = notaCreditoElectronica.Receptor.NombreComercial != null ? notaCreditoElectronica.Receptor.NombreComercial : "";
                                                datos.IdentificacionReceptor = notaCreditoElectronica.Receptor.Identificacion.Numero;
                                                datos.CorreoElectronicoReceptor = notaCreditoElectronica.Receptor.CorreoElectronico;
                                                datos.TelefonoReceptor = notaCreditoElectronica.Receptor.Telefono != null ? notaCreditoElectronica.Receptor.Telefono.NumTelefono.ToString() : "";
                                                datos.FaxReceptor = notaCreditoElectronica.Receptor.Fax != null ? notaCreditoElectronica.Receptor.Fax.NumTelefono.ToString() : "";
                                                intProvincia = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Provincia);
                                                intCanton = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Canton);
                                                intDistrito = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Distrito);
                                                intBarrio = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Barrio);
                                                datos.ProvinciaReceptor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                                datos.CantonReceptor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                                datos.DistritoReceptor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                                datos.BarrioReceptor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                                datos.DireccionReceptor = notaCreditoElectronica.Receptor.Ubicacion.OtrasSenas;
                                            }
                                            foreach (NotaCreditoElectronicaLineaDetalle linea in notaCreditoElectronica.DetalleServicio)
                                            {
                                                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio();
                                                detalle.NumeroLinea = linea.NumeroLinea;
                                                detalle.Codigo = linea.Codigo[0].Codigo;
                                                detalle.Detalle = linea.Detalle;
                                                detalle.PrecioUnitario = string.Format("{0:N5}", Convert.ToDouble(linea.PrecioUnitario, CultureInfo.InvariantCulture));
                                                detalle.TotalLinea = string.Format("{0:N5}", Convert.ToDouble(linea.MontoTotalLinea, CultureInfo.InvariantCulture));
                                                datos.DetalleServicio.Add(detalle);
                                            }
                                            datos.SubTotal = string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalVenta, CultureInfo.InvariantCulture));
                                            datos.Descuento = notaCreditoElectronica.ResumenFactura.TotalDescuentosSpecified ? string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalDescuentos, CultureInfo.InvariantCulture)) : "0.00000";
                                            datos.Impuesto = notaCreditoElectronica.ResumenFactura.TotalImpuestoSpecified ? string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalImpuesto, CultureInfo.InvariantCulture)) : "0.00000";
                                            datos.TotalGeneral = string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalComprobante, CultureInfo.InvariantCulture));
                                            datos.CodigoMoneda = notaCreditoElectronica.ResumenFactura.CodigoMonedaSpecified ? notaCreditoElectronica.ResumenFactura.CodigoMoneda.ToString() : "";
                                            datos.TipoDeCambio = notaCreditoElectronica.ResumenFactura.CodigoMonedaSpecified ? notaCreditoElectronica.ResumenFactura.TipoCambio.ToString() : "";
                                        }
                                        byte[] pdfAttactment = Utilitario.GenerarPDFFacturaElectronica(datos);
                                        JObject jobDatosAdjuntos1 = new JObject();
                                        jobDatosAdjuntos1["nombre"] = documentoElectronico.ClaveNumerica + ".pdf";
                                        jobDatosAdjuntos1["contenido"] = Convert.ToBase64String(pdfAttactment);
                                        jarrayObj.Add(jobDatosAdjuntos1);
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
                                else
                                {
                                    if ((strEstado == "aceptado" || strEstado == "rechazado") && documentoElectronico.CorreoNotificacion != "")
                                    {
                                        strBody = "Adjunto XML con estado " + strEstado + " del documento electrónico con clave " + mensaje.Clave + " y la respuesta del Ministerio de Hacienda.";
                                        JObject jobDatosAdjuntos1 = new JObject();
                                        jobDatosAdjuntos1["nombre"] = documentoElectronico.ClaveNumerica + ".xml";
                                        jobDatosAdjuntos1["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento);
                                        jarrayObj.Add(jobDatosAdjuntos1);
                                        JObject jobDatosAdjuntos2 = new JObject();
                                        jobDatosAdjuntos2["nombre"] = "RespuestaHacienda.xml";
                                        jobDatosAdjuntos2["contenido"] = Convert.ToBase64String(bytRespuestaXML);
                                        jarrayObj.Add(jobDatosAdjuntos2);
                                        servicioEnvioCorreo.SendEmail(new string[] { documentoElectronico.CorreoNotificacion }, new string[] { }, "Documento electrónico con clave " + mensaje.Clave, strBody, false, jarrayObj);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                JArray emptyJArray = new JArray();
                                string strBody = "El documento con clave " + mensaje.Clave + " genero un error en el envío del PDF al receptor:" + ex.Message;
                                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al tratar de enviar el correo al receptor.", strBody, false, emptyJArray);
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
    }
}
