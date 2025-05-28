using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class Factura
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public int IdFactura { get; set; }
        public int ConsecFactura { get; set; }
        public int IdUsuario { get; set; }
        public string CodigoActividad { get; set; }
        public int IdTipoMoneda { get; set; }
        public decimal TipoDeCambioDolar { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public DateTime Fecha { get; set; }
        public string Telefono { get; set; }
        public string TextoAdicional { get; set; }
        public int IdVendedor { get; set; }
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string ArticuloExoneracion { get; set; }
        public string IncisoExoneracion { get; set; }
        public int IdNombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoAdelanto { get; set; }
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto; } }
        public int IdCxC { get; set; }
        public int IdAsiento { get; set; }
        public int IdMovBanco { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdProforma { get; set; }
        public int IdApartado { get; set; }
        public decimal TotalCosto { get; set; }
        public bool Nulo { get; set; }
        public int? IdAnuladoPor { get; set; }
        public string MotivoAnulacion { get; set; }
        public bool Procesado { get; set; }
        public string IdDocElectronico { get; set; }
        public string IdDocElectronicoRev { get; set; }

        public Cliente Cliente { get; set; }
        public Vendedor Vendedor { get; set; }
        public List<DetalleFactura> DetalleFactura { get; set; }
        public List<DesglosePagoFactura> DesglosePagoFactura { get; set; }
    }
}