using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.FacturaElectronicaHacienda.Dominio.Entidades
{
    [Table("empresa")]
    public partial class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string UsuarioHacienda { get; set; }
        public string ClaveHacienda { get; set; }
        public string CorreoNotificacion { get; set; }
        public bool PermiteFacturar { get; set; }
        public string AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public int? RefreshExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? EmitedAt { get; set; }
        public byte[] Logotipo { get; set; }
    }
}
