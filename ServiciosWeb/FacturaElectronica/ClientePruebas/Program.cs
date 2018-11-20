using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace LeandroSoftware.FacturaElectronicaHacienda.ClientePruebas
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            string input = "C";
            try
            {
                while (input == "C")
                {
                    Console.WriteLine("Digite 'C' para consultar un comprobante o cualquier otra tecla para salir:");
                    input = Console.ReadLine();
                    string operacion = input.Substring(0, 1);
                    if (operacion == "C")
                    {
                        try
                        {
                            List<DatosDocumentoElectronicoDTO> documentoLista = obtenerListadoDocumentos(int.Parse(appSettings["Empresa"]));
                            foreach (DatosDocumentoElectronicoDTO doc in documentoLista)
                            {
                                Console.WriteLine("id: " + doc.IdDocumento + " Clave: " + doc.ClaveNumerica + " Estado: " + doc.EstadoEnvio);
                            }
                            Console.WriteLine("Ingrese el Id del documento a procesar o 'S' para abortar la consulta:");
                            string idDoc = Console.ReadLine();
                            if (idDoc != "S")
                            {
                                int intIdDocumento = int.Parse(idDoc);
                                DatosDocumentoElectronicoDTO documento = documentoLista.Where(x => x.IdDocumento == intIdDocumento).FirstOrDefault();
                                if (documento != null)
                                {
                                    Console.WriteLine("Ingrese 'C' para consultar el estado, 'E' para enviar o cualquier otra tecla para salir.");
                                    string strOpcion = Console.ReadLine();
                                    if (strOpcion == "C")
                                    {
                                        DatosDocumentoElectronicoDTO consulta = consultarEstadoDocumento(documento);
                                        Console.WriteLine("El documento posee un estado: " + consulta.EstadoEnvio);
                                        if (consulta.RespuestaHacienda != null)
                                        {
                                            if (documento.EstadoEnvio != "pendiente" && documento.EstadoEnvio != "erroralenviar")
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
                                    if (strOpcion == "E")
                                    {
                                        if (documento.EstadoEnvio == "pendiente" || documento.EstadoEnvio == "erroralenviar")
                                        {
                                            enviarDocumentoElectronico(documento);
                                        }
                                        else
                                        {
                                            Console.WriteLine("El documento no posee un estado de 'Pendiente' o 'ErrorEnviando' por lo que no se puede procesar.");
                                            Console.WriteLine("");
                                        }
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

        private static List<DatosDocumentoElectronicoDTO> obtenerListadoDocumentos(int intIdEmpresa)
        {
            Uri uri = new Uri(appSettings["ServicioHaciendaURL"] + "/consultarlistadodocumentos?empresa=" + intIdEmpresa + "&estado=");
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
                List<DatosDocumentoElectronicoDTO> listado = json_serializer.Deserialize<List<DatosDocumentoElectronicoDTO>>(results);
                return listado;
            }
            catch (AggregateException ex)
            {
                log.Error("Error consultado la lista de documentos: ", ex.Flatten());
                throw ex.Flatten();
            }
        }

        private static DatosDocumentoElectronicoDTO consultarEstadoDocumento(DatosDocumentoElectronicoDTO datos)
        {
            Uri uri = new Uri(appSettings["ServicioHaciendaURL"] + "/consultardocumentoelectronico?empresa=" + datos.IdEmpresa + "&clave=" + datos.ClaveNumerica + "&consecutivo=" + datos.Consecutivo);
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
                DatosDocumentoElectronicoDTO respuesta = json_serializer.Deserialize<DatosDocumentoElectronicoDTO>(results);
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
            Uri uri = new Uri(appSettings["ServicioHaciendaURL"] + "/recibirrespuestahacienda");
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

        private static void enviarDocumentoElectronico(DatosDocumentoElectronicoDTO datos)
        {
            string jsonRequest = new JavaScriptSerializer().Serialize(datos);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(appSettings["ServicioHaciendaURL"] + "/enviardocumentoelectronico");
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
    }
}
