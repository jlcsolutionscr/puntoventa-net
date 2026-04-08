using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Notificacion
    {
        public int Id { get; set; }
        public string CorreoNotificacion { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string ErrorEnvio { get; set; }
    }
}