-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: mysql5048.site4now.net
-- Generation Time: May 01, 2024 at 11:13 AM
-- Server version: 8.0.33
-- PHP Version: 8.3.3

START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_aa6591_factura`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`aa6591_develop`@`%` PROCEDURE `limpiarregistros` (IN `intIdEmpresa` INT(11))   BEGIN
DELETE FROM desglosepagomovimientoapartado WHERE idmovapartado IN (select idmovapartado FROM movimientoapartado WHERE idempresa = intIdEmpresa);
DELETE FROM movimientoapartado WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagoapartado WHERE idapartado IN (select idapartado FROM apartado WHERE idempresa = intIdEmpresa);
DELETE FROM detalleapartado WHERE idapartado IN (select idapartado FROM apartado WHERE idempresa = intIdEmpresa);
DELETE FROM apartado WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientoordenservicio WHERE idmovorden IN (select idmovorden FROM movimientoordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM movimientoordenservicio WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagoordenservicio WHERE idorden IN (select idorden FROM ordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM detalleordenservicio WHERE idorden IN (select idorden FROM ordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM ordenservicio WHERE idempresa = intIdEmpresa;
DELETE FROM detalleproforma WHERE idproforma IN (select idproforma FROM proforma WHERE idempresa = intIdEmpresa);
DELETE FROM proforma WHERE idempresa = intIdEmpresa;
DELETE FROM detalledevolucioncliente WHERE iddevolucion IN (select iddevolucion FROM devolucioncliente WHERE idempresa = intIdEmpresa);
DELETE FROM devolucioncliente WHERE idempresa = intIdEmpresa;
DELETE FROM detallefactura WHERE idfactura IN (select idfactura FROM factura WHERE idempresa = intIdEmpresa);
DELETE FROM desglosepagofactura WHERE idfactura IN (select idfactura FROM factura WHERE idempresa = intIdEmpresa);
DELETE FROM factura WHERE idempresa = intIdEmpresa;
DELETE FROM detalleajusteinventario WHERE idajuste IN (select idajuste FROM ajusteinventario WHERE idempresa = intIdEmpresa);
DELETE FROM ajusteinventario WHERE idempresa = intIdEmpresa;
DELETE FROM detalletraslado WHERE idtraslado IN (select idtraslado FROM traslado WHERE idempresa = intIdEmpresa);
DELETE FROM traslado WHERE idempresa = intIdEmpresa;
DELETE FROM detallecompra WHERE idcompra IN (select idcompra FROM compra WHERE idempresa = intIdEmpresa);
DELETE FROM desglosepagocompra WHERE idcompra IN (select idcompra FROM compra WHERE idempresa = intIdEmpresa);
DELETE FROM compra WHERE idempresa = intIdEmpresa;
DELETE FROM movimientoproducto WHERE idproducto IN (select idproducto FROM producto WHERE idempresa = intIdEmpresa);
DELETE FROM existenciaporsucursal WHERE idempresa = intIdEmpresa;
DELETE FROM producto WHERE idempresa = intIdEmpresa;
DELETE FROM proveedor WHERE idempresa = intIdEmpresa;
DELETE FROM lineaporsucursal WHERE idempresa = intIdEmpresa;
DELETE FROM linea WHERE idempresa = intIdEmpresa;
DELETE FROM cliente WHERE idempresa = intIdEmpresa;
DELETE FROM detallefacturacompra WHERE idfactcompra IN (select idfactcompra FROM facturacompra WHERE idempresa = intIdEmpresa);
DELETE FROM facturacompra WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientocuentaporcobrar WHERE idmovcxc IN (SELECT idmovcxc FROM movimientocuentaporcobrar WHERE idempresa = intIdEmpresa);
DELETE FROM movimientocuentaporcobrar WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaporcobrar WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientocuentaporpagar WHERE idmovcxp IN (SELECT idmovcxp FROM movimientocuentaporpagar WHERE idempresa = intIdEmpresa);
DELETE FROM movimientocuentaporpagar WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaporpagar WHERE idempresa = intIdEmpresa;
DELETE FROM documentoelectronico WHERE idempresa = intIdEmpresa;
DELETE FROM vendedor WHERE idempresa = intIdEmpresa;
DELETE FROM movimientobanco WHERE idcuenta in(select idcuenta FROM cuentabanco WHERE idempresa = intIdEmpresa);
DELETE FROM cuentabanco WHERE idempresa = intIdEmpresa;
DELETE FROM bancoadquiriente WHERE idempresa = intIdEmpresa;
DELETE FROM cantfemensualempresa WHERE idempresa = intIdEmpresa;
DELETE FROM reporteporempresa WHERE idempresa = intIdEmpresa;
DELETE FROM roleporempresa WHERE idempresa = intIdEmpresa;
DELETE FROM detallemovimientocierrecaja WHERE idcierre IN (SELECT idcierre FROM cierrecaja WHERE idempresa = intIdEmpresa);
DELETE FROM detalleefectivocierrecaja WHERE idcierre IN (SELECT idcierre FROM cierrecaja WHERE idempresa = intIdEmpresa);
DELETE FROM cierrecaja WHERE idempresa = intIdEmpresa;
DELETE FROM egreso WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaegreso WHERE idempresa = intIdEmpresa;
DELETE FROM ingreso WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaingreso WHERE idempresa = intIdEmpresa;
DELETE FROM tiqueteordenservicio WHERE idempresa = intIdEmpresa;
DELETE FROM puntodeservicio WHERE idempresa = intIdEmpresa;
DELETE FROM terminalporsucursal WHERE idempresa = intIdEmpresa;
DELETE FROM sucursalporempresa WHERE idempresa = intIdEmpresa;
DELETE FROM credencialeshacienda WHERE idempresa = intIdEmpresa;
END$$

CREATE DEFINER=`aa6591_factura`@`%` PROCEDURE `MarcaRegistrosProcesados` (IN `intIdEmpresa` INT(11), IN `intIdSucursal` INT(11))   BEGIN
UPDATE apartado SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE ordenservicio SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Egreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Ingreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Factura SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoCuentaPorCobrar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Compra SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoCuentaPorPagar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE DevolucionCliente SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoApartado SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoOrdenServicio SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
END$$

--
-- Functions
--
CREATE DEFINER=`aa6591_develop`@`%` FUNCTION `DiffDays` (`dateFrom` DATE, `dateTo` DATE) RETURNS INT  RETURN TIMESTAMPDIFF(DAY, dateFrom, dateTo)$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `actividadeconomicaempresa`
--

CREATE TABLE `actividadeconomicaempresa` (
  `IdEmpresa` int NOT NULL,
  `CodigoActividad` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `ajusteinventario`
--

CREATE TABLE `ajusteinventario` (
  `IdEmpresa` int NOT NULL,
  `IdAjuste` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(220) DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `IdSucursal` int NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `apartado`
--

CREATE TABLE `apartado` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdApartado` int NOT NULL,
  `ConsecApartado` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCliente` int NOT NULL,
  `NombreCliente` varchar(80) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `TextoAdicional` varchar(500) DEFAULT NULL,
  `IdVendedor` int NOT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Exonerado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `MontoAdelanto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  `Procesado` bit(1) NOT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `MontoPagado` double NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `asiento`
--

