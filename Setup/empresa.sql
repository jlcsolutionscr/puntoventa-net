﻿USE PUNTOVENTA;
INSERT INTO Empresa (IdEmpresa, NombreEmpresa, NombreComercial, IdTipoIdentificacion, Identificacion, IdProvincia, IdCanton, IdDistrito, IdBarrio, IdTipoMoneda, Direccion, Telefono, CuentaCorreoElectronico, FechaVence, PorcentajeIVA, LineasPorFactura, Contabiliza, AutoCompletaProducto, ModificaDescProducto, DesglosaServicioInst, PorcentajeInstalacion, CodigoServicioInst, IncluyeInsumosEnFactura, RespaldoEnLinea, CierrePorTurnos, CierreEnEjecucion, FacturaElectronica)
VALUES (1, 'NOMBRE', 'NOMBRE COMERCIAL', 0, '', 1, 1, 1, 1, 1, '', '', '', NULL, 13, 0, False, False, False, False, 0, 0, False, False, False, False, True);
INSERT INTO Usuario(IdEmpresa, IdUsuario, CodigoUsuario, Clave, Modifica, AutorizaCredito)
VALUES(1, 1, 'JASLOP', 'FM3hxI7oNJztOpmPDtatI+meEpEUZkBUj+oT+M4UWQ39VKg0j0tTRVUKdPXN8eGJE7M5a43wkW0dgnJoesZY71vbmHM+4cqKGfFSHqKv7qrc1BoW0ZFIynlhdKpSiG+Ji2BmnWgr8OQFbpxluDW9QirgAPoEf8RSJvqOTM5myzHW/zIDMjmnaj6ea9vep6yvX4/UQ2GlJekRetq6bBluGKxfyPho3iSuQxPNNfOe2jN/FNgkojvUhZEEXFTITjnR4xWrIBdcyFbkQGgZnskAq0Pf6NJZvzbkDJiSyZyncwFVThKfTniGTleedBNC0JHt1WbXHhcsmChSSQ73UtKTLA==', true, true);
INSERT INTO Cliente (IdEmpresa, IdCliente, IdTipoIdentificacion, Identificacion, IdProvincia, IdCanton, IdDistrito, IdBarrio, Nombre, Direccion)
VALUES(1, 1, 0, '', 1, 1, 1, 1, 'CLIENTE DE CONTADO', '');
INSERT INTO ModuloPorEmpresa VALUES(1, 1);
INSERT INTO ModuloPorEmpresa VALUES(1, 2);
INSERT INTO ModuloPorEmpresa VALUES(1, 3);
INSERT INTO ModuloPorEmpresa VALUES(1, 4);
INSERT INTO ReportePorEmpresa VALUES(1, 1);
INSERT INTO ReportePorEmpresa VALUES(1, 2);
INSERT INTO ReportePorEmpresa VALUES(1, 3);
INSERT INTO ReportePorEmpresa VALUES(1, 11);
INSERT INTO ReportePorEmpresa VALUES(1, 14);
INSERT INTO ReportePorEmpresa VALUES(1, 15);
INSERT INTO RolePorUsuario VALUES(1,1,1);
INSERT INTO RolePorUsuario VALUES(1,1,2);
INSERT INTO RolePorUsuario VALUES(1,1,4);
INSERT INTO RolePorUsuario VALUES(1,1,5);
INSERT INTO RolePorUsuario VALUES(1,1,6);
INSERT INTO RolePorUsuario VALUES(1,1,50);
INSERT INTO RolePorUsuario VALUES(1,1,52);
INSERT INTO RolePorUsuario VALUES(1,1,53);
INSERT INTO RolePorUsuario VALUES(1,1,54);
INSERT INTO RolePorUsuario VALUES(1,1,55);
INSERT INTO RolePorUsuario VALUES(1,1,61);
INSERT INTO RolePorUsuario VALUES(1,1,101);
INSERT INTO RolePorUsuario VALUES(1,1,125);
INSERT INTO RolePorUsuario VALUES(1,1,126);
INSERT INTO RolePorUsuario VALUES(1,1,127);