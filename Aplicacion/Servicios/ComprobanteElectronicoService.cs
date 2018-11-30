using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using LeandroSoftware.PuntoVenta.Dominio.Entidades;
using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using LeandroSoftware.Core.CommonTypes;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using FirmaXadesNet.Signature;
using FirmaXadesNet;
using FirmaXadesNet.Signature.Parameters;
using FirmaXadesNet.Crypto;
using System.Security.Cryptography.X509Certificates;
using LeandroSoftware.PuntoVenta.Datos;
using log4net;

namespace LeandroSoftware.PuntoVenta.Servicios
{
    public static class ComprobanteElectronicoService
    {
        private static readonly HttpClient client;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static ComprobanteElectronicoService()
        {
            client = new HttpClient();
        }

        public static string GeneraFacturaElectronica(Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, int intSucursal, int intTerminal)
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
                emisor.CorreoElectronico = empresa.CuentaCorreoElectronico;
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
                    resumenFactura.TipoCambio = factura.Empresa.TipoMoneda.TipoCambioVenta;
                    resumenFactura.TipoCambioSpecified = true;
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

        public static string GenerarNotaDeCreditoElectronica (Factura factura, Empresa empresa, Cliente cliente, IDbContext dbContext, int intSucursal, int intTerminal)
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
                emisor.CorreoElectronico = empresa.CuentaCorreoElectronico;
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
                    resumenFactura.TipoCambio = factura.Empresa.TipoMoneda.TipoCambioVenta;
                    resumenFactura.TipoCambioSpecified = true;
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