CREATE TABLE `asiento` (
  `IdEmpresa` int NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Fecha` datetime NOT NULL,
  `TotalDebito` double NOT NULL,
  `TotalCredito` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `bancoadquiriente`
--

CREATE TABLE `bancoadquiriente` (
  `IdEmpresa` int NOT NULL,
  `IdBanco` int NOT NULL,
  `Codigo` varchar(30) NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `PorcentajeRetencion` double NOT NULL,
  `PorcentajeComision` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `barrio`
--

CREATE TABLE `barrio` (
  `IdProvincia` int NOT NULL,
  `IdCanton` int NOT NULL,
  `IdDistrito` int NOT NULL,
  `IdBarrio` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cantfemensualempresa`
--

CREATE TABLE `cantfemensualempresa` (
  `IdEmpresa` int NOT NULL,
  `IdMes` int NOT NULL,
  `IdAnio` int NOT NULL,
  `CantidadDoc` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `canton`
--

CREATE TABLE `canton` (
  `IdProvincia` int NOT NULL,
  `IdCanton` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `catalogocontable`
--

CREATE TABLE `catalogocontable` (
  `IdEmpresa` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Nivel_1` varchar(1) NOT NULL,
  `Nivel_2` varchar(2) DEFAULT NULL,
  `Nivel_3` varchar(2) DEFAULT NULL,
  `Nivel_4` varchar(2) DEFAULT NULL,
  `Nivel_5` varchar(2) DEFAULT NULL,
  `Nivel_6` varchar(2) DEFAULT NULL,
  `Nivel_7` varchar(2) DEFAULT NULL,
  `IdCuentaGrupo` int DEFAULT NULL,
  `EsCuentaBalance` bit(1) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `IdTipoCuenta` int NOT NULL,
  `IdClaseCuenta` int NOT NULL,
  `PermiteMovimiento` bit(1) NOT NULL,
  `PermiteSobrejiro` bit(1) NOT NULL,
  `SaldoActual` double NOT NULL DEFAULT '0',
  `TotalDebito` double NOT NULL DEFAULT '0',
  `TotalCredito` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `catalogoreporte`
--

CREATE TABLE `catalogoreporte` (
  `IdReporte` int NOT NULL,
  `NombreReporte` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cierrecaja`
--

CREATE TABLE `cierrecaja` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdCierre` int NOT NULL,
  `FechaCierre` datetime NOT NULL,
  `FondoInicio` double NOT NULL,
  `AdelantosApartadoEfectivo` double NOT NULL,
  `AdelantosApartadoTarjeta` double NOT NULL,
  `AdelantosApartadoBancos` double NOT NULL,
  `AdelantosOrdenEfectivo` double NOT NULL,
  `AdelantosOrdenTarjeta` double NOT NULL,
  `AdelantosOrdenBancos` double NOT NULL,
  `VentasEfectivo` double NOT NULL,
  `VentasTarjeta` double NOT NULL,
  `VentasBancos` double NOT NULL,
  `IngresosEfectivo` double NOT NULL,
  `PagosCxCEfectivo` double NOT NULL,
  `PagosCxCTarjeta` double NOT NULL,
  `PagosCxCBancos` double NOT NULL,
  `ComprasEfectivo` double NOT NULL,
  `ComprasBancos` double NOT NULL,
  `EgresosEfectivo` double NOT NULL,
  `PagosCxPEfectivo` double NOT NULL,
  `PagosCxPBancos` double NOT NULL,
  `RetencionTarjeta` double NOT NULL,
  `ComisionTarjeta` double NOT NULL,
  `LiquidacionTarjeta` double NOT NULL,
  `VentasCredito` double NOT NULL,
  `ComprasCredito` double NOT NULL,
  `RetiroEfectivo` double NOT NULL,
  `FondoCierre` double NOT NULL,
  `Observaciones` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `clasecuentacontable`
--

CREATE TABLE `clasecuentacontable` (
  `IdClaseCuenta` int NOT NULL,
  `Descripcion` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `clasificacionproducto`
--

CREATE TABLE `clasificacionproducto` (
  `Id` varchar(20) NOT NULL,
  `Descripcion` varchar(600) NOT NULL,
  `Impuesto` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cliente`
--

CREATE TABLE `cliente` (
  `IdEmpresa` int NOT NULL,
  `IdCliente` int NOT NULL,
  `IdTipoIdentificacion` int NOT NULL,
  `Identificacion` varchar(20) NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Nombre` varchar(80) NOT NULL,
  `NombreComercial` varchar(80) DEFAULT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `Celular` varchar(20) DEFAULT NULL,
  `Fax` varchar(20) DEFAULT NULL,
  `CorreoElectronico` varchar(200) DEFAULT NULL,
  `IdVendedor` int DEFAULT NULL,
  `IdTipoPrecio` int DEFAULT NULL,
  `AplicaTasaDiferenciada` int NOT NULL,
  `IdImpuesto` int DEFAULT NULL,
  `IdTipoExoneracion` int NOT NULL,
  `NumDocExoneracion` varchar(100) DEFAULT NULL,
  `NombreInstExoneracion` varchar(100) DEFAULT NULL,
  `FechaEmisionDoc` datetime NOT NULL,
  `PorcentajeExoneracion` int DEFAULT NULL,
  `PermiteCredito` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `compra`
--

CREATE TABLE `compra` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdCompra` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `TipoDeCambioDolar` double NOT NULL,
  `IdProveedor` int NOT NULL,
  `IdCondicionVenta` int NOT NULL,
  `PlazoCredito` int DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(20) DEFAULT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdCxP` int NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `IdOrdenCompra` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `Observaciones` varchar(500) DEFAULT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `condicionventa`
--

CREATE TABLE `condicionventa` (
  `IdCondicionVenta` int NOT NULL,
  `Descripcion` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `credencialeshacienda`
--

CREATE TABLE `credencialeshacienda` (
  `UsuarioHacienda` varchar(100) DEFAULT NULL,
  `ClaveHacienda` varchar(100) DEFAULT NULL,
  `AccessToken` blob,
  `ExpiresIn` int DEFAULT NULL,
  `RefreshExpiresIn` int DEFAULT NULL,
  `RefreshToken` blob,
  `EmitedAt` datetime DEFAULT NULL,
  `Certificado` blob,
  `NombreCertificado` varchar(100) DEFAULT NULL,
  `PinCertificado` varchar(4) DEFAULT NULL,
  `IdEmpresa` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cuentabanco`
--

CREATE TABLE `cuentabanco` (
  `IdEmpresa` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Codigo` varchar(50) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Saldo` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cuentaegreso`
--

CREATE TABLE `cuentaegreso` (
  `IdEmpresa` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cuentaingreso`
--

CREATE TABLE `cuentaingreso` (
  `IdEmpresa` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cuentaporcobrar`
--

CREATE TABLE `cuentaporcobrar` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdCxC` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdPropietario` int NOT NULL,
  `Referencia` varchar(50) DEFAULT NULL,
  `NroDocOrig` int DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `Plazo` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `Total` double NOT NULL,
  `Saldo` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `cuentaporpagar`
--

CREATE TABLE `cuentaporpagar` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdCxP` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdPropietario` int NOT NULL,
  `Referencia` varchar(50) DEFAULT NULL,
  `NroDocOrig` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Plazo` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `Total` double NOT NULL,
  `Saldo` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagoapartado`
--

CREATE TABLE `desglosepagoapartado` (
  `IdConsecutivo` int NOT NULL,
  `IdApartado` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagocompra`
--

CREATE TABLE `desglosepagocompra` (
  `IdConsecutivo` int NOT NULL,
  `IdCompra` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagofactura`
--

CREATE TABLE `desglosepagofactura` (
  `IdConsecutivo` int NOT NULL,
  `IdFactura` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagomovimientoapartado`
--

CREATE TABLE `desglosepagomovimientoapartado` (
  `IdConsecutivo` int NOT NULL,
  `IdMovApartado` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagomovimientocuentaporcobrar`
--

CREATE TABLE `desglosepagomovimientocuentaporcobrar` (
  `IdConsecutivo` int NOT NULL,
  `IdMovCxC` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagomovimientocuentaporpagar`
--

CREATE TABLE `desglosepagomovimientocuentaporpagar` (
  `IdConsecutivo` int NOT NULL,
  `IdMovCxP` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagomovimientoordenservicio`
--

CREATE TABLE `desglosepagomovimientoordenservicio` (
  `IdConsecutivo` int NOT NULL,
  `IdMovOrden` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `desglosepagoordenservicio`
--

CREATE TABLE `desglosepagoordenservicio` (
  `IdConsecutivo` int NOT NULL,
  `IdOrden` int NOT NULL,
  `IdFormaPago` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCuentaBanco` int NOT NULL,
  `TipoTarjeta` varchar(50) DEFAULT NULL,
  `NroMovimiento` varchar(50) DEFAULT NULL,
  `MontoLocal` double NOT NULL,
  `TipoDeCambio` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleajusteinventario`
--

CREATE TABLE `detalleajusteinventario` (
  `IdAjuste` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleapartado`
--

CREATE TABLE `detalleapartado` (
  `IdConsecutivo` int NOT NULL,
  `IdApartado` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  `PorcDescuento` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleasiento`
--

CREATE TABLE `detalleasiento` (
  `IdAsiento` int NOT NULL,
  `Linea` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Debito` double NOT NULL,
  `Credito` double NOT NULL,
  `SaldoAnterior` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detallecompra`
--

CREATE TABLE `detallecompra` (
  `IdConsecutivo` int NOT NULL,
  `IdCompra` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `PrecioVenta` double NOT NULL,
  `PrecioVentaAnt` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalledevolucioncliente`
--

CREATE TABLE `detalledevolucioncliente` (
  `IdDevolucion` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleefectivocierrecaja`
--

CREATE TABLE `detalleefectivocierrecaja` (
  `IdCierre` int NOT NULL DEFAULT '0',
  `Denominacion` int NOT NULL,
  `Cantidad` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detallefactura`
--

CREATE TABLE `detallefactura` (
  `IdFactura` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  `IdConsecutivo` int NOT NULL,
  `PorcDescuento` double NOT NULL,
  `CantDevuelto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detallefacturacompra`
--

CREATE TABLE `detallefacturacompra` (
  `IdFactCompra` int NOT NULL,
  `Linea` int NOT NULL,
  `Codigo` varchar(13) NOT NULL,
  `Cantidad` double NOT NULL,
  `UnidadMedida` varchar(5) NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `PrecioVenta` double NOT NULL,
  `IdImpuesto` int NOT NULL,
  `PorcentajeIVA` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detallemovimientocierrecaja`
--

CREATE TABLE `detallemovimientocierrecaja` (
  `Consecutivo` int NOT NULL,
  `IdCierre` int NOT NULL,
  `IdReferencia` int NOT NULL,
  `Tipo` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Descripcion` varchar(200) DEFAULT NULL,
  `Total` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleordencompra`
--

CREATE TABLE `detalleordencompra` (
  `IdOrdenCompra` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleordenservicio`
--

CREATE TABLE `detalleordenservicio` (
  `IdConsecutivo` int NOT NULL,
  `IdOrden` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  `PorcDescuento` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalleproforma`
--

CREATE TABLE `detalleproforma` (
  `IdConsecutivo` int NOT NULL,
  `IdProforma` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioVenta` double NOT NULL,
  `Excento` bit(1) NOT NULL,
  `PorcentajeIVA` double NOT NULL,
  `PorcDescuento` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `detalletraslado`
--

CREATE TABLE `detalletraslado` (
  `IdTraslado` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL,
  `PrecioCosto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `devolucioncliente`
--

CREATE TABLE `devolucioncliente` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdDevolucion` int NOT NULL,
  `IdFactura` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdCliente` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdMovimientoCxC` int NOT NULL,
  `IdAsiento` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `IdDocElectronico` varchar(50) DEFAULT NULL,
  `IdDocElectronicoRev` varchar(50) DEFAULT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `ConsecFactura` int NOT NULL,
  `Detalle` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `distrito`
--

CREATE TABLE `distrito` (
  `IdProvincia` int NOT NULL,
  `IdCanton` int NOT NULL,
  `IdDistrito` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `documentoelectronico`
--

CREATE TABLE `documentoelectronico` (
  `IdDocumento` int NOT NULL,
  `IdEmpresa` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `ClaveNumerica` varchar(50) NOT NULL,
  `Consecutivo` varchar(20) DEFAULT NULL,
  `TipoIdentificacionEmisor` varchar(2) NOT NULL,
  `IdentificacionEmisor` varchar(12) NOT NULL,
  `TipoIdentificacionReceptor` varchar(2) NOT NULL,
  `IdentificacionReceptor` varchar(12) NOT NULL,
  `EsMensajeReceptor` varchar(1) NOT NULL,
  `DatosDocumento` mediumblob,
  `Respuesta` blob,
  `EstadoEnvio` varchar(20) NOT NULL,
  `CorreoNotificacion` varchar(200) NOT NULL,
  `IdTipoDocumento` int NOT NULL,
  `ErrorEnvio` varchar(500) DEFAULT NULL,
  `IdSucursal` int NOT NULL,
  `IdTerminal` int NOT NULL,
  `IdConsecutivo` int NOT NULL,
  `DatosDocumentoOri` mediumblob,
  `EsIvaAcreditable` varchar(1) NOT NULL,
  `NombreReceptor` varchar(80) NOT NULL,
  `Reprocesado` bit(1) NOT NULL,
  `Total` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `egreso`
--

CREATE TABLE `egreso` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdEgreso` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdCuenta` int NOT NULL,
  `Beneficiario` varchar(100) DEFAULT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int DEFAULT NULL,
  `IdMovBanco` int DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `empresa`
--

CREATE TABLE `empresa` (
  `IdEmpresa` int NOT NULL,
  `NombreEmpresa` varchar(80) NOT NULL,
  `CorreoNotificacion` varchar(200) DEFAULT NULL,
  `PermiteFacturar` bit(1) NOT NULL,
  `Logotipo` mediumblob,
  `NombreComercial` varchar(80) NOT NULL,
  `IdTipoIdentificacion` int NOT NULL,
  `Identificacion` varchar(20) NOT NULL,
  `IdProvincia` int NOT NULL,
  `IdCanton` int NOT NULL,
  `IdDistrito` int NOT NULL,
  `IdBarrio` int NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Telefono1` varchar(8) DEFAULT NULL,
  `LineasPorFactura` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `Contabiliza` bit(1) NOT NULL,
  `AutoCompletaProducto` bit(1) NOT NULL,
  `AsignaVendedorPorDefecto` bit(1) NOT NULL,
  `RegimenSimplificado` bit(1) NOT NULL,
  `FechaVence` datetime DEFAULT NULL,
  `TipoContrato` int NOT NULL,
  `CantidadDisponible` int NOT NULL,
  `RecepcionGastos` bit(1) NOT NULL,
  `Telefono2` varchar(8) DEFAULT NULL,
  `IngresaPagoCliente` bit(1) NOT NULL,
  `MontoRedondeoDescuento` double DEFAULT NULL,
  `LeyendaFactura` varchar(500) DEFAULT NULL,
  `LeyendaProforma` varchar(500) DEFAULT NULL,
  `LeyendaApartado` varchar(500) DEFAULT NULL,
  `LeyendaOrdenServicio` varchar(500) DEFAULT NULL,
  `PrecioVentaIncluyeIVA` bit(1) NOT NULL,
  `Modalidad` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 ROW_FORMAT=DYNAMIC;

-- --------------------------------------------------------

--
-- Table structure for table `existenciaporsucursal`
--

CREATE TABLE `existenciaporsucursal` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Cantidad` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `factura`
--

CREATE TABLE `factura` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdTerminal` int NOT NULL,
  `IdFactura` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdCliente` int NOT NULL,
  `IdCondicionVenta` int NOT NULL,
  `PlazoCredito` int DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `TextoAdicional` varchar(500) DEFAULT NULL,
  `IdVendedor` int NOT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Descuento` double NOT NULL,
  `MontoPagado` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdCxC` int NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `IdOrdenServicio` int NOT NULL,
  `IdProforma` int NOT NULL,
  `TotalCosto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `IdDocElectronico` varchar(50) DEFAULT NULL,
  `IdDocElectronicoRev` varchar(50) DEFAULT NULL,
  `IdTipoMoneda` int NOT NULL,
  `TipoDeCambioDolar` decimal(13,5) NOT NULL,
  `IdTipoExoneracion` int NOT NULL,
  `NumDocExoneracion` varchar(100) DEFAULT NULL,
  `NombreInstExoneracion` varchar(100) DEFAULT NULL,
  `FechaEmisionDoc` datetime NOT NULL,
  `PorcentajeExoneracion` int DEFAULT NULL,
  `Exonerado` double NOT NULL,
  `NombreCliente` varchar(80) DEFAULT NULL,
  `ConsecFactura` int NOT NULL,
  `MontoAdelanto` double NOT NULL,
  `IdApartado` int NOT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `CodigoActividad` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `facturacompra`
--

CREATE TABLE `facturacompra` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdTerminal` int NOT NULL,
  `IdFactCompra` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `TipoDeCambioDolar` double NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdCondicionVenta` int NOT NULL,
  `PlazoCredito` varchar(10) DEFAULT NULL,
  `NombreEmisor` varchar(100) DEFAULT NULL,
  `IdTipoIdentificacion` int NOT NULL,
  `IdentificacionEmisor` varchar(12) DEFAULT NULL,
  `NombreComercialEmisor` varchar(80) DEFAULT NULL,
  `TelefonoEmisor` varchar(20) DEFAULT NULL,
  `IdProvinciaEmisor` int NOT NULL,
  `IdCantonEmisor` int NOT NULL,
  `IdDistritoEmisor` int NOT NULL,
  `IdBarrioEmisor` int NOT NULL,
  `DireccionEmisor` varchar(250) DEFAULT NULL,
  `CorreoElectronicoEmisor` varchar(160) DEFAULT NULL,
  `IdTipoExoneracion` int NOT NULL,
  `NumDocExoneracion` varchar(40) DEFAULT NULL,
  `NombreInstExoneracion` varchar(160) DEFAULT NULL,
  `FechaEmisionDoc` datetime NOT NULL,
  `PorcentajeExoneracion` int DEFAULT NULL,
  `TextoAdicional` varchar(500) DEFAULT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Exonerado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `IdDocElectronico` varchar(50) DEFAULT NULL,
  `CodigoActividad` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `formapago`
--

CREATE TABLE `formapago` (
  `IdFormaPago` int NOT NULL,
  `Descripcion` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `ingreso`
--

CREATE TABLE `ingreso` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdIngreso` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdCuenta` int NOT NULL,
  `RecibidoDe` varchar(100) NOT NULL,
  `Detalle` varchar(255) NOT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int DEFAULT NULL,
  `IdMovBanco` int DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `linea`
--

CREATE TABLE `linea` (
  `IdEmpresa` int NOT NULL,
  `IdLinea` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `lineaporsucursal`
--

CREATE TABLE `lineaporsucursal` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdLinea` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientoapartado`
--

CREATE TABLE `movimientoapartado` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdMovApartado` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdApartado` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `Fecha` datetime NOT NULL,
  `Observaciones` varchar(100) DEFAULT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `SaldoActual` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientobanco`
--

CREATE TABLE `movimientobanco` (
  `IdMov` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipo` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `Numero` varchar(50) NOT NULL,
  `Beneficiario` varchar(200) NOT NULL,
  `SaldoAnterior` double NOT NULL,
  `Monto` double NOT NULL,
  `Descripcion` varchar(255) NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `IdSucursal` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientocuentaporcobrar`
--

CREATE TABLE `movimientocuentaporcobrar` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdMovCxC` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdPropietario` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `Fecha` datetime NOT NULL,
  `Observaciones` varchar(100) DEFAULT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `IdCxC` int DEFAULT NULL,
  `SaldoActual` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientocuentaporpagar`
--

CREATE TABLE `movimientocuentaporpagar` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdMovCxP` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `IdPropietario` int NOT NULL,
  `TipoPropietario` smallint NOT NULL,
  `Recibo` varchar(50) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Observaciones` varchar(100) DEFAULT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `IdCxP` int DEFAULT NULL,
  `SaldoActual` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientoordenservicio`
--

CREATE TABLE `movimientoordenservicio` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdMovOrden` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdOrden` int NOT NULL,
  `Tipo` smallint NOT NULL,
  `Fecha` datetime NOT NULL,
  `Observaciones` varchar(100) DEFAULT NULL,
  `Monto` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `IdMovBanco` int NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Procesado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL,
  `SaldoActual` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `movimientoproducto`
--

CREATE TABLE `movimientoproducto` (
  `IdMovimiento` int NOT NULL,
  `IdProducto` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Cantidad` double NOT NULL,
  `Tipo` varchar(10) NOT NULL,
  `Origen` varchar(100) NOT NULL,
  `PrecioCosto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `ordencompra`
--

CREATE TABLE `ordencompra` (
  `IdEmpresa` int NOT NULL,
  `IdOrdenCompra` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdProveedor` int NOT NULL,
  `IdCondicionVenta` int NOT NULL,
  `PlazoCredito` int DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `NoDocumento` varchar(50) DEFAULT NULL,
  `TipoPago` smallint NOT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `ordenservicio`
--

CREATE TABLE `ordenservicio` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdOrden` int NOT NULL,
  `ConsecOrdenServicio` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCliente` int NOT NULL,
  `NombreCliente` varchar(80) DEFAULT NULL,
  `IdVendedor` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `Direccion` varchar(200) DEFAULT NULL,
  `Descripcion` varchar(200) DEFAULT NULL,
  `FechaEntrega` varchar(20) NOT NULL,
  `HoraEntrega` varchar(20) NOT NULL,
  `OtrosDetalles` varchar(500) DEFAULT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Exonerado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `MontoAdelanto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  `Procesado` bit(1) NOT NULL,
  `MontoPagado` double NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `padron`
--

CREATE TABLE `padron` (
  `Identificacion` varchar(9) NOT NULL,
  `IdProvincia` int NOT NULL,
  `IdCanton` int NOT NULL,
  `IdDistrito` int NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `PrimerApellido` varchar(100) NOT NULL,
  `SegundoApellido` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `parametrocontable`
--

CREATE TABLE `parametrocontable` (
  `IdParametro` int NOT NULL,
  `IdTipo` int NOT NULL,
  `IdCuenta` int NOT NULL,
  `IdProducto` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `parametroexoneracion`
--

CREATE TABLE `parametroexoneracion` (
  `IdTipoExoneracion` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `parametroimpuesto`
--

CREATE TABLE `parametroimpuesto` (
  `IdImpuesto` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `TasaImpuesto` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `parametrosistema`
--

CREATE TABLE `parametrosistema` (
  `IdParametro` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL,
  `Valor` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `planfacturacion`
--

CREATE TABLE `planfacturacion` (
  `IdPlan` int NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `CantidadDocumentos` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `producto`
--

CREATE TABLE `producto` (
  `IdEmpresa` int NOT NULL,
  `IdProducto` int NOT NULL,
  `Tipo` int NOT NULL,
  `IdLinea` int NOT NULL,
  `Codigo` varchar(50) DEFAULT NULL,
  `IdProveedor` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `PrecioCosto` double NOT NULL,
  `PrecioVenta1` double NOT NULL,
  `PrecioVenta2` double NOT NULL,
  `PrecioVenta3` double NOT NULL,
  `PrecioVenta4` double NOT NULL,
  `PrecioVenta5` double NOT NULL,
  `Imagen` longblob,
  `IdImpuesto` int NOT NULL,
  `CodigoProveedor` varchar(50) DEFAULT NULL,
  `Marca` varchar(50) DEFAULT NULL,
  `Observacion` varchar(200) DEFAULT NULL,
  `Activo` bit(1) NOT NULL,
  `ModificaPrecio` bit(1) NOT NULL,
  `PorcDescuento` double NOT NULL,
  `CodigoClasificacion` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `proforma`
--

CREATE TABLE `proforma` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdProforma` int NOT NULL,
  `ConsecProforma` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdTipoMoneda` int NOT NULL,
  `IdCliente` int NOT NULL,
  `NombreCliente` varchar(80) DEFAULT NULL,
  `Fecha` datetime NOT NULL,
  `TextoAdicional` varchar(500) DEFAULT NULL,
  `Telefono` varchar(20) DEFAULT NULL,
  `IdVendedor` int NOT NULL,
  `Excento` double NOT NULL,
  `Gravado` double NOT NULL,
  `Exonerado` double NOT NULL,
  `Descuento` double NOT NULL,
  `Impuesto` double NOT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `Aplicado` bit(1) NOT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `proveedor`
--

CREATE TABLE `proveedor` (
  `IdEmpresa` int NOT NULL,
  `IdProveedor` int NOT NULL,
  `Identificacion` varchar(35) NOT NULL,
  `Nombre` varchar(200) DEFAULT NULL,
  `Direccion` varchar(255) DEFAULT NULL,
  `Telefono1` varchar(9) DEFAULT NULL,
  `Telefono2` varchar(9) DEFAULT NULL,
  `Fax` varchar(9) DEFAULT NULL,
  `Correo` varchar(100) DEFAULT NULL,
  `PlazoCredito` int NOT NULL,
  `Contacto1` varchar(100) DEFAULT NULL,
  `TelCont1` varchar(9) DEFAULT NULL,
  `Contacto2` varchar(100) DEFAULT NULL,
  `TelCont2` varchar(9) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `provincia`
--

CREATE TABLE `provincia` (
  `IdProvincia` int NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `puntodeservicio`
--

CREATE TABLE `puntodeservicio` (
  `IdPunto` int NOT NULL,
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `Activo` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `registroautenticacion`
--

CREATE TABLE `registroautenticacion` (
  `Id` varchar(36) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Role` int NOT NULL,
  `IdEmpresa` int NOT NULL,
  `CodigoUsuario` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `reporteporempresa`
--

CREATE TABLE `reporteporempresa` (
  `IdEmpresa` int NOT NULL,
  `IdReporte` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `IdRole` int NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `MenuPadre` varchar(100) NOT NULL,
  `MenuItem` varchar(100) NOT NULL,
  `Descripcion` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `roleporempresa`
--

CREATE TABLE `roleporempresa` (
  `IdEmpresa` int NOT NULL,
  `IdRole` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `roleporusuario`
--

CREATE TABLE `roleporusuario` (
  `IdUsuario` int NOT NULL,
  `IdRole` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `saldomensualcontable`
--

CREATE TABLE `saldomensualcontable` (
  `IdCuenta` int NOT NULL,
  `Mes` int NOT NULL,
  `Annio` int NOT NULL,
  `SaldoFinMes` double NOT NULL,
  `TotalDebito` double NOT NULL,
  `TotalCredito` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `sucursalporempresa`
--

CREATE TABLE `sucursalporempresa` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `NombreSucursal` varchar(160) NOT NULL,
  `Direccion` varchar(160) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `CierreEnEjecucion` bit(1) NOT NULL,
  `ConsecFactura` int NOT NULL,
  `ConsecProforma` int NOT NULL,
  `ConsecOrdenServicio` int NOT NULL,
  `ConsecApartado` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `terminalporsucursal`
--

CREATE TABLE `terminalporsucursal` (
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `IdTerminal` int NOT NULL,
  `ValorRegistro` varchar(100) NOT NULL,
  `ImpresoraFactura` varchar(150) DEFAULT NULL,
  `UltimoDocFE` int NOT NULL,
  `UltimoDocND` int NOT NULL,
  `UltimoDocNC` int NOT NULL,
  `UltimoDocTE` int NOT NULL,
  `UltimoDocMR` int NOT NULL,
  `IdTipoDispositivo` int NOT NULL,
  `UltimoDocFEC` int NOT NULL,
  `AnchoLinea` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipocuentacontable`
--

CREATE TABLE `tipocuentacontable` (
  `IdTipoCuenta` int NOT NULL,
  `TipoSaldo` varchar(1) NOT NULL,
  `Descripcion` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipodecambiodolar`
--

CREATE TABLE `tipodecambiodolar` (
  `FechaTipoCambio` varchar(10) NOT NULL DEFAULT '',
  `ValorTipoCambio` decimal(13,5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipoidentificacion`
--

CREATE TABLE `tipoidentificacion` (
  `IdTipoIdentificacion` int NOT NULL,
  `Descripcion` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipomoneda`
--

CREATE TABLE `tipomoneda` (
  `IdTipoMoneda` int NOT NULL,
  `Descripcion` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipomovimientobanco`
--

CREATE TABLE `tipomovimientobanco` (
  `IdTipoMov` int NOT NULL,
  `DebeHaber` varchar(2) NOT NULL,
  `Descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipoparametrocontable`
--

CREATE TABLE `tipoparametrocontable` (
  `IdTipo` int NOT NULL,
  `Descripcion` varchar(200) NOT NULL,
  `MultiCuenta` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tipoproducto`
--

CREATE TABLE `tipoproducto` (
  `IdTipoProducto` int NOT NULL,
  `Descripcion` varchar(25) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `tiqueteordenservicio`
--

CREATE TABLE `tiqueteordenservicio` (
  `IdTiquete` int NOT NULL,
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `Impresora` varchar(100) NOT NULL,
  `lineas` blob,
  `Impreso` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `traslado`
--

CREATE TABLE `traslado` (
  `IdEmpresa` int NOT NULL,
  `IdTraslado` int NOT NULL,
  `IdUsuario` int NOT NULL,
  `IdSucursalOrigen` int NOT NULL,
  `IdSucursalDestino` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `Referencia` varchar(200) DEFAULT NULL,
  `Total` double NOT NULL,
  `IdAsiento` int NOT NULL,
  `Aplicado` bit(1) NOT NULL,
  `IdAplicadoPor` int DEFAULT NULL,
  `Nulo` bit(1) NOT NULL,
  `IdAnuladoPor` int DEFAULT NULL,
  `MotivoAnulacion` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `usuario`
--

CREATE TABLE `usuario` (
  `IdUsuario` int NOT NULL,
  `CodigoUsuario` varchar(10) NOT NULL,
  `Clave` varchar(1000) NOT NULL,
  `PermiteRegistrarDispositivo` tinyint(1) DEFAULT NULL,
  `PorcMaxDescuento` double NOT NULL,
  `IdEmpresa` int NOT NULL,
  `IdSucursal` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `vendedor`
--

CREATE TABLE `vendedor` (
  `IdEmpresa` int NOT NULL,
  `IdVendedor` int NOT NULL,
  `Nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `actividadeconomicaempresa`
--
ALTER TABLE `actividadeconomicaempresa`
  ADD PRIMARY KEY (`IdEmpresa`,`CodigoActividad`);

--
-- Indexes for table `ajusteinventario`
--
ALTER TABLE `ajusteinventario`
  ADD PRIMARY KEY (`IdAjuste`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdUsuario` (`IdUsuario`);

--
-- Indexes for table `apartado`
--
ALTER TABLE `apartado`
  ADD PRIMARY KEY (`IdApartado`),
  ADD UNIQUE KEY `unique_consec_index` (`IdEmpresa`,`IdSucursal`,`ConsecApartado`),
  ADD KEY `IdCliente` (`IdCliente`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdVendedor` (`IdVendedor`),
  ADD KEY `apartado_fecha_ind` (`Fecha`),
  ADD KEY `apartado_procesado_ind` (`Procesado`),
  ADD KEY `mov_apartado_ind` (`Procesado`);

--
-- Indexes for table `asiento`
--
ALTER TABLE `asiento`
  ADD PRIMARY KEY (`IdAsiento`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `asiento_fecha_ind` (`Fecha`);

--
-- Indexes for table `bancoadquiriente`
--
ALTER TABLE `bancoadquiriente`
  ADD PRIMARY KEY (`IdBanco`);

--
-- Indexes for table `barrio`
--
ALTER TABLE `barrio`
  ADD PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`,`IdBarrio`);

--
-- Indexes for table `cantfemensualempresa`
--
ALTER TABLE `cantfemensualempresa`
  ADD PRIMARY KEY (`IdEmpresa`,`IdMes`,`IdAnio`);

--
-- Indexes for table `canton`
--
ALTER TABLE `canton`
  ADD PRIMARY KEY (`IdProvincia`,`IdCanton`);

--
-- Indexes for table `catalogocontable`
--
ALTER TABLE `catalogocontable`
  ADD PRIMARY KEY (`IdCuenta`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdTipoCuenta` (`IdTipoCuenta`),
  ADD KEY `IdClaseCuenta` (`IdClaseCuenta`);

--
-- Indexes for table `catalogoreporte`
--
ALTER TABLE `catalogoreporte`
  ADD PRIMARY KEY (`IdReporte`);

--
-- Indexes for table `cierrecaja`
--
ALTER TABLE `cierrecaja`
  ADD PRIMARY KEY (`IdCierre`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `cierre_index` (`FechaCierre`);

--
-- Indexes for table `clasecuentacontable`
--
ALTER TABLE `clasecuentacontable`
  ADD PRIMARY KEY (`IdClaseCuenta`);

--
-- Indexes for table `clasificacionproducto`
--
ALTER TABLE `clasificacionproducto`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`IdCliente`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdTipoIdentificacion` (`IdTipoIdentificacion`);

--
-- Indexes for table `compra`
--
ALTER TABLE `compra`
  ADD PRIMARY KEY (`IdCompra`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdProveedor` (`IdProveedor`),
  ADD KEY `cxp_index` (`IdCxP`),
  ADD KEY `compra_fecha_ind` (`Fecha`),
  ADD KEY `compra_procesado_ind` (`IdCondicionVenta`,`Procesado`);

--
-- Indexes for table `condicionventa`
--
ALTER TABLE `condicionventa`
  ADD PRIMARY KEY (`IdCondicionVenta`);

--
-- Indexes for table `credencialeshacienda`
--
ALTER TABLE `credencialeshacienda`
  ADD PRIMARY KEY (`IdEmpresa`);

--
-- Indexes for table `cuentabanco`
--
ALTER TABLE `cuentabanco`
  ADD PRIMARY KEY (`IdCuenta`),
  ADD KEY `IdEmpresa` (`IdEmpresa`);

--
-- Indexes for table `cuentaegreso`
--
ALTER TABLE `cuentaegreso`
  ADD PRIMARY KEY (`IdCuenta`);

--
-- Indexes for table `cuentaingreso`
--
ALTER TABLE `cuentaingreso`
  ADD PRIMARY KEY (`IdCuenta`);

--
-- Indexes for table `cuentaporcobrar`
--
ALTER TABLE `cuentaporcobrar`
  ADD PRIMARY KEY (`IdCxC`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`),
  ADD KEY `cxc_fecha_ind` (`Fecha`);

--
-- Indexes for table `cuentaporpagar`
--
ALTER TABLE `cuentaporpagar`
  ADD PRIMARY KEY (`IdCxP`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`),
  ADD KEY `cxp_fecha_ind` (`Fecha`);

--
-- Indexes for table `desglosepagoapartado`
--
ALTER TABLE `desglosepagoapartado`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdApartado` (`IdApartado`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagocompra`
--
ALTER TABLE `desglosepagocompra`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdCompra` (`IdCompra`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagofactura`
--
ALTER TABLE `desglosepagofactura`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdFactura` (`IdFactura`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagomovimientoapartado`
--
ALTER TABLE `desglosepagomovimientoapartado`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdMovApartado` (`IdMovApartado`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagomovimientocuentaporcobrar`
--
ALTER TABLE `desglosepagomovimientocuentaporcobrar`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdMovCxC` (`IdMovCxC`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagomovimientocuentaporpagar`
--
ALTER TABLE `desglosepagomovimientocuentaporpagar`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdMovCxP` (`IdMovCxP`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagomovimientoordenservicio`
--
ALTER TABLE `desglosepagomovimientoordenservicio`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdMovOrden` (`IdMovOrden`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `desglosepagoordenservicio`
--
ALTER TABLE `desglosepagoordenservicio`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdOrden` (`IdOrden`),
  ADD KEY `IdFormaPago` (`IdFormaPago`),
  ADD KEY `IdTipoMoneda` (`IdTipoMoneda`);

--
-- Indexes for table `detalleajusteinventario`
--
ALTER TABLE `detalleajusteinventario`
  ADD PRIMARY KEY (`IdAjuste`,`IdProducto`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalleapartado`
--
ALTER TABLE `detalleapartado`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdApartado` (`IdApartado`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalleasiento`
--
ALTER TABLE `detalleasiento`
  ADD PRIMARY KEY (`IdAsiento`,`Linea`),
  ADD KEY `IdCuenta` (`IdCuenta`);

--
-- Indexes for table `detallecompra`
--
ALTER TABLE `detallecompra`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdCompra` (`IdCompra`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalledevolucioncliente`
--
ALTER TABLE `detalledevolucioncliente`
  ADD PRIMARY KEY (`IdDevolucion`,`IdProducto`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalleefectivocierrecaja`
--
ALTER TABLE `detalleefectivocierrecaja`
  ADD PRIMARY KEY (`IdCierre`,`Denominacion`);

--
-- Indexes for table `detallefactura`
--
ALTER TABLE `detallefactura`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdProducto` (`IdProducto`),
  ADD KEY `IdFactura` (`IdFactura`);

--
-- Indexes for table `detallefacturacompra`
--
ALTER TABLE `detallefacturacompra`
  ADD PRIMARY KEY (`IdFactCompra`,`Linea`);

--
-- Indexes for table `detallemovimientocierrecaja`
--
ALTER TABLE `detallemovimientocierrecaja`
  ADD PRIMARY KEY (`Consecutivo`),
  ADD KEY `IdCierre` (`IdCierre`);

--
-- Indexes for table `detalleordencompra`
--
ALTER TABLE `detalleordencompra`
  ADD PRIMARY KEY (`IdOrdenCompra`,`IdProducto`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalleordenservicio`
--
ALTER TABLE `detalleordenservicio`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdOrden` (`IdOrden`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalleproforma`
--
ALTER TABLE `detalleproforma`
  ADD PRIMARY KEY (`IdConsecutivo`),
  ADD KEY `IdProforma` (`IdProforma`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `detalletraslado`
--
ALTER TABLE `detalletraslado`
  ADD PRIMARY KEY (`IdTraslado`,`IdProducto`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `devolucioncliente`
--
ALTER TABLE `devolucioncliente`
  ADD PRIMARY KEY (`IdDevolucion`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdFactura` (`IdFactura`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdCliente` (`IdCliente`),
  ADD KEY `devol_cliente_fecha_ind` (`Fecha`);

--
-- Indexes for table `distrito`
--
ALTER TABLE `distrito`
  ADD PRIMARY KEY (`IdProvincia`,`IdCanton`,`IdDistrito`);

--
-- Indexes for table `documentoelectronico`
--
ALTER TABLE `documentoelectronico`
  ADD PRIMARY KEY (`IdDocumento`),
  ADD KEY `ClaveNumerica` (`ClaveNumerica`),
  ADD KEY `ClaveNumerica_2` (`ClaveNumerica`,`Consecutivo`),
  ADD KEY `doc_date_index` (`Fecha`),
  ADD KEY `IdEmpresa_2` (`IdEmpresa`,`IdSucursal`);
ALTER TABLE `documentoelectronico` ADD FULLTEXT KEY `nombre_receptor_fulltext_idx` (`NombreReceptor`);

--
-- Indexes for table `egreso`
--
ALTER TABLE `egreso`
  ADD PRIMARY KEY (`IdEgreso`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdCuenta` (`IdCuenta`),
  ADD KEY `egreso_fecha_ind` (`Fecha`),
  ADD KEY `egreso_procesado_ind` (`Procesado`);

--
-- Indexes for table `empresa`
--
ALTER TABLE `empresa`
  ADD PRIMARY KEY (`IdEmpresa`),
  ADD KEY `Identificacion` (`Identificacion`),
  ADD KEY `TipoContrato` (`TipoContrato`);

--
-- Indexes for table `existenciaporsucursal`
--
ALTER TABLE `existenciaporsucursal`
  ADD PRIMARY KEY (`IdEmpresa`,`IdSucursal`,`IdProducto`),
  ADD KEY `IdProducto` (`IdProducto`);

--
-- Indexes for table `factura`
--
ALTER TABLE `factura`
  ADD PRIMARY KEY (`IdFactura`),
  ADD UNIQUE KEY `unique_consec_index` (`IdEmpresa`,`IdSucursal`,`ConsecFactura`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdCliente` (`IdCliente`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdCondicionVenta` (`IdCondicionVenta`),
  ADD KEY `IdVendedor` (`IdVendedor`),
  ADD KEY `cxc_index` (`IdCxC`),
  ADD KEY `fact_fecha_ind` (`Fecha`),
  ADD KEY `factura_procesado_ind` (`IdCondicionVenta`,`Procesado`);

--
-- Indexes for table `facturacompra`
--
ALTER TABLE `facturacompra`
  ADD PRIMARY KEY (`IdFactCompra`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdUsuario` (`IdUsuario`);

--
-- Indexes for table `formapago`
--
ALTER TABLE `formapago`
  ADD PRIMARY KEY (`IdFormaPago`);

--
-- Indexes for table `ingreso`
--
ALTER TABLE `ingreso`
  ADD PRIMARY KEY (`IdIngreso`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdCuenta` (`IdCuenta`),
  ADD KEY `ingreso_fecha_ind` (`Fecha`),
  ADD KEY `ingreso_procesado_ind` (`Procesado`);

--
-- Indexes for table `linea`
--
ALTER TABLE `linea`
  ADD PRIMARY KEY (`IdLinea`),
  ADD KEY `IdEmpresa` (`IdEmpresa`);

--
-- Indexes for table `lineaporsucursal`
--
ALTER TABLE `lineaporsucursal`
  ADD PRIMARY KEY (`IdEmpresa`,`IdSucursal`,`IdLinea`),
  ADD KEY `IdLinea` (`IdLinea`);

--
-- Indexes for table `movimientoapartado`
--
ALTER TABLE `movimientoapartado`
  ADD PRIMARY KEY (`IdMovApartado`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `IdApartado` (`IdApartado`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `mov_apartado_procesado_ind` (`Procesado`);

--
-- Indexes for table `movimientobanco`
--
ALTER TABLE `movimientobanco`
  ADD PRIMARY KEY (`IdMov`),
  ADD KEY `IdCuenta` (`IdCuenta`),
  ADD KEY `IdTipo` (`IdTipo`),
  ADD KEY `mov_banco_fecha_ind` (`Fecha`);

--
-- Indexes for table `movimientocuentaporcobrar`
--
ALTER TABLE `movimientocuentaporcobrar`
  ADD PRIMARY KEY (`IdMovCxC`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `mov_cxc_fecha_ind` (`Fecha`),
  ADD KEY `mov_cxc_procesado_ind` (`Procesado`);

--
-- Indexes for table `movimientocuentaporpagar`
--
ALTER TABLE `movimientocuentaporpagar`
  ADD PRIMARY KEY (`IdMovCxP`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `mov_cxp_fecha_ind` (`Fecha`),
  ADD KEY `mov_cxp_procesado_ind` (`Procesado`);

--
-- Indexes for table `movimientoordenservicio`
--
ALTER TABLE `movimientoordenservicio`
  ADD PRIMARY KEY (`IdMovOrden`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`),
  ADD KEY `IdOrden` (`IdOrden`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `mov_orden_serv_proc_ind` (`Procesado`);

--
-- Indexes for table `movimientoproducto`
--
ALTER TABLE `movimientoproducto`
  ADD PRIMARY KEY (`IdMovimiento`);

--
-- Indexes for table `ordencompra`
--
ALTER TABLE `ordencompra`
  ADD PRIMARY KEY (`IdOrdenCompra`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdProveedor` (`IdProveedor`),
  ADD KEY `IdCondicionVenta` (`IdCondicionVenta`);

--
-- Indexes for table `ordenservicio`
--
ALTER TABLE `ordenservicio`
  ADD PRIMARY KEY (`IdOrden`),
  ADD UNIQUE KEY `unique_consec_index` (`IdEmpresa`,`IdSucursal`,`ConsecOrdenServicio`),
  ADD KEY `IdCliente` (`IdCliente`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdVendedor` (`IdVendedor`),
  ADD KEY `orden_serv_fecha_ind` (`Fecha`),
  ADD KEY `orden_servicio_procesado_ind` (`Procesado`);

--
-- Indexes for table `padron`
--
ALTER TABLE `padron`
  ADD PRIMARY KEY (`Identificacion`);

--
-- Indexes for table `parametrocontable`
--
ALTER TABLE `parametrocontable`
  ADD PRIMARY KEY (`IdParametro`),
  ADD KEY `IdTipo` (`IdTipo`),
  ADD KEY `IdCuenta` (`IdCuenta`);

--
-- Indexes for table `parametroexoneracion`
--
ALTER TABLE `parametroexoneracion`
  ADD PRIMARY KEY (`IdTipoExoneracion`);

--
-- Indexes for table `parametroimpuesto`
--
ALTER TABLE `parametroimpuesto`
  ADD PRIMARY KEY (`IdImpuesto`);

--
-- Indexes for table `parametrosistema`
--
ALTER TABLE `parametrosistema`
  ADD PRIMARY KEY (`IdParametro`);

--
-- Indexes for table `planfacturacion`
--
ALTER TABLE `planfacturacion`
  ADD PRIMARY KEY (`IdPlan`);

--
-- Indexes for table `producto`
--
ALTER TABLE `producto`
  ADD PRIMARY KEY (`IdProducto`),
  ADD UNIQUE KEY `empresa_codigo_idx` (`IdEmpresa`,`Codigo`),
  ADD UNIQUE KEY `empresa_codigo_prov_idx` (`IdEmpresa`,`CodigoProveedor`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdLinea` (`IdLinea`),
  ADD KEY `IdProveedor` (`IdProveedor`),
  ADD KEY `Tipo` (`Tipo`),
  ADD KEY `IdImpuesto` (`IdImpuesto`),
  ADD KEY `empresa_linea_tipo_key` (`IdEmpresa`,`IdLinea`,`Tipo`);
ALTER TABLE `producto` ADD FULLTEXT KEY `descripcion_fulltext_idx` (`Descripcion`);

--
-- Indexes for table `proforma`
--
ALTER TABLE `proforma`
  ADD PRIMARY KEY (`IdProforma`),
  ADD UNIQUE KEY `unique_consec_index` (`IdEmpresa`,`IdSucursal`,`ConsecProforma`),
  ADD KEY `IdCliente` (`IdCliente`),
  ADD KEY `IdUsuario` (`IdUsuario`),
  ADD KEY `IdVendedor` (`IdVendedor`),
  ADD KEY `proforma_fecha_ind` (`Fecha`);

--
-- Indexes for table `proveedor`
--
ALTER TABLE `proveedor`
  ADD PRIMARY KEY (`IdProveedor`),
  ADD KEY `IdEmpresa` (`IdEmpresa`);

--
-- Indexes for table `provincia`
--
ALTER TABLE `provincia`
  ADD PRIMARY KEY (`IdProvincia`);

--
-- Indexes for table `puntodeservicio`
--
ALTER TABLE `puntodeservicio`
  ADD PRIMARY KEY (`IdPunto`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`);

--
-- Indexes for table `registroautenticacion`
--
ALTER TABLE `registroautenticacion`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `reporteporempresa`
--
ALTER TABLE `reporteporempresa`
  ADD PRIMARY KEY (`IdEmpresa`,`IdReporte`),
  ADD KEY `IdReporte` (`IdReporte`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`IdRole`);

--
-- Indexes for table `roleporempresa`
--
ALTER TABLE `roleporempresa`
  ADD PRIMARY KEY (`IdEmpresa`,`IdRole`);

--
-- Indexes for table `roleporusuario`
--
ALTER TABLE `roleporusuario`
  ADD PRIMARY KEY (`IdUsuario`,`IdRole`),
  ADD KEY `IdRole` (`IdRole`);

--
-- Indexes for table `saldomensualcontable`
--
ALTER TABLE `saldomensualcontable`
  ADD PRIMARY KEY (`IdCuenta`,`Mes`,`Annio`);

--
-- Indexes for table `sucursalporempresa`
--
ALTER TABLE `sucursalporempresa`
  ADD PRIMARY KEY (`IdEmpresa`,`IdSucursal`);

--
-- Indexes for table `terminalporsucursal`
--
ALTER TABLE `terminalporsucursal`
  ADD PRIMARY KEY (`IdEmpresa`,`IdSucursal`,`IdTerminal`);

--
-- Indexes for table `tipocuentacontable`
--
ALTER TABLE `tipocuentacontable`
  ADD PRIMARY KEY (`IdTipoCuenta`);

--
-- Indexes for table `tipodecambiodolar`
--
ALTER TABLE `tipodecambiodolar`
  ADD PRIMARY KEY (`FechaTipoCambio`);

--
-- Indexes for table `tipoidentificacion`
--
ALTER TABLE `tipoidentificacion`
  ADD PRIMARY KEY (`IdTipoIdentificacion`);

--
-- Indexes for table `tipomoneda`
--
ALTER TABLE `tipomoneda`
  ADD PRIMARY KEY (`IdTipoMoneda`);

--
-- Indexes for table `tipomovimientobanco`
--
ALTER TABLE `tipomovimientobanco`
  ADD PRIMARY KEY (`IdTipoMov`);

--
-- Indexes for table `tipoparametrocontable`
--
ALTER TABLE `tipoparametrocontable`
  ADD PRIMARY KEY (`IdTipo`);

--
-- Indexes for table `tipoproducto`
--
ALTER TABLE `tipoproducto`
  ADD PRIMARY KEY (`IdTipoProducto`);

--
-- Indexes for table `tiqueteordenservicio`
--
ALTER TABLE `tiqueteordenservicio`
  ADD PRIMARY KEY (`IdTiquete`),
  ADD KEY `IdEmpresa` (`IdEmpresa`,`IdSucursal`);

--
-- Indexes for table `traslado`
--
ALTER TABLE `traslado`
  ADD PRIMARY KEY (`IdTraslado`),
  ADD KEY `IdEmpresa` (`IdEmpresa`),
  ADD KEY `IdUsuario` (`IdUsuario`);

--
-- Indexes for table `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`IdUsuario`);

--
-- Indexes for table `vendedor`
--
ALTER TABLE `vendedor`
  ADD PRIMARY KEY (`IdVendedor`),
  ADD KEY `IdEmpresa` (`IdEmpresa`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `ajusteinventario`
--
ALTER TABLE `ajusteinventario`
  MODIFY `IdAjuste` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `apartado`
--
ALTER TABLE `apartado`
  MODIFY `IdApartado` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `asiento`
--
ALTER TABLE `asiento`
  MODIFY `IdAsiento` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `bancoadquiriente`
--
ALTER TABLE `bancoadquiriente`
  MODIFY `IdBanco` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `catalogocontable`
--
ALTER TABLE `catalogocontable`
  MODIFY `IdCuenta` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cierrecaja`
--
ALTER TABLE `cierrecaja`
  MODIFY `IdCierre` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cliente`
--
ALTER TABLE `cliente`
  MODIFY `IdCliente` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `compra`
--
ALTER TABLE `compra`
  MODIFY `IdCompra` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cuentabanco`
--
ALTER TABLE `cuentabanco`
  MODIFY `IdCuenta` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cuentaegreso`
--
ALTER TABLE `cuentaegreso`
  MODIFY `IdCuenta` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cuentaingreso`
--
ALTER TABLE `cuentaingreso`
  MODIFY `IdCuenta` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cuentaporcobrar`
--
ALTER TABLE `cuentaporcobrar`
  MODIFY `IdCxC` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cuentaporpagar`
--
ALTER TABLE `cuentaporpagar`
  MODIFY `IdCxP` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagoapartado`
--
ALTER TABLE `desglosepagoapartado`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagocompra`
--
ALTER TABLE `desglosepagocompra`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagofactura`
--
ALTER TABLE `desglosepagofactura`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagomovimientoapartado`
--
ALTER TABLE `desglosepagomovimientoapartado`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagomovimientocuentaporcobrar`
--
ALTER TABLE `desglosepagomovimientocuentaporcobrar`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagomovimientocuentaporpagar`
--
ALTER TABLE `desglosepagomovimientocuentaporpagar`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagomovimientoordenservicio`
--
ALTER TABLE `desglosepagomovimientoordenservicio`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `desglosepagoordenservicio`
--
ALTER TABLE `desglosepagoordenservicio`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detalleapartado`
--
ALTER TABLE `detalleapartado`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detallecompra`
--
ALTER TABLE `detallecompra`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detallefactura`
--
ALTER TABLE `detallefactura`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detallemovimientocierrecaja`
--
ALTER TABLE `detallemovimientocierrecaja`
  MODIFY `Consecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detalleordenservicio`
--
ALTER TABLE `detalleordenservicio`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `detalleproforma`
--
ALTER TABLE `detalleproforma`
  MODIFY `IdConsecutivo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `devolucioncliente`
--
ALTER TABLE `devolucioncliente`
  MODIFY `IdDevolucion` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `documentoelectronico`
--
ALTER TABLE `documentoelectronico`
  MODIFY `IdDocumento` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `egreso`
--
ALTER TABLE `egreso`
  MODIFY `IdEgreso` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `empresa`
--
ALTER TABLE `empresa`
  MODIFY `IdEmpresa` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `factura`
--
ALTER TABLE `factura`
  MODIFY `IdFactura` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `facturacompra`
--
ALTER TABLE `facturacompra`
  MODIFY `IdFactCompra` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ingreso`
--
ALTER TABLE `ingreso`
  MODIFY `IdIngreso` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `linea`
--
ALTER TABLE `linea`
  MODIFY `IdLinea` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientoapartado`
--
ALTER TABLE `movimientoapartado`
  MODIFY `IdMovApartado` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientobanco`
--
ALTER TABLE `movimientobanco`
  MODIFY `IdMov` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientocuentaporcobrar`
--
ALTER TABLE `movimientocuentaporcobrar`
  MODIFY `IdMovCxC` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientocuentaporpagar`
--
ALTER TABLE `movimientocuentaporpagar`
  MODIFY `IdMovCxP` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientoordenservicio`
--
ALTER TABLE `movimientoordenservicio`
  MODIFY `IdMovOrden` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `movimientoproducto`
--
ALTER TABLE `movimientoproducto`
  MODIFY `IdMovimiento` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ordencompra`
--
ALTER TABLE `ordencompra`
  MODIFY `IdOrdenCompra` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ordenservicio`
--
ALTER TABLE `ordenservicio`
  MODIFY `IdOrden` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `parametrocontable`
--
ALTER TABLE `parametrocontable`
  MODIFY `IdParametro` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `parametroexoneracion`
--
ALTER TABLE `parametroexoneracion`
  MODIFY `IdTipoExoneracion` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `parametroimpuesto`
--
ALTER TABLE `parametroimpuesto`
  MODIFY `IdImpuesto` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `planfacturacion`
--
ALTER TABLE `planfacturacion`
  MODIFY `IdPlan` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `producto`
--
ALTER TABLE `producto`
  MODIFY `IdProducto` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `proforma`
--
ALTER TABLE `proforma`
  MODIFY `IdProforma` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `proveedor`
--
ALTER TABLE `proveedor`
  MODIFY `IdProveedor` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `puntodeservicio`
--
ALTER TABLE `puntodeservicio`
  MODIFY `IdPunto` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `IdRole` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tipomoneda`
--
ALTER TABLE `tipomoneda`
  MODIFY `IdTipoMoneda` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tipomovimientobanco`
--
ALTER TABLE `tipomovimientobanco`
  MODIFY `IdTipoMov` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tipoparametrocontable`
--
ALTER TABLE `tipoparametrocontable`
  MODIFY `IdTipo` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tipoproducto`
--
ALTER TABLE `tipoproducto`
  MODIFY `IdTipoProducto` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tiqueteordenservicio`
--
ALTER TABLE `tiqueteordenservicio`
  MODIFY `IdTiquete` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `traslado`
--
ALTER TABLE `traslado`
  MODIFY `IdTraslado` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usuario`
--
ALTER TABLE `usuario`
  MODIFY `IdUsuario` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `vendedor`
--
ALTER TABLE `vendedor`
  MODIFY `IdVendedor` int NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `actividadeconomicaempresa`
--
ALTER TABLE `actividadeconomicaempresa`
  ADD CONSTRAINT `actividadeconomicaempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `ajusteinventario`
--
ALTER TABLE `ajusteinventario`
  ADD CONSTRAINT `ajusteinventario_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ajusteinventario_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `apartado`
--
ALTER TABLE `apartado`
  ADD CONSTRAINT `apartado_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `apartado_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `apartado_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `apartado_ibfk_4` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Constraints for table `asiento`
--
ALTER TABLE `asiento`
  ADD CONSTRAINT `asiento_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `barrio`
--
ALTER TABLE `barrio`
  ADD CONSTRAINT `barrio_ibfk_1` FOREIGN KEY (`IdProvincia`,`IdCanton`,`IdDistrito`) REFERENCES `distrito` (`IdProvincia`, `IdCanton`, `IdDistrito`);

--
-- Constraints for table `cantfemensualempresa`
--
ALTER TABLE `cantfemensualempresa`
  ADD CONSTRAINT `cantfemensualempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `canton`
--
ALTER TABLE `canton`
  ADD CONSTRAINT `canton_ibfk_1` FOREIGN KEY (`IdProvincia`) REFERENCES `provincia` (`IdProvincia`);

--
-- Constraints for table `catalogocontable`
--
ALTER TABLE `catalogocontable`
  ADD CONSTRAINT `catalogocontable_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `catalogocontable_ibfk_2` FOREIGN KEY (`IdTipoCuenta`) REFERENCES `tipocuentacontable` (`IdTipoCuenta`),
  ADD CONSTRAINT `catalogocontable_ibfk_3` FOREIGN KEY (`IdClaseCuenta`) REFERENCES `clasecuentacontable` (`IdClaseCuenta`);

--
-- Constraints for table `cierrecaja`
--
ALTER TABLE `cierrecaja`
  ADD CONSTRAINT `cierrecaja_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`);

--
-- Constraints for table `cliente`
--
ALTER TABLE `cliente`
  ADD CONSTRAINT `cliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `cliente_ibfk_2` FOREIGN KEY (`IdTipoIdentificacion`) REFERENCES `tipoidentificacion` (`IdTipoIdentificacion`);

--
-- Constraints for table `compra`
--
ALTER TABLE `compra`
  ADD CONSTRAINT `compra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `compra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `compra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `compra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`);

--
-- Constraints for table `credencialeshacienda`
--
ALTER TABLE `credencialeshacienda`
  ADD CONSTRAINT `credencialeshacienda_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `cuentabanco`
--
ALTER TABLE `cuentabanco`
  ADD CONSTRAINT `cuentabanco_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `cuentaporcobrar`
--
ALTER TABLE `cuentaporcobrar`
  ADD CONSTRAINT `cuentaporcobrar_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `cuentaporcobrar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `cuentaporcobrar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `cuentaporpagar`
--
ALTER TABLE `cuentaporpagar`
  ADD CONSTRAINT `cuentaporpagar_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `cuentaporpagar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `cuentaporpagar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagoapartado`
--
ALTER TABLE `desglosepagoapartado`
  ADD CONSTRAINT `desglosepagoapartado_ibfk_1` FOREIGN KEY (`IdApartado`) REFERENCES `apartado` (`IdApartado`),
  ADD CONSTRAINT `desglosepagoapartado_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagoapartado_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagocompra`
--
ALTER TABLE `desglosepagocompra`
  ADD CONSTRAINT `desglosepagocompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  ADD CONSTRAINT `desglosepagocompra_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagocompra_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagomovimientoapartado`
--
ALTER TABLE `desglosepagomovimientoapartado`
  ADD CONSTRAINT `desglosepagomovimientoapartado_ibfk_1` FOREIGN KEY (`IdMovApartado`) REFERENCES `movimientoapartado` (`IdMovApartado`),
  ADD CONSTRAINT `desglosepagomovimientoapartado_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientoapartado_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagomovimientocuentaporcobrar`
--
ALTER TABLE `desglosepagomovimientocuentaporcobrar`
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdMovCxC`) REFERENCES `movimientocuentaporcobrar` (`IdMovCxC`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporcobrar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagomovimientocuentaporpagar`
--
ALTER TABLE `desglosepagomovimientocuentaporpagar`
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdMovCxP`) REFERENCES `movimientocuentaporpagar` (`IdMovCxP`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientocuentaporpagar_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagomovimientoordenservicio`
--
ALTER TABLE `desglosepagomovimientoordenservicio`
  ADD CONSTRAINT `desglosepagomovimientoordenservicio_ibfk_1` FOREIGN KEY (`IdMovOrden`) REFERENCES `movimientoordenservicio` (`IdMovOrden`),
  ADD CONSTRAINT `desglosepagomovimientoordenservicio_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagomovimientoordenservicio_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `desglosepagoordenservicio`
--
ALTER TABLE `desglosepagoordenservicio`
  ADD CONSTRAINT `desglosepagoordenservicio_ibfk_1` FOREIGN KEY (`IdOrden`) REFERENCES `ordenservicio` (`IdOrden`),
  ADD CONSTRAINT `desglosepagoordenservicio_ibfk_2` FOREIGN KEY (`IdFormaPago`) REFERENCES `formapago` (`IdFormaPago`),
  ADD CONSTRAINT `desglosepagoordenservicio_ibfk_3` FOREIGN KEY (`IdTipoMoneda`) REFERENCES `tipomoneda` (`IdTipoMoneda`);

--
-- Constraints for table `detalleajusteinventario`
--
ALTER TABLE `detalleajusteinventario`
  ADD CONSTRAINT `detalleajusteinventario_ibfk_1` FOREIGN KEY (`IdAjuste`) REFERENCES `ajusteinventario` (`IdAjuste`),
  ADD CONSTRAINT `detalleajusteinventario_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalleapartado`
--
ALTER TABLE `detalleapartado`
  ADD CONSTRAINT `detalleapartado_ibfk_1` FOREIGN KEY (`IdApartado`) REFERENCES `apartado` (`IdApartado`),
  ADD CONSTRAINT `detalleapartado_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalleasiento`
--
ALTER TABLE `detalleasiento`
  ADD CONSTRAINT `detalleasiento_ibfk_1` FOREIGN KEY (`IdAsiento`) REFERENCES `asiento` (`IdAsiento`),
  ADD CONSTRAINT `detalleasiento_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Constraints for table `detallecompra`
--
ALTER TABLE `detallecompra`
  ADD CONSTRAINT `detallecompra_ibfk_1` FOREIGN KEY (`IdCompra`) REFERENCES `compra` (`IdCompra`),
  ADD CONSTRAINT `detallecompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalledevolucioncliente`
--
ALTER TABLE `detalledevolucioncliente`
  ADD CONSTRAINT `detalledevolucioncliente_ibfk_1` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalleefectivocierrecaja`
--
ALTER TABLE `detalleefectivocierrecaja`
  ADD CONSTRAINT `detalleefectivocierrecaja_ibfk_1` FOREIGN KEY (`IdCierre`) REFERENCES `cierrecaja` (`IdCierre`);

--
-- Constraints for table `detallefactura`
--
ALTER TABLE `detallefactura`
  ADD CONSTRAINT `detallefactura_ibfk_1` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  ADD CONSTRAINT `detallefactura_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detallefacturacompra`
--
ALTER TABLE `detallefacturacompra`
  ADD CONSTRAINT `detallefacturacompra_ibfk_1` FOREIGN KEY (`IdFactCompra`) REFERENCES `facturacompra` (`IdFactCompra`);

--
-- Constraints for table `detallemovimientocierrecaja`
--
ALTER TABLE `detallemovimientocierrecaja`
  ADD CONSTRAINT `detallemovimientocierrecaja_ibfk_1` FOREIGN KEY (`IdCierre`) REFERENCES `cierrecaja` (`IdCierre`);

--
-- Constraints for table `detalleordencompra`
--
ALTER TABLE `detalleordencompra`
  ADD CONSTRAINT `detalleordencompra_ibfk_1` FOREIGN KEY (`IdOrdenCompra`) REFERENCES `ordencompra` (`IdOrdenCompra`),
  ADD CONSTRAINT `detalleordencompra_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalleordenservicio`
--
ALTER TABLE `detalleordenservicio`
  ADD CONSTRAINT `detalleordenservicio_ibfk_1` FOREIGN KEY (`IdOrden`) REFERENCES `ordenservicio` (`IdOrden`),
  ADD CONSTRAINT `detalleordenservicio_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalleproforma`
--
ALTER TABLE `detalleproforma`
  ADD CONSTRAINT `detalleproforma_ibfk_1` FOREIGN KEY (`IdProforma`) REFERENCES `proforma` (`IdProforma`),
  ADD CONSTRAINT `detalleproforma_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `detalletraslado`
--
ALTER TABLE `detalletraslado`
  ADD CONSTRAINT `detalletraslado_ibfk_1` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `devolucioncliente`
--
ALTER TABLE `devolucioncliente`
  ADD CONSTRAINT `devolucioncliente_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `devolucioncliente_ibfk_2` FOREIGN KEY (`IdFactura`) REFERENCES `factura` (`IdFactura`),
  ADD CONSTRAINT `devolucioncliente_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `devolucioncliente_ibfk_4` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`);

--
-- Constraints for table `distrito`
--
ALTER TABLE `distrito`
  ADD CONSTRAINT `distrito_ibfk_1` FOREIGN KEY (`IdProvincia`,`IdCanton`) REFERENCES `canton` (`IdProvincia`, `IdCanton`);

--
-- Constraints for table `documentoelectronico`
--
ALTER TABLE `documentoelectronico`
  ADD CONSTRAINT `documentoelectronico_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `egreso`
--
ALTER TABLE `egreso`
  ADD CONSTRAINT `egreso_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `egreso_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentaegreso` (`IdCuenta`);

--
-- Constraints for table `empresa`
--
ALTER TABLE `empresa`
  ADD CONSTRAINT `empresa_ibfk_1` FOREIGN KEY (`TipoContrato`) REFERENCES `planfacturacion` (`IdPlan`);

--
-- Constraints for table `existenciaporsucursal`
--
ALTER TABLE `existenciaporsucursal`
  ADD CONSTRAINT `existenciaporsucursal_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `existenciaporsucursal_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `producto` (`IdProducto`);

--
-- Constraints for table `factura`
--
ALTER TABLE `factura`
  ADD CONSTRAINT `factura_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `factura_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `factura_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `factura_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`),
  ADD CONSTRAINT `factura_ibfk_5` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Constraints for table `facturacompra`
--
ALTER TABLE `facturacompra`
  ADD CONSTRAINT `facturacompra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `facturacompra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `linea`
--
ALTER TABLE `linea`
  ADD CONSTRAINT `linea_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `lineaporsucursal`
--
ALTER TABLE `lineaporsucursal`
  ADD CONSTRAINT `lineaporsucursal_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `lineaporsucursal_ibfk_2` FOREIGN KEY (`IdLinea`) REFERENCES `linea` (`IdLinea`);

--
-- Constraints for table `movimientoapartado`
--
ALTER TABLE `movimientoapartado`
  ADD CONSTRAINT `movimientoapartado_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `movimientoapartado_ibfk_2` FOREIGN KEY (`IdApartado`) REFERENCES `apartado` (`IdApartado`),
  ADD CONSTRAINT `movimientoapartado_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `movimientobanco`
--
ALTER TABLE `movimientobanco`
  ADD CONSTRAINT `movimientobanco_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `cuentabanco` (`IdCuenta`),
  ADD CONSTRAINT `movimientobanco_ibfk_2` FOREIGN KEY (`IdTipo`) REFERENCES `tipomovimientobanco` (`IdTipoMov`);

--
-- Constraints for table `movimientocuentaporcobrar`
--
ALTER TABLE `movimientocuentaporcobrar`
  ADD CONSTRAINT `movimientocuentaporcobrar_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `movimientocuentaporcobrar_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `movimientocuentaporpagar`
--
ALTER TABLE `movimientocuentaporpagar`
  ADD CONSTRAINT `movimientocuentaporpagar_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `movimientoordenservicio`
--
ALTER TABLE `movimientoordenservicio`
  ADD CONSTRAINT `movimientoordenservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`),
  ADD CONSTRAINT `movimientoordenservicio_ibfk_2` FOREIGN KEY (`IdOrden`) REFERENCES `ordenservicio` (`IdOrden`),
  ADD CONSTRAINT `movimientoordenservicio_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `ordencompra`
--
ALTER TABLE `ordencompra`
  ADD CONSTRAINT `ordencompra_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ordencompra_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `ordencompra_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `ordencompra_ibfk_4` FOREIGN KEY (`IdCondicionVenta`) REFERENCES `condicionventa` (`IdCondicionVenta`);

--
-- Constraints for table `ordenservicio`
--
ALTER TABLE `ordenservicio`
  ADD CONSTRAINT `ordenservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `ordenservicio_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `ordenservicio_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `ordenservicio_ibfk_4` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Constraints for table `parametrocontable`
--
ALTER TABLE `parametrocontable`
  ADD CONSTRAINT `parametrocontable_ibfk_1` FOREIGN KEY (`IdTipo`) REFERENCES `tipoparametrocontable` (`IdTipo`),
  ADD CONSTRAINT `parametrocontable_ibfk_2` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Constraints for table `producto`
--
ALTER TABLE `producto`
  ADD CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `producto_ibfk_2` FOREIGN KEY (`IdLinea`) REFERENCES `linea` (`IdLinea`),
  ADD CONSTRAINT `producto_ibfk_3` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedor` (`IdProveedor`),
  ADD CONSTRAINT `producto_ibfk_4` FOREIGN KEY (`Tipo`) REFERENCES `tipoproducto` (`IdTipoProducto`),
  ADD CONSTRAINT `producto_ibfk_6` FOREIGN KEY (`IdImpuesto`) REFERENCES `parametroimpuesto` (`IdImpuesto`);

--
-- Constraints for table `proforma`
--
ALTER TABLE `proforma`
  ADD CONSTRAINT `proforma_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `proforma_ibfk_2` FOREIGN KEY (`IdCliente`) REFERENCES `cliente` (`IdCliente`),
  ADD CONSTRAINT `proforma_ibfk_3` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `proforma_ibfk_4` FOREIGN KEY (`IdVendedor`) REFERENCES `vendedor` (`IdVendedor`);

--
-- Constraints for table `proveedor`
--
ALTER TABLE `proveedor`
  ADD CONSTRAINT `proveedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `puntodeservicio`
--
ALTER TABLE `puntodeservicio`
  ADD CONSTRAINT `puntodeservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`);

--
-- Constraints for table `reporteporempresa`
--
ALTER TABLE `reporteporempresa`
  ADD CONSTRAINT `reporteporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `reporteporempresa_ibfk_2` FOREIGN KEY (`IdReporte`) REFERENCES `catalogoreporte` (`IdReporte`);

--
-- Constraints for table `roleporusuario`
--
ALTER TABLE `roleporusuario`
  ADD CONSTRAINT `roleporusuario_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`),
  ADD CONSTRAINT `roleporusuario_ibfk_2` FOREIGN KEY (`IdRole`) REFERENCES `role` (`IdRole`);

--
-- Constraints for table `saldomensualcontable`
--
ALTER TABLE `saldomensualcontable`
  ADD CONSTRAINT `saldomensualcontable_ibfk_1` FOREIGN KEY (`IdCuenta`) REFERENCES `catalogocontable` (`IdCuenta`);

--
-- Constraints for table `sucursalporempresa`
--
ALTER TABLE `sucursalporempresa`
  ADD CONSTRAINT `sucursalporempresa_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);

--
-- Constraints for table `terminalporsucursal`
--
ALTER TABLE `terminalporsucursal`
  ADD CONSTRAINT `terminalporsucursal_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `terminalporsucursal_ibfk_2` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`);

--
-- Constraints for table `tiqueteordenservicio`
--
ALTER TABLE `tiqueteordenservicio`
  ADD CONSTRAINT `tiqueteordenservicio_ibfk_1` FOREIGN KEY (`IdEmpresa`,`IdSucursal`) REFERENCES `sucursalporempresa` (`IdEmpresa`, `IdSucursal`);

--
-- Constraints for table `traslado`
--
ALTER TABLE `traslado`
  ADD CONSTRAINT `traslado_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`),
  ADD CONSTRAINT `traslado_ibfk_2` FOREIGN KEY (`IdUsuario`) REFERENCES `usuario` (`IdUsuario`);

--
-- Constraints for table `vendedor`
--
ALTER TABLE `vendedor`
  ADD CONSTRAINT `vendedor_ibfk_1` FOREIGN KEY (`IdEmpresa`) REFERENCES `empresa` (`IdEmpresa`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
