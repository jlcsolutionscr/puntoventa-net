namespace LeandroSoftware.Core.TiposDatosHacienda
{
    /// <remarks/>
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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
    [System.Serializable()]
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