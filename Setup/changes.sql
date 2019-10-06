create table sucursalporempresa (
  IdEmpresa INTEGER NOT NULL,
  IdSucursal INTEGER NOT NULL,
  Nombre VARCHAR(160) NOT NULL,
  Direccion VARCHAR(160) NOT NULL,
  Telefono VARCHAR(20) NOT NULL,
  PRIMARY KEY(IdEmpresa, IdSucursal),
  FOREIGN KEY(IdEmpresa)
    REFERENCES empresa(IdEmpresa)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT
);
insert into sucursalporempresa select idempresa, idsucursal, NombreSucursal, Direccion, Telefono from terminalporempresa where idsucursal = 1 and nombresucursal != '';
delete from terminalporempresa where nombresucursal = '';
alter table terminalporempresa drop column nombresucursal;
alter table terminalporempresa drop column direccion;
alter table terminalporempresa drop column telefono;
RENAME TABLE terminalporempresa TO terminalporsucursal;
alter table terminalporsucursal ADD FOREIGN KEY(idempresa, idsucursal)
    REFERENCES sucursalporempresa(idempresa, idsucursal)
      ON DELETE RESTRICT
      ON UPDATE RESTRICT;