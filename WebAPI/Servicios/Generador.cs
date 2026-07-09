using LeandroSoftware.ServicioWeb.EstructuraDatos;
using LeandroSoftware.Common.Dominio.Entidades;
using System.Globalization;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;

namespace LeandroSoftware.ServicioWeb.Utilitario
{
    public static class Generador
    {
        public static byte[] GenerarPDF(EstructuraDocumentoPDF datos)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
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
                XImage image = XImage.FromStream(() => new MemoryStream(datos.Logotipo));
                gfx.DrawImage(image, 20, 60, 175, 80);
            }
            if (datos.PoweredByLogotipo != null)
            {
                tf.DrawString("Powered by", font, XBrushes.Black, new XRect(20, 35, gfx.PageSize.Width - 142.5, 15), XStringFormats.TopLeft);
                XImage poweredByImage = XImage.FromStream(() => new MemoryStream(datos.PoweredByLogotipo));
                gfx.DrawImage(poweredByImage, gfx.PageSize.Width - 117.5, 20, 87.5, 40);
            }
            font = new XFont("Courier New", 12, XFontStyle.Bold, options);
            gfx.DrawString(datos.NombreComercial.ToUpper(), font, XBrushes.Black, new XRect(210, 60, 300, 15), XStringFormats.TopLeft);
            font = new XFont("Courier New", 10, XFontStyle.Bold, options);
            gfx.DrawString(datos.NombreEmpresa.ToUpper(), font, XBrushes.Black, new XRect(210, 85, 300, 15), XStringFormats.TopLeft);
            gfx.DrawString("IDENTIFICACION: " + datos.IdentificacionEmisor, font, XBrushes.Black, new XRect(210, 100, 200, 15), XStringFormats.TopLeft);
            
