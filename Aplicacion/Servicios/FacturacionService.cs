using System;
using System.Linq;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using LeandroSoftware.Core.CommonTypes;
using LeandroSoftware.PuntoVenta.Dominio;
using LeandroSoftware.PuntoVenta.Dominio.Entidades;
using LeandroSoftware.PuntoVenta.Datos;
using log4net;
using Unity;
using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using System.Threading.Tasks;

namespace LeandroSoftware.PuntoVenta.Servicios
{
    public interface IFacturacionService
    {
        Cliente AgregarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void EliminarCliente(int intIdCliente);
        Cliente ObtenerCliente(int intIdCliente);
        Cliente ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion);
        int ObtenerTotalListaClientes(int intIdEmpresa, string strNombre = "", bool incluyeClienteContado = false);
        IEnumerable<Cliente> ObtenerListaClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre = "", bool incluyeClienteContado = false);
        Factura AgregarFactura(Factura factura, int intSucursal, int intTerminal);
        void ActualizarFactura(Factura factura);
        void AnularFactura(int intIdFactura, int intIdUsuario, int intSucursal, int intTerminal);
        Factura ObtenerFactura(int intIdFactura);
        int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura = 0, string strNombre = "");
        IEnumerable<Factura> ObtenerListaFacturas(int intIdEmpresa, int numPagina, int cantRec, int intIdFactura = 0, string strNombre = "");
        IEnumerable<Factura> ObtenerListaFacturasPorCliente(int intIdCliente);
        Proforma AgregarProforma(Proforma proforma);
        void ActualizarProforma(Proforma proforma);
        void AnularProforma(int intIdProforma, int intIdUsuario);
        Proforma ObtenerProforma(int intIdProforma);
        int ObtenerTotalListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int intIdProforma = 0, string strNombre = "");
        IEnumerable<Proforma> ObtenerListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdProforma = 0, string strNombre = "");
        OrdenServicio AgregarOrdenServicio(OrdenServicio ordenServicio);
        void ActualizarOrdenServicio(OrdenServicio ordenServicio);
        void AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario);
        OrdenServicio ObtenerOrdenServicio(int intIdOrdenServicio);
        int ObtenerTotalListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenServicio = 0, string strNombre = "");
        IEnumerable<OrdenServicio> ObtenerListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenServicio = 0, string strNombre = "");
        DevolucionCliente AgregarDevolucionCliente(DevolucionCliente devolucion);
        void AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario);
        DevolucionCliente ObtenerDevolucionCliente(int intIdDevolucion);
        int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdDevolucion = 0, string strNombre = "");
        IEnumerable<DevolucionCliente> ObtenerListaDevolucionesPorCliente(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion = 0, string strNombre = "");
        void GeneraMensajeReceptor(string datos, int intIdEmpresa, int intSucursal, int intTerminal, int intMensaje);
        Task<IList<DocumentoElectronico>> ObtenerListaDocumentosElectronicosPendientes(int intIdEmpresa);
        void ProcesarDocumentosElectronicosPendientes(int intIdEmpresa, IList<DocumentoElectronico> listaPendientes);
    }

    public class FacturacionService : IFacturacionService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FacturacionService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Facturación. Por favor consulte con su proveedor.");
            }
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ClienteRepository.Add(cliente);
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
                    log.Error("Error al agregar el cliente: ", ex);
                    throw new Exception("Se produjo un error agregando al cliente. Por favor consulte con su proveedor.");
                }
            }
            return cliente;
        }

        public void ActualizarCliente(Cliente cliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(cliente);
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
                    log.Error("Error al actualizar el cliente: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCliente(int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Cliente cliente = dbContext.ClienteRepository.Find(intIdCliente);
                    if (cliente == null)
                        throw new Exception("El cliente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ClienteRepository.Remove(cliente);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar cliente: ", ex);
                    throw new BusinessException("No es posible eliminar el cliente seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el cliente: ", ex);
                    throw new Exception("Se produjo un error eliminando al cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public Cliente ObtenerCliente(int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var cliente = dbContext.ClienteRepository.Find(intIdCliente);
                    if (cliente.IdVendedor != null)
                    {
                        var vendedor = dbContext.VendedorRepository.Find(cliente.IdVendedor);
                        cliente.Vendedor = vendedor;
                    }
                    return cliente;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el cliente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public Cliente ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    Cliente cliente = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Identificacion == strIdentificacion).FirstOrDefault();
                    if (cliente != null)
                        return cliente;
                    else
                    {
                        PadronDTO persona = ComprobanteElectronicoService.ConsultarPersonaPorIdentificacion(empresa, strIdentificacion);
                        if (persona.Identificacion == null)
                            return null;
                        else
                        {
                            cliente = new Cliente();
                            cliente.Identificacion = persona.Identificacion;
                            cliente.IdProvincia = persona.IdProvincia;
                            cliente.IdCanton = persona.IdCanton;
                            cliente.IdDistrito = persona.IdDistrito;
                            cliente.Nombre = persona.NombreCompleto;
                            return cliente;
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el cliente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaClientes(int intIdEmpresa, string strNombre = "", bool incluyeClienteContado = false)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaClientes = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!incluyeClienteContado)
                        listaClientes = listaClientes.Where(x => x.IdCliente > 1);
                    if (!strNombre.Equals(string.Empty))
                        listaClientes = listaClientes.Where(x => x.Nombre.Contains(strNombre));
                    return listaClientes.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de clientes: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Cliente> ObtenerListaClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre = "", bool incluyeClienteContado = false)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaClientes = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!incluyeClienteContado)
                        listaClientes = listaClientes.Where(x => x.IdCliente > 1);
                    if (!strNombre.Equals(string.Empty))
                        listaClientes = listaClientes.Where(x => x.Nombre.Contains(strNombre));
                    if (cantRec == 0)
                        return listaClientes.OrderBy(x => x.Nombre).OrderBy(x => x.Nombre).ToList();
                    else
                        return listaClientes.OrderBy(x => x.IdCliente).Skip((numPagina - 1) * cantRec).Take(cantRec).OrderBy(x => x.Nombre).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de clientes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public Factura AgregarFactura(Factura factura, int intSucursal, int intTerminal)
        {
            decimal decTotalIngresosMercancia = 0;
            decimal decTotalIngresosServicios = 0;
            decimal decSubTotalFactura = 0;
            decimal decTotalImpuesto = 0;
            decimal decTotalCostoVentas = 0;
            ParametroContable ingresosVentasParam = null;
            ParametroContable costoVentasParam = null;
            ParametroContable ivaPorPagarParam = null;
            ParametroContable efectivoParam = null;
            ParametroContable cuentasPorCobrarClientesParam = null;
            ParametroContable otraCondicionVentaParam = null;
            ParametroContable lineaParam = null;
            ParametroContable cuentaPorCobrarTarjetaParam = null;
            ParametroContable gastoComisionParam = null;
            DataTable dtbInventarios = new DataTable();
            dtbInventarios.Columns.Add("IdLinea", typeof(int));
            dtbInventarios.Columns.Add("Total", typeof(decimal));
            dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
            CuentaPorCobrar cuentaPorCobrar = null;
            Asiento asiento = null;
            MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (factura.IdOrdenServicio > 0)
                    {
                        OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(factura.IdOrdenServicio);
                        ordenServicio.Aplicado = true;
                        dbContext.NotificarModificacion(ordenServicio);
                    }
                    if (factura.IdProforma > 0)
                    {
                        Proforma proforma = dbContext.ProformaRepository.Find(factura.IdProforma);
                        proforma.Aplicado = true;
                        dbContext.NotificarModificacion(proforma);
                    }
                    if (empresa.Contabiliza)
                    {
                        ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IngresosPorVentas).FirstOrDefault();
                        costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CostosDeVentas).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentasPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.OtraCondicionVenta).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                        if (ingresosVentasParam == null || costoVentasParam == null || ivaPorPagarParam == null || efectivoParam == null || cuentasPorCobrarClientesParam == null || otraCondicionVentaParam == null || cuentaPorCobrarTarjetaParam == null || gastoComisionParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    factura.IdCxC = 0;
                    factura.IdAsiento = 0;
                    factura.IdMovBanco = 0;
                    dbContext.FacturaRepository.Add(factura);
                    if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        if (cliente == null)
                            throw new Exception("El cliente asignado a la factura no existe.");
                        ICuentaPorCobrarService servicioCuentaPorCobrar = new CuentaPorCobrarService();
                        cuentaPorCobrar = new CuentaPorCobrar
                        {
                            IdEmpresa = factura.IdEmpresa,
                            IdUsuario = factura.IdUsuario,
                            IdPropietario = factura.IdCliente,
                            Descripcion = "Cuenta por cobrar de Factura nro. ",
                            Referencia = factura.NoDocumento,
                            NroDocOrig = factura.IdFactura,
                            Fecha = factura.Fecha,
                            Tipo = StaticTipoCuentaPorCobrar.Clientes,
                            Total = factura.Total,
                            Saldo = factura.Total,
                            Nulo = false
                        };
                        dbContext.CuentaPorCobrarRepository.Add(cuentaPorCobrar);
                    }
                    decTotalImpuesto += factura.Impuesto;
                    foreach (DetalleFactura detalle in factura.DetalleFactura)
                    {
                        decSubTotalFactura += detalle.Cantidad * detalle.PrecioVenta;
                    }
                    foreach (var detalleFactura in factura.DetalleFactura)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleFactura.IdProducto);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Registro de facturación.",
                                Referencia = factura.NoDocumento,
                                Cantidad = detalleFactura.Cantidad,
                                PrecioCosto = detalleFactura.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad -= detalleFactura.Cantidad;
                            dbContext.NotificarModificacion(producto);
                        }
                        if (empresa.Contabiliza)
                        {
                            if (producto.Tipo == StaticTipoProducto.Producto)
                            {
                                decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                                decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                if (!producto.Excento)
                                    decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (factura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                                decTotalIngresosMercancia += decTotalPorLinea;
                                decTotalCostoVentas += producto.PrecioCosto * detalleFactura.Cantidad;
                                int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                                if (intExiste >= 0)
                                    dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + (producto.PrecioCosto * detalleFactura.Cantidad);
                                else
                                {
                                    DataRow data = dtbInventarios.NewRow();
                                    data["IdLinea"] = producto.Linea.IdLinea;
                                    data["Total"] = producto.PrecioCosto * detalleFactura.Cantidad;
                                    dtbInventarios.Rows.Add(data);
                                }
                            }
                            else
                            {
                                decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                                decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                if (!producto.Excento)
                                    decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (factura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                                decTotalIngresosServicios += decTotalPorLinea;
                            }
                        }
                    }
                    foreach (var desglosePago in factura.DesglosePagoFactura)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                        {
                            movimientoBanco = new MovimientoBanco();
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new Exception("La cuenta bancaria asignada al movimiento no existe.");
                            movimientoBanco.IdCuenta = cuentaBanco.IdCuenta;
                            movimientoBanco.IdUsuario = factura.IdUsuario;
                            movimientoBanco.Fecha = factura.Fecha;
                            if (desglosePago.IdFormaPago == StaticFormaPago.Cheque)
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.ChequeEntrante;
                                movimientoBanco.Descripcion = "Registro de cheque bancario para pago de factura. ";
                            }
                            else
                            {
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.TransferenciaDeposito;
                                movimientoBanco.Descripcion = "Registro de depósito bancario para pago de factura. ";
                            }
                            movimientoBanco.Numero = desglosePago.TipoTarjeta;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService();
                            servicioAuxiliarBancario.AgregarMovimientoBanco(dbContext, movimientoBanco);
                        }
                    }
                    if (empresa.Contabiliza)
                    {
                        decimal decTotalDiff = decTotalIngresosMercancia + decTotalImpuesto + decTotalIngresosServicios - factura.Total;
                        if (decTotalDiff != 0)
                        {
                            if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                            if (decTotalIngresosMercancia > 0)
                                decTotalIngresosMercancia -= decTotalDiff;
                            else
                            {
                                decTotalIngresosServicios -= decTotalDiff;
                            }
                        }
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = factura.IdEmpresa,
                            Fecha = factura.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de venta de mercancía de Factura nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        if (decTotalIngresosMercancia > 0 || decTotalIngresosServicios > 0)
                        {
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            detalleAsiento.IdCuenta = ingresosVentasParam.IdCuenta;
                            detalleAsiento.Credito = decTotalIngresosMercancia + decTotalIngresosServicios;
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        if (decTotalImpuesto > 0)
                        {
                            detalleAsiento = new DetalleAsiento();
                            intLineaDetalleAsiento += 1;
                            detalleAsiento.Linea = intLineaDetalleAsiento;
                            detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                            detalleAsiento.Credito = decTotalImpuesto;
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = cuentasPorCobrarClientesParam.IdCuenta,
                                Debito = factura.Total,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentasPorCobrarClientesParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        else if (factura.IdCondicionVenta == StaticCondicionVenta.Contado)
                        {
                            foreach (var desglosePago in factura.DesglosePagoFactura)
                            {
                                if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                                {
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                                    if (bancoParam == null)
                                        throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento = new DetalleAsiento
                                    {
                                        Linea = intLineaDetalleAsiento,
                                        IdCuenta = bancoParam.IdCuenta,
                                        Debito = desglosePago.MontoLocal,
                                        SaldoAnterior = dbContext.CatalogoContableRepository.Find(bancoParam.IdCuenta).SaldoActual
                                    };
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                }
                                else if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                                {
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento = new DetalleAsiento
                                    {
                                        Linea = intLineaDetalleAsiento,
                                        IdCuenta = efectivoParam.IdCuenta,
                                        Debito = desglosePago.MontoLocal,
                                        SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                                    };
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                }
                                else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                                {
                                    BancoAdquiriente bancoAdquiriente = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                                    decimal decTotalGastoComisionTarjeta = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeComision / 100), 2, MidpointRounding.AwayFromZero);
                                    decimal decTotalImpuestoRetenido = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeRetencion / 100), 2, MidpointRounding.AwayFromZero);
                                    decimal decTotalIngresosTarjeta = Math.Round(desglosePago.MontoLocal - decTotalGastoComisionTarjeta - decTotalImpuestoRetenido, 2, MidpointRounding.AwayFromZero);
                                    intLineaDetalleAsiento += 1;
                                    detalleAsiento = new DetalleAsiento
                                    {
                                        Linea = intLineaDetalleAsiento,
                                        IdCuenta = cuentaPorCobrarTarjetaParam.IdCuenta,
                                        Debito = decTotalIngresosTarjeta,
                                        SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentaPorCobrarTarjetaParam.IdCuenta).SaldoActual
                                    };
                                    asiento.DetalleAsiento.Add(detalleAsiento);
                                    asiento.TotalDebito += detalleAsiento.Debito;
                                    if (decTotalImpuestoRetenido > 0)
                                    {
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = ivaPorPagarParam.IdCuenta,
                                            Debito = decTotalImpuestoRetenido,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(ivaPorPagarParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalDebito += detalleAsiento.Debito;
                                    }
                                    if (decTotalGastoComisionTarjeta > 0)
                                    {
                                        intLineaDetalleAsiento += 1;
                                        detalleAsiento = new DetalleAsiento
                                        {
                                            Linea = intLineaDetalleAsiento,
                                            IdCuenta = gastoComisionParam.IdCuenta,
                                            Debito = decTotalGastoComisionTarjeta,
                                            SaldoAnterior = dbContext.CatalogoContableRepository.Find(gastoComisionParam.IdCuenta).SaldoActual
                                        };
                                        asiento.DetalleAsiento.Add(detalleAsiento);
                                        asiento.TotalDebito += detalleAsiento.Debito;
                                    }
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
                                Debito = factura.Total,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(otraCondicionVentaParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        if (decTotalCostoVentas > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = costoVentasParam.IdCuenta,
                                Debito = decTotalCostoVentas,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(costoVentasParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                            foreach (DataRow data in dtbInventarios.Rows)
                            {
                                int intIdLinea = (int)data["IdLinea"];
                                lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
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
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    if (empresa.FacturaElectronica)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        factura.IdDocElectronico = ComprobanteElectronicoService.GeneraFacturaElectronica(factura, factura.Empresa, cliente, dbContext, intSucursal, intTerminal);
                    }
                    dbContext.Commit();
                    if (cuentaPorCobrar != null)
                    {
                        factura.IdCxC = cuentaPorCobrar.IdCxC;
                        dbContext.NotificarModificacion(factura);
                        cuentaPorCobrar.Descripcion += factura.IdFactura;
                        dbContext.NotificarModificacion(cuentaPorCobrar);
                    }
                    if (asiento != null)
                    {
                        factura.IdAsiento = asiento.IdAsiento;
                        dbContext.NotificarModificacion(factura);
                        asiento.Detalle += factura.IdFactura;
                        dbContext.NotificarModificacion(asiento);
                    }
                    if (movimientoBanco != null)
                    {
                        factura.IdMovBanco = movimientoBanco.IdMov;
                        dbContext.NotificarModificacion(factura);
                        movimientoBanco.Descripcion += factura.IdFactura;
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
                    log.Error("Error al agregar el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error guardando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
            return factura;
        }

        public void ActualizarFactura(Factura factura)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa); ;
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(factura);
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
                    log.Error("Error al actualizar el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularFactura(int intIdFactura, int intIdUsuario, int intSucursal, int intTerminal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    if (factura == null)
                        throw new Exception("La factura por anular no existe.");
                    if (factura.Nulo == true)
                        throw new BusinessException("La factura ya ha sido anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa); ;
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (factura.Procesado)
                        throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    factura.Nulo = true;
                    factura.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(factura);
                    foreach (var detalleFactura in factura.DetalleFactura)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleFactura.IdProducto);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Entrada,
                                Origen = "Anulación registro de facturación.",
                                Referencia = factura.NoDocumento,
                                Cantidad = detalleFactura.Cantidad,
                                PrecioCosto = detalleFactura.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad += detalleFactura.Cantidad;
                            dbContext.NotificarModificacion(producto);
                        }
                    }
                    if (factura.IdCxC > 0)
                    {
                        CuentaPorCobrar cuentaPorCobrar = dbContext.CuentaPorCobrarRepository.Find(factura.IdCxC);
                        if (cuentaPorCobrar == null)
                            throw new Exception("La cuenta por cobrar correspondiente a la factura no existe.");
                        if (cuentaPorCobrar.Total > cuentaPorCobrar.Saldo)
                            throw new BusinessException("La cuenta por cobrar generada por este registro de facturación ya posee movimientos de abono. No puede ser reversada.");
                        cuentaPorCobrar.Nulo = true;
                        cuentaPorCobrar.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(cuentaPorCobrar);
                    }
                    if (factura.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService();
                        servicioAuxiliarBancario.AnularMovimientoBanco(dbContext, factura.IdMovBanco, intIdUsuario);
                    }
                    if (factura.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.ReversarAsientoContable(dbContext, factura.IdAsiento);
                    }
                    if (empresa.FacturaElectronica)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        ComprobanteElectronicoService.GenerarNotaDeCreditoElectronica(factura, factura.Empresa, cliente, dbContext, intSucursal, intTerminal);
                        dbContext.NotificarModificacion(factura);
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
                    log.Error("Error al anular el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public Factura ObtenerFactura(int intIdFactura)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FacturaRepository.Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto.TipoProducto").Include("DesglosePagoFactura.FormaPago").Include("DesglosePagoFactura.TipoMoneda").FirstOrDefault(x => x.IdFactura == intIdFactura);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaFacturas = dbContext.FacturaRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdFactura > 0)
                        listaFacturas = listaFacturas.Where(x => !x.Nulo && x.IdFactura == intIdFactura);
                    else if (!strNombre.Equals(string.Empty))
                        listaFacturas = listaFacturas.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Cliente.Nombre.Contains(strNombre));
                    return listaFacturas.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Factura> ObtenerListaFacturas(int intIdEmpresa, int numPagina, int cantRec, int intIdFactura = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaFacturas = dbContext.FacturaRepository.Include("Cliente").Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdFactura > 0)
                        listaFacturas = listaFacturas.Where(x => !x.Nulo && x.IdFactura == intIdFactura);
                    else if (!strNombre.Equals(string.Empty))
                        listaFacturas = listaFacturas.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Cliente.Nombre.Contains(strNombre));
                    return listaFacturas.OrderByDescending(x => x.IdFactura).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Factura> ObtenerListaFacturasPorCliente(int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FacturaRepository.Where(x => !x.Nulo && x.IdCliente == intIdCliente).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de facturación de un cliente: ", ex);
                    throw new Exception("Se produjo un error consultado el listado de facturas de un cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public Proforma AgregarProforma(Proforma proforma)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    dbContext.ProformaRepository.Add(proforma);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
            return proforma;
        }

        public void ActualizarProforma(Proforma proforma)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (proforma.Aplicado == true)
                        throw new BusinessException("La proforma no puede ser modificada porque ya fue facturada.");
                    Proforma proformaPersistente = dbContext.ProformaRepository.Include("DetalleProforma").FirstOrDefault(x => x.IdProforma == proforma.IdProforma);
                    dbContext.ApplyCurrentValues(proformaPersistente, proforma);
                    dbContext.DetalleProformaRepository.RemoveRange(proformaPersistente.DetalleProforma);
                    dbContext.DetalleProformaRepository.AddRange(proforma.DetalleProforma);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularProforma(int intIdProforma, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Proforma proforma = dbContext.ProformaRepository.Find(intIdProforma);
                    if (proforma == null)
                        throw new Exception("La proforma por anular no existe.");
                    if (proforma.Nulo == true)
                        throw new BusinessException("La proforma ya ha sido anulada.");
                    if (proforma.Aplicado == true)
                        throw new BusinessException("La proforma no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    proforma.Nulo = true;
                    proforma.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(proforma);
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
                    log.Error("Error al anular el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public Proforma ObtenerProforma(int intIdProforma)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProformaRepository.Include("Cliente").Include("Vendedor").Include("DetalleProforma.Producto").FirstOrDefault(x => x.IdProforma == intIdProforma);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int intIdProforma = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProformas = dbContext.ProformaRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdProforma > 0)
                        listaProformas = listaProformas.Where(x => !x.Nulo && x.IdProforma == intIdProforma);
                    else
                    {
                        if (!bolIncluyeTodo)
                            listaProformas = listaProformas.Where(x => !x.Aplicado);
                        if (!strNombre.Equals(string.Empty))
                            listaProformas = listaProformas.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Cliente.Nombre.Contains(strNombre));
                    }
                    return listaProformas.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un consultando el total del listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Proforma> ObtenerListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdProforma = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProformas = dbContext.ProformaRepository.Include("Cliente").Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdProforma > 0)
                        listaProformas = listaProformas.Where(x => !x.Nulo && x.IdProforma == intIdProforma);
                    else
                    {
                        if (!bolIncluyeTodo)
                            listaProformas = listaProformas.Where(x => !x.Aplicado);
                        if (!strNombre.Equals(string.Empty))
                            listaProformas = listaProformas.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Cliente.Nombre.Contains(strNombre));
                    }
                    return listaProformas.OrderByDescending(x => x.IdProforma).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un consultando el listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public OrdenServicio AgregarOrdenServicio(OrdenServicio ordenServicio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    dbContext.OrdenServicioRepository.Add(ordenServicio);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
            return ordenServicio;
        }

        public void ActualizarOrdenServicio(OrdenServicio ordenServicio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (ordenServicio.Aplicado == true)
                        throw new BusinessException("La orden de servicio no puede ser modificada porque ya fue facturada.");
                    OrdenServicio ordenServicioPersistente = dbContext.OrdenServicioRepository.Include("DetalleOrdenServicio").FirstOrDefault(x => x.IdOrden == ordenServicio.IdOrden);
                    dbContext.ApplyCurrentValues(ordenServicioPersistente, ordenServicio);
                    dbContext.DetalleOrdenServicioRepository.RemoveRange(ordenServicioPersistente.DetalleOrdenServicio);
                    dbContext.DetalleOrdenServicioRepository.AddRange(ordenServicio.DetalleOrdenServicio);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el registro de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(intIdOrdenServicio);
                    if (ordenServicio == null)
                        throw new Exception("La orden de servicio por anular no existe.");
                    if (ordenServicio.Nulo == true)
                        throw new BusinessException("La orden de servicio ya ha sido anulada.");
                    if (ordenServicio.Aplicado == true)
                        throw new BusinessException("La orden de servicio no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    ordenServicio.Nulo = true;
                    ordenServicio.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(ordenServicio);
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
                    log.Error("Error al anular el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public OrdenServicio ObtenerOrdenServicio(int intIdOrdenServicio)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.OrdenServicioRepository.Include("DetalleOrdenServicio.Producto").Include("Cliente").Include("Vendedor").FirstOrDefault(x => x.IdOrden == intIdOrdenServicio);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenServicio = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesServicio = dbContext.OrdenServicioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (!bolIncluyeTodo)
                        listaOrdenesServicio = listaOrdenesServicio.Where(x => !x.Aplicado);
                    if (intIdOrdenServicio > 0)
                        listaOrdenesServicio = listaOrdenesServicio.Where(x => x.IdOrden == intIdOrdenServicio);
                    if (!strNombre.Equals(string.Empty))
                        listaOrdenesServicio = listaOrdenesServicio.Where(x => x.Cliente.Nombre.Contains(strNombre));
                    return listaOrdenesServicio.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un consultando el total del listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<OrdenServicio> ObtenerListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenServicio = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesServicio = dbContext.OrdenServicioRepository.Include("Cliente").Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (!bolIncluyeTodo)
                        listaOrdenesServicio = listaOrdenesServicio.Where(x => !x.Aplicado);
                    else
                    {
                        if (intIdOrdenServicio > 0)
                            listaOrdenesServicio = listaOrdenesServicio.Where(x => x.IdOrden == intIdOrdenServicio);
                        if (!strNombre.Equals(string.Empty))
                            listaOrdenesServicio = listaOrdenesServicio.Where(x => x.Cliente.Nombre.Contains(strNombre));
                    }
                    return listaOrdenesServicio.OrderByDescending(x => x.IdOrden).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un consultando el listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public DevolucionCliente AgregarDevolucionCliente(DevolucionCliente devolucion)
        {
            decimal decTotalImpuesto = 0;
            decimal decTotalCostos = 0;
            decimal decTotalIngresos = 0;
            decimal decSubTotalFactura = 0;
            ParametroContable ingresosVentasParam = null;
            ParametroContable costoVentasParam = null;
            ParametroContable ivaPorPagarParam = null;
            ParametroContable efectivoParam = null;
            ParametroContable lineaParam = null;
            ParametroContable cuentasPorCobrarParam = null;
            DataTable dtbInventarios = new DataTable();
            dtbInventarios.Columns.Add("IdLinea", typeof(int));
            dtbInventarios.Columns.Add("Total", typeof(decimal));
            dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
            Asiento asiento = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura").FirstOrDefault(x => x.IdFactura == devolucion.IdFactura);
                    if (factura == null)
                        throw new Exception("La factura asignada a la devolución no existe");
                    if (empresa.Contabiliza)
                    {
                        ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IngresosPorVentas).FirstOrDefault();
                        costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CostosDeVentas).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                        cuentasPorCobrarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                        if (ingresosVentasParam == null || costoVentasParam == null || ivaPorPagarParam == null || efectivoParam == null || cuentasPorCobrarParam == null)
                            throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    }
                    devolucion.IdAsiento = 0;
                    decTotalImpuesto = devolucion.Impuesto;
                    foreach (DetalleFactura detalle in factura.DetalleFactura)
                    {
                        decSubTotalFactura += detalle.Cantidad * detalle.PrecioVenta;
                    }
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionCliente)
                    {
                        if (detalleDevolucion.CantDevolucion > 0)
                        {
                            Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                            if (producto == null)
                                throw new Exception("El producto asignado al detalle de la devolución no existe");
                            if (producto.Tipo == StaticTipoProducto.Servicio)
                                throw new BusinessException("El tipo de producto por devolver no puede ser un servicio. Por favor verificar.");
                            else if (producto.Tipo == StaticTipoProducto.Producto)
                            {
                                producto.Cantidad += detalleDevolucion.CantDevolucion;
                                dbContext.NotificarModificacion(producto);
                                MovimientoProducto movimientoProducto = new MovimientoProducto
                                {
                                    IdProducto = producto.IdProducto,
                                    Fecha = DateTime.Now,
                                    Tipo = StaticTipoMovimientoProducto.Entrada,
                                    Origen = "Registro de devolución de mercancía del cliente.",
                                    Referencia = devolucion.IdFactura.ToString(),
                                    Cantidad = detalleDevolucion.CantDevolucion,
                                    PrecioCosto = detalleDevolucion.PrecioCosto
                                };
                                producto.MovimientoProducto.Add(movimientoProducto);
                                if (empresa.Contabiliza)
                                {
                                    decimal decTotalCostoPorLinea = Math.Round(detalleDevolucion.PrecioCosto * detalleDevolucion.CantDevolucion, 2, MidpointRounding.AwayFromZero);
                                    decTotalCostos += decTotalCostoPorLinea;
                                    decimal decTotalPorLinea = detalleDevolucion.PrecioVenta * detalleDevolucion.CantDevolucion;
                                    decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                                    if (!producto.Excento)
                                        decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (devolucion.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                                    decTotalIngresos += decTotalPorLinea;
                                    int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                                    if (intExiste >= 0)
                                        dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + decTotalCostoPorLinea;
                                    else
                                    {
                                        DataRow data = dtbInventarios.NewRow();
                                        data["IdLinea"] = producto.Linea.IdLinea;
                                        data["Total"] = decTotalCostoPorLinea;
                                        dtbInventarios.Rows.Add(data);
                                    }
                                }
                            }
                        }
                    }
                    MovimientoCuentaPorCobrar movimientoCuentaPorCobrar = null;
                    decimal decMontoMovimientoCxC = 0;
                    decimal decMontoDevolucionEfectivo = devolucion.Total;
                    if (factura.IdCxC > 0)
                    {
                        CuentaPorCobrar cuentaPorCobrar = dbContext.CuentaPorCobrarRepository.Find(factura.IdCxC);
                        if (cuentaPorCobrar.Saldo > 0)
                        {
                            decMontoMovimientoCxC = cuentaPorCobrar.Saldo > decMontoDevolucionEfectivo ? decMontoDevolucionEfectivo : cuentaPorCobrar.Saldo;
                            decMontoDevolucionEfectivo -= decMontoMovimientoCxC;
                            movimientoCuentaPorCobrar = new MovimientoCuentaPorCobrar
                            {
                                IdAsiento = 0,
                                IdEmpresa = devolucion.IdEmpresa,
                                IdUsuario = devolucion.IdUsuario,
                                IdPropietario = devolucion.IdCliente,
                                Tipo = StaticTipoAbono.NotaCredito,
                                Descripcion = "Nota de crédito por devolución de mercancía nro. ",
                                Monto = devolucion.Total,
                                Fecha = devolucion.Fecha
                            };
                            DesgloseMovimientoCuentaPorCobrar desgloseMovimiento = new DesgloseMovimientoCuentaPorCobrar
                            {
                                IdCxC = factura.IdCxC,
                                Monto = decMontoMovimientoCxC
                            };
                            movimientoCuentaPorCobrar.DesgloseMovimientoCuentaPorCobrar.Add(desgloseMovimiento);
                            DesglosePagoMovimientoCuentaPorCobrar desglosePagoMovimiento = new DesglosePagoMovimientoCuentaPorCobrar
                            {
                                IdFormaPago = StaticFormaPago.Efectivo,
                                IdCuentaBanco = 1,
                                TipoTarjeta = null,
                                NroMovimiento = null,
                                IdTipoMoneda = StaticValoresPorDefecto.MonedaDelSistema,
                                MontoLocal = decMontoMovimientoCxC,
                                MontoForaneo = decMontoMovimientoCxC
                            };
                            movimientoCuentaPorCobrar.DesglosePagoMovimientoCuentaPorCobrar.Add(desglosePagoMovimiento);
                            dbContext.MovimientoCuentaPorCobrarRepository.Add(movimientoCuentaPorCobrar);
                            cuentaPorCobrar.Saldo -= decMontoMovimientoCxC;
                            dbContext.NotificarModificacion(cuentaPorCobrar);
                        }
                    }
                    DesglosePagoDevolucionCliente desglosePagoDevolucion = new DesglosePagoDevolucionCliente
                    {
                        IdFormaPago = StaticFormaPago.Efectivo,
                        IdCuentaBanco = 1,
                        Beneficiario = null,
                        NroMovimiento = null,
                        IdTipoMoneda = StaticValoresPorDefecto.MonedaDelSistema,
                        MontoLocal = devolucion.Total,
                        MontoForaneo = devolucion.Total,
                    };
                    devolucion.DesglosePagoDevolucionCliente.Add(desglosePagoDevolucion);
                    dbContext.DevolucionClienteRepository.Add(devolucion);
                    if (empresa.Contabiliza)
                    {
                        decimal decTotalDiff = decTotalIngresos + decTotalImpuesto - devolucion.Total;
                        if (decTotalDiff != 0)
                        {
                            if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                            dtbInventarios.Rows[0]["Total"] = (decimal)dtbInventarios.Rows[0]["Total"] - decTotalDiff;
                            decTotalIngresos -= decTotalDiff;
                        }
                        int intLineaDetalleAsiento = 0;
                        asiento = new Asiento
                        {
                            IdEmpresa = devolucion.IdEmpresa,
                            Fecha = devolucion.Fecha,
                            TotalCredito = 0,
                            TotalDebito = 0,
                            Detalle = "Registro de devolución de mercancía del cliente nro. "
                        };
                        DetalleAsiento detalleAsiento = null;
                        if (decTotalIngresos > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = ingresosVentasParam.IdCuenta,
                                Debito = decTotalIngresos,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(ingresosVentasParam.IdCuenta).SaldoActual
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
                                Debito = decTotalImpuesto,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(ivaPorPagarParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalDebito += detalleAsiento.Debito;
                        }
                        if (decMontoDevolucionEfectivo > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = efectivoParam.IdCuenta,
                                Credito = decMontoDevolucionEfectivo,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(efectivoParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        if (decMontoMovimientoCxC > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = cuentasPorCobrarParam.IdCuenta,
                                Credito = decMontoDevolucionEfectivo,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(cuentasPorCobrarParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        foreach (DataRow data in dtbInventarios.Rows)
                        {
                            int intIdLinea = (int)data["IdLinea"];
                            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
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
                        if (decTotalCostos > 0)
                        {
                            intLineaDetalleAsiento += 1;
                            detalleAsiento = new DetalleAsiento
                            {
                                Linea = intLineaDetalleAsiento,
                                IdCuenta = costoVentasParam.IdCuenta,
                                Credito = decTotalCostos,
                                SaldoAnterior = dbContext.CatalogoContableRepository.Find(costoVentasParam.IdCuenta).SaldoActual
                            };
                            asiento.DetalleAsiento.Add(detalleAsiento);
                            asiento.TotalCredito += detalleAsiento.Credito;
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    dbContext.Commit();
                    if (movimientoCuentaPorCobrar != null)
                    {
                        devolucion.IdMovimientoCxC = movimientoCuentaPorCobrar.IdMovCxC;
                        dbContext.NotificarModificacion(devolucion);
                        movimientoCuentaPorCobrar.Descripcion += devolucion.IdDevolucion;
                        dbContext.NotificarModificacion(movimientoCuentaPorCobrar);
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
            return devolucion;
        }

        public void AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.Include("DetalleDevolucionCliente").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                    if (devolucion == null)
                        throw new Exception("La devolución por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (devolucion.Procesado)
                        throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    devolucion.Nulo = true;
                    devolucion.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(devolucion);
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionCliente)
                    {
                        Producto producto = dbContext.ProductoRepository.Find(detalleDevolucion.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe");
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            producto.Cantidad -= detalleDevolucion.CantDevolucion;
                            dbContext.NotificarModificacion(producto);
                            MovimientoProducto movimientoProducto = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Anulación de registro de devolución de mercancía del cliente.",
                                Referencia = devolucion.IdFactura.ToString(),
                                Cantidad = detalleDevolucion.CantDevolucion,
                                PrecioCosto = detalleDevolucion.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimientoProducto);
                        }
                    }
                    if (devolucion.IdMovimientoCxC > 0)
                    {
                        MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.Include("DesgloseMovimientoCuentaPorCobrar").FirstOrDefault(x => x.IdMovCxC == devolucion.IdMovimientoCxC);
                        if (movimiento == null)
                            throw new Exception("El movimiento de la cuenta por cobrar correspondiente a la devolución no existe");
                        movimiento.Nulo = true;
                        movimiento.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(movimiento);
                        foreach (DesgloseMovimientoCuentaPorCobrar desglose in movimiento.DesgloseMovimientoCuentaPorCobrar)
                        {
                            CuentaPorCobrar cuentaPorCobrar = dbContext.CuentaPorCobrarRepository.Find(desglose.IdCxC);
                            cuentaPorCobrar.Saldo += desglose.Monto;
                            dbContext.NotificarModificacion(cuentaPorCobrar);
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

        public DevolucionCliente ObtenerDevolucionCliente(int intIdDevolucion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.DevolucionClienteRepository.Include("DetalleDevolucionCliente.Producto").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error consultado la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdDevolucion = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionClienteRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Cliente.Nombre.Contains(strNombre));
                    return listaDevoluciones.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<DevolucionCliente> ObtenerListaDevolucionesPorCliente(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion = 0, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionClienteRepository.Include("Factura").Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Cliente.Nombre.Contains(strNombre));
                    return listaDevoluciones.OrderByDescending(x => x.IdDevolucion).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GeneraMensajeReceptor(string datos, int intIdEmpresa, int intSucursal, int intTerminal, int intMensaje)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    ComprobanteElectronicoService.GeneraMensajeReceptor(datos, empresa, dbContext, intSucursal, intTerminal, intMensaje);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar factura electrónica: ", ex);
                    throw new Exception("Se produjo un error al procesar la factura electrónica ingresada. Por favor consulte con su proveedor.");
                }
            }
        }

        public async Task<IList<DocumentoElectronico>> ObtenerListaDocumentosElectronicosPendientes(int intIdEmpresa)
        {
            IList<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    var listaPendientes = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EstadoEnvio == "pendiente" || x.EstadoEnvio == "registrado");
                    foreach (DocumentoElectronico doc in listaPendientes)
                    {
                        try
                        {
                            DatosDocumentoElectronicoDTO datos = await ComprobanteElectronicoService.ConsultarDocumentoElectronico(empresa, doc.ClaveNumerica, doc.Consecutivo);
                            if (datos.EstadoEnvio != "procesando")
                            {
                                if (doc.EstadoEnvio != datos.EstadoEnvio)
                                {
                                    doc.EstadoEnvio = datos.EstadoEnvio;
                                    dbContext.NotificarModificacion(doc);
                                }
                                if (datos.EstadoEnvio != "aceptado" && datos.EstadoEnvio != "rechazado")
                                {
                                    listado.Add(doc);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Service Unavailable")
                                throw ex;
                            listado.Add(doc);
                        }
                        
                    }
                    dbContext.Commit();
                    return listado;
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar factura electrónica: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor consulte con su proveedor.");
                    else
                        throw new Exception("Se produjo un error al procesar la factura electrónica ingresada. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarDocumentosElectronicosPendientes(int intIdEmpresa, IList<DocumentoElectronico> listaPendientes)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    foreach (DocumentoElectronico doc in listaPendientes)
                    {
                        DatosDocumentoElectronicoDTO datos = new DatosDocumentoElectronicoDTO();
                        datos.IdEmpresa = empresa.IdEmpresa;
                        datos.IdTipoDocumento = doc.IdTipoDocumento;
                        datos.ClaveNumerica = doc.ClaveNumerica;
                        datos.FechaEmision = doc.Fecha;
                        datos.TipoIdentificacionEmisor = doc.TipoIdentificacionEmisor.ToString("D2");
                        datos.IdentificacionEmisor = doc.IdentificacionEmisor;
                        datos.TipoIdentificacionReceptor = doc.TipoIdentificacionReceptor.ToString("D2");
                        datos.IdentificacionReceptor = doc.IdentificacionReceptor;
                        datos.EsMensajeReceptor = new int[] { 5, 6, 7 }.Contains(doc.IdTipoDocumento) ? "S" : "N";
                        datos.Consecutivo = doc.Consecutivo;
                        datos.CorreoNotificacion = doc.CorreoNotificacion;
                        datos.EstadoEnvio = doc.EstadoEnvio;
                        datos.DatosDocumento = Convert.ToBase64String(doc.DatosDocumento);
                        ComprobanteElectronicoService.ProcesarDocumentoElectronico(empresa, datos);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar factura electrónica: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor consulte con su proveedor.");
                    else
                        throw new Exception("Se produjo un error al procesar la factura electrónica ingresada. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}
