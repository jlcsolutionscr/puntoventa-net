--
-- Volcar la base de datos para la tabla `clasecuentacontable`
--

INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(1, 'Activo');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(2, 'Pasivo');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(3, 'Resultados');
INSERT INTO `clasecuentacontable` (`IdClaseCuenta`, `Descripcion`) VALUES(4, 'Patrimonio');

--
-- Volcar la base de datos para la tabla `condicionventa`
--

INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(1, 'Contado');
INSERT INTO `condicionventa` (`IdCondicionVenta`, `Descripcion`) VALUES(2, 'Crédito');

--
-- Volcar la base de datos para la tabla `formapago`
--

INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(1, 'Efectivo');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(2, 'Tarjeta');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(3, 'Cheque');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(4, 'Transferencia - Dep. Bancario');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(5, 'Racaudado por terceros');
INSERT INTO `formapago` (`IdFormaPago`, `Descripcion`) VALUES(99, 'Otros');

--
-- Volcar la base de datos para la tabla `parametroexoneracion`
--

INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(1, 'Compras Autorizadas');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(2, 'Ventas exentas a diplomáticos');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(3, 'Autorizado por Ley Especial');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(4, 'Exenciones Direccion General de Hacienda');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(5, 'Transitorio V');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(6, 'Transitorio IX');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(7, 'Transitorio XVII');
INSERT INTO `parametroexoneracion` (`IdTipoExoneracion`, `Descripcion`) VALUES(8, 'Otros');

--
-- Volcar la base de datos para la tabla `parametroimpuesto`
--

INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(1, 'Tarifa 0% (Exento)', 0);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(2, 'Tarifa Reducida 1%', 1);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(3, 'Tarifa Reducida 2%', 2);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(4, 'Tarifa Reducida 4%', 4);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(5, 'Transitorio 0%', 0);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(6, 'Transitorio 4%', 4);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(7, 'Transitorio 8%', 8);
INSERT INTO `parametroimpuesto` (`IdImpuesto`, `Descripcion`, `TasaImpuesto`) VALUES(8, 'Tarifa General 13%', 13);

--
-- Volcar la base de datos para la tabla `parametrosistema`
--

INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(1, 'Version', '4.1.1.0');
INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(2, 'UltimaEjecucion', '29-04-2020 10:02:48');
INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(3, 'UltimaEjecucionCorreo', '29-04-2020 10:02:48');
INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(4, 'MobileAppVersion', '1.0.5');
INSERT INTO parametrosistema (IdParametro, Descripcion, Valor) VALUES(5, 'Modo mantenimiento', 'NO');

--
-- Volcar la base de datos para la tabla `tipocuentacontable`
--

INSERT INTO `tipocuentacontable` (`IdTipoCuenta`, `TipoSaldo`, `Descripcion`) VALUES(1, 'D', 'Deudor');
INSERT INTO `tipocuentacontable` (`IdTipoCuenta`, `TipoSaldo`, `Descripcion`) VALUES(2, 'C', 'Acreedor');

--
-- Volcar la base de datos para la tabla `tipoidentificacion`
--

INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(0, 'Cédula Física');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(1, 'Cédula Jurídica');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(2, 'DIMEX');
INSERT INTO `tipoidentificacion` (`IdTipoIdentificacion`, `Descripcion`) VALUES(3, 'NITE');

--
-- Volcar la base de datos para la tabla `tipomoneda`
--

INSERT INTO `tipomoneda` (`IdTipoMoneda`, `Descripcion`) VALUES(1, 'Colones');
INSERT INTO `tipomoneda` (`IdTipoMoneda`, `Descripcion`) VALUES(2, 'Dólares');

--
-- Volcar la base de datos para la tabla `tipomovimientobanco`
--

INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(1, 'D', 'Cheque Saliente');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(2, 'C', 'Depósito Entrante');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(3, 'C', 'Inversión');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(4, 'D', 'Nota Débito');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(5, 'C', 'Nota Crédito');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(6, 'C', 'Cheque Entrante');
INSERT INTO `tipomovimientobanco` (`IdTipoMov`, `DebeHaber`, `Descripcion`) VALUES(7, 'D', 'Depósito Saliente');

--
-- Volcar la base de datos para la tabla `tipoparametrocontable`
--

INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(1, 'Ingresos Ventas', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(2, 'Costos de Ventas', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(3, 'IVA Por Pagar', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(4, 'Líneas de Producto', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(5, 'Líneas de Servicios', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(6, 'Cuentas de Bancos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(7, 'Efectivo', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(8, 'Otras Condiciones de venta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(9, 'Cuentas por Cobrar Clientes', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(10, 'Cuentas por Cobrar Tarjeta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(11, 'Gastos/Comisión Tarjeta', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(12, 'Cuentas por Pagar Proveedores', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(13, 'Cuentas por Cobrar Proveedores', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(14, 'Cuentas de Ingresos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(15, 'Cuentas de Egresos', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(16, 'Traslados', '');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(17, 'Cuenta de Perdidas/Ganancias', '\0');
INSERT INTO `tipoparametrocontable` (`IdTipo`, `Descripcion`, `MultiCuenta`) VALUES(18, 'Cuenta por Pagar Particulares', '\0');

--
-- Volcar la base de datos para la tabla `tipoproducto`
--

INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(1, 'Producto');
INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(2, 'Servicios Profesionales');
INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(3, 'Otros Servicios');
INSERT INTO `tipoproducto` (`IdTipoProducto`, `Descripcion`) VALUES(4, 'Transitorio');