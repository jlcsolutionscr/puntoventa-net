-- phpMyAdmin SQL Dump
-- version 3.3.1
-- http://www.phpmyadmin.net
--
-- Servidor: 204.93.216.11:3306
-- Tiempo de generación: 15-10-2019 a las 09:31:58
-- Versión del servidor: 5.6.40
-- Versión de PHP: 5.2.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `jasoncr_puntoventa`
--

--
-- Volcar la base de datos para la tabla `catalogocontable`
--


--
-- Volcar la base de datos para la tabla `catalogoreporte`
--

INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(1, 'Ventas en general');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(2, 'Ventas anuladas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(3, 'Ventas por vendedor');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(4, 'Compras en general');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(5, 'Compras anuladas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(6, 'Cuentas por cobrar a clientes');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(7, 'Cuentas por pagar a proveedores');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(8, 'Pagos a cuentas por cobrar de clientes');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(9, 'Pagos a cuentas por pagar de proveedores');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(10, 'Conciliación bancaria');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(11, 'Resumen de movimientos');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(12, 'Detalle de egresos');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(13, 'Detalle de ingresos');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(14, 'Reporte resumido de ventas por línea');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(15, 'Reporte detallado de ventas por línea');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(16, 'Facturas electrónicas emitidas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(17, 'Facturas electrónicas recibidas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(18, 'Notas de crédito electrónicas emitidas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(19, 'Notas de crédito electrónicas recibidas');
INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES(20, 'Resumen de comprobantes electrónicos');

--
-- Volcar la base de datos para la tabla `clasecuentacontable`
--

INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(1, 'Activo');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(2, 'Pasivo');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(3, 'Resultados');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(4, 'Patrimonio');

--
-- Volcar la base de datos para la tabla `condicionventa`
--

INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(1, 'Contado');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(2, 'Crédito');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(3, 'Consignación');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(4, 'Apartado');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(5, 'Arrendamiento con opción de compra');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(6, 'Arrendamiento en función financiera');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(99, 'Otros');

--
-- Volcar la base de datos para la tabla `formapago`
--

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(1, 'Efectivo');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(2, 'Tarjeta');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(3, 'Cheque');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(4, 'Transferencia - Dep. Bancario');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(5, 'Racaudado por terceros');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(99, 'Otros');

--
-- Volcar la base de datos para la tabla `parametrocontable`
--


--
-- Volcar la base de datos para la tabla `parametroexoneracion`
--

INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(1, 'Compras Autorizadas');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(2, 'Ventas exentas a diplomáticos');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(3, 'Autorizado por Ley Especial');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(4, 'Exenciones Direccion General de Hacienda');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(5, 'Transitorio V');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(6, 'Transitorio IX');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(7, 'Transitorio XVII');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(8, 'Otros');

--
-- Volcar la base de datos para la tabla `parametroimpuesto`
--

INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(1, 'Tarifa 0% (Exento)', 0);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(2, 'Tarifa Reducida 1%', 1);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(3, 'Tarifa Reducida 2%', 2);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(4, 'Tarifa Reducida 4%', 4);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(5, 'Transitorio 0%', 0);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(6, 'Transitorio 4%', 4);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(7, 'Transitorio 8%', 8);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(8, 'Tarifa General 13%', 13);

--
-- Volcar la base de datos para la tabla `parametrosistema`
--

INSERT INTO `parametrosistema` (`IdParametro`, `Descripcion`, `Valor`) VALUES(1, 'Version', '2.1.0.0');
INSERT INTO `parametrosistema` (`IdParametro`, `Descripcion`, `Valor`) VALUES(2, 'Procesando', 'NO');

--
-- Volcar la base de datos para la tabla `role`
--

INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(1, 'Cierre Diario', 'mnuArchivo', 'mnuArchivoCierre', 'Permite ejecutar el cierre diario');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(2, 'Menu de Reportes', 'mnuArchivo', 'MnuArchivoReporte', 'Permite acceder el menu de reportes');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(4, 'Parametrización de Tipos de Moneda', 'MnuParam', 'MnuParamTM', 'Permite parametrizar los tipos de monedas para el desglose de pagos');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(5, 'Parametrización de Bancos Adquirientes', 'MnuParam', 'MnuParamBA', 'Permite parametrizar los bancos adquirientes');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(6, 'Respaldo de datos', 'mnuArchivo', 'mnuArchivoRespaldo', 'Permite ejecutar el respaldo en línea de la base de datos del sistema');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(50, 'Mantenimiento de Clientes', 'MnuMant', 'MnuMantCliente', 'Permite acceder al módulo de Mantenimiento de Clientes');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(52, 'Mantenimiento de Líneas', 'MnuMant', 'MnuMantLinea', 'Permite acceder al módulo de Mantenimiento de Líneas');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(53, 'Mantenimiento de Proveedores', 'MnuMant', 'MnuMantProveedor', 'Permite acceder al módulo de Mantenimiento de Proveedores');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(54, 'Mantenimiento de Productos', 'MnuMant', 'MnuMantProducto', 'Permite acceder al módulo de Mantenimiento de Productos');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(55, 'Mantenimiento de Usuarios', 'MnuMant', 'MnuMantUsuario', 'Permite acceder al módulo de Mantenimiento de Usuarios');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(57, 'Mantenimiento de Cuentas de Egreso', 'MnuMant', 'MnuMantCE', 'Permite acceder al módulo de Mantenimiento de Cuenta de Egresos');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(59, 'Mantenimiento de Cuentas Bancarias', 'MnuMant', 'MnuMantCB', 'Permite actualizar los saldos de las cuentas de bancos');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(61, 'Mantenimiento de Vendedores', 'MnuMant', 'MnuMantVend', 'Permite actualizar los datos de los vendedores de la empresa');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(62, 'Mantenimiento de empresa', 'MnuMant', 'ManuMantEmpresa', 'Permite dar mantenimiento a los datos de la empresa');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(101, 'Módulo de Facturación', 'MnuCaptura', 'MnuCapturaFactura', 'Permite registrar la Facturación');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(109, 'Módulo de Registro de Egresos', 'MnuCaptura', 'MnuCapturaEgreso', 'Permite registrar los movimientos de Egresos');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(125, 'Consulta documentos electrónicos pendientes', 'MnuDocElect', 'MnuDocElectCDE', 'Permite consultar documentos electrónicos pendientes de procesar');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(126, 'Recepción de facturas electrónicas', 'MnuDocElect', 'MnuDocElectADE', 'Permite registrar facturas electrónicas recibidas');
INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES(127, 'Lista de documentos electrónicos procesados', 'MnuDocElect', 'MnuDocElectRDE', 'Permite revisar documentos electrónicos procesados');

--
-- Volcar la base de datos para la tabla `tipocuentacontable`
--

INSERT INTO `tipocuentacontable` (`IdTipoCuenta`, `TipoSaldo`, `Descripcion`) VALUES(1, 'D', 'Deudor');
INSERT INTO `tipocuentacontable` (`IdTipoCuenta`, `TipoSaldo`, `Descripcion`) VALUES(2, 'C', 'Acreedor');

--
-- Volcar la base de datos para la tabla `tipodecambiodolar`
--

INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/02/2019', 614.32000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/03/2019', 609.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/04/2019', 602.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/05/2019', 598.90000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/06/2019', 591.03000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/07/2019', 583.64000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/08/2019', 572.26000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/09/2019', 575.16000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('01/10/2019', 583.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/02/2019', 615.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/03/2019', 609.32000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/04/2019', 602.74000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/05/2019', 598.90000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/06/2019', 591.03000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/07/2019', 583.23000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/08/2019', 573.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/09/2019', 575.16000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('02/10/2019', 583.23000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/02/2019', 615.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/03/2019', 609.32000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/04/2019', 605.49000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/05/2019', 599.48000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/06/2019', 591.03000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/07/2019', 583.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/08/2019', 573.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/09/2019', 577.15000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('03/10/2019', 585.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/02/2019', 615.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/03/2019', 609.32000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/04/2019', 608.93000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/05/2019', 598.34000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/06/2019', 591.07000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/07/2019', 585.17000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/08/2019', 573.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/09/2019', 579.59000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('04/10/2019', 585.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/01/2019', 611.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/02/2019', 616.40000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/03/2019', 608.50000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/04/2019', 609.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/05/2019', 598.34000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/06/2019', 593.65000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/07/2019', 585.83000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/08/2019', 573.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/09/2019', 580.82000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('05/10/2019', 585.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/02/2019', 616.45000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/03/2019', 610.37000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/04/2019', 608.58000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/05/2019', 598.34000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/06/2019', 594.50000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/07/2019', 585.77000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/08/2019', 573.27000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/09/2019', 582.67000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('06/10/2019', 585.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/02/2019', 615.35000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/03/2019', 611.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/04/2019', 608.58000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/05/2019', 598.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/06/2019', 596.50000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/07/2019', 585.77000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/08/2019', 573.18000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/09/2019', 581.13000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('07/10/2019', 585.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/02/2019', 615.12000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/03/2019', 611.39000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/04/2019', 608.58000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/05/2019', 599.10000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/06/2019', 595.40000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/07/2019', 585.77000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/08/2019', 573.32000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/09/2019', 581.13000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('08/10/2019', 585.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/01/2019', 612.21000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/02/2019', 614.96000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/03/2019', 610.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/04/2019', 607.96000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/05/2019', 597.82000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/06/2019', 595.40000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/07/2019', 585.53000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/08/2019', 573.53000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/09/2019', 581.13000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('09/10/2019', 584.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/01/2019', 612.12000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/02/2019', 614.96000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/03/2019', 610.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/04/2019', 605.30000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/05/2019', 598.45000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/06/2019', 595.40000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/07/2019', 585.50000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/08/2019', 574.07000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/09/2019', 579.79000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('10/10/2019', 583.98000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/01/2019', 610.49000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/02/2019', 614.96000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/03/2019', 610.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/04/2019', 603.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/05/2019', 597.68000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/06/2019', 592.68000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/07/2019', 584.18000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/08/2019', 574.07000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/09/2019', 577.96000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('11/10/2019', 583.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/01/2019', 608.80000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/02/2019', 615.06000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/03/2019', 608.21000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/04/2019', 603.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/05/2019', 597.68000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/06/2019', 591.39000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/07/2019', 584.05000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/08/2019', 574.07000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/09/2019', 577.28000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('12/10/2019', 583.57000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/02/2019', 614.08000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/03/2019', 607.25000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/04/2019', 602.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/05/2019', 597.68000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/06/2019', 589.59000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/07/2019', 582.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/08/2019', 573.62000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/09/2019', 578.83000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('13/10/2019', 583.57000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/01/2019', 608.80000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/02/2019', 612.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/03/2019', 604.75000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/04/2019', 602.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/05/2019', 596.50000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/06/2019', 588.52000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/07/2019', 582.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/08/2019', 572.43000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/09/2019', 579.66000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('14/10/2019', 583.57000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/01/2019', 606.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/02/2019', 612.42000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/03/2019', 601.76000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/04/2019', 602.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/05/2019', 594.57000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/06/2019', 588.89000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/07/2019', 582.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/08/2019', 571.30000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/09/2019', 579.66000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('15/10/2019', 581.85000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/01/2019', 605.08000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/02/2019', 611.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/03/2019', 600.62000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/04/2019', 600.73000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/05/2019', 592.66000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/06/2019', 588.89000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/07/2019', 578.39000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/08/2019', 571.30000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('16/09/2019', 579.66000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/01/2019', 602.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/02/2019', 611.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/03/2019', 600.62000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/04/2019', 598.79000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/05/2019', 591.45000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/06/2019', 588.89000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/07/2019', 577.89000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/08/2019', 570.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('17/09/2019', 580.49000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/01/2019', 603.52000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/02/2019', 611.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/03/2019', 600.62000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/05/2019', 590.91000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/06/2019', 587.86000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/07/2019', 578.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/08/2019', 570.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('18/09/2019', 580.77000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/01/2019', 604.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/02/2019', 611.20000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/03/2019', 598.73000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/05/2019', 590.91000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/06/2019', 587.43000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/07/2019', 578.78000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/08/2019', 570.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('19/09/2019', 582.00000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/01/2019', 604.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/02/2019', 612.62000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/03/2019', 600.39000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/05/2019', 590.91000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/06/2019', 589.16000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/07/2019', 577.37000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/08/2019', 569.92000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('20/09/2019', 583.55000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/01/2019', 604.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/02/2019', 612.00000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/03/2019', 603.16000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/05/2019', 591.28000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/06/2019', 587.95000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/07/2019', 577.37000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/08/2019', 568.07000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('21/09/2019', 583.86000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/01/2019', 605.15000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/02/2019', 611.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/03/2019', 605.23000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/05/2019', 591.09000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/06/2019', 588.06000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/07/2019', 577.37000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/08/2019', 568.45000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('22/09/2019', 583.86000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/01/2019', 606.56000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/02/2019', 611.12000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/03/2019', 612.61000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/04/2019', 601.24000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/05/2019', 593.71000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/06/2019', 588.06000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/07/2019', 576.82000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/08/2019', 569.53000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('23/09/2019', 583.86000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/01/2019', 608.11000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/02/2019', 611.12000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/03/2019', 612.61000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/04/2019', 603.51000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/05/2019', 594.03000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/06/2019', 588.06000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/07/2019', 575.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/08/2019', 570.20000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('24/09/2019', 585.06000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/01/2019', 610.70000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/02/2019', 611.12000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/03/2019', 612.61000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/04/2019', 605.24000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/05/2019', 594.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/06/2019', 587.81000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/07/2019', 575.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/08/2019', 570.20000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('25/09/2019', 585.84000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/01/2019', 612.60000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/02/2019', 612.21000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/03/2019', 610.54000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/04/2019', 603.05000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/05/2019', 594.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/06/2019', 587.13000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/07/2019', 575.69000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/08/2019', 570.20000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('26/09/2019', 585.43000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/01/2019', 612.60000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/02/2019', 611.66000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/03/2019', 608.14000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/04/2019', 599.75000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/05/2019', 594.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/06/2019', 586.31000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/07/2019', 574.94000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/08/2019', 570.17000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('27/09/2019', 584.74000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/01/2019', 612.60000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/02/2019', 610.72000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/03/2019', 606.75000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/04/2019', 599.75000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/05/2019', 594.51000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/06/2019', 585.45000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/07/2019', 574.94000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/08/2019', 570.03000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('28/09/2019', 583.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/01/2019', 613.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/03/2019', 603.85000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/04/2019', 598.63000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/05/2019', 592.86000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/06/2019', 583.64000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/07/2019', 574.94000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/08/2019', 570.99000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('29/09/2019', 583.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/01/2019', 614.95000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/03/2019', 602.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/04/2019', 599.09000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/05/2019', 591.24000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/06/2019', 583.64000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/07/2019', 573.97000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/08/2019', 572.89000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('30/09/2019', 583.88000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('31/01/2019', 614.17000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('31/03/2019', 602.36000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('31/05/2019', 590.54000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('31/07/2019', 573.46000);
INSERT INTO `tipodecambiodolar` (`FechaTipoCambio`, `ValorTipoCambio`) VALUES('31/08/2019', 575.16000);

--
-- Volcar la base de datos para la tabla `tipoidentificacion`
--

INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(0, 'Cédula Física');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(1, 'Cédula Jurídica');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(2, 'DIMEX');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(3, 'NITE');

--
-- Volcar la base de datos para la tabla `tipomoneda`
--

INSERT INTO `tipomoneda` (`IdTipoMoneda`, `Descripcion`) VALUES(1, 'Colones');
INSERT INTO `tipomoneda` (`IdTipoMoneda`, `Descripcion`) VALUES(2, 'Dólares');

--
-- Volcar la base de datos para la tabla `tipomovimientobanco`
--

INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(1, 'D', 'Cheque Saliente');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(2, 'C', 'Depósito Entrante');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(3, 'C', 'Inversión');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(4, 'D', 'Nota Débito');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(5, 'C', 'Nota Crédito');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(6, 'C', 'Cheque Entrante');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(7, 'D', 'Depósito Saliente');

--
-- Volcar la base de datos para la tabla `tipoparametrocontable`
--

INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(1, 'Ingresos Ventas', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(2, 'Costos de Ventas', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(3, 'IVA Por Pagar', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(4, 'Líneas de Producto', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(5, 'Líneas de Servicios', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(6, 'Cuentas de Bancos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(7, 'Efectivo', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(8, 'Otras Condiciones de venta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(9, 'Cuentas por Cobrar Clientes', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(10, 'Cuentas por Cobrar Tarjeta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(11, 'Gastos/Comisión Tarjeta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(12, 'Cuentas por Pagar Proveedores', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(13, 'Cuentas por Cobrar Proveedores', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(14, 'Cuentas de Ingresos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(15, 'Cuentas de Egresos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(16, 'Traslados', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(17, 'Cuenta de Perdidas/Ganancias', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(18, 'Cuenta por Pagar Particulares', '\0');

--
-- Volcar la base de datos para la tabla `tipoproducto`
--

INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(1, 'Producto');
INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(2, 'Servicio');

--
-- Volcar la base de datos para la tabla `tipounidad`
--

INSERT INTO `tipounidad` (`IdTipoUnidad`, `Descripcion`) VALUES(1, 'UND');
INSERT INTO `tipounidad` (`IdTipoUnidad`, `Descripcion`) VALUES(2, 'SP');
