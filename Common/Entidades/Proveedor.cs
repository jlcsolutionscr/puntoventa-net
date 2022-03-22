using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Proveedor
    {
        public int IdEmpresa { get; set; }
        public int IdProveedor { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Fax { get; set; }
        public string Correo { get; set; }
        public int? PlazoCredito { get; set; }
        public string Contacto1 { get; set; }
        public string TelCont1 { get; set; }
        public string Contacto2 { get; set; }
        public string TelCont2 { get; set; }
    }
}