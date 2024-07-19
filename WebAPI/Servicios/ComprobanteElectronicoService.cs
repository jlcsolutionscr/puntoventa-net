using System.Text;
using System.Globalization;
using FirmaXadesNetCore.Signature;
using FirmaXadesNetCore;
using FirmaXadesNetCore.Signature.Parameters;
using FirmaXadesNetCore.Crypto;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.TiposDatosHacienda;
using LeandroSoftware.ServicioWeb.Utilitario;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public static class ComprobanteElectronicoService
    {
        private static HttpClient httpClient = new HttpClient();

        public static void ValidarToken(LeandroContext dbContext, CredencialesHacienda credenciales, string strServicioTokenURL, string strClientId)
        {
            TokenType nuevoToken;
            if (credenciales.AccessToken != null)
            {
                if (credenciales.EmitedAt != null)
                {
                    DateTime horaEmision = DateTime.Parse(credenciales.EmitedAt.ToString());
                    DateTime horaActual = Validador.ObtenerFechaHoraCostaRica();
                    if (horaEmision.AddSeconds((int)credenciales.ExpiresIn) < horaActual)
                    {
                        if (horaEmision.AddSeconds((int)credenciales.RefreshExpiresIn) < horaActual)
                        {
                            nuevoToken = ObtenerToken(strServicioTokenURL, strClientId, credenciales.UsuarioHacienda, credenciales.ClaveHacienda).Result;
                            credenciales.AccessToken = nuevoToken.access_token;
                            credenciales.ExpiresIn = nuevoToken.expires_in;
                            credenciales.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                            credenciales.RefreshToken = nuevoToken.refresh_token;
                            credenciales.EmitedAt = nuevoToken.emitedAt;
                            dbContext.NotificarModificacion(credenciales);
                            dbContext.Commit();
                        }
                        else
                        {
                            nuevoToken = RefrescarToken(strServicioTokenURL, strClientId, credenciales.RefreshToken).Result;
                            credenciales.AccessToken = nuevoToken.access_token;
                            credenciales.ExpiresIn = nuevoToken.expires_in;
                            credenciales.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                            credenciales.RefreshToken = nuevoToken.refresh_token;
                            credenciales.EmitedAt = nuevoToken.emitedAt;
                            dbContext.NotificarModificacion(credenciales);
                            dbContext.Commit();
                        }
                    }
                }
            }
            else
            {
                nuevoToken = ObtenerToken(strServicioTokenURL, strClientId, credenciales.UsuarioHacienda, credenciales.ClaveHacienda).Result;
                credenciales.AccessToken = nuevoToken.access_token;
                credenciales.ExpiresIn = nuevoToken.expires_in;
                credenciales.RefreshExpiresIn = nuevoToken.refresh_expires_in;
                credenciales.RefreshToken = nuevoToken.refresh_token;
                credenciales.EmitedAt = nuevoToken.emitedAt;
                dbContext.NotificarModificacion(credenciales);
                dbContext.Commit();
            }
        }

        public static async Task<TokenType> ObtenerToken(string strServicioTokenURL, string strClientId, string strUsuario, string strPassword)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", strClientId),
                new KeyValuePair<string, string>("username", strUsuario),
                new KeyValuePair<string, string>("password", strPassword)
            });
            HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioTokenURL + "/token", formContent);
            if (httpResponse.StatusCode != HttpStatusCode.OK) throw new Exception(httpResponse.ReasonPhrase);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            TokenType objToken = JsonConvert.DeserializeObject<TokenType>(responseContent);
            objToken.emitedAt = Validador.ObtenerFechaHoraCostaRica();
            return objToken;
        }

        private static async Task<TokenType> RefrescarToken(string strServicioTokenURL, string strClientId, string strRefreshToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("client_id", strClientId),
                new KeyValuePair<string, string>("refresh_token", strRefreshToken)
            });
            HttpResponseMessage httpResponse = await httpClient.PostAsync(strServicioTokenURL + "/token", formContent);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            TokenType objToken = JsonConvert.DeserializeObject<TokenType>(responseContent);
            objToken.emitedAt = Validador.ObtenerFechaHoraCostaRica();
            return objToken;
        }

        public static decimal ObtenerTipoCambioVenta(string strServicioURL, DateTime fechaConsulta)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage httpResponse = httpClient.GetAsync(strServicioURL).Result;
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string strRespuesta = httpResponse.Content.ReadAsStringAsync().Result;
                if (strRespuesta == "") throw new Exception("No fue posible obtener la respuesta del servicio de consulta del tipo de cambio del dolar");
                JObject datosJO = JObject.Parse(strRespuesta);
                if (datosJO.Property("venta") == null) throw new Exception("La respuesta del servicio de consulta del tipo de cambio del dolar no posee una entrada para el valor de 'Venta'");
                JObject venta = JObject.Parse(datosJO.Property("venta").Value.ToString());
                if (venta.Property("valor") == null) throw new Exception("La respuesta del servicio de consulta del tipo de cambio del dolar no posee una entrada para el campo 'Valor de venta'");
                try
                {
                    return Math.Round(decimal.Parse(venta.Property("valor").Value.ToString(), CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero);
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    throw new Exception("Error parseando el tipo de cambio: " + venta.Property("valor").Value.ToString() + ": " + errorMessage);
                }
            }
            else
            {
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(responseContent);
            }
        }

        public static string ObtenerInformacionContribuyente(string strServicioURL, string strIdentificacion)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage httpResponse = httpClient.GetAsync(strServicioURL + "?identificacion=" + strIdentificacion).Result;
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string strRespuesta = httpResponse.Content.ReadAsStringAsync().Result;
                if (strRespuesta == "") throw new Exception("No fue posible obtener la respuesta del servicio de consulta para la información del contribuyente");
                return strRespuesta;
            }
            else
            {
                string responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                throw new Exception(responseContent);
            }
        }

        public static DocumentoElectronico GenerarFacturaCompraElectronica(FacturaCompra facturaCompra, Empresa empresa, LeandroContext dbContext, decimal decTipoCambioDolar)
        {
            string strCorreoNotificacion = empresa.CorreoNotificacion;
            if (facturaCompra.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            if (facturaCompra.PorcentajeExoneracion > 13) throw new BusinessException("El porcentaje de exoneración no puede ser mayor al 13%");
            FacturaElectronicaCompra facturaElectronica = new FacturaElectronicaCompra
            {
                Clave = "",
                CodigoActividad = facturaCompra.CodigoActividad.ToString(),
                NumeroConsecutivo = "",
                FechaEmision = facturaCompra.Fecha
            };

            FacturaElectronicaCompraEmisorType emisor = new FacturaElectronicaCompraEmisorType();
            FacturaElectronicaCompraIdentificacionType identificacionReceptorType = new FacturaElectronicaCompraIdentificacionType
            {
                Tipo = (FacturaElectronicaCompraIdentificacionTypeTipo)facturaCompra.IdTipoIdentificacion,
                Numero = facturaCompra.IdentificacionEmisor
            };
            emisor.Identificacion = identificacionReceptorType;
            emisor.Nombre = facturaCompra.NombreEmisor;
            if (facturaCompra.NombreComercialEmisor.Length > 0) emisor.NombreComercial = facturaCompra.NombreComercialEmisor;
            if (facturaCompra.TelefonoEmisor.Length > 0)
            {
                FacturaElectronicaCompraTelefonoType telefonoType = new FacturaElectronicaCompraTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = facturaCompra.TelefonoEmisor
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = facturaCompra.CorreoElectronicoEmisor;
            FacturaElectronicaCompraUbicacionType ubicacionType = new FacturaElectronicaCompraUbicacionType
            {
                Provincia = facturaCompra.IdProvinciaEmisor.ToString(),
                Canton = facturaCompra.IdCantonEmisor.ToString("D2"),
                Distrito = facturaCompra.IdDistritoEmisor.ToString("D2"),
                Barrio = facturaCompra.IdBarrioEmisor.ToString("D2"),
                OtrasSenas = facturaCompra.DireccionEmisor
            };
            emisor.Ubicacion = ubicacionType;
            facturaElectronica.Emisor = emisor;
            FacturaElectronicaCompraReceptorType receptor = new FacturaElectronicaCompraReceptorType();
            FacturaElectronicaCompraIdentificacionType identificacionEmisorType = new FacturaElectronicaCompraIdentificacionType
            {
                Tipo = (FacturaElectronicaCompraIdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            receptor.Identificacion = identificacionEmisorType;
            receptor.Nombre = empresa.NombreEmpresa;
            receptor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                FacturaElectronicaCompraTelefonoType telefonoType = new FacturaElectronicaCompraTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                receptor.Telefono = telefonoType;
            }
            receptor.CorreoElectronico = empresa.CorreoNotificacion;
            ubicacionType = new FacturaElectronicaCompraUbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                Barrio = empresa.IdBarrio.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            receptor.Ubicacion = ubicacionType;
            facturaElectronica.Receptor = receptor;
            facturaElectronica.CondicionVenta = (FacturaElectronicaCompraCondicionVenta)facturaCompra.IdCondicionVenta - 1;
            if (facturaElectronica.CondicionVenta == FacturaElectronicaCompraCondicionVenta.Item02)
            {
                facturaElectronica.PlazoCredito = facturaCompra.PlazoCredito.ToString();
            }
            List<FacturaElectronicaCompraMedioPago> medioPagoList = new List<FacturaElectronicaCompraMedioPago>();
            FacturaElectronicaCompraMedioPago medioPago = FacturaElectronicaCompraMedioPago.Item01;
            medioPagoList.Add(medioPago);
            facturaElectronica.MedioPago = medioPagoList.ToArray();
            List<FacturaElectronicaCompraLineaDetalle> detalleServicioList = new List<FacturaElectronicaCompraLineaDetalle>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleFacturaCompra detalleFactura in facturaCompra.DetalleFacturaCompra)
            {
                decimal decSubtotal = 0;
                FacturaElectronicaCompraLineaDetalle lineaDetalle = new FacturaElectronicaCompraLineaDetalle
                {
                    NumeroLinea = detalleFactura.Linea.ToString(),
                    Codigo = detalleFactura.Codigo,
                    Cantidad = detalleFactura.Cantidad
                };
                if (detalleFactura.UnidadMedida == "Und")
                    lineaDetalle.UnidadMedida = FacturaElectronicaCompraUnidadMedidaType.Unid;
                else if (detalleFactura.UnidadMedida == "Sp")
                    lineaDetalle.UnidadMedida = FacturaElectronicaCompraUnidadMedidaType.Sp;
                else if (detalleFactura.UnidadMedida == "Spe")
                    lineaDetalle.UnidadMedida = FacturaElectronicaCompraUnidadMedidaType.Spe;
                else if (detalleFactura.UnidadMedida == "St")
                    lineaDetalle.UnidadMedida = FacturaElectronicaCompraUnidadMedidaType.St;
                else
                    lineaDetalle.UnidadMedida = FacturaElectronicaCompraUnidadMedidaType.Os;
                lineaDetalle.Detalle = detalleFactura.Descripcion;
                decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                decimal decTotalPorLinea = 0;
                decimal decMontoImpuestoPorLinea = 0;
                if (detalleFactura.PorcentajeIVA > 0)
                {
                    decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                    decimal decMontoExoneradoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                    FacturaElectronicaCompraImpuestoType impuestoType = new FacturaElectronicaCompraImpuestoType
                    {
                        Codigo = FacturaElectronicaCompraImpuestoTypeCodigo.Item01,
                        CodigoTarifa = (FacturaElectronicaCompraImpuestoTypeCodigoTarifa)detalleFactura.IdImpuesto - 1,
                        CodigoTarifaSpecified = true,
                        Tarifa = detalleFactura.PorcentajeIVA,
                        TarifaSpecified = true,
                        Monto = decMontoImpuestoPorLinea
                    };
                    if (facturaCompra.PorcentajeExoneracion > 0)
                    {
                        decimal decPorcentajeGravado = Math.Max(detalleFactura.PorcentajeIVA - facturaCompra.PorcentajeExoneracion, 0);
                        decimal decPorcentajeExonerado = detalleFactura.PorcentajeIVA - decPorcentajeGravado;
                        decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                        decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                        decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                        decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                        FacturaElectronicaCompraExoneracionType exoneracionType = new FacturaElectronicaCompraExoneracionType
                        {
                            TipoDocumento = (FacturaElectronicaCompraExoneracionTypeTipoDocumento)facturaCompra.IdTipoExoneracion - 1,
                            NumeroDocumento = facturaCompra.NumDocExoneracion,
                            NombreInstitucion = facturaCompra.NombreInstExoneracion,
                            FechaEmision = facturaCompra.FechaEmisionDoc,
                            PorcentajeExoneracion = facturaCompra.PorcentajeExoneracion.ToString("N0"),
                            MontoExoneracion = decMontoExoneracion
                        };
                        impuestoType.Exoneracion = exoneracionType;
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoNetoSpecified = true;
                    }
                    lineaDetalle.Impuesto = new FacturaElectronicaCompraImpuestoType[] { impuestoType };
                    decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                    if (detalleFactura.UnidadMedida == "Und")
                    {
                        decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                        decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                    }
                    else
                    {
                        decTotalServiciosGravados += decMontoGravadoPorLinea;
                        decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                    }
                    decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                }
                else
                {
                    decimal decMontoExcento = lineaDetalle.MontoTotal;
                    if (detalleFactura.UnidadMedida == "Und")
                        decTotalMercanciasExcentas += decMontoExcento;
                    else
                        decTotalServiciosExcentos += decMontoExcento;
                    decTotalPorLinea += decMontoExcento;
                }
                lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                detalleServicioList.Add(lineaDetalle);
            }
            facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
            FacturaElectronicaCompraResumenFactura resumenFactura = new FacturaElectronicaCompraResumenFactura();
            FacturaElectronicaCompraCodigoMonedaType codigoMonedaType = null;
            if (facturaCompra.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new FacturaElectronicaCompraCodigoMonedaType
                {
                    CodigoMoneda = FacturaElectronicaCompraCodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = decTipoCambioDolar
                };
            }
            else if (facturaCompra.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new FacturaElectronicaCompraCodigoMonedaType
                {
                    CodigoMoneda = FacturaElectronicaCompraCodigoMonedaTypeCodigoMoneda.CRC,
                    TipoCambio = 1
                };
            }
            resumenFactura.CodigoTipoMoneda = codigoMonedaType;
            if (facturaCompra.Descuento > 0)
            {
                resumenFactura.TotalDescuentos = facturaCompra.Descuento;
                resumenFactura.TotalDescuentosSpecified = true;
            }
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta - facturaCompra.Descuento, 2, MidpointRounding.AwayFromZero);
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            facturaElectronica.ResumenFactura = resumenFactura;
            if (facturaCompra.TextoAdicional != "")
            {
                FacturaElectronicaCompraOtros otros = new FacturaElectronicaCompraOtros();
                FacturaElectronicaCompraOtrosOtroTexto otrosTextos = new FacturaElectronicaCompraOtrosOtroTexto
                {
                    Value = facturaCompra.TextoAdicional
                };
                otros.OtroTexto = new FacturaElectronicaCompraOtrosOtroTexto[] { otrosTextos };
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
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, facturaCompra.IdSucursal, facturaCompra.IdTerminal, TipoDocumento.FacturaElectronicaCompra, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GenerarFacturaElectronica(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext, decimal decTipoCambioDolar)
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
            if (factura.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            FacturaElectronica facturaElectronica = new FacturaElectronica
            {
                Clave = "",
                CodigoActividad = factura.CodigoActividad.ToString(),
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
            if (empresa.Telefono1.Length > 0)
            {
                FacturaElectronicaTelefonoType telefonoType = new FacturaElectronicaTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
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
            List<FacturaElectronicaOtrosCargosType> detalleOtrosCargosList = new List<FacturaElectronicaOtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    FacturaElectronicaLineaDetalle lineaDetalle = new FacturaElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        Codigo = detalleFactura.Producto.CodigoClasificacion
                    };
                    FacturaElectronicaCodigoType codigoComercial = new FacturaElectronicaCodigoType
                    {
                        Tipo = FacturaElectronicaCodigoTypeTipo.Item01,
                        Codigo = detalleFactura.Producto.Codigo
                    };
                    lineaDetalle.CodigoComercial = new FacturaElectronicaCodigoType[] { codigoComercial };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = FacturaElectronicaUnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    decimal decMontoImpuestoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        FacturaElectronicaImpuestoType impuestoType = new FacturaElectronicaImpuestoType
                        {
                            Codigo = FacturaElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (FacturaElectronicaImpuestoTypeCodigoTarifa)intCodigoTarifa - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decimal decPorcentajeGravado = Math.Max(detalleFactura.PorcentajeIVA - factura.PorcentajeExoneracion, 0);
                            decimal decPorcentajeExonerado = detalleFactura.PorcentajeIVA - decPorcentajeGravado;
                            decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            FacturaElectronicaExoneracionType exoneracionType = new FacturaElectronicaExoneracionType
                            {
                                TipoDocumento = (FacturaElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString("N0"),
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                            lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                        } else {
                            decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        }
                        lineaDetalle.Impuesto = new FacturaElectronicaImpuestoType[] { impuestoType };
                        decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                            decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                        }
                        else
                        {
                            decTotalServiciosGravados += decMontoGravadoPorLinea;
                            decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                        }
                        decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                    }
                    else
                    {
                        decimal decMontoExcento = lineaDetalle.MontoTotal;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += decMontoExcento;
                        else
                            decTotalServiciosExcentos += decMontoExcento;
                        decTotalPorLinea += decMontoExcento;
                    }
                    lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                    detalleServicioList.Add(lineaDetalle);
                }
                else
                {
                    decimal decTotalPorLinea = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    FacturaElectronicaOtrosCargosType lineaOtrosCargos = new FacturaElectronicaOtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        Porcentaje = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumento = FacturaElectronicaOtrosCargosTypeTipoDocumento.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) facturaElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
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
            /*if (factura.Descuento > 0)
            {
                resumenFactura.TotalDescuentos = Math.Round(factura.Descuento, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalDescuentosSpecified = true;
            }*/
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta, 2, MidpointRounding.AwayFromZero);
            if (decTotalOtrosCargos > 0)
            {
                resumenFactura.TotalOtrosCargos = decTotalOtrosCargos;
                resumenFactura.TotalOtrosCargosSpecified = true;
            }
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + resumenFactura.TotalOtrosCargos + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            facturaElectronica.ResumenFactura = resumenFactura;
            if (factura.TextoAdicional != "")
            {
                FacturaElectronicaOtros otros = new FacturaElectronicaOtros();
                FacturaElectronicaOtrosOtroTexto otrosTextos = new FacturaElectronicaOtrosOtroTexto
                {
                    Value = factura.TextoAdicional
                };
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
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.FacturaElectronica, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GeneraTiqueteElectronico(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext, decimal decTipoCambioDolar)
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
            if (factura.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            TiqueteElectronico tiqueteElectronico = new TiqueteElectronico
            {
                Clave = "",
                CodigoActividad = factura.CodigoActividad.ToString(),
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
            if (empresa.Telefono1.Length > 0)
            {
                TiqueteElectronicoTelefonoType telefonoType = new TiqueteElectronicoTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
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
            List<TiqueteElectronicoOtrosCargosType> detalleOtrosCargosList = new List<TiqueteElectronicoOtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    TiqueteElectronicoLineaDetalle lineaDetalle = new TiqueteElectronicoLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        Codigo = detalleFactura.Producto.CodigoClasificacion
                    };
                    TiqueteElectronicoCodigoType codigoComercial = new TiqueteElectronicoCodigoType
                    {
                        Tipo = TiqueteElectronicoCodigoTypeTipo.Item01,
                        Codigo = detalleFactura.Producto.Codigo
                    };
                    lineaDetalle.CodigoComercial = new TiqueteElectronicoCodigoType[] { codigoComercial };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = TiqueteElectronicoUnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = TiqueteElectronicoUnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = TiqueteElectronicoUnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    decimal decMontoImpuestoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        TiqueteElectronicoImpuestoType impuestoType = new TiqueteElectronicoImpuestoType
                        {
                            Codigo = TiqueteElectronicoImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (TiqueteElectronicoImpuestoTypeCodigoTarifa)intCodigoTarifa - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decimal decPorcentajeGravado = Math.Max(detalleFactura.PorcentajeIVA - factura.PorcentajeExoneracion, 0);
                            decimal decPorcentajeExonerado = detalleFactura.PorcentajeIVA - decPorcentajeGravado;
                            decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            TiqueteElectronicoExoneracionType exoneracionType = new TiqueteElectronicoExoneracionType
                            {
                                TipoDocumento = (TiqueteElectronicoExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString("N0"),
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                            lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                        } else {
                            decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        }
                        lineaDetalle.Impuesto = new TiqueteElectronicoImpuestoType[] { impuestoType };
                        decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                            decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                        }
                        else
                        {
                            decTotalServiciosGravados += decMontoGravadoPorLinea;
                            decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                        }
                        decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                    }
                    else
                    {
                        decimal decMontoExcento = lineaDetalle.MontoTotal;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += decMontoExcento;
                        else
                            decTotalServiciosExcentos += decMontoExcento;
                        decTotalPorLinea += decMontoExcento;
                    }
                    lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                    detalleServicioList.Add(lineaDetalle);
                }
                else
                {
                    decimal decTotalPorLinea = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    TiqueteElectronicoOtrosCargosType lineaOtrosCargos = new TiqueteElectronicoOtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        Porcentaje = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumento = TiqueteElectronicoOtrosCargosTypeTipoDocumento.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            tiqueteElectronico.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) tiqueteElectronico.OtrosCargos = detalleOtrosCargosList.ToArray();
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
            /*if (factura.Descuento > 0)
            {
                resumenFactura.TotalDescuentos = Math.Round(factura.Descuento, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalDescuentosSpecified = true;
            }*/
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta, 2, MidpointRounding.AwayFromZero);
            if (decTotalOtrosCargos > 0)
            {
                resumenFactura.TotalOtrosCargos = decTotalOtrosCargos;
                resumenFactura.TotalOtrosCargosSpecified = true;
            }
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + resumenFactura.TotalOtrosCargos + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            tiqueteElectronico.ResumenFactura = resumenFactura;
            if (factura.TextoAdicional != "")
            {
                TiqueteElectronicoOtros otros = new TiqueteElectronicoOtros();
                TiqueteElectronicoOtrosOtroTexto otrosTextos = new TiqueteElectronicoOtrosOtroTexto
                {
                    Value = factura.TextoAdicional
                };
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
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.TiqueteElectronico, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GenerarNotaDeCreditoElectronica(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext, decimal decTipoCambioDolar)
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
            if (factura.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
            {
                Clave = "",
                CodigoActividad = factura.CodigoActividad.ToString(),
                NumeroConsecutivo = "",
                FechaEmision = Validador.ObtenerFechaHoraCostaRica()
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
            if (empresa.Telefono1.Length > 0)
            {
                NotaCreditoElectronicaTelefonoType telefonoType = new NotaCreditoElectronicaTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
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
            List<NotaCreditoElectronicaOtrosCargosType> detalleOtrosCargosList = new List<NotaCreditoElectronicaOtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        Codigo = detalleFactura.Producto.CodigoClasificacion
                    };
                    NotaCreditoElectronicaCodigoType codigoComercial = new NotaCreditoElectronicaCodigoType
                    {
                        Tipo = NotaCreditoElectronicaCodigoTypeTipo.Item01,
                        Codigo = detalleFactura.Producto.Codigo
                    };
                    lineaDetalle.CodigoComercial = new NotaCreditoElectronicaCodigoType[] { codigoComercial };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    decimal decMontoImpuestoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        NotaCreditoElectronicaImpuestoType impuestoType = new NotaCreditoElectronicaImpuestoType
                        {
                            Codigo = NotaCreditoElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (NotaCreditoElectronicaImpuestoTypeCodigoTarifa)intCodigoTarifa - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decimal decPorcentajeGravado = Math.Max(detalleFactura.PorcentajeIVA - factura.PorcentajeExoneracion, 0);
                            decimal decPorcentajeExonerado = detalleFactura.PorcentajeIVA - decPorcentajeGravado;
                            decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalleFactura.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            NotaCreditoElectronicaExoneracionType exoneracionType = new NotaCreditoElectronicaExoneracionType
                            {
                                TipoDocumento = (NotaCreditoElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString("N0"),
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                            lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                        } else {
                            decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        }
                        lineaDetalle.Impuesto = new NotaCreditoElectronicaImpuestoType[] { impuestoType };
                        decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                            decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                        }
                        else
                        {
                            decTotalServiciosGravados += decMontoGravadoPorLinea;
                            decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                        }
                        decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                    }
                    else
                    {
                        decimal decMontoExcento = lineaDetalle.MontoTotal;
                        if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += decMontoExcento;
                        else
                            decTotalServiciosExcentos += decMontoExcento;
                        decTotalPorLinea += decMontoExcento;
                    }
                    lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                    detalleServicioList.Add(lineaDetalle);
                }
                else
                {
                    decimal decTotalPorLinea = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    NotaCreditoElectronicaOtrosCargosType lineaOtrosCargos = new NotaCreditoElectronicaOtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        Porcentaje = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumento = NotaCreditoElectronicaOtrosCargosTypeTipoDocumento.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) notaCreditoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
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
            /*if (factura.Descuento > 0)
            {
                resumenFactura.TotalDescuentos = Math.Round(factura.Descuento, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalDescuentosSpecified = true;
            }*/
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta, 2, MidpointRounding.AwayFromZero);
            if (decTotalOtrosCargos > 0)
            {
                resumenFactura.TotalOtrosCargos = decTotalOtrosCargos;
                resumenFactura.TotalOtrosCargosSpecified = true;
            }
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + resumenFactura.TotalOtrosCargos + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            notaCreditoElectronica.ResumenFactura = resumenFactura;
            NotaCreditoElectronicaInformacionReferencia informacionReferencia = new NotaCreditoElectronicaInformacionReferencia
            {
                TipoDoc = NotaCreditoElectronicaInformacionReferenciaTipoDoc.Item01,
                Numero = factura.IdDocElectronico,
                FechaEmision = factura.Fecha,
                Codigo = NotaCreditoElectronicaInformacionReferenciaCodigo.Item01,
                Razon = "Anulación del documento factura electrónica con la respectiva clave númerica."
            };
            notaCreditoElectronica.InformacionReferencia = new NotaCreditoElectronicaInformacionReferencia[] { informacionReferencia };
            if (factura.TextoAdicional != "")
            {
                NotaCreditoElectronicaOtros otros = new NotaCreditoElectronicaOtros();
                NotaCreditoElectronicaOtrosOtroTexto otrosTextos = new NotaCreditoElectronicaOtrosOtroTexto
                {
                    Value = factura.TextoAdicional
                };
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
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.NotaCreditoElectronica, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GenerarNotaDeCreditoElectronicaParcial(DevolucionCliente devolucion, Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext, decimal decTipoCambioDolar)
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
            if (factura.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
            {
                Clave = "",
                CodigoActividad = factura.CodigoActividad.ToString(),
                NumeroConsecutivo = "",
                FechaEmision = devolucion.Fecha
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
            if (empresa.Telefono1.Length > 0)
            {
                NotaCreditoElectronicaTelefonoType telefonoType = new NotaCreditoElectronicaTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
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
            if (devolucion.IdCliente > 1)
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
                notaCreditoElectronica.Receptor = receptor;
            }
            notaCreditoElectronica.CondicionVenta = NotaCreditoElectronicaCondicionVenta.Item01;
            List<NotaCreditoElectronicaMedioPago> medioPagoList = new List<NotaCreditoElectronicaMedioPago>();
            NotaCreditoElectronicaMedioPago medioPago = NotaCreditoElectronicaMedioPago.Item99;
            medioPagoList.Add(medioPago);
            notaCreditoElectronica.MedioPago = medioPagoList.ToArray();
            List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
            List<NotaCreditoElectronicaOtrosCargosType> detalleOtrosCargosList = new List<NotaCreditoElectronicaOtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleDevolucionCliente detalle in devolucion.DetalleDevolucionCliente)
            {
                if (detalle.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        Codigo = detalle.Producto.CodigoClasificacion
                    };
                    NotaCreditoElectronicaCodigoType codigoComercial = new NotaCreditoElectronicaCodigoType
                    {
                        Tipo = NotaCreditoElectronicaCodigoTypeTipo.Item01,
                        Codigo = detalle.Producto.Codigo
                    };
                    lineaDetalle.CodigoComercial = new NotaCreditoElectronicaCodigoType[] { codigoComercial };
                    lineaDetalle.Cantidad = detalle.Cantidad;
                    if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Unid;
                    else if (detalle.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = NotaCreditoElectronicaUnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalle.Producto.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalle.PrecioVenta * detalle.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    decimal decMontoImpuestoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    if (!detalle.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalle.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        int intCodigoTarifa = detalle.Producto.IdImpuesto;
                        NotaCreditoElectronicaImpuestoType impuestoType = new NotaCreditoElectronicaImpuestoType
                        {
                            Codigo = NotaCreditoElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (NotaCreditoElectronicaImpuestoTypeCodigoTarifa)intCodigoTarifa - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalle.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decimal decPorcentajeGravado = Math.Max(detalle.PorcentajeIVA - factura.PorcentajeExoneracion, 0);
                            decimal decPorcentajeExonerado = detalle.PorcentajeIVA - decPorcentajeGravado;
                            decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            NotaCreditoElectronicaExoneracionType exoneracionType = new NotaCreditoElectronicaExoneracionType
                            {
                                TipoDocumento = (NotaCreditoElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString("N0"),
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                            lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                        } else {
                            decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        }
                        lineaDetalle.Impuesto = new NotaCreditoElectronicaImpuestoType[] { impuestoType };
                        decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                        if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                            decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                        }
                        else
                        {
                            decTotalServiciosGravados += decMontoGravadoPorLinea;
                            decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                        }
                        decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                    }
                    else
                    {
                        decimal decMontoExcento = lineaDetalle.MontoTotal;
                        if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += decMontoExcento;
                        else
                            decTotalServiciosExcentos += decMontoExcento;
                        decTotalPorLinea += decMontoExcento;
                    }
                    lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                    detalleServicioList.Add(lineaDetalle);
                }
                else
                {
                    decimal decTotalPorLinea = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    NotaCreditoElectronicaOtrosCargosType lineaOtrosCargos = new NotaCreditoElectronicaOtrosCargosType
                    {
                        Detalle = detalle.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        Porcentaje = detalle.Producto.PrecioVenta1,
                        TipoDocumento = NotaCreditoElectronicaOtrosCargosTypeTipoDocumento.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += detalle.PrecioVenta;
                }
            }
            notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) notaCreditoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
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
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta, 2, MidpointRounding.AwayFromZero);
            if (decTotalOtrosCargos > 0)
            {
                resumenFactura.TotalOtrosCargos = decTotalOtrosCargos;
                resumenFactura.TotalOtrosCargosSpecified = true;
            }
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + resumenFactura.TotalOtrosCargos + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            notaCreditoElectronica.ResumenFactura = resumenFactura;
            NotaCreditoElectronicaInformacionReferencia informacionReferencia = new NotaCreditoElectronicaInformacionReferencia
            {
                TipoDoc = NotaCreditoElectronicaInformacionReferenciaTipoDoc.Item01,
                Numero = factura.IdDocElectronico,
                FechaEmision = factura.Fecha,
                Codigo = NotaCreditoElectronicaInformacionReferenciaCodigo.Item02,
                Razon = "Ajuste de monto de factura electrónica por devolución de mercancía."
            };
            notaCreditoElectronica.InformacionReferencia = new NotaCreditoElectronicaInformacionReferencia[] { informacionReferencia };
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
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.NotaCreditoElectronica, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GenerarNotaDeDebitoElectronicaParcial(DevolucionCliente devolucion, Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext, decimal decTipoCambioDolar)
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
            if (factura.CodigoActividad == 0) throw new BusinessException("Debe ingresar el código de actividad económica en el mantenimiento de la empresa.");
            NotaDebitoElectronica NotaDebitoElectronica = new NotaDebitoElectronica
            {
                Clave = "",
                CodigoActividad = factura.CodigoActividad.ToString(),
                NumeroConsecutivo = "",
                FechaEmision = Validador.ObtenerFechaHoraCostaRica()
            };
            NotaDebitoElectronicaEmisorType emisor = new NotaDebitoElectronicaEmisorType();
            NotaDebitoElectronicaIdentificacionType identificacionEmisorType = new NotaDebitoElectronicaIdentificacionType
            {
                Tipo = (NotaDebitoElectronicaIdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                NotaDebitoElectronicaTelefonoType telefonoType = new NotaDebitoElectronicaTelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = empresa.CorreoNotificacion;
            NotaDebitoElectronicaUbicacionType ubicacionType = new NotaDebitoElectronicaUbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                Barrio = empresa.IdBarrio.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            NotaDebitoElectronica.Emisor = emisor;
            if (devolucion.IdCliente > 1)
            {
                NotaDebitoElectronicaReceptorType receptor = new NotaDebitoElectronicaReceptorType();
                NotaDebitoElectronicaIdentificacionType identificacionReceptorType = new NotaDebitoElectronicaIdentificacionType
                {
                    Tipo = (NotaDebitoElectronicaIdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                    Numero = cliente.Identificacion
                };
                receptor.Identificacion = identificacionReceptorType;
                receptor.Nombre = cliente.Nombre;
                if (cliente.NombreComercial.Length > 0)
                    receptor.NombreComercial = cliente.NombreComercial;
                if (cliente.Telefono.Length > 0)
                {
                    NotaDebitoElectronicaTelefonoType telefonoType = new NotaDebitoElectronicaTelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = cliente.Telefono
                    };
                    receptor.Telefono = telefonoType;
                }
                if (cliente.Fax.Length > 0)
                {
                    NotaDebitoElectronicaTelefonoType faxType = new NotaDebitoElectronicaTelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = cliente.Fax
                    };
                    receptor.Fax = faxType;
                }
                receptor.CorreoElectronico = cliente.CorreoElectronico;
                NotaDebitoElectronica.Receptor = receptor;
            }
            NotaDebitoElectronica.CondicionVenta = NotaDebitoElectronicaCondicionVenta.Item01;
            List<NotaDebitoElectronicaMedioPago> medioPagoList = new List<NotaDebitoElectronicaMedioPago>();
            NotaDebitoElectronicaMedioPago medioPago = NotaDebitoElectronicaMedioPago.Item99;
            medioPagoList.Add(medioPago);
            NotaDebitoElectronica.MedioPago = medioPagoList.ToArray();
            List<NotaDebitoElectronicaLineaDetalle> detalleServicioList = new List<NotaDebitoElectronicaLineaDetalle>();
            List<NotaDebitoElectronicaOtrosCargosType> detalleOtrosCargosList = new List<NotaDebitoElectronicaOtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            foreach (DetalleDevolucionCliente detalle in devolucion.DetalleDevolucionCliente)
            {
                if (detalle.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaDebitoElectronicaLineaDetalle lineaDetalle = new NotaDebitoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        Codigo = detalle.Producto.CodigoClasificacion
                    };
                    NotaDebitoElectronicaCodigoType codigoComercial = new NotaDebitoElectronicaCodigoType
                    {
                        Tipo = NotaDebitoElectronicaCodigoTypeTipo.Item01,
                        Codigo = detalle.Producto.Codigo
                    };
                    lineaDetalle.CodigoComercial = new NotaDebitoElectronicaCodigoType[] { codigoComercial };
                    lineaDetalle.Cantidad = detalle.Cantidad;
                    if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = NotaDebitoElectronicaUnidadMedidaType.Unid;
                    else if (detalle.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = NotaDebitoElectronicaUnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = NotaDebitoElectronicaUnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalle.Producto.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalle.PrecioVenta * detalle.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    decimal decMontoImpuestoPorLinea = 0;
                    decimal decMontoImpuestoNetoPorLinea = 0;
                    if (!detalle.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalle.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        int intCodigoTarifa = detalle.Producto.IdImpuesto;
                        NotaDebitoElectronicaImpuestoType impuestoType = new NotaDebitoElectronicaImpuestoType
                        {
                            Codigo = NotaDebitoElectronicaImpuestoTypeCodigo.Item01,
                            CodigoTarifa = (NotaDebitoElectronicaImpuestoTypeCodigoTarifa)intCodigoTarifa - 1,
                            CodigoTarifaSpecified = true,
                            Tarifa = detalle.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        if (factura.PorcentajeExoneracion > 0)
                        {
                            decimal decPorcentajeGravado = Math.Max(detalle.PorcentajeIVA - factura.PorcentajeExoneracion, 0);
                            decimal decPorcentajeExonerado = detalle.PorcentajeIVA - decPorcentajeGravado;
                            decMontoGravadoPorLinea = Math.Round(decSubtotal * decPorcentajeGravado * 100 / detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoExoneradoPorLinea = Math.Round(decSubtotal * decPorcentajeExonerado * 100 / detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decMontoImpuestoNetoPorLinea = Math.Round(decMontoGravadoPorLinea * detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            decimal decMontoExoneracion = Math.Round(decMontoExoneradoPorLinea * detalle.PorcentajeIVA / 100, 2, MidpointRounding.AwayFromZero);
                            NotaDebitoElectronicaExoneracionType exoneracionType = new NotaDebitoElectronicaExoneracionType
                            {
                                TipoDocumento = (NotaDebitoElectronicaExoneracionTypeTipoDocumento)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = factura.NombreInstExoneracion,
                                FechaEmision = factura.FechaEmisionDoc,
                                PorcentajeExoneracion = factura.PorcentajeExoneracion.ToString("N0"),
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                            lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                            lineaDetalle.ImpuestoNetoSpecified = true;
                        } else {
                            decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        }
                        lineaDetalle.Impuesto = new NotaDebitoElectronicaImpuestoType[] { impuestoType };
                        decTotalImpuestos += decMontoImpuestoNetoPorLinea;
                        if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decTotalMercanciasGravadas += decMontoGravadoPorLinea;
                            decTotalMercanciasExoneradas += decMontoExoneradoPorLinea;
                        }
                        else
                        {
                            decTotalServiciosGravados += decMontoGravadoPorLinea;
                            decTotalServiciosExonerados += decMontoExoneradoPorLinea;
                        }
                        decTotalPorLinea = lineaDetalle.SubTotal + decMontoImpuestoNetoPorLinea;
                    }
                    else
                    {
                        decimal decMontoExcento = lineaDetalle.MontoTotal;
                        if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                            decTotalMercanciasExcentas += decMontoExcento;
                        else
                            decTotalServiciosExcentos += decMontoExcento;
                        decTotalPorLinea += decMontoExcento;
                    }
                    lineaDetalle.MontoTotalLinea = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                    detalleServicioList.Add(lineaDetalle);
                }
                else
                {
                    decimal decTotalPorLinea = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    NotaDebitoElectronicaOtrosCargosType lineaOtrosCargos = new NotaDebitoElectronicaOtrosCargosType
                    {
                        Detalle = detalle.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        Porcentaje = detalle.Producto.PrecioVenta1,
                        TipoDocumento = NotaDebitoElectronicaOtrosCargosTypeTipoDocumento.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += detalle.PrecioVenta;
                }
            }
            NotaDebitoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) NotaDebitoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
            NotaDebitoElectronicaResumenFactura resumenFactura = new NotaDebitoElectronicaResumenFactura();
            NotaDebitoElectronicaCodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new NotaDebitoElectronicaCodigoMonedaType
                {
                    CodigoMoneda = NotaDebitoElectronicaCodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = decTipoCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new NotaDebitoElectronicaCodigoMonedaType
                {
                    CodigoMoneda = NotaDebitoElectronicaCodigoMonedaTypeCodigoMoneda.CRC,
                    TipoCambio = 1
                };
            }
            resumenFactura.CodigoTipoMoneda = codigoMonedaType;
            resumenFactura.TotalMercanciasGravadas = Math.Round(decTotalMercanciasGravadas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasGravadasSpecified = true;
            resumenFactura.TotalMercExonerada = Math.Round(decTotalMercanciasExoneradas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercExoneradaSpecified = true;
            resumenFactura.TotalMercanciasExentas = Math.Round(decTotalMercanciasExcentas, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalMercanciasExentasSpecified = true;
            resumenFactura.TotalServGravados = Math.Round(decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServGravadosSpecified = true;
            resumenFactura.TotalServExonerado = Math.Round(decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExoneradoSpecified = true;
            resumenFactura.TotalServExentos = Math.Round(decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalServExentosSpecified = true;
            resumenFactura.TotalGravado = Math.Round(decTotalMercanciasGravadas + decTotalServiciosGravados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalGravadoSpecified = true;
            resumenFactura.TotalExonerado = Math.Round(decTotalMercanciasExoneradas + decTotalServiciosExonerados, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExoneradoSpecified = true;
            resumenFactura.TotalExento = Math.Round(decTotalMercanciasExcentas + decTotalServiciosExcentos, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalExentoSpecified = true;
            resumenFactura.TotalVenta = Math.Round(resumenFactura.TotalGravado + resumenFactura.TotalExonerado + resumenFactura.TotalExento, 2, MidpointRounding.AwayFromZero);
            resumenFactura.TotalVentaNeta = Math.Round(resumenFactura.TotalVenta, 2, MidpointRounding.AwayFromZero);
            if (decTotalOtrosCargos > 0)
            {
                resumenFactura.TotalOtrosCargos = decTotalOtrosCargos;
                resumenFactura.TotalOtrosCargosSpecified = true;
            }
            if (decTotalImpuestos > 0)
            {
                resumenFactura.TotalImpuesto = Math.Round(decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
                resumenFactura.TotalImpuestoSpecified = true;
            }
            resumenFactura.TotalComprobante = Math.Round(resumenFactura.TotalVentaNeta + resumenFactura.TotalOtrosCargos + decTotalImpuestos, 2, MidpointRounding.AwayFromZero);
            NotaDebitoElectronica.ResumenFactura = resumenFactura;
            NotaDebitoElectronicaInformacionReferencia informacionReferencia = new NotaDebitoElectronicaInformacionReferencia
            {
                TipoDoc = NotaDebitoElectronicaInformacionReferenciaTipoDoc.Item03,
                Numero = devolucion.IdDocElectronico,
                FechaEmision = devolucion.Fecha,
                Codigo = NotaDebitoElectronicaInformacionReferenciaCodigo.Item01,
                Razon = "Anulación de devolucin de mercancía de factura electrónica con la respectiva clave númerica."
            };
            NotaDebitoElectronica.InformacionReferencia = new NotaDebitoElectronicaInformacionReferencia[] { informacionReferencia };
            XmlDocument documentoXml = new XmlDocument();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            XmlSerializer serializer = new XmlSerializer(NotaDebitoElectronica.GetType());
            using (MemoryStream msDatosXML = new MemoryStream())
            using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
            {
                serializer.Serialize(writer, NotaDebitoElectronica);
                msDatosXML.Position = 0;
                documentoXml.Load(msDatosXML);
            }
            return RegistrarDocumentoElectronico(empresa, documentoXml, null, dbContext, factura.IdSucursal, factura.IdTerminal, TipoDocumento.NotaDebitoElectronica, false, strCorreoNotificacion);
        }

        public static DocumentoElectronico GeneraMensajeReceptor(string datosXml, Empresa empresa, LeandroContext dbContext, int intSucursal, int intTerminal, int intMensaje, bool bolIvaAcreditable)
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
            if (documentoXml.DocumentElement.Name != "FacturaElectronica" && documentoXml.DocumentElement.Name != "NotaCreditoElectronica" && documentoXml.DocumentElement.Name != "NotaDebitoElectronica")
                throw new BusinessException("El documento por aceptar no corresponde a una factura electrónica, nota de débito electrónica o nota de crédito electrónica. Por favor verifique. . .");
            string strClaveNumerica = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText;
            DocumentoElectronico documentoExistente = dbContext.DocumentoElectronicoRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.ClaveNumerica == strClaveNumerica);
            if (documentoExistente != null) throw new BusinessException("El documento electrónico con clave " + strClaveNumerica + " ya se encuentra registrado en el sistema. . .");
            decimal decTotalComprobante = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            DateTime fechaEmisionUTC = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmision").Item(0).InnerText, CultureInfo.InvariantCulture).ToUniversalTime();
            DateTime fechaEmisionDoc = TimeZoneInfo.ConvertTimeFromUtc(fechaEmisionUTC, cstZone);
            MensajeReceptor mensajeReceptor = new MensajeReceptor
            {
                Clave = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,
                FechaEmisionDoc = fechaEmisionDoc,
                Mensaje = (MensajeReceptorMensaje)intMensaje,
                DetalleMensaje = "Mensaje de receptor con estado: " + (intMensaje == 0 ? "Aceptado" : intMensaje == 1 ? "Aceptado parcialmente" : "Rechazado"),
                TotalFactura = decTotalComprobante,
                CondicionImpuesto = MensajeReceptorCondicionImpuesto.Item01,
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
                        throw new BusinessException("No se encuentra el número de identificación del EMISOR en el archivo XML.");
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
                        throw new BusinessException("No se encuentra el número de identificación del RECEPTOR en el archivo XML.");
                }
                if (mensajeReceptor.NumeroCedulaReceptor != empresa.Identificacion)
                    throw new BusinessException("El número de identificación de la empresa no corresponde con el número de identificación del RECEPTOR en el archivo XML.");
            }
            else
                throw new BusinessException("No se encuentra el nodo RECEPTOR en el archivo XML.");
            if (documentoXml.GetElementsByTagName("TotalImpuesto").Count > 0)
            {
                decimal decMontoImpuesto = decimal.Parse(documentoXml.GetElementsByTagName("TotalImpuesto").Item(0).InnerText, CultureInfo.InvariantCulture);
                mensajeReceptor.MontoTotalImpuesto = decMontoImpuesto;
                mensajeReceptor.MontoTotalImpuestoSpecified = true;
                if (bolIvaAcreditable)
                {
                    mensajeReceptor.MontoTotalImpuestoAcreditar = decMontoImpuesto;
                    mensajeReceptor.MontoTotalDeGastoAplicable = decTotalComprobante - decMontoImpuesto;
                }
                else
                {
                    mensajeReceptor.MontoTotalImpuestoAcreditar = 0;
                    mensajeReceptor.MontoTotalDeGastoAplicable = decTotalComprobante;
                }
                mensajeReceptor.MontoTotalImpuestoAcreditarSpecified = true;
                mensajeReceptor.MontoTotalDeGastoAplicableSpecified = true;
            }
            else
            {
                mensajeReceptor.MontoTotalDeGastoAplicable = decTotalComprobante;
                mensajeReceptor.MontoTotalDeGastoAplicableSpecified = true;
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
            DocumentoElectronico documento = RegistrarDocumentoElectronico(empresa, mensajeReceptorXml, documentoXml, dbContext, intSucursal, intTerminal, tipoDoc, bolIvaAcreditable, strCorreoNotificacion);
            return documento;
        }

        public static DocumentoElectronico RegistrarDocumentoElectronico(Empresa empresa, XmlDocument documentoXml, XmlDocument? documentoOriXml, LeandroContext dbContext, int intSucursal, int intTerminal, TipoDocumento tipoDocumento, bool bolIvaAcreditable, string strCorreoNotificacion)
        {
            DateTime horaActual = Validador.ObtenerFechaHoraCostaRica();
            int intMesEnCurso = horaActual.Month;
            int intAnnioEnCurso = horaActual.Year;
            var documentosRecepcion = new[] { TipoDocumento.MensajeReceptorAceptado, TipoDocumento.MensajeReceptorAceptadoParcial, TipoDocumento.MensajeReceptorRechazado };
            bool esMensajeReceptor = documentosRecepcion.Contains(tipoDocumento);
            CantFEMensualEmpresa cantiFacturasMensual = dbContext.CantFEMensualEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdMes == intMesEnCurso & x.IdAnio == intAnnioEnCurso).FirstOrDefault();
            int intCantidadActual = cantiFacturasMensual == null ? 0 : cantiFacturasMensual.CantidadDoc;
            int intCantidadPlan = empresa.PlanFacturacion.CantidadDocumentos + empresa.CantidadDisponible;
            if (intCantidadPlan <= intCantidadActual) throw new BusinessException("La cantidad de documentos disponibles para su plan de facturación ha sido agotado. Contacte con su proveedor del servicio. . .");
            if (intCantidadActual < empresa.PlanFacturacion.CantidadDocumentos)
            {
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
            else
            {
                empresa.CantidadDisponible -= 1;
                dbContext.NotificarModificacion(empresa);
            }
            string strTipoIdentificacionEmisor = "";
            string strIdentificacionEmisor = "";
            string strTipoIdentificacionReceptor = "";
            string strIdentificacionReceptor = "";
            DateTime fechaEmision;
            string strConsucutivo = "";
            string strClaveNumerica = "";
            string strNombreReceptor = "CLIENTE DE CONTADO";
            decimal decTotal = 0;
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
                    strNombreReceptor = documentoXml.GetElementsByTagName("Receptor").Item(0)["Nombre"].InnerText;
                }
                if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                    decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                if (tipoDocumento == TipoDocumento.FacturaElectronicaCompra)
                    if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                        strNombreReceptor = documentoXml.GetElementsByTagName("Emisor").Item(0)["Nombre"].InnerText;
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
                if (documentoOriXml.GetElementsByTagName("TotalComprobante").Count > 0)
                    decTotal = decimal.Parse(documentoOriXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                if (documentoOriXml.GetElementsByTagName("Emisor").Count > 0)
                    strNombreReceptor = documentoOriXml.GetElementsByTagName("Emisor").Item(0)["Nombre"].InnerText;
            }
            int intTipoDocumentoElectronico = (int)tipoDocumento;
            int intIdConsecutivo = 1;
            TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa & x.IdSucursal == intSucursal & x.IdTerminal == intTerminal).FirstOrDefault();
            switch (tipoDocumento)
            {
                case TipoDocumento.FacturaElectronica:
                    intIdConsecutivo = terminal.UltimoDocFE + 1;
                    terminal.UltimoDocFE = intIdConsecutivo;
                    break;
                case TipoDocumento.NotaDebitoElectronica:
                    intIdConsecutivo = terminal.UltimoDocND + 1;
                    terminal.UltimoDocND = intIdConsecutivo;
                    break;
                case TipoDocumento.NotaCreditoElectronica:
                    intIdConsecutivo = terminal.UltimoDocNC + 1;
                    terminal.UltimoDocNC = intIdConsecutivo;
                    break;
                case TipoDocumento.TiqueteElectronico:
                    intIdConsecutivo = terminal.UltimoDocTE + 1;
                    terminal.UltimoDocTE = intIdConsecutivo;
                    break;
                case TipoDocumento.MensajeReceptorAceptado:
                case TipoDocumento.MensajeReceptorAceptadoParcial:
                case TipoDocumento.MensajeReceptorRechazado:
                    intIdConsecutivo = terminal.UltimoDocMR + 1;
                    terminal.UltimoDocMR = intIdConsecutivo;
                    break;
                case TipoDocumento.FacturaElectronicaCompra:
                    intIdConsecutivo = terminal.UltimoDocFEC + 1;
                    terminal.UltimoDocFEC = intIdConsecutivo;
                    break;
            }
            strConsucutivo = intSucursal.ToString("D3") + intTerminal.ToString("D5") + intTipoDocumentoElectronico.ToString("D2") + intIdConsecutivo.ToString("D10");
            if (!esMensajeReceptor)
            {
                Random rnd = new Random();
                int intRandom = rnd.Next(10000000, 99999999);
                if (tipoDocumento == TipoDocumento.FacturaElectronicaCompra)
                    strClaveNumerica = "506" + fechaEmision.Day.ToString().PadLeft(2, '0') + fechaEmision.ToString("MM") + fechaEmision.ToString("yy") + strIdentificacionReceptor + strConsucutivo + "1" + intRandom.ToString();
                else
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
                SigningDate = horaActual,
                SignaturePackaging = SignaturePackaging.ENVELOPED
            };
            CredencialesHacienda credenciales = dbContext.CredencialesHaciendaRepository.Find(empresa.IdEmpresa);
            if (credenciales == null) throw new BusinessException("La empresa no tiene registrado los credenciales ATV para generar documentos electrónicos");
            X509Certificate2 uidCert;
            try
            {
                uidCert = new X509Certificate2(credenciales.Certificado, credenciales.PinCertificado, X509KeyStorageFlags.MachineKeySet);
            }
            catch (Exception)
            {
                throw new BusinessException("No se logró abrir la llave criptográfica con el pin suministrado. Por favor verifique la información registrada");
            }
            if (uidCert.NotAfter <= horaActual) throw new BusinessException("La llave criptográfica para la firma del documento electrónico se encuentra vencida. Por favor reemplace su llave criptográfica para poder emitir documentos electrónicos");
            using (Signer signer2 = signatureParameters.Signer = new Signer(uidCert))
            using (MemoryStream smDatos = new MemoryStream(mensajeEncoded))
            {
                signatureDocument = xadesService.Sign(smDatos, signatureParameters);
            }
            // Almacenaje del documento en base de datos
            byte[] signedDataEncoded = Encoding.UTF8.GetBytes(signatureDocument.Document.OuterXml);
            byte[] documentoOriEncoded = new byte[0];
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
                EsIvaAcreditable = bolIvaAcreditable ? "S" : "N",
                NombreReceptor = strNombreReceptor,
                EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado,
                CorreoNotificacion = strCorreoNotificacion,
                DatosDocumento = signedDataEncoded,
                DatosDocumentoOri = documentoOriEncoded,
                Respuesta = new byte[0],
                Total = decTotal
            };
            dbContext.DocumentoElectronicoRepository.Add(documento);
            return documento;
        }

        public static async Task EnviarDocumentoElectronico(string AccessToken, DocumentoElectronico documento, IConfiguracionGeneral? datos)
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", AccessToken);
                HttpResponseMessage httpResponse = httpClient.PostAsync(datos.ComprobantesElectronicosURL + "/recepcion", contentJson).Result;
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.StatusCode == HttpStatusCode.Accepted)
                {
                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                }
                else
                {
                    string strClave = documento.ClaveNumerica;
                    if (new int[] { 5, 6, 7 }.Contains(documento.IdTipoDocumento)) strClave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                    if (httpResponse.Headers.Where(x => x.Key == "x-error-cause").FirstOrDefault().Value != null)
                    {
                        IList<string> headers = httpResponse.Headers.Where(x => x.Key == "x-error-cause").FirstOrDefault().Value.ToList();
                        if (headers.Count > 0)
                        {
                            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                                if (headers[0] == "El comprobante [" + strClave + "] ya fue recibido anteriormente.")
                                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Enviado;
                                else
                                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                            else
                                documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                            documento.ErrorEnvio = "Error en el envío: " + headers[0];
                        }
                    }
                    else
                    {
                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                        documento.ErrorEnvio = "Error en el envío: " + httpResponse.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                string strMensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                if (strMensajeError.Length > 500) strMensajeError = strMensajeError.Substring(0, 500);
                documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                documento.ErrorEnvio = "Error en el envío: " + strMensajeError;
            }
        }

        public static async Task<DocumentoElectronico> ConsultarDocumentoElectronico(CredencialesHacienda credenciales, DocumentoElectronico documento, LeandroContext dbContext, IConfiguracionGeneral? datos)
        {
            if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)
            {
                try
                {
                    ValidarToken(dbContext, credenciales, datos.ServicioTokenURL, datos.ClientId);
                }
                catch (Exception ex)
                {
                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                    documento.ErrorEnvio = "No se logro obtener un token: " + ex.Message;
                    credenciales.AccessToken = null;
                }
                if (credenciales.AccessToken != null)
                {
                    string strClave = documento.ClaveNumerica;
                    if (new int[] { 5, 6, 7 }.Contains(documento.IdTipoDocumento)) strClave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                    try
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", credenciales.AccessToken);
                        HttpResponseMessage httpResponse = await httpClient.GetAsync(datos.ComprobantesElectronicosURL + "/recepcion/" + strClave);
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            JObject estadoDocumento = JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);
                            string strEstado = estadoDocumento.Property("ind-estado").Value.ToString();
                            if (strEstado != "procesando")
                            {
                                string strRespuesta = estadoDocumento.Property("respuesta-xml").Value.ToString();
                                documento.Respuesta = Convert.FromBase64String(strRespuesta);
                                documento.EstadoEnvio = strEstado;
                            }
                            else
                                documento.ErrorEnvio = "El documento se encuentra procesando en el Ministerio de Hacienda";
                        }
                        else
                        {
                            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                            {
                                if (httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value != null)
                                {
                                    IList<string> headers = httpResponse.Headers.Where(x => x.Key == "X-Error-Cause").FirstOrDefault().Value.ToList();
                                    if (headers.Count > 0)
                                    {
                                        if (headers[0] == "El comprobante [" + documento.ClaveNumerica + "] no ha sido recibido.")
                                            documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                                        documento.ErrorEnvio = "Error en la consulta: " + headers[0];
                                    }
                                }
                                else
                                {
                                    documento.ErrorEnvio = "Error en la consulta: " + httpResponse.ReasonPhrase;
                                }
                            }
                            else
                            {
                                documento.ErrorEnvio = "Error en la consulta: " + httpResponse.ReasonPhrase;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMensajeError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        if (strMensajeError.Length > 500) strMensajeError = strMensajeError.Substring(0, 500);
                        documento.ErrorEnvio = "Error en la consulta: " + strMensajeError;
                    }
                }
            }
            return documento;
        }
    }
}