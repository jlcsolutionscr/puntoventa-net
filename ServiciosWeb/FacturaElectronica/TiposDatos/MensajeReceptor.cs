namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor")]
    [System.Xml.Serialization.XmlRoot(Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor", IsNullable=false)]
    public partial class MensajeReceptor {
        
        private string claveField;
        
        private string numeroCedulaEmisorField;
        
        private System.DateTime fechaEmisionDocField;
        
        private MensajeReceptorMensaje mensajeField;
        
        private string detalleMensajeField;
        
        private decimal montoTotalImpuestoField;
        
        private bool montoTotalImpuestoFieldSpecified;
        
        private decimal totalFacturaField;
        
        private string numeroCedulaReceptorField;
        
        private string numeroConsecutivoReceptorField;
        
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
        public string NumeroCedulaEmisor {
            get {
                return numeroCedulaEmisorField;
            }
            set {
                numeroCedulaEmisorField = value;
            }
        }
        
        /// <comentarios/>
        public System.DateTime FechaEmisionDoc {
            get {
                return fechaEmisionDocField;
            }
            set {
                fechaEmisionDocField = value;
            }
        }
        
        /// <comentarios/>
        public MensajeReceptorMensaje Mensaje {
            get {
                return mensajeField;
            }
            set {
                mensajeField = value;
            }
        }
        
        /// <comentarios/>
        public string DetalleMensaje {
            get {
                return detalleMensajeField;
            }
            set {
                detalleMensajeField = value;
            }
        }
        
        /// <comentarios/>
        public decimal MontoTotalImpuesto {
            get {
                return montoTotalImpuestoField;
            }
            set {
                montoTotalImpuestoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool MontoTotalImpuestoSpecified {
            get {
                return montoTotalImpuestoFieldSpecified;
            }
            set {
                montoTotalImpuestoFieldSpecified = value;
            }
        }
        
        /// <comentarios/>
        public decimal TotalFactura {
            get {
                return totalFacturaField;
            }
            set {
                totalFacturaField = value;
            }
        }
        
        /// <comentarios/>
        public string NumeroCedulaReceptor {
            get {
                return numeroCedulaReceptorField;
            }
            set {
                numeroCedulaReceptorField = value;
            }
        }
        
        /// <comentarios/>
        public string NumeroConsecutivoReceptor {
            get {
                return numeroConsecutivoReceptorField;
            }
            set {
                numeroConsecutivoReceptorField = value;
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
    [System.Xml.Serialization.XmlType(AnonymousType=true, Namespace="https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor")]
    public enum MensajeReceptorMensaje {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("1")]
        Item1,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("2")]
        Item2,
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("3")]
        Item3,
    }
}