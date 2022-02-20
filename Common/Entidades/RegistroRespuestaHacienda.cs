using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class RegistroRespuestaHacienda
    {
        public int IdRegistro { get; set; }
        public DateTime Fecha { get; set; }
        public string ClaveNumerica { get; set; }
        public byte[] Respuesta { get; set; }
    }
}