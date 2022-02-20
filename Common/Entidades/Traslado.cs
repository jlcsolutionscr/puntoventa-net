using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Traslado
    {
        public Traslado()
        {
            DetalleTraslado = new HashSet<DetalleTraslado>();
        }

        public int IdEmpresa { get; set; }
        public int IdTraslado { get; set; }
        public int IdUsuario { get; set; }
        public int IdSucursalOrigen { get; set; }
        public string NombreSucursalOrigen { get; set; }
        public int IdSucursalDestino { get; set; }
        public string NombreSucursalDestino { get; set; }
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public int IdAsiento { get; set; }
        public bool Aplicado { get; set; }
        public int? IdAplicadoPor { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<DetalleTraslado> DetalleTraslado { get; set; }
    }
}