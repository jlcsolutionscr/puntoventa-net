using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.AccesoDatos.ClienteWCF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LeandroSoftware.AccesoDatos.ClientePruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Empresa> empresaLista = PuntoventaWCF.ObtenerListaEmpresas().Result;
                string inputEmpresa = "C";
                while (inputEmpresa != "S")
                {
                    int intIdEmpresa = 0;
                    foreach (Empresa empresa in empresaLista)
                    {
                        Console.WriteLine("Id: " + empresa.IdEmpresa + " Nombre: " + empresa.NombreComercial);
                    }
                    Console.WriteLine("Ingrese el Id de la empresa a consultar o 'S' para salir:");
                    inputEmpresa = Console.ReadLine();
                    try
                    {
                        intIdEmpresa = int.Parse(inputEmpresa);
                    }
                    catch (Exception)
                    {
                        intIdEmpresa = 0;
                    }
                    if (intIdEmpresa > 0)
                    {
                        try
                        {
                            string operacion = "P";
                            while (operacion != "S")
                            {
                                Console.WriteLine("Digite 'P' para consultar pendientes, 'C' para procesados o 'S' para salir:");
                                operacion = Console.ReadLine();
                                if (operacion == "P")
                                {
                                    List<DocumentoElectronico> documentoLista = null;
                                    try
                                    {
                                        documentoLista = PuntoventaWCF.ObtenerListaDocumentosElectronicosEnProceso(intIdEmpresa).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (ex.InnerException != null)
                                            Console.WriteLine(ex.InnerException.Message);
                                        else
                                            Console.WriteLine(ex.Message);
                                        Console.WriteLine("");
                                        Console.WriteLine("");
                                    }
                                    if (documentoLista != null && documentoLista.Count > 0)
                                    {
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
                                                if (documento.EstadoEnvio == "enviado")
                                                {
                                                    Console.WriteLine("El documento ya fue enviado. Desea realizar la consulta del estado en Hacienda? (S/N)");
                                                    string strOpcion = Console.ReadLine();
                                                    if (strOpcion == "S")
                                                    {
                                                        DocumentoElectronico consulta = null;
                                                        try
                                                        {
                                                            consulta = PuntoventaWCF.ObtenerRespuestaDocumentoElectronicoEnviado(documento.IdDocumento).Result;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("Error al consultar la respuesta del documento electrónico: " + ex.Message);
                                                            Console.WriteLine("");
                                                        }
                                                        Console.WriteLine("El documento posee un estado: " + consulta.EstadoEnvio);
                                                        if (consulta != null && consulta.Respuesta != null)
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
                                                                        Console.WriteLine("Respuesta procesada satisfactoriamente. . .");
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        Console.WriteLine("Error en el procesamiento de la respuesta de Hacienda: " + ex.Message);
                                                                        Console.WriteLine("");
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
                                                            PuntoventaWCF.EnviarDocumentoElectronicoPendiente(documento.IdDocumento).Wait();
                                                            Console.WriteLine("Envio procesado satisfactoriamente. . .");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("Error al enviar el documento electrónico a Hacienda: " + ex.Message);
                                                            Console.WriteLine("");
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
                                }
                                else if (operacion == "C")
                                {
                                    int intCantidad = 0;
                                    List<DocumentoElectronico> documentoLista = null;
                                    try
                                    {
                                        intCantidad = PuntoventaWCF.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa).Result;
                                        documentoLista = PuntoventaWCF.ObtenerListaDocumentosElectronicosProcesados(intIdEmpresa, 1, intCantidad).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        if (ex.InnerException != null)
                                            Console.WriteLine(ex.InnerException.Message);
                                        else
                                            Console.WriteLine(ex.Message);
                                        Console.WriteLine("");
                                    }
                                    if (intCantidad > 0)
                                    {
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
                                                        try
                                                        {
                                                            PuntoventaWCF.EnviarNotificacion(documento.IdDocumento).Wait();
                                                            Console.WriteLine("Notificación procesada satisfactoriamente. . .");
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Console.WriteLine("Error al enviar la notificación al receptor del documento: " + ex.Message);
                                                            Console.WriteLine("");
                                                        }
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
                                            }
                                            else
                                            {
                                                Console.WriteLine("El identificador del documento ingresado no existe. Verifique la información ingresada. . .");
                                                Console.WriteLine("");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("");
                            if (ex.InnerException != null)
                                Console.WriteLine(ex.InnerException.Message);
                            else
                                Console.WriteLine(ex.Message);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
                else
                    Console.WriteLine(ex.Message);
                Console.WriteLine("");
            }
        }
    }
}
