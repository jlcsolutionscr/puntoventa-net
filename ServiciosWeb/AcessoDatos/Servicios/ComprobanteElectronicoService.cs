using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using LeandroSoftware.Core.Dominio.Entidades;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using LeandroSoftware.AccesoDatos.TiposDatos;
using FirmaXadesNet.Signature;
using FirmaXadesNet;
using FirmaXadesNet.Signature.Parameters;
using System.Security.Cryptography.X509Certificates;
using FirmaXadesNet.Crypto;
using Unity;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Configuration;
using Unity.Lifetime;
using Unity.Injection;
using LeandroSoftware.Puntoventa.CommonTypes;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public static class ComprobanteElectronicoService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static HttpClient httpClient = new HttpClient();
        private static IUnityContainer unityContainer = new UnityContainer();

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

        public static decimal ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta, IUnityContainer unityContainer)
        {
            try
            {
                TipoDeCambioDolar tipoDeCambio = null;
                using (IDbContext dbContext = unityContainer.Resolve<IDbContext>())
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
                    } else
                    {
                        return tipoDeCambio.ValorTipoCambio;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el tipo de cambio de venta: ", ex);
                throw ex;
            }
        }

        public static DocumentoElectronico GeneraFacturaElectronica(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, decimal decTipoCambioDolar)
        {
            try
            {
                string strCorreoNotificacion = "";
                if (cliente.IdCliente > 1)
                {
                    if (cliente.CorreoElectronico == null || cliente.CorreoElectronico.Length == 0)
                    {
                        throw new Exception("El cliente seleccionado debe poseer una dirección de correo electrónico para ser notificado.");
                    }
                    else
                    {
                        strCorreoNotificacion = cliente.CorreoElectronico;
                    }
                }
                FacturaElectronica facturaElectronica = new FacturaElectronica
                {
                    Clave = "",
                    NumeroConsecutivo = "",
                    FechaEmision = factura.Fecha
                };
                FacturaElectronicaEmisorType emisor = new FacturaElectronicaEmisorType();
                FacturaElectronicaIdentificacionType identificacionEmisorType = new FacturaElectronicaIdentificacionType
                {
                    Tipo = (FacturaElectronicaIdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                    Numero = empresa.Identificacion
                };
                emisor.Identificacion = identificacionEmisorType;
                emisor.Nombre = empresa.NombreEmpresa;
                emisor.NombreComercial = empresa.NombreComercial;
                if (empresa.Telefono.Length > 0)
                {
                    FacturaElectronicaTelefonoType telefonoType = new FacturaElectronicaTelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = empresa.Telefono
                    };
                    emisor.Telefono = telefonoType;
                }
                emisor.CorreoElectronico = empresa.CorreoNotificacion;
                FacturaElectronicaUbicacionType ubicacionType = new FacturaElectronicaUbicacionType
                {
                    Provincia = empresa.IdProvincia.ToString(),
                    Canton = empresa.IdCanton.ToString("D2"),
                    Distrito = empresa.IdDistrito.ToString("D2"),
                    Barrio = empresa.IdBarrio.ToString("D2"),
                    OtrasSenas = empresa.Direccion
                };
                emisor.Ubicacion = ubicacionType;
                facturaElectronica.Emisor = emisor;
                if (factura.IdCliente > 1)
                {
                    FacturaElectronicaReceptorType receptor = new FacturaElectronicaReceptorType();
                    FacturaElectronicaIdentificacionType identificacionReceptorType = new FacturaElectronicaIdentificacionType
                    {
                        Tipo = (FacturaElectronicaIdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                        Numero = cliente.Identificacion
                    };
                    receptor.Identificacion = identificacionReceptorType;
                    receptor.Nombre = cliente.Nombre;
                    if (cliente.NombreComercial.Length > 0)
                        receptor.NombreComercial = cliente.NombreComercial;
                    if (cliente.Telefono.Length > 0)
                    {
                        FacturaElectronicaTelefonoType telefonoType = new FacturaElectronicaTelefonoType
                        {
                            CodigoPais = "506",
                            NumTelefono = cliente.Telefono
                        };
                        receptor.Telefono = telefonoType;
                    }
                    if (cliente.Fax.Length > 0)
                    {
                        FacturaElectronicaTelefonoType faxType = new FacturaElectronicaTelefonoType
                        {
                            CodigoPais = "506",
                            NumTelefono = cliente.Fax
                        };
                        receptor.Fax = faxType;
                    }
                    receptor.CorreoElectronico = cliente.CorreoElectronico;
                    ubicacionType = new FacturaElectronicaUbicacionType
                    {
                        Provincia = cliente.IdProvincia.ToString(),
                        Canton = cliente.IdCanton.ToString("D2"),
                        Distrito = cliente.IdDistrito.ToString("D2"),
                        Barrio = cliente.IdBarrio.ToString("D2"),
                        OtrasSenas = cliente.Direccion
                    };
                    receptor.Ubicacion = ubicacionType;
                    facturaElectronica.Receptor = receptor;
                }
                facturaElectronica.CondicionVenta = (FacturaElectronicaCondicionVenta)factura.IdCondicionVenta - 1;
                if (facturaElectronica.CondicionVenta == FacturaElectronicaCondicionVenta.Item02)
                {
                    facturaElectronica.PlazoCredito = factura.PlazoCredito.ToString();
                }
                List<FacturaElectronicaMedioPago> medioPagoList = new List<FacturaElectronicaMedioPago>();
                if (facturaElectronica.CondicionVenta != FacturaElectronicaCondicionVenta.Item01)
                {
                    FacturaElectronicaMedioPago medioPago = FacturaElectronicaMedioPago.Item99;
                    if (!medioPagoList.Contains(medioPago))
                    {
                        medioPagoList.Add(medioPago);
                    }
                }
                else
                {
                    foreach (DesglosePagoFactura desglose in factura.DesglosePagoFactura)
                    {
                        if (medioPagoList.Count() == 4)
                        {
                            throw new Exception("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                        }
                        FacturaElectronicaMedioPago medioPago = (FacturaElectronicaMedioPago)desglose.IdFormaPago - 1;
                        if (!medioPagoList.Contains(medioPago))
                        {
                            medioPagoList.Add(medioPago);
                        }
                    }
                }
                facturaElectronica.MedioPago = medioPagoList.ToArray();
                List<FacturaElectronicaLineaDetalle> detalleServicioList = new List<FacturaElectronicaLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
                {
                    FacturaElectronicaLineaDetalle lineaDetalle = new FacturaElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString()
                    };
                    FacturaElectronicaCodigoType codigoType = new FacturaElectronicaCodigoType
                    {
                        Codigo = detalleFactura.Producto.Codigo,
                        Tipo = FacturaElectronicaCodigoTypeTipo.Item01
                    };
                    lineaDetalle.Codigo = new FacturaElectronicaCodigoType[] { codigoType };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Unid;
                    else
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Sp;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    decimal montoTotalPorLinea = Math.Round(detalleFactura.Cantidad * detalleFactura.PrecioVenta, 5);
                    if (!detalleFactura.Excento)
                        decTotalMercanciasGrabadas += montoTotalPorLinea;
                    else
                        decTotalServiciosGrabados += montoTotalPorLinea;
                    lineaDetalle.PrecioUnitario = detalleFactura.PrecioVenta;
                    lineaDetalle.MontoTotal = montoTotalPorLinea;
                    decimal descuentoPorLinea = 0;
                    if (factura.Descuento > 0)
                    {
                        descuentoPorLinea = Math.Round(factura.Descuento / (factura.Total - factura.Impuesto) * montoTotalPorLinea, 5);
                        if (descuentoPorLinea > decTotalDescuentoPorFactura)
                            descuentoPorLinea = decTotalDescuentoPorFactura;
                        else
                            decTotalDescuentoPorFactura -= descuentoPorLinea;
                        lineaDetalle.MontoDescuento = descuentoPorLinea;
                        lineaDetalle.MontoDescuentoSpecified = true;
                        lineaDetalle.NaturalezaDescuento = "Descuento sobre mercancías";
                    }
                    lineaDetalle.SubTotal = montoTotalPorLinea - descuentoPorLinea;
                    decimal montoImpuestoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        FacturaElectronicaImpuestoType impuestoType = new FacturaElectronicaImpuestoType
                        {
                            Codigo = FacturaElectronicaImpuestoTypeCodigo.Item01,
                            Tarifa = detalleFactura.PorcentajeIVA
                        };
                        montoImpuestoPorLinea = Math.Round(lineaDetalle.SubTotal * detalleFactura.PorcentajeIVA / 100, 5);
                        impuestoType.Monto = montoImpuestoPorLinea;
                        decTotalImpuestos += montoImpuestoPorLinea;
                        lineaDetalle.Impuesto = new FacturaElectronicaImpuestoType[] { impuestoType };
                        
                    }
                    lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + montoImpuestoPorLinea;
                    detalleServicioList.Add(lineaDetalle);
                }
                facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
                FacturaElectronicaResumenFactura resumenFactura = new FacturaElectronicaResumenFactura();
                if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    resumenFactura.CodigoMoneda = FacturaElectronicaResumenFacturaCodigoMoneda.USD;
                    resumenFactura.CodigoMonedaSpecified = true;
                    resumenFactura.TipoCambio = decTipoCambioDolar;
                    resumenFactura.TipoCambioSpecified = true;
                }
                else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    resumenFactura.CodigoMoneda = FacturaElectronicaResumenFacturaCodigoMoneda.CRC;
                    resumenFactura.CodigoMonedaSpecified = true;
                }
                if (factura.Descuento > 0)
                {
                    resumenFactura.TotalDescuentos = factura.Descuento;
                    resumenFactura.TotalDescuentosSpecified = true;
                }
                resumenFactura.TotalMercanciasGravadas = decTotalMercanciasGrabadas;
                resumenFactura.TotalMercanciasGravadasSpecified = true;
                resumenFactura.TotalMercanciasExentas = decTotalMercanciasExcentas;
                resumenFactura.TotalMercanciasExentasSpecified = true;
                resumenFactura.TotalServGravados = decTotalServiciosGrabados;
                resumenFactura.TotalServGravadosSpecified = true;
                resumenFactura.TotalServExentos = decTotalServiciosExcentos;
                resumenFactura.TotalServExentosSpecified = true;
                resumenFactura.TotalGravado = decTotalMercanciasGrabadas + decTotalServiciosGrabados;
                resumenFactura.TotalGravadoSpecified = true;
                resumenFactura.TotalExento = decTotalMercanciasExcentas + decTotalServiciosExcentos;
                resumenFactura.TotalExentoSpecified = true;
                resumenFactura.TotalVenta = resumenFactura.TotalGravado + resumenFactura.TotalExento;
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - factura.Descuento;
                if (decTotalImpuestos > 0)
                {
                    resumenFactura.TotalImpuesto = decTotalImpuestos;
                    resumenFactura.TotalImpuestoSpecified = true;
                }
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + decTotalImpuestos;
                facturaElectronica.ResumenFactura = resumenFactura;
                FacturaElectronicaNormativa normativa = new FacturaElectronicaNormativa
                {
                    NumeroResolucion = "DGT-R-48-2016",
                    FechaResolucion = "07-10-2016 00:00:00"
                };
                facturaElectronica.Normativa = normativa;
                if (factura.TextoAdicional != "")
                {
                    FacturaElectronicaOtros otros = new FacturaElectronicaOtros();
                    FacturaElectronicaOtrosOtroTexto otrosTextos = new FacturaElectronicaOtrosOtroTexto();
                    otrosTextos.Value = factura.TextoAdicional;
                    otros.OtroTexto = new FacturaElectronicaOtrosOtroTexto[] { otrosTextos };
                    facturaElectronica.Otros = otros;
                }
                XmlDocument documentoXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                XmlSerializer serializer = new XmlSerializer(facturaElectronica.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, facturaElectronica);
                    msDatosXML.Position = 0;
                    documentoXml.Load(msDatosXML);
                }
                return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.FacturaElectronica, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DocumentoElectronico GenerarNotaDeCreditoElectronica(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, decimal decTipoCambioDolar)
        {
            try
            {
                string strCorreoNotificacion = "";
                if (cliente.IdCliente > 1)
                {
                    if (cliente.CorreoElectronico == null || cliente.CorreoElectronico.Length == 0)
                    {
                        throw new Exception("El cliente seleccionado debe poseer una dirección de correo electrónico para ser notificado.");
                    }
                    else
                    {
                        strCorreoNotificacion = cliente.CorreoElectronico;
                    }
                }
                NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
                {
                    Clave = "",
                    NumeroConsecutivo = "",
                    FechaEmision = DateTime.Now
                };
                NotaCreditoElectronicaEmisorType emisor = new NotaCreditoElectronicaEmisorType();
                NotaCreditoElectronicaIdentificacionType identificacionEmisorType = new NotaCreditoElectronicaIdentificacionType
                {
                    Tipo = (NotaCreditoElectronicaIdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                    Numero = empresa.Identificacion
                };
                emisor.Identificacion = identificacionEmisorType;
                emisor.Nombre = empresa.NombreEmpresa;
                emisor.NombreComercial = empresa.NombreComercial;
                if (empresa.Telefono.Length > 0)
                {
                    NotaCreditoElectronicaTelefonoType telefonoType = new NotaCreditoElectronicaTelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = empresa.Telefono
                    };
                    emisor.Telefono = telefonoType;
                }
                emisor.CorreoElectronico = empresa.CorreoNotificacion;
                NotaCreditoElectronicaUbicacionType ubicacionType = new NotaCreditoElectronicaUbicacionType
                {
                    Provincia = empresa.IdProvincia.ToString(),
                    Canton = empresa.IdCanton.ToString("D2"),
                    Distrito = empresa.IdDistrito.ToString("D2"),
                    Barrio = empresa.IdBarrio.ToString("D2"),
                    OtrasSenas = empresa.Direccion
                };
                emisor.Ubicacion = ubicacionType;
                notaCreditoElectronica.Emisor = emisor;
                if (factura.IdCliente > 1)
                {
                    NotaCreditoElectronicaReceptorType receptor = new NotaCreditoElectronicaReceptorType();
                    NotaCreditoElectronicaIdentificacionType identificacionReceptorType = new NotaCreditoElectronicaIdentificacionType
                    {
                        Tipo = (NotaCreditoElectronicaIdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                        Numero = cliente.Identificacion
                    };
                    receptor.Identificacion = identificacionReceptorType;
                    receptor.Nombre = cliente.Nombre;
                    if (cliente.NombreComercial.Length > 0)
                        receptor.NombreComercial = cliente.NombreComercial;
                    if (cliente.Telefono.Length > 0)
                    {
                        NotaCreditoElectronicaTelefonoType telefonoType = new NotaCreditoElectronicaTelefonoType
                        {
                            CodigoPais = "506",
                            NumTelefono = cliente.Telefono
                        };
                        receptor.Telefono = telefonoType;
                    }
                    if (cliente.Fax.Length > 0)
                    {
                        NotaCreditoElectronicaTelefonoType faxType = new NotaCreditoElectronicaTelefonoType
                        {
                            CodigoPais = "506",
                            NumTelefono = cliente.Fax
                        };
                        receptor.Fax = faxType;
                    }
                    receptor.CorreoElectronico = cliente.CorreoElectronico;
                    ubicacionType = new NotaCreditoElectronicaUbicacionType
                    {
                        Provincia = cliente.IdProvincia.ToString(),
                        Canton = cliente.IdCanton.ToString("D2"),
                        Distrito = cliente.IdDistrito.ToString("D2"),
                        Barrio = cliente.IdBarrio.ToString("D2"),
                        OtrasSenas = cliente.Direccion
                    };
                    receptor.Ubicacion = ubicacionType;
                    notaCreditoElectronica.Receptor = receptor;
                }
                notaCreditoElectronica.CondicionVenta = (NotaCreditoElectronicaCondicionVenta)factura.IdCondicionVenta - 1;
                if (notaCreditoElectronica.CondicionVenta == NotaCreditoElectronicaCondicionVenta.Item02)
                {
                    notaCreditoElectronica.PlazoCredito = factura.PlazoCredito.ToString();
                }
                List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
                {
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString()
                    };
                    NotaCreditoElectronicaCodigoType codigoType = new NotaCreditoElectronicaCodigoType
                    {
                        Codigo = detalleFactura.Producto.Codigo,
                        Tipo = NotaCreditoElectronicaCodigoTypeTipo.Item01
                    };
                    lineaDetalle.Codigo = new NotaCreditoElectronicaCodigoType[] { codigoType };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Unid;
                    else
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Sp;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    decimal montoTotalPorLinea = Math.Round(detalleFactura.Cantidad * detalleFactura.PrecioVenta, 5);
                    if (!detalleFactura.Excento)
                    {
                        decTotalMercanciasGrabadas += montoTotalPorLinea;
                    }
                    else
                    {
                        decTotalMercanciasExcentas += montoTotalPorLinea;
                    }
                    lineaDetalle.PrecioUnitario = detalleFactura.PrecioVenta;
                    lineaDetalle.MontoTotal = montoTotalPorLinea;
                    decimal descuentoPorLinea = 0;
                    if (factura.Descuento > 0)
                    {
                        descuentoPorLinea = Math.Round(factura.Descuento / (factura.Total - factura.Impuesto) * montoTotalPorLinea, 5);
                        if (descuentoPorLinea > decTotalDescuentoPorFactura)
                            descuentoPorLinea = decTotalDescuentoPorFactura;
                        else
                            decTotalDescuentoPorFactura -= descuentoPorLinea;
                        lineaDetalle.MontoDescuento = descuentoPorLinea;
                        lineaDetalle.MontoDescuentoSpecified = true;
                        lineaDetalle.NaturalezaDescuento = "Descuento sobre mercancías";
                    }
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal - descuentoPorLinea;
                    decimal montoImpuestoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        NotaCreditoElectronicaImpuestoType impuestoType = new NotaCreditoElectronicaImpuestoType
                        {
                            Codigo = NotaCreditoElectronicaImpuestoTypeCodigo.Item01,
                            Tarifa = detalleFactura.PorcentajeIVA
                        };
                        montoImpuestoPorLinea = Math.Round(lineaDetalle.SubTotal * detalleFactura.PorcentajeIVA / 100, 5);
                        impuestoType.Monto = montoImpuestoPorLinea;
                        decTotalImpuestos += montoImpuestoPorLinea;
                        lineaDetalle.Impuesto = new NotaCreditoElectronicaImpuestoType[] { impuestoType };
                    }
                    lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + montoImpuestoPorLinea;
                    detalleServicioList.Add(lineaDetalle);
                }
                notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
                NotaCreditoElectronicaResumenFactura resumenFactura = new NotaCreditoElectronicaResumenFactura();
                if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    resumenFactura.CodigoMoneda = NotaCreditoElectronicaResumenFacturaCodigoMoneda.USD;
                    resumenFactura.CodigoMonedaSpecified = true;
                    resumenFactura.TipoCambio = decTipoCambioDolar;
                    resumenFactura.TipoCambioSpecified = true;
                }
                else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    resumenFactura.CodigoMoneda = NotaCreditoElectronicaResumenFacturaCodigoMoneda.CRC;
                    resumenFactura.CodigoMonedaSpecified = true;
                }
                if (factura.Descuento > 0)
                {
                    resumenFactura.TotalDescuentos = factura.Descuento;
                    resumenFactura.TotalDescuentosSpecified = true;
                }
                resumenFactura.TotalMercanciasGravadas = decTotalMercanciasGrabadas;
                resumenFactura.TotalMercanciasGravadasSpecified = true;
                resumenFactura.TotalMercanciasExentas = decTotalMercanciasExcentas;
                resumenFactura.TotalMercanciasExentasSpecified = true;
                resumenFactura.TotalServGravados = decTotalServiciosGrabados;
                resumenFactura.TotalServGravadosSpecified = true;
                resumenFactura.TotalServExentos = decTotalServiciosExcentos;
                resumenFactura.TotalServExentosSpecified = true;
                resumenFactura.TotalGravado = decTotalMercanciasGrabadas + decTotalServiciosGrabados;
                resumenFactura.TotalGravadoSpecified = true;
                resumenFactura.TotalExento = decTotalMercanciasExcentas + decTotalServiciosExcentos;
                resumenFactura.TotalExentoSpecified = true;
                resumenFactura.TotalVenta = resumenFactura.TotalGravado + resumenFactura.TotalExento;
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - factura.Descuento;
                if (decTotalImpuestos > 0)
                {
                    resumenFactura.TotalImpuesto = decTotalImpuestos;
                    resumenFactura.TotalImpuestoSpecified = true;
                }
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + decTotalImpuestos;
                notaCreditoElectronica.ResumenFactura = resumenFactura;
                NotaCreditoElectronicaInformacionReferencia informacionReferencia = new NotaCreditoElectronicaInformacionReferencia
                {
                    TipoDoc = NotaCreditoElectronicaInformacionReferenciaTipoDoc.Item01,
                    Numero = factura.IdDocElectronico,
                    FechaEmision = factura.Fecha,
                    Codigo = NotaCreditoElectronicaInformacionReferenciaCodigo.Item01,
                    Razon = "Anulación del documento factura electronica con la respectiva clave númerica."
                };
                notaCreditoElectronica.InformacionReferencia = new NotaCreditoElectronicaInformacionReferencia[] { informacionReferencia };
                NotaCreditoElectronicaNormativa normativa = new NotaCreditoElectronicaNormativa
                {
                    NumeroResolucion = "DGT-R-48-2016",
                    FechaResolucion = "07-10-2016 00:00:00"
                };
                notaCreditoElectronica.Normativa = normativa;
                if (factura.TextoAdicional != "")
                {
                    NotaCreditoElectronicaOtros otros = new NotaCreditoElectronicaOtros();
                    NotaCreditoElectronicaOtrosOtroTexto otrosTextos = new NotaCreditoElectronicaOtrosOtroTexto();
                    otrosTextos.Value = factura.TextoAdicional;
                    otros.OtroTexto = new NotaCreditoElectronicaOtrosOtroTexto[] { otrosTextos };
                    notaCreditoElectronica.Otros = otros;
                }
                XmlDocument documentoXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                XmlSerializer serializer = new XmlSerializer(notaCreditoElectronica.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, notaCreditoElectronica);
                    msDatosXML.Position = 0;
                    documentoXml.Load(msDatosXML);
                }
                return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.NotaCreditoElectronica, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DocumentoElectronico GeneraMensajeReceptor(string datosXml, Empresa empresa, IDbContext dbContext, int intSucursal, int intTerminal, int intMensaje)
        {
            try
            {
                string strCorreoNotificacion = "";
                if (empresa.CorreoNotificacion == null || empresa.CorreoNotificacion.Length == 0)
                {
                    throw new Exception("La empresa debe poseer una dirección de correo electrónico para ser notificada.");
                }
                else
                {
                    strCorreoNotificacion = empresa.CorreoNotificacion;
                }
                XmlDocument documentoXml = new XmlDocument();
                documentoXml.LoadXml(datosXml);
                string strClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
                DocumentoElectronico documentoExistente = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClaveNumerica & x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                if (documentoExistente != null) throw new Exception("El documento electrónico con clave " + strClaveNumerica + " ya se encuentra registrado en el sistema. . .");
                MensajeReceptor mensajeReceptor = new MensajeReceptor
                {
                    Clave = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,
                    FechaEmisionDoc = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmision").Item(0).InnerText, CultureInfo.InvariantCulture),
                    Mensaje = (MensajeReceptorMensaje)intMensaje,
                    DetalleMensaje = "Mensaje de receptor con estado: " + (intMensaje == 0 ? "Aceptado" : intMensaje == 1 ? "Aceptado parcialmente" : "Rechazado"),
                    TotalFactura = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture),
                    NumeroConsecutivoReceptor = ""
                };
                if (documentoXml.GetElementsByTagName("Emisor") != null)
                {
                    XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0).ChildNodes.Item(1);
                    if (emisorNode.Name == "IdentificacionExtranjero")
                        mensajeReceptor.NumeroCedulaEmisor = emisorNode.InnerText;
                    else
                    {
                        string strNumeroCedulaEmisor = "";
                        foreach (XmlNode item in emisorNode.ChildNodes)
                        {
                            if (item.Name == "Numero")
                                strNumeroCedulaEmisor = item.InnerText;
                        }
                        if (strNumeroCedulaEmisor != "")
                            mensajeReceptor.NumeroCedulaEmisor = strNumeroCedulaEmisor;
                        else
                            throw new Exception("No se encuentra el número de identificacion del EMISOR en el archivo XML.");
                    }
                }
                else
                    throw new Exception("No se encuentra el nodo EMISOR en el archivo XML.");
                if (documentoXml.GetElementsByTagName("Receptor") != null)
                {
                    XmlNode receptorNode = documentoXml.GetElementsByTagName("Receptor").Item(0).ChildNodes.Item(1);
                    if (receptorNode.Name == "IdentificacionExtranjero")
                        mensajeReceptor.NumeroCedulaReceptor = receptorNode.InnerText;
                    else
                    {
                        string strNumeroCedulaReceptor = "";
                        foreach (XmlNode item in receptorNode.ChildNodes)
                        {
                            if (item.Name == "Numero")
                                strNumeroCedulaReceptor = item.InnerText;
                        }
                        if (strNumeroCedulaReceptor != "")
                            mensajeReceptor.NumeroCedulaReceptor = strNumeroCedulaReceptor;
                        else
                            throw new Exception("No se encuentra el número de identificacion del RECEPTOR en el archivo XML.");
                    }
                }
                else
                    throw new Exception("No se encuentra el nodo RECEPTOR en el archivo XML.");
                if (documentoXml.GetElementsByTagName("TotalImpuesto").Count > 0)
                {
                    mensajeReceptor.MontoTotalImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("TotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                    mensajeReceptor.MontoTotalImpuestoSpecified = true;
                }
                XmlDocument mensajeReceptorXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                XmlSerializer serializer = new XmlSerializer(mensajeReceptor.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, mensajeReceptor);
                    msDatosXML.Position = 0;
                    mensajeReceptorXml.Load(msDatosXML);
                }
                TipoDocumento tipoDoc = intMensaje == 0 ? TipoDocumento.MensajeReceptorAceptado : intMensaje == 1 ? TipoDocumento.MensajeReceptorAceptadoParcial : TipoDocumento.MensajeReceptorRechazado;
                DocumentoElectronico documento = RegistrarDocumentoElectronico(empresa, mensajeReceptorXml, documentoXml, dbContext, intSucursal, intTerminal, tipoDoc, strCorreoNotificacion);
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el MensajeReceptor: ", ex);
                throw ex;
            }
        }

        public static DocumentoElectronico RegistrarDocumentoElectronico(Empresa empresa, XmlDocument documentoXml, XmlDocument documentoOriXml, IDbContext dbContext, int intSucursal, int intTerminal, TipoDocumento tipoDocumento, string strCorreoNotificacion)
        {
            try
            {
                string strTipoIdentificacionEmisor = "";
                string strIdentificacionEmisor = "";
                string strTipoIdentificacionReceptor = "";
                string strIdentificacionReceptor = "";
                DateTime fechaEmision;
                string strConsucutivo = "";
                string strClaveNumerica = "";
                bool esMensajeReceptor = false;

                if (tipoDocumento == TipoDocumento.MensajeReceptorAceptado || tipoDocumento == TipoDocumento.MensajeReceptorAceptadoParcial || tipoDocumento == TipoDocumento.MensajeReceptorRechazado)
                    esMensajeReceptor = true;
                if (!esMensajeReceptor)
                {
                    fechaEmision = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmision").Item(0).InnerText, CultureInfo.InvariantCulture);
                    XmlNodeList codigoMonedaNode = documentoXml.GetElementsByTagName("CodigoMoneda");
                    strTipoIdentificacionEmisor = documentoXml.GetElementsByTagName("Emisor").Item(0).ChildNodes.Item(1).ChildNodes.Item(0).InnerText;
                    strIdentificacionEmisor = documentoXml.GetElementsByTagName("Emisor").Item(0).ChildNodes.Item(1).ChildNodes.Item(1).InnerText.PadLeft(12, '0');
                    if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                    {
                        strTipoIdentificacionReceptor = documentoXml.GetElementsByTagName("Receptor").Item(0).ChildNodes.Item(1).ChildNodes.Item(0).InnerText;
                        strIdentificacionReceptor = documentoXml.GetElementsByTagName("Receptor").Item(0).ChildNodes.Item(1).ChildNodes.Item(1).InnerText.PadLeft(12, '0');
                    }
                }
                else
                {
                    fechaEmision = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmisionDoc").Item(0).InnerText, CultureInfo.InvariantCulture);
                    strClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
                    esMensajeReceptor = true;
                    string strEmisorId = documentoXml.GetElementsByTagName("NumeroCedulaEmisor").Item(0).InnerText;
                    strTipoIdentificacionEmisor = strEmisorId.Length == 9 ? "01" : strEmisorId.Length == 10 ? "02" : "03";
                    strIdentificacionEmisor = strEmisorId.PadLeft(12, '0');
                    string strReceptorId = documentoXml.GetElementsByTagName("NumeroCedulaReceptor").Item(0).InnerText;
                    strTipoIdentificacionReceptor = strReceptorId.Length == 9 ? "01" : strReceptorId.Length == 10 ? "02" : "03";
                    strIdentificacionReceptor = strReceptorId.PadLeft(12, '0');
                }
                int intTipoDocumentoElectronico = (int)tipoDocumento;
                int intIdConsecutivo = 1;
                int? consecutivoBaseDatos = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdSucursal == intSucursal & x.IdTerminal == intTerminal & x.IdTipoDocumento == intTipoDocumentoElectronico).Max(x => (int?)x.IdConsecutivo);
                TerminalPorEmpresa terminal = dbContext.TerminalPorEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdSucursal == intSucursal & x.IdTerminal == intTerminal).FirstOrDefault();
                switch (tipoDocumento)
                {
                    case TipoDocumento.FacturaElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            if (consecutivoBaseDatos >= terminal.UltimoDocFE)
                                intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            else
                                intIdConsecutivo = terminal.UltimoDocFE + 1;
                        }
                        else
                            intIdConsecutivo = terminal.UltimoDocFE + 1;
                        terminal.UltimoDocFE = intIdConsecutivo;
                        break;
                    case TipoDocumento.NotaDebitoElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            if (consecutivoBaseDatos >= terminal.UltimoDocND)
                                intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            else
                                intIdConsecutivo = terminal.UltimoDocND + 1;
                        }
                        else
                            intIdConsecutivo = terminal.UltimoDocND + 1;
                        terminal.UltimoDocND = intIdConsecutivo;
                        break;
                    case TipoDocumento.NotaCreditoElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            if (consecutivoBaseDatos >= terminal.UltimoDocNC)
                                intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            else
                                intIdConsecutivo = terminal.UltimoDocNC + 1;
                        }
                        else
                            intIdConsecutivo = terminal.UltimoDocNC + 1;
                        terminal.UltimoDocNC = intIdConsecutivo;
                        break;
                    case TipoDocumento.TiqueteElectronico:
                        if (!(consecutivoBaseDatos is null))
                        {
                            if (consecutivoBaseDatos >= terminal.UltimoDocTE)
                                intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            else
                                intIdConsecutivo = terminal.UltimoDocTE + 1;
                        }
                        else
                            intIdConsecutivo = terminal.UltimoDocTE + 1;
                        terminal.UltimoDocTE = intIdConsecutivo;
                        break;
                    case TipoDocumento.MensajeReceptorAceptado:
                    case TipoDocumento.MensajeReceptorAceptadoParcial:
                    case TipoDocumento.MensajeReceptorRechazado:
                        if (!(consecutivoBaseDatos is null))
                        {
                            if (consecutivoBaseDatos >= terminal.UltimoDocMR)
                                intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            else
                                intIdConsecutivo = terminal.UltimoDocTE + 1;
                        }
                        else
                            intIdConsecutivo = terminal.UltimoDocMR + 1;
                        terminal.UltimoDocMR = intIdConsecutivo;
                        break;
                }
                strConsucutivo = intSucursal.ToString("D3") + intTerminal.ToString("D5") + intTipoDocumentoElectronico.ToString("D2") + intIdConsecutivo.ToString("D10");
                if (!esMensajeReceptor)
                {
                    Random rnd = new Random();
                    int intRandom = rnd.Next(10000000, 99999999);
                    strClaveNumerica = "506" + fechaEmision.Day.ToString().PadLeft(2, '0') + fechaEmision.ToString("MM") + fechaEmision.ToString("yy") + strIdentificacionEmisor + strConsucutivo + "1" + intRandom.ToString();
                    documentoXml.GetElementsByTagName("Clave").Item(0).InnerText = strClaveNumerica;
                    documentoXml.GetElementsByTagName("NumeroConsecutivo").Item(0).InnerText = strConsucutivo;
                }
                else
                {
                    documentoXml.GetElementsByTagName("NumeroConsecutivoReceptor").Item(0).InnerText = strConsucutivo;
                }
                XmlDeclaration xmldecl = documentoXml.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = documentoXml.DocumentElement;
                documentoXml.InsertBefore(xmldecl, root);
                byte[] mensajeEncoded = Encoding.UTF8.GetBytes(documentoXml.OuterXml);
                SignatureDocument signatureDocument;
                XadesService xadesService = new XadesService();
                SignatureParameters signatureParameters = new SignatureParameters
                {
                    SignaturePolicyInfo = new SignaturePolicyInfo
                    {
                        PolicyIdentifier = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4/Resolucion%20Comprobantes%20Electronicos%20%20DGT-R-48-2016.pdf",
                        PolicyHash = "V8lVVNGDCPen6VELRD1Ja8HARFk=",
                        PolicyUri = ""
                    },
                    SignatureMethod = SignatureMethod.RSAwithSHA256,
                    SigningDate = DateTime.Now,
                    SignaturePackaging = SignaturePackaging.ENVELOPED
                };
                X509Certificate2 uidCert = new X509Certificate2(empresa.Certificado, empresa.PinCertificado, X509KeyStorageFlags.MachineKeySet);
                using (Signer signer2 = signatureParameters.Signer = new Signer(uidCert))
                using (MemoryStream smDatos = new MemoryStream(mensajeEncoded))
                {
                    signatureDocument = xadesService.Sign(smDatos, signatureParameters);
                }
                // Almacenaje del documento en base de datos
                byte[] signedDataEncoded = Encoding.UTF8.GetBytes(signatureDocument.Document.OuterXml);
                byte[] documentoOriEncoded = null;
                if (documentoOriXml != null) documentoOriEncoded = Encoding.UTF8.GetBytes(documentoOriXml.OuterXml);
                DocumentoElectronico documento = new DocumentoElectronico
                {
                    IdEmpresa = empresa.IdEmpresa,
                    IdSucursal = intSucursal,
                    IdTerminal = intTerminal,
                    IdTipoDocumento = intTipoDocumentoElectronico,
                    IdConsecutivo = intIdConsecutivo,
                    Consecutivo = strConsucutivo,
                    ClaveNumerica = strClaveNumerica,
                    Fecha = fechaEmision,
                    TipoIdentificacionEmisor = strTipoIdentificacionEmisor,
                    IdentificacionEmisor = strIdentificacionEmisor,
                    TipoIdentificacionReceptor = strTipoIdentificacionReceptor,
                    IdentificacionReceptor = strIdentificacionReceptor,
                    EsMensajeReceptor = esMensajeReceptor ? "S" : "N",
                    EstadoEnvio = StaticEstadoDocumentoElectronico.Procesando,
                    CorreoNotificacion = strCorreoNotificacion,
                    DatosDocumento = signedDataEncoded,
                    DatosDocumentoOri = documentoOriEncoded
                };
                dbContext.DocumentoElectronicoRepository.Add(documento);
                if (empresa.TipoContrato == 1)
                {
                    int intMesEnCurso = DateTime.Now.Month;
                    int intAnnioEnCurso = DateTime.Now.Year;
                    CantFEMensualEmpresa cantiFacturasMensual = dbContext.CantFEMensualEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdMes == intMesEnCurso & x.IdAnio == intAnnioEnCurso).FirstOrDefault();
                    if (cantiFacturasMensual == null)
                    {
                        cantiFacturasMensual = new CantFEMensualEmpresa
                        {
                            IdEmpresa = empresa.IdEmpresa,
                            IdMes = intMesEnCurso,
                            IdAnio = intAnnioEnCurso,
                            CantidadDoc = 1
                        };
                        dbContext.CantFEMensualEmpresaRepository.Add(cantiFacturasMensual);
                    }
                    else
                    {
                        cantiFacturasMensual.CantidadDoc += 1;
                        dbContext.NotificarModificacion(cantiFacturasMensual);
                    }
                }
                else if (empresa.TipoContrato == 2)
                {
                    empresa.CantidadDisponible -= 1;
                    dbContext.NotificarModificacion(empresa);
                }
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el registrar el documento electrónico: ", ex);
                throw ex;
            }
        }

        public static async Task EnviarDocumentoElectronico(int intIdEmpresa, int intIdDocumento, DatosConfiguracion datos)
        {
            try
            {
                string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
                unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
                unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
                using (IDbContext dbContext = unityContainer.Resolve<IDbContext>())
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento != null)
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                        if (empresa != null)
                        {
                            try
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
                                        dbContext.NotificarModificacion(documento);
                                    }
                                    else
                                    {
                                        string strClave = documento.ClaveNumerica;
                                        if (new int[] { 5, 6, 7 }.Contains(documento.IdTipoDocumento)) strClave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                                        if (httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value != null)
                                        {
                                            IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                                            if (headers.Count > 0)
                                            {
                                                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                                                    if (headers[0] == "El comprobante [" + strClave + "] ya fue recibido anteriormente.")
                                                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                                                    else
                                                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                                else
                                                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                                documento.ErrorEnvio = headers[0];
                                            }
                                        }
                                        else
                                        {
                                            documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                            documento.ErrorEnvio = httpResponse.ReasonPhrase;
                                        }
                                        dbContext.NotificarModificacion(documento);
                                    }
                                    dbContext.Commit();
                                }
                                else
                                {
                                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                    documento.ErrorEnvio = "No se logro obtener un token válido para la empresa correspondiente al documento electrónico.";
                                    dbContext.NotificarModificacion(documento);
                                    dbContext.Commit();
                                }
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
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el enviar el documento electrónico: ", ex);
                throw ex;
            }
        }

        public static async Task<DocumentoElectronico> ConsultarDocumentoElectronico(Empresa empresaLocal, DocumentoElectronico documento, IDbContext dbContext, DatosConfiguracion datos)
        {
            try
            {
                if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)
                {
                    ValidarToken(dbContext, empresaLocal, datos.ServicioTokenURL, datos.ClientId);
                    if (empresaLocal.AccessToken != null)
                    {
                        string strClave = documento.ClaveNumerica;
                        if (new int[] { 5, 6, 7 }.Contains(documento.IdTipoDocumento)) strClave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                        HttpResponseMessage httpResponse = await httpClient.GetAsync(datos.ComprobantesElectronicosURL + "/recepcion/" + strClave);
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            JObject estadoDocumento = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);
                            string strEstado = estadoDocumento.Property("ind-estado").Value.ToString();
                            if (strEstado != "procesando")
                            {
                                string strRespuesta = estadoDocumento.Property("respuesta-xml").Value.ToString();
                                documento.Respuesta = Convert.FromBase64String(strRespuesta);

                            }
                            documento.EstadoEnvio = strEstado;
                        }
                        else
                        {
                            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                                documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                            if (httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value != null)
                            {
                                IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                                if (headers.Count > 0)
                                {
                                    documento.ErrorEnvio = headers[0];
                                }
                            }
                            else
                            {
                                documento.ErrorEnvio = httpResponse.ReasonPhrase;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("No se logro obtener un token válido para la empresa correspondiente al documento electrónico.");
                    }
                }
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error al consultar el estado del documento electrónico: ", ex);
                throw ex;
            }
        }
    }
}