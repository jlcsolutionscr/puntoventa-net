using System.Data;
using System.Globalization;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICompraService
    {
        void AgregarProveedor(Proveedor cuenta);
        void ActualizarProveedor(Proveedor cuenta);
        void EliminarProveedor(int intIdCuenta);
        Proveedor ObtenerProveedor(int intIdCuenta);
        int ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre);
        IList<LlaveDescripcion> ObtenerListadoProveedores(int intIdEmpresa, int numPagina, int cantRec, string strNombre);
        string AgregarCompra(Compra compra);
        void ActualizarCompra(Compra compra);
        void AnularCompra(int intIdCompra, int intIdUsuario, string strMotivoAnulacion);
        Compra ObtenerCompra(int intIdCompra);
        int ObtenerTotalListaCompras(int intIdEmpresa, int intIdSucursal, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal);
        IList<CompraDetalle> ObtenerListadoCompras(int intIdEmpresa, int intIdSucursal,  int numPagina, int cantRec, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal);
        void AgregarOrdenCompra(OrdenCompra ordenCompra);
        void ActualizarOrdenCompra(OrdenCompra ordenCompra);
        void AnularOrdenCompra(int intIdOrdenCompra, int intIdUsuario, string strMotivoAnulacion);
        OrdenCompra ObtenerOrdenCompra(int intIdOrdenCompra);
        int ObtenerTotalListaOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenCompra, string strNombre);
        IList<OrdenCompra> ObtenerListadoOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenCompra, string strNombre);
        IList<Compra> ObtenerListadoComprasPorProveedor(int intIdProveedor);
        void AgregarDevolucionProveedor(DevolucionProveedor devolucion);
        void AnularDevolucionProveedor(int intIdDevolucion, int intIdUsuario, string strMotivoAnulacion);
        DevolucionProveedor ObtenerDevolucionProveedor(int intIdDevolucion);
        int ObtenerTotalListaDevolucionesPorProveedor(int intIdEmpresa, int intIdDevolucion, string strNombre);
        IList<DevolucionProveedor> ObtenerListadoDevolucionesPorProveedor(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre);
    }

    public class CompraService : ICompraService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory serviceScopeFactory;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public CompraService(ILoggerManager logger, IServiceScopeFactory pServiceScopeFactory)
        {
            try
            {
                _logger = logger;
                serviceScopeFactory = pServiceScopeFactory;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Compras. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.ProveedorRepository.Add(proveedor);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el proveedor: ", ex);
                    throw new Exception("Se produjo un error agregando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(proveedor);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el proveedor: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarProveedor(int intIdProveedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Proveedor proveedor = dbContext.ProveedorRepository.Find(intIdProveedor);
                    if (proveedor == null) throw new Exception("El proveedor por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.ProveedorRepository.Remove(proveedor);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al eliminar el proveedor: ", ex);
                    throw new BusinessException("No es posible eliminar el proveedor seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al eliminar el proveedor: ", ex);
                    throw new Exception("Se produjo un error eliminando al proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Proveedor ObtenerProveedor(int intIdProveedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ProveedorRepository.Find(intIdProveedor);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el proveedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaProveedores = dbContext.ProveedorRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listaProveedores = listaProveedores.Where(x => x.Nombre.Contains(strNombre));
                    return listaProveedores.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de proveedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de proveedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoProveedores(int intIdEmpresa, int numPagina, int cantRec, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaProveedores = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ProveedorRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.Nombre.Contains(strNombre));
                    if (cantRec == 0)
                        listado = listado.OrderBy(x => x.IdProveedor);
                    else
                        listado = listado.OrderBy(x => x.IdProveedor).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdProveedor, value.Nombre);
                        listaProveedores.Add(item);
                    }
                    return listaProveedores;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de proveedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de proveedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarCompra(Compra compra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                compra.Fecha = Validador.ObtenerFechaHoraCostaRica();
                decimal decTotalImpuesto = 0;
                decimal decSubTotalCompra = 0;
                decimal decTotalInventario = 0;
                ParametroContable efectivoParam = null;
                ParametroContable cuentasPorPagarProveedoresParam = null;
                ParametroContable otraCondicionVentaParam = null;
                ParametroContable ivaPorPagarParam = null;
                ParametroContable lineaParam = null;
                DataTable dtbInventarios = new DataTable();
                dtbInventarios.Columns.Add("IdLinea", typeof(int));
                dtbInventarios.Columns.Add("Total", typeof(decimal));
                dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
                CuentaPorPagar cuentaPorPagar = null;
                Asiento asiento = null;
                MovimientoBanco movimientoBanco = null;
                decimal decTipoDeCambio = 1;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == compra.IdEmpresa && x.IdSucursal == compra.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    Compra existeFactura = dbContext.CompraRepository.AsNoTracking().Where(x => x.NoDocumento == compra.NoDocumento && !x.Nulo).FirstOrDefault();
                    if (existeFactura != null) throw new BusinessException("El número de factura por registrar en la compra ya existe. . .");
                    if (compra.IdOrdenCompra > 0)
                    {
                        OrdenCompra ordenCompra = dbContext.OrdenRepository.Find(compra.IdOrdenCompra);
                        ordenCompra.Aplicado = true;
                        dbContext.NotificarModificacion(ordenCompra);
                    }
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentasPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorPagarProveedores).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.OtraCondicionVenta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        if (efectivoParam == null || cuentasPorPagarProveedoresParam == null || otraCondicionVentaParam == null || ivaPorPagarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    compra.IdCxP = 0;
                    compra.IdAsiento = 0;
                    compra.IdMovBanco = 0;
                    compra.TipoDeCambioDolar = decTipoDeCambio;
                    dbContext.CompraRepository.Add(compra);
                    if (compra.IdCondicionVenta == StaticCondicionVenta.Credito)
                    {
                        cuentaPorPagar = new CuentaPorPagar
                        {
                            IdEmpresa = compra.IdEmpresa,
                            IdSucursal = compra.IdSucursal,
                            IdUsuario = compra.IdUsuario,
                            IdTipoMoneda = compra.IdTipoMoneda,
                            IdPropietario = compra.IdProveedor,
                            Referencia = compra.NoDocumento,
                            Fecha = compra.Fecha,
                            Tipo = StaticTipoCuentaPorPagar.Proveedores,
                            Total = compra.Total,
                            Saldo = compra.Total,
                            Nulo = false
                        };
                        dbContext.CuentaPorPagarRepository.Add(cuentaPorPagar);
                    }
                    decTotalImpuesto = compra.Impuesto;
                    decSubTotalCompra = compra.Excento + compra.Gravado + compra.Descuento;
                    foreach (var detalleCompra in compra.DetalleCompra)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleCompra.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la compra no existe.");
                        if (producto.Tipo != StaticTipoProducto.Producto && producto.Tipo != StaticTipoProducto.Transitorio)
                            throw new BusinessException("El tipo del producto " + producto.Descripcion + " no puede ser un servicio. Por favor verificar.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            decimal decPrecioVenta = Math.Round(detalleCompra.PrecioVenta * (1 + (detalleCompra.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                            if (producto.PrecioVenta1 != decPrecioVenta)
                            {
                                producto.PrecioVenta1 = decPrecioVenta;
                                dbContext.NotificarModificacion(producto);
                            }
                            List<ExistenciaPorSucursal> existenciasLista = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto).ToList();
                            decimal cantidadExistente = existenciasLista.Sum(x => x.Cantidad);
                            if (producto.PrecioCosto > 0 && cantidadExistente > 0)
                            {
                                decimal decPrecioCostoPromedio = ((cantidadExistente * producto.PrecioCosto) + (detalleCompra.Cantidad * detalleCompra.PrecioCosto)) / (cantidadExistente + detalleCompra.Cantidad);
                                producto.PrecioCosto = decPrecioCostoPromedio;
                            }
                            else
                            {
                                producto.PrecioCosto = detalleCompra.PrecioCosto;
                            }
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == compra.IdSucursal).FirstOrDefault();
                            if (existencias != null)
                            {
                                existencias.Cantidad += detalleCompra.Cantidad;
                                dbContext.NotificarModificacion(existencias);
                            }
                            else
                            {
                                ExistenciaPorSucursal nuevoRegistro = new ExistenciaPorSucursal
                                {
                                    IdEmpresa = compra.IdEmpresa,
                                    IdSucursal = compra.IdSucursal,
                                    IdProducto = detalleCompra.IdProducto,
                                    Cantidad = detalleCompra.Cantidad
                                };
                                dbContext.ExistenciaPorSucursalRepository.Add(nuevoRegistro);
                            }
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = compra.IdSucursal,
                                Fecha = compra.Fecha,
                                Tipo = StaticTipoMovimientoProducto.Entrada,
                                Origen = "Registro de compra de mercancía de factura " + compra.NoDocumento,
                                Cantidad = detalleCompra.Cantidad,
                                PrecioCosto = detalleCompra.PrecioCosto
                            };
                            dbContext.MovimientoProductoRepository.Add(movimiento);
                            dbContext.NotificarModificacion(producto);
                        }
                        if (empresa.Contabiliza)
                        {
                            decimal decTotalPorLinea = detalleCompra.PrecioCosto * detalleCompra.Cantidad;
                            decTotalPorLinea = Math.Round(decTotalPorLinea - (compra.Descuento / decSubTotalCompra * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                            decTotalInventario += decTotalPorLinea;
                            int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.IdLinea));
                            if (intExiste >= 0)
                                dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + decTotalPorLinea;
                            else
                            {
                                DataRow data = dtbInventarios.NewRow();
                                data["IdLinea"] = producto.IdLinea;
                                data["Total"] = decTotalPorLinea;
                                dtbInventarios.Rows.Add(data);
                            }
                        }
                    }
                    foreach (var desglosePago in compra.DesglosePagoCompra)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario || desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = compra.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = compra.IdUsuario;
                            movimientoBanco.Fecha = compra.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeSaliente;
                                movimientoBanco.Descripcion = "Emisión de cheque por pago de compra de mercancía nro. ";
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoSaliente;
                                movimientoBanco.Descripcion = "Registro de transferencia por pago de compra de mercancía nro. ";
                            }
                            else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.NotaDebito;
                                movimientoBanco.Descripcion = "Registro de transferencia por pago de compra de mercancía nro. ";
                            }
                            movimientoBanco.Numero = desglosePago.NroMovimiento;
                            movimientoBanco.Beneficiario = dbContext.ProveedorRepository.Find(compra.IdProveedor).Nombre;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    if (empresa.Contabiliza)
                    {
                        DetalleAsiento detalleAsiento = null;
                        decimal decTotalDiff = decTotalInventario + decTotalImpuesto - compra.Total;
                        if (decTotalDiff != 0)
                        {
                            if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                            dtbInventarios.Rows[0]["Total"] = (decimal)dtbInventarios.Rows[0]["Total"] - decTotalDiff;
                            decTotalInventario -= decTotalDiff;
                        }
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = compra.IdEmpresa,
                            Fecha = compra.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de compra de mercancía nro. "
                        };
                        if (decTotalImpuesto > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = ivaPorPagarParam.IdCuenta,
                                Debito = decTotalImpuesto,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(ivaPorPagarParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        if (compra.IdCondicionVenta == StaticCondicionVenta.Credito)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = cuentasPorPagarProveedoresParam.IdCuenta,
                                Credito = compra.Total,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentasPorPagarProveedoresParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        else if (compra.IdCondicionVenta == StaticCondicionVenta.Contado)
                        {
                            foreach (var desglosePago in compra.DesglosePagoCompra)
                            {
                                if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                                {
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento = new DetalleAsiento
                                    {
                                        Linea = intLineaDetalleAsiento,
                                        IdCuenta = efectivoParam.IdCuenta,
                                        Credito = desglosePago.MontoLocal,
                                        SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                                    };
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalCredito += detalleAsiento.Credito;
                                }
                                else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                                {
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                    if (bancoParam == null)
                                        throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento = new DetalleAsiento
                                    {
                                        Linea = intLineaDetalleAsiento,
                                        IdCuenta = bancoParam.IdCuenta,
                                        Credito = desglosePago.MontoLocal,
                                        SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                    };
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalCredito += detalleAsiento.Credito;
                                }
                            }
                        }
                        else
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = otraCondicionVentaParam.IdCuenta,
                                Credito = compra.Total,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(otraCondicionVentaParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        foreach (DataRow data in dtbInventarios.Rows)
                        {
                            int intIdLinea = (int)data["IdLinea"];
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.LineaDeProductos & x.IdProducto == intIdLinea).FirstOrDefault();
                            if (lineaParam == null)
                                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = lineaParam.IdCuenta,
                                Debito = (decimal)data["Total"],
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(lineaParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    dbContext.Commit();
                    if (cuentaPorPagar != null)
                    {
                        compra.IdCxP = cuentaPorPagar.IdCxP;
                        dbContext.NotificarModificacion(compra);
                        cuentaPorPagar.NroDocOrig = compra.IdCompra;
                        dbContext.NotificarModificacion(cuentaPorPagar);
                    }
                    if (asiento != null)
                    {
                        compra.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(compra);
                        asiento.Detalle += compra.IdCompra;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        compra.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(compra);
                        movimientoBanco.Descripcion += compra.IdCompra;
                        dbContext.NotificarModificacion(movimientoBanco);
                    }
                    dbContext.Commit();
                    return compra.IdCompra.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de compra: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCompra(Compra compra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == compra.IdEmpresa && x.IdSucursal == compra.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(compra);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el registro de compra: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularCompra(int intIdCompra, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Compra compra = dbContext.CompraRepository.Include("DetalleCompra").FirstOrDefault(x => x.IdCompra == intIdCompra);
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == compra.IdEmpresa && x.IdSucursal == compra.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (compra.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    compra.Nulo = true;
                    compra.IdAnuladoPor = intIdUsuario;
                    compra.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(compra);
                    foreach (var detalleCompra in compra.DetalleCompra)
                    {
                        Producto producto = dbContext.ProductoRepository.FirstOrDefault(x => x.IdProducto == detalleCompra.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la compra no existe.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == compra.IdSucursal).FirstOrDefault();
                            if (existencias == null)
                                throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                            List<ExistenciaPorSucursal> existenciasLista = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto).ToList();
                            decimal cantidadExistente = existenciasLista.Sum(x => x.Cantidad);
                            decimal decPrecioCostoPromedio = producto.PrecioCosto;
                            if (cantidadExistente > 0)
                                decPrecioCostoPromedio = ((cantidadExistente * producto.PrecioCosto) - (detalleCompra.Cantidad * detalleCompra.PrecioCosto)) / cantidadExistente;
                            existencias.Cantidad -= detalleCompra.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = compra.IdSucursal,
                                Fecha = Validador.ObtenerFechaHoraCostaRica(),
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Anulación registro de compra de mercancía de factura " + compra.NoDocumento,
                                Cantidad = detalleCompra.Cantidad,
                                PrecioCosto = detalleCompra.PrecioCosto
                            };
                            dbContext.MovimientoProductoRepository.Add(movimiento);
                            producto.PrecioCosto = decPrecioCostoPromedio;
                            dbContext.NotificarModificacion(producto);
                        }
                    }
                    if (compra.IdCxP > 0)
                    {
                        CuentaPorPagar cuentaPorPagar = dbContext.CuentaPorPagarRepository.Find(compra.IdCxP);
                        if (cuentaPorPagar == null)
                            throw new Exception("La cuenta por pagar correspondiente a la compra no existe.");
                        cuentaPorPagar.Nulo = true;
                        cuentaPorPagar.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(cuentaPorPagar);
                        if (cuentaPorPagar.Total > cuentaPorPagar.Saldo)
                            throw new BusinessException("La cuenta por pagar generada por este registro de compra ya posee movimientos de abono. No puede ser reversada.");
                    }
                    if (compra.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger);
                        servicioContabilidad.ReversarAsientoContable(compra.IdAsiento, dbContext);
                    }
                    if (compra.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService(_logger);
                        servicioAuxiliarBancario.AnularMovimientoBanco(compra.IdMovBanco, intIdUsuario, "Anulación de registro de compra " + compra.IdCompra, dbContext);
                    }
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al anular el registro de compra: ", ex);
                    throw new Exception("Se produjo un error anulando la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public Compra ObtenerCompra(int intIdCompra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Compra compra = dbContext.CompraRepository.Include("Proveedor").Include("DetalleCompra.Producto").Include("DesglosePagoCompra").FirstOrDefault(x => x.IdCompra == intIdCompra);
                    foreach (DesglosePagoCompra desglosePago in compra.DesglosePagoCompra)
                    {
                        CuentaBanco banco = dbContext.CuentaBancoRepository.AsNoTracking().Where(x => x.IdCuenta == desglosePago.IdCuentaBanco).FirstOrDefault();
                        if (banco != null)
                            desglosePago.DescripcionCuenta = banco.Descripcion;
                        else
                            desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                    }
                    return compra;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de compra: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la compra. Por favor consulte con su proveedor..");
                }
            }
        }

        public int ObtenerTotalListaCompras(int intIdEmpresa, int intIdSucursal, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaCompras = dbContext.CompraRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdCompra > 0)
                        listaCompras = listaCompras.Where(x => x.IdCompra == intIdCompra);
                    if (!strRefFactura.Equals(string.Empty))
                        listaCompras = listaCompras.Where(x => x.NoDocumento.Contains(strRefFactura));
                    if (!strNombre.Equals(string.Empty))
                        listaCompras = listaCompras.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listaCompras = listaCompras.Where(x => x.Fecha < datFechaFinal);
                    }
                    return listaCompras.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de las compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<CompraDetalle> ObtenerListadoCompras(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdCompra, string strRefFactura, string strNombre, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCompras = new List<CompraDetalle>();
                try
                {
                    var listado = dbContext.CompraRepository.Include("Proveedor").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdCompra > 0)
                        listado = listado.Where(x => x.IdCompra == intIdCompra);
                    if (!strRefFactura.Equals(string.Empty))
                        listado = listado.Where(x => x.NoDocumento.Contains(strRefFactura));
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    if (strFechaFinal != "") {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha < datFechaFinal);
                    }
                    listado = listado.OrderByDescending(x => x.IdCompra).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var compra in listado)
                    {
                        CompraDetalle item = new CompraDetalle(compra.IdCompra, compra.NoDocumento, compra.Proveedor.Nombre, compra.Fecha.ToString("dd/MM/yyyy"), compra.Gravado, compra.Excento, compra.Impuesto, compra.Total, compra.Nulo);
                        listaCompras.Add(item);
                    }
                    return listaCompras;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de las compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarOrdenCompra(OrdenCompra ordenCompra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ordenCompra.Fecha = Validador.ObtenerFechaHoraCostaRica();
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenCompra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.OrdenRepository.Add(ordenCompra);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarOrdenCompra(OrdenCompra ordenCompra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenCompra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (ordenCompra.Aplicado == true) throw new BusinessException("La orden de servicio no puede ser modificada porque ya genero un registro de compra.");
                    List<DetalleOrdenCompra> listadoDetalleAnterior = dbContext.DetalleOrdenCompraRepository.Where(x => x.IdOrdenCompra == ordenCompra.IdOrdenCompra).ToList();
                    foreach (DetalleOrdenCompra detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(ordenCompra);
                    foreach (DetalleOrdenCompra detalle in ordenCompra.DetalleOrdenCompra)
                        dbContext.DetalleOrdenCompraRepository.Add(detalle);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularOrdenCompra(int intIdOrdenCompra, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    OrdenCompra ordenCompra = dbContext.OrdenRepository.Find(intIdOrdenCompra);
                    if (ordenCompra == null) throw new Exception("La orden de compra por anular no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenCompra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (ordenCompra.Aplicado == true) throw new BusinessException("La orden de servicio no puede ser anulada porque ya genero un registro de compra.");
                    ordenCompra.Nulo = true;
                    ordenCompra.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(ordenCompra);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al anular el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error anulando la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public OrdenCompra ObtenerOrdenCompra(int intIdOrdenCompra)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.OrdenRepository.Include("Proveedor").Include("DetalleOrdenCompra.Producto").FirstOrDefault(x => x.IdOrdenCompra == intIdOrdenCompra);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenCompra, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaOrdenesCompra = dbContext.OrdenRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!bolIncluyeTodo)
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => !x.Aplicado);
                    if (intIdOrdenCompra > 0)
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => x.IdOrdenCompra == intIdOrdenCompra);
                    else if (!strNombre.Equals(string.Empty))
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaOrdenesCompra.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de ordenes de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<OrdenCompra> ObtenerListadoOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenCompra, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaOrdenesCompra = dbContext.OrdenRepository.Include("Proveedor").Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!bolIncluyeTodo)
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => !x.Aplicado);
                    if (intIdOrdenCompra > 0)
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => x.IdOrdenCompra == intIdOrdenCompra);
                    else if (!strNombre.Equals(string.Empty))
                        listaOrdenesCompra = listaOrdenesCompra.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaOrdenesCompra.OrderByDescending(x => x.IdOrdenCompra).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de ordenes de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<Compra> ObtenerListadoComprasPorProveedor(int intIdProveedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CompraRepository.Where(x => x.IdProveedor == intIdProveedor && !x.Nulo).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de compra por proveedor: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de compras por proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarDevolucionProveedor(DevolucionProveedor devolucion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    devolucion.Fecha = Validador.ObtenerFechaHoraCostaRica();
                    Compra compra = dbContext.CompraRepository.AsNoTracking().Where(x => x.IdCompra == devolucion.IdCompra).FirstOrDefault();
                    if (compra == null) throw new Exception("La compra asignada a la devolución no existe.");
                    if (compra.Nulo) throw new BusinessException("La compra asingada a la devolución está anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    //if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    devolucion.IdAsiento = 0;
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionProveedor)
                    {
                        if (detalleDevolucion.CantDevolucion > 0)
                        {
                            Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                            if (producto == null)
                                throw new Exception("El producto asignado al detalle de la devolución no existe.");
                            if (producto.Tipo != StaticTipoProducto.Producto)
                                throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == compra.IdSucursal).FirstOrDefault();
                            if (existencias == null)
                                throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                            existencias.Cantidad -= detalleDevolucion.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = compra.IdSucursal,
                                Fecha = devolucion.Fecha,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Registro de devolución de mercancía al proveedor de factura " + compra.NoDocumento,
                                Cantidad = detalleDevolucion.CantDevolucion,
                                PrecioCosto = detalleDevolucion.PrecioCosto
                            };
                            dbContext.MovimientoProductoRepository.Add(movimiento);
                        }
                    }
                    dbContext.DevolucionProveedorRepository.Add(devolucion);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularDevolucionProveedor(int intIdDevolucion, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DevolucionProveedor devolucion = dbContext.DevolucionProveedorRepository.Include("DetalleDevolucionProveedor").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                    if (devolucion == null) throw new Exception("La devolución por anular no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    //if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (devolucion.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    Compra compra = dbContext.CompraRepository.AsNoTracking().Where(x => x.IdCompra == devolucion.IdCompra).FirstOrDefault();
                    if (compra == null) throw new Exception("La compra asignada a la devolución no existe.");
                    if (compra.Nulo) throw new BusinessException("La compra asingada a la devolución está anulada.");
                    devolucion.Nulo = true;
                    devolucion.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(devolucion);
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionProveedor)
                    {
                        Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Tipo != StaticTipoProducto.Producto)
                            throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                        ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == compra.IdSucursal).FirstOrDefault();
                        if (existencias == null)
                            throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                        existencias.Cantidad += detalleDevolucion.Cantidad;
                        dbContext.NotificarModificacion(existencias);
                        MovimientoProducto movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = compra.IdSucursal,
                            Fecha = Validador.ObtenerFechaHoraCostaRica(),
                            Tipo = StaticTipoMovimientoProducto.Entrada,
                            Origen = "Anulación de registro de devolución de mercancía al proveedor de factura " + compra.NoDocumento,
                            Cantidad = detalleDevolucion.CantDevolucion,
                            PrecioCosto = detalleDevolucion.PrecioCosto
                        };
                        dbContext.MovimientoProductoRepository.Add(movimiento);
                    }
                    if (devolucion.IdMovimientoCxP > 0)
                    {
                        MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == devolucion.IdMovimientoCxP);
                        if (movimiento == null)
                            throw new Exception("El movimiento de la cuenta por pagar correspondiente a la devolución no existe.");
                        movimiento.Nulo = true;
                        movimiento.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(movimiento);
                        CuentaPorPagar cuentaPorPagar = dbContext.CuentaPorPagarRepository.Find(movimiento.IdCxP);
                        cuentaPorPagar.Saldo += movimiento.Monto;
                        dbContext.NotificarModificacion(cuentaPorPagar);
                    }
                    if (devolucion.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger);
                        servicioContabilidad.ReversarAsientoContable(devolucion.IdAsiento, dbContext);
                    }
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al anular el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error anulando la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public DevolucionProveedor ObtenerDevolucionProveedor(int intIdDevolucion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.DevolucionProveedorRepository.Include("DetalleDevolucionProveedor.Producto").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error consultado la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaDevolucionesPorProveedor(int intIdEmpresa, int intIdDevolucion, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionProveedorRepository.Where(x => x.IdEmpresa == intIdEmpresa && !x.Nulo);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaDevoluciones.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DevolucionProveedor> ObtenerListadoDevolucionesPorProveedor(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionProveedorRepository.Include("Proveedor").Where(x => x.IdEmpresa == intIdEmpresa && !x.Nulo);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaDevoluciones.OrderByDescending(x => x.IdDevolucion).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}