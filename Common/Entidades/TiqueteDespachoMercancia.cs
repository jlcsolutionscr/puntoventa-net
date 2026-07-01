using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class TiqueteDespachoMercancia
    {
        public int IdTiquete { get; set; }
        public int IdReferencia { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string FechaEmision { get; set; }
        public string Etiqueta { get; set; }
        public string Descripcion { get; set; }
        public string Impresora { get; set; }
        public string DetalleTiqueteDespachoMercancia { get; set; }
        public bool Impreso { get; set; }
    }
}