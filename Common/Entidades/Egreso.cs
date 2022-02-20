using System;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Egreso
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdEgreso { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
    }
}