DELIMITER //
CREATE PROCEDURE MarcaRegistrosProcesados(IN intIdEmpresa INT(11), IN intIdSucursal INT(11))
BEGIN
UPDATE apartado SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE ordenservicio SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Egreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Ingreso SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Factura SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoCuentaPorCobrar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE Compra SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE MovimientoCuentaPorPagar SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
UPDATE DevolucionCliente SET Procesado = True WHERE IdEmpresa = intIdEmpresa AND IdSucursal = intIdSucursal AND Procesado = False;
END//
DELIMITER ;