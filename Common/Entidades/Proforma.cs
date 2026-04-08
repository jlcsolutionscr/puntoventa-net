using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Proforma
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdProforma { get; set; }
        public int ConsecProforma { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public string TextoAdicional { get; set; }
        public string Telefono { get; set; }
        public int IdVendedor { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto; } }
        public decimal TipoDeCambioDolar { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Aplicado { get; set; }

        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public List<DetalleProforma> DetalleProforma { get; set; }
    }
}