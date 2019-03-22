using System.Runtime.Serialization;

namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    [DataContract]
    public class RespuestaHaciendaDTO
    {
        [DataMember(Name = "clave")]
        public string Clave { get; set; }
        [DataMember(Name = "fecha")]
        public string Fecha { get; set; }
        [DataMember(Name = "ind-estado")]
        public string IndEstado { get; set; }
        [DataMember(Name = "respuesta-xml")]
        public string RespuestaXml { get; set; }
    }
}
