using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace LeandroSoftware.Updater
{
    class Program
    {
        static WebClient client = new WebClient();
        static string strMSIFileName = "puntoventaJLC.msi";
        static string strMSIFilePath = Path.GetTempPath() + strMSIFileName;
        private static string strServicioPuntoventaURL = ConfigurationManager.AppSettings["ServicioPuntoventaURL"];

        static void Main(string[] args)
        {
            Console.WriteLine("INICIO DEL PROCESO DE ACTUALIZACION. . .");
            Console.WriteLine("Cerrando procesos del sistema en memoria:");
            foreach (Process p in Process.GetProcessesByName("PuntoventaJLCApp"))
            {
                p.Kill();
            }
            Console.WriteLine("Procesos del sistema en memoria cerrados satisfactoriamente. . .\n");
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.WriteLine("Descarga de archivos de actualización iniciada:");
            try
            {
                client.DownloadFile(strServicioPuntoventaURL + "/descargaractualizacion", strMSIFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al descargar el archivo de actualización: " + ex.Message + ". Consulte con su proveedor del servicio. . .");
                Console.ReadLine();
                return;
            }
            finally
            {
                client.Dispose();
            }
            Console.WriteLine("Descarga de archivos de actualización concluida. . .\n");
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.WriteLine("Ejecutando actualización mediante instalador:\n");
            try
            {
                Process installerProcess = new Process();
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = "/i " + strMSIFilePath;
                processInfo.FileName = "msiexec";
                installerProcess = Process.Start(processInfo);
                installerProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el instalador: " + ex.Message);
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Actualización instalada satisfactoriamente. . .\n");
            Console.WriteLine("------------------------------------------------------------------\n");
            Console.WriteLine("Eliminando archivos de actualización temporales:");
            File.Delete(strMSIFilePath);
            Console.WriteLine("Archivos de actualización temporales eliminados satisfactoriamente. . .");
            Console.WriteLine("FINAL DEL PROCESO DE ACTUALIZACION. . .\n");
        }

        static string getProgramFilesx86Folder()
        {
            if (8 == IntPtr.Size || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
