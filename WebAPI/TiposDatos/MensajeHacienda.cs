namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda", IsNullable = false)]
    public partial class MensajeHacienda
    {

        private string claveField;

        private string nombreEmisorField;

        private MensajeHaciendaTipoIdentificacionEmisor tipoIdentificacionEmisorField;

        private string numeroCedulaEmisorField;

        private string nombreReceptorField;

        private MensajeHaciendaTipoIdentificacionReceptor? tipoIdentificacionReceptorField;

        private bool tipoIdentificacionReceptorFieldSpecified;

        private string numeroCedulaReceptorField;

        private MensajeHaciendaMensaje mensajeField;

        private string detalleMensajeField;

        private decimal montoTotalImpuestoField;

        private bool montoTotalImpuestoFieldSpecified;

        private decimal totalFacturaField;

        private SignatureType signatureField;

        /// <remarks/>
        public string Clave
        {
            get => claveField;
            set => claveField = value;
        }

        /// <remarks/>
        public string NombreEmisor
        {
            get => nombreEmisorField;
            set => nombreEmisorField = value;
        }

        /// <remarks/>
        public MensajeHaciendaTipoIdentificacionEmisor TipoIdentificacionEmisor
        {
            get => tipoIdentificacionEmisorField;
            set => tipoIdentificacionEmisorField = value;
        }

        /// <remarks/>
        public string NumeroCedulaEmisor
        {
            get => numeroCedulaEmisorField;
            set => numeroCedulaEmisorField = value;
        }

        /// <remarks/>
        public string NombreReceptor
        {
            get => nombreReceptorField;
            set => nombreReceptorField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public MensajeHaciendaTipoIdentificacionReceptor? TipoIdentificacionReceptor
        {
            get => tipoIdentificacionReceptorField;
            set => tipoIdentificacionReceptorField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TipoIdentificacionReceptorSpecified
        {
            get => tipoIdentificacionReceptorFieldSpecified;
            set => tipoIdentificacionReceptorFieldSpecified = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(IsNullable = true)]
        public string NumeroCedulaReceptor
        {
            get => numeroCedulaReceptorField;
            set => numeroCedulaReceptorField = value;
        }

        /// <remarks/>
        public MensajeHaciendaMensaje Mensaje
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
        public decimal TotalFactura
        {
            get => totalFacturaField;
            set => totalFacturaField = value;
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda")]
    public enum MensajeHaciendaTipoIdentificacionEmisor
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda")]
    public enum MensajeHaciendaTipoIdentificacionReceptor
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

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda")]
    public enum MensajeHaciendaMensaje
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnum("3")]
        Item3,
    }
}
