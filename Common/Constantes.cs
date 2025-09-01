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
        public static readonly int ImpuestodeServicio = 5;
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
        public static readonly int SinpeMovil = 6;
        public static readonly int PlataformaDigital = 7;
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

    public static class StaticTipoParametroContable
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
}
