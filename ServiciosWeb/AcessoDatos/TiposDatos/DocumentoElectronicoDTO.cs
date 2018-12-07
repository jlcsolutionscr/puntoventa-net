using System;
using System.Runtime.Serialization;

namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    [DataContract]
    public class DocumentoElectronicoDTO
    {
        public DocumentoElectronicoDTO()
        {
        }

        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public int IdTipoDocumento { get; set; }
        [DataMember]
        public string ClaveNumerica { get; set; }
        [DataMember]
        public string Consecutivo { get; set; }
        [DataMember]
        public DateTime FechaEmision { get; set; }
        [DataMember]
        public string TipoIdentificacionEmisor { get; set; }
        [DataMember]
        public string IdentificacionEmisor { get; set; }
        [DataMember]
        public string TipoIdentificacionReceptor { get; set; }
        [DataMember]
        public string IdentificacionReceptor { get; set; }
        [DataMember]
        public string EsMensajeReceptor { get; set; }
        [DataMember]
        public string DatosDocumento { get; set; }
        [DataMember]
        public string RespuestaHacienda { get; set; }
        [DataMember]
        public string EstadoEnvio { get; set; }
        [DataMember]
        public string CorreoNotificacion { get; set; }
    }
}

