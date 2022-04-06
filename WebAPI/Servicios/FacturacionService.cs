using System.Data;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Xml;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;
using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Parametros;
using LeandroSoftware.ServicioWeb.EstructuraDatos;
using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.TiposDatosHacienda;
using LeandroSoftware.ServicioWeb.Utilitario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        IList<LlaveDescripcion> ObtenerListadoClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre);
        string AgregarFactura(Factura factura, ConfiguracionGeneral datos);
        string AgregarFacturaCompra(FacturaCompra facturaCompra, ConfiguracionGeneral datos);
        void AnularFactura(int intIdFactura, int intIdUsuario, string strMotivoAnulacion, ConfiguracionGeneral datos);
        Factura ObtenerFactura(int intIdFactura);
        int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int intIdFactura, string strNombre, string strIdentificacion);
        IList<FacturaDetalle> ObtenerListadoFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdFactura, string strNombre, string strIdentificacion);
        string AgregarProforma(Proforma proforma);
        void ActualizarProforma(Proforma proforma);
        void AnularProforma(int intIdProforma, int intIdUsuario, string strMotivoAnulacion);
        Proforma ObtenerProforma(int intIdProforma);
        int ObtenerTotalListaProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdProforma, string strNombre);
        IList<FacturaDetalle> ObtenerListadoProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdProforma, string strNombre);
        string AgregarApartado(Apartado apartado);
        void AnularApartado(int intIdApartado, int intIdUsuario, string strMotivoAnulacion);
        Apartado ObtenerApartado(int intIdApartado);
        int ObtenerTotalListaApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdApartado, string strNombre);
        IList<FacturaDetalle> ObtenerListadoApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdApartado, string strNombre);
        string AgregarOrdenServicio(OrdenServicio ordenServicio);
        void ActualizarOrdenServicio(OrdenServicio ordenServicio);
        void AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario, string strMotivoAnulacion);
        OrdenServicio ObtenerOrdenServicio(int intIdOrdenServicio);
        int ObtenerTotalListaOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdOrdenServicio, string strNombre);
        IList<FacturaDetalle> ObtenerListadoOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdOrdenServicio, string strNombre);
        IList<ClsTiquete> ObtenerListadoTiqueteOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolImpreso, bool bolSortedDesc);
        void ActualizarEstadoTiqueteOrdenServicio(int intIdTiquete, bool bolEstado);
        string AgregarDevolucionCliente(DevolucionCliente devolucion, ConfiguracionGeneral datos);
        void AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario, string strMotivoAnulacion, ConfiguracionGeneral datos);
        DevolucionCliente ObtenerDevolucionCliente(int intIdDevolucion);
        int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal,  int intIdDevolucion, string strNombre);
        IList<FacturaDetalle> ObtenerListadoDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdDevolucion, string strNombre);
        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosPendientes();
        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa);
        void ProcesarDocumentosElectronicosPendientes(ConfiguracionGeneral datos, byte[] bytLogo);
        void GenerarMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado, bool bolIvaAplicable, ConfiguracionGeneral datos);
        void ProcesarCorreoRecepcion(ConfiguracionGeneral config, ConfiguracionRecepcion datos);
        void ReprocesarDocumentoElectronico(int intIdDocumento, ConfiguracionGeneral datos);
        int ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, string strNombre);
        IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strNombre);
        DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento);
        void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, string strCorreoNotificacionErrores, byte[] bytLogo);
        void EnviarNotificacionDocumentoElectronico(int intIdDocumento, string strCorreoReceptor, string strCorreoNotificacionErrores, byte[] bytLogo);
        byte[] GenerarFacturaPDF(int intIdFactura, byte[] bytLogo);
        byte[] GenerarApartadoPDF(int intIdApartado, byte[] bytLogo);
        byte[] GenerarOrdenServicioPDF(int intIdOrdenServicio, byte[] bytLogo);
        byte[] GenerarProformaPDF(int intIdProforma, byte[] bytLogo);
        void GenerarNotificacionFactura(int intIdFactura, byte[] bytLogo);
        void GenerarNotificacionProforma(int intIdProforma, string strCorreoReceptor, byte[] bytLogo);
    }

    public class FacturacionService : IFacturacionService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory serviceScopeFactory;
        private static ICorreoService servicioCorreo;

        public FacturacionService(ILoggerManager logger, ICorreoService pServicioCorreo, IServiceScopeFactory pServiceScopeFactory)
        {
            try
            {
                _logger = logger;
                serviceScopeFactory = pServiceScopeFactory;
                servicioCorreo = pServicioCorreo;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Facturación. Por favor consulte con su proveedor.");
            }
        }

        public void AgregarCliente(Cliente cliente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    try
                    {
                        Validador.ValidaFormatoIdentificacion(cliente.IdTipoIdentificacion, cliente.Identificacion);
                        Validador.ValidaFormatoEmail(cliente.CorreoElectronico);
                    }
                    catch (Exception ex)
                    {
                        throw new BusinessException(ex.Message);
                    }
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    bool existe = dbContext.ClienteRepository.AsNoTracking().FirstOrDefault(x => x.Identificacion == cliente.Identificacion && x.IdEmpresa == empresa.IdEmpresa) != null;
                    if (existe) throw new BusinessException("El cliente con identificación " + cliente.Identificacion + " ya se encuentra registrado en la empresa. Por favor verifique.");
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
                    _logger.LogError("Error al agregar el cliente: ", ex);
                    throw new Exception("Se produjo un error agregando al cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCliente(Cliente cliente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    try
                    {
                        Validador.ValidaFormatoIdentificacion(cliente.IdTipoIdentificacion, cliente.Identificacion);
                        Validador.ValidaFormatoEmail(cliente.CorreoElectronico);
                    }
                    catch (Exception ex)
                    {
                        throw new BusinessException(ex.Message);
                    }
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    bool existe = dbContext.ClienteRepository.AsNoTracking().FirstOrDefault(x => x.Identificacion == cliente.Identificacion && x.IdEmpresa == empresa.IdEmpresa && x.IdCliente != cliente.IdCliente) != null;
                    if (existe) throw new BusinessException("El cliente con identificación " + cliente.Identificacion + " ya se encuentra registrado en la empresa. Por favor verifique.");
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
                    _logger.LogError("Error al actualizar el cliente: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarCliente(int intIdCliente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Cliente cliente = dbContext.ClienteRepository.Find(intIdCliente);
                    if (cliente == null) throw new Exception("El cliente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(cliente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.ClienteRepository.Remove(cliente);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al eliminar cliente: ", ex);
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
                    _logger.LogError("Error al eliminar el cliente: ", ex);
                    throw new Exception("Se produjo un error eliminando al cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public Cliente ObtenerCliente(int intIdCliente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var cliente = dbContext.ClienteRepository.Where(x => x.IdCliente == intIdCliente).FirstOrDefault();
                    if (cliente.IdVendedor != null)
                    {
                        var vendedor = dbContext.VendedorRepository.Find(cliente.IdVendedor);
                        cliente.Vendedor = vendedor;
                    }
                    return cliente;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el cliente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public Cliente ValidaIdentificacionCliente(int intIdEmpresa, string strIdentificacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                                Nombre = persona.Nombre + " " + persona.PrimerApellido + " " + persona.SegundoApellido
                            };
                            return cliente;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el cliente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaClientes(int intIdEmpresa, string strNombre, bool incluyeClienteContado = false)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el total del listado de clientes: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoClientes(int intIdEmpresa, int numPagina, int cantRec, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaCliente = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ClienteRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdCliente > 1);
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
                    _logger.LogError("Error al obtener el listado de clientes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de clientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarFactura(Factura factura, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                decimal decTotalIngresosMercancia = 0;
                decimal decTotalIngresosServicios = 0;
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
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == factura.IdEmpresa && x.IdSucursal == factura.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == factura.IdEmpresa && x.IdSucursal == factura.IdSucursal && x.IdTerminal == factura.IdTerminal).FirstOrDefault();
                    if (terminal == null) throw new BusinessException("No se logró obtener la información de la terminal que envia la solicitud. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    if (factura.IdApartado > 0)
                    {
                        Apartado apartado = dbContext.ApartadoRepository.Find(factura.IdApartado);
                        apartado.Aplicado = true;
                        dbContext.NotificarModificacion(apartado);
                    }
                    if (empresa.Contabiliza)
                    {
                        ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IngresosPorVentas).FirstOrDefault();
                        costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CostosDeVentas).FirstOrDefault();
                        ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.IVAPorPagar).FirstOrDefault();
                        efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.Efectivo).FirstOrDefault();
                        cuentasPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarClientes).FirstOrDefault();
                        otraCondicionVentaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.OtraCondicionVenta).FirstOrDefault();
                        cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                        gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.GastoComisionTarjeta).FirstOrDefault();
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
                    if (terminal.IdTipoDispositivo == StaticTipoDispisitivo.AppMovil)
                    {
                        Vendedor vendedor = dbContext.VendedorRepository.Where(x => x.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                        if (vendedor == null) throw new BusinessException("La empresa no posee registrado ningún vendedor");
                        factura.IdVendedor = vendedor.IdVendedor;
                    }
                    Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                    if (cliente == null) throw new BusinessException("El cliente asignado a la factura no existe.");
                    if (cliente.IdCliente > 1)
                    {
                        if (cliente.IdTipoIdentificacion == 0 && cliente.Identificacion.Length != 9) throw new BusinessException("El cliente posee una identificación de tipo 'Cedula física' con una longitud inadecuada. Deben ser 9 caracteres.");
                        if (cliente.IdTipoIdentificacion == 1 && cliente.Identificacion.Length != 10) throw new BusinessException("El cliente posee una identificación de tipo 'Cedula jurídica' con una longitud inadecuada. Deben ser 10 caracteres");
                        if (cliente.IdTipoIdentificacion > 1 && (cliente.Identificacion.Length < 11 || cliente.Identificacion.Length > 12)) throw new BusinessException("El cliente posee una identificación de tipo 'DIMEX o DITE' con una longitud inadecuada. Deben ser 11 o 12 caracteres");
                    }
                    sucursal.ConsecFactura += 1;
                    dbContext.NotificarModificacion(sucursal);
                    factura.ConsecFactura = sucursal.ConsecFactura;
                    dbContext.FacturaRepository.Add(factura);
                    if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                    {
                        cuentaPorCobrar = new CuentaPorCobrar
                        {
                            IdEmpresa = factura.IdEmpresa,
                            IdSucursal = factura.IdSucursal,
                            IdUsuario = factura.IdUsuario,
                            IdTipoMoneda = factura.IdTipoMoneda,
                            IdPropietario = factura.IdCliente,
                            Referencia = factura.ConsecFactura.ToString(),
                            Fecha = factura.Fecha,
                            Tipo = StaticTipoCuentaPorCobrar.Clientes,
                            Total = factura.Total - factura.MontoAdelanto,
                            Saldo = factura.Total - factura.MontoAdelanto,
                            Nulo = false
                        };
                        dbContext.CuentaPorCobrarRepository.Add(cuentaPorCobrar);
                    }
                    decTotalImpuesto += factura.Impuesto;
                    foreach (var detalleFactura in factura.DetalleFactura)
                    {
                        Producto producto = dbContext.ProductoRepository.FirstOrDefault(x => x.IdProducto == detalleFactura.IdProducto);
                        if (producto == null)
                            throw new BusinessException("El producto con código " + producto.Codigo + " asignado al detalle de la factura no existe.");
                        if (!empresa.RegimenSimplificado && producto.CodigoClasificacion == "")
                            throw new BusinessException("El producto con código " + producto.Codigo + " asignado al detalle de la factura no posee clasificación CABYS.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == factura.IdSucursal).FirstOrDefault();
                            if (existencias != null)
                            {
                                existencias.Cantidad -= detalleFactura.Cantidad;
                                dbContext.NotificarModificacion(existencias);
                            }
                            else
                            {
                                ExistenciaPorSucursal nuevoRegistro = new ExistenciaPorSucursal
                                {
                                    IdEmpresa = factura.IdEmpresa,
                                    IdSucursal = factura.IdSucursal,
                                    IdProducto = detalleFactura.IdProducto,
                                    Cantidad = detalleFactura.Cantidad * -1
                                };
                                dbContext.ExistenciaPorSucursalRepository.Add(nuevoRegistro);
                            }
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = factura.IdSucursal,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Registro de facturación de mercancía de factura " + factura.ConsecFactura,
                                Cantidad = detalleFactura.Cantidad,
                                PrecioCosto = detalleFactura.PrecioCosto
                            };
                            producto.MovimientoProducto = new List<MovimientoProducto>();
                            producto.MovimientoProducto.Add(movimiento);
                        }
                        if (empresa.Contabiliza)
                        {
                            decimal decTotalPorLinea = Math.Round(detalleFactura.PrecioVenta * detalleFactura.Cantidad, 2, MidpointRounding.AwayFromZero);
                            if (!detalleFactura.Excento)
                                decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (detalleFactura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                            if (producto.Tipo == StaticTipoProducto.Producto)
                            {
                                decTotalIngresosMercancia += decTotalPorLinea;
                                decTotalCostoVentas += producto.PrecioCosto * detalleFactura.Cantidad;
                                int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.IdLinea));
                                if (intExiste >= 0)
                                    dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + (producto.PrecioCosto * detalleFactura.Cantidad);
                                else
                                {
                                    DataRow data = dtbInventarios.NewRow();
                                    data["IdLinea"] = producto.IdLinea;
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
                            movimientoBanco = new MovimientoBanco
                            {
                                IdSucursal = factura.IdSucursal
                            };
                            CuentaBanco cuentaBanco = dbContext.CuentaBancoRepository.Find(desglosePago.IdCuentaBanco);
                            if (cuentaBanco == null)
                                throw new BusinessException("La cuenta bancaria asignada al movimiento no existe.");
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
                                movimientoBanco.IdTipo = StaticTipoMovimientoBanco.DepositoEntrante;
                                movimientoBanco.Descripcion = "Registro de depósito bancario para pago de factura. ";
                            }
                            movimientoBanco.Numero = desglosePago.TipoTarjeta;
                            movimientoBanco.Beneficiario = empresa.NombreEmpresa;
                            movimientoBanco.Monto = desglosePago.MontoLocal;
                            IBancaService servicioAuxiliarBancario = new BancaService(_logger);
                            servicioAuxiliarBancario.AgregarMovimientoBanco(movimientoBanco, dbContext);
                        }
                    }
                    if (empresa.Contabiliza)
                    {
                        decimal decTotalDiff = decTotalIngresosMercancia + decTotalImpuesto + decTotalIngresosServicios - factura.Total;
                        if (decTotalDiff != 0)
                        {
                            if (decTotalDiff >= 1 || decTotalDiff <= -1)
                                throw new BusinessException("La diferencia de ajuste sobrepasa el valor permitido.");
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
                                    ParametroContable bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
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
                                lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoParametroContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
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
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger);
                        servicioContabilidad.AgregarAsiento(asiento, dbContext);
                    }
                    DocumentoElectronico documentoFE = null;
                    if (!empresa.RegimenSimplificado)
                    {
                        if (factura.IdCliente > 1)
                            documentoFE = ComprobanteElectronicoService.GenerarFacturaElectronica(factura, empresa, cliente, dbContext, decTipoDeCambio);
                        else
                            documentoFE = ComprobanteElectronicoService.GeneraTiqueteElectronico(factura, empresa, cliente, dbContext, decTipoDeCambio);
                        factura.IdDocElectronico = documentoFE.ClaveNumerica;
                    }
                    dbContext.Commit();
                    if (cuentaPorCobrar != null)
                    {
                        factura.IdCxC = cuentaPorCobrar.IdCxC;
                        dbContext.NotificarModificacion(factura);
                        cuentaPorCobrar.NroDocOrig = factura.IdFactura;
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
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoFE, datos));
                    }
                    return factura.IdFactura.ToString() + "-" + factura.ConsecFactura.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error guardando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarFacturaCompra(FacturaCompra facturaCompra, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                decimal decTipoDeCambio = 1;
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == facturaCompra.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.RegimenSimplificado) throw new BusinessException("La empresa se encuentra parametrizada dentro del regimen simplificado. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == facturaCompra.IdEmpresa && x.IdSucursal == facturaCompra.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == facturaCompra.IdEmpresa && x.IdSucursal == facturaCompra.IdSucursal && x.IdTerminal == facturaCompra.IdTerminal).FirstOrDefault();
                    if (terminal == null) throw new BusinessException("No se logró obtener la información de la terminal que envia la solicitud. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (facturaCompra.IdTipoMoneda == 2)
                    {
                        string criteria = facturaCompra.Fecha.ToString("dd/MM/yyyy");
                        TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                        if (tipoDeCambio == null) throw new BusinessException("El tipo de cambio para la fecha '" + criteria + "' no ha sido actualizado. Por favor consulte con su proveedor.");
                        decTipoDeCambio = tipoDeCambio.ValorTipoCambio;
                    }
                    facturaCompra.TipoDeCambioDolar = decTipoDeCambio;
                    foreach (var detalle in facturaCompra.DetalleFacturaCompra)
                    {
                        var clasificacion = dbContext.ClasificacionProductoRepository.Where(x => x.Id == detalle.Codigo).FirstOrDefault();
                        if (clasificacion == null) throw new BusinessException("El código " + detalle.Codigo + " asignado al detalle de la factura de compra no corresponde a un código CABYS válido.");
                    }
                    dbContext.FacturaCompraRepository.Add(facturaCompra);
                    DocumentoElectronico documentoFE = ComprobanteElectronicoService.GenerarFacturaCompraElectronica(facturaCompra, empresa, dbContext, decTipoDeCambio);
                    dbContext.Commit();
                    if (documentoFE != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoFE, datos));
                    }
                    return facturaCompra.IdFactCompra.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error guardando la información de la facturaCompra. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularFactura(int intIdFactura, int intIdUsuario, string strMotivoAnulacion, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    if (factura.Nulo) throw new BusinessException("La factura ya ha sido anulada.");
                    if (factura.Procesado) throw new BusinessException("La factura ya fue procesada en un cierre diario y no puede ser anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == factura.IdEmpresa && x.IdSucursal == factura.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.AsNoTracking().FirstOrDefault(x => x.IdFactura == factura.IdFactura && x.Nulo == false);
                    if (devolucion != null) throw new BusinessException("La factura posee un registro de devolución de mercancía y no puede ser anulada.");
                    Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                    factura.Nulo = true;
                    factura.IdAnuladoPor = intIdUsuario;
                    factura.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(factura);
                    foreach (var detalleFactura in factura.DetalleFactura)
                    {
                        Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalleFactura.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == factura.IdSucursal).FirstOrDefault();
                            if (existencias == null)
                                throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                            decimal cantPorAnular = detalleFactura.Cantidad - detalleFactura.CantDevuelto;
                            if (cantPorAnular > 0)
                            {
                                existencias.Cantidad += cantPorAnular;
                                dbContext.NotificarModificacion(existencias);
                                MovimientoProducto movimiento = new MovimientoProducto
                                {
                                    IdProducto = producto.IdProducto,
                                    IdSucursal = factura.IdSucursal,
                                    Fecha = DateTime.Now,
                                    Tipo = StaticTipoMovimientoProducto.Entrada,
                                    Origen = "Anulación de registro de facturación " + factura.ConsecFactura,
                                    Cantidad = cantPorAnular,
                                    PrecioCosto = detalleFactura.PrecioCosto
                                };
                                producto.MovimientoProducto = new List<MovimientoProducto>();
                                producto.MovimientoProducto.Add(movimiento);
                            }
                        }
                    }
                    if (factura.IdCxC > 0)
                    {
                        CuentaPorCobrar cuentaPorCobrar = dbContext.CuentaPorCobrarRepository.Find(factura.IdCxC);
                        if (cuentaPorCobrar == null)
                            throw new Exception("La cuenta por cobrar correspondiente a la factura no existe.");
                        if (cuentaPorCobrar.Total > cuentaPorCobrar.Saldo)
                            throw new BusinessException("El registro de facturación posee una cuenta por cobrar asociada con abonos procesados. No puede ser reversada.");
                        cuentaPorCobrar.Nulo = true;
                        cuentaPorCobrar.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(cuentaPorCobrar);
                    }
                    if (factura.IdMovBanco > 0)
                    {
                        IBancaService servicioAuxiliarBancario = new BancaService(_logger);
                        servicioAuxiliarBancario.AnularMovimientoBanco(factura.IdMovBanco, intIdUsuario, "Anulación de registro de factura " + factura.ConsecFactura, dbContext);
                    }
                    if (factura.IdAsiento > 0)
                    {
                        IContabilidadService servicioContabilidad = new ContabilidadService(_logger);
                        servicioContabilidad.ReversarAsientoContable(factura.IdAsiento, dbContext);
                    }
                    DocumentoElectronico documentoNC = null;
                    if (!empresa.RegimenSimplificado && factura.IdDocElectronico != null)
                    {
                        DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.FirstOrDefault(x => x.ClaveNumerica == factura.IdDocElectronico);
                        if (documento == null)
                            throw new BusinessException("El documento electrónico relacionado con la factura no existe.");
                        if (documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Aceptado && documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            throw new BusinessException("El documento electrónico de la factura no ha sido procesado. No se puede proceder con la anulación en este momento");
                        }
                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado)
                        {
                            DateTime fechaDocumento = DateTime.UtcNow.AddHours(-6);
                            string criteria = fechaDocumento.ToString("dd/MM/yyyy");
                            TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                            if (tipoDeCambio == null) throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                            documentoNC = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronica(factura, empresa, cliente, dbContext, tipoDeCambio.ValorTipoCambio);
                            factura.IdDocElectronicoRev = documentoNC.ClaveNumerica;
                        }
                    }
                    dbContext.Commit();
                    if (documentoNC != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoNC, datos));
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
                    _logger.LogError("Error al anular el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public Factura ObtenerFactura(int intIdFactura)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    foreach (DesglosePagoFactura desglosePago in factura.DesglosePagoFactura)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.AsNoTracking().Where(x => x.IdBanco == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                        else
                        {
                            CuentaBanco banco = dbContext.CuentaBancoRepository.AsNoTracking().Where(x => x.IdCuenta == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                    }
                    return factura;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int intIdFactura, string strNombre, string strIdentificacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.FacturaRepository.Include("Cliente").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdFactura > 0)
                        listado = listado.Where(x => x.ConsecFactura == intIdFactura);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    if (!strIdentificacion.Equals(string.Empty))
                        listado = listado.Where(x => x.Cliente.Identificacion.Contains(strIdentificacion));
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<FacturaDetalle> ObtenerListadoFacturas(int intIdEmpresa, int intIdSucursal, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdFactura, string strNombre, string strIdentificacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaFactura = new List<FacturaDetalle>();
                try
                {
                    var listado = dbContext.FacturaRepository.Include("Cliente").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdFactura > 0)
                        listado = listado.Where(x => x.ConsecFactura == intIdFactura);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    if (!strIdentificacion.Equals(string.Empty))
                        listado = listado.Where(x => x.Cliente.Identificacion.Contains(strIdentificacion));
                    listado = listado.OrderByDescending(x => x.IdFactura).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var factura in listado)
                    {
                        string strEstado = factura.IdDocElectronicoRev != null ? "Anulando" : "Activa";
                        FacturaDetalle item = new FacturaDetalle(factura.IdFactura, factura.ConsecFactura, factura.NombreCliente, factura.Cliente.Identificacion, factura.Fecha.ToString("dd/MM/yyyy"), factura.Gravado, factura.Exonerado, factura.Excento, factura.Impuesto, factura.Total, 0, strEstado, "", factura.Nulo);
                        listaFactura.Add(item);
                    }
                    return listaFactura;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarProforma(Proforma proforma)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == proforma.IdEmpresa && x.IdSucursal == proforma.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.RegimenSimplificado)
                    {
                        foreach (DetalleProforma detalle in proforma.DetalleProforma)
                        {
                            Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalle.IdProducto);
                            if (producto == null)
                                throw new BusinessException("El producto con código " + producto.Codigo + " asignado al detalle de la proforma no existe.");
                            if (producto.CodigoClasificacion == "")
                                throw new BusinessException("El producto con código " + producto.Codigo + " asignado al detalle de la proforma no posee clasificación CABYS.");
                        }
                    }
                    sucursal.ConsecProforma += 1;
                    dbContext.NotificarModificacion(sucursal);
                    proforma.ConsecProforma = sucursal.ConsecProforma;
                    dbContext.ProformaRepository.Add(proforma);
                    dbContext.Commit();
                    return proforma.IdProforma.ToString() + "-" + proforma.ConsecProforma.ToString();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarProforma(Proforma proforma)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    Proforma ordenNoTracking = dbContext.ProformaRepository.AsNoTracking().Where(x => x.IdProforma == proforma.IdProforma).FirstOrDefault();
                    if (ordenNoTracking != null && ordenNoTracking.Aplicado) throw new BusinessException("La proforma no puede ser modificada porque ya fue facturada.");
                    proforma.Vendedor = null;
                    List<DetalleProforma> listadoDetalleAnterior = dbContext.DetalleProformaRepository.Where(x => x.IdProforma == proforma.IdProforma).ToList();
                    List<DetalleProforma> listadoDetalle = proforma.DetalleProforma.ToList();
                    proforma.DetalleProforma = null;
                    foreach (DetalleProforma detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(proforma);
                    foreach (DetalleProforma detalle in listadoDetalle)
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
                    _logger.LogError("Error al actualizar el registro de proforma: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularProforma(int intIdProforma, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Proforma proforma = dbContext.ProformaRepository.Find(intIdProforma);
                    if (proforma == null) throw new Exception("La proforma por anular no existe.");
                    if (proforma.Nulo) throw new BusinessException("La proforma ya ha sido anulada.");
                    if (proforma.Aplicado) throw new BusinessException("La proforma no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(proforma.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    proforma.Nulo = true;
                    proforma.IdAnuladoPor = intIdUsuario;
                    proforma.MotivoAnulacion = strMotivoAnulacion;
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
                    _logger.LogError("Error al anular el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public Proforma ObtenerProforma(int intIdProforma)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Proforma proforma = dbContext.ProformaRepository.Include("Cliente").Include("Vendedor").Include("DetalleProforma.Producto").FirstOrDefault(x => x.IdProforma == intIdProforma);
                    foreach (DetalleProforma detalle in proforma.DetalleProforma)
                        detalle.Codigo = detalle.Producto.Codigo;
                    return proforma;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdProforma, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.ProformaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdProforma > 0)
                        listado = listado.Where(x => x.ConsecProforma == intIdProforma);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<FacturaDetalle> ObtenerListadoProformas(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdProforma, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaProforma = new List<FacturaDetalle>();
                try
                {
                    var listado = dbContext.ProformaRepository.Include("Cliente").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdProforma > 0)
                        listado = listado.Where(x => x.ConsecProforma == intIdProforma);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    var proformas = listado.OrderByDescending(x => x.IdProforma).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var proforma in proformas)
                    {
                        string strEstado = "Activa";
                        FacturaDetalle item = new FacturaDetalle(proforma.IdProforma, proforma.ConsecProforma, proforma.NombreCliente, proforma.Cliente.Identificacion, proforma.Fecha.ToString("dd/MM/yyyy"), proforma.Gravado, proforma.Exonerado, proforma.Excento, proforma.Impuesto, proforma.Total, 0, strEstado, "", proforma.Nulo);
                        listaProforma.Add(item);
                    }
                    return listaProforma;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de proforma de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de proformas. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarApartado(Apartado apartado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(apartado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == apartado.IdEmpresa && x.IdSucursal == apartado.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    sucursal.ConsecApartado += 1;
                    dbContext.NotificarModificacion(sucursal);
                    apartado.ConsecApartado = sucursal.ConsecApartado;
                    dbContext.ApartadoRepository.Add(apartado);
                    dbContext.Commit();
                    return apartado.IdApartado.ToString() + "-" + apartado.ConsecApartado.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de apartado de facturación: ", ex);
                    throw new Exception("Se produjo un error agregando la información del apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AnularApartado(int intIdApartado, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Apartado apartado = dbContext.ApartadoRepository.Find(intIdApartado);
                    if (apartado == null) throw new Exception("El apartado por anular no existe.");
                    if (apartado.Nulo) throw new BusinessException("El apartado ya ha sido anulado.");
                    if (apartado.Aplicado) throw new BusinessException("El apartado no puede ser anulado porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(apartado.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == apartado.IdEmpresa && x.IdSucursal == apartado.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    apartado.Nulo = true;
                    apartado.IdAnuladoPor = intIdUsuario;
                    apartado.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(apartado);
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
                    _logger.LogError("Error al anular el registro de apartado de facturación: ", ex);
                    throw new Exception("Se produjo un error anulando el apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public Apartado ObtenerApartado(int intIdApartado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Apartado apartado = dbContext.ApartadoRepository.Include("Cliente").Include("Vendedor").Include("DetalleApartado.Producto").Include("DesglosePagoApartado").FirstOrDefault(x => x.IdApartado == intIdApartado);
                    foreach (DesglosePagoApartado desglosePago in apartado.DesglosePagoApartado)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.AsNoTracking().Where(x => x.IdBanco == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                        else
                        {
                            CuentaBanco banco = dbContext.CuentaBancoRepository.AsNoTracking().Where(x => x.IdCuenta == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                    }
                    return apartado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de apartado de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información del apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdApartado, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaApartados = dbContext.ApartadoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listaApartados = listaApartados.Where(x => !x.Nulo);
                    if (intIdApartado > 0)
                        listaApartados = listaApartados.Where(x => x.ConsecApartado == intIdApartado);
                    if (!strNombre.Equals(string.Empty))
                        listaApartados = listaApartados.Where(x => x.NombreCliente.Contains(strNombre));
                    return listaApartados.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de apartados: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de apartados. Por favor consulte con su proveedor.");
                }
            }
        }
        
        public IList<FacturaDetalle> ObtenerListadoApartados(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdApartado, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaApartado = new List<FacturaDetalle>();
                try
                {
                    var listado = dbContext.ApartadoRepository.Include("Cliente").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdApartado > 0)
                        listado = listado.Where(x => x.ConsecApartado == intIdApartado);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    listado = listado.OrderByDescending(x => x.IdApartado).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var apartado in listado)
                    {
                        string strEstado = "Activa";
                        FacturaDetalle item = new FacturaDetalle(apartado.IdApartado, apartado.ConsecApartado, apartado.NombreCliente, apartado.Cliente.Identificacion, apartado.Fecha.ToString("dd/MM/yyyy"), apartado.Gravado, apartado.Exonerado, apartado.Excento, apartado.Impuesto, apartado.Total, apartado.Total - apartado.MontoAdelanto, strEstado, "", apartado.Nulo);
                        listaApartado.Add(item);
                    }
                    return listaApartado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de apartados: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de apartados. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarOrdenServicio(OrdenServicio ordenServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ordenServicio.IdEmpresa && x.IdSucursal == ordenServicio.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    sucursal.ConsecOrdenServicio += 1;
                    dbContext.NotificarModificacion(sucursal);
                    ordenServicio.ConsecOrdenServicio = sucursal.ConsecOrdenServicio;
                    dbContext.OrdenServicioRepository.Add(ordenServicio);
                    if (empresa.Modalidad == StaticModalidadEmpresa.Restaurante)
                    {
                        AgregarTiqueteOrdenServicio(ordenServicio, ordenServicio.DetalleOrdenServicio);
                    }
                    dbContext.Commit();
                    return ordenServicio.IdOrden.ToString() + "-" + ordenServicio.ConsecOrdenServicio.ToString();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el registro de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarOrdenServicio(OrdenServicio ordenServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    OrdenServicio ordenNoTracking = dbContext.OrdenServicioRepository.AsNoTracking().Where(x => x.IdOrden == ordenServicio.IdOrden).FirstOrDefault();
                    if (ordenNoTracking != null && ordenNoTracking.Aplicado) throw new BusinessException("La orden de servicio no puede ser modificada porque ya fue facturada.");
                    ordenServicio.Vendedor = null;
                    if (ordenServicio.MontoAdelanto != ordenNoTracking.MontoAdelanto) ordenServicio.MontoAdelanto = ordenNoTracking.MontoAdelanto;
                    List<DetalleOrdenServicio> listadoDetalleAnterior = dbContext.DetalleOrdenServicioRepository.Where(x => x.IdOrden == ordenServicio.IdOrden).ToList();
                    List<DetalleOrdenServicio> listadoDetalle = ordenServicio.DetalleOrdenServicio.ToList();
                    if (empresa.Modalidad == StaticModalidadEmpresa.Restaurante)
                    {
                        List<DetalleOrdenServicio> nuevoDetalle = new List<DetalleOrdenServicio> { };
                        foreach (DetalleOrdenServicio detalle in listadoDetalle)
                        {
                            DetalleOrdenServicio anterior = listadoDetalleAnterior.Where(x => x.IdOrden == detalle.IdOrden && x.IdProducto == detalle.IdProducto).FirstOrDefault();
                            if (anterior != null)
                            {
                                if (anterior.Cantidad != detalle.Cantidad)
                                {
                                    nuevoDetalle.Add(new DetalleOrdenServicio
                                    {
                                        IdProducto = detalle.IdProducto,
                                        Cantidad = detalle.Cantidad - anterior.Cantidad,
                                        Descripcion = detalle.Descripcion
                                    });
                                }
                            }
                            else
                            {
                                nuevoDetalle.Add(detalle);
                            }
                        }
                        if (nuevoDetalle.Count > 0) AgregarTiqueteOrdenServicio(ordenServicio, nuevoDetalle);
                    }
                    ordenServicio.DetalleOrdenServicio = null;
                    foreach (DetalleOrdenServicio detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(ordenServicio);
                    foreach (DetalleOrdenServicio detalle in listadoDetalle)
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
                    _logger.LogError("Error al actualizar el registro de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        private void AgregarTiqueteOrdenServicio(OrdenServicio ordenServicio, ICollection<DetalleOrdenServicio> detalleOrdenServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                DataTable dtbDetalleTiquete = new DataTable();
                dtbDetalleTiquete.Columns.Add("Linea", typeof(string));
                dtbDetalleTiquete.Columns.Add("Descripcion", typeof(string));
                dtbDetalleTiquete.Columns.Add("Cantidad", typeof(string));
                foreach (DetalleOrdenServicio detalle in detalleOrdenServicio)
                {
                    Producto producto = dbContext.ProductoRepository.Include("Linea").AsNoTracking().FirstOrDefault(x => x.IdProducto == detalle.IdProducto);
                    DataRow data = dtbDetalleTiquete.NewRow();
                    data["Linea"] = producto.Linea.Descripcion;
                    data["Descripcion"] = detalle.Descripcion;
                    data["Cantidad"] = detalle.Cantidad.ToString();
                    dtbDetalleTiquete.Rows.Add(data);
                }
                DataView view = new DataView(dtbDetalleTiquete);
                view.Sort = "Linea ASC";
                string strLineaDesc = view[0].Row["Linea"].ToString();
                IList<ClsLineaImpresion> lineasDetalle = new List<ClsLineaImpresion> { };
                IList<ClsLineaImpresion> lineas = new List<ClsLineaImpresion> { };
                TiqueteOrdenServicio tiquete;
                foreach (DataRowView row in view)
                {
                    if (strLineaDesc != row["Linea"].ToString())
                    {
                        tiquete = GenerarTiqueteOrdenServicio(ordenServicio, lineasDetalle, strLineaDesc);
                        dbContext.TiqueteOrdenServicioRepository.Add(tiquete);
                        lineasDetalle = new List<ClsLineaImpresion> { };
                        strLineaDesc = row["Linea"].ToString();
                    }
                    string strLinea = row["Descripcion"].ToString();
                    lineasDetalle.Add(new ClsLineaImpresion(0, strLinea.Substring(0, Math.Min(30, strLinea.Length)), 0, 95, 10, (int)StringAlignment.Near, false));
                    lineasDetalle.Add(new ClsLineaImpresion(1, row["Cantidad"].ToString(), 95, 5, 10, (int)StringAlignment.Far, false));
                    strLinea = strLinea.Substring(Math.Min(30, strLinea.Length));
                    while (strLinea.Length > 30)
                    {
                        lineasDetalle.Add(new ClsLineaImpresion(1, strLinea.Substring(0, 30), 0, 100, 10, (int)StringAlignment.Near, false));
                        strLinea = strLinea.Substring(30);
                    }
                    if (strLinea.Length > 0) lineasDetalle.Add(new ClsLineaImpresion(1, strLinea, 0, 100, 10, (int)StringAlignment.Near, false));
                }
                tiquete = GenerarTiqueteOrdenServicio(ordenServicio, lineasDetalle, strLineaDesc);
                dbContext.TiqueteOrdenServicioRepository.Add(tiquete);
            }
        }

        private TiqueteOrdenServicio GenerarTiqueteOrdenServicio(OrdenServicio ordenServicio, IList<ClsLineaImpresion> lineasDetalle, string strLineaDesc)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                List<ClsLineaImpresion> lineas = new List<ClsLineaImpresion> { };
                lineas.Add(new ClsLineaImpresion(2, "PEDIDO EN PROCESO", 0, 100, 14, (int)StringAlignment.Center, true));
                lineas.Add(new ClsLineaImpresion(2, DateTime.Now.ToString(), 0, 100, 12, (int)StringAlignment.Center, false));
                lineas.Add(new ClsLineaImpresion(2, ordenServicio.NombreCliente, 0, 100, 14, (int)StringAlignment.Center, true));
                lineas.Add(new ClsLineaImpresion(1, "DETALLE DE ORDEN", 0, 100, 12, (int)StringAlignment.Center, false));
                foreach (ClsLineaImpresion linea in lineasDetalle)
                    lineas.Add(linea);
                lineas.Add(new ClsLineaImpresion(2, "", 0, 100, 10, (int)StringAlignment.Near, false));
                byte[] bytLineas = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lineas));
                return new TiqueteOrdenServicio
                {
                    IdTiquete = 0,
                    IdEmpresa = ordenServicio.IdEmpresa,
                    IdSucursal = ordenServicio.IdSucursal,
                    Descripcion = ordenServicio.NombreCliente,
                    Impresora = strLineaDesc,
                    Lineas = bytLineas,
                    Impreso = false
                };
            }
        }

        public void AnularOrdenServicio(int intIdOrdenServicio, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Find(intIdOrdenServicio);
                    if (ordenServicio == null) throw new Exception("La orden de servicio por anular no existe.");
                    if (ordenServicio.Nulo) throw new BusinessException("La orden de servicio ya ha sido anulada.");
                    if (ordenServicio.Aplicado) throw new BusinessException("La orden de servicio no puede ser anulada porque ya fue facturada.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ordenServicio.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ordenServicio.IdEmpresa && x.IdSucursal == ordenServicio.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    ordenServicio.Nulo = true;
                    ordenServicio.IdAnuladoPor = intIdUsuario;
                    ordenServicio.MotivoAnulacion = strMotivoAnulacion;
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
                    _logger.LogError("Error al anular el registro de la orden de servicio: ", ex);
                    throw new Exception("Se produjo un error anulando la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public OrdenServicio ObtenerOrdenServicio(int intIdOrdenServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Include("Cliente").Include("Vendedor").Include("DetalleOrdenServicio.Producto").Include("DesglosePagoOrdenServicio").FirstOrDefault(x => x.IdOrden == intIdOrdenServicio);
                    foreach (DetalleOrdenServicio detalle in ordenServicio.DetalleOrdenServicio)
                        detalle.Codigo = detalle.Producto.Codigo;
                    foreach (DesglosePagoOrdenServicio desglosePago in ordenServicio.DesglosePagoOrdenServicio)
                    {
                        if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                        {
                            BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.AsNoTracking().Where(x => x.IdBanco == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                        else
                        {
                            CuentaBanco banco = dbContext.CuentaBancoRepository.AsNoTracking().Where(x => x.IdCuenta == desglosePago.IdCuentaBanco).FirstOrDefault();
                            if (banco != null)
                                desglosePago.DescripcionCuenta = banco.Descripcion;
                            else
                                desglosePago.DescripcionCuenta = "NO INFORMATION AVAILABLE";
                        }
                    }
                    return ordenServicio;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int intIdOrdenServicio, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.OrdenServicioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdOrdenServicio > 0)
                        listado = listado.Where(x => x.ConsecOrdenServicio == intIdOrdenServicio);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de ordenes de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de ordenes de serviciov. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<FacturaDetalle> ObtenerListadoOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolAplicado, bool bolIncluyeNulos, int numPagina, int cantRec, int intIdOrdenServicio, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaOrdenServicio = new List<FacturaDetalle>();
                try
                {
                    var listado = dbContext.OrdenServicioRepository.Include("Cliente").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Aplicado == bolAplicado);
                    if (!bolIncluyeNulos)
                        listado = listado.Where(x => !x.Nulo);
                    if (intIdOrdenServicio > 0)
                        listado = listado.Where(x => x.ConsecOrdenServicio == intIdOrdenServicio);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreCliente.Contains(strNombre));
                    listado = listado.OrderByDescending(x => x.IdOrden).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var ordenServicio in listado)
                    {
                        string strEstado = "Activa";
                        FacturaDetalle item = new FacturaDetalle(ordenServicio.IdOrden, ordenServicio.ConsecOrdenServicio, ordenServicio.NombreCliente, ordenServicio.Cliente.Identificacion, ordenServicio.Fecha.ToString("dd/MM/yyyy"), ordenServicio.Gravado, 0, ordenServicio.Excento, ordenServicio.Impuesto, ordenServicio.Total, ordenServicio.Total - ordenServicio.MontoAdelanto, strEstado, ordenServicio.Descripcion, ordenServicio.Nulo);
                        listaOrdenServicio.Add(item);
                    }
                    return listaOrdenServicio;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de ordenes de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de ordenes de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<ClsTiquete> ObtenerListadoTiqueteOrdenServicio(int intIdEmpresa, int intIdSucursal, bool bolImpreso, bool bolSortedDesc)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaTiquete = new List<ClsTiquete>();
                try
                {
                    var listado = dbContext.TiqueteOrdenServicioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.Impreso == bolImpreso);
                    if (bolSortedDesc)
                        listado = listado.OrderByDescending(x => x.IdTiquete);
                    else
                        listado = listado.OrderBy(x => x.IdTiquete);
                    foreach (var tiquete in listado)
                    {
                        string strLineas = Encoding.UTF8.GetString(tiquete.Lineas);
                        IList<ClsLineaImpresion> lineas = JsonConvert.DeserializeObject<List<ClsLineaImpresion>>(strLineas);
                        ClsTiquete item = new ClsTiquete(tiquete.IdTiquete, tiquete.IdEmpresa, tiquete.IdSucursal, tiquete.Descripcion, tiquete.Impresora, lineas, tiquete.Impreso);
                        listaTiquete.Add(item);
                    }
                    return listaTiquete;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de tiquetes de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tiquetes de orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarEstadoTiqueteOrdenServicio(int intIdTiquete, bool bolEstado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    TiqueteOrdenServicio tiquete = dbContext.TiqueteOrdenServicioRepository.Where(x => x.IdTiquete == intIdTiquete).FirstOrDefault();
                    if (tiquete == null) throw new Exception("No se logró obtener la información del tiquete de orden de servicio. Por favor, pongase en contacto con su proveedor del servicio.");
                    tiquete.Impreso = bolEstado;
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el registro de tiquete de orden de servicio: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del tiquete de orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public string AgregarDevolucionCliente(DevolucionCliente devolucion, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("DetalleFactura").FirstOrDefault(x => x.IdFactura == devolucion.IdFactura);
                    if (factura == null) throw new Exception("La factura asignada a la devolución no existe.");
                    if (factura.Nulo) throw new BusinessException("La factura asingada a la devolución ya ha sido anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == devolucion.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == factura.IdEmpresa && x.IdSucursal == factura.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    devolucion.IdAsiento = 0;
                    devolucion.IdSucursal = factura.IdSucursal;
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionCliente)
                    {
                        Producto producto = dbContext.ProductoRepository.FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        detalleDevolucion.Producto = producto;
                        DetalleFactura detalleFactura = dbContext.DetalleFacturaRepository.Where(x => x.IdFactura == factura.IdFactura && x.IdProducto == detalleDevolucion.IdProducto).FirstOrDefault();
                        if (detalleFactura == null)
                            throw new BusinessException("El producto " + producto.IdProducto + " no posee registro en el detalle de la factura con id " + factura.IdFactura + ". Por favor consulte con su proveedor.");
                        detalleFactura.CantDevuelto += detalleDevolucion.Cantidad;
                        dbContext.NotificarModificacion(detalleFactura);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == factura.IdSucursal).FirstOrDefault();
                            if (existencias == null)
                                throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                            existencias.Cantidad += detalleDevolucion.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                            MovimientoProducto movimientoProducto = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = factura.IdSucursal,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Entrada,
                                Origen = "Registro de devolución de mercancía de clientes sobre factura " + factura.ConsecFactura,
                                Cantidad = detalleDevolucion.Cantidad,
                                PrecioCosto = detalleDevolucion.PrecioCosto
                            };
                            producto.MovimientoProducto = new List<MovimientoProducto>();
                            producto.MovimientoProducto.Add(movimientoProducto);
                        }
                    }
                    MovimientoCuentaPorCobrar mov = null;
                    if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                    {
                        BancoAdquiriente cuentaBanco = dbContext.BancoAdquirienteRepository.FirstOrDefault(x => x.IdEmpresa == devolucion.IdEmpresa);
                        if (cuentaBanco == null) throw new BusinessException("La empresa no posee ningun banco adquiriente parametrizado");
                        CuentaPorCobrar cxc = dbContext.CuentaPorCobrarRepository.Find(factura.IdCxC);
                        if (cxc == null) throw new BusinessException("La cuenta por cobrar asignada a la factura de la devolución no existe");
                        mov = new MovimientoCuentaPorCobrar
                        {
                            IdEmpresa = devolucion.IdEmpresa,
                            IdUsuario = devolucion.IdUsuario,
                            IdPropietario = factura.IdCliente,
                            IdSucursal = devolucion.IdSucursal,
                            Tipo = StaticTipoAbono.AbonoEfectivo,
                            IdCxC = factura.IdCxC,
                            Observaciones = "Abono por devolución de mercancía",
                            Monto = devolucion.Total,
                            SaldoActual = cxc.Saldo,
                            Fecha = DateTime.Now
                        };
                        DesglosePagoMovimientoCuentaPorCobrar desglosePagoMovimiento = new DesglosePagoMovimientoCuentaPorCobrar
                        {
                            IdFormaPago = StaticFormaPago.TransferenciaDepositoBancario,
                            IdCuentaBanco = cuentaBanco.IdBanco,
                            TipoTarjeta = "",
                            NroMovimiento = "",
                            IdTipoMoneda = factura.IdTipoMoneda,
                            MontoLocal = devolucion.Total,
                            TipoDeCambio = factura.TipoDeCambioDolar
                        };
                        mov.DesglosePagoMovimientoCuentaPorCobrar = new List<DesglosePagoMovimientoCuentaPorCobrar>();
                        mov.DesglosePagoMovimientoCuentaPorCobrar.Add(desglosePagoMovimiento);
                        dbContext.MovimientoCuentaPorCobrarRepository.Add(mov);
                        cxc.Saldo -= devolucion.Total;
                        dbContext.NotificarModificacion(cxc);
                    }
                    else
                    {
                        CuentaEgreso cuenta = dbContext.CuentaEgresoRepository.FirstOrDefault(x => x.IdEmpresa == devolucion.IdEmpresa && x.Descripcion.ToUpper().Contains("DEVOLUCION"));
                        if (cuenta == null) throw new BusinessException("La empresa no posee ninguna cuenta de egresos parametrizada para devoluciones de clientes");
                        Egreso egreso = new Egreso
                        {
                            IdEmpresa = devolucion.IdEmpresa,
                            IdSucursal = devolucion.IdSucursal,
                            IdUsuario = devolucion.IdUsuario,
                            Fecha = DateTime.Now,
                            IdCuenta = cuenta.IdCuenta,
                            Beneficiario = factura.NombreCliente,
                            Detalle = "Devolución de mercancías de factura " + factura.ConsecFactura,
                            Monto = devolucion.Total,
                            Nulo = false,
                            Procesado = false
                        };
                        dbContext.EgresoRepository.Add(egreso);
                    }
                    DocumentoElectronico documentoNC = null;
                    if (!empresa.RegimenSimplificado && factura.IdDocElectronico != null)
                    {
                        Cliente cliente = dbContext.ClienteRepository.Find(factura.IdCliente);
                        DateTime fechaDocumento = DateTime.UtcNow.AddHours(-6);
                        string criteria = fechaDocumento.ToString("dd/MM/yyyy");
                        TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                        if (tipoDeCambio != null)
                        {
                            documentoNC = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronicaParcial(devolucion, factura, empresa, cliente, dbContext, tipoDeCambio.ValorTipoCambio, factura.IdDocElectronico);
                            devolucion.IdDocElectronico = documentoNC.ClaveNumerica;
                        }
                        else
                        {
                            throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                        }
                    }
                    dbContext.DevolucionClienteRepository.Add(devolucion);
                    dbContext.Commit();
                    if (mov != null)
                    {
                        devolucion.IdMovimientoCxC = mov.IdMovCxC;
                        dbContext.NotificarModificacion(devolucion);
                        dbContext.Commit();
                    }
                    if (documentoNC != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoNC, datos));
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
                    _logger.LogError("Error al agregar el registro de devolución: ", ex.InnerException);
                    throw new Exception("Se produjo un error agregando la información de la devolución. Por favor consulte con su proveedor.");
                }
                return devolucion.IdDevolucion.ToString();
            }
        }

        public void AnularDevolucionCliente(int intIdDevolucion, int intIdUsuario, string strMotivoAnulacion, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.Include("Cliente").Include("DetalleDevolucionCliente").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                    if (devolucion == null) throw new Exception("La devolución por anular no existe.");
                    if (devolucion.Nulo) throw new Exception("La devolución ya ha sido anulada.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == devolucion.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (devolucion.Procesado) throw new BusinessException("El registro ya fue procesado por el cierre. No es posible registrar la transacción.");
                    Factura factura = dbContext.FacturaRepository.AsNoTracking().Include("DetalleFactura").FirstOrDefault(x => x.IdFactura == devolucion.IdFactura);
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == factura.IdEmpresa && x.IdSucursal == factura.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    if (factura == null) throw new Exception("La factura asignada a la devolución no existe.");
                    if (factura.Nulo) throw new BusinessException("La factura asingada a la devolución ya ha sido anulada.");
                    devolucion.Nulo = true;
                    devolucion.IdAnuladoPor = intIdUsuario;
                    devolucion.MotivoAnulacion = strMotivoAnulacion;
                    devolucion.IdSucursal = factura.IdSucursal;
                    foreach (var detalleDevolucion in devolucion.DetalleDevolucionCliente)
                    {
                        Producto producto = dbContext.ProductoRepository.FirstOrDefault(x => x.IdProducto == detalleDevolucion.IdProducto);
                        if (producto == null) throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Imagen == null) producto.Imagen = new byte[0];
                        DetalleFactura detalleFactura = dbContext.DetalleFacturaRepository.Where(x => x.IdFactura == factura.IdFactura && x.IdProducto == detalleDevolucion.IdProducto).FirstOrDefault();
                        if (detalleFactura == null)
                            throw new BusinessException("El producto " + producto.IdProducto + " no posee registro en el detalle de la factura con id " + factura.IdFactura + ". Por favor consulte con su proveedor.");
                        detalleFactura.CantDevuelto -= detalleDevolucion.Cantidad;
                        dbContext.NotificarModificacion(detalleFactura);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == factura.IdSucursal).FirstOrDefault();
                            if (existencias == null)
                                throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                            existencias.Cantidad -= detalleDevolucion.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                            MovimientoProducto movimientoProducto = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                IdSucursal = factura.IdSucursal,
                                Fecha = DateTime.Now,
                                Tipo = StaticTipoMovimientoProducto.Salida,
                                Origen = "Anulación de registro de devolución de mercancía del cliente de factura " + factura.ConsecFactura,
                                Cantidad = detalleDevolucion.Cantidad,
                                PrecioCosto = detalleDevolucion.PrecioCosto
                            };
                            producto.MovimientoProducto = new List<MovimientoProducto>();
                            producto.MovimientoProducto.Add(movimientoProducto);
                        }
                    }
                    if (devolucion.IdMovimientoCxC > 0)
                    {
                        MovimientoCuentaPorCobrar movimiento = dbContext.MovimientoCuentaPorCobrarRepository.FirstOrDefault(x => x.IdMovCxC == devolucion.IdMovimientoCxC);
                        if (movimiento == null)
                            throw new Exception("El movimiento de la cuenta por cobrar correspondiente a la devolución no existe.");
                        movimiento.Nulo = true;
                        movimiento.IdAnuladoPor = intIdUsuario;
                        dbContext.NotificarModificacion(movimiento);
                        CuentaPorCobrar cuentaPorCobrar = dbContext.CuentaPorCobrarRepository.Find(movimiento.IdCxC);
                        cuentaPorCobrar.Saldo += movimiento.Monto;
                        dbContext.NotificarModificacion(cuentaPorCobrar);
                    }
                    else
                    {
                        CuentaIngreso cuenta = dbContext.CuentaIngresoRepository.FirstOrDefault(x => x.IdEmpresa == devolucion.IdEmpresa && x.Descripcion.ToUpper().Contains("DEVOLUCION"));
                        if (cuenta == null) throw new BusinessException("La empresa no posee ninguna cuenta de ingresos parametrizada para devoluciones de clientes");
                        Ingreso ingreso = new Ingreso
                        {
                            IdEmpresa = devolucion.IdEmpresa,
                            IdSucursal = devolucion.IdSucursal,
                            IdUsuario = devolucion.IdUsuario,
                            Fecha = DateTime.Now,
                            IdCuenta = cuenta.IdCuenta,
                            RecibidoDe = devolucion.Cliente.Nombre,
                            Detalle = "Anulación de devolución de mercancías de factura " + factura.ConsecFactura,
                            Monto = devolucion.Total,
                            Nulo = false,
                            Procesado = false
                        };
                        dbContext.IngresoRepository.Add(ingreso);
                    }
                    DocumentoElectronico documentoND = null;
                    if (!empresa.RegimenSimplificado && devolucion.IdDocElectronico != null)
                    {
                        DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.FirstOrDefault(x => x.ClaveNumerica == devolucion.IdDocElectronico);
                        if (documento == null)
                            throw new BusinessException("El documento electrónico relacionado con la devolución no existe.");
                        if (documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Aceptado && documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            throw new BusinessException("El documento electrónico de la devolución no ha sido procesado. No se puede proceder con la anulación en este momento");
                        }
                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado)
                        {
                            DateTime fechaDocumento = DateTime.UtcNow.AddHours(-6);
                            string criteria = fechaDocumento.ToString("dd/MM/yyyy");
                            TipoDeCambioDolar tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                            if (tipoDeCambio != null)
                            {
                                documentoND = ComprobanteElectronicoService.GenerarNotaDeDebitoElectronicaParcial(devolucion, factura, empresa, devolucion.Cliente, dbContext, tipoDeCambio.ValorTipoCambio, devolucion.IdDocElectronico);
                                devolucion.IdDocElectronicoRev = documentoND.ClaveNumerica;
                            }
                            else
                            {
                                throw new BusinessException("El tipo de cambio para la fecha " + criteria + " no ha sido actualizado. Por favor consulte con su proveedor.");
                            }
                        }
                    }
                    dbContext.NotificarModificacion(devolucion);
                    dbContext.Commit();
                    if (documentoND != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoND, datos));
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
                    _logger.LogError("Error al anular el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error anulando la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public DevolucionCliente ObtenerDevolucionCliente(int intIdDevolucion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.Include("Cliente").Include("DetalleDevolucionCliente.Producto").FirstOrDefault(x => x.IdDevolucion == intIdDevolucion);
                    return devolucion;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de devolución: ", ex);
                    throw new Exception("Se produjo un error consultado la información de la devolución. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, int intIdDevolucion, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listaDevoluciones = dbContext.DevolucionClienteRepository.Include("Factura").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdDevolucion > 0)
                        listaDevoluciones = listaDevoluciones.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listaDevoluciones = listaDevoluciones.Where(x => x.Factura.NombreCliente.Contains(strNombre));
                    return listaDevoluciones.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<FacturaDetalle> ObtenerListadoDevolucionesPorCliente(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdDevolucion, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaDevoluciones = new List<FacturaDetalle>();
                try
                {
                    var listado = dbContext.DevolucionClienteRepository.Include("Cliente").Include("Factura").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdDevolucion > 0)
                        listado = listado.Where(x => x.IdDevolucion == intIdDevolucion);
                    else if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.Factura.NombreCliente.Contains(strNombre));
                    listado = listado.OrderByDescending(x => x.IdDevolucion).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var devolucion in listado)
                    {
                        string strEstado = "Activa";
                        FacturaDetalle item = new FacturaDetalle(devolucion.IdDevolucion, devolucion.IdDevolucion, devolucion.NombreCliente, devolucion.Cliente.Identificacion, devolucion.Fecha.ToString("dd/MM/yyyy"), devolucion.Gravado, 0, devolucion.Excento, devolucion.Impuesto, devolucion.Total, 0, strEstado, "", devolucion.Nulo);
                        listaDevoluciones.Add(item);
                    }
                    return listaDevoluciones;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de devolución: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de devoluciones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosPendientes()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    List<DocumentoElectronico> listado = dbContext.DocumentoElectronicoRepository.Where(x => x.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado).OrderBy(x => x.ClaveNumerica).ToList();
                    foreach (var value in listado)
                    {
                        string datosXml = "";
                        string strNombre = "";
                        decimal decTotal = 0;
                        if (value.EsMensajeReceptor == "S")
                            datosXml = Encoding.UTF8.GetString(value.DatosDocumentoOri);
                        else
                            datosXml = Encoding.UTF8.GetString(value.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (value.EsMensajeReceptor == "S")
                        {
                            strNombre = "DOCUMENTO NO POSEE EMISOR";
                            if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                                strNombre = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            strNombre = "CLIENTE DE CONTADO";
                            if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                                strNombre = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.IdTipoDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha.ToString("dd/MM/yyyy"), strNombre, value.EstadoEnvio, value.ErrorEnvio, decTotal, value.EsMensajeReceptor, value.Reprocesado, value.CorreoNotificacion);
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
                    _logger.LogError("Error al obtener el listado de documentos electrónicos pendientes: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosEnProceso(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    List<DocumentoElectronico> listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)).ToList();
                    foreach (var value in listado)
                    {
                        string datosXml = "";
                        string strNombre = "";
                        decimal decTotal = 0;
                        if (value.EsMensajeReceptor == "S")
                            datosXml = Encoding.UTF8.GetString(value.DatosDocumentoOri);
                        else
                            datosXml = Encoding.UTF8.GetString(value.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (value.EsMensajeReceptor == "S")
                        {
                            strNombre = "DOCUMENTO NO POSEE EMISOR";
                            if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                                strNombre = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            strNombre = "CLIENTE DE CONTADO";
                            if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                                strNombre = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.IdTipoDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha.ToString("dd/MM/yyyy"), strNombre, value.EstadoEnvio, value.ErrorEnvio, decTotal, value.EsMensajeReceptor, value.Reprocesado, value.CorreoNotificacion);
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
                    _logger.LogError("Error al obtener el listado de documentos electrónicos pendientes: ", ex);
                    throw new Exception("Se produjo un error al obtener el listado de documentos electrónicos pendientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarDocumentosElectronicosPendientes(ConfiguracionGeneral datos, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var stringBuilder = new StringBuilder();
                try
                {
                    ParametroSistema procesando = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 2).FirstOrDefault();
                    if (procesando != null)
                    {
                        DateTime lastProcessAt = DateTime.ParseExact(procesando.Valor, "dd-MM-yyyy HH:mm:ss", null);
                        if (lastProcessAt.AddMinutes(2.00) < DateTime.Now)
                        {
                            procesando.Valor = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
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
                                    CredencialesHacienda credenciales = dbContext.CredencialesHaciendaRepository.Find(empresa.IdEmpresa);
                                    if (credenciales != null)
                                    {
                                        if (documento.DatosDocumentoOri == null) documento.DatosDocumentoOri = new byte[0];
                                        if (documento.Respuesta == null) documento.Respuesta = new byte[0];
                                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Registrado)
                                        {
                                            EnviarDocumentoElectronico(empresa.IdEmpresa, documento, datos).Wait();
                                        }
                                        else if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Enviado)
                                        {
                                            DocumentoElectronico estadoDoc = null;
                                            try
                                            {
                                                estadoDoc = ComprobanteElectronicoService.ConsultarDocumentoElectronico(credenciales, documento, dbContext, datos).Result;
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
                                                        ProcesarRespuestaHacienda(dbContext, respuesta, datos.CorreoNotificacionErrores, bytLogo);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                                        stringBuilder.AppendLine("Error al procesar la respuesta del documento electrónico con id: " + documento.IdDocumento + " Detalle: " + strError);
                                                    }
                                                }
                                                else
                                                {
                                                    dbContext.NotificarModificacion(estadoDoc);
                                                    dbContext.Commit();
                                                }
                                            }
                                            else
                                            {
                                                stringBuilder.AppendLine("Error al obtener el estado del documento electrónico en Hacienda");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        stringBuilder.AppendLine("Error al procesar el documento electrónico: No se encontraron los credenciales de hacienda para la empresa con id: " + documento.IdEmpresa);
                                    }
                                }
                                else
                                {
                                    stringBuilder.AppendLine("Error al procesar el documento electrónico: No se encontro la empresa con id: " + documento.IdEmpresa);
                                }
                            }
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
                    servicioCorreo.SendEmail(new string[] { datos.CorreoNotificacionErrores }, new string[] { }, "Excepción en la interface de procesamiento de documentos pendientes", "Adjunto el archivo con el detalle de los errores en procesamiento.", false, archivosJArray);
                }
            }
        }

        public void GenerarMensajeReceptor(string strDatos, int intIdEmpresa, int intSucursal, int intTerminal, int intEstado, bool bolIvaAplicable, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == intIdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    DocumentoElectronico documentoMR = ComprobanteElectronicoService.GeneraMensajeReceptor(strDatos, empresa, dbContext, intSucursal, intTerminal, intEstado, bolIvaAplicable);
                    dbContext.DocumentoElectronicoRepository.Add(documentoMR);
                    dbContext.Commit();
                    if (documentoMR != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoMR, datos));
                    }
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al procesar el mensaje receptor: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor intente más tarde.");
                    else
                        throw new Exception("Se produjo un error al procesar el mensaje receptor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarCorreoRecepcion(ConfiguracionGeneral config, ConfiguracionRecepcion datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var stringBuilder = new StringBuilder();
                ParametroSistema procesando = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 3).FirstOrDefault();
                if (procesando != null)
                {
                    DateTime lastProcessAt = DateTime.ParseExact(procesando.Valor, "dd-MM-yyyy HH:mm:ss", null);
                    if (lastProcessAt.AddMinutes(2.00) < DateTime.Now)
                    {
                        procesando.Valor = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        dbContext.Commit();
                        List<POPEmail> listadoCorreoAcreditable = new List<POPEmail>();
                        try
                        {
                            listadoCorreoAcreditable = servicioCorreo.ObtenerListadoMensaje(datos.CuentaIvaAcreditable, datos.ClaveIvaAcreditable).ToList();
                        }
                        catch (Exception ex)
                        {
                            string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                            stringBuilder.AppendLine("Error al obtener la lista de correo de gastos con iva acreditable. Detalle: " + strError);
                        }
                        foreach (POPEmail correo in listadoCorreoAcreditable)
                        {
                            try
                            {
                                ProcesarMensajeReceptor(dbContext, correo, config, true);
                                servicioCorreo.EliminarMensaje(datos.CuentaIvaAcreditable, datos.ClaveIvaAcreditable, correo.MessageNumber);
                            }
                            catch (BusinessException ex)
                            {
                                string strFrom = correo.From.ToString().Substring(correo.From.ToString().IndexOf("'") + 8);
                                strFrom = strFrom.Substring(0, strFrom.IndexOf("'"));
                                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                stringBuilder.AppendLine("Error al procesar el documento con IVA acreditable. Enviado por " + strFrom + " Asunto " + correo.Subject + ". Detalle: " + strError);
                                servicioCorreo.EliminarMensaje(datos.CuentaIvaAcreditable, datos.ClaveIvaAcreditable, correo.MessageNumber);
                                JArray archivosJArray = new JArray();
                                servicioCorreo.SendEmail(new string[] { strFrom }, new string[] { }, "Notificación de error en recepción de documento electrónico", "El correo del envio del documento electrónico con asunto " + correo.Subject + " presenta el siguiente detalle: " + ex.Message, false, archivosJArray);
                            }
                            catch (Exception ex)
                            {
                                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                stringBuilder.AppendLine("Error al procesar el documento con IVA acreditable con asunto " + correo.Subject + ". Detalle: " + strError);
                            }
                        }
                        List<POPEmail> listadoCorreoGasto = new List<POPEmail>();
                        try
                        {
                            listadoCorreoGasto = servicioCorreo.ObtenerListadoMensaje(datos.CuentaGastoNoAcreditable, datos.ClaveGastoNoAcreditable).ToList();
                        }
                        catch (Exception ex)
                        {
                            string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                            stringBuilder.AppendLine("Error al obtener la lista de correos de gastos con iva no acreditable. Detalle: " + strError);
                        }
                        foreach (POPEmail correo in listadoCorreoGasto)
                        {
                            try
                            {
                                ProcesarMensajeReceptor(dbContext, correo, config, false);
                                servicioCorreo.EliminarMensaje(datos.CuentaGastoNoAcreditable, datos.ClaveGastoNoAcreditable, correo.MessageNumber);
                            }
                            catch (BusinessException ex)
                            {
                                string strFrom = correo.From.ToString().Substring(correo.From.ToString().IndexOf("'") + 8);
                                strFrom = strFrom.Substring(0, strFrom.IndexOf("'"));
                                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                stringBuilder.AppendLine("Error al procesar el documento sin IVA acreditable. Enviado por " + strFrom + " Asunto " + correo.Subject + ". Detalle: " + strError);
                                servicioCorreo.EliminarMensaje(datos.CuentaGastoNoAcreditable, datos.ClaveGastoNoAcreditable, correo.MessageNumber);
                                JArray archivosJArray = new JArray();
                                servicioCorreo.SendEmail(new string[] { strFrom }, new string[] { }, "Notificación de error en recepción de documento electrónico", "El correo del envio del documento electrónico con asunto " + correo.Subject + " presenta el siguiente detalle: " + ex.Message, false, archivosJArray);
                            }
                            catch (Exception ex)
                            {
                                string strError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                                stringBuilder.AppendLine("Error al procesar el documento sin IVA acreditable con asunto " + correo.Subject + ". Detalle: " + strError);
                            }
                        }
                    }
                }
                if (stringBuilder.Length > 0)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                    stringBuilder = null;
                    JArray archivosJArray = new JArray();
                    JObject jobDatosAdjuntos1 = new JObject
                    {
                        ["nombre"] = "logRecepcion-" + DateTime.Now.ToString("ddMMyyyy-HH-mm-ss") + ".txt",
                        ["contenido"] = Convert.ToBase64String(bytes)
                    };
                    archivosJArray.Add(jobDatosAdjuntos1);
                    servicioCorreo.SendEmail(new string[] { config.CorreoNotificacionErrores }, new string[] { }, "Detalle de errores del procesamiento de recepción de documentos electrónicos", "Adjunto el archivo con el detalle del procesamiento.", false, archivosJArray);
                }
            }
        }

        void ProcesarMensajeReceptor(LeandroContext dbContext, POPEmail correo, ConfiguracionGeneral datos, bool bolIvaAplicable)
        {
            string strDatos = "";
            string strError = "";
            string strIdentificacion = "";
            if (correo.Attachments.Count == 0) throw new BusinessException("El correo no contiene ningún archivo adjunto para poder procesar el mensaje de recepción");
            XmlDocument documentoXml = new XmlDocument();
            foreach (Attachment archivo in correo.Attachments)
            {
                if (strDatos == "" && archivo.FileName.ToUpper().EndsWith(".XML"))
                {
                    string strXml = "";
                    try
                    {
                        strXml = Encoding.UTF8.GetString(archivo.Content);
                        documentoXml.LoadXml(strXml);
                    }
                    catch
                    {
                        try
                        {
                            strXml = Encoding.ASCII.GetString(archivo.Content);
                            strXml = strXml.Substring(strXml.IndexOf("<?xml"));
                            documentoXml.LoadXml(strXml);
                        }
                        catch (Exception)
                        {
                            strError = "El archivo XML del documento electrónico no se posee el formato adecuado para ser procesado";
                        }
                    }
                    if (documentoXml.DocumentElement.Name == "FacturaElectronica" || documentoXml.DocumentElement.Name == "NotaCreditoElectronica" || documentoXml.DocumentElement.Name == "NotaDebitoElectronica")
                    {
                        strError = "";
                        if (strXml.Contains("v4.2/facturaElectronica"))
                            throw new BusinessException("El documento electrónico no contiene el formato V4.3 requerido por el Ministerio de Hacienda. Consulte con el emisor de su factura de gastos");
                        if (documentoXml.GetElementsByTagName("Otros").Count > 0)
                        {
                            XmlNode otrosNode = documentoXml.GetElementsByTagName("Otros").Item(0);
                            otrosNode.InnerText = "";
                        }
                        if (documentoXml.GetElementsByTagName("Receptor").Count > 0)
                        {
                            XmlNode emisorNode = documentoXml.GetElementsByTagName("Receptor").Item(0);
                            strIdentificacion = emisorNode["Identificacion"]["Numero"].InnerText;
                        }
                        strDatos = documentoXml.OuterXml.Replace("'", "");
                    } else
                    {
                        strError = "El documento electrónico no corresponde a una factura o nota de crédito/débito electrónica. Consulte con el emisor de su factura de gastos";
                    }
                }
            }
            if (strDatos == "")
            {
                if (strError == "") strError = "No se logró extraer la  información requerida para procesar el documento eletrónico adjunto. Por favor comuniquese con su proveedor.";
                throw new BusinessException(strError);
            }
            Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.Identificacion == strIdentificacion).FirstOrDefault();
            if (empresa == null) throw new BusinessException("La identificación contenida en el archivo XML enviado: " + strIdentificacion + " no pertenece a ninguna empresa suscrita al servicio de facturación electrónica.");
            if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la solicitud no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
            if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación de la empresa que envía la solicitud ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
            DocumentoElectronico documentoMR = ComprobanteElectronicoService.GeneraMensajeReceptor(strDatos, empresa, dbContext, 1, 1, 0, bolIvaAplicable);
            dbContext.DocumentoElectronicoRepository.Add(documentoMR);
            dbContext.Commit();
            if (documentoMR != null)
            {
                Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, documentoMR, datos));
            }
        }

        public void ReprocesarDocumentoElectronico(int intIdDocumento, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.AsNoTracking().FirstOrDefault(x => x.IdDocumento == intIdDocumento);
                    Empresa empresa = dbContext.EmpresaRepository.Find(documento.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (documento.Reprocesado) throw new BusinessException("El documento ya fue reprocesado y no puede procesarse nuevamente.");
                    if (documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Aceptado && documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Rechazado)
                    {
                        throw new BusinessException("El documento electrónico por procesar no ha sido procesado. No se puede generar un nuevo documento por el momento");
                    }
                    if (documento.EstadoEnvio != StaticEstadoDocumentoElectronico.Rechazado) throw new BusinessException("El documento no posee un estado de rechazado por lo que no puede ser reenviado al Ministerio de Hacienda.");
                    if (documento.EsMensajeReceptor == "S") throw new BusinessException("No se puede reenviar documento recepcionados, solo documentos emitidos por el sistema.");
                    DocumentoElectronico nuevoDocumento = null;
                    if (new int[] { (int)TipoDocumento.FacturaElectronica, (int)TipoDocumento.TiqueteElectronico }.Contains(documento.IdTipoDocumento))
                    {
                        Factura factura = dbContext.FacturaRepository.Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdDocElectronico == documento.ClaveNumerica);
                        if (factura == null) throw new BusinessException("La registro origen del documento no existe.");
                        if ((int)TipoDocumento.FacturaElectronica == documento.IdTipoDocumento)
                            nuevoDocumento = ComprobanteElectronicoService.GenerarFacturaElectronica(factura, empresa, factura.Cliente, dbContext, factura.TipoDeCambioDolar);
                        else
                            nuevoDocumento = ComprobanteElectronicoService.GeneraTiqueteElectronico(factura, empresa, factura.Cliente, dbContext, factura.TipoDeCambioDolar);
                        factura.IdDocElectronico = nuevoDocumento.ClaveNumerica;
                        dbContext.NotificarModificacion(factura);
                    }
                    else if ((int)TipoDocumento.NotaCreditoElectronica == documento.IdTipoDocumento)
                    {
                        Factura factura = dbContext.FacturaRepository.Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdDocElectronicoRev == documento.ClaveNumerica);
                        if (factura != null)
                        {
                            nuevoDocumento = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronica(factura, empresa, factura.Cliente, dbContext, factura.TipoDeCambioDolar);
                            factura.IdDocElectronicoRev = nuevoDocumento.ClaveNumerica;
                            dbContext.NotificarModificacion(factura);
                        }
                        else
                        {
                            DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.Include("Cliente").Include("DetalleDevolucionCliente.Producto").FirstOrDefault(x => x.IdDocElectronico == documento.ClaveNumerica);
                            if (devolucion == null) throw new BusinessException("El registro origen del documento no existe.");
                            factura = dbContext.FacturaRepository.AsNoTracking().Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == devolucion.IdFactura);
                            nuevoDocumento = ComprobanteElectronicoService.GenerarNotaDeCreditoElectronicaParcial(devolucion, factura, empresa, factura.Cliente, dbContext, factura.TipoDeCambioDolar, factura.IdDocElectronico);
                            devolucion.IdDocElectronico = nuevoDocumento.ClaveNumerica;
                            dbContext.NotificarModificacion(devolucion);
                        }
                    }
                    else if ((int)TipoDocumento.NotaDebitoElectronica == documento.IdTipoDocumento)
                    {
                        DevolucionCliente devolucion = dbContext.DevolucionClienteRepository.Include("Cliente").Include("DetalleDevolucionCliente.Producto").FirstOrDefault(x => x.IdDocElectronicoRev == documento.ClaveNumerica);
                        if (devolucion == null) throw new BusinessException("El registro origen del documento no existe.");
                        Factura factura = dbContext.FacturaRepository.AsNoTracking().Include("Cliente").Include("Vendedor").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == devolucion.IdFactura);
                        nuevoDocumento = ComprobanteElectronicoService.GenerarNotaDeDebitoElectronicaParcial(devolucion, factura, empresa, devolucion.Cliente, dbContext, factura.TipoDeCambioDolar, devolucion.IdDocElectronico);
                        devolucion.IdDocElectronicoRev = nuevoDocumento.ClaveNumerica;
                        dbContext.NotificarModificacion(devolucion);
                    }
                    if (nuevoDocumento == null) throw new BusinessException("No fue posible generar un nuevo documento electrónico.");
                    dbContext.DocumentoElectronicoRepository.Add(nuevoDocumento);
                    documento.Reprocesado = true;
                    dbContext.NotificarModificacion(documento);
                    dbContext.Commit();
                    if (nuevoDocumento != null)
                    {
                        Task.Run(() => EnviarDocumentoElectronico(empresa.IdEmpresa, nuevoDocumento, datos));
                    }
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al reenviar el documento electrónico pendiente: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor intente más tarde.");
                    else
                        throw new Exception("Se produjo un error al reenviar el documento electrónico pendiente. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado));
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreReceptor.Contains(strNombre));
                    return listado.Count();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de documentos electrónicos procesados: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de documentos electrónicos procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<DocumentoDetalle> ObtenerListadoDocumentosElectronicosProcesados(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaDocumento = new List<DocumentoDetalle>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listado = dbContext.DocumentoElectronicoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && (x.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || x.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado));
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.NombreReceptor.Contains(strNombre));
                    listado = listado.OrderByDescending(x => x.IdDocumento).Skip((numPagina - 1) * cantRec).Take(cantRec);

                    foreach (var value in listado)
                    {
                        string datosXml = "";
                        string strReceptor = "";
                        decimal decTotal = 0;
                        if (value.EsMensajeReceptor == "S")
                            if (value.DatosDocumentoOri != null)
                                datosXml = Encoding.UTF8.GetString(value.DatosDocumentoOri);
                            else
                                datosXml = Encoding.UTF8.GetString(value.DatosDocumento);
                        else
                            datosXml = Encoding.UTF8.GetString(value.DatosDocumento);
                        XmlDocument documentoXml = new XmlDocument();
                        documentoXml.LoadXml(datosXml);
                        if (value.EsMensajeReceptor == "S" || value.IdTipoDocumento == 8)
                        {
                            strReceptor = "SIN INFORMACION DEL EMISOR";
                            if (documentoXml.GetElementsByTagName("Emisor").Count > 0)
                            {
                                XmlNode emisorNode = documentoXml.GetElementsByTagName("Emisor").Item(0);
                                strReceptor = emisorNode["Nombre"].InnerText;
                            }
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                            else
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalFactura").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            strReceptor = value.NombreReceptor;
                            if (documentoXml.GetElementsByTagName("TotalComprobante").Count > 0)
                                decTotal = decimal.Parse(documentoXml.GetElementsByTagName("TotalComprobante").Item(0).InnerText, CultureInfo.InvariantCulture);
                        }
                        DocumentoDetalle item = new DocumentoDetalle(value.IdDocumento, value.IdTipoDocumento, value.ClaveNumerica, value.Consecutivo, value.Fecha.ToString("dd/MM/yyyy"), strReceptor, value.EstadoEnvio, value.ErrorEnvio, decTotal, value.EsMensajeReceptor, value.Reprocesado, value.CorreoNotificacion);
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
                    _logger.LogError("Error al consultar el listado de documentos electrónicos procesados: ", ex);
                    if (ex.Message == "Service Unavailable")
                        throw new Exception("El servicio de factura electrónica se encuentra fuera de servicio. Por favor consulte con su proveedor.");
                    else
                        throw new Exception("Se produjo un error al consultar el listado de documentos electrónicos procesados. Por favor consulte con su proveedor.");
                }
            }
        }

        public DocumentoElectronico ObtenerDocumentoElectronico(int intIdDocumento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al consultar documento electrónico con ID: " + intIdDocumento, ex);
                    throw new Exception("Se produjo un error al consultar el documento electrónico. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ProcesarRespuestaHacienda(RespuestaHaciendaDTO mensaje, string strCorreoNotificacionErrores, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                ProcesarRespuestaHacienda(dbContext, mensaje, strCorreoNotificacionErrores, bytLogo);
            }
        }

        private void ProcesarRespuestaHacienda(LeandroContext dbContext, RespuestaHaciendaDTO mensaje, string strCorreoNotificacionErrores, byte[] bytLogo)
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
                    documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave && x.Consecutivo == strConsecutivo).FirstOrDefault();
                else
                    documentoElectronico = dbContext.DocumentoElectronicoRepository.Where(x => x.ClaveNumerica == strClave).FirstOrDefault();
                if (documentoElectronico == null)
                {
                    JArray emptyJArray = new JArray();
                    string strBody = "El documento con clave " + mensaje.Clave + " no se encuentra registrado en los registros del cliente.";
                    servicioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al recibir respuesta de Hacienda.", strBody, false, emptyJArray);
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
                        dbContext.Commit();
                        if (documentoElectronico.IdTipoDocumento != (int)TipoDocumento.TiqueteElectronico && documentoElectronico.CorreoNotificacion != "") GenerarNotificacionDocumentoElectronico(dbContext, documentoElectronico, empresa, documentoElectronico.CorreoNotificacion, strCorreoNotificacionErrores, bytLogo);
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                servicioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Excepción en el procesamiento de la respuesta de hacienda para el comprobante con clave: " + mensaje.Clave, ex.Message, false, emptyJArray);
            }
        }

        public void EnviarNotificacionDocumentoElectronico(int intIdDocumento, string strCorreoReceptor, string strCorreoNotificacionErrores, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    if (strCorreoReceptor == "") throw new BusinessException("Se debe proveer una dirección de válida para el receptor del documento. Por favor verifique la información suministrada");
                    DocumentoElectronico documento = dbContext.DocumentoElectronicoRepository.Find(intIdDocumento);
                    if (documento == null && documento.IdTipoDocumento != (int)TipoDocumento.TiqueteElectronico)
                    {
                        JArray emptyJArray = new JArray();
                        string strBody = "El documento con ID " + intIdDocumento + " no se encuentra registrado.";
                        servicioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al recibir respuesta de Hacienda.", strBody, false, emptyJArray);
                    }
                    else
                    {
                        Empresa empresa = dbContext.EmpresaRepository.Where(x => x.IdEmpresa == documento.IdEmpresa).FirstOrDefault();
                        if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Aceptado || documento.EstadoEnvio == StaticEstadoDocumentoElectronico.Rechazado)
                        {
                            GenerarNotificacionDocumentoElectronico(dbContext, documento, empresa, strCorreoReceptor, strCorreoNotificacionErrores, bytLogo);
                        }
                    }
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al enviar notificación al receptor para el documento con ID: " + intIdDocumento, ex);
                    throw new Exception("Se produjo un error al enviar el documento electrónico al receptor. Por favor consulte con su proveedor.");
                }
            }
        }

        public byte[] GenerarFacturaPDF(int intIdFactura, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Factura factura = dbContext.FacturaRepository.Include("Cliente").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    EstructuraPDF datos = GenerarEstructuraFacturaPDF(empresa, factura, bytLogo);
                    return Generador.GenerarPDF(datos);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al generar archivo PDF de factura con ID: " + intIdFactura, ex);
                    throw new Exception("Se produjo un error al generar el archivo PDF de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public byte[] GenerarApartadoPDF(int intIdApartado, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Apartado apartado = dbContext.ApartadoRepository.Include("Cliente").Include("DetalleApartado.Producto").FirstOrDefault(x => x.IdApartado == intIdApartado);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == apartado.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    EstructuraPDF datos = GenerarEstructuraApartadoPDF(empresa, apartado, bytLogo);
                    return Generador.GenerarPDF(datos);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al generar archivo PDF del apartado con ID: " + intIdApartado, ex);
                    throw new Exception("Se produjo un error al generar el archivo PDF del apartado. Por favor consulte con su proveedor.");
                }
            }
        }

        public byte[] GenerarOrdenServicioPDF(int intIdOrdenServicio, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    OrdenServicio ordenServicio = dbContext.OrdenServicioRepository.Include("Cliente").Include("DetalleOrdenServicio.Producto").FirstOrDefault(x => x.IdOrden == intIdOrdenServicio);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == ordenServicio.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    EstructuraPDF datos = GenerarEstructuraOrdenServicioPDF(empresa, ordenServicio, bytLogo);
                    return Generador.GenerarPDF(datos);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al generar archivo PDF de orden de servicio con ID: " + intIdOrdenServicio, ex);
                    throw new Exception("Se produjo un error al generar el archivo PDF de la orden de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public byte[] GenerarProformaPDF(int intIdProforma, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Proforma proforma = dbContext.ProformaRepository.Include("Cliente").Include("DetalleProforma.Producto").FirstOrDefault(x => x.IdProforma == intIdProforma);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == proforma.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    EstructuraPDF datos = GenerarEstructuraProformaPDF(empresa, proforma, bytLogo);
                    return Generador.GenerarPDF(datos);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al generar archivo PDF de la proforma con ID: " + intIdProforma, ex);
                    throw new Exception("Se produjo un error al generar el archivo PDF de la proforma. Por favor consulte con su proveedor.");
                }
            }
        }

        public void GenerarNotificacionFactura(int intIdFactura, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    JArray jarrayObj = new JArray();
                    Factura factura = dbContext.FacturaRepository.Include("Cliente").Include("DetalleFactura.Producto").Include("DesglosePagoFactura").FirstOrDefault(x => x.IdFactura == intIdFactura);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CorreoNotificacion != "")
                    {
                        EstructuraPDF datos = GenerarEstructuraFacturaPDF(empresa, factura, bytLogo);
                        byte[] pdfAttactment = Generador.GenerarPDF(datos);
                        JObject jobDatosAdjuntos1 = new JObject
                        {
                            ["nombre"] = "Factura-" + intIdFactura + ".pdf",
                            ["contenido"] = Convert.ToBase64String(pdfAttactment)
                        };
                        jarrayObj.Add(jobDatosAdjuntos1);
                        servicioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Notificación de factura nro. " + intIdFactura + " en formato PDF", "Adunto encotrará el documento en formato PDF correspondiente a la factura número " + intIdFactura, false, jarrayObj);
                    }
                    else
                        throw new BusinessException("La empresa no cuenta con un correo para el envío de notificaciones. Por favor actualice su información");
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al enviar por correo la factura con ID: " + intIdFactura, ex);
                    throw new Exception("Se produjo un error al enviar el documento por correo. Por favor consulte con su proveedor.");
                }
            }
        }

        private EstructuraPDF GenerarEstructuraFacturaPDF(Empresa empresa, Factura factura, byte[] bytLogo)
        {
            EstructuraPDF datos = new EstructuraPDF();
            datos.PoweredByLogotipo = bytLogo;
            if (empresa.Logotipo.Length > 0)
            {
                try
                {
                    datos.Logotipo = empresa.Logotipo;
                }
                catch (Exception)
                {
                    datos.Logotipo = null;
                }
            }
            else
            {
                datos.Logotipo = null;
            }
            datos.TituloDocumento = "FACTURA ELECTRONICA";
            datos.NombreEmpresa = empresa.NombreEmpresa;
            datos.NombreComercial = empresa.NombreComercial;
            datos.PlazoCredito = factura.PlazoCredito > 0 ? factura.PlazoCredito.ToString() : "";
            datos.ConsecInterno = factura.ConsecFactura.ToString();
            datos.Clave = factura.IdDocElectronico;
            if (factura.IdDocElectronico != "") datos.Consecutivo = factura.IdDocElectronico.Substring(21, 20);
            datos.CondicionVenta = CondicionDeVenta.ObtenerDescripcion(factura.IdCondicionVenta);
            datos.Fecha = factura.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
            if (factura.IdCondicionVenta == StaticCondicionVenta.Credito)
                datos.MedioPago = "Crédito";
            else if (factura.DesglosePagoFactura.ToList().Count > 1)
                datos.MedioPago = "Otros";
            else
                datos.MedioPago = FormaDePago.ObtenerDescripcion(factura.DesglosePagoFactura.FirstOrDefault().IdFormaPago);
            datos.NombreEmisor = empresa.NombreEmpresa;
            datos.NombreComercialEmisor = empresa.NombreComercial;
            datos.IdentificacionEmisor = empresa.Identificacion;
            datos.CorreoElectronicoEmisor = empresa.CorreoNotificacion;
            datos.TelefonoEmisor = empresa.Telefono1 + (empresa.Telefono2.Length > 0 ? " - " + empresa.Telefono2 : "");
            datos.FaxEmisor = "";
            datos.ProvinciaEmisor = empresa.Barrio.Distrito.Canton.Provincia.Descripcion;
            datos.CantonEmisor = empresa.Barrio.Distrito.Canton.Descripcion;
            datos.DistritoEmisor = empresa.Barrio.Distrito.Descripcion;
            datos.BarrioEmisor = empresa.Barrio.Descripcion;
            datos.DireccionEmisor = empresa.Direccion;
            datos.NombreReceptor = factura.NombreCliente;
            if (factura.IdCliente > 1)
            {
                datos.PoseeReceptor = true;
                datos.NombreComercialReceptor = factura.Cliente.NombreComercial;
                datos.IdentificacionReceptor = factura.Cliente.Identificacion;
                datos.CorreoElectronicoReceptor = factura.Cliente.CorreoElectronico;
                datos.TelefonoReceptor = factura.Cliente.Telefono;
                datos.FaxReceptor = factura.Cliente.Fax;
            }
            datos.DetalleServicio = new List<EstructuraPDFDetalleServicio>();
            foreach (DetalleFactura linea in factura.DetalleFactura)
            {
                decimal decTotalLinea = linea.Cantidad * linea.PrecioVenta;
                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                {
                    Cantidad = linea.Cantidad.ToString("N2", CultureInfo.InvariantCulture),
                    Codigo = linea.Producto.CodigoClasificacion,
                    Detalle = linea.Descripcion,
                    PrecioUnitario = linea.PrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                    TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
                };
                datos.DetalleServicio.Add(detalle);
            };
            if (factura.TextoAdicional != null) datos.OtrosTextos = factura.TextoAdicional;
            datos.TotalGravado = factura.Gravado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExonerado = factura.Exonerado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExento = factura.Excento.ToString("N2", CultureInfo.InvariantCulture);
            datos.Descuento = "0.00";
            datos.Impuesto = factura.Impuesto.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalGeneral = (factura.Gravado + factura.Exonerado + factura.Excento + factura.Impuesto).ToString("N2", CultureInfo.InvariantCulture);
            datos.CodigoMoneda = factura.IdTipoMoneda == 1 ? "CRC" : "USD";
            datos.TipoDeCambio = "1";
            return datos;
        }

        private EstructuraPDF GenerarEstructuraApartadoPDF(Empresa empresa, Apartado apartado, byte[] bytLogo)
        {
            EstructuraPDF datos = new EstructuraPDF();
            datos.PoweredByLogotipo = bytLogo;
            if (empresa.Logotipo.Length > 0)
            {
                try
                {
                    datos.Logotipo = empresa.Logotipo;
                }
                catch (Exception)
                {
                    datos.Logotipo = null;
                }
            }
            else
            {
                datos.Logotipo = null;
            }
            datos.TituloDocumento = "APARTADO";
            datos.NombreEmpresa = empresa.NombreEmpresa;
            datos.NombreComercial = empresa.NombreComercial;
            datos.PlazoCredito = "";
            datos.ConsecInterno = apartado.ConsecApartado.ToString();
            datos.Consecutivo = null;
            datos.Clave = null;
            datos.CondicionVenta = "Proforma";
            datos.PlazoCredito = "";
            datos.Fecha = apartado.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
            datos.MedioPago = "";
            datos.NombreEmisor = empresa.NombreEmpresa;
            datos.NombreComercialEmisor = empresa.NombreComercial;
            datos.IdentificacionEmisor = empresa.Identificacion;
            datos.CorreoElectronicoEmisor = empresa.CorreoNotificacion;
            datos.TelefonoEmisor = empresa.Telefono1 + (empresa.Telefono2.Length > 0 ? " - " + empresa.Telefono2 : "");
            datos.FaxEmisor = "";
            datos.ProvinciaEmisor = empresa.Barrio.Distrito.Canton.Provincia.Descripcion;
            datos.CantonEmisor = empresa.Barrio.Distrito.Canton.Descripcion;
            datos.DistritoEmisor = empresa.Barrio.Distrito.Descripcion;
            datos.BarrioEmisor = empresa.Barrio.Descripcion;
            datos.DireccionEmisor = empresa.Direccion;
            datos.NombreReceptor = apartado.NombreCliente;
            if (apartado.IdCliente > 1)
            {
                datos.PoseeReceptor = true;
                datos.NombreComercialReceptor = apartado.Cliente.NombreComercial;
                datos.IdentificacionReceptor = apartado.Cliente.Identificacion;
                datos.CorreoElectronicoReceptor = apartado.Cliente.CorreoElectronico;
                datos.TelefonoReceptor = apartado.Cliente.Telefono;
                datos.FaxReceptor = apartado.Cliente.Fax;
            }
            datos.DetalleServicio = new List<EstructuraPDFDetalleServicio>();
            foreach (DetalleApartado linea in apartado.DetalleApartado)
            {
                decimal decTotalLinea = linea.Cantidad * linea.PrecioVenta;
                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                {
                    Cantidad = linea.Cantidad.ToString("N2", CultureInfo.InvariantCulture),
                    Codigo = linea.Producto.CodigoClasificacion,
                    Detalle = linea.Descripcion,
                    PrecioUnitario = linea.PrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                    TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
                };
                datos.DetalleServicio.Add(detalle);
            };
            if (apartado.TextoAdicional != null) datos.OtrosTextos = apartado.TextoAdicional;
            datos.TotalGravado = apartado.Gravado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExonerado = apartado.Exonerado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExento = apartado.Excento.ToString("N2", CultureInfo.InvariantCulture);
            datos.Descuento = "0.00";
            datos.Impuesto = apartado.Impuesto.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalGeneral = (apartado.Gravado + apartado.Exonerado + apartado.Excento + apartado.Impuesto).ToString("N2", CultureInfo.InvariantCulture);
            datos.CodigoMoneda = apartado.IdTipoMoneda == 1 ? "CRC" : "USD";
            datos.TipoDeCambio = "1";
            return datos;
        }

        private EstructuraPDF GenerarEstructuraOrdenServicioPDF(Empresa empresa, OrdenServicio ordenServicio, byte[] bytLogo)
        {
            EstructuraPDF datos = new EstructuraPDF();
            datos.PoweredByLogotipo = bytLogo;
            if (empresa.Logotipo.Length > 0)
            {
                try
                {
                    datos.Logotipo = empresa.Logotipo;
                }
                catch (Exception)
                {
                    datos.Logotipo = null;
                }
            }
            else
            {
                datos.Logotipo = null;
            }
            datos.TituloDocumento = "ORDEN DE SERVICIO";
            datos.NombreEmpresa = empresa.NombreEmpresa;
            datos.NombreComercial = empresa.NombreComercial;
            datos.PlazoCredito = "";
            datos.ConsecInterno = ordenServicio.ConsecOrdenServicio.ToString();
            datos.Consecutivo = null;
            datos.Clave = null;
            datos.CondicionVenta = "Efectivo";
            datos.PlazoCredito = "";
            datos.Fecha = ordenServicio.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
            datos.MedioPago = "";
            datos.NombreEmisor = empresa.NombreEmpresa;
            datos.NombreComercialEmisor = empresa.NombreComercial;
            datos.IdentificacionEmisor = empresa.Identificacion;
            datos.CorreoElectronicoEmisor = empresa.CorreoNotificacion;
            datos.TelefonoEmisor = empresa.Telefono1 + (empresa.Telefono2.Length > 0 ? " - " + empresa.Telefono2 : "");
            datos.FaxEmisor = "";
            datos.ProvinciaEmisor = empresa.Barrio.Distrito.Canton.Provincia.Descripcion;
            datos.CantonEmisor = empresa.Barrio.Distrito.Canton.Descripcion;
            datos.DistritoEmisor = empresa.Barrio.Distrito.Descripcion;
            datos.BarrioEmisor = empresa.Barrio.Descripcion;
            datos.DireccionEmisor = empresa.Direccion;
            datos.NombreReceptor = ordenServicio.NombreCliente;
            if (ordenServicio.IdCliente > 1)
            {
                datos.PoseeReceptor = true;
                datos.NombreComercialReceptor = ordenServicio.Cliente.NombreComercial;
                datos.IdentificacionReceptor = ordenServicio.Cliente.Identificacion;
                datos.CorreoElectronicoReceptor = ordenServicio.Cliente.CorreoElectronico;
                datos.TelefonoReceptor = ordenServicio.Cliente.Telefono;
                datos.FaxReceptor = ordenServicio.Cliente.Fax;
            }
            datos.DetalleServicio = new List<EstructuraPDFDetalleServicio>();
            foreach (DetalleOrdenServicio linea in ordenServicio.DetalleOrdenServicio)
            {
                decimal decTotalLinea = linea.Cantidad * linea.PrecioVenta;
                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                {
                    Cantidad = linea.Cantidad.ToString("N2", CultureInfo.InvariantCulture),
                    Codigo = linea.Producto.CodigoClasificacion,
                    Detalle = linea.Descripcion,
                    PrecioUnitario = linea.PrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                    TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
                };
                datos.DetalleServicio.Add(detalle);
            };
            datos.TotalGravado = ordenServicio.Gravado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExonerado = ordenServicio.Exonerado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExento = ordenServicio.Excento.ToString("N2", CultureInfo.InvariantCulture);
            datos.Descuento = "0.00";
            datos.Impuesto = ordenServicio.Impuesto.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalGeneral = (ordenServicio.Gravado + ordenServicio.Exonerado + ordenServicio.Excento + ordenServicio.Impuesto).ToString("N2", CultureInfo.InvariantCulture);
            datos.CodigoMoneda = ordenServicio.IdTipoMoneda == 1 ? "CRC" : "USD";
            datos.TipoDeCambio = "1";
            return datos;
        }

        private EstructuraPDF GenerarEstructuraProformaPDF(Empresa empresa, Proforma proforma, byte[] bytLogo)
        {
            EstructuraPDF datos = new EstructuraPDF();
            datos.PoweredByLogotipo = bytLogo;
            if (empresa.Logotipo.Length > 0)
            {
                try
                {
                    datos.Logotipo = empresa.Logotipo;
                }
                catch (Exception)
                {
                    datos.Logotipo = null;
                }
            }
            else
            {
                datos.Logotipo = null;
            }
            datos.TituloDocumento = "PROFORMA";
            datos.NombreEmpresa = empresa.NombreEmpresa;
            datos.NombreComercial = empresa.NombreComercial;
            datos.PlazoCredito = "";
            datos.ConsecInterno = proforma.ConsecProforma.ToString();
            datos.Consecutivo = null;
            datos.Clave = null;
            datos.CondicionVenta = "Proforma";
            datos.PlazoCredito = "";
            datos.Fecha = proforma.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
            datos.MedioPago = "";
            datos.NombreEmisor = empresa.NombreEmpresa;
            datos.NombreComercialEmisor = empresa.NombreComercial;
            datos.IdentificacionEmisor = empresa.Identificacion;
            datos.CorreoElectronicoEmisor = empresa.CorreoNotificacion;
            datos.TelefonoEmisor = empresa.Telefono1 + (empresa.Telefono2.Length > 0 ? " - " + empresa.Telefono2 : "");
            datos.FaxEmisor = "";
            datos.ProvinciaEmisor = empresa.Barrio.Distrito.Canton.Provincia.Descripcion;
            datos.CantonEmisor = empresa.Barrio.Distrito.Canton.Descripcion;
            datos.DistritoEmisor = empresa.Barrio.Distrito.Descripcion;
            datos.BarrioEmisor = empresa.Barrio.Descripcion;
            datos.DireccionEmisor = empresa.Direccion;
            datos.NombreReceptor = proforma.NombreCliente;
            if (proforma.IdCliente > 1)
            {
                datos.PoseeReceptor = true;
                datos.NombreComercialReceptor = proforma.Cliente.NombreComercial;
                datos.IdentificacionReceptor = proforma.Cliente.Identificacion;
                datos.CorreoElectronicoReceptor = proforma.Cliente.CorreoElectronico;
                datos.TelefonoReceptor = proforma.Cliente.Telefono;
                datos.FaxReceptor = proforma.Cliente.Fax;
            }
            datos.DetalleServicio = new List<EstructuraPDFDetalleServicio>();
            foreach (DetalleProforma linea in proforma.DetalleProforma)
            {
                decimal decTotalLinea = linea.Cantidad * linea.PrecioVenta;
                EstructuraPDFDetalleServicio detalle = new EstructuraPDFDetalleServicio
                {
                    Cantidad = linea.Cantidad.ToString("N2", CultureInfo.InvariantCulture),
                    Codigo = linea.Producto.CodigoClasificacion,
                    Detalle = linea.Descripcion,
                    PrecioUnitario = linea.PrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                    TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
                };
                datos.DetalleServicio.Add(detalle);
            };
            if (proforma.TextoAdicional != "") datos.OtrosTextos = proforma.TextoAdicional;
            datos.TotalGravado = proforma.Gravado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExonerado = proforma.Exonerado.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalExento = proforma.Excento.ToString("N2", CultureInfo.InvariantCulture);
            datos.Descuento = "0.00";
            datos.Impuesto = proforma.Impuesto.ToString("N2", CultureInfo.InvariantCulture);
            datos.TotalGeneral = (proforma.Gravado + proforma.Exonerado + proforma.Excento + proforma.Impuesto).ToString("N2", CultureInfo.InvariantCulture);
            datos.CodigoMoneda = proforma.IdTipoMoneda == 1 ? "CRC" : "USD";
            datos.TipoDeCambio = "1";
            return datos;
        }

        private void GenerarNotificacionDocumentoElectronico(LeandroContext dbContext, DocumentoElectronico documentoElectronico, Empresa empresa, string strCorreoReceptor, string strCorreoNotificacionErrores, byte[] bytLogo)
        {
            try
            {
                string strBody;
                JArray jarrayObj = new JArray();
                if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica || documentoElectronico.IdTipoDocumento == (int)TipoDocumento.TiqueteElectronico || documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaCreditoElectronica || documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaDebitoElectronica)
                {
                    if (documentoElectronico.EstadoEnvio == "aceptado" && strCorreoReceptor != "")
                    {
                        string strTitle = "";
                        string[] arrCorreoReceptor = strCorreoReceptor.Split(';');
                        strBody = "Estimado cliente, adjunto encontrará el detalle del documento electrónico en formato PDF y XML con clave " + documentoElectronico.ClaveNumerica + " y la respuesta de aceptación del Ministerio de Hacienda.";
                        if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica)
                        {
                            strTitle = "Factura electrónica de emisor " + empresa.NombreComercial;
                        }
                        else if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.TiqueteElectronico)
                        {
                            strTitle = "Tiquete electrónico de emisor " + empresa.NombreComercial;
                        }
                        else if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaCreditoElectronica)
                        {
                            strTitle = "Nota de crédito electrónica de emisor " + empresa.NombreComercial;
                        }
                        else
                        {
                            strTitle = "Nota de débito electrónica de emisor " + empresa.NombreComercial;
                        }
                        EstructuraPDF datos = GenerarEstructuraDocumentoPDF(dbContext, empresa, documentoElectronico, bytLogo);
                        byte[] pdfAttactment = Generador.GenerarPDF(datos);
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
                        servicioCorreo.SendEmail(arrCorreoReceptor, new string[] { }, strTitle, strBody, false, jarrayObj);
                    }
                    else if(documentoElectronico.EstadoEnvio == "rechazado")
                    {
                        XmlDocument xmlRespuesta = new XmlDocument();
                        xmlRespuesta.LoadXml(Encoding.UTF8.GetString(documentoElectronico.Respuesta));
                        string strMensajeHacienda = xmlRespuesta.GetElementsByTagName("DetalleMensaje").Item(0).InnerText;
                        strBody = "Estimado cliente, le informamos que el documento electrónico con clave " + documentoElectronico.ClaveNumerica + " fue rechazado por el Ministerio de Hacienda con el siguiente mensaje:\n\n" + strMensajeHacienda + "\n\nPara mayor información consulte el documento en su plataforma de factura electrónica.";
                        JArray emptyJArray = new JArray();
                        servicioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Rechazo de documento electrónico con clave " + documentoElectronico.ClaveNumerica, strBody, false, emptyJArray);
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
                        servicioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Estado de documento electrónico enviado a aceptación", strBody, false, jarrayObj);
                    }
                }
            }
            catch (Exception ex)
            {
                JArray emptyJArray = new JArray();
                string strBody = "El documento con clave " + documentoElectronico.ClaveNumerica + " registrado en la empresa " + empresa.NombreEmpresa + " generó un error en el envío del PDF al remitente: " + strCorreoReceptor + " Error: " + ex.Message;
                servicioCorreo.SendEmail(new string[] { strCorreoNotificacionErrores }, new string[] { }, "Error al tratar de enviar el correo al receptor.", strBody, false, emptyJArray);
            }
        }

        private EstructuraPDF GenerarEstructuraDocumentoPDF(LeandroContext dbContext, Empresa empresa, DocumentoElectronico documentoElectronico, byte[] bytLogo)
        {
            EstructuraPDF datos = new EstructuraPDF();
            datos.PoweredByLogotipo = bytLogo;
            if (empresa.Logotipo.Length > 0)
            {
                try
                {
                    datos.Logotipo = empresa.Logotipo;
                }
                catch (Exception)
                {
                    datos.Logotipo = null;
                }
            }
            else
            {
                datos.Logotipo = null;
            }
            if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.FacturaElectronica)
            {
                datos.TituloDocumento = "FACTURA ELECTRONICA";
            }
            else if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.TiqueteElectronico)
            {
                datos.TituloDocumento = "TIQUETE ELECTRONICO";
            }
            else if (documentoElectronico.IdTipoDocumento == (int)TipoDocumento.NotaCreditoElectronica)
            {
                datos.TituloDocumento = "NOTA DE CREDITO ELECTRONICA";
            }
            else
            {
                datos.TituloDocumento = "NOTA DE DEBITO ELECTRONICA";
            }
            string datosXml = Encoding.UTF8.GetString(documentoElectronico.DatosDocumento);
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.LoadXml(datosXml);
            datos.NombreEmpresa = empresa.NombreEmpresa;
            datos.NombreComercial = empresa.NombreComercial;
            datos.ConsecInterno = documentoElectronico.IdDocumento.ToString();
            datos.Consecutivo = documentoElectronico.Consecutivo;
            datos.PlazoCredito = documentoXml.GetElementsByTagName("PlazoCredito").Count > 0 ? documentoXml.GetElementsByTagName("PlazoCredito").Item(0).InnerText : "";
            datos.Clave = documentoElectronico.ClaveNumerica;
            datos.CondicionVenta = CondicionDeVenta.ObtenerDescripcion(int.Parse(documentoXml.GetElementsByTagName("CondicionVenta").Item(0).InnerText));
            datos.Fecha = documentoElectronico.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
            datos.MedioPago = FormaDePago.ObtenerDescripcion(int.Parse(documentoXml.GetElementsByTagName("MedioPago").Item(0).InnerText));
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
            datos.CantonEmisor = dbContext.CantonRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton).FirstOrDefault().Descripcion;
            datos.DistritoEmisor = dbContext.DistritoRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito).FirstOrDefault().Descripcion;
            datos.BarrioEmisor = dbContext.BarrioRepository.Where(x => x.IdProvincia == intProvincia && x.IdCanton == intCanton && x.IdDistrito == intDistrito && x.IdBarrio == intBarrio).FirstOrDefault().Descripcion;
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
            }
            else
            {
                datos.NombreReceptor = documentoElectronico.NombreReceptor;
            }
            datos.DetalleServicio = new List<EstructuraPDFDetalleServicio>();
            foreach (XmlNode lineaDetalle in documentoXml.GetElementsByTagName("LineaDetalle"))
            {
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
                    PrecioUnitario = string.Format("{0:N2}", Convert.ToDouble(lineaDetalle["PrecioUnitario"].InnerText, CultureInfo.InvariantCulture)),
                    TotalLinea = string.Format("{0:N2}", Convert.ToDouble(lineaDetalle["MontoTotal"].InnerText, CultureInfo.InvariantCulture))
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
            datos.TotalGravado = string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalGravado"].InnerText, CultureInfo.InvariantCulture));
            datos.TotalExonerado = resumenFacturaNode["TotalExonerado"] != null && resumenFacturaNode["TotalExonerado"].ChildNodes.Count > 0 ? string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalExonerado"].InnerText, CultureInfo.InvariantCulture)) : "0.00";
            datos.TotalExento = string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalExento"].InnerText, CultureInfo.InvariantCulture));
            datos.Descuento = resumenFacturaNode["TotalDescuentos"] != null && resumenFacturaNode["TotalDescuentos"].ChildNodes.Count > 0 ? string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalDescuentos"].InnerText, CultureInfo.InvariantCulture)) : "0.00";
            datos.Impuesto = resumenFacturaNode["TotalImpuesto"] != null && resumenFacturaNode["TotalImpuesto"].ChildNodes.Count > 0 ? string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalImpuesto"].InnerText, CultureInfo.InvariantCulture)) : "0.00";
            datos.TotalGeneral = string.Format("{0:N2}", Convert.ToDouble(resumenFacturaNode["TotalComprobante"].InnerText, CultureInfo.InvariantCulture));
            datos.CodigoMoneda = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? resumenFacturaNode["CodigoTipoMoneda"]["CodigoMoneda"].InnerText : "CRC";
            datos.TipoDeCambio = resumenFacturaNode["CodigoTipoMoneda"] != null && resumenFacturaNode["CodigoTipoMoneda"].ChildNodes.Count > 0 ? resumenFacturaNode["CodigoTipoMoneda"]["TipoCambio"].InnerText : "1.00000";
            return datos;
        }

        public void GenerarNotificacionProforma(int intIdProforma, string strCorreoReceptor, byte[] bytLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    if (strCorreoReceptor == "") throw new BusinessException("Se debe proveer una dirección de válida para el receptor del documento. Por favor verifique la información suministrada");
                    Proforma proforma = dbContext.ProformaRepository.Include("Cliente").Include("DetalleProforma.Producto").Where(x => x.IdProforma == intIdProforma).FirstOrDefault();
                    if (proforma == null) throw new BusinessException("No existe registro de proforma para el identificador suministrado: " + intIdProforma);
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").Where(x => x.IdEmpresa == proforma.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    string strBody;
                    string strTitle = empresa.NombreComercial + " - Factura proforma";
                    strBody = "Estimado cliente, adjunto encontrará el detalle de la proforma solicitada.";
                    EstructuraPDF datos = GenerarEstructuraProformaPDF(empresa, proforma, bytLogo);
                    JArray jarrayObj = new JArray();
                    string[] arrCorreoReceptor = strCorreoReceptor.Split(';');
                    byte[] pdfAttactment = Generador.GenerarPDF(datos);
                    JObject jobDatosAdjuntos1 = new JObject
                    {
                        ["nombre"] = "proforma-" + intIdProforma + ".pdf",
                        ["contenido"] = Convert.ToBase64String(pdfAttactment)
                    };
                    jarrayObj.Add(jobDatosAdjuntos1);
                    servicioCorreo.SendEmail(arrCorreoReceptor, new string[] { }, strTitle, strBody, false, jarrayObj);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al enviar notificación al receptor para el documento con ID: " + intIdProforma, ex);
                    throw new Exception("Se produjo un error al notificar al cliente. Por favor consulte con su proveedor.");
                }
            }
        }

        private async Task EnviarDocumentoElectronico(int IdEmpresa, DocumentoElectronico documento, ConfiguracionGeneral datos)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                CredencialesHacienda credenciales = dbContext.CredencialesHaciendaRepository.Find(IdEmpresa);
                if (credenciales == null)
                {
                    documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                    documento.ErrorEnvio = "La empresa no posee parámetros para los credenciales de Hacienda";
                    dbContext.NotificarModificacion(documento);
                    dbContext.Commit();
                }
                else
                {
                    try
                    {
                        ComprobanteElectronicoService.ValidarToken(dbContext, credenciales, datos.ServicioTokenURL, datos.ClientId);
                    }
                    catch (Exception ex)
                    {
                        documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Registrado;
                        documento.ErrorEnvio = "No se logro obtener un token: " + ex.Message;
                        dbContext.NotificarModificacion(documento);
                        dbContext.Commit();
                    }
                    if (credenciales.AccessToken != null)
                    {
                        await ComprobanteElectronicoService.EnviarDocumentoElectronico(credenciales.AccessToken, documento, datos);
                        dbContext.NotificarModificacion(documento);
                        dbContext.Commit();
                    }
                }
            }
        }
    }
}