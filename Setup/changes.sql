drop table moduloporempresa;
drop table modulo;
create table sucursalporempresa (
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  NombreSucursal VARCHAR(160) NOT NULL,
  Direccion VARCHAR(160) NOT NULL,
  Telefono VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdEmpresa, IdSucursal),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);
insert into sucursalporempresa select idempresa, idsucursal, NombreSucursal, Direccion, Telefono from terminalporempresa;
alter table terminalporempresa drop column nombresucursal;
alter table terminalporempresa drop column direccion;
alter table terminalporempresa drop column telefono;
RENAME TABLE terminalporempresa TO terminalporsucursal;
alter table terminalporsucursal ADD FOREIGN KEY(idempresa, idsucursal)
    REFERENCES sucursalporempresa(idempresa, idsucursal)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT;
update terminalporsucursal set IdTipoDispositivo = 1, ValorRegistro = "";
update usuario set PermiteRegistrarDispositivo = 1 where idusuario not in(5,9,11,12,15,16,17,18,24,25,29);