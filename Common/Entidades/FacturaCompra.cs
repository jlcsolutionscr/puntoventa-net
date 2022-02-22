using System;
using System.Collections.Generic;

namespace LeandroSoftware.Common.Dominio.Entidades
{
    public partial class FacturaCompra
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public int IdFactCompra { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoMoneda { get; set; }
        public decimal TipoDeCambioDolar { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCondicionVenta { get; set; }
        public int PlazoCredito { get; set; }
        public string NombreEmisor { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string IdentificacionEmisor { get; set; }
        public string NombreComercialEmisor { get; set; }
        public string TelefonoEmisor { get; set; }
        public int IdProvinciaEmisor { get; set; }
        public int IdCantonEmisor { get; set; }
        public int IdDistritoEmisor { get; set; }
        public int IdBarrioEmisor { get; set; }
        public string DireccionEmisor { get; set; }
        public string CorreoElectronicoEmisor { get; set; }
        public int IdTipoExoneracion { get; set; }
        public string NumDocExoneracion { get; set; }
        public string NombreInstExoneracion { get; set; }
        public DateTime FechaEmisionDoc { get; set; }
        public int PorcentajeExoneracion { get; set; }
        public string TextoAdicional { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get { return Excento + Gravado + Exonerado + Impuesto - Descuento; } }
        public string IdDocElectronico { get; set; }
        public List<DetalleFacturaCompra> DetalleFacturaCompra { get; set; }
    }
}