using System.Runtime.Serialization;

namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    [DataContract]
    public class RequestDTO
    {
        [DataMember]
        public string NombreMetodo { get; set; }
        [DataMember]
        public string DatosPeticion { get; set; }
    }
}
