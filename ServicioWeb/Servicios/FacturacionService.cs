using System;
using System.Linq;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using log4net;
using Unity;
using System.Threading.Tasks;
using LeandroSoftware.Core.Servicios;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Hosting;
using System.Globalization;
using System.Xml;
using System.Text;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.TiposDatosHacienda;
using LeandroSoftware.Core.Utilities;
using System.Drawing;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IFacturacionService
    {
        void AgregarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void EliminarCliente(int intIdCliente);
        Cliente ObtenerCliente(int intIdCliente);
        Cliente ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion);
        int ObtenerTotalListaClientes(int intIdEmpresa, string strNombre, bool incluyeClienteContado = false);
        IEnumerable<LlaveDescripcion> ObtenerListadoClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre, bool incluyeClienteContado = false);
        string AgregarFactura(Factura factura, DatosConfiguracion datos);
        void AnularFactura(int intIdFactura, int intIdUsuario, DatosConfiguracion datos);
        Factura ObtenerFactura(int intIdFactura);
        int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura, string strNombre);
        IEnumerable<LlaveDescripcion> ObtenerListadoFacturas(int intIdEmpresa, int numPagina, int cantRec, int intIdFactura, string strNombre);
        IEnumerable<LlaveDescripcion> ObtenerListadoFacturasPorCliente(int intIdCliente);
        Proforma AgregarProforma(Proforma proforma);
        void ActualizarProforma(Proforma proforma);
        void AnularProforma(int intIdProforma, int intIdUsuario);
        Proforma ObtenerProforma(int intIdProforma);
        int ObtenerTotalListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int intIdProforma, string strNombre);
        IEnumerable<Proforma> ObtenerListadoProformas(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdProforma, string strNombre);
        OrdenServicio AgregarOrdenServicio(OrdenServicio ordenServicio);
        void ActualizarOrdenServicio(OrdenServicio ordenServicio);
        void AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario);
        OrdenServicio ObtenerOrdenServicio(int intIdOrdenServicio);
        int ObtenerTotalListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenServicio, string strNombre);
        IEnumerable<OrdenServicio> ObtenerListadoOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenServicio, string strNombre);
        DevolucionCliente AgregarDevolucionCliente(DevolucionCliente devolucion);
        void AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario);
        DevolucionCliente ObtenerDevolucionCliente(int intIdDevolucion);
        int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdDevolucion, string strNombre);
        IEnumerable<DevolucionCliente> ObtenerListadoDevolucionesPorCliente(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre);
        void GeneraMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intMensaje, DatosConfiguracion datos);

        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosPendientes();
        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa);
        void ProcesarDocumentosElectronicosPendientes(ICorreoService servicioEnvioCorreo, DatosConfiguracion datos);
        void EnviarDocumentoElectronicoPendiente(int intIdDocumento, DatosConfiguracion datos);
        DocumentoElectronico ObtenerRespuestaDocumentoElectronicoEnviado(int intIdDocumento, DatosConfiguracion datos);
        int ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa);
        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int numPagina, int cantRec);
        DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento);
        DocumentoElectronico ObtenerDocumentoElectronicoPorClave(string strClave);
        void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores);
        void EnviarNotificacionDocumentoElectronico(int intIdDocumento, string strCorreoReceptor, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores);
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

        public void AgregarCliente(Cliente cliente)
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
                    cliente.ParametroImpuesto = null;
                    cliente.Vendedor = null;
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
                    var cliente = dbContext.ClienteRepository.Include("ParametroImpuesto").Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdCliente == intIdCliente).FirstOrDefault();
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
                    Cliente cliente = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa & x.Identificacion == strIdentificacion).FirstOrDefault();
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

        public int ObtenerTotalListaClientes(int intIdEmpresa, string strNombre, bool incluyeClienteContado = false)
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

        public IEnumerable<LlaveDescripcion> ObtenerListadoClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre, bool incluyeClienteContado = false)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCliente = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!incluyeClienteContado)
                        listado = listado.Where(x => x.IdCliente > 1);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.Nombre.Contains(strNombre));
                    if (cantRec == 0)
                        listado = listado.OrderBy(x => x.Nombre).OrderBy(x => x.Nombre);
                    else
                        listado = listado.OrderBy(x => x.IdCliente).Skip((numPagina - 1) * cantRec).Take(cantRec).OrderBy(x => x.Nombre);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCliente, value.Nombre);
                        listaCliente.Add(item);
                    }
                    return listaCliente;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de clientes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarFactura(Factura factura, DatosConfiguracion datos)
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
            decimal decTipoDeCambio = 1;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (factura.IdTipoMoneda == 2)
                    {
                        string criteria = factura.Fecha.ToString("dd/MM/yyyy");
                        TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                        if (tipoDeCambio == null) throw new BusinessException("El tipo de cambio para la fecha '" + criteria + "' no ha sido actualizado. Por favor consulte con su proveedor.");
                        decTipoDeCambio = tipoDeCambio.ValorTipoCambio;
                    }
                    factura.TipoDeCambioDolar = decTipoDeCambio;
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
                            Referencia = factura.TextoAdicional,
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
                                Referencia = factura.TextoAdicional,
                                Cantidad = detalleFactura.Cantidad,
                                PrecioCosto = detalleFactura.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad -= detalleFactura.Cantidad;
                            dbContext.NotificarModificacion(producto);
                        }
                        if (empresa.Contabiliza)
                        {
                            decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                            decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                            if (!detalleFactura.Excento)
                                decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (detalleFactura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                            if (producto.Tipo == StaticTipoProducto.Producto)
                            {
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
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos & x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                        }
                        IContabilidadService servicioContabilidad = new ContabilidadService();
                        servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    }
                    DocumentoElectronico documentoFE = null;
                    if (!empresa.RegimenSimplificado)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);                        
                        if (factura.IdCliente > 1)
                            documentoFE = ComprobanteElectronicoService.GeneraFacturaElectronica(factura, factura.Empresa, cliente, dbContext, decTipoDeCambio);
                        else
                            documentoFE = ComprobanteElectronicoService.GeneraTiqueteElectronico(factura, factura.Empresa, cliente, dbContext, decTipoDeCambio);
                        factura.IdDocElectronico = documentoFE.ClaveNumerica;
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
                        Task.Run(() => ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa.IdEmpresa, documentoFE.IdDocumento, datos));
                    }
                    Factura newFactura = dbContext.FacturaRepository.Include("Cliente.Barrio.Distrito.Canton.Provincia").Include("Vendedor").Include("DetalleFactura.Producto.TipoProducto").Include("DesglosePagoFactura.FormaPago").Include("DesglosePagoFactura.TipoMoneda").FirstOrDefault(x => x.IdFactura == factura.IdFactura);
                    newFactura.Empresa = null;
                    foreach (DetalleFactura detalle in newFactura.DetalleFactura)
                    {
                        detalle.Factura = null;
                        detalle.Producto.Empresa = null;
                        detalle.Producto.Linea = null;
                        detalle.Producto.MovimientoProducto = null;
                    }
                    foreach (DesglosePagoFactura desglosePago in newFactura.DesglosePagoFactura)
                        desglosePago.Factura = null;
                    newFactura.Cliente.Empresa = null;
                    newFactura.Cliente.Factura = null;
                    newFactura.Vendedor.Empresa = null;
                    return newFactura.IdFactura.ToString();
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
        }

        public void AnularFactura(int intIdFactura, int intIdUsuario, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    if (factura.Nulo == true) throw new BusinessException("La factura ya ha sido anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(factura.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                    if (!empresa.RegimenSimplificado && factura.IdDocElectronicoRev == null)
                    {
                        DocumentoElectronico documentoNC = null;
                        DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == factura.IdDocElectronico).FirstOrDefault();
                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado)
                        {
                            string criteria = factura.Fecha.ToString("dd/MM/yyyy");
                            TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                            if (tipoDeCambio != null)
                            {
                                documentoNC = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronica(factura, factura.Empresa, cliente, dbContext, tipoDeCambio.ValorTipoCambio);
                                factura.IdDocElectronicoRev = documentoNC.ClaveNumerica;
                            }
                            else
                            {
                                throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                            }
                            dbContext.NotificarModificacion(factura);
                            dbContext.Commit();
                            if (documentoNC != null)
                            {
                                Task.Run(() => ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa.IdEmpresa, documentoNC.IdDocumento, datos));
                            }
                        }
                        else if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            AnularRegistroFactura(factura, intIdUsuario, dbContext);
                            dbContext.Commit();
                        }
                        else
                        {
                            throw new BusinessException("El documento electrónico no ha sido procesado por el Ministerio de Hacienda. La factura no puede ser reversada en este momento.");
                        }
                    }
                    else
                    {
                        if (factura.Nulo == false && factura.IdDocElectronicoRev != null)
                        {
                            DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == factura.IdDocElectronicoRev).FirstOrDefault();
                            if (documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Aceptado & documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Rechazado)
                            {
                                throw new BusinessException("La factura se encuentra en proceso de anulación ya que posee una nota de crédito pendiente de procesar por el Ministerio de Hacienda.");
                            }
                            if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado)
                            {
                                AnularRegistroFactura(factura, intIdUsuario, dbContext);
                                dbContext.Commit();
                            } else
                            {
                                factura.IdDocElectronicoRev = null;
                                dbContext.NotificarModificacion(factura);
                                dbContext.Commit();
                            }
                        }
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

        private void AnularRegistroFactura(Factura factura, int intIdUsuario, IDbContext dbContext)
        {
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
                        Referencia = factura.TextoAdicional,
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
        }

        public Factura ObtenerFactura(int intIdFactura)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("Cliente.Barrio.Distrito.Canton.Provincia").Include("Vendedor").Include("DetalleFactura.Producto.TipoProducto").Include("DesglosePagoFactura.FormaPago").Include("DesglosePagoFactura.TipoMoneda").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    foreach (DetalleFactura detalle in factura.DetalleFactura)
                        detalle.Factura = null;
                    foreach (DesglosePagoFactura desglosePago in factura.DesglosePagoFactura)
                        desglosePago.Factura = null;
                    factura.Cliente.Factura = null;
                    return factura;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdFactura, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaFacturas = dbContext.FacturaRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdFactura > 0)
                        listaFacturas = listaFacturas.Where(x => !x.Nulo & x.IdFactura == intIdFactura);
                    else if (!strNombre.Equals(string.Empty))
                        listaFacturas = listaFacturas.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa & x.Cliente.Nombre.Contains(strNombre));
                    return listaFacturas.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFacturas(int intIdEmpresa, int numPagina, int cantRec, int intIdFactura, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFactura = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FacturaRepository.Include("Cliente").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdFactura > 0)
                        listado = listado.Where(x => !x.Nulo & x.IdFactura == intIdFactura);
                    else if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa & x.Cliente.Nombre.Contains(strNombre));
                    listado = listado.OrderByDescending(x => x.IdFactura).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFactura, value.Fecha.ToString("dd/MM/yyyy") + "   -   " + value.Cliente.Nombre);
                        listaFactura.Add(item);
                    }
                    return listaFactura;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFacturasPorCliente(int intIdCliente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFactura = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FacturaRepository.Where(x => !x.Nulo & x.IdCliente == intIdCliente);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFactura, value.Fecha.ToString("dd/MM/yyyy") + "   -   " + value.Cliente.Nombre);
                        listaFactura.Add(item);
                    }
                    return listaFactura;
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

        public int ObtenerTotalListaProformas(int intIdEmpresa, bool bolIncluyeTodo, int intIdProforma, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProformas = dbContext.ProformaRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdProforma > 0)
                        listaProformas = listaProformas.Where(x => !x.Nulo & x.IdProforma == intIdProforma);
                    else
                    {
                        if (!bolIncluyeTodo)
                            listaProformas = listaProformas.Where(x => !x.Aplicado);
                        if (!strNombre.Equals(string.Empty))
                            listaProformas = listaProformas.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa & x.Cliente.Nombre.Contains(strNombre));
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

        public IEnumerable<Proforma> ObtenerListadoProformas(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdProforma, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProformas = dbContext.ProformaRepository.Include("Cliente").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
                    if (intIdProforma > 0)
                        listaProformas = listaProformas.Where(x => !x.Nulo & x.IdProforma == intIdProforma);
                    else
                    {
                        if (!bolIncluyeTodo)
                            listaProformas = listaProformas.Where(x => !x.Aplicado);
                        if (!strNombre.Equals(string.Empty))
                            listaProformas = listaProformas.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa & x.Cliente.Nombre.Contains(strNombre));
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

        public int ObtenerTotalListaOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int intIdOrdenServicio, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesServicio = dbContext.OrdenServicioRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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

        public IEnumerable<OrdenServicio> ObtenerListadoOrdenesServicio(int intIdEmpresa, bool bolIncluyeTodo, int numPagina, int cantRec, int intIdOrdenServicio, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaOrdenesServicio = dbContext.OrdenServicioRepository.Include("Cliente").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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
                                    if (!detalleDevolucion.Excento)
                                        decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (detalleDevolucion.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
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

        public int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdDevolucion, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionClienteRepository.Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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

        public IEnumerable<DevolucionCliente> ObtenerListadoDevolucionesPorCliente(int intIdEmpresa, int numPagina, int cantRec, int intIdDevolucion, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionClienteRepository.Include("Factura").Where(x => !x.Nulo & x.IdEmpresa == intIdEmpresa);
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

        public void GeneraMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intMensaje, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    DocumentoElectronico documentoMR = ComprobanteElectronicoService.GeneraMensajeReceptor(strDatos, empresa, dbContext, intSucursal, intTerminal, intMensaje);
                    dbContext.Commit();
                    if (documentoMR != null)
                    {
                        Task.Run(() => ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa.IdEmpresa, documentoMR.IdDocumento, datos));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error al procesar factura electrónica: ", ex);
                    throw ex;
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosPendientes()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    List<DocumentoElectronico> listado = dbContext.DocumentoElectronicoRepository.Where(x => x.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Procesando).OrderBy(x => x.ClaveNumerica).ToList();
                    foreach (var value in listado)
                    {
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha, value.EstadoEnvio, value.EsMensajeReceptor, value.CorreoNotificacion);
                        listaDocumento.Add(item);
                    }
                    return listaDocumento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de documentos electrónicos pendientes: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    List<DocumentoElectronico> listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa & (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Procesando)).ToList();
                    foreach (var value in listado)
                    {
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha, value.EstadoEnvio, value.EsMensajeReceptor, value.CorreoNotificacion);
                        listaDocumento.Add(item);
                    }
                    return listaDocumento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de documentos electrónicos pendientes: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public async void ProcesarDocumentosElectronicosPendientes(ICorreoService servicioEnvioCorreo, DatosConfiguracion datos)
        {
            var stringBuilder = new StringBuilder();
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    ParametroSistema procesando = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 2).FirstOrDefault();
                    if (procesando != null && procesando.Valor == "NO")
                    {
                        procesando.Valor = "SI";
                        dbContext.Commit();
                        List<DocumentoElectronico> listaPendientes = new List<DocumentoElectronico>();
                        try
                        {
                            listaPendientes = dbContext.DocumentoElectronicoRepository.Where(x => x.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado).OrderBy(x => x.IdEmpresa).ToList();
                        }
                        catch (Exception ex)
                        {
                            string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                            stringBuilder.AppendLine("Error al obtener la lista de documentos pendientes de procesar. Detalle: " + strError);
                        }
                        foreach (DocumentoElectronico documento in listaPendientes)
                        {
                            Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                            if (empresa != null)
                            {
                                if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado)
                                {
                                    try
                                    {
                                        await ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa.IdEmpresa, documento.IdDocumento, datos);
                                    }
                                    catch (Exception ex)
                                    {
                                        string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                        stringBuilder.AppendLine("Error al enviar el documento electrónico con id: " + documento.IdDocumento + " Detalle: " + strError);
                                    }
                                }
                                else if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)
                                {
                                    DocumentoElectronico estadoDoc = null;
                                    try
                                    {
                                        estadoDoc = await ComprobanteElectronicoService.ConsultarDocumentoElectronico(empresa, documento, dbContext, datos);
                                    }
                                    catch (Exception ex)
                                    {
                                        string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                        stringBuilder.AppendLine("Error al consultar el documento electrónico con id: " + documento.IdDocumento + " Detalle: " + strError);
                                    }
                                    if (estadoDoc != null)
                                    {
                                        if (estadoDoc.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || estadoDoc.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado)
                                        {
                                            RespuestaHaciendaDTO respuesta = new RespuestaHaciendaDTO();
                                            if (documento.EsMensajeReceptor == "S")
                                                respuesta.Clave = documento.ClaveNumerica + "-" + documento.Consecutivo;
                                            else
                                                respuesta.Clave = documento.ClaveNumerica;
                                            respuesta.Fecha = documento.Fecha.ToString("yyyy-MM-dd'T'HH:mm:ssZ");
                                            respuesta.IndEstado = estadoDoc.EstadoEnvio;
                                            respuesta.RespuestaXml = Convert.ToBase64String(estadoDoc.Respuesta);
                                            try
                                            {
                                                ProcesarMensajeDeRespuesta(respuesta, dbContext, servicioEnvioCorreo, datos.CorreoNotificacionErrores);
                                            }
                                            catch (Exception ex)
                                            {
                                                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                                stringBuilder.AppendLine("Error al procesar la respuesta del documento electrónico con id: " + documento.IdDocumento + " Detalle: " + strError);
                                            }
                                        }
                                    }
                                }
                            } else
                            {
                                stringBuilder.AppendLine("Error al procesar el documento electrónico: No se encontro la empresa con id: " + documento.IdEmpresa);
                            }
                            
                        }
                        procesando.Valor = "NO";
                        dbContext.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                stringBuilder.AppendLine("Error al procesar los documentos electrónicos pendientes: " + strError);
            }
            if (stringBuilder.Length > 0)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                stringBuilder = null;
                JArray archivosJArray = new JArray();
                JObject jobDatosAdjuntos1 = new JObject
                {
                    ["nombre"] = "logErrores-" + DateTime.Now.ToString("ddMMyyyy-HH-mm-ss") + ".txt",
                    ["contenido"] = Convert.ToBase64String(bytes)
                };
                archivosJArray.Add(jobDatosAdjuntos1);
                servicioEnvioCorreo.SendEmail(new string[] { datos.CorreoNotificacionErrores }, new string[] { }, "Excepción en la interface de procesamiento de documentos pendientes", "Adjunto el archivo con el detalle de los errores en procesamiento.", false, archivosJArray);
            }
        }

        public async void EnviarDocumentoElectronicoPendiente(int intIdDocumento, DatosConfiguracion datos)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado)
                    {
                        try
                        {
                            await ComprobanteElectronicoService.EnviarDocumentoElectronico(empresa.IdEmpresa, documento.IdDocumento, datos);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (BusinessException ex)
                {
                    throw ex;
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
                    if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)
                    {
                        respuesta = ComprobanteElectronicoService.ConsultarDocumentoElectronico(empresa, documento, dbContext, datos).Result;
                    }
                    return respuesta;
                }
                catch (BusinessException ex)
                {
                    throw ex;
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
                    var listaProcesados = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa & (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado));
                    return listaProcesados.Count();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de documentos electrónicos procesados: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de documentos electrónicos procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int numPagina, int cantRec)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<DocumentoElectronico> listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa & (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado))
                        .OrderByDescending(x => x.IdDocumento)
                        .Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    foreach (var value in listado)
                    {
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha, value.EstadoEnvio, value.EsMensajeReceptor, value.CorreoNotificacion);
                        listaDocumento.Add(item);
                    }
                    return listaDocumento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null) throw new BusinessException("El documento solicitado no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    return documento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar documento electrónico con ID: " + intIdDocumento, ex);
                    throw new Exception("Se produjo un error al consultar el documento electrónico. Por favor consulte con su proveedor.");
                }
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronicoPorClave(string strClave)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave).FirstOrDefault();
                    if (documento == null) throw new BusinessException("El documento solicitado no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    return documento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar documento electrónico con clave: " + strClave, ex);
                    throw new Exception("Se produjo un error al consultar el documento electrónico. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    ProcesarMensajeDeRespuesta(mensaje, dbContext, servicioEnvioCorreo, strCorreoNotificacionErrores);
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en la interface de procesamiento del mensaje de respuesta de Hacienda", ex.Message, false, emptyJArray);
            }
        }

        private void ProcesarMensajeDeRespuesta(RespuestaHaciendaDTO mensaje, IDbContext dbContext, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
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
                    if (strEstado == StaticEstadoDocumentoElectronico.Aceptado || strEstado == StaticEstadoDocumentoElectronico.Rechazado)
                    {
                        byte[] bytRespuestaXML = Convert.FromBase64String(mensaje.RespuestaXml);
                        documentoElectronico.Respuesta = bytRespuestaXML;
                        dbContext.NotificarModificacion(documentoElectronico);
                        RegistroRespuestaHacienda registro = new RegistroRespuestaHacienda
                        {
                            ClaveNumerica = documentoElectronico.ClaveNumerica,
                            Fecha = DateTime.Now,
                            Respuesta = bytRespuestaXML
                        };
                        dbContext.RegistroRespuestaHaciendaRepository.Add(registro);
                        dbContext.Commit();
                        if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaCreditoElectronica)
                        {
                            Factura factura = dbContext.FacturaRepository.Where(x => x.IdDocElectronicoRev == strClave).FirstOrDefault();
                            if (factura != null)
                            {
                                if (!factura.Nulo)
                                {
                                    if (strEstado == StaticEstadoDocumentoElectronico.Aceptado)
                                    {
                                        AnularRegistroFactura(factura, factura.IdUsuario, dbContext);
                                        dbContext.Commit();
                                    }
                                    else
                                    {
                                        factura.IdDocElectronicoRev = null;
                                        dbContext.NotificarModificacion(factura);
                                        dbContext.Commit();
                                    }
                                }
                            }
                        }
                        if ((documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica || documentoElectronico.IdTipoDocumento == (int)TipoDocumento.TiqueteElectronico) && strEstado == StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            Factura factura = dbContext.FacturaRepository.Where(x => x.IdDocElectronico == strClave).FirstOrDefault();
                            if (factura != null)
                            {
                                if (!factura.Nulo)
                                {
                                    AnularRegistroFactura(factura, factura.IdUsuario, dbContext);
                                    dbContext.Commit();
                                }
                            }
                        }
                        if (documentoElectronico.CorreoNotificacion != "") GenerarNotificacionDocumentoElectronico(documentoElectronico, empresa, dbContext, servicioEnvioCorreo, documentoElectronico.CorreoNotificacion, strCorreoNotificacionErrores);
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioEnvioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el procesamiento de la respuesta de hacienda para el comprobante con clave: " + mensaje.Clave, ex.Message, false, emptyJArray);
            }
        }

        public void EnviarNotificacionDocumentoElectronico(int intIdDocumento, string strCorreoReceptor, ICorreoService servicioEnvioCorreo, string strCorreoNotificacionErrores)
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
                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            GenerarNotificacionDocumentoElectronico(documento, empresa, dbContext, servicioEnvioCorreo, strCorreoReceptor, strCorreoNotificacionErrores);
                        }
                    }
                }
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error("Error al enviar notificación al receptor para el documento con ID: " + intIdDocumento, ex);
                throw new Exception("Se produjo un error al consultar el documento electrónico. Por favor consulte con su proveedor.");
            }
        }

        private void GenerarNotificacionDocumentoElectronico(DocumentoElectronico documentoElectronico, Empresa empresa, IDbContext dbContext, ICorreoService servicioEnvioCorreo, string strCorreoReceptor, string strCorreoNotificacionErrores)
        {
            try
            {
                string strBody;
                string strTitle = "";
                JArray jarrayObj = new JArray();
                if (documentoElectronico.EsMensajeReceptor == "N")
                {
                    if (documentoElectronico.EstadoEnvio == "aceptado" & strCorreoReceptor != "")
                    {
                        string[] arrCorreoReceptor = strCorreoReceptor.Split(';');
                        strBody = "Estimado cliente, adjunto encontrará el detalle del documento electrónico en formato PDF y XML con clave " + documentoElectronico.ClaveNumerica + " y la respuesta de aceptación del Ministerio de Hacienda.";
                        EstructuraPDF datos = new EstructuraPDF();
                        try
                        {
                            string apPath = HostingEnvironment.ApplicationPhysicalPath + "images\\Logo.png";
                            Image poweredByImage = Image.FromFile(apPath);
                            datos.PoweredByLogotipo = poweredByImage;
                        }
                        catch (Exception)
                        {
                            datos.PoweredByLogotipo = null;
                        }
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
                        if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica || documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaCreditoElectronica)
                        {
                            if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica)
                            {
                                strTitle = "Factura electrónica de emisor " + empresa.NombreComercial;
                                datos.TituloDocumento = "FACTURA ELECTRONICA";
                            }
                            else
                            {
                                strTitle = "Nota de Crédito electrónica de emisor " + empresa.NombreComercial;
                                datos.TituloDocumento = "NOTA DE CREDITO ELECTRONICA";
                            }
                            string datosXml = Encoding.Default.GetString(documentoElectronico.DatosDocumento);
                            XmlDocument documentoXml = new XmlDocument();
                            documentoXml.LoadXml(datosXml);
                            datos.NombreEmpresa = empresa.NombreEmpresa;
                            datos.NombreComercial = empresa.NombreComercial;
                            datos.Consecutivo = documentoElectronico.Consecutivo;
                            datos.PlazoCredito = documentoXml.GetElementsByTagName("PlazoCredito").Count > 0 ? documentoXml.GetElementsByTagName("PlazoCredito").Item(0).InnerText : "";
                            datos.Clave = documentoElectronico.ClaveNumerica;
                            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(int.Parse(documentoXml.GetElementsByTagName("CondicionVenta").Item(0).InnerText));
                            datos.Fecha = documentoElectronico.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
                            datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(int.Parse(documentoXml.GetElementsByTagName("MedioPago").Item(0).InnerText));
                            XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                            datos.NombreEmisor = emisorNode["Nombre"] != null && emisorNode["Nombre"].ChildNodes.Count > 0 ? emisorNode["Nombre"].InnerText : "";
                            datos.NombreComercialEmisor = emisorNode["NombreComercial"] != null && emisorNode["NombreComercial"].ChildNodes.Count > 0 ? emisorNode["NombreComercial"].InnerText : "";
                            datos.IdentificacionEmisor = emisorNode["Identificacion"]["Numero"].InnerText;
                            datos.CorreoElectronicoEmisor = emisorNode["CorreoElectronico"].InnerText;
                            datos.TelefonoEmisor = emisorNode["Telefono"] != null && emisorNode["Telefono"].ChildNodes.Count > 0 ? emisorNode["Telefono"]["NumTelefono"].InnerText : "";
                            datos.FaxEmisor = emisorNode["Fax"] != null && emisorNode["Fax"].ChildNodes.Count > 0 ? emisorNode["Fax"]["NumTelefono"].InnerText : "";
                            int intProvincia = int.Parse(emisorNode["Ubicacion"]["Provincia"].InnerText);
                            int intCanton = int.Parse(emisorNode["Ubicacion"]["Canton"].InnerText);
                            int intDistrito = int.Parse(emisorNode["Ubicacion"]["Distrito"].InnerText);
                            int intBarrio = int.Parse(emisorNode["Ubicacion"]["Barrio"].InnerText);
                            datos.ProvinciaEmisor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton & x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton & x.IdDistrito == intDistrito & x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                            datos.DireccionEmisor = emisorNode["Ubicacion"]["OtrasSenas"].InnerText;
                            string strExoneracionLeyenda = "";
                            if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                            {
                                XmlNode receptorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                                datos.PoseeReceptor = true;
                                datos.NombreReceptor = receptorNode["Nombre"] != null && receptorNode["Nombre"].ChildNodes.Count > 0 ? receptorNode["Nombre"].InnerText : "";
                                datos.NombreComercialReceptor = receptorNode["NombreComercial"] != null && receptorNode["NombreComercial"].ChildNodes.Count > 0 ? receptorNode["NombreComercial"].InnerText : "";
                                datos.IdentificacionReceptor = receptorNode["Identificacion"]["Numero"].InnerText;
                                datos.CorreoElectronicoReceptor = receptorNode["CorreoElectronico"].InnerText;
                                datos.TelefonoReceptor = receptorNode["Telefono"] != null && receptorNode["Telefono"].ChildNodes.Count > 0 ? receptorNode["Telefono"]["NumTelefono"].InnerText : "";
                                datos.FaxReceptor = receptorNode["Fax"] != null && receptorNode["Fax"].ChildNodes.Count > 0 ? receptorNode["Fax"]["NumTelefono"].InnerText : "";
                                intProvincia = int.Parse(receptorNode["Ubicacion"]["Provincia"].InnerText);
                                intCanton = int.Parse(receptorNode["Ubicacion"]["Canton"].InnerText);
                                intDistrito = int.Parse(receptorNode["Ubicacion"]["Distrito"].InnerText);
                                intBarrio = int.Parse(receptorNode["Ubicacion"]["Barrio"].InnerText);
                                datos.ProvinciaReceptor = dbContext.ProvinciaRepository.Where(x => x.IdProvincia == intProvincia).FirstOrDefault().Descripcion;
                                datos.CantonReceptor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton).FirstOrDefault().Descripcion;
                                datos.DistritoReceptor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton & x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
                                datos.BarrioReceptor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia & x.IdCanton == intCanton & x.IdDistrito == intDistrito & x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
                                datos.DireccionReceptor = receptorNode["Ubicacion"]["OtrasSenas"].InnerText;
                            }
                            foreach (XmlNode linea in documentoXml.GetElementsByTagName("DetalleServicio"))
                            {
                                XmlNode lineaDetalle = linea["LineaDetalle"];
                                if (lineaDetalle["Impuesto"] != null)
                                {
                                    if (lineaDetalle["Impuesto"]["Exoneracion"] != null)
                                    {
                                        if (strExoneracionLeyenda.Length == 0) strExoneracionLeyenda = "Se aplica exoneración segun oficio " + lineaDetalle["Impuesto"]["Exoneracion"]["NumeroDocumento"].InnerText + " por un porcentaje del " + lineaDetalle["Impuesto"]["Exoneracion"]["PorcentajeExoneracion"].InnerText + "% del valor gravado.";
                                    }
                                }
                                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                                {
                                    Cantidad = lineaDetalle["Cantidad"].InnerText,
                                    Codigo = lineaDetalle["Codigo"].InnerText,
                                    Detalle = lineaDetalle["Detalle"].InnerText,
                                    PrecioUnitario = string.Format("{0:N5}", Convert.ToDouble(lineaDetalle["PrecioUnitario"].InnerText, CultureInfo.InvariantCulture)),
                                    TotalLinea = string.Format("{0:N5}", Convert.ToDouble(lineaDetalle["MontoTotalLinea"].InnerText, CultureInfo.InvariantCulture))
                                };
                                datos.DetalleServicio.Add(detalle);
                            }
                            string otrosTextos = documentoXml.GetElementsByTagName("Otros") != null && documentoXml.GetElementsByTagName("Otros").Count > 0 ? documentoXml.GetElementsByTagName("Otros").Item(0)["OtroTexto"].InnerText : "";
                            if (strExoneracionLeyenda.Length > 0)
                            {
                                if (otrosTextos.Length > 0) otrosTextos += " ";
                                otrosTextos += strExoneracionLeyenda;
                            }
                            if (otrosTextos.Length > 0) datos.OtrosTextos = otrosTextos;
                            XmlNode resumenFacturaNode = documentoXml.GetElementsByTagName("ResumenFactura").Item(0);
                            datos.TotalGravado = string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalGravado"].InnerText, CultureInfo.InvariantCulture));
                            datos.TotalExonerado = resumenFacturaNode["TotalExonerado"] != null && resumenFacturaNode["TotalExonerado"].ChildNodes.Count > 0 ? string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalExonerado"].InnerText, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.TotalExento = string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalExento"].InnerText, CultureInfo.InvariantCulture));
                            datos.Descuento = resumenFacturaNode["TotalDescuentos"] != null && resumenFacturaNode["TotalDescuentos"].ChildNodes.Count > 0 ? string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalDescuentos"].InnerText, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.Impuesto = resumenFacturaNode["TotalImpuesto"] != null && resumenFacturaNode["TotalImpuesto"].ChildNodes.Count > 0 ? string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalImpuesto"].InnerText, CultureInfo.InvariantCulture)) : "0.00000";
                            datos.TotalGeneral = string.Format("{0:N5}", Convert.ToDouble(resumenFacturaNode["TotalComprobante"].InnerText, CultureInfo.InvariantCulture));
                            datos.CodigoMoneda = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? resumenFacturaNode["CodigoTipoMoneda"]["CodigoMoneda"].InnerText : "CRC";
                            datos.TipoDeCambio = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? resumenFacturaNode["CodigoTipoMoneda"]["TipoCambio"].InnerText : "1.00000";
                        }
                        byte[] pdfAttactment = UtilitarioPDF.GenerarPDFFacturaElectronica(datos);
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = documentoElectronico.ClaveNumerica + ".pdf",
                            ["contenido"] = Convert.ToBase64String(pdfAttactment)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        JObject jobDatosAdjuntos2 = new JObject
                        {
                            ["nombre"] = documentoElectronico.ClaveNumerica + ".xml",
                            ["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento)
                        };
                        jarrayObj.Add(jobDatosAdjuntos2);
                        JObject jobDatosAdjuntos3 = new JObject
                        {
                            ["nombre"] = "RespuestaHacienda.xml",
                            ["contenido"] = Convert.ToBase64String(documentoElectronico.Respuesta)
                        };
                        jarrayObj.Add(jobDatosAdjuntos3);
                        servicioEnvioCorreo.SendEmail(arrCorreoReceptor, new string[] { }, strTitle, strBody, false, jarrayObj);
                    }
                    else if(documentoElectronico.EstadoEnvio == "rechazado")
                    {
                        XmlDocument xmlRespuesta = new XmlDocument();
                        xmlRespuesta.LoadXml(Encoding.UTF8.GetString(documentoElectronico.Respuesta));
                        string strMensajeHacienda = xmlRespuesta.GetElementsByTagName("DetalleMensaje").Item(0).InnerText;
                        strBody = "Estimado cliente, le informamos que el documento electrónico con clave " + documentoElectronico.ClaveNumerica + " fue rechazado por el Ministerio de Hacienda con el siguiente mensaje:\n\n" + strMensajeHacienda + "\n\nPara mayor información consulte el documento en su plataforma de factura electrónica.";
                        JArray emptyJArray = new JArray();
                        servicioEnvioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Rechazo de documento electrónico con clave " + documentoElectronico.ClaveNumerica, strBody, false, emptyJArray);
                    }
                }
                else
                {
                    if ((documentoElectronico.EstadoEnvio == "aceptado" || documentoElectronico.EstadoEnvio == "rechazado"))
                    {
                        strBody = "Estimado cliente, adjunto detalle del documento electrónico con clave " + documentoElectronico.ClaveNumerica + " el cual fue " + documentoElectronico.EstadoEnvio + ". Adjunto puede observar los archivos XML correspondientes al documento electrónico y la respuesta del Ministerio de Hacienda.";
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = documentoElectronico.ClaveNumerica + ".xml",
                            ["contenido"] = Convert.ToBase64String(documentoElectronico.DatosDocumento)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        JObject jobDatosAdjuntos2 = new JObject
                        {
                            ["nombre"] = "RespuestaHacienda.xml",
                            ["contenido"] = Convert.ToBase64String(documentoElectronico.Respuesta)
                        };
                        jarrayObj.Add(jobDatosAdjuntos2);
                        servicioEnvioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Estado de documento electrónico enviado a aceptación", strBody, false, jarrayObj);
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