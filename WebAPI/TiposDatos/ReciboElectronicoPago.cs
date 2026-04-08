namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago", IsNullable = false)]
    public partial class ReciboElectronicoPago
    {

        private string claveField;

        private string proveedorSistemasField;

        private string numeroConsecutivoField;

        private DateTime fechaEmisionField;

        private EmisorType emisorField;

        private ReceptorType receptorField;

        private ReciboElectronicoPagoCondicionVenta condicionVentaField;

        private ReciboElectronicoPagoLineaDetalle[] detalleServicioField;

        private ReciboElectronicoPagoResumenFactura resumenFacturaField;

        private ReciboElectronicoPagoInformacionReferencia[] informacionReferenciaField;

        private SignatureType signatureField;

        /// <remarks/>
        public string Clave
        {
            get => claveField;
            set => claveField = value;
        }

        /// <remarks/>
        public string ProveedorSistemas
        {
            get => proveedorSistemasField;
            set => proveedorSistemasField = value;
        }

        /// <remarks/>
        public string NumeroConsecutivo
        {
            get => numeroConsecutivoField;
            set => numeroConsecutivoField = value;
        }

        /// <remarks/>
        public DateTime FechaEmision
        {
            get => fechaEmisionField;
            set => fechaEmisionField = value;
        }

        /// <remarks/>
        public EmisorType Emisor
        {
            get => emisorField;
            set => emisorField = value;
        }

        /// <remarks/>
        public ReceptorType Receptor
        {
            get => receptorField;
            set => receptorField = value;
        }

        /// <remarks/>
        public ReciboElectronicoPagoCondicionVenta CondicionVenta
        {
            get => condicionVentaField;
            set => condicionVentaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalle", IsNullable = false)]
        public ReciboElectronicoPagoLineaDetalle[] DetalleServicio
        {
            get => detalleServicioField;
            set => detalleServicioField = value;
        }

        /// <remarks/>
        public ReciboElectronicoPagoResumenFactura ResumenFactura
        {
            get => resumenFacturaField;
            set => resumenFacturaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public ReciboElectronicoPagoInformacionReferencia[] InformacionReferencia
        {
            get => informacionReferenciaField;
            set => informacionReferenciaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get => signatureField;
            set => signatureField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public enum ReciboElectronicoPagoCondicionVenta
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("11")]
        Item11,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public partial class ReciboElectronicoPagoLineaDetalle
    {

        private string numeroLineaField;

        private string detalleField;

        private decimal montoTotalField;

        private decimal subTotalField;

        private ImpuestoType[] impuestoField;

        private decimal impuestoNetoField;

        private decimal montoTotalLineaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string NumeroLinea
        {
            get => numeroLineaField;
            set => numeroLineaField = value;
        }

        /// <remarks/>
        public string Detalle
        {
            get => detalleField;
            set => detalleField = value;
        }

        /// <remarks/>
        public decimal MontoTotal
        {
            get => montoTotalField;
            set => montoTotalField = value;
        }

        /// <remarks/>
        public decimal SubTotal
        {
            get => subTotalField;
            set => subTotalField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public ImpuestoType[] Impuesto
        {
            get => impuestoField;
            set => impuestoField = value;
        }

        /// <remarks/>
        public decimal ImpuestoNeto
        {
            get => impuestoNetoField;
            set => impuestoNetoField = value;
        }

        /// <remarks/>
        public decimal MontoTotalLinea
        {
            get => montoTotalLineaField;
            set => montoTotalLineaField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public partial class ReciboElectronicoPagoResumenFactura
    {

        private CodigoMonedaType codigoTipoMonedaField;

        private decimal totalVentaField;

        private decimal totalVentaNetaField;

        private ReciboElectronicoPagoResumenFacturaTotalDesgloseImpuesto[] totalDesgloseImpuestoField;

        private decimal totalImpuestoField;

        private bool totalImpuestoFieldSpecified;

        private ReciboElectronicoPagoResumenFacturaMedioPago[] medioPagoField;

        private decimal totalComprobanteField;

        /// <remarks/>
        public CodigoMonedaType CodigoTipoMoneda
        {
            get => codigoTipoMonedaField;
            set => codigoTipoMonedaField = value;
        }

        /// <remarks/>
        public decimal TotalVenta
        {
            get => totalVentaField;
            set => totalVentaField = value;
        }

        /// <remarks/>
        public decimal TotalVentaNeta
        {
            get => totalVentaNetaField;
            set => totalVentaNetaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TotalDesgloseImpuesto")]
        public ReciboElectronicoPagoResumenFacturaTotalDesgloseImpuesto[] TotalDesgloseImpuesto
        {
            get => totalDesgloseImpuestoField;
            set => totalDesgloseImpuestoField = value;
        }

        /// <remarks/>
        public decimal TotalImpuesto
        {
            get => totalImpuestoField;
            set => totalImpuestoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalImpuestoSpecified
        {
            get => totalImpuestoFieldSpecified;
            set => totalImpuestoFieldSpecified = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("MedioPago")]
        public ReciboElectronicoPagoResumenFacturaMedioPago[] MedioPago
        {
            get => medioPagoField;
            set => medioPagoField = value;
        }

        /// <remarks/>
        public decimal TotalComprobante
        {
            get => totalComprobanteField;
            set => totalComprobanteField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public partial class ReciboElectronicoPagoResumenFacturaTotalDesgloseImpuesto
    {

        private CodigoImpuestoType codigoField;

        private CodigoTarifaIVAType codigoTarifaIVAField;

        private bool codigoTarifaIVAFieldSpecified;

        private decimal totalMontoImpuestoField;

        /// <remarks/>
        public CodigoImpuestoType Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        public CodigoTarifaIVAType CodigoTarifaIVA
        {
            get => codigoTarifaIVAField;
            set => codigoTarifaIVAField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoTarifaIVASpecified
        {
            get => codigoTarifaIVAFieldSpecified;
            set => codigoTarifaIVAFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalMontoImpuesto
        {
            get => totalMontoImpuestoField;
            set => totalMontoImpuestoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public partial class ReciboElectronicoPagoResumenFacturaMedioPago
    {

        private ReciboElectronicoPagoResumenFacturaMedioPagoTipoMedioPago tipoMedioPagoField;

        private string medioPagoOtrosField;

        private decimal totalMedioPagoField;

        private bool totalMedioPagoFieldSpecified;

        /// <remarks/>
        public ReciboElectronicoPagoResumenFacturaMedioPagoTipoMedioPago TipoMedioPago
        {
            get => tipoMedioPagoField;
            set => tipoMedioPagoField = value;
        }

        /// <remarks/>
        public string MedioPagoOtros
        {
            get => medioPagoOtrosField;
            set => medioPagoOtrosField = value;
        }

        /// <remarks/>
        public decimal TotalMedioPago
        {
            get => totalMedioPagoField;
            set => totalMedioPagoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMedioPagoSpecified
        {
            get => totalMedioPagoFieldSpecified;
            set => totalMedioPagoFieldSpecified = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public enum ReciboElectronicoPagoResumenFacturaMedioPagoTipoMedioPago
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("06")]
        Item06,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("07")]
        Item07,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/reciboElectronicoPago")]
    public partial class ReciboElectronicoPagoInformacionReferencia
    {

        private TipoDocReferenciaType tipoDocIRField;

        private string tipoDocRefOTROField;

        private string numeroField;

        private DateTime fechaEmisionIRField;

        private CodigoReferenciaType codigoField;

        private bool codigoFieldSpecified;

        private string codigoReferenciaOTROField;

        private string razonField;

        /// <remarks/>
        public TipoDocReferenciaType TipoDocIR
        {
            get => tipoDocIRField;
            set => tipoDocIRField = value;
        }

        /// <remarks/>
        public string TipoDocRefOTRO
        {
            get => tipoDocRefOTROField;
            set => tipoDocRefOTROField = value;
        }

        /// <remarks/>
        public string Numero
        {
            get => numeroField;
            set => numeroField = value;
        }

        /// <remarks/>
        public DateTime FechaEmisionIR
        {
            get => fechaEmisionIRField;
            set => fechaEmisionIRField = value;
        }

        /// <remarks/>
        public CodigoReferenciaType Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoSpecified
        {
            get => codigoFieldSpecified;
            set => codigoFieldSpecified = value;
        }

        /// <remarks/>
        public string CodigoReferenciaOTRO
        {
            get => codigoReferenciaOTROField;
            set => codigoReferenciaOTROField = value;
        }

        /// <remarks/>
        public string Razon
        {
            get => razonField;
            set => razonField = value;
        }
    }
}
