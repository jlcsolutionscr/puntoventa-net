﻿using System;

namespace LeandroSoftware.Core.TiposComunes
{
    public static class StaticTipoUsuario
    {
        public static readonly short Normal = 1;
        public static readonly short Administrador = 2;
        public static readonly short Contador = 3;
    };

    public static class StaticTipoDispisitivo
    {
        public static readonly short AppEscritorio = 1;
        public static readonly short AppMovil = 2;
    };

    public static class StaticRolePorUsuario
    {
        public static readonly short ADMINISTRADOR = 1;
        public static readonly short USUARIO_SISTEMA = 2;
    };

    public static class StaticTipoMoneda
    {
        public static readonly short Colones = 1;
        public static readonly short Dolares = 2;
    };

    public static class StaticValoresPorDefecto
    {
        public static readonly short ClienteContado = 1;
        public static readonly short CodigoVendedor = 1;
        public static readonly int MonedaDelSistema = 1;
    };

    public static class StaticTipoCuentaPorCobrar
    {
        public static readonly short Clientes = 1;
    };

    public static class StaticTipoCuentaPorPagar
    {
        public static readonly short Proveedores = 1;
        public static readonly short Particulares = 2;
    };

    public static class StaticTipoProducto
    {
        public static readonly int Producto = 1;
        public static readonly int ServicioProfesionales = 2;
        public static readonly int OtrosServicios = 3;
        public static readonly int Transitorio = 4;
    };

    public static class StaticTipoMovimientoProducto
    {
        public static readonly string Entrada = "Entrada";
        public static readonly string Salida = "Salida";
    };

    public static class StaticCondicionVenta
    {
        public static readonly int Contado = 1;
        public static readonly int Credito = 2;
        public static readonly int Consignación = 3;
        public static readonly int Apartado = 4;
        public static readonly int ArrendamientoOpcionCompra = 5;
        public static readonly int ArrendamientoFuncionFinanciera = 6;
        public static readonly int Otros = 99;
    };

    public static class StaticFormaPago
    {
        public static readonly int Efectivo = 1;
        public static readonly int Tarjeta = 2;
        public static readonly int Cheque = 3;
        public static readonly int TransferenciaDepositoBancario = 4;
        public static readonly int RacaudadoPorTerceros = 5;
        public static readonly int Otros = 99;
    };

    public static class StaticReporteCondicionVentaFormaPago
    {
        public static readonly int Credito = 1;
        public static readonly int ContadoEfectivo = 2;
        public static readonly int ContadoTarjeta = 3;
        public static readonly int ContadoCheque = 4;
        public static readonly int ContadoTransferenciaDepositoBancario = 5;
        public static readonly int Otros = 6;

    }

    public static class StaticTipoNulo
    {
        public static readonly int Nulo = 1;
        public static readonly int NoNulo = 0;
    };

    public static class StaticTipoAbono
    {
        public static readonly short AbonoEfectivo = 1;
        public static readonly short NotaCredito = 2;
    };

    public static class StaticTipoCuentaContable
    {
        public static readonly int IngresosPorVentas = 1;
        public static readonly int CostosDeVentas = 2;
        public static readonly int IVAPorPagar = 3;
        public static readonly int LineaDeProductos = 4;
        public static readonly int LineaDeServicios = 5;
        public static readonly int CuentaDeBancos = 6;
        public static readonly int Efectivo = 7;
        public static readonly int OtraCondicionVenta = 8;
        public static readonly int CuentasPorCobrarClientes = 9;
        public static readonly int CuentasPorCobrarTarjeta = 10;
        public static readonly int GastoComisionTarjeta = 11;
        public static readonly int CuentasPorPagarProveedores = 12;
        public static readonly int CuentasPorPagarParticulares = 13;
        public static readonly int CuentaDeIngresos = 14;
        public static readonly int CuentaDeEgresos = 15;
        public static readonly int Traslados = 16;
        public static readonly int PerdidasyGanancias = 17;
        public static readonly int CuentasPorCobrarParticulares = 18;
    };

    public static class StaticTipoMovimientoBanco
    {
        public static readonly int ChequeSaliente = 1;
        public static readonly int DepositoEntrante = 2;
        public static readonly int Inversion = 3;
        public static readonly int NotaDebito = 4;
        public static readonly int NotaCredito = 5;
        public static readonly int ChequeEntrante = 6;
        public static readonly int DepositoSaliente = 7;
    };

    public static class StaticTipoDebitoCredito
    {
        public static readonly string Debito = "D";
        public static readonly string Credito = "C";
    };

    public static class StaticClaseCuentaContable
    {
        public static readonly int Activo = 1;
        public static readonly int Pasivo = 2;
        public static readonly int Resultado = 3;
        public static readonly int Patrimonio = 4;
    };

    public static class StaticEstadoDocumentoElectronico
    {
        public static readonly string Registrado = "registrado";
        public static readonly string Enviado = "enviado";
        public static readonly string Procesando = "procesando";
        public static readonly string Aceptado = "aceptado";
        public static readonly string Rechazado = "rechazado";
    };

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
        public DescripcionValor()
        {
        }

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
        public ReporteInventario()
        {

        }

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
        public ReporteCompra()
        {

        }

        public ReporteCompra(int id, string referencia, string nombreProveedor, string fecha, string codigo, string codigoProveedor, string descripcion, decimal cantidad, decimal precioVenta)
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
        public LlaveDescripcion()
        {
        }

        public LlaveDescripcion(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class ProductoDetalle
    {
        public ProductoDetalle()
        {
        }

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
        public FacturaDetalle()
        {
        }

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
        public TrasladoDetalle()
        {
        }

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
        public AjusteInventarioDetalle()
        {
        }

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
        public CompraDetalle()
        {
        }

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
        public DocumentoDetalle()
        {
        }

        public DocumentoDetalle(int id, string clave, string consecutivo, string fecha, string nombre, string estado, string error, decimal monto, string esMensajeReceptor, bool reprocesado, string correoNotificacion)
        {
            IdDocumento = id;
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
        public CierreDeEfectivo()
        {
        }

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
        public EfectivoDetalle()
        {
        }

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
        public CuentaPorProcesar()
        {
        }

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
}
