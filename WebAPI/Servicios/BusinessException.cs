using System;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    [Serializable]
    public class BusinessException : Exception
    {  
        public BusinessException()
            : base() { }

        public BusinessException(string message)
            : base(message) { }
    }
}
