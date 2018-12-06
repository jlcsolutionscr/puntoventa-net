namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    [System.Xml.Serialization.XmlRoot(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica", IsNullable=false)]
    public partial class NotaCreditoElectronica {
        
        private string claveField;
        
        private string numeroConsecutivoField;
        
        private System.DateTime fechaEmisionField;
        
        private NotaCreditoElectronicaEmisorType emisorField;
        
        private NotaCreditoElectronicaReceptorType receptorField;
        
        private NotaCreditoElectronicaCondicionVenta condicionVentaField;
        
        private string plazoCreditoField;
        
        private NotaCreditoElectronicaMedioPago[] medioPagoField;
        
        private NotaCreditoElectronicaLineaDetalle[] detalleServicioField;
        
        private NotaCreditoElectronicaResumenFactura resumenFacturaField;
        
        private NotaCreditoElectronicaInformacionReferencia[] informacionReferenciaField;
        
        private NotaCreditoElectronicaNormativa normativaField;
        
        private NotaCreditoElectronicaOtros otrosField;
        
        private SignatureType signatureField;
        
        /// <comentarios/>
        public string Clave {
            get {
                return claveField;
            }
            set {
                claveField = value;
            }
        }
        
        /// <comentarios/>
        public string NumeroConsecutivo {
            get {
                return numeroConsecutivoField;
            }
            set {
                numeroConsecutivoField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaEmision {
            get {
                return fechaEmisionField;
            }
            set {
                fechaEmisionField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaEmisorType Emisor {
            get {
                return emisorField;
            }
            set {
                emisorField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaReceptorType Receptor {
            get {
                return receptorField;
            }
            set {
                receptorField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaCondicionVenta CondicionVenta {
            get {
                return condicionVentaField;
            }
            set {
                condicionVentaField = value;
            }
        }
        
        /// <comentarios/>
        public string PlazoCredito {
            get {
                return plazoCreditoField;
            }
            set {
                plazoCreditoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("MedioPago")]
        public NotaCreditoElectronicaMedioPago[] MedioPago {
            get {
                return medioPagoField;
            }
            set {
                medioPagoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalle", IsNullable=false)]
        public NotaCreditoElectronicaLineaDetalle[] DetalleServicio {
            get {
                return detalleServicioField;
            }
            set {
                detalleServicioField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaResumenFactura ResumenFactura {
            get {
                return resumenFacturaField;
            }
            set {
                resumenFacturaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public NotaCreditoElectronicaInformacionReferencia[] InformacionReferencia {
            get {
                return informacionReferenciaField;
            }
            set {
                informacionReferenciaField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaNormativa Normativa {
            get {
                return normativaField;
            }
            set {
                normativaField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaOtros Otros {
            get {
                return otrosField;
            }
            set {
                otrosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature {
            get {
                return signatureField;
            }
            set {
                signatureField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaCondicionVenta {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("06")]
        Item06,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaMedioPago {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaLineaDetalle {
        
        private string numeroLineaField;
        
        private NotaCreditoElectronicaCodigoType[] codigoField;
        
        private decimal cantidadField;
        
        private NotaCreditoElectronicaUnidadMedidaType unidadMedidaField;
        
        private string unidadMedidaComercialField;
        
        private string detalleField;
        
        private decimal precioUnitarioField;
        
        private decimal montoTotalField;
        
        private decimal montoDescuentoField;
        
        private bool montoDescuentoFieldSpecified;
        
        private string naturalezaDescuentoField;
        
        private decimal subTotalField;
        
        private NotaCreditoElectronicaImpuestoType[] impuestoField;
        
        private decimal montoTotalLineaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="positiveInteger")]
        public string NumeroLinea {
            get {
                return numeroLineaField;
            }
            set {
                numeroLineaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Codigo")]
        public NotaCreditoElectronicaCodigoType[] Codigo {
            get {
                return codigoField;
            }
            set {
                codigoField = value;
            }
        }
        
        /// <comentarios/>
        public decimal Cantidad {
            get {
                return cantidadField;
            }
            set {
                cantidadField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaUnidadMedidaType UnidadMedida {
            get {
                return unidadMedidaField;
            }
            set {
                unidadMedidaField = value;
            }
        }
        
        /// <comentarios/>
        public string UnidadMedidaComercial {
            get {
                return unidadMedidaComercialField;
            }
            set {
                unidadMedidaComercialField = value;
            }
        }
        
        /// <comentarios/>
        public string Detalle {
            get {
                return detalleField;
            }
            set {
                detalleField = value;
            }
        }
        
        /// <comentarios/>
        public decimal PrecioUnitario {
            get {
                return precioUnitarioField;
            }
            set {
                precioUnitarioField = value;
            }
        }
        
        /// <comentarios/>
        public decimal MontoTotal {
            get {
                return montoTotalField;
            }
            set {
                montoTotalField = value;
            }
        }
        
        /// <comentarios/>
        public decimal MontoDescuento {
            get {
                return montoDescuentoField;
            }
            set {
                montoDescuentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoDescuentoSpecified {
            get {
                return montoDescuentoFieldSpecified;
            }
            set {
                montoDescuentoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public string NaturalezaDescuento {
            get {
                return naturalezaDescuentoField;
            }
            set {
                naturalezaDescuentoField = value;
            }
        }
        
        /// <comentarios/>
        public decimal SubTotal {
            get {
                return subTotalField;
            }
            set {
                subTotalField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public NotaCreditoElectronicaImpuestoType[] Impuesto {
            get {
                return impuestoField;
            }
            set {
                impuestoField = value;
            }
        }
        
        /// <comentarios/>
        public decimal MontoTotalLinea {
            get {
                return montoTotalLineaField;
            }
            set {
                montoTotalLineaField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaResumenFactura {
        
        private NotaCreditoElectronicaResumenFacturaCodigoMoneda codigoMonedaField;
        
        private bool codigoMonedaFieldSpecified;
        
        private decimal tipoCambioField;
        
        private bool tipoCambioFieldSpecified;
        
        private decimal totalServGravadosField;
        
        private bool totalServGravadosFieldSpecified;
        
        private decimal totalServExentosField;
        
        private bool totalServExentosFieldSpecified;
        
        private decimal totalMercanciasGravadasField;
        
        private bool totalMercanciasGravadasFieldSpecified;
        
        private decimal totalMercanciasExentasField;
        
        private bool totalMercanciasExentasFieldSpecified;
        
        private decimal totalGravadoField;
        
        private bool totalGravadoFieldSpecified;
        
        private decimal totalExentoField;
        
        private bool totalExentoFieldSpecified;
        
        private decimal totalVentaField;
        
        private decimal totalDescuentosField;
        
        private bool totalDescuentosFieldSpecified;
        
        private decimal totalVentaNetaField;
        
        private decimal totalImpuestoField;
        
        private bool totalImpuestoFieldSpecified;
        
        private decimal totalComprobanteField;
        
        /// <comentarios/>
        public NotaCreditoElectronicaResumenFacturaCodigoMoneda CodigoMoneda {
            get {
                return codigoMonedaField;
            }
            set {
                codigoMonedaField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoMonedaSpecified {
            get {
                return codigoMonedaFieldSpecified;
            }
            set {
                codigoMonedaFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TipoCambio {
            get {
                return tipoCambioField;
            }
            set {
                tipoCambioField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TipoCambioSpecified {
            get {
                return tipoCambioFieldSpecified;
            }
            set {
                tipoCambioFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalServGravados {
            get {
                return totalServGravadosField;
            }
            set {
                totalServGravadosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServGravadosSpecified {
            get {
                return totalServGravadosFieldSpecified;
            }
            set {
                totalServGravadosFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalServExentos {
            get {
                return totalServExentosField;
            }
            set {
                totalServExentosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExentosSpecified {
            get {
                return totalServExentosFieldSpecified;
            }
            set {
                totalServExentosFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalMercanciasGravadas {
            get {
                return totalMercanciasGravadasField;
            }
            set {
                totalMercanciasGravadasField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasGravadasSpecified {
            get {
                return totalMercanciasGravadasFieldSpecified;
            }
            set {
                totalMercanciasGravadasFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalMercanciasExentas {
            get {
                return totalMercanciasExentasField;
            }
            set {
                totalMercanciasExentasField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercanciasExentasSpecified {
            get {
                return totalMercanciasExentasFieldSpecified;
            }
            set {
                totalMercanciasExentasFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalGravado {
            get {
                return totalGravadoField;
            }
            set {
                totalGravadoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalGravadoSpecified {
            get {
                return totalGravadoFieldSpecified;
            }
            set {
                totalGravadoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalExento {
            get {
                return totalExentoField;
            }
            set {
                totalExentoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExentoSpecified {
            get {
                return totalExentoFieldSpecified;
            }
            set {
                totalExentoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalVenta {
            get {
                return totalVentaField;
            }
            set {
                totalVentaField = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalDescuentos {
            get {
                return totalDescuentosField;
            }
            set {
                totalDescuentosField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalDescuentosSpecified {
            get {
                return totalDescuentosFieldSpecified;
            }
            set {
                totalDescuentosFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalVentaNeta {
            get {
                return totalVentaNetaField;
            }
            set {
                totalVentaNetaField = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalImpuesto {
            get {
                return totalImpuestoField;
            }
            set {
                totalImpuestoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalImpuestoSpecified {
            get {
                return totalImpuestoFieldSpecified;
            }
            set {
                totalImpuestoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalComprobante {
            get {
                return totalComprobanteField;
            }
            set {
                totalComprobanteField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaResumenFacturaCodigoMoneda {
        
        /// <comentarios/>
        AED,
        
        /// <comentarios/>
        AFN,
        
        /// <comentarios/>
        ALL,
        
        /// <comentarios/>
        AMD,
        
        /// <comentarios/>
        ANG,
        
        /// <comentarios/>
        AOA,
        
        /// <comentarios/>
        ARS,
        
        /// <comentarios/>
        AUD,
        
        /// <comentarios/>
        AWG,
        
        /// <comentarios/>
        AZN,
        
        /// <comentarios/>
        BAM,
        
        /// <comentarios/>
        BBD,
        
        /// <comentarios/>
        BDT,
        
        /// <comentarios/>
        BGN,
        
        /// <comentarios/>
        BHD,
        
        /// <comentarios/>
        BIF,
        
        /// <comentarios/>
        BMD,
        
        /// <comentarios/>
        BND,
        
        /// <comentarios/>
        BOB,
        
        /// <comentarios/>
        BOV,
        
        /// <comentarios/>
        BRL,
        
        /// <comentarios/>
        BSD,
        
        /// <comentarios/>
        BTN,
        
        /// <comentarios/>
        BWP,
        
        /// <comentarios/>
        BYR,
        
        /// <comentarios/>
        BZD,
        
        /// <comentarios/>
        CAD,
        
        /// <comentarios/>
        CDF,
        
        /// <comentarios/>
        CHE,
        
        /// <comentarios/>
        CHF,
        
        /// <comentarios/>
        CHW,
        
        /// <comentarios/>
        CLF,
        
        /// <comentarios/>
        CLP,
        
        /// <comentarios/>
        CNY,
        
        /// <comentarios/>
        COP,
        
        /// <comentarios/>
        COU,
        
        /// <comentarios/>
        CRC,
        
        /// <comentarios/>
        CUC,
        
        /// <comentarios/>
        CUP,
        
        /// <comentarios/>
        CVE,
        
        /// <comentarios/>
        CZK,
        
        /// <comentarios/>
        DJF,
        
        /// <comentarios/>
        DKK,
        
        /// <comentarios/>
        DOP,
        
        /// <comentarios/>
        DZD,
        
        /// <comentarios/>
        EGP,
        
        /// <comentarios/>
        ERN,
        
        /// <comentarios/>
        ETB,
        
        /// <comentarios/>
        EUR,
        
        /// <comentarios/>
        FJD,
        
        /// <comentarios/>
        FKP,
        
        /// <comentarios/>
        GBP,
        
        /// <comentarios/>
        GEL,
        
        /// <comentarios/>
        GHS,
        
        /// <comentarios/>
        GIP,
        
        /// <comentarios/>
        GMD,
        
        /// <comentarios/>
        GNF,
        
        /// <comentarios/>
        GTQ,
        
        /// <comentarios/>
        GYD,
        
        /// <comentarios/>
        HKD,
        
        /// <comentarios/>
        HNL,
        
        /// <comentarios/>
        HRK,
        
        /// <comentarios/>
        HTG,
        
        /// <comentarios/>
        HUF,
        
        /// <comentarios/>
        IDR,
        
        /// <comentarios/>
        ILS,
        
        /// <comentarios/>
        INR,
        
        /// <comentarios/>
        IQD,
        
        /// <comentarios/>
        IRR,
        
        /// <comentarios/>
        ISK,
        
        /// <comentarios/>
        JMD,
        
        /// <comentarios/>
        JOD,
        
        /// <comentarios/>
        JPY,
        
        /// <comentarios/>
        KES,
        
        /// <comentarios/>
        KGS,
        
        /// <comentarios/>
        KHR,
        
        /// <comentarios/>
        KMF,
        
        /// <comentarios/>
        KPW,
        
        /// <comentarios/>
        KRW,
        
        /// <comentarios/>
        KWD,
        
        /// <comentarios/>
        KYD,
        
        /// <comentarios/>
        KZT,
        
        /// <comentarios/>
        LAK,
        
        /// <comentarios/>
        LBP,
        
        /// <comentarios/>
        LKR,
        
        /// <comentarios/>
        LRD,
        
        /// <comentarios/>
        LSL,
        
        /// <comentarios/>
        LYD,
        
        /// <comentarios/>
        MAD,
        
        /// <comentarios/>
        MDL,
        
        /// <comentarios/>
        MGA,
        
        /// <comentarios/>
        MKD,
        
        /// <comentarios/>
        MMK,
        
        /// <comentarios/>
        MNT,
        
        /// <comentarios/>
        MOP,
        
        /// <comentarios/>
        MRO,
        
        /// <comentarios/>
        MUR,
        
        /// <comentarios/>
        MVR,
        
        /// <comentarios/>
        MWK,
        
        /// <comentarios/>
        MXN,
        
        /// <comentarios/>
        MXV,
        
        /// <comentarios/>
        MYR,
        
        /// <comentarios/>
        MZN,
        
        /// <comentarios/>
        NAD,
        
        /// <comentarios/>
        NGN,
        
        /// <comentarios/>
        NIO,
        
        /// <comentarios/>
        NOK,
        
        /// <comentarios/>
        NPR,
        
        /// <comentarios/>
        NZD,
        
        /// <comentarios/>
        OMR,
        
        /// <comentarios/>
        PAB,
        
        /// <comentarios/>
        PEN,
        
        /// <comentarios/>
        PGK,
        
        /// <comentarios/>
        PHP,
        
        /// <comentarios/>
        PKR,
        
        /// <comentarios/>
        PLN,
        
        /// <comentarios/>
        PYG,
        
        /// <comentarios/>
        QAR,
        
        /// <comentarios/>
        RON,
        
        /// <comentarios/>
        RSD,
        
        /// <comentarios/>
        RUB,
        
        /// <comentarios/>
        RWF,
        
        /// <comentarios/>
        SAR,
        
        /// <comentarios/>
        SBD,
        
        /// <comentarios/>
        SCR,
        
        /// <comentarios/>
        SDG,
        
        /// <comentarios/>
        SEK,
        
        /// <comentarios/>
        SGD,
        
        /// <comentarios/>
        SHP,
        
        /// <comentarios/>
        SLL,
        
        /// <comentarios/>
        SOS,
        
        /// <comentarios/>
        SRD,
        
        /// <comentarios/>
        SSP,
        
        /// <comentarios/>
        STD,
        
        /// <comentarios/>
        SVC,
        
        /// <comentarios/>
        SYP,
        
        /// <comentarios/>
        SZL,
        
        /// <comentarios/>
        THB,
        
        /// <comentarios/>
        TJS,
        
        /// <comentarios/>
        TMT,
        
        /// <comentarios/>
        TND,
        
        /// <comentarios/>
        TOP,
        
        /// <comentarios/>
        TRY,
        
        /// <comentarios/>
        TTD,
        
        /// <comentarios/>
        TWD,
        
        /// <comentarios/>
        TZS,
        
        /// <comentarios/>
        UAH,
        
        /// <comentarios/>
        UGX,
        
        /// <comentarios/>
        USD,
        
        /// <comentarios/>
        USN,
        
        /// <comentarios/>
        UYI,
        
        /// <comentarios/>
        UYU,
        
        /// <comentarios/>
        UZS,
        
        /// <comentarios/>
        VEF,
        
        /// <comentarios/>
        VND,
        
        /// <comentarios/>
        VUV,
        
        /// <comentarios/>
        WST,
        
        /// <comentarios/>
        XAF,
        
        /// <comentarios/>
        XAG,
        
        /// <comentarios/>
        XAU,
        
        /// <comentarios/>
        XBA,
        
        /// <comentarios/>
        XBB,
        
        /// <comentarios/>
        XBC,
        
        /// <comentarios/>
        XBD,
        
        /// <comentarios/>
        XCD,
        
        /// <comentarios/>
        XDR,
        
        /// <comentarios/>
        XOF,
        
        /// <comentarios/>
        XPD,
        
        /// <comentarios/>
        XPF,
        
        /// <comentarios/>
        XPT,
        
        /// <comentarios/>
        XSU,
        
        /// <comentarios/>
        XTS,
        
        /// <comentarios/>
        XUA,
        
        /// <comentarios/>
        XXX,
        
        /// <comentarios/>
        YER,
        
        /// <comentarios/>
        ZAR,
        
        /// <comentarios/>
        ZMW,
        
        /// <comentarios/>
        ZWL,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaInformacionReferencia {
        
        private NotaCreditoElectronicaInformacionReferenciaTipoDoc tipoDocField;
        
        private string numeroField;
        
        private System.DateTime fechaEmisionField;
        
        private NotaCreditoElectronicaInformacionReferenciaCodigo codigoField;
        
        private string razonField;
        
        /// <comentarios/>
        public NotaCreditoElectronicaInformacionReferenciaTipoDoc TipoDoc {
            get {
                return tipoDocField;
            }
            set {
                tipoDocField = value;
            }
        }
        
        /// <comentarios/>
        public string Numero {
            get {
                return numeroField;
            }
            set {
                numeroField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaEmision {
            get {
                return fechaEmisionField;
            }
            set {
                fechaEmisionField = value;
            }
        }
        
        /// <comentarios/>
        public NotaCreditoElectronicaInformacionReferenciaCodigo Codigo {
            get {
                return codigoField;
            }
            set {
                codigoField = value;
            }
        }
        
        /// <comentarios/>
        public string Razon {
            get {
                return razonField;
            }
            set {
                razonField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaInformacionReferenciaTipoDoc {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("06")]
        Item06,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("07")]
        Item07,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaInformacionReferenciaCodigo {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaNormativa {
        
        private string numeroResolucionField;
        
        private string fechaResolucionField;
        
        /// <comentarios/>
        public string NumeroResolucion {
            get {
                return numeroResolucionField;
            }
            set {
                numeroResolucionField = value;
            }
        }
        
        /// <comentarios/>
        public string FechaResolucion {
            get {
                return fechaResolucionField;
            }
            set {
                fechaResolucionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtros {
        
        private NotaCreditoElectronicaOtrosOtroTexto[] otroTextoField;
        
        private NotaCreditoElectronicaOtrosOtroContenido[] otroContenidoField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("OtroTexto")]
        public NotaCreditoElectronicaOtrosOtroTexto[] OtroTexto {
            get {
                return otroTextoField;
            }
            set {
                otroTextoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("OtroContenido")]
        public NotaCreditoElectronicaOtrosOtroContenido[] OtroContenido {
            get {
                return otroContenidoField;
            }
            set {
                otroContenidoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtrosOtroTexto {
        
        private string codigoField;
        
        private string valueField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo {
            get {
                return codigoField;
            }
            set {
                codigoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        public string Value {
            get {
                return valueField;
            }
            set {
                valueField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaOtrosOtroContenido {
        
        private System.Xml.XmlElement anyField;
        
        private string codigoField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute()]
        public string codigo {
            get {
                return codigoField;
            }
            set {
                codigoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaEmisorType
    {

        private string nombreField;

        private NotaCreditoElectronicaIdentificacionType identificacionField;

        private string nombreComercialField;

        private NotaCreditoElectronicaUbicacionType ubicacionField;

        private NotaCreditoElectronicaTelefonoType telefonoField;

        private NotaCreditoElectronicaTelefonoType faxField;

        private string correoElectronicoField;

        /// <comentarios/>
        public string Nombre
        {
            get
            {
                return nombreField;
            }
            set
            {
                nombreField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaIdentificacionType Identificacion
        {
            get
            {
                return identificacionField;
            }
            set
            {
                identificacionField = value;
            }
        }

        /// <comentarios/>
        public string NombreComercial
        {
            get
            {
                return nombreComercialField;
            }
            set
            {
                nombreComercialField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaUbicacionType Ubicacion
        {
            get
            {
                return ubicacionField;
            }
            set
            {
                ubicacionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public NotaCreditoElectronicaTelefonoType Telefono
        {
            get
            {
                return telefonoField;
            }
            set
            {
                telefonoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public NotaCreditoElectronicaTelefonoType Fax
        {
            get
            {
                return faxField;
            }
            set
            {
                faxField = value;
            }
        }

        /// <comentarios/>
        public string CorreoElectronico
        {
            get
            {
                return correoElectronicoField;
            }
            set
            {
                correoElectronicoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaReceptorType
    {

        private string nombreField;

        private NotaCreditoElectronicaIdentificacionType identificacionField;

        private string identificacionExtranjeroField;

        private string nombreComercialField;

        private NotaCreditoElectronicaUbicacionType ubicacionField;

        private NotaCreditoElectronicaTelefonoType telefonoField;

        private NotaCreditoElectronicaTelefonoType faxField;

        private string correoElectronicoField;

        /// <comentarios/>
        public string Nombre
        {
            get
            {
                return nombreField;
            }
            set
            {
                nombreField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaIdentificacionType Identificacion
        {
            get
            {
                return identificacionField;
            }
            set
            {
                identificacionField = value;
            }
        }

        /// <comentarios/>
        public string IdentificacionExtranjero
        {
            get
            {
                return identificacionExtranjeroField;
            }
            set
            {
                identificacionExtranjeroField = value;
            }
        }

        /// <comentarios/>
        public string NombreComercial
        {
            get
            {
                return nombreComercialField;
            }
            set
            {
                nombreComercialField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaUbicacionType Ubicacion
        {
            get
            {
                return ubicacionField;
            }
            set
            {
                ubicacionField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaTelefonoType Telefono
        {
            get
            {
                return telefonoField;
            }
            set
            {
                telefonoField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaTelefonoType Fax
        {
            get
            {
                return faxField;
            }
            set
            {
                faxField = value;
            }
        }

        /// <comentarios/>
        public string CorreoElectronico
        {
            get
            {
                return correoElectronicoField;
            }
            set
            {
                correoElectronicoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaIdentificacionType
    {

        private NotaCreditoElectronicaIdentificacionTypeTipo tipoField;

        private string numeroField;

        /// <comentarios/>
        public NotaCreditoElectronicaIdentificacionTypeTipo Tipo
        {
            get
            {
                return tipoField;
            }
            set
            {
                tipoField = value;
            }
        }

        /// <comentarios/>
        public string Numero
        {
            get
            {
                return numeroField;
            }
            set
            {
                numeroField = value;
            }
        }
    }

    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaUbicacionType
    {

        private string provinciaField;

        private string cantonField;

        private string distritoField;

        private string barrioField;

        private string otrasSenasField;

        /// <comentarios/>
        public string Provincia
        {
            get
            {
                return provinciaField;
            }
            set
            {
                provinciaField = value;
            }
        }

        /// <comentarios/>
        public string Canton
        {
            get
            {
                return cantonField;
            }
            set
            {
                cantonField = value;
            }
        }

        /// <comentarios/>
        public string Distrito
        {
            get
            {
                return distritoField;
            }
            set
            {
                distritoField = value;
            }
        }

        /// <comentarios/>
        public string Barrio
        {
            get
            {
                return barrioField;
            }
            set
            {
                barrioField = value;
            }
        }

        /// <comentarios/>
        public string OtrasSenas
        {
            get
            {
                return otrasSenasField;
            }
            set
            {
                otrasSenasField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaTelefonoType
    {

        private string codigoPaisField;

        private string numTelefonoField;

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string CodigoPais
        {
            get
            {
                return codigoPaisField;
            }
            set
            {
                codigoPaisField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string NumTelefono
        {
            get
            {
                return numTelefonoField;
            }
            set
            {
                numTelefonoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaCodigoTypeTipo
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaIdentificacionTypeTipo
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaImpuestoType
    {

        private NotaCreditoElectronicaImpuestoTypeCodigo codigoField;

        private decimal tarifaField;

        private decimal montoField;

        private NotaCreditoElectronicaExoneracionType exoneracionField;

        /// <comentarios/>
        public NotaCreditoElectronicaImpuestoTypeCodigo Codigo
        {
            get
            {
                return codigoField;
            }
            set
            {
                codigoField = value;
            }
        }

        /// <comentarios/>
        public decimal Tarifa
        {
            get
            {
                return tarifaField;
            }
            set
            {
                tarifaField = value;
            }
        }

        /// <comentarios/>
        public decimal Monto
        {
            get
            {
                return montoField;
            }
            set
            {
                montoField = value;
            }
        }

        /// <comentarios/>
        public NotaCreditoElectronicaExoneracionType Exoneracion
        {
            get
            {
                return exoneracionField;
            }
            set
            {
                exoneracionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaImpuestoTypeCodigo
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("06")]
        Item06,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("07")]
        Item07,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("08")]
        Item08,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("09")]
        Item09,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("10")]
        Item10,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("11")]
        Item11,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("12")]
        Item12,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("98")]
        Item98,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaExoneracionType
    {

        private NotaCreditoElectronicaExoneracionTypeTipoDocumento tipoDocumentoField;

        private string numeroDocumentoField;

        private string nombreInstitucionField;

        private System.DateTime fechaEmisionField;

        private decimal montoImpuestoField;

        private string porcentajeCompraField;

        /// <comentarios/>
        public NotaCreditoElectronicaExoneracionTypeTipoDocumento TipoDocumento
        {
            get
            {
                return tipoDocumentoField;
            }
            set
            {
                tipoDocumentoField = value;
            }
        }

        /// <comentarios/>
        public string NumeroDocumento
        {
            get
            {
                return numeroDocumentoField;
            }
            set
            {
                numeroDocumentoField = value;
            }
        }

        /// <comentarios/>
        public string NombreInstitucion
        {
            get
            {
                return nombreInstitucionField;
            }
            set
            {
                nombreInstitucionField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaEmision
        {
            get
            {
                return fechaEmisionField;
            }
            set
            {
                fechaEmisionField = value;
            }
        }

        /// <comentarios/>
        public decimal MontoImpuesto
        {
            get
            {
                return montoImpuestoField;
            }
            set
            {
                montoImpuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string PorcentajeCompra
        {
            get
            {
                return porcentajeCompraField;
            }
            set
            {
                porcentajeCompraField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaExoneracionTypeTipoDocumento
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("01")]
        Item01,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("02")]
        Item02,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("03")]
        Item03,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("04")]
        Item04,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("05")]
        Item05,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public partial class NotaCreditoElectronicaCodigoType
    {

        private NotaCreditoElectronicaCodigoTypeTipo tipoField;

        private string codigoField;

        /// <comentarios/>
        public NotaCreditoElectronicaCodigoTypeTipo Tipo
        {
            get
            {
                return tipoField;
            }
            set
            {
                tipoField = value;
            }
        }

        /// <comentarios/>
        public string Codigo
        {
            get
            {
                return codigoField;
            }
            set
            {
                codigoField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica")]
    public enum NotaCreditoElectronicaUnidadMedidaType
    {

        /// <comentarios/>
        Sp,

        /// <comentarios/>
        m,

        /// <comentarios/>
        kg,

        /// <comentarios/>
        s,

        /// <comentarios/>
        A,

        /// <comentarios/>
        K,

        /// <comentarios/>
        mol,

        /// <comentarios/>
        cd,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("mÂ²")]
        mÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("mÂ³")]
        mÂ1,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("m/s")]
        ms,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("m/sÂ²")]
        msÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("1/m")]
        Item1m,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("kg/mÂ³")]
        kgmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("A/mÂ²")]
        AmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("A/m")]
        Am,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("mol/mÂ³")]
        molmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("cd/mÂ²")]
        cdmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <comentarios/>
        rad,

        /// <comentarios/>
        sr,

        /// <comentarios/>
        Hz,

        /// <comentarios/>
        N,

        /// <comentarios/>
        Pa,

        /// <comentarios/>
        J,

        /// <comentarios/>
        W,

        /// <comentarios/>
        C,

        /// <comentarios/>
        V,

        /// <comentarios/>
        F,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("â„¦")]
        â,

        /// <comentarios/>
        S,

        /// <comentarios/>
        Wb,

        /// <comentarios/>
        T,

        /// <comentarios/>
        H,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("Â°C")]
        ÂC,

        /// <comentarios/>
        lm,

        /// <comentarios/>
        lx,

        /// <comentarios/>
        Bq,

        /// <comentarios/>
        Gy,

        /// <comentarios/>
        Sv,

        /// <comentarios/>
        kat,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("PaÂ·s")]
        PaÂs,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("NÂ·m")]
        NÂm,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("N/m")]
        Nm,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("rad/s")]
        rads,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("rad/sÂ²")]
        radsÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("W/mÂ²")]
        WmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/K")]
        JK,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/(kgÂ·K)")]
        JkgÂK,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/kg")]
        Jkg,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("W/(mÂ·K)")]
        WmÂK,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/mÂ³")]
        JmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("V/m")]
        Vm,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("C/mÂ³")]
        CmÂ,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("C/mÂ²")]
        CmÂ1,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("F/m")]
        Fm,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("H/m")]
        Hm,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/mol")]
        Jmol,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("J/(molÂ·K)")]
        JmolÂK,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("C/kg")]
        Ckg,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("Gy/s")]
        Gys,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("W/sr")]
        Wsr,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("W/(mÂ²Â·sr)")]
        WmÂÂsr,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("kat/mÂ³")]
        katmÂ,

        /// <comentarios/>
        min,

        /// <comentarios/>
        h,

        /// <comentarios/>
        d,

        /// <comentarios/>
        Âº,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("Â´")]
        Â,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("Â´Â´")]
        ÂÂ,

        /// <comentarios/>
        L,

        /// <comentarios/>
        t,

        /// <comentarios/>
        Np,

        /// <comentarios/>
        B,

        /// <comentarios/>
        eV,

        /// <comentarios/>
        u,

        /// <comentarios/>
        ua,

        /// <comentarios/>
        Unid,

        /// <comentarios/>
        Gal,

        /// <comentarios/>
        g,

        /// <comentarios/>
        Km,

        /// <comentarios/>
        ln,

        /// <comentarios/>
        cm,

        /// <comentarios/>
        mL,

        /// <comentarios/>
        mm,

        /// <comentarios/>
        Oz,

        /// <comentarios/>
        Otros,
    }
}
