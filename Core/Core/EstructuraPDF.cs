﻿
using System.Drawing;
using System.Collections.Generic;

namespace LeandroSoftware.Core.TiposComunes
{
    public class EstructuraPDF
    {
        public EstructuraPDF()
        {
            DetalleServicio = new HashSet<EstructuraPDFDetalleServicio>();
        }

        public string TituloDocumento { get; set; }
        public string NombreComercial { get; set; }
        public string NombreEmpresa { get; set; }
        public string ConsecInterno { get; set; }
        public string Consecutivo { get; set; }
        public string PlazoCredito { get; set; }
        public string Clave { get; set; }
        public string CondicionVenta { get; set; }
        public string Fecha { get; set; }
        public string MedioPago { get; set; }
        public string NombreEmisor { get; set; }
        public string IdentificacionEmisor { get; set; }
        public string NombreComercialEmisor { get; set; }
        public string CorreoElectronicoEmisor { get; set; }
        public string TelefonoEmisor { get; set; }
        public string FaxEmisor { get; set; }
        public string ProvinciaEmisor { get; set; }
        public string CantonEmisor { get; set; }
        public string DistritoEmisor { get; set; }
        public string BarrioEmisor { get; set; }
        public string DireccionEmisor { get; set; }
        public string NombreReceptor { get; set; }
        public bool PoseeReceptor { get; set; }
        public string IdentificacionReceptor { get; set; }
        public string NombreComercialReceptor { get; set; }
        public string CorreoElectronicoReceptor { get; set; }
        public string TelefonoReceptor { get; set; }
        public string FaxReceptor { get; set; }
        public string TotalGravado { get; set; }
        public string TotalExonerado { get; set; }
        public string TotalExento { get; set; }
        public string Descuento { get; set; }
        public string Impuesto { get; set; }
        public string TotalGeneral { get; set; }
        public string OtrosTextos { get; set; }
        public string CodigoMoneda { get; set; }
        public string TipoDeCambio { get; set; }
        public Image Logotipo { get; set; }
        public Image PoweredByLogotipo { get; set; }

        public ICollection<EstructuraPDFDetalleServicio> DetalleServicio { get; set; }
    }

    public class EstructuraPDFDetalleServicio
    {
        public string Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public string PrecioUnitario { get; set; }
        public string TotalLinea { get; set; }
    }

    public class EstructuraListadoProductosPDF
    {
        public EstructuraListadoProductosPDF()
        {
            DetalleProducto = new HashSet<EstructuraPDFDetalleProducto>();
        }

        public string TituloDocumento { get; set; }
        public string TotalInventario { get; set; }
        public ICollection<EstructuraPDFDetalleProducto> DetalleProducto { get; set; }
    }

    public class EstructuraPDFDetalleProducto
    {

        public string Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Descripcion { get; set; }
        public string Existencias { get; set; }
        public string PrecioCosto { get; set; }
        public string PrecioVenta { get; set; }
        public string TotalLinea { get; set; }
    }
}
