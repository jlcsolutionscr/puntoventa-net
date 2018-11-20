CREATE DATABASE PUNTOVENTA;

use PUNTOVENTA;

CREATE TABLE TipoIdentificacion (
  IdTipoIdentificacion INTEGER NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdTipoIdentificacion)
);

CREATE TABLE Provincia (
  IdProvincia INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia)
);

CREATE TABLE Canton (
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia, IdCanton),
  FOREIGN KEY(IdProvincia)
    REFERENCES Provincia(IdProvincia)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Distrito (
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia, IdCanton, IdDistrito),
  FOREIGN KEY(IdProvincia, IdCanton)
    REFERENCES Canton(IdProvincia, IdCanton)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Barrio (
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia, IdCanton, IdDistrito, IdBarrio),
  FOREIGN KEY(IdProvincia, IdCanton, IdDistrito)
    REFERENCES Distrito(IdProvincia, IdCanton, IdDistrito)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE TipoMoneda (
  IdTipoMoneda INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(10) NOT NULL,
  TipoCambioCompra DOUBLE NOT NULL,
  TipoCambioVenta DOUBLE NOT NULL,
  PRIMARY KEY(IdTipoMoneda)
);

CREATE TABLE Empresa (
  IdEmpresa INTEGER NOT NULL AUTO_INCREMENT,
  NombreEmpresa VARCHAR(80) NOT NULL,
  NombreComercial VARCHAR(80) NOT NULL,
  IdTipoIdentificacion INTEGER NOT NULL,
  Identificacion VARCHAR(20) NOT NULL,
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  Direccion VARCHAR(160) NOT NULL,
  Telefono VARCHAR(20) NOT NULL,
  CuentaCorreoElectronico VARCHAR(200) NOT NULL,
  FechaVence DATETIME NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  LineasPorFactura INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  Contabiliza BIT NOT NULL,
  AutoCompletaProducto BIT NOT NULL,
  ModificaDescProducto BIT NOT NULL,
  DesglosaServicioInst BIT NOT NULL,
  PorcentajeInstalacion DOUBLE NULL,
  CodigoServicioInst INTEGER NULL,
  IncluyeInsumosEnFactura BIT NOT NULL,
  RespaldoEnLinea BIT NOT NULL,
  CierrePorTurnos BIT NOT NULL,
  CierreEnEjecucion BIT NOT NULL,
  FacturaElectronica BIT NOT NULL,
  ServicioFacturaElectronicaURL VARCHAR(500) NULL,
  IdCertificado VARCHAR(100) NULL,
  PinCertificado VARCHAR(4) NULL,
  PRIMARY KEY(IdEmpresa),
  FOREIGN KEY(IdTipoIdentificacion)
    REFERENCES TipoIdentificacion(IdTipoIdentificacion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProvincia, IdCanton, IdDistrito, IdBarrio)
    REFERENCES Barrio(IdProvincia, IdCanton, IdDistrito, IdBarrio)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleRegistro (
  IdEmpresa INTEGER NOT NULL,
  ValorRegistro INTEGER NOT NULL,
  ImpresoraFactura VARCHAR(50),
  UsaImpresoraImpacto BIT NOT NULL,
  PRIMARY KEY(IdEmpresa,ValorRegistro),
  FOREIGN KEY(IdEmpresa)
    REFERENCES Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Usuario (
  IdEmpresa INTEGER NOT NULL,
  IdUsuario INTEGER NOT NULL AUTO_INCREMENT,
  CodigoUsuario VARCHAR(10) NOT NULL,
  Clave VARCHAR(1000) NOT NULL,
  Modifica BOOL NOT NULL,
  AutorizaCredito BOOL NOT NULL,
  PRIMARY KEY(IdUsuario),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Modulo (
  IdModulo INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  MenuPadre VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdModulo)
);

CREATE TABLE ModuloPorEmpresa (
  IdEmpresa INTEGER NOT NULL,
  IdModulo INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa, IdModulo),
  FOREIGN KEY(IdEmpresa)
    REFERENCES Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdModulo)
    REFERENCES Modulo(IdModulo)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CatalogoReporte (
  IdReporte INTEGER NOT NULL,
  NombreReporte VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdReporte)
);

