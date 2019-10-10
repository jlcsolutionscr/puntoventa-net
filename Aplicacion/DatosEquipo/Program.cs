using LeandroSoftware.Core.CommonTypes;
using System;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatosEquipo
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string jsonRequest = "{\"clave\": \"102100212121\"," +
                "\"fecha\": \"" + DateTime.Now.ToString() + "\"," +
                "\"ind-estado\": \"aceptado\"," +
                "\"respuesta-xml\": \"resdede\"}";
            try
            {
                StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                Uri uri = new Uri("http://localhost/puntoventa/RecepcionWCF.svc/recibirrespuestahacienda");
                Task<HttpResponseMessage> task1 = httpClient.PostAsync(uri, stringContent);
                if (!task1.Result.IsSuccessStatusCode)
                {
                    string strErrorMessage = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                    Console.WriteLine("Error: " + strErrorMessage);
                }
            }
            catch (Exception ex)
            {
                string strErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                Console.WriteLine("Exception: " + strErrorMessage);
            }

            /* Console.WriteLine("Propiedades de win32_logicaldisk");
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            foreach (PropertyData prop in dsk.Properties)
            {
                Console.Write("Nombre: " + prop.Name.ToString());
                if (prop.Value != null)
                {
                    Console.WriteLine(" - Valor: " + prop.Value.ToString());
                }
                else
                {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Propiedades de win32_processor");
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Processor Where DeviceID =\"CPU0\"");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    Console.Write("Nombre: " + prop.Name.ToString());
                    if (prop.Value != null)
                    {
                        Console.WriteLine(" - Valor: " + prop.Value.ToString());
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            }
            Console.WriteLine("Propiedades de Win32_BaseBoard");
            mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    Console.Write("Nombre: " + prop.Name.ToString());
                    if (prop.Value != null)
                    {
                        Console.WriteLine(" - Valor: " + prop.Value.ToString());
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            }
            Console.WriteLine("Propiedades de Win32_MotherboardDevice");
            mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData prop in mo.Properties)
                {
                    Console.Write("Nombre: " + prop.Name.ToString());
                    if (prop.Value != null)
                    {
                        Console.WriteLine(" - Valor: " + prop.Value.ToString());
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
            } */
            string inputEmpresa = Console.ReadLine();
        }
    }
}
