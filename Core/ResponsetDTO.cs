using System.Runtime.Serialization;

namespace LeandroSoftware.Core.CommonTypes
{
    [DataContract]
    public class ResponseDTO
    {
        [DataMember]
        public string DatosRespuesta { get; set; }
    }
}
