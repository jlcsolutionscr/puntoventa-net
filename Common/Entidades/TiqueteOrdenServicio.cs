using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class TiqueteOrdenServicio
    {
        public int IdTiquete { get; set; }
        public int IdOrden { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string FechaEmision { get; set; }
        public string Etiqueta { get; set; }
        public string Descripcion { get; set; }
        public string Impresora { get; set; }
        public string DetalleTiqueteOrdenServicio { get; set; }
        public bool Impreso { get; set; }

        public OrdenServicio OrdenServicio { get; set; }
    }
}