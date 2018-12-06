namespace LeandroSoftware.AccesoDatos.TiposDatos
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeHacienda")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeHacienda", IsNullable = false)]
    public partial class MensajeHacienda
    {

        private string claveField;

        private string nombreEmisorField;

        private MensajeHaciendaTipoIdentificacionEmisor tipoIdentificacionEmisorField;

        private string numeroCedulaEmisorField;

        private string nombreReceptorField;

        private MensajeHaciendaTipoIdentificacionReceptor tipoIdentificacionReceptorField;

        private bool tipoIdentificacionReceptorFieldSpecified;

        private string numeroCedulaReceptorField;

        private MensajeHaciendaMensaje mensajeField;

        private string detalleMensajeField;

        private decimal montoTotalImpuestoField;

        private bool montoTotalImpuestoFieldSpecified;

        private decimal totalFacturaField;

        private SignatureType signatureField;

        /// <comentarios/>
        public string Clave
        {
            get
            {
                return claveField;
            }
            set
            {
                claveField = value;
            }
        }

        /// <comentarios/>
        public string NombreEmisor
        {
            get
            {
                return nombreEmisorField;
            }
            set
            {
                nombreEmisorField = value;
            }
        }

        /// <comentarios/>
        public MensajeHaciendaTipoIdentificacionEmisor TipoIdentificacionEmisor
        {
            get
            {
                return tipoIdentificacionEmisorField;
            }
            set
            {
                tipoIdentificacionEmisorField = value;
            }
        }

        /// <comentarios/>
        public string NumeroCedulaEmisor
        {
            get
            {
                return numeroCedulaEmisorField;
            }
            set
            {
                numeroCedulaEmisorField = value;
            }
        }

        /// <comentarios/>
        public string NombreReceptor
        {
            get
            {
                return nombreReceptorField;
            }
            set
            {
                nombreReceptorField = value;
            }
        }

        /// <comentarios/>
        public MensajeHaciendaTipoIdentificacionReceptor TipoIdentificacionReceptor
        {
            get
            {
                return tipoIdentificacionReceptorField;
            }
            set
            {
                tipoIdentificacionReceptorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TipoIdentificacionReceptorSpecified
        {
            get
            {
                return tipoIdentificacionReceptorFieldSpecified;
            }
            set
            {
                tipoIdentificacionReceptorFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string NumeroCedulaReceptor
        {
            get
            {
                return numeroCedulaReceptorField;
            }
            set
            {
                numeroCedulaReceptorField = value;
            }
        }

        /// <comentarios/>
        public MensajeHaciendaMensaje Mensaje
        {
            get
            {
                return mensajeField;
            }
            set
            {
                mensajeField = value;
            }
        }

        /// <comentarios/>
        public string DetalleMensaje
        {
            get
            {
                return detalleMensajeField;
            }
            set
            {
                detalleMensajeField = value;
            }
        }

        /// <comentarios/>
        public decimal MontoTotalImpuesto
        {
            get
            {
                return montoTotalImpuestoField;
            }
            set
            {
                montoTotalImpuestoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoTotalImpuestoSpecified
        {
            get
            {
                return montoTotalImpuestoFieldSpecified;
            }
            set
            {
                montoTotalImpuestoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal TotalFactura
        {
            get
            {
                return totalFacturaField;
            }
            set
            {
                totalFacturaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return signatureField;
            }
            set
            {
                signatureField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeHacienda")]
    public enum MensajeHaciendaTipoIdentificacionEmisor
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeHacienda")]
    public enum MensajeHaciendaTipoIdentificacionReceptor
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
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeHacienda")]
    public enum MensajeHaciendaMensaje
    {

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,

        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("3")]
        Item3,
    }
}
