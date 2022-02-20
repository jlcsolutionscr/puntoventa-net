using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class DetalleMovimientoCierreCaja
    {
        public int Consecutivo { get; set; }
        public int IdCierre { get; set; }
        public int IdReferencia { get; set; }
        public int Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }

        public CierreCaja CierreCaja { get; set; }
    }
}