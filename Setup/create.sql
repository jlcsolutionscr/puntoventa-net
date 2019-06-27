-- MySQL dump 10.13  Distrib 5.7.23, for Win32 (AMD64)
--
-- Host: localhost    Database: puntoventa
-- ------------------------------------------------------
-- Server version	5.7.23-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ajusteinventario`
--

DROP TABLE IF EXISTS `ajusteinventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ajusteinventario` (
  `IdEmpresa` int(11) NOT NULL,
  `IdAjuste` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(500) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdAjuste`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `ajusteinventario_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `ajusteinventario_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `asiento`
--

DROP TABLE IF EXISTS `asiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `asiento` (
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
  KEY `IdEmpresa` (`IdEmpresa`),
  CONSTRAINT `asiento_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `bancoadquiriente`
--

DROP TABLE IF EXISTS `bancoadquiriente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bancoadquiriente` (
  `IdEmpresa` int(11) NOT NULL,
  `IdBanco` int(11) NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(30) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `PorcentajeRetencion` double NOT NULL,
  `PorcentajeComision` double NOT NULL,
  PRIMARY KEY (`IdBanco`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `barrio`
--

DROP TABLE IF EXISTS `barrio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `barrio` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `IdBarrio` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`,`IdBarrio`),
  CONSTRAINT `barrio_ibfk_1` FOREIGN KEY (`IdProvincia`, `IdCanton`, `IdDistrito`) REFERENCES `distrito` (`IdProvincia`, `IdCanton`, `IdDistrito`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cantfemensualempresa`
--

DROP TABLE IF EXISTS `cantfemensualempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cantfemensualempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdMes` int(11) NOT NULL,
  `IdAnio` int(11) NOT NULL,
  `CantidadDoc` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdMes`,`IdAnio`),
  CONSTRAINT `cantfemensualempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `canton`
--

DROP TABLE IF EXISTS `canton`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `canton` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`),
  CONSTRAINT `canton_ibfk_1` FOREIGN KEY (`IdProvincia`) REFERENCES `provincia` (`IdProvincia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `catalogocontable`
--

DROP TABLE IF EXISTS `catalogocontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `catalogocontable` (
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
  KEY `IdClaseCuenta` (`IdClaseCuenta`),
  CONSTRAINT `catalogocontable_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `catalogocontable_ibfk_2` FOREIGN KEY (`IdTipoCuenta`) REFERENCES `tipocuentacontable` (`IdTipoCuenta`),
  CONSTRAINT `catalogocontable_ibfk_3` FOREIGN KEY (`IdClaseCuenta`) REFERENCES `clasecuentacontable` (`IdClaseCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `catalogoreporte`
--

DROP TABLE IF EXISTS `catalogoreporte`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `catalogoreporte` (
  `IdReporte` int(11) NOT NULL,
  `NombreReporte` varchar(100) NOT NULL,
  PRIMARY KEY (`IdReporte`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cierrecaja`
--

DROP TABLE IF EXISTS `cierrecaja`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cierrecaja` (
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
  KEY `cierre_index` (`FechaCierre`),
  CONSTRAINT `cierrecaja_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `clasecuentacontable`
--

DROP TABLE IF EXISTS `clasecuentacontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `clasecuentacontable` (
  `IdClaseCuenta` int(11) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdClaseCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cliente` (
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
  `ExoneradoDeImpuesto` bit(1) NOT NULL,
  PRIMARY KEY (`IdCliente`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdTipoIdentificacion` (`IdTipoIdentificacion`),
  KEY `IdProvincia` (`IdProvincia`,`IdCanton`,`IdDistrito`,`IdBarrio`),
  CONSTRAINT `cliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `cliente_ibfk_2` FOREIGN KEY (`IdTipoIdentificacion`) REFERENCES `tipoidentificacion` (`IdTipoIdentificacion`),
  CONSTRAINT `cliente_ibfk_3` FOREIGN KEY (`IdProvincia`, `IdCanton`, `IdDistrito`, `IdBarrio`) REFERENCES `barrio` (`IdProvincia`, `IdCanton`, `IdDistrito`, `IdBarrio`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `compra`
--

DROP TABLE IF EXISTS `compra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `compra` (
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
  KEY `cxp_index` (`IdCxP`),
  CONSTRAINT `compra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `compra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `compra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  CONSTRAINT `compra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `condicionventa`
--

DROP TABLE IF EXISTS `condicionventa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `condicionventa` (
  `IdCondicionVenta` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  PRIMARY KEY (`IdCondicionVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cuentabanco`
--

DROP TABLE IF EXISTS `cuentabanco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuentabanco` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(50) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Saldo` double NOT NULL,
  PRIMARY KEY (`IdCuenta`),
  KEY `IdEmpresa` (`IdEmpresa`),
  CONSTRAINT `cuentabanco_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cuentaegreso`
--

DROP TABLE IF EXISTS `cuentaegreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuentaegreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdCuenta`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cuentaingreso`
--

DROP TABLE IF EXISTS `cuentaingreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuentaingreso` (
  `IdEmpresa` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cuentaporcobrar`
--

DROP TABLE IF EXISTS `cuentaporcobrar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuentaporcobrar` (
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
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `cuentaporcobrar_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `cuentaporcobrar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cuentaporpagar`
--

DROP TABLE IF EXISTS `cuentaporpagar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cuentaporpagar` (
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
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `cuentaporpagar_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `cuentaporpagar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosemovimientocuentaporcobrar`
--

DROP TABLE IF EXISTS `desglosemovimientocuentaporcobrar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosemovimientocuentaporcobrar` (
  `IdMovCxC` int(11) NOT NULL,
  `IdCxC` int(11) NOT NULL,
  `Monto` double NOT NULL,
  PRIMARY KEY (`IdMovCxC`,`IdCxC`),
  KEY `IdCxC` (`IdCxC`),
  CONSTRAINT `desglosemovimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdMovCxC`) REFERENCES `movimientocuentaporcobrar` (`IdMovCxC`),
  CONSTRAINT `desglosemovimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdCxC`) REFERENCES `cuentaporcobrar` (`IdCxC`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosemovimientocuentaporpagar`
--

DROP TABLE IF EXISTS `desglosemovimientocuentaporpagar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosemovimientocuentaporpagar` (
  `IdMovCxP` int(11) NOT NULL,
  `IdCxP` int(11) NOT NULL,
  `Monto` double NOT NULL,
  PRIMARY KEY (`IdMovCxP`,`IdCxP`),
  KEY `IdCxP` (`IdCxP`),
  CONSTRAINT `desglosemovimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdMovCxP`) REFERENCES `movimientocuentaporpagar` (`IdMovCxP`),
  CONSTRAINT `desglosemovimientocuentaporpagar_ibfk_2` FOREIGN KEY (`IdCxP`) REFERENCES `cuentaporpagar` (`IdCxP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagocompra`
--

DROP TABLE IF EXISTS `desglosepagocompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagocompra` (
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
  KEY `IdCuentaBanco` (`IdCuentaBanco`),
  CONSTRAINT `desglosepagocompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  CONSTRAINT `desglosepagocompra_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagocompra_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagocompra_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagodevolucioncliente`
--

DROP TABLE IF EXISTS `desglosepagodevolucioncliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagodevolucioncliente` (
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
  KEY `IdCuentaBanco` (`IdCuentaBanco`),
  CONSTRAINT `desglosepagodevolucioncliente_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucioncliente` (`IdDevolucion`),
  CONSTRAINT `desglosepagodevolucioncliente_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagodevolucioncliente_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagodevolucioncliente_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagodevolucionproveedor`
--

DROP TABLE IF EXISTS `desglosepagodevolucionproveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagodevolucionproveedor` (
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
  KEY `IdCuentaBanco` (`IdCuentaBanco`),
  CONSTRAINT `desglosepagodevolucionproveedor_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucionproveedor` (`IdDevolucion`),
  CONSTRAINT `desglosepagodevolucionproveedor_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagodevolucionproveedor_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagodevolucionproveedor_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagoegreso`
--

DROP TABLE IF EXISTS `desglosepagoegreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagoegreso` (
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
  KEY `IdCuentaBanco` (`IdCuentaBanco`),
  CONSTRAINT `desglosepagoegreso_ibfk_1` FOREIGN KEY (`IdEgreso`) REFERENCES `egreso` (`IdEgreso`),
  CONSTRAINT `desglosepagoegreso_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagoegreso_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagoegreso_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagofactura`
--

DROP TABLE IF EXISTS `desglosepagofactura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagofactura` (
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
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagofactura_ibfk_1` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  CONSTRAINT `desglosepagofactura_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagofactura_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB AUTO_INCREMENT=355 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagoingreso`
--

DROP TABLE IF EXISTS `desglosepagoingreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagoingreso` (
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
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagoingreso_ibfk_1` FOREIGN KEY (`IdIngreso`) REFERENCES `ingreso` (`IdIngreso`),
  CONSTRAINT `desglosepagoingreso_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagoingreso_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagomovimientocuentaporcobrar`
--

DROP TABLE IF EXISTS `desglosepagomovimientocuentaporcobrar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagomovimientocuentaporcobrar` (
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
  KEY `IdTipoMoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdMovCxC`) REFERENCES `movimientocuentaporcobrar` (`IdMovCxC`),
  CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `desglosepagomovimientocuentaporpagar`
--

DROP TABLE IF EXISTS `desglosepagomovimientocuentaporpagar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `desglosepagomovimientocuentaporpagar` (
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
  KEY `IdCuentaBanco` (`IdCuentaBanco`),
  CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdMovCxP`) REFERENCES `movimientocuentaporpagar` (`IdMovCxP`),
  CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`),
  CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_4` FOREIGN KEY (`IdCuentaBanco`) REFERENCES `cuentabanco` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalleajusteinventario`
--

DROP TABLE IF EXISTS `detalleajusteinventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalleajusteinventario` (
  `IdAjuste` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  PRIMARY KEY (`IdAjuste`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalleajusteinventario_ibfk_1` FOREIGN KEY (`IdAjuste`) REFERENCES `ajusteinventario` (`IdAjuste`),
  CONSTRAINT `detalleajusteinventario_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalleasiento`
--

DROP TABLE IF EXISTS `detalleasiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalleasiento` (
  `IdAsiento` int(11) NOT NULL,
  `Linea` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `Debito` double NOT NULL,
  `Credito` double NOT NULL,
  `SaldoAnterior` double NOT NULL,
  PRIMARY KEY (`IdAsiento`,`Linea`),
  KEY `IdCuenta` (`IdCuenta`),
  CONSTRAINT `detalleasiento_ibfk_1` FOREIGN KEY (`IdAsiento`) REFERENCES `asiento` (`IdAsiento`),
  CONSTRAINT `detalleasiento_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detallecompra`
--

DROP TABLE IF EXISTS `detallecompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detallecompra` (
  `IdCompra` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdCompra`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detallecompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  CONSTRAINT `detallecompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalledevolucioncliente`
--

DROP TABLE IF EXISTS `detalledevolucioncliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalledevolucioncliente` (
  `IdDevolucion` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `CantDevolucion` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalledevolucioncliente_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucioncliente` (`IdDevolucion`),
  CONSTRAINT `detalledevolucioncliente_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalledevolucionproveedor`
--

DROP TABLE IF EXISTS `detalledevolucionproveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalledevolucionproveedor` (
  `IdDevolucion` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `CantDevolucion` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdDevolucion`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalledevolucionproveedor_ibfk_1` FOREIGN KEY (`IdDevolucion`) REFERENCES `devolucionproveedor` (`IdDevolucion`),
  CONSTRAINT `detalledevolucionproveedor_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detallefactura`
--

DROP TABLE IF EXISTS `detallefactura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detallefactura` (
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
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detallefactura_ibfk_1` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  CONSTRAINT `detallefactura_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalleordencompra`
--

DROP TABLE IF EXISTS `detalleordencompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalleordencompra` (
  `IdOrdenCompra` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdOrdenCompra`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalleordencompra_ibfk_1` FOREIGN KEY (`IdOrdenCompra`) REFERENCES `ordencompra` (`IdOrdenCompra`),
  CONSTRAINT `detalleordencompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalleordenservicio`
--

DROP TABLE IF EXISTS `detalleordenservicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalleordenservicio` (
  `IdOrden` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `CostoInstalacion` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdOrden`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalleordenservicio_ibfk_1` FOREIGN KEY (`IdOrden`) REFERENCES `ordenservicio` (`IdOrden`),
  CONSTRAINT `detalleordenservicio_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalleproforma`
--

DROP TABLE IF EXISTS `detalleproforma`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalleproforma` (
  `IdProforma` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  PRIMARY KEY (`IdProforma`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalleproforma_ibfk_1` FOREIGN KEY (`IdProforma`) REFERENCES `proforma` (`IdProforma`),
  CONSTRAINT `detalleproforma_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detalletraslado`
--

DROP TABLE IF EXISTS `detalletraslado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `detalletraslado` (
  `IdTraslado` int(11) NOT NULL,
  `IdProducto` int(11) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  PRIMARY KEY (`IdTraslado`,`IdProducto`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `detalletraslado_ibfk_1` FOREIGN KEY (`IdTraslado`) REFERENCES `traslado` (`IdTraslado`),
  CONSTRAINT `detalletraslado_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `devolucioncliente`
--

DROP TABLE IF EXISTS `devolucioncliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `devolucioncliente` (
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
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `devolucioncliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `devolucioncliente_ibfk_2` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  CONSTRAINT `devolucioncliente_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `devolucioncliente_ibfk_4` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `devolucionproveedor`
--

DROP TABLE IF EXISTS `devolucionproveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `devolucionproveedor` (
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
  KEY `IdProveedor` (`IdProveedor`),
  CONSTRAINT `devolucionproveedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `devolucionproveedor_ibfk_2` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  CONSTRAINT `devolucionproveedor_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `devolucionproveedor_ibfk_4` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `distrito`
--

DROP TABLE IF EXISTS `distrito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `distrito` (
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`),
  CONSTRAINT `distrito_ibfk_1` FOREIGN KEY (`IdProvincia`, `IdCanton`) REFERENCES `canton` (`IdProvincia`, `IdCanton`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `documentoelectronico`
--

DROP TABLE IF EXISTS `documentoelectronico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `documentoelectronico` (
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
  PRIMARY KEY (`IdDocumento`),
  KEY `ClaveNumerica` (`ClaveNumerica`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `EstadoEnvio` (`EstadoEnvio`),
  KEY `ClaveNumerica_2` (`ClaveNumerica`,`Consecutivo`),
  CONSTRAINT `documentoelectronico_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=1102 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `egreso`
--

DROP TABLE IF EXISTS `egreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `egreso` (
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
  KEY `IdCuenta` (`IdCuenta`),
  CONSTRAINT `egreso_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `egreso_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentaegreso` (`IdCuenta`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `empresa`
--

DROP TABLE IF EXISTS `empresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `empresa` (
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
  `Logotipo` blob,
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
  `UltimoDocFE` int(11) NOT NULL,
  `UltimoDocND` int(11) NOT NULL,
  `UltimoDocNC` int(11) NOT NULL,
  `UltimoDocTE` int(11) NOT NULL,
  `UltimoDocMR` int(11) NOT NULL,
  `TipoContrato` int(11) NOT NULL,
  `CantidadDisponible` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`),
  KEY `Identificacion` (`Identificacion`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `factura`
--

DROP TABLE IF EXISTS `factura`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `factura` (
  `IdEmpresa` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL,
  `IdTerminal` int(11) NOT NULL,
  `IdFactura` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) NOT NULL,
  `IdCliente` int(11) NOT NULL,
  `IdCondicionVenta` int(11) NOT NULL,
  `PlazoCredito` int(11) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(50) DEFAULT NULL,
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
  PRIMARY KEY (`IdFactura`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdCliente` (`IdCliente`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdCondicionVenta` (`IdCondicionVenta`),
  KEY `IdVendedor` (`IdVendedor`),
  KEY `cxc_index` (`IdCxC`),
  CONSTRAINT `factura_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `factura_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  CONSTRAINT `factura_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `factura_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`),
  CONSTRAINT `factura_ibfk_5` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`)
) ENGINE=InnoDB AUTO_INCREMENT=370 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `formapago`
--

DROP TABLE IF EXISTS `formapago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `formapago` (
  `IdFormaPago` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  PRIMARY KEY (`IdFormaPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingreso`
--

DROP TABLE IF EXISTS `ingreso`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ingreso` (
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
  KEY `IdCuenta` (`IdCuenta`),
  CONSTRAINT `ingreso_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `ingreso_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentaingreso` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `linea`
--

DROP TABLE IF EXISTS `linea`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `linea` (
  `IdEmpresa` int(11) NOT NULL,
  `IdLinea` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipoProducto` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdLinea`),
  KEY `IdEmpresa` (`IdEmpresa`),
  KEY `IdTipoProducto` (`IdTipoProducto`),
  CONSTRAINT `linea_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `linea_ibfk_2` FOREIGN KEY (`IdTipoProducto`) REFERENCES `tipoproducto` (`IdTipoProducto`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `modulo`
--

DROP TABLE IF EXISTS `modulo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `modulo` (
  `IdModulo` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `MenuPadre` varchar(100) NOT NULL,
  PRIMARY KEY (`IdModulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `moduloporempresa`
--

DROP TABLE IF EXISTS `moduloporempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `moduloporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdModulo` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdModulo`),
  KEY `IdModulo` (`IdModulo`),
  CONSTRAINT `moduloporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `moduloporempresa_ibfk_2` FOREIGN KEY (`IdModulo`) REFERENCES `modulo` (`IdModulo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `movimientobanco`
--

DROP TABLE IF EXISTS `movimientobanco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movimientobanco` (
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
  KEY `IdTipo` (`IdTipo`),
  CONSTRAINT `movimientobanco_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentabanco` (`IdCuenta`),
  CONSTRAINT `movimientobanco_ibfk_2` FOREIGN KEY (`IdTipo`) REFERENCES `tipomovimientobanco` (`IdTipoMov`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `movimientocuentaporcobrar`
--

DROP TABLE IF EXISTS `movimientocuentaporcobrar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movimientocuentaporcobrar` (
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
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `movimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `movimientocuentaporpagar`
--

DROP TABLE IF EXISTS `movimientocuentaporpagar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movimientocuentaporpagar` (
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
  KEY `IdUsuario` (`IdUsuario`),
  CONSTRAINT `movimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `movimientoproducto`
--

DROP TABLE IF EXISTS `movimientoproducto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movimientoproducto` (
  `IdProducto` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Cantidad` double NOT NULL,
  `Tipo` varchar(10) NOT NULL,
  `Origen` varchar(100) NOT NULL,
  `Referencia` varchar(100) NOT NULL,
  `PrecioCosto` double NOT NULL,
  PRIMARY KEY (`IdProducto`,`Fecha`),
  CONSTRAINT `movimientoproducto_ibfk_1` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ordencompra`
--

DROP TABLE IF EXISTS `ordencompra`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ordencompra` (
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
  KEY `IdCondicionVenta` (`IdCondicionVenta`),
  CONSTRAINT `ordencompra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `ordencompra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `ordencompra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  CONSTRAINT `ordencompra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ordenservicio`
--

DROP TABLE IF EXISTS `ordenservicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ordenservicio` (
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
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `ordenservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `ordenservicio_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `ordenservicio_ibfk_3` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `padron`
--

DROP TABLE IF EXISTS `padron`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `padron` (
  `Identificacion` varchar(9) NOT NULL,
  `IdProvincia` int(11) NOT NULL,
  `IdCanton` int(11) NOT NULL,
  `IdDistrito` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `PrimerApellido` varchar(100) NOT NULL,
  `SegundoApellido` varchar(100) NOT NULL,
  PRIMARY KEY (`Identificacion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `parametrocontable`
--

DROP TABLE IF EXISTS `parametrocontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `parametrocontable` (
  `IdParametro` int(11) NOT NULL AUTO_INCREMENT,
  `IdTipo` int(11) NOT NULL,
  `IdCuenta` int(11) NOT NULL,
  `IdProducto` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdParametro`),
  KEY `IdTipo` (`IdTipo`),
  KEY `IdCuenta` (`IdCuenta`),
  CONSTRAINT `parametrocontable_ibfk_1` FOREIGN KEY (`IdTipo`) REFERENCES `tipoparametrocontable` (`IdTipo`),
  CONSTRAINT `parametrocontable_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `parametroimpuesto`
--

DROP TABLE IF EXISTS `parametroimpuesto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `parametroimpuesto` (
  `IdImpuesto` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) NOT NULL,
  `TasaImpuesto` double NOT NULL,
  PRIMARY KEY (`IdImpuesto`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `parametrosistema`
--

DROP TABLE IF EXISTS `parametrosistema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `parametrosistema` (
  `IdParametro` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `Valor` varchar(100) NOT NULL,
  PRIMARY KEY (`IdParametro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `producto`
--

DROP TABLE IF EXISTS `producto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `producto` (
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
  KEY `IdImpuesto` (`IdImpuesto`),
  CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `producto_ibfk_2` FOREIGN KEY (`IdLinea`) REFERENCES `linea` (`IdLinea`),
  CONSTRAINT `producto_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  CONSTRAINT `producto_ibfk_4` FOREIGN KEY (`Tipo`) REFERENCES `tipoproducto` (`IdTipoProducto`),
  CONSTRAINT `producto_ibfk_5` FOREIGN KEY (`IdTipoUnidad`) REFERENCES `tipounidad` (`IdTipoUnidad`),
  CONSTRAINT `producto_ibfk_6` FOREIGN KEY (`IdImpuesto`) REFERENCES `parametroimpuesto` (`IdImpuesto`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `proforma`
--

DROP TABLE IF EXISTS `proforma`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `proforma` (
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
  KEY `IdVendedor` (`IdVendedor`),
  CONSTRAINT `proforma_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `proforma_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  CONSTRAINT `proforma_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `proforma_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`),
  CONSTRAINT `proforma_ibfk_5` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `proveedor`
--

DROP TABLE IF EXISTS `proveedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `proveedor` (
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
  KEY `IdEmpresa` (`IdEmpresa`),
  CONSTRAINT `proveedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `provincia`
--

DROP TABLE IF EXISTS `provincia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `provincia` (
  `IdProvincia` int(11) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdProvincia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `registrorespuestahacienda`
--

DROP TABLE IF EXISTS `registrorespuestahacienda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `registrorespuestahacienda` (
  `IdRegistro` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `ClaveNumerica` varchar(50) NOT NULL,
  `Respuesta` blob,
  PRIMARY KEY (`IdRegistro`),
  KEY `idx_clavenumerica` (`ClaveNumerica`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `reporteporempresa`
--

DROP TABLE IF EXISTS `reporteporempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reporteporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdReporte` int(11) NOT NULL,
  PRIMARY KEY (`IdEmpresa`,`IdReporte`),
  KEY `IdReporte` (`IdReporte`),
  CONSTRAINT `reporteporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `reporteporempresa_ibfk_2` FOREIGN KEY (`IdReporte`) REFERENCES `catalogoreporte` (`IdReporte`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `IdRole` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  `MenuPadre` varchar(100) NOT NULL,
  `MenuItem` varchar(100) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  PRIMARY KEY (`IdRole`)
) ENGINE=InnoDB AUTO_INCREMENT=128 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `roleporusuario`
--

DROP TABLE IF EXISTS `roleporusuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `roleporusuario` (
  `IdUsuario` int(11) NOT NULL,
  `IdRole` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdRole`),
  KEY `IdRole` (`IdRole`),
  CONSTRAINT `roleporusuario_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `roleporusuario_ibfk_2` FOREIGN KEY (`IdRole`) REFERENCES `role` (`IdRole`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `saldomensualcontable`
--

DROP TABLE IF EXISTS `saldomensualcontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `saldomensualcontable` (
  `IdCuenta` int(11) NOT NULL,
  `Mes` int(2) NOT NULL,
  `Annio` int(4) NOT NULL,
  `SaldoFinMes` double NOT NULL,
  `TotalDebito` double NOT NULL,
  `TotalCredito` double NOT NULL,
  PRIMARY KEY (`IdCuenta`,`Mes`,`Annio`),
  CONSTRAINT `saldomensualcontable_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `sucursal`
--

DROP TABLE IF EXISTS `sucursal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sucursal` (
  `IdEmpresa` int(11) NOT NULL,
  `IdSucursal` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(200) NOT NULL,
  `Direccion` varchar(500) NOT NULL,
  `Telefono` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`IdSucursal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `terminalporempresa`
--

DROP TABLE IF EXISTS `terminalporempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `terminalporempresa` (
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
  PRIMARY KEY (`IdEmpresa`,`IdSucursal`,`IdTerminal`),
  CONSTRAINT `terminalporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipocuentacontable`
--

DROP TABLE IF EXISTS `tipocuentacontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipocuentacontable` (
  `IdTipoCuenta` int(11) NOT NULL,
  `TipoSaldo` varchar(1) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdTipoCuenta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipodecambiodolar`
--

DROP TABLE IF EXISTS `tipodecambiodolar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipodecambiodolar` (
  `FechaTipoCambio` varchar(10) NOT NULL DEFAULT '',
  `ValorTipoCambio` decimal(13,5) DEFAULT NULL,
  PRIMARY KEY (`FechaTipoCambio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipoidentificacion`
--

DROP TABLE IF EXISTS `tipoidentificacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipoidentificacion` (
  `IdTipoIdentificacion` int(11) NOT NULL,
  `Descripcion` varchar(20) NOT NULL,
  PRIMARY KEY (`IdTipoIdentificacion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipomoneda`
--

DROP TABLE IF EXISTS `tipomoneda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipomoneda` (
  `IdTipoMoneda` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(10) NOT NULL,
  PRIMARY KEY (`IdTipoMoneda`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipomovimientobanco`
--

DROP TABLE IF EXISTS `tipomovimientobanco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipomovimientobanco` (
  `IdTipoMov` int(11) NOT NULL AUTO_INCREMENT,
  `DebeHaber` varchar(2) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  PRIMARY KEY (`IdTipoMov`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipoparametrocontable`
--

DROP TABLE IF EXISTS `tipoparametrocontable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipoparametrocontable` (
  `IdTipo` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(200) NOT NULL,
  `MultiCuenta` bit(1) NOT NULL,
  PRIMARY KEY (`IdTipo`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipoproducto`
--

DROP TABLE IF EXISTS `tipoproducto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipoproducto` (
  `IdTipoProducto` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(10) NOT NULL,
  PRIMARY KEY (`IdTipoProducto`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipounidad`
--

DROP TABLE IF EXISTS `tipounidad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipounidad` (
  `IdTipoUnidad` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(5) NOT NULL,
  PRIMARY KEY (`IdTipoUnidad`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `traslado`
--

DROP TABLE IF EXISTS `traslado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `traslado` (
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
  KEY `IdSucursal` (`IdSucursal`),
  CONSTRAINT `traslado_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `traslado_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  CONSTRAINT `traslado_ibfk_3` FOREIGN KEY (`IdSucursal`) REFERENCES `sucursal` (`IdSucursal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `IdUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `CodigoUsuario` varchar(10) NOT NULL,
  `Clave` varchar(1000) NOT NULL,
  `Modifica` tinyint(1) NOT NULL,
  `AutorizaCredito` tinyint(1) NOT NULL,
  PRIMARY KEY (`IdUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usuarioporempresa`
--

DROP TABLE IF EXISTS `usuarioporempresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuarioporempresa` (
  `IdEmpresa` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdEmpresa`),
  KEY `IdEmpresa` (`IdEmpresa`),
  CONSTRAINT `usuarioporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  CONSTRAINT `usuarioporempresa_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `vendedor`
--

DROP TABLE IF EXISTS `vendedor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vendedor` (
  `IdEmpresa` int(11) NOT NULL,
  `IdVendedor` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) NOT NULL,
  PRIMARY KEY (`IdVendedor`),
  KEY `IdEmpresa` (`IdEmpresa`),
  CONSTRAINT `vendedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-06-20  9:16:10
