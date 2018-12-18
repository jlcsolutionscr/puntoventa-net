using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core;
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
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        static void Main(string[] args)
        {
            try
            {
                int idEmpresa = 0;
                while (idEmpresa == 0)
                {
                    List<Empresa> empresaLista = ObtenerListadoEmpresas();
                    foreach (Empresa empresa in empresaLista)
                    {
                        Console.WriteLine("Id: " + empresa.IdEmpresa + " Nombre: " + empresa.NombreComercial);
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
                            List<DocumentoElectronico> documentoLista = ObtenerListaDocumentosElectronicosEnProceso(idEmpresa);
                            foreach (DocumentoElectronico doc in documentoLista)
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
                                DocumentoElectronico documento = documentoLista.Where(x => x.IdDocumento == idDocumento).FirstOrDefault();
                                if (documento != null)
                                {
                                    if (documento.EstadoEnvio == "aceptado")
                                    {
                                        Console.WriteLine("El documento fue ACEPTADO. Desea reenviar la notificacion al cliente (S/N)");
                                        string strSiNo = Console.ReadLine();
                                        if (strSiNo == "S")
                                        {
                                            EnviarNotificacion(documento.IdDocumento);
                                        }
                                    }
                                    if (documento.EstadoEnvio == "rechazado")
                                    {
                                        Console.WriteLine("El documento fue RECHAZADO y posee la siguiente respuesta de hacienda:");
                                        DocumentoElectronico consulta = ObtenerDocumentoElectronico(documento.IdDocumento);
                                        XmlSerializer serializer = new XmlSerializer(typeof(MensajeHacienda));
                                        MensajeHacienda mensajeRespuesta;
                                        using (MemoryStream ms = new MemoryStream(consulta.Respuesta))
                                        {
                                            mensajeRespuesta = (MensajeHacienda)serializer.Deserialize(ms);
                                        }
                                        Console.WriteLine(mensajeRespuesta.DetalleMensaje);
                                    }
                                    if (documento.EstadoEnvio == "enviado")
                                    {
                                        Console.WriteLine("El documento ya fue enviado. Desea realizar la consulta del estado en Hacienda? (S/N)");
                                        string strOpcion = Console.ReadLine();
                                        if (strOpcion == "S")
                                        {
                                            DocumentoElectronico consulta = ObtenerRespuestaDocumentoElectronicoEnviado(documento.IdDocumento);
                                            Console.WriteLine("El documento posee un estado: " + consulta.EstadoEnvio);
                                            if (consulta.Respuesta != null)
                                            {
                                                if (consulta.EstadoEnvio == "aceptado" || consulta.EstadoEnvio == "rechazado")
                                                {
                                                    XmlDocument xmlRespuesta = new XmlDocument();
                                                    xmlRespuesta.LoadXml(Encoding.UTF8.GetString(consulta.Respuesta));
                                                    Console.WriteLine("Respuesta de hacienda: " + xmlRespuesta.GetElementsByTagName("DetalleMensaje").Item(0).InnerText);
                                                    Console.WriteLine("");
                                                    Console.WriteLine("Desea proceder con la aplicación de la respuesta de Hacienda (S/N):");
                                                    string strSiNo = Console.ReadLine();
                                                    if (strSiNo == "S")
                                                    {
                                                        RespuestaHaciendaDTO respuesta = new RespuestaHaciendaDTO();
                                                        if (documento.EsMensajeReceptor == "S")
                                                            respuesta.Clave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                                                        else
                                                            respuesta.Clave = documento.ClaveNumerica;
                                                        respuesta.Fecha = documento.Fecha.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
                                                        respuesta.IndEstado = consulta.EstadoEnvio;
                                                        respuesta.RespuestaXml = Convert.ToBase64String(consulta.Respuesta);
                                                        try
                                                        {
                                                            ProcesarRespuesta(respuesta);
                                                            Console.WriteLine("Procesado satisfactoriamente. . .");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("Error en el procesamiento de la respuesta de Hacienda: " + ex.Message);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Procesamiento abortado por el usuario. . .");
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
                                        {
                                            try
                                            {
                                                EnviarDocumentoElectronico(documento.IdDocumento);
                                                Console.WriteLine("Procesado satisfactoriamente. . .");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error en el procesamiento de la respuesta de Hacienda: " + ex.Message);
                                            }
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

        private static List<Empresa> ObtenerListadoEmpresas()
        {
            List<Empresa> listado = new List<Empresa>();
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaEmpresas",
                    DatosPeticion = ""
                };
                string strPeticion = serializer.Serialize(peticion);
                string strRespuesta = Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Result;
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                if (strRespuesta != "")
                    listado = serializer.Deserialize<List<Empresa>>(strRespuesta);
                return listado;
            }
            catch (Exception ex)
            {
                log.Error("Error consultado la lista de empresas: ", ex);
                throw ex;
            }
        }

        private static List<DocumentoElectronico> ObtenerListaDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaDocumentosElectronicosPendientes",
                    DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
                };
                string strPeticion = serializer.Serialize(peticion);
                string strRespuesta = Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Result;
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                if (strRespuesta != "")
                {
                    List<DocumentoElectronico> listadoPendientes = serializer.Deserialize<List<DocumentoElectronico>>(strRespuesta);
                    foreach (DocumentoElectronico doc in listadoPendientes)
                        listado.Add(doc);
                }
                peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerListaDocumentosElectronicosEnviados",
                    DatosPeticion = "{IdEmpresa: " + intIdEmpresa + "}"
                };
                strPeticion = serializer.Serialize(peticion);
                strRespuesta = Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Result;
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                if (strRespuesta != "")
                {
                    List<DocumentoElectronico> listadoEnviados = serializer.Deserialize<List<DocumentoElectronico>>(strRespuesta);
                    foreach (DocumentoElectronico doc in listadoEnviados)
                        listado.Add(doc);
                }
                return listado;
            }
            catch (Exception ex)
            {
                log.Error("Error consultado la lista de documentos: ", ex);
                throw ex;
            }
        }

        private static void EnviarDocumentoElectronico(int intIdDocumento)
        {
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "EnviarDocumentoElectronicoPendiente",
                    DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
                };
                string strPeticion = serializer.Serialize(peticion);
                Utilitario.Ejecutar(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Wait();
            }
            catch (Exception ex)
            {
                log.Error("Error enviando el documento con ID: " + intIdDocumento, ex);
                throw ex;
            }
        }

        private static DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento)
        {
            DocumentoElectronico documento = null;
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerDocumentoElectronico",
                    DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
                };
                string strPeticion = serializer.Serialize(peticion);
                string strRespuesta = Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Result;
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                if (strRespuesta != "")
                    documento = serializer.Deserialize<DocumentoElectronico>(strRespuesta);
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error consultado el documento con ID: " + intIdDocumento, ex);
                throw ex;
            }
        }

        private static DocumentoElectronico ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento)
        {
            DocumentoElectronico documento = null;
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "ObtenerRespuestaDocumentoElectronicoEnviado",
                    DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
                };
                string strPeticion = serializer.Serialize(peticion);
                string strRespuesta = Utilitario.EjecutarConsulta(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Result;
                strRespuesta = serializer.Deserialize<string>(strRespuesta);
                documento = new JavaScriptSerializer().Deserialize<DocumentoElectronico>(strRespuesta);
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error obteniendo respuesta de Hacienda para el documento con ID: " + intIdDocumento, ex);
                throw ex;
            }
        }

        private static void ProcesarRespuesta(RespuestaHaciendaDTO respuesta)
        {
            string jsonRequest = "{\"clave\": \"" + respuesta.Clave + "\"," +
                "\"fecha\": \"" + respuesta.Fecha + "\"," +
                "\"ind-estado\": \"" + respuesta.IndEstado + "\"," +
                "\"respuesta-xml\": \"" + respuesta.RespuestaXml + "\"}";

            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Uri uri = new Uri(appSettings["ServicioPuntoventaURL"] + "/recibirrespuestahacienda");
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

        private static void EnviarNotificacion(int intIdDocumento)
        {
            try
            {
                RequestDTO peticion = new RequestDTO
                {
                    NombreMetodo = "EnviarNotificacionDocumentoElectronico",
                    DatosPeticion = "{IdDocumento: " + intIdDocumento + "}"
                };
                string strPeticion = serializer.Serialize(peticion);
                Utilitario.Ejecutar(strPeticion, appSettings["ServicioPuntoventaURL"].ToString(), "").Wait();
            }
            catch (Exception ex)
            {
                log.Error("Error enviando la notificación para el documento con ID: " + intIdDocumento, ex);
                throw ex;
            }
        }
    }
}
