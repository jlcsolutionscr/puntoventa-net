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
ALTER TABLE empresa ADD HabilitaCodigoTransitorio bit(1) NOT NULL;
ALTER TABLE empresa ADD HabilitaCodigoImpuestoServicio bit(1) NOT NULL;
ALTER TABLE empresa ADD HabilitaFacturacionMonedaExtranjera bit(1) NOT NULL;
ALTER TABLE empresa RENAME COLUMN MontoRedondeoDescuento TO MontoRedondeoFactura;

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

ALTER TABLE tiquetedespachomercancia MODIFY IdTiquete int NOT NULL AUTO_INCREMENT;

CREATE TABLE detallemovimientocuentaporcobrar (
  IdMovCxC int NOT NULL,
  IdCxC int DEFAULT NULL,
  SaldoActual double NOT NULL,
  Monto double NOT NULL
);
ALTER TABLE detallemovimientocuentaporcobrar ADD PRIMARY KEY (IdMovCxC, IdCxC);
ALTER TABLE detallemovimientocuentaporcobrar ADD CONSTRAINT detallemovimientocuentaporcobrar_ibfk_1 FOREIGN KEY (IdMovCxC) REFERENCES movimientocuentaporcobrar (IdMovCxC);
ALTER TABLE detallemovimientocuentaporcobrar ADD CONSTRAINT detallemovimientocuentaporcobrar_ibfk_2 FOREIGN KEY (IdCxC) REFERENCES cuentaporcobrar (IdCxC);

INSERT INTO detallemovimientocuentaporcobrar SELECT IdMovCxC, IdCxC, SaldoActual, Monto FROM movimientocuentaporcobrar;

ALTER TABLE cuentaporcobrar DROP COLUMN Tipo;
ALTER TABLE movimientocuentaporcobrar DROP COLUMN Tipo;
ALTER TABLE movimientocuentaporcobrar DROP COLUMN IdCxC;
ALTER TABLE movimientocuentaporcobrar DROP COLUMN IdPropietario;
ALTER TABLE movimientocuentaporcobrar DROP COLUMN SaldoActual;
ALTER TABLE movimientocuentaporcobrar DROP COLUMN Monto;

ALTER TABLE movimientocuentaporpagar ADD CONSTRAINT movimientocuentaporpagar_ibfk_2 FOREIGN KEY (IdEmpresa,IdSucursal) REFERENCES sucursalporempresa (IdEmpresa, IdSucursal);

CREATE TABLE detallemovimientocuentaporpagar (
  IdMovCxP int NOT NULL,
  IdCxP int DEFAULT NULL,
  SaldoActual double NOT NULL,
  Monto double NOT NULL
);
ALTER TABLE detallemovimientocuentaporpagar ADD PRIMARY KEY (IdMovCxP, IdCxP);
ALTER TABLE detallemovimientocuentaporpagar ADD CONSTRAINT detallemovimientocuentaporpagar_ibfk_1 FOREIGN KEY (IdMovCxP) REFERENCES movimientocuentaporpagar (IdMovCxP);
ALTER TABLE detallemovimientocuentaporpagar ADD CONSTRAINT detallemovimientocuentaporpagar_ibfk_2 FOREIGN KEY (IdCxP) REFERENCES cuentaporpagar (IdCxP);

INSERT INTO detallemovimientocuentaporpagar SELECT IdMovCxP, IdCxP, SaldoActual, Monto FROM movimientocuentaporpagar;

ALTER TABLE cuentaporpagar DROP COLUMN Tipo;
ALTER TABLE movimientocuentaporpagar DROP COLUMN Tipo;
ALTER TABLE movimientocuentaporpagar DROP COLUMN TipoPropietario;
ALTER TABLE movimientocuentaporpagar DROP COLUMN IdCxP;
ALTER TABLE movimientocuentaporpagar DROP COLUMN IdPropietario;
ALTER TABLE movimientocuentaporpagar DROP COLUMN SaldoActual;
ALTER TABLE movimientocuentaporpagar DROP COLUMN Monto;

DELETE FROM roleporusuario WHERE IdRole IN (302, 303);
DELETE FROM role WHERE IdRole IN (302, 303);

ALTER TABLE cuentaporcobrar DROP COLUMN IdTipoMoneda;
ALTER TABLE cuentaporpagar DROP COLUMN IdTipoMoneda;
ALTER TABLE apartado DROP COLUMN IdTipoMoneda;
ALTER TABLE apartado DROP COLUMN TipoDeCambioDolar;
ALTER TABLE ordenservicio DROP COLUMN IdTipoMoneda;
ALTER TABLE ordenservicio DROP COLUMN TipoDeCambioDolar;
ALTER TABLE compra DROP COLUMN IdTipoMoneda;
ALTER TABLE compra DROP COLUMN TipoDeCambioDolar;
ALTER TABLE desglosepagoapartado DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagocompra DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagocompra DROP COLUMN Beneficiario;
ALTER TABLE desglosepagofactura DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagomovimientoapartado DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagomovimientocuentaporcobrar DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagomovimientocuentaporpagar DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagomovimientocuentaporpagar DROP COLUMN Beneficiario;
ALTER TABLE desglosepagomovimientoordenservicio DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagoordenservicio DROP COLUMN IdTipoMoneda;
ALTER TABLE desglosepagoapartado DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagocompra DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagofactura DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagomovimientoapartado DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagomovimientocuentaporcobrar DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagomovimientocuentaporpagar DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagomovimientoordenservicio DROP COLUMN TipoDeCambio;
ALTER TABLE desglosepagoordenservicio DROP COLUMN TipoDeCambio;