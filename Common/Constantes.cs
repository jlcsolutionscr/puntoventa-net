using System.Collections.Generic;

namespace LeandroSoftware.Common.Constantes
{

    public static class StaticModalidadEmpresa
    {
        public static readonly short Puntoventa = 1;
        public static readonly short Restaurante = 2;
    };
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
        public static readonly short SOPORTE = 3;
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
        public static readonly int TasaImpuesto = 8;
        public static readonly int TipoExoneracion = 1;
        public static readonly int IdNombreInstExoneracion = 1;
    };

    public static class StaticTipoProducto
    {
        public static readonly int Producto = 1;
        public static readonly int ServicioProfesionales = 2;
        public static readonly int OtrosServicios = 3;
    };

    public static class StaticTipoProductoEspecial
    {
        public static readonly string Transitorio = "CMD";
        public static readonly string ImpuestoServicio = "IMPSER";
    }

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
        public static readonly int SinpeMovil = 6;
        public static readonly int PlataformaDigital = 7;
        public static readonly int NotaCredito = 8;
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

    public static class StaticEstadoDocumentoElectronico
    {
        public static readonly string Registrado = "registrado";
        public static readonly string Enviado = "enviado";
        public static readonly string Procesando = "procesando";
        public static readonly string Aceptado = "aceptado";
        public static readonly string Rechazado = "rechazado";
    };

    public static class StaticEstadoNotificacion
    {
        public static readonly string Pendiente = "pendiente";
        public static readonly string Rechazado = "rechazado";
    };

    public static class StaticTipoParametroContable
    {
        public static readonly string IngresosPorVentas = "IngresosPorVentas";
        public static readonly string DevolucionSobreVentas = "DevolucionSobreVentas";
        public static readonly string CostosDeVentas = "CostosDeVentas";
        public static readonly string IvaDevengado = "IvaDevengado";
        public static readonly string IvaSoportado = "IvaSoportado";
        public static readonly string LineaDeProductos = "LineaDeProductos";
        public static readonly string LineaDeServicios = "LineaDeServicios";
        public static readonly string CuentaDeBancos = "CuentaDeBancos";
        public static readonly string Efectivo = "Efectivo";
        public static readonly string OtraCondicionVenta = "OtraCondicionVenta";
        public static readonly string CuentasPorCobrarClientes = "CuentasPorCobrarClientes";
        public static readonly string CuentasPorCobrarTarjeta = "CuentasPorCobrarTarjeta";
        public static readonly string CuentasPorPagarProveedores = "CuentasPorPagarProveedores";
        public static readonly string CuentaDeIngresos = "CuentaDeIngresos";
        public static readonly string CuentaDeEgresos = "CuentaDeEgresos";
        public static readonly string NotaCreditoClientes = "NotaCreditoClientes";
        public static readonly string PerdidasyGanancias = "PerdidasyGanancias";
    };
}
