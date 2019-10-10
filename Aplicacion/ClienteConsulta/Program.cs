using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.Core.ClienteWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LeandroSoftware.Core.CommonTypes;
using System.Net.Http;
using System.Net;
using System.Configuration;

namespace LeandroSoftware.ServicioWeb.ClientePruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputEmpresa = "C";
            List<LlaveDescripcion> empresaLista = null;
            try
            {
                empresaLista = ClienteFEWCF.ObtenerListadoEmpresasAdministrador().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
                else
                    Console.WriteLine(ex.Message);
                Console.WriteLine("");
                inputEmpresa = "S";
            }
            while (inputEmpresa != "S")
            {
                int intIdEmpresa = 0;
                foreach (var empresa in empresaLista)
                {
                    Console.WriteLine("Id: " + empresa.Id + " Nombre: " + empresa.Descripcion);
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
                    string operacion = "P";
                    while (operacion != "S")
                    {
                        Console.WriteLine("Digite 'P' para consultar pendientes, 'C' para procesados o 'S' para salir:");
                        operacion = Console.ReadLine();
                        if (operacion == "P")
                        {
                            List<DocumentoDetalle> documentoLista = null;
                            try
                            {
                                documentoLista = ClienteFEWCF.ObtenerListadoDocumentosElectronicosEnProceso(intIdEmpresa).Result;
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException != null)
                                    Console.WriteLine(ex.InnerException.Message);
                                else
                                    Console.WriteLine(ex.Message);
                                Console.WriteLine("");
                            }
                            if (documentoLista != null && documentoLista.Count > 0)
                            {
                                foreach (DocumentoDetalle doc in documentoLista)
                                {
                                    Console.WriteLine("id: " + doc.IdDocumento + " Clave: " + doc.ClaveNumerica + " Estado: " + doc.EstadoEnvio);
                                }
                                Console.WriteLine("Desea procesar los documentos pendientes (S/N):");
                                string idDoc = Console.ReadLine();
                                if (idDoc == "S")
                                {
                                    try
                                    {
                                        HttpClient httpClient = new HttpClient();
                                        string servicioURL = ConfigurationManager.AppSettings["ServicioRecepcionURL"];
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                                        HttpResponseMessage httpResponse = httpClient.GetAsync(servicioURL + "/procesarpendientes").Result;
                                        if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                                            throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                                        if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                                            throw new Exception(httpResponse.ReasonPhrase);
                                    }
                                    catch (Exception ex)
                                    {
                                        if (ex.InnerException != null)
                                            Console.WriteLine(ex.InnerException.Message);
                                        else
                                            Console.WriteLine(ex.Message);
                                        Console.WriteLine("");
                                    }
                                }
                            }
                        }
                        else if (operacion == "C")
                        {
                            int intCantidad = 0;
                            List<DocumentoDetalle> documentoLista = null;
                            int intNumeroPagina = 1;
                            try
                            {
                                intCantidad = ClienteFEWCF.ObtenerTotalDocumentosElectronicosProcesados(intIdEmpresa).Result;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error al consultar la cantidad de documentos procesados:");
                                if (ex.InnerException != null)
                                    Console.WriteLine(ex.InnerException.Message);
                                else
                                    Console.WriteLine(ex.Message);
                                Console.WriteLine("");
                            }
                            int intCantidadRestante = intCantidad;
                            while (intCantidadRestante > 0)
                            {
                                try
                                {
                                    int intCantidadPorPagina = intCantidadRestante >= 10 ? 10 : intCantidadRestante;
                                    documentoLista = ClienteFEWCF.ObtenerListadoDocumentosElectronicosProcesados(intIdEmpresa, intNumeroPagina, intCantidadPorPagina).Result;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error al consultar el listado de documentos electrónicos procesados:");
                                    if (ex.InnerException != null)
                                        Console.WriteLine(ex.InnerException.Message);
                                    else
                                        Console.WriteLine(ex.Message);
                                    Console.WriteLine("");
                                }
                                foreach (DocumentoDetalle doc in documentoLista)
                                {
                                    Console.WriteLine("id: " + doc.IdDocumento + " Clave: " + doc.ClaveNumerica + " Estado: " + doc.EstadoEnvio);
                                }
                                intCantidadRestante -= intCantidadRestante >= 10 ? 10 : intCantidadRestante;
                                intNumeroPagina += 1;
                                if (intCantidadRestante > 0)
                                {
                                    Console.WriteLine("Digite cualquier tecla consultar los siguientes documentos o 'S' para continuar");
                                    string continuar = Console.ReadLine();
                                    if (continuar == "S")
                                    {
                                        intCantidadRestante = 0;
                                    }
                                }
                            }
                            if (intCantidad > 0)
                            {
                                Console.WriteLine("Ingrese el Id del documento a consultar o 'S' para abortar la consulta:");
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
                                    DocumentoDetalle documento = documentoLista.Where(x => x.IdDocumento == idDocumento).FirstOrDefault();
                                    if (documento != null)
                                    {
                                        try
                                        {
                                            Console.WriteLine("El documento fue " + documento.EstadoEnvio + " posee la siguiente respuesta de hacienda:");
                                            Console.WriteLine("");
                                            try
                                            {
                                                DocumentoElectronico consulta = ClienteFEWCF.ObtenerDocumentoElectronico(documento.IdDocumento).Result;
                                                XmlDocument xmlRespuesta = new XmlDocument();
                                                xmlRespuesta.LoadXml(Encoding.UTF8.GetString(consulta.Respuesta));
                                                Console.WriteLine(xmlRespuesta.GetElementsByTagName("DetalleMensaje").Item(0).InnerText);
                                                Console.WriteLine("");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Error al consultar el detalle del documento electrónico:");
                                                if (ex.InnerException != null)
                                                    Console.WriteLine(ex.InnerException.Message);
                                                else
                                                    Console.WriteLine(ex.Message);
                                                Console.WriteLine("");
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            idDoc = "S";
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
            }
        }
    }
}
