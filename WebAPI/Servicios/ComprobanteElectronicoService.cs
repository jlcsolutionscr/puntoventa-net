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

        public static DocumentoElectronico GenerarFacturaCompraElectronica(FacturaCompra facturaCompra, Empresa empresa, LeandroContext dbContext)
        {
            string strCorreoNotificacion = empresa.CorreoNotificacion;
            if (facturaCompra.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura de compra.");
            if (facturaCompra.PorcentajeExoneracion > 13) throw new BusinessException("El porcentaje de exoneración no puede ser mayor al 13%");
            FacturaElectronicaCompra facturaElectronica = new FacturaElectronicaCompra
            {
                Clave = "",
                CodigoActividadEmisor = facturaCompra.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = facturaCompra.Fecha
            };
            if (facturaCompra.CodigoActividadReceptor != "") facturaElectronica.CodigoActividadReceptor = facturaCompra.CodigoActividad;
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionReceptorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)facturaCompra.IdTipoIdentificacion,
                Numero = facturaCompra.IdentificacionEmisor
            };
            emisor.Identificacion = identificacionReceptorType;
            emisor.Nombre = facturaCompra.NombreEmisor;
            if (facturaCompra.NombreComercialEmisor.Length > 0) emisor.NombreComercial = facturaCompra.NombreComercialEmisor;
            if (facturaCompra.TelefonoEmisor.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = facturaCompra.TelefonoEmisor
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { facturaCompra.CorreoElectronicoEmisor };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = facturaCompra.IdProvinciaEmisor.ToString(),
                Canton = facturaCompra.IdCantonEmisor.ToString("D2"),
                Distrito = facturaCompra.IdDistritoEmisor.ToString("D2"),
                OtrasSenas = facturaCompra.DireccionEmisor
            };
            emisor.Ubicacion = ubicacionType;
            facturaElectronica.Emisor = emisor;
            ReceptorType receptor = new ReceptorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            receptor.Identificacion = identificacionEmisorType;
            receptor.Nombre = empresa.NombreEmpresa;
            receptor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                receptor.Telefono = telefonoType;
            }
            receptor.CorreoElectronico = empresa.CorreoNotificacion;
            ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            receptor.Ubicacion = ubicacionType;
            facturaElectronica.Receptor = receptor;
            facturaElectronica.CondicionVenta = (FacturaElectronicaCompraCondicionVenta)facturaCompra.IdCondicionVenta - 1;
            if (facturaElectronica.CondicionVenta == FacturaElectronicaCompraCondicionVenta.Item02)
            {
                facturaElectronica.PlazoCredito = facturaCompra.PlazoCredito.ToString();
            }
            List<FacturaElectronicaCompraResumenFacturaMedioPago> medioPagoList = new List<FacturaElectronicaCompraResumenFacturaMedioPago>();
            FacturaElectronicaCompraResumenFacturaMedioPago medioPago = new FacturaElectronicaCompraResumenFacturaMedioPago
            {
                TipoMedioPago = FacturaElectronicaCompraResumenFacturaMedioPagoTipoMedioPago.Item99    
            };
            medioPagoList.Add(medioPago);
            List<FacturaElectronicaCompraLineaDetalle> detalleServicioList = new List<FacturaElectronicaCompraLineaDetalle>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleFacturaCompra detalleFactura in facturaCompra.DetalleFacturaCompra)
            {
                decimal decSubtotal = 0;
                FacturaElectronicaCompraLineaDetalle lineaDetalle = new FacturaElectronicaCompraLineaDetalle
                {
                    NumeroLinea = detalleFactura.Linea.ToString(),
                    CodigoCABYS = detalleFactura.Codigo,
                    Cantidad = detalleFactura.Cantidad
                };
                if (detalleFactura.UnidadMedida == "Und")
                    lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                else if (detalleFactura.UnidadMedida == "Sp")
                    lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                else if (detalleFactura.UnidadMedida == "Spe")
                    lineaDetalle.UnidadMedida = UnidadMedidaType.Spe;
                else if (detalleFactura.UnidadMedida == "St")
                    lineaDetalle.UnidadMedida = UnidadMedidaType.St;
                else
                    lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
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
                    decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                    ImpuestoType impuestoType = new ImpuestoType
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)detalleFactura.IdImpuesto - 1,
                        CodigoTarifaIVASpecified = true,
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
                        ExoneracionType exoneracionType = new ExoneracionType
                        {
                            TipoDocumentoEX1 = (TipoExoneracionType)facturaCompra.IdTipoExoneracion - 1,
                            NumeroDocumento = facturaCompra.NumDocExoneracion,
                            NombreInstitucion = (ExoneracionTypeNombreInstitucion)facturaCompra.IdNombreInstExoneracion - 1,
                            FechaEmisionEX = facturaCompra.FechaEmisionDoc,
                            Articulo = facturaCompra.ArticuloExoneracion,
                            Inciso = facturaCompra.IncisoExoneracion,
                            TarifaExonerada = facturaCompra.PorcentajeExoneracion,
                            MontoExoneracion = decMontoExoneracion
                        };
                        impuestoType.Exoneracion = exoneracionType;
                    }
                    lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                    lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (facturaCompra.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = facturaCompra.TipoDeCambioDolar
                };
            }
            else if (facturaCompra.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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

        public static DocumentoElectronico GenerarFacturaElectronica(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext)
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
            if (factura.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura.");
            FacturaElectronica facturaElectronica = new FacturaElectronica
            {
                Clave = "",
                CodigoActividadEmisor = factura.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = factura.Fecha,
                ProveedorSistemas = empresa.Identificacion
            };
            if (factura.CodigoActividadReceptor != "") facturaElectronica.CodigoActividadReceptor = factura.CodigoActividadReceptor;
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { empresa.CorreoNotificacion };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            facturaElectronica.Emisor = emisor;
            ReceptorType receptor = new ReceptorType();
            IdentificacionType identificacionReceptorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                Numero = cliente.Identificacion
            };
            receptor.Identificacion = identificacionReceptorType;
            receptor.Nombre = cliente.Nombre;
            if (cliente.NombreComercial.Length > 0)
                receptor.NombreComercial = cliente.NombreComercial;
            if (cliente.Telefono.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = cliente.Telefono
                };
                receptor.Telefono = telefonoType;
            }
            receptor.CorreoElectronico = cliente.CorreoElectronico;
            facturaElectronica.Receptor = receptor;
            facturaElectronica.CondicionVenta = (FacturaElectronicaCondicionVenta)factura.IdCondicionVenta - 1;
            List<FacturaElectronicaResumenFacturaMedioPago> medioPagoList = new List<FacturaElectronicaResumenFacturaMedioPago>();
            if (facturaElectronica.CondicionVenta == FacturaElectronicaCondicionVenta.Item01)
            {
                foreach (DesglosePagoFactura desglose in factura.DesglosePagoFactura)
                {
                    if (medioPagoList.Count() == 4)
                    {
                        throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                    }
                    FacturaElectronicaResumenFacturaMedioPago medioPago = new FacturaElectronicaResumenFacturaMedioPago
                    {
                        TipoMedioPago = (FacturaElectronicaResumenFacturaMedioPagoTipoMedioPago)desglose.IdFormaPago - 1,
                        TotalMedioPago = desglose.MontoLocal
                    };
                    if (!medioPagoList.Contains(medioPago))
                    {
                        medioPagoList.Add(medioPago);
                    }
                }
            }
            else if (facturaElectronica.CondicionVenta == FacturaElectronicaCondicionVenta.Item02)
            {
                facturaElectronica.PlazoCredito = factura.PlazoCredito.ToString();
            }
            else
            {
                throw new BusinessException("El sistema solo permite ventas de contado o crédito.");
            }
            List<FacturaElectronicaLineaDetalle> detalleServicioList = new List<FacturaElectronicaLineaDetalle>();
            List<OtrosCargosType> detalleOtrosCargosList = new List<OtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    FacturaElectronicaLineaDetalle lineaDetalle = new FacturaElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        CodigoCABYS = detalleFactura.Producto.CodigoClasificacion
                    };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    lineaDetalle.BaseImponible = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decimal decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        decimal decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        if (impuestoResumen.ContainsKey(intCodigoTarifa))
                            impuestoResumen[intCodigoTarifa] = impuestoResumen[intCodigoTarifa] + decMontoImpuestoPorLinea;
                        else
                            impuestoResumen[intCodigoTarifa] = decMontoImpuestoPorLinea;
                        ImpuestoType impuestoType = new ImpuestoType
                        {
                            Codigo = CodigoImpuestoType.Item01,
                            CodigoTarifaIVA = (CodigoTarifaIVAType)intCodigoTarifa - 1,
                            CodigoTarifaIVASpecified = true,
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
                            ExoneracionType exoneracionType = new ExoneracionType
                            {
                                TipoDocumentoEX1 = (TipoExoneracionType)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = (ExoneracionTypeNombreInstitucion)factura.IdNombreInstExoneracion - 1,
                                FechaEmisionEX = factura.FechaEmisionDoc,
                                Articulo = factura.ArticuloExoneracion,
                                Inciso = factura.IncisoExoneracion,
                                TarifaExonerada = factura.PorcentajeExoneracion,
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoAsumidoEmisorFabrica = 0;
                        lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
                    OtrosCargosType lineaOtrosCargos = new OtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        PorcentajeOC = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumentoOC = OtrosCargosTypeTipoDocumentoOC.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            facturaElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) facturaElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
            FacturaElectronicaResumenFactura resumenFactura = new FacturaElectronicaResumenFactura();
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<FacturaElectronicaResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<FacturaElectronicaResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    FacturaElectronicaResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new FacturaElectronicaResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = factura.TipoDeCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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

        public static DocumentoElectronico GeneraTiqueteElectronico(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext)
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
            if (factura.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura.");
            TiqueteElectronico tiqueteElectronico = new TiqueteElectronico
            {
                Clave = "",
                CodigoActividadEmisor = factura.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = factura.Fecha,
                ProveedorSistemas = empresa.Identificacion
            };
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { empresa.CorreoNotificacion };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            tiqueteElectronico.Emisor = emisor;
            tiqueteElectronico.CondicionVenta = (TiqueteElectronicoCondicionVenta)factura.IdCondicionVenta - 1;
            List<TiqueteElectronicoResumenFacturaMedioPago> medioPagoList = new List<TiqueteElectronicoResumenFacturaMedioPago>();
            if (tiqueteElectronico.CondicionVenta == TiqueteElectronicoCondicionVenta.Item01)
            {
                foreach (DesglosePagoFactura desglose in factura.DesglosePagoFactura)
                {
                    if (medioPagoList.Count() == 4)
                    {
                        throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                    }
                    TiqueteElectronicoResumenFacturaMedioPago medioPago = new TiqueteElectronicoResumenFacturaMedioPago
                    {
                        TipoMedioPago = (TiqueteElectronicoResumenFacturaMedioPagoTipoMedioPago)desglose.IdFormaPago - 1,
                        TotalMedioPago = desglose.MontoLocal
                    };
                    if (!medioPagoList.Contains(medioPago))
                    {
                        medioPagoList.Add(medioPago);
                    }
                }
            }
            else if (tiqueteElectronico.CondicionVenta == TiqueteElectronicoCondicionVenta.Item02)
            {
                tiqueteElectronico.PlazoCredito = factura.PlazoCredito.ToString();
            }
            else
            {
                throw new BusinessException("El sistema solo permite ventas de contado o crédito.");
            }
            List<TiqueteElectronicoLineaDetalle> detalleServicioList = new List<TiqueteElectronicoLineaDetalle>();
            List<OtrosCargosType> detalleOtrosCargosList = new List<OtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    TiqueteElectronicoLineaDetalle lineaDetalle = new TiqueteElectronicoLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        CodigoCABYS = detalleFactura.Producto.CodigoClasificacion
                    };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    lineaDetalle.BaseImponible = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decimal decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        decimal decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        if (impuestoResumen.ContainsKey(intCodigoTarifa))
                            impuestoResumen[intCodigoTarifa] = impuestoResumen[intCodigoTarifa] + decMontoImpuestoPorLinea;
                        else
                            impuestoResumen[intCodigoTarifa] = decMontoImpuestoPorLinea;
                        ImpuestoType impuestoType = new ImpuestoType
                        {
                            Codigo = CodigoImpuestoType.Item01,
                            CodigoTarifaIVA = (CodigoTarifaIVAType)intCodigoTarifa - 1,
                            CodigoTarifaIVASpecified = true,
                            Tarifa = detalleFactura.PorcentajeIVA,
                            TarifaSpecified = true,
                            Monto = decMontoImpuestoPorLinea
                        };
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoAsumidoEmisorFabrica = 0;
                        lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
                    OtrosCargosType lineaOtrosCargos = new OtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        PorcentajeOC = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumentoOC = OtrosCargosTypeTipoDocumentoOC.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            tiqueteElectronico.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) tiqueteElectronico.OtrosCargos = detalleOtrosCargosList.ToArray();
            TiqueteElectronicoResumenFactura resumenFactura = new TiqueteElectronicoResumenFactura();
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<TiqueteElectronicoResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<TiqueteElectronicoResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    TiqueteElectronicoResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new TiqueteElectronicoResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = factura.TipoDeCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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

        public static DocumentoElectronico GenerarNotaDeCreditoElectronica(Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext)
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
            if (factura.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura.");
            NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
            {
                Clave = "",
                CodigoActividadEmisor = factura.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = Validador.ObtenerFechaHoraCostaRica(),
                ProveedorSistemas = empresa.Identificacion
            };
            if (factura.CodigoActividadReceptor != "") notaCreditoElectronica.CodigoActividadReceptor = factura.CodigoActividadReceptor;
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { empresa.CorreoNotificacion };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            notaCreditoElectronica.Emisor = emisor;
            if (factura.IdCliente > 1)
            {
                ReceptorType receptor = new ReceptorType();
                IdentificacionType identificacionReceptorType = new IdentificacionType
                {
                    Tipo = (IdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                    Numero = cliente.Identificacion
                };
                receptor.Identificacion = identificacionReceptorType;
                receptor.Nombre = cliente.Nombre;
                if (cliente.NombreComercial.Length > 0)
                    receptor.NombreComercial = cliente.NombreComercial;
                if (cliente.Telefono.Length > 0)
                {
                    TelefonoType telefonoType = new TelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = cliente.Telefono
                    };
                    receptor.Telefono = telefonoType;
                }
                receptor.CorreoElectronico = cliente.CorreoElectronico;
                notaCreditoElectronica.Receptor = receptor;
            }
            notaCreditoElectronica.CondicionVenta = (NotaCreditoElectronicaCondicionVenta)factura.IdCondicionVenta - 1;
            List<NotaCreditoElectronicaResumenFacturaMedioPago> medioPagoList = new List<NotaCreditoElectronicaResumenFacturaMedioPago>();
            if (notaCreditoElectronica.CondicionVenta == NotaCreditoElectronicaCondicionVenta.Item01)
            {
                foreach (DesglosePagoFactura desglose in factura.DesglosePagoFactura)
                {
                    if (medioPagoList.Count() == 4)
                    {
                        throw new BusinessException("La factura electrónica no permite más de 4 medios de pago por registro. Por favor corrija la información suministrada.");
                    }
                    NotaCreditoElectronicaResumenFacturaMedioPago medioPago = new NotaCreditoElectronicaResumenFacturaMedioPago
                    {
                        TipoMedioPago = (NotaCreditoElectronicaResumenFacturaMedioPagoTipoMedioPago)desglose.IdFormaPago - 1,
                        TotalMedioPago = desglose.MontoLocal
                    };
                    if (!medioPagoList.Contains(medioPago))
                    {
                        medioPagoList.Add(medioPago);
                    }
                }
            }
            else if (notaCreditoElectronica.CondicionVenta == NotaCreditoElectronicaCondicionVenta.Item02)
            {
                notaCreditoElectronica.PlazoCredito = factura.PlazoCredito.ToString();
            }
            else
            {
                throw new BusinessException("El sistema solo permite ventas de contado o crédito.");
            }
            List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
            List<OtrosCargosType> detalleOtrosCargosList = new List<OtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleFactura detalleFactura in factura.DetalleFactura)
            {
                if (detalleFactura.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        CodigoCABYS = detalleFactura.Producto.CodigoClasificacion
                    };
                    lineaDetalle.Cantidad = detalleFactura.Cantidad;
                    if (detalleFactura.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                    else if (detalleFactura.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalleFactura.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalleFactura.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    lineaDetalle.BaseImponible = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    if (!detalleFactura.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decimal decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalleFactura.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        decimal decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        int intCodigoTarifa = detalleFactura.Producto.IdImpuesto;
                        if (impuestoResumen.ContainsKey(intCodigoTarifa))
                            impuestoResumen[intCodigoTarifa] = impuestoResumen[intCodigoTarifa] + decMontoImpuestoPorLinea;
                        else
                            impuestoResumen[intCodigoTarifa] = decMontoImpuestoPorLinea;
                        ImpuestoType impuestoType = new ImpuestoType
                        {
                            Codigo = CodigoImpuestoType.Item01,
                            CodigoTarifaIVA = (CodigoTarifaIVAType)intCodigoTarifa - 1,
                            CodigoTarifaIVASpecified = true,
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
                            ExoneracionType exoneracionType = new ExoneracionType
                            {
                                TipoDocumentoEX1 = (TipoExoneracionType)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = (ExoneracionTypeNombreInstitucion)factura.IdNombreInstExoneracion - 1,
                                FechaEmisionEX = factura.FechaEmisionDoc,
                                Articulo = factura.ArticuloExoneracion,
                                Inciso = factura.IncisoExoneracion,
                                TarifaExonerada = factura.PorcentajeExoneracion,
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoAsumidoEmisorFabrica = 0;
                        lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
                    OtrosCargosType lineaOtrosCargos = new OtrosCargosType
                    {
                        Detalle = detalleFactura.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        PorcentajeOC = detalleFactura.Producto.PrecioVenta1,
                        TipoDocumentoOC = OtrosCargosTypeTipoDocumentoOC.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) notaCreditoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
            NotaCreditoElectronicaResumenFactura resumenFactura = new NotaCreditoElectronicaResumenFactura();
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = factura.TipoDeCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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
                TipoDocIR = TipoDocReferenciaType.Item01,
                Numero = factura.IdDocElectronico,
                FechaEmisionIR = factura.Fecha,
                Codigo = CodigoReferenciaType.Item01,
                CodigoSpecified = true,
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

        public static DocumentoElectronico GenerarNotaDeCreditoElectronicaParcial(DevolucionCliente devolucion, Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext)
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
            if (factura.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura.");
            NotaCreditoElectronica notaCreditoElectronica = new NotaCreditoElectronica
            {
                Clave = "",
                CodigoActividadEmisor = factura.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = devolucion.Fecha,
                ProveedorSistemas = empresa.Identificacion
            };
            if (factura.CodigoActividadReceptor != "") notaCreditoElectronica.CodigoActividadReceptor = factura.CodigoActividadReceptor;
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { empresa.CorreoNotificacion };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            notaCreditoElectronica.Emisor = emisor;
            if (devolucion.IdCliente > 1)
            {
                ReceptorType receptor = new ReceptorType();
                IdentificacionType identificacionReceptorType = new IdentificacionType
                {
                    Tipo = (IdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                    Numero = cliente.Identificacion
                };
                receptor.Identificacion = identificacionReceptorType;
                receptor.Nombre = cliente.Nombre;
                if (cliente.NombreComercial.Length > 0)
                    receptor.NombreComercial = cliente.NombreComercial;
                if (cliente.Telefono.Length > 0)
                {
                    TelefonoType telefonoType = new TelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = cliente.Telefono
                    };
                    receptor.Telefono = telefonoType;
                }
                receptor.CorreoElectronico = cliente.CorreoElectronico;
                notaCreditoElectronica.Receptor = receptor;
            }
            notaCreditoElectronica.CondicionVenta = NotaCreditoElectronicaCondicionVenta.Item01;
            List<NotaCreditoElectronicaResumenFacturaMedioPago> medioPagoList = new List<NotaCreditoElectronicaResumenFacturaMedioPago>();
            NotaCreditoElectronicaResumenFacturaMedioPago medioPago = new NotaCreditoElectronicaResumenFacturaMedioPago
            {
                TipoMedioPago = NotaCreditoElectronicaResumenFacturaMedioPagoTipoMedioPago.Item01
            };
            medioPagoList.Add(medioPago);
            List<NotaCreditoElectronicaLineaDetalle> detalleServicioList = new List<NotaCreditoElectronicaLineaDetalle>();
            List<OtrosCargosType> detalleOtrosCargosList = new List<OtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleDevolucionCliente detalle in devolucion.DetalleDevolucionCliente)
            {
                if (detalle.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaCreditoElectronicaLineaDetalle lineaDetalle = new NotaCreditoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        CodigoCABYS = detalle.Producto.CodigoClasificacion
                    };
                    lineaDetalle.Cantidad = detalle.Cantidad;
                    if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                    else if (detalle.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalle.Producto.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalle.PrecioVenta * detalle.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    lineaDetalle.BaseImponible = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    if (!detalle.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decimal decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalle.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        decimal decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        int intCodigoTarifa = detalle.Producto.IdImpuesto;
                        if (impuestoResumen.ContainsKey(intCodigoTarifa))
                            impuestoResumen[intCodigoTarifa] = impuestoResumen[intCodigoTarifa] + decMontoImpuestoPorLinea;
                        else
                            impuestoResumen[intCodigoTarifa] = decMontoImpuestoPorLinea;
                        ImpuestoType impuestoType = new ImpuestoType
                        {
                            Codigo = CodigoImpuestoType.Item01,
                            CodigoTarifaIVA = (CodigoTarifaIVAType)intCodigoTarifa - 1,
                            CodigoTarifaIVASpecified = true,
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
                            ExoneracionType exoneracionType = new ExoneracionType
                            {
                                TipoDocumentoEX1 = (TipoExoneracionType)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = (ExoneracionTypeNombreInstitucion)factura.IdNombreInstExoneracion - 1,
                                FechaEmisionEX = factura.FechaEmisionDoc,
                                Articulo = factura.ArticuloExoneracion,
                                Inciso = factura.IncisoExoneracion,
                                TarifaExonerada = factura.PorcentajeExoneracion,
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoAsumidoEmisorFabrica = 0;
                        lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
                    OtrosCargosType lineaOtrosCargos = new OtrosCargosType
                    {
                        Detalle = detalle.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        PorcentajeOC = detalle.Producto.PrecioVenta1,
                        TipoDocumentoOC = OtrosCargosTypeTipoDocumentoOC.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            notaCreditoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) notaCreditoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
            NotaCreditoElectronicaResumenFactura resumenFactura = new NotaCreditoElectronicaResumenFactura();
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new NotaCreditoElectronicaResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = factura.TipoDeCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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
                TipoDocIR = TipoDocReferenciaType.Item01,
                Numero = factura.IdDocElectronico,
                FechaEmisionIR = factura.Fecha,
                Codigo = CodigoReferenciaType.Item01,
                CodigoSpecified = true,
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

        public static DocumentoElectronico GenerarNotaDeDebitoElectronicaParcial(DevolucionCliente devolucion, Factura factura, Empresa empresa, Cliente cliente, LeandroContext dbContext)
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
            if (factura.CodigoActividad == "") throw new BusinessException("Debe ingresar el código de actividad económica en el encabezado de la factura.");
            NotaDebitoElectronica notaDebitoElectronica = new NotaDebitoElectronica
            {
                Clave = "",
                CodigoActividadEmisor = factura.CodigoActividad,
                NumeroConsecutivo = "",
                FechaEmision = Validador.ObtenerFechaHoraCostaRica(),
                ProveedorSistemas = empresa.Identificacion
            };
            if (factura.CodigoActividadReceptor != "") notaDebitoElectronica.CodigoActividadReceptor = factura.CodigoActividadReceptor;
            EmisorType emisor = new EmisorType();
            IdentificacionType identificacionEmisorType = new IdentificacionType
            {
                Tipo = (IdentificacionTypeTipo)empresa.IdTipoIdentificacion,
                Numero = empresa.Identificacion
            };
            emisor.Identificacion = identificacionEmisorType;
            emisor.Nombre = empresa.NombreEmpresa;
            emisor.NombreComercial = empresa.NombreComercial;
            if (empresa.Telefono1.Length > 0)
            {
                TelefonoType telefonoType = new TelefonoType
                {
                    CodigoPais = "506",
                    NumTelefono = empresa.Telefono1
                };
                emisor.Telefono = telefonoType;
            }
            emisor.CorreoElectronico = new string[] { empresa.CorreoNotificacion };
            UbicacionType ubicacionType = new UbicacionType
            {
                Provincia = empresa.IdProvincia.ToString(),
                Canton = empresa.IdCanton.ToString("D2"),
                Distrito = empresa.IdDistrito.ToString("D2"),
                OtrasSenas = empresa.Direccion
            };
            emisor.Ubicacion = ubicacionType;
            notaDebitoElectronica.Emisor = emisor;
            if (devolucion.IdCliente > 1)
            {
                ReceptorType receptor = new ReceptorType();
                IdentificacionType identificacionReceptorType = new IdentificacionType
                {
                    Tipo = (IdentificacionTypeTipo)cliente.IdTipoIdentificacion,
                    Numero = cliente.Identificacion
                };
                receptor.Identificacion = identificacionReceptorType;
                receptor.Nombre = cliente.Nombre;
                if (cliente.NombreComercial.Length > 0)
                    receptor.NombreComercial = cliente.NombreComercial;
                if (cliente.Telefono.Length > 0)
                {
                    TelefonoType telefonoType = new TelefonoType
                    {
                        CodigoPais = "506",
                        NumTelefono = cliente.Telefono
                    };
                    receptor.Telefono = telefonoType;
                }
                receptor.CorreoElectronico = cliente.CorreoElectronico;
                notaDebitoElectronica.Receptor = receptor;
            }
            notaDebitoElectronica.CondicionVenta = NotaDebitoElectronicaCondicionVenta.Item01;
            List<NotaDebitoElectronicaResumenFacturaMedioPago> medioPagoList = new List<NotaDebitoElectronicaResumenFacturaMedioPago>();
            NotaDebitoElectronicaResumenFacturaMedioPago medioPago = new NotaDebitoElectronicaResumenFacturaMedioPago
            {
                TipoMedioPago = NotaDebitoElectronicaResumenFacturaMedioPagoTipoMedioPago.Item99
            };
            medioPagoList.Add(medioPago);
            List<NotaDebitoElectronicaLineaDetalle> detalleServicioList = new List<NotaDebitoElectronicaLineaDetalle>();
            List<OtrosCargosType> detalleOtrosCargosList = new List<OtrosCargosType>();
            decimal decTotalMercanciasGravadas = 0;
            decimal decTotalServiciosGravados = 0;
            decimal decTotalMercanciasExcentas = 0;
            decimal decTotalServiciosExcentos = 0;
            decimal decTotalMercanciasExoneradas = 0;
            decimal decTotalServiciosExonerados = 0;
            decimal decTotalOtrosCargos = 0;
            decimal decTotalImpuestos = 0;
            Dictionary <int,decimal> impuestoResumen = new Dictionary<int,decimal>();
            foreach (DetalleDevolucionCliente detalle in devolucion.DetalleDevolucionCliente)
            {
                if (detalle.Producto.Tipo != StaticTipoProducto.ImpuestodeServicio)
                {
                    decimal decSubtotal = 0;
                    NotaDebitoElectronicaLineaDetalle lineaDetalle = new NotaDebitoElectronicaLineaDetalle
                    {
                        NumeroLinea = (detalleServicioList.Count() + 1).ToString(),
                        CodigoCABYS = detalle.Producto.CodigoClasificacion
                    };
                    lineaDetalle.Cantidad = detalle.Cantidad;
                    if (detalle.Producto.Tipo == StaticTipoProducto.Producto)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Unid;
                    else if (detalle.Producto.Tipo == StaticTipoProducto.ServicioProfesionales)
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Sp;
                    else
                        lineaDetalle.UnidadMedida = UnidadMedidaType.Os;
                    lineaDetalle.Detalle = detalle.Producto.Descripcion;
                    lineaDetalle.PrecioUnitario = Math.Round(detalle.PrecioVenta, 2, MidpointRounding.AwayFromZero);
                    decSubtotal = detalle.PrecioVenta * detalle.Cantidad;
                    lineaDetalle.MontoTotal = Math.Round(decSubtotal, 2, MidpointRounding.AwayFromZero);
                    lineaDetalle.SubTotal = lineaDetalle.MontoTotal;
                    lineaDetalle.BaseImponible = lineaDetalle.MontoTotal;
                    decimal decTotalPorLinea = 0;
                    if (!detalle.Excento)
                    {
                        decimal decMontoGravadoPorLinea = lineaDetalle.SubTotal;
                        decimal decMontoExoneradoPorLinea = 0;
                        decimal decMontoImpuestoPorLinea = Math.Round(decSubtotal * (detalle.PorcentajeIVA / 100), 2, MidpointRounding.AwayFromZero);
                        decimal decMontoImpuestoNetoPorLinea = decMontoImpuestoPorLinea;
                        int intCodigoTarifa = detalle.Producto.IdImpuesto;
                        if (impuestoResumen.ContainsKey(intCodigoTarifa))
                            impuestoResumen[intCodigoTarifa] = impuestoResumen[intCodigoTarifa] + decMontoImpuestoPorLinea;
                        else
                            impuestoResumen[intCodigoTarifa] = decMontoImpuestoPorLinea;
                        ImpuestoType impuestoType = new ImpuestoType
                        {
                            Codigo = CodigoImpuestoType.Item01,
                            CodigoTarifaIVA = (CodigoTarifaIVAType)intCodigoTarifa - 1,
                            CodigoTarifaIVASpecified = true,
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
                            ExoneracionType exoneracionType = new ExoneracionType
                            {
                                TipoDocumentoEX1 = (TipoExoneracionType)factura.IdTipoExoneracion - 1,
                                NumeroDocumento = factura.NumDocExoneracion,
                                NombreInstitucion = (ExoneracionTypeNombreInstitucion)factura.IdNombreInstExoneracion - 1,
                                FechaEmisionEX = factura.FechaEmisionDoc,
                                Articulo = factura.ArticuloExoneracion,
                                Inciso = factura.IncisoExoneracion,
                                TarifaExonerada = factura.PorcentajeExoneracion,
                                MontoExoneracion = decMontoExoneracion
                            };
                            impuestoType.Exoneracion = exoneracionType;
                        }
                        lineaDetalle.ImpuestoNeto = decMontoImpuestoNetoPorLinea;
                        lineaDetalle.ImpuestoAsumidoEmisorFabrica = 0;
                        lineaDetalle.Impuesto = new ImpuestoType[] { impuestoType };
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
                    OtrosCargosType lineaOtrosCargos = new OtrosCargosType
                    {
                        Detalle = detalle.Producto.Descripcion,
                        MontoCargo = decTotalPorLinea,
                        PorcentajeOC = detalle.Producto.PrecioVenta1,
                        TipoDocumentoOC = OtrosCargosTypeTipoDocumentoOC.Item06
                    };
                    detalleOtrosCargosList.Add(lineaOtrosCargos);
                    decTotalOtrosCargos += decTotalPorLinea;
                }
            }
            notaDebitoElectronica.DetalleServicio = detalleServicioList.ToArray();
            if (detalleOtrosCargosList.Count > 0) notaDebitoElectronica.OtrosCargos = detalleOtrosCargosList.ToArray();
            NotaDebitoElectronicaResumenFactura resumenFactura = new NotaDebitoElectronicaResumenFactura();
            resumenFactura.MedioPago = medioPagoList.ToArray();
            if (impuestoResumen.Count > 0)
            {
                List<NotaDebitoElectronicaResumenFacturaTotalDesgloseImpuesto> totalDesgloseImpuesto = new List<NotaDebitoElectronicaResumenFacturaTotalDesgloseImpuesto>();
                impuestoResumen.ToList().ForEach(impuesto => {
                    NotaDebitoElectronicaResumenFacturaTotalDesgloseImpuesto desgloseImpuesto = new NotaDebitoElectronicaResumenFacturaTotalDesgloseImpuesto
                    {
                        Codigo = CodigoImpuestoType.Item01,
                        CodigoTarifaIVA = (CodigoTarifaIVAType)impuesto.Key - 1,
                        CodigoTarifaIVASpecified = true,
                        TotalMontoImpuesto = impuesto.Value
                    };
                    totalDesgloseImpuesto.Add(desgloseImpuesto);
                });
                resumenFactura.TotalDesgloseImpuesto = totalDesgloseImpuesto.ToArray();
            }
            CodigoMonedaType codigoMonedaType = null;
            if (factura.IdTipoMoneda == StaticTipoMoneda.Dolares)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.USD,
                    TipoCambio = factura.TipoDeCambioDolar
                };
            }
            else if (factura.IdTipoMoneda == StaticTipoMoneda.Colones)
            {
                codigoMonedaType = new CodigoMonedaType
                {
                    CodigoMoneda = CodigoMonedaTypeCodigoMoneda.CRC,
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
            notaDebitoElectronica.ResumenFactura = resumenFactura;
            NotaDebitoElectronicaInformacionReferencia informacionReferencia = new NotaDebitoElectronicaInformacionReferencia
            {
                TipoDocIR = TipoDocReferenciaType.Item01,
                Numero = factura.IdDocElectronico,
                FechaEmisionIR = factura.Fecha,
                Codigo = CodigoReferenciaType.Item01,
                CodigoSpecified = true,
                Razon = "Anulación de devolución de mercancía de factura electrónica con la respectiva clave númerica."
            };
            notaDebitoElectronica.InformacionReferencia = new NotaDebitoElectronicaInformacionReferencia[] { informacionReferencia };
            XmlDocument documentoXml = new XmlDocument();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            XmlSerializer serializer = new XmlSerializer(notaDebitoElectronica.GetType());
            using (MemoryStream msDatosXML = new MemoryStream())
            using (XmlWriter writer = XmlWriter.Create(msDatosXML, settings))
            {
                serializer.Serialize(writer, notaDebitoElectronica);
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
                    PolicyIdentifier = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/Resoluci%C3%B3n_General_sobre_disposiciones_t%C3%A9cnicas_comprobantes_electr%C3%B3nicos_para_efectos_tributarios.pdf",
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