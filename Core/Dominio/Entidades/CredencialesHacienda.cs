using LeandroSoftware.Core.TiposComunes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("credencialeshacienda")]
    public partial class CredencialesHacienda
    {
        [Key]
        public string Identificacion { get; set; }
        public string UsuarioHacienda { get; set; }
        public string ClaveHacienda { get; set; }
        public string AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public int? RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? EmitedAt { get; set; }
        public byte[] Certificado { get; set; }
        public string NombreCertificado { get; set; }
        public string PinCertificado { get; set; }
    }
}