        public static void GeneraMensajeReceptor (string datosXml, Empresa empresa, IDbContext dbContext, int intSucursal, int intTerminal, int intMensaje)
        {
            try
            {
                string strCorreoNotificacion = "";
                if (empresa.CuentaCorreoElectronico == null || empresa.CuentaCorreoElectronico.Length == 0)
                {
                    throw new Exception("La empresa debe poseer una dirección de correo electrónico para ser notificada.");
                }
                else
                {
                    strCorreoNotificacion = empresa.CuentaCorreoElectronico;
                }
                XmlDocument documentoXml = new XmlDocument();
                documentoXml.LoadXml(datosXml);
                MensajeReceptor mensajeReceptor = new MensajeReceptor
                {
                    Clave = documentoXml.GetElementsByTagName("Clave").Item(0).InnerText,
                    NumeroCedulaEmisor = documentoXml.GetElementsByTagName("Emisor").Item(0).ChildNodes.Item(1).ChildNodes.Item(1).InnerText,
                    FechaEmisionDoc = DateTime.Parse(documentoXml.GetElementsByTagName("FechaEmision").Item(0).InnerText, CultureInfo.InvariantCulture),
                    Mensaje = (MensajeReceptorMensaje)intMensaje,
                    DetalleMensaje = "Mensaje de receptor con estado: " + (intMensaje == 0 ? "Aceptado" : intMensaje == 1 ? "Aceptado parcialmente" : "Rechazado"),
                    TotalFactura = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText),
                    NumeroCedulaReceptor = documentoXml.GetElementsByTagName("Receptor").Item(0).ChildNodes.Item(1).ChildNodes.Item(1).InnerText,
                    NumeroConsecutivoReceptor = ""
                };
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
                string respuesta = RegistrarDocumentoElectronico(empresa, mensajeReceptorXml, dbContext, intSucursal, intTerminal, tipoDoc, strCorreoNotificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string RegistrarDocumentoElectronico(Empresa empresa, XmlDocument documentoXml, IDbContext dbContext, int intSucursal, int intTerminal, TipoDocumento tipoDocumento, string strCorreoNotificacion)
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
                // Firma del documento XML
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
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string filePath = appPath + "/Certificates/" + empresa.IdCertificado;
                if (File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    X509Certificate2 uidCert = new X509Certificate2(fileBytes, empresa.PinCertificado, X509KeyStorageFlags.UserKeySet);
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
                        Fecha = fechaEmision,
                        TipoIdentificacionEmisor = int.Parse(strTipoIdentificacionEmisor),
                        IdentificacionEmisor = strIdentificacionEmisor,
                        TipoIdentificacionReceptor = strTipoIdentificacionReceptor.Length > 0 ? int.Parse(strTipoIdentificacionReceptor) : 0,
                        IdentificacionReceptor = strIdentificacionReceptor,
                        CorreoNotificacion = strCorreoNotificacion
                    };
                    documento.IdConsecutivo = intIdConsecutivo;
                    documento.Consecutivo = strConsucutivo;
                    documento.ClaveNumerica = strClaveNumerica;
                    documento.EstadoEnvio = "pendiente";
                    documento.DatosDocumento = signedDataEncoded;
                    dbContext.DocumentoElectronicoRepository.Add(documento);
                    // Generación de datos para envío de XML al servicio de Hacienda
                    DatosDocumentoElectronicoDTO datos = new DatosDocumentoElectronicoDTO();
                    datos.IdEmpresa = empresa.IdEmpresa;
                    datos.IdTipoDocumento = (int)tipoDocumento;
                    datos.ClaveNumerica = strClaveNumerica;
                    datos.FechaEmision = fechaEmision;
                    datos.TipoIdentificacionEmisor = strTipoIdentificacionEmisor;
                    datos.IdentificacionEmisor = strIdentificacionEmisor;
                    datos.TipoIdentificacionReceptor = strTipoIdentificacionReceptor;
                    datos.IdentificacionReceptor = strIdentificacionReceptor;
                    datos.EsMensajeReceptor = esMensajeReceptor ? "S" : "N";
                    datos.Consecutivo = strConsucutivo;
                    datos.CorreoNotificacion = strCorreoNotificacion;
                    datos.DatosDocumento = Convert.ToBase64String(signedDataEncoded);
                    // Envío de solicitud al servicio web de factura electrónica
                    Task.Run(() => RegistrarDocumentoElectronicoAsync(empresa, datos));
                    return strClaveNumerica;
                }
                else
                {
                    throw new Exception("No se logró encontrar la llave cryptográfica para la firma del documento electrónico. Contacte a su proveedor.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void RegistrarDocumentoElectronicoAsync(Empresa empresa, DatosDocumentoElectronicoDTO datos)
        {
            try
            {
                Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/registrardocumentoelectronico");
                string jsonRequest = new JavaScriptSerializer().Serialize(datos);
                StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
                task1.Wait();
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                log.Error("Error al enviar el el documento al servicio de facturación electrónica: ", flattenEx);
            }
        }

        public static void RegistrarDocumentoElectronico(Empresa empresa, DatosDocumentoElectronicoDTO documento)
        {
            Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/registrardocumentoelectronico");
            string jsonRequest = new JavaScriptSerializer().Serialize(documento);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    if (task1.Result.ReasonPhrase == "Service Unavailable")
                        throw new Exception("Service Unavailable");
                    throw new Exception("Error en el procesamiento de algunos o todos los documentos electrónicos. Por favor realice la consulta del estado de nuevo.");
                }
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                throw flattenEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EnviarDocumentoElectronico(Empresa empresa, DatosDocumentoElectronicoDTO documento)
        {
            Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/enviardocumentoelectronico");
            string jsonRequest = new JavaScriptSerializer().Serialize(documento);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    if (task1.Result.ReasonPhrase == "Service Unavailable")
                        throw new Exception("Service Unavailable");
                    throw new Exception("Error en el procesamiento de algunos o todos los documentos electrónicos. Por favor realice la consulta del estado de nuevo.");
                }
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                throw flattenEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<EmpresaDTO> ConsultarEmpresa(Empresa empresa)
        {
            Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/consultarempresa?empresa=" + empresa.IdEmpresa);
            Task<HttpResponseMessage> tskObtenerEmpresa = client.GetAsync(uri);
            EmpresaDTO empresaDTO;
            try
            {
                HttpResponseMessage httpRespuesta = await tskObtenerEmpresa;
                if (!httpRespuesta.IsSuccessStatusCode)
                {
                    if (httpRespuesta.ReasonPhrase == "Service Unavailable")
                        throw new Exception("Service Unavailable");
                    else
                        throw new Exception("Error al consultar el registro de la empresa en el servicio de factura electrónica: " + httpRespuesta.ReasonPhrase);
                }
                string strDatos = await httpRespuesta.Content.ReadAsStringAsync();
                empresaDTO = new JavaScriptSerializer().Deserialize<EmpresaDTO>(strDatos);
                return empresaDTO;
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                throw flattenEx;
            }
            catch (Exception ex)
            {
                log.Error("Error al enviar el el documento al servicio de facturación electrónica: ", ex);
                throw ex;
            }
        }

        public static async Task<DatosDocumentoElectronicoDTO> ConsultarDocumentoElectronico(Empresa empresa, string strClaveNumerica, string strConsecutivo)
        {
            Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/consultardocumentoelectronico?empresa=" + empresa.IdEmpresa + "&clave=" + strClaveNumerica + "&consecutivo=" + strConsecutivo);
            Task <HttpResponseMessage> tskObtenerListado = client.GetAsync(uri);
            try
            {
                HttpResponseMessage httpRespuesta = await tskObtenerListado;
                if (!httpRespuesta.IsSuccessStatusCode)
                {
                    if (httpRespuesta.ReasonPhrase == "Service Unavailable")
                        throw new Exception("Service Unavailable");
                    else
                        throw new Exception("Error al consultar el documento electrónico en el servicio de factura electrónica: " + httpRespuesta.ReasonPhrase);
                }
                string strListado = await httpRespuesta.Content.ReadAsStringAsync();
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                DatosDocumentoElectronicoDTO datos = json_serializer.Deserialize<DatosDocumentoElectronicoDTO>(strListado);
                return datos;
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                throw flattenEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PadronDTO ConsultarPersonaPorIdentificacion(Empresa empresa, string strIdentificacion)
        {
            Uri uri = new Uri(empresa.ServicioFacturaElectronicaURL + "/consultarpersonaporidentificacion?id=" + strIdentificacion);
            Task<HttpResponseMessage> task1 = client.GetAsync(uri);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    if (task1.Result.ReasonPhrase == "Service Unavailable")
                        throw new Exception("Service Unavailable");
                    throw new Exception("Error en el procesamiento de algunos o todos los documentos electrónicos. Por favor realice la consulta del estado de nuevo.");
                }
                string strPersna = task1.Result.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                PadronDTO persona = json_serializer.Deserialize<PadronDTO>(strPersna);
                return persona;
            }
            catch (AggregateException ex)
            {
                Exception flattenEx = ex.Flatten();
                throw flattenEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
