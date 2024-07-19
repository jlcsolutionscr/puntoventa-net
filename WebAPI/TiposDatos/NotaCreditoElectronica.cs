namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica", IsNullable = false)]
    public partial class NotaCreditoElectronica
    {

        private string claveField;

        private string codigoActividadField;

        private string numeroConsecutivoField;

        private System.DateTime fechaEmisionField;

        private NotaCreditoElectronicaEmisorType emisorField;

        private NotaCreditoElectronicaReceptorType receptorField;

        private NotaCreditoElectronicaCondicionVenta condicionVentaField;

        private string plazoCreditoField;

        private NotaCreditoElectronicaMedioPago[] medioPagoField;

        private NotaCreditoElectronicaLineaDetalle[] detalleServicioField;

        private NotaCreditoElectronicaOtrosCargosType[] otrosCargosField;

        private NotaCreditoElectronicaResumenFactura resumenFacturaField;

        private NotaCreditoElectronicaInformacionReferencia[] informacionReferenciaField;

        private NotaCreditoElectronicaOtros otrosField;

        private SignatureType signatureField;

        /// <remarks/>
        public string Clave
        {
            get => claveField;
            set => claveField = value;
        }

        /// <remarks/>
        public string CodigoActividad
        {
            get => codigoActividadField;
            set => codigoActividadField = value;
        }

        /// <remarks/>
        public string NumeroConsecutivo
        {
            get => numeroConsecutivoField;
            set => numeroConsecutivoField = value;
        }

        /// <remarks/>
        public System.DateTime FechaEmision
        {
            get => fechaEmisionField;
            set => fechaEmisionField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaEmisorType Emisor
        {
            get => emisorField;
            set => emisorField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaReceptorType Receptor
        {
            get => receptorField;
            set => receptorField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaCondicionVenta CondicionVenta
        {
            get => condicionVentaField;
            set => condicionVentaField = value;
        }

        /// <remarks/>
        public string PlazoCredito
        {
            get => plazoCreditoField;
            set => plazoCreditoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("MedioPago")]
        public NotaCreditoElectronicaMedioPago[] MedioPago
        {
            get => medioPagoField;
            set => medioPagoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalle", IsNullable = false)]
        public NotaCreditoElectronicaLineaDetalle[] DetalleServicio
        {
            get => detalleServicioField;
            set => detalleServicioField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtrosCargos")]
        public NotaCreditoElectronicaOtrosCargosType[] OtrosCargos
        {
            get => otrosCargosField;
            set => otrosCargosField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaResumenFactura ResumenFactura
        {
            get => resumenFacturaField;
            set => resumenFacturaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public NotaCreditoElectronicaInformacionReferencia[] InformacionReferencia
        {
            get => informacionReferenciaField;
            set => informacionReferenciaField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaOtros Otros
        {
            get => otrosField;
            set => otrosField = value;
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
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaEmisorType
    {

        private string nombreField;

        private NotaCreditoElectronicaIdentificacionType identificacionField;

        private string nombreComercialField;

        private NotaCreditoElectronicaUbicacionType ubicacionField;

        private NotaCreditoElectronicaTelefonoType telefonoField;

        private NotaCreditoElectronicaTelefonoType faxField;

        private string correoElectronicoField;

        /// <remarks/>
        public string Nombre
        {
            get => nombreField;
            set => nombreField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaIdentificacionType Identificacion
        {
            get => identificacionField;
            set => identificacionField = value;
        }

        /// <remarks/>
        public string NombreComercial
        {
            get => nombreComercialField;
            set => nombreComercialField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaUbicacionType Ubicacion
        {
            get => ubicacionField;
            set => ubicacionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public NotaCreditoElectronicaTelefonoType Telefono
        {
            get => telefonoField;
            set => telefonoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public NotaCreditoElectronicaTelefonoType Fax
        {
            get => faxField;
            set => faxField = value;
        }

        /// <remarks/>
        public string CorreoElectronico
        {
            get => correoElectronicoField;
            set => correoElectronicoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaIdentificacionType
    {

        private NotaCreditoElectronicaIdentificacionTypeTipo tipoField;

        private string numeroField;

        /// <remarks/>
        public NotaCreditoElectronicaIdentificacionTypeTipo Tipo
        {
            get => tipoField;
            set => tipoField = value;
        }

        /// <remarks/>
        public string Numero
        {
            get => numeroField;
            set => numeroField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaIdentificacionTypeTipo
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
    }
 
    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaCodigoMonedaType
    {

        private NotaCreditoElectronicaCodigoMonedaTypeCodigoMoneda codigoMonedaField;

        private decimal tipoCambioField;

        /// <remarks/>
        public NotaCreditoElectronicaCodigoMonedaTypeCodigoMoneda CodigoMoneda
        {
            get => codigoMonedaField;
            set => codigoMonedaField = value;
        }

        /// <remarks/>
        public decimal TipoCambio
        {
            get => tipoCambioField;
            set => tipoCambioField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaCodigoMonedaTypeCodigoMoneda
    {

        /// <remarks/>
        AED,

        /// <remarks/>
        AFN,

        /// <remarks/>
        ALL,

        /// <remarks/>
        AMD,

        /// <remarks/>
        ANG,

        /// <remarks/>
        AOA,

        /// <remarks/>
        ARS,

        /// <remarks/>
        AUD,

        /// <remarks/>
        AWG,

        /// <remarks/>
        AZN,

        /// <remarks/>
        BAM,

        /// <remarks/>
        BBD,

        /// <remarks/>
        BDT,

        /// <remarks/>
        BGN,

        /// <remarks/>
        BHD,

        /// <remarks/>
        BIF,

        /// <remarks/>
        BMD,

        /// <remarks/>
        BND,

        /// <remarks/>
        BOB,

        /// <remarks/>
        BOV,

        /// <remarks/>
        BRL,

        /// <remarks/>
        BSD,

        /// <remarks/>
        BTN,

        /// <remarks/>
        BWP,

        /// <remarks/>
        BYR,

        /// <remarks/>
        BZD,

        /// <remarks/>
        CAD,

        /// <remarks/>
        CDF,

        /// <remarks/>
        CHE,

        /// <remarks/>
        CHF,

        /// <remarks/>
        CHW,

        /// <remarks/>
        CLF,

        /// <remarks/>
        CLP,

        /// <remarks/>
        CNY,

        /// <remarks/>
        COP,

        /// <remarks/>
        COU,

        /// <remarks/>
        CRC,

        /// <remarks/>
        CUC,

        /// <remarks/>
        CUP,

        /// <remarks/>
        CVE,

        /// <remarks/>
        CZK,

        /// <remarks/>
        DJF,

        /// <remarks/>
        DKK,

        /// <remarks/>
        DOP,

        /// <remarks/>
        DZD,

        /// <remarks/>
        EGP,

        /// <remarks/>
        ERN,

        /// <remarks/>
        ETB,

        /// <remarks/>
        EUR,

        /// <remarks/>
        FJD,

        /// <remarks/>
        FKP,

        /// <remarks/>
        GBP,

        /// <remarks/>
        GEL,

        /// <remarks/>
        GHS,

        /// <remarks/>
        GIP,

        /// <remarks/>
        GMD,

        /// <remarks/>
        GNF,

        /// <remarks/>
        GTQ,

        /// <remarks/>
        GYD,

        /// <remarks/>
        HKD,

        /// <remarks/>
        HNL,

        /// <remarks/>
        HRK,

        /// <remarks/>
        HTG,

        /// <remarks/>
        HUF,

        /// <remarks/>
        IDR,

        /// <remarks/>
        ILS,

        /// <remarks/>
        INR,

        /// <remarks/>
        IQD,

        /// <remarks/>
        IRR,

        /// <remarks/>
        ISK,

        /// <remarks/>
        JMD,

        /// <remarks/>
        JOD,

        /// <remarks/>
        JPY,

        /// <remarks/>
        KES,

        /// <remarks/>
        KGS,

        /// <remarks/>
        KHR,

        /// <remarks/>
        KMF,

        /// <remarks/>
        KPW,

        /// <remarks/>
        KRW,

        /// <remarks/>
        KWD,

        /// <remarks/>
        KYD,

        /// <remarks/>
        KZT,

        /// <remarks/>
        LAK,

        /// <remarks/>
        LBP,

        /// <remarks/>
        LKR,

        /// <remarks/>
        LRD,

        /// <remarks/>
        LSL,

        /// <remarks/>
        LYD,

        /// <remarks/>
        MAD,

        /// <remarks/>
        MDL,

        /// <remarks/>
        MGA,

        /// <remarks/>
        MKD,

        /// <remarks/>
        MMK,

        /// <remarks/>
        MNT,

        /// <remarks/>
        MOP,

        /// <remarks/>
        MRO,

        /// <remarks/>
        MUR,

        /// <remarks/>
        MVR,

        /// <remarks/>
        MWK,

        /// <remarks/>
        MXN,

        /// <remarks/>
        MXV,

        /// <remarks/>
        MYR,

        /// <remarks/>
        MZN,

        /// <remarks/>
        NAD,

        /// <remarks/>
        NGN,

        /// <remarks/>
        NIO,

        /// <remarks/>
        NOK,

        /// <remarks/>
        NPR,

        /// <remarks/>
        NZD,

        /// <remarks/>
        OMR,

        /// <remarks/>
        PAB,

        /// <remarks/>
        PEN,

        /// <remarks/>
        PGK,

        /// <remarks/>
        PHP,

        /// <remarks/>
        PKR,

        /// <remarks/>
        PLN,

        /// <remarks/>
        PYG,

        /// <remarks/>
        QAR,

        /// <remarks/>
        RON,

        /// <remarks/>
        RSD,

        /// <remarks/>
        RUB,

        /// <remarks/>
        RWF,

        /// <remarks/>
        SAR,

        /// <remarks/>
        SBD,

        /// <remarks/>
        SCR,

        /// <remarks/>
        SDG,

        /// <remarks/>
        SEK,

        /// <remarks/>
        SGD,

        /// <remarks/>
        SHP,

        /// <remarks/>
        SLL,

        /// <remarks/>
        SOS,

        /// <remarks/>
        SRD,

        /// <remarks/>
        SSP,

        /// <remarks/>
        STD,

        /// <remarks/>
        SVC,

        /// <remarks/>
        SYP,

        /// <remarks/>
        SZL,

        /// <remarks/>
        THB,

        /// <remarks/>
        TJS,

        /// <remarks/>
        TMT,

        /// <remarks/>
        TND,

        /// <remarks/>
        TOP,

        /// <remarks/>
        TRY,

        /// <remarks/>
        TTD,

        /// <remarks/>
        TWD,

        /// <remarks/>
        TZS,

        /// <remarks/>
        UAH,

        /// <remarks/>
        UGX,

        /// <remarks/>
        USD,

        /// <remarks/>
        USN,

        /// <remarks/>
        UYI,

        /// <remarks/>
        UYU,

        /// <remarks/>
        UZS,

        /// <remarks/>
        VEF,

        /// <remarks/>
        VND,

        /// <remarks/>
        VUV,

        /// <remarks/>
        WST,

        /// <remarks/>
        XAF,

        /// <remarks/>
        XAG,

        /// <remarks/>
        XAU,

        /// <remarks/>
        XBA,

        /// <remarks/>
        XBB,

        /// <remarks/>
        XBC,

        /// <remarks/>
        XBD,

        /// <remarks/>
        XCD,

        /// <remarks/>
        XDR,

        /// <remarks/>
        XOF,

        /// <remarks/>
        XPD,

        /// <remarks/>
        XPF,

        /// <remarks/>
        XPT,

        /// <remarks/>
        XSU,

        /// <remarks/>
        XTS,

        /// <remarks/>
        XUA,

        /// <remarks/>
        XXX,

        /// <remarks/>
        YER,

        /// <remarks/>
        ZAR,

        /// <remarks/>
        ZMW,

        /// <remarks/>
        ZWL,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtrosCargosType
    {

        private NotaCreditoElectronicaOtrosCargosTypeTipoDocumento tipoDocumentoField;

        private string numeroIdentidadTerceroField;

        private string nombreTerceroField;

        private string detalleField;

        private decimal porcentajeField;

        private bool porcentajeFieldSpecified;

        private decimal montoCargoField;

        /// <remarks/>
        public NotaCreditoElectronicaOtrosCargosTypeTipoDocumento TipoDocumento
        {
            get => tipoDocumentoField;
            set => tipoDocumentoField = value;
        }

        /// <remarks/>
        public string NumeroIdentidadTercero
        {
            get => numeroIdentidadTerceroField;
            set => numeroIdentidadTerceroField = value;
        }

        /// <remarks/>
        public string NombreTercero
        {
            get => nombreTerceroField;
            set => nombreTerceroField = value;
        }

        /// <remarks/>
        public string Detalle
        {
            get => detalleField;
            set => detalleField = value;
        }

        /// <remarks/>
        public decimal Porcentaje
        {
            get => porcentajeField;
            set => porcentajeField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool PorcentajeSpecified
        {
            get => porcentajeFieldSpecified;
            set => porcentajeFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoCargo
        {
            get => montoCargoField;
            set => montoCargoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaOtrosCargosTypeTipoDocumento
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
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaExoneracionType
    {

        private NotaCreditoElectronicaExoneracionTypeTipoDocumento tipoDocumentoField;

        private string numeroDocumentoField;

        private string nombreInstitucionField;

        private System.DateTime fechaEmisionField;

        private string porcentajeExoneracionField;

        private decimal montoExoneracionField;

        /// <remarks/>
        public NotaCreditoElectronicaExoneracionTypeTipoDocumento TipoDocumento
        {
            get => tipoDocumentoField;
            set => tipoDocumentoField = value;
        }

        /// <remarks/>
        public string NumeroDocumento
        {
            get => numeroDocumentoField;
            set => numeroDocumentoField = value;
        }

        /// <remarks/>
        public string NombreInstitucion
        {
            get => nombreInstitucionField;
            set => nombreInstitucionField = value;
        }

        /// <remarks/>
        public System.DateTime FechaEmision
        {
            get => fechaEmisionField;
            set => fechaEmisionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string PorcentajeExoneracion
        {
            get => porcentajeExoneracionField;
            set => porcentajeExoneracionField = value;
        }

        /// <remarks/>
        public decimal MontoExoneracion
        {
            get => montoExoneracionField;
            set => montoExoneracionField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaExoneracionTypeTipoDocumento
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
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaImpuestoType
    {

        private NotaCreditoElectronicaImpuestoTypeCodigo codigoField;

        private NotaCreditoElectronicaImpuestoTypeCodigoTarifa codigoTarifaField;

        private bool codigoTarifaFieldSpecified;

        private decimal tarifaField;

        private bool tarifaFieldSpecified;

        private decimal factorIVAField;

        private bool factorIVAFieldSpecified;

        private decimal montoField;

        private decimal montoExportacionField;

        private bool montoExportacionFieldSpecified;

        private NotaCreditoElectronicaExoneracionType exoneracionField;

        /// <remarks/>
        public NotaCreditoElectronicaImpuestoTypeCodigo Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaImpuestoTypeCodigoTarifa CodigoTarifa
        {
            get => codigoTarifaField;
            set => codigoTarifaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoTarifaSpecified
        {
            get => codigoTarifaFieldSpecified;
            set => codigoTarifaFieldSpecified = value;
        }

        /// <remarks/>
        public decimal Tarifa
        {
            get => tarifaField;
            set => tarifaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TarifaSpecified
        {
            get => tarifaFieldSpecified;
            set => tarifaFieldSpecified = value;
        }

        /// <remarks/>
        public decimal FactorIVA
        {
            get => factorIVAField;
            set => factorIVAField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool FactorIVASpecified
        {
            get => factorIVAFieldSpecified;
            set => factorIVAFieldSpecified = value;
        }

        /// <remarks/>
        public decimal Monto
        {
            get => montoField;
            set => montoField = value;
        }

        /// <remarks/>
        public decimal MontoExportacion
        {
            get => montoExportacionField;
            set => montoExportacionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoExportacionSpecified
        {
            get => montoExportacionFieldSpecified;
            set => montoExportacionFieldSpecified = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaExoneracionType Exoneracion
        {
            get => exoneracionField;
            set => exoneracionField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaImpuestoTypeCodigo
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
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaImpuestoTypeCodigoTarifa
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
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaDescuentoType
    {

        private decimal montoDescuentoField;

        private string naturalezaDescuentoField;

        /// <remarks/>
        public decimal MontoDescuento
        {
            get => montoDescuentoField;
            set => montoDescuentoField = value;
        }

        /// <remarks/>
        public string NaturalezaDescuento
        {
            get => naturalezaDescuentoField;
            set => naturalezaDescuentoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaCodigoType
    {

        private NotaCreditoElectronicaCodigoTypeTipo tipoField;

        private string codigoField;

        /// <remarks/>
        public NotaCreditoElectronicaCodigoTypeTipo Tipo
        {
            get => tipoField;
            set => tipoField = value;
        }

        /// <remarks/>
        public string Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaCodigoTypeTipo
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaReceptorType
    {

        private string nombreField;

        private NotaCreditoElectronicaIdentificacionType identificacionField;

        private string identificacionExtranjeroField;

        private string nombreComercialField;

        private NotaCreditoElectronicaUbicacionType ubicacionField;

        private string otrasSenasExtranjeroField;

        private NotaCreditoElectronicaTelefonoType telefonoField;

        private NotaCreditoElectronicaTelefonoType faxField;

        private string correoElectronicoField;

        /// <remarks/>
        public string Nombre
        {
            get => nombreField;
            set => nombreField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaIdentificacionType Identificacion
        {
            get => identificacionField;
            set => identificacionField = value;
        }

        /// <remarks/>
        public string IdentificacionExtranjero
        {
            get => identificacionExtranjeroField;
            set => identificacionExtranjeroField = value;
        }

        /// <remarks/>
        public string NombreComercial
        {
            get => nombreComercialField;
            set => nombreComercialField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaUbicacionType Ubicacion
        {
            get => ubicacionField;
            set => ubicacionField = value;
        }

        /// <remarks/>
        public string OtrasSenasExtranjero
        {
            get => otrasSenasExtranjeroField;
            set => otrasSenasExtranjeroField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaTelefonoType Telefono
        {
            get => telefonoField;
            set => telefonoField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaTelefonoType Fax
        {
            get => faxField;
            set => faxField = value;
        }

        /// <remarks/>
        public string CorreoElectronico
        {
            get => correoElectronicoField;
            set => correoElectronicoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaUbicacionType
    {

        private string provinciaField;

        private string cantonField;

        private string distritoField;

        private string barrioField;

        private string otrasSenasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string Provincia
        {
            get => provinciaField;
            set => provinciaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string Canton
        {
            get => cantonField;
            set => cantonField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string Distrito
        {
            get => distritoField;
            set => distritoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string Barrio
        {
            get => barrioField;
            set => barrioField = value;
        }

        /// <remarks/>
        public string OtrasSenas
        {
            get => otrasSenasField;
            set => otrasSenasField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaTelefonoType
    {

        private string codigoPaisField;

        private string numTelefonoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string CodigoPais
        {
            get => codigoPaisField;
            set => codigoPaisField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string NumTelefono
        {
            get => numTelefonoField;
            set => numTelefonoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaCondicionVenta
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
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaMedioPago
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaLineaDetalle
    {

        private string numeroLineaField;

        private string partidaArancelariaField;

        private string codigoField;

        private NotaCreditoElectronicaCodigoType[] codigoComercialField;

        private decimal cantidadField;

        private NotaCreditoElectronicaUnidadMedidaType unidadMedidaField;

        private string unidadMedidaComercialField;

        private string detalleField;

        private decimal precioUnitarioField;

        private decimal montoTotalField;

        private NotaCreditoElectronicaDescuentoType[] descuentoField;

        private decimal subTotalField;

        private decimal baseImponibleField;

        private bool baseImponibleFieldSpecified;

        private NotaCreditoElectronicaImpuestoType[] impuestoField;

        private decimal impuestoNetoField;

        private bool impuestoNetoFieldSpecified;

        private decimal montoTotalLineaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string NumeroLinea
        {
            get => numeroLineaField;
            set => numeroLineaField = value;
        }

        /// <remarks/>
        public string PartidaArancelaria
        {
            get => partidaArancelariaField;
            set => partidaArancelariaField = value;
        }

        /// <remarks/>
        public string Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CodigoComercial")]
        public NotaCreditoElectronicaCodigoType[] CodigoComercial
        {
            get => codigoComercialField;
            set => codigoComercialField = value;
        }

        /// <remarks/>
        public decimal Cantidad
        {
            get => cantidadField;
            set => cantidadField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaUnidadMedidaType UnidadMedida
        {
            get => unidadMedidaField;
            set => unidadMedidaField = value;
        }

        /// <remarks/>
        public string UnidadMedidaComercial
        {
            get => unidadMedidaComercialField;
            set => unidadMedidaComercialField = value;
        }

        /// <remarks/>
        public string Detalle
        {
            get => detalleField;
            set => detalleField = value;
        }

        /// <remarks/>
        public decimal PrecioUnitario
        {
            get => precioUnitarioField;
            set => precioUnitarioField = value;
        }

        /// <remarks/>
        public decimal MontoTotal
        {
            get => montoTotalField;
            set => montoTotalField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Descuento")]
        public NotaCreditoElectronicaDescuentoType[] Descuento
        {
            get => descuentoField;
            set => descuentoField = value;
        }

        /// <remarks/>
        public decimal SubTotal
        {
            get => subTotalField;
            set => subTotalField = value;
        }

        /// <remarks/>
        public decimal BaseImponible
        {
            get => baseImponibleField;
            set => baseImponibleField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool BaseImponibleSpecified
        {
            get => baseImponibleFieldSpecified;
            set => baseImponibleFieldSpecified = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public NotaCreditoElectronicaImpuestoType[] Impuesto
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
        [System.Xml.Serialization.XmlIgnore()]
        public bool ImpuestoNetoSpecified
        {
            get => impuestoNetoFieldSpecified;
            set => impuestoNetoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoTotalLinea
        {
            get => montoTotalLineaField;
            set => montoTotalLineaField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaUnidadMedidaType
    {

        /// <remarks/>
        Al,

        /// <remarks/>
        Alc,

        /// <remarks/>
        Cm,

        /// <remarks/>
        I,

        /// <remarks/>
        Os,

        /// <remarks/>
        Sp,

        /// <remarks/>
        Spe,

        /// <remarks/>
        St,

        /// <remarks/>
        m,

        /// <remarks/>
        kg,

        /// <remarks/>
        s,

        /// <remarks/>
        A,

        /// <remarks/>
        K,

        /// <remarks/>
        mol,

        /// <remarks/>
        cd,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m²")]
        m1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m³")]
        m2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m/s")]
        ms,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m/s²")]
        ms1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1/m")]
        Item1m,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("kg/m³")]
        kgm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("A/m²")]
        Am,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("A/m")]
        Am1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("mol/m³")]
        molm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("cd/m²")]
        cdm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <remarks/>
        rad,

        /// <remarks/>
        sr,

        /// <remarks/>
        Hz,

        /// <remarks/>
        N,

        /// <remarks/>
        Pa,

        /// <remarks/>
        J,

        /// <remarks/>
        W,

        /// <remarks/>
        C,

        /// <remarks/>
        V,

        /// <remarks/>
        F,

        /// <remarks/>
        Ω,

        /// <remarks/>
        S,

        /// <remarks/>
        Wb,

        /// <remarks/>
        T,

        /// <remarks/>
        H,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("°C")]
        C1,

        /// <remarks/>
        lm,

        /// <remarks/>
        lx,

        /// <remarks/>
        Bq,

        /// <remarks/>
        Gy,

        /// <remarks/>
        Sv,

        /// <remarks/>
        kat,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("Pa·s")]
        Pas,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("N·m")]
        Nm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("N/m")]
        Nm1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("rad/s")]
        rads,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("rad/s²")]
        rads1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/m²")]
        Wm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/K")]
        JK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/(kg·K)")]
        JkgK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/kg")]
        Jkg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/(m·K)")]
        WmK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/m³")]
        Jm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("V/m")]
        Vm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/m³")]
        Cm1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/m²")]
        Cm2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("F/m")]
        Fm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("H/m")]
        Hm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/mol")]
        Jmol,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/(mol·K)")]
        JmolK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/kg")]
        Ckg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("Gy/s")]
        Gys,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/sr")]
        Wsr,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/(m²·sr)")]
        Wmsr,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("kat/m³")]
        katm,

        /// <remarks/>
        min,

        /// <remarks/>
        h,

        /// <remarks/>
        d,

        /// <remarks/>
        º,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("´")]
        Item,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("´´")]
        Item2,

        /// <remarks/>
        L,

        /// <remarks/>
        t,

        /// <remarks/>
        Np,

        /// <remarks/>
        B,

        /// <remarks/>
        eV,

        /// <remarks/>
        u,

        /// <remarks/>
        ua,

        /// <remarks/>
        Unid,

        /// <remarks/>
        Gal,

        /// <remarks/>
        g,

        /// <remarks/>
        Km,

        /// <remarks/>
        Kw,

        /// <remarks/>
        ln,

        /// <remarks/>
        cm,

        /// <remarks/>
        mL,

        /// <remarks/>
        mm,

        /// <remarks/>
        Oz,

        /// <remarks/>
        Otros,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaResumenFactura
    {

        private NotaCreditoElectronicaCodigoMonedaType codigoTipoMonedaField;

        private decimal totalServGravadosField;

        private bool totalServGravadosFieldSpecified;

        private decimal totalServExentosField;

        private bool totalServExentosFieldSpecified;

        private decimal totalServExoneradoField;

        private bool totalServExoneradoFieldSpecified;

        private decimal totalMercanciasGravadasField;

        private bool totalMercanciasGravadasFieldSpecified;

        private decimal totalMercanciasExentasField;

        private bool totalMercanciasExentasFieldSpecified;

        private decimal totalMercExoneradaField;

        private bool totalMercExoneradaFieldSpecified;

        private decimal totalGravadoField;

        private bool totalGravadoFieldSpecified;

        private decimal totalExentoField;

        private bool totalExentoFieldSpecified;

        private decimal totalExoneradoField;

        private bool totalExoneradoFieldSpecified;

        private decimal totalVentaField;

        private decimal totalDescuentosField;

        private bool totalDescuentosFieldSpecified;

        private decimal totalVentaNetaField;

        private decimal totalImpuestoField;

        private bool totalImpuestoFieldSpecified;

        private decimal totalIVADevueltoField;

        private bool totalIVADevueltoFieldSpecified;

        private decimal totalOtrosCargosField;

        private bool totalOtrosCargosFieldSpecified;

        private decimal totalComprobanteField;

        /// <remarks/>
        public NotaCreditoElectronicaCodigoMonedaType CodigoTipoMoneda
        {
            get => codigoTipoMonedaField;
            set => codigoTipoMonedaField = value;
        }

        /// <remarks/>
        public decimal TotalServGravados
        {
            get => totalServGravadosField;
            set => totalServGravadosField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServGravadosSpecified
        {
            get => totalServGravadosFieldSpecified;
            set => totalServGravadosFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalServExentos
        {
            get => totalServExentosField;
            set => totalServExentosField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExentosSpecified
        {
            get => totalServExentosFieldSpecified;
            set => totalServExentosFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalServExonerado
        {
            get => totalServExoneradoField;
            set => totalServExoneradoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExoneradoSpecified
        {
            get => totalServExoneradoFieldSpecified;
            set => totalServExoneradoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalMercanciasGravadas
        {
            get => totalMercanciasGravadasField;
            set => totalMercanciasGravadasField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasGravadasSpecified
        {
            get => totalMercanciasGravadasFieldSpecified;
            set => totalMercanciasGravadasFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalMercanciasExentas
        {
            get => totalMercanciasExentasField;
            set => totalMercanciasExentasField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasExentasSpecified
        {
            get => totalMercanciasExentasFieldSpecified;
            set => totalMercanciasExentasFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalMercExonerada
        {
            get => totalMercExoneradaField;
            set => totalMercExoneradaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercExoneradaSpecified
        {
            get => totalMercExoneradaFieldSpecified;
            set => totalMercExoneradaFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalGravado
        {
            get => totalGravadoField;
            set => totalGravadoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalGravadoSpecified
        {
            get => totalGravadoFieldSpecified;
            set => totalGravadoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalExento
        {
            get => totalExentoField;
            set => totalExentoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExentoSpecified
        {
            get => totalExentoFieldSpecified;
            set => totalExentoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalExonerado
        {
            get => totalExoneradoField;
            set => totalExoneradoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExoneradoSpecified
        {
            get => totalExoneradoFieldSpecified;
            set => totalExoneradoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalVenta
        {
            get => totalVentaField;
            set => totalVentaField = value;
        }

        /// <remarks/>
        public decimal TotalDescuentos
        {
            get => totalDescuentosField;
            set => totalDescuentosField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalDescuentosSpecified
        {
            get => totalDescuentosFieldSpecified;
            set => totalDescuentosFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalVentaNeta
        {
            get => totalVentaNetaField;
            set => totalVentaNetaField = value;
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
        public decimal TotalIVADevuelto
        {
            get => totalIVADevueltoField;
            set => totalIVADevueltoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalIVADevueltoSpecified
        {
            get => totalIVADevueltoFieldSpecified;
            set => totalIVADevueltoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalOtrosCargos
        {
            get => totalOtrosCargosField;
            set => totalOtrosCargosField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalOtrosCargosSpecified
        {
            get => totalOtrosCargosFieldSpecified;
            set => totalOtrosCargosFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalComprobante
        {
            get => totalComprobanteField;
            set => totalComprobanteField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaInformacionReferencia
    {

        private NotaCreditoElectronicaInformacionReferenciaTipoDoc tipoDocField;

        private string numeroField;

        private System.DateTime fechaEmisionField;

        private NotaCreditoElectronicaInformacionReferenciaCodigo codigoField;

        private string razonField;

        /// <remarks/>
        public NotaCreditoElectronicaInformacionReferenciaTipoDoc TipoDoc
        {
            get => tipoDocField;
            set => tipoDocField = value;
        }

        /// <remarks/>
        public string Numero
        {
            get => numeroField;
            set => numeroField = value;
        }

        /// <remarks/>
        public System.DateTime FechaEmision
        {
            get => fechaEmisionField;
            set => fechaEmisionField = value;
        }

        /// <remarks/>
        public NotaCreditoElectronicaInformacionReferenciaCodigo Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        public string Razon
        {
            get => razonField;
            set => razonField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaInformacionReferenciaTipoDoc
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
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("09")]
        Item09,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("10")]
        Item10,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("")]
        Item,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaInformacionReferenciaCodigo
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtros
    {

        private NotaCreditoElectronicaOtrosOtroTexto[] otroTextoField;

        private NotaCreditoElectronicaOtrosOtroContenido[] otroContenidoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroTexto")]
        public NotaCreditoElectronicaOtrosOtroTexto[] OtroTexto
        {
            get => otroTextoField;
            set => otroTextoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroContenido")]
        public NotaCreditoElectronicaOtrosOtroContenido[] OtroContenido
        {
            get => otroContenidoField;
            set => otroContenidoField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtrosOtroTexto
    {

        private string codigoField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get => valueField;
            set => valueField = value;
        }
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtrosOtroContenido
    {

        private System.Xml.XmlElement anyField;

        private string codigoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement Any
        {
            get => anyField;
            set => anyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo
        {
            get => codigoField;
            set => codigoField = value;
        }
    }
}
