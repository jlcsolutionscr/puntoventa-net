using System;

namespace LeandroSoftware.AccesoDatos.Dominio
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