CREATE TABLE ReportePorEmpresa (
  IdEmpresa INTEGER NOT NULL,
  IdReporte INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa, IdReporte),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdReporte)
    REFERENCES  CatalogoReporte(IdReporte)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Role (
  IdRole INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(100) NOT NULL,
  MenuPadre VARCHAR(100) NOT NULL,
  MenuItem VARCHAR(100) NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdRole)
);

CREATE TABLE RolePorUsuario (
  IdEmpresa INTEGER NOT NULL,
  IdUsuario INTEGER NOT NULL,
  IdRole INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa,IdUsuario,IdRole),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdRole)
    REFERENCES Role(IdRole)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CierreCaja (
  IdEmpresa INTEGER NOT NULL,
  IdCierre INTEGER NOT NULL AUTO_INCREMENT,
  FechaCierre DATE,
  FondoInicio DOUBLE,
  VentasContado DOUBLE,
  VentasCredito DOUBLE,
  VentasTarjeta DOUBLE,
  OtrasVentas DOUBLE,
  VentasPorMayor DOUBLE,
  VentasDetalle DOUBLE,
  RetencionIVA DOUBLE,
  ComisionVT DOUBLE,
  LiquidacionTarjeta DOUBLE,
  IngresoCxCEfectivo DOUBLE,
  IngresoCxCTarjeta DOUBLE,
  DevolucionesProveedores DOUBLE,
  OtrosIngresos DOUBLE,
  ComprasContado DOUBLE,
  ComprasCredito DOUBLE,
  OtrasCompras DOUBLE,
  EgresoCxPEfectivo DOUBLE,
  DevolucionesClientes DOUBLE,
  OtrosEgresos DOUBLE,
  FondoCierre DOUBLE,
  PRIMARY KEY(IdCierre),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cierre_index (FechaCierre)
);

CREATE TABLE TipoCuentaContable (
  IdTipoCuenta INTEGER NOT NULL,
  TipoSaldo VARCHAR(1) NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdTipoCuenta)
);

CREATE TABLE ClaseCuentaContable (
  IdClaseCuenta INTEGER NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdClaseCuenta)
);

