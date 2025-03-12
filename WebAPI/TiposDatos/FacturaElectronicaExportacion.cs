namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion", IsNullable = false)]
    public partial class FacturaElectronicaExportacion
    {

        private string claveField;

        private string proveedorSistemasField;

        private string codigoActividadEmisorField;

        private string numeroConsecutivoField;

        private DateTime fechaEmisionField;

        private EmisorType emisorField;

        private ReceptorType receptorField;

        private FacturaElectronicaExportacionCondicionVenta condicionVentaField;

        private string condicionVentaOtrosField;

        private string plazoCreditoField;

        private FacturaElectronicaExportacionLineaDetalle[] detalleServicioField;

        private OtrosCargosType[] otrosCargosField;

        private FacturaElectronicaExportacionResumenFactura resumenFacturaField;

        private FacturaElectronicaExportacionInformacionReferencia[] informacionReferenciaField;

        private FacturaElectronicaExportacionOtros otrosField;

        private SignatureType signatureField;

        /// <remarks/>
        public string Clave
        {
            get => claveField;
            set => claveField = value;
        }

        /// <remarks/>
        public string ProveedorSistemas
        {
            get => proveedorSistemasField;
            set => proveedorSistemasField = value;
        }

        /// <remarks/>
        public string CodigoActividadEmisor
        {
            get => codigoActividadEmisorField;
            set => codigoActividadEmisorField = value;
        }

        /// <remarks/>
        public string NumeroConsecutivo
        {
            get => numeroConsecutivoField;
            set => numeroConsecutivoField = value;
        }

        /// <remarks/>
        public DateTime FechaEmision
        {
            get => fechaEmisionField;
            set => fechaEmisionField = value;
        }

        /// <remarks/>
        public EmisorType Emisor
        {
            get => emisorField;
            set => emisorField = value;
        }

        /// <remarks/>
        public ReceptorType Receptor
        {
            get => receptorField;
            set => receptorField = value;
        }

        /// <remarks/>
        public FacturaElectronicaExportacionCondicionVenta CondicionVenta
        {
            get => condicionVentaField;
            set => condicionVentaField = value;
        }

        /// <remarks/>
        public string CondicionVentaOtros
        {
            get => condicionVentaOtrosField;
            set => condicionVentaOtrosField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "integer")]
        public string PlazoCredito
        {
            get => plazoCreditoField;
            set => plazoCreditoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalle", IsNullable = false)]
        public FacturaElectronicaExportacionLineaDetalle[] DetalleServicio
        {
            get => detalleServicioField;
            set => detalleServicioField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtrosCargos")]
        public OtrosCargosType[] OtrosCargos
        {
            get => otrosCargosField;
            set => otrosCargosField = value;
        }

        /// <remarks/>
        public FacturaElectronicaExportacionResumenFactura ResumenFactura
        {
            get => resumenFacturaField;
            set => resumenFacturaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public FacturaElectronicaExportacionInformacionReferencia[] InformacionReferencia
        {
            get => informacionReferenciaField;
            set => informacionReferenciaField = value;
        }

        /// <remarks/>
        public FacturaElectronicaExportacionOtros Otros
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public enum FacturaElectronicaExportacionCondicionVenta
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
        [System.Xml.Serialization.XmlEnum("10")]
        Item10,

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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionLineaDetalle
    {

        private string numeroLineaField;

        private string partidaArancelariaField;

        private string codigoCABYSField;

        private CodigoType[] codigoComercialField;

        private decimal cantidadField;

        private UnidadMedidaType unidadMedidaField;

        private FacturaElectronicaExportacionLineaDetalleTipoTransaccion tipoTransaccionField;

        private bool tipoTransaccionFieldSpecified;

        private string unidadMedidaComercialField;

        private string detalleField;

        private string[] numeroVINoSerieField;

        private string registroMedicamentoField;

        private string formaFarmaceuticaField;

        private FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtido[] detalleSurtidoField;

        private decimal precioUnitarioField;

        private decimal montoTotalField;

        private DescuentoType[] descuentoField;

        private decimal subTotalField;

        private ImpuestoType[] impuestoField;

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
        public string CodigoCABYS
        {
            get => codigoCABYSField;
            set => codigoCABYSField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CodigoComercial")]
        public CodigoType[] CodigoComercial
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
        public UnidadMedidaType UnidadMedida
        {
            get => unidadMedidaField;
            set => unidadMedidaField = value;
        }

        /// <remarks/>
        public FacturaElectronicaExportacionLineaDetalleTipoTransaccion TipoTransaccion
        {
            get => tipoTransaccionField;
            set => tipoTransaccionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TipoTransaccionSpecified
        {
            get => tipoTransaccionFieldSpecified;
            set => tipoTransaccionFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElement("NumeroVINoSerie")]
        public string[] NumeroVINoSerie
        {
            get => numeroVINoSerieField;
            set => numeroVINoSerieField = value;
        }

        /// <remarks/>
        public string RegistroMedicamento
        {
            get => registroMedicamentoField;
            set => registroMedicamentoField = value;
        }

        /// <remarks/>
        public string FormaFarmaceutica
        {
            get => formaFarmaceuticaField;
            set => formaFarmaceuticaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("LineaDetalleSurtido", IsNullable = false)]
        public FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtido[] DetalleSurtido
        {
            get => detalleSurtidoField;
            set => detalleSurtidoField = value;
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
        public DescuentoType[] Descuento
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
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public ImpuestoType[] Impuesto
        {
            get => impuestoField;
            set => impuestoField = value;
        }

        /// <remarks/>
        public decimal MontoTotalLinea
        {
            get => montoTotalLineaField;
            set => montoTotalLineaField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public enum FacturaElectronicaExportacionLineaDetalleTipoTransaccion
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
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtido
    {

        private string codigoCABYSSurtidoField;

        private FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtido[] codigoComercialSurtidoField;

        private decimal cantidadSurtidoField;

        private UnidadMedidaType unidadMedidaSurtidoField;

        private string unidadMedidaComercialSurtidoField;

        private string detalleSurtidoField;

        private decimal precioUnitarioSurtidoField;

        private decimal montoTotalSurtidoField;

        private FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoDescuentoSurtido[] descuentoSurtidoField;

        private decimal subTotalSurtidoField;

        private FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoImpuestoSurtido[] impuestoSurtidoField;

        /// <remarks/>
        public string CodigoCABYSSurtido
        {
            get => codigoCABYSSurtidoField;
            set => codigoCABYSSurtidoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("CodigoComercialSurtido")]
        public FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtido[] CodigoComercialSurtido
        {
            get => codigoComercialSurtidoField;
            set => codigoComercialSurtidoField = value;
        }

        /// <remarks/>
        public decimal CantidadSurtido
        {
            get => cantidadSurtidoField;
            set => cantidadSurtidoField = value;
        }

        /// <remarks/>
        public UnidadMedidaType UnidadMedidaSurtido
        {
            get => unidadMedidaSurtidoField;
            set => unidadMedidaSurtidoField = value;
        }

        /// <remarks/>
        public string UnidadMedidaComercialSurtido
        {
            get => unidadMedidaComercialSurtidoField;
            set => unidadMedidaComercialSurtidoField = value;
        }

        /// <remarks/>
        public string DetalleSurtido
        {
            get => detalleSurtidoField;
            set => detalleSurtidoField = value;
        }

        /// <remarks/>
        public decimal PrecioUnitarioSurtido
        {
            get => precioUnitarioSurtidoField;
            set => precioUnitarioSurtidoField = value;
        }

        /// <remarks/>
        public decimal MontoTotalSurtido
        {
            get => montoTotalSurtidoField;
            set => montoTotalSurtidoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("DescuentoSurtido")]
        public FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoDescuentoSurtido[] DescuentoSurtido
        {
            get => descuentoSurtidoField;
            set => descuentoSurtidoField = value;
        }

        /// <remarks/>
        public decimal SubTotalSurtido
        {
            get => subTotalSurtidoField;
            set => subTotalSurtidoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ImpuestoSurtido")]
        public FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoImpuestoSurtido[] ImpuestoSurtido
        {
            get => impuestoSurtidoField;
            set => impuestoSurtidoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtido
    {

        private FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtidoTipoSurtido tipoSurtidoField;

        private string codigoSurtidoField;

        /// <remarks/>
        public FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtidoTipoSurtido TipoSurtido
        {
            get => tipoSurtidoField;
            set => tipoSurtidoField = value;
        }

        /// <remarks/>
        public string CodigoSurtido
        {
            get => codigoSurtidoField;
            set => codigoSurtidoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public enum FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoCodigoComercialSurtidoTipoSurtido
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoDescuentoSurtido
    {

        private decimal montoDescuentoSurtidoField;

        private CodigoDescuentoType codigoDescuentoSurtidoField;

        private string descuentoSurtidoOtrosField;

        /// <remarks/>
        public decimal MontoDescuentoSurtido
        {
            get => montoDescuentoSurtidoField;
            set => montoDescuentoSurtidoField = value;
        }

        /// <remarks/>
        public CodigoDescuentoType CodigoDescuentoSurtido
        {
            get => codigoDescuentoSurtidoField;
            set => codigoDescuentoSurtidoField = value;
        }

        /// <remarks/>
        public string DescuentoSurtidoOtros
        {
            get => descuentoSurtidoOtrosField;
            set => descuentoSurtidoOtrosField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionLineaDetalleLineaDetalleSurtidoImpuestoSurtido
    {

        private CodigoImpuestoType codigoImpuestoSurtidoField;

        private string codigoImpuestoOTROSurtidoField;

        private CodigoTarifaIVAType codigoTarifaIVASurtidoField;

        private bool codigoTarifaIVASurtidoFieldSpecified;

        private decimal tarifaSurtidoField;

        private bool tarifaSurtidoFieldSpecified;

        private decimal montoImpuestoSurtidoField;

        /// <remarks/>
        public CodigoImpuestoType CodigoImpuestoSurtido
        {
            get => codigoImpuestoSurtidoField;
            set => codigoImpuestoSurtidoField = value;
        }

        /// <remarks/>
        public string CodigoImpuestoOTROSurtido
        {
            get => codigoImpuestoOTROSurtidoField;
            set => codigoImpuestoOTROSurtidoField = value;
        }

        /// <remarks/>
        public CodigoTarifaIVAType CodigoTarifaIVASurtido
        {
            get => codigoTarifaIVASurtidoField;
            set => codigoTarifaIVASurtidoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoTarifaIVASurtidoSpecified
        {
            get => codigoTarifaIVASurtidoFieldSpecified;
            set => codigoTarifaIVASurtidoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TarifaSurtido
        {
            get => tarifaSurtidoField;
            set => tarifaSurtidoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TarifaSurtidoSpecified
        {
            get => tarifaSurtidoFieldSpecified;
            set => tarifaSurtidoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal MontoImpuestoSurtido
        {
            get => montoImpuestoSurtidoField;
            set => montoImpuestoSurtidoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionResumenFactura
    {

        private CodigoMonedaType codigoTipoMonedaField;

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

        private FacturaElectronicaExportacionResumenFacturaTotalDesgloseImpuesto[] totalDesgloseImpuestoField;

        private decimal totalImpuestoField;

        private bool totalImpuestoFieldSpecified;

        private decimal totalImpAsumEmisorFabricaField;

        private bool totalImpAsumEmisorFabricaFieldSpecified;

        private decimal totalOtrosCargosField;

        private bool totalOtrosCargosFieldSpecified;

        private FacturaElectronicaExportacionResumenFacturaMedioPago[] medioPagoField;

        private decimal totalComprobanteField;

        /// <remarks/>
        public CodigoMonedaType CodigoTipoMoneda
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
        [System.Xml.Serialization.XmlElement("TotalDesgloseImpuesto")]
        public FacturaElectronicaExportacionResumenFacturaTotalDesgloseImpuesto[] TotalDesgloseImpuesto
        {
            get => totalDesgloseImpuestoField;
            set => totalDesgloseImpuestoField = value;
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
        public decimal TotalImpAsumEmisorFabrica
        {
            get => totalImpAsumEmisorFabricaField;
            set => totalImpAsumEmisorFabricaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalImpAsumEmisorFabricaSpecified
        {
            get => totalImpAsumEmisorFabricaFieldSpecified;
            set => totalImpAsumEmisorFabricaFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElement("MedioPago")]
        public FacturaElectronicaExportacionResumenFacturaMedioPago[] MedioPago
        {
            get => medioPagoField;
            set => medioPagoField = value;
        }

        /// <remarks/>
        public decimal TotalComprobante
        {
            get => totalComprobanteField;
            set => totalComprobanteField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionResumenFacturaTotalDesgloseImpuesto
    {

        private CodigoImpuestoType codigoField;

        private CodigoTarifaIVAType codigoTarifaIVAField;

        private bool codigoTarifaIVAFieldSpecified;

        private decimal totalMontoImpuestoField;

        /// <remarks/>
        public CodigoImpuestoType Codigo
        {
            get => codigoField;
            set => codigoField = value;
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
        public decimal TotalMontoImpuesto
        {
            get => totalMontoImpuestoField;
            set => totalMontoImpuestoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionResumenFacturaMedioPago
    {

        private FacturaElectronicaExportacionResumenFacturaMedioPagoTipoMedioPago tipoMedioPagoField;

        private bool tipoMedioPagoFieldSpecified;

        private string medioPagoOtrosField;

        private decimal totalMedioPagoField;

        private bool totalMedioPagoFieldSpecified;

        /// <remarks/>
        public FacturaElectronicaExportacionResumenFacturaMedioPagoTipoMedioPago TipoMedioPago
        {
            get => tipoMedioPagoField;
            set => tipoMedioPagoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TipoMedioPagoSpecified
        {
            get => tipoMedioPagoFieldSpecified;
            set => tipoMedioPagoFieldSpecified = value;
        }

        /// <remarks/>
        public string MedioPagoOtros
        {
            get => medioPagoOtrosField;
            set => medioPagoOtrosField = value;
        }

        /// <remarks/>
        public decimal TotalMedioPago
        {
            get => totalMedioPagoField;
            set => totalMedioPagoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMedioPagoSpecified
        {
            get => totalMedioPagoFieldSpecified;
            set => totalMedioPagoFieldSpecified = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public enum FacturaElectronicaExportacionResumenFacturaMedioPagoTipoMedioPago
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionInformacionReferencia
    {

        private TipoDocReferenciaType tipoDocIRField;

        private string tipoDocRefOTROField;

        private string numeroField;

        private DateTime fechaEmisionIRField;

        private CodigoReferenciaType codigoField;

        private bool codigoFieldSpecified;

        private string codigoReferenciaOTROField;

        private string razonField;

        /// <remarks/>
        public TipoDocReferenciaType TipoDocIR
        {
            get => tipoDocIRField;
            set => tipoDocIRField = value;
        }

        /// <remarks/>
        public string TipoDocRefOTRO
        {
            get => tipoDocRefOTROField;
            set => tipoDocRefOTROField = value;
        }

        /// <remarks/>
        public string Numero
        {
            get => numeroField;
            set => numeroField = value;
        }

        /// <remarks/>
        public DateTime FechaEmisionIR
        {
            get => fechaEmisionIRField;
            set => fechaEmisionIRField = value;
        }

        /// <remarks/>
        public CodigoReferenciaType Codigo
        {
            get => codigoField;
            set => codigoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool CodigoSpecified
        {
            get => codigoFieldSpecified;
            set => codigoFieldSpecified = value;
        }

        /// <remarks/>
        public string CodigoReferenciaOTRO
        {
            get => codigoReferenciaOTROField;
            set => codigoReferenciaOTROField = value;
        }

        /// <remarks/>
        public string Razon
        {
            get => razonField;
            set => razonField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionOtros
    {

        private FacturaElectronicaExportacionOtrosOtroTexto[] otroTextoField;

        private FacturaElectronicaExportacionOtrosOtroContenido[] otroContenidoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroTexto")]
        public FacturaElectronicaExportacionOtrosOtroTexto[] OtroTexto
        {
            get => otroTextoField;
            set => otroTextoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroContenido")]
        public FacturaElectronicaExportacionOtrosOtroContenido[] OtroContenido
        {
            get => otroContenidoField;
            set => otroContenidoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionOtrosOtroTexto
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
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaExportacion")]
    public partial class FacturaElectronicaExportacionOtrosOtroContenido
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
}
