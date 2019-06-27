using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeandroSoftware.Core.Dominio.Entidades
{
    [Table("registrorespuestahacienda")]
    public partial class RegistroRespuestaHacienda
    {
        [Key]
        public int IdRegistro { get; set; }
        public DateTime Fecha { get; set; }
        public string ClaveNumerica { get; set; }
        public byte[] Respuesta { get; set; }
    }
}
