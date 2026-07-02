ALTER TABLE catalogocontable DROP CONSTRAINT catalogocontable_ibfk_2;
ALTER TABLE catalogocontable DROP COLUMN IdTipoCuenta;
ALTER TABLE catalogocontable DROP COLUMN PermiteSobrejiro;
ALTER TABLE parametrocontable DROP CONSTRAINT parametrocontable_ibfk_1;
DROP TABLE clasecuentacontable;
DROP TABLE tipocuentacontable;
DROP TABLE tipoparametrocontable;

ALTER TABLE empresa ADD DevolucionEnEfectivo bit(1) NOT NULL;
ALTER TABLE empresa ADD ImprimeTiqueteDespachoMercancia bit(1) NOT NULL;
ALTER TABLE empresa ADD ImprimeTiqueteAlFacturar bit(1) NOT NULL;

CREATE TABLE notacreditocliente (
  IdEmpresa int NOT NULL,
  IdCliente int NOT NULL,
  IdNotaCredito int NOT NULL, 
  IdUsuario int NOT NULL,
  Fecha datetime NOT NULL,
  Detalle varchar(200) DEFAULT NULL,
  Referencia int NOT NULL,
  MontoOriginal double NOT NULL,
  Saldo double NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int DEFAULT NULL
);

ALTER TABLE notacreditocliente
  ADD PRIMARY KEY (IdNotaCredito),
  ADD CONSTRAINT notacreditocliente_ibfk_1 FOREIGN KEY (IdEmpresa) REFERENCES empresa (IdEmpresa),
  ADD CONSTRAINT notacreditocliente_ibfk_2 FOREIGN KEY (IdCliente) REFERENCES cliente (IdCliente);

ALTER TABLE notacreditocliente MODIFY IdNotaCredito int NOT NULL AUTO_INCREMENT;

CREATE TABLE movimientonotacreditocliente (
  IdNotaCredito int NOT NULL,
  Consecutivo int NOT NULL,
  IdUsuario int NOT NULL,
  Fecha datetime NOT NULL,
  Monto double NOT NULL,
  IdFactura int NOT NULL,
  Nulo bit(1) NOT NULL,
  IdAnuladoPor int DEFAULT NULL
);

ALTER TABLE movimientonotacreditocliente
  ADD PRIMARY KEY (Consecutivo),
  ADD CONSTRAINT movimientonotacreditocliente_ibfk_1 FOREIGN KEY (IdNotaCredito) REFERENCES notacreditocliente (IdNotaCredito);

ALTER TABLE movimientonotacreditocliente MODIFY Consecutivo int NOT NULL AUTO_INCREMENT;

ALTER TABLE desglosepagoapartado CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagocompra CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagodevolucionproveedor CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagomovimientoapartado CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagomovimientocuentaporcobrar CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagomovimientocuentaporpagar CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagomovimientoordenservicio CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagoordenservicio CHANGE IdCuentaBanco IdReferencia int NOT NULL;
ALTER TABLE desglosepagofactura CHANGE IdCuentaBanco IdReferencia int NOT NULL;

ALTER TABLE factura ADD IdNotaCredito int NOT NULL;
ALTER TABLE devolucioncliente ADD IdNotaCredito int NOT NULL;
ALTER TABLE ordenservicio ADD IdProforma int NOT NULL;

ALTER TABLE producto DROP COLUMN Tipo;
ALTER TABLE linea ADD Tipo int NOT NULL;

DROP TABLE tiqueteordenservicio;

CREATE TABLE tiquetedespachomercancia (
  IdTiquete int NOT NULL,
  IdReferencia int NOT NULL,
  IdEmpresa int NOT NULL,
  IdSucursal int NOT NULL,
  FechaEmision varchar(25) NOT NULL,
  Etiqueta varchar(100) NOT NULL,
  Descripcion varchar(200) NOT NULL,
  Impresora varchar(100) NOT NULL,
  DetalleTiqueteDespachoMercancia varchar(10000) NOT NULL,
  Impreso bit(1) NOT NULL
);

ALTER TABLE tiquetedespachomercancia
  ADD PRIMARY KEY (IdTiquete),
  ADD KEY tiquetedespachomercancia_ibfk_1 (IdReferencia),
  ADD KEY tiquetedespachomercancia_ibfk_2 (IdEmpresa,IdSucursal);
