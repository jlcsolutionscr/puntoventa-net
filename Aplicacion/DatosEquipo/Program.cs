using System;
using System.Management;

namespace DatosEquipo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Propiedades de win32_logicaldisk");
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
            }
            string inputEmpresa = Console.ReadLine();
        }
    }
}
