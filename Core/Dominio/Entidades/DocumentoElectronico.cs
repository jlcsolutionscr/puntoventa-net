using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("documentoelectronico")]
    public partial class DocumentoElectronico
    {
        [Key]
        public int IdDocumento { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdConsecutivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Consecutivo { get; set; }
        public string ClaveNumerica { get; set; }
        public string TipoIdentificacionEmisor { get; set; }
        public string IdentificacionEmisor { get; set; }
        public string TipoIdentificacionReceptor { get; set; }
        public string IdentificacionReceptor { get; set; }
        public string EsMensajeReceptor { get; set; }
        public byte[] DatosDocumento { get; set; }
        public byte[] Respuesta { get; set; }
        public string EstadoEnvio { get; set; }
        public string ErrorEnvio { get; set; }
        public string CorreoNotificacion { get; set; }
    }
}