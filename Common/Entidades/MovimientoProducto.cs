using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoProducto
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public int IdSucursal { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public string Tipo { get; set; }
        public string Origen { get; set; }
        public decimal PrecioCosto { get; set; }
    }
}