using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class CredencialesHacienda
    {
        public int IdEmpresa { get; set; }
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