            string[] direccionArray = datos.DireccionEmisor.Split(" ");
            int intLineasDireccion = 0;
            if (direccionArray.Length > 0)
            {
                intLineasDireccion = 1;
                string lineaTexto = "";
                do
                {
                    string strPalabra = direccionArray[0];
                    direccionArray = direccionArray.Skip(1).ToArray();
                    if (lineaTexto.Length > 50) {
                        gfx.DrawString(lineaTexto, font, XBrushes.Black, new XRect(210, 100 + (intLineasDireccion * 15), 200, 15), XStringFormats.TopLeft);
                        lineaTexto = strPalabra;
                        intLineasDireccion++;
                    } else {
                        lineaTexto += (lineaTexto == "" ? "" : " ") + strPalabra;
                    }
                } while (direccionArray.Length > 0);
                if (lineaTexto.Length > 0) {
                    gfx.DrawString(lineaTexto, font, XBrushes.Black, new XRect(210, 100 + (intLineasDireccion * 15), 200, 15), XStringFormats.TopLeft);
                }
            }
            gfx.DrawString("CORREO: " + datos.CorreoElectronicoEmisor, font, XBrushes.Black, new XRect(210, 115 + (intLineasDireccion * 15), 200, 15), XStringFormats.TopLeft);
            gfx.DrawString("TELEFONO: " + datos.TelefonoEmisor + (datos.FaxEmisor.Length > 0 ? " Fax: " + datos.FaxEmisor : ""), font, XBrushes.Black, new XRect(210, 130 + (intLineasDireccion * 15), 200, 15), XStringFormats.TopLeft);
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
            string strMediosPago = "";
            foreach (EstructuraFormaPagoPDF desglosePago in datos.DetalleFormaPago)
            {
                if (strMediosPago != "") strMediosPago += ", ";
                strMediosPago += desglosePago.Descripcion;
            }
            gfx.DrawString(strMediosPago, font, XBrushes.Black, new XRect(470, lineaPos, 80, 12), XStringFormats.TopLeft);
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
            }
            lineaPos += 27;
            font = new XFont("Arial", 8, XFontStyle.Bold, options);
            gfx.DrawString("DETALLE DE SERVICIOS", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

            lineaPos += 12;
            gfx.DrawString("Cant", font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.TopCenter);
            gfx.DrawString("Código", font, XBrushes.Black, new XRect(60, lineaPos, 90, 12), XStringFormats.TopLeft);
            gfx.DrawString("Detalle", font, XBrushes.Black, new XRect(150, lineaPos, 280, 12), XStringFormats.TopLeft);
            tf.DrawString("Precio Unitario", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
            tf.DrawString("Total", font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 582, lineaPos + 11);

            font = new XFont("Arial", 8, XFontStyle.Regular, options);
            int cantLineasDetalle = 0;
            int cantPaginas = 1;
            foreach (EstructuraDetalleServicioPDF linea in datos.DetalleServicio)
            {
                cantLineasDetalle += 1;
                lineaPos += 12;
                string strDescripcion = linea.Detalle.Length > 60 ? linea.Detalle.Substring(0, 60) : linea.Detalle;
                gfx.DrawString(linea.Cantidad, font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.TopCenter);
                gfx.DrawString(linea.Codigo, font, XBrushes.Black, new XRect(60, lineaPos, 90, 12), XStringFormats.TopLeft);
                gfx.DrawString(strDescripcion, font, XBrushes.Black, new XRect(150, lineaPos, 280, 12), XStringFormats.TopLeft);
                tf.DrawString(linea.PrecioUnitario, font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                tf.DrawString(linea.TotalLinea, font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);
                if ((cantPaginas == 1 && cantLineasDetalle == 27) || (cantPaginas > 1 && cantLineasDetalle == 47))
                {
                    cantPaginas += 1;
                    cantLineasDetalle = 0;
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    tf = new XTextFormatter(gfx)
                    {
                        Alignment = XParagraphAlignment.Right
                    };
                    lineaPos = 35;
                    font = new XFont("Arial", 8, XFontStyle.Bold, options);
                    gfx.DrawString("DETALLE DE SERVICIOS", font, XBrushes.Black, new XRect(20, lineaPos, 100, 12), XStringFormats.TopLeft);

                    lineaPos += 12;
                    gfx.DrawString("Cant", font, XBrushes.Black, new XRect(30, lineaPos, 30, 12), XStringFormats.TopCenter);
                    gfx.DrawString("Código", font, XBrushes.Black, new XRect(60, lineaPos, 90, 12), XStringFormats.TopLeft);
                    gfx.DrawString("Detalle", font, XBrushes.Black, new XRect(150, lineaPos, 280, 12), XStringFormats.TopLeft);
                    tf.DrawString("Precio Unitario", font, XBrushes.Black, new XRect(420, lineaPos, 80, 12), XStringFormats.TopLeft);
                    tf.DrawString("Total", font, XBrushes.Black, new XRect(500, lineaPos, 80, 12), XStringFormats.TopLeft);
                    gfx.DrawLine(XPens.DarkGray, 28, lineaPos + 11, 582, lineaPos + 11);
                    font = new XFont("Arial", 8, XFontStyle.Regular, options);
                }
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
            if (datos.EsDocumentoElectronico)
            {
                lineaPos += 24;
                font = new XFont("Arial", 8, XFontStyle.Bold, options);
                gfx.DrawString("AUTORIZADO MEDIANTE RESOLUCION DGT-R-48-2016 DEL 07-OCT-2016", font, XBrushes.Black, new XRect(20, lineaPos, 550, 12), XStringFormats.Center    );
            }
            if (datos.LeyendaPiePagina != null)
            {
                string stringToSplit = datos.LeyendaPiePagina;
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
                lineaPos += 24;
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

        public static byte[] GenerarPDFListadoProductos(EstructuraListadoProductosPDF datos)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
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
            foreach (EstructuraDetalleProductoPDF linea in datos.DetalleProducto)
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
            return stream.ToArray();
        }

        public static byte[] GenerarTiquetePDF(EstructuraDocumentoPDF datos, int intLargoLinea)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            document.Info.Title = datos.TituloDocumento;
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(intLargoLinea / 10);
            int availableChars = (int) Math.Floor(intLargoLinea / 3.0);
            List<string> lineasDescEmpresa = obtenerLineasPorAnchoDeLinea(datos.NombreComercial.ToUpper().Split(" "), availableChars);
            availableChars = (int) Math.Floor(intLargoLinea / 2.5);
            List<string> lineasDireccion = obtenerLineasPorAnchoDeLinea(datos.DireccionEmisor.ToUpper().Split(" "), availableChars);
            List<string> lineasOtroTexto = datos.OtrosTextos != null ? obtenerLineasPorAnchoDeLinea(datos.OtrosTextos.Split(" "), availableChars) : new List<string>();
            List<string> lineasLeyenda = datos.LeyendaPiePagina != null ? obtenerLineasPorAnchoDeLinea(datos.LeyendaPiePagina.Split(" "), availableChars) : new List<string>();
            page.Height = 326 + (datos.PoseeReceptor ? 12 : 0) + (lineasDescEmpresa.Count * 12) + (datos.Logotipo != null ? 45 : 0) + (lineasDireccion.Count * 12) + (datos.DetalleServicio.Count * 24) + (datos.DetalleFormaPago.Count * 12) + (datos.Clave != null ? 90 : 0) + (datos.EsDocumentoElectronico ? 36 : 0) + (datos.OtrosTextos != null ? 12 + (lineasOtroTexto.Count * 12) : 0) + (datos.LeyendaPiePagina != null ? 12 + (lineasLeyenda.Count * 12) : 0);
            double pageWidth = page.Width;
            double pageHeight = page.Height;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Right
            };
            int lineaPos = 20;
            if (datos.Logotipo != null)
            {
                double x =  (pageWidth - 120) / 2;
                XImage logoImage = XImage.FromStream(() => new MemoryStream(datos.Logotipo));
                gfx.DrawImage(logoImage, x, lineaPos, 120, 50);
                lineaPos += 50;
            }
            XFont font = new XFont("Arial", 10, XFontStyle.Bold, options);
            for (int intPos = 0; intPos < lineasDescEmpresa.Count; intPos++)
            {
                lineaPos += 12;
                gfx.DrawString(lineasDescEmpresa[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            lineaPos += 12;
            gfx.DrawString("Ced: " + datos.IdentificacionEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            for (int intPos = 0; intPos < lineasDireccion.Count; intPos++)
            {
                lineaPos += 12;
                gfx.DrawString(lineasDireccion[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            lineaPos += 12;
            gfx.DrawString(datos.CorreoElectronicoEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString("Tel: " +datos.TelefonoEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 20;
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawString("Fact. Nro: " + datos.ConsecInterno, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            gfx.DrawString("Fecha: " + datos.Fecha, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString("Atentido por: " + datos.Usuario, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString(datos.NombreReceptor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            if (datos.PoseeReceptor)
            {
                lineaPos += 12;
                gfx.DrawString(datos.IdentificacionReceptor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            lineaPos += 24;
            double dblMitadLinea = (pageWidth - 10) / 2;
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawString("Descripción", font, XBrushes.Black, new XRect(2, lineaPos, pageWidth, 12), XStringFormats.TopLeft);
            lineaPos += 12;
            gfx.DrawString("Cant.", font, XBrushes.Black, new XRect(2, lineaPos, 10, 12), XStringFormats.TopLeft);
            tf.DrawString("Precio Unit.", font, XBrushes.Black, new XRect(13, lineaPos, dblMitadLinea + 10, 12), XStringFormats.TopLeft);
            tf.DrawString("Total", font, XBrushes.Black, new XRect(dblMitadLinea + 11, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.DarkGray, 1, lineaPos + 13, pageWidth - 1, lineaPos + 13);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            foreach (EstructuraDetalleServicioPDF linea in datos.DetalleServicio)
            {
                lineaPos += 12;
                string strDescripcion = linea.Detalle.Length > availableChars ? linea.Detalle.Substring(0, availableChars) : linea.Detalle;
                gfx.DrawString(strDescripcion, font, XBrushes.Black, new XRect(2, lineaPos, pageWidth, 12), XStringFormats.TopLeft);
                lineaPos += 12;
                gfx.DrawString(linea.Cantidad, font, XBrushes.Black, new XRect(2, lineaPos, 10, 12), XStringFormats.TopLeft);
                tf.DrawString(linea.PrecioUnitario, font, XBrushes.Black, new XRect(13, lineaPos, dblMitadLinea + 10, 12), XStringFormats.TopLeft);
                tf.DrawString(linea.TotalLinea, font, XBrushes.Black, new XRect(dblMitadLinea + 11, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            }
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawLine(XPens.DarkGray, 1, lineaPos + 13, pageWidth - 1, lineaPos + 13);
            dblMitadLinea = pageWidth / 2;
            lineaPos += 17;
            gfx.DrawString("Subtotal:", font, XBrushes.Black, new XRect(12, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            tf.DrawString(datos.Subtotal, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            lineaPos += 12;
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawString("Total Impuesto:", font, XBrushes.Black, new XRect(12, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            tf.DrawString(datos.Impuesto, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            lineaPos += 12;
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawString("Total a pagar:", font, XBrushes.Black, new XRect(12, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
            tf.DrawString(datos.TotalGeneral, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            lineaPos += 22;
            gfx.DrawString("Desglose de pago", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            foreach (EstructuraFormaPagoPDF desglosePago in datos.DetalleFormaPago)
            {
                lineaPos += 12;
                gfx.DrawString(desglosePago.Descripcion, font, XBrushes.Black, new XRect(48, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
                tf.DrawString(desglosePago.Monto, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 48, 12), XStringFormats.TopLeft);
            }
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            lineaPos += 22;
            gfx.DrawString("Pago con: ", font, XBrushes.Black, new XRect(12, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
            tf.DrawString(datos.MontoPagado, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            lineaPos += 22;
            gfx.DrawString("Cambio:", font, XBrushes.Black, new XRect(12, lineaPos, dblMitadLinea, 12), XStringFormats.TopLeft);
            tf.DrawString(datos.MontoCambio, font, XBrushes.Black, new XRect(dblMitadLinea + 1, lineaPos, dblMitadLinea - 2, 12), XStringFormats.TopLeft);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            if (datos.OtrosTextos != null) {
                lineaPos += 12;
                for (int intPos = 0; intPos < lineasOtroTexto.Count; intPos++)
                {
                    lineaPos += 12;
                    gfx.DrawString(lineasOtroTexto[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                }
            }
            if (datos.EsDocumentoElectronico)
            {
                lineaPos += 20;
                gfx.DrawString(datos.TituloDocumento, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString("Clave numérica:", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString(datos.Clave.Substring(0, 25), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString(datos.Clave.Substring(25, 25), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString("Consecutivo:", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString(datos.Consecutivo, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 24;
                gfx.DrawString("AUTORIZADO MEDIANTE RESOLUCION", font, XBrushes.Black, new XRect(1, lineaPos, pageWidth, 12), XStringFormats.Center);
                lineaPos += 12;
                gfx.DrawString("DGT-R-48-2016 DEL 07-OCT-2016", font, XBrushes.Black, new XRect(1, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            if (datos.LeyendaPiePagina != null) {
                lineaPos += 12;
                for (int intPos = 0; intPos < lineasLeyenda.Count; intPos++)
                {
                    lineaPos += 12;
                    gfx.DrawString(lineasLeyenda[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
                }
            }
            lineaPos += 24;
            font = new XFont("Arial", 8, XFontStyle.BoldItalic, options);
            tf.DrawString("Powered by", font, XBrushes.Black, new XRect(1, lineaPos + 13, pageWidth - 100, 12), XStringFormats.TopLeft);
            XImage poweredByImage = XImage.FromStream(() => new MemoryStream(datos.PoweredByLogotipo));
            gfx.DrawImage(poweredByImage, pageWidth - 90, lineaPos, 88, 40);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        public static byte[] GenerarTiqueteNotaCreditoPDF(EstructuraDocumentoPDF datos, int intLargoLinea)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            document.Info.Title = datos.TituloDocumento;
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(intLargoLinea / 10);
            int availableChars = (int) Math.Floor(intLargoLinea / 3.0);
            List<string> lineasDescEmpresa = obtenerLineasPorAnchoDeLinea(datos.NombreComercial.ToUpper().Split(" "), availableChars);
            availableChars = (int) Math.Floor(intLargoLinea / 2.5);
            List<string> lineasDireccion = obtenerLineasPorAnchoDeLinea(datos.DireccionEmisor.ToUpper().Split(" "), availableChars);
            page.Height = 326 + (lineasDescEmpresa.Count * 12) + (datos.Logotipo != null ? 45 : 0) + (lineasDireccion.Count * 12);
            double pageWidth = page.Width;
            double pageHeight = page.Height;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Right
            };
            int lineaPos = 20;
            if (datos.Logotipo != null)
            {
                double x =  (pageWidth - 120) / 2;
                XImage logoImage = XImage.FromStream(() => new MemoryStream(datos.Logotipo));
                gfx.DrawImage(logoImage, x, lineaPos, 120, 50);
                lineaPos += 50;
            }
            XFont font = new XFont("Arial", 10, XFontStyle.Bold, options);
            for (int intPos = 0; intPos < lineasDescEmpresa.Count; intPos++)
            {
                lineaPos += 12;
                gfx.DrawString(lineasDescEmpresa[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            lineaPos += 12;
            gfx.DrawString("Ced: " + datos.IdentificacionEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            for (int intPos = 0; intPos < lineasDireccion.Count; intPos++)
            {
                lineaPos += 12;
                gfx.DrawString(lineasDireccion[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            lineaPos += 12;
            gfx.DrawString(datos.CorreoElectronicoEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString("Tel: " +datos.TelefonoEmisor, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 24;
            gfx.DrawString("Nota Crédito. Nro: " + datos.ConsecInterno, font, XBrushes.Black, new XRect(20, lineaPos, pageWidth, 12), XStringFormats.TopLeft);
            lineaPos += 24;
            gfx.DrawString("Fecha: " + datos.Fecha, font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 24;
            gfx.DrawString("Monto: " + datos.TotalGeneral, font, XBrushes.Black, new XRect(20, lineaPos, pageWidth, 12), XStringFormats.TopLeft);
            lineaPos += 24;
            gfx.DrawString("GRACIAS POR PREFERIRNOS", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 24;
            font = new XFont("Arial", 8, XFontStyle.BoldItalic, options);
            tf.DrawString("Powered by", font, XBrushes.Black, new XRect(1, lineaPos + 13, pageWidth - 100, 12), XStringFormats.TopLeft);
            XImage poweredByImage = XImage.FromStream(() => new MemoryStream(datos.PoweredByLogotipo));
            gfx.DrawImage(poweredByImage, pageWidth - 90, lineaPos, 88, 40);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        public static byte[] GenerarTiqueteCierreCaja(Empresa empresa, CierreCaja cierreCaja, int intLargoLinea)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            document.Info.Title = "Cierre de Efectivo de Caja";
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(intLargoLinea / 10);
            int availableChars = (int) Math.Floor(intLargoLinea / 3.0);
            page.Height = 560;
            double pageWidth = page.Width;
            double pageHeight = page.Height;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Right
            };
            int lineaPos = 20;
            XFont font = new XFont("Arial", 10, XFontStyle.Bold, options);
            gfx.DrawString("Cierre de Efectivo de Caja", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            lineaPos += 24;
            gfx.DrawString("Fecha: " + cierreCaja.FechaCierre.ToString("dd/MM/yyyy hh:mm:ss tt"), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 24;
            font = new XFont("Arial", 10, XFontStyle.Bold, options);
            AgregarLineaDescripcionValor(gfx, tf, font, "Inicio de efectivo", cierreCaja.FondoInicio, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Efectivo del cierre anterior", cierreCaja.EfectivoCierreAnterior, pageWidth, lineaPos);
            lineaPos += 24;
            gfx.DrawString("Detalle de Ingresos", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 4;
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ingresos adelanto de apartados", cierreCaja.AdelantosApartadoEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ingresos adelantos de ordenes de servicio", cierreCaja.AdelantosOrdenEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Pagos de CxC en efectivo", cierreCaja.PagosCxCEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ingresos por ventas en efectivo", cierreCaja.VentasEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Otros ingresos en efectivo", cierreCaja.IngresosEfectivo, pageWidth, lineaPos);
            font = new XFont("Arial", 10, XFontStyle.Bold);
            lineaPos += 30;
            gfx.DrawString("Detalle de Egresos", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            font = new XFont("Arial", 10, XFontStyle.Regular);
            lineaPos += 30;
            AgregarLineaDescripcionValor(gfx, tf, font, "Compras en efectivo", cierreCaja.ComprasEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Otros egresos en efectivo", cierreCaja.EgresosEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Pagos a CxP en efectivo", cierreCaja.PagosCxPEfectivo, pageWidth, lineaPos);
            font = new XFont("Arial", 10, XFontStyle.Bold);
            lineaPos += 30;
            AgregarLineaDescripcionValor(gfx, tf, font, "Cierre de efectivo de caja", cierreCaja.FondoCierre, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Retiro de efectivo de caja", cierreCaja.RetiroEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Efectivo para el siguiente cierre", cierreCaja.EfectivoCierreSiguiente, pageWidth, lineaPos);
            lineaPos += 30;
            gfx.DrawString("Detalle de ventas", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            font = new XFont("Arial", 10, XFontStyle.Regular);
            lineaPos += 30;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ventas en efectivo", cierreCaja.VentasEfectivo, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ventas en tarjeta", cierreCaja.VentasTarjeta, pageWidth, lineaPos);
            lineaPos += 20;
            AgregarLineaDescripcionValor(gfx, tf, font, "Ventas en transferencia", cierreCaja.VentasBancos, pageWidth, lineaPos);
            font = new XFont("Arial", 10, XFontStyle.Bold);
            lineaPos += 30;
            AgregarLineaDescripcionValor(gfx, tf, font, "Total de ventas", cierreCaja.VentasEfectivo + cierreCaja.VentasTarjeta + cierreCaja.VentasBancos, pageWidth, lineaPos);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        public static byte[] GenerarTiqueteNCClientePDF(NotaCreditoCliente notaCredito, byte[] bytPoweredByLogo, int intLargoLinea)
        {
            PdfDocument document = new PdfDocument();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            document.Info.Title = "Nota de crédito del cliente";
            PdfPage page = document.AddPage();
            page.Width = XUnit.FromCentimeter(intLargoLinea / 10);
            int availableChars = (int) Math.Floor(intLargoLinea / 2.5);
            List<string> lineasDetalle = obtenerLineasPorAnchoDeLinea(notaCredito.Detalle.Split(" "), availableChars);
            page.Height = 155 + (lineasDetalle.Count * 12);
            double pageWidth = page.Width;
            double pageHeight = page.Height;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Right
            };
            int lineaPos = 20;
            XFont font = new XFont("Arial", 12, XFontStyle.Bold, options);
            gfx.DrawString("NOTA DE CREDITO", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            font = new XFont("Arial", 10, XFontStyle.Regular, options);
            lineaPos += 15;
            gfx.DrawString("IdNota: " + notaCredito.IdNotaCredito.ToString(), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString("Fecha emisión: " + notaCredito.Fecha.ToString("dd/MM/yyyy hh:mm:ss tt"), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 12;
            gfx.DrawString("REFERENCIA", font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            for (int intPos = 0; intPos < lineasDetalle.Count; intPos++)
            {
                lineaPos += 12;
                gfx.DrawString(lineasDetalle[intPos], font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            }
            lineaPos += 12;
            gfx.DrawString("Monto total: " + notaCredito.MontoOriginal.ToString("N2", CultureInfo.InvariantCulture), font, XBrushes.Black, new XRect(0, lineaPos, pageWidth, 12), XStringFormats.Center);
            lineaPos += 24;
            font = new XFont("Arial", 8, XFontStyle.BoldItalic, options);
            tf.DrawString("Powered by", font, XBrushes.Black, new XRect(1, lineaPos + 13, pageWidth - 100, 12), XStringFormats.TopLeft);
            XImage poweredByImage = XImage.FromStream(() => new MemoryStream(bytPoweredByLogo));
            gfx.DrawImage(poweredByImage, pageWidth - 90, lineaPos, 88, 40);
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        private static void AgregarLineaDescripcionValor(XGraphics gfx, XTextFormatter tf, XFont font, string strEtiqueta, decimal decMonto, double pageWidth, int lineaPos)
        {
            gfx.DrawString(strEtiqueta, font, XBrushes.Black, new XRect(2, lineaPos, pageWidth - 100, 12), XStringFormats.TopLeft);
            tf.DrawString(decMonto.ToString("N2", CultureInfo.InvariantCulture), font, XBrushes.Black, new XRect(pageWidth - 101, lineaPos, 100, 12), XStringFormats.TopLeft);
        }

        private static List<string> obtenerLineasPorAnchoDeLinea(string[] palabras, int cantidadPorLinea)
        {
            List<string> lineas = new List<string>();
            if (palabras.Length > 0)
            {
                int intCantidadLineas = 1;
                string lineaTexto = "";
                do
                {
                    string strPalabra = palabras[0];
                    palabras = palabras.Skip(1).ToArray();
                    if (lineaTexto.Length > cantidadPorLinea) {
                        lineas.Add(lineaTexto);
                        lineaTexto = strPalabra;
                        intCantidadLineas++;
                    } else {
                        lineaTexto += (lineaTexto == "" ? "" : " ") + strPalabra;
                    }
                } while (palabras.Length > 0);
                if (lineaTexto.Length > 0) {
                    lineas.Add(lineaTexto);
                }
            }
            return lineas;
        }
    }
}
