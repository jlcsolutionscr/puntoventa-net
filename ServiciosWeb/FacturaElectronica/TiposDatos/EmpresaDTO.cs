using System.Runtime.Serialization;

namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    [DataContract]
    public class EmpresaDTO
    {
        [DataMember]
        public int? IdEmpresa { get; set; }
        [DataMember]
        public string NombreEmpresa { get; set; }
        [DataMember]
        public string IdCertificado { get; set; }
        [DataMember]
        public string PinCertificado { get; set; }
        [DataMember]
        public string UsuarioATV { get; set; }
        [DataMember]
        public string ClaveATV { get; set; }
        [DataMember]
        public string CorreoNotificacion { get; set; }
        [DataMember]
        public string PermiteFacturar { get; set; }
    }
}
