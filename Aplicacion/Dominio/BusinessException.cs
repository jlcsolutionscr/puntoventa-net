using System;

namespace LeandroSoftware.PuntoVenta.Dominio
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
