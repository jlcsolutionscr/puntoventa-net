namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor", IsNullable = false)]
    public partial class MensajeReceptor
    {

        private string claveField;

        private string numeroCedulaEmisorField;

        private System.DateTime fechaEmisionDocField;

        private MensajeReceptorMensaje mensajeField;

        private string detalleMensajeField;

        private decimal montoTotalImpuestoField;

        private bool montoTotalImpuestoFieldSpecified;

        private string codigoActividadField;

        private MensajeReceptorCondicionImpuesto condicionImpuestoField;

        private bool condicionImpuestoFieldSpecified;

        private decimal montoTotalImpuestoAcreditarField;

        private bool montoTotalImpuestoAcreditarFieldSpecified;

        private decimal montoTotalDeGastoAplicableField;

        private bool montoTotalDeGastoAplicableFieldSpecified;

        private decimal totalFacturaField;

        private string numeroCedulaReceptorField;

        private string numeroConsecutivoReceptorField;

        private SignatureType signatureField;

        /// <remarks/>
        public string Clave
        {
            get => claveField;
            set => claveField = value;
        }

        /// <remarks/>
        public string NumeroCedulaEmisor
        {
            get => numeroCedulaEmisorField;
            set => numeroCedulaEmisorField = value;
        }

        /// <remarks/>
        public System.DateTime FechaEmisionDoc
        {
            get => fechaEmisionDocField;
            set => fechaEmisionDocField = value;
        }

        /// <remarks/>
        public MensajeReceptorMensaje Mensaje
        {
            get => mensajeField;
            set => mensajeField = value;
        }

        /// <remarks/>
        public string DetalleMensaje
        {
            get => detalleMensajeField;
            set => detalleMensajeField = value;
        }

        /// <remarks/>
        public decimal MontoTotalImpuesto
        {
            get => montoTotalImpuestoField;
            set => montoTotalImpuestoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoTotalImpuestoSpecified
        {
            get => montoTotalImpuestoFieldSpecified;
            set => montoTotalImpuestoFieldSpecified = value;
        }

        /// <remarks/>
        public string CodigoActividad
        {
            get => codigoActividadField;
            set => codigoActividadField = value;
        }

        /// <remarks/>
        public MensajeReceptorCondicionImpuesto CondicionImpuesto
        {
            get => condicionImpuestoField;
            set => condicionImpuestoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CondicionImpuestoSpecified
        {
            get => condicionImpuestoFieldSpecified;
            set => condicionImpuestoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoTotalImpuestoAcreditar
        {
            get => montoTotalImpuestoAcreditarField;
            set => montoTotalImpuestoAcreditarField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoTotalImpuestoAcreditarSpecified
        {
            get => montoTotalImpuestoAcreditarFieldSpecified;
            set => montoTotalImpuestoAcreditarFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoTotalDeGastoAplicable
        {
            get => montoTotalDeGastoAplicableField;
            set => montoTotalDeGastoAplicableField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoTotalDeGastoAplicableSpecified
        {
            get => montoTotalDeGastoAplicableFieldSpecified;
            set => montoTotalDeGastoAplicableFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalFactura
        {
            get => totalFacturaField;
            set => totalFacturaField = value;
        }

        /// <remarks/>
        public string NumeroCedulaReceptor
        {
            get => numeroCedulaReceptorField;
            set => numeroCedulaReceptorField = value;
        }

        /// <remarks/>
        public string NumeroConsecutivoReceptor
        {
            get => numeroConsecutivoReceptorField;
            set => numeroConsecutivoReceptorField = value;
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor")]
    public enum MensajeReceptorMensaje
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("3")]
        Item3,
    }

    /// <remarks/>
    [System.Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor")]
    public enum MensajeReceptorCondicionImpuesto
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
    }
}