using System;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public class TokenType
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public int refresh_expires_in { get; set; }
        public string refresh_token { get; set; }
        public int not_before_policy { get; set; }
        public string session_state { get; set; }
        public DateTime emitedAt { get; set; }
    }
}