CREATE TABLE CatalogoContable (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Nivel_1 VARCHAR(1) NOT NULL,
  Nivel_2 VARCHAR(2) NULL,
  Nivel_3 VARCHAR(2) NULL,
  Nivel_4 VARCHAR(2) NULL,
  Nivel_5 VARCHAR(2) NULL,
  Nivel_6 VARCHAR(2) NULL,
  Nivel_7 VARCHAR(2) NULL,
  IdCuentaGrupo INTEGER NULL,
  EsCuentaBalance BIT NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  IdTipoCuenta INTEGER NOT NULL,
  IdClaseCuenta INTEGER NOT NULL,
  PermiteMovimiento BIT NOT NULL,
  PermiteSobrejiro BIT NOT NULL,
  SaldoActual DOUBLE NOT NULL DEFAULT 0,
  TotalDebito DOUBLE NOT NULL DEFAULT 0,
  TotalCredito DOUBLE NOT NULL DEFAULT 0,
  PRIMARY KEY(IdCuenta),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoCuenta)
    REFERENCES TipoCuentaContable(IdTipoCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdClaseCuenta)
    REFERENCES ClaseCuentaContable(IdClaseCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE SaldoMensualContable (
  IdCuenta INTEGER NOT NULL,
  Mes INTEGER(2) NOT NULL,
  Annio INTEGER(4) NOT NULL,
  SaldoFinMes DOUBLE NOT NULL,
  TotalDebito DOUBLE NOT NULL,
  TotalCredito DOUBLE NOT NULL,
  PRIMARY KEY(IdCuenta,Mes,Annio),
  FOREIGN KEY(IdCuenta)
    REFERENCES CatalogoContable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE TipoParametroContable (
  IdTipo INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  MultiCuenta BIT NOT NULL,
  PRIMARY KEY(IdtIPO)
);

CREATE TABLE ParametroContable (
  IdParametro INTEGER NOT NULL AUTO_INCREMENT,
  IdTipo INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL,
  IdProducto INTEGER NULL,
  PRIMARY KEY(IdParametro),
  FOREIGN KEY(IdTipo)
    REFERENCES TipoParametroContable(IdTipo)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES CatalogoContable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Asiento (
  IdEmpresa INTEGER NOT NULL,
  IdAsiento INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Detalle VARCHAR(255) NOT NULL,
  Fecha DATETIME NOT NULL,
  TotalDebito DOUBLE NOT NULL,
  TotalCredito DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdAsiento),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleAsiento (
  IdAsiento INTEGER NOT NULL,
  Linea INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL,
  Debito DOUBLE NOT NULL,
  Credito DOUBLE NOT NULL,
  SaldoAnterior DOUBLE NOT NULL,
  PRIMARY KEY(IdAsiento,Linea),
  FOREIGN KEY(IdAsiento)
    REFERENCES Asiento(IdAsiento)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES CatalogoContable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE TipoMovimientoBanco (
  IdTipoMov INTEGER NOT NULL AUTO_INCREMENT,
  DebeHaber VARCHAR(2) NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdTipoMov)
);

CREATE TABLE CuentaBanco (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(50) NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Saldo DOUBLE NOT NULL,
  PRIMARY KEY(IdCuenta),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE MovimientoBanco (
  IdMov INTEGER NOT NULL AUTO_INCREMENT,
  Fecha DATETIME NOT NULL,
  IdUsuario INTEGER NOT NULL,
  IdTipo INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL,
  Numero VARCHAR(50) NOT NULL,
  Beneficiario VARCHAR(200) NOT NULL,
  SaldoAnterior DOUBLE NOT NULL,
  Monto DOUBLE NOT NULL,
  Descripcion VARCHAR(255) NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdMov),
  FOREIGN KEY(IdCuenta)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipo)
    REFERENCES TipoMovimientoBanco(IdTipoMov)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CondicionVenta (
  IdCondicionVenta INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdCondicionVenta)
);

CREATE TABLE FormaPago (
  IdFormaPago INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdFormaPago)
);

CREATE TABLE BancoAdquiriente (
  IdEmpresa INTEGER NOT NULL,
  IdBanco INTEGER NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(30) NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PorcentajeRetencion DOUBLE NOT NULL,
  PorcentajeComision DOUBLE NOT NULL,
  PRIMARY KEY(IdBanco)
);

CREATE TABLE CuentaEgreso (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdCuenta)
);

CREATE TABLE Egreso (
  IdEmpresa INTEGER NOT NULL,
  IdEgreso INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  IdCuenta INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  Detalle VARCHAR(255) NOT NULL,
  Monto DOUBLE NOT NULL,
  IdAsiento INTEGER NULL,
  IdMovBanco INTEGER NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdEgreso),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES CuentaEgreso(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoEgreso (
  IdEgreso INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdEgreso, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdEgreso)
    REFERENCES Egreso(IdEgreso)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CuentaIngreso (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdCuenta)
);

