using LeandroSoftware.AccesoDatos.TiposDatos;
using LeandroSoftware.PuntoVenta.Datos;
using LeandroSoftware.PuntoVenta.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace LeandroSoftware.Migracion.ClienteWeb
{
    public partial class Migracion : Form
    {
        private NameValueCollection appSettings;
        private string connString;
        private string strAppThumptPrint;

        public Migracion()
        {
            InitializeComponent();
            try
            {
                connString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                appSettings = ConfigurationManager.AppSettings;
                string strkey = appSettings.Get("SubjectName");
                strAppThumptPrint = Core.Utilitario.VerificarCertificadoPorNombre(strkey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el archivo de configuración: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnProcesar_Click(object sender, EventArgs e)
        {
            btnProcesar.Enabled = false;
            rtbSalida.Text = "";
            int intIdSucursal = 0;
            int intIdTerminal = 0;
            try
            {
                intIdSucursal = int.Parse(txtIdSucursal.Text);
                intIdTerminal = int.Parse(txtIdTerminal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el código de la sucursal o terminal: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            rtbSalida.AppendText("INICIO DEL PROCESO DE MIGRACION\n");
            IDictionary<int, int> bancoAdquirienteDict = new Dictionary<int, int>();
            IDictionary<int, int> clienteDict = new Dictionary<int, int>();
            IDictionary<int, int> lineaDict = new Dictionary<int, int>();
            IDictionary<int, int> proveedorDict = new Dictionary<int, int>();
            IDictionary<int, int> productoDict = new Dictionary<int, int>();
            IDictionary<int, int> usuarioDict = new Dictionary<int, int>();
            IDictionary<int, int> cuentaBancoDict = new Dictionary<int, int>();
            IDictionary<int, int> cuentaEgresoDict = new Dictionary<int, int>();
            IDictionary<int, int> vendedorDict = new Dictionary<int, int>();
            try
            {
                using (IDbContext dbContext = new LeandroContext(connString))
                {
                    Empresa empresa = dbContext.EmpresaRepository.FirstOrDefault();
                    if (empresa == null) throw new Exception("No se logró obtener el registro de empresa de la base de datos.");
                    // Registros banco adquiriente
                    rtbSalida.AppendText("Agregando registros de banco adquiriente:\n");
                    List<BancoAdquiriente> listadoBancoAdquiriente = dbContext.BancoAdquirienteRepository.ToList();
                    foreach (BancoAdquiriente detalle in listadoBancoAdquiriente)
                    {
                        if (chkBancoAdquiriente.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.BancoAdquiriente nuevoRegistro = new AccesoDatos.Dominio.Entidades.BancoAdquiriente();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Codigo = detalle.Codigo;
                            nuevoRegistro.Descripcion = detalle.Descripcion;
                            nuevoRegistro.PorcentajeComision = detalle.PorcentajeComision;
                            nuevoRegistro.PorcentajeRetencion = detalle.PorcentajeRetencion;
                            string strId = await MigracionClient.AgregarBancoAdquiriente(nuevoRegistro);
                            bancoAdquirienteDict.Add(detalle.IdBanco, int.Parse(strId));
                            rtbSalida.AppendText("Migración de banco adquiriente IdLocal: " + detalle.IdBanco + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            bancoAdquirienteDict.Add(detalle.IdBanco, detalle.IdBanco);
                        }
                    }
                    rtbSalida.AppendText("Registros de banco adquiriente agregados satisfactoriamente. . .\n");

                    // Registros Clientes
                    rtbSalida.AppendText("Agregando registros de clientes:\n");
                    List<Cliente> listadoCliente = dbContext.ClienteRepository.Where(x => x.IdCliente > 1).ToList();
                    foreach (Cliente detalle in listadoCliente)
                    {
                        if (chkCliente.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.Cliente nuevoRegistro = new AccesoDatos.Dominio.Entidades.Cliente();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.IdTipoIdentificacion = detalle.IdTipoIdentificacion;
                            nuevoRegistro.Identificacion = detalle.Identificacion;
                            nuevoRegistro.IdentificacionExtranjero = detalle.IdentificacionExtranjero;
                            nuevoRegistro.IdProvincia = detalle.IdProvincia;
                            nuevoRegistro.IdCanton = detalle.IdCanton;
                            nuevoRegistro.IdDistrito = detalle.IdDistrito;
                            nuevoRegistro.IdBarrio = detalle.IdBarrio;
                            nuevoRegistro.Direccion = detalle.Direccion;
                            nuevoRegistro.Nombre = detalle.Nombre;
                            nuevoRegistro.NombreComercial = detalle.NombreComercial;
                            nuevoRegistro.Telefono = detalle.Telefono;
                            nuevoRegistro.Celular = detalle.Celular;
                            nuevoRegistro.Fax = detalle.Fax;
                            nuevoRegistro.CorreoElectronico = detalle.CorreoElectronico;
                            nuevoRegistro.IdVendedor = detalle.IdVendedor;
                            nuevoRegistro.IdTipoPrecio = detalle.IdTipoPrecio;
                            string strId = await MigracionClient.AgregarCliente(nuevoRegistro);
                            clienteDict.Add(detalle.IdCliente, int.Parse(strId));
                            rtbSalida.AppendText("Migración de cliente IdLocal: " + detalle.IdCliente + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            clienteDict.Add(detalle.IdCliente, detalle.IdCliente);
                        }
                    }
                    rtbSalida.AppendText("Registros de cliente agregados satisfactoriamente. . .\n");
                    // Registros Lineas
                    rtbSalida.AppendText("Agregando registros de lineas de producto:\n");
                    List<Linea> listadoLinea = dbContext.LineaRepository.ToList();
                    foreach (Linea detalle in listadoLinea)
                    {
                        if (chkLinea.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.Linea nuevoRegistro = new AccesoDatos.Dominio.Entidades.Linea();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.IdTipoProducto = detalle.IdTipoProducto;
                            nuevoRegistro.Descripcion = detalle.Descripcion;
                            string strId = await MigracionClient.AgregarLinea(nuevoRegistro);
                            lineaDict.Add(detalle.IdLinea, int.Parse(strId));
                            rtbSalida.AppendText("Migración de línea de producto IdLocal: " + detalle.IdLinea + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            lineaDict.Add(detalle.IdLinea, detalle.IdLinea);
                        }
                    }
                    rtbSalida.AppendText("Registros de lineas de producto agregados satisfactoriamente. . .\n");
                    // Registros Proveedores
                    rtbSalida.AppendText("Agregando registros de proveedores:\n");
                    List<Proveedor> listadoProveedor = dbContext.ProveedorRepository.ToList();
                    foreach (Proveedor detalle in listadoProveedor)
                    {
                        if (chkProveedor.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.Proveedor nuevoRegistro = new AccesoDatos.Dominio.Entidades.Proveedor();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Identificacion = detalle.Identificacion;
                            nuevoRegistro.Nombre = detalle.Nombre;
                            nuevoRegistro.Direccion = detalle.Direccion;
                            nuevoRegistro.Telefono1 = detalle.Telefono1;
                            nuevoRegistro.Telefono2 = detalle.Telefono2;
                            nuevoRegistro.Fax = detalle.Fax;
                            nuevoRegistro.Correo = detalle.Correo;
                            nuevoRegistro.PlazoCredito = detalle.PlazoCredito;
                            nuevoRegistro.Contacto1 = detalle.Contacto1;
                            nuevoRegistro.TelCont1 = detalle.TelCont1;
                            nuevoRegistro.Contacto2 = detalle.Contacto2;
                            nuevoRegistro.TelCont2 = detalle.TelCont2;
                            string strId = await MigracionClient.AgregarProveedor(nuevoRegistro);
                            proveedorDict.Add(detalle.IdProveedor, int.Parse(strId));
                            rtbSalida.AppendText("Migración de proveedor IdLocal: " + detalle.IdProveedor + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            proveedorDict.Add(detalle.IdProveedor, detalle.IdProveedor);
                        }
                    }
                    rtbSalida.AppendText("Registros de proveedores agregados satisfactoriamente. . .\n");
                    // Registros Productos
                    rtbSalida.AppendText("Agregando registros de productos:\n");
                    List<Producto> listadoProducto = dbContext.ProductoRepository.ToList();
                    foreach (Producto detalle in listadoProducto)
                    {
                        if (chkProducto.Checked)
                        {
                            int intNuevoIdLinea;
                            if (!lineaDict.TryGetValue(detalle.IdLinea, out intNuevoIdLinea)) throw new Exception("Línea del producto no ha sido migrada. Por favor verifique el proceso. . .");
                            int intNuevoIdProveedor;
                            if (!proveedorDict.TryGetValue(detalle.IdProveedor, out intNuevoIdProveedor)) throw new Exception("Línea del producto no ha sido migrada. Por favor verifique el proceso. . .");
                            AccesoDatos.Dominio.Entidades.Producto nuevoRegistro = new AccesoDatos.Dominio.Entidades.Producto();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Tipo = detalle.Tipo;
                            nuevoRegistro.IdLinea = intNuevoIdLinea;
                            nuevoRegistro.Codigo = detalle.Codigo;
                            nuevoRegistro.IdProveedor = intNuevoIdProveedor;
                            nuevoRegistro.Descripcion = detalle.Descripcion;
                            nuevoRegistro.Cantidad = detalle.Cantidad;
                            nuevoRegistro.PrecioCosto = detalle.PrecioCosto;
                            nuevoRegistro.PrecioVenta1 = detalle.PrecioVenta1;
                            nuevoRegistro.PrecioVenta2 = detalle.PrecioVenta2;
                            nuevoRegistro.PrecioVenta3 = detalle.PrecioVenta3;
                            nuevoRegistro.PrecioVenta4 = detalle.PrecioVenta4;
                            nuevoRegistro.PrecioVenta5 = detalle.PrecioVenta5;
                            nuevoRegistro.Excento = detalle.Excento;
                            nuevoRegistro.IndExistencia = detalle.IndExistencia;
                            nuevoRegistro.IdTipoUnidad = detalle.IdTipoUnidad;
                            string strId = await MigracionClient.AgregarProducto(nuevoRegistro);
                            productoDict.Add(detalle.IdProducto, int.Parse(strId));
                            rtbSalida.AppendText("Migración de producto IdLocal: " + detalle.IdProducto + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            productoDict.Add(detalle.IdProducto, detalle.IdProducto);
                        }
                    }
                    rtbSalida.AppendText("Registros de productos agregados satisfactoriamente. . .\n");
                    // Registros Usuario
                    rtbSalida.AppendText("Agregando registros de usuarios:\n");
                    List<Usuario> listadoUsuario = dbContext.UsuarioRepository.Include("RolePorUsuario").Where(x => x.IdUsuario > 1).ToList();
                    foreach (Usuario detalle in listadoUsuario)
                    {
                        if (chkUsuario.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.Usuario nuevoRegistro = new AccesoDatos.Dominio.Entidades.Usuario();
                            nuevoRegistro.CodigoUsuario = detalle.CodigoUsuario;
                            nuevoRegistro.Clave = Core.Utilitario.DesencriptarDatos(strAppThumptPrint, detalle.Clave);
                            nuevoRegistro.Modifica = detalle.Modifica;
                            nuevoRegistro.AutorizaCredito = detalle.AutorizaCredito;
                            foreach (RolePorUsuario role in detalle.RolePorUsuario)
                            {
                                AccesoDatos.Dominio.Entidades.RolePorUsuario nuevoRole = new AccesoDatos.Dominio.Entidades.RolePorUsuario();
                                nuevoRole.IdRole = role.IdRole;
                                nuevoRegistro.RolePorUsuario.Add(nuevoRole);
                            }
                            string strId = await MigracionClient.AgregarUsuario(nuevoRegistro);
                            string strBlank = await MigracionClient.AgregarUsuarioPorEmpresa(int.Parse(strId), empresa.IdEmpresa);
                            usuarioDict.Add(detalle.IdUsuario, int.Parse(strId));
                            rtbSalida.AppendText("Migración de usuario IdLocal: " + detalle.IdUsuario + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            usuarioDict.Add(detalle.IdUsuario, detalle.IdUsuario);
                        }
                    }
                    rtbSalida.AppendText("Registros de usuarios agregados satisfactoriamente. . .\n");
                    // Registros CuentaEgreso
                    rtbSalida.AppendText("Agregando registros de cuentas de egreso:\n");
                    List<CuentaEgreso> listadoCuentaEgreso = dbContext.CuentaEgresoRepository.ToList();
                    foreach (CuentaEgreso detalle in listadoCuentaEgreso)
                    {
                        if (chkCuentaEgreso.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.CuentaEgreso nuevoRegistro = new AccesoDatos.Dominio.Entidades.CuentaEgreso();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Descripcion = detalle.Descripcion;
                            string strId = await MigracionClient.AgregarCuentaEgreso(nuevoRegistro);
                            cuentaEgresoDict.Add(detalle.IdCuenta, int.Parse(strId));
                            rtbSalida.AppendText("Migración de cuenta de egreso IdLocal: " + detalle.IdCuenta + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            cuentaEgresoDict.Add(detalle.IdCuenta, detalle.IdCuenta);
                        }
                    }
                    rtbSalida.AppendText("Registros de cuentas de egreso agregados satisfactoriamente. . .\n");
                    // Registros CuentaBanco
                    rtbSalida.AppendText("Agregando registros de cuentas bancarias:\n");
                    List<CuentaBanco> listadoCuentaBanco = dbContext.CuentaBancoRepository.ToList();
                    foreach (CuentaBanco detalle in listadoCuentaBanco)
                    {
                        if (chkCuentaBanco.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.CuentaBanco nuevoRegistro = new AccesoDatos.Dominio.Entidades.CuentaBanco();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Codigo = detalle.Codigo;
                            nuevoRegistro.Descripcion = detalle.Descripcion;
                            nuevoRegistro.Saldo = detalle.Saldo;
                            string strId = (detalle.IdCuenta + 23).ToString(); // await MigracionClient.AgregarCuentaBanco(nuevoRegistro);
                            cuentaBancoDict.Add(detalle.IdCuenta, int.Parse(strId));
                            rtbSalida.AppendText("Migración de cuenta bancaria IdLocal: " + detalle.IdCuenta + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            cuentaBancoDict.Add(detalle.IdCuenta, detalle.IdCuenta);
                        }
                    }
                    rtbSalida.AppendText("Registros de cuentas bancarias agregados satisfactoriamente. . .\n");
                    // Registros Vendedor
                    rtbSalida.AppendText("Agregando registros de vendedores:\n");
                    List<Vendedor> listadoVendedor = dbContext.VendedorRepository.ToList();
                    foreach (Vendedor detalle in listadoVendedor)
                    {
                        if (chkVendedor.Checked)
                        {
                            AccesoDatos.Dominio.Entidades.Vendedor nuevoRegistro = new AccesoDatos.Dominio.Entidades.Vendedor();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.Nombre = detalle.Nombre;
                            string strId = await MigracionClient.AgregarVendedor(nuevoRegistro);
                            vendedorDict.Add(detalle.IdVendedor, int.Parse(strId));
                            rtbSalida.AppendText("Migración de vendedor IdLocal: " + detalle.IdVendedor + " IdServer: " + strId + "\n");
                        }
                        else
                        {
                            vendedorDict.Add(detalle.IdVendedor, detalle.IdVendedor);
                        }
                    }
                    rtbSalida.AppendText("Registros de vendedores satisfactoriamente. . .\n");
                    if (chkEgreso.Checked)
                    {
                        // Registros Egreso
                        rtbSalida.AppendText("Agregando registros de egresos:\n");
                        List<Egreso> listadoEgreso = dbContext.EgresoRepository.Include("DesglosePagoEgreso").Where(x => x.IdUsuario > 1).ToList();
                        foreach (Egreso detalle in listadoEgreso)
                        {
                            int intNuevoIdUsuario;
                            if (!usuarioDict.TryGetValue(detalle.IdUsuario, out intNuevoIdUsuario)) throw new Exception("Usuario no ha sido migrado. Por favor verifique el proceso. . .");
                            int intNuevoIdCuentaEgreso;
                            if (!cuentaEgresoDict.TryGetValue(detalle.IdCuenta, out intNuevoIdCuentaEgreso)) throw new Exception("Cuenta de egreso no ha sido migrada. Por favor verifique el proceso. . .");
                            AccesoDatos.Dominio.Entidades.Egreso nuevoRegistro = new AccesoDatos.Dominio.Entidades.Egreso();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.IdUsuario = intNuevoIdUsuario;
                            nuevoRegistro.Fecha = detalle.Fecha;
                            nuevoRegistro.IdCuenta = intNuevoIdCuentaEgreso;
                            nuevoRegistro.Beneficiario = detalle.Beneficiario;
                            nuevoRegistro.Detalle = detalle.Detalle;
                            nuevoRegistro.Monto = detalle.Monto;
                            nuevoRegistro.IdAsiento = detalle.IdAsiento;
                            nuevoRegistro.IdMovBanco = detalle.IdMovBanco;
                            nuevoRegistro.Nulo = detalle.Nulo;
                            nuevoRegistro.IdAnuladoPor = detalle.IdAnuladoPor;
                            nuevoRegistro.Procesado = detalle.Procesado;
                            foreach (DesglosePagoEgreso subDetalle in detalle.DesglosePagoEgreso)
                            {
                                int intNuevoIdCuentaBanco;
                                if (!cuentaBancoDict.TryGetValue(subDetalle.IdCuentaBanco, out intNuevoIdCuentaBanco)) throw new Exception("Cuenta bancaria no ha sido migrada. Por favor verifique el proceso. . .");
                                AccesoDatos.Dominio.Entidades.DesglosePagoEgreso nuevoDesglose = new AccesoDatos.Dominio.Entidades.DesglosePagoEgreso();
                                nuevoDesglose.IdFormaPago = subDetalle.IdFormaPago;
                                nuevoDesglose.IdTipoMoneda = subDetalle.IdTipoMoneda;
                                nuevoDesglose.IdCuentaBanco = intNuevoIdCuentaBanco;
                                nuevoDesglose.Beneficiario = subDetalle.Beneficiario;
                                nuevoDesglose.NroMovimiento = subDetalle.NroMovimiento;
                                nuevoDesglose.MontoLocal = subDetalle.MontoLocal;
                                nuevoDesglose.MontoForaneo = subDetalle.MontoForaneo;
                                nuevoRegistro.DesglosePagoEgreso.Add(nuevoDesglose);
                            }
                            string strId = await MigracionClient.AgregarEgreso(nuevoRegistro);
                            rtbSalida.AppendText("Migración de egreso IdLocal: " + detalle.IdEgreso + " IdServer: " + strId + "\n");
                        }
                        rtbSalida.AppendText("Registros de egresos agregados satisfactoriamente. . .\n");
                    }
                    if (chkFactura.Checked)
                    {
                        // Registros Factura
                        rtbSalida.AppendText("Agregando registros de facturación:\n");
                        List<Factura> listadoFactura = dbContext.FacturaRepository.Include("DetalleFactura").Include("DesglosePagoFactura").Where(x => x.IdUsuario > 1).ToList();
                        foreach (Factura detalle in listadoFactura)
                        {
                            int intNuevoIdUsuario;
                            if (!usuarioDict.TryGetValue(detalle.IdUsuario, out intNuevoIdUsuario)) throw new Exception("Usuario no ha sido migrado. Por favor verifique el proceso. . .");
                            int intNuevoIdCliente = 1;
                            if (detalle.IdCliente > 1)
                            {
                                if (!clienteDict.TryGetValue(detalle.IdCliente, out intNuevoIdCliente)) throw new Exception("Cliente no ha sido migrado. Por favor verifique el proceso. . .");
                            }
                            int intNuevoIdVendedor;
                            if (!vendedorDict.TryGetValue(detalle.IdVendedor, out intNuevoIdVendedor)) throw new Exception("Vendedor no ha sido migrado. Por favor verifique el proceso. . .");
                            AccesoDatos.Dominio.Entidades.Factura nuevoRegistro = new AccesoDatos.Dominio.Entidades.Factura();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.IdSucursal = intIdSucursal;
                            nuevoRegistro.IdTerminal = intIdTerminal;
                            nuevoRegistro.IdUsuario = intNuevoIdUsuario;
                            nuevoRegistro.IdCliente = intNuevoIdCliente;
                            nuevoRegistro.IdCondicionVenta = detalle.IdCondicionVenta;
                            nuevoRegistro.PlazoCredito = detalle.PlazoCredito;
                            nuevoRegistro.Fecha = detalle.Fecha;
                            nuevoRegistro.NoDocumento = detalle.NoDocumento;
                            nuevoRegistro.IdVendedor = intNuevoIdVendedor;
                            nuevoRegistro.Excento = detalle.Excento;
                            nuevoRegistro.Grabado = detalle.Grabado;
                            nuevoRegistro.Descuento = detalle.Descuento;
                            nuevoRegistro.PorcentajeIVA = detalle.PorcentajeIVA;
                            nuevoRegistro.Impuesto = detalle.Impuesto;
                            nuevoRegistro.MontoPagado = detalle.MontoPagado;
                            nuevoRegistro.IdCxC = detalle.IdCxC;
                            nuevoRegistro.IdAsiento = detalle.IdAsiento;
                            nuevoRegistro.IdMovBanco = detalle.IdMovBanco;
                            nuevoRegistro.IdOrdenServicio = detalle.IdOrdenServicio;
                            nuevoRegistro.IdProforma = detalle.IdProforma;
                            nuevoRegistro.TotalCosto = detalle.TotalCosto;
                            nuevoRegistro.Nulo = detalle.Nulo;
                            nuevoRegistro.IdAnuladoPor = detalle.IdAnuladoPor;
                            nuevoRegistro.Procesado = detalle.Procesado;
                            nuevoRegistro.IdDocElectronico = detalle.IdDocElectronico;
                            nuevoRegistro.IdDocElectronicoRev = detalle.IdDocElectronicoRev;
                            foreach (DetalleFactura detalleFactura in detalle.DetalleFactura)
                            {
                                int intNuevoIdProducto;
                                if (!productoDict.TryGetValue(detalleFactura.IdProducto, out intNuevoIdProducto)) throw new Exception("Producto no ha sido migrado. Por favor verifique el proceso. . .");
                                AccesoDatos.Dominio.Entidades.DetalleFactura nuevoDetalle = new AccesoDatos.Dominio.Entidades.DetalleFactura();
                                nuevoDetalle.IdProducto = intNuevoIdProducto;
                                nuevoDetalle.Descripcion = detalleFactura.Descripcion;
                                nuevoDetalle.Cantidad = detalleFactura.Cantidad;
                                nuevoDetalle.PrecioVenta = detalleFactura.PrecioVenta;
                                nuevoDetalle.Excento = detalleFactura.Excento;
                                nuevoDetalle.PrecioCosto = detalleFactura.PrecioCosto;
                                nuevoDetalle.CostoInstalacion = detalleFactura.CostoInstalacion;
                                nuevoRegistro.DetalleFactura.Add(nuevoDetalle);
                            }
                            foreach (DesglosePagoFactura desglosePago in detalle.DesglosePagoFactura)
                            {
                                int intNuevoIdBanco = 0;
                                if (desglosePago.IdFormaPago == 1 || desglosePago.IdFormaPago == 2 || desglosePago.IdFormaPago == 5 || desglosePago.IdFormaPago == 99)
                                {
                                    if (!bancoAdquirienteDict.TryGetValue(desglosePago.IdCuentaBanco, out intNuevoIdBanco)) throw new Exception("Banco adquiriente no ha sido migrado. Por favor verifique el proceso. . .");
                                }
                                else
                                {
                                    if (!cuentaBancoDict.TryGetValue(desglosePago.IdCuentaBanco, out intNuevoIdBanco)) throw new Exception("Cuenta bancaria no ha sido migrada. Por favor verifique el proceso. . .");
                                }
                                AccesoDatos.Dominio.Entidades.DesglosePagoFactura nuevoDesglose = new AccesoDatos.Dominio.Entidades.DesglosePagoFactura();
                                nuevoDesglose.IdFormaPago = desglosePago.IdFormaPago;
                                nuevoDesglose.IdTipoMoneda = desglosePago.IdTipoMoneda;
                                nuevoDesglose.IdCuentaBanco = intNuevoIdBanco;
                                nuevoDesglose.TipoTarjeta = desglosePago.TipoTarjeta;
                                nuevoDesglose.NroMovimiento = desglosePago.NroMovimiento;
                                nuevoDesglose.MontoLocal = desglosePago.MontoLocal;
                                nuevoDesglose.MontoForaneo = desglosePago.MontoForaneo;
                                nuevoRegistro.DesglosePagoFactura.Add(nuevoDesglose);
                            }
                            string strId = await MigracionClient.AgregarFactura(nuevoRegistro);
                            rtbSalida.AppendText("Migración de factura IdLocal: " + detalle.IdFactura + " IdServer: " + strId + "\n");
                        }
                        rtbSalida.AppendText("Registros de facturación agregados satisfactoriamente. . .\n");
                    }
                    if (chkDocElect.Checked)
                    {
                        // Registros DocumentoElectronico
                        rtbSalida.AppendText("Agregando registros de documentos electrónicos:\n");
                        List<DocumentoElectronico> listadoDocumentoElectronico = dbContext.DocumentoElectronicoRepository.ToList();
                        foreach (DocumentoElectronico detalle in listadoDocumentoElectronico)
                        {
                            string strMensajeReceptor = "N";
                            if (detalle.IdTipoDocumento == (int)TipoDocumento.MensajeReceptorAceptado || detalle.IdTipoDocumento == (int)TipoDocumento.MensajeReceptorAceptadoParcial || detalle.IdTipoDocumento == (int)TipoDocumento.MensajeReceptorRechazado)
                                strMensajeReceptor = "S";
                            string strIdentificadorEmisor = detalle.IdentificacionEmisor.TrimStart('0');
                            string strTipoIdentificacionEmisor = strIdentificadorEmisor.Length == 9 ? "01" : strIdentificadorEmisor.Length == 10 ? "02" : "03";
                            string strTipoIdentificacionReceptor = "";
                            if (detalle.IdentificacionReceptor != "")
                            {
                                string strIdentificadorReceptor = detalle.IdentificacionReceptor.TrimStart('0');
                                strTipoIdentificacionReceptor = strIdentificadorReceptor.Length == 9 ? "01" : strIdentificadorReceptor.Length == 10 ? "02" : "03";
                            }
                            AccesoDatos.Dominio.Entidades.DocumentoElectronico nuevoRegistro = new AccesoDatos.Dominio.Entidades.DocumentoElectronico();
                            nuevoRegistro.IdEmpresa = detalle.IdEmpresa;
                            nuevoRegistro.IdSucursal = intIdSucursal;
                            nuevoRegistro.IdTerminal = intIdTerminal;
                            nuevoRegistro.IdTipoDocumento = detalle.IdTipoDocumento;
                            nuevoRegistro.IdConsecutivo = detalle.IdConsecutivo;
                            nuevoRegistro.Fecha = detalle.Fecha;
                            nuevoRegistro.Consecutivo = detalle.Consecutivo;
                            nuevoRegistro.ClaveNumerica = detalle.ClaveNumerica;
                            nuevoRegistro.TipoIdentificacionEmisor = strTipoIdentificacionEmisor;
                            nuevoRegistro.IdentificacionEmisor = detalle.IdentificacionEmisor;
                            nuevoRegistro.TipoIdentificacionReceptor = strTipoIdentificacionReceptor;
                            nuevoRegistro.IdentificacionReceptor = detalle.IdentificacionReceptor;
                            nuevoRegistro.EsMensajeReceptor = strMensajeReceptor;
                            nuevoRegistro.DatosDocumento = detalle.DatosDocumento;
                            nuevoRegistro.Respuesta = detalle.Respuesta;
                            if (detalle.EstadoEnvio == "pendiente")
                                nuevoRegistro.EstadoEnvio = "procesando";
                            else
                                nuevoRegistro.EstadoEnvio = detalle.EstadoEnvio;
                            nuevoRegistro.ErrorEnvio = "";
                            nuevoRegistro.CorreoNotificacion = detalle.CorreoNotificacion;
                            string strId = await MigracionClient.AgregarDocumentoElectronico(nuevoRegistro);
                            rtbSalida.AppendText("Migración de documento electrónico IdLocal: " + detalle.IdDocumento + " IdServer: " + strId + "\n");
                        }
                        rtbSalida.AppendText("Registros de documentos electrónicos satisfactoriamente. . .\n");
                    }
                }
                rtbSalida.AppendText("FIN DEL PROCESO DE MIGRACION. . .\n");
                btnProcesar.Enabled = true;
            }
            catch (Exception ex)
            {
                rtbSalida.AppendText(ex.Message + "\n");
                rtbSalida.AppendText("PROCESO ABORTADO. . .\n");
                btnProcesar.Enabled = true;
            }
        }

        private void btnLimpiarRegistro_Click(object sender, EventArgs e)
        {
            rtbSalida.Text = "";
        }
    }
}
