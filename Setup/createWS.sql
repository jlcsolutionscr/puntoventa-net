CREATE DATABASE facturaelectronica;

USE facturaelectronica;

CREATE TABLE provincia (
  IdProvincia INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia)
);

CREATE TABLE canton (
  IdProvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  Descripcion VARCHAR(50) NOT NULL,
  PRIMARY KEY(IdProvincia, IdCanton),
  FOREIGN KEY(IdProvincia)
    REFERENCES Provincia(IdProvincia)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE distrito (
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

CREATE TABLE barrio (
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

CREATE TABLE tipodecambiodolar (
  FechaTipoCambio VARCHAR(10),
  ValorTipoCambio DECIMAL(13,5),
  PRIMARY KEY(FechaTipoCambio)
);

CREATE TABLE padron (
  Identificacion VARCHAR(9) NOT NULL,
  IdPronvincia INTEGER NOT NULL,
  IdCanton INTEGER NOT NULL,
  IdDistrito INTEGER NOT NULL,
  Nombre VARCHAR(100) NOT NULL,
  PrimerApellido VARCHAR(100) NOT NULL,
  SegundoApellido VARCHAR(100) NOT NULL,
  PRIMARY KEY(Identificacion)
);

CREATE TABLE empresa (
  IdEmpresa INTEGER NOT NULL AUTO_INCREMENT,
  NombreEmpresa VARCHAR(200) NOT NULL,
  UsuarioHacienda VARCHAR(100) NULL,
  ClaveHacienda VARCHAR(100) NULL,
  CorreoNotificacion VARCHAR(200) NULL,
  PermiteFacturar BIT NOT NULL,
  AccessToken BLOB NULL,
  ExpiresIn INTEGER NULL,
  RefreshExpiresIn INTEGER NULL,
  RefreshToken BLOB NULL,
  EmitedAt DATETIME NULL,
  Logotipo BLOB NULL,
  PRIMARY KEY(IdEmpresa)
);

CREATE TABLE cantfemensualempresa (
  IdEmpresa INTEGER NOT NULL,
  IdMes INTEGER NOT NULL,
  IdAnio INTEGER NOT NULL,
  CantidadDoc INTEGER NOT NULL,
  PRIMARY KEY(IdEmpresa,IdMes,IdAnio),
  FOREIGN KEY(IdEmpresa)
    REFERENCES Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

CREATE TABLE documentoelectronico (
  IdDocumento INTEGER NOT NULL AUTO_INCREMENT,
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  IdTerminal INTEGER NOT NULL,
  IdTipoDocumento INTEGER NOT NULL,
  IdConsecutivo INTEGER NOT NULL,
  Fecha DATETIME NOT NULL,
  Consecutivo VARCHAR(20) NOT NULL,
  ClaveNumerica VARCHAR(50) NOT NULL,
  TipoIdentificacionEmisor VARCHAR(2) NOT NULL,
  IdentificacionEmisor VARCHAR(12) NOT NULL,
  TipoIdentificacionReceptor VARCHAR(2) NOT NULL,
  IdentificacionReceptor VARCHAR(12) NOT NULL,
  EsMensajeReceptor VARCHAR(1) NOT NULL,
  DatosDocumento BLOB NOT NULL,
  Respuesta BLOB NULL,
  EstadoEnvio VARCHAR(20) NOT NULL,
  ErrorEnvio VARCHAR(500) NULL,
  CorreoNotificacion VARCHAR(200) NOT NULL,
  PRIMARY KEY(IdDocumento),
  INDEX (ClaveNumerica),
  FOREIGN KEY(IdEmpresa)
    REFERENCES Empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);

use mysql;
CREATE USER 'usuarioConn'@'%' identified by 'Mcmddppv090281';
INSERT INTO db(host, db, user, Select_Priv, Insert_priv, Update_priv, Delete_priv) VALUES('localhost','facturaelectronica','usuarioConn','Y','Y','Y','Y');
FLUSH PRIVILEGES;