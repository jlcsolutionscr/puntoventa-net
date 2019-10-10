-- phpMyAdmin SQL Dump
-- version 3.3.1
-- http://www.phpmyadmin.net
--
-- Servidor: 204.93.216.11:3306
-- Tiempo de generación: 08-10-2019 a las 15:55:46
-- Versión del servidor: 5.6.40
-- Versión de PHP: 5.2.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: 'jasoncr_puntoventa'

--
-- Volcar la base de datos para la tabla 'catalogocontable'
--


--
-- Volcar la base de datos para la tabla 'catalogoreporte'
--

INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(1, 'Ventas en general');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(2, 'Ventas anuladas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(3, 'Ventas por vendedor');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(4, 'Compras en general');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(5, 'Compras anuladas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(6, 'Cuentas por cobrar a clientes');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(7, 'Cuentas por pagar a proveedores');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(8, 'Pagos a cuentas por cobrar de clientes');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(9, 'Pagos a cuentas por pagar de proveedores');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(10, 'Conciliación bancaria');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(11, 'Resumen de movimientos');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(12, 'Detalle de egresos');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(13, 'Detalle de ingresos');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(14, 'Reporte resumido de ventas por línea');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(15, 'Reporte detallado de ventas por línea');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(16, 'Facturas electrónicas emitidas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(17, 'Facturas electrónicas recibidas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(18, 'Notas de crédito electrónicas emitidas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(19, 'Notas de crédito electrónicas recibidas');
INSERT INTO catalogoreporte (IdReporte, NombreReporte) VALUES(20, 'Resumen de comprobantes electrónicos');

--
-- Volcar la base de datos para la tabla 'clasecuentacontable'
--

INSERT INTO clasecuentacontable (IdClaseCuenta, Descripcion) VALUES(1, 'Activo');
INSERT INTO clasecuentacontable (IdClaseCuenta, Descripcion) VALUES(2, 'Pasivo');
INSERT INTO clasecuentacontable (IdClaseCuenta, Descripcion) VALUES(3, 'Resultados');
INSERT INTO clasecuentacontable (IdClaseCuenta, Descripcion) VALUES(4, 'Patrimonio');

--
-- Volcar la base de datos para la tabla 'condicionventa'
--

INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(1, 'Contado');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(2, 'Crédito');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(3, 'Consignación');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(4, 'Apartado');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(5, 'Arrendamiento con opción de compra');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(6, 'Arrendamiento en función financiera');
INSERT INTO condicionventa (IdCondicionVenta, Descripcion) VALUES(99, 'Otros');

--
-- Volcar la base de datos para la tabla 'parametrocontable'
--


--
-- Volcar la base de datos para la tabla 'parametroexoneracion'
--

INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(1, 'Compras Autorizadas');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(2, 'Ventas exentas a diplomáticos');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(3, 'Autorizado por Ley Especial');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(4, 'Exenciones Direccion General de Hacienda');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(5, 'Transitorio V');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(6, 'Transitorio IX');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(7, 'Transitorio XVII');
INSERT INTO parametroexoneracion (IdTipoExoneracion, Descripcion) VALUES(8, 'Otros');

--
-- Volcar la base de datos para la tabla 'parametroimpuesto'
--

INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(1, 'Tarifa 0% (Exento)', 0);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(2, 'Tarifa Reducida 1%', 1);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(3, 'Tarifa Reducida 2%', 2);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(4, 'Tarifa Reducida 4%', 4);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(5, 'Transitorio 0%', 0);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(6, 'Transitorio 4%', 4);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(7, 'Transitorio 8%', 8);
INSERT INTO parametroimpuesto (IdImpuesto, Descripcion, TasaImpuesto) VALUES(8, 'Tarifa General 13%', 13);

--
-- Volcar la base de datos para la tabla 'parametrosistema'
--

INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(1, 'Version', '1.0.0.11');

--
-- Volcar la base de datos para la tabla 'role'
--

INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(1, 'Cierre Diario', 'mnuArchivo', 'mnuArchivoCierre', 'Permite ejecutar el cierre diario');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(2, 'Menu de Reportes', 'mnuArchivo', 'MnuArchivoReporte', 'Permite acceder el menu de reportes');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(4, 'Parametrización de Tipos de Moneda', 'MnuParam', 'MnuParamTM', 'Permite parametrizar los tipos de monedas para el desglose de pagos');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(5, 'Parametrización de Bancos Adquirientes', 'MnuParam', 'MnuParamBA', 'Permite parametrizar los bancos adquirientes');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(6, 'Respaldo de datos', 'mnuArchivo', 'mnuArchivoRespaldo', 'Permite ejecutar el respaldo en línea de la base de datos del sistema');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(50, 'Mantenimiento de Clientes', 'MnuMant', 'MnuMantCliente', 'Permite acceder al módulo de Mantenimiento de Clientes');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(52, 'Mantenimiento de Líneas', 'MnuMant', 'MnuMantLinea', 'Permite acceder al módulo de Mantenimiento de Líneas');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(53, 'Mantenimiento de Proveedores', 'MnuMant', 'MnuMantProveedor', 'Permite acceder al módulo de Mantenimiento de Proveedores');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(54, 'Mantenimiento de Productos', 'MnuMant', 'MnuMantProducto', 'Permite acceder al módulo de Mantenimiento de Productos');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(55, 'Mantenimiento de Usuarios', 'MnuMant', 'MnuMantUsuario', 'Permite acceder al módulo de Mantenimiento de Usuarios');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(57, 'Mantenimiento de Cuentas de Egreso', 'MnuMant', 'MnuMantCE', 'Permite acceder al módulo de Mantenimiento de Cuenta de Egresos');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(59, 'Mantenimiento de Cuentas Bancarias', 'MnuMant', 'MnuMantCB', 'Permite actualizar los saldos de las cuentas de bancos');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(61, 'Mantenimiento de Vendedores', 'MnuMant', 'MnuMantVend', 'Permite actualizar los datos de los vendedores de la empresa');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(62, 'Mantenimiento de empresa', 'MnuMant', 'ManuMantEmpresa', 'Permite dar mantenimiento a los datos de la empresa');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(101, 'Módulo de Facturación', 'MnuCaptura', 'MnuCapturaFactura', 'Permite registrar la Facturación');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(109, 'Módulo de Registro de Egresos', 'MnuCaptura', 'MnuCapturaEgreso', 'Permite registrar los movimientos de Egresos');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(125, 'Consulta documentos electrónicos pendientes', 'MnuDocElect', 'MnuDocElectCDE', 'Permite consultar documentos electrónicos pendientes de procesar');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(126, 'Recepción de facturas electrónicas', 'MnuDocElect', 'MnuDocElectADE', 'Permite registrar facturas electrónicas recibidas');
INSERT INTO role (IdRole, Nombre, MenuPadre, MenuItem, Descripcion) VALUES(127, 'Lista de documentos electrónicos procesados', 'MnuDocElect', 'MnuDocElectRDE', 'Permite revisar documentos electrónicos procesados');

--
-- Volcar la base de datos para la tabla 'tipocuentacontable'
--

INSERT INTO tipocuentacontable (IdTipoCuenta, TipoSaldo, Descripcion) VALUES(1, 'D', 'Deudor');
INSERT INTO tipocuentacontable (IdTipoCuenta, TipoSaldo, Descripcion) VALUES(2, 'C', 'Acreedor');

--
-- Volcar la base de datos para la tabla 'tipoidentificacion'
--

INSERT INTO tipoidentificacion (IdTipoIdentificacion, Descripcion) VALUES(0, 'Cédula Física');
INSERT INTO tipoidentificacion (IdTipoIdentificacion, Descripcion) VALUES(1, 'Cédula Jurídica');
INSERT INTO tipoidentificacion (IdTipoIdentificacion, Descripcion) VALUES(2, 'DIMEX');
INSERT INTO tipoidentificacion (IdTipoIdentificacion, Descripcion) VALUES(3, 'NITE');

--
-- Volcar la base de datos para la tabla 'tipomoneda'
--

INSERT INTO tipomoneda (IdTipoMoneda, Descripcion) VALUES(1, 'Colones');
INSERT INTO tipomoneda (IdTipoMoneda, Descripcion) VALUES(2, 'Dólares');

--
-- Volcar la base de datos para la tabla 'tipomovimientobanco'
--

INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(1, 'D', 'Cheque Saliente');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(2, 'C', 'Depósito Entrante');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(3, 'C', 'Inversión');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(4, 'D', 'Nota Débito');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(5, 'C', 'Nota Crédito');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(6, 'C', 'Cheque Entrante');
INSERT INTO tipomovimientobanco (IdTipoMov, DebeHaber, Descripcion) VALUES(7, 'D', 'Depósito Saliente');

--
-- Volcar la base de datos para la tabla 'tipoparametrocontable'
--

INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(1, 'Ingresos Ventas', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(2, 'Costos de Ventas', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(3, 'IVA Por Pagar', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(4, 'Líneas de Producto', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(5, 'Líneas de Servicios', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(6, 'Cuentas de Bancos', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(7, 'Efectivo', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(8, 'Otras Condiciones de venta', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(9, 'Cuentas por Cobrar Clientes', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(10, 'Cuentas por Cobrar Tarjeta', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(11, 'Gastos/Comisión Tarjeta', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(12, 'Cuentas por Pagar Proveedores', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(13, 'Cuentas por Cobrar Proveedores', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(14, 'Cuentas de Ingresos', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(15, 'Cuentas de Egresos', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(16, 'Traslados', '');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(17, 'Cuenta de Perdidas/Ganancias', '\0');
INSERT INTO tipoparametrocontable (IdTipo, Descripcion, MultiCuenta) VALUES(18, 'Cuenta por Pagar Particulares', '\0');

--
-- Volcar la base de datos para la tabla 'tipoproducto'
--

INSERT INTO tipoproducto (IdTipoProducto, Descripcion) VALUES(1, 'Producto');
INSERT INTO tipoproducto (IdTipoProducto, Descripcion) VALUES(2, 'Servicio');

--
-- Volcar la base de datos para la tabla 'tipounidad'
--

INSERT INTO tipounidad (IdTipoUnidad, Descripcion) VALUES(1, 'UND');
INSERT INTO tipounidad (IdTipoUnidad, Descripcion) VALUES(2, 'SP');

--
-- Volcar la base de datos para la tabla 'formapago'
--



INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES
(1, 'Efectivo');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(2, 'Tarjeta');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(3, 'Cheque');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(4, 'Transferencia - Dep. Bancario');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(5, 'Racaudado por terceros');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(99, 'Otros');