CREATE TABLE Ingreso (
  IdEmpresa INTEGER NOT NULL,
  IdIngreso INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  IdCuenta INTEGER NOT NULL,
  RecibidoDe VARCHAR(100) NOT NULL,
  Detalle VARCHAR(255) NOT NULL,
  Monto DOUBLE NOT NULL,
  IdAsiento INTEGER NULL,
  IdMovBanco INTEGER NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdIngreso),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES CuentaEgreso(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoIngreso (
  IdIngreso INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  TipoTarjeta VARCHAR(50) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdIngreso, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdIngreso)
    REFERENCES Ingreso(IdIngreso)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Vendedor (
  IdEmpresa INTEGER NOT NULL,
  IdVendedor INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdVendedor),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Cliente (
  IdEmpresa INTEGER NOT NULL,
  IdCliente INTEGER NOT NULL AUTO_INCREMENT,
  IdTipoIdentificacion INTEGER NOT NULL,
  Identificacion VARCHAR(20) NOT NULL,
  IdentificacionExtranjero VARCHAR(20) NULL,
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  Direccion VARCHAR(160) NOT NULL,
  Nombre VARCHAR(80) NOT NULL,
  NombreComercial VARCHAR(80) NULL,
  Telefono VARCHAR(20) NULL,
  Celular VARCHAR(20) NULL,
  Fax VARCHAR(20) NULL,
  CorreoElectronico VARCHAR(200) NULL,
  IdVendedor INTEGER NULL,
  IdTipoPrecio INTEGER NULL,
  PRIMARY KEY(IdCliente),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoIdentificacion)
    REFERENCES TipoIdentificacion(IdTipoIdentificacion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProvincia, IdCanton, IdDistrito, IdBarrio)
    REFERENCES Barrio(IdProvincia, IdCanton, IdDistrito, IdBarrio)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Particular (
  IdEmpresa INTEGER NOT NULL,
  IdParticular INTEGER NOT NULL AUTO_INCREMENT,
  Identificacion VARCHAR(35),
  Nombre VARCHAR(200) NOT NULL,
  Direccion VARCHAR(300) NULL,
  Telefono VARCHAR(9) NULL,
  Celular VARCHAR(9) NULL,
  Fax VARCHAR(9) NULL,
  EMail VARCHAR(200) NULL,
  PRIMARY KEY(IdParticular),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Proveedor (
  IdEmpresa INTEGER NOT NULL,
  IdProveedor INTEGER NOT NULL AUTO_INCREMENT,
  Identificacion VARCHAR(35) NOT NULL,
  Nombre VARCHAR(200) NULL,
  Direccion VARCHAR(255) NULL,
  Telefono1 VARCHAR(9) NULL,
  Telefono2 VARCHAR(9) NULL,
  Fax VARCHAR(9) NULL,
  Correo VARCHAR(100) NULL,
  PlazoCredito INTEGER NOT NULL,
  Contacto1 VARCHAR(100) NULL,
  TelCont1 VARCHAR(9) NULL,
  Contacto2 VARCHAR(100) NULL,
  TelCont2 VARCHAR(9) NULL,
  PRIMARY KEY(IdProveedor),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE TipoProducto (
  IdTipoProducto INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(10) NOT NULL,
  PRIMARY KEY(IdTipoProducto)
);

CREATE TABLE Linea (
  IdEmpresa INTEGER NOT NULL,
  IdLinea INTEGER NOT NULL AUTO_INCREMENT,
  IdTipoProducto INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdLinea),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoProducto)
    REFERENCES TipoProducto(IdTipoProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE TipoUnidad (
  IdTipoUnidad INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(5) NOT NULL,
  PRIMARY KEY(IdTipoUnidad)
);

CREATE TABLE Producto (
  IdEmpresa INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL AUTO_INCREMENT,
  Tipo INTEGER NOT NULL,
  IdLinea INTEGER NOT NULL,
  Codigo VARCHAR(20) UNIQUE NOT NULL,
  IdProveedor INTEGER NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  PrecioVenta1 DOUBLE NOT NULL,
  PrecioVenta2 DOUBLE NOT NULL,
  PrecioVenta3 DOUBLE NOT NULL,
  PrecioVenta4 DOUBLE NOT NULL,
  PrecioVenta5 DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  IndExistencia DOUBLE NOT NULL,
  IdTipoUnidad INTEGER NOT NULL,
  Imagen LONGBLOB NULL,
  PRIMARY KEY(IdProducto),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdLinea)
    REFERENCES Linea(IdLinea)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES Proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(Tipo)
    REFERENCES TipoProducto(IdTipoProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoUnidad)
    REFERENCES TipoUnidad(IdTipoUnidad)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE MovimientoProducto (
  IdProducto INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Cantidad DOUBLE NOT NULL,
  Tipo VARCHAR(10) NOT NULL,
  Origen VARCHAR(100) NOT NULL,
  Referencia VARCHAR(100) NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  PRIMARY KEY(IdProducto, Fecha),
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Proforma (
  IdEmpresa INTEGER NOT NULL,
  IdProforma INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdCliente INTEGER NOT NULL,
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  Fecha DATETIME NOT NULL,
  NoDocumento VARCHAR(50) NULL,
  IdVendedor INTEGER NOT NULL,
  TipoPago SMALLINT NOT NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  Descuento DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Aplicado BIT NOT NULL,
  PRIMARY KEY(IdProforma),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES Cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdVendedor)
    REFERENCES Vendedor(IdVendedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleProforma (
  IdProforma INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdProforma,IdProducto),
  FOREIGN KEY(IdProforma)
    REFERENCES Proforma(IdProforma)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE OrdenServicio (
  IdEmpresa INTEGER NOT NULL,
  IdOrden INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdCliente INTEGER NOT NULL,
  IdVendedor INTEGER NULL,
  Fecha DATETIME NOT NULL,
  Operarios VARCHAR(200) NULL,
  HoraEntrada VARCHAR(15) NULL,
  HoraSalida VARCHAR(15) NULL,
  Marca VARCHAR(20) NULL,
  Modelo VARCHAR(30) NULL,
  Placa VARCHAR(10) NULL,
  Color VARCHAR(20) NULL,
  EstadoActual VARCHAR(500) NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  Descuento DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Aplicado BIT NOT NULL,
  PRIMARY KEY(IdOrden),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES Cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleOrdenServicio (
  IdOrden INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  CostoInstalacion DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdOrden,IdProducto),
  FOREIGN KEY(IdOrden)
    REFERENCES OrdenServicio(IdOrden)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE OrdenCompra (
  IdEmpresa INTEGER NOT NULL,
  IdOrdenCompra INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdProveedor INTEGER NOT NULL,
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  Fecha DATETIME NOT NULL,
  NoDocumento VARCHAR(50) NULL,
  TipoPago SMALLINT NOT NULL, 
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  Descuento DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Aplicado BIT NOT NULL,
  PRIMARY KEY(IdOrdenCompra),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES Proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleOrdenCompra (
  IdOrdenCompra INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdOrdenCompra,IdProducto),
  FOREIGN KEY(IdOrdenCompra)
    REFERENCES OrdenCompra(IdOrdenCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Factura (
  IdEmpresa INTEGER NOT NULL,
  IdFactura INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdCliente INTEGER NOT NULL,
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  Fecha DATETIME NOT NULL,
  NoDocumento VARCHAR(50) NULL,
  IdVendedor INTEGER NOT NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  Descuento DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  MontoPagado  DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  IdCxC INTEGER NOT NULL,
  IdAsiento INTEGER NOT NULL,
  IdMovBanco INTEGER NOT NULL,
  IdOrdenServicio INTEGER NOT NULL,
  IdProforma INTEGER NOT NULL,
  TotalCosto DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  IdDocElectronico VARCHAR(50) NULL,
  IdDocElectronicoRev VARCHAR(50) NULL,
  PRIMARY KEY(IdFactura),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES Cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdVendedor)
    REFERENCES Vendedor(IdVendedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxc_index (IdCxC)
);

CREATE TABLE DetalleFactura (
  IdFactura INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  CostoInstalacion DOUBLE NOT NULL,
  PRIMARY KEY(IdFactura,IdProducto),
  FOREIGN KEY(IdFactura)
    REFERENCES Factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoFactura (
  IdConsecutivo INTEGER NOT NULL AUTO_INCREMENT,
  IdFactura INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  TipoTarjeta VARCHAR(50) NULL,
  NroMovimiento VARCHAR(50),
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdConsecutivo),
  FOREIGN KEY(IdFactura)
    REFERENCES Factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CuentaPorCobrar (
  IdEmpresa INTEGER NOT NULL,
  IdCxC INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdPropietario INTEGER NOT NULL,
  Descripcion VARCHAR(255) NOT NULL,
  Referencia VARCHAR(50) NULL,
  NroDocOrig INTEGER NULL,
  Fecha DATETIME NOT NULL,
  Plazo INTEGER NOT NULL,
  Tipo SMALLINT NOT NULL,
  Total DOUBLE NOT NULL,
  Saldo DOUBLE NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdCxC),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxc_index (IdCxC)
);

CREATE TABLE MovimientoCuentaPorCobrar (
  IdEmpresa INTEGER NOT NULL,
  IdMovCxC INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdPropietario INTEGER NOT NULL,
  Tipo SMALLINT NOT NULL,
  Fecha DATETIME NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  Monto DOUBLE NOT NULL,
  IdAsiento INTEGER NOT NULL,
  IdMovBanco INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdMovCxC),
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesgloseMovimientoCuentaPorCobrar (
  IdMovCxC INTEGER NOT NULL,
  IdCxC INTEGER NOT NULL,
  Monto DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxC, IdCxC),
  FOREIGN KEY(IdMovCxC)
    REFERENCES MovimientoCuentaPorCobrar(IdMovCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCxC)
    REFERENCES CuentaPorCobrar(IdCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoMovimientoCuentaPorCobrar (
  IdMovCxC INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  TipoTarjeta VARCHAR(50) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxC, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdMovCxC)
    REFERENCES MovimientoCuentaPorCobrar(IdMovCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Compra (
  IdEmpresa INTEGER NOT NULL,
  IdCompra INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdProveedor INTEGER NOT NULL,
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  Fecha DATETIME NOT NULL,
  NoDocumento VARCHAR(20) NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  Descuento DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  IdCxP INTEGER NOT NULL,
  IdAsiento INTEGER NOT NULL,
  IdMovBanco INTEGER NOT NULL,
  IdOrdenCompra INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdCompra),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES Proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,

  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxp_index (idCxP)
);

CREATE TABLE DetalleCompra (
  IdCompra INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdCompra,IdProducto),
  FOREIGN KEY(IdCompra)
    REFERENCES Compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoCompra (
  IdCompra INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdCompra, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdCompra)
    REFERENCES Compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE CuentaPorPagar (
  IdEmpresa INTEGER NOT NULL,
  IdCxP INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdPropietario INTEGER NOT NULL,
  Descripcion VARCHAR(255) NOT NULL,
  Referencia VARCHAR(50),
  NroDocOrig INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Plazo INTEGER NOT NULL,
  Tipo SMALLINT NOT NULL,
  Total DOUBLE NOT NULL,
  Saldo DOUBLE NOT NULL,
  IdAsiento INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdCxP),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxp_index (IdCxP)
);

CREATE TABLE DesglosePagoCuentaPorPagar (
  IdCxP INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  TipoTarjeta VARCHAR(50) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdCxP, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdCxP)
    REFERENCES CuentaPorPagar(IdCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE MovimientoCuentaPorPagar (
  IdEmpresa INTEGER NOT NULL,
  IdMovCxP INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Tipo SMALLINT NOT NULL,
  IdPropietario INTEGER NOT NULL,
  TipoPropietario SMALLINT NOT NULL,
  Recibo VARCHAR(50) NOT NULL,
  Fecha DATETIME NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  Monto DOUBLE NOT NULL,
  IdAsiento INTEGER NOT NULL,
  IdMovBanco INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdMovCxP),
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesgloseMovimientoCuentaPorPagar (
  IdMovCxP INTEGER NOT NULL,
  IdCxP INTEGER NOT NULL,
  Monto DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxP, IdCxP),
  FOREIGN KEY(IdMovCxP)
    REFERENCES MovimientoCuentaPorPagar(IdMovCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCxP)
    REFERENCES CuentaPorPagar(IdCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoMovimientoCuentaPorPagar (
  IdMovCxP INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxP, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdMovCxP)
    REFERENCES MovimientoCuentaPorPagar(IdMovCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DevolucionProveedor (
  IdEmpresa INTEGER NOT NULL,
  IdDevolucion INTEGER NOT NULL AUTO_INCREMENT,
  IdCompra INTEGER NOT NULL,
  IdUsuario INTEGER NOT NULL,
  IdProveedor INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  IdMovimientoCxP INTEGER NOT NULL,
  IdAsiento INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  PRIMARY KEY(IdDevolucion),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCompra)
    REFERENCES Compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES Proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleDevolucionProveedor (
  IdDevolucion INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  CantDevolucion REAL NOT NULL,
  PRIMARY KEY(IdDevolucion,IdProducto),
  FOREIGN KEY(IdDevolucion)
    REFERENCES DevolucionProveedor(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoDevolucionProveedor (
  IdDevolucion INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdDevolucion, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdDevolucion)
    REFERENCES DevolucionProveedor(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DevolucionCliente (
  IdEmpresa INTEGER NOT NULL,
  IdDevolucion INTEGER NOT NULL AUTO_INCREMENT,
  IdFactura INTEGER NOT NULL,
  IdUsuario INTEGER NOT NULL,
  IdCliente INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Excento DOUBLE NOT NULL,
  Grabado DOUBLE NOT NULL,
  PorcentajeIVA DOUBLE NOT NULL,
  Impuesto DOUBLE NOT NULL,
  IdMovimientoCxC INTEGER NOT NULL,
  IdAsiento INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  Procesado BIT NOT NULL,
  IdDocElectronico VARCHAR(50) NULL,
  IdDocElectronicoRev VARCHAR(50) NULL,
  PRIMARY KEY(IdDevolucion),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFactura)
    REFERENCES Factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES Cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleDevolucionCliente (
  IdDevolucion INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  CantDevolucion REAL NOT NULL,
  PRIMARY KEY(IdDevolucion,IdProducto),
  FOREIGN KEY(IdDevolucion)
    REFERENCES DevolucionCliente(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DesglosePagoDevolucionCliente (
  IdDevolucion INTEGER NOT NULL,
  IdFormaPago INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  IdCuentaBanco INTEGER NOT NULL,
  Beneficiario VARCHAR(100) NULL,
  NroMovimiento VARCHAR(50) NULL,
  MontoLocal DOUBLE NOT NULL,
  MontoForaneo DOUBLE NOT NULL,
  PRIMARY KEY(IdDevolucion, IdFormaPago, IdTipoMoneda, IdCuentaBanco),
  FOREIGN KEY(IdDevolucion)
    REFERENCES DevolucionCliente(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES FormaPago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES TipoMoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES CuentaBanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE AjusteInventario (
  IdEmpresa INTEGER NOT NULL,
  IdAjuste INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Descripcion VARCHAR(500) NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdAjuste),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleAjusteInventario (
  IdAjuste INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdAjuste,IdProducto),
  FOREIGN KEY(IdAjuste)
    REFERENCES AjusteInventario(IdAjuste)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE Sucursal (
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(200) NOT NULL,
  Direccion VARCHAR(500) NOT NULL,
  Telefono VARCHAR(100) NULL,
  PRIMARY KEY(IdSucursal)
);

CREATE TABLE Traslado (
  IdEmpresa INTEGER NOT NULL,
  IdTraslado INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  Tipo INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  NoDocumento VARCHAR(50) NULL,
  Total DOUBLE NOT NULL,
  IdAsiento INTEGER NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdTraslado),
  FOREIGN KEY(IdEmpresa)
    REFERENCES  Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES Usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdSucursal)
    REFERENCES Sucursal(IdSucursal)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DetalleTraslado (
  IdTraslado INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdTraslado,IdProducto),
  FOREIGN KEY(IdTraslado)
    REFERENCES Traslado(IdTraslado)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES Producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE DocumentoElectronico (
  IdDocumento INTEGER NOT NULL AUTO_INCREMENT,
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  IdTerminal INTEGER NOT NULL,
  IdTipoDocumento INTEGER NOT NULL,
  IdConsecutivo INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  TipoIdentificacionEmisor INTEGER NOT NULL,
  IdentificacionEmisor VARCHAR(20) NOT NULL,
  TipoIdentificacionReceptor INTEGER NOT NULL,
  IdentificacionReceptor VARCHAR(12) NOT NULL,
  Consecutivo VARCHAR(20) NULL,
  ClaveNumerica VARCHAR(50) NULL,
  DatosDocumento BLOB NULL,
  Respuesta BLOB NULL,
  EstadoEnvio VARCHAR(20) NOT NULL,
  CorreoNotificacion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdDocumento),
  INDEX (ClaveNumerica)
);

DELIMITER //
CREATE FUNCTION DiffDays (dateFrom DATE, dateTo DATE) RETURNS INTEGER
  RETURN TIMESTAMPDIFF(DAY, dateFrom, dateTo);
CREATE PROCEDURE MarcaRegistrosProcesados(intIdEmpresa INT)
  BEGIN
    UPDATE Egreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE Ingreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE Factura SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE MovimientoCuentaPorCobrar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE Compra SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE MovimientoCuentaPorPagar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE DevolucionProveedor SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
    UPDATE DevolucionCliente SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND Procesado = False;
  END//
DELIMITER ;