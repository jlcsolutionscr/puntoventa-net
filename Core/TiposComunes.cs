﻿using System;

namespace LeandroSoftware.Core.TiposComunes
{
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
        public static readonly int Servicio = 2;
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
        public static readonly int TransferenciaDeposito = 2;
        public static readonly int Inversion = 3;
        public static readonly int NotaDebito = 4;
        public static readonly int NotaCredito = 5;
        public static readonly int ChequeEntrante = 6;
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

    public class ReporteVentas
    {
        public int IdFactura { get; set; }
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

    public class ReporteCompras
    {
        public int IdCompra { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string NoDocumento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteCuentasPorCobrar
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int IdCxC { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
    }

    public class ReporteCuentasPorPagar
    {
        public int IdPropietario { get; set; }
        public string Nombre { get; set; }
        public int IdCxP { get; set; }
        public string Fecha { get; set; }
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

    public class ReporteEstadoResultados
    {
        public string NombreTipoRegistro { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
    }

    public class ReporteDetalleEgreso
    {
        public int IdMov { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteDetalleIngreso
    {
        public int IdMov { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
    }

    public class ReporteVentasPorLineaResumen
    {
        public string Codigo { get; set; }
        public int IdLinea { get; set; }
        public string NombreLinea { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Costo { get; set; }
        public decimal PorcentajeIVA { get; set; }

    }

    public class ReporteVentasPorLineaDetalle
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdLinea { get; set; }
        public string NombreLinea { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Excento { get; set; }
        public decimal Gravado { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal PorcentajeIVA { get; set; }
    }

    public class ReporteCierreDeCaja
    {
        public decimal FondoInicio { get; set; }
        public decimal VentasPorMayor { get; set; }
        public decimal VentasDetalle { get; set; }
        public decimal VentasContado { get; set; }
        public decimal VentasCredito { get; set; }
        public decimal VentasTarjeta { get; set; }
        public decimal OtrasVentas { get; set; }
        public decimal RetencionIVA { get; set; }
        public decimal ComisionVT { get; set; }
        public decimal Liquidacion { get; set; }
        public decimal IngresoCxCEfectivo { get; set; }
        public decimal IngresoCxCTarjeta { get; set; }
        public decimal DevolucionesProveedores { get; set; }
        public decimal OtrosIngresos { get; set; }
        public decimal ComprasContado { get; set; }
        public decimal ComprasCredito { get; set; }
        public decimal OtrasCompras { get; set; }
        public decimal EgresoCxPEfectivo { get; set; }
        public decimal DevolucionesClientes { get; set; }
        public decimal OtrosEgresos { get; set; }
    }

    public class ReporteProforma
    {
        public int IdProforma { get; set; }
        public string NombreCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
        public string TipoPago { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal TotalLinea { get; set; }
    }

    public class ReporteOrdenServicio
    {
        public int IdOrden { get; set; }
        public string NombreCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
        public string Placa { get; set; }
        public string TipoPago { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal TotalLinea { get; set; }
    }

    public class ReporteOrdenCompra
    {
        public int IdOrden { get; set; }
        public string Nombre { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
        public string TipoPago { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal TotalLinea { get; set; }
    }

    public class ReporteInventario
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
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
        public string Descripcion { get; set; }
        public decimal MontoLocal { get; set; }
    }

    public class ReporteIngreso
    {
        public int IdIngreso { get; set; }
        public string Fecha { get; set; }
        public string RecibidoDe { get; set; }
        public string Detalle { get; set; }
        public decimal Monto { get; set; }
        public string MontoEnLetras { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoLocal { get; set; }
    }

    public class ReporteDocumentoElectronico
    {
        public string ClaveNumerica { get; set; }

        public string Consecutivo { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Moneda { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
    }

    public class EquipoRegistrado
    {
        public int IdSucursal { get; set; }
        public int IdTerminal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }
        public string TelefonoSucursal { get; set; }
        public string ImpresoraFactura { get; set; }
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

    public class DocumentoDetalle
    {
        public DocumentoDetalle()
        {
        }

        public DocumentoDetalle(int id, string clave, string consecutivo, DateTime fecha, string estado, string esMensajeReceptor, string correoNotificacion)
        {
            IdDocumento = id;
            ClaveNumerica = clave;
            Consecutivo = consecutivo;
            Fecha = fecha;
            EstadoEnvio = estado;
            EsMensajeReceptor = esMensajeReceptor;
            CorreoNotificacion = correoNotificacion;
        }
        public int IdDocumento { get; set; }
        public string ClaveNumerica { get; set; }
        public string Consecutivo { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoEnvio { get; set; }
        public string EsMensajeReceptor { get; set; }
        public string CorreoNotificacion { get; set; }
    }
}