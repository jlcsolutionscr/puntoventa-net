-- phpMyAdmin SQL Dump
-- version 3.3.1
-- http://www.phpmyadmin.net
--
-- Servidor: 204.93.216.11:3306
-- Tiempo de generación: 28-09-2019 a las 14:45:10
-- Versión del servidor: 5.6.40
-- Versión de PHP: 5.2.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `jasoncr_puntoventa_dev`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ajusteinventario`
--

CREATE TABLE IF NOT EXISTS `ajusteinventario` (
  `IdEmpresa` int(11) NOT NULL,
  `IdAjuste` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(500) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdAjuste`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asiento`
--

CREATE TABLE IF NOT EXISTS `asiento` (
  `IdEmpresa` int(11) NOT NULL,
  `IdAsiento` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Fecha` datetime NOT NULL,
  `TotalDebito` double NOT NULL,
  `TotalCredito` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdAsiento`),
  KEY `IdEmpresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bancoadquiriente`
--

CREATE TABLE IF NOT EXISTS `bancoadquiriente` (
  `IdEmpresa` int(11) NOT NULL,
  `IdBanco` int(11) NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(30) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `PorcentajeRetencion` double NOT NULL,
  `PorcentajeComision` double NOT NULL,
  PRIMARY KEY (`IdBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `barrio`
--

CREATE TABLE IF NOT EXISTS `barrio` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `IdBarrio` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`,`IdBarrio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cantfemensualempresa`
--

CREATE TABLE IF NOT EXISTS `cantfemensualempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdMes` int(11) NOT NULL,
  `IdAnio` int(11) NOT NULL,
  `CantidadDoc` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdMes`,`IdAnio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `canton`
--

CREATE TABLE IF NOT EXISTS `canton` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `catalogocontable`
--

CREATE TABLE IF NOT EXISTS `catalogocontable` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Nivel_1` varchar(1) NOT NULL,
  `Nivel_2` varchar(2) DEFAULT NULL,
  `Nivel_3` varchar(2) DEFAULT NULL,
  `Nivel_4` varchar(2) DEFAULT NULL,
  `Nivel_5` varchar(2) DEFAULT NULL,
  `Nivel_6` varchar(2) DEFAULT NULL,
  `Nivel_7` varchar(2) DEFAULT NULL,
  `IdCuentaGrupo` int(11) DEFAULT NULL,
  `EsCuentaBalance` bit(1) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `IdTipoCuenta` int(11) NOT NULL,
  `IdClaseCuenta` int(11) NOT NULL,
  `PermiteMovimiento` bit(1) NOT NULL,
  `PermiteSobrejiro` bit(1) NOT NULL,
  `SaldoActual` double NOT NULL DEFAULT '0',
  `TotalDebito` double NOT NULL DEFAULT '0',
  `TotalCredito` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`IdCuenta`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdTipoCuenta` (`IdTipoCuenta`),
  KEY `IdClaseCuenta` (`IdClaseCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `catalogoreporte`
--

