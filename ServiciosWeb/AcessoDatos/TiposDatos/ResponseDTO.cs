using System.Runtime.Serialization;

namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    [DataContract]
    public class ResponseDTO
    {
        [DataMember]
        public bool ElementoSimple { get; set; }
        [DataMember]
        public string DatosPeticion { get; set; }
    }
}
