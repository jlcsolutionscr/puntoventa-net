USE PUNTOVENTA;
//Roles para configurar usuarios especiales
INSERT INTO Role values(1,'ADMINISTRADOR','', '','Concede permisos de administrador');
INSERT INTO Role values(2,'CONTADOR','', '','Concede permisos para visualizar reportes');
// Modulo de parametros y otros
INSERT INTO Role values(50,'Aplicación de descuentos','','','Permite aplicar descuentos por línea de detalle');
INSERT INTO Role values(51,'Modifica precio de venta','','','Permite modificar el precio de venta del producto por facturar');
INSERT INTO Role values(52,'Modifica descripción del producto','','','Permite modificar la descripción del producto por facturar');
INSERT INTO Role values(53,'Cierre Diario','mnuArchivo','mnuArchivoCierre','Permite ejecutar el cierre diario');
INSERT INTO Role values(54,'Menu de Reportes','mnuArchivo','MnuArchivoReporte','Permite acceder el menu de reportes');
INSERT INTO Role values(55,'Parametrización Contable de Servicios','MnuParam','MnuParamPC','Permite parametrizar los servicios con las cuentas contables respectivas');
INSERT INTO Role values(56,'Parametrización de Bancos Adquirientes','MnuParam','MnuParamBA','Permite parametrizar los bancos adquirientes');
// Modulo de mantenimientos
INSERT INTO Role values(100,'Mantenimiento de Clientes','MnuMant','MnuMantCliente','Permite acceder al módulo de Mantenimiento de Clientes');
INSERT INTO Role values(101,'Mantenimiento de Particulares','MnuMant','MnuMantPArticular','Permite acceder al módulo de Mantenimiento de Particulares');
INSERT INTO Role values(102,'Mantenimiento de Líneas','MnuMant','MnuMantLinea','Permite acceder al módulo de Mantenimiento de Líneas');
INSERT INTO Role values(103,'Mantenimiento de Proveedores','MnuMant','MnuMantProveedor','Permite acceder al módulo de Mantenimiento de Proveedores');
INSERT INTO Role values(104,'Mantenimiento de Productos','MnuMant','MnuMantProducto','Permite acceder al módulo de Mantenimiento de Productos');
INSERT INTO Role values(105,'Mantenimiento de Usuarios','MnuMant','MnuMantUsuario','Permite acceder al módulo de Mantenimiento de Usuarios');
INSERT INTO Role values(106,'Mantenimiento de Sucursales','MnuMant','MnuMantSucursal','Permite acceder al módulo de Mantenimiento de Sucursales');
INSERT INTO Role values(107,'Mantenimiento de Cuentas de Ingreso','MnuMant','MnuMantCI','Permite acceder al módulo de Mantenimiento de Cuenta de Ingresos');
INSERT INTO Role values(108,'Mantenimiento de Cuentas de Egreso','MnuMant','MnuMantCE','Permite acceder al módulo de Mantenimiento de Cuenta de Egresos');
INSERT INTO Role values(109,'Mantenimiento de Cuentas Bancarias','MnuMant','MnuMantCB','Permite actualizar los saldos de las cuentas de bancos');
INSERT INTO Role values(110,'Mantenimiento de Vendedores','MnuMant','MnuMantVend','Permite actualizar los datos de los vendedores de la empresa');
INSERT INTO Role values(111,'Mantenimiento del Catalogo Contable','MnuMant','MnuMantCC','Permite actualizar los datos de las cuentas del catálogo contable');
// Modulo de captura
INSERT INTO Role values(200,'Módulo de Inventario','MnuMant','MnuMantInv','Permite acceder al módulo de Mantenimiento de Inventario');
INSERT INTO Role values(201,'Módulo de Ordenes de Compra','MnuCaptura','MnuCapturaOrden','Permite registrar las ordenes de compra');
INSERT INTO Role values(202,'Módulo de Compras','MnuCaptura','MnuCapturaCompra','Permite registrar las Compras');
INSERT INTO Role values(203,'Módulo de Proformas','MnuCaptura','MnuCapturaProforma','Permite registrar las proformas');
INSERT INTO Role values(204,'Módulo de Ordenes de Servicio','MnuCaptura','MnuCapturaOrdenServicio','Permite registrar las Ordenes de Servicio');
INSERT INTO Role values(205,'Módulo de Apartados','MnuCaptura','MnuApartado','Permite registrar los registros de apartados');
INSERT INTO Role values(206,'Módulo de Facturación','MnuCaptura','MnuCapturaFactura','Permite registrar la Facturación');
INSERT INTO Role values(207,'Módulo de Devoluciones a Provedores','MnuCaptura','MnuDevolucionProveedor','Permite registrar los movimientos de devoluciones de producto a proveedores');
INSERT INTO Role values(208,'Módulo de Devoluciones de Clientes','MnuCaptura','MnuDevolucionCliente','Permite registrar los movimientos de devoluciones de producto de clientes');
INSERT INTO Role values(209,'Módulo de Traslados','MnuCaptura','MnuCapturaTraslado','Permite registrar los movimientos de Traslados de Producto');
INSERT INTO Role values(210,'Módulo de Registro de Ingresos','MnuCaptura','MnuCapturaIngreso','Permite registrar los movimientos de Ingresos');
INSERT INTO Role values(211,'Módulo de Registro de Egresos','MnuCaptura','MnuCapturaEgreso','Permite registrar los movimientos de Egresos');
INSERT INTO Role values(212,'Módulo de Ajustes al Inventario','MnuCaptura','MnuCapturaAI','Permite registrar los ajustes de inventario');
INSERT INTO Role values(213,'Módulo de CxP Particulares','MnuCaptura','MnuCapturaCxPP','Permite registrar las cuentas por pagar a particulares');
//Modulo CXC
INSERT INTO Role values(250,'Aplicación de Créditos a Cuentas por Cobrar','MnuCC','MnuApRCxC','Permite aplicar un abono a Cuentas por Cobrar');
INSERT INTO Role values(251,'Aplicación de Créditos a Cuentas por Pagar','MnuCC','MnuApRCxP','Permite aplicar un abono a Cuentas por Pagar');
INSERT INTO Role values(252,'Aplicación de Créditos CxP Particulares','MnuCC','MnuApRCxPP','Permite aplicar un abono a Cuentas por Pagar de un particular');
INSERT INTO Role values(253,'Anulación de Créditos de Cuentas por Cobrar','MnuCC','MnuAnRCxC','Permite anular un abono a Cuentas por Cobrar');
INSERT INTO Role values(254,'Anulación de Créditos de Cuentas por Pagar','MnuCC','MnuAnRCxP','Permite anular un abono a Cuentas por Pagar');
INSERT INTO Role values(255,'Anulación de Créditos de CxP Particulares','MnuCC','MnuAnRCxPP','Permite anular un abono a Cuentas por Pagar de un particular');
INSERT INTO Role values(256,'Reimpresión de recibos de Cuentas por Cobrar','MnuCC','MnuImpCxC','Permite reimprimir recibos de cuentas por cobrar');
INSERT INTO Role values(257,'Reimpresión de recibos de Cuentas por Pagar','MnuCC','MnuImpCxP','Permite reimprimir recibos de cuentas por Pagar');
INSERT INTO Role values(258,'Reimpresión de recibos de CxP Particulares','MnuCC','MnuImpCxPP','Permite reimprimir recibos de cuentas por Pagar de un particular');
//Modulo contable
INSERT INTO Role values(300,'Módulo de movimientos en Cuentas Bancarias','MnuBC','MnuBCMov','Permite registrar y actualizar movimientos sobre cuentas');
//Modulo de cuentas bancarias
INSERT INTO Role values(350,'Módulo de Asientos Contables','mnuConta','mnuContaAsiento','Permite registrar y actualizar asientos contables de la empresa');
INSERT INTO Role values(351,'Módulo de Cierre Contable','mnuConta','mnuContaCierre','Permite realizar el cierre mensual contable');
INSERT INTO Role values(352,'Módulo de Reportes Contables','mnuConta','mnuContaReporte','Permite acceder al menu de reportes contable');
//Documentos electronicos
INSERT INTO Role values(400,'Documentos Electrónicos','MnuDocElect','MnuDocElectCDE','Permite consultar documentos pendientes de procesar');
INSERT INTO Role values(401,'Documentos Electrónicos','MnuDocElect','MnuDocElectADE','Permite registrar facturas electrónicas recibidas');
INSERT INTO Role values(402,'Documentos Electrónicos','MnuDocElect','MnuDocElectRDE','Permite revisar documentos procesados');