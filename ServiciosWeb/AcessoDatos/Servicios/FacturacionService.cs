using System;
using System.Linq;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using LeandroSoftware.Core.CommonTypes;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using Unity;
using System.Threading.Tasks;
using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.Core.Servicios;
using Newtonsoft.Json.Linq;
using LeandroSoftware.Core;
using System.Xml.Serialization;
using System.IO;
using System.Web.Hosting;
using System.Drawing;
using System.Globalization;

namespace LeandroSoftware.AccesoDatos.Servicios
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
        Factura AgregarFactura(Factura factura, int intSucursal, int intTerminal, DatosConfiguracion datos);
        void ActualizarFactura(Factura factura);
        void AnularFactura(int intIdFactura, int intIdUsuario, int intSucursal, int intTerminal, DatosConfiguracion datos);
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
        IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosPendientes(int intIdEmpresa);
        IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosEnviados(int intIdEmpresa);
        void EnviarDocumentoElectronicoPendiente(int intIdDocumento, DatosConfiguracion datos);
        DocumentoElectronico ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento, DatosConfiguracion datos);
        int ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa);
        IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosProcesados(int intIdEmpresa, int numPagina, int cantRec);
        DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento);
        void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores);
        void EnviarNotificacionDocumentoElectronico(int intIdDocumento, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores);
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (cliente == null) throw new Exception("El cliente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    var cliente = dbContext.ClienteRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdCliente == intIdCliente).FirstOrDefault();
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    Cliente cliente = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Identificacion == strIdentificacion).FirstOrDefault();
                    if (cliente != null)
                        return cliente;
                    else
                    {
                        Padron persona = dbContext.PadronRepository.Where(x => x.Identificacion == strIdentificacion).FirstOrDefault();
                        if (persona == null)
                            return null;
                        else
                        {
                            cliente = new Cliente
                            {
                                Identificacion = persona.Identificacion,
                                IdProvincia = persona.IdProvincia,
                                IdCanton = persona.IdCanton,
                                IdDistrito = persona.IdDistrito,
                                Nombre = persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido
                            };
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

        public Factura AgregarFactura(Factura factura, int intSucursal, int intTerminal, DatosConfiguracion datos)
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    DocumentoElectronico documentoFE = null;
                    if (!empresa.RegimenSimplificado)
                    {
                        if (empresa.FechaVence < DateTime.Today) throw new BusinessException("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        string criteria = factura.Fecha.ToString("dd/MM/yyyy");
                        TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                        if (tipoDeCambio != null)
                        {
                            documentoFE = ComprobanteElectronicoService.GeneraFacturaElectronica(factura, factura.Empresa, cliente, dbContext, intSucursal, intTerminal, tipoDeCambio.ValorTipoCambio);
                            factura.IdDocElectronico = documentoFE.ClaveNumerica;
                        }
                        else
                        {
                            throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                        }
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
                    if (documentoFE != null)
                    {
                        Task.Run(() => ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa, documentoFE, localContainer, datos));
                    }
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public void AnularFactura(int intIdFactura, int intIdUsuario, int intSucursal, int intTerminal, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    if (factura.Nulo == true) throw new BusinessException("La factura ya ha sido anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (factura.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
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
                    DocumentoElectronico documentoNC = null;
                    if (!empresa.RegimenSimplificado)
                    {
                        if (empresa.FechaVence < DateTime.Today) throw new BusinessException("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == factura.IdDocElectronico).FirstOrDefault();
                        if (documento.EstadoEnvio == "aceptado")
                        {
                            string criteria = factura.Fecha.ToString("dd/MM/yyyy");
                            TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                            if (tipoDeCambio == null)
                            {
                                documentoNC = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronica(factura, factura.Empresa, cliente, dbContext, intSucursal, intTerminal, tipoDeCambio.ValorTipoCambio);
                                factura.IdDocElectronicoRev = documentoNC.ClaveNumerica;
                            }
                            else
                            {
                                throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                            }
                            dbContext.NotificarModificacion(factura);
                        }
                        else if(documento.EstadoEnvio != "rechazado")
                        {
                            throw new BusinessException("El documento electrónico no ha sido procesado por el Ministerio de Hacienda. La factura no puede ser reversada en este momento.");
                        }
                    }
                    dbContext.Commit();
                    if (documentoNC != null)
                    {
                        Task.Run(() => ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa, documentoNC, localContainer, datos));
                    }
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
                    return dbContext.FacturaRepository.Include("Cliente.Barrio.Distrito.Canton.Provincia").Include("Vendedor").Include("DetalleFactura.Producto.TipoProducto").Include("DesglosePagoFactura.FormaPago").Include("DesglosePagoFactura.TipoMoneda").FirstOrDefault(x => x.IdFactura == intIdFactura);
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (proforma.Aplicado == true) throw new BusinessException("La proforma no puede ser modificada porque ya fue facturada.");
                    List<DetalleProforma> listadoDetalleAnterior = dbContext.DetalleProformaRepository.Where(x => x.IdProforma == proforma.IdProforma).ToList();
                    foreach (DetalleProforma detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(proforma);
                    foreach (DetalleProforma detalle in proforma.DetalleProforma)
                        dbContext.DetalleProformaRepository.Add(detalle);
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
                    if (proforma == null) throw new Exception("La proforma por anular no existe.");
                    if (proforma.Nulo == true) throw new BusinessException("La proforma ya ha sido anulada.");
                    if (proforma.Aplicado == true) throw new BusinessException("La proforma no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (ordenServicio.Aplicado == true) throw new BusinessException("La orden de servicio no puede ser modificada porque ya fue facturada.");
                    List<DetalleOrdenServicio> listadoDetalleAnterior = dbContext.DetalleOrdenServicioRepository.Where(x => x.IdOrden == ordenServicio.IdOrden).ToList();
                    foreach (DetalleOrdenServicio detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(ordenServicio);
                    foreach (DetalleOrdenServicio detalle in ordenServicio.DetalleOrdenServicio)
                        dbContext.DetalleOrdenServicioRepository.Add(detalle);
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
                    if (ordenServicio == null) throw new Exception("La orden de servicio por anular no existe.");
                    if (ordenServicio.Nulo == true) throw new BusinessException("La orden de servicio ya ha sido anulada.");
                    if (ordenServicio.Aplicado == true) throw new BusinessException("La orden de servicio no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (devolucion == null) throw new Exception("La devolución por anular no existe");
                    Empresa empresa = dbContext.EmpresaRepository.Find(devolucion.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (devolucion.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
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
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    ComprobanteElectronicoService.GeneraMensajeReceptor(datos, empresa, dbContext, intSucursal, intTerminal, intMensaje);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar factura electrónica: ", ex);
                    throw ex;
                }
            }
        }

        public IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosPendientes(int intIdEmpresa)
        {
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listaPendientes = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EstadoEnvio == "registrado");
                    foreach (DocumentoElectronico doc in listaPendientes)
                    {
                        doc.DatosDocumento = null;
                        doc.Respuesta = null;
                        listado.Add(doc);
                    }
                    return listado;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de documentos electrónicos pendientes: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosEnviados(int intIdEmpresa)
        {
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listaEnviados = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EstadoEnvio == "enviado");
                    foreach (DocumentoElectronico doc in listaEnviados)
                    {
                        doc.DatosDocumento = null;
                        doc.Respuesta = null;
                        listado.Add(doc);
                    }
                    return listado;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de documentos electrónicos enviados: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos enviados. Por favor consulte con su proveedor.");
                }
            }
        }

        public async void EnviarDocumentoElectronicoPendiente(int intIdDocumento, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null) throw new BusinessException("El documento solicitado no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (documento.EstadoEnvio == "registrado")
                    {
                        documento.EstadoEnvio = "enviado";
                        dbContext.NotificarModificacion(documento);
                        dbContext.Commit();
                        try
                        {
                            await ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa, documento, localContainer, datos);
                        }
                        catch (Exception ex)
                        {
                            documento.EstadoEnvio = "registrado";
                            documento.ErrorEnvio = ex.Message;
                            dbContext.NotificarModificacion(documento);
                            dbContext.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar el documento electrónico pendiente: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor intente más tarde.");
                    else
                        throw new Exception("Se produjo un error al procesar el documento electrónico pendiente. Por favor consulte con su proveedor.");
                }
            }
        }

        public DocumentoElectronico ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                DocumentoElectronico respuesta = null;
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null) throw new BusinessException("El documento solicitado no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (documento.EstadoEnvio == "enviado")
                    {
                        respuesta = ComprobanteElectronicoService.ConsultarDocumentoElectronico(empresa, documento, localContainer, datos).Result;
                    }
                    return respuesta;
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar respuesta de Hacienda del documento electrónico: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor intente más tarde.");
                    else
                        throw new Exception("Se produjo un error al consultar respuesta de Hacienda del documento electrónico. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listaProcesados = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EstadoEnvio == "aceptado" || x.EstadoEnvio == "rechazado");
                    return listaProcesados.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de documentos electrónicos procesados: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de documentos electrónicos procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoElectronico> ObtenerListaDocumentosElectronicosProcesados(int intIdEmpresa, int numPagina, int cantRec)
        {
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listaProcesados = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.EstadoEnvio == "aceptado" || x.EstadoEnvio == "rechazado")
                        .OrderByDescending(x => x.IdDocumento)
                        .Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (DocumentoElectronico doc in listaProcesados)
                    {
                        doc.DatosDocumento = null;
                        doc.Respuesta = null;
                        listado.Add(doc);
                    }
                    return listado;
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar el listado de documentos electrónicos procesados: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor consulte con su proveedor.");
                    else
                        throw new Exception("Se produjo un error al consultar el listado de documentos electrónicos procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento)
        {
            List<DocumentoElectronico> listado = new List<DocumentoElectronico>();
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null) throw new BusinessException("El documento solicitado no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                    return documento;
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar documento electrónico con ID: " + intIdDocumento, ex);
                    throw new Exception("Se produjo un error al consultar el documento electrónico. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
        {
            string strClave = "";
            string strConsecutivo = "";
            if (mensaje.Clave.Length > 50)
            {
                strClave = mensaje.Clave.Substring(0, 50);
                strConsecutivo = mensaje.Clave.Substring(51);
            }
            else
                strClave = mensaje.Clave;
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    DocumentoElectronico documentoElectronico = null;
                    Empresa empresa = null;
                    if (strConsecutivo.Length > 0)
                        documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave & x.Consecutivo == strConsecutivo).FirstOrDefault();
                    else
                        documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave).FirstOrDefault();
                    if (documentoElectronico == null)
                    {
                        JArray emptyJArray = new JArray();
                        string strBody = "El documento con clave " + mensaje.Clave + " no se encuentra registrado en los registros del cliente.";
                        servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al recibir respuesta de Hacienda.", strBody, false, emptyJArray);
                    }
                    else
                    {
                        empresa = dbContext.EmpresaRepository.Where(x => x.IdEmpresa == documentoElectronico.IdEmpresa).FirstOrDefault();
                        string strEstado = mensaje.IndEstado;
                        documentoElectronico.EstadoEnvio = strEstado;
                        if (strEstado == "aceptado" || strEstado == "rechazado")
                        {
                            byte[] bytRespuestaXML = Convert.FromBase64String(mensaje.RespuestaXml);
                            documentoElectronico.Respuesta = bytRespuestaXML;
                            dbContext.NotificarModificacion(documentoElectronico);
                            dbContext.Commit();
                            GenerarNotificacion(documentoElectronico, empresa, dbContext, servicioEnvioCorreo, strCorreoNotificacionErrores);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el procesamiento de la respuesta de hacienda para el comprobante con clave: " + mensaje.Clave, ex.Message, false, emptyJArray);
            }
        }

        public void EnviarNotificacionDocumentoElectronico(int intIdDocumento, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null)
                    {
                        JArray emptyJArray = new JArray();
                        string strBody = "El documento con ID " + intIdDocumento + " no se encuentra registrado.";
                        servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al recibir respuesta de Hacienda.", strBody, false, emptyJArray);
                    }
                    else
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Where(x => x.IdEmpresa == documento.IdEmpresa).FirstOrDefault();
                        if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (!empresa.PermiteFacturar) throw new Exception("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (empresa.FechaVence < DateTime.Today) throw new Exception("El período del plan de facturación electrónica adquirido ya ha expirado. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (documento.EstadoEnvio == "aceptado" || documento.EstadoEnvio == "rechazado")
                        {
                            GenerarNotificacion(documento, empresa, dbContext, servicioEnvioCorreo, strCorreoNotificacionErrores);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el envio de la notificacion del documento electrónico con ID: " + intIdDocumento, ex.Message, false, emptyJArray);
            }
        }

        private void GenerarNotificacion(DocumentoElectronico documentoElectronico, Empresa empresa, IDbContext dbContext, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
        {
            try
            {
                string strBody;
                JArray jarrayObj = new JArray();
                if (documentoElectronico.EsMensajeReceptor == "N")
                {
                    if (documentoElectronico.EstadoEnvio == "aceptado" && documentoElectronico.CorreoNotificacion != "")
                    {
                        strBody = "Adjunto documento electrónico en formato PDF y XML con clave " + documentoElectronico.ClaveNumerica + " y la respuesta de aceptación del Ministerio de Hacienda.";
                        EstructuraPDF datos = new EstructuraPDF();
                        try
                        {
                            Image logoImage;
                            using (MemoryStream memStream = new MemoryStream(empresa.Logotipo))
                                logoImage = Image.FromStream(memStream);
                            datos.Logotipo = logoImage;
                        }
                        catch (Exception)
                        {
                            datos.Logotipo = null;
                        }
                        try
                        {
                            string apPath = HostingEnvironment.ApplicationPhysicalPath + "bin\\images\\Logo.png";
                            Image poweredByImage = Image.FromFile(apPath);
                            datos.PoweredByLogotipo = poweredByImage;
                        }
                        catch (Exception)
                        {
                            datos.PoweredByLogotipo = null;
                        }
                        if (documentoElectronico.IdTipoDocumento == 1)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(FacturaElectronica));
                            FacturaElectronica facturaElectronica;
                            using (MemoryStream memStream = new MemoryStream(documentoElectronico.DatosDocumento))
                                facturaElectronica = (FacturaElectronica)serializer.Deserialize(memStream);

                            datos.TituloDocumento = "FACTURA ELECTRONICA";
                            datos.NombreEmpresa = facturaElectronica.Emisor.NombreComercial ?? facturaElectronica.Emisor.Nombre;
                            datos.Consecutivo = facturaElectronica.NumeroConsecutivo;
                            datos.PlazoCredito = facturaElectronica.PlazoCredito ?? "";
                            datos.Clave = facturaElectronica.Clave;
                            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(facturaElectronica.CondicionVenta.ToString().Substring(5)));
                            datos.Fecha = facturaElectronica.FechaEmision.ToString("dd/MM/yyyy hh:mm:ss");
                            datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(facturaElectronica.MedioPago[0].ToString().Substring(5)));
                            datos.NombreEmisor = facturaElectronica.Emisor.Nombre;
                            datos.NombreComercialEmisor = facturaElectronica.Emisor.NombreComercial;
                            datos.IdentificacionEmisor = facturaElectronica.Emisor.Identificacion.Numero;
                            datos.CorreoElectronicoEmisor = facturaElectronica.Emisor.CorreoElectronico;
                            datos.TelefonoEmisor = facturaElectronica.Emisor.Telefono != null ? facturaElectronica.Emisor.Telefono.NumTelefono.ToString() : "";
                            datos.FaxEmisor = facturaElectronica.Emisor.Fax != null ? facturaElectronica.Emisor.Fax.NumTelefono.ToString() : "";
                            int intProvincia = int.Parse(facturaElectronica.Emisor.Ubicacion.Provincia);
                            int intCanton = int.Parse(facturaElectronica.Emisor.Ubicacion.Canton);
                            int intDistrito = int.Parse(facturaElectronica.Emisor.Ubicacion.Distrito);
                            int intBarrio = int.Parse(facturaElectronica.Emisor.Ubicacion.Barrio);
                            datos.ProvinciaEmisor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                            datos.DireccionEmisor = facturaElectronica.Emisor.Ubicacion.OtrasSenas;
                            if (facturaElectronica.Receptor != null)
                            {
                                datos.PoseeReceptor = true;
                                datos.NombreReceptor = facturaElectronica.Receptor.Nombre;
                                datos.NombreComercialReceptor = facturaElectronica.Receptor.NombreComercial ?? "";
                                datos.IdentificacionReceptor = facturaElectronica.Receptor.Identificacion.Numero;
                                datos.CorreoElectronicoReceptor = facturaElectronica.Receptor.CorreoElectronico;
                                datos.TelefonoReceptor = facturaElectronica.Receptor.Telefono != null ? facturaElectronica.Receptor.Telefono.NumTelefono.ToString() : "";
                                datos.FaxReceptor = facturaElectronica.Receptor.Fax != null ? facturaElectronica.Receptor.Fax.NumTelefono.ToString() : "";
                                intProvincia = int.Parse(facturaElectronica.Receptor.Ubicacion.Provincia);
                                intCanton = int.Parse(facturaElectronica.Receptor.Ubicacion.Canton);
                                intDistrito = int.Parse(facturaElectronica.Receptor.Ubicacion.Distrito);
                                intBarrio = int.Parse(facturaElectronica.Receptor.Ubicacion.Barrio);
                                datos.ProvinciaReceptor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                datos.CantonReceptor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                datos.DistritoReceptor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                datos.BarrioReceptor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                datos.DireccionReceptor = facturaElectronica.Receptor.Ubicacion.OtrasSenas;
                            }
                            foreach (FacturaElectronicaLineaDetalle linea in facturaElectronica.DetalleServicio)
                            {
                                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                                {
                                    NumeroLinea = linea.NumeroLinea,
                                    Codigo = linea.Codigo[0].Codigo,
                                    Detalle = linea.Detalle,
                                    PrecioUnitario = string.Format("{0:N5}", Convert.ToDouble(linea.PrecioUnitario, CultureInfo.InvariantCulture)),
                                    TotalLinea = string.Format("{0:N5}", Convert.ToDouble(linea.MontoTotalLinea, CultureInfo.InvariantCulture))
                                };
                                datos.DetalleServicio.Add(detalle);
                            }
                            datos.SubTotal = string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalVenta, CultureInfo.InvariantCulture));
                            datos.Descuento = facturaElectronica.ResumenFactura.TotalDescuentosSpecified ? string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalDescuentos, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.Impuesto = facturaElectronica.ResumenFactura.TotalImpuestoSpecified ? string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalImpuesto, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.TotalGeneral = string.Format("{0:N5}", Convert.ToDouble(facturaElectronica.ResumenFactura.TotalComprobante, CultureInfo.InvariantCulture));
                            datos.CodigoMoneda = facturaElectronica.ResumenFactura.CodigoMonedaSpecified ? facturaElectronica.ResumenFactura.CodigoMoneda.ToString() : "";
                            datos.TipoDeCambio = facturaElectronica.ResumenFactura.CodigoMonedaSpecified ? facturaElectronica.ResumenFactura.TipoCambio.ToString() : "";
                        }
                        else if (documentoElectronico.IdTipoDocumento == 3)
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(NotaCreditoElectronica));
                            NotaCreditoElectronica notaCreditoElectronica;
                            using (MemoryStream memStream = new MemoryStream(documentoElectronico.DatosDocumento))
                                notaCreditoElectronica = (NotaCreditoElectronica)serializer.Deserialize(memStream);
                            datos.TituloDocumento = "NOTA DE CREDITO ELECTRONICA";
                            datos.NombreEmpresa = notaCreditoElectronica.Emisor.NombreComercial ?? notaCreditoElectronica.Emisor.Nombre;
                            datos.Consecutivo = notaCreditoElectronica.NumeroConsecutivo;
                            datos.PlazoCredito = notaCreditoElectronica.PlazoCredito ?? "";
                            datos.Clave = notaCreditoElectronica.Clave;
                            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(notaCreditoElectronica.CondicionVenta.ToString().Substring(5)));
                            datos.Fecha = notaCreditoElectronica.FechaEmision.ToString("dd/MM/yyyy hh:mm:ss");
                            if (notaCreditoElectronica.MedioPago != null)
                                datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(notaCreditoElectronica.MedioPago[0].ToString().Substring(5)));
                            else
                                datos.MedioPago = "";
                            datos.NombreEmisor = notaCreditoElectronica.Emisor.Nombre;
                            datos.NombreComercialEmisor = notaCreditoElectronica.Emisor.NombreComercial;
                            datos.IdentificacionEmisor = notaCreditoElectronica.Emisor.Identificacion.Numero;
                            datos.CorreoElectronicoEmisor = notaCreditoElectronica.Emisor.CorreoElectronico;
                            datos.TelefonoEmisor = notaCreditoElectronica.Emisor.Telefono != null ? notaCreditoElectronica.Emisor.Telefono.NumTelefono.ToString() : "";
                            datos.FaxEmisor = notaCreditoElectronica.Emisor.Fax != null ? notaCreditoElectronica.Emisor.Fax.NumTelefono.ToString() : "";
                            int intProvincia = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Provincia);
                            int intCanton = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Canton);
                            int intDistrito = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Distrito);
                            int intBarrio = int.Parse(notaCreditoElectronica.Emisor.Ubicacion.Barrio);
                            datos.ProvinciaEmisor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                            datos.DireccionEmisor = notaCreditoElectronica.Emisor.Ubicacion.OtrasSenas;
                            if (notaCreditoElectronica.Receptor != null)
                            {
                                datos.PoseeReceptor = true;
                                datos.NombreReceptor = notaCreditoElectronica.Receptor.Nombre;
                                datos.NombreComercialReceptor = notaCreditoElectronica.Receptor.NombreComercial ?? "";
                                datos.IdentificacionReceptor = notaCreditoElectronica.Receptor.Identificacion.Numero;
                                datos.CorreoElectronicoReceptor = notaCreditoElectronica.Receptor.CorreoElectronico;
                                datos.TelefonoReceptor = notaCreditoElectronica.Receptor.Telefono != null ? notaCreditoElectronica.Receptor.Telefono.NumTelefono.ToString() : "";
                                datos.FaxReceptor = notaCreditoElectronica.Receptor.Fax != null ? notaCreditoElectronica.Receptor.Fax.NumTelefono.ToString() : "";
                                intProvincia = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Provincia);
                                intCanton = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Canton);
                                intDistrito = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Distrito);
                                intBarrio = int.Parse(notaCreditoElectronica.Receptor.Ubicacion.Barrio);
                                datos.ProvinciaReceptor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                datos.CantonReceptor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                datos.DistritoReceptor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                datos.BarrioReceptor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                datos.DireccionReceptor = notaCreditoElectronica.Receptor.Ubicacion.OtrasSenas;
                            }
                            foreach (NotaCreditoElectronicaLineaDetalle linea in notaCreditoElectronica.DetalleServicio)
                            {
                                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                                {
                                    NumeroLinea = linea.NumeroLinea,
                                    Codigo = linea.Codigo[0].Codigo,
                                    Detalle = linea.Detalle,
                                    PrecioUnitario = string.Format("{0:N5}", Convert.ToDouble(linea.PrecioUnitario, CultureInfo.InvariantCulture)),
                                    TotalLinea = string.Format("{0:N5}", Convert.ToDouble(linea.MontoTotalLinea, CultureInfo.InvariantCulture))
                                };
                                datos.DetalleServicio.Add(detalle);
                            }
                            datos.SubTotal = string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalVenta, CultureInfo.InvariantCulture));
                            datos.Descuento = notaCreditoElectronica.ResumenFactura.TotalDescuentosSpecified ? string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalDescuentos, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.Impuesto = notaCreditoElectronica.ResumenFactura.TotalImpuestoSpecified ? string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalImpuesto, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.TotalGeneral = string.Format("{0:N5}", Convert.ToDouble(notaCreditoElectronica.ResumenFactura.TotalComprobante, CultureInfo.InvariantCulture));
                            datos.CodigoMoneda = notaCreditoElectronica.ResumenFactura.CodigoMonedaSpecified ? notaCreditoElectronica.ResumenFactura.CodigoMoneda.ToString() : "";
                            datos.TipoDeCambio = notaCreditoElectronica.ResumenFactura.CodigoMonedaSpecified ? notaCreditoElectronica.ResumenFactura.TipoCambio.ToString() : "";
                        }
                        byte[] pdfAttactment = Utilitario.GenerarPDFFacturaElectronica(datos);
                        JObject jobDatosAdjuntos1 = new JObject();
                        jobDatosAdjuntos1["nombre"] = documentoElectronico.ClaveNumerica + ".pdf";
                        jobDatosAdjuntos1["contenido"] = Convert.ToBase64String(pdfAttactment);
                        jarrayObj.Add(jobDatosAdjuntos1);
                        JObject jobDatosAdjuntos2 = new JObject();
                        jobDatosAdjuntos2["nombre"] = documentoElectronico.ClaveNumerica + ".xml";
                        jobDatosAdjuntos2["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento);
                        jarrayObj.Add(jobDatosAdjuntos2);
                        JObject jobDatosAdjuntos3 = new JObject();
                        jobDatosAdjuntos3["nombre"] = "RespuestaHacienda.xml";
                        jobDatosAdjuntos3["contenido"] = Convert.ToBase64String(documentoElectronico.Respuesta);
                        jarrayObj.Add(jobDatosAdjuntos3);
                        servicioEnvioCorreo.SendEmail(new string[] { documentoElectronico.CorreoNotificacion }, new string[] { }, "Documento electrónico con clave " + documentoElectronico.ClaveNumerica, strBody, false, jarrayObj);
                    }
                }
                else
                {
                    if ((documentoElectronico.EstadoEnvio == "aceptado" || documentoElectronico.EstadoEnvio == "rechazado") && documentoElectronico.CorreoNotificacion != "")
                    {
                        strBody = "Adjunto XML con estado " + documentoElectronico.EstadoEnvio + " del documento electrónico con clave " + documentoElectronico.ClaveNumerica + " y la respuesta del Ministerio de Hacienda.";
                        JObject jobDatosAdjuntos1 = new JObject();
                        jobDatosAdjuntos1["nombre"] = documentoElectronico.ClaveNumerica + ".xml";
                        jobDatosAdjuntos1["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento);
                        jarrayObj.Add(jobDatosAdjuntos1);
                        JObject jobDatosAdjuntos2 = new JObject();
                        jobDatosAdjuntos2["nombre"] = "RespuestaHacienda.xml";
                        jobDatosAdjuntos2["contenido"] = Convert.ToBase64String(documentoElectronico.Respuesta);
                        jarrayObj.Add(jobDatosAdjuntos2);
                        servicioEnvioCorreo.SendEmail(new string[] { documentoElectronico.CorreoNotificacion }, new string[] { }, "Documento electrónico con clave " + documentoElectronico.ClaveNumerica, strBody, false, jarrayObj);
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                string strBody = "El documento con clave " + documentoElectronico.ClaveNumerica + " genero un error en el envío del PDF al receptor:" + ex.Message;
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al tratar de enviar el correo al receptor.", strBody, false, emptyJArray);
            }
        }
    }
}
