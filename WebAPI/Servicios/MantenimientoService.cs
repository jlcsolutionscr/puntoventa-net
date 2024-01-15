using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.Seguridad;
using LeandroSoftware.ServicioWeb.Contexto;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using LeandroSoftware.ServicioWeb.Parametros;
using LeandroSoftware.ServicioWeb.Utilitario;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IMantenimientoService
    {
        // Métodos para administrar parametros del sistema
        IList<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strCodigoUsuario, string strClave, string strIdentificacion, int intTipoDispositivo);
        IList<LlaveDescripcion> ObtenerListadoEmpresasAdministrador();
        IList<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId);
        bool EnModoMantenimiento();
        void RegistrarTerminal(string strCodigoUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId);
        Usuario ValidarCredencialesAdmin(string strCodigoUsuario, string strClave);
        Empresa ValidarCredenciales(string strCodigoUsuario, string strClave, string id);
        Empresa ValidarCredenciales(string strCodigoUsuario, string strClave, int intIdEmpresa, string strValorRegistro);
        decimal AutorizacionPorcentaje(string strCodigoUsuario, string strClave, int intIdEmpresa);
        string ObtenerUltimaVersionApp();
        string ObtenerUltimaVersionMobileApp();
        IList<ParametroSistema> ObtenerListadoParametros();
        void ActualizarParametroSistema(int intIdParametro, string strValor);
        // Métodos para administrar las empresas
        IList<LlaveDescripcion> ObtenerListadoEmpresa();
        IList<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa);
        IList<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal);
        string AgregarEmpresa(Empresa empresa);
        Empresa ObtenerEmpresa(int intIdEmpresa);
        void ActualizarEmpresa(Empresa empresa);
        void ValidarCredencialesHacienda(string strCodigoUsuario, string strClave, ConfiguracionGeneral config);
        void ValidarCertificadoHacienda(string strPin, string strCertificado);
        void AgregarCredencialesHacienda(CredencialesHacienda credenciales);
        CredencialesHacienda ObtenerCredencialesHacienda(int intIdEmpresa);
        void ActualizarCredencialesHacienda(int intIdEmpresa, string strCodigoUsuario, string strClave, string strNombreCertificado, string strPin, string strCertificado);
        List<LlaveDescripcion> ObtenerListadoReportePorEmpresa(int intIdEmpresa);
        List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa, bool bolAdministrator);
        void ActualizarReportePorEmpresa(int intIdEmpresa, List<ReportePorEmpresa> listado);
        void ActualizarRolePorEmpresa(int intIdEmpresa, List<RolePorEmpresa> listado);
        string ObtenerLogotipoEmpresa(int intIdEmpresa);
        void ActualizarLogoEmpresa(int intIdEmpresa, string strLogo);
        // Métodos para administrar las sucursales
        SucursalPorEmpresa ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal);
        void AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal);
        void ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal);
        void EliminarRegistrosPorEmpresa(int intIdEmpresa);
        TerminalPorSucursal ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal);
        void AgregarTerminalPorSucursal(TerminalPorSucursal terminal);
        void ActualizarTerminalPorSucursal(TerminalPorSucursal terminal);
        // Métodos para administrar los usuarios del sistema
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave);
        void EliminarUsuario(int intIdUsuario);
        Usuario ObtenerUsuario(int intIdUsuario);
        IList<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo);
        // Métodos para administrar los vendedores del sistema
        void AgregarVendedor(Vendedor vendedor);
        void ActualizarVendedor(Vendedor vendedor);
        void EliminarVendedor(int intIdVendedor);
        Vendedor ObtenerVendedor(int intIdVendedor);
        Vendedor ObtenerVendedorPorDefecto(int intIdEmpresa);
        IList<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre);
        // Métodos para administrar los roles del sistema
        Role ObtenerRole(int intIdRole);
        IList<LlaveDescripcion> ObtenerListadoRoles();
        // Métodos para administrar las líneas de producto
        void AgregarLinea(Linea linea);
        void ActualizarLinea(Linea linea);
        void EliminarLinea(int intIdLinea);
        Linea ObtenerLinea(int intIdLinea);
        IList<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion);
        // Métodos para administrar los productos
        LlaveDescripcionValor ObtenerParametroImpuesto(int intIdImpuesto);
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion, decimal decPorcentajeAumento);
        void EliminarProducto(int intIdProducto);
        Producto ObtenerProducto(int intIdProducto, int intIdSucursal);
        Producto ObtenerProductoEspecial(int intIdEmpresa, int intIdTipo);
        Producto ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo, int intIdSucursal);
        Producto ObtenerProductoPorCodigoProveedor(int intIdEmpresa, string strCodigo, int intIdSucursal);
        int ObtenerTotalListaProductos(int intIdEmpresa, int intIdSucursal, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion);
        IList<ProductoDetalle> ObtenerListadoProductos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion);
        int ObtenerTotalMovimientosPorProducto(int intIdProducto, int intIdSucursal, string strFechaInicial, string strFechaFinal);
        IList<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int intIdSucursal, int numPagina, int cantRec, string strFechaInicial, string strFechaFinal);
        // Métodos para administrar los parámetros de banco adquiriente
        void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void EliminarBancoAdquiriente(int intIdBanco);
        BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco);
        IList<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion);
        // Métodos para administrar los ajustes de inventario
        string AgregarAjusteInventario(AjusteInventario ajusteInventario);
        void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario, string strMotivoAnulacion);
        AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario);
        int ObtenerTotalListaAjusteInventario(int intIdEmpresa, int intIdSucursal, int intIdAjusteInventario, string strDescripcion, string strFechaFinal);
        IList<AjusteInventarioDetalle> ObtenerListadoAjusteInventario(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion, string strFechaFinal);
        // Métodos para obtener parámetros generales del sistema
        IList<LlaveDescripcion> ObtenerListadoTipoIdentificacion();
        IList<LlaveDescripcion> ObtenerListadoCatalogoReportes();
        CatalogoReporte ObtenerCatalogoReporte(int intIdReporte);
        IList<LlaveDescripcion> ObtenerListadoProvincias();
        IList<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia);
        IList<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton);
        IList<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito);
        int ObtenerTotalListaClasificacionProducto(string strDescripcion);
        IList<ClasificacionProducto> ObtenerListadoClasificacionProducto(int numPagina, int cantRec, string strDescripcion);
        ClasificacionProducto ObtenerClasificacionProducto(string strCodigo);
        void AgregarPuntoDeServicio(PuntoDeServicio puntoDeServicio);
        void ActualizarPuntoDeServicio(PuntoDeServicio puntoDeServicio);
        void EliminarPuntoDeServicio(int intIdPunto);
        PuntoDeServicio ObtenerPuntoDeServicio(int intIdPunto);
        IList<LlaveDescripcion> ObtenerListadoPuntoDeServicio(int intIdEmpresa, int intIdSucursal, bool bolSoloActivo, string strDescripcion);
        void ValidarRegistroAutenticacion(string strToken, int intRole, int intHoras);
        void EliminarRegistroAutenticacionInvalidos();
        List<LlaveDescripcion> ObtenerListadoActividadEconomica(string strServicioURL, string strIdentificacion);
        void IniciarRestablecerClaveUsuario(string strServicioWebURL, string strIdentificacion, string strCodigoUsuario);
        void RestablecerClaveUsuario(string strToken, string strClave);
    }

    public class MantenimientoService : IMantenimientoService
    {
        private readonly ILoggerManager _logger;
        private static IServiceScopeFactory serviceScopeFactory;
        private static ICorreoService servicioCorreo;
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public MantenimientoService(ILoggerManager logger, ICorreoService pServicioCorreo, IServiceScopeFactory pServiceScopeFactory)
        {
            try
            {
                _logger = logger;
                servicioCorreo = pServicioCorreo;
                serviceScopeFactory = pServiceScopeFactory;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Mantenimiento. Por favor consulte con su proveedor.");
            }
        }

        public IList<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strCodigoUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                if (strCodigoUsuario.ToUpper() == "CONTADOR") throw new Exception("El usuario que envia la petición no posee los privilegios necesarios.");
                var listaEquipoRegistrado = new List<EquipoRegistrado>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < Validador.ObtenerFechaHoraCostaRica()) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    Usuario usuario = null;
                    if (strCodigoUsuario.ToUpper() == "ADMIN")
                    {
                        usuario = dbContext.UsuarioRepository.AsNoTracking().FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuario = dbContext.UsuarioRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strCodigoUsuario.ToUpper());

                    }
                    if (usuario == null) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (!usuario.PermiteRegistrarDispositivo) throw new BusinessException("El usuario suministrado no esta autorizado para registrar el punto de venta. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listado = dbContext.TerminalPorSucursalRepository.Include("SucursalPorEmpresa").Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdTipoDispositivo == intTipoDispositivo)
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
                            ImpresoraFactura = terminal.ImpresoraFactura,
                            AnchoLinea = terminal.AnchoLinea
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
                    _logger.LogError("Error al obtener el listado de terminales disponibles: ", ex);
                    throw new Exception("Error al obtener las terminales disponibles. . .");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresasAdministrador()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al validar la lista de empresas para usuario administrador: ", ex);
                    throw new Exception("Error al validar la lista de empresas para usuario administrador. . .");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    foreach (SucursalPorEmpresa value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdSucursal, value.NombreSucursal);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar la lista de sucursales asignadas a la empresa: ", ex);
                    throw new Exception("Error al validar la lista de sucursales asignadas a la empresa. . .");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TerminalPorSucursalRepository.Include("SucursalPorEmpresa").Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    foreach (TerminalPorSucursal value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdTerminal, value.SucursalPorEmpresa.NombreSucursal);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar la lista de terminales asignadas a la empresa: ", ex);
                    throw new Exception("Error al validar la lista de terminales asignadas a la empresa. . .");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.TerminalPorSucursalRepository.Include("SucursalPorEmpresa.Empresa").Where(x => x.ValorRegistro == strDispositivoId);
                    foreach (TerminalPorSucursal value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdEmpresa, value.SucursalPorEmpresa.Empresa.NombreComercial);
                        listaEmpresa.Add(item);
                    }
                    return listaEmpresa;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar la lista de empresas asignadas a una terminal: ", ex);
                    throw new Exception("Error al validar la lista de empresas asignadas a una terminal. . .");
                }
            }
        }

        public bool EnModoMantenimiento()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ParametroSistema parametro = dbContext.ParametroSistemaRepository.FirstOrDefault(x => x.IdParametro == 5);
                    if (parametro == null) throw new BusinessException("El parámetro del modo mantenimiento no se encuentra registrado en el sistema.");
                    if (parametro.Valor == "SI") return true;
                    return false;
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al consultar el parámetro de modo mantenimiento del sistema: ", ex);
                    throw new Exception("No es posible consultar el modo mantenimieto del sistema.");
                }
            }
        }

        public void RegistrarTerminal(string strCodigoUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < Validador.ObtenerFechaHoraCostaRica()) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    Usuario usuario = null;
                    if (strCodigoUsuario.ToUpper() == "ADMIN")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strCodigoUsuario.ToUpper());
                    }
                    if (usuario == null) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (!usuario.PermiteRegistrarDispositivo) throw new BusinessException("El usuario suministrado no esta autorizado para registrar el punto de venta. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == intIdSucursal);
                    if (sucursal == null) throw new BusinessException("La sucursal donde desea registrar su punto de venta no existe. Por favor, pongase en contacto con su proveedor del servicio.");
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == intIdSucursal && x.IdTerminal == intIdTerminal && x.IdTipoDispositivo == intTipoDispositivo);
                    if (terminal == null) throw new BusinessException("La terminal donde desea registrar su punto de venta no existe. Por favor, pongase en contacto con su proveedor del servicio.");
                    terminal.ValorRegistro = strDispositivoId;
                    dbContext.NotificarModificacion(terminal);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al registrar el dispositivo movil para la identificación suministrada: ", ex);
                    throw new Exception("Error al registrar el dispositivo movil para la identificación suministrada.");
                }
            }
        }

        public Usuario ValidarCredencialesAdmin(string strCodigoUsuario, string strClave)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                if (strCodigoUsuario.ToUpper() != "ADMIN") throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Where(x => x.CodigoUsuario == strCodigoUsuario.ToUpper()).FirstOrDefault();
                    if (usuario == null) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Por favor verifique la información suministrada.");
                    string strToken = GenerarRegistroAutenticacion(1, usuario.CodigoUsuario, StaticRolePorUsuario.ADMINISTRADOR);
                    usuario.Token = strToken;
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar credenciales del usuario: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public Empresa ValidarCredenciales(string strCodigoUsuario, string strClave, string strIdentificacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa local = dbContext.EmpresaRepository.AsNoTracking().FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (local == null) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    Empresa empresa = ObtenerEmpresaPorUsuario(strCodigoUsuario, strClave, local.IdEmpresa, "WebAPI");
                    string strToken = GenerarRegistroAutenticacion(empresa.IdEmpresa, empresa.Usuario.CodigoUsuario, StaticRolePorUsuario.USUARIO_SISTEMA);
                    empresa.Usuario.Token = strToken;
                    return empresa;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar los credenciales del usuario por identificación: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public Empresa ValidarCredenciales(string strCodigoUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = ObtenerEmpresaPorUsuario(strCodigoUsuario, strClave, intIdEmpresa, strValorRegistro);
                    string strToken = GenerarRegistroAutenticacion(empresa.IdEmpresa, empresa.Usuario.CodigoUsuario, StaticRolePorUsuario.USUARIO_SISTEMA);
                    empresa.Usuario.Token = strToken;
                    return empresa;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar los credenciales del usuario por terminal: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        private Empresa ObtenerEmpresaPorUsuario(string strCodigoUsuario, string strClave, int intIdEmpresa, string strValorRegistro)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                Empresa empresa = dbContext.EmpresaRepository.AsNoTracking().Include("ActividadEconomicaEmpresa").Include("SucursalPorEmpresa").Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                empresa.ListadoTipoIdentificacion = ObtenerListadoTipoIdentificacion();
                empresa.ListadoFormaPagoCliente = ObtenerListadoFormaPagoCliente();
                empresa.ListadoFormaPagoEmpresa = ObtenerListadoFormaPagoEmpresa();
                empresa.ListadoTipoProducto = ObtenerListadoTipoProducto(strCodigoUsuario);
                empresa.ListadoTipoImpuesto = ObtenerListadoTipoImpuesto();
                empresa.ListadoTipoMoneda = ObtenerListadoTipoMoneda();
                empresa.ListadoCondicionVenta = ObtenerListadoCondicionVenta();
                empresa.ListadoTipoExoneracion = ObtenerListadoTipoExoneracion();
                empresa.ListadoTipoPrecio = ObtenerListadoTipodePrecio();

                Usuario usuario = null;
                if (strCodigoUsuario.ToUpper() == "ADMIN" || strCodigoUsuario.ToUpper() == "CONTADOR")
                {
                    usuario = dbContext.UsuarioRepository.AsNoTracking().Include("RolePorUsuario.Role").FirstOrDefault(x => x.CodigoUsuario == strCodigoUsuario);
                    usuario.IdEmpresa = empresa.IdEmpresa;
                    usuario.IdSucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa).IdSucursal;
                }
                else
                {
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < Validador.ObtenerFechaHoraCostaRica()) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    usuario = dbContext.UsuarioRepository.AsNoTracking().Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strCodigoUsuario.ToUpper());
                }
                if (usuario == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                TerminalPorSucursal terminal = null;
                SucursalPorEmpresa sucursal = null;
                if (strValorRegistro == "WebAPI" || strCodigoUsuario.ToUpper() == "ADMIN")
                {
                    sucursal = dbContext.SucursalPorEmpresaRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == usuario.IdSucursal);
                    terminal = dbContext.TerminalPorSucursalRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == sucursal.IdSucursal);
                }
                else
                {
                    terminal = dbContext.TerminalPorSucursalRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.ValorRegistro == strValorRegistro);
                    if (terminal == null) throw new BusinessException("El dispositivo no se encuentra registrado en el sistema.");
                    sucursal = dbContext.SucursalPorEmpresaRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == terminal.IdSucursal);
                }
                if (terminal == null || sucursal == null) throw new BusinessException("La terminal o dispositivo movil no se encuentra registrado para la empresa suministrada.");
                EquipoRegistrado equipo = new EquipoRegistrado
                {
                    IdSucursal = sucursal.IdSucursal,
                    IdTerminal = terminal.IdTerminal,
                    NombreSucursal = sucursal.NombreSucursal,
                    DireccionSucursal = sucursal.Direccion,
                    TelefonoSucursal = sucursal.Telefono,
                    ImpresoraFactura = terminal.ImpresoraFactura,
                    AnchoLinea = terminal.AnchoLinea
                };
                empresa.Logotipo = null;
                empresa.EquipoRegistrado = equipo;
                empresa.Usuario = usuario;
                foreach (SucursalPorEmpresa sucursalEmpresa in empresa.SucursalPorEmpresa)
                    sucursalEmpresa.Empresa = null;
                return empresa;
            }
        }

        public void ValidarCredencialesHacienda(string strCodigoUsuario, string strClave, ConfiguracionGeneral config)
        {
            try
            {
                TokenType token = ComprobanteElectronicoService.ObtenerToken(config.ServicioTokenURL, config.ClientId, strCodigoUsuario, strClave).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al validar los credenciales del usuario en Hacienda: ", ex);
                throw new Exception("No fue posible validar los credenciales de Hacienda. Por favor verifique la información. . .");
            }
        }

        public void ValidarCertificadoHacienda(string strPin, string strCertificado)
        {
            X509Certificate2 uidCert;
            try
            {
                byte[] bytCertificado = Convert.FromBase64String(strCertificado);
                uidCert = new X509Certificate2(bytCertificado, strPin, X509KeyStorageFlags.MachineKeySet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al validar la llave criptográfica: ", ex);
                throw new Exception("No se logró abrir la llave criptográfica con el pin suministrado. Por favor verifique la información suministrada");
            }
            if (uidCert.NotAfter <= Validador.ObtenerFechaHoraCostaRica()) throw new Exception("La llave criptográfica para la firma del documento electrónico se encuentra vencida. Por favor reemplace su llave criptográfica para poder emitir documentos electrónicos");
        }

        public decimal AutorizacionPorcentaje(string strCodigoUsuario, string strClave, int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.CodigoUsuario == strCodigoUsuario.ToUpper());
                    if (usuario == null) throw new BusinessException("Los credenciales suministrados no son válidos.Verifique los credenciales suministrados.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    return usuario.PorcMaxDescuento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al autorizar un porcentaje de descuento con credenciales: ", ex);
                    throw new Exception("Error al obtener la autorización del descuento. Por favor consulte con su proveedor.");
                }
            }
        }

        public string ObtenerUltimaVersionApp()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                string strUltimaVersion = "";
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 1).FirstOrDefault();
                    if (version is null) throw new BusinessException("No se logró obtener el parámetro con descripción 'Version' de la tabla de parámetros del sistema.");
                    strUltimaVersion = version.Valor;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al consultar el parámetro 'Version' del sistema: ", ex);
                    throw new Exception("Se produjo un error consultado la versión actual del sistema. Por favor consulte con su proveedor.");
                }
                return strUltimaVersion;
            }
        }

        public string ObtenerUltimaVersionMobileApp()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                string strUltimaVersion = "";
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 4).FirstOrDefault();
                    if (version is null) throw new BusinessException("No se logró obtener el parámetro con descripción 'Version' de la tabla de parámetros del sistema.");
                    strUltimaVersion = version.Valor;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al consultar el parámetro 'Version' del sistema: ", ex);
                    throw new Exception("Se produjo un error consultado la versión actual del sistema. Por favor consulte con su proveedor.");
                }
                return strUltimaVersion;
            }
        }

        public IList<ParametroSistema> ObtenerListadoParametros()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ParametroSistemaRepository.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al consultar el listado d parámetros del sistema: ", ex);
                    throw new Exception("Error al consultar el listado d parámetros del sistema. . .");
                }
            }
        }

        public void ActualizarParametroSistema(int intIdParametro, string strValor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var parametro = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == intIdParametro).FirstOrDefault();
                    if (parametro is null) throw new BusinessException("No se logró obtener el parámetro con Id: " + intIdParametro + " de la tabla de parámetros del sistema.");
                    parametro.Valor = strValor;
                    dbContext.NotificarModificacion(parametro);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el parámetro del sistema: ", ex);
                    throw new Exception("Se produjo un error actualizando el parámetro del sistema. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresa()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al validar la lista de empresas para usuario administrador: ", ex);
                    throw new Exception("Error al validar la lista de empresas para usuario administrador. . .");
                }
            }
        }

        public string AgregarEmpresa(Empresa empresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    if (empresa.Logotipo == null) empresa.Logotipo = new byte[0];
                    dbContext.EmpresaRepository.Add(empresa);
                    dbContext.Commit();
                    return empresa.IdEmpresa.ToString();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar la empresa: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public Empresa ObtenerEmpresa(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ActividadEconomicaEmpresa").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    return empresa;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarEmpresa(Empresa empresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    try
                    {
                        Validador.ValidaFormatoIdentificacion(empresa.IdTipoIdentificacion, empresa.Identificacion);
                        Validador.ValidaFormatoEmail(empresa.CorreoNotificacion);
                    }
                    catch (Exception ex)
                    {
                        throw new BusinessException(ex.Message);
                    }
                    Empresa noTracking = dbContext.EmpresaRepository.AsNoTracking().Include("ActividadEconomicaEmpresa").Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                    empresa.Barrio = null;
                    if (noTracking != null && noTracking.Logotipo != null) empresa.Logotipo = noTracking.Logotipo;
                    if (empresa.Logotipo == null) empresa.Logotipo = new byte[0];
                    List<ActividadEconomicaEmpresa> listadoDetalle = empresa.ActividadEconomicaEmpresa.ToList();
                    empresa.ActividadEconomicaEmpresa = null;
                    foreach (ActividadEconomicaEmpresa detalle in noTracking.ActividadEconomicaEmpresa)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(empresa);
                    foreach (ActividadEconomicaEmpresa detalle in listadoDetalle)
                        dbContext.ActividadEconomicaEmpresaRepository.Add(detalle);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar la empresa: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoReportePorEmpresa(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaReportes = new List<LlaveDescripcion>();
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listado = dbContext.ReportePorEmpresaRepository.Include("CatalogoReporte").Where(x => x.IdEmpresa == intIdEmpresa);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdReporte, value.CatalogoReporte.NombreReporte);
                        listaReportes.Add(item);
                    }
                    return listaReportes;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa, bool bolAdministrator)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaRoles = new List<LlaveDescripcion>();
                try
                {
                    var intIdLowerRole = bolAdministrator ? 0 : 2;
                    var listado = dbContext.RolePorEmpresaRepository.Include("Role").Where(x => x.IdEmpresa == intIdEmpresa && x.IdRole > intIdLowerRole);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdRole, value.Role.Descripcion);
                        listaRoles.Add(item);
                    }
                    return listaRoles;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de roles de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de roles de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarReportePorEmpresa(int intIdEmpresa, List<ReportePorEmpresa> listado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<ReportePorEmpresa> listadoReportePorEmpresaAnt = dbContext.ReportePorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa).ToList();
                    foreach (ReportePorEmpresa reporte in listadoReportePorEmpresaAnt)
                        dbContext.ReportePorEmpresaRepository.Remove(reporte);
                    foreach (ReportePorEmpresa reporte in listado)
                        dbContext.ReportePorEmpresaRepository.Add(reporte);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al actualizar el listado de reportes por empresa: ", ex);
                    throw new Exception("Se produjo un error al actualizar el listado de reportes por empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarRolePorEmpresa(int intIdEmpresa, List<RolePorEmpresa> listado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    List<RolePorEmpresa> listadoReportePorEmpresaAnt = dbContext.RolePorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa).ToList();
                    foreach (RolePorEmpresa reporte in listadoReportePorEmpresaAnt)
                        dbContext.RolePorEmpresaRepository.Remove(reporte);
                    foreach (RolePorEmpresa reporte in listado)
                        dbContext.RolePorEmpresaRepository.Add(reporte);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al actualizar el listado de roles por empresa: ", ex);
                    throw new Exception("Se produjo un error al actualizar el listado de roles por empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public string ObtenerLogotipoEmpresa(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarLogoEmpresa(int intIdEmpresa, string strLogo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                        empresa.Logotipo = new byte[0];
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al actualizar el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error registrando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarCredencialesHacienda(CredencialesHacienda credenciales)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.CredencialesHaciendaRepository.Add(credenciales);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al agregar los credenciales de Hacienda: ", ex);
                    throw new Exception("Se produjo un error agregando los credenciales de Hacienda. Por favor consulte con su proveedor.");
                }
            }
        }

        public CredencialesHacienda ObtenerCredencialesHacienda(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CredencialesHacienda credenciales = dbContext.CredencialesHaciendaRepository.Find(intIdEmpresa);
                    if (credenciales != null) credenciales.Certificado = new byte[0];
                    return credenciales;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al consultar los credenciales de Hacienda: ", ex);
                    throw new Exception("Se produjo un error consultando la información de los credenciales de Hacienda. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarCredencialesHacienda(int intIdEmpresa, string strCodigoUsuario, string strClave, string strNombreCertificado, string strPin, string strCertificado)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    CredencialesHacienda credenciales = dbContext.CredencialesHaciendaRepository.Find(intIdEmpresa);
                    credenciales.UsuarioHacienda = strCodigoUsuario;
                    credenciales.ClaveHacienda = strClave;
                    credenciales.NombreCertificado = strNombreCertificado;
                    credenciales.PinCertificado = strPin;
                    if (strCertificado != "")
                    {
                        byte[] bytCertificado = Convert.FromBase64String(strCertificado);
                        credenciales.Certificado = bytCertificado;
                    }
                    dbContext.NotificarModificacion(credenciales);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    _logger.LogError("Error al actualizar los credenciales de Hacienda: ", ex);
                    throw new Exception("Se produjo un error actualizando los credenciales de Hacienda. Por favor consulte con su proveedor.");
                }
            }
        }

        public CatalogoReporte ObtenerCatalogoReporte(int intIdReporte)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.CatalogoReporteRepository.FirstOrDefault(x => x.IdReporte == intIdReporte);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la información del catalogo de reporte: ", ex);
                    throw new Exception("Se produjo un error consultando la parametrización de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public SucursalPorEmpresa ObtenerSucursalPorEmpresa(int intIdEmpresa, int intIdSucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal).FirstOrDefault();
                    return sucursal;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la información de la sucursal: ", ex);
                    throw new Exception("Se produjo un error al obtener la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.SucursalPorEmpresaRepository.Add(sucursal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar la sucursal: ", ex);
                    throw new Exception("Se produjo un error adicionando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarSucursalPorEmpresa(SucursalPorEmpresa sucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(sucursal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar la sucursal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarRegistrosPorEmpresa(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    object[] objParameters = new object[1];
                    objParameters.SetValue(intIdEmpresa, 0);
                    dbContext.ExecuteProcedure("LimpiarRegistros", objParameters);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al eliminar los registros de la empresa: ", ex);
                    throw new Exception("Se produjo un error eliminando los registros de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public TerminalPorSucursal ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdTerminal == intIdTerminal).FirstOrDefault();
                    return terminal;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la información de la terminal: ", ex);
                    throw new Exception("Se produjo un error al obtener la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.TerminalPorSucursalRepository.Add(terminal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar la terminal: ", ex);
                    throw new Exception("Se produjo un error adicionando la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarTerminalPorSucursal(TerminalPorSucursal terminal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(terminal);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar la terminal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la terminal. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarUsuario(Usuario usuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    if (usuario.IdEmpresa == null || usuario.IdSucursal == null) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    Usuario usuarioExistente = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario.Contains(usuario.CodigoUsuario.ToUpper()));
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
                    _logger.LogError("Error al agregar el usuario: ", ex);
                    throw new Exception("Se produjo un error agregando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    if (usuario.IdEmpresa == null || usuario.IdSucursal == null) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<RolePorUsuario> listadoDetalleAnterior = dbContext.RolePorUsuarioRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    List<RolePorUsuario> listadoDetalle = usuario.RolePorUsuario.ToList();
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
                    _logger.LogError("Error al actualizar el usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Usuario usuario = usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null) throw new Exception("El usuario seleccionado para la actualización de la clave no existe.");
                    if (usuario.IdEmpresa == null || usuario.IdSucursal == null) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    usuario.Clave = strClave;
                    dbContext.NotificarModificacion(usuario);
                    dbContext.Commit();
                    return usuario;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar la contraseña del usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la contraseña del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarUsuario(int intIdUsuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Where(x => x.IdUsuario == intIdUsuario).FirstOrDefault();
                    if (usuario == null) throw new BusinessException("El usuario por eliminar no existe.");
                    if (usuario.IdEmpresa == null || usuario.IdSucursal == null) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<RolePorUsuario> listaRole = dbContext.RolePorUsuarioRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    foreach (RolePorUsuario roleUsuario in listaRole)
                        dbContext.NotificarEliminacion(roleUsuario);
                    dbContext.NotificarEliminacion(usuario);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al eliminar el usuario: ", ex);
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
                    _logger.LogError("Error al eliminar el usuario: ", ex);
                    throw new Exception("Se produjo un error eliminando al usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ObtenerUsuario(int intIdUsuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null)
                        throw new BusinessException("El usuario por consultar no existe");
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el usuario: ", ex);
                    throw new Exception("Se produjo un error consultando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaUsuario = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.UsuarioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdUsuario > 2);
                    if (!strCodigo.Equals(string.Empty))
                        listado = listado.Where(x => x.CodigoUsuario.Contains(strCodigo.ToUpper()));
                    listado.OrderBy(x => x.IdUsuario);
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdUsuario, value.CodigoUsuario);
                        listaUsuario.Add(item);
                    }
                    return listaUsuario;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de usuarios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de usuarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarVendedor(Vendedor vendedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al agregar el vendedor: ", ex);
                    throw new Exception("Se produjo un error agregando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarVendedor(Vendedor vendedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al actualizar el vendedor: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarVendedor(int intIdVendedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Validación al eliminar el vendedor: ", ex);
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
                    _logger.LogError("Error al eliminar el vendedor: ", ex);
                    throw new Exception("Se produjo un error eliminando al vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Vendedor ObtenerVendedor(int intIdVendedor)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el vendedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public Vendedor ObtenerVendedorPorDefecto(int intIdEmpresa)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el vendedor: ", ex);
                    throw new Exception("Se produjo un error consultando la información del vendedor. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de vendedores: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de vendedores. Por favor consulte con su proveedor.");
                }
            }
        }

        public Role ObtenerRole(int intIdRole)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.RoleRepository.Find(intIdRole);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de role de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando la información del role de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoRoles()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de registros de roles de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de roles de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarLinea(Linea linea)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al agregar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarLinea(Linea linea)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<LineaPorSucursal> listadoDetalleAnterior = dbContext.LineaPorSucursalRepository.Where(x => x.IdLinea == linea.IdLinea).ToList();
                    List<LineaPorSucursal> listadoDetalle = linea.LineaPorSucursal.ToList();
                    linea.LineaPorSucursal = null;
                    foreach (LineaPorSucursal detalle in listadoDetalleAnterior)
                        dbContext.NotificarEliminacion(detalle);
                    dbContext.NotificarModificacion(linea);
                    foreach (LineaPorSucursal detalle in listadoDetalle)
                        dbContext.LineaPorSucursalRepository.Add(detalle);
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
                    _logger.LogError("Error al actualizar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarLinea(int intIdLinea)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Validación al agregar el parámetro contable: ", ex);
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
                    _logger.LogError("Error al eliminar la línea de producto: ", ex);
                    throw new Exception("Se produjo un error eliminando la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public Linea ObtenerLinea(int intIdLinea)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Linea linea = dbContext.LineaRepository.Include("LineaPorSucursal.SucursalPorEmpresa").FirstOrDefault(x => x.IdLinea == intIdLinea);
                    return linea;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la línea de producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la línea. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de líneas general: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de líneas. Por favor consulte con su proveedor.");
                }
            }
        }

        IList<LlaveDescripcion> ObtenerListadoTipoProducto(string strCodigoUsuario)
        {
            var tiposProducto = strCodigoUsuario.ToUpper() == "ADMIN" ? new int[] { 1, 2, 3, 4, 5 } : new int[] { 1, 2, 3 };
            return TipoDeProducto.ObtenerListado().Where(x => tiposProducto.Contains(x.Id)).ToList();
        }

        IList<LlaveDescripcion> ObtenerListadoTipoExoneracion()
        {
            return TipoDeExoneracion.ObtenerListado();
        }

        IList<LlaveDescripcionValor> ObtenerListadoTipoImpuesto()
        {
            return TipoDeImpuesto.ObtenerListado();
        }

        public LlaveDescripcionValor ObtenerParametroImpuesto(int intIdImpuesto)
        {
            return TipoDeImpuesto.ObtenerParametro(intIdImpuesto);
        }

        public void AgregarProducto(Producto producto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    bool existe = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && (x.Codigo == producto.Codigo || x.CodigoProveedor == producto.CodigoProveedor)).Count() > 0;
                    if (existe) throw new BusinessException("El código o código de proveedor de producto ingresado ya está registrado en la empresa.");
                    if (producto.Tipo == StaticTipoProducto.Transitorio)
                    {
                        bool transitorio = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.Tipo == StaticTipoProducto.Transitorio).Count() > 0;
                        if (transitorio) throw new BusinessException("Ya existe un producto de tipo 'Transitorio' registrado en la empresa.");
                    }
                    if (producto.Tipo == StaticTipoProducto.ImpuestodeServicio)
                    {
                        bool impuestoServ = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.Tipo == StaticTipoProducto.ImpuestodeServicio).Count() > 0;
                        if (impuestoServ) throw new BusinessException("Ya existe un producto de tipo 'Impuesto de servicio' registrado en la empresa.");
                    }
                    if (producto.Imagen == null) producto.Imagen = new byte[0];
                    if (producto.CodigoClasificacion.Length < 13) throw new BusinessException("El código CABYS debe tener una longitud mínima de 13 caracteres.");
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
                    _logger.LogError("Error al agregar el producto: ", ex);
                    throw new Exception("Se produjo un error agregando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    bool existe = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto != producto.IdProducto && (x.Codigo == producto.Codigo || x.CodigoProveedor == producto.CodigoProveedor)).Count() > 0;
                    if (existe) throw new BusinessException("El código o código de proveedor del producto ingresado ya está registrado en la empresa.");
                    if (producto.Tipo == StaticTipoProducto.Transitorio)
                    {
                        bool transitorio = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto != producto.IdProducto && x.Tipo == StaticTipoProducto.Transitorio).Count() > 0;
                        if (transitorio) throw new BusinessException("Ya existe un producto de tipo 'Transitorio' registrado en la empresa.");
                    }
                    if (producto.Tipo == StaticTipoProducto.ImpuestodeServicio)
                    {
                        bool transitorio = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto != producto.IdProducto && x.Tipo == StaticTipoProducto.ImpuestodeServicio).Count() > 0;
                        if (transitorio) throw new BusinessException("Ya existe un producto de tipo 'Impuesto de servicio' registrado en la empresa.");
                    }
                    if (producto.Imagen == null) producto.Imagen = new byte[0];
                    if (producto.CodigoClasificacion.Length < 13) throw new BusinessException("El código CABYS debe tener una longitud mínima de 13 caracteres.");
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
                    _logger.LogError("Error al actualizar el producto: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion, decimal decPorcentajeAumento)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && new int[] { 1, 2, 3 }.Contains(x.Tipo));
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
                    _logger.LogError("Error al actualizar el precio de venta del inventario: ", ex);
                    throw new Exception("Se produjo un error actualizando el precio de venta del inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarProducto(int intIdProducto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Find(intIdProducto);
                    if (producto == null) throw new BusinessException("El producto por eliminar no existe.");
                    dbContext.ProductoRepository.Remove(producto);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al agregar el parámetro contable: ", ex);
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
                    _logger.LogError("Error al eliminar el producto: ", ex);
                    throw new Exception("Se produjo un error eliminando el producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProducto(int intIdProducto, int intIdSucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("Proveedor").FirstOrDefault(x => x.IdProducto == intIdProducto);
                    if (producto != null)
                    {
                        var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == intIdProducto && x.IdSucursal == intIdSucursal).FirstOrDefault();
                        decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                        producto.Existencias = decCantidad;
                    }
                    return producto;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoEspecial(int intIdEmpresa, int intIdTipo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Tipo == intIdTipo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el código de producto transitorio: ", ex);
                    throw new Exception("Se produjo un error consultando el producto transitorio. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo, int intIdSucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("Proveedor").AsNoTracking().FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Codigo.Equals(strCodigo));
                    if (producto != null)
                    {
                        var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == intIdSucursal).FirstOrDefault();
                        decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                        producto.Existencias = decCantidad;
                    }
                    return producto;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigoProveedor(int intIdEmpresa, string strCodigo, int intIdSucursal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.CodigoProveedor.Equals(strCodigo));
                    if (producto != null)
                    {
                        var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == intIdSucursal).FirstOrDefault();
                        decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                        producto.Existencias = decCantidad;
                    }
                    return producto;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProductos(int intIdEmpresa, int intIdSucursal, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                using (var connection = dbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        int intTotal = 0;
                        try
                        {
                            List<ReporteInventario> listaReporte = new List<ReporteInventario>();
                            var listadoLineaPorSucursal = dbContext.LineaPorSucursalRepository.Where(y => y.IdEmpresa == intIdEmpresa && y.IdSucursal == intIdSucursal);
                            if (intIdLinea > 0)
                                listadoLineaPorSucursal = listadoLineaPorSucursal.Where(x => x.IdLinea == intIdLinea);
                            int[] lstLineasPorSucursal = listadoLineaPorSucursal.Select(x => x.IdLinea).ToArray();
                            if (lstLineasPorSucursal.Length > 0)
                            {
                                string listaProductos = " AND p.IdLinea IN(" + string.Join(",", lstLineasPorSucursal) + ")";
                                string strUsaIndex = "";        
                                if (!bolIncluyeServicios)
                                    listaProductos += " AND p.Tipo = " + StaticTipoProducto.Producto;
                                else
                                    listaProductos += " AND p.Tipo IN(1, 2, 3)";
                                if (bolFiltraActivos)
                                    listaProductos += " AND p.Activo = true";
                                if (bolFiltraConDescuento)
                                    listaProductos += " AND p.PorcDescuento > 0";
                                if (!strCodigo.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (empresa_codigo_idx)";
                                    listaProductos += " AND LOCATE('" + strCodigo + "', p.Codigo) > 0";
                                }
                                if (!strCodigoProveedor.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (empresa_codigo_prov_idx)";
                                    listaProductos += " AND LOCATE('" + strCodigoProveedor + "', p.CodigoProveedor) > 0";
                                }
                                if (!strDescripcion.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (descripcion_fulltext_idx)";
                                    listaProductos += " AND LOCATE('" + strDescripcion + "', p.Descripcion) > 0";
                                }
                                if (bolFiltraExistencias)
                                    listaProductos = "SELECT COUNT(*) FROM Producto p, ExistenciaPorSucursal e " + strUsaIndex + " WHERE p.IdProducto = e.IdProducto AND e.IdEmpresa = " + intIdEmpresa + " AND e.IdSucursal = " + intIdSucursal + " AND p.IdEmpresa = " + intIdEmpresa + " AND e.Cantidad > 0" + listaProductos;
                                else
                                    listaProductos = "SELECT COUNT(*) FROM Producto p " + strUsaIndex + " WHERE p.IdEmpresa = " + intIdEmpresa + listaProductos;
                                listaProductos += ";";
                                command.CommandText = listaProductos;
                                string result = command.ExecuteScalar().ToString();
                                if (result != null && result != "") intTotal = int.Parse(result);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Error al obtener el listado de productos por criterios: ", ex);
                            throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                        }
                        return intTotal;
                    }
                }
            }
        }

        public IList<ProductoDetalle> ObtenerListadoProductos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                using (var connection = dbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        var listaProducto = new List<ProductoDetalle>();
                        try
                        {
                            var listadoLineaPorSucursal = dbContext.LineaPorSucursalRepository.Where(y => y.IdEmpresa == intIdEmpresa && y.IdSucursal == intIdSucursal);
                            if (intIdLinea > 0)
                                listadoLineaPorSucursal = listadoLineaPorSucursal.Where(x => x.IdLinea == intIdLinea);
                            int[] lstLineasPorSucursal = listadoLineaPorSucursal.Select(x => x.IdLinea).ToArray();
                            if (lstLineasPorSucursal.Length > 0)
                            {
                                string listaProductos = " AND p.IdLinea IN(" + string.Join(",", lstLineasPorSucursal) + ")";
                                string strUsaIndex = "";        
                                if (!bolIncluyeServicios)
                                    listaProductos += " AND p.Tipo = " + StaticTipoProducto.Producto;
                                else
                                    listaProductos += " AND p.Tipo IN(1, 2, 3)";
                                if (bolFiltraActivos)
                                    listaProductos += " AND p.Activo = true";
                                if (bolFiltraConDescuento)
                                    listaProductos += " AND p.PorcDescuento > 0";
                                if (!strCodigo.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (empresa_codigo_idx)";
                                    listaProductos += " AND LOCATE('" + strCodigo + "', p.Codigo) > 0";
                                }
                                if (!strCodigoProveedor.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (empresa_codigo_prov_idx)";
                                    listaProductos += " AND LOCATE('" + strCodigoProveedor + "', p.CodigoProveedor) > 0";
                                }
                                if (!strDescripcion.Equals(string.Empty))
                                {
                                    strUsaIndex = "USE INDEX (descripcion_fulltext_idx)";
                                    listaProductos += " AND LOCATE('" + strDescripcion + "', p.Descripcion) > 0";
                                }
                                if (bolFiltraExistencias)
                                    listaProductos = "SELECT p.* FROM Producto p, ExistenciaPorSucursal e " + strUsaIndex + " WHERE p.IdProducto = e.IdProducto AND e.IdEmpresa = " + intIdEmpresa + " AND e.IdSucursal = " + intIdSucursal + " AND p.IdEmpresa = " + intIdEmpresa + " AND e.Cantidad > 0" + listaProductos;
                                else
                                    listaProductos = "SELECT p.* FROM Producto p " + strUsaIndex + " WHERE p.IdEmpresa = " + intIdEmpresa + listaProductos;
                                listaProductos += " ORDER BY p.Codigo LIMIT " + cantRec + " OFFSET " + ((numPagina - 1) * cantRec) + ";";
                                var listado = dbContext.ProductoRepository.FromSqlRaw(listaProductos).ToList();
                                foreach (var value in listado)
                                {
                                    LlaveDescripcionValor tipoImpuesto = TipoDeImpuesto.ObtenerParametro(value.IdImpuesto);
                                    var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdProducto == value.IdProducto).FirstOrDefault();
                                    decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                                    decimal decUtilidad = value.PrecioCosto > 0 ? (value.PrecioVenta1 / (1 + (tipoImpuesto.Valor / 100)) * 100 / value.PrecioCosto) - 100 : value.PrecioVenta1 > 0 ? 100 : 0;
                                    ProductoDetalle item = new ProductoDetalle(value.IdProducto, value.Codigo, value.CodigoProveedor, value.Descripcion, decCantidad, value.PrecioCosto, value.PrecioVenta1, value.Observacion, decUtilidad, value.Activo);
                                    listaProducto.Add(item);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Error al obtener el listado de productos por criterios: ", ex);
                            throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                        }
                        return listaProducto;
                    }
                }
            }
        }

        public int ObtenerTotalMovimientosPorProducto(int intIdProducto, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                try
                {
                    var listaMovimientos = dbContext.MovimientoProductoRepository.Where(x => x.IdProducto == intIdProducto && x.IdSucursal == intIdSucursal && x.Fecha > datFechaInicial && x.Fecha < datFechaFinal);
                    return listaMovimientos.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int intIdSucursal, int numPagina, int cantRec, string strFechaInicial, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
                DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                try
                {
                    var listaMovimientos = dbContext.MovimientoProductoRepository.Where(x => x.IdProducto == intIdProducto && x.IdSucursal == intIdSucursal && x.Fecha >= datFechaInicial && x.Fecha <= datFechaFinal);
                    if (cantRec > 0)
                        return listaMovimientos.OrderByDescending(x => x.Fecha).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    else
                        return listaMovimientos.OrderByDescending(x => x.Fecha).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        IList<LlaveDescripcion> ObtenerListadoCondicionVenta()
        {
            return CondicionDeVenta.ObtenerListado();
        }

        IList<LlaveDescripcion> ObtenerListadoFormaPagoCliente()
        {
            return FormaDePago.ObtenerListado().Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.Id)).ToList();
        }

        IList<LlaveDescripcion> ObtenerListadoFormaPagoEmpresa()
        {
            return FormaDePago.ObtenerListado().Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.Id)).ToList();
        }

        public void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al agregar el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error agregando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
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
                    _logger.LogError("Error al actualizar el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarBancoAdquiriente(int intIdBanco)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    BancoAdquiriente banco = dbContext.BancoAdquirienteRepository.Find(intIdBanco);
                    if (banco == null) throw new BusinessException("El banco adquiriente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(banco.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    dbContext.BancoAdquirienteRepository.Remove(banco);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al agregar el parámetro contable: ", ex);
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
                    _logger.LogError("Error al eliminar registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error eliminando el banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    return dbContext.BancoAdquirienteRepository.Find(intIdBanco);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error consultando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de registros de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de bancos adquirientes. Por favor consulte con su proveedor.");
                }
            }
        }

        IList<LlaveDescripcion> ObtenerListadoTipoMoneda()
        {
            return TipoDeMoneda.ObtenerListado();
        }

        public string AgregarAjusteInventario(AjusteInventario ajusteInventario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ajusteInventario.Fecha = Validador.ObtenerFechaHoraCostaRica();
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    MovimientoProducto movimiento = null;
                    List<MovimientoProducto> listadoMovimientos = new();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ajusteInventario.IdEmpresa && x.IdSucursal == ajusteInventario.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.AjusteInventarioRepository.Add(ajusteInventario);
                    foreach (var detalleAjuste in ajusteInventario.DetalleAjusteInventario)
                    {
                        Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Tipo != StaticTipoProducto.Producto)
                            throw new BusinessException("El tipo de producto por ajustar no puede ser un servicio. Por favor verificar.");
                        ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == ajusteInventario.IdSucursal).FirstOrDefault();
                        if (existencias != null)
                        {
                            existencias.Cantidad += detalleAjuste.Cantidad;
                            dbContext.NotificarModificacion(existencias);
                        }
                        else
                        {
                            ExistenciaPorSucursal nuevoRegistro = new ExistenciaPorSucursal
                            {
                                IdEmpresa = ajusteInventario.IdEmpresa,
                                IdSucursal = ajusteInventario.IdSucursal,
                                IdProducto = detalleAjuste.IdProducto,
                                Cantidad = detalleAjuste.Cantidad
                            };
                            dbContext.ExistenciaPorSucursalRepository.Add(nuevoRegistro);
                        }
                        movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = ajusteInventario.IdSucursal,
                            Fecha = ajusteInventario.Fecha,
                            Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Salida : StaticTipoMovimientoProducto.Entrada,
                            Origen = "Registro de ajuste de inventario",
                            Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                            PrecioCosto = detalleAjuste.PrecioCosto
                        };
                        listadoMovimientos.Add(movimiento);
                        dbContext.MovimientoProductoRepository.Add(movimiento);
                    }
                    dbContext.Commit();
                    if (listadoMovimientos.Count > 0)
                    {
                        foreach (MovimientoProducto elm in listadoMovimientos)
                        {
                            elm.Origen = "Registro de ajuste de inventario nro. " + ajusteInventario.IdAjuste;
                            dbContext.NotificarModificacion(elm);
                        }
                        dbContext.Commit();
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
                    _logger.LogError("Error al agregar el registro de ajuste de inventario: ", ex);
                    throw new Exception("Se produjo un error guardando la información del ajuste de inventario. Por favor consulte con su proveedor.");
                }
                return ajusteInventario.IdAjuste.ToString();
            }
        }

        public void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario, string strMotivoAnulacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    AjusteInventario ajusteInventario = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    if (ajusteInventario == null) throw new BusinessException("El registro de ajuste de inventario por anular no existe.");
                    if (ajusteInventario.Nulo) throw new BusinessException("El registro de ajuste de inventario ya ha sido anulado.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ajusteInventario.IdEmpresa && x.IdSucursal == ajusteInventario.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    ajusteInventario.Nulo = true;
                    ajusteInventario.IdAnuladoPor = intIdUsuario;
                    ajusteInventario.MotivoAnulacion = strMotivoAnulacion;
                    dbContext.NotificarModificacion(ajusteInventario);
                    foreach (var detalleAjuste in ajusteInventario.DetalleAjusteInventario)
                    {
                        Producto producto = dbContext.ProductoRepository.AsNoTracking().FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
                        if (producto == null)
                            throw new Exception("El producto asignado al detalle de la devolución no existe.");
                        if (producto.Tipo != StaticTipoProducto.Producto)
                            throw new BusinessException("El tipo de producto por ajustar no puede ser un servicio. Por favor verificar.");
                        ExistenciaPorSucursal existencias = dbContext.ExistenciaPorSucursalRepository.Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto && x.IdSucursal == ajusteInventario.IdSucursal).FirstOrDefault();
                        if (existencias == null)
                            throw new BusinessException("El producto " + producto.IdProducto + " no posee registro de existencias. Por favor consulte con su proveedor.");
                        existencias.Cantidad -= detalleAjuste.Cantidad;
                        dbContext.NotificarModificacion(existencias);
                        MovimientoProducto movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = ajusteInventario.IdSucursal,
                            Fecha = Validador.ObtenerFechaHoraCostaRica(),
                            Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Entrada : StaticTipoMovimientoProducto.Salida,
                            Origen = "Registro de reversión de ajuste de inventario nro. " + ajusteInventario.IdAjuste,
                            Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                            PrecioCosto = detalleAjuste.PrecioCosto
                        };
                        dbContext.MovimientoProductoRepository.Add(movimiento);
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
                    _logger.LogError("Error al anular el registro de ajuste de inventario: ", ex);
                    throw new Exception("Se produjo un error anulando el ajuste de inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    AjusteInventario ajuste = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario.Producto").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    return ajuste;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAjusteInventario(int intIdEmpresa, int intIdSucursal, int intIdAjusteInventario, string strDescripcion, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.AjusteInventarioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdAjusteInventario > 0)
                        listado = listado.Where(x => !x.Nulo && x.IdAjuste == intIdAjusteInventario);
                    else if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Descripcion.Contains(strDescripcion));
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el total del listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<AjusteInventarioDetalle> ObtenerListadoAjusteInventario(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion, string strFechaFinal)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaAjustes = new List<AjusteInventarioDetalle>();
                try
                {
                    var listado = dbContext.AjusteInventarioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdAjusteInventario > 0)
                        listado = listado.Where(x => x.IdAjuste == intIdAjusteInventario);
                    else if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (strFechaFinal != "")
                    {
                        DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);
                        listado = listado.Where(x => x.Fecha <= datFechaFinal);
                    }
                    listado = listado.OrderByDescending(x => x.IdAjuste).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var ajuste in listado)
                    {
                        AjusteInventarioDetalle item = new AjusteInventarioDetalle(ajuste.IdAjuste, ajuste.Fecha.ToString("dd/MM/yyyy"), ajuste.Descripcion, ajuste.Nulo);
                        listaAjustes.Add(item);
                    }
                    return listaAjustes;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTipoIdentificacion()
        {
            return TipoDeIdentificacion.ObtenerListado();
        }

        public IList<LlaveDescripcion> ObtenerListadoCatalogoReportes()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de reportes: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de reportes. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoProvincias()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de provincias: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de provincias. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de cantones: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cantones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de distritos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de distritos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
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
                    _logger.LogError("Error al obtener el listado de barrios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de barrios. Por favor consulte con su proveedor.");
                }
            }
        }

        private IList<LlaveDescripcion> ObtenerListadoTipodePrecio()
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
                _logger.LogError("Error al obtener el listado de tipos de precio: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de precio. Por favor consulte con su proveedor.");
            }
        }

        public int ObtenerTotalListaClasificacionProducto(string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.ClasificacionProductoRepository.AsQueryable();
                    if (!strDescripcion.Equals(String.Empty))
                    {
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    }
                    return listado.Count();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de barrios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de barrios. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<ClasificacionProducto> ObtenerListadoClasificacionProducto(int numPagina, int cantRec, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    var listado = dbContext.ClasificacionProductoRepository.AsQueryable();
                    if (!strDescripcion.Equals(String.Empty))
                    {
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    }
                    return listado.OrderByDescending(x => x.Descripcion).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de clasificaciones de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de clasificaciones de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public ClasificacionProducto ObtenerClasificacionProducto(string strCodigo)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    ClasificacionProducto clasificacionProducto = dbContext.ClasificacionProductoRepository.Where(x => x.Id == strCodigo).FirstOrDefault();
                    return clasificacionProducto;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la clasificación del producto: ", ex);
                    throw new Exception("Se produjo un error consultando la clasificación del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarPuntoDeServicio(PuntoDeServicio puntoDeServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.PuntoDeServicioRepository.Add(puntoDeServicio);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al agregar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error agragando la información del punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarPuntoDeServicio(PuntoDeServicio puntoDeServicio)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    dbContext.NotificarModificacion(puntoDeServicio);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al actualizar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarPuntoDeServicio(int intIdPunto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    PuntoDeServicio puntoDeServicio = dbContext.PuntoDeServicioRepository.Find(intIdPunto);
                    if (puntoDeServicio == null)
                        throw new BusinessException("El punto de servicio por eliminar no existe.");
                    dbContext.PuntoDeServicioRepository.Remove(puntoDeServicio);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Validación al eliminar el punto de servicio: ", ex);
                    throw new BusinessException("No es posible eliminar el punto de servicio seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    _logger.LogError("Error al eliminar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error eliminando al punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public PuntoDeServicio ObtenerPuntoDeServicio(int intIdPunto)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    PuntoDeServicio puntoDeServicio = dbContext.PuntoDeServicioRepository.Where(x => x.IdPunto == intIdPunto).FirstOrDefault();
                    return puntoDeServicio;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener la entidad PuntoDeServicio: ", ex);
                    throw new Exception("Se produjo un error consultando el punto  de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoPuntoDeServicio(int intIdEmpresa, int intIdSucursal, bool bolSoloActivo, string strDescripcion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                var listaPuntos = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.PuntoDeServicioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (bolSoloActivo)
                        listado = listado.Where(x => x.Activo);
                    if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => x.Descripcion.Contains(strDescripcion));
                    foreach (var value in listado)
                    {
                        LlaveDescripcion item = new LlaveDescripcion(value.IdPunto, value.Descripcion);
                        listaPuntos.Add(item);
                    }
                    return listaPuntos;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al obtener el listado de puntos de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de puntos de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        private string GenerarRegistroAutenticacion(int intIdEmpresa, string strCodigoUsuario, int intRole)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                string strGuid = Guid.NewGuid().ToString();
                DateTime fechaRegistro = Validador.ObtenerFechaHoraCostaRica();
                RegistroAutenticacion registro = new RegistroAutenticacion
                {
                    IdEmpresa = intIdEmpresa,
                    CodigoUsuario = strCodigoUsuario,
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
                string strTokenEncriptado = Encriptador.EncriptarDatos(strGuid);
                return strTokenEncriptado;
            }
        }

        public void ValidarRegistroAutenticacion(string strToken, int intRole, int intHoras)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    string strTokenDesencriptado = Encriptador.DesencriptarDatos(strToken);
                    RegistroAutenticacion registro = dbContext.RegistroAutenticacionRepository.Where(x => x.Id == strTokenDesencriptado).FirstOrDefault();
                    if (registro == null) throw new BusinessException("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    if (registro.Fecha < Validador.ObtenerFechaHoraCostaRica().AddHours(-1 * intHoras))
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
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar el registro de autenticación: ", ex);
                    throw ex;
                }
            }
        }

        public void EliminarRegistroAutenticacionInvalidos()
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    DateTime detFechaMaxima = Validador.ObtenerFechaHoraCostaRica().AddHours(-12);
                    dbContext.RegistroAutenticacionRepository.RemoveRange(dbContext.RegistroAutenticacionRepository.Where(x => x.Fecha < detFechaMaxima));
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al validar el registro de autenticación: ", ex);
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoActividadEconomica(string strServicioURL, string strIdentificacion)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                List<LlaveDescripcion> listado = new List<LlaveDescripcion>();
                try
                {
                    string strInformacionContribuyente = ComprobanteElectronicoService.ObtenerInformacionContribuyente(strServicioURL, strIdentificacion);
                    JObject datosJO = JObject.Parse(strInformacionContribuyente);
                    if (datosJO.Property("actividades") == null) throw new Exception("La respuesta del servicio de consulta de informacion del contribuyente no posee una entrada para 'actividades'");
                    JArray actividades = JArray.Parse(datosJO.Property("actividades").Value.ToString());
                    foreach (JObject item in actividades)
                    {
                        listado.Add(new LlaveDescripcion(int.Parse(item.Property("codigo").Value.ToString()), item.Property("descripcion").Value.ToString()));
                    }
                    return listado;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al consultar el listado de actividades económicas del contribuyente: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de actividades económicas del contribuyente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void IniciarRestablecerClaveUsuario(string strServicioWebURL, string strIdentificacion, string strCodigoUsuario)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.Identificacion == strIdentificacion).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < Validador.ObtenerFechaHoraCostaRica()) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    Usuario usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strCodigoUsuario.ToUpper());
                    if (usuario == null) throw new BusinessException("Se produjo un error en el proceso de restablecimiento de su contraseña. Por favor verifique la información suministrada!");
                    string strToken = GenerarRegistroAutenticacion(empresa.IdEmpresa, usuario.CodigoUsuario, StaticRolePorUsuario.USUARIO_SISTEMA);
                    JArray archivosJArray = new JArray();
                    servicioCorreo.SendEmail(new string[] { empresa.CorreoNotificacion }, new string[] { }, "Solicitud para restablecer la contraseña", "Adjunto se adjunta el link para restablecer la contraseña.\n\n" + strServicioWebURL + "reset?id=" + strToken.Replace("/", "~") + "\n\nEl acceso es válido por un único intento y expira en 1 hora.", false, archivosJArray);
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al iniciar el proceso de restablecimiento de la clave del usuario: ", ex);
                    throw new Exception("Se produjo un error al iniciar el proceso de restablecimiento de la clave del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void RestablecerClaveUsuario(string strToken, string strClave)
        {
            using (var dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<LeandroContext>())
            {
                try
                {
                    string strTokenDesencriptado = Encriptador.DesencriptarDatos(strToken);
                    RegistroAutenticacion registro = dbContext.RegistroAutenticacionRepository.Where(x => x.Id == strTokenDesencriptado).FirstOrDefault();
                    if (registro == null) throw new BusinessException("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    if (registro.Fecha < Validador.ObtenerFechaHoraCostaRica().AddHours(-1)) throw new BusinessException("La sessión del usuario se encuentra expirada. Debe reiniciar su sesión.");
                    if (registro.Role != StaticRolePorUsuario.USUARIO_SISTEMA) throw new BusinessException("El usuario no se encuentra autorizado para ejecutar la acción solicitada.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("PlanFacturacion").Where(x => x.IdEmpresa == registro.IdEmpresa).FirstOrDefault();
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < Validador.ObtenerFechaHoraCostaRica()) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    Usuario usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == registro.CodigoUsuario.ToUpper());
                    if (usuario == null) throw new BusinessException("Se produjo un error en el proceso de restablecimiento de su contraseña. Por favor verifique la información suministrada!");
                    usuario.Clave = strClave;
                    dbContext.NotificarModificacion(usuario);
                    dbContext.NotificarEliminacion(registro);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error al restablecer la clave del usuario: ", ex);
                    throw new Exception("Se produjo un error al restablecer la clave del usuario. Por favor consulte con su proveedor.");
                }
            }
        }
    }
}