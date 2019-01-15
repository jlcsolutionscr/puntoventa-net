CREATE TABLE tipoidentificacion (
  IdTipoIdentificacion INTEGER NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdTipoIdentificacion)
);

CREATE TABLE detalleregistro (
  Idempresa INTEGER NOT NULL,
  ValorRegistro VARCHAR(100) NOT NULL,
  ImpresoraFactura VARCHAR(50),
  UsaImpresoraImpacto BIT NOT NULL,
  PRIMARY KEY(Idempresa,ValorRegistro),
  FOREIGN KEY(Idempresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE usuario (
  IdUsuario INTEGER NOT NULL AUTO_INCREMENT,
  CodigoUsuario VARCHAR(10) NOT NULL,
  Clave VARCHAR(1000) NOT NULL,
  Modifica BOOL NOT NULL,
  AutorizaCredito BOOL NOT NULL,
  PRIMARY KEY(IdUsuario)
);

CREATE TABLE usuarioporempresa (
  IdEmpresa INTEGER NOT NULL,
  IdUsuario INTEGER NOT NULL,
  PRIMARY KEY(IdUsuario, IdEmpresa),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE modulo (
  IdModulo INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  MenuPadre VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdModulo)
);

CREATE TABLE moduloporempresa (
  IdEmpresa INTEGER NOT NULL,
  IdModulo INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa, IdModulo),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdModulo)
    REFERENCES modulo(IdModulo)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE catalogoreporte (
  IdReporte INTEGER NOT NULL,
  NombreReporte VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdReporte)
);

CREATE TABLE reporteporempresa (
  IdEmpresa INTEGER NOT NULL,
  IdReporte INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa, IdReporte),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdReporte)
    REFERENCES catalogoreporte(IdReporte)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE role (
  IdRole INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(100) NOT NULL,
  MenuPadre VARCHAR(100) NOT NULL,
  MenuItem VARCHAR(100) NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdRole)
);

CREATE TABLE roleporusuario (
  IdUsuario INTEGER NOT NULL,
  IdRole INTEGER NOT NULL,
  PRIMARY KEY(IdUsuario,IdRole),
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdRole)
    REFERENCES role(IdRole)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE cierrecaja (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cierre_index (FechaCierre)
);

CREATE TABLE tipocuentacontable (
  IdTipoCuenta INTEGER NOT NULL,
  TipoSaldo VARCHAR(1) NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdTipoCuenta)
);

CREATE TABLE clasecuentacontable (
  IdClaseCuenta INTEGER NOT NULL,
  Descripcion VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdClaseCuenta)
);

