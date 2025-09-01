namespace LeandroSoftware.ServicioWeb.TiposDatosHacienda
{
    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    [System.Xml.Serialization.XmlRoot(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra", IsNullable = false)]
    public partial class FacturaElectronicaCompra
    {

        private string claveField;

        private string proveedorSistemasField;

        private string codigoActividadEmisorField;

        private string codigoActividadReceptorField;

        private string numeroConsecutivoField;

        private DateTime fechaEmisionField;

        private EmisorType emisorField;

        private ReceptorType receptorField;

        private FacturaElectronicaCompraCondicionVenta condicionVentaField;

        private string condicionVentaOtrosField;

        private string plazoCreditoField;

        private FacturaElectronicaCompraLineaDetalle[] detalleServicioField;

        private OtrosCargosType[] otrosCargosField;

        private FacturaElectronicaCompraResumenFactura resumenFacturaField;

        private FacturaElectronicaCompraInformacionReferencia[] informacionReferenciaField;

        private FacturaElectronicaCompraOtros otrosField;

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
        public string CodigoActividadReceptor
        {
            get => codigoActividadReceptorField;
            set => codigoActividadReceptorField = value;
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
        public FacturaElectronicaCompraCondicionVenta CondicionVenta
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
        public FacturaElectronicaCompraLineaDetalle[] DetalleServicio
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
        public FacturaElectronicaCompraResumenFactura ResumenFactura
        {
            get => resumenFacturaField;
            set => resumenFacturaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InformacionReferencia")]
        public FacturaElectronicaCompraInformacionReferencia[] InformacionReferencia
        {
            get => informacionReferenciaField;
            set => informacionReferenciaField = value;
        }

        /// <remarks/>
        public FacturaElectronicaCompraOtros Otros
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraCondicionVenta
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraLineaDetalle
    {

        private string numeroLineaField;

        private string codigoCABYSField;

        private CodigoType[] codigoComercialField;

        private decimal cantidadField;

        private UnidadMedidaType unidadMedidaField;

        private FacturaElectronicaCompraLineaDetalleTipoTransaccion tipoTransaccionField;

        private bool tipoTransaccionFieldSpecified;

        private string unidadMedidaComercialField;

        private string detalleField;

        private string[] numeroVINoSerieField;

        private string registroMedicamentoField;

        private string formaFarmaceuticaField;

        private decimal precioUnitarioField;

        private decimal montoTotalField;

        private DescuentoType[] descuentoField;

        private decimal subTotalField;

        private decimal baseImponibleField;

        private ImpuestoType[] impuestoField;

        private decimal impuestoNetoField;

        private decimal montoTotalLineaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "positiveInteger")]
        public string NumeroLinea
        {
            get => numeroLineaField;
            set => numeroLineaField = value;
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
        public FacturaElectronicaCompraLineaDetalleTipoTransaccion TipoTransaccion
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
        public decimal BaseImponible
        {
            get => baseImponibleField;
            set => baseImponibleField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Impuesto")]
        public ImpuestoType[] Impuesto
        {
            get => impuestoField;
            set => impuestoField = value;
        }

        /// <remarks/>
        public decimal ImpuestoNeto
        {
            get => impuestoNetoField;
            set => impuestoNetoField = value;
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
    [System.Xml.Serialization.XmlType(Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraLineaDetalleTipoTransaccion
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraResumenFactura
    {

        private CodigoMonedaType codigoTipoMonedaField;

        private decimal totalServGravadosField;

        private bool totalServGravadosFieldSpecified;

        private decimal totalServExentosField;

        private bool totalServExentosFieldSpecified;

        private decimal totalServExoneradoField;

        private bool totalServExoneradoFieldSpecified;

        private decimal totalServNoSujetoField;

        private bool totalServNoSujetoFieldSpecified;

        private decimal totalMercanciasGravadasField;

        private bool totalMercanciasGravadasFieldSpecified;

        private decimal totalMercanciasExentasField;

        private bool totalMercanciasExentasFieldSpecified;

        private decimal totalMercExoneradaField;

        private bool totalMercExoneradaFieldSpecified;

        private decimal totalMercNoSujetaField;

        private bool totalMercNoSujetaFieldSpecified;

        private decimal totalGravadoField;

        private bool totalGravadoFieldSpecified;

        private decimal totalExentoField;

        private bool totalExentoFieldSpecified;

        private decimal totalExoneradoField;

        private bool totalExoneradoFieldSpecified;

        private decimal totalNoSujetoField;

        private bool totalNoSujetoFieldSpecified;

        private decimal totalVentaField;

        private decimal totalDescuentosField;

        private bool totalDescuentosFieldSpecified;

        private decimal totalVentaNetaField;

        private FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto[] totalDesgloseImpuestoField;

        private decimal totalImpuestoField;

        private bool totalImpuestoFieldSpecified;

        private decimal totalImpAsumEmisorFabricaField;

        private bool totalImpAsumEmisorFabricaFieldSpecified;

        private decimal totalOtrosCargosField;

        private bool totalOtrosCargosFieldSpecified;

        private FacturaElectronicaCompraResumenFacturaMedioPago[] medioPagoField;

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
        public decimal TotalServExonerado
        {
            get => totalServExoneradoField;
            set => totalServExoneradoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServExoneradoSpecified
        {
            get => totalServExoneradoFieldSpecified;
            set => totalServExoneradoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalServNoSujeto
        {
            get => totalServNoSujetoField;
            set => totalServNoSujetoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalServNoSujetoSpecified
        {
            get => totalServNoSujetoFieldSpecified;
            set => totalServNoSujetoFieldSpecified = value;
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
        public decimal TotalMercExonerada
        {
            get => totalMercExoneradaField;
            set => totalMercExoneradaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercExoneradaSpecified
        {
            get => totalMercExoneradaFieldSpecified;
            set => totalMercExoneradaFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalMercNoSujeta
        {
            get => totalMercNoSujetaField;
            set => totalMercNoSujetaField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalMercNoSujetaSpecified
        {
            get => totalMercNoSujetaFieldSpecified;
            set => totalMercNoSujetaFieldSpecified = value;
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
        public decimal TotalExonerado
        {
            get => totalExoneradoField;
            set => totalExoneradoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalExoneradoSpecified
        {
            get => totalExoneradoFieldSpecified;
            set => totalExoneradoFieldSpecified = value;
        }

        /// <remarks/>
        public decimal TotalNoSujeto
        {
            get => totalNoSujetoField;
            set => totalNoSujetoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TotalNoSujetoSpecified
        {
            get => totalNoSujetoFieldSpecified;
            set => totalNoSujetoFieldSpecified = value;
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
        public FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto[] TotalDesgloseImpuesto
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
        public FacturaElectronicaCompraResumenFacturaMedioPago[] MedioPago
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraResumenFacturaTotalDesgloseImpuesto
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraResumenFacturaMedioPago
    {

        private FacturaElectronicaCompraResumenFacturaMedioPagoTipoMedioPago tipoMedioPagoField;

        private bool tipoMedioPagoFieldSpecified;

        private string medioPagoOtrosField;

        private decimal totalMedioPagoField;

        private bool totalMedioPagoFieldSpecified;

        /// <remarks/>
        public FacturaElectronicaCompraResumenFacturaMedioPagoTipoMedioPago TipoMedioPago
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public enum FacturaElectronicaCompraResumenFacturaMedioPagoTipoMedioPago
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraInformacionReferencia
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtros
    {

        private FacturaElectronicaCompraOtrosOtroTexto[] otroTextoField;

        private FacturaElectronicaCompraOtrosOtroContenido[] otroContenidoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroTexto")]
        public FacturaElectronicaCompraOtrosOtroTexto[] OtroTexto
        {
            get => otroTextoField;
            set => otroTextoField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("OtroContenido")]
        public FacturaElectronicaCompraOtrosOtroContenido[] OtroContenido
        {
            get => otroContenidoField;
            set => otroContenidoField = value;
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtrosOtroTexto
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
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronicaCompra")]
    public partial class FacturaElectronicaCompraOtrosOtroContenido
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