CREATE TABLE IF NOT EXISTS `catalogoreporte` (
  `IdReporte` int(11) NOT NULL,
  `NombreReporte` varchar(100) NOT NULL,
  PRIMARY KEY (`IdReporte`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cierrecaja`
--

CREATE TABLE IF NOT EXISTS `cierrecaja` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCierre` int(11) NOT NULL AUTO_INCREMENT,
  `FechaCierre` date DEFAULT NULL,
  `FondoInicio` double DEFAULT NULL,
  `VentasContado` double DEFAULT NULL,
  `VentasCredito` double DEFAULT NULL,
  `VentasTarjeta` double DEFAULT NULL,
  `OtrasVentas` double DEFAULT NULL,
  `RetencionIVA` double DEFAULT NULL,
  `ComisionVT` double DEFAULT NULL,
  `LiquidacionTarjeta` double DEFAULT NULL,
  `IngresoCxCEfectivo` double DEFAULT NULL,
  `IngresoCxCTarjeta` double DEFAULT NULL,
  `DevolucionesProveedores` double DEFAULT NULL,
  `OtrosIngresos` double DEFAULT NULL,
  `ComprasContado` double DEFAULT NULL,
  `ComprasCredito` double DEFAULT NULL,
  `OtrasCompras` double DEFAULT NULL,
  `EgresoCxPEfectivo` double DEFAULT NULL,
  `DevolucionesClientes` double DEFAULT NULL,
  `OtrosEgresos` double DEFAULT NULL,
  `FondoCierre` double DEFAULT NULL,
  PRIMARY KEY (`IdCierre`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `cierre_index` (`FechaCierre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clasecuentacontable`
--

CREATE TABLE IF NOT EXISTS `clasecuentacontable` (
  `IdClaseCuenta` int(11) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdClaseCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cliente`
--

CREATE TABLE IF NOT EXISTS `cliente` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipoIdentificacion` int(11) NOT NULL,
  `Identificacion` varchar(20) NOT NULL,
  `IdentificacionExtranjero` varchar(20) DEFAULT NULL,
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `IdBarrio` int(11) NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Nombre` varchar(80) NOT NULL,
  `NombreComercial` varchar(80) DEFAULT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `Celular` varchar(20) DEFAULT NULL,
  `Fax` varchar(20) DEFAULT NULL,
  `CorreoElectronico` varchar(200) DEFAULT NULL,
  `IdVendedor` int(11) DEFAULT NULL,
  `IdTipoPrecio` int(11) DEFAULT NULL,
  `ExoneradoDeImpuesto` int(11) NOT NULL,
  PRIMARY KEY (`IdCliente`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdTipoIdentificacion` (`IdTipoIdentificacion`),
  KEY `IdProvincia` (`IdProvincia`,`IdCanton`,`IdDistrito`,`IdBarrio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `compra`
--

CREATE TABLE IF NOT EXISTS `compra` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCompra` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdProveedor` int(11) NOT NULL,
  `IdCondicionVenta` int(11) NOT NULL,
  `PlazoCredito` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(20) DEFAULT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdCxP` int(11) NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `IdMovBanco` int(11) NOT NULL,
  `IdOrdenCompra` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdCompra`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdProveedor` (`IdProveedor`),
  KEY `IdCondicionVenta` (`IdCondicionVenta`),
  KEY `cxp_index` (`IdCxP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `condicionventa`
--

CREATE TABLE IF NOT EXISTS `condicionventa` (
  `IdCondicionVenta` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  PRIMARY KEY (`IdCondicionVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuentabanco`
--

CREATE TABLE IF NOT EXISTS `cuentabanco` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(50) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Saldo` double NOT NULL,
  PRIMARY KEY (`IdCuenta`),
  KEY `IdEmpresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuentaegreso`
--

CREATE TABLE IF NOT EXISTS `cuentaegreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuentaingreso`
--

CREATE TABLE IF NOT EXISTS `cuentaingreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuentaporcobrar`
--

CREATE TABLE IF NOT EXISTS `cuentaporcobrar` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCxC` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `Descripcion` varchar(255) NOT NULL,
  `Referencia` varchar(50) DEFAULT NULL,
  `NroDocOrig` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `Plazo` int(11) NOT NULL,
  `Tipo` smallint(6) NOT NULL,
  `Total` double NOT NULL,
  `Saldo` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdCxC`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cuentaporpagar`
--

CREATE TABLE IF NOT EXISTS `cuentaporpagar` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCxP` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `Descripcion` varchar(255) NOT NULL,
  `Referencia` varchar(50) DEFAULT NULL,
  `NroDocOrig` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Plazo` int(11) NOT NULL,
  `Tipo` smallint(6) NOT NULL,
  `Total` double NOT NULL,
  `Saldo` double NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdCxP`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosemovimientocuentaporcobrar`
--

CREATE TABLE IF NOT EXISTS `desglosemovimientocuentaporcobrar` (
  `IdMovCxC` int(11) NOT NULL,
  `IdCxC` int(11) NOT NULL,
  `Monto` double NOT NULL,
  PRIMARY KEY (`IdMovCxC`,`IdCxC`),
  KEY `IdCxC` (`IdCxC`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosemovimientocuentaporpagar`
--

CREATE TABLE IF NOT EXISTS `desglosemovimientocuentaporpagar` (
  `IdMovCxP` int(11) NOT NULL,
  `IdCxP` int(11) NOT NULL,
  `Monto` double NOT NULL,
  PRIMARY KEY (`IdMovCxP`,`IdCxP`),
  KEY `IdCxP` (`IdCxP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagocompra`
--

CREATE TABLE IF NOT EXISTS `desglosepagocompra` (
  `IdCompra` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdCompra`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  KEY `IdCuentaBanco` (`IdCuentaBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagodevolucioncliente`
--

CREATE TABLE IF NOT EXISTS `desglosepagodevolucioncliente` (
  `IdDevolucion` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  KEY `IdCuentaBanco` (`IdCuentaBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagodevolucionproveedor`
--

CREATE TABLE IF NOT EXISTS `desglosepagodevolucionproveedor` (
  `IdDevolucion` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  KEY `IdCuentaBanco` (`IdCuentaBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagoegreso`
--

CREATE TABLE IF NOT EXISTS `desglosepagoegreso` (
  `IdEgreso` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdEgreso`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  KEY `IdCuentaBanco` (`IdCuentaBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagofactura`
--

CREATE TABLE IF NOT EXISTS `desglosepagofactura` (
  `IdConsecutivo` int(11) NOT NULL AUTO_INCREMENT,
  `IdFactura` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdConsecutivo`),
  KEY `IdFactura` (`IdFactura`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagoingreso`
--

CREATE TABLE IF NOT EXISTS `desglosepagoingreso` (
  `IdIngreso` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdIngreso`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagomovimientocuentaporcobrar`
--

CREATE TABLE IF NOT EXISTS `desglosepagomovimientocuentaporcobrar` (
  `IdMovCxC` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdMovCxC`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `desglosepagomovimientocuentaporpagar`
--

CREATE TABLE IF NOT EXISTS `desglosepagomovimientocuentaporpagar` (
  `IdMovCxP` int(11) NOT NULL,
  `IdFormaPago` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `IdCuentaBanco` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `MontoForaneo` double NOT NULL,
  PRIMARY KEY (`IdMovCxP`,`IdFormaPago`,`IdTipoMoneda`,`IdCuentaBanco`),
  KEY `IdFormaPago` (`IdFormaPago`),
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  KEY `IdCuentaBanco` (`IdCuentaBanco`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleajusteinventario`
--

CREATE TABLE IF NOT EXISTS `detalleajusteinventario` (
  `IdAjuste` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  PRIMARY KEY (`IdAjuste`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleasiento`
--

CREATE TABLE IF NOT EXISTS `detalleasiento` (
  `IdAsiento` int(11) NOT NULL,
  `Linea` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `Debito` double NOT NULL,
  `Credito` double NOT NULL,
  `SaldoAnterior` double NOT NULL,
  PRIMARY KEY (`IdAsiento`,`Linea`),
  KEY `IdCuenta` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallecompra`
--

CREATE TABLE IF NOT EXISTS `detallecompra` (
  `IdCompra` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdCompra`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalledevolucioncliente`
--

CREATE TABLE IF NOT EXISTS `detalledevolucioncliente` (
  `IdDevolucion` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `CantDevolucion` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalledevolucionproveedor`
--

CREATE TABLE IF NOT EXISTS `detalledevolucionproveedor` (
  `IdDevolucion` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `CantDevolucion` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallefactura`
--

CREATE TABLE IF NOT EXISTS `detallefactura` (
  `IdFactura` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PrecioCosto` double NOT NULL,
  `CostoInstalacion` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdFactura`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleordencompra`
--

CREATE TABLE IF NOT EXISTS `detalleordencompra` (
  `IdOrdenCompra` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdOrdenCompra`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleordenservicio`
--

CREATE TABLE IF NOT EXISTS `detalleordenservicio` (
  `IdOrden` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `CostoInstalacion` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdOrden`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleproforma`
--

CREATE TABLE IF NOT EXISTS `detalleproforma` (
  `IdProforma` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdProforma`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalletraslado`
--

CREATE TABLE IF NOT EXISTS `detalletraslado` (
  `IdTraslado` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  PRIMARY KEY (`IdTraslado`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `devolucioncliente`
--

CREATE TABLE IF NOT EXISTS `devolucioncliente` (
  `IdEmpresa` int(11) NOT NULL,
  `IdDevolucion` int(11) NOT NULL AUTO_INCREMENT,
  `IdFactura` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdMovimientoCxC` int(11) NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `IdDocElectronico` varchar(50) DEFAULT NULL,
  `IdDocElectronicoRev` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdDevolucion`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdFactura` (`IdFactura`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdCliente` (`IdCliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `devolucionproveedor`
--

CREATE TABLE IF NOT EXISTS `devolucionproveedor` (
  `IdEmpresa` int(11) NOT NULL,
  `IdDevolucion` int(11) NOT NULL AUTO_INCREMENT,
  `IdCompra` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `IdProveedor` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdMovimientoCxP` int(11) NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdDevolucion`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCompra` (`IdCompra`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdProveedor` (`IdProveedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `distrito`
--

CREATE TABLE IF NOT EXISTS `distrito` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `documentoelectronico`
--

CREATE TABLE IF NOT EXISTS `documentoelectronico` (
  `IdDocumento` int(11) NOT NULL AUTO_INCREMENT,
  `IdEmpresa` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `ClaveNumerica` varchar(50) NOT NULL,
  `Consecutivo` varchar(20) DEFAULT NULL,
  `TipoIdentificacionEmisor` varchar(2) NOT NULL,
  `IdentificacionEmisor` varchar(12) NOT NULL,
  `TipoIdentificacionReceptor` varchar(2) NOT NULL,
  `IdentificacionReceptor` varchar(12) NOT NULL,
  `EsMensajeReceptor` varchar(1) NOT NULL,
  `DatosDocumento` blob NOT NULL,
  `Respuesta` blob,
  `EstadoEnvio` varchar(20) NOT NULL,
  `CorreoNotificacion` varchar(200) NOT NULL,
  `IdTipoDocumento` int(11) NOT NULL,
  `ErrorEnvio` varchar(500) DEFAULT NULL,
  `IdSucursal` int(11) NOT NULL,
  `IdTerminal` int(11) NOT NULL,
  `IdConsecutivo` int(11) NOT NULL,
  `DatosDocumentoOri` blob,
  PRIMARY KEY (`IdDocumento`),
  KEY `ClaveNumerica` (`ClaveNumerica`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `EstadoEnvio` (`EstadoEnvio`),
  KEY `ClaveNumerica_2` (`ClaveNumerica`,`Consecutivo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `egreso`
--

CREATE TABLE IF NOT EXISTS `egreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdEgreso` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int(11) DEFAULT NULL,
  `IdMovBanco` int(11) DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdEgreso`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCuenta` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empresa`
--

CREATE TABLE IF NOT EXISTS `empresa` (
  `IdEmpresa` int(11) NOT NULL AUTO_INCREMENT,
  `NombreEmpresa` varchar(80) NOT NULL,
  `UsuarioHacienda` varchar(100) DEFAULT NULL,
  `ClaveHacienda` varchar(100) DEFAULT NULL,
  `CorreoNotificacion` varchar(200) DEFAULT NULL,
  `PermiteFacturar` bit(1) NOT NULL,
  `AccessToken` blob,
  `ExpiresIn` int(11) DEFAULT NULL,
  `RefreshExpiresIn` int(11) DEFAULT NULL,
  `RefreshToken` blob,
  `EmitedAt` datetime DEFAULT NULL,
  `Logotipo` mediumblob,
  `NombreComercial` varchar(80) NOT NULL,
  `IdTipoIdentificacion` int(11) NOT NULL,
  `Identificacion` varchar(20) NOT NULL,
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `IdBarrio` int(11) NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `LineasPorFactura` int(11) NOT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `Contabiliza` bit(1) NOT NULL,
  `AutoCompletaProducto` bit(1) NOT NULL,
  `ModificaDescProducto` bit(1) NOT NULL,
  `DesglosaServicioInst` bit(1) NOT NULL,
  `PorcentajeInstalacion` double DEFAULT NULL,
  `CodigoServicioInst` int(11) DEFAULT NULL,
  `IncluyeInsumosEnFactura` bit(1) NOT NULL,
  `CierrePorTurnos` bit(1) NOT NULL,
  `CierreEnEjecucion` bit(1) NOT NULL,
  `RegimenSimplificado` bit(1) NOT NULL,
  `Certificado` blob,
  `NombreCertificado` varchar(100) DEFAULT NULL,
  `PinCertificado` varchar(4) DEFAULT NULL,
  `FechaVence` datetime DEFAULT NULL,
  `TipoContrato` int(11) NOT NULL,
  `CantidadDisponible` int(11) NOT NULL,
  `CodigoActividad` varchar(20) NOT NULL,
  PRIMARY KEY (`IdEmpresa`),
  KEY `Identificacion` (`Identificacion`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura`
--

CREATE TABLE IF NOT EXISTS `factura` (
  `IdEmpresa` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL,
  `IdTerminal` int(11) NOT NULL,
  `IdFactura` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdCondicionVenta` int(11) NOT NULL,
  `PlazoCredito` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `TextoAdicional` varchar(500) DEFAULT NULL,
  `IdVendedor` int(11) NOT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Descuento` double NOT NULL,
  `MontoPagado` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdCxC` int(11) NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `IdMovBanco` int(11) NOT NULL,
  `IdOrdenServicio` int(11) NOT NULL,
  `IdProforma` int(11) NOT NULL,
  `TotalCosto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `IdDocElectronico` varchar(50) DEFAULT NULL,
  `IdDocElectronicoRev` varchar(50) DEFAULT NULL,
  `IdTipoMoneda` int(11) NOT NULL,
  `TipoDeCambioDolar` decimal(13,5) NOT NULL,
  `IdTipoExoneracion` int(11) NOT NULL,
  `NumDocExoneracion` varchar(100) DEFAULT NULL,
  `NombreInstExoneracion` varchar(100) DEFAULT NULL,
  `FechaEmisionDoc` datetime NOT NULL,
  `PorcentajeExoneracion` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdFactura`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdCondicionVenta` (`IdCondicionVenta`),
  KEY `IdVendedor` (`IdVendedor`),
  KEY `cxc_index` (`IdCxC`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `formapago`
--

CREATE TABLE IF NOT EXISTS `formapago` (
  `IdFormaPago` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  PRIMARY KEY (`IdFormaPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ingreso`
--

CREATE TABLE IF NOT EXISTS `ingreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdIngreso` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `RecibidoDe` varchar(100) NOT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int(11) DEFAULT NULL,
  `IdMovBanco` int(11) DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdIngreso`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCuenta` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `linea`
--

CREATE TABLE IF NOT EXISTS `linea` (
  `IdEmpresa` int(11) NOT NULL,
  `IdLinea` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipoProducto` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdLinea`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdTipoProducto` (`IdTipoProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modulo`
--

CREATE TABLE IF NOT EXISTS `modulo` (
  `IdModulo` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `MenuPadre` varchar(100) NOT NULL,
  PRIMARY KEY (`IdModulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `moduloporempresa`
--

CREATE TABLE IF NOT EXISTS `moduloporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdModulo` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdModulo`),
  KEY `IdModulo` (`IdModulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientobanco`
--

CREATE TABLE IF NOT EXISTS `movimientobanco` (
  `IdMov` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `IdTipo` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `Numero` varchar(50) NOT NULL,
  `Beneficiario` varchar(200) NOT NULL,
  `SaldoAnterior` double NOT NULL,
  `Monto` double NOT NULL,
  `Descripcion` varchar(255) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdMov`),
  KEY `IdCuenta` (`IdCuenta`),
  KEY `IdTipo` (`IdTipo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientocuentaporcobrar`
--

CREATE TABLE IF NOT EXISTS `movimientocuentaporcobrar` (
  `IdEmpresa` int(11) NOT NULL,
  `IdMovCxC` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `Tipo` smallint(6) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `IdMovBanco` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdMovCxC`),
  KEY `IdUsuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientocuentaporpagar`
--

CREATE TABLE IF NOT EXISTS `movimientocuentaporpagar` (
  `IdEmpresa` int(11) NOT NULL,
  `IdMovCxP` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Tipo` smallint(6) NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `TipoPropietario` smallint(6) NOT NULL,
  `Recibo` varchar(50) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `IdMovBanco` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  PRIMARY KEY (`IdMovCxP`),
  KEY `IdUsuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientoproducto`
--

CREATE TABLE IF NOT EXISTS `movimientoproducto` (
  `IdProducto` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Cantidad` double NOT NULL,
  `Tipo` varchar(10) NOT NULL,
  `Origen` varchar(100) NOT NULL,
  `Referencia` varchar(100) NOT NULL,
  `PrecioCosto` double NOT NULL,
  PRIMARY KEY (`IdProducto`,`Fecha`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordencompra`
--

CREATE TABLE IF NOT EXISTS `ordencompra` (
  `IdEmpresa` int(11) NOT NULL,
  `IdOrdenCompra` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdProveedor` int(11) NOT NULL,
  `IdCondicionVenta` int(11) NOT NULL,
  `PlazoCredito` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(50) DEFAULT NULL,
  `TipoPago` smallint(6) NOT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  PRIMARY KEY (`IdOrdenCompra`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdProveedor` (`IdProveedor`),
  KEY `IdCondicionVenta` (`IdCondicionVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenservicio`
--

CREATE TABLE IF NOT EXISTS `ordenservicio` (
  `IdEmpresa` int(11) NOT NULL,
  `IdOrden` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdVendedor` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `Operarios` varchar(200) DEFAULT NULL,
  `HoraEntrada` varchar(15) DEFAULT NULL,
  `HoraSalida` varchar(15) DEFAULT NULL,
  `Marca` varchar(20) DEFAULT NULL,
  `Modelo` varchar(30) DEFAULT NULL,
  `Placa` varchar(10) DEFAULT NULL,
  `Color` varchar(20) DEFAULT NULL,
  `EstadoActual` varchar(500) DEFAULT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  PRIMARY KEY (`IdOrden`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdCliente` (`IdCliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `padron`
--

CREATE TABLE IF NOT EXISTS `padron` (
  `Identificacion` varchar(9) NOT NULL,
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `PrimerApellido` varchar(100) NOT NULL,
  `SegundoApellido` varchar(100) NOT NULL,
  PRIMARY KEY (`Identificacion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametrocontable`
--

CREATE TABLE IF NOT EXISTS `parametrocontable` (
  `IdParametro` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipo` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `IdProducto` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdParametro`),
  KEY `IdTipo` (`IdTipo`),
  KEY `IdCuenta` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametroexoneracion`
--

CREATE TABLE IF NOT EXISTS `parametroexoneracion` (
  `IdTipoExoneracion` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdTipoExoneracion`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=9 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametroimpuesto`
--

CREATE TABLE IF NOT EXISTS `parametroimpuesto` (
  `IdImpuesto` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) NOT NULL,
  `TasaImpuesto` double NOT NULL,
  PRIMARY KEY (`IdImpuesto`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=9 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametrosistema`
--

CREATE TABLE IF NOT EXISTS `parametrosistema` (
  `IdParametro` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `Valor` varchar(100) NOT NULL,
  PRIMARY KEY (`IdParametro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `producto`
--

CREATE TABLE IF NOT EXISTS `producto` (
  `IdEmpresa` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` int(11) NOT NULL,
  `IdLinea` int(11) NOT NULL,
  `Codigo` varchar(20) NOT NULL,
  `IdProveedor` int(11) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PrecioVenta1` double NOT NULL,
  `PrecioVenta2` double NOT NULL,
  `PrecioVenta3` double NOT NULL,
  `PrecioVenta4` double NOT NULL,
  `PrecioVenta5` double NOT NULL,
  `IndExistencia` double NOT NULL,
  `IdTipoUnidad` int(11) NOT NULL,
  `Imagen` longblob,
  `IdImpuesto` int(11) NOT NULL,
  PRIMARY KEY (`IdProducto`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdLinea` (`IdLinea`),
  KEY `IdProveedor` (`IdProveedor`),
  KEY `Tipo` (`Tipo`),
  KEY `IdTipoUnidad` (`IdTipoUnidad`),
  KEY `IdImpuesto` (`IdImpuesto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proforma`
--

CREATE TABLE IF NOT EXISTS `proforma` (
  `IdEmpresa` int(11) NOT NULL,
  `IdProforma` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdCondicionVenta` int(11) NOT NULL,
  `PlazoCredito` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(50) DEFAULT NULL,
  `IdVendedor` int(11) NOT NULL,
  `TipoPago` smallint(6) NOT NULL,
  `Excento` double NOT NULL,
  `Grabado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  PRIMARY KEY (`IdProforma`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdCondicionVenta` (`IdCondicionVenta`),
  KEY `IdVendedor` (`IdVendedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedor`
--

CREATE TABLE IF NOT EXISTS `proveedor` (
  `IdEmpresa` int(11) NOT NULL,
  `IdProveedor` int(11) NOT NULL AUTO_INCREMENT,
  `Identificacion` varchar(35) NOT NULL,
  `Nombre` varchar(200) DEFAULT NULL,
  `Direccion` varchar(255) DEFAULT NULL,
  `Telefono1` varchar(9) DEFAULT NULL,
  `Telefono2` varchar(9) DEFAULT NULL,
  `Fax` varchar(9) DEFAULT NULL,
  `Correo` varchar(100) DEFAULT NULL,
  `PlazoCredito` int(11) NOT NULL,
  `Contacto1` varchar(100) DEFAULT NULL,
  `TelCont1` varchar(9) DEFAULT NULL,
  `Contacto2` varchar(100) DEFAULT NULL,
  `TelCont2` varchar(9) DEFAULT NULL,
  PRIMARY KEY (`IdProveedor`),
  KEY `IdEmpresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `provincia`
--

CREATE TABLE IF NOT EXISTS `provincia` (
  `IdProvincia` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `registrorespuestahacienda`
--

CREATE TABLE IF NOT EXISTS `registrorespuestahacienda` (
  `IdRegistro` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `ClaveNumerica` varchar(50) NOT NULL,
  `Respuesta` blob,
  PRIMARY KEY (`IdRegistro`),
  KEY `idx_clavenumerica` (`ClaveNumerica`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reporteporempresa`
--

CREATE TABLE IF NOT EXISTS `reporteporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdReporte` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdReporte`),
  KEY `IdReporte` (`IdReporte`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `role`
--

CREATE TABLE IF NOT EXISTS `role` (
  `IdRole` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `MenuPadre` varchar(100) NOT NULL,
  `MenuItem` varchar(100) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdRole`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=128 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roleporusuario`
--

CREATE TABLE IF NOT EXISTS `roleporusuario` (
  `IdUsuario` int(11) NOT NULL,
  `IdRole` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdRole`),
  KEY `IdRole` (`IdRole`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `saldomensualcontable`
--

CREATE TABLE IF NOT EXISTS `saldomensualcontable` (
  `IdCuenta` int(11) NOT NULL,
  `Mes` int(2) NOT NULL,
  `Annio` int(4) NOT NULL,
  `SaldoFinMes` double NOT NULL,
  `TotalDebito` double NOT NULL,
  `TotalCredito` double NOT NULL,
  PRIMARY KEY (`IdCuenta`,`Mes`,`Annio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sucursal`
--

CREATE TABLE IF NOT EXISTS `sucursal` (
  `IdEmpresa` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(200) NOT NULL,
  `Direccion` varchar(500) NOT NULL,
  `Telefono` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdSucursal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `terminalporempresa`
--

CREATE TABLE IF NOT EXISTS `terminalporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL,
  `IdTerminal` int(11) NOT NULL,
  `ValorRegistro` varchar(100) NOT NULL,
  `ImpresoraFactura` varchar(50) DEFAULT NULL,
  `UltimoDocFE` int(11) NOT NULL,
  `UltimoDocND` int(11) NOT NULL,
  `UltimoDocNC` int(11) NOT NULL,
  `UltimoDocTE` int(11) NOT NULL,
  `UltimoDocMR` int(11) NOT NULL,
  `NombreSucursal` varchar(160) NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `IdTipoDispositivo` int(1) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdSucursal`,`IdTerminal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipocuentacontable`
--

CREATE TABLE IF NOT EXISTS `tipocuentacontable` (
  `IdTipoCuenta` int(11) NOT NULL,
  `TipoSaldo` varchar(1) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdTipoCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipodecambiodolar`
--

CREATE TABLE IF NOT EXISTS `tipodecambiodolar` (
  `FechaTipoCambio` varchar(10) NOT NULL DEFAULT '',
  `ValorTipoCambio` decimal(13,5) DEFAULT NULL,
  PRIMARY KEY (`FechaTipoCambio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoidentificacion`
--

CREATE TABLE IF NOT EXISTS `tipoidentificacion` (
  `IdTipoIdentificacion` int(11) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdTipoIdentificacion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipomoneda`
--

CREATE TABLE IF NOT EXISTS `tipomoneda` (
  `IdTipoMoneda` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(10) NOT NULL,
  PRIMARY KEY (`IdTipoMoneda`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipomovimientobanco`
--

CREATE TABLE IF NOT EXISTS `tipomovimientobanco` (
  `IdTipoMov` int(11) NOT NULL AUTO_INCREMENT,
  `DebeHaber` varchar(2) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdTipoMov`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=8 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoparametrocontable`
--

CREATE TABLE IF NOT EXISTS `tipoparametrocontable` (
  `IdTipo` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  `MultiCuenta` bit(1) NOT NULL,
  PRIMARY KEY (`IdTipo`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=19 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoproducto`
--

CREATE TABLE IF NOT EXISTS `tipoproducto` (
  `IdTipoProducto` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(10) NOT NULL,
  PRIMARY KEY (`IdTipoProducto`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipounidad`
--

CREATE TABLE IF NOT EXISTS `tipounidad` (
  `IdTipoUnidad` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(5) NOT NULL,
  PRIMARY KEY (`IdTipoUnidad`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `traslado`
--

CREATE TABLE IF NOT EXISTS `traslado` (
  `IdEmpresa` int(11) NOT NULL,
  `IdTraslado` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL,
  `Tipo` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(50) DEFAULT NULL,
  `Total` double NOT NULL,
  `IdAsiento` int(11) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdTraslado`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdSucursal` (`IdSucursal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE IF NOT EXISTS `usuario` (
  `IdUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `CodigoUsuario` varchar(10) NOT NULL,
  `Clave` varchar(1000) NOT NULL,
  `Modifica` tinyint(1) NOT NULL,
  `PermiteRegistrarDispositivo` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdUsuario`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarioporempresa`
--

CREATE TABLE IF NOT EXISTS `usuarioporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdEmpresa`),
  KEY `IdEmpresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vendedor`
--

CREATE TABLE IF NOT EXISTS `vendedor` (
  `IdEmpresa` int(11) NOT NULL,
  `IdVendedor` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  PRIMARY KEY (`IdVendedor`),
  KEY `IdEmpresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

--
-- Filtros para las tablas descargadas (dump)
--

--
-- Filtros para la tabla `ajusteinventario`
--
ALTER TABLE `ajusteinventario`
  ADD CONSTRAINT `ajusteinventario_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ajusteinventario_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `asiento`
--
ALTER TABLE `asiento`
  ADD CONSTRAINT `asiento_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `barrio`
--
ALTER TABLE `barrio`
  ADD CONSTRAINT `barrio_ibfk_1` FOREIGN KEY (`IdProvincia`, `IdCanton`, `IdDistrito`) REFERENCES `distrito` (`IdProvincia`, `IdCanton`, `IdDistrito`);

--
-- Filtros para la tabla `cantfemensualempresa`
--
ALTER TABLE `cantfemensualempresa`
  ADD CONSTRAINT `cantfemensualempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `canton`
--
ALTER TABLE `canton`
  ADD CONSTRAINT `canton_ibfk_1` FOREIGN KEY (`IdProvincia`) REFERENCES `provincia` (`IdProvincia`);

--
-- Filtros para la tabla `catalogocontable`
--
ALTER TABLE `catalogocontable`
  ADD CONSTRAINT `catalogocontable_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `catalogocontable_ibfk_2` FOREIGN KEY (`IdTipoCuenta`) REFERENCES `tipocuentacontable` (`IdTipoCuenta`),
  ADD CONSTRAINT `catalogocontable_ibfk_3` FOREIGN KEY (`IdClaseCuenta`) REFERENCES `clasecuentacontable` (`IdClaseCuenta`);

--
-- Filtros para la tabla `cierrecaja`
--
ALTER TABLE `cierrecaja`
  ADD CONSTRAINT `cierrecaja_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `cliente`
--
ALTER TABLE `cliente`
  ADD CONSTRAINT `cliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `cliente_ibfk_2` FOREIGN KEY (`IdTipoIdentificacion`) REFERENCES `tipoidentificacion` (`IdTipoIdentificacion`),
  ADD CONSTRAINT `cliente_ibfk_3` FOREIGN KEY (`IdProvincia`, `IdCanton`, `IdDistrito`, `IdBarrio`) REFERENCES `barrio` (`IdProvincia`, `IdCanton`, `IdDistrito`, `IdBarrio`);

--
-- Filtros para la tabla `compra`
--
ALTER TABLE `compra`
  ADD CONSTRAINT `compra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `compra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `compra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `compra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`);

--
-- Filtros para la tabla `cuentabanco`
--
ALTER TABLE `cuentabanco`
  ADD CONSTRAINT `cuentabanco_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `cuentaporcobrar`
--
ALTER TABLE `cuentaporcobrar`
  ADD CONSTRAINT `cuentaporcobrar_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `cuentaporcobrar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `cuentaporpagar`
--
ALTER TABLE `cuentaporpagar`
  ADD CONSTRAINT `cuentaporpagar_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `cuentaporpagar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `desglosemovimientocuentaporcobrar`
--
ALTER TABLE `desglosemovimientocuentaporcobrar`
  ADD CONSTRAINT `desglosemovimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdMovCxC`) REFERENCES `movimientocuentaporcobrar` (`IdMovCxC`),
  ADD CONSTRAINT `desglosemovimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdCxC`) REFERENCES `cuentaporcobrar` (`IdCxC`);

--
-- Filtros para la tabla `desglosemovimientocuentaporpagar`
--
ALTER TABLE `desglosemovimientocuentaporpagar`
  ADD CONSTRAINT `desglosemovimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdMovCxP`) REFERENCES `movimientocuentaporpagar` (`IdMovCxP`),
  ADD CONSTRAINT `desglosemovimientocuentaporpagar_ibfk_2` FOREIGN KEY (`IdCxP`) REFERENCES `cuentaporpagar` (`IdCxP`);

--
-- Filtros para la tabla `desglosepagocompra`
--
ALTER TABLE `desglosepagocompra`
  ADD CONSTRAINT `desglosepagocompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  ADD CONSTRAINT `desglosepagocompra_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagocompra_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  ADD CONSTRAINT `desglosepagocompra_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`);

--
-- Filtros para la tabla `desglosepagodevolucioncliente`
--
ALTER TABLE `desglosepagodevolucioncliente`
  ADD CONSTRAINT `desglosepagodevolucioncliente_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucioncliente` (`IdDevolucion`),
  ADD CONSTRAINT `desglosepagodevolucioncliente_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagodevolucioncliente_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  ADD CONSTRAINT `desglosepagodevolucioncliente_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`);

--
-- Filtros para la tabla `desglosepagodevolucionproveedor`
--
ALTER TABLE `desglosepagodevolucionproveedor`
  ADD CONSTRAINT `desglosepagodevolucionproveedor_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucionproveedor` (`IdDevolucion`),
  ADD CONSTRAINT `desglosepagodevolucionproveedor_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagodevolucionproveedor_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  ADD CONSTRAINT `desglosepagodevolucionproveedor_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`);

--
-- Filtros para la tabla `desglosepagoegreso`
--
ALTER TABLE `desglosepagoegreso`
  ADD CONSTRAINT `desglosepagoegreso_ibfk_1` FOREIGN KEY (`IdEgreso`) REFERENCES `egreso` (`IdEgreso`),
  ADD CONSTRAINT `desglosepagoegreso_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagoegreso_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  ADD CONSTRAINT `desglosepagoegreso_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`);

--
-- Filtros para la tabla `desglosepagofactura`
--
ALTER TABLE `desglosepagofactura`
  ADD CONSTRAINT `desglosepagofactura_ibfk_1` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  ADD CONSTRAINT `desglosepagofactura_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagofactura_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Filtros para la tabla `desglosepagoingreso`
--
ALTER TABLE `desglosepagoingreso`
  ADD CONSTRAINT `desglosepagoingreso_ibfk_1` FOREIGN KEY (`IdIngreso`) REFERENCES `ingreso` (`IdIngreso`),
  ADD CONSTRAINT `desglosepagoingreso_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagoingreso_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Filtros para la tabla `desglosepagomovimientocuentaporcobrar`
--
ALTER TABLE `desglosepagomovimientocuentaporcobrar`
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdMovCxC`) REFERENCES `movimientocuentaporcobrar` (`IdMovCxC`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Filtros para la tabla `desglosepagomovimientocuentaporpagar`
--
ALTER TABLE `desglosepagomovimientocuentaporpagar`
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdMovCxP`) REFERENCES `movimientocuentaporpagar` (`IdMovCxP`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`);

--
-- Filtros para la tabla `detalleajusteinventario`
--
ALTER TABLE `detalleajusteinventario`
  ADD CONSTRAINT `detalleajusteinventario_ibfk_1` FOREIGN KEY (`IdAjuste`) REFERENCES `ajusteinventario` (`IdAjuste`),
  ADD CONSTRAINT `detalleajusteinventario_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalleasiento`
--
ALTER TABLE `detalleasiento`
  ADD CONSTRAINT `detalleasiento_ibfk_1` FOREIGN KEY (`IdAsiento`) REFERENCES `asiento` (`IdAsiento`),
  ADD CONSTRAINT `detalleasiento_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Filtros para la tabla `detallecompra`
--
ALTER TABLE `detallecompra`
  ADD CONSTRAINT `detallecompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  ADD CONSTRAINT `detallecompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalledevolucioncliente`
--
ALTER TABLE `detalledevolucioncliente`
  ADD CONSTRAINT `detalledevolucioncliente_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucioncliente` (`IdDevolucion`),
  ADD CONSTRAINT `detalledevolucioncliente_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalledevolucionproveedor`
--
ALTER TABLE `detalledevolucionproveedor`
  ADD CONSTRAINT `detalledevolucionproveedor_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucionproveedor` (`IdDevolucion`),
  ADD CONSTRAINT `detalledevolucionproveedor_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detallefactura`
--
ALTER TABLE `detallefactura`
  ADD CONSTRAINT `detallefactura_ibfk_1` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  ADD CONSTRAINT `detallefactura_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalleordencompra`
--
ALTER TABLE `detalleordencompra`
  ADD CONSTRAINT `detalleordencompra_ibfk_1` FOREIGN KEY (`IdOrdenCompra`) REFERENCES `ordencompra` (`IdOrdenCompra`),
  ADD CONSTRAINT `detalleordencompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalleordenservicio`
--
ALTER TABLE `detalleordenservicio`
  ADD CONSTRAINT `detalleordenservicio_ibfk_1` FOREIGN KEY (`IdOrden`) REFERENCES `ordenservicio` (`IdOrden`),
  ADD CONSTRAINT `detalleordenservicio_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalleproforma`
--
ALTER TABLE `detalleproforma`
  ADD CONSTRAINT `detalleproforma_ibfk_1` FOREIGN KEY (`IdProforma`) REFERENCES `proforma` (`IdProforma`),
  ADD CONSTRAINT `detalleproforma_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `detalletraslado`
--
ALTER TABLE `detalletraslado`
  ADD CONSTRAINT `detalletraslado_ibfk_1` FOREIGN KEY (`IdTraslado`) REFERENCES `traslado` (`IdTraslado`),
  ADD CONSTRAINT `detalletraslado_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `devolucioncliente`
--
ALTER TABLE `devolucioncliente`
  ADD CONSTRAINT `devolucioncliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `devolucioncliente_ibfk_2` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  ADD CONSTRAINT `devolucioncliente_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `devolucioncliente_ibfk_4` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`);

--
-- Filtros para la tabla `devolucionproveedor`
--
ALTER TABLE `devolucionproveedor`
  ADD CONSTRAINT `devolucionproveedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `devolucionproveedor_ibfk_2` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  ADD CONSTRAINT `devolucionproveedor_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `devolucionproveedor_ibfk_4` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`);

--
-- Filtros para la tabla `distrito`
--
ALTER TABLE `distrito`
  ADD CONSTRAINT `distrito_ibfk_1` FOREIGN KEY (`IdProvincia`, `IdCanton`) REFERENCES `canton` (`IdProvincia`, `IdCanton`);

--
-- Filtros para la tabla `documentoelectronico`
--
ALTER TABLE `documentoelectronico`
  ADD CONSTRAINT `documentoelectronico_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `egreso`
--
ALTER TABLE `egreso`
  ADD CONSTRAINT `egreso_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `egreso_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentaegreso` (`IdCuenta`);

--
-- Filtros para la tabla `factura`
--
ALTER TABLE `factura`
  ADD CONSTRAINT `factura_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `factura_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `factura_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `factura_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`),
  ADD CONSTRAINT `factura_ibfk_5` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Filtros para la tabla `ingreso`
--
ALTER TABLE `ingreso`
  ADD CONSTRAINT `ingreso_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ingreso_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentaingreso` (`IdCuenta`);

--
-- Filtros para la tabla `linea`
--
ALTER TABLE `linea`
  ADD CONSTRAINT `linea_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `linea_ibfk_2` FOREIGN KEY (`IdTipoProducto`) REFERENCES `tipoproducto` (`IdTipoProducto`);

--
-- Filtros para la tabla `moduloporempresa`
--
ALTER TABLE `moduloporempresa`
  ADD CONSTRAINT `moduloporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `moduloporempresa_ibfk_2` FOREIGN KEY (`IdModulo`) REFERENCES `modulo` (`IdModulo`);

--
-- Filtros para la tabla `movimientobanco`
--
ALTER TABLE `movimientobanco`
  ADD CONSTRAINT `movimientobanco_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentabanco` (`IdCuenta`),
  ADD CONSTRAINT `movimientobanco_ibfk_2` FOREIGN KEY (`IdTipo`) REFERENCES `tipomovimientobanco` (`IdTipoMov`);

--
-- Filtros para la tabla `movimientocuentaporcobrar`
--
ALTER TABLE `movimientocuentaporcobrar`
  ADD CONSTRAINT `movimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `movimientocuentaporpagar`
--
ALTER TABLE `movimientocuentaporpagar`
  ADD CONSTRAINT `movimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `movimientoproducto`
--
ALTER TABLE `movimientoproducto`
  ADD CONSTRAINT `movimientoproducto_ibfk_1` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Filtros para la tabla `ordencompra`
--
ALTER TABLE `ordencompra`
  ADD CONSTRAINT `ordencompra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ordencompra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `ordencompra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `ordencompra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`);

--
-- Filtros para la tabla `ordenservicio`
--
ALTER TABLE `ordenservicio`
  ADD CONSTRAINT `ordenservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ordenservicio_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `ordenservicio_ibfk_3` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`);

--
-- Filtros para la tabla `parametrocontable`
--
ALTER TABLE `parametrocontable`
  ADD CONSTRAINT `parametrocontable_ibfk_1` FOREIGN KEY (`IdTipo`) REFERENCES `tipoparametrocontable` (`IdTipo`),
  ADD CONSTRAINT `parametrocontable_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Filtros para la tabla `producto`
--
ALTER TABLE `producto`
  ADD CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `producto_ibfk_2` FOREIGN KEY (`IdLinea`) REFERENCES `linea` (`IdLinea`),
  ADD CONSTRAINT `producto_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `producto_ibfk_4` FOREIGN KEY (`Tipo`) REFERENCES `tipoproducto` (`IdTipoProducto`),
  ADD CONSTRAINT `producto_ibfk_5` FOREIGN KEY (`IdTipoUnidad`) REFERENCES `tipounidad` (`IdTipoUnidad`),
  ADD CONSTRAINT `producto_ibfk_6` FOREIGN KEY (`IdImpuesto`) REFERENCES `parametroimpuesto` (`IdImpuesto`);

--
-- Filtros para la tabla `proforma`
--
ALTER TABLE `proforma`
  ADD CONSTRAINT `proforma_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `proforma_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `proforma_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `proforma_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`),
  ADD CONSTRAINT `proforma_ibfk_5` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Filtros para la tabla `proveedor`
--
ALTER TABLE `proveedor`
  ADD CONSTRAINT `proveedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `reporteporempresa`
--
ALTER TABLE `reporteporempresa`
  ADD CONSTRAINT `reporteporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `reporteporempresa_ibfk_2` FOREIGN KEY (`IdReporte`) REFERENCES `catalogoreporte` (`IdReporte`);

--
-- Filtros para la tabla `roleporusuario`
--
ALTER TABLE `roleporusuario`
  ADD CONSTRAINT `roleporusuario_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `roleporusuario_ibfk_2` FOREIGN KEY (`IdRole`) REFERENCES `role` (`IdRole`);

--
-- Filtros para la tabla `saldomensualcontable`
--
ALTER TABLE `saldomensualcontable`
  ADD CONSTRAINT `saldomensualcontable_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Filtros para la tabla `terminalporempresa`
--
ALTER TABLE `terminalporempresa`
  ADD CONSTRAINT `terminalporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Filtros para la tabla `traslado`
--
ALTER TABLE `traslado`
  ADD CONSTRAINT `traslado_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `traslado_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `traslado_ibfk_3` FOREIGN KEY (`IdSucursal`) REFERENCES `sucursal` (`IdSucursal`);

--
-- Filtros para la tabla `usuarioporempresa`
--
ALTER TABLE `usuarioporempresa`
  ADD CONSTRAINT `usuarioporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `usuarioporempresa_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Filtros para la tabla `vendedor`
--
ALTER TABLE `vendedor`
  ADD CONSTRAINT `vendedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);