CREATE TABLE catalogocontable (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoCuenta)
    REFERENCES tipocuentacontable(IdTipoCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdClaseCuenta)
    REFERENCES clasecuentacontable(IdClaseCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE saldomensualcontable (
  IdCuenta INTEGER NOT NULL,
  Mes INTEGER(2) NOT NULL,
  Annio INTEGER(4) NOT NULL,
  SaldoFinMes DOUBLE NOT NULL,
  TotalDebito DOUBLE NOT NULL,
  TotalCredito DOUBLE NOT NULL,
  PRIMARY KEY(IdCuenta,Mes,Annio),
  FOREIGN KEY(IdCuenta)
    REFERENCES catalogocontable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE tipoparametrocontable (
  IdTipo INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  MultiCuenta BIT NOT NULL,
  PRIMARY KEY(IdtIPO)
);

CREATE TABLE parametrocontable (
  IdParametro INTEGER NOT NULL AUTO_INCREMENT,
  IdTipo INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL,
  IdProducto INTEGER NULL,
  PRIMARY KEY(IdParametro),
  FOREIGN KEY(IdTipo)
    REFERENCES tipoparametrocontable(IdTipo)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES catalogocontable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE asiento (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalleasiento (
  IdAsiento INTEGER NOT NULL,
  Linea INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL,
  Debito DOUBLE NOT NULL,
  Credito DOUBLE NOT NULL,
  SaldoAnterior DOUBLE NOT NULL,
  PRIMARY KEY(IdAsiento,Linea),
  FOREIGN KEY(IdAsiento)
    REFERENCES asiento(IdAsiento)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES catalogocontable(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE tipomovimientobanco (
  IdTipoMov INTEGER NOT NULL AUTO_INCREMENT,
  DebeHaber VARCHAR(2) NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdTipoMov)
);

CREATE TABLE cuentabanco (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(50) NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Saldo DOUBLE NOT NULL,
  PRIMARY KEY(IdCuenta),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE movimientobanco (
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
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipo)
    REFERENCES tipomovimientobanco(IdTipoMov)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE condicionventa (
  IdCondicionVenta INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdCondicionVenta)
);

CREATE TABLE formapago (
  IdFormaPago INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdFormaPago)
);

CREATE TABLE tipomoneda (
  IdTipoMoneda INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(10) NOT NULL,
  PRIMARY KEY(IdTipoMoneda)
);

CREATE TABLE bancoadquiriente (
  IdEmpresa INTEGER NOT NULL,
  IdBanco INTEGER NOT NULL AUTO_INCREMENT,
  Codigo VARCHAR(30) NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PorcentajeRetencion DOUBLE NOT NULL,
  PorcentajeComision DOUBLE NOT NULL,
  PRIMARY KEY(IdBanco)
);

CREATE TABLE cuentaegreso (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdCuenta)
);

CREATE TABLE egreso (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES cuentaegreso(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagoegreso (
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
    REFERENCES egreso(IdEgreso)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE cuentaingreso (
  IdEmpresa INTEGER NOT NULL,
  IdCuenta INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdCuenta)
);

CREATE TABLE ingreso (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuenta)
    REFERENCES cuentaingreso(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagoingreso (
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
    REFERENCES ingreso(IdIngreso)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE vendedor (
  IdEmpresa INTEGER NOT NULL,
  IdVendedor INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdVendedor),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE cliente (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoIdentificacion)
    REFERENCES tipoidentificacion(IdTipoIdentificacion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProvincia, IdCanton, IdDistrito, IdBarrio)
    REFERENCES barrio(IdProvincia, IdCanton, IdDistrito, IdBarrio)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE particular (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE proveedor (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE tipoproducto (
  IdTipoProducto INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(10) NOT NULL,
  PRIMARY KEY(IdTipoProducto)
);

CREATE TABLE linea (
  IdEmpresa INTEGER NOT NULL,
  IdLinea INTEGER NOT NULL AUTO_INCREMENT,
  IdTipoProducto INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdLinea),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoProducto)
    REFERENCES tipoproducto(IdTipoProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE tipounidad (
  IdTipoUnidad INTEGER NOT NULL AUTO_INCREMENT,
  Descripcion VARCHAR(5) NOT NULL,
  PRIMARY KEY(IdTipoUnidad)
);

CREATE TABLE producto (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdLinea)
    REFERENCES linea(IdLinea)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(Tipo)
    REFERENCES tipoproducto(IdTipoProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoUnidad)
    REFERENCES tipounidad(IdTipoUnidad)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE movimientoproducto (
  IdProducto INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Cantidad DOUBLE NOT NULL,
  Tipo VARCHAR(10) NOT NULL,
  Origen VARCHAR(100) NOT NULL,
  Referencia VARCHAR(100) NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  PRIMARY KEY(IdProducto, Fecha),
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE proforma (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES condicionventa(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdVendedor)
    REFERENCES vendedor(IdVendedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalleproforma (
  IdProforma INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdProforma,IdProducto),
  FOREIGN KEY(IdProforma)
    REFERENCES proforma(IdProforma)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE ordenservicio (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalleordenservicio (
  IdOrden INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Descripcion VARCHAR(200) NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  CostoInstalacion DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdOrden,IdProducto),
  FOREIGN KEY(IdOrden)
    REFERENCES ordenservicio(IdOrden)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE ordencompra (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES condicionventa(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalleordencompra (
  IdOrdenCompra INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdOrdenCompra,IdProducto),
  FOREIGN KEY(IdOrdenCompra)
    REFERENCES ordencompra(IdOrdenCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE factura (
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  IdTerminal INTEGER NOT NULL,
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES condicionventa(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdVendedor)
    REFERENCES vendedor(IdVendedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxc_index (IdCxC)
);

CREATE TABLE detallefactura (
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
    REFERENCES factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagofactura (
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
    REFERENCES factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE cuentaporcobrar (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE movimientocuentaporcobrar (
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
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosemovimientocuentaporcobrar (
  IdMovCxC INTEGER NOT NULL,
  IdCxC INTEGER NOT NULL,
  Monto DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxC, IdCxC),
  FOREIGN KEY(IdMovCxC)
    REFERENCES movimientocuentaporcobrar(IdMovCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCxC)
    REFERENCES cuentaporcobrar(IdCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagomovimientocuentaporcobrar (
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
    REFERENCES movimientocuentaporcobrar(IdMovCxC)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE compra (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES condicionventa(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX cxp_index (idCxP)
);

CREATE TABLE detallecompra (
  IdCompra INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdCompra,IdProducto),
  FOREIGN KEY(IdCompra)
    REFERENCES compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagocompra (
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
    REFERENCES compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE cuentaporpagar (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagocuentaporpagar (
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
    REFERENCES cuentaporpagar(IdCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE movimientocuentaporpagar (
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
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosemovimientocuentaporpagar (
  IdMovCxP INTEGER NOT NULL,
  IdCxP INTEGER NOT NULL,
  Monto DOUBLE NOT NULL,
  PRIMARY KEY(IdMovCxP, IdCxP),
  FOREIGN KEY(IdMovCxP)
    REFERENCES movimientocuentaporpagar(IdMovCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCxP)
    REFERENCES cuentaporpagar(IdCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagomovimientocuentaporpagar (
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
    REFERENCES movimientocuentaporpagar(IdMovCxP)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE devolucionproveedor (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCompra)
    REFERENCES compra(IdCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProveedor)
    REFERENCES proveedor(IdProveedor)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalledevolucionproveedor (
  IdDevolucion INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  CantDevolucion REAL NOT NULL,
  PRIMARY KEY(IdDevolucion,IdProducto),
  FOREIGN KEY(IdDevolucion)
    REFERENCES devolucionproveedor(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagodevolucionproveedor (
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
    REFERENCES devolucionproveedor(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE devolucioncliente (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFactura)
    REFERENCES factura(IdFactura)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCliente)
    REFERENCES cliente(IdCliente)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalledevolucioncliente (
  IdDevolucion INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  PrecioVenta DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  CantDevolucion REAL NOT NULL,
  PRIMARY KEY(IdDevolucion,IdProducto),
  FOREIGN KEY(IdDevolucion)
    REFERENCES devolucioncliente(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE desglosepagodevolucioncliente (
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
    REFERENCES devolucioncliente(IdDevolucion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdFormaPago)
    REFERENCES formapago(IdFormaPago)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdTipoMoneda)
    REFERENCES tipomoneda(IdTipoMoneda)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdCuentaBanco)
    REFERENCES cuentabanco(IdCuenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE ajusteinventario (
  IdEmpresa INTEGER NOT NULL,
  IdAjuste INTEGER NOT NULL AUTO_INCREMENT,
  IdUsuario INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Descripcion VARCHAR(500) NOT NULL,
  Nulo BIT NOT NULL,
  IdAnuladoPor INTEGER NULL,
  PRIMARY KEY(IdAjuste),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalleajusteinventario (
  IdAjuste INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad DOUBLE NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdAjuste,IdProducto),
  FOREIGN KEY(IdAjuste)
    REFERENCES ajusteinventario(IdAjuste)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE sucursal (
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(200) NOT NULL,
  Direccion VARCHAR(500) NOT NULL,
  Telefono VARCHAR(100) NULL,
  PRIMARY KEY(IdSucursal)
);

CREATE TABLE traslado (
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
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdUsuario)
    REFERENCES usuario(IdUsuario)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdSucursal)
    REFERENCES Sucursal(IdSucursal)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE detalletraslado  (
  IdTraslado INTEGER NOT NULL,
  IdProducto INTEGER NOT NULL,
  Cantidad REAL NOT NULL,
  PrecioCosto DOUBLE NOT NULL,
  Excento BIT NOT NULL,
  PRIMARY KEY(IdTraslado,IdProducto),
  FOREIGN KEY(IdTraslado)
    REFERENCES traslado(IdTraslado)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProducto)
    REFERENCES producto(IdProducto)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
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