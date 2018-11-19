namespace LeandroSoftware.FacturaElectronicaHacienda.TiposDatos
{
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Object", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class ObjectType {
        
        private System.Xml.XmlNode[] anyField;
        
        private string idField;
        
        private string mimeTypeField;
        
        private string encodingField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute()]
        public string MimeType {
            get {
                return mimeTypeField;
            }
            set {
                mimeTypeField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Encoding {
            get {
                return encodingField;
            }
            set {
                encodingField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SPKIData", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SPKIDataType {
        
        private byte[][] sPKISexpField;
        
        private System.Xml.XmlElement anyField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("SPKISexp", DataType="base64Binary")]
        public byte[][] SPKISexp {
            get {
                return sPKISexpField;
            }
            set {
                sPKISexpField = value;
            }
        }
        
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
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("PGPData", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class PGPDataType {
        
        private object[] itemsField;
        
        private ItemsChoiceType1[] itemsElementNameField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("PGPKeyID", typeof(byte[]), DataType="base64Binary")]
        [System.Xml.Serialization.XmlElement("PGPKeyPacket", typeof(byte[]), DataType="base64Binary")]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType1[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType1 {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,
        
        /// <comentarios/>
        PGPKeyID,
        
        /// <comentarios/>
        PGPKeyPacket,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    public partial class X509IssuerSerialType {
        
        private string x509IssuerNameField;
        
        private string x509SerialNumberField;
        
        /// <comentarios/>
        public string X509IssuerName {
            get {
                return x509IssuerNameField;
            }
            set {
                x509IssuerNameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="integer")]
        public string X509SerialNumber {
            get {
                return x509SerialNumberField;
            }
            set {
                x509SerialNumberField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("X509Data", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class X509DataType {
        
        private object[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("X509CRL", typeof(byte[]), DataType="base64Binary")]
        [System.Xml.Serialization.XmlElement("X509Certificate", typeof(byte[]), DataType="base64Binary")]
        [System.Xml.Serialization.XmlElement("X509IssuerSerial", typeof(X509IssuerSerialType))]
        [System.Xml.Serialization.XmlElement("X509SKI", typeof(byte[]), DataType="base64Binary")]
        [System.Xml.Serialization.XmlElement("X509SubjectName", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,
        
        /// <comentarios/>
        X509CRL,
        
        /// <comentarios/>
        X509Certificate,
        
        /// <comentarios/>
        X509IssuerSerial,
        
        /// <comentarios/>
        X509SKI,
        
        /// <comentarios/>
        X509SubjectName,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("RetrievalMethod", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class RetrievalMethodType {
        
        private TransformType[] transformsField;
        
        private string uRIField;
        
        private string typeField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItem("Transform", IsNullable=false)]
        public TransformType[] Transforms {
            get {
                return transformsField;
            }
            set {
                transformsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string URI {
            get {
                return uRIField;
            }
            set {
                uRIField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Type {
            get {
                return typeField;
            }
            set {
                typeField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Transform", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class TransformType {
        
        private object[] itemsField;
        
        private string[] textField;
        
        private string algorithmField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("XPath", typeof(string))]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text {
            get {
                return textField;
            }
            set {
                textField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return algorithmField;
            }
            set {
                algorithmField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("RSAKeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class RSAKeyValueType {
        
        private byte[] modulusField;
        
        private byte[] exponentField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Modulus {
            get {
                return modulusField;
            }
            set {
                modulusField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Exponent {
            get {
                return exponentField;
            }
            set {
                exponentField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("DSAKeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class DSAKeyValueType {
        
        private byte[] pField;
        
        private byte[] qField;
        
        private byte[] gField;
        
        private byte[] yField;
        
        private byte[] jField;
        
        private byte[] seedField;
        
        private byte[] pgenCounterField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] P {
            get {
                return pField;
            }
            set {
                pField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Q {
            get {
                return qField;
            }
            set {
                qField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] G {
            get {
                return gField;
            }
            set {
                gField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Y {
            get {
                return yField;
            }
            set {
                yField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] J {
            get {
                return jField;
            }
            set {
                jField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] Seed {
            get {
                return seedField;
            }
            set {
                seedField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] PgenCounter {
            get {
                return pgenCounterField;
            }
            set {
                pgenCounterField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("KeyValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class KeyValueType {
        
        private object itemField;
        
        private string[] textField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("DSAKeyValue", typeof(DSAKeyValueType))]
        [System.Xml.Serialization.XmlElement("RSAKeyValue", typeof(RSAKeyValueType))]
        public object Item {
            get {
                return itemField;
            }
            set {
                itemField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text {
            get {
                return textField;
            }
            set {
                textField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("KeyInfo", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class KeyInfoType {
        
        private object[] itemsField;
        
        private ItemsChoiceType2[] itemsElementNameField;
        
        private string[] textField;
        
        private string idField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        [System.Xml.Serialization.XmlElement("KeyName", typeof(string))]
        [System.Xml.Serialization.XmlElement("KeyValue", typeof(KeyValueType))]
        [System.Xml.Serialization.XmlElement("MgmtData", typeof(string))]
        [System.Xml.Serialization.XmlElement("PGPData", typeof(PGPDataType))]
        [System.Xml.Serialization.XmlElement("RetrievalMethod", typeof(RetrievalMethodType))]
        [System.Xml.Serialization.XmlElement("SPKIData", typeof(SPKIDataType))]
        [System.Xml.Serialization.XmlElement("X509Data", typeof(X509DataType))]
        [System.Xml.Serialization.XmlChoiceIdentifier("ItemsElementName")]
        public object[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnore()]
        public ItemsChoiceType2[] ItemsElementName {
            get {
                return itemsElementNameField;
            }
            set {
                itemsElementNameField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text {
            get {
                return textField;
            }
            set {
                textField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#", IncludeInSchema=false)]
    public enum ItemsChoiceType2 {
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlEnum("##any:")]
        Item,
        
        /// <comentarios/>
        KeyName,
        
        /// <comentarios/>
        KeyValue,
        
        /// <comentarios/>
        MgmtData,
        
        /// <comentarios/>
        PGPData,
        
        /// <comentarios/>
        RetrievalMethod,
        
        /// <comentarios/>
        SPKIData,
        
        /// <comentarios/>
        X509Data,
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureValue", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignatureValueType {
        
        private string idField;
        
        private byte[] valueField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText(DataType="base64Binary")]
        public byte[] Value {
            get {
                return valueField;
            }
            set {
                valueField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("DigestMethod", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class DigestMethodType {
        
        private System.Xml.XmlNode[] anyField;
        
        private string algorithmField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return algorithmField;
            }
            set {
                algorithmField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Reference", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class ReferenceType {
        
        private TransformType[] transformsField;
        
        private DigestMethodType digestMethodField;
        
        private byte[] digestValueField;
        
        private string idField;
        
        private string uRIField;
        
        private string typeField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayItem("Transform", IsNullable=false)]
        public TransformType[] Transforms {
            get {
                return transformsField;
            }
            set {
                transformsField = value;
            }
        }
        
        /// <comentarios/>
        public DigestMethodType DigestMethod {
            get {
                return digestMethodField;
            }
            set {
                digestMethodField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="base64Binary")]
        public byte[] DigestValue {
            get {
                return digestValueField;
            }
            set {
                digestValueField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string URI {
            get {
                return uRIField;
            }
            set {
                uRIField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Type {
            get {
                return typeField;
            }
            set {
                typeField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureMethod", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignatureMethodType {
        
        private string hMACOutputLengthField;
        
        private System.Xml.XmlNode[] anyField;
        
        private string algorithmField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement(DataType="integer")]
        public string HMACOutputLength {
            get {
                return hMACOutputLengthField;
            }
            set {
                hMACOutputLengthField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return algorithmField;
            }
            set {
                algorithmField = value;
            }
        }
    }
    
    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Transforms", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class TransformsType {
        
        private TransformType[] transformField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Transform")]
        public TransformType[] Transform {
            get {
                return transformField;
            }
            set {
                transformField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("CanonicalizationMethod", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class CanonicalizationMethodType {
        
        private System.Xml.XmlNode[] anyField;
        
        private string algorithmField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlNode[] Any {
            get {
                return anyField;
            }
            set {
                anyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Algorithm {
            get {
                return algorithmField;
            }
            set {
                algorithmField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignedInfo", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignedInfoType {
        
        private CanonicalizationMethodType canonicalizationMethodField;
        
        private SignatureMethodType signatureMethodField;
        
        private ReferenceType[] referenceField;
        
        private string idField;
        
        /// <comentarios/>
        public CanonicalizationMethodType CanonicalizationMethod {
            get {
                return canonicalizationMethodField;
            }
            set {
                canonicalizationMethodField = value;
            }
        }
        
        /// <comentarios/>
        public SignatureMethodType SignatureMethod {
            get {
                return signatureMethodField;
            }
            set {
                signatureMethodField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Reference")]
        public ReferenceType[] Reference {
            get {
                return referenceField;
            }
            set {
                referenceField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Signature", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignatureType {
        
        private SignedInfoType signedInfoField;
        
        private SignatureValueType signatureValueField;
        
        private KeyInfoType keyInfoField;
        
        private ObjectType[] objectField;
        
        private string idField;
        
        /// <comentarios/>
        public SignedInfoType SignedInfo {
            get {
                return signedInfoField;
            }
            set {
                signedInfoField = value;
            }
        }
        
        /// <comentarios/>
        public SignatureValueType SignatureValue {
            get {
                return signatureValueField;
            }
            set {
                signatureValueField = value;
            }
        }
        
        /// <comentarios/>
        public KeyInfoType KeyInfo {
            get {
                return keyInfoField;
            }
            set {
                keyInfoField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Object")]
        public ObjectType[] Object {
            get {
                return objectField;
            }
            set {
                objectField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("Manifest", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class ManifestType {
        
        private ReferenceType[] referenceField;
        
        private string idField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("Reference")]
        public ReferenceType[] Reference {
            get {
                return referenceField;
            }
            set {
                referenceField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureProperties", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignaturePropertiesType {
        
        private SignaturePropertyType[] signaturePropertyField;
        
        private string idField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElement("SignatureProperty")]
        public SignaturePropertyType[] SignatureProperty {
            get {
                return signaturePropertyField;
            }
            set {
                signaturePropertyField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }

    /// <comentarios/>
    [System.Xml.Serialization.XmlType(Namespace="http://www.w3.org/2000/09/xmldsig#")]
    [System.Xml.Serialization.XmlRoot("SignatureProperty", Namespace="http://www.w3.org/2000/09/xmldsig#", IsNullable=false)]
    public partial class SignaturePropertyType {
        
        private System.Xml.XmlElement[] itemsField;
        
        private string[] textField;
        
        private string targetField;
        
        private string idField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAnyElement()]
        public System.Xml.XmlElement[] Items {
            get {
                return itemsField;
            }
            set {
                itemsField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlText()]
        public string[] Text {
            get {
                return textField;
            }
            set {
                textField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
        public string Target {
            get {
                return targetField;
            }
            set {
                targetField = value;
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttribute(DataType="ID")]
        public string Id {
            get {
                return idField;
            }
            set {
                idField = value;
            }
        }
    }
}