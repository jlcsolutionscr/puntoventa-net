namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <remarks/>
    [Serializable()]
    public partial class EmisorType
    {

        private string nombreField;

        private IdentificacionType identificacionField;

        private string registrofiscal8707Field;

        private string nombreComercialField;

        private UbicacionType ubicacionField;

        private string otrasSenasExtranjeroField;

        private TelefonoType telefonoField;

        private string[] correoElectronicoField;

        /// <remarks/>
        public string Nombre
        {
            get => nombreField;
            set => nombreField = value;
        }

        /// <remarks/>
        public IdentificacionType Identificacion
        {
            get => identificacionField;
            set => identificacionField = value;
        }

        /// <remarks/>
        public string Registrofiscal8707
        {
            get => registrofiscal8707Field;
            set => registrofiscal8707Field = value;
        }

        /// <remarks/>
        public string NombreComercial
        {
            get => nombreComercialField;
            set => nombreComercialField = value;
        }

        /// <remarks/>
        public UbicacionType Ubicacion
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
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public TelefonoType Telefono
        {
            get => telefonoField;
            set => telefonoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CorreoElectronico")]
        public string[] CorreoElectronico
        {
            get => correoElectronicoField;
            set => correoElectronicoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public partial class IdentificacionType
    {

        private IdentificacionTypeTipo tipoField;

        private string numeroField;

        /// <remarks/>
        public IdentificacionTypeTipo Tipo
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
    [Serializable()]
    public enum IdentificacionTypeTipo
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
    }

    /// <remarks/>
    [Serializable()]
    public partial class CodigoMonedaType
    {

        private CodigoMonedaTypeCodigoMoneda codigoMonedaField;

        private decimal tipoCambioField;

        /// <remarks/>
        public CodigoMonedaTypeCodigoMoneda CodigoMoneda
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
    [Serializable()]
    public enum CodigoMonedaTypeCodigoMoneda
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
    [Serializable()]
    public partial class OtrosCargosType
    {

        private OtrosCargosTypeTipoDocumentoOC tipoDocumentoOCField;

        private string tipoDocumentoOTROSField;

        private IdentificacionType identificacionTerceroField;

        private string nombreTerceroField;

        private string detalleField;

        private decimal porcentajeOCField;

        private bool porcentajeOCFieldSpecified;

        private decimal montoCargoField;

        /// <remarks/>
        public OtrosCargosTypeTipoDocumentoOC TipoDocumentoOC
        {
            get => tipoDocumentoOCField;
            set => tipoDocumentoOCField = value;
        }

        /// <remarks/>
        public string TipoDocumentoOTROS
        {
            get => tipoDocumentoOTROSField;
            set => tipoDocumentoOTROSField = value;
        }

        /// <remarks/>
        public IdentificacionType IdentificacionTercero
        {
            get => identificacionTerceroField;
            set => identificacionTerceroField = value;
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
        public decimal PorcentajeOC
        {
            get => porcentajeOCField;
            set => porcentajeOCField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool PorcentajeOCSpecified
        {
            get => porcentajeOCFieldSpecified;
            set => porcentajeOCFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoCargo
        {
            get => montoCargoField;
            set => montoCargoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public enum OtrosCargosTypeTipoDocumentoOC
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [Serializable()]
    public partial class ReceptorType
    {

        private string nombreField;

        private IdentificacionType identificacionField;

        private string nombreComercialField;

        private UbicacionType ubicacionField;

        private string otrasSenasExtranjeroField;

        private TelefonoType telefonoField;

        private string correoElectronicoField;

        /// <remarks/>
        public string Nombre
        {
            get => nombreField;
            set => nombreField = value;
        }

        /// <remarks/>
        public IdentificacionType Identificacion
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
        public UbicacionType Ubicacion
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
        public TelefonoType Telefono
        {
            get => telefonoField;
            set => telefonoField = value;
        }

        /// <remarks/>
        public string CorreoElectronico
        {
            get => correoElectronicoField;
            set => correoElectronicoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public partial class UbicacionType
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
    [Serializable()]
    public partial class TelefonoType
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
    [Serializable()]
    public enum UnidadMedidaType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("´")]
        Item,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("´´")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("°C")]
        C,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1/m")]
        Item1m,

        /// <remarks/>
        A,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("A/m")]
        Am,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("A/m²")]
        Am1,

        /// <remarks/>
        Acv,

        /// <remarks/>
        Al,

        /// <remarks/>
        Alc,

        /// <remarks/>
        B,

        /// <remarks/>
        Bq,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C")]
        C1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/kg")]
        Ckg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/m²")]
        Cm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("C/m³")]
        Cm1,

        /// <remarks/>
        Cc,

        /// <remarks/>
        Cd,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("cd/m²")]
        cdm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("Cm")]
        Cm2,

        /// <remarks/>
        cm,

        /// <remarks/>
        Cu,

        /// <remarks/>
        D,

        /// <remarks/>
        eV,

        /// <remarks/>
        F,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("F/m")]
        Fm,

        /// <remarks/>
        Fa,

        /// <remarks/>
        G,

        /// <remarks/>
        Gal,

        /// <remarks/>
        Gy,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("Gy/s")]
        Gys,

        /// <remarks/>
        h,

        /// <remarks/>
        H,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("H/m")]
        Hm,

        /// <remarks/>
        Hz,

        /// <remarks/>
        I,

        /// <remarks/>
        J,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/(kg·K)")]
        JkgK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/(mol·K)")]
        JmolK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/K")]
        JK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/kg")]
        Jkg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/m³")]
        Jm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("J/mol")]
        Jmol,

        /// <remarks/>
        K,

        /// <remarks/>
        Kat,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("kat/m³")]
        katm,

        /// <remarks/>
        Kg,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("kg/m³")]
        kgm,

        /// <remarks/>
        Km,

        /// <remarks/>
        Kw,

        /// <remarks/>
        kWh,

        /// <remarks/>
        L,

        /// <remarks/>
        Lm,

        /// <remarks/>
        Ln,

        /// <remarks/>
        Lx,

        /// <remarks/>
        M,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m/s")]
        ms,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m/s²")]
        ms1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m²")]
        m,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("m³")]
        m1,

        /// <remarks/>
        Min,

        /// <remarks/>
        mL,

        /// <remarks/>
        Mm,

        /// <remarks/>
        Mol,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("mol/m³")]
        molm,

        /// <remarks/>
        N,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("N/m")]
        Nm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("N·m")]
        Nm1,

        /// <remarks/>
        Np,

        /// <remarks/>
        º,

        /// <remarks/>
        Os,

        /// <remarks/>
        Otros,

        /// <remarks/>
        Oz,

        /// <remarks/>
        Pa,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("Pa·s")]
        Pas,

        /// <remarks/>
        Qq,

        /// <remarks/>
        Rad,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("rad/s")]
        rads,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("rad/s²")]
        rads1,

        /// <remarks/>
        S,

        /// <remarks/>
        s,

        /// <remarks/>
        Sp,

        /// <remarks/>
        Spe,

        /// <remarks/>
        Sr,

        /// <remarks/>
        St,

        /// <remarks/>
        Sv,

        /// <remarks/>
        t,

        /// <remarks/>
        T,

        /// <remarks/>
        U,

        /// <remarks/>
        Ua,

        /// <remarks/>
        Unid,

        /// <remarks/>
        V,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("V/m")]
        Vm,

        /// <remarks/>
        W,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/(m·K)")]
        WmK,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/(m²·sr)")]
        Wmsr,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/m²")]
        Wm,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("W/sr")]
        Wsr,

        /// <remarks/>
        Wb,

        /// <remarks/>
        Ω,
    }

    /// <remarks/>
    [Serializable()]
    public enum TipoDocReferenciaType
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
        [System.Xml.Serialization.XmlEnum("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("18")]
        Item18,
    }

    /// <remarks/>
    [Serializable()]
    public enum CodigoReferenciaType
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [Serializable()]
    public partial class DescuentoType
    {

        private decimal montoDescuentoField;

        private CodigoDescuentoType codigoDescuentoField;

        private string codigoDescuentoOTROField;

        private string naturalezaDescuentoField;

        /// <remarks/>
        public decimal MontoDescuento
        {
            get => montoDescuentoField;
            set => montoDescuentoField = value;
        }

        /// <remarks/>
        public CodigoDescuentoType CodigoDescuento
        {
            get => codigoDescuentoField;
            set => codigoDescuentoField = value;
        }

        /// <remarks/>
        public string CodigoDescuentoOTRO
        {
            get => codigoDescuentoOTROField;
            set => codigoDescuentoOTROField = value;
        }

        /// <remarks/>
        public string NaturalezaDescuento
        {
            get => naturalezaDescuentoField;
            set => naturalezaDescuentoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public partial class ImpuestoType
    {

        private CodigoImpuestoType codigoField;

        private string codigoImpuestoOTROField;

        private CodigoTarifaIVAType codigoTarifaIVAField;

        private bool codigoTarifaIVAFieldSpecified;

        private decimal tarifaField;

        private bool tarifaFieldSpecified;

        private decimal factorCalculoIVAField;

        private bool factorCalculoIVAFieldSpecified;

        private ImpuestoTypeDatosImpuestoEspecifico datosImpuestoEspecificoField;

        private decimal montoField;

        private ExoneracionType exoneracionField;

        /// <remarks/>
        public CodigoImpuestoType Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        public string CodigoImpuestoOTRO
        {
            get => codigoImpuestoOTROField;
            set => codigoImpuestoOTROField = value;
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
        public decimal FactorCalculoIVA
        {
            get => factorCalculoIVAField;
            set => factorCalculoIVAField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool FactorCalculoIVASpecified
        {
            get => factorCalculoIVAFieldSpecified;
            set => factorCalculoIVAFieldSpecified = value;
        }

        /// <remarks/>
        public ImpuestoTypeDatosImpuestoEspecifico DatosImpuestoEspecifico
        {
            get => datosImpuestoEspecificoField;
            set => datosImpuestoEspecificoField = value;
        }

        /// <remarks/>
        public decimal Monto
        {
            get => montoField;
            set => montoField = value;
        }

        /// <remarks/>
        public ExoneracionType Exoneracion
        {
            get => exoneracionField;
            set => exoneracionField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public partial class ExoneracionType
    {
        private TipoExoneracionType tipoDocumentoEX1Field;

        private string tipoDocumentoOTROField;

        private string numeroDocumentoField;

        private string articuloField;

        private string incisoField;

        private ExoneracionTypeNombreInstitucion nombreInstitucionField;

        private string nombreInstitucionOtrosField;

        private DateTime fechaEmisionEXField;

        private decimal tarifaExoneradaField;

        private decimal montoExoneracionField;

        /// <remarks/>
        public TipoExoneracionType TipoDocumentoEX1
        {
            get => tipoDocumentoEX1Field;
            set => tipoDocumentoEX1Field = value;
        }

        /// <remarks/>
        public string TipoDocumentoOTRO
        {
            get => tipoDocumentoOTROField;
            set => tipoDocumentoOTROField = value;
        }

        /// <remarks/>
        public string NumeroDocumento
        {
            get => numeroDocumentoField;
            set => numeroDocumentoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string Articulo
        {
            get => articuloField;
            set => articuloField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string Inciso
        {
            get => incisoField;
            set => incisoField = value;
        }

        /// <remarks/>
        public ExoneracionTypeNombreInstitucion NombreInstitucion
        {
            get => nombreInstitucionField;
            set => nombreInstitucionField = value;
        }

        /// <remarks/>
        public string NombreInstitucionOtros
        {
            get => nombreInstitucionOtrosField;
            set => nombreInstitucionOtrosField = value;
        }

        /// <remarks/>
        public DateTime FechaEmisionEX
        {
            get => fechaEmisionEXField;
            set => fechaEmisionEXField = value;
        }

        /// <remarks/>
        public decimal TarifaExonerada
        {
            get => tarifaExoneradaField;
            set => tarifaExoneradaField = value;
        }

        /// <remarks/>
        public decimal MontoExoneracion
        {
            get => montoExoneracionField;
            set => montoExoneracionField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/tiqueteElectronico")]
    public enum TipoExoneracionType
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/tiqueteElectronico")]
    public enum ExoneracionTypeNombreInstitucion
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
        [System.Xml.Serialization.XmlEnum("99")]
        Item99,
    }

    /// <remarks/>
    [Serializable()]
    public enum CodigoImpuestoType
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
    [Serializable()]
    public enum CodigoTarifaIVAType
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
    }

    /// <remarks/>
    [Serializable()]
    public partial class ImpuestoTypeDatosImpuestoEspecifico
    {

        private decimal cantidadUnidadMedidaField;

        private decimal porcentajeField;

        private bool porcentajeFieldSpecified;

        private decimal proporcionField;

        private bool proporcionFieldSpecified;

        private decimal volumenUnidadConsumoField;

        private bool volumenUnidadConsumoFieldSpecified;

        private decimal impuestoUnidadField;

        /// <remarks/>
        public decimal CantidadUnidadMedida
        {
            get => cantidadUnidadMedidaField;
            set => cantidadUnidadMedidaField = value;
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
        public decimal Proporcion
        {
            get => proporcionField;
            set => proporcionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ProporcionSpecified
        {
            get => proporcionFieldSpecified;
            set => proporcionFieldSpecified = value;
        }

        /// <remarks/>
        public decimal VolumenUnidadConsumo
        {
            get => volumenUnidadConsumoField;
            set => volumenUnidadConsumoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool VolumenUnidadConsumoSpecified
        {
            get => volumenUnidadConsumoFieldSpecified;
            set => volumenUnidadConsumoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal ImpuestoUnidad
        {
            get => impuestoUnidadField;
            set => impuestoUnidadField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    public enum CodigoDescuentoType
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ObjectType
    {

        private System.Xml.XmlNode[] anyField;

        private string idField;

        private string mimeTypeField;

        private string encodingField;

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any
        {
            get => anyField;
            set => anyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string MimeType
        {
            get => mimeTypeField;
            set => mimeTypeField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get => encodingField;
            set => encodingField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SPKIData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SPKIDataType
    {

        private byte[][] sPKISexpField;

        private System.Xml.XmlElement anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SPKISexp", DataType = "base64Binary")]
        public byte[][] SPKISexp
        {
            get => sPKISexpField;
            set => sPKISexpField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement Any
        {
            get => anyField;
            set => anyField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("PGPData", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class PGPDataType
    {

        private object[] itemsField;

        private ItemsChoiceType1[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("PGPKeyID", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElement("PGPKeyPacket", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType1[] ItemsElementName
        {
            get => itemsElementNameField;
            set => itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,

        /// <remarks/>
        PGPKeyID,

        /// <remarks/>
        PGPKeyPacket,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType
    {

        private string x509IssuerNameField;

        private string x509SerialNumberField;

        /// <remarks/>
        public string X509IssuerName
        {
            get => x509IssuerNameField;
            set => x509IssuerNameField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string X509SerialNumber
        {
            get => x509SerialNumberField;
            set => x509SerialNumberField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class X509DataType
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("X509CRL", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElement("X509Certificate", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElement("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [System.Xml.Serialization.XmlElement("X509SKI", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElement("X509SubjectName", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType[] ItemsElementName
        {
            get => itemsElementNameField;
            set => itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,

        /// <remarks/>
        X509CRL,

        /// <remarks/>
        X509Certificate,

        /// <remarks/>
        X509IssuerSerial,

        /// <remarks/>
        X509SKI,

        /// <remarks/>
        X509SubjectName,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("RetrievalMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RetrievalMethodType
    {

        private TransformType[] transformsField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get => transformsField;
            set => transformsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string URI
        {
            get => uRIField;
            set => uRIField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Type
        {
            get => typeField;
            set => typeField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformType
    {

        private object[] itemsField;

        private string[] textField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("XPath", typeof(string))]
        public object[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text
        {
            get => textField;
            set => textField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get => algorithmField;
            set => algorithmField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("RSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class RSAKeyValueType
    {

        private byte[] modulusField;

        private byte[] exponentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] Modulus
        {
            get => modulusField;
            set => modulusField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] Exponent
        {
            get => exponentField;
            set => exponentField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("DSAKeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DSAKeyValueType
    {

        private byte[] pField;

        private byte[] qField;

        private byte[] gField;

        private byte[] yField;

        private byte[] jField;

        private byte[] seedField;

        private byte[] pgenCounterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] P
        {
            get => pField;
            set => pField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] Q
        {
            get => qField;
            set => qField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] G
        {
            get => gField;
            set => gField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] Y
        {
            get => yField;
            set => yField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] J
        {
            get => jField;
            set => jField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] Seed
        {
            get => seedField;
            set => seedField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] PgenCounter
        {
            get => pgenCounterField;
            set => pgenCounterField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("KeyValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyValueType
    {

        private object itemField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("DSAKeyValue", typeof(DSAKeyValueType))]
        [System.Xml.Serialization.XmlElement("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item
        {
            get => itemField;
            set => itemField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text
        {
            get => textField;
            set => textField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class KeyInfoType
    {

        private object[] itemsField;

        private ItemsChoiceType2[] itemsElementNameField;

        private string[] textField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("KeyName", typeof(string))]
        [System.Xml.Serialization.XmlElement("KeyValue", typeof(KeyValueType))]
        [System.Xml.Serialization.XmlElement("MgmtData", typeof(string))]
        [System.Xml.Serialization.XmlElement("PGPData", typeof(PGPDataType))]
        [System.Xml.Serialization.XmlElement("RetrievalMethod", typeof(RetrievalMethodType))]
        [System.Xml.Serialization.XmlElement("SPKIData", typeof(SPKIDataType))]
        [System.Xml.Serialization.XmlElement("X509Data", typeof(X509DataType))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType2[] ItemsElementName
        {
            get => itemsElementNameField;
            set => itemsElementNameField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text
        {
            get => textField;
            set => textField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#", IncludeInSchema = false)]
    public enum ItemsChoiceType2
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,

        /// <remarks/>
        KeyName,

        /// <remarks/>
        KeyValue,

        /// <remarks/>
        MgmtData,

        /// <remarks/>
        PGPData,

        /// <remarks/>
        RetrievalMethod,

        /// <remarks/>
        SPKIData,

        /// <remarks/>
        X509Data,
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureValue", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureValueType
    {

        private string idField;

        private byte[] valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText(DataType = "base64Binary")]
        public byte[] Value
        {
            get => valueField;
            set => valueField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class DigestMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any
        {
            get => anyField;
            set => anyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get => algorithmField;
            set => algorithmField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ReferenceType
    {

        private TransformType[] transformsField;

        private DigestMethodType digestMethodField;

        private byte[] digestValueField;

        private string idField;

        private string uRIField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get => transformsField;
            set => transformsField = value;
        }

        /// <remarks/>
        public DigestMethodType DigestMethod
        {
            get => digestMethodField;
            set => digestMethodField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get => digestValueField;
            set => digestValueField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string URI
        {
            get => uRIField;
            set => uRIField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Type
        {
            get => typeField;
            set => typeField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureMethodType
    {

        private string hMACOutputLengthField;

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string HMACOutputLength
        {
            get => hMACOutputLengthField;
            set => hMACOutputLengthField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any
        {
            get => anyField;
            set => anyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get => algorithmField;
            set => algorithmField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class CanonicalizationMethodType
    {

        private System.Xml.XmlNode[] anyField;

        private string algorithmField;

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any
        {
            get => anyField;
            set => anyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get => algorithmField;
            set => algorithmField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignedInfoType
    {

        private CanonicalizationMethodType canonicalizationMethodField;

        private SignatureMethodType signatureMethodField;

        private ReferenceType[] referenceField;

        private string idField;

        /// <remarks/>
        public CanonicalizationMethodType CanonicalizationMethod
        {
            get => canonicalizationMethodField;
            set => canonicalizationMethodField = value;
        }

        /// <remarks/>
        public SignatureMethodType SignatureMethod
        {
            get => signatureMethodField;
            set => signatureMethodField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Reference")]
        public ReferenceType[] Reference
        {
            get => referenceField;
            set => referenceField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignatureType
    {

        private SignedInfoType signedInfoField;

        private SignatureValueType signatureValueField;

        private KeyInfoType keyInfoField;

        private ObjectType[] objectField;

        private string idField;

        /// <remarks/>
        public SignedInfoType SignedInfo
        {
            get => signedInfoField;
            set => signedInfoField = value;
        }

        /// <remarks/>
        public SignatureValueType SignatureValue
        {
            get => signatureValueField;
            set => signatureValueField = value;
        }

        /// <remarks/>
        public KeyInfoType KeyInfo
        {
            get => keyInfoField;
            set => keyInfoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Object")]
        public ObjectType[] Object
        {
            get => objectField;
            set => objectField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class TransformsType
    {

        private TransformType[] transformField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Transform")]
        public TransformType[] Transform
        {
            get => transformField;
            set => transformField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Manifest", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class ManifestType
    {

        private ReferenceType[] referenceField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Reference")]
        public ReferenceType[] Reference
        {
            get => referenceField;
            set => referenceField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureProperties", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertiesType
    {

        private SignaturePropertyType[] signaturePropertyField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SignatureProperty")]
        public SignaturePropertyType[] SignatureProperty
        {
            get => signaturePropertyField;
            set => signaturePropertyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureProperty", Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
    public partial class SignaturePropertyType
    {

        private System.Xml.XmlElement[] itemsField;

        private string[] textField;

        private string targetField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement[] Items
        {
            get => itemsField;
            set => itemsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text
        {
            get => textField;
            set => textField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "anyURI")]
        public string Target
        {
            get => targetField;
            set => targetField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => idField;
            set => idField = value;
        }
    }
}