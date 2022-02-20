using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Apartado
    {
        public Apartado()
        {
            DetalleApartado = new HashSet<DetalleApartado>();
            DesglosePagoApartado = new HashSet<DesglosePagoApartado>();
        }

        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdApartado { get; set; }
        public int ConsecApartado { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Telefono { get; set; }
        public string TextoAdicional { get; set; }
        public int IdVendedor { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal MontoAdelanto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto; } }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Aplicado { get; set; }
        public bool Procesado { get; set; }

        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public Vendedor Vendedor { get; set; }
        public ICollection<DetalleApartado> DetalleApartado { get; set; }
        public ICollection<DesglosePagoApartado> DesglosePagoApartado { get; set; }
    }
}
