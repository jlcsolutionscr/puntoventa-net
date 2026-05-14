ALTER TABLE empresa DROP COLUMN MontoCierreEfectivo;
ALTER TABLE sucursalporempresa ADD MontoCierreEfectivo DECIMAL(18, 2) NOT NULL DEFAULT 0;
ALTER TABLE cierrecaja ADD EfectivoCierreAnterior DECIMAL(18, 2) NOT NULL DEFAULT 0;