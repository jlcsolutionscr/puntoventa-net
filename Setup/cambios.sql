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

ALTER TABLE Empresa ADD (
  TipoIdentificacion INTEGER NOT NULL,
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  IdTipoMoneda INTEGER NOT NULL,
  CuentaCorreoElectronico VARCHAR(200) NOT NULL,
  FacturaElectronica BIT NOT NULL,
  ServicioHaciendaURL VARCHAR(500) NULL,
  UsuarioHacienda VARCHAR(100) NULL,
  ClaveHacienda VARCHAR(100) NULL,
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

ALTER TABLE Empresa CHANGE Direccion1 Direccion VARCHAR(160) NOT NULL;
ALTER TABLE Empresa CHANGE Nombre NombreEmpresa VARCHAR(80) NOT NULL;
ALTER TABLE Empresa CHANGE NombreFactura NombreComercial VARCHAR(80) NOT NULL;
UPDATE Empresa SET Direccion = concat(Direccion, ' ', Direccion2);
ALTER TABLE Empresa DROP COLUMN Direccion2;

ALTER TABLE Cliente ADD (
  TipoIdentificacion INTEGER NOT NULL,
  IdentificacionExtranjero VARCHAR(20) NULL,
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  NombreComercial VARCHAR(80) NULL,
  IdVendedor INTEGER NULL,
  FOREIGN KEY(IdTipoIdentificacion)
    REFERENCES TipoIdentificacion(IdTipoIdentificacion)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  FOREIGN KEY(IdProvincia, IdCanton, IdDistrito, IdBarrio)
    REFERENCES Barrio(IdProvincia, IdCanton, IdDistrito, IdBarrio)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER TABLE Cliente CHANGE Email CorreoElectronico VARCHAR(200) NULL;
ALTER TABLE FormaPago CHANGE Descripcion Descripcion VARCHAR(100) NULL;

CREATE TABLE CondicionVenta (
  IdCondicionVenta INTEGER NOT NULL,
  Descripcion VARCHAR(100) NOT NULL,
  PRIMARY KEY(IdCondicionVenta)
);

ALTER TABLE Proforma ADD (
  IdCondicionVenta INTEGER NOT NULL,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER TABLE Proforma CHANGE Plazo PlazoCredito INTEGER NULL;

ALTER TABLE Proforma DROP COLUMN VentaPorMayor;
ALTER TABLE Proforma DROP COLUMN NombreCliente;

ALTER TABLE OrdenServicio DROP COLUMN VentaPorMayor;
ALTER TABLE OrdenServicio DROP COLUMN NombreCliente;

ALTER TABLE Orden ADD (
  IdCondicionVenta INTEGER NOT NULL,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER TABLE Orden CHANGE IdOrden IdOrdenCompra INTEGER NOT NULL AUTO_INCREMENT;
ALTER TABLE Orden CHANGE Plazo PlazoCredito INTEGER NULL;

RENAME TABLE Orden TO OrdenCompra;

ALTER TABLE DetalleOrden DROP FOREIGN KEY detalleordencompra_ibfk_1;
ALTER TABLE DetalleOrden CHANGE IdOrden IdOrdenCompra INTEGER NOT NULL;

RENAME TABLE DetalleOrden TO DetalleOrdenCompra;

ALTER TABLE DetalleOrdenCompra ADD (
  FOREIGN KEY(IdOrdenCompra)
    REFERENCES OrdenCompra(IdOrdenCompra)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER TABLE Factura ADD (
  IdSucursal INTEGER NOT NULL,
  IdTerminal INTEGER NOT NULL,
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  IdDocElectronico VARCHAR(50) NULL,
  IdDocElectronicoRev VARCHAR(50) NULL,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER TABLE Factura DROP COLUMN VentaPorMayor;
ALTER TABLE Factura DROP COLUMN NombreCliente;

ALTER  TABLE DesglosePagoFactura DROP COLUMN Plazo;

ALTER TABLE Compra ADD (
  IdCondicionVenta INTEGER NOT NULL,
  PlazoCredito INTEGER NULL,
  FOREIGN KEY(IdCondicionVenta)
    REFERENCES CondicionVenta(IdCondicionVenta)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

ALTER  TABLE DesglosePagoCompra DROP COLUMN Plazo;

ALTER TABLE DevolucionCliente ADD (
  IdDocElectronico VARCHAR(50) NULL,
  IdDocElectronicoRev VARCHAR(50) NULL
);