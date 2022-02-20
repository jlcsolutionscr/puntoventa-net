using System.Collections.Generic;
using System.IO;

namespace LeandroSoftware.Common.DatosComunes
{
    public class ReporteDetalle
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string NoDocumento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteVentasPorVendedor
    {
        public string NombreVendedor { get; set; }
        public int IdFactura { get; set; }
        public string Fecha { get; set; }
        public string NombreCliente { get; set; }
        public string NoDocumento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteCuentas
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int IdCuenta { get; set; }
        public string Fecha { get; set; }
        public int Plazo { get; set; }
        public string FechaVence { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
    }

    public class ReporteMovimientosCxC
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int IdCxC { get; set; }
        public string DescCxC { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public int IdMovCxC { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
    }

    public class ReporteMovimientosCxP
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public int IdCxP { get; set; }
        public string DescCxP { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Recibo { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public int IdMovCxP { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
    }

    public class ReporteMovimientosBanco
    {
        public int IdMov { get; set; }
        public int IdCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public decimal SaldoAnterior { get; set; }
        public string Fecha { get; set; }
        public string Numero { get; set; }
        public string Beneficiario { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
        public decimal Saldo { get; set; }
    }

    public class DescripcionValor
    {
        public DescripcionValor() { }

        public DescripcionValor(string descripcion, decimal valor)
        {
            Descripcion = descripcion;
            Valor = valor;
        }

        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
    }

    public class ReporteGrupoDetalle
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteGrupoLineaDetalle
    {
        public string NombreLinea { get; set; }
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteInventario
    {
        public ReporteInventario() { }

        public ReporteInventario(int id, string codigo, string descripcion, decimal cantidad, decimal precioCosto, decimal precioVenta)
        {
            IdProducto = id;
            Codigo = codigo;
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioCosto = precioCosto;
            PrecioVenta = precioVenta;
        }

        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
    }

    public class ReporteCompra
    {
        public ReporteCompra() { }

        public ReporteCompra(int id, string referencia, string nombreProveedor, string fecha, string codigo, string codigoProveedor, string descripcion, decimal cantidad, decimal precioVenta, decimal precioVentaAnt)
        {
            IdCompra = id;
            Referencia = referencia;
            NombreProveedor = nombreProveedor;
            Fecha = fecha;
            Codigo = codigo;
            CodigoProveedor = codigoProveedor;
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioVenta = precioVenta;
            PrecioVentaAnt = precioVentaAnt;
        }

        public int IdCompra { get; set; }
        public string Referencia { get; set; }
        public string NombreProveedor { get; set; }
        public string Fecha { get; set; }
        public string Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioVentaAnt { get; set; }
    }

    public class ReporteMovimientosContables
    {
        public string Descripcion { get; set; }
        public decimal SaldoDebe { get; set; }
        public decimal SaldoHaber { get; set; }
    }

    public class ReporteBalanceComprobacion
    {
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }
        public decimal SaldoDebe { get; set; }
        public decimal SaldoHaber { get; set; }
    }

    public class ReportePerdidasyGanancias
    {
        public string Descripcion { get; set; }
        public int IdTipoCuenta { get; set; }
        public string DescGrupo { get; set; }
        public decimal SaldoDebe { get; set; }
        public decimal SaldoHaber { get; set; }
    }

    public class ReporteDetalleMovimientosCuentasDeBalance
    {
        public string DescCuentaBalance { get; set; }
        public string Descripcion { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Fecha { get; set; }
        public string Detalle { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
    }

    public class ReporteEgreso
    {
        public int IdEgreso { get; set; }
        public string Fecha { get; set; }
        public string Detalle { get; set; }
        public string Beneficiario { get; set; }
        public decimal Monto { get; set; }
        public string MontoEnLetras { get; set; }
    }

    public class ReporteIngreso
    {
        public int IdIngreso { get; set; }
        public string Fecha { get; set; }
        public string RecibidoDe { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public string MontoEnLetras { get; set; }
    }

    public class ReporteDocumentoElectronico
    {
        public string TipoDocumento { get; set; }
        public string ClaveNumerica { get; set; }

        public string Consecutivo { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Moneda { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteResumenMovimiento
    {
        public string Descripcion { get; set; }
        public decimal Exento { get; set; }
        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa4 { get; set; }
        public decimal Tasa8 { get; set; }
        public decimal Tasa13 { get; set; }

    }

    public class EquipoRegistrado
    {
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }
        public string TelefonoSucursal { get; set; }
        public string ImpresoraFactura { get; set; }
        public int AnchoLinea { get; set; }
    }

    public class LlaveDescripcion
    {
        public LlaveDescripcion() { }

        public LlaveDescripcion(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class LlaveDescripcionValor
    {
        public LlaveDescripcionValor() { }

        public LlaveDescripcionValor(int id, string descripcion, decimal valor)
        {
            Id = id;
            Descripcion = descripcion;
            Valor = valor;
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
    }

    public class ProductoDetalle
    {
        public ProductoDetalle() { }

        public ProductoDetalle(int id, string codigo, string codigoProveedor, string descripcion, decimal cantidad, decimal precioCosto, decimal precioVenta1, string observacion, decimal utilidad, bool activo)
        {
            Id = id;
            Codigo = codigo;
            CodigoProveedor = codigoProveedor;
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioCosto = precioCosto;
            PrecioVenta1 = precioVenta1;
            Observacion = observacion;
            Utilidad = utilidad;
            Activo = activo;
        }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta1 { get; set; }
        public string Observacion { get; set; }
        public decimal Utilidad { get; set; }
        public bool Activo { get; set; }
    }

    public class FacturaDetalle
    {
        public FacturaDetalle() { }

        public FacturaDetalle(int id, int consecutivo, string nombreCliente, string identificacion, string fecha, decimal gravado, decimal exonerado, decimal excento, decimal impuesto, decimal total, decimal saldo, string estado, string descripcion)
        {
            IdFactura = id;
            Consecutivo = consecutivo;
            NombreCliente = nombreCliente;
            Identificacion = identificacion;
            Fecha = fecha;
            Gravado = gravado;
            Exonerado = exonerado;
            Excento = excento;
            Impuesto = impuesto;
            Total = total;
            Saldo = saldo;
            Estado = estado;
            Descripcion = descripcion;
        }
        public int IdFactura { get; set; }
        public int Consecutivo { get; set; }
        public string NombreCliente { get; set; }
        public string Identificacion { get; set; }
        public string Fecha { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exonerado { get; set; }
        public decimal Excento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }

    public class TrasladoDetalle
    {
        public TrasladoDetalle() { }

        public TrasladoDetalle(int id, string fecha, string nombreSucursal, decimal total)
        {
            IdTraslado = id;
            Fecha = fecha;
            NombreSucursal = nombreSucursal;
            Total = total;
        }
        public int IdTraslado { get; set; }
        public string Fecha { get; set; }
        public string NombreSucursal { get; set; }
        public decimal Total { get; set; }
    }

    public class AjusteInventarioDetalle
    {
        public AjusteInventarioDetalle() { }

        public AjusteInventarioDetalle(int id, string fecha, string descripcion)
        {
            IdAjuste = id;
            Fecha = fecha;
            Descripcion = descripcion;
        }
        public int IdAjuste { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
    }

    public class CompraDetalle
    {
        public CompraDetalle() { }

        public CompraDetalle(int id, string referencia, string nombreProveedor, string fecha, decimal gravado, decimal excento, decimal impuesto, decimal total)
        {
            IdCompra = id;
            RefFactura = referencia;
            NombreProveedor = nombreProveedor;
            Fecha = fecha;
            Gravado = gravado;
            Excento = excento;
            Impuesto = impuesto;
            Total = total;
        }
        public int IdCompra { get; set; }
        public string RefFactura { get; set; }
        public string NombreProveedor { get; set; }
        public string Fecha { get; set; }
        public decimal Gravado { get; set; }
        public decimal Excento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class DocumentoDetalle
    {
        public DocumentoDetalle() { }

        public DocumentoDetalle(int id, int idTipo, string clave, string consecutivo, string fecha, string nombre, string estado, string error, decimal monto, string esMensajeReceptor, bool reprocesado, string correoNotificacion)
        {
            IdDocumento = id;
            IdTipoDocumento = idTipo;
            ClaveNumerica = clave;
            Consecutivo = consecutivo;
            Fecha = fecha;
            NombreReceptor = nombre;
            EstadoEnvio = estado;
            ErrorEnvio = error;
            MontoTotal = monto;
            EsMensajeReceptor = esMensajeReceptor;
            Reprocesado = reprocesado;
            CorreoNotificacion = correoNotificacion;
        }
        public int IdDocumento { get; set; }
        public int IdTipoDocumento { get; set; }
        public string ClaveNumerica { get; set; }
        public string Consecutivo { get; set; }
        public string Fecha { get; set; }
        public string NombreReceptor { get; set; }
        public string EstadoEnvio { get; set; }
        public string ErrorEnvio { get; set; }
        public decimal MontoTotal { get; set; }
        public string EsMensajeReceptor { get; set; }
        public bool Reprocesado { get; set; }
        public string CorreoNotificacion { get; set; }
    }

    public class CierreDeEfectivo
    {
        public CierreDeEfectivo() { }

        public CierreDeEfectivo(int id, string fecha, string detalle, decimal total)
        {
            Id = id;
            Fecha = fecha;
            Detalle = detalle;
            Total = total;
        }
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Detalle { get; set; }
        public decimal Total { get; set; }
    }

    public class EfectivoDetalle
    {
        public EfectivoDetalle() { }

        public EfectivoDetalle(int id, string fecha, string descripcion, decimal total)
        {
            Id = id;
            Fecha = fecha;
            Descripcion = descripcion;
            Total = total;
        }
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
    }

    public class CuentaPorProcesar
    {
        public CuentaPorProcesar() { }

        public CuentaPorProcesar(int id, string fecha, string propietario, string referencia, decimal total, decimal saldo)
        {
            Id = id;
            Fecha = fecha;
            Propietario = propietario;
            Referencia = referencia;
            Total = total;
            Saldo = saldo;
        }
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Propietario { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
    }

    public class ClsLineaImpresion
    {
        public ClsLineaImpresion() { }

        public ClsLineaImpresion(int saltos, string texto, int posicionX, int ancho, int fuente, int alineado, bool bold)
        {
            intSaltos = saltos;
            strTexto = texto;
            intPosicionX = posicionX;
            intAncho = ancho;
            intFuente = fuente;
            intAlineado = alineado;
            bolBold = bold;
        }

        public int intSaltos { get; set; }
        public string strTexto { get; set; }
        public int intPosicionX { get; set; }
        public int intAncho { get; set; }
        public int intFuente { get; set; }
        public int intAlineado { get; set; }
        public bool bolBold { get; set; }
    }

    public class ClsTiquete
    {
        public ClsTiquete() { }

        public ClsTiquete(int idTiquete, int idEmpresa, int idSucursal, string descripcion, string impresora, IList<ClsLineaImpresion> lineas, bool impreso)
        {
            IdTiquete = idTiquete;
            IdEmpresa = idEmpresa;
            IdSucursal = idSucursal;
            Descripcion = descripcion;
            Impresora = impresora;
            Lineas = lineas;
            Impreso = impreso;
        }
        public int IdTiquete { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Descripcion { get; set; }
        public string Impresora { get; set; }
        public IList<ClsLineaImpresion> Lineas { get; set; }
        public bool Impreso { get; set; }
    }

    public class EstructuraPDF
    {
        public EstructuraPDF()
        {
            DetalleServicio = new HashSet<EstructuraPDFDetalleServicio>();
        }

        public string TituloDocumento { get; set; }
        public string NombreComercial { get; set; }
        public string NombreEmpresa { get; set; }
        public string ConsecInterno { get; set; }
        public string Consecutivo { get; set; }
        public string PlazoCredito { get; set; }
        public string Clave { get; set; }
        public string CondicionVenta { get; set; }
        public string Fecha { get; set; }
        public string MedioPago { get; set; }
        public string NombreEmisor { get; set; }
        public string IdentificacionEmisor { get; set; }
        public string NombreComercialEmisor { get; set; }
        public string CorreoElectronicoEmisor { get; set; }
        public string TelefonoEmisor { get; set; }
        public string FaxEmisor { get; set; }
        public string ProvinciaEmisor { get; set; }
        public string CantonEmisor { get; set; }
        public string DistritoEmisor { get; set; }
        public string BarrioEmisor { get; set; }
        public string DireccionEmisor { get; set; }
        public string NombreReceptor { get; set; }
        public bool PoseeReceptor { get; set; }
        public string IdentificacionReceptor { get; set; }
        public string NombreComercialReceptor { get; set; }
        public string CorreoElectronicoReceptor { get; set; }
        public string TelefonoReceptor { get; set; }
        public string FaxReceptor { get; set; }
        public string TotalGravado { get; set; }
        public string TotalExonerado { get; set; }
        public string TotalExento { get; set; }
        public string Descuento { get; set; }
        public string Impuesto { get; set; }
        public string TotalGeneral { get; set; }
        public string OtrosTextos { get; set; }
        public string CodigoMoneda { get; set; }
        public string TipoDeCambio { get; set; }
        public byte[] Logotipo { get; set; }
        public byte[] PoweredByLogotipo { get; set; }

        public ICollection<EstructuraPDFDetalleServicio> DetalleServicio { get; set; }
    }

    public class EstructuraPDFDetalleServicio
    {
        public string Cantidad { get; set; }
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public string PrecioUnitario { get; set; }
        public string TotalLinea { get; set; }
    }

    public class EstructuraListadoProductosPDF
    {
        public EstructuraListadoProductosPDF()
        {
            DetalleProducto = new HashSet<EstructuraPDFDetalleProducto>();
        }

        public string TituloDocumento { get; set; }
        public string TotalInventario { get; set; }
        public ICollection<EstructuraPDFDetalleProducto> DetalleProducto { get; set; }
    }

    public class EstructuraPDFDetalleProducto
    {

        public string Codigo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Descripcion { get; set; }
        public string Existencias { get; set; }
        public string PrecioCosto { get; set; }
        public string PrecioVenta { get; set; }
        public string TotalLinea { get; set; }
    }
}
