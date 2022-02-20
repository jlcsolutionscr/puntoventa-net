using System.Management;

namespace LeandroSoftware.Aplicacion
{
    public static class Utilitario
    {
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
                    strProcessorId = mo["ProcessorId"] != null ? mo["ProcessorId"].ToString() : "N/F-PROCESSOR";
            }
            catch
            {
                strProcessorId = "N/F-PROCESSOR";
            }
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject mo in mos.Get())
                    strBoardSerialNumber = mo["SerialNumber"] != null ? mo["SerialNumber"].ToString() : "N/F-BASEBOARD";
            }
            catch
            {
                strBoardSerialNumber = "N/F-BASEBOARD";
            }
            return strProcessorId + "-" + strVolumeSerial + "-" + strBoardSerialNumber;
        }

        public static decimal ObtenerPrecioRedondeado(decimal decValorRedondeo, decimal decPrecioVenta)
        {
            decimal decPrecioRedondeado = decPrecioVenta;
            string[] arrPrecioConDescuento = decPrecioVenta.ToString().Split('.');
            decimal decDecimales = arrPrecioConDescuento.Length > 1 ? decimal.Parse("0." + arrPrecioConDescuento[1].ToString()) : 0;
            decimal decTotalIncremento = decDecimales > 0 ? 1 - decDecimales : 0;
            decimal decDigitos = decimal.Parse(arrPrecioConDescuento[0].Substring(arrPrecioConDescuento[0].Length - 2)) + decDecimales;
            while ((decDigitos + decTotalIncremento) % decValorRedondeo != 0)
            {
                decTotalIncremento += 1;
            }
            if (decTotalIncremento > 0) decPrecioRedondeado += decTotalIncremento;
            return decPrecioRedondeado;
        }
    }
}
