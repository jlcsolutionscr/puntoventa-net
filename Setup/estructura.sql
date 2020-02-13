-- phpMyAdmin SQL Dump
-- version 3.3.1
-- http://www.phpmyadmin.net
--
-- Host: 204.93.216.11:3306
-- Generation Time: Nov 29, 2019 at 06:59 AM
-- Server version: 5.6.40
-- PHP Version: 5.2.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: 'jasoncr_puntoventa'
--

-- --------------------------------------------------------

--
-- Table structure for table 'ajusteinventario'
--

CREATE TABLE ajusteinventario (
  IdEmpresa int(11) NOT NULL,
  IdAjuste int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  Fecha datetime NOT NULL,
  Descripcion varchar(500) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdAjuste),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'asiento'
--

CREATE TABLE asiento (
  IdEmpresa int(11) NOT NULL,
  IdAsiento int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  Detalle varchar(255) NOT NULL,
  Fecha datetime NOT NULL,
  TotalDebito double NOT NULL,
  TotalCredito double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdAsiento),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'bancoadquiriente'
--

CREATE TABLE bancoadquiriente (
  IdEmpresa int(11) NOT NULL,
  IdBanco int(11) NOT NULL AUTO_INCREMENT,
  Codigo varchar(30) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PorcentajeRetencion double NOT NULL,
  PorcentajeComision double NOT NULL,
  PRIMARY KEY (IdBanco)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'barrio'
--

CREATE TABLE barrio (
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  IdDistrito int(11) NOT NULL,
  IdBarrio int(11) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdProvincia,IdCanton,IdDistrito,IdBarrio)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cantfemensualempresa'
--

CREATE TABLE cantfemensualempresa (
  IdEmpresa int(11) NOT NULL,
  IdMes int(11) NOT NULL,
  IdAnio int(11) NOT NULL,
  CantidadDoc int(11) NOT NULL,
  PRIMARY KEY (IdEmpresa,IdMes,IdAnio)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'canton'
--

CREATE TABLE canton (
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdProvincia,IdCanton)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'catalogocontable'
--

CREATE TABLE catalogocontable (
  IdEmpresa int(11) NOT NULL,
  IdCuenta int(11) NOT NULL AUTO_INCREMENT,
  Nivel_1 varchar(1) NOT NULL,
  Nivel_2 varchar(2) DEFAULT NULL,
  Nivel_3 varchar(2) DEFAULT NULL,
  Nivel_4 varchar(2) DEFAULT NULL,
  Nivel_5 varchar(2) DEFAULT NULL,
  Nivel_6 varchar(2) DEFAULT NULL,
  Nivel_7 varchar(2) DEFAULT NULL,
  IdCuentaGrupo int(11) DEFAULT NULL,
  EsCuentaBalance bit(1) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  IdTipoCuenta int(11) NOT NULL,
  IdClaseCuenta int(11) NOT NULL,
  PermiteMovimiento bit(1) NOT NULL,
  PermiteSobrejiro bit(1) NOT NULL,
  SaldoActual double NOT NULL DEFAULT '0',
  TotalDebito double NOT NULL DEFAULT '0',
  TotalCredito double NOT NULL DEFAULT '0',
  PRIMARY KEY (IdCuenta),
  KEY IdEmpresa (IdEmpresa),
  KEY IdTipoCuenta (IdTipoCuenta),
  KEY IdClaseCuenta (IdClaseCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'catalogoreporte'
--

CREATE TABLE catalogoreporte (
  IdReporte int(11) NOT NULL,
  NombreReporte varchar(100) NOT NULL,
  PRIMARY KEY (IdReporte)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cierrecaja'
--

CREATE TABLE cierrecaja (
  IdEmpresa int(11) NOT NULL,
  IdCierre int(11) NOT NULL AUTO_INCREMENT,
  FechaCierre date DEFAULT NULL,
  FondoInicio double DEFAULT NULL,
  VentasContado double DEFAULT NULL,
  VentasCredito double DEFAULT NULL,
  VentasTarjeta double DEFAULT NULL,
  OtrasVentas double DEFAULT NULL,
  RetencionIVA double DEFAULT NULL,
  ComisionVT double DEFAULT NULL,
  LiquidacionTarjeta double DEFAULT NULL,
  IngresoCxCEfectivo double DEFAULT NULL,
  IngresoCxCTarjeta double DEFAULT NULL,
  DevolucionesProveedores double DEFAULT NULL,
  OtrosIngresos double DEFAULT NULL,
  ComprasContado double DEFAULT NULL,
  ComprasCredito double DEFAULT NULL,
  OtrasCompras double DEFAULT NULL,
  EgresoCxPEfectivo double DEFAULT NULL,
  DevolucionesClientes double DEFAULT NULL,
  OtrosEgresos double DEFAULT NULL,
  FondoCierre double DEFAULT NULL,
  PRIMARY KEY (IdCierre),
  KEY IdEmpresa (IdEmpresa),
  KEY cierre_index (FechaCierre)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'clasecuentacontable'
--

CREATE TABLE clasecuentacontable (
  IdClaseCuenta int(11) NOT NULL,
  Descripcion varchar(20) NOT NULL,
  PRIMARY KEY (IdClaseCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cliente'
--

CREATE TABLE cliente (
  IdEmpresa int(11) NOT NULL,
  IdCliente int(11) NOT NULL AUTO_INCREMENT,
  IdTipoIdentificacion int(11) NOT NULL,
  Identificacion varchar(20) NOT NULL,
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  IdDistrito int(11) NOT NULL,
  IdBarrio int(11) NOT NULL,
  Direccion varchar(160) NOT NULL,
  Nombre varchar(80) NOT NULL,
  NombreComercial varchar(80) DEFAULT NULL,
  Telefono varchar(20) DEFAULT NULL,
  Celular varchar(20) DEFAULT NULL,
  Fax varchar(20) DEFAULT NULL,
  CorreoElectronico varchar(200) DEFAULT NULL,
  IdVendedor int(11) DEFAULT NULL,
  IdTipoPrecio int(11) DEFAULT NULL,
  AplicaTasaDiferenciada int(11) NOT NULL,
  IdImpuesto int(11) DEFAULT NULL,
  IdTipoExoneracion int(11) NOT NULL,
  NumDocExoneracion varchar(100) DEFAULT NULL,
  NombreInstExoneracion varchar(100) DEFAULT NULL,
  FechaEmisionDoc datetime NOT NULL,
  PorcentajeExoneracion int(11) DEFAULT NULL,
  PRIMARY KEY (IdCliente),
  KEY IdEmpresa (IdEmpresa),
  KEY IdTipoIdentificacion (IdTipoIdentificacion),
  KEY IdProvincia (IdProvincia,IdCanton,IdDistrito,IdBarrio)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'compra'
--

CREATE TABLE compra (
  IdEmpresa int(11) NOT NULL,
  IdCompra int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdProveedor int(11) NOT NULL,
  IdCondicionVenta int(11) NOT NULL,
  PlazoCredito int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  NoDocumento varchar(20) DEFAULT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Descuento double NOT NULL,
  Impuesto double NOT NULL,
  IdCxP int(11) NOT NULL,
  IdAsiento int(11) NOT NULL,
  IdMovBanco int(11) NOT NULL,
  IdOrdenCompra int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdCompra),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario),
  KEY IdProveedor (IdProveedor),
  KEY IdCondicionVenta (IdCondicionVenta),
  KEY cxp_index (IdCxP)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'condicionventa'
--

CREATE TABLE condicionventa (
  IdCondicionVenta int(11) NOT NULL,
  Descripcion varchar(100) NOT NULL,
  PRIMARY KEY (IdCondicionVenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cuentabanco'
--

CREATE TABLE cuentabanco (
  IdEmpresa int(11) NOT NULL,
  IdCuenta int(11) NOT NULL AUTO_INCREMENT,
  Codigo varchar(50) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  Saldo double NOT NULL,
  PRIMARY KEY (IdCuenta),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cuentaegreso'
--

CREATE TABLE cuentaegreso (
  IdEmpresa int(11) NOT NULL,
  IdCuenta int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(200) NOT NULL,
  PRIMARY KEY (IdCuenta)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cuentaingreso'
--

CREATE TABLE cuentaingreso (
  IdEmpresa int(11) NOT NULL,
  IdCuenta int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(200) NOT NULL,
  PRIMARY KEY (IdCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cuentaporcobrar'
--

CREATE TABLE cuentaporcobrar (
  IdEmpresa int(11) NOT NULL,
  IdCxC int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdPropietario int(11) NOT NULL,
  Descripcion varchar(255) NOT NULL,
  Referencia varchar(50) DEFAULT NULL,
  NroDocOrig int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  Plazo int(11) NOT NULL,
  Tipo smallint(6) NOT NULL,
  Total double NOT NULL,
  Saldo double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdCxC),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'cuentaporpagar'
--

CREATE TABLE cuentaporpagar (
  IdEmpresa int(11) NOT NULL,
  IdCxP int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdPropietario int(11) NOT NULL,
  Descripcion varchar(255) NOT NULL,
  Referencia varchar(50) DEFAULT NULL,
  NroDocOrig int(11) NOT NULL,
  Fecha datetime NOT NULL,
  Plazo int(11) NOT NULL,
  Tipo smallint(6) NOT NULL,
  Total double NOT NULL,
  Saldo double NOT NULL,
  IdAsiento int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdCxP),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosemovimientocuentaporcobrar'
--

CREATE TABLE desglosemovimientocuentaporcobrar (
  IdMovCxC int(11) NOT NULL,
  IdCxC int(11) NOT NULL,
  Monto double NOT NULL,
  PRIMARY KEY (IdMovCxC,IdCxC),
  KEY IdCxC (IdCxC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosemovimientocuentaporpagar'
--

CREATE TABLE desglosemovimientocuentaporpagar (
  IdMovCxP int(11) NOT NULL,
  IdCxP int(11) NOT NULL,
  Monto double NOT NULL,
  PRIMARY KEY (IdMovCxP,IdCxP),
  KEY IdCxP (IdCxP)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagocompra'
--

CREATE TABLE desglosepagocompra (
  IdCompra int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdCompra,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda),
  KEY IdCuentaBanco (IdCuentaBanco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagodevolucioncliente'
--

CREATE TABLE desglosepagodevolucioncliente (
  IdDevolucion int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdDevolucion,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda),
  KEY IdCuentaBanco (IdCuentaBanco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagodevolucionproveedor'
--

CREATE TABLE desglosepagodevolucionproveedor (
  IdDevolucion int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdDevolucion,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda),
  KEY IdCuentaBanco (IdCuentaBanco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagoegreso'
--

CREATE TABLE desglosepagoegreso (
  IdEgreso int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdEgreso,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda),
  KEY IdCuentaBanco (IdCuentaBanco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagofactura'
--

CREATE TABLE desglosepagofactura (
  IdConsecutivo int(11) NOT NULL AUTO_INCREMENT,
  IdFactura int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  TipoTarjeta varchar(50) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdConsecutivo),
  KEY IdFactura (IdFactura),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagoingreso'
--

CREATE TABLE desglosepagoingreso (
  IdIngreso int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  TipoTarjeta varchar(50) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdIngreso,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagomovimientocuentaporcobrar'
--

CREATE TABLE desglosepagomovimientocuentaporcobrar (
  IdMovCxC int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  TipoTarjeta varchar(50) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdMovCxC,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'desglosepagomovimientocuentaporpagar'
--

CREATE TABLE desglosepagomovimientocuentaporpagar (
  IdMovCxP int(11) NOT NULL,
  IdFormaPago int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  IdCuentaBanco int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  NroMovimiento varchar(50) DEFAULT NULL,
  MontoLocal double NOT NULL,
  MontoForaneo double NOT NULL,
  PRIMARY KEY (IdMovCxP,IdFormaPago,IdTipoMoneda,IdCuentaBanco),
  KEY IdFormaPago (IdFormaPago),
  KEY IdTipoMoneda (IdTipoMoneda),
  KEY IdCuentaBanco (IdCuentaBanco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalleajusteinventario'
--

CREATE TABLE detalleajusteinventario (
  IdAjuste int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  Excento bit(1) NOT NULL,
  PRIMARY KEY (IdAjuste,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalleasiento'
--

CREATE TABLE detalleasiento (
  IdAsiento int(11) NOT NULL,
  Linea int(11) NOT NULL,
  IdCuenta int(11) NOT NULL,
  Debito double NOT NULL,
  Credito double NOT NULL,
  SaldoAnterior double NOT NULL,
  PRIMARY KEY (IdAsiento,Linea),
  KEY IdCuenta (IdCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detallecompra'
--

CREATE TABLE detallecompra (
  IdCompra int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  Excento bit(1) NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdCompra,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalledevolucioncliente'
--

CREATE TABLE detalledevolucioncliente (
  IdDevolucion int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  PrecioVenta double NOT NULL,
  Excento bit(1) NOT NULL,
  CantDevolucion double NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdDevolucion,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalledevolucionproveedor'
--

CREATE TABLE detalledevolucionproveedor (
  IdDevolucion int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  Excento bit(1) NOT NULL,
  CantDevolucion double NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdDevolucion,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detallefactura'
--

CREATE TABLE detallefactura (
  IdFactura int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  Cantidad double NOT NULL,
  PrecioVenta double NOT NULL,
  Excento bit(1) NOT NULL,
  PrecioCosto double NOT NULL,
  CostoInstalacion double NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdFactura,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalleordencompra'
--

CREATE TABLE detalleordencompra (
  IdOrdenCompra int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  Excento bit(1) NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdOrdenCompra,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalleordenservicio'
--

CREATE TABLE detalleordenservicio (
  IdOrden int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  Cantidad double NOT NULL,
  PrecioVenta double NOT NULL,
  CostoInstalacion double NOT NULL,
  Excento bit(1) NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdOrden,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalleproforma'
--

CREATE TABLE detalleproforma (
  IdProforma int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioVenta double NOT NULL,
  Excento bit(1) NOT NULL,
  PorcentajeIVA double NOT NULL,
  PRIMARY KEY (IdProforma,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'detalletraslado'
--

CREATE TABLE detalletraslado (
  IdTraslado int(11) NOT NULL,
  IdProducto int(11) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  Excento bit(1) NOT NULL,
  PRIMARY KEY (IdTraslado,IdProducto),
  KEY IdProducto (IdProducto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'devolucioncliente'
--

CREATE TABLE devolucioncliente (
  IdEmpresa int(11) NOT NULL,
  IdDevolucion int(11) NOT NULL AUTO_INCREMENT,
  IdFactura int(11) NOT NULL,
  IdUsuario int(11) NOT NULL,
  IdCliente int(11) NOT NULL,
  Fecha datetime NOT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Impuesto double NOT NULL,
  IdMovimientoCxC int(11) NOT NULL,
  IdAsiento int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  IdDocElectronico varchar(50) DEFAULT NULL,
  IdDocElectronicoRev varchar(50) DEFAULT NULL,
  PRIMARY KEY (IdDevolucion),
  KEY IdEmpresa (IdEmpresa),
  KEY IdFactura (IdFactura),
  KEY IdUsuario (IdUsuario),
  KEY IdCliente (IdCliente)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'devolucionproveedor'
--

CREATE TABLE devolucionproveedor (
  IdEmpresa int(11) NOT NULL,
  IdDevolucion int(11) NOT NULL AUTO_INCREMENT,
  IdCompra int(11) NOT NULL,
  IdUsuario int(11) NOT NULL,
  IdProveedor int(11) NOT NULL,
  Fecha datetime NOT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Impuesto double NOT NULL,
  IdMovimientoCxP int(11) NOT NULL,
  IdAsiento int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdDevolucion),
  KEY IdEmpresa (IdEmpresa),
  KEY IdCompra (IdCompra),
  KEY IdUsuario (IdUsuario),
  KEY IdProveedor (IdProveedor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'distrito'
--

CREATE TABLE distrito (
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  IdDistrito int(11) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdProvincia,IdCanton,IdDistrito)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'documentoelectronico'
--

CREATE TABLE documentoelectronico (
  IdDocumento int(11) NOT NULL AUTO_INCREMENT,
  IdEmpresa int(11) NOT NULL,
  Fecha datetime NOT NULL,
  ClaveNumerica varchar(50) NOT NULL,
  Consecutivo varchar(20) DEFAULT NULL,
  TipoIdentificacionEmisor varchar(2) NOT NULL,
  IdentificacionEmisor varchar(12) NOT NULL,
  TipoIdentificacionReceptor varchar(2) NOT NULL,
  IdentificacionReceptor varchar(12) NOT NULL,
  EsMensajeReceptor varchar(1) NOT NULL,
  DatosDocumento blob NOT NULL,
  Respuesta blob,
  EstadoEnvio varchar(20) NOT NULL,
  CorreoNotificacion varchar(200) NOT NULL,
  IdTipoDocumento int(11) NOT NULL,
  ErrorEnvio varchar(500) DEFAULT NULL,
  IdSucursal int(11) NOT NULL,
  IdTerminal int(11) NOT NULL,
  IdConsecutivo int(11) NOT NULL,
  DatosDocumentoOri blob,
  EsIvaAcreditable varchar(1) NOT NULL,
  PRIMARY KEY (IdDocumento),
  KEY ClaveNumerica (ClaveNumerica),
  KEY IdEmpresa (IdEmpresa),
  KEY EstadoEnvio (EstadoEnvio),
  KEY ClaveNumerica_2 (ClaveNumerica,Consecutivo)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'egreso'
--

CREATE TABLE egreso (
  IdEmpresa int(11) NOT NULL,
  IdEgreso int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  Fecha datetime NOT NULL,
  IdCuenta int(11) NOT NULL,
  Beneficiario varchar(100) DEFAULT NULL,
  Detalle varchar(255) NOT NULL,
  Monto double NOT NULL,
  IdAsiento int(11) DEFAULT NULL,
  IdMovBanco int(11) DEFAULT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdEgreso),
  KEY IdEmpresa (IdEmpresa),
  KEY IdCuenta (IdCuenta)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'empresa'
--

CREATE TABLE empresa (
  IdEmpresa int(11) NOT NULL AUTO_INCREMENT,
  NombreEmpresa varchar(80) NOT NULL,
  UsuarioHacienda varchar(100) DEFAULT NULL,
  ClaveHacienda varchar(100) DEFAULT NULL,
  CorreoNotificacion varchar(200) DEFAULT NULL,
  PermiteFacturar bit(1) NOT NULL,
  AccessToken blob,
  ExpiresIn int(11) DEFAULT NULL,
  RefreshExpiresIn int(11) DEFAULT NULL,
  RefreshToken blob,
  EmitedAt datetime DEFAULT NULL,
  Logotipo mediumblob,
  NombreComercial varchar(80) NOT NULL,
  IdTipoIdentificacion int(11) NOT NULL,
  Identificacion varchar(20) NOT NULL,
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  IdDistrito int(11) NOT NULL,
  IdBarrio int(11) NOT NULL,
  Direccion varchar(160) NOT NULL,
  Telefono varchar(20) NOT NULL,
  LineasPorFactura int(11) NOT NULL,
  IdTipoMoneda int(11) NOT NULL,
  Contabiliza bit(1) NOT NULL,
  AutoCompletaProducto bit(1) NOT NULL,
  ModificaDescProducto bit(1) NOT NULL,
  DesglosaServicioInst bit(1) NOT NULL,
  PorcentajeInstalacion double DEFAULT NULL,
  CodigoServicioInst int(11) DEFAULT NULL,
  IncluyeInsumosEnFactura bit(1) NOT NULL,
  CierrePorTurnos bit(1) NOT NULL,
  CierreEnEjecucion bit(1) NOT NULL,
  RegimenSimplificado bit(1) NOT NULL,
  Certificado blob,
  NombreCertificado varchar(100) DEFAULT NULL,
  PinCertificado varchar(4) DEFAULT NULL,
  FechaVence datetime DEFAULT NULL,
  TipoContrato int(11) NOT NULL,
  CantidadDisponible int(11) NOT NULL,
  CodigoActividad varchar(20) NOT NULL,
  PRIMARY KEY (IdEmpresa),
  KEY Identificacion (Identificacion)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'factura'
--

CREATE TABLE factura (
  IdEmpresa int(11) NOT NULL,
  IdSucursal int(11) NOT NULL,
  IdTerminal int(11) NOT NULL,
  IdFactura int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdCliente int(11) NOT NULL,
  IdCondicionVenta int(11) NOT NULL,
  PlazoCredito int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  TextoAdicional varchar(500) DEFAULT NULL,
  IdVendedor int(11) NOT NULL,
  Excento double NOT NULL,
  Gravado double NOT NULL,
  Descuento double NOT NULL,
  MontoPagado double NOT NULL,
  Impuesto double NOT NULL,
  IdCxC int(11) NOT NULL,
  IdAsiento int(11) NOT NULL,
  IdMovBanco int(11) NOT NULL,
  IdOrdenServicio int(11) NOT NULL,
  IdProforma int(11) NOT NULL,
  TotalCosto double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  IdDocElectronico varchar(50) DEFAULT NULL,
  IdDocElectronicoRev varchar(50) DEFAULT NULL,
  IdTipoMoneda int(11) NOT NULL,
  TipoDeCambioDolar decimal(13,5) NOT NULL,
  IdTipoExoneracion int(11) NOT NULL,
  NumDocExoneracion varchar(100) DEFAULT NULL,
  NombreInstExoneracion varchar(100) DEFAULT NULL,
  FechaEmisionDoc datetime NOT NULL,
  PorcentajeExoneracion int(11) DEFAULT NULL,
  Exonerado double NOT NULL,
  PRIMARY KEY (IdFactura),
  KEY IdEmpresa (IdEmpresa),
  KEY IdCliente (IdCliente),
  KEY IdUsuario (IdUsuario),
  KEY IdCondicionVenta (IdCondicionVenta),
  KEY IdVendedor (IdVendedor),
  KEY cxc_index (IdCxC)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'formapago'
--

CREATE TABLE formapago (
  IdFormaPago int(11) NOT NULL,
  Descripcion varchar(100) NOT NULL,
  PRIMARY KEY (IdFormaPago)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'ingreso'
--

CREATE TABLE ingreso (
  IdEmpresa int(11) NOT NULL,
  IdIngreso int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  Fecha datetime NOT NULL,
  IdCuenta int(11) NOT NULL,
  RecibidoDe varchar(100) NOT NULL,
  Detalle varchar(255) NOT NULL,
  Monto double NOT NULL,
  IdAsiento int(11) DEFAULT NULL,
  IdMovBanco int(11) DEFAULT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdIngreso),
  KEY IdEmpresa (IdEmpresa),
  KEY IdCuenta (IdCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'linea'
--

CREATE TABLE linea (
  IdEmpresa int(11) NOT NULL,
  IdLinea int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdLinea),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'movimientobanco'
--

CREATE TABLE movimientobanco (
  IdMov int(11) NOT NULL AUTO_INCREMENT,
  Fecha datetime NOT NULL,
  IdUsuario int(11) NOT NULL,
  IdTipo int(11) NOT NULL,
  IdCuenta int(11) NOT NULL,
  Numero varchar(50) NOT NULL,
  Beneficiario varchar(200) NOT NULL,
  SaldoAnterior double NOT NULL,
  Monto double NOT NULL,
  Descripcion varchar(255) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdMov),
  KEY IdCuenta (IdCuenta),
  KEY IdTipo (IdTipo)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'movimientocuentaporcobrar'
--

CREATE TABLE movimientocuentaporcobrar (
  IdEmpresa int(11) NOT NULL,
  IdMovCxC int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdPropietario int(11) NOT NULL,
  Tipo smallint(6) NOT NULL,
  Fecha datetime NOT NULL,
  Descripcion varchar(100) NOT NULL,
  Monto double NOT NULL,
  IdAsiento int(11) NOT NULL,
  IdMovBanco int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdMovCxC),
  KEY IdUsuario (IdUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'movimientocuentaporpagar'
--

CREATE TABLE movimientocuentaporpagar (
  IdEmpresa int(11) NOT NULL,
  IdMovCxP int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  Tipo smallint(6) NOT NULL,
  IdPropietario int(11) NOT NULL,
  TipoPropietario smallint(6) NOT NULL,
  Recibo varchar(50) NOT NULL,
  Fecha datetime NOT NULL,
  Descripcion varchar(100) NOT NULL,
  Monto double NOT NULL,
  IdAsiento int(11) NOT NULL,
  IdMovBanco int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Procesado bit(1) NOT NULL,
  PRIMARY KEY (IdMovCxP),
  KEY IdUsuario (IdUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'movimientoproducto'
--

CREATE TABLE movimientoproducto (
  IdMovimiento int(11) NOT NULL AUTO_INCREMENT,
  IdProducto int(11) NOT NULL,
  IdSucursal int(11) NOT NULL,
  Fecha datetime NOT NULL,
  Cantidad double NOT NULL,
  Tipo varchar(10) NOT NULL,
  Origen varchar(100) NOT NULL,
  Referencia varchar(100) NOT NULL,
  PrecioCosto double NOT NULL,
  PRIMARY KEY (IdMovimiento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'ordencompra'
--

CREATE TABLE ordencompra (
  IdEmpresa int(11) NOT NULL,
  IdOrdenCompra int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdProveedor int(11) NOT NULL,
  IdCondicionVenta int(11) NOT NULL,
  PlazoCredito int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  NoDocumento varchar(50) DEFAULT NULL,
  TipoPago smallint(6) NOT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Descuento double NOT NULL,
  Impuesto double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Aplicado bit(1) NOT NULL,
  PRIMARY KEY (IdOrdenCompra),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario),
  KEY IdProveedor (IdProveedor),
  KEY IdCondicionVenta (IdCondicionVenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'ordenservicio'
--

CREATE TABLE ordenservicio (
  IdEmpresa int(11) NOT NULL,
  IdOrden int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdCliente int(11) NOT NULL,
  IdVendedor int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  Operarios varchar(200) DEFAULT NULL,
  HoraEntrada varchar(15) DEFAULT NULL,
  HoraSalida varchar(15) DEFAULT NULL,
  Marca varchar(20) DEFAULT NULL,
  Modelo varchar(30) DEFAULT NULL,
  Placa varchar(10) DEFAULT NULL,
  Color varchar(20) DEFAULT NULL,
  EstadoActual varchar(500) DEFAULT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Descuento double NOT NULL,
  Impuesto double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Aplicado bit(1) NOT NULL,
  PRIMARY KEY (IdOrden),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario),
  KEY IdCliente (IdCliente)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'padron'
--

CREATE TABLE padron (
  Identificacion varchar(9) NOT NULL,
  IdProvincia int(11) NOT NULL,
  IdCanton int(11) NOT NULL,
  IdDistrito int(11) NOT NULL,
  Nombre varchar(100) NOT NULL,
  PrimerApellido varchar(100) NOT NULL,
  SegundoApellido varchar(100) NOT NULL,
  PRIMARY KEY (Identificacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'parametrocontable'
--

CREATE TABLE parametrocontable (
  IdParametro int(11) NOT NULL AUTO_INCREMENT,
  IdTipo int(11) NOT NULL,
  IdCuenta int(11) NOT NULL,
  IdProducto int(11) DEFAULT NULL,
  PRIMARY KEY (IdParametro),
  KEY IdTipo (IdTipo),
  KEY IdCuenta (IdCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'parametroexoneracion'
--

CREATE TABLE parametroexoneracion (
  IdTipoExoneracion int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdTipoExoneracion)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'parametroimpuesto'
--

CREATE TABLE parametroimpuesto (
  IdImpuesto int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(50) NOT NULL,
  TasaImpuesto double NOT NULL,
  PRIMARY KEY (IdImpuesto)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'parametrosistema'
--

CREATE TABLE parametrosistema (
  IdParametro int(11) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  Valor varchar(100) NOT NULL,
  PRIMARY KEY (IdParametro)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'producto'
--

CREATE TABLE producto (
  IdEmpresa int(11) NOT NULL,
  IdProducto int(11) NOT NULL AUTO_INCREMENT,
  Tipo int(11) NOT NULL,
  IdLinea int(11) NOT NULL,
  Codigo varchar(20) NOT NULL,
  IdProveedor int(11) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  Cantidad double NOT NULL,
  PrecioCosto double NOT NULL,
  PrecioVenta1 double NOT NULL,
  PrecioVenta2 double NOT NULL,
  PrecioVenta3 double NOT NULL,
  PrecioVenta4 double NOT NULL,
  PrecioVenta5 double NOT NULL,
  IndExistencia double NOT NULL,
  Imagen longblob,
  IdImpuesto int(11) NOT NULL,
  PRIMARY KEY (IdProducto),
  KEY IdEmpresa (IdEmpresa),
  KEY IdLinea (IdLinea),
  KEY IdProveedor (IdProveedor),
  KEY Tipo (Tipo),
  KEY IdImpuesto (IdImpuesto)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'proforma'
--

CREATE TABLE proforma (
  IdEmpresa int(11) NOT NULL,
  IdProforma int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdCliente int(11) NOT NULL,
  IdCondicionVenta int(11) NOT NULL,
  PlazoCredito int(11) DEFAULT NULL,
  Fecha datetime NOT NULL,
  NoDocumento varchar(50) DEFAULT NULL,
  IdVendedor int(11) NOT NULL,
  TipoPago smallint(6) NOT NULL,
  Excento double NOT NULL,
  Grabado double NOT NULL,
  Descuento double NOT NULL,
  Impuesto double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  Aplicado bit(1) NOT NULL,
  PRIMARY KEY (IdProforma),
  KEY IdEmpresa (IdEmpresa),
  KEY IdCliente (IdCliente),
  KEY IdUsuario (IdUsuario),
  KEY IdCondicionVenta (IdCondicionVenta),
  KEY IdVendedor (IdVendedor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'proveedor'
--

CREATE TABLE proveedor (
  IdEmpresa int(11) NOT NULL,
  IdProveedor int(11) NOT NULL AUTO_INCREMENT,
  Identificacion varchar(35) NOT NULL,
  Nombre varchar(200) DEFAULT NULL,
  Direccion varchar(255) DEFAULT NULL,
  Telefono1 varchar(9) DEFAULT NULL,
  Telefono2 varchar(9) DEFAULT NULL,
  Fax varchar(9) DEFAULT NULL,
  Correo varchar(100) DEFAULT NULL,
  PlazoCredito int(11) NOT NULL,
  Contacto1 varchar(100) DEFAULT NULL,
  TelCont1 varchar(9) DEFAULT NULL,
  Contacto2 varchar(100) DEFAULT NULL,
  TelCont2 varchar(9) DEFAULT NULL,
  PRIMARY KEY (IdProveedor),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'provincia'
--

CREATE TABLE provincia (
  IdProvincia int(11) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdProvincia)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'registroautenticacion'
--

CREATE TABLE registroautenticacion (
  Id varchar(36) NOT NULL,
  Fecha datetime NOT NULL,
  Role int(11) NOT NULL,
  PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'registrorespuestahacienda'
--

CREATE TABLE registrorespuestahacienda (
  IdRegistro int(11) NOT NULL AUTO_INCREMENT,
  Fecha datetime NOT NULL,
  ClaveNumerica varchar(50) NOT NULL,
  Respuesta blob,
  PRIMARY KEY (IdRegistro),
  KEY idx_clavenumerica (ClaveNumerica)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'reporteporempresa'
--

CREATE TABLE reporteporempresa (
  IdEmpresa int(11) NOT NULL,
  IdReporte int(11) NOT NULL,
  PRIMARY KEY (IdEmpresa,IdReporte),
  KEY IdReporte (IdReporte)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'role'
--

CREATE TABLE role (
  IdRole int(11) NOT NULL AUTO_INCREMENT,
  Nombre varchar(100) NOT NULL,
  MenuPadre varchar(100) NOT NULL,
  MenuItem varchar(100) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  PRIMARY KEY (IdRole)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'roleporusuario'
--

CREATE TABLE roleporusuario (
  IdUsuario int(11) NOT NULL,
  IdRole int(11) NOT NULL,
  PRIMARY KEY (IdUsuario,IdRole),
  KEY IdRole (IdRole)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'saldomensualcontable'
--

CREATE TABLE saldomensualcontable (
  IdCuenta int(11) NOT NULL,
  Mes int(2) NOT NULL,
  Annio int(4) NOT NULL,
  SaldoFinMes double NOT NULL,
  TotalDebito double NOT NULL,
  TotalCredito double NOT NULL,
  PRIMARY KEY (IdCuenta,Mes,Annio)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'sucursal'
--

CREATE TABLE sucursal (
  IdEmpresa int(11) NOT NULL,
  IdSucursal int(11) NOT NULL AUTO_INCREMENT,
  Nombre varchar(200) NOT NULL,
  Direccion varchar(500) NOT NULL,
  Telefono varchar(100) DEFAULT NULL,
  PRIMARY KEY (IdSucursal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'sucursalporempresa'
--

CREATE TABLE sucursalporempresa (
  IdEmpresa int(11) NOT NULL,
  IdSucursal int(11) NOT NULL,
  NombreSucursal varchar(160) NOT NULL,
  Direccion varchar(160) NOT NULL,
  Telefono varchar(20) NOT NULL,
  PRIMARY KEY (IdEmpresa,IdSucursal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'terminalporsucursal'
--

CREATE TABLE terminalporsucursal (
  IdEmpresa int(11) NOT NULL,
  IdSucursal int(11) NOT NULL,
  IdTerminal int(11) NOT NULL,
  ValorRegistro varchar(100) NOT NULL,
  ImpresoraFactura varchar(50) DEFAULT NULL,
  UltimoDocFE int(11) NOT NULL,
  UltimoDocND int(11) NOT NULL,
  UltimoDocNC int(11) NOT NULL,
  UltimoDocTE int(11) NOT NULL,
  UltimoDocMR int(11) NOT NULL,
  IdTipoDispositivo int(1) NOT NULL,
  PRIMARY KEY (IdEmpresa,IdSucursal,IdTerminal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipocuentacontable'
--

CREATE TABLE tipocuentacontable (
  IdTipoCuenta int(11) NOT NULL,
  TipoSaldo varchar(1) NOT NULL,
  Descripcion varchar(20) NOT NULL,
  PRIMARY KEY (IdTipoCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipodecambiodolar'
--

CREATE TABLE tipodecambiodolar (
  FechaTipoCambio varchar(10) NOT NULL DEFAULT '',
  ValorTipoCambio decimal(13,5) DEFAULT NULL,
  PRIMARY KEY (FechaTipoCambio)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipoidentificacion'
--

CREATE TABLE tipoidentificacion (
  IdTipoIdentificacion int(11) NOT NULL,
  Descripcion varchar(20) NOT NULL,
  PRIMARY KEY (IdTipoIdentificacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipomoneda'
--

CREATE TABLE tipomoneda (
  IdTipoMoneda int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(10) NOT NULL,
  PRIMARY KEY (IdTipoMoneda)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipomovimientobanco'
--

CREATE TABLE tipomovimientobanco (
  IdTipoMov int(11) NOT NULL AUTO_INCREMENT,
  DebeHaber varchar(2) NOT NULL,
  Descripcion varchar(50) NOT NULL,
  PRIMARY KEY (IdTipoMov)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipoparametrocontable'
--

CREATE TABLE tipoparametrocontable (
  IdTipo int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(200) NOT NULL,
  MultiCuenta bit(1) NOT NULL,
  PRIMARY KEY (IdTipo)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipoproducto'
--

CREATE TABLE tipoproducto (
  IdTipoProducto int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(25) DEFAULT NULL,
  PRIMARY KEY (IdTipoProducto)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'tipounidad'
--

CREATE TABLE tipounidad (
  IdTipoUnidad int(11) NOT NULL AUTO_INCREMENT,
  Descripcion varchar(5) NOT NULL,
  PRIMARY KEY (IdTipoUnidad)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'traslado'
--

CREATE TABLE traslado (
  IdEmpresa int(11) NOT NULL,
  IdTraslado int(11) NOT NULL AUTO_INCREMENT,
  IdUsuario int(11) NOT NULL,
  IdSucursal int(11) NOT NULL,
  Tipo int(11) NOT NULL,
  Fecha datetime NOT NULL,
  NoDocumento varchar(50) DEFAULT NULL,
  Total double NOT NULL,
  IdAsiento int(11) NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int(11) DEFAULT NULL,
  PRIMARY KEY (IdTraslado),
  KEY IdEmpresa (IdEmpresa),
  KEY IdUsuario (IdUsuario),
  KEY IdSucursal (IdSucursal)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'usuario'
--

CREATE TABLE usuario (
  IdUsuario int(11) NOT NULL AUTO_INCREMENT,
  CodigoUsuario varchar(10) NOT NULL,
  Clave varchar(100) NOT NULL,
  Modifica tinyint(1) NOT NULL,
  PermiteRegistrarDispositivo tinyint(1) DEFAULT NULL,
  PRIMARY KEY (IdUsuario)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'usuarioporempresa'
--

CREATE TABLE usuarioporempresa (
  IdEmpresa int(11) NOT NULL,
  IdUsuario int(11) NOT NULL,
  PRIMARY KEY (IdUsuario,IdEmpresa),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table 'vendedor'
--

CREATE TABLE vendedor (
  IdEmpresa int(11) NOT NULL,
  IdVendedor int(11) NOT NULL AUTO_INCREMENT,
  Nombre varchar(100) NOT NULL,
  PRIMARY KEY (IdVendedor),
  KEY IdEmpresa (IdEmpresa)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `ajusteinventario`
--
ALTER TABLE `ajusteinventario`
  ADD CONSTRAINT ajusteinventario_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT ajusteinventario_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `asiento`
--
ALTER TABLE `asiento`
  ADD CONSTRAINT asiento_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `barrio`
--
ALTER TABLE `barrio`
  ADD CONSTRAINT barrio_ibfk_1 FOREIGN KEY (IdProvincia, IdCanton, IdDistrito) REFERENCES distrito (IdProvincia, IdCanton, IdDistrito);

--
-- Constraints for table `cantfemensualempresa`
--
ALTER TABLE `cantfemensualempresa`
  ADD CONSTRAINT cantfemensualempresa_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `canton`
--
ALTER TABLE `canton`
  ADD CONSTRAINT canton_ibfk_1 FOREIGN KEY (IdProvincia) REFERENCES provincia (IdProvincia);

--
-- Constraints for table `catalogocontable`
--
ALTER TABLE `catalogocontable`
  ADD CONSTRAINT catalogocontable_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT catalogocontable_ibfk_2 FOREIGN KEY (IdTipoCuenta) REFERENCES tipocuentacontable (IdTipoCuenta),
  ADD CONSTRAINT catalogocontable_ibfk_3 FOREIGN KEY (IdClaseCuenta) REFERENCES clasecuentacontable (IdClaseCuenta);

--
-- Constraints for table `cierrecaja`
--
ALTER TABLE `cierrecaja`
  ADD CONSTRAINT cierrecaja_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `cliente`
--
ALTER TABLE `cliente`
  ADD CONSTRAINT cliente_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT cliente_ibfk_2 FOREIGN KEY (IdTipoIdentificacion) REFERENCES tipoidentificacion (IdTipoIdentificacion),
  ADD CONSTRAINT cliente_ibfk_3 FOREIGN KEY (IdProvincia, IdCanton, IdDistrito, IdBarrio) REFERENCES barrio (IdProvincia, IdCanton, IdDistrito, IdBarrio);

--
-- Constraints for table `compra`
--
ALTER TABLE `compra`
  ADD CONSTRAINT compra_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT compra_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT compra_ibfk_3 FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor),
  ADD CONSTRAINT compra_ibfk_4 FOREIGN KEY (IdCondicionVenta) REFERENCES condicionventa (IdCondicionVenta);

--
-- Constraints for table `cuentabanco`
--
ALTER TABLE `cuentabanco`
  ADD CONSTRAINT cuentabanco_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `cuentaporcobrar`
--
ALTER TABLE `cuentaporcobrar`
  ADD CONSTRAINT cuentaporcobrar_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT cuentaporcobrar_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `cuentaporpagar`
--
ALTER TABLE `cuentaporpagar`
  ADD CONSTRAINT cuentaporpagar_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT cuentaporpagar_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `desglosemovimientocuentaporcobrar`
--
ALTER TABLE `desglosemovimientocuentaporcobrar`
  ADD CONSTRAINT desglosemovimientocuentaporcobrar_ibfk_1 FOREIGN KEY (IdMovCxC) REFERENCES movimientocuentaporcobrar (IdMovCxC),
  ADD CONSTRAINT desglosemovimientocuentaporcobrar_ibfk_2 FOREIGN KEY (IdCxC) REFERENCES cuentaporcobrar (IdCxC);

--
-- Constraints for table `desglosemovimientocuentaporpagar`
--
ALTER TABLE `desglosemovimientocuentaporpagar`
  ADD CONSTRAINT desglosemovimientocuentaporpagar_ibfk_1 FOREIGN KEY (IdMovCxP) REFERENCES movimientocuentaporpagar (IdMovCxP),
  ADD CONSTRAINT desglosemovimientocuentaporpagar_ibfk_2 FOREIGN KEY (IdCxP) REFERENCES cuentaporpagar (IdCxP);

--
-- Constraints for table `desglosepagocompra`
--
ALTER TABLE `desglosepagocompra`
  ADD CONSTRAINT desglosepagocompra_ibfk_1 FOREIGN KEY (IdCompra) REFERENCES compra (IdCompra),
  ADD CONSTRAINT desglosepagocompra_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagocompra_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda),
  ADD CONSTRAINT desglosepagocompra_ibfk_4 FOREIGN KEY (IdCuentaBanco) REFERENCES cuentabanco (IdCuenta);

--
-- Constraints for table `desglosepagodevolucioncliente`
--
ALTER TABLE `desglosepagodevolucioncliente`
  ADD CONSTRAINT desglosepagodevolucioncliente_ibfk_1 FOREIGN KEY (IdDevolucion) REFERENCES devolucioncliente (IdDevolucion),
  ADD CONSTRAINT desglosepagodevolucioncliente_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagodevolucioncliente_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda),
  ADD CONSTRAINT desglosepagodevolucioncliente_ibfk_4 FOREIGN KEY (IdCuentaBanco) REFERENCES cuentabanco (IdCuenta);

--
-- Constraints for table `desglosepagodevolucionproveedor`
--
ALTER TABLE `desglosepagodevolucionproveedor`
  ADD CONSTRAINT desglosepagodevolucionproveedor_ibfk_1 FOREIGN KEY (IdDevolucion) REFERENCES devolucionproveedor (IdDevolucion),
  ADD CONSTRAINT desglosepagodevolucionproveedor_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagodevolucionproveedor_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda),
  ADD CONSTRAINT desglosepagodevolucionproveedor_ibfk_4 FOREIGN KEY (IdCuentaBanco) REFERENCES cuentabanco (IdCuenta);

--
-- Constraints for table `desglosepagoegreso`
--
ALTER TABLE `desglosepagoegreso`
  ADD CONSTRAINT desglosepagoegreso_ibfk_1 FOREIGN KEY (IdEgreso) REFERENCES egreso (IdEgreso),
  ADD CONSTRAINT desglosepagoegreso_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagoegreso_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda),
  ADD CONSTRAINT desglosepagoegreso_ibfk_4 FOREIGN KEY (IdCuentaBanco) REFERENCES cuentabanco (IdCuenta);

--
-- Constraints for table `desglosepagofactura`
--
ALTER TABLE `desglosepagofactura`
  ADD CONSTRAINT desglosepagofactura_ibfk_1 FOREIGN KEY (IdFactura) REFERENCES factura (IdFactura),
  ADD CONSTRAINT desglosepagofactura_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagofactura_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda);

--
-- Constraints for table `desglosepagoingreso`
--
ALTER TABLE `desglosepagoingreso`
  ADD CONSTRAINT desglosepagoingreso_ibfk_1 FOREIGN KEY (IdIngreso) REFERENCES ingreso (IdIngreso),
  ADD CONSTRAINT desglosepagoingreso_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagoingreso_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda);

--
-- Constraints for table `desglosepagomovimientocuentaporcobrar`
--
ALTER TABLE `desglosepagomovimientocuentaporcobrar`
  ADD CONSTRAINT desglosepagomovimientocuentaporcobrar_ibfk_1 FOREIGN KEY (IdMovCxC) REFERENCES movimientocuentaporcobrar (IdMovCxC),
  ADD CONSTRAINT desglosepagomovimientocuentaporcobrar_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagomovimientocuentaporcobrar_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda);

--
-- Constraints for table `desglosepagomovimientocuentaporpagar`
--
ALTER TABLE `desglosepagomovimientocuentaporpagar`
  ADD CONSTRAINT desglosepagomovimientocuentaporpagar_ibfk_1 FOREIGN KEY (IdMovCxP) REFERENCES movimientocuentaporpagar (IdMovCxP),
  ADD CONSTRAINT desglosepagomovimientocuentaporpagar_ibfk_2 FOREIGN KEY (IdFormaPago) REFERENCES formapago (IdFormaPago),
  ADD CONSTRAINT desglosepagomovimientocuentaporpagar_ibfk_3 FOREIGN KEY (IdTipoMoneda) REFERENCES tipomoneda (IdTipoMoneda),
  ADD CONSTRAINT desglosepagomovimientocuentaporpagar_ibfk_4 FOREIGN KEY (IdCuentaBanco) REFERENCES cuentabanco (IdCuenta);

--
-- Constraints for table `detalleajusteinventario`
--
ALTER TABLE `detalleajusteinventario`
  ADD CONSTRAINT detalleajusteinventario_ibfk_1 FOREIGN KEY (IdAjuste) REFERENCES ajusteinventario (IdAjuste),
  ADD CONSTRAINT detalleajusteinventario_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalleasiento`
--
ALTER TABLE `detalleasiento`
  ADD CONSTRAINT detalleasiento_ibfk_1 FOREIGN KEY (IdAsiento) REFERENCES asiento (IdAsiento),
  ADD CONSTRAINT detalleasiento_ibfk_2 FOREIGN KEY (IdCuenta) REFERENCES catalogocontable (IdCuenta);

--
-- Constraints for table `detallecompra`
--
ALTER TABLE `detallecompra`
  ADD CONSTRAINT detallecompra_ibfk_1 FOREIGN KEY (IdCompra) REFERENCES compra (IdCompra),
  ADD CONSTRAINT detallecompra_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalledevolucioncliente`
--
ALTER TABLE `detalledevolucioncliente`
  ADD CONSTRAINT detalledevolucioncliente_ibfk_1 FOREIGN KEY (IdDevolucion) REFERENCES devolucioncliente (IdDevolucion),
  ADD CONSTRAINT detalledevolucioncliente_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalledevolucionproveedor`
--
ALTER TABLE `detalledevolucionproveedor`
  ADD CONSTRAINT detalledevolucionproveedor_ibfk_1 FOREIGN KEY (IdDevolucion) REFERENCES devolucionproveedor (IdDevolucion),
  ADD CONSTRAINT detalledevolucionproveedor_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detallefactura`
--
ALTER TABLE `detallefactura`
  ADD CONSTRAINT detallefactura_ibfk_1 FOREIGN KEY (IdFactura) REFERENCES factura (IdFactura),
  ADD CONSTRAINT detallefactura_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalleordencompra`
--
ALTER TABLE `detalleordencompra`
  ADD CONSTRAINT detalleordencompra_ibfk_1 FOREIGN KEY (IdOrdenCompra) REFERENCES ordencompra (IdOrdenCompra),
  ADD CONSTRAINT detalleordencompra_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalleordenservicio`
--
ALTER TABLE `detalleordenservicio`
  ADD CONSTRAINT detalleordenservicio_ibfk_1 FOREIGN KEY (IdOrden) REFERENCES ordenservicio (IdOrden),
  ADD CONSTRAINT detalleordenservicio_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalleproforma`
--
ALTER TABLE `detalleproforma`
  ADD CONSTRAINT detalleproforma_ibfk_1 FOREIGN KEY (IdProforma) REFERENCES proforma (IdProforma),
  ADD CONSTRAINT detalleproforma_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `detalletraslado`
--
ALTER TABLE `detalletraslado`
  ADD CONSTRAINT detalletraslado_ibfk_1 FOREIGN KEY (IdTraslado) REFERENCES traslado (IdTraslado),
  ADD CONSTRAINT detalletraslado_ibfk_2 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `devolucioncliente`
--
ALTER TABLE `devolucioncliente`
  ADD CONSTRAINT devolucioncliente_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT devolucioncliente_ibfk_2 FOREIGN KEY (IdFactura) REFERENCES factura (IdFactura),
  ADD CONSTRAINT devolucioncliente_ibfk_3 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT devolucioncliente_ibfk_4 FOREIGN KEY (IdCliente) REFERENCES `cliente` (IdCliente);

--
-- Constraints for table `devolucionproveedor`
--
ALTER TABLE `devolucionproveedor`
  ADD CONSTRAINT devolucionproveedor_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT devolucionproveedor_ibfk_2 FOREIGN KEY (IdCompra) REFERENCES compra (IdCompra),
  ADD CONSTRAINT devolucionproveedor_ibfk_3 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT devolucionproveedor_ibfk_4 FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor);

--
-- Constraints for table `distrito`
--
ALTER TABLE `distrito`
  ADD CONSTRAINT distrito_ibfk_1 FOREIGN KEY (IdProvincia, IdCanton) REFERENCES canton (IdProvincia, IdCanton);

--
-- Constraints for table `documentoelectronico`
--
ALTER TABLE `documentoelectronico`
  ADD CONSTRAINT documentoelectronico_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `egreso`
--
ALTER TABLE `egreso`
  ADD CONSTRAINT egreso_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT egreso_ibfk_2 FOREIGN KEY (IdCuenta) REFERENCES cuentaegreso (IdCuenta);

--
-- Constraints for table `factura`
--
ALTER TABLE `factura`
  ADD CONSTRAINT factura_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT factura_ibfk_2 FOREIGN KEY (IdCliente) REFERENCES `cliente` (IdCliente),
  ADD CONSTRAINT factura_ibfk_3 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT factura_ibfk_4 FOREIGN KEY (IdCondicionVenta) REFERENCES condicionventa (IdCondicionVenta),
  ADD CONSTRAINT factura_ibfk_5 FOREIGN KEY (IdVendedor) REFERENCES vendedor (IdVendedor);

--
-- Constraints for table `ingreso`
--
ALTER TABLE `ingreso`
  ADD CONSTRAINT ingreso_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT ingreso_ibfk_2 FOREIGN KEY (IdCuenta) REFERENCES cuentaingreso (IdCuenta);

--
-- Constraints for table `linea`
--
ALTER TABLE `linea`
  ADD CONSTRAINT linea_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `movimientobanco`
--
ALTER TABLE `movimientobanco`
  ADD CONSTRAINT movimientobanco_ibfk_1 FOREIGN KEY (IdCuenta) REFERENCES cuentabanco (IdCuenta),
  ADD CONSTRAINT movimientobanco_ibfk_2 FOREIGN KEY (IdTipo) REFERENCES tipomovimientobanco (IdTipoMov);

--
-- Constraints for table `movimientocuentaporcobrar`
--
ALTER TABLE `movimientocuentaporcobrar`
  ADD CONSTRAINT movimientocuentaporcobrar_ibfk_1 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `movimientocuentaporpagar`
--
ALTER TABLE `movimientocuentaporpagar`
  ADD CONSTRAINT movimientocuentaporpagar_ibfk_1 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `movimientoproducto`
--
ALTER TABLE `movimientoproducto`
  ADD CONSTRAINT movimientoproducto_ibfk_1 FOREIGN KEY (IdProducto) REFERENCES producto (IdProducto);

--
-- Constraints for table `ordencompra`
--
ALTER TABLE `ordencompra`
  ADD CONSTRAINT ordencompra_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT ordencompra_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT ordencompra_ibfk_3 FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor),
  ADD CONSTRAINT ordencompra_ibfk_4 FOREIGN KEY (IdCondicionVenta) REFERENCES condicionventa (IdCondicionVenta);

--
-- Constraints for table `ordenservicio`
--
ALTER TABLE `ordenservicio`
  ADD CONSTRAINT ordenservicio_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT ordenservicio_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT ordenservicio_ibfk_3 FOREIGN KEY (IdCliente) REFERENCES `cliente` (IdCliente);

--
-- Constraints for table `parametrocontable`
--
ALTER TABLE `parametrocontable`
  ADD CONSTRAINT parametrocontable_ibfk_1 FOREIGN KEY (IdTipo) REFERENCES tipoparametrocontable (IdTipo),
  ADD CONSTRAINT parametrocontable_ibfk_2 FOREIGN KEY (IdCuenta) REFERENCES catalogocontable (IdCuenta);

--
-- Constraints for table `producto`
--
ALTER TABLE `producto`
  ADD CONSTRAINT producto_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT producto_ibfk_2 FOREIGN KEY (IdLinea) REFERENCES linea (IdLinea),
  ADD CONSTRAINT producto_ibfk_3 FOREIGN KEY (IdProveedor) REFERENCES proveedor (IdProveedor),
  ADD CONSTRAINT producto_ibfk_4 FOREIGN KEY (Tipo) REFERENCES tipoproducto (IdTipoProducto),
  ADD CONSTRAINT producto_ibfk_6 FOREIGN KEY (IdImpuesto) REFERENCES parametroimpuesto (IdImpuesto);

--
-- Constraints for table `proforma`
--
ALTER TABLE `proforma`
  ADD CONSTRAINT proforma_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT proforma_ibfk_2 FOREIGN KEY (IdCliente) REFERENCES `cliente` (IdCliente),
  ADD CONSTRAINT proforma_ibfk_3 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT proforma_ibfk_4 FOREIGN KEY (IdCondicionVenta) REFERENCES condicionventa (IdCondicionVenta),
  ADD CONSTRAINT proforma_ibfk_5 FOREIGN KEY (IdVendedor) REFERENCES vendedor (IdVendedor);

--
-- Constraints for table `proveedor`
--
ALTER TABLE `proveedor`
  ADD CONSTRAINT proveedor_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `reporteporempresa`
--
ALTER TABLE `reporteporempresa`
  ADD CONSTRAINT reporteporempresa_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT reporteporempresa_ibfk_2 FOREIGN KEY (IdReporte) REFERENCES catalogoreporte (IdReporte);

--
-- Constraints for table `roleporusuario`
--
ALTER TABLE `roleporusuario`
  ADD CONSTRAINT roleporusuario_ibfk_1 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT roleporusuario_ibfk_2 FOREIGN KEY (IdRole) REFERENCES role (IdRole);

--
-- Constraints for table `saldomensualcontable`
--
ALTER TABLE `saldomensualcontable`
  ADD CONSTRAINT saldomensualcontable_ibfk_1 FOREIGN KEY (IdCuenta) REFERENCES catalogocontable (IdCuenta);

--
-- Constraints for table `sucursalporempresa`
--
ALTER TABLE `sucursalporempresa`
  ADD CONSTRAINT sucursalporempresa_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);

--
-- Constraints for table `terminalporsucursal`
--
ALTER TABLE `terminalporsucursal`
  ADD CONSTRAINT terminalporsucursal_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT terminalporsucursal_ibfk_2 FOREIGN KEY (IdEmpresa, IdSucursal) REFERENCES sucursalporempresa (IdEmpresa, IdSucursal);

--
-- Constraints for table `traslado`
--
ALTER TABLE `traslado`
  ADD CONSTRAINT traslado_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT traslado_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario),
  ADD CONSTRAINT traslado_ibfk_3 FOREIGN KEY (IdSucursal) REFERENCES sucursal (IdSucursal);

--
-- Constraints for table `usuarioporempresa`
--
ALTER TABLE `usuarioporempresa`
  ADD CONSTRAINT usuarioporempresa_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT usuarioporempresa_ibfk_2 FOREIGN KEY (IdUsuario) REFERENCES usuario (IdUsuario);

--
-- Constraints for table `vendedor`
--
ALTER TABLE `vendedor`
  ADD CONSTRAINT vendedor_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa);
