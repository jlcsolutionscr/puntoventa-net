using System;
using System.Linq;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface ICompraService
    {
        void AgregarProveedor(Proveedor cuenta);
        void ActualizarProveedor(Proveedor cuenta);
        void EliminarProveedor(int intIdCuenta);
        Proveedor ObtenerProveedor(int intIdCuenta);
        int ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre);
        IEnumerable<LlaveDescripcion> ObtenerListadoProveedores(int intIdEmpresa, int numPagina, int cantRec, string strNombre);
        void AgregarCompra(Compra compra);
        void ActualizarCompra(Compra compra);
        void AnularCompra(int intIdCompra, int intIdUsuario);
        Compra ObtenerCompra(int intIdCompra);
        int ObtenerTotalListaCompras(int intIdEmpresa, int intIdCompra, string strNombre);
        IEnumerable<Compra> ObtenerListadoCompras(int intIdEmpresa, int numPagina, int cantRec, int intIdCompra, string strNombre);
        void AgregarOrdenCompra(OrdenCompra ordenCompra);
        void ActualizarOrdenCompra(OrdenCompra ordenCompra);
        void AnularOrdenCompra(int intIdOrdenCompra, int intIdUsuario);
        OrdenCompra ObtenerOrdenCompra(int intIdOrdenCompra);
        int ObtenerTotalListaOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenCompra, string strNombre);
        IEnumerable<OrdenCompra> ObtenerListadoOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenCompra, string strNombre);
        IEnumerable<Compra> ObtenerListadoComprasPorProveedor(int intIdProveedor);
        void AgregarDevolucionProveedor(DevolucionProveedor devolucion);
        void AnularDevolucionProveedor(int intIdDevolucion, int intIdUsuario);
        DevolucionProveedor ObtenerDevolucionProveedor(int intIdDevolucion);
        int ObtenerTotalListaDevolucionesPorProveedor(int intIdEmpresa, int intIdDevolucion, string strNombre);
        IEnumerable<DevolucionProveedor> ObtenerListadoDevolucionesPorProveedor(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre);
    }

    public class CompraService : ICompraService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CompraService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Compras. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarProveedor(Proveedor proveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al agregar el proveedor: ", ex);
                    throw new Exception("Se produjo un error agregando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al actualizar el proveedor: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarProveedor(int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Proveedor proveedor = dbContext.ProveedorRepository.Find(intIdProveedor);
                    if (proveedor == null) throw new Exception("El proveedor por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(proveedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ProveedorRepository.Remove(proveedor);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar el proveedor: ", ex);
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
                    log.Error("Error al eliminar el proveedor: ", ex);
                    throw new Exception("Se produjo un error eliminando al proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Proveedor ObtenerProveedor(int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProveedorRepository.Find(intIdProveedor);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el proveedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProveedores(int intIdEmpresa, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al obtener el listado de proveedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de proveedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoProveedores(int intIdEmpresa, int numPagina, int cantRec, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al obtener el listado de proveedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de proveedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarCompra(Compra compra)
        {
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (compra.IdOrdenCompra > 0)
                    {
                        OrdenCompra ordenCompra = dbContext.OrdenRepository.Find(compra.IdOrdenCompra);
                        ordenCompra.Aplicado = true;
                        dbContext.NotificarModificacion(ordenCompra);
                    }
                    if (empresa.Contabiliza)
                    {
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentasPorPagarProveedoresParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarProveedores).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.OtraCondicionVenta).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        if (efectivoParam == null || cuentasPorPagarProveedoresParam == null || otraCondicionVentaParam == null || ivaPorPagarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    compra.IdCxP = 0;
                    compra.IdAsiento = 0;
                    compra.IdMovBanco = 0;
                    dbContext.CompraRepository.Add(compra);
                    if (compra.IdCondicionVenta == StaticCondicionVenta.Credito)
                    {
                        cuentaPorPagar = new CuentaPorPagar
                        {
                            IdEmpresa = compra.IdEmpresa,
                            IdUsuario = compra.IdUsuario,
                            IdPropietario = compra.IdProveedor,
                            Descripcion = "Cuenta por pagar de Compra nro. ",
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
                        if (producto.Tipo == StaticTipoProducto.Servicio)
                            throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                        if (producto.PrecioCosto != detalleCompra.PrecioCosto)
                        {
                            decimal decPrecioCostoPromedio;
                            if (producto.Cantidad > 0)
                                decPrecioCostoPromedio = ((producto.Cantidad * producto.PrecioCosto) + (detalleCompra.Cantidad * detalleCompra.PrecioCosto)) / (producto.Cantidad + detalleCompra.Cantidad);
                            else
                                decPrecioCostoPromedio = detalleCompra.PrecioCosto;
                            producto.PrecioCosto = decPrecioCostoPromedio;
                        }
                        MovimientoProducto movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            Fecha = DateTime.Now,
                            Tipo = StaticTipoMovimientoProducto.Entrada,
                            Origen = "Registro de compra",
                            Referencia = compra.NoDocumento,
                            Cantidad = detalleCompra.Cantidad,
                            PrecioCosto = detalleCompra.PrecioCosto
                        };
                        producto.MovimientoProducto.Add(movimiento);
                        producto.Cantidad += detalleCompra.Cantidad;
                        dbContext.NotificarModificacion(producto);
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
                            movimientoBanco = new MovimientoBanco();
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
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
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
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
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos & x.IdProducto == intIdLinea).FirstOrDefault();
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
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    if (cuentaPorPagar != null)
                    {
                        compra.IdCxP = cuentaPorPagar.IdCxP;
                        dbContext.NotificarModificacion(compra);
                        cuentaPorPagar.Descripcion += compra.IdCompra;
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
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de compra: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCompra(Compra compra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al actualizar el registro de compra: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularCompra(int intIdCompra, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Compra compra = dbContext.CompraRepository.Include("DetalleCompra").FirstOrDefault(x => x.IdCompra == intIdCompra);
                    Empresa empresa = dbContext.EmpresaRepository.Find(compra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (compra.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    compra.Nulo = true;
                    compra.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(compra);
                    foreach (var detalleCompra in compra.DetalleCompra)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleCompra.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la compra no existe.");
                        if (producto.PrecioCosto != detalleCompra.PrecioCosto)
                        {
                            decimal decPrecioCostoPromedio = ((producto.Cantidad * producto.PrecioCosto) - (detalleCompra.Cantidad * detalleCompra.PrecioCosto)) / (producto.Cantidad - detalleCompra.Cantidad);
                            producto.PrecioCosto = decPrecioCostoPromedio;
                        }
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Anulación registro de compra",
                                Referencia = compra.NoDocumento,
                                Cantidad = detalleCompra.Cantidad,
                                PrecioCosto = detalleCompra.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad -= detalleCompra.Cantidad;
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
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, compra.IdAsiento);
                    }
                    if (compra.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService();
                        servicioAuxiliarBancario.AnularMovimientoBanco(dbContext, compra.IdMovBanco, intIdUsuario);
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
                    log.Error("Error al anular el registro de compra: ", ex);
                    throw new Exception("Se produjo un error anulando la compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public Compra ObtenerCompra(int intIdCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CompraRepository.Include("Proveedor").Include("DetalleCompra.Producto.TipoProducto").Include("DesglosePagoCompra.CuentaBanco").Include("DesglosePagoCompra.FormaPago").Include("DesglosePagoCompra.TipoMoneda").FirstOrDefault(x => x.IdCompra == intIdCompra);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de compra: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la compra. Por favor consulte con su proveedor..");
                }
            }
        }

        public int ObtenerTotalListaCompras(int intIdEmpresa, int intIdCompra, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCompras = dbContext.CompraRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (intIdCompra > 0)
                        listaCompras = listaCompras.Where(x => x.IdCompra == intIdCompra);
                    else if (!strNombre.Equals(string.Empty))
                        listaCompras = listaCompras.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaCompras.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de las compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Compra> ObtenerListadoCompras(int intIdEmpresa, int numPagina, int cantRec, int intIdCompra, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaCompras = dbContext.CompraRepository.Include("Proveedor").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdCompra > 0)
                        listaCompras = listaCompras.Where(x => x.IdCompra == intIdCompra);
                    else if (!strNombre.Equals(string.Empty))
                        listaCompras = listaCompras.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaCompras.OrderByDescending(x => x.IdCompra).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de las compras. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarOrdenCompra(OrdenCompra ordenCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenCompra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.OrdenRepository.Add(ordenCompra);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarOrdenCompra(OrdenCompra ordenCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al actualizar el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularOrdenCompra(int intIdOrdenCompra, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    OrdenCompra ordenCompra = dbContext.OrdenRepository.Find(intIdOrdenCompra);
                    if (ordenCompra == null) throw new Exception("La orden de compra por anular no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenCompra.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    log.Error("Error al anular el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error anulando la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public OrdenCompra ObtenerOrdenCompra(int intIdOrdenCompra)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.OrdenRepository.Include("Proveedor").Include("DetalleOrdenCompra.Producto.TipoProducto").FirstOrDefault(x => x.IdOrdenCompra == intIdOrdenCompra);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la orden de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenCompra, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesCompra = dbContext.OrdenRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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
                    log.Error("Error al obtener el total del listado de registros de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de ordenes de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<OrdenCompra> ObtenerListadoOrdenesCompra(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenCompra, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesCompra = dbContext.OrdenRepository.Include("Proveedor").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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
                    log.Error("Error al obtener el listado de registros de orden de compra: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de ordenes de compra. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Compra> ObtenerListadoComprasPorProveedor(int intIdProveedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CompraRepository.Where(x => !x.Nulo & x.IdProveedor == intIdProveedor).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de compra por proveedor: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de compras por proveedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarDevolucionProveedor(DevolucionProveedor devolucion)
        {
            decimal decTotalImpuesto = 0;
            decimal decTotalInventario = 0;
            ParametroContable ivaPorPagarParam = null;
            ParametroContable efectivoParam = null;
            ParametroContable lineaParam = null;
            ParametroContable cuentasPorPagarParam = null;
            ParametroContable otraCondicionVentaParam = null;
            DataTable dtbInventarios = new DataTable();
            dtbInventarios.Columns.Add("IdLinea", typeof(int));
            dtbInventarios.Columns.Add("Total", typeof(decimal));
            dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
            Asiento asiento = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Compra compra = dbContext.CompraRepository.Find(devolucion.IdCompra);
                    if (compra == null) throw new Exception("La compra asignada a la devolución no existe.");
                    if (compra.Nulo) throw new BusinessException("La compra asingada a la devolución está anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (empresa.Contabiliza)
                    {
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentasPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorPagarProveedores).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.OtraCondicionVenta).FirstOrDefault();
                        if (ivaPorPagarParam == null || efectivoParam == null || cuentasPorPagarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    devolucion.IdAsiento = 0;
                    decTotalImpuesto = devolucion.Impuesto;
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionProveedor)
                    {
                        if (detalleDevolucion.CantDevolucion > 0)
                        {
                            Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                            if (producto == null)
                                throw new Exception("El producto asignado al detalle de la devolución no existe.");
                            if (producto.Tipo == StaticTipoProducto.Servicio)
                                throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                            else if (producto.Tipo == StaticTipoProducto.Producto)
                            {
                                producto.Cantidad -= detalleDevolucion.Cantidad;
                                if (producto.PrecioCosto != detalleDevolucion.PrecioCosto)
                                {
                                    decimal decPrecioCostoPromedio;
                                    if (producto.Cantidad > 0)
                                        decPrecioCostoPromedio = ((producto.Cantidad * producto.PrecioCosto) + (detalleDevolucion.Cantidad * detalleDevolucion.PrecioCosto)) / (producto.Cantidad + detalleDevolucion.Cantidad);
                                    else
                                        decPrecioCostoPromedio = detalleDevolucion.PrecioCosto;
                                    producto.PrecioCosto = decPrecioCostoPromedio;
                                }
                                dbContext.NotificarModificacion(producto);
                                MovimientoProducto movimientoProducto = new MovimientoProducto
                                {
                                    IdProducto = producto.IdProducto,
                                    Fecha = DateTime.Now,
                                    Tipo = StaticTipoMovimientoProducto.Salida,
                                    Origen = "Registro de devolución de mercancía al proveedor.",
                                    Referencia = devolucion.IdCompra.ToString(),
                                    Cantidad = detalleDevolucion.CantDevolucion,
                                    PrecioCosto = detalleDevolucion.PrecioCosto
                                };
                                producto.MovimientoProducto.Add(movimientoProducto);
                                if (empresa.Contabiliza)
                                {
                                    decimal decTotalPorLinea = Math.Round(detalleDevolucion.PrecioCosto * detalleDevolucion.CantDevolucion, 2, MidpointRounding.AwayFromZero);
                                    decTotalInventario += decTotalPorLinea;
                                    int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                                    if (intExiste >= 0)
                                        dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + decTotalPorLinea;
                                    else
                                    {
                                        DataRow data = dtbInventarios.NewRow();
                                        data["IdLinea"] = producto.Linea.IdLinea;
                                        data["Total"] = decTotalPorLinea;
                                        dtbInventarios.Rows.Add(data);
                                    }
                                }
                            }
                        }
                    }
                    decimal decMontoDevolucionEfectivo = devolucion.Total;
                    decimal decMontoMovimientoCxP = 0;
                    MovimientoCuentaPorPagar movimientoCuentaPorPagar = null;
                    if (compra.IdCxP > 0)
                    {
                        CuentaPorPagar cuentaPorPagar = dbContext.CuentaPorPagarRepository.Find(compra.IdCxP);
                        if (cuentaPorPagar.Saldo > 0)
                        {
                            decMontoMovimientoCxP = cuentaPorPagar.Saldo > decMontoDevolucionEfectivo ? decMontoDevolucionEfectivo : cuentaPorPagar.Saldo;
                            decMontoDevolucionEfectivo -= decMontoMovimientoCxP;
                            movimientoCuentaPorPagar = new MovimientoCuentaPorPagar
                            {
                                IdAsiento = 0,
                                IdEmpresa = devolucion.IdEmpresa,
                                IdUsuario = devolucion.IdUsuario,
                                IdPropietario = devolucion.IdProveedor,
                                Tipo = StaticTipoAbono.NotaCredito,
                                TipoPropietario = StaticTipoCuentaPorPagar.Proveedores,
                                Recibo = "N/A",
                                Descripcion = "Nota de crédito por devolución de mercancía nro. ",
                                Monto = devolucion.Total,
                                Fecha = devolucion.Fecha
                            };
                            DesgloseMovimientoCuentaPorPagar desgloseMovimiento = new DesgloseMovimientoCuentaPorPagar
                            {
                                IdCxP = compra.IdCxP,
                                Monto = decMontoMovimientoCxP
                            };
                            movimientoCuentaPorPagar.DesgloseMovimientoCuentaPorPagar.Add(desgloseMovimiento);
                            DesglosePagoMovimientoCuentaPorPagar desglosePagoMovimiento = new DesglosePagoMovimientoCuentaPorPagar
                            {
                                IdFormaPago = StaticFormaPago.Efectivo,
                                IdCuentaBanco = 1,
                                Beneficiario = null,
                                NroMovimiento = null,
                                IdTipoMoneda = StaticValoresPorDefecto.MonedaDelSistema,
                                MontoLocal = decMontoMovimientoCxP,
                                MontoForaneo = decMontoMovimientoCxP
                            };
                            movimientoCuentaPorPagar.DesglosePagoMovimientoCuentaPorPagar.Add(desglosePagoMovimiento);
                            dbContext.MovimientoCuentaPorPagarRepository.Add(movimientoCuentaPorPagar);
                            cuentaPorPagar.Saldo -= decMontoMovimientoCxP;
                            dbContext.NotificarModificacion(cuentaPorPagar);
                        }
                    }
                    DesglosePagoDevolucionProveedor desglosePagoDevolucion = new DesglosePagoDevolucionProveedor
                    {
                        IdFormaPago = StaticFormaPago.Efectivo,
                        IdCuentaBanco = 1,
                        Beneficiario = null,
                        NroMovimiento = null,
                        IdTipoMoneda = StaticValoresPorDefecto.MonedaDelSistema,
                        MontoLocal = devolucion.Total,
                        MontoForaneo = devolucion.Total
                    };
                    devolucion.DesglosePagoDevolucionProveedor.Add(desglosePagoDevolucion);
                    dbContext.DevolucionProveedorRepository.Add(devolucion);
                    if (empresa.Contabiliza)
                    {
                        decimal decTotalDiff = decTotalInventario + decTotalImpuesto - devolucion.Total;
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
                            IdEmpresa = devolucion.IdEmpresa,
                            Fecha = devolucion.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de devolución de mercancía al proveedor nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        if (decMontoDevolucionEfectivo > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = efectivoParam.IdCuenta,
                                Debito = decMontoDevolucionEfectivo,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        if (decMontoMovimientoCxP > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = cuentasPorPagarParam.IdCuenta,
                                Debito = decTotalImpuesto,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentasPorPagarParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        if (decTotalImpuesto > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = ivaPorPagarParam.IdCuenta,
                                Credito = decTotalImpuesto,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(ivaPorPagarParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        foreach (DataRow data in dtbInventarios.Rows)
                        {
                            int intIdLinea = (int)data["IdLinea"];
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos & x.IdProducto == intIdLinea).FirstOrDefault();
                            if (lineaParam == null)
                                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = lineaParam.IdCuenta,
                                Credito = (decimal)data["Total"],
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(lineaParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    if (movimientoCuentaPorPagar != null)
                    {
                        devolucion.IdMovimientoCxP = movimientoCuentaPorPagar.IdMovCxP;
                        dbContext.NotificarModificacion(devolucion);
                        movimientoCuentaPorPagar.Descripcion += devolucion.IdDevolucion;
                        dbContext.NotificarModificacion(movimientoCuentaPorPagar);
                    }
                    if (asiento != null)
                    {
                        devolucion.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(devolucion);
                        asiento.Detalle += devolucion.IdDevolucion;
                        dbContext.NotificarModificacion(asiento);
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
                    log.Error("Error al agregar el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularDevolucionProveedor(int intIdDevolucion, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DevolucionProveedor devolucion = dbContext.DevolucionProveedorRepository.Include("DetalleDevolucionProveedor").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                    if (devolucion == null) throw new Exception("La devolución por anular no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (devolucion.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    devolucion.Nulo = true;
                    devolucion.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(devolucion);
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionProveedor)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleDevolucion.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            producto.Cantidad += detalleDevolucion.Cantidad;
                            dbContext.NotificarModificacion(producto);
                            MovimientoProducto movimientoProducto = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Entrada,
                                Origen = "Anulación de registro de devolución de mercancía al proveedor.",
                                Referencia = devolucion.IdCompra.ToString(),
                                Cantidad = detalleDevolucion.CantDevolucion,
                                PrecioCosto = detalleDevolucion.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimientoProducto);
                        }
                    }
                    if (devolucion.IdMovimientoCxP > 0)
                    {
                        MovimientoCuentaPorPagar movimiento = dbContext.MovimientoCuentaPorPagarRepository.Include("DesgloseMovimientoCuentaPorPagar").FirstOrDefault(x => x.IdMovCxP == devolucion.IdMovimientoCxP);
                        if (movimiento == null)
                            throw new Exception("El movimiento de la cuenta por pagar correspondiente a la devolución no existe.");
                        movimiento.Nulo = true;
                        movimiento.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(movimiento);
                        foreach (DesgloseMovimientoCuentaPorPagar desglose in movimiento.DesgloseMovimientoCuentaPorPagar)
                        {
                            CuentaPorPagar cuentaPorPagar = dbContext.CuentaPorPagarRepository.Find(desglose.IdCxP);
                            cuentaPorPagar.Saldo += desglose.Monto;
                            dbContext.NotificarModificacion(cuentaPorPagar);
                        }
                    }
                    if (devolucion.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, devolucion.IdAsiento);
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
                    log.Error("Error al anular el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error anulando la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public DevolucionProveedor ObtenerDevolucionProveedor(int intIdDevolucion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.DevolucionProveedorRepository.Include("DetalleDevolucionProveedor.Producto").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error consultado la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaDevolucionesPorProveedor(int intIdEmpresa, int intIdDevolucion, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionProveedorRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaDevoluciones.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<DevolucionProveedor> ObtenerListadoDevolucionesPorProveedor(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionProveedorRepository.Include("Proveedor").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Proveedor.Nombre.Contains(strNombre));
                    return listaDevoluciones.OrderByDescending(x => x.IdDevolucion).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}