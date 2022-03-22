using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class MovimientoBanco
    {
        public int IdMov { get; set; }
        public int IdSucursal { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipo { get; set; }
        public int IdCuenta { get; set; }
        public string Numero { get; set; }
        public string Beneficiario { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}