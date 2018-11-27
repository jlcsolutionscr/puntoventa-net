
using System.Collections.Generic;
using System.Drawing;

namespace LeandroSoftware.Core
{
    public class EstructuraPDF
    {
        public EstructuraPDF()
        {
            DetalleServicio = new HashSet<EstructuraPDFDetalleServicio>();
        }

        public string TituloDocumento { get; set; }
        public string NombreEmpresa { get; set; }
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
        public string ProvinciaReceptor { get; set; }
        public string CantonReceptor { get; set; }
        public string DistritoReceptor { get; set; }
        public string BarrioReceptor { get; set; }
        public string DireccionReceptor { get; set; }
        public string SubTotal { get; set; }
        public string Descuento { get; set; }
        public string Impuesto { get; set; }
        public string TotalGeneral { get; set; }
        public string CodigoMoneda { get; set; }
        public string TipoDeCambio { get; set; }
        public Image Logotipo { get; set; }

        public ICollection<EstructuraPDFDetalleServicio> DetalleServicio { get; set; }
    }

    public class EstructuraPDFDetalleServicio
    {
        public string NumeroLinea { get; set; }
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public string PrecioUnitario { get; set; }
        public string TotalLinea { get; set; }
    }
}
