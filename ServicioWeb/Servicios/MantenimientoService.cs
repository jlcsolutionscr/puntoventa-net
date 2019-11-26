using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;
using System.Text;
using LeandroSoftware.Core.Utilitario;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IMantenimientoService
    {
        // Métodos para administrar parametros del sistema
        IEnumerable<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo);
        IEnumerable<LlaveDescripcion> ObtenerListadoEmpresasAdministrador();
        IEnumerable<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId);
        void RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId);
        Usuario ValidarCredencialesAdmin(string strUsuario, string strClave);
        Usuario ValidarCredenciales(string strUsuario, string strClave, string id);
        Empresa ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro);
        void ActualizarVersionApp(string strVersion);
        string ObtenerUltimaVersionApp();
        // Métodos para administrar las empresas
        IEnumerable<LlaveDescripcion> ObtenerListadoEmpresa();
        IEnumerable<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa);
        IEnumerable<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal);
        string AgregarEmpresa(Empresa empresa);
        Empresa ObtenerEmpresa(int intIdEmpresa);
        void ActualizarEmpresa(Empresa empresa);
        void ActualizarEmpresaConDetalle(Empresa empresa);
        string ObtenerLogotipoEmpresa(int intIdEmpresa);
        void ActualizarLogoEmpresa(int intIdEmpresa, string strLogo);
        void ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado);
        // Métodos para administrar las sucursales
        SucursalPorEmpresa ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal);
        void AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal);
        void ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal);
        TerminalPorSucursal ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal);
        void AgregarTerminalPorSucursal(TerminalPorSucursal terminal);
        void ActualizarTerminalPorSucursal(TerminalPorSucursal terminal);
        // Métodos para administrar los usuarios del sistema
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave);
        void AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa);
        void EliminarUsuario(int intIdUsuario);
        Usuario ObtenerUsuario(int intIdUsuario);
        IEnumerable<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo);
        // Métodos para administrar los vendedores del sistema
        void AgregarVendedor(Vendedor vendedor);
        void ActualizarVendedor(Vendedor vendedor);
        void EliminarVendedor(int intIdVendedor);
        Vendedor ObtenerVendedor(int intIdVendedor);
        Vendedor ObtenerVendedorPorDefecto(int intIdEmpresa);
        IEnumerable<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre);
        // Métodos para administrar los roles del sistema
        Role ObtenerRole(int intIdRole);
        IEnumerable<LlaveDescripcion> ObtenerListadoRoles();
        // Métodos para administrar las líneas de producto
        void AgregarLinea(Linea linea);
        void ActualizarLinea(Linea linea);
        void EliminarLinea(int intIdLinea);
        Linea ObtenerLinea(int intIdLinea);
        IEnumerable<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion);
        // Métodos para administrar los productos
        IEnumerable<LlaveDescripcion> ObtenerListadoTipoProducto();
        IEnumerable<LlaveDescripcion> ObtenerListadoTipoExoneracion();
        IEnumerable<LlaveDescripcion> ObtenerListadoTipoImpuesto();
        ParametroImpuesto ObtenerParametroImpuesto(int intIdImpuesto);
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion, decimal decPorcentajeAumento);
        void EliminarProducto(int intIdProducto);
        Producto ObtenerProducto(int intIdProducto);
        Producto ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo);
        int ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion);
        IEnumerable<LlaveDescripcion> ObtenerListadoProductos(int intIdEmpresa, int numPagina, int cantRec, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion);
        int ObtenerTotalMovimientosPorProducto(int intIdProducto, DateTime datFechaInicial, DateTime datFechaFinal);
        IEnumerable<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int numPagina, int cantRec, DateTime datFechaInicial, DateTime datFechaFinal);
        // Métodos para obtener las condiciones de venta para facturación
        IEnumerable<LlaveDescripcion> ObtenerListadoCondicionVenta();
        // Métodos para obtener las formas de pago por tipo de servicio
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoFactura();
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoCompra();
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoEgreso();
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoIngreso();
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoMovimientoCxC();
        IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoMovimientoCxP();
        // Métodos para administrar los parámetros de banco adquiriente
        void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void EliminarBancoAdquiriente(int intIdBanco);
        BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco);
        IEnumerable<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion);
        // Métodos para administrar los parámetros de tipo de moneda
        TipoMoneda AgregarTipoMoneda(TipoMoneda tipoMoneda);
        void ActualizarTipoMoneda(TipoMoneda tipoMoneda);
        void EliminarTipoMoneda(int intIdTipoMoneda);
        TipoMoneda ObtenerTipoMoneda(int intIdTipoMoneda);
        IEnumerable<LlaveDescripcion> ObtenerListadoTipoMoneda();
        // Métodos para administrar los ajustes de inventario
        AjusteInventario AgregarAjusteInventario(AjusteInventario ajusteInventario);
        void ActualizarAjusteInventario(AjusteInventario ajusteInventario);
        void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario);
        AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario);
        int ObtenerTotalListaAjustes(int intIdEmpresa, int intIdAjusteInventario, string strDescripcion);
        IEnumerable<LlaveDescripcion> ObtenerListadoAjustes(int intIdEmpresa, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion);
        // Métodos para obtener parámetros generales del sistema
        IEnumerable<LlaveDescripcion> ObtenerListadoTipoIdentificacion();
        IEnumerable<LlaveDescripcion> ObtenerListadoCatalogoReportes();
        CatalogoReporte ObtenerCatalogoReporte(int intIdReporte);
        IEnumerable<LlaveDescripcion> ObtenerListadoProvincias();
        IEnumerable<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia);
        IEnumerable<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton);
        IEnumerable<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito);
        IList<LlaveDescripcion> ObtenerListadoTipodePrecio();
        void ValidarRegistroAutenticacion(string strToken, int intRole);
        void EliminarRegistroAutenticacionInvalidos();
    }

    public class MantenimientoService : IMantenimientoService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MantenimientoService(IUnityContainer Container)
        {
            try
            {
                localContainer = Container;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Mantenimiento. Por favor consulte con su proveedor.");
            }
        }

        public IEnumerable<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEquipoRegistrado = new List<EquipoRegistrado>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    UsuarioPorEmpresa usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.Include("Usuario").FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                    if (strUsuario.ToUpper() != "JASLOP" && usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa indicada. Por favor verifique la información suministrada.");
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "JASLOP")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (!usuario.PermiteRegistrarDispositivo) throw new BusinessException("El usuario suministrado no esta autorizado para registrar el punto de venta. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listado = dbContext.TerminalPorSucursalRepository.Include("SucursalPorEmpresa").Where(x => x.IdEmpresa == empresa.IdEmpresa && x.ValorRegistro == "" && x.IdTipoDispositivo == intTipoDispositivo)
                        .OrderBy(x => x.IdSucursal).ThenBy(x => x.IdTerminal);
                    foreach (var terminal in listado)
                    {
                        EquipoRegistrado item = new EquipoRegistrado
                        {
                            IdSucursal = terminal.IdSucursal,
                            IdTerminal = terminal.IdTerminal,
                            NombreSucursal = terminal.SucursalPorEmpresa.NombreSucursal,
                            DireccionSucursal = terminal.SucursalPorEmpresa.Direccion,
                            TelefonoSucursal = terminal.SucursalPorEmpresa.Telefono,
                            ImpresoraFactura = terminal.ImpresoraFactura
                        };
                        listaEquipoRegistrado.Add(item);
                    }
                    return listaEquipoRegistrado;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de terminales disponibles: ", ex);
                    throw new Exception("Error al obtener las terminales disponibles. . .");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoEmpresasAdministrador()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.EmpresaRepository.AsQueryable();
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdEmpresa, value.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar la lista de empresas para usuario administrador: ", ex);
                    throw new Exception("Error al validar la lista de empresas para usuario administrador. . .");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    foreach (SucursalPorEmpresa value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.Empresa.IdEmpresa, value.Empresa.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar la lista de empresas por valor de registro: ", ex);
                    throw new Exception("Error al validar la lista de empresas por valor de registro. . .");
                }
            }
        }
        
        public IEnumerable<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    foreach (TerminalPorSucursal value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.Empresa.IdEmpresa, value.Empresa.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar la lista de empresas por valor de registro: ", ex);
                    throw new Exception("Error al validar la lista de empresas por valor de registro. . .");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TerminalPorSucursalRepository.Include("Empresa").Where(x => x.Empresa.PermiteFacturar && x.ValorRegistro == strDispositivoId);
                    foreach (TerminalPorSucursal value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.Empresa.IdEmpresa, value.Empresa.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar la lista de empresas por valor de registro: ", ex);
                    throw new Exception("Error al validar la lista de empresas por valor de registro. . .");
                }
            }
        }

        public void RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    UsuarioPorEmpresa usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.Include("Usuario").FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                    if (strUsuario.ToUpper() != "JASLOP" && usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa indicada. Por favor verifique la información suministrada.");
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "JASLOP")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (!usuario.PermiteRegistrarDispositivo) throw new BusinessException("El usuario suministrado no esta autorizado para registrar el punto de venta. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == intIdSucursal);
                    if (sucursal == null) throw new BusinessException("La sucursal donde desea registrar su punto de venta no existe. Por favor, pongase en contacto con su proveedor del servicio.");
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == intIdSucursal && x.IdTerminal == intIdTerminal && x.IdTipoDispositivo == intTipoDispositivo);
                    if (terminal == null) throw new BusinessException("La terminal donde desea registrar su punto de venta no existe. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (terminal.ValorRegistro == "")
                    {
                        terminal.ValorRegistro = strDispositivoId;
                        dbContext.NotificarModificacion(terminal);
                        dbContext.Commit();
                    } else
                    {
                        throw new BusinessException("La terminal donde desea registrar su punto de venta ya en uso. Por favor, pongase en contacto con su proveedor del servicio.");
                    }
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al registrar el dispositivo movil para la identificación suministrada: ", ex);
                    throw new Exception("Error al registrar el dispositivo movil para la identificación suministrada.");
                }
            }
        }

        public Usuario ValidarCredencialesAdmin(string strUsuario, string strClave)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Where(x => x.CodigoUsuario == strUsuario.ToUpper()).FirstOrDefault();
                    if (usuario == null) throw new BusinessException("Usuario no registrado. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (usuario.IdUsuario != 1) throw new BusinessException("Los credenciales suministrados no corresponden al usuario administrador. Por favor verifique la información suministrada.");
                    string strToken = GenerarRegistroAutenticacion(StaticRolePorUsuario.ADMINISTRADOR);
                    usuario.Token = strToken;
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar credenciales del usuario: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public Usuario ValidarCredenciales(string strUsuario, string strClave, string strIdentificacion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    Usuario usuario = null;
                    UsuarioPorEmpresa usuarioEmpresa = null;
                    if (strUsuario.ToUpper() == "JASLOP")
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa);
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                        if (usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa indicada. Por favor verifique la información suministrada.");
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    usuario.UsuarioPorEmpresa = new HashSet<UsuarioPorEmpresa>();
                    usuarioEmpresa.Usuario = null;
                    Empresa refEmpresa = new Empresa();
                    refEmpresa.NombreEmpresa = empresa.NombreEmpresa;
                    refEmpresa.Identificacion = empresa.Identificacion;
                    usuarioEmpresa.Empresa = refEmpresa;
                    usuario.UsuarioPorEmpresa.Add(usuarioEmpresa);
                    int tipoRole = strUsuario.ToUpper() == "JASLOP" ? StaticRolePorUsuario.ADMINISTRADOR : StaticRolePorUsuario.USUARIO_SISTEMA;
                    string strToken = GenerarRegistroAutenticacion(tipoRole);
                    usuario.Token = strToken;
                    foreach (RolePorUsuario role in usuario.RolePorUsuario)
                    {
                        role.Usuario = null;
                    }
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    log.Error("Error al validar credenciales para la identificación: " + strIdentificacion + " Error: " + ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar los credenciales del usuario por identificación: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public Empresa ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = null;
                    UsuarioPorEmpresa usuarioEmpresa = null;
                    if (strUsuario.ToUpper() == "JASLOP")
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                        if (usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa indicada. Por favor verifique la información suministrada.");
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                    TerminalPorSucursal terminal = null;
                    SucursalPorEmpresa sucursal = null;
                    if (strUsuario.ToUpper() == "JASLOP")
                    {
                        terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                        sucursal = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == terminal.IdTerminal).FirstOrDefault();
                    }
                    else
                    {
                        terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.ValorRegistro == strValorRegistro).FirstOrDefault();
                        if (terminal == null) throw new BusinessException("El dispositivo no se encuentra registrado en el sistema.");
                        sucursal = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == terminal.IdSucursal).FirstOrDefault();
                    }
                    if (terminal == null || sucursal == null) throw new BusinessException("La terminal o dispositivo movil no se encuentra registrado para la empresa suministrada.");
                    EquipoRegistrado equipo = new EquipoRegistrado
                    {
                        IdSucursal = sucursal.IdSucursal,
                        IdTerminal = terminal.IdTerminal,
                        NombreSucursal = sucursal.NombreSucursal,
                        DireccionSucursal = sucursal.Direccion,
                        TelefonoSucursal = sucursal.Telefono,
                        ImpresoraFactura = terminal.ImpresoraFactura
                    };
                    usuario.UsuarioPorEmpresa = new HashSet<UsuarioPorEmpresa>();
                    string strToken = GenerarRegistroAutenticacion(StaticRolePorUsuario.USUARIO_SISTEMA);
                    usuario.Token = strToken;
                    foreach (ReportePorEmpresa reporte in empresa.ReportePorEmpresa)
                        reporte.Empresa = null;
                    foreach (RolePorUsuario role in usuario.RolePorUsuario)
                    {
                        role.Usuario = null;
                    }
                    empresa.Logotipo = null;
                    terminal.Empresa = null;
                    empresa.Certificado = null;
                    empresa.AccessToken = null;
                    empresa.RefreshToken = null;
                    empresa.EmitedAt = null;
                    empresa.ExpiresIn = null;
                    empresa.RefreshExpiresIn = null;
                    empresa.EquipoRegistrado = equipo;
                    empresa.Usuario = usuario;
                    return empresa;
                }
                catch (BusinessException ex)
                {
                    log.Error("Error al validar credenciales para la terminal: " + strValorRegistro + " Error: " + ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar los credenciales del usuario por terminal: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public void ActualizarVersionApp(string strVersion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.Descripcion == "Version").FirstOrDefault();
                    if (version is null) throw new BusinessException("No se logró obtener el parámetro con descripción 'Version' de la tabla de parámetros del sistema.");
                    version.Valor = strVersion;
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la empresa: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public string ObtenerUltimaVersionApp()
        {
            string strUltimaVersion = "";
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.Descripcion == "Version").FirstOrDefault();
                    if (version is null) throw new BusinessException("No se logró obtener el parámetro con descripción 'Version' de la tabla de parámetros del sistema.");
                    strUltimaVersion = version.Valor;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al consultar el parámetro 'Version' del sistema: ", ex);
                    throw new Exception("Se produjo un error consultado la versión actual del sistema. Por favor consulte con su proveedor.");
                }
            }
            return strUltimaVersion;
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoEmpresa()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.EmpresaRepository.Select(x => new { x.IdEmpresa, x.NombreComercial });
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdEmpresa, value.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar la lista de empresas para usuario administrador: ", ex);
                    throw new Exception("Error al validar la lista de empresas para usuario administrador. . .");
                }
            }
        }

        public string AgregarEmpresa(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.EmpresaRepository.Add(empresa);
                    dbContext.Commit();
                    return empresa.IdEmpresa.ToString();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la empresa: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarEmpresa(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    byte[] certificado = dbContext.EmpresaRepository.AsNoTracking().Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault().Certificado;
                    if (certificado != null) empresa.Certificado = certificado;
                    dbContext.NotificarModificacion(empresa);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la empresa: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarEmpresaConDetalle(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    byte[] certificado = dbContext.EmpresaRepository.AsNoTracking().Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault().Certificado;
                    List<ReportePorEmpresa> listadoReportePorEmpresa = empresa.ReportePorEmpresa.OrderBy(o => o.IdReporte).ToList();
                    empresa.ReportePorEmpresa = null;
                    empresa.Barrio = null;
                    if (certificado != null) empresa.Certificado = certificado;
                    dbContext.NotificarModificacion(empresa);
                    List<ReportePorEmpresa> listadoReportePorEmpresaAnt = dbContext.ReportePorEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa).ToList();
                    foreach (ReportePorEmpresa reporte in listadoReportePorEmpresaAnt)
                        dbContext.ReportePorEmpresaRepository.Remove(reporte);
                    foreach (ReportePorEmpresa reporte in listadoReportePorEmpresa)
                        dbContext.ReportePorEmpresaRepository.Add(reporte);
                    empresa.Barrio = null;
                    dbContext.NotificarModificacion(empresa);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la empresa: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public Empresa ObtenerEmpresa(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    empresa.Certificado = null;
                    empresa.AccessToken = null;
                    empresa.RefreshToken = null;
                    empresa.EmitedAt = null;
                    empresa.ExpiresIn = null;
                    empresa.RefreshExpiresIn = null;
                    foreach (ReportePorEmpresa reporte in empresa.ReportePorEmpresa)
                        reporte.Empresa = null;
                    return empresa;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public string ObtenerLogotipoEmpresa(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    string strLogotipo = "";
                    if (empresa.Logotipo != null)
                        strLogotipo = Convert.ToBase64String(empresa.Logotipo);
                    return strLogotipo;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarLogoEmpresa(int intIdEmpresa, string strLogo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (strLogo != "")
                    {
                        byte[] bytLogotipo = Convert.FromBase64String(strLogo);
                        empresa.Logotipo = bytLogotipo;
                    }
                    else
                        empresa.Logotipo = null;
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al actualizar el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error registrando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    byte[] bytCertificado = Convert.FromBase64String(strCertificado);
                    empresa.Certificado = bytCertificado;
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al actualizar el certificado de la empresa: ", ex);
                    throw new Exception("Se produjo un error registrando el certificado de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public CatalogoReporte ObtenerCatalogoReporte(int intIdReporte)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CatalogoReporteRepository.FirstOrDefault(x => x.IdReporte == intIdReporte);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la información del catalogo de reporte: ", ex);
                    throw new Exception("Se produjo un error consultando la parametrización de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public SucursalPorEmpresa ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal).FirstOrDefault();
                    return sucursal;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la información de la sucursal: ", ex);
                    throw new Exception("Se produjo un error al obtener la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.SucursalPorEmpresaRepository.Add(sucursal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la sucursal: ", ex);
                    throw new Exception("Se produjo un error adicionando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(sucursal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la sucursal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public TerminalPorSucursal ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdTerminal == intIdTerminal).FirstOrDefault();
                    return terminal;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la información de la terminal: ", ex);
                    throw new Exception("Se produjo un error al obtener la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.TerminalPorSucursalRepository.Add(terminal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la terminal: ", ex);
                    throw new Exception("Se produjo un error adicionando la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(terminal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la terminal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "JASLOP") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    List<UsuarioPorEmpresa> empresaUsuario = usuario.UsuarioPorEmpresa.ToList();
                    if (empresaUsuario.Count == 0) throw new BusinessException("El usuario por agregar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(empresaUsuario[0].IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    UsuarioPorEmpresa usuarioExistente = dbContext.UsuarioPorEmpresaRepository.Include("Usuario").Where(x => x.IdEmpresa == empresa.IdEmpresa && x.Usuario.CodigoUsuario.Contains(usuario.CodigoUsuario.ToUpper())).FirstOrDefault();
                    if (usuarioExistente != null) throw new BusinessException("El código de usuario que desea agregar ya existe para la empresa suministrada.");
                    usuario.Clave = usuario.Clave;
                    dbContext.UsuarioRepository.Add(usuario);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el usuario: ", ex);
                    throw new Exception("Se produjo un error agregando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "JASLOP") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    UsuarioPorEmpresa usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.Where(x => x.IdUsuario == usuario.IdUsuario).FirstOrDefault();
                    if (usuarioEmpresa == null) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuarioEmpresa.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<RolePorUsuario> listadoDetalleAnterior = dbContext.RolePorUsuarioRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    List<RolePorUsuario> listadoDetalle = usuario.RolePorUsuario.ToList();
                    usuario.UsuarioPorEmpresa = null;
                    usuario.RolePorUsuario = null;
                    foreach (RolePorUsuario detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(usuario);
                    foreach (RolePorUsuario detalle in listadoDetalle)
                        dbContext.RolePorUsuarioRepository.Add(detalle);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("UsuarioPorEmpresa").Where(x => x.IdUsuario == intIdUsuario).FirstOrDefault();
                    if (usuario == null) throw new Exception("El usuario seleccionado para la actualización de la clave no existe.");
                    if (usuario.UsuarioPorEmpresa.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.UsuarioPorEmpresa.ToList()[0].IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    usuario.Clave = strClave;
                    dbContext.NotificarModificacion(usuario);
                    dbContext.Commit();
                    usuario.UsuarioPorEmpresa = null;
                    return usuario;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la contraseña del usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la contraseña del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    UsuarioPorEmpresa usuarioEmpresa = new UsuarioPorEmpresa
                    {
                        IdUsuario = intIdUsuario,
                        IdEmpresa = intIdEmpresa
                    };
                    dbContext.UsuarioPorEmpresaRepository.Add(usuarioEmpresa);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de usuario por empresa: ", ex);
                    throw new Exception("Se produjo un error agregando el registro de usuario por empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarUsuario(int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Where(x => x.IdUsuario == intIdUsuario).FirstOrDefault();
                    if (usuario == null)
                        throw new BusinessException("El usuario por eliminar no existe.");
                    List<UsuarioPorEmpresa> listaEmpresa = dbContext.UsuarioPorEmpresaRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    if (listaEmpresa.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.UsuarioPorEmpresa.FirstOrDefault().IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<RolePorUsuario> listaRole = dbContext.RolePorUsuarioRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    foreach (RolePorUsuario roleUsuario in listaRole)
                        dbContext.NotificarEliminacion(roleUsuario);
                    foreach (UsuarioPorEmpresa empresaUsuario in listaEmpresa)
                        dbContext.NotificarEliminacion(empresaUsuario);
                    dbContext.NotificarEliminacion(usuario);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar el usuario: ", ex);
                    throw new BusinessException("No es posible eliminar el usuario seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el usuario: ", ex);
                    throw new Exception("Se produjo un error eliminando al usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ObtenerUsuario(int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").Include("UsuarioPorEmpresa").FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null)
                        throw new BusinessException("El usuario por consultar no existe");
                    foreach (RolePorUsuario roleUsuario in usuario.RolePorUsuario)
                        roleUsuario.Usuario = null;
                    foreach (UsuarioPorEmpresa usuarioEmpresa in usuario.UsuarioPorEmpresa)
                        usuarioEmpresa.Usuario = null;
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el usuario: ", ex);
                    throw new Exception("Se produjo un error consultando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaUsuario = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.UsuarioPorEmpresaRepository.Include("Usuario").Where(x => x.IdEmpresa == intIdEmpresa && x.IdUsuario > 1);
                    if (!strCodigo.Equals(string.Empty))
                        listado = listado.Where(x => x.Usuario.CodigoUsuario.Contains(strCodigo.ToUpper()));
                    listado.OrderBy(x => x.Usuario.IdUsuario);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdUsuario, value.Usuario.CodigoUsuario);
                        listaUsuario.Add(item);
                    }
                    return listaUsuario;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de usuarios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de usuarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarVendedor(Vendedor vendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.VendedorRepository.Add(vendedor);
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
                    log.Error("Error al agregar el vendedor: ", ex);
                    throw new Exception("Se produjo un error agregando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarVendedor(Vendedor vendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(vendedor);
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
                    log.Error("Error al actualizar el vendedor: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarVendedor(int intIdVendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Vendedor vendedor = dbContext.VendedorRepository.Find(intIdVendedor);
                    if (vendedor == null)
                        throw new BusinessException("El vendedor por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.VendedorRepository.Remove(vendedor);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al eliminar el vendedor: ", ex);
                    throw new BusinessException("No es posible eliminar el vendedor seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el vendedor: ", ex);
                    throw new Exception("Se produjo un error eliminando al vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Vendedor ObtenerVendedor(int intIdVendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Vendedor Vendedor = dbContext.VendedorRepository.Find(intIdVendedor);
                    if (Vendedor == null)
                        throw new BusinessException("El Vendedor por consultar no existe");
                    return Vendedor;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el vendedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Vendedor ObtenerVendedorPorDefecto(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Vendedor Vendedor = dbContext.VendedorRepository.Where(x => x.IdEmpresa == intIdEmpresa).FirstOrDefault();
                    if (Vendedor == null)
                        throw new BusinessException("La empresa no posee registrado ningún vendedor");
                    return Vendedor;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el vendedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaVendedor = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.VendedorRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listado = listado.Where(x => x.Nombre.Contains(strNombre));
                    listado = listado.OrderBy(x => x.IdVendedor);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdVendedor, value.Nombre);
                        listaVendedor.Add(item);
                    }
                    return listaVendedor;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de vendedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de vendedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public Role ObtenerRole(int intIdRole)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.RoleRepository.Find(intIdRole);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de role de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando la información del role de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoRoles()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaRoles = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.RoleRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdRole, value.Descripcion);
                        listaRoles.Add(item);
                    }
                    return listaRoles;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de roles de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de roles de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarLinea(Linea linea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.LineaRepository.Add(linea);
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
                    log.Error("Error al agregar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarLinea(Linea linea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.NotificarModificacion(linea);
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
                    log.Error("Error al actualizar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarLinea(int intIdLinea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Linea linea = dbContext.LineaRepository.Find(intIdLinea);
                    if (linea == null)
                        throw new BusinessException("La línea por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.LineaRepository.Remove(linea);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar la línea seleccionada. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error eliminando la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public Linea ObtenerLinea(int intIdLinea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.LineaRepository.Find(intIdLinea);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la línea de producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaLinea = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.LineaRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderBy(x => x.IdLinea).ThenBy(x => x.Descripcion);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdLinea, value.Descripcion);
                        listaLinea.Add(item);
                    }
                    return listaLinea;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de líneas general: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de líneas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoTipoProducto()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTipoProducto = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TipoProductoRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTipoProducto, value.Descripcion);
                        listaTipoProducto.Add(item);
                    }
                    return listaTipoProducto;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoTipoExoneracion()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTipoExoneracion = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ParametroExoneracionRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTipoExoneracion, value.Descripcion);
                        listaTipoExoneracion.Add(item);
                    }
                    return listaTipoExoneracion;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la lista de parametros de exoneración: ", ex);
                    throw new Exception("Se produjo un error consultando la lista de parametros de exoneración. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoTipoImpuesto()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTipoImpuesto = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ParametroImpuestoRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdImpuesto, value.Descripcion);
                        listaTipoImpuesto.Add(item);
                    }
                    return listaTipoImpuesto;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public ParametroImpuesto ObtenerParametroImpuesto(int intIdImpuesto)
        {
            ParametroImpuesto parametroImpuesto = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    parametroImpuesto = dbContext.ParametroImpuestoRepository.Find(intIdImpuesto);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro para el tipo de impuesto: ", ex);
                    throw new Exception("Se produjo un error consultando el. Por favor consulte con su proveedor.");
                }
            }
            return parametroImpuesto;
        }

        public void AgregarProducto(Producto producto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    bool existe = dbContext.ProductoRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.Codigo == producto.Codigo).Count() > 0;
                    if (existe) throw new BusinessException("El código de producto ingresado ya está registrado en la empresa.");
                    dbContext.ProductoRepository.Add(producto);
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
                    log.Error("Error al agregar el producto: ", ex);
                    throw new Exception("Se produjo un error agregando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    bool existe = dbContext.ProductoRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.Codigo == producto.Codigo && x.IdProducto != producto.IdProducto).Count() > 0;
                    if (existe) throw new BusinessException("El código de producto ingresado ya está registrado en la empresa.");
                    producto.ParametroImpuesto = null;
                    dbContext.NotificarModificacion(producto);
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
                    log.Error("Error al actualizar el producto: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion, decimal decPorcentajeAumento)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    else if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    else if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    listaProductos = listaProductos.OrderBy(x => x.IdProducto);
                    foreach (Producto producto in listaProductos)
                    {
                        producto.PrecioVenta1 = producto.PrecioVenta1 * (1 + (decPorcentajeAumento / 100));
                        producto.PrecioVenta2 = producto.PrecioVenta2 * (1 + (decPorcentajeAumento / 100));
                        producto.PrecioVenta3 = producto.PrecioVenta3 * (1 + (decPorcentajeAumento / 100));
                        producto.PrecioVenta4 = producto.PrecioVenta4 * (1 + (decPorcentajeAumento / 100));
                        producto.PrecioVenta5 = producto.PrecioVenta5 * (1 + (decPorcentajeAumento / 100));
                        dbContext.NotificarModificacion(producto);                        
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
                    log.Error("Error al actualizar el precio de venta del inventario: ", ex);
                    throw new Exception("Se produjo un error actualizando el precio de venta del inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarProducto(int intIdProducto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Find(intIdProducto);
                    if (producto == null) throw new BusinessException("El producto por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ProductoRepository.Remove(producto);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar el producto seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el producto: ", ex);
                    throw new Exception("Se produjo un error eliminando el producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProducto(int intIdProducto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("ParametroImpuesto").Include("Proveedor").FirstOrDefault(x => x.IdProducto == intIdProducto);
                    producto.Proveedor.Producto = null;
                    foreach (MovimientoProducto movimiento in producto.MovimientoProducto)
                        movimiento.Producto = null;
                    return producto;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProductoRepository.Include("ParametroImpuesto").Where(x => x.IdEmpresa == intIdEmpresa && x.Codigo.Equals(strCodigo)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (!bolIncluyeServicios)
                        listaProductos = listaProductos.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    return listaProductos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoProductos(int intIdEmpresa, int numPagina, int cantRec, bool bolIncluyeServicios, int intIdLinea, string strCodigo, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaProducto = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (intIdLinea > 0)
                        listado = listado.Where(x => x.IdLinea == intIdLinea);
                    if (!strCodigo.Equals(string.Empty))
                        listado = listado.Where(x => x.Codigo.Contains(strCodigo));
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (!bolIncluyeServicios)
                        listado = listado.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    if (cantRec > 0)
                        listado = listado.OrderBy(x => x.Codigo).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    else
                        listado = listado.OrderBy(x => x.Codigo);
                    foreach (Producto value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdProducto, value.Codigo + "   -   " + value.Descripcion);
                        listaProducto.Add(item);
                    }
                    return listaProducto;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalMovimientosPorProducto(int intIdProducto, DateTime datFechaInicial, DateTime datFechaFinal)
        {
            DateTime datFechaInicio = new DateTime(datFechaInicial.Year, datFechaInicial.Month, datFechaInicial.Day, 0, 0, 0);
            DateTime datFechaFin = new DateTime(datFechaFinal.Year, datFechaFinal.Month, datFechaFinal.Day, 23, 59, 59);
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoProductoRepository.Where(x => x.IdProducto == intIdProducto && x.Fecha > datFechaInicio && x.Fecha < datFechaFin);
                    return listaMovimientos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int numPagina, int cantRec, DateTime datFechaInicial, DateTime datFechaFinal)
        {
            DateTime datFechaInicio = new DateTime(datFechaInicial.Year, datFechaInicial.Month, datFechaInicial.Day, 0, 0, 0);
            DateTime datFechaFin = new DateTime(datFechaFinal.Year, datFechaFinal.Month, datFechaFinal.Day, 23, 59, 59);
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoProductoRepository.Where(x => x.IdProducto == intIdProducto && x.Fecha >= datFechaInicio && x.Fecha <= datFechaFin);
                    if (cantRec > 0)
                        return listaMovimientos.OrderByDescending(x => x.Fecha).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    else
                        return listaMovimientos.OrderByDescending(x => x.Fecha).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoCondicionVenta()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCondicionVenta = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CondicionVentaRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCondicionVenta, value.Descripcion);
                        listaCondicionVenta.Add(item);
                    }
                    return listaCondicionVenta;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de condiciones de venta para facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de condiciones de venta. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoFactura()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoFactura = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoFactura.Add(item);
                    }
                    return listaFormaPagoFactura;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoCompra()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoCompra = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoCompra.Add(item);
                    }
                    return listaFormaPagoCompra;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para compras: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoEgreso()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoEgreso = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoEgreso.Add(item);
                    }
                    return listaFormaPagoEgreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para egresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoIngreso()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoIngreso = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoIngreso.Add(item);
                    }
                    return listaFormaPagoIngreso;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para egresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoMovimientoCxC()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoCxC = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoCxC.Add(item);
                    }
                    return listaFormaPagoCxC;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para movimientos de cuentas por cobrar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoFormaPagoMovimientoCxP()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaFormaPagoCxP = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdFormaPago, value.Descripcion);
                        listaFormaPagoCxP.Add(item);
                    }
                    return listaFormaPagoCxP;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para movimientos de cuentas por pagar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.BancoAdquirienteRepository.Add(bancoAdquiriente);
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
                    log.Error("Error al agregar el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error agregando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(bancoAdquiriente);
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
                    log.Error("Error al actualizar el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarBancoAdquiriente(int intIdBanco)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(intIdBanco);
                    if (banco == null) throw new BusinessException("El banco adquiriente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(banco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.BancoAdquirienteRepository.Remove(banco);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar el banco adquiriente seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error eliminando el banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.BancoAdquirienteRepository.Find(intIdBanco);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaBancoAdquiriente = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.BancoAdquirienteRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (strDescripcion != "")
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdBanco, value.Descripcion);
                        listaBancoAdquiriente.Add(item);
                    }
                    return listaBancoAdquiriente;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de bancos adquirientes. Por favor consulte con su proveedor.");
                }
            }
        }

        public TipoMoneda AgregarTipoMoneda(TipoMoneda tipoMoneda)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.TipoMonedaRepository.Add(tipoMoneda);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de tipo de moneda: ", ex);
                    throw new Exception("Se produjo un error agregando la información del tipo de moneda. Por favor consulte con su proveedor.");
                }
            }
            return tipoMoneda;
        }

        public void ActualizarTipoMoneda(TipoMoneda tipoMoneda)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(tipoMoneda);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el registro de tipo de moneda: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del tipo de moneda. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarTipoMoneda(int intIdTipoMoneda)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    TipoMoneda tipo = dbContext.TipoMonedaRepository.Find(intIdTipoMoneda);
                    if (tipo == null)
                        throw new BusinessException("El parametro tipo de moneda por eliminar no existe");
                    dbContext.TipoMonedaRepository.Remove(tipo);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar el tipo de moneda seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el registro de tipo de moneda: ", ex);
                    throw new Exception("Se produjo un error eliminando el tipo de moneda. Por favor consulte con su proveedor.");
                }
            }
        }

        public TipoMoneda ObtenerTipoMoneda(int intIdTipoMoneda)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoMonedaRepository.Find(intIdTipoMoneda);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de tipo de moneda: ", ex);
                    throw new Exception("Se produjo un error consultando la información del tipo de moneda. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoTipoMoneda()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTipoMoneda = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TipoMonedaRepository.AsQueryable();
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTipoMoneda, value.Descripcion);
                        listaTipoMoneda.Add(item);
                    }
                    return listaTipoMoneda;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de tipos de moneda: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de moneda. Por favor consulte con su proveedor.");
                }
            }
        }

        public AjusteInventario AgregarAjusteInventario(AjusteInventario ajusteInventario)
        {
            //decimal decTotalAjustePorLinea = 0;
            //ParametroContable lineaParam = null;
            DataTable dtbInventarios = new DataTable();
            dtbInventarios.Columns.Add("IdLinea", typeof(int));
            dtbInventarios.Columns.Add("Total", typeof(decimal));
            dtbInventarios.PrimaryKey = new DataColumn[] { dtbInventarios.Columns[0] };
            //CuentaPorCobrar cuentaPorCobrar = null;
            //Asiento asiento = null;
            //MovimientoBanco movimientoBanco = null;
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    //if (empresa.Contabiliza)
                    //{
                    //    ingresosVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IngresosPorVentas).FirstOrDefault();
                    //    costoVentasParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CostosDeVentas).FirstOrDefault();
                    //    ivaPorPagarParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.IVAPorPagar).FirstOrDefault();
                    //    efectivoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.Efectivo).FirstOrDefault();
                    //    cuentaPorCobrarClientesParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarClientes).FirstOrDefault();
                    //    cuentaPorCobrarTarjetaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentasPorCobrarTarjeta).FirstOrDefault();
                    //    gastoComisionParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.GastoComisionTarjeta).FirstOrDefault();
                    //    if (ingresosVentasParam == null || costoVentasParam == null || ivaPorPagarParam == null || efectivoParam == null || cuentaPorCobrarClientesParam == null || cuentaPorCobrarTarjetaParam == null || gastoComisionParam == null)
                    //        throw new BusinessException("La parametrización contable está incompleta y no se puede continuar. Por favor verificar.");
                    //}
                    dbContext.AjusteInventarioRepository.Add(ajusteInventario);
                    foreach (var detalleAjuste in ajusteInventario.DetalleAjusteInventario)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Salida : StaticTipoMovimientoProducto.Entrada,
                                Origen = "Registro de ajuste de inventario.",
                                Referencia = detalleAjuste.IdAjuste.ToString(),
                                Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                                PrecioCosto = detalleAjuste.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad += detalleAjuste.Cantidad;
                            dbContext.NotificarModificacion(producto);
                        }
                        //if (empresa.Contabiliza)
                        //{
                        //    if (producto.Tipo == StaticTipoProducto.Producto)
                        //    {
                        //        decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                        //        decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                        //        if (!producto.Excento)
                        //            decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (factura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                        //        decTotalMercancia += decTotalPorLinea;
                        //        decTotalCostoVentas += producto.PrecioCosto * detalleFactura.Cantidad;
                        //        int intExiste = dtbInventarios.Rows.IndexOf(dtbInventarios.Rows.Find(producto.Linea.IdLinea));
                        //        if (intExiste >= 0)
                        //            dtbInventarios.Rows[intExiste]["Total"] = (decimal)dtbInventarios.Rows[intExiste]["Total"] + (producto.PrecioCosto * detalleFactura.Cantidad);
                        //        else
                        //        {
                        //            DataRow data = dtbInventarios.NewRow();
                        //            data["IdLinea"] = producto.Linea.IdLinea;
                        //            data["Total"] = producto.PrecioCosto * detalleFactura.Cantidad;
                        //            dtbInventarios.Rows.Add(data);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        decimal decTotalPorLinea = detalleFactura.PrecioVenta * detalleFactura.Cantidad;
                        //        decTotalPorLinea = Math.Round(decTotalPorLinea - (factura.Descuento / decSubTotalFactura * decTotalPorLinea), 2, MidpointRounding.AwayFromZero);
                        //        if (!producto.Excento)
                        //            decTotalPorLinea = Math.Round(decTotalPorLinea / (1 + (factura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero);
                        //        decTotalServicios += decTotalPorLinea;
                        //        int intExiste = dtbIngresosPorServicios.Rows.IndexOf(dtbIngresosPorServicios.Rows.Find(producto.Linea.IdLinea));
                        //        if (intExiste >= 0)
                        //            dtbIngresosPorServicios.Rows[intExiste]["Total"] = (decimal)dtbIngresosPorServicios.Rows[intExiste]["Total"] + decTotalPorLinea;
                        //        else
                        //        {
                        //            DataRow data = dtbIngresosPorServicios.NewRow();
                        //            data["IdLinea"] = detalleFactura.Producto.Linea.IdLinea;
                        //            data["Total"] = Math.Round(decTotalPorLinea, 2, MidpointRounding.AwayFromZero);
                        //            dtbIngresosPorServicios.Rows.Add(data);
                        //        }
                        //    }
                        //}
                    }
                    //if (empresa.Contabiliza)
                    //{
                    //    decimal decTotalDiff = decTotalMercancia + decTotalImpuesto + decTotalServicios - factura.Total;
                    //    if (decTotalDiff != 0)
                    //    {
                    //        if (decTotalDiff >= 1 || decTotalDiff <= -1)
                    //            throw new Exception("La diferencia de ajuste sobrepasa el valor permitido.");
                    //        if (decTotalMercancia > 0)
                    //            decTotalMercancia -= decTotalDiff;
                    //        else
                    //        {
                    //            dtbIngresosPorServicios.Rows[0]["Total"] = (decimal)dtbIngresosPorServicios.Rows[0]["Total"] - decTotalDiff;
                    //            decTotalServicios -= decTotalDiff;
                    //        }
                    //    }
                    //    int intLineaDetalleAsiento = 0;
                    //    asiento = new Asiento();
                    //    asiento.IdEmpresa = factura.IdEmpresa;
                    //    asiento.Fecha = factura.Fecha;
                    //    asiento.TotalCredito = 0;
                    //    asiento.TotalDebito = 0;
                    //    asiento.Detalle = "Registro de venta de mercancía de Factura nro. ";
                    //    DetalleAsiento detalleAsiento = null;
                    //    foreach (var desglosePago in factura.DesglosePagoFactura)
                    //    {
                    //        if (desglosePago.IdFormaPago == StaticFormaPago.Efectivo)
                    //        {
                    //            detalleAsiento = new DetalleAsiento();
                    //            intLineaDetalleAsiento += 1;
                    //            detalleAsiento.Linea = intLineaDetalleAsiento;
                    //            detalleAsiento.IdCuenta = efectivoParam.IdCuenta;
                    //            detalleAsiento.Debito = desglosePago.MontoLocal;
                    //            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //            asiento.DetalleAsiento.Add(detalleAsiento);
                    //            asiento.TotalDebito += detalleAsiento.Debito;
                    //        }
                    //        else if (desglosePago.IdFormaPago == StaticFormaPago.Credito)
                    //        {
                    //            detalleAsiento = new DetalleAsiento();
                    //            intLineaDetalleAsiento += 1;
                    //            detalleAsiento.Linea = intLineaDetalleAsiento;
                    //            detalleAsiento.IdCuenta = cuentaPorCobrarClientesParam.IdCuenta;
                    //            detalleAsiento.Debito = desglosePago.MontoLocal;
                    //            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //            asiento.DetalleAsiento.Add(detalleAsiento);
                    //            asiento.TotalDebito += detalleAsiento.Debito;
                    //        }
                    //        else if (desglosePago.IdFormaPago == StaticFormaPago.Cheque || desglosePago.IdFormaPago == StaticFormaPago.TransferenciaDepositoBancario)
                    //        {
                    //            detalleAsiento = new DetalleAsiento();
                    //            intLineaDetalleAsiento += 1;
                    //            detalleAsiento.Linea = intLineaDetalleAsiento;
                    //            bancoParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.CuentaDeBancos && x.IdProducto == desglosePago.IdCuentaBanco).FirstOrDefault();
                    //            if (bancoParam == null)
                    //                throw new BusinessException("No existe parametrización contable para la cuenta bancaría " + desglosePago.IdCuentaBanco + " y no se puede continuar. Por favor verificar.");
                    //            detalleAsiento.IdCuenta = bancoParam.IdCuenta;
                    //            detalleAsiento.Debito = desglosePago.MontoLocal;
                    //            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //            asiento.DetalleAsiento.Add(detalleAsiento);
                    //            asiento.TotalDebito += detalleAsiento.Debito;
                    //        }
                    //        else if (desglosePago.IdFormaPago == StaticFormaPago.Tarjeta)
                    //        {
                    //            BancoAdquiriente bancoAdquiriente = dbContext.BancoAdquirienteRepository.Find(desglosePago.IdCuentaBanco);
                    //            decTotalGastoComisionTarjeta = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeComision / 100), 2, MidpointRounding.AwayFromZero);
                    //            decTotalImpuestoRetenido = Math.Round(desglosePago.MontoLocal * (bancoAdquiriente.PorcentajeRetencion / 100), 2, MidpointRounding.AwayFromZero);
                    //            decTotalIngresosTarjeta = Math.Round(desglosePago.MontoLocal - decTotalGastoComisionTarjeta - decTotalImpuestoRetenido, 2, MidpointRounding.AwayFromZero);
                    //            detalleAsiento = new DetalleAsiento();
                    //            intLineaDetalleAsiento += 1;
                    //            detalleAsiento.Linea = intLineaDetalleAsiento;
                    //            detalleAsiento.IdCuenta = cuentaPorCobrarTarjetaParam.IdCuenta;
                    //            detalleAsiento.Debito = decTotalIngresosTarjeta;
                    //            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //            asiento.DetalleAsiento.Add(detalleAsiento);
                    //            asiento.TotalDebito += detalleAsiento.Debito;
                    //            if (decTotalImpuestoRetenido > 0)
                    //            {
                    //                detalleAsiento = new DetalleAsiento();
                    //                intLineaDetalleAsiento += 1;
                    //                detalleAsiento.Linea = intLineaDetalleAsiento;
                    //                detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                    //                detalleAsiento.Debito = decTotalImpuestoRetenido;
                    //                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //                asiento.DetalleAsiento.Add(detalleAsiento);
                    //                asiento.TotalDebito += detalleAsiento.Debito;
                    //            }
                    //            if (decTotalGastoComisionTarjeta > 0)
                    //            {
                    //                detalleAsiento = new DetalleAsiento();
                    //                intLineaDetalleAsiento += 1;
                    //                detalleAsiento.Linea = intLineaDetalleAsiento;
                    //                detalleAsiento.IdCuenta = gastoComisionParam.IdCuenta;
                    //                detalleAsiento.Debito = decTotalGastoComisionTarjeta;
                    //                detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //                asiento.DetalleAsiento.Add(detalleAsiento);
                    //                asiento.TotalDebito += detalleAsiento.Debito;
                    //            }
                    //        }
                    //    }
                    //    if (decTotalMercancia > 0)
                    //    {
                    //        detalleAsiento = new DetalleAsiento();
                    //        intLineaDetalleAsiento += 1;
                    //        detalleAsiento.Linea = intLineaDetalleAsiento;
                    //        detalleAsiento.IdCuenta = ingresosVentasParam.IdCuenta;
                    //        detalleAsiento.Credito = decTotalMercancia;
                    //        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //        asiento.DetalleAsiento.Add(detalleAsiento);
                    //        asiento.TotalCredito += detalleAsiento.Credito;
                    //    }
                    //    if (decTotalImpuesto > 0)
                    //    {
                    //        detalleAsiento = new DetalleAsiento();
                    //        intLineaDetalleAsiento += 1;
                    //        detalleAsiento.Linea = intLineaDetalleAsiento;
                    //        detalleAsiento.IdCuenta = ivaPorPagarParam.IdCuenta;
                    //        detalleAsiento.Credito = decTotalImpuesto;
                    //        asiento.DetalleAsiento.Add(detalleAsiento);
                    //        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //        asiento.TotalCredito += detalleAsiento.Credito;
                    //    }
                    //    foreach (DataRow data in dtbIngresosPorServicios.Rows)
                    //    {
                    //        detalleAsiento = new DetalleAsiento();
                    //        intLineaDetalleAsiento += 1;
                    //        detalleAsiento.Linea = intLineaDetalleAsiento;
                    //        int intIdLinea = (int)data["IdLinea"];
                    //        lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeServicios && x.IdProducto == intIdLinea).FirstOrDefault();
                    //        if (lineaParam == null)
                    //            throw new BusinessException("No existe parametrización contable para la línea de servicios " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                    //        detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                    //        detalleAsiento.Credito = (decimal)data["Total"];
                    //        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //        asiento.DetalleAsiento.Add(detalleAsiento);
                    //        asiento.TotalCredito += detalleAsiento.Credito;
                    //    }
                    //    if (decTotalCostoVentas > 0)
                    //    {
                    //        detalleAsiento = new DetalleAsiento();
                    //        intLineaDetalleAsiento += 1;
                    //        detalleAsiento.Linea = intLineaDetalleAsiento;
                    //        detalleAsiento.IdCuenta = costoVentasParam.IdCuenta;
                    //        detalleAsiento.Debito = decTotalCostoVentas;
                    //        detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //        asiento.DetalleAsiento.Add(detalleAsiento);
                    //        asiento.TotalDebito += detalleAsiento.Debito;
                    //        foreach (DataRow data in dtbInventarios.Rows)
                    //        {
                    //            detalleAsiento = new DetalleAsiento();
                    //            intLineaDetalleAsiento += 1;
                    //            detalleAsiento.Linea = intLineaDetalleAsiento;
                    //            int intIdLinea = (int)data["IdLinea"];
                    //            lineaParam = dbContext.ParametroContableRepository.Where(x => x.IdTipo == StaticTipoCuentaContable.LineaDeProductos && x.IdProducto == intIdLinea).FirstOrDefault();
                    //            if (lineaParam == null)
                    //                throw new BusinessException("No existe parametrización contable para la línea de producto " + intIdLinea + " y no se puede continuar. Por favor verificar.");
                    //            detalleAsiento.IdCuenta = lineaParam.IdCuenta;
                    //            detalleAsiento.Credito = (decimal)data["Total"];
                    //            detalleAsiento.SaldoAnterior = dbContext.CatalogoContableRepository.Find(detalleAsiento.IdCuenta).SaldoActual;
                    //            asiento.DetalleAsiento.Add(detalleAsiento);
                    //            asiento.TotalCredito += detalleAsiento.Credito;
                    //        }
                    //    }
                    //    IContabilidadService servicioContabilidad = new ContabilidadService();
                    //    servicioContabilidad.AgregarAsiento(dbContext, asiento);
                    //}
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
                    log.Error("Error al agregar el registro de ajuste de inventario: ", ex);
                    throw new Exception("Se produjo un error guardando la información del ajuste de inventario. Por favor consulte con su proveedor.");
                }
            }
            return ajusteInventario;
        }

        public void ActualizarAjusteInventario(AjusteInventario ajusteInventario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(ajusteInventario);
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
        public void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    AjusteInventario ajusteInventario = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    if (ajusteInventario == null) throw new Exception("El registro de ajuste de inventario por anular no existe.");
                    if (ajusteInventario.Nulo == true) throw new BusinessException("El registro de ajuste de inventario ya ha sido anulado.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    ajusteInventario.Nulo = true;
                    ajusteInventario.IdAnuladoPor = intIdUsuario;
                    dbContext.NotificarModificacion(ajusteInventario);
                    foreach (var detalleAjuste in ajusteInventario.DetalleAjusteInventario)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
                        if (producto.Tipo == StaticTipoProducto.Producto)
                        {
                            MovimientoProducto movimiento = new MovimientoProducto
                            {
                                IdProducto = producto.IdProducto,
                                Fecha = DateTime.Now,
                                Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Entrada : StaticTipoMovimientoProducto.Salida,
                                Origen = "Registro de reversión de ajuste de inventario.",
                                Referencia = detalleAjuste.IdAjuste.ToString(),
                                Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                                PrecioCosto = detalleAjuste.PrecioCosto
                            };
                            producto.MovimientoProducto.Add(movimiento);
                            producto.Cantidad -= detalleAjuste.Cantidad;
                            dbContext.NotificarModificacion(producto);
                        }
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
                    log.Error("Error al anular el registro de ajuste de inventario: ", ex);
                    throw new Exception("Se produjo un error anulando el ajuste de inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.AjusteInventarioRepository.Find(intIdAjusteInventario);
                    return dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario.Producto.TipoProducto").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAjustes(int intIdEmpresa, int intIdAjusteInventario, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaAjusteInventario = dbContext.AjusteInventarioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdAjusteInventario > 0)
                        listaAjusteInventario = listaAjusteInventario.Where(x => !x.Nulo && x.IdAjuste == intIdAjusteInventario);
                    else if (!strDescripcion.Equals(string.Empty))
                        listaAjusteInventario = listaAjusteInventario.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Descripcion.Contains(strDescripcion));
                    return listaAjusteInventario.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoAjustes(int intIdEmpresa, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaAjustes = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.AjusteInventarioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa);
                    if (intIdAjusteInventario > 0)
                        listado = listado.Where(x => !x.Nulo && x.IdAjuste == intIdAjusteInventario);
                    else if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderByDescending(x => x.IdAjuste).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdAjuste, value.Descripcion);
                        listaAjustes.Add(item);
                    }
                    return listaAjustes;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoTipoIdentificacion()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaTipoIdentificacion = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TipoIdentificacionRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTipoIdentificacion, value.Descripcion);
                        listaTipoIdentificacion.Add(item);
                    }
                    return listaTipoIdentificacion;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de tipos de identificación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de identificación. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoCatalogoReportes()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCatalogoReporte = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CatalogoReporteRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdReporte, value.NombreReporte);
                        listaCatalogoReporte.Add(item);
                    }
                    return listaCatalogoReporte;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de reportes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de reportes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoProvincias()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaProvincia = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.ProvinciaRepository;
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdProvincia, value.Descripcion);
                        listaProvincia.Add(item);
                    }
                    return listaProvincia;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de provincias: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de provincias. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCanton = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CantonRepository.Where(x => x.IdProvincia == intIdProvincia);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdCanton, value.Descripcion);
                        listaCanton.Add(item);
                    }
                    return listaCanton;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cantones: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cantones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaDistrito = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.DistritoRepository.Where(x => x.IdProvincia == intIdProvincia && x.IdCanton == intIdCanton);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdDistrito, value.Descripcion);
                        listaDistrito.Add(item);
                    }
                    return listaDistrito;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de distritos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de distritos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaBarrio = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.BarrioRepository.Where(x => x.IdProvincia == intIdProvincia && x.IdCanton == intIdCanton && x.IdDistrito == intIdDistrito);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdBarrio, value.Descripcion);
                        listaBarrio.Add(item);
                    }
                    return listaBarrio;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de barrios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de barrios. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTipodePrecio()
        {
            try
            {
                IList<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                var tipoPrecio = new LlaveDescripcion(1, "Precio 1");
                listado.Add(tipoPrecio);
                tipoPrecio = new LlaveDescripcion(2, "Precio 2");
                listado.Add(tipoPrecio);
                tipoPrecio = new LlaveDescripcion(3, "Precio 3");
                listado.Add(tipoPrecio);
                tipoPrecio = new LlaveDescripcion(4, "Precio 4");
                listado.Add(tipoPrecio);
                tipoPrecio = new LlaveDescripcion(5, "Precio 5");
                listado.Add(tipoPrecio);
                return listado;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el listado de tipos de precio: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de precio. Por favor consulte con su proveedor.");
            }
        }

        private string GenerarRegistroAutenticacion(int intRole)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                string strGuid = Guid.NewGuid().ToString();
                DateTime fechaRegistro = DateTime.UtcNow;
                RegistroAutenticacion registro = new RegistroAutenticacion
                {
                    Id = strGuid,
                    Fecha = fechaRegistro,
                    Role = intRole
                };
                try
                {
                    dbContext.RegistroAutenticacionRepository.Add(registro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                string strTokenEncriptado = Utilitario.EncriptarDatos(strGuid);
                return strTokenEncriptado;
            }
        }

        public void ValidarRegistroAutenticacion(string strToken, int intRole)
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    string strTokenDesencriptado = Utilitario.DesencriptarDatos(strToken);
                    RegistroAutenticacion registro = dbContext.RegistroAutenticacionRepository.Where(x => x.Id == strTokenDesencriptado).FirstOrDefault();
                    if (registro == null) throw new BusinessException("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    if (registro.Fecha < DateTime.UtcNow.AddHours(-12))
                    {
                        dbContext.NotificarEliminacion(registro);
                        dbContext.Commit();
                        throw new BusinessException("La sessión del usuario se encuentra expirada. Debe reiniciar su sesión.");
                    }
                    if (registro.Role != StaticRolePorUsuario.ADMINISTRADOR)
                    {
                        if (registro.Role != intRole) throw new BusinessException("El usuario no se encuentra autorizado para ejecutar la acción solicitada.");
                    }
                }
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                log.Error("Error al validar el registro de autenticación: ", ex);
                throw ex;
            }
        }

        public void EliminarRegistroAutenticacionInvalidos()
        {
            try
            {
                using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
                {
                    DateTime detFechaMaxima = DateTime.UtcNow.AddHours(-12);
                    var listado = dbContext.RegistroAutenticacionRepository.Where(x => x.Fecha < detFechaMaxima).ToList();
                    foreach(RegistroAutenticacion registro in listado)
                    {
                        dbContext.NotificarEliminacion(registro);
                        dbContext.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al validar el registro de autenticación: ", ex);
            }
        }
    }
}