using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using LeandroSoftware.Core.TiposComunes;
using System.IO;
using System.Collections.Generic;

namespace LeandroSoftware.Core.Utilitario
{
    public static class UtilitarioPDF
    {
        public static byte[] GenerarPDF(EstructuraPDF datos)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
                document.Info.Title = datos.TituloDocumento;
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 10, XFontStyle.BoldItalic, options);
                XTextFormatter tf = new XTextFormatter(gfx)
                {
                    Alignment = XParagraphAlignment.Right
                };
                if (datos.Logotipo != null)
                {
                    XImage image = XImage.FromGdiPlusImage(datos.Logotipo);
                    gfx.DrawImage(image, 20, 60, 175, 80);
                }
                if (datos.PoweredByLogotipo != null)
                {
                    tf.DrawString("Powered by", font, XBrushes.Black, new XRect(20, 35, gfx.PageSize.Width - 142.5, 15), XStringFormats.TopLeft);
                    XImage poweredByImage = XImage.FromGdiPlusImage(datos.PoweredByLogotipo);
                    gfx.DrawImage(poweredByImage, gfx.PageSize.Width - 117.5, 20, 87.5, 40);
                }
                font = new XFont("Courier New", 12, XFontStyle.Bold, options);
                gfx.DrawString(datos.NombreComercial.ToUpper(), font, XBrushes.Black, new XRect(210, 60, 300, 15), XStringFormats.TopLeft);
                font = new XFont("Courier New", 10, XFontStyle.Bold, options);
                gfx.DrawString(datos.NombreEmpresa.ToUpper(), font, XBrushes.Black, new XRect(210, 85, 300, 15), XStringFormats.TopLeft);
                gfx.DrawString("IDENTIFICACION: " + datos.IdentificacionEmisor, font, XBrushes.Black, new XRect(210, 100, 200, 15), XStringFormats.TopLeft);
                gfx.DrawString(datos.DireccionEmisor, font, XBrushes.Black, new XRect(210, 115, 200, 15), XStringFormats.TopLeft);
                gfx.DrawString("CORREO: " + datos.CorreoElectronicoEmisor, font, XBrushes.Black, new XRect(210, 130, 200, 15), XStringFormats.TopLeft);
                gfx.DrawString("TELEFONO: " + datos.TelefonoEmisor + (datos.FaxEmisor.Length > 0 ? " Fax: " + datos.FaxEmisor : ""), font, XBrushes.Black, new XRect(210, 145, 200, 15), XStringFormats.TopLeft);
                font = new XFont("Courier New", 12, XFontStyle.Bold, options);
                gfx.DrawString(datos.TituloDocumento, font, XBrushes.Black, new XRect(210, 190, 200, 15), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                int lineaPos = 217;
                gfx.DrawString("Registro número:", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.ConsecInterno, font, XBrushes.Black, new XRect(110, lineaPos, 80, 12), XStringFormats.TopLeft);
                lineaPos += 12;
                if (datos.Clave != null)
                {
                    gfx.DrawString("Clave: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.Clave, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Consecutivo: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.Consecutivo, font, XBrushes.Black, new XRect(470, lineaPos, 200, 12), XStringFormats.TopLeft);
                    lineaPos += 12;
                }
                gfx.DrawString("Condición de Venta: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.CondicionVenta, font, XBrushes.Black, new XRect(110, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString("Plazo de crédito: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.PlazoCredito, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Fecha: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.Fecha, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Medio de Pago: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.MedioPago, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Codigo Moneda:", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.CodigoMoneda, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Tipo de cambio:", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.TipoDeCambio, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 27;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("DATOS DEL CLIENTE", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);
                lineaPos += 12;

                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                gfx.DrawString("Nombre: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.NombreReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                if (datos.PoseeReceptor)
                {
                    gfx.DrawString("Identificación: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.IdentificacionReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Nombre comercial: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.NombreComercialReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Teléfono: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.TelefonoReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 400, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Correo electrónico: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.CorreoElectronicoReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Fax: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.FaxReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Otras señas: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.DireccionReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);
                }
                lineaPos += 27;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("DETALLE DE SERVICIOS", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Cant", font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.TopLeft);
                gfx.DrawString("Código", font, XBrushes.Black, new XRect(60, lineaPos, 100, 12), XStringFormats.TopLeft);
                gfx.DrawString("Detalle", font, XBrushes.Black, new XRect(160, lineaPos, 280, 12), XStringFormats.TopLeft);
                tf.DrawString("Precio Unitario", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                tf.DrawString("Total", font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 582, lineaPos + 11);

                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                foreach (EstructuraPDFDetalleServicio linea in datos.DetalleServicio)
                {
                    lineaPos += 12;
                    string strDescripcion = linea.Detalle.Length > 60 ? linea.Detalle.Substring(0, 60) : linea.Detalle;
                    gfx.DrawString(linea.Cantidad, font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.Center);
                    gfx.DrawString(linea.Codigo, font, XBrushes.Black, new XRect(60, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(strDescripcion, font, XBrushes.Black, new XRect(160, lineaPos, 280, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.PrecioUnitario, font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.TotalLinea, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);
                }
                gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 582, lineaPos + 11);
                lineaPos += 17;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Gravado:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.TotalGravado, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Exonerado:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.TotalExonerado, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Exento:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.TotalExento, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Descuento:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.Descuento, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Impuesto:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.Impuesto, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("Total Comprobante:", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular, options);
                tf.DrawString(datos.TotalGeneral, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);

                if (datos.OtrosTextos != null)
                {
                    string stringToSplit = datos.OtrosTextos;
                    List<string> lstLines = new List<string>();
                    while (stringToSplit.Length > 0)
                    {
                        if (stringToSplit.Length > 100)
                        {
                            lstLines.Add(stringToSplit.Substring(0, 100));
                            stringToSplit = stringToSplit.Substring(100, stringToSplit.Length - 100);
                        }
                        else
                        {
                            lstLines.Add(stringToSplit.Substring(0, stringToSplit.Length));
                            break;
                        }
                    }
                    lineaPos += 27;
                    font = new XFont("Arial", 8, XFontStyle.Bold, options);
                    gfx.DrawString("Referencias: ", font, XBrushes.Black, new XRect(20, lineaPos, 40, 12), XStringFormats.TopLeft);
                    font = new XFont("Arial", 8, XFontStyle.Regular, options);
                    foreach (string element in lstLines)
                    {
                        gfx.DrawString(element, font, XBrushes.Black, new XRect(75, lineaPos, 550, 12), XStringFormats.TopLeft);
                        lineaPos += 12;
                    }
                }

                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                byte[] bytes = stream.ToArray();
                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] GenerarPDFListadoProductos(EstructuraListadoProductosPDF datos)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
                document.Info.Title = datos.TituloDocumento;
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 10, XFontStyle.BoldItalic, options);
                XTextFormatter tf = new XTextFormatter(gfx)
                {
                    Alignment = XParagraphAlignment.Right
                };
                font = new XFont("Courier New", 12, XFontStyle.Bold, options);
                gfx.DrawString(datos.TituloDocumento, font, XBrushes.Black, new XRect(0, 20, page.Width, 15), XStringFormats.TopCenter);
                int lineaPos = 23;
                foreach (EstructuraPDFDetalleProducto linea in datos.DetalleProducto)
                {
                    lineaPos += 12;
                    gfx.DrawString(linea.Descripcion, font, XBrushes.Black, new XRect(10, lineaPos, page.Width - 20, 12), XStringFormats.TopLeft);
                    lineaPos += 12;
                    gfx.DrawString(linea.Codigo, font, XBrushes.Black, new XRect(10, lineaPos, 50, 12), XStringFormats.TopCenter);
                    gfx.DrawString(linea.CodigoProveedor, font, XBrushes.Black, new XRect(60, lineaPos, 50, 12), XStringFormats.TopLeft);
                    gfx.DrawString(linea.Existencias, font, XBrushes.Black, new XRect(110, lineaPos, 30, 12), XStringFormats.TopCenter);
                    tf.DrawString(linea.PrecioCosto, font, XBrushes.Black, new XRect(140, lineaPos, 100, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.PrecioVenta, font, XBrushes.Black, new XRect(240, lineaPos, 100, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.TotalLinea, font, XBrushes.Black, new XRect(340, lineaPos, 100, 12), XStringFormats.TopLeft);
                }
                gfx.DrawString("Total inventario: " + datos.TotalInventario, font, XBrushes.Black, new XRect(0, 20, page.Width, 15), XStringFormats.TopLeft);

                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);
                byte[] bytes = stream.ToArray();
                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}