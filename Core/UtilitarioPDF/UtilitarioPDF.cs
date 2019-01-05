using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using LeandroSoftware.Puntoventa.CommonTypes;
using System.IO;

namespace LeandroSoftware.Puntoventa.Utilitario
{
    public static class UtilitarioPDF
    {
        public static byte[] GenerarPDFFacturaElectronica(EstructuraPDF datos)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = datos.TituloDocumento;
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                if (datos.Logotipo != null)
                {
                    XImage image = XImage.FromGdiPlusImage(datos.Logotipo);
                    gfx.DrawImage(image, 340, 40, 175, 80);
                }

                XFont font = new XFont("Courier New", 12, XFontStyle.Bold);
                XTextFormatter tf = new XTextFormatter(gfx)
                {
                    Alignment = XParagraphAlignment.Right
                };
                gfx.DrawString(datos.NombreEmpresa.ToUpper(), font, XBrushes.Black, new XRect(20, 80, 300, 15), XStringFormats.TopLeft);
                gfx.DrawString(datos.TituloDocumento, font, XBrushes.Black, new XRect(20, 95, 200, 15), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                gfx.DrawString("Consecutivo: ", font, XBrushes.Black, new XRect(20, 160, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.Consecutivo, font, XBrushes.Black, new XRect(110, 160, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Plazo de crédito: ", font, XBrushes.Black, new XRect(370, 160, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.PlazoCredito, font, XBrushes.Black, new XRect(470, 160, 80, 12), XStringFormats.TopLeft);

                int lineaPos = 172;
                gfx.DrawString("Clave: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.Clave, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);

                gfx.DrawString("Condición de Venta: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.CondicionVenta, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Fecha: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.Fecha, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Medio de Pago: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.MedioPago, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 27;
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("DATOS DEL EMISOR", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Regular);
                gfx.DrawString("Nombre: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.NombreEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Identificación: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.IdentificacionEmisor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Nombre comercial: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.NombreComercialEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Correo electrónico: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.CorreoElectronicoEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Teléfono: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.TelefonoEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Fax: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.FaxEmisor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Provincia: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.ProvinciaEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Cantón: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.CantonEmisor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Distrito: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.DistritoEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                gfx.DrawString("Barrio: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.BarrioEmisor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Otras señas: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString(datos.DireccionEmisor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);

                lineaPos += 27;
                if (datos.PoseeReceptor)
                {
                    font = new XFont("Arial", 8, XFontStyle.Bold);
                    gfx.DrawString("DATOS DEL CLIENTE", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    font = new XFont("Arial", 8, XFontStyle.Regular);
                    gfx.DrawString("Nombre: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.NombreReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Identificación: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.IdentificacionReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Nombre comercial: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.NombreComercialReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Correo electrónico: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.CorreoElectronicoReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Teléfono: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.TelefonoReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Fax: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.FaxReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Provincia: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.ProvinciaReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Cantón: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.CantonReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Distrito: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.DistritoReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 200, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Barrio: ", font, XBrushes.Black, new XRect(370, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.BarrioReceptor, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Otras señas: ", font, XBrushes.Black, new XRect(20, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(datos.DireccionReceptor, font, XBrushes.Black, new XRect(110, lineaPos, 400, 12), XStringFormats.TopLeft);
                    lineaPos += 27;
                }

                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("DETALLE DE SERVICIOS", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                gfx.DrawString("Línea", font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.TopLeft);
                gfx.DrawString("Código", font, XBrushes.Black, new XRect(60, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawString("Detalle", font, XBrushes.Black, new XRect(140, lineaPos, 280, 12), XStringFormats.TopLeft);
                tf.DrawString("Precio Unitario", font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                tf.DrawString("Total", font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 562, lineaPos + 11);

                font = new XFont("Arial", 8, XFontStyle.Regular);
                foreach (EstructuraPDFDetalleServicio linea in datos.DetalleServicio)
                {
                    lineaPos += 12;
                    gfx.DrawString(linea.NumeroLinea, font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.Center);
                    gfx.DrawString(linea.Codigo, font, XBrushes.Black, new XRect(60, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawString(linea.Detalle, font, XBrushes.Black, new XRect(140, lineaPos, 280, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.PrecioUnitario, font, XBrushes.Black, new XRect(400, lineaPos, 80, 12), XStringFormats.TopLeft);
                    tf.DrawString(linea.TotalLinea, font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);
                }
                gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 562, lineaPos + 11);
                lineaPos += 17;
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("SubTotal Factura:", font, XBrushes.Black, new XRect(380, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                tf.DrawString(datos.SubTotal, font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("Descuento:", font, XBrushes.Black, new XRect(380, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                tf.DrawString(datos.Descuento, font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("Impuesto:", font, XBrushes.Black, new XRect(380, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                tf.DrawString(datos.Impuesto, font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);

                lineaPos += 12;
                font = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("Total General:", font, XBrushes.Black, new XRect(380, lineaPos, 80, 12), XStringFormats.TopLeft);
                font = new XFont("Arial", 8, XFontStyle.Regular);
                tf.DrawString(datos.TotalGeneral, font, XBrushes.Black, new XRect(480, lineaPos, 80, 12), XStringFormats.TopLeft);

                if (datos.CodigoMoneda != "")
                {
                    lineaPos += 32;
                    font = new XFont("Arial", 8, XFontStyle.Bold);
                    gfx.DrawString("Codigo Moneda:", font, XBrushes.Black, new XRect(70, lineaPos, 80, 12), XStringFormats.TopLeft);
                    font = new XFont("Arial", 8, XFontStyle.Regular);
                    gfx.DrawString(datos.CodigoMoneda, font, XBrushes.Black, new XRect(150, lineaPos, 80, 12), XStringFormats.TopLeft);
                    if (datos.TipoDeCambio != "")
                    {
                        lineaPos += 12;
                        font = new XFont("Arial", 8, XFontStyle.Bold);
                        gfx.DrawString("Tipo de cambio:", font, XBrushes.Black, new XRect(70, lineaPos, 80, 12), XStringFormats.TopLeft);
                        font = new XFont("Arial", 8, XFontStyle.Regular);
                        gfx.DrawString(datos.TipoDeCambio, font, XBrushes.Black, new XRect(150, lineaPos, 80, 12), XStringFormats.TopLeft);
                    }
                }

                if (datos.PoweredByLogotipo != null)
                {
                    font = new XFont("Arial", 10, XFontStyle.BoldItalic);
                    tf.DrawString("Powered by", font, XBrushes.Black, new XRect(20, gfx.PageSize.Height - 30, gfx.PageSize.Width - 137.5, 15), XStringFormats.TopLeft);
                    XImage poweredByImage = XImage.FromGdiPlusImage(datos.PoweredByLogotipo);
                    gfx.DrawImage(poweredByImage, gfx.PageSize.Width - 117.5, gfx.PageSize.Height - 50, 87.5, 40);
                }

                string filename = datos.Clave + ".pdf";
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
