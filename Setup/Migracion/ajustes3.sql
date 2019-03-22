ALTER TABLE empresa ADD (
  NombreComercial VARCHAR(80) NOT NULL,
  IdTipoIdentificacion INTEGER NOT NULL,
  Identificacion VARCHAR(20) NOT NULL,
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  IdBarrio INTEGER NOT NULL,
  Direccion VARCHAR(160) NOT NULL,
  Telefono VARCHAR(20) NOT NULL,
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
  CierrePorTurnos BIT NOT NULL,
  CierreEnEjecucion BIT NOT NULL,
  RegimenSimplificado BIT NOT NULL,
  Certificado BLOB NULL,
  NombreCertificado VARCHAR(100) NULL,
  PinCertificado VARCHAR(4) NULL,
  FechaVence DATETIME NULL,
  UltimoDocFE INTEGER NOT NULL,
  UltimoDocND INTEGER NOT NULL,
  UltimoDocNC INTEGER NOT NULL,
  UltimoDocTE INTEGER NOT NULL,
  UltimoDocMR INTEGER NOT NULL,
  INDEX (Identificacion)
);

ALTER TABLE empresa CHANGE NombreEmpresa NombreEmpresa VARCHAR(80) NOT NULL;

UPDATE empresa SET
  PorcentajeInstalacion=0,
  CodigoServicioInst=0,
  LineasPorFactura=14,
  IdTipoMoneda=1,
  CodigoServicioInst=0,
  IdProvincia=1,
  IdCanton=1,
  IdDistrito=1,
  IdBarrio=1;

ALTER TABLE documentoelectronico ADD (
  IdSucursal INTEGER NOT NULL,
  IdTerminal INTEGER NOT NULL,
  IdConsecutivo INTEGER NOT NULL,
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT,
  INDEX (EstadoEnvio),
  INDEX (ClaveNumerica, Consecutivo)
);

ALTER TABLE documentoelectronico CHANGE Consecutivo Consecutivo VARCHAR(20) NULL;

UPDATE documentoelectronico SET IdConsecutivo=CONVERT(SUBSTRING(Consecutivo,18),UNSIGNED INTEGER);
UPDATE documentoelectronico SET IdSucursal=CONVERT(SUBSTRING(Consecutivo,1,3),UNSIGNED INTEGER);
UPDATE documentoelectronico SET IdTerminal=CONVERT(SUBSTRING(Consecutivo,4,5),UNSIGNED INTEGER);