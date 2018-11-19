using System.Runtime.Serialization;

namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    [DataContract]
    public class PadronDTO
    {
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public int IdProvincia { get; set; }
        [DataMember]
        public int IdCanton { get; set; }
        [DataMember]
        public int IdDistrito { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
    }
}
