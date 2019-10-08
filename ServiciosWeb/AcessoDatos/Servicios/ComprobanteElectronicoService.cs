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
using LeandroSoftware.Core.CommonTypes;
using LeandroSoftware.Core.TiposDatosHacienda;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public static class ComprobanteElectronicoService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static HttpClient httpClient = new HttpClient();
        private static IUnityContainer unityContainer = new UnityContainer();

        private static void ValidarToken(IDbContext dbContext, Empresa empresaLocal, string strServicioTokenURL, string strClientId)
        {
            TokenType nuevoToken;
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
                                    string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                    throw new Exception("Error parseando el tipo de cambio: " + strTipoCambioDolar + ": " + errorMessage);
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
                        throw new BusinessException("El cliente seleccionado debe poseer una dirección de correo electrónico para ser notificado.");
                    }
                    else
                    {
                        strCorreoNotificacion = cliente.CorreoElectronico;
                    }
                }
                if (empresa.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
                FacturaElectronica facturaElectronica = new FacturaElectronica
                {
                    Clave = "",
                    CodigoActividad = empresa.CodigoActividad,
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
                            throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
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
                decimal decTotalMercanciasExoneradas = 0;
                decimal decTotalServiciosExonerados = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
                {
                    FacturaElectronicaLineaDetalle lineaDetalle = new FacturaElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString()
                    };
                    lineaDetalle.Codigo = detalleFactura.Producto.Codigo;
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Unid;
                    else
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Sp;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    decimal montoTotalPorLinea = Math.Round(detalleFactura.Cantidad * detalleFactura.PrecioVenta, 5);
                    decimal decMontoGrabado = montoTotalPorLinea;
                    decimal decMontoExonerado = 0;
                    if (!detalleFactura.Excento)
                    {
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decMontoGrabado = Math.Round(montoTotalPorLinea * (1 - (decimal.Parse(factura.PorcentajeExoneracion.ToString()) / 100)), 5);
                            decMontoExonerado = Math.Round(montoTotalPorLinea - decMontoGrabado, 5);
                        }
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGrabadas += decMontoGrabado;
                            decTotalMercanciasExoneradas += decMontoExonerado;
                        }
                        else
                        {
                            decTotalServiciosGrabados += decMontoGrabado;
                            decTotalServiciosExonerados += decMontoExonerado;
                        }
                    }
                    else
                    {
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += montoTotalPorLinea;
                        else
                            decTotalServiciosExcentos += montoTotalPorLinea;
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
                        FacturaElectronicaDescuentoType descuentoType = new FacturaElectronicaDescuentoType
                        {
                            MontoDescuento = descuentoPorLinea,
                            NaturalezaDescuento = "Descuento sobre mercancías"
                        };
                        lineaDetalle.Descuento = new FacturaElectronicaDescuentoType[] { descuentoType };
                    }
                    lineaDetalle.SubTotal = montoTotalPorLinea - descuentoPorLinea;
                    decimal montoImpuestoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        FacturaElectronicaImpuestoType impuestoType = new FacturaElectronicaImpuestoType
                        {
                            Codigo = FacturaElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (FacturaElectronicaImpuestoTypeCodigoTarifa)detalleFactura.Producto.IdImpuesto - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true
                        };
                        montoImpuestoPorLinea = Math.Round(lineaDetalle.SubTotal * detalleFactura.PorcentajeIVA / 100, 5);
                        impuestoType.Monto = montoImpuestoPorLinea;
                        if (decMontoExonerado > 0)
                        {
                            FacturaElectronicaExoneracionType exoneracionType = new FacturaElectronicaExoneracionType
                            {
                                TipoDocumento = (FacturaElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString(),
                                MontoExoneracion = decMontoExonerado * detalleFactura.PorcentajeIVA / 100
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.Impuesto = new FacturaElectronicaImpuestoType[] { impuestoType };
                        if (decMontoExonerado > 0)
                        {
                            lineaDetalle.ImpuestoNeto = montoImpuestoPorLinea - impuestoType.Exoneracion.MontoExoneracion;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + lineaDetalle.ImpuestoNeto;
                            decTotalImpuestos += lineaDetalle.ImpuestoNeto;
                        }
                        else
                        {
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + montoImpuestoPorLinea;
                            decTotalImpuestos += montoImpuestoPorLinea;
                        }
                    }
                    detalleServicioList.Add(lineaDetalle);
                }
                facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
                FacturaElectronicaResumenFactura resumenFactura = new FacturaElectronicaResumenFactura();
                FacturaElectronicaCodigoMonedaType codigoMonedaType = null;
                if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    codigoMonedaType = new FacturaElectronicaCodigoMonedaType
                    {
                        CodigoMoneda = FacturaElectronicaCodigoMonedaTypeCodigoMoneda.USD,
                        TipoCambio = decTipoCambioDolar
                    };
                }
                else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    codigoMonedaType = new FacturaElectronicaCodigoMonedaType
                    {
                        CodigoMoneda = FacturaElectronicaCodigoMonedaTypeCodigoMoneda.CRC,
                        TipoCambio = 1
                    };
                }
                resumenFactura.CodigoTipoMoneda = codigoMonedaType;
                if (factura.Descuento > 0)
                {
                    resumenFactura.TotalDescuentos = factura.Descuento;
                    resumenFactura.TotalDescuentosSpecified = true;
                }
                resumenFactura.TotalMercanciasGravadas = decTotalMercanciasGrabadas;
                resumenFactura.TotalMercanciasGravadasSpecified = true;
                resumenFactura.TotalMercExonerada = decTotalMercanciasExoneradas;
                resumenFactura.TotalMercExoneradaSpecified = true;
                resumenFactura.TotalMercanciasExentas = decTotalMercanciasExcentas;
                resumenFactura.TotalMercanciasExentasSpecified = true;
                resumenFactura.TotalServGravados = decTotalServiciosGrabados;
                resumenFactura.TotalServGravadosSpecified = true;
                resumenFactura.TotalServExonerado = decTotalServiciosExonerados;
                resumenFactura.TotalServExoneradoSpecified = true;
                resumenFactura.TotalServExentos = decTotalServiciosExcentos;
                resumenFactura.TotalServExentosSpecified = true;
                resumenFactura.TotalGravado = decTotalMercanciasGrabadas + decTotalServiciosGrabados;
                resumenFactura.TotalGravadoSpecified = true;
                resumenFactura.TotalExonerado = decTotalMercanciasExoneradas + decTotalServiciosExonerados;
                resumenFactura.TotalExoneradoSpecified = true;
                resumenFactura.TotalExento = decTotalMercanciasExcentas + decTotalServiciosExcentos;
                resumenFactura.TotalExentoSpecified = true;
                resumenFactura.TotalVenta = resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento;
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - factura.Descuento;
                if (decTotalImpuestos > 0)
                {
                    resumenFactura.TotalImpuesto = decTotalImpuestos;
                    resumenFactura.TotalImpuestoSpecified = true;
                }
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + decTotalImpuestos;
                facturaElectronica.ResumenFactura = resumenFactura;
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

        public static DocumentoElectronico GeneraTiqueteElectronico(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, decimal decTipoCambioDolar)
        {
            try
            {
                string strCorreoNotificacion = "";
                if (cliente.IdCliente > 1)
                {
                    if (cliente.CorreoElectronico == null || cliente.CorreoElectronico.Length == 0)
                    {
                        throw new BusinessException("El cliente seleccionado debe poseer una dirección de correo electrónico para ser notificado.");
                    }
                    else
                    {
                        strCorreoNotificacion = cliente.CorreoElectronico;
                    }
                }
                if (empresa.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
                TiqueteElectronico tiqueteElectronico = new TiqueteElectronico
                {
                    Clave = "",
                    CodigoActividad = empresa.CodigoActividad,
                    NumeroConsecutivo = "",
                    FechaEmision = factura.Fecha
                };
                TiqueteElectronicoEmisorType emisor = new TiqueteElectronicoEmisorType();
                TiqueteElectronicoIdentificacionType identificacionEmisorType = new TiqueteElectronicoIdentificacionType
                {
                    Tipo = (TiqueteElectronicoIdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                    Numero = empresa.Identificacion
                };
                emisor.Identificacion = identificacionEmisorType;
                emisor.Nombre = empresa.NombreEmpresa;
                emisor.NombreComercial = empresa.NombreComercial;
                if (empresa.Telefono.Length > 0)
                {
                    TiqueteElectronicoTelefonoType telefonoType = new TiqueteElectronicoTelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = empresa.Telefono
                    };
                    emisor.Telefono = telefonoType;
                }
                emisor.CorreoElectronico = empresa.CorreoNotificacion;
                TiqueteElectronicoUbicacionType ubicacionType = new TiqueteElectronicoUbicacionType
                {
                    Provincia = empresa.IdProvincia.ToString(),
                    Canton = empresa.IdCanton.ToString("D2"),
                    Distrito = empresa.IdDistrito.ToString("D2"),
                    Barrio = empresa.IdBarrio.ToString("D2"),
                    OtrasSenas = empresa.Direccion
                };
                emisor.Ubicacion = ubicacionType;
                tiqueteElectronico.Emisor = emisor;
                tiqueteElectronico.CondicionVenta = (TiqueteElectronicoCondicionVenta)factura.IdCondicionVenta - 1;
                if (tiqueteElectronico.CondicionVenta == TiqueteElectronicoCondicionVenta.Item02)
                {
                    tiqueteElectronico.PlazoCredito = factura.PlazoCredito.ToString();
                }
                List<TiqueteElectronicoMedioPago> medioPagoList = new List<TiqueteElectronicoMedioPago>();
                if (tiqueteElectronico.CondicionVenta != TiqueteElectronicoCondicionVenta.Item01)
                {
                    TiqueteElectronicoMedioPago medioPago = TiqueteElectronicoMedioPago.Item99;
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
                            throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                        }
                        TiqueteElectronicoMedioPago medioPago = (TiqueteElectronicoMedioPago)desglose.IdFormaPago - 1;
                        if (!medioPagoList.Contains(medioPago))
                        {
                            medioPagoList.Add(medioPago);
                        }
                    }
                }
                tiqueteElectronico.MedioPago = medioPagoList.ToArray();
                List<TiqueteElectronicoLineaDetalle> detalleServicioList = new List<TiqueteElectronicoLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalMercanciasExoneradas = 0;
                decimal decTotalServiciosExonerados = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
                {
                    TiqueteElectronicoLineaDetalle lineaDetalle = new TiqueteElectronicoLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString()
                    };
                    lineaDetalle.Codigo = detalleFactura.Producto.Codigo;
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = TiqueteElectronicoUnidadMedidaType.Unid;
                    else
                        lineaDetalle.UnidadMedida = TiqueteElectronicoUnidadMedidaType.Sp;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    decimal montoTotalPorLinea = Math.Round(detalleFactura.Cantidad * detalleFactura.PrecioVenta, 5);
                    decimal decMontoGrabado = montoTotalPorLinea;
                    decimal decMontoExonerado = 0;
                    if (!detalleFactura.Excento)
                    {
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decMontoGrabado = Math.Round(montoTotalPorLinea * (1 - (decimal.Parse(factura.PorcentajeExoneracion.ToString()) / 100)), 5);
                            decMontoExonerado = Math.Round(montoTotalPorLinea - decMontoGrabado, 5);
                        }
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGrabadas += decMontoGrabado;
                            decTotalMercanciasExoneradas += decMontoExonerado;
                        }
                        else
                        {
                            decTotalServiciosGrabados += decMontoGrabado;
                            decTotalServiciosExonerados += decMontoExonerado;
                        }
                    }
                    else
                    {
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += montoTotalPorLinea;
                        else
                            decTotalServiciosExcentos += montoTotalPorLinea;
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
                        TiqueteElectronicoDescuentoType descuentoType = new TiqueteElectronicoDescuentoType
                        {
                            MontoDescuento = descuentoPorLinea,
                            NaturalezaDescuento = "Descuento sobre mercancías"
                        };
                        lineaDetalle.Descuento = new TiqueteElectronicoDescuentoType[] { descuentoType };
                    }
                    lineaDetalle.SubTotal = montoTotalPorLinea - descuentoPorLinea;
                    decimal montoImpuestoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        TiqueteElectronicoImpuestoType impuestoType = new TiqueteElectronicoImpuestoType
                        {
                            Codigo = TiqueteElectronicoImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (TiqueteElectronicoImpuestoTypeCodigoTarifa)detalleFactura.Producto.IdImpuesto - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true
                        };
                        montoImpuestoPorLinea = Math.Round(lineaDetalle.SubTotal * detalleFactura.PorcentajeIVA / 100, 5);
                        impuestoType.Monto = montoImpuestoPorLinea;
                        if (decMontoExonerado > 0)
                        {
                            TiqueteElectronicoExoneracionType exoneracionType = new TiqueteElectronicoExoneracionType
                            {
                                TipoDocumento = (TiqueteElectronicoExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString(),
                                MontoExoneracion = decMontoExonerado * detalleFactura.PorcentajeIVA / 100
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.Impuesto = new TiqueteElectronicoImpuestoType[] { impuestoType };
                        if (decMontoExonerado > 0)
                        {
                            lineaDetalle.ImpuestoNeto = montoImpuestoPorLinea - impuestoType.Exoneracion.MontoExoneracion;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + lineaDetalle.ImpuestoNeto;
                            decTotalImpuestos += lineaDetalle.ImpuestoNeto;
                        }
                        else
                        {
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + montoImpuestoPorLinea;
                            decTotalImpuestos += montoImpuestoPorLinea;
                        }
                    }
                    detalleServicioList.Add(lineaDetalle);
                }
                tiqueteElectronico.DetalleServicio = detalleServicioList.ToArray();
                TiqueteElectronicoResumenFactura resumenFactura = new TiqueteElectronicoResumenFactura();
                TiqueteElectronicoCodigoMonedaType codigoMonedaType = null;
                if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    codigoMonedaType = new TiqueteElectronicoCodigoMonedaType
                    {
                        CodigoMoneda = TiqueteElectronicoCodigoMonedaTypeCodigoMoneda.USD,
                        TipoCambio = decTipoCambioDolar
                    };
                }
                else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    codigoMonedaType = new TiqueteElectronicoCodigoMonedaType
                    {
                        CodigoMoneda = TiqueteElectronicoCodigoMonedaTypeCodigoMoneda.CRC,
                        TipoCambio = 1
                    };
                }
                resumenFactura.CodigoTipoMoneda = codigoMonedaType;
                if (factura.Descuento > 0)
                {
                    resumenFactura.TotalDescuentos = factura.Descuento;
                    resumenFactura.TotalDescuentosSpecified = true;
                }
                resumenFactura.TotalMercanciasGravadas = decTotalMercanciasGrabadas;
                resumenFactura.TotalMercanciasGravadasSpecified = true;
                resumenFactura.TotalMercExonerada = decTotalMercanciasExoneradas;
                resumenFactura.TotalMercExoneradaSpecified = true;
                resumenFactura.TotalMercanciasExentas = decTotalMercanciasExcentas;
                resumenFactura.TotalMercanciasExentasSpecified = true;
                resumenFactura.TotalServGravados = decTotalServiciosGrabados;
                resumenFactura.TotalServGravadosSpecified = true;
                resumenFactura.TotalServExonerado = decTotalServiciosExonerados;
                resumenFactura.TotalServExoneradoSpecified = true;
                resumenFactura.TotalServExentos = decTotalServiciosExcentos;
                resumenFactura.TotalServExentosSpecified = true;
                resumenFactura.TotalGravado = decTotalMercanciasGrabadas + decTotalServiciosGrabados;
                resumenFactura.TotalGravadoSpecified = true;
                resumenFactura.TotalExonerado = decTotalMercanciasExoneradas + decTotalServiciosExonerados;
                resumenFactura.TotalExoneradoSpecified = true;
                resumenFactura.TotalExento = decTotalMercanciasExcentas + decTotalServiciosExcentos;
                resumenFactura.TotalExentoSpecified = true;
                resumenFactura.TotalVenta = resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento;
                resumenFactura.TotalVentaNeta = resumenFactura.TotalVenta - factura.Descuento;
                if (decTotalImpuestos > 0)
                {
                    resumenFactura.TotalImpuesto = decTotalImpuestos;
                    resumenFactura.TotalImpuestoSpecified = true;
                }
                resumenFactura.TotalComprobante = resumenFactura.TotalVentaNeta + decTotalImpuestos;
                tiqueteElectronico.ResumenFactura = resumenFactura;
                if (factura.TextoAdicional != "")
                {
                    TiqueteElectronicoOtros otros = new TiqueteElectronicoOtros();
                    TiqueteElectronicoOtrosOtroTexto otrosTextos = new TiqueteElectronicoOtrosOtroTexto();
                    otrosTextos.Value = factura.TextoAdicional;
                    otros.OtroTexto = new TiqueteElectronicoOtrosOtroTexto[] { otrosTextos };
                    tiqueteElectronico.Otros = otros;
                }
                XmlDocument documentoXml = new XmlDocument();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                XmlSerializer serializer = new XmlSerializer(tiqueteElectronico.GetType());
                using (MemoryStream msDatosXML = new MemoryStream())
                using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
                {
                    serializer.Serialize(writer, tiqueteElectronico);
                    msDatosXML.Position = 0;
                    documentoXml.Load(msDatosXML);
                }
                return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.TiqueteElectronico, strCorreoNotificacion);
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
                        throw new BusinessException("El cliente seleccionado debe poseer una dirección de correo electrónico para ser notificado.");
                    }
                    else
                    {
                        strCorreoNotificacion = cliente.CorreoElectronico;
                    }
                }
                if (empresa.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
                NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
                {
                    Clave = "",
                    CodigoActividad = empresa.CodigoActividad,
                    NumeroConsecutivo = "",
                    FechaEmision = factura.Fecha
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
                List<NotaCreditoElectronicaMedioPago> medioPagoList = new List<NotaCreditoElectronicaMedioPago>();
                if (notaCreditoElectronica.CondicionVenta != NotaCreditoElectronicaCondicionVenta.Item01)
                {
                    NotaCreditoElectronicaMedioPago medioPago = NotaCreditoElectronicaMedioPago.Item99;
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
                            throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                        }
                        NotaCreditoElectronicaMedioPago medioPago = (NotaCreditoElectronicaMedioPago)desglose.IdFormaPago - 1;
                        if (!medioPagoList.Contains(medioPago))
                        {
                            medioPagoList.Add(medioPago);
                        }
                    }
                }
                notaCreditoElectronica.MedioPago = medioPagoList.ToArray();
                List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
                decimal decTotalMercanciasGrabadas = 0;
                decimal decTotalServiciosGrabados = 0;
                decimal decTotalMercanciasExcentas = 0;
                decimal decTotalServiciosExcentos = 0;
                decimal decTotalMercanciasExoneradas = 0;
                decimal decTotalServiciosExonerados = 0;
                decimal decTotalDescuentoPorFactura = factura.Descuento;
                decimal decTotalImpuestos = 0;
                foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
                {
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString()
                    };
                    lineaDetalle.Codigo = detalleFactura.Producto.Codigo;
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Unid;
                    else
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Sp;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    decimal montoTotalPorLinea = Math.Round(detalleFactura.Cantidad * detalleFactura.PrecioVenta, 5);
                    decimal decMontoGrabado = montoTotalPorLinea;
                    decimal decMontoExonerado = 0;
                    if (!detalleFactura.Excento)
                    {
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decMontoGrabado = Math.Round(montoTotalPorLinea * (1 - (decimal.Parse(factura.PorcentajeExoneracion.ToString()) / 100)), 5);
                            decMontoExonerado = Math.Round(montoTotalPorLinea - decMontoGrabado, 5);
                        }
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGrabadas += decMontoGrabado;
                            decTotalMercanciasExoneradas += decMontoExonerado;
                        }
                        else
                        {
                            decTotalServiciosGrabados += decMontoGrabado;
                            decTotalServiciosExonerados += decMontoExonerado;
                        }
                    }
                    else
                    {
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += montoTotalPorLinea;
                        else
                            decTotalServiciosExcentos += montoTotalPorLinea;
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
                        NotaCreditoElectronicaDescuentoType descuentoType = new NotaCreditoElectronicaDescuentoType
                        {
                            MontoDescuento = descuentoPorLinea,
                            NaturalezaDescuento = "Descuento sobre mercancías"
                        };
                        lineaDetalle.Descuento = new NotaCreditoElectronicaDescuentoType[] { descuentoType };
                    }
                    lineaDetalle.SubTotal = montoTotalPorLinea - descuentoPorLinea;
                    decimal montoImpuestoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        NotaCreditoElectronicaImpuestoType impuestoType = new NotaCreditoElectronicaImpuestoType
                        {
                            Codigo = NotaCreditoElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (NotaCreditoElectronicaImpuestoTypeCodigoTarifa)detalleFactura.Producto.IdImpuesto - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true
                        };
                        montoImpuestoPorLinea = Math.Round(lineaDetalle.SubTotal * detalleFactura.PorcentajeIVA / 100, 5);
                        impuestoType.Monto = montoImpuestoPorLinea;
                        if (decMontoExonerado > 0)
                        {
                            NotaCreditoElectronicaExoneracionType exoneracionType = new NotaCreditoElectronicaExoneracionType
                            {
                                TipoDocumento = (NotaCreditoElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString(),
                                MontoExoneracion = decMontoExonerado * detalleFactura.PorcentajeIVA / 100
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.Impuesto = new NotaCreditoElectronicaImpuestoType[] { impuestoType };
                        if (decMontoExonerado > 0)
                        {
                            lineaDetalle.ImpuestoNeto = montoImpuestoPorLinea - impuestoType.Exoneracion.MontoExoneracion;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + lineaDetalle.ImpuestoNeto;
                            decTotalImpuestos += lineaDetalle.ImpuestoNeto;
                        }
                        else
                        {
                            lineaDetalle.MontoTotalLinea = lineaDetalle.SubTotal + montoImpuestoPorLinea;
                            decTotalImpuestos += montoImpuestoPorLinea;
                        }
                    }
                    detalleServicioList.Add(lineaDetalle);
                }
                notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
                NotaCreditoElectronicaResumenFactura resumenFactura = new NotaCreditoElectronicaResumenFactura();
                NotaCreditoElectronicaCodigoMonedaType codigoMonedaType = null;
                if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
                {
                    codigoMonedaType = new NotaCreditoElectronicaCodigoMonedaType
                    {
                        CodigoMoneda = NotaCreditoElectronicaCodigoMonedaTypeCodigoMoneda.USD,
                        TipoCambio = decTipoCambioDolar
                    };
                }
                else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
                {
                    codigoMonedaType = new NotaCreditoElectronicaCodigoMonedaType
                    {
                        CodigoMoneda = NotaCreditoElectronicaCodigoMonedaTypeCodigoMoneda.CRC,
                        TipoCambio = 1
                    };
                }
                resumenFactura.CodigoTipoMoneda = codigoMonedaType;
                if (factura.Descuento > 0)
                {
                    resumenFactura.TotalDescuentos = factura.Descuento;
                    resumenFactura.TotalDescuentosSpecified = true;
                }
                resumenFactura.TotalMercanciasGravadas = decTotalMercanciasGrabadas;
                resumenFactura.TotalMercanciasGravadasSpecified = true;
                resumenFactura.TotalMercExonerada = decTotalMercanciasExoneradas;
                resumenFactura.TotalMercExoneradaSpecified = true;
                resumenFactura.TotalMercanciasExentas = decTotalMercanciasExcentas;
                resumenFactura.TotalMercanciasExentasSpecified = true;
                resumenFactura.TotalServGravados = decTotalServiciosGrabados;
                resumenFactura.TotalServGravadosSpecified = true;
                resumenFactura.TotalServExonerado = decTotalServiciosExonerados;
                resumenFactura.TotalServExoneradoSpecified = true;
                resumenFactura.TotalServExentos = decTotalServiciosExcentos;
                resumenFactura.TotalServExentosSpecified = true;
                resumenFactura.TotalGravado = decTotalMercanciasGrabadas + decTotalServiciosGrabados;
                resumenFactura.TotalGravadoSpecified = true;
                resumenFactura.TotalExonerado = decTotalMercanciasExoneradas + decTotalServiciosExonerados;
                resumenFactura.TotalExoneradoSpecified = true;
                resumenFactura.TotalExento = decTotalMercanciasExcentas + decTotalServiciosExcentos;
                resumenFactura.TotalExentoSpecified = true;
                resumenFactura.TotalVenta = resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento;
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
                    throw new BusinessException("La empresa debe poseer una dirección de correo electrónico para ser notificada.");
                }
                else
                {
                    strCorreoNotificacion = empresa.CorreoNotificacion;
                }
                XmlDocument documentoXml = new XmlDocument();
                documentoXml.LoadXml(datosXml);
                if (documentoXml.DocumentElement.Name != "FacturaElectronica" && documentoXml.DocumentElement.Name != "NotaCreditoElectronica")
                    throw new BusinessException("El documento por aceptar no corresponde a una factura electrónica o nota de crédito electrónica. Por favor verifique. . .");
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
                            throw new BusinessException("No se encuentra el número de identificacion del EMISOR en el archivo XML.");
                    }
                }
                else
                    throw new BusinessException("No se encuentra el nodo EMISOR en el archivo XML.");
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
                            throw new BusinessException("No se encuentra el número de identificacion del RECEPTOR en el archivo XML.");
                    }
                    if (mensajeReceptor.NumeroCedulaReceptor != empresa.Identificacion)
                        throw new BusinessException("El número de identificación de la empresa no corresponde con el número de identificacion del RECEPTOR en el archivo XML.");
                }
                else
                    throw new BusinessException("No se encuentra el nodo RECEPTOR en el archivo XML.");
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
                TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdSucursal == intSucursal & x.IdTerminal == intTerminal).FirstOrDefault();
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