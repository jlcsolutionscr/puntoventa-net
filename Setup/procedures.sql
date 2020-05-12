DELIMITER //
DROP PROCEDURE IF EXISTS MarcaRegistrosProcesados//
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
DROP PROCEDURE IF EXISTS LimpiarRegistros//
CREATE PROCEDURE LimpiarRegistros(IN intIdEmpresa INT(11))
BEGIN
DELETE FROM desglosepagomovimientoapartado WHERE idmovapartado IN (select idmovapartado FROM movimientoapartado WHERE idempresa = intIdEmpresa);
DELETE FROM movimientoapartado WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagoapartado WHERE idapartado IN (select idapartado FROM apartado WHERE idempresa = intIdEmpresa);
DELETE FROM detalleapartado WHERE idapartado IN (select idapartado FROM apartado WHERE idempresa = intIdEmpresa);
DELETE FROM apartado WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientoordenservicio WHERE idmovorden IN (select idmovorden FROM movimientoordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM movimientoordenservicio WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagoordenservicio WHERE idorden IN (select idorden FROM ordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM detalleordenservicio WHERE idorden IN (select idorden FROM ordenservicio WHERE idempresa = intIdEmpresa);
DELETE FROM ordenservicio WHERE idempresa = intIdEmpresa;
DELETE FROM detalleproforma WHERE idproforma IN (select idproforma FROM proforma WHERE idempresa = intIdEmpresa);
DELETE FROM proforma WHERE idempresa = intIdEmpresa;
DELETE FROM detalledevolucioncliente WHERE iddevolucion IN (select iddevolucion FROM devolucioncliente WHERE idempresa = intIdEmpresa);
DELETE FROM devolucioncliente WHERE idempresa = intIdEmpresa;
DELETE FROM detallefactura WHERE idfactura IN (select idfactura FROM factura WHERE idempresa = intIdEmpresa);
DELETE FROM desglosepagofactura WHERE idfactura IN (select idfactura FROM factura WHERE idempresa = intIdEmpresa);
DELETE FROM factura WHERE idempresa = intIdEmpresa;
DELETE FROM detalleajusteinventario WHERE idajuste IN (select idajuste FROM ajusteinventario WHERE idempresa = intIdEmpresa);
DELETE FROM ajusteinventario WHERE idempresa = intIdEmpresa;
DELETE FROM detalletraslado WHERE idtraslado IN (select idtraslado FROM traslado WHERE idempresa = intIdEmpresa);
DELETE FROM traslado WHERE idempresa = intIdEmpresa;
DELETE FROM detallecompra WHERE idcompra IN (select idcompra FROM compra WHERE idempresa = intIdEmpresa);
DELETE FROM desglosepagocompra WHERE idcompra IN (select idcompra FROM compra WHERE idempresa = intIdEmpresa);
DELETE FROM compra WHERE idempresa = intIdEmpresa;
DELETE FROM movimientoproducto WHERE idproducto IN (select idproducto FROM producto WHERE idempresa = intIdEmpresa);
DELETE FROM existenciaporsucursal WHERE idproducto IN (select idproducto FROM producto WHERE idempresa = intIdEmpresa);
DELETE FROM producto WHERE idempresa = intIdEmpresa;
DELETE FROM proveedor WHERE idempresa = intIdEmpresa;
DELETE FROM linea WHERE idempresa = intIdEmpresa;
DELETE FROM cliente WHERE idempresa = intIdEmpresa;
DELETE FROM detallefacturacompra WHERE idfactcompra IN (select idfactcompra FROM facturacompra WHERE idempresa = intIdEmpresa);
DELETE FROM facturacompra WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientocuentaporcobrar WHERE idmovcxc IN (SELECT idmovcxc FROM movimientocuentaporcobrar WHERE idempresa = intIdEmpresa);
DELETE FROM movimientocuentaporcobrar WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaporcobrar WHERE idempresa = intIdEmpresa;
DELETE FROM desglosepagomovimientocuentaporpagar WHERE idmovcxp IN (SELECT idmovcxp FROM movimientocuentaporpagar WHERE idempresa = intIdEmpresa);
DELETE FROM movimientocuentaporpagar WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaporpagar WHERE idempresa = intIdEmpresa;
DELETE FROM documentoelectronico WHERE idempresa = intIdEmpresa;
DELETE FROM vendedor WHERE idempresa = intIdEmpresa;
DELETE FROM movimientobanco WHERE idcuenta in(select idcuenta FROM cuentabanco WHERE idempresa = intIdEmpresa);
DELETE FROM cuentabanco WHERE idempresa = intIdEmpresa;
DELETE FROM bancoadquiriente WHERE idempresa = intIdEmpresa;
DELETE FROM cantfemensualempresa WHERE idempresa = intIdEmpresa;
DELETE FROM reporteporempresa WHERE idempresa = intIdEmpresa;
DELETE FROM roleporempresa WHERE idempresa = intIdEmpresa;
DELETE FROM detallemovimientocierrecaja WHERE idcierre IN (SELECT idcierre FROM cierrecaja WHERE idempresa = intIdEmpresa);
DELETE FROM detalleefectivocierrecaja WHERE idcierre IN (SELECT idcierre FROM cierrecaja WHERE idempresa = intIdEmpresa);
DELETE FROM cierrecaja WHERE idempresa = intIdEmpresa;
DELETE FROM egreso WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaegreso WHERE idempresa = intIdEmpresa;
DELETE FROM ingreso WHERE idempresa = intIdEmpresa;
DELETE FROM cuentaingreso WHERE idempresa = intIdEmpresa;
DELETE FROM terminalporsucursal WHERE idempresa = intIdEmpresa;
DELETE FROM sucursalporempresa WHERE idempresa = intIdEmpresa;
END//
DELIMITER ;