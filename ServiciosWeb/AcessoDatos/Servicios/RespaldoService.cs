using System;
using LeandroSoftware.Core.Utilities;
using log4net;

namespace LeandroSoftware.AccesoDatos.Servicios
{
    public interface IRespaldoService
    {
        string GenerarContenidoRespaldo(string backupUser, string backupPassword, string databaseHost, string databaseName, string mySQLDumpOptions);
        void SubirRespaldo(byte[] bytes, string fileName);
    }

    public class RespaldoService : IRespaldoService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string GenerarContenidoRespaldo (string backupUser, string backupPassword, string databaseHost, string databaseName, string mySQLDumpOptions)
        {
            string strData;
            try
            {
                strData = Utilitario.GenerarRespaldo(backupUser, backupPassword, databaseHost, databaseName, mySQLDumpOptions);
            }
            catch (Exception ex)
            {
                log.Error("Error al generar los bytes del respaldo: ", ex);
                throw new Exception("Se produjo un error al generar el archivo respaldo desde la base de datos. Por favor consulte con su proveedor.");
            }
            return strData;
        }

        public void SubirRespaldo(byte[] bytes, string fileName)
        {
            try
            {
                // Subir respaldo
            }
            catch (Exception ex)
            {
                log.Error("Error al generar el archivo binario con los bytes del respaldo: ", ex);
                throw new Exception("Se produjo un error al generar el archivo local de respaldo. Por favor consulte con su proveedor.");
            }
        }
    }
}
