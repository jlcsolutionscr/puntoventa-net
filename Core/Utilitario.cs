using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Management;

namespace LeandroSoftware.Core.Utilities
{
    public static class Utilitario
    {
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static bool VerificarCertificado(string strThumbprint)
        {
            bool bolCertificadoValido = false;
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, strThumbprint, true);
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
                        bolCertificadoValido = true;
                    }
                }
                else
                {
                    throw new Exception("Existe más de un certificado con la huella digital: " + strThumbprint + ". Por favor verificar.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolCertificadoValido;
        }

        public static string GenerarRespaldo(string strUser, string strPassword, string strHost, string strDatabase, string strMySQLDumpOptions)
        {
            ProcessStartInfo psi = null;
            string output = "";
            try
            {
                psi = new ProcessStartInfo
                {
                    FileName = "mysqldump",
                    RedirectStandardInput = false,
                    RedirectStandardOutput = true,
                    Arguments = strMySQLDumpOptions + " -u" + strUser + " -p" + strPassword + " -h" + strHost + " " + strDatabase,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8
                };
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

        public static string EncriptarDatos(string plainText, string passPhrase)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] keyBytes = new Rfc2898DeriveBytes(passPhrase, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DesencriptarDatos(string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            byte[] keyBytes = new Rfc2898DeriveBytes(passPhrase, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public static string ObtenerLlaveEncriptadoLocal(string strThumbprint, string strData)
        {
            RSACryptoServiceProvider rsaEncryptor = null;
            string strResult = null;
            try
            {
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, strThumbprint, true);
                store.Close();
                if (certs.Count == 0)
                {
                    throw new Exception("No se logró ubicar el certificado con la huella digital: " + strThumbprint + ". Por favor verificar.");
                }
                if (certs.Count == 1)
                {
                    foreach (X509Certificate2 cert in certs)
                    {
                        if (!cert.HasPrivateKey)
                            throw new Exception("El certificado con la huella digital: " + strThumbprint + " no posee la llave privada requerida. Por favor verificar.");
                        rsaEncryptor = (RSACryptoServiceProvider)cert.PrivateKey;
                        break;
                    }
                }
                else
                {
                    throw new Exception("Existe más de un certificado con la huella digital: " + strThumbprint + ". Por favor verificar.");
                }
                if (rsaEncryptor != null)
                {
                    byte[] cipherData = rsaEncryptor.Decrypt(Convert.FromBase64String(strData), true);
                    strResult = Encoding.UTF8.GetString(cipherData);
                }
                else
                {
                    throw new Exception("No se logró obtener un encriptador para el certificado con huella digital: " + strThumbprint + ". Por favor verificar.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strResult;
        }

        public static byte[] EncriptarArchivo(string strThumbprint, string strAppKey, string strData)
        {
            string sKey = ObtenerLlaveEncriptadoLocal(strThumbprint, strAppKey);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            var msEncrypted = new MemoryStream();
            ICryptoTransform encryptor = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(msEncrypted, encryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(strData);
            cryptostream.Write(plainBytes, 0, plainBytes.Length);
            cryptostream.Close();
            return msEncrypted.ToArray();
        }

        public static void DesencriptarArchivo(string strThumbprint, string strAppKey, string strInputFilename, string strOutPutFilename)
        {
            string sKey = ObtenerLlaveEncriptadoLocal(strThumbprint, strAppKey);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            FileStream fsread = new FileStream(strInputFilename, FileMode.Open, FileAccess.Read);
	        ICryptoTransform desdecrypt = DES.CreateDecryptor();
	        CryptoStream decryptoStream = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            StreamWriter fsDecrypted = new StreamWriter(strOutPutFilename);
	        fsDecrypted.Write(new StreamReader(decryptoStream).ReadToEnd());
	        fsDecrypted.Flush();
	        fsDecrypted.Close();
        }

        public static string ObtenerIdentificadorEquipo()
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string strVolumeSerial = dsk["VolumeSerialNumber"].ToString();
            string strProcessorId = "";
            string strBoardSerialNumber = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Processor Where DeviceID =\"CPU0\"");
                foreach (ManagementObject mo in mos.Get())
                    strProcessorId = mo["ProcessorId"].ToString();
            }
            catch
            {
                strProcessorId = "N/F-PROCESSOR";
            }
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject mo in mos.Get())
                    strBoardSerialNumber = mo["SerialNumber"].ToString();
            }
            catch
            {
                strBoardSerialNumber = "N/F-BASEBOARD";
            }
            return strProcessorId + "-" + strVolumeSerial + "-" + strBoardSerialNumber;
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
