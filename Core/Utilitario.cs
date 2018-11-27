using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using log4net;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace LeandroSoftware.Core
{
    public static class Utilitario
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string VerificarCertificadoPorNombre(string key)
        {
            string strThumbPrint = null;
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, key, true);
                store.Close();
                if (certs.Count == 0)
                {
                    throw new Exception("No se logró ubicar el certificado con la llave utilizada por el sistema. Por favor verificar.");
                }
                if (certs.Count == 1)
                {
                    foreach (X509Certificate2 cert in certs)
                    {
                        if (!cert.HasPrivateKey)
                            throw new Exception("El certificado con la llave utilizada por el sistema no posee la llave privada requerida. Por favor verificar.");
                        strThumbPrint = cert.Thumbprint;
                        break;
                    }
                }
                else
                {
                    throw new Exception("Existe más de un certificado con la huella digital: " + key + ". Por favor verificar.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error cargando el certificado:", ex);
                throw new Exception("Error al cargar la configuración del sistema. Por favor contacte a su proveedor. . .");
            }
            return strThumbPrint;
        }

        public static string GenerarRespaldo(string strUser, string strPassword, string strHost, string strDatabase, string strMySQLDumpOptions)
        {
            ProcessStartInfo psi = null;
            string output = "";
            try
            {
                psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = strMySQLDumpOptions + " -u" + strUser + " -p" + strPassword + " -h" + strHost + " " + strDatabase;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.StandardOutputEncoding = Encoding.UTF8;
                using (Process process = Process.Start(psi))
                {
                    output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public static string EncriptarDatos(string key, string strData)
        {
            RSACryptoServiceProvider rsaEncryptor = null;
            string strResult = null;
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, key, true);
                store.Close();
                if (certs.Count == 0)
                {
                    throw new Exception("No se logró ubicar el certificado con la huella digital: " + key + ". Por favor verificar.");
                }
                if (certs.Count == 1)
                {
                    foreach (X509Certificate2 cert in certs)
                    {
                        if (!cert.HasPrivateKey)
                            throw new Exception("El certificado con la huella digital: " + key + " no posee la llave privada requerida. Por favor verificar.");
                        rsaEncryptor = (RSACryptoServiceProvider)cert.PrivateKey;
                        break;
                    }
                }
                else
                {
                    throw new Exception("Existe más de un certificado con la huella digital: " + key + ". Por favor verificar.");
                }

                if (rsaEncryptor != null)
                {
                    byte[] cipherData = rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(strData), true);
                    strResult = Convert.ToBase64String(cipherData);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error cargando el certificado:", ex);
                throw new Exception("Error al cargar la configuración del sistema. Por favor contacte a su proveedor. . .");
            }
            return strResult;
        }

        public static string DesencriptarDatos(string key, string strData)
        {
            RSACryptoServiceProvider rsaEncryptor = null;
            string strResult = null;
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, key, true);
                store.Close();
                if (certs.Count == 0)
                {
                    throw new Exception("No se logró ubicar el certificado con la huella digital: " + key + ". Por favor verificar.");
                }
                if (certs.Count == 1)
                {
                    foreach (X509Certificate2 cert in certs)
                    {
                        if (!cert.HasPrivateKey)
                            throw new Exception("El certificado con la huella digital: " + key + " no posee la llave privada requerida. Por favor verificar.");
                        rsaEncryptor = (RSACryptoServiceProvider)cert.PrivateKey;
                        break;
                    }
                }
                else
                {
                    throw new Exception("Existe más de un certificado con la huella digital: " + key + ". Por favor verificar.");
                }
                if (rsaEncryptor != null)
                {
                    byte[] cipherData = rsaEncryptor.Decrypt(Convert.FromBase64String(strData), true);
                    strResult = Encoding.UTF8.GetString(cipherData);
                }
                else
                {
                    throw new Exception("No se logró obtener un encriptador para el certificado con huella digital: " + key + ". Por favor verificar.");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error cargando el certificado:", ex);
                throw new Exception("Error al cargar la configuración del sistema. Por favor contacte a su proveedor. . .");
            }
            return strResult;
        }

        public static byte[] EncriptarArchivo(string key, string AppKey, string strData)
        {
            string sKey = DesencriptarDatos(key, AppKey);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(sKey);
            DES.IV = Encoding.ASCII.GetBytes(sKey);
            var msEncrypted = new MemoryStream();
            ICryptoTransform encryptor = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(msEncrypted, encryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(strData);
            cryptostream.Write(plainBytes, 0, plainBytes.Length);
            cryptostream.Close();
            return msEncrypted.ToArray();
        }

        public static void DesencriptarArchivo(string key, string AppKey, string strInputFilename, string strOutPutFilename)
        {
            string sKey = DesencriptarDatos(key, AppKey);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
	        DES.Key = Encoding.ASCII.GetBytes(sKey);
	        DES.IV = Encoding.ASCII.GetBytes(sKey);
	        FileStream fsread = new FileStream(strInputFilename, FileMode.Open, FileAccess.Read);
	        ICryptoTransform desdecrypt = DES.CreateDecryptor();
	        CryptoStream decryptoStream = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            StreamWriter fsDecrypted = new StreamWriter(strOutPutFilename);
	        fsDecrypted.Write(new StreamReader(decryptoStream).ReadToEnd());
	        fsDecrypted.Flush();
	        fsDecrypted.Close();
        }

        public static byte[] GenerarPDFFacturaElectronica(EstructuraPDF datos)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                document.Info.Title = datos.TituloDocumento;
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XImage image = XImage.FromGdiPlusImage(datos.Logotipo);
                gfx.DrawImage(image, 340, 40, 175, 80);

                XFont font = new XFont("Courier New", 12, XFontStyle.Bold);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.Alignment = XParagraphAlignment.Right;
                gfx.DrawString(datos.NombreEmpresa.ToUpper(), font, XBrushes.Black, new XRect(20, 110, 300, 15), XStringFormats.TopLeft);
                gfx.DrawString(datos.TituloDocumento, font, XBrushes.Black, new XRect(20, 125, 200, 15), XStringFormats.TopLeft);
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

        public static string NumeroALetras(double t)
        {
            string strCadena, strText, strLetra, strDec;
            int intPos, intMax, intDecintPos;
            bool bolFlat;
            bolFlat = false;
            intPos = 0;
            strText = "";
            strCadena = Convert.ToString(t);
            intDecintPos = strCadena.IndexOf(".");
            if (intDecintPos > 0)
            {
                strDec = strCadena.Substring(strCadena.IndexOf(".") + 1).PadRight(2, '0');
                strCadena = strCadena.Substring(0, strCadena.IndexOf("."));
            }
            else
                strDec = "00";
            intMax = strCadena.Length;
            if(intMax > 9) goto Err_NToChar;
            if(intMax == 9) goto C_Millon;
            if(intMax == 8) goto D_Millon;
            if(intMax == 7) goto Millon;
            if(intMax == 6) goto C_Millar;
            if(intMax == 5) goto D_Millar;
            if(intMax == 4) goto U_Millar;
            if(intMax == 3) goto Centena;
            if(intMax == 2) goto Decena;
            if(intMax == 1) goto Unidad;
            if(intMax == 0) goto Final;
            C_Millon:
            strLetra = strCadena.Substring(intPos, 1);
            if(strLetra == "1")
            {
                if(strCadena.Substring(intPos + 1, 2) == "00")
                    strText = strText + "CIEN ";
                else
                    strText = strText + "CIENTO ";
            }
            else if(strLetra == "2")
                strText = strText + "DOSCIENTOS ";
            else if(strLetra == "3")
                strText = strText + "TRESCIENTOS ";
            else if(strLetra == "4")
                strText = strText + "CUATROCIENTOS ";
            else if(strLetra == "5")
                strText = strText + "QUINIENTOS ";
            else if(strLetra == "6")
                strText = strText + "SEISCIENTOS ";
            else if(strLetra == "7")
                strText = strText + "SETECIENTOS ";
            else if(strLetra == "8")
                strText = strText + "OCHOCIENTOS ";
            else if(strLetra == "9")
                strText = strText + "NOVECIENTOS ";
            intPos = intPos + 1;
            D_Millon:
            strLetra = strCadena.Substring(intPos, 1);
            if(strLetra == "1" && strCadena.Substring(intPos + 1, 1) == "0")
                strText = strText + "DIEZ ";
            else if(strLetra == "2")
            {
                if(strCadena.Substring(intPos + 1, 1) == "0")
                    strText = strText + "VEINTE ";
                else
                    strText = strText + "VEINTI";
            }
            else if(strLetra == "3")
                strText = strText + "TREINTA ";
            else if(strLetra == "4")
                strText = strText + "CUARENTA ";
            else if(strLetra == "5")
                strText = strText + "CINCUENTA ";
            else if(strLetra == "6")
                strText = strText + "SESENTA ";
            else if(strLetra == "7")
                strText = strText + "SETENTA ";
            else if(strLetra == "8")
                strText = strText + "OCHENTA ";
            else if(strLetra == "9")
                strText = strText + "NOVENTA ";
            if(!strLetra.Equals("0") && !strLetra.Equals("1") && !strLetra.Equals("2") && !strCadena.Substring(intPos + 1, 1).Equals("0"))
                strText = strText + "Y ";
            intPos = intPos + 1;
            Millon:
            strLetra = strCadena.Substring(intPos, 1);
            if (intMax > 7)
            {
                if (strCadena.Substring(intPos - 1, 1).Equals("1"))
                {
                    if (strLetra == "1")
                        strText = strText + "ONCE ";
                    else if (strLetra == "2")
                        strText = strText + "DOCE ";
                    else if (strLetra == "3")
                        strText = strText + "TRECE ";
                    else if (strLetra == "4")
                        strText = strText + "CATORCE ";
                    else if (strLetra == "5")
                        strText = strText + "QUINCE ";
                    else if (strLetra == "6")
                        strText = strText + "DIECISEIS ";
                    else if (strLetra == "7")
                        strText = strText + "DIECISIETE ";
                    else if (strLetra == "8")
                        strText = strText + "DIECIOCHO ";
                    else if (strLetra == "9")
                        strText = strText + "DIECINUEVE ";
                }
                else
                {
                    if (strLetra == "1")
                        strText = strText + "UN ";
                    else if (strLetra == "2")
                        strText = strText + "DOS ";
                    else if (strLetra == "3")
                        strText = strText + "TRES ";
                    else if (strLetra == "4")
                        strText = strText + "CUATRO ";
                    else if (strLetra == "5")
                        strText = strText + "CINCO ";
                    else if (strLetra == "6")
                        strText = strText + "SEIS ";
                    else if (strLetra == "7")
                        strText = strText + "SIETE ";
                    else if (strLetra == "8")
                        strText = strText + "OCHO ";
                    else if (strLetra == "9")
                        strText = strText + "NUEVE ";
                }
                strText = strText + "MILLONES ";
            }
            else
            {
                if (strLetra == "1")
                    strText = strText + "UN MILLON ";
                else if (strLetra == "2")
                    strText = strText + "DOS ";
                else if (strLetra == "3")
                    strText = strText + "TRES ";
                else if (strLetra == "4")
                    strText = strText + "CUATRO ";
                else if (strLetra == "5")
                    strText = strText + "CINCO ";
                else if (strLetra == "6")
                    strText = strText + "SEIS ";
                else if (strLetra == "7")
                    strText = strText + "SIETE ";
                else if (strLetra == "8")
                    strText = strText + "OCHO ";
                else if (strLetra == "9")
                    strText = strText + "NUEVE ";
                if(!strLetra.Equals("1"))
                        strText = strText + "MILLONES ";
                if(strCadena.Substring(intPos + 1, 6).Equals("000000"))
                    strText = strText + "DE ";
            }
            intPos = intPos + 1;
            C_Millar:
            strLetra = strCadena.Substring(intPos, 1);
            if(strLetra == "1")
            {
                if(strCadena.Substring(intPos + 1, 2).Equals("00"))
                    strText = strText + "CIEN ";
                else
                    strText = strText + "CIENTO ";
            }
            else if(strLetra == "2")
                strText = strText + "DOSCIENTOS ";
            else if(strLetra == "3")
                strText = strText + "TRESCIENTOS ";
            else if(strLetra == "4")
                strText = strText + "CUATROCIENTOS ";
            else if(strLetra == "5")
                strText = strText + "QUINIENTOS ";
            else if(strLetra == "6")
                strText = strText + "SEISCIENTOS ";
            else if(strLetra == "7")
                strText = strText + "SETECIENTOS ";
            else if(strLetra == "8")
                strText = strText + "OCHOCIENTOS ";
            else if(strLetra == "9")
                strText = strText + "NOVECIENTOS ";
            if(!strLetra.Equals("0"))
                bolFlat = true;
            intPos = intPos + 1;
            D_Millar:
            strLetra = strCadena.Substring(intPos, 1);
            if(strLetra.Equals("1") && strCadena.Substring(intPos + 1, 1).Equals("0"))
                strText = strText + "DIEZ ";
            else if(strLetra == "2")
            {
                if(strCadena.Substring(intPos + 1, 1).Equals("0"))
                    strText = strText + "VEINTE ";
                else
                    strText = strText + "VEINTI";
            }
            else if(strLetra == "3")
                strText = strText + "TREINTA ";
            else if(strLetra == "4")
                strText = strText + "CUARENTA ";
            else if(strLetra == "5")
                strText = strText + "CINCUENTA ";
            else if(strLetra == "6")
                strText = strText + "SESENTA ";
            else if(strLetra == "7")
                strText = strText + "SETENTA ";
            else if(strLetra == "8")
                strText = strText + "OCHENTA ";
            else if(strLetra == "9")
                strText = strText + "NOVENTA ";
            if(!strLetra.Equals("0") && !strLetra.Equals("1") && !strLetra.Equals("2") && !strCadena.Substring(intPos + 1, 1).Equals("0"))
                strText = strText + "Y ";
            if(!strLetra.Equals("0")) 
                bolFlat = true;
            intPos = intPos + 1;
            U_Millar:
            strLetra = strCadena.Substring(intPos, 1);
            if(intMax > 4)
            {
                if (strCadena.Substring(intPos - 1, 1).Equals("1"))
                {
                    if (strLetra == "1")
                        strText = strText + "ONCE ";
                    else if (strLetra == "2")
                        strText = strText + "DOCE ";
                    else if (strLetra == "3")
                        strText = strText + "TRECE ";
                    else if (strLetra == "4")
                        strText = strText + "CATORCE ";
                    else if (strLetra == "5")
                        strText = strText + "QUINCE ";
                    else if (strLetra == "6")
                        strText = strText + "DIECISEIS ";
                    else if (strLetra == "7")
                        strText = strText + "DIECISIETE ";
                    else if (strLetra == "8")
                        strText = strText + "DIECIOCHO ";
                    else if (strLetra == "9")
                        strText = strText + "DIECINUEVE ";
                }
                else
                {
                    if (strLetra == "1")
                        strText = strText + "UN ";
                    else if (strLetra == "2")
                        strText = strText + "DOS ";
                    else if (strLetra == "3")
                        strText = strText + "TRES ";
                    else if (strLetra == "4")
                        strText = strText + "CUATRO ";
                    else if (strLetra == "5")
                        strText = strText + "CINCO ";
                    else if (strLetra == "6")
                        strText = strText + "SEIS ";
                    else if (strLetra == "7")
                        strText = strText + "SIETE ";
                    else if (strLetra == "8")
                        strText = strText + "OCHO ";
                    else if (strLetra == "9")
                        strText = strText + "NUEVE ";
                }
            }
            else
            {
                if(strLetra == "1")
                    strText = strText + "UN ";
                else if(strLetra == "2")
                    strText = strText + "DOS ";
                else if(strLetra == "3")
                    strText = strText + "TRES ";
                else if(strLetra == "4")
                    strText = strText + "CUATRO ";
                else if(strLetra == "5")
                    strText = strText + "CINCO ";
                else if(strLetra == "6")
                    strText = strText + "SEIS ";
                else if(strLetra == "7")
                    strText = strText + "SIETE ";
                else if(strLetra == "8")
                    strText = strText + "OCHO ";
                else if(strLetra == "9")
                    strText = strText + "NUEVE ";
            }
            if(!strLetra.Equals("0"))
                bolFlat = true;
            if(bolFlat == true)
                strText = strText + "MIL ";
            intPos = intPos + 1;
            Centena:
            strLetra = strCadena.Substring(intPos, 1);
            if (strLetra == "1")
            {
                if (strCadena.Substring(intPos + 1, 2).Equals("00"))
                    strText = strText + "CIEN ";
                else
                    strText = strText + "CIENTO ";
            }
            else if (strLetra == "2")
                strText = strText + "DOSCIENTOS ";
            else if (strLetra == "3")
                strText = strText + "TRESCIENTOS ";
            else if (strLetra == "4")
                strText = strText + "CUATROCIENTOS ";
            else if (strLetra == "5")
                strText = strText + "QUINIENTOS ";
            else if (strLetra == "6")
                strText = strText + "SEISCIENTOS ";
            else if (strLetra == "7")
                strText = strText + "SETECIENTOS ";
            else if (strLetra == "8")
                strText = strText + "OCHOCIENTOS ";
            else if (strLetra == "9")
                strText = strText + "NOVECIENTOS ";
            intPos = intPos + 1;
            Decena:
            strLetra = strCadena.Substring(intPos, 1);
            if(strLetra.Equals("1") && strCadena.Substring(intPos + 1, 1).Equals("0"))
                strText = strText + "DIEZ ";
            else if(strLetra == "2")
            {
                if(strCadena.Substring(intPos + 1, 1).Equals("0"))
                    strText = strText + "VEINTE ";
                else
                    strText = strText + "VEINTI";
            }
            else if(strLetra == "3")
                strText = strText + "TREINTA ";
            else if(strLetra == "4")
                strText = strText + "CUARENTA ";
            else if(strLetra == "5")
                strText = strText + "CINCUENTA ";
            else if(strLetra == "6")
                strText = strText + "SESENTA ";
            else if(strLetra == "7")
                strText = strText + "SETENTA ";
            else if(strLetra == "8")
                strText = strText + "OCHENTA ";
            else if(strLetra == "9")
                strText = strText + "NOVENTA ";
            if(!strLetra.Equals("0") && !strLetra.Equals("1") && !strLetra.Equals("2") && !strCadena.Substring(intPos + 1, 1).Equals("0"))
                strText = strText + "Y ";
            intPos = intPos + 1;
            Unidad:
            strLetra = strCadena.Substring(intPos, 1);
            if (intMax > 1)
            {
                if (strCadena.Substring(intPos - 1, 1).Equals("1"))
                {
                    if (strLetra == "1")
                        strText = strText + "ONCE ";
                    else if (strLetra == "2")
                        strText = strText + "DOCE ";
                    else if (strLetra == "3")
                        strText = strText + "TRECE ";
                    else if (strLetra == "4")
                        strText = strText + "CATORCE ";
                    else if (strLetra == "5")
                        strText = strText + "QUINCE ";
                    else if (strLetra == "6")
                        strText = strText + "DIECISEIS ";
                    else if (strLetra == "7")
                        strText = strText + "DIECISIETE ";
                    else if (strLetra == "8")
                        strText = strText + "DIECIOCHO ";
                    else if (strLetra == "9")
                        strText = strText + "DIECINUEVE ";
                }
                else
                {
                    if (strLetra == "1")
                        strText = strText + "UN ";
                    else if (strLetra == "2")
                        strText = strText + "DOS ";
                    else if (strLetra == "3")
                        strText = strText + "TRES ";
                    else if (strLetra == "4")
                        strText = strText + "CUATRO ";
                    else if (strLetra == "5")
                        strText = strText + "CINCO ";
                    else if (strLetra == "6")
                        strText = strText + "SEIS ";
                    else if (strLetra == "7")
                        strText = strText + "SIETE ";
                    else if (strLetra == "8")
                        strText = strText + "OCHO ";
                    else if (strLetra == "9")
                        strText = strText + "NUEVE ";
                }
            }
            else
            {
                if (strLetra == "1")
                    strText = strText + "UN ";
                else if (strLetra == "2")
                    strText = strText + "DOS ";
                else if (strLetra == "3")
                    strText = strText + "TRES ";
                else if (strLetra == "4")
                    strText = strText + "CUATRO ";
                else if (strLetra == "5")
                    strText = strText + "CINCO ";
                else if (strLetra == "6")
                    strText = strText + "SEIS ";
                else if (strLetra == "7")
                    strText = strText + "SIETE ";
                else if (strLetra == "8")
                    strText = strText + "OCHO ";
                else if (strLetra == "9")
                    strText = strText + "NUEVE ";
            }
            strText = strText + "COLONES CON " + strDec + " CENTIMOS.";
            Final:
            return strText;
            Err_NToChar:
            throw new Exception("Número sobrepasa el valor de la función.");
        }
    }
}
