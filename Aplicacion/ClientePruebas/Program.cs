using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.AccesoDatos.ClienteWCF;
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

        static void Main(string[] args)
        {
            try
            {
                int intIdEmpresa = 0;
                while (intIdEmpresa == 0)
                {
                    List<Empresa> empresaLista = PuntoventaWCF.ObtenerListaEmpresas().Result;
                    foreach (Empresa empresa in empresaLista)
                    {
                        Console.WriteLine("Id: " + empresa.IdEmpresa + " Nombre: " + empresa.NombreComercial);
                    }
                    Console.WriteLine("Ingrese el Id de la empresa a consultar:");
                    string strIdEmpresa = Console.ReadLine();
                    try
                    {
                        intIdEmpresa = int.Parse(strIdEmpresa);
                    }
                    catch (Exception)
                    {
                        intIdEmpresa = 0;
                    }
                };
                string input = "C";
                while (input != "S")
                {
                    try
                    {
                        Console.WriteLine("Digite 'C' para consultar un comprobante o 'S' para salir:");
                        input = Console.ReadLine();
                        string operacion = input.Substring(0, 1);
                        if (operacion == "C")
                        {
                            List<DocumentoElectronico> documentoLista = PuntoventaWCF.ObtenerListaDocumentosElectronicosEnProceso(intIdEmpresa).Result;
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
                                            PuntoventaWCF.EnviarNotificacion(documento.IdDocumento).Wait();
                                        }
                                    }
                                    if (documento.EstadoEnvio == "rechazado")
                                    {
                                        Console.WriteLine("El documento fue RECHAZADO y posee la siguiente respuesta de hacienda:");
                                        DocumentoElectronico consulta = PuntoventaWCF.ObtenerDocumentoElectronico(documento.IdDocumento).Result;
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
                                            DocumentoElectronico consulta = PuntoventaWCF.ObtenerRespuestaDocumentoElectronicoEnviado(documento.IdDocumento).Result;
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
                                                            PuntoventaWCF.ProcesarRespuesta(respuesta);
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
                                            PuntoventaWCF.EnviarDocumentoElectronicoPendiente(documento.IdDocumento).Wait();
                                            Console.WriteLine("Procesado satisfactoriamente. . .");
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(ex.InnerException.Message);
                        Console.WriteLine("");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine("");
            }
        }
    }
}
