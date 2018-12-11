using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.Core.CommonTypes;
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
using LeandroSoftware.Core.Servicios;
using Unity;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Core;
using System.Drawing;
using System.Web.Hosting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Web.Script.Serialization;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public class ComprobanteElectronicoService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static HttpClient httpClient = new HttpClient();
        private static DatosConfiguracion configuracionLocal;

        public ComprobanteElectronicoService(DatosConfiguracion configuracion)
        {
            configuracionLocal = configuracion;
        }

        private static void validarToken(IDbContext dbContext, Empresa empresaLocal, string strServicioTokenURL, string strClientId)
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

        public static void ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta, IUnityContainer localContainer)
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
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el tipo de cambio de venta: ", ex);
                throw ex;
            }
        }

        public static DocumentoElectronico GeneraFacturaElectronica(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, int intSucursal, int intTerminal, decimal decTipoCambioDolar)
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
                List<FacturaElectronicaMedioPago> medioPagoList = new List<FacturaElectronicaMedioPago>();
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
                facturaElectronica.MedioPago = medioPagoList.ToArray();
                List<FacturaElectronicaLineaDetalle> detalleServicioList = new List<FacturaElectronicaLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                decimal decPorcentajeIVA = factura.Empresa.PorcentajeIVA;
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
                    decimal precioVenta = 0;
                    if (!detalleFactura.Producto.Excento)
                    {
                        precioVenta = Math.Round(detalleFactura.PrecioVenta / (1 + (decPorcentajeIVA / 100)), 5);
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasGrabadas += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                        else
                            decTotalServiciosGrabados += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    }
                    else
                    {
                        precioVenta = detalleFactura.PrecioVenta;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                        else
                            decTotalServiciosExcentos += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    }
                    lineaDetalle.PrecioUnitario = precioVenta;
                    lineaDetalle.MontoTotal = Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    decimal decDescuentoPorLinea = 0;
                    if (factura.Descuento > 0)
                    {
                        decDescuentoPorLinea = Math.Round(factura.Descuento / (factura.Total - factura.Impuesto) * lineaDetalle.MontoTotal, 5);
                        if (decDescuentoPorLinea > decTotalDescuentoPorFactura)
                            decDescuentoPorLinea = decTotalDescuentoPorFactura;
                        else
                            decTotalDescuentoPorFactura -= decDescuentoPorLinea;
                        lineaDetalle.MontoDescuento = decDescuentoPorLinea;
                        lineaDetalle.NaturalezaDescuento = "Descuento sobre mercancías";
                    }
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal - lineaDetalle.MontoDescuento;
                    if (!detalleFactura.Excento)
                    {
                        FacturaElectronicaImpuestoType impuestoType = new FacturaElectronicaImpuestoType
                        {
                            Codigo = FacturaElectronicaImpuestoTypeCodigo.Item01,
                            Tarifa = factura.Empresa.PorcentajeIVA
                        };
                        impuestoType.Monto = Math.Round(lineaDetalle.SubTotal * impuestoType.Tarifa / 100, 5);
                        decTotalImpuestos += impuestoType.Monto;
                        lineaDetalle.Impuesto = new FacturaElectronicaImpuestoType[] { impuestoType };
                        lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + impuestoType.Monto;
                    }
                    else
                    {
                        lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal;
                    }
                    detalleServicioList.Add(lineaDetalle);
                }
                facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
                FacturaElectronicaResumenFactura resumenFactura = new FacturaElectronicaResumenFactura();
                if (factura.Empresa.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    resumenFactura.CodigoMoneda = FacturaElectronicaResumenFacturaCodigoMoneda.USD;
                    resumenFactura.CodigoMonedaSpecified = true;
                    resumenFactura.TipoCambio = decTipoCambioDolar;
                    resumenFactura.TipoCambioSpecified = true;
                }
                else if (factura.Empresa.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    resumenFactura.CodigoMoneda = FacturaElectronicaResumenFacturaCodigoMoneda.CRC;
                    resumenFactura.CodigoMonedaSpecified = true;
                }
                resumenFactura.TotalDescuentos = decTotalDescuentoPorFactura;
                resumenFactura.TotalDescuentosSpecified = true;
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
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - resumenFactura.TotalDescuentos;
                resumenFactura.TotalImpuesto = decTotalImpuestos;
                resumenFactura.TotalImpuestoSpecified = true;
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + resumenFactura.TotalImpuesto;
                facturaElectronica.ResumenFactura = resumenFactura;
                FacturaElectronicaNormativa normativa = new FacturaElectronicaNormativa
                {
                    NumeroResolucion = "DGT-R-48-2016",
                    FechaResolucion = "07-10-2016 00:00:00"
                };
                facturaElectronica.Normativa = normativa;

                XmlDocument documentoXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                XmlSerializer serializer = new XmlSerializer(facturaElectronica.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, facturaElectronica);
                    msDatosXML.Position = 0;
                    documentoXml.Load(msDatosXML);
                }
                return RegistrarDocumentoElectronico(empresa, documentoXml, dbContext, intSucursal, intTerminal, TipoDocumento.FacturaElectronica, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DocumentoElectronico GenerarNotaDeCreditoElectronica(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, int intSucursal, int intTerminal, decimal decTipoCambioDolar)
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
                List<NotaCreditoElectronicaMedioPago> medioPagoList = new List<NotaCreditoElectronicaMedioPago>();
                foreach (DesglosePagoFactura desglose in factura.DesglosePagoFactura)
                {
                    if (medioPagoList.Count() == 4)
                    {
                        throw new Exception("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                    }
                    NotaCreditoElectronicaMedioPago medioPago = (NotaCreditoElectronicaMedioPago)desglose.IdFormaPago - 1;
                    if (!medioPagoList.Contains(medioPago))
                    {
                        medioPagoList.Add(medioPago);
                    }
                }
                notaCreditoElectronica.MedioPago = medioPagoList.ToArray();
                List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                decimal decPorcentajeIVA = factura.Empresa.PorcentajeIVA;
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
                    decimal precioVenta = 0;
                    if (!detalleFactura.Producto.Excento)
                    {
                        precioVenta = Math.Round(detalleFactura.PrecioVenta / (1 + (decPorcentajeIVA / 100)), 5);
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasGrabadas += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                        else
                            decTotalServiciosGrabados += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    }
                    else
                    {
                        precioVenta = detalleFactura.PrecioVenta;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                        else
                            decTotalServiciosExcentos += Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    }
                    lineaDetalle.PrecioUnitario = precioVenta;
                    lineaDetalle.MontoTotal = Math.Round(detalleFactura.Cantidad * precioVenta, 5);
                    decimal decDescuentoPorLinea = 0;
                    if (factura.Descuento > 0)
                    {
                        decDescuentoPorLinea = Math.Round(factura.Descuento / (factura.Total - factura.Impuesto) * lineaDetalle.MontoTotal, 5);
                        if (decDescuentoPorLinea > decTotalDescuentoPorFactura)
                            decDescuentoPorLinea = decTotalDescuentoPorFactura;
                        else
                            decTotalDescuentoPorFactura -= decDescuentoPorLinea;
                        lineaDetalle.MontoDescuento = decDescuentoPorLinea;
                        lineaDetalle.NaturalezaDescuento = "Descuento sobre mercancías";
                    }
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal - lineaDetalle.MontoDescuento;
                    if (!detalleFactura.Excento)
                    {
                        NotaCreditoElectronicaImpuestoType impuestoType = new NotaCreditoElectronicaImpuestoType
                        {
                            Codigo = NotaCreditoElectronicaImpuestoTypeCodigo.Item01,
                            Tarifa = factura.Empresa.PorcentajeIVA
                        };
                        impuestoType.Monto = Math.Round(lineaDetalle.SubTotal * impuestoType.Tarifa / 100, 5);
                        decTotalImpuestos += impuestoType.Monto;
                        lineaDetalle.Impuesto = new NotaCreditoElectronicaImpuestoType[] { impuestoType };
                        lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + impuestoType.Monto;
                    }
                    else
                    {
                        lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal;
                    }
                    detalleServicioList.Add(lineaDetalle);
                }
                notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
                NotaCreditoElectronicaResumenFactura resumenFactura = new NotaCreditoElectronicaResumenFactura();
                if (factura.Empresa.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    resumenFactura.CodigoMoneda = NotaCreditoElectronicaResumenFacturaCodigoMoneda.USD;
                    resumenFactura.CodigoMonedaSpecified = true;
                    resumenFactura.TipoCambio = decTipoCambioDolar;
                    resumenFactura.TipoCambioSpecified = true;
                }
                else if (factura.Empresa.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    resumenFactura.CodigoMoneda = NotaCreditoElectronicaResumenFacturaCodigoMoneda.CRC;
                    resumenFactura.CodigoMonedaSpecified = true;
                }
                resumenFactura.TotalDescuentos = decTotalDescuentoPorFactura;
                resumenFactura.TotalDescuentosSpecified = true;
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
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - resumenFactura.TotalDescuentos;
                resumenFactura.TotalImpuesto = decTotalImpuestos;
                resumenFactura.TotalImpuestoSpecified = true;
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + resumenFactura.TotalImpuesto;
                notaCreditoElectronica.ResumenFactura = resumenFactura;
                NotaCreditoElectronicaInformacionReferencia informacionReferencia = new NotaCreditoElectronicaInformacionReferencia();
                informacionReferencia.TipoDoc = NotaCreditoElectronicaInformacionReferenciaTipoDoc.Item01;
                informacionReferencia.Numero = factura.IdDocElectronico;
                informacionReferencia.FechaEmision = factura.Fecha;
                informacionReferencia.Codigo = NotaCreditoElectronicaInformacionReferenciaCodigo.Item01;
                informacionReferencia.Razon = "Anulación del documento factura electronica con la respectiva clave númerica.";
                notaCreditoElectronica.InformacionReferencia = new NotaCreditoElectronicaInformacionReferencia[] { informacionReferencia };
                NotaCreditoElectronicaNormativa normativa = new NotaCreditoElectronicaNormativa
                {
                    NumeroResolucion = "DGT-R-48-2016",
                    FechaResolucion = "07-10-2016 00:00:00"
                };
                notaCreditoElectronica.Normativa = normativa;
                XmlDocument documentoXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                XmlSerializer serializer = new XmlSerializer(notaCreditoElectronica.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, notaCreditoElectronica);
                    msDatosXML.Position = 0;
                    documentoXml.Load(msDatosXML);
                }
                return RegistrarDocumentoElectronico(empresa, documentoXml, dbContext, intSucursal, intTerminal, TipoDocumento.NotaCreditoElectronica, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void GeneraMensajeReceptor(string datosXml, Empresa empresa, IDbContext dbContext, int intSucursal, int intTerminal, int intMensaje)
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
                MensajeReceptor mensajeReceptor = new MensajeReceptor
                {
                    Clave = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,

                    FechaEmisionDoc = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmision").Item(0).InnerText, CultureInfo.InvariantCulture),
                    Mensaje = (MensajeReceptorMensaje)intMensaje,
                    DetalleMensaje = "Mensaje de receptor con estado: " + (intMensaje == 0 ? "Aceptado" : intMensaje == 1 ? "Aceptado parcialmente" : "Rechazado"),
                    TotalFactura = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText),
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
                    string strTotalImpuesto = documentoXml.GetElementsByTagName("TotalImpuesto").Item(0).InnerText;
                    mensajeReceptor.MontoTotalImpuesto = decimal.Parse(strTotalImpuesto);
                    mensajeReceptor.MontoTotalImpuestoSpecified = true;
                }

                XmlDocument mensajeReceptorXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                XmlSerializer serializer = new XmlSerializer(mensajeReceptor.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, mensajeReceptor);
                    msDatosXML.Position = 0;
                    mensajeReceptorXml.Load(msDatosXML);
                }
                TipoDocumento tipoDoc = intMensaje == 0 ? TipoDocumento.MensajeReceptorAceptado : intMensaje == 1 ? TipoDocumento.MensajeReceptorAceptadoParcial : TipoDocumento.MensajeReceptorRechazado;
                DocumentoElectronico documento = RegistrarDocumentoElectronico(empresa, mensajeReceptorXml, dbContext, intSucursal, intTerminal, tipoDoc, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el MensajeReceptor: ", ex);
                throw ex;
            }
        }

        public static DocumentoElectronico RegistrarDocumentoElectronico(Empresa empresa, XmlDocument documentoXml, IDbContext dbContext, int intSucursal, int intTerminal, TipoDocumento tipoDocumento, string strCorreoNotificacion)
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
                switch (tipoDocumento)
                {
                    case TipoDocumento.FacturaElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            empresa.UltimoDocFE = intIdConsecutivo;
                        }
                        else
                            intIdConsecutivo = empresa.UltimoDocFE + 1;
                        break;
                    case TipoDocumento.NotaDebitoElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            empresa.UltimoDocND = intIdConsecutivo;
                        }
                        else
                            intIdConsecutivo = empresa.UltimoDocND + 1;
                        break;
                    case TipoDocumento.NotaCreditoElectronica:
                        if (!(consecutivoBaseDatos is null))
                        {
                            intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            empresa.UltimoDocNC = intIdConsecutivo;
                        }
                        else
                            intIdConsecutivo = empresa.UltimoDocNC + 1;
                        break;
                    case TipoDocumento.TiqueteElectronico:
                        if (!(consecutivoBaseDatos is null))
                        {
                            intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            empresa.UltimoDocTE = intIdConsecutivo;
                        }
                        else
                            intIdConsecutivo = empresa.UltimoDocTE + 1;
                        break;
                    case TipoDocumento.MensajeReceptorAceptado:
                    case TipoDocumento.MensajeReceptorAceptadoParcial:
                    case TipoDocumento.MensajeReceptorRechazado:
                        if (!(consecutivoBaseDatos is null))
                        {
                            intIdConsecutivo = (int)consecutivoBaseDatos + 1;
                            empresa.UltimoDocMR = intIdConsecutivo;
                        }
                        else
                            intIdConsecutivo = empresa.UltimoDocMR + 1;
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
                SignatureParameters signatureParameters = new SignatureParameters();
                signatureParameters.SignaturePolicyInfo = new SignaturePolicyInfo();
                signatureParameters.SignaturePolicyInfo.PolicyIdentifier = "https://tribunet.hacienda.go.cr/docs/esquemas/2016/v4/Resolucion%20Comprobantes%20Electronicos%20%20DGT-R-48-2016.pdf";
                signatureParameters.SignaturePolicyInfo.PolicyHash = "V8lVVNGDCPen6VELRD1Ja8HARFk=";
                signatureParameters.SignaturePolicyInfo.PolicyUri = "";
                signatureParameters.SignatureMethod = SignatureMethod.RSAwithSHA256;
                signatureParameters.SigningDate = DateTime.Now;
                signatureParameters.SignaturePackaging = SignaturePackaging.ENVELOPED;
                X509Certificate2 uidCert = new X509Certificate2(empresa.Certificado, empresa.PinCertificado, X509KeyStorageFlags.UserKeySet);
                using (Signer signer2 = signatureParameters.Signer = new Signer(uidCert))
                using (MemoryStream smDatos = new MemoryStream(mensajeEncoded))
                {
                    signatureDocument = xadesService.Sign(smDatos, signatureParameters);
                }
                // Almacenaje del documento en base de datos
                byte[] signedDataEncoded = Encoding.UTF8.GetBytes(signatureDocument.Document.OuterXml);
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
                    EstadoEnvio = "registrado",
                    CorreoNotificacion = strCorreoNotificacion,
                    DatosDocumento = signedDataEncoded
                };
                dbContext.DocumentoElectronicoRepository.Add(documento);
                int intMesEnCurso = DateTime.Now.Month;
                int intAnnioEnCurso = DateTime.Now.Year;
                CantFEMensualEmpresa cantiFacturasMensual = dbContext.CantFEMensualEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdMes == intMesEnCurso && x.IdAnio == intAnnioEnCurso).FirstOrDefault();
                if (cantiFacturasMensual == null)
                {
                    cantiFacturasMensual = new CantFEMensualEmpresa();
                    cantiFacturasMensual.IdEmpresa = empresa.IdEmpresa;
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
                return documento;
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el registrar el documento electrónico: ", ex);
                throw ex;
            }
        }

        

        public static async Task EnviarDocumentoElectronico(Empresa empresaLocal, DocumentoElectronico documento, IDbContext dbContext)
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
                validarToken(dbContext, empresaLocal, configuracionLocal.ServicioTokenURL, configuracionLocal.ClientId);
                if (empresaLocal.AccessToken != null)
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
                        if (configuracionLocal.CallbackURL != "")
                            JsonObject += "\"callbackUrl\": \"" + configuracionLocal.CallbackURL + "\",";
                        if (documento.EsMensajeReceptor == "S")
                            JsonObject += "\"consecutivoReceptor\": \"" + documento.Consecutivo + "\",";
                        JsonObject += "\"comprobanteXml\": \"" + strComprobanteXML + "\"}";
                        StringContent contentJson = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                        HttpResponseMessage httpResponse = httpClient.PostAsync(configuracionLocal.ComprobantesElectronicosURL + "/recepcion", contentJson).Result;
                        string responseContent = await httpResponse.Content.ReadAsStringAsync();
                        if (httpResponse.StatusCode != HttpStatusCode.Accepted)
                        {
                            string strMensajeError = httpResponse.ReasonPhrase;
                            throw new Exception(strMensajeError);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMensajeError = ex.Message;
                        if (ex.Message.Length > 500) strMensajeError = ex.Message.Substring(0, 500);
                        throw new Exception(strMensajeError);
                    }
                }
                else
                {
                    throw new Exception("No se logro obtener un token válido para la empresa correspondiente al documento electrónico");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el enviar el documento electrónico: ", ex);
                throw ex;
            }
        }

        public static async Task<DocumentoElectronico> ConsultarDocumentoElectronico(Empresa empresaLocal, DocumentoElectronico documento, IDbContext dbContext)
        {
            try
            {
                if (documento.EstadoEnvio == "enviado")
                {
                    validarToken(dbContext, empresaLocal, configuracionLocal.ServicioTokenURL, configuracionLocal.ClientId);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", empresaLocal.AccessToken);
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(configuracionLocal.ComprobantesElectronicosURL + "/recepcion/" + documento.ClaveNumerica);
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
                        if (httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value != null)
                        {
                            IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                            if (headers.Count > 0)
                                documento.ErrorEnvio = headers[0];
                        }
                        documento.EstadoEnvio = "registrado";
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

        public static void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores, IUnityContainer localContainer)
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
                log.Error("Error al procesar la respuesta de hacienda: ", ex);
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el procesamiento de la respuesta de hacienda para el comprobante con clave: " + mensaje.Clave, ex.Message, false, emptyJArray);
            }
        }
    }
}
