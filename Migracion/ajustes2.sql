INSERT INTO `catalogoreporte` (`IdReporte`, `NombreReporte`) VALUES (1,'Ventas en general'),(2,'Ventas anuladas'),(3,'Ventas por vendedor'),(4,'Compras en general'),(5,'Compras anuladas'),(6,'Cuentas por cobrar a clientes'),(7,'Cuentas por pagar a proveedores'),(8,'Pagos a cuentas por cobrar de clientes'),(9,'Pagos a cuentas por pagar de proveedores'),(10,'Conciliación bancaria'),(11,'Resumen de movimientos'),(12,'Detalle de egresos'),(13,'Detalle de ingresos'),(14,'Reporte resumido de ventas por línea'),(15,'Reporte detallado de ventas por línea');

INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES (1,'Activo'),(2,'Pasivo'),(3,'Resultados'),(4,'Patrimonio');

INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES (1,'Contado'),(2,'Crédito'),(3,'Consignación'),(4,'Apartado'),(5,'Arrendamiento con opción de compra'),(6,'Arrendamiento en función financiera'),(99,'Otros');

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES (1,'Efectivo'),(2,'Tarjeta'),(3,'Cheque'),(4,'Transferencia - Dep. Bancario'),(5,'Racaudado por terceros'),(99,'Otros');

INSERT INTO `modulo` (`IdModulo`, `Descripcion`, `MenuPadre`) VALUES (1,'Modulo archivo','mnuArchivo'),(2,'Modulo parametros','MnuParam'),(3,'Modulo mantenimiento','MnuMant'),(4,'Modulo capturas','MnuCaptura'),(5,'Modulo CXC y CXP','MnuCC'),(6,'Modulo bancos','MnuBC'),(7,'Modulo contabilidad','mnuConta');

INSERT INTO `role` (`IdRole`, `Nombre`, `MenuPadre`, `MenuItem`, `Descripcion`) VALUES (1,'Cierre Diario','mnuArchivo','mnuArchivoCierre','Permite ejecutar el cierre diario'),(2,'Menu de Reportes','mnuArchivo','MnuArchivoReporte','Permite acceder el menu de reportes'),(5,'Parametrización de Bancos Adquirientes','MnuParam','MnuParamBA','Permite parametrizar los bancos adquirientes'),(50,'Mantenimiento de Clientes','MnuMant','MnuMantCliente','Permite acceder al módulo de Mantenimiento de Clientes'),(52,'Mantenimiento de Líneas','MnuMant','MnuMantLinea','Permite acceder al módulo de Mantenimiento de Líneas'),(53,'Mantenimiento de Proveedores','MnuMant','MnuMantProveedor','Permite acceder al módulo de Mantenimiento de Proveedores'),(54,'Mantenimiento de Productos','MnuMant','MnuMantProducto','Permite acceder al módulo de Mantenimiento de Productos'),(55,'Mantenimiento de Usuarios','MnuMant','MnuMantUsuario','Permite acceder al módulo de Mantenimiento de Usuarios'),(57,'Mantenimiento de Cuentas de Egreso','MnuMant','MnuMantCE','Permite acceder al módulo de Mantenimiento de Cuenta de Egresos'),(59,'Mantenimiento de Cuentas Bancarias','MnuMant','MnuMantCB','Permite actualizar los saldos de las cuentas de bancos'),(61,'Mantenimiento de Vendedores','MnuMant','MnuMantVend','Permite actualizar los datos de los vendedores de la empresa'),(101,'Módulo de Facturación','MnuCaptura','MnuCapturaFactura','Permite registrar la Facturación'),(125,'Consulta documentos electrónicos pendientes','MnuDocElect','MnuDocElectCDE','Permite consultar documentos electrónicos pendientes de procesar'),(109,'Módulo de Registro de Egresos','MnuCaptura','MnuCapturaEgreso','Permite registrar los movimientos de Egresos'),(126,'Recepción de facturas electrónicas','MnuDocElect','MnuDocElectADE','Permite registrar facturas electrónicas recibidas'),(127,'Lista de documentos electrónicos procesados','MnuDocElect','MnuDocElectRDE','Permite revisar documentos electrónicos procesados');

INSERT INTO `tipocuentacontable` (`IdTipoCuenta`, `TipoSaldo`, `Descripcion`) VALUES (1,'D','Deudor'),(2,'C','Acreedor');

INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES (0,'Cédula Física'),(1,'Cédula Jurídica'),(2,'DIMEX'),(3,'NITE');

INSERT INTO `tipomoneda` (`IdTipoMoneda`, `Descripcion`) VALUES (1,'Colones'),(2,'Dólares');

INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES (1,'D','Cheque Saliente'),(2,'C','Depósito Entrante'),(3,'C','Inversión'),(4,'D','Nota Débito'),(5,'C','Nota Crédito'),(6,'C','Cheque Entrante'),(7,'D','Depósito Saliente');

INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES (1,'Ingresos Ventas','\0'),(2,'Costos de Ventas','\0'),(3,'IVA Por Pagar','\0'),(4,'Líneas de Producto',''),(5,'Líneas de Servicios',''),(6,'Cuentas de Bancos',''),(7,'Efectivo','\0'),(8,'Otras Condiciones de venta','\0'),(9,'Cuentas por Cobrar Clientes','\0'),(10,'Cuentas por Cobrar Tarjeta','\0'),(11,'Gastos/Comisión Tarjeta','\0'),(12,'Cuentas por Pagar Proveedores','\0'),(13,'Cuentas por Cobrar Proveedores','\0'),(14,'Cuentas de Ingresos',''),(15,'Cuentas de Egresos',''),(16,'Traslados',''),(17,'Cuenta de Perdidas/Ganancias','\0'),(18,'Cuenta por Pagar Particulares','\0');

INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES (1,'Producto'),(2,'Servicio');

INSERT INTO `tipounidad` (`IdTipoUnidad`, `Descripcion`) VALUES (1,'UND'),(2,'SP');

INSERT INTO `usuario` (IdUsuario, CodigoUsuario, Clave, Modifica, AutorizaCredito)
VALUES(1, 'JASLOP', 'FM3hxI7oNJztOpmPDtatI+meEpEUZkBUj+oT+M4UWQ39VKg0j0tTRVUKdPXN8eGJE7M5a43wkW0dgnJoesZY71vbmHM+4cqKGfFSHqKv7qrc1BoW0ZFIynlhdKpSiG+Ji2BmnWgr8OQFbpxluDW9QirgAPoEf8RSJvqOTM5myzHW/zIDMjmnaj6ea9vep6yvX4/UQ2GlJekRetq6bBluGKxfyPho3iSuQxPNNfOe2jN/FNgkojvUhZEEXFTITjnR4xWrIBdcyFbkQGgZnskAq0Pf6NJZvzbkDJiSyZyncwFVThKfTniGTleedBNC0JHt1WbXHhcsmChSSQ73UtKTLA==', true, true);

INSERT INTO roleporusuario VALUES(1,1),(1,2),(1,5),(1,50),(1,52),(1,53),(1,55),(1,57),(1,59),(1,61),(1,101),(1,109),(1,125),(1,126),(1,127);

INSERT INTO `usuarioporempresa` VALUES(1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9);

INSERT INTO `cliente` (IdEmpresa, IdCliente, IdTipoIdentificacion, Identificacion, IdProvincia, IdCanton, IdDistrito, IdBarrio, Nombre, Direccion, IdTipoPrecio)
VALUES(1, 1, 0, '', 1, 1, 1, 1, 'CLIENTE DE CONTADO', '', 1);