using System;
using System.Runtime.Serialization;

namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    [DataContract]
    public class DatosDocumentoElectronicoDTO
    {
        public DatosDocumentoElectronicoDTO()
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

    public enum TipoDocumento
    {
        FacturaElectronica=1,
        NotaDebitoElectronica=2,
        NotaCreditoElectronica = 3,
        TiqueteElectronico=4,
        MensajeReceptorAceptado=5,
        MensajeReceptorAceptadoParcial = 6,
        MensajeReceptorRechazado = 7
    }
}
