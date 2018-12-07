using LeandroSoftware.AccesoDatos.TiposDatos;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace LeandroSoftware.AccesoDatos.ClientePruebas
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            try
            {
                int idEmpresa = 0;
                while (idEmpresa == 0)
                {
                    List<EmpresaDTO> empresaLista = obtenerListadoEmpresas();
                    foreach (EmpresaDTO empresa in empresaLista)
                    {
                        Console.WriteLine("id: " + empresa.IdEmpresa + " Clave: " + empresa.NombreEmpresa);
                    }
                    Console.WriteLine("Ingrese el Id de la empresa a consultar:");
                    string strIdEmpresa = Console.ReadLine();
                    try
                    {
                        idEmpresa = int.Parse(strIdEmpresa);
                    }
                    catch (Exception)
                    {
                        idEmpresa = 0;
                    }
                };
                string input = "C";
                while (input != "S")
                {
                    Console.WriteLine("Digite 'C' para consultar un comprobante o 'S' para salir:");
                    input = Console.ReadLine();
                    string operacion = input.Substring(0, 1);
                    if (operacion == "C")
                    {
                        try
                        {
                            List<DocumentoElectronicoDTO> documentoLista = obtenerListadoDocumentos(idEmpresa);
                            foreach (DocumentoElectronicoDTO doc in documentoLista)
                            {
                                Console.WriteLine("id: " + doc.IdDocumento + " Clave: " + doc.ClaveNumerica + " Estado: " + doc.EstadoEnvio);
                            }
                            Console.WriteLine("Ingrese el Id del documento a procesar o 'S' para abortar la consulta:");
                            string idDoc = Console.ReadLine();
                            int idDocumento = 0;
                            try
                            {
                                idDocumento = int.Parse(idDoc);
                            }
                            catch (Exception)
                            {
                                idDoc = "S";
                            }
                            if (idDoc != "S")
                            {
                                DocumentoElectronicoDTO documento = documentoLista.Where(x => x.IdDocumento == idDocumento).FirstOrDefault();
                                if (documento != null)
                                {
                                    if (documento.EstadoEnvio == "aceptado")
                                    {
                                        Console.WriteLine("El documento fue ACEPTADO. Desea reenviar la notificacion al cliente (S/N)");
                                        string strSiNo = Console.ReadLine();
                                        if (strSiNo == "S")
                                        {
                                            enviarNotificacion(documento);
                                        }
                                    }
                                    if (documento.EstadoEnvio == "rechazado")
                                    {
                                        Console.WriteLine("El documento fue RECHAZADO y posee la siguiente respuesta de hacienda:");
                                        if (documento.RespuestaHacienda == null)
                                        {
                                            DocumentoElectronicoDTO consulta = consultarEstadoDocumento(documento);
                                            byte[] bytRespuesta = Convert.FromBase64String(consulta.RespuestaHacienda);
                                            XmlSerializer serializer = new XmlSerializer(typeof(MensajeHacienda));
                                            MensajeHacienda mensajeRespuesta;
                                            using (MemoryStream ms = new MemoryStream(bytRespuesta))
                                            {
                                                mensajeRespuesta = (MensajeHacienda)serializer.Deserialize(ms);
                                            }
                                            Console.WriteLine(mensajeRespuesta.DetalleMensaje);
                                        }
                                        else
                                            Console.WriteLine(documento.RespuestaHacienda);
                                    }
                                    if (documento.EstadoEnvio == "enviado")
                                    {
                                        Console.WriteLine("El documento ya fue enviado. Desea realizar la consulta del estado en Hacienda? (S/N)");
                                        string strOpcion = Console.ReadLine();
                                        if (strOpcion == "S")
                                        {
                                            DocumentoElectronicoDTO consulta = consultarEstadoDocumento(documento);
                                            Console.WriteLine("El documento posee un estado: " + consulta.EstadoEnvio);
                                            if (consulta.RespuestaHacienda != null)
                                            {
                                                if (documento.EstadoEnvio == "enviado")
                                                {
                                                    if (consulta.EstadoEnvio == "aceptado" || consulta.EstadoEnvio == "rechazado")
                                                    {
                                                        XmlDocument xmlRespuesta = new XmlDocument();
                                                        byte[] bytRespuesta = Convert.FromBase64String(consulta.RespuestaHacienda);
                                                        xmlRespuesta.LoadXml(Encoding.UTF8.GetString(bytRespuesta));
                                                        Console.WriteLine("Respuesta de hacienda: " + xmlRespuesta.GetElementsByTagName("DetalleMensaje").Item(0).InnerText);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Respuesta de hacienda: " + consulta.RespuestaHacienda);
                                                    }
                                                    Console.WriteLine("");
                                                    if (documento.EstadoEnvio != consulta.EstadoEnvio)
                                                    {
                                                        Console.WriteLine("Desea proceder con la aplicación de la respuesta de Hacienda (S/N):");
                                                        string strSiNo = Console.ReadLine();
                                                        if (strSiNo == "S")
                                                        {
                                                            RespuestaHaciendaDTO respuesta = new RespuestaHaciendaDTO();
                                                            if (documento.EsMensajeReceptor == "S")
                                                                respuesta.Clave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                                                            else
                                                                respuesta.Clave = documento.ClaveNumerica;
                                                            respuesta.Fecha = documento.FechaEmision.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
                                                            respuesta.IndEstado = consulta.EstadoEnvio;
                                                            respuesta.RespuestaXml = consulta.RespuestaHacienda;
                                                            procesarRespuesta(respuesta);
                                                            Console.WriteLine("Procesado satisfactoriamente. . .");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Procesamiento abortado por el usuario. . .");
                                                        }
                                                    }
                                                }
                                            }
                                            Console.WriteLine("");
                                        }
                                    }
                                    if (documento.EstadoEnvio == "registrado")
                                    {
                                        Console.WriteLine("El documento se encuentra pendiente de enviar a Hacienda. Desea realizar el envío del documento a Hacienda? (S/N)");
                                        string strOpcion = Console.ReadLine();
                                        if (strOpcion == "S")
                                            enviarDocumentoElectronico(documento);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("El identificador del documento ingresado no existe. Verifique la información ingresada. . .");
                                    Console.WriteLine("");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static List<EmpresaDTO> obtenerListadoEmpresas()
        {
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/consultarlistadoempresas");
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + task1.Result.ReasonPhrase);
                }
                string results = task1.Result.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                List<EmpresaDTO> listado = json_serializer.Deserialize<List<EmpresaDTO>>(results);
                return listado;
            }
            catch (AggregateException ex)
            {
                log.Error("Error consultado la lista de empresas: ", ex.Flatten());
                throw ex.Flatten();
            }
        }

        private static List<DocumentoElectronicoDTO> obtenerListadoDocumentos(int intIdEmpresa)
        {
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/consultarlistadodocumentos?empresa=" + intIdEmpresa + "&estado=");
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + task1.Result.ReasonPhrase);
                }
                string results = task1.Result.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                List<DocumentoElectronicoDTO> listado = json_serializer.Deserialize<List<DocumentoElectronicoDTO>>(results);
                return listado;
            }
            catch (AggregateException ex)
            {
                log.Error("Error consultado la lista de documentos: ", ex.Flatten());
                throw ex.Flatten();
            }
        }

        private static DocumentoElectronicoDTO consultarEstadoDocumento(DocumentoElectronicoDTO datos)
        {
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/consultardocumentoelectronico?empresa=" + datos.IdEmpresa + "&clave=" + datos.ClaveNumerica + "&consecutivo=" + datos.Consecutivo);
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + task1.Result.ReasonPhrase);
                }
                string results = task1.Result.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                DocumentoElectronicoDTO respuesta = json_serializer.Deserialize<DocumentoElectronicoDTO>(results);
                return respuesta;
            }
            catch (AggregateException ex)
            {
                log.Error("Error consultado el documento con clave: " + datos.ClaveNumerica, ex.Flatten());
                throw ex.Flatten();
            }
        }

        private static void procesarRespuesta(RespuestaHaciendaDTO respuesta)
        {
            string jsonRequest = "{\"clave\": \"" + respuesta.Clave + "\"," +
                "\"fecha\": \"" + respuesta.Fecha + "\"," +
                "\"ind-estado\": \"" + respuesta.IndEstado + "\"," +
                "\"respuesta-xml\": \"" + respuesta.RespuestaXml + "\"}";

            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/recibirrespuestahacienda");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    string strErrorMessage = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + strErrorMessage);
                }
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten();
            }
        }

        private static void enviarDocumentoElectronico(DocumentoElectronicoDTO datos)
        {
            string jsonRequest = new JavaScriptSerializer().Serialize(datos);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/enviardocumentoelectronico");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    string strErrorMessage = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + strErrorMessage);
                }
            }
            catch (AggregateException ex)
            {
                log.Error("Error enviando el documento con clave: " + datos.ClaveNumerica, ex.Flatten());
                throw ex.Flatten();
            }
        }

        private static void enviarNotificacion(DocumentoElectronicoDTO datos)
        {
            Uri uri = new Uri(appSettings["ServicioFacturaElectronicaURL"] + "/enviarnotificacion?empresa=" + datos.IdEmpresa + "&clave=" + datos.ClaveNumerica + "&consecutivo=" + datos.Consecutivo);
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    throw new Exception("Error al consumir el servicio web de factura electrónica: " + task1.Result.ReasonPhrase);
                }
            }
            catch (AggregateException ex)
            {
                log.Error("Error enviando la notificación para el documento con clave: " + datos.ClaveNumerica, ex.Flatten());
                throw ex.Flatten();
            }
        }
    }
}
