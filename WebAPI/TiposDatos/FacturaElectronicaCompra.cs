namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    [System.Xml.Serialization.XmlRoot(Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra", IsNullable=false)]
    public partial class FacturaElectronicaCompra {
        
        private string claveField;
        
        private string codigoActividadField;
        
        private string numeroConsecutivoField;
        
        private System.DateTime fechaEmisionField;
        
        private FacturaElectronicaCompraEmisorType emisorField;
        
        private FacturaElectronicaCompraReceptorType receptorField;
        
        private FacturaElectronicaCompraCondicionVenta condicionVentaField;
        
        private string plazoCreditoField;
        
        private FacturaElectronicaCompraMedioPago[] medioPagoField;
        
        private FacturaElectronicaCompraLineaDetalle[] detalleServicioField;
        
        private FacturaElectronicaCompraOtrosCargosType[] otrosCargosField;
        
        private FacturaElectronicaCompraResumenFactura resumenFacturaField;
        
        private FacturaElectronicaCompraInformacionReferencia[] informacionReferenciaField;
        
        private FacturaElectronicaCompraOtros otrosField;
        
        private SignatureType signatureField;
        
        /// <remarks/>
        public string Clave {
            get {
                return this.claveField;
            }
            set {
                this.claveField = value;
            }
        }
        
        /// <remarks/>
        public string CodigoActividad {
            get {
                return this.codigoActividadField;
            }
            set {
                this.codigoActividadField = value;
            }
        }
        
        /// <remarks/>
        public string NumeroConsecutivo {
            get {
                return this.numeroConsecutivoField;
            }
            set {
                this.numeroConsecutivoField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime FechaEmision {
            get {
                return this.fechaEmisionField;
            }
            set {
                this.fechaEmisionField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraEmisorType Emisor {
            get {
                return this.emisorField;
            }
            set {
                this.emisorField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraReceptorType Receptor {
            get {
                return this.receptorField;
            }
            set {
                this.receptorField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraCondicionVenta CondicionVenta {
            get {
                return this.condicionVentaField;
            }
            set {
                this.condicionVentaField = value;
            }
        }
        
        /// <remarks/>
        public string PlazoCredito {
            get {
                return this.plazoCreditoField;
            }
            set {
                this.plazoCreditoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("MedioPago")]
        public FacturaElectronicaCompraMedioPago[] MedioPago {
            get {
                return this.medioPagoField;
            }
            set {
                this.medioPagoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalle", IsNullable=false)]
        public FacturaElectronicaCompraLineaDetalle[] DetalleServicio {
            get {
                return this.detalleServicioField;
            }
            set {
                this.detalleServicioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtrosCargos")]
        public FacturaElectronicaCompraOtrosCargosType[] OtrosCargos {
            get {
                return this.otrosCargosField;
            }
            set {
                this.otrosCargosField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraResumenFactura ResumenFactura {
            get {
                return this.resumenFacturaField;
            }
            set {
                this.resumenFacturaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public FacturaElectronicaCompraInformacionReferencia[] InformacionReferencia {
            get {
                return this.informacionReferenciaField;
            }
            set {
                this.informacionReferenciaField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraOtros Otros {
            get {
                return this.otrosField;
            }
            set {
                this.otrosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraEmisorType {
        private string nombreField;
        
        private FacturaElectronicaCompraIdentificacionType identificacionField;
        
        private string nombreComercialField;
        
        private FacturaElectronicaCompraUbicacionType ubicacionField;
        
        private FacturaElectronicaCompraTelefonoType telefonoField;
        
        private FacturaElectronicaCompraTelefonoType faxField;
        
        private string correoElectronicoField;
        
        /// <remarks/>
        public string Nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraIdentificacionType Identificacion {
            get {
                return this.identificacionField;
            }
            set {
                this.identificacionField = value;
            }
        }
        
        /// <remarks/>
        public string NombreComercial {
            get {
                return this.nombreComercialField;
            }
            set {
                this.nombreComercialField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraUbicacionType Ubicacion {
            get {
                return this.ubicacionField;
            }
            set {
                this.ubicacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable=true)]
        public FacturaElectronicaCompraTelefonoType Telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable=true)]
        public FacturaElectronicaCompraTelefonoType Fax {
            get {
                return this.faxField;
            }
            set {
                this.faxField = value;
            }
        }
        
        /// <remarks/>
        public string CorreoElectronico {
            get {
                return this.correoElectronicoField;
            }
            set {
                this.correoElectronicoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraIdentificacionType {
        private FacturaElectronicaCompraIdentificacionTypeTipo tipoField;
        
        private string numeroField;
        
        /// <remarks/>
        public FacturaElectronicaCompraIdentificacionTypeTipo Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
            }
        }
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraIdentificacionTypeTipo {
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

    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraCodigoMonedaType {
        private FacturaElectronicaCompraCodigoMonedaTypeCodigoMoneda codigoMonedaField;
        
        private decimal tipoCambioField;
        
        /// <remarks/>
        public FacturaElectronicaCompraCodigoMonedaTypeCodigoMoneda CodigoMoneda {
            get {
                return this.codigoMonedaField;
            }
            set {
                this.codigoMonedaField = value;
            }
        }
        
        /// <remarks/>
        public decimal TipoCambio {
            get {
                return this.tipoCambioField;
            }
            set {
                this.tipoCambioField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraCodigoMonedaTypeCodigoMoneda {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtrosCargosType {
        private FacturaElectronicaCompraOtrosCargosTypeTipoDocumento tipoDocumentoField;
        
        private string detalleField;
        
        private decimal porcentajeField;
        
        private bool porcentajeFieldSpecified;
        
        private decimal montoCargoField;
        
        /// <remarks/>
        public FacturaElectronicaCompraOtrosCargosTypeTipoDocumento TipoDocumento {
            get {
                return this.tipoDocumentoField;
            }
            set {
                this.tipoDocumentoField = value;
            }
        }
        
        /// <remarks/>
        public string Detalle {
            get {
                return this.detalleField;
            }
            set {
                this.detalleField = value;
            }
        }
        
        /// <remarks/>
        public decimal Porcentaje {
            get {
                return this.porcentajeField;
            }
            set {
                this.porcentajeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool PorcentajeSpecified {
            get {
                return this.porcentajeFieldSpecified;
            }
            set {
                this.porcentajeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal MontoCargo {
            get {
                return this.montoCargoField;
            }
            set {
                this.montoCargoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraOtrosCargosTypeTipoDocumento {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraExoneracionType {
        private FacturaElectronicaCompraExoneracionTypeTipoDocumento tipoDocumentoField;
        
        private string numeroDocumentoField;
        
        private string nombreInstitucionField;
        
        private System.DateTime fechaEmisionField;
        
        private string porcentajeExoneracionField;
        
        private decimal montoExoneracionField;
        
        /// <remarks/>
        public FacturaElectronicaCompraExoneracionTypeTipoDocumento TipoDocumento {
            get {
                return this.tipoDocumentoField;
            }
            set {
                this.tipoDocumentoField = value;
            }
        }
        
        /// <remarks/>
        public string NumeroDocumento {
            get {
                return this.numeroDocumentoField;
            }
            set {
                this.numeroDocumentoField = value;
            }
        }
        
        /// <remarks/>
        public string NombreInstitucion {
            get {
                return this.nombreInstitucionField;
            }
            set {
                this.nombreInstitucionField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime FechaEmision {
            get {
                return this.fechaEmisionField;
            }
            set {
                this.fechaEmisionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string PorcentajeExoneracion {
            get {
                return this.porcentajeExoneracionField;
            }
            set {
                this.porcentajeExoneracionField = value;
            }
        }
        
        /// <remarks/>
        public decimal MontoExoneracion {
            get {
                return this.montoExoneracionField;
            }
            set {
                this.montoExoneracionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraExoneracionTypeTipoDocumento {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraImpuestoType {
        private FacturaElectronicaCompraImpuestoTypeCodigo codigoField;
        
        private FacturaElectronicaCompraImpuestoTypeCodigoTarifa codigoTarifaField;
        
        private bool codigoTarifaFieldSpecified;
        
        private decimal tarifaField;
        
        private bool tarifaFieldSpecified;
        
        private decimal factorIVAField;
        
        private bool factorIVAFieldSpecified;
        
        private decimal montoField;
        
        private FacturaElectronicaCompraExoneracionType exoneracionField;
        
        /// <remarks/>
        public FacturaElectronicaCompraImpuestoTypeCodigo Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraImpuestoTypeCodigoTarifa CodigoTarifa {
            get {
                return this.codigoTarifaField;
            }
            set {
                this.codigoTarifaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoTarifaSpecified {
            get {
                return this.codigoTarifaFieldSpecified;
            }
            set {
                this.codigoTarifaFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal Tarifa {
            get {
                return this.tarifaField;
            }
            set {
                this.tarifaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TarifaSpecified {
            get {
                return this.tarifaFieldSpecified;
            }
            set {
                this.tarifaFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal FactorIVA {
            get {
                return this.factorIVAField;
            }
            set {
                this.factorIVAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool FactorIVASpecified {
            get {
                return this.factorIVAFieldSpecified;
            }
            set {
                this.factorIVAFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal Monto {
            get {
                return this.montoField;
            }
            set {
                this.montoField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraExoneracionType Exoneracion {
            get {
                return this.exoneracionField;
            }
            set {
                this.exoneracionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraImpuestoTypeCodigo {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraImpuestoTypeCodigoTarifa {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraDescuentoType {
        private decimal montoDescuentoField;
        
        private string naturalezaDescuentoField;
        
        /// <remarks/>
        public decimal MontoDescuento {
            get {
                return this.montoDescuentoField;
            }
            set {
                this.montoDescuentoField = value;
            }
        }
        
        /// <remarks/>
        public string NaturalezaDescuento {
            get {
                return this.naturalezaDescuentoField;
            }
            set {
                this.naturalezaDescuentoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraCodigoType {
        private FacturaElectronicaCompraCodigoTypeTipo tipoField;
        
        private string codigoField;
        
        /// <remarks/>
        public FacturaElectronicaCompraCodigoTypeTipo Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
            }
        }
        
        /// <remarks/>
        public string Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraCodigoTypeTipo {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraReceptorType {
        private string nombreField;
        
        private FacturaElectronicaCompraIdentificacionType identificacionField;
        
        private string identificacionExtranjeroField;
        
        private string nombreComercialField;
        
        private FacturaElectronicaCompraUbicacionType ubicacionField;
        
        private string otrasSenasExtranjeroField;
        
        private FacturaElectronicaCompraTelefonoType telefonoField;
        
        private FacturaElectronicaCompraTelefonoType faxField;
        
        private string correoElectronicoField;
        
        /// <remarks/>
        public string Nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraIdentificacionType Identificacion {
            get {
                return this.identificacionField;
            }
            set {
                this.identificacionField = value;
            }
        }
        
        /// <remarks/>
        public string IdentificacionExtranjero {
            get {
                return this.identificacionExtranjeroField;
            }
            set {
                this.identificacionExtranjeroField = value;
            }
        }
        
        /// <remarks/>
        public string NombreComercial {
            get {
                return this.nombreComercialField;
            }
            set {
                this.nombreComercialField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraUbicacionType Ubicacion {
            get {
                return this.ubicacionField;
            }
            set {
                this.ubicacionField = value;
            }
        }
        
        /// <remarks/>
        public string OtrasSenasExtranjero {
            get {
                return this.otrasSenasExtranjeroField;
            }
            set {
                this.otrasSenasExtranjeroField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraTelefonoType Telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraTelefonoType Fax {
            get {
                return this.faxField;
            }
            set {
                this.faxField = value;
            }
        }
        
        /// <remarks/>
        public string CorreoElectronico {
            get {
                return this.correoElectronicoField;
            }
            set {
                this.correoElectronicoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraUbicacionType {
        private string provinciaField;
        
        private string cantonField;
        
        private string distritoField;
        
        private string barrioField;
        
        private string otrasSenasField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string Provincia {
            get {
                return this.provinciaField;
            }
            set {
                this.provinciaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string Canton {
            get {
                return this.cantonField;
            }
            set {
                this.cantonField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string Distrito {
            get {
                return this.distritoField;
            }
            set {
                this.distritoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string Barrio {
            get {
                return this.barrioField;
            }
            set {
                this.barrioField = value;
            }
        }
        
        /// <remarks/>
        public string OtrasSenas {
            get {
                return this.otrasSenasField;
            }
            set {
                this.otrasSenasField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraTelefonoType {
        private string codigoPaisField;
        
        private string numTelefonoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="integer")]
        public string CodigoPais {
            get {
                return this.codigoPaisField;
            }
            set {
                this.codigoPaisField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="integer")]
        public string NumTelefono {
            get {
                return this.numTelefonoField;
            }
            set {
                this.numTelefonoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraCondicionVenta {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraMedioPago {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraLineaDetalle {
        private string numeroLineaField;
        
        private string codigoField;
        
        private FacturaElectronicaCompraCodigoType[] codigoComercialField;
        
        private decimal cantidadField;
        
        private FacturaElectronicaCompraUnidadMedidaType unidadMedidaField;
        
        private string unidadMedidaComercialField;
        
        private string detalleField;
        
        private decimal precioUnitarioField;
        
        private decimal montoTotalField;
        
        private FacturaElectronicaCompraDescuentoType[] descuentoField;
        
        private decimal subTotalField;
        
        private decimal baseImponibleField;
        
        private bool baseImponibleFieldSpecified;
        
        private FacturaElectronicaCompraImpuestoType[] impuestoField;
        
        private decimal impuestoNetoField;
        
        private bool impuestoNetoFieldSpecified;
        
        private decimal montoTotalLineaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string NumeroLinea {
            get {
                return this.numeroLineaField;
            }
            set {
                this.numeroLineaField = value;
            }
        }
        
        /// <remarks/>
        public string Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CodigoComercial")]
        public FacturaElectronicaCompraCodigoType[] CodigoComercial {
            get {
                return this.codigoComercialField;
            }
            set {
                this.codigoComercialField = value;
            }
        }
        
        /// <remarks/>
        public decimal Cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraUnidadMedidaType UnidadMedida {
            get {
                return this.unidadMedidaField;
            }
            set {
                this.unidadMedidaField = value;
            }
        }
        
        /// <remarks/>
        public string UnidadMedidaComercial {
            get {
                return this.unidadMedidaComercialField;
            }
            set {
                this.unidadMedidaComercialField = value;
            }
        }
        
        /// <remarks/>
        public string Detalle {
            get {
                return this.detalleField;
            }
            set {
                this.detalleField = value;
            }
        }
        
        /// <remarks/>
        public decimal PrecioUnitario {
            get {
                return this.precioUnitarioField;
            }
            set {
                this.precioUnitarioField = value;
            }
        }
        
        /// <remarks/>
        public decimal MontoTotal {
            get {
                return this.montoTotalField;
            }
            set {
                this.montoTotalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Descuento")]
        public FacturaElectronicaCompraDescuentoType[] Descuento {
            get {
                return this.descuentoField;
            }
            set {
                this.descuentoField = value;
            }
        }
        
        /// <remarks/>
        public decimal SubTotal {
            get {
                return this.subTotalField;
            }
            set {
                this.subTotalField = value;
            }
        }
        
        /// <remarks/>
        public decimal BaseImponible {
            get {
                return this.baseImponibleField;
            }
            set {
                this.baseImponibleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool BaseImponibleSpecified {
            get {
                return this.baseImponibleFieldSpecified;
            }
            set {
                this.baseImponibleFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public FacturaElectronicaCompraImpuestoType[] Impuesto {
            get {
                return this.impuestoField;
            }
            set {
                this.impuestoField = value;
            }
        }
        
        /// <remarks/>
        public decimal ImpuestoNeto {
            get {
                return this.impuestoNetoField;
            }
            set {
                this.impuestoNetoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ImpuestoNetoSpecified {
            get {
                return this.impuestoNetoFieldSpecified;
            }
            set {
                this.impuestoNetoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal MontoTotalLinea {
            get {
                return this.montoTotalLineaField;
            }
            set {
                this.montoTotalLineaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraUnidadMedidaType {
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
        d,
        
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
        [System.Xml.Serialization.XmlEnum("d")]
        d1,
        
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraResumenFactura {
        private FacturaElectronicaCompraCodigoMonedaType codigoTipoMonedaField;
        
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
        
        private decimal totalOtrosCargosField;
        
        private bool totalOtrosCargosFieldSpecified;
        
        private decimal totalComprobanteField;
        
        /// <remarks/>
        public FacturaElectronicaCompraCodigoMonedaType CodigoTipoMoneda {
            get {
                return this.codigoTipoMonedaField;
            }
            set {
                this.codigoTipoMonedaField = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalServGravados {
            get {
                return this.totalServGravadosField;
            }
            set {
                this.totalServGravadosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServGravadosSpecified {
            get {
                return this.totalServGravadosFieldSpecified;
            }
            set {
                this.totalServGravadosFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalServExentos {
            get {
                return this.totalServExentosField;
            }
            set {
                this.totalServExentosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExentosSpecified {
            get {
                return this.totalServExentosFieldSpecified;
            }
            set {
                this.totalServExentosFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalServExonerado {
            get {
                return this.totalServExoneradoField;
            }
            set {
                this.totalServExoneradoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExoneradoSpecified {
            get {
                return this.totalServExoneradoFieldSpecified;
            }
            set {
                this.totalServExoneradoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalMercanciasGravadas {
            get {
                return this.totalMercanciasGravadasField;
            }
            set {
                this.totalMercanciasGravadasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasGravadasSpecified {
            get {
                return this.totalMercanciasGravadasFieldSpecified;
            }
            set {
                this.totalMercanciasGravadasFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalMercanciasExentas {
            get {
                return this.totalMercanciasExentasField;
            }
            set {
                this.totalMercanciasExentasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasExentasSpecified {
            get {
                return this.totalMercanciasExentasFieldSpecified;
            }
            set {
                this.totalMercanciasExentasFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalMercExonerada {
            get {
                return this.totalMercExoneradaField;
            }
            set {
                this.totalMercExoneradaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercExoneradaSpecified {
            get {
                return this.totalMercExoneradaFieldSpecified;
            }
            set {
                this.totalMercExoneradaFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalGravado {
            get {
                return this.totalGravadoField;
            }
            set {
                this.totalGravadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalGravadoSpecified {
            get {
                return this.totalGravadoFieldSpecified;
            }
            set {
                this.totalGravadoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalExento {
            get {
                return this.totalExentoField;
            }
            set {
                this.totalExentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExentoSpecified {
            get {
                return this.totalExentoFieldSpecified;
            }
            set {
                this.totalExentoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalExonerado {
            get {
                return this.totalExoneradoField;
            }
            set {
                this.totalExoneradoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExoneradoSpecified {
            get {
                return this.totalExoneradoFieldSpecified;
            }
            set {
                this.totalExoneradoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalVenta {
            get {
                return this.totalVentaField;
            }
            set {
                this.totalVentaField = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalDescuentos {
            get {
                return this.totalDescuentosField;
            }
            set {
                this.totalDescuentosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalDescuentosSpecified {
            get {
                return this.totalDescuentosFieldSpecified;
            }
            set {
                this.totalDescuentosFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalVentaNeta {
            get {
                return this.totalVentaNetaField;
            }
            set {
                this.totalVentaNetaField = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalImpuesto {
            get {
                return this.totalImpuestoField;
            }
            set {
                this.totalImpuestoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalImpuestoSpecified {
            get {
                return this.totalImpuestoFieldSpecified;
            }
            set {
                this.totalImpuestoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalOtrosCargos {
            get {
                return this.totalOtrosCargosField;
            }
            set {
                this.totalOtrosCargosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalOtrosCargosSpecified {
            get {
                return this.totalOtrosCargosFieldSpecified;
            }
            set {
                this.totalOtrosCargosFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalComprobante {
            get {
                return this.totalComprobanteField;
            }
            set {
                this.totalComprobanteField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraInformacionReferencia {
        private FacturaElectronicaCompraInformacionReferenciaTipoDoc tipoDocField;
        
        private string numeroField;
        
        private System.DateTime fechaEmisionField;
        
        private FacturaElectronicaCompraInformacionReferenciaCodigo codigoField;
        
        private bool codigoFieldSpecified;
        
        private string razonField;
        
        /// <remarks/>
        public FacturaElectronicaCompraInformacionReferenciaTipoDoc TipoDoc {
            get {
                return this.tipoDocField;
            }
            set {
                this.tipoDocField = value;
            }
        }
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime FechaEmision {
            get {
                return this.fechaEmisionField;
            }
            set {
                this.fechaEmisionField = value;
            }
        }
        
        /// <remarks/>
        public FacturaElectronicaCompraInformacionReferenciaCodigo Codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoSpecified {
            get {
                return this.codigoFieldSpecified;
            }
            set {
                this.codigoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Razon {
            get {
                return this.razonField;
            }
            set {
                this.razonField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraInformacionReferenciaTipoDoc {
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
        [System.Xml.Serialization.XmlEnum("14")]
        Item14,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("15")]
        Item15,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraInformacionReferenciaCodigo {
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtros {
        private FacturaElectronicaCompraOtrosOtroTexto[] otroTextoField;
        
        private FacturaElectronicaCompraOtrosOtroContenido[] otroContenidoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroTexto")]
        public FacturaElectronicaCompraOtrosOtroTexto[] OtroTexto {
            get {
                return this.otroTextoField;
            }
            set {
                this.otroTextoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroContenido")]
        public FacturaElectronicaCompraOtrosOtroContenido[] OtroContenido {
            get {
                return this.otroContenidoField;
            }
            set {
                this.otroContenidoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtrosOtroTexto {
        private string codigoField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtrosOtroContenido {
        private System.Xml.XmlElement anyField;
        
        private string codigoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
            }
        }
    }
}
