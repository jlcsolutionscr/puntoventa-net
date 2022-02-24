using LeandroSoftware.Common.Constantes;
using LeandroSoftware.Common.DatosComunes;
using LeandroSoftware.Common.Dominio.Entidades;
using LeandroSoftware.Common.Seguridad;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using LeandroSoftware.ServicioWeb.Parametros;
using LeandroSoftware.ServicioWeb.Utilitario;

namespace LeandroSoftware.ServicioWeb.Servicios
{
    public interface IMantenimientoService
    {
        // Métodos para administrar parametros del sistema
        IList<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo);
        IList<LlaveDescripcion> ObtenerListadoEmpresasAdministrador();
        IList<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId);
        bool EnModoMantenimiento();
        void RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId);
        Usuario ValidarCredencialesAdmin(string strUsuario, string strClave);
        Empresa ValidarCredenciales(string strUsuario, string strClave, string id);
        Empresa ValidarCredenciales(string strUsuario, string strClave, int intIdEmpresa, string strValorRegistro);
        bool ValidarUsuarioHacienda(string strUsuario, string strClave, ConfiguracionGeneral config);
        decimal AutorizacionPorcentaje(string strUsuario, string strClave, int intIdEmpresa);
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
        List<LlaveDescripcion> ObtenerListadoReportePorEmpresa(int intIdEmpresa);
        List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa, bool bolAdministrator);
        void ActualizarReportePorEmpresa(int intIdEmpresa, List<ReportePorEmpresa> listado);
        void ActualizarRolePorEmpresa(int intIdEmpresa, List<RolePorEmpresa> listado);
        string ObtenerLogotipoEmpresa(int intIdEmpresa);
        void ActualizarLogoEmpresa(int intIdEmpresa, string strLogo);
        void ActualizarCertificadoEmpresa(int intIdEmpresa, string strCertificado);
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
        IList<LlaveDescripcion> ObtenerListadoTipoProducto(string strUsuario);
        IList<LlaveDescripcion> ObtenerListadoTipoExoneracion();
        IList<LlaveDescripcionValor> ObtenerListadoTipoImpuesto();
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
        // Métodos para obtener las condiciones de venta para facturación
        IList<LlaveDescripcion> ObtenerListadoCondicionVenta();
        // Métodos para obtener las formas de pago por tipo de servicio
        IList<LlaveDescripcion> ObtenerListadoFormaPagoCliente();
        IList<LlaveDescripcion> ObtenerListadoFormaPagoEmpresa();
        // Métodos para administrar los parámetros de banco adquiriente
        void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void EliminarBancoAdquiriente(int intIdBanco);
        BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco);
        IList<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion);
        IList<LlaveDescripcion> ObtenerListadoTipoMoneda();
        // Métodos para administrar los ajustes de inventario
        string AgregarAjusteInventario(AjusteInventario ajusteInventario);
        void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario, string strMotivoAnulacion);
        AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario);
        int ObtenerTotalListaAjusteInventario(int intIdEmpresa, int intIdSucursal, int intIdAjusteInventario, string strDescripcion);
        IList<AjusteInventarioDetalle> ObtenerListadoAjusteInventario(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion);
        // Métodos para obtener parámetros generales del sistema
        IList<LlaveDescripcion> ObtenerListadoTipoIdentificacion();
        IList<LlaveDescripcion> ObtenerListadoCatalogoReportes();
        CatalogoReporte ObtenerCatalogoReporte(int intIdReporte);
        IList<LlaveDescripcion> ObtenerListadoProvincias();
        IList<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia);
        IList<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton);
        IList<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito);
        IList<LlaveDescripcion> ObtenerListadoTipodePrecio();
        int ObtenerTotalListaClasificacionProducto(string strDescripcion);
        IList<ClasificacionProducto> ObtenerListadoClasificacionProducto(int numPagina, int cantRec, string strDescripcion);
        ClasificacionProducto ObtenerClasificacionProducto(string strCodigo);
        void AgregarPuntoDeServicio(PuntoDeServicio puntoDeServicio);
        void ActualizarPuntoDeServicio(PuntoDeServicio puntoDeServicio);
        void EliminarPuntoDeServicio(int intIdPunto);
        PuntoDeServicio ObtenerPuntoDeServicio(int intIdPunto);
        IList<LlaveDescripcion> ObtenerListadoPuntoDeServicio(int intIdEmpresa, int intIdSucursal, bool bolSoloActivo, string strDescripcion);
        void ValidarRegistroAutenticacion(string strToken, int intRole);
        void EliminarRegistroAutenticacionInvalidos();

        decimal ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta);
    }

    public class MantenimientoService : IMantenimientoService
    {
        private static ILeandroContext dbContext;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";

        public MantenimientoService(ILeandroContext pContext)
        {
            try
            {
                dbContext = pContext;
            }
            catch (Exception ex)
            {
                log.Error("Error al inicializar el servicio: ", ex);
                throw new Exception("Se produjo un error al inicializar el servicio de Mantenimiento. Por favor consulte con su proveedor.");
            }
        }

        public IList<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
        {
            var listaEquipoRegistrado = new List<EquipoRegistrado>();
            try
            {
                Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                Usuario usuario = null;
                if (strUsuario.ToUpper() == "ADMIN")
                {
                    usuario = dbContext.UsuarioRepository.Include("RolePorUsuario").FirstOrDefault(x => x.IdUsuario == 1);
                }
                else
                {
                    usuario = dbContext.UsuarioRepository.Include("SucursalPorUsuario").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault().IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strUsuario.ToUpper());

                }
                if (usuario == null)
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
                log.Error("Error al obtener el listado de terminales disponibles: ", ex);
                throw new Exception("Error al obtener las terminales disponibles. . .");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresasAdministrador()
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

        public IList<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa)
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
                log.Error("Error al validar la lista de sucursales asignadas a la empresa: ", ex);
                throw new Exception("Error al validar la lista de sucursales asignadas a la empresa. . .");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal)
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
                log.Error("Error al validar la lista de terminales asignadas a la empresa: ", ex);
                throw new Exception("Error al validar la lista de terminales asignadas a la empresa. . .");
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId)
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
                log.Error("Error al validar la lista de empresas asignadas a una terminal: ", ex);
                throw new Exception("Error al validar la lista de empresas asignadas a una terminal. . .");
            }
        }

        public bool EnModoMantenimiento()
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
                log.Error("Error al consultar el parámetro de modo mantenimiento del sistema: ", ex);
                throw new Exception("No es posible consultar el modo mantenimieto del sistema.");
            }
        }

        public void RegistrarTerminal(string strUsuario, string strClave, string strIdentificacion, int intIdSucursal, int intIdTerminal, int intTipoDispositivo, string strDispositivoId)
        {
            try
            {
                Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                Usuario usuario = null;
                if (strUsuario.ToUpper() == "ADMIN")
                {
                    usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                }
                else
                {
                    usuario = dbContext.UsuarioRepository.Include("SucursalPorUsuario").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault().IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario == strUsuario.ToUpper());
                }
                if (usuario == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
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
                log.Error("Error al registrar el dispositivo movil para la identificación suministrada: ", ex);
                throw new Exception("Error al registrar el dispositivo movil para la identificación suministrada.");
            }
        }

        public Usuario ValidarCredencialesAdmin(string strUsuario, string strClave)
        {

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

        public Empresa ValidarCredenciales(string strUsuario, string strClave, string strIdentificacion)
        {

            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("SucursalPorEmpresa").Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.Identificacion == strIdentificacion);
                    if (empresa == null) throw new BusinessException("La identificación suministrada no pertenece a ninguna empresa registrada en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "ADMIN")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                        usuario.IdSucursal = 1;
                    }
                    else
                    {
                        if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").Include("SucursalPorUsuario").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault(z => z.IdEmpresa == empresa.IdEmpresa) != null && x.CodigoUsuario == strUsuario.ToUpper());
                        usuario.IdSucursal = usuario.SucursalPorUsuario.FirstOrDefault().IdSucursal;
                        usuario.SucursalPorUsuario = new List<SucursalPorUsuario> {};
                    }
                    if (usuario == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdSucursal == usuario.IdSucursal).FirstOrDefault();
                    TerminalPorSucursal terminal = dbContext.TerminalPorSucursalRepository.Where(x => x.IdEmpresa == empresa.IdEmpresa && x.IdTerminal == 1).FirstOrDefault();
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
                    string strToken = GenerarRegistroAutenticacion(StaticRolePorUsuario.USUARIO_SISTEMA);
                    usuario.Token = strToken;
                    empresa.Logotipo = null;
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

            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "ADMIN")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                        usuario.IdSucursal = 1;
                    }
                    else
                    {
                        if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                        if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                        usuario = dbContext.UsuarioRepository.Include("SucursalPorUsuario").Include("RolePorUsuario.Role").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault(y => y.IdEmpresa == empresa.IdEmpresa) != null && x.CodigoUsuario == strUsuario.ToUpper());
                        usuario.IdSucursal = usuario.SucursalPorUsuario.FirstOrDefault().IdSucursal;
                    }
                    if (usuario == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    TerminalPorSucursal terminal = null;
                    SucursalPorEmpresa sucursal = null;
                    if (strUsuario.ToUpper() == "ADMIN")
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
                        ImpresoraFactura = terminal.ImpresoraFactura,
                        AnchoLinea = terminal.AnchoLinea
                    };
                    string strToken = GenerarRegistroAutenticacion(StaticRolePorUsuario.USUARIO_SISTEMA);
                    usuario.SucursalPorUsuario = new List<SucursalPorUsuario> { };
                    usuario.Token = strToken;
                    empresa.SucursalPorEmpresa = new List<SucursalPorEmpresa> { };
                    empresa.Logotipo = null;
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
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar los credenciales del usuario por terminal: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public bool ValidarUsuarioHacienda(string strUsuario, string strClave, ConfiguracionGeneral config)
        {
            bool bolRespuesta = false;
            try
            {
                TokenType token = ComprobanteElectronicoService.ObtenerToken(config.ServicioTokenURL, config.ClientId, strUsuario, strClave).Result;
                if (token.access_token != null) bolRespuesta = true;
            }
            catch (Exception ex)
            {
                log.Error("Error al validar los credenciales del usuario en Hacienda: ", ex);
            }
            return bolRespuesta;
        }

        public decimal AutorizacionPorcentaje(string strUsuario, string strClave, int intIdEmpresa)
        {

            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("SucursalPorUsuario").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault().IdEmpresa == intIdEmpresa && x.CodigoUsuario == strUsuario.ToUpper());
                    if (usuario == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    return usuario.PorcMaxDescuento;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar los credenciales del usuario por terminal: ", ex);
                    throw new Exception("Error en la validación de los credenciales suministrados por favor verifique la información. . .");
                }
            }
        }

        public string ObtenerUltimaVersionApp()
        {
            string strUltimaVersion = "";

            {
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 1).FirstOrDefault();
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

        public string ObtenerUltimaVersionMobileApp()
        {
            string strUltimaVersion = "";

            {
                try
                {
                    var version = dbContext.ParametroSistemaRepository.Where(x => x.IdParametro == 4).FirstOrDefault();
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

        public IList<ParametroSistema> ObtenerListadoParametros()
        {

            {
                var listaEmpresa = new List<LlaveDescripcion>();
                try
                {
                    return dbContext.ParametroSistemaRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al consultar el listado d parámetros del sistema: ", ex);
                    throw new Exception("Error al consultar el listado d parámetros del sistema. . .");
                }
            }
        }

        public void ActualizarParametroSistema(int intIdParametro, string strValor)
        {

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
                    log.Error("Error al actualizar el parámetro del sistema: ", ex);
                    throw new Exception("Se produjo un error actualizando el parámetro del sistema. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresa()
        {

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

        public Empresa ObtenerEmpresa(int intIdEmpresa)
        {

            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    empresa.Certificado = null;
                    empresa.Logotipo = null;
                    empresa.AccessToken = null;
                    empresa.RefreshToken = null;
                    empresa.EmitedAt = null;
                    empresa.ExpiresIn = null;
                    empresa.RefreshExpiresIn = null;
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

        public void ActualizarEmpresa(Empresa empresa)
        {

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
                    Empresa noTracking = dbContext.EmpresaRepository.AsNoTracking().Where(x => x.IdEmpresa == empresa.IdEmpresa).FirstOrDefault();
                    empresa.Barrio = null;
                    if (noTracking != null && noTracking.Certificado != null) empresa.Certificado = noTracking.Certificado;
                    if (noTracking != null && noTracking.Logotipo != null) empresa.Logotipo = noTracking.Logotipo;
                    dbContext.NotificarModificacion(empresa);
                    dbContext.Commit();
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar la empresa: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoReportePorEmpresa(int intIdEmpresa)
        {

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
                    log.Error("Error al obtener el logotipo de la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando el logotipo de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa, bool bolAdministrator)
        {

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
                    log.Error("Error al obtener el listado de registros de roles de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de roles de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarReportePorEmpresa(int intIdEmpresa, List<ReportePorEmpresa> listado)
        {

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
                    log.Error("Error al actualizar el listado de reportes por empresa: ", ex);
                    throw new Exception("Se produjo un error al actualizar el listado de reportes por empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarRolePorEmpresa(int intIdEmpresa, List<RolePorEmpresa> listado)
        {

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
                    log.Error("Error al actualizar el listado de roles por empresa: ", ex);
                    throw new Exception("Se produjo un error al actualizar el listado de roles por empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public string ObtenerLogotipoEmpresa(int intIdEmpresa)
        {

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

            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(intIdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    byte[] bytCertificado = Convert.FromBase64String(strCertificado);
                    ComprobanteElectronicoService.ValidarCertificado(empresa.PinCertificado, bytCertificado);
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

        public void EliminarRegistrosPorEmpresa(int intIdEmpresa)
        {

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
                    log.Error("Error al actualizar la sucursal: ", ex);
                    throw new Exception("Se produjo un error actualizando la información de la sucursal. Por favor consulte con su proveedor.");
                }
            }
        }

        public TerminalPorSucursal ObtenerTerminalPorSucursal(int intIdEmpresa, int intIdSucursal, int intIdTerminal)
        {

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

            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    if (usuario.SucursalPorUsuario.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.SucursalPorUsuario.FirstOrDefault().IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    Usuario usuarioExistente = dbContext.UsuarioRepository.Include("SucursalPorUsuario").FirstOrDefault(x => x.SucursalPorUsuario.FirstOrDefault().IdEmpresa == empresa.IdEmpresa && x.CodigoUsuario.Contains(usuario.CodigoUsuario.ToUpper()));
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

            {
                try
                {
                    usuario.CodigoUsuario = usuario.CodigoUsuario.ToUpper();
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
                    if (usuario.SucursalPorUsuario.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.SucursalPorUsuario.FirstOrDefault().IdEmpresa);
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
                    log.Error("Error al actualizar el usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave)
        {

            {
                try
                {
                    Usuario usuario = usuario = dbContext.UsuarioRepository.FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null) throw new Exception("El usuario seleccionado para la actualización de la clave no existe.");
                    if (usuario.SucursalPorUsuario.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.SucursalPorUsuario.FirstOrDefault().IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    usuario.Clave = strClave;
                    dbContext.NotificarModificacion(usuario);
                    dbContext.Commit();
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

        public void EliminarUsuario(int intIdUsuario)
        {

            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Where(x => x.IdUsuario == intIdUsuario).FirstOrDefault();
                    if (usuario == null) throw new BusinessException("El usuario por eliminar no existe.");
                    if (usuario.SucursalPorUsuario.Count == 0) throw new BusinessException("El usuario por modificar debe estar vinculado a la empresa actual. Por favor, pongase en contacto con su proveedor del servicio.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.SucursalPorUsuario.FirstOrDefault().IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    List<RolePorUsuario> listaRole = dbContext.RolePorUsuarioRepository.Where(x => x.IdUsuario == usuario.IdUsuario).ToList();
                    foreach (RolePorUsuario roleUsuario in listaRole)
                        dbContext.NotificarEliminacion(roleUsuario);
                    foreach (SucursalPorUsuario sucursalUsuario in usuario.SucursalPorUsuario)
                        dbContext.NotificarEliminacion(sucursalUsuario);
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

            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("SucursalPorUsuario").Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null)
                        throw new BusinessException("El usuario por consultar no existe");
                    foreach (SucursalPorUsuario sucursal in usuario.SucursalPorUsuario)
                        sucursal.Usuario = null;
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

        public IList<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo)
        {

            {
                var listaUsuario = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.UsuarioRepository.Include("SucursalPorUsuario").Where(x => x.SucursalPorUsuario.FirstOrDefault(y => y.IdEmpresa == intIdEmpresa) != null && x.IdUsuario > 2);
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
                    log.Error("Error al obtener el listado de usuarios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de usuarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarVendedor(Vendedor vendedor)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoRoles()
        {

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

        public IList<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoTipoProducto(string strUsuario)
        {
            var tiposProducto = strUsuario.ToUpper() == "ADMIN" ? new int[] { 1, 2, 3, 4, 5 } : new int[] { 1, 2, 3 };
            return TipoDeProducto.ObtenerListado().Where(x => tiposProducto.Contains(x.Id)).ToList();
        }

        public IList<LlaveDescripcion> ObtenerListadoTipoExoneracion()
        {
            return TipoDeExoneracion.ObtenerListado();
        }

        public IList<LlaveDescripcionValor> ObtenerListadoTipoImpuesto()
        {
            return TipoDeImpuesto.ObtenerListado();
        }

        public LlaveDescripcionValor ObtenerParametroImpuesto(int intIdImpuesto)
        {
            return TipoDeImpuesto.ObtenerParametro(intIdImpuesto);
        }

        public void AgregarProducto(Producto producto)
        {

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
                    log.Error("Error al actualizar el precio de venta del inventario: ", ex);
                    throw new Exception("Se produjo un error actualizando el precio de venta del inventario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarProducto(int intIdProducto)
        {

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

        public Producto ObtenerProducto(int intIdProducto, int intIdSucursal)
        {

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
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoEspecial(int intIdEmpresa, int intIdTipo)
        {

            {
                try
                {
                    return dbContext.ProductoRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Tipo == intIdTipo);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el código de producto transitorio: ", ex);
                    throw new Exception("Se produjo un error consultando el producto transitorio. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigo(int intIdEmpresa, string strCodigo, int intIdSucursal)
        {

            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.Codigo.Equals(strCodigo)).FirstOrDefault();
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
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigoProveedor(int intIdEmpresa, string strCodigo, int intIdSucursal)
        {

            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.CodigoProveedor.Equals(strCodigo)).FirstOrDefault();
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
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProductos(int intIdEmpresa, int intIdSucursal, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion)
        {

            {
                try
                {
                    List<ReporteInventario> listaReporte = new List<ReporteInventario>();
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && new int[] { 1, 2, 3 }.Contains(x.Tipo));
                    if (!bolIncluyeServicios)
                        listaProductos = listaProductos.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    if (bolFiltraActivos)
                        listaProductos = listaProductos.Where(x => x.Activo);
                    if (bolFiltraConDescuento)
                        listaProductos = listaProductos.Where(x => x.PorcDescuento > 0);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    if (!strCodigoProveedor.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.CodigoProveedor.Contains(strCodigoProveedor));
                    if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (bolFiltraExistencias)
                        return listaProductos.Join(dbContext.ExistenciaPorSucursalRepository, x => x.IdProducto, y => y.IdProducto, (x, y) => new { x, y }).Where(x => x.y.IdEmpresa == intIdEmpresa && x.y.IdSucursal == intIdSucursal && x.y.Cantidad > 0).Count();
                    else
                        return listaProductos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<ProductoDetalle> ObtenerListadoProductos(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, bool bolIncluyeServicios, bool bolFiltraActivos, bool bolFiltraExistencias, bool bolFiltraConDescuento, int intIdLinea, string strCodigo, string strCodigoProveedor, string strDescripcion)
        {

            {
                var listaProducto = new List<ProductoDetalle>();
                try
                {
                    List<ProductoDetalle> listaReporte = new List<ProductoDetalle>();
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa && new int[] { 1, 2, 3 }.Contains(x.Tipo));
                    if (!bolIncluyeServicios)
                        listaProductos = listaProductos.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    if (bolFiltraActivos)
                        listaProductos = listaProductos.Where(x => x.Activo);
                    if (bolFiltraConDescuento)
                        listaProductos = listaProductos.Where(x => x.PorcDescuento > 0);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    if (!strCodigoProveedor.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.CodigoProveedor.Contains(strCodigoProveedor));
                    if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (bolFiltraExistencias)
                    {
                        var listado = listaProductos.Join(dbContext.ExistenciaPorSucursalRepository, x => x.IdProducto, y => y.IdProducto, (x, y) => new { x, y }).Where(x => x.y.IdEmpresa == intIdEmpresa && x.y.IdSucursal == intIdSucursal && x.y.Cantidad > 0).OrderBy(x => x.x.Codigo).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                        foreach (var value in listado)
                        {
                            LlaveDescripcionValor tipoImpuesto = TipoDeImpuesto.ObtenerParametro(value.x.IdImpuesto);
                            decimal decUtilidad = value.x.PrecioCosto > 0 ? ((value.x.PrecioVenta1 / (1 + (tipoImpuesto.Valor / 100))) * 100 / value.x.PrecioCosto) - 100 : value.x.PrecioVenta1 > 0 ? 100 : 0;
                            ProductoDetalle item = new ProductoDetalle(value.x.IdProducto, value.x.Codigo, value.x.CodigoProveedor, value.x.Descripcion, value.y.Cantidad, value.x.PrecioCosto, value.x.PrecioVenta1, value.x.Observacion, decUtilidad, value.x.Activo);
                            listaProducto.Add(item);
                        }
                    }
                    else
                    {
                        var listado = listaProductos.OrderBy(x => x.Codigo).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                        foreach (var value in listado)
                        {
                            LlaveDescripcionValor tipoImpuesto = TipoDeImpuesto.ObtenerParametro(value.IdImpuesto);
                            var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdProducto == value.IdProducto).FirstOrDefault();
                            decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                            decimal decUtilidad = value.PrecioCosto > 0 ? ((value.PrecioVenta1 / (1 + (tipoImpuesto.Valor / 100))) * 100 / value.PrecioCosto) - 100 : value.PrecioVenta1 > 0 ? 100 : 0;
                            ProductoDetalle item = new ProductoDetalle(value.IdProducto, value.Codigo, value.CodigoProveedor, value.Descripcion, decCantidad, value.PrecioCosto, value.PrecioVenta1, value.Observacion, decUtilidad, value.Activo);
                            listaProducto.Add(item);
                        }
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

        public int ObtenerTotalMovimientosPorProducto(int intIdProducto, int intIdSucursal, string strFechaInicial, string strFechaFinal)
        {
            DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
            DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);

            {
                try
                {
                    var listaMovimientos = dbContext.MovimientoProductoRepository.Where(x => x.IdProducto == intIdProducto && x.IdSucursal == intIdSucursal && x.Fecha > datFechaInicial && x.Fecha < datFechaFinal);
                    return listaMovimientos.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int intIdSucursal, int numPagina, int cantRec, string strFechaInicial, string strFechaFinal)
        {
            DateTime datFechaInicial = DateTime.ParseExact(strFechaInicial + " 00:00:01", strFormat, provider);
            DateTime datFechaFinal = DateTime.ParseExact(strFechaFinal + " 23:59:59", strFormat, provider);

            {
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
                    log.Error("Error al obtener el listado de productos por criterios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de productos por criterio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoCondicionVenta()
        {
            return CondicionDeVenta.ObtenerListado();
        }

        public IList<LlaveDescripcion> ObtenerListadoFormaPagoCliente()
        {
            return FormaDePago.ObtenerListado().Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.Id)).ToList();
        }

        public IList<LlaveDescripcion> ObtenerListadoFormaPagoEmpresa()
        {
            return FormaDePago.ObtenerListado().Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.Id)).ToList();
        }

        public void AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {

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
                    log.Error("Error al actualizar el registro de banco adquiriente: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del banco adquiriente. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarBancoAdquiriente(int intIdBanco)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoTipoMoneda()
        {
            return TipoDeMoneda.ObtenerListado();
        }

        public string AgregarAjusteInventario(AjusteInventario ajusteInventario)
        {

            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null) throw new BusinessException("Empresa no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    SucursalPorEmpresa sucursal = dbContext.SucursalPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == ajusteInventario.IdEmpresa && x.IdSucursal == ajusteInventario.IdSucursal);
                    if (sucursal == null) throw new BusinessException("Sucursal no registrada en el sistema. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (sucursal.CierreEnEjecucion) throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.AjusteInventarioRepository.Add(ajusteInventario);
                    foreach (var detalleAjuste in ajusteInventario.DetalleAjusteInventario)
                    {
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
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
                        MovimientoProducto movimiento = new MovimientoProducto
                        {
                            IdProducto = producto.IdProducto,
                            IdSucursal = ajusteInventario.IdSucursal,
                            Fecha = DateTime.Now,
                            Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Salida : StaticTipoMovimientoProducto.Entrada,
                            Origen = "Registro de ajuste de inventario",
                            Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                            PrecioCosto = detalleAjuste.PrecioCosto
                        };
                        producto.MovimientoProducto.Add(movimiento);
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
                    log.Error("Error al agregar el registro de ajuste de inventario: ", ex);
                    throw new Exception("Se produjo un error guardando la información del ajuste de inventario. Por favor consulte con su proveedor.");
                }
            }
            return ajusteInventario.IdAjuste.ToString();
        }

        public void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario, string strMotivoAnulacion)
        {

            {
                try
                {
                    AjusteInventario ajusteInventario = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    if (ajusteInventario == null) throw new Exception("El registro de ajuste de inventario por anular no existe.");
                    if (ajusteInventario.Nulo == true) throw new BusinessException("El registro de ajuste de inventario ya ha sido anulado.");
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
                        Producto producto = dbContext.ProductoRepository.Include("Linea").FirstOrDefault(x => x.IdProducto == detalleAjuste.IdProducto);
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
                            Fecha = DateTime.Now,
                            Tipo = detalleAjuste.Cantidad < 0 ? StaticTipoMovimientoProducto.Entrada : StaticTipoMovimientoProducto.Salida,
                            Origen = "Registro de reversión de ajuste de inventario",
                            Cantidad = detalleAjuste.Cantidad < 0 ? detalleAjuste.Cantidad * -1 : detalleAjuste.Cantidad,
                            PrecioCosto = detalleAjuste.PrecioCosto
                        };
                        producto.MovimientoProducto.Add(movimiento);
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

            {
                try
                {
                    AjusteInventario ajuste = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario.Producto").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    return ajuste;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el registro de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la factura. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaAjusteInventario(int intIdEmpresa, int intIdSucursal, int intIdAjusteInventario, string strDescripcion)
        {

            {
                try
                {
                    var listaAjusteInventario = dbContext.AjusteInventarioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
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

        public IList<AjusteInventarioDetalle> ObtenerListadoAjusteInventario(int intIdEmpresa, int intIdSucursal, int numPagina, int cantRec, int intIdAjusteInventario, string strDescripcion)
        {

            {
                var listaAjustes = new List<AjusteInventarioDetalle>();
                try
                {
                    var listado = dbContext.AjusteInventarioRepository.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal);
                    if (intIdAjusteInventario > 0)
                        listado = listado.Where(x => !x.Nulo && x.IdAjuste == intIdAjusteInventario);
                    else if (!strDescripcion.Equals(string.Empty))
                        listado = listado.Where(x => !x.Nulo && x.IdEmpresa == intIdEmpresa && x.Descripcion.Contains(strDescripcion));
                    listado = listado.OrderByDescending(x => x.IdAjuste).Skip((numPagina - 1) * cantRec).Take(cantRec);
                    foreach (var value in listado)
                    {
                        AjusteInventarioDetalle item = new AjusteInventarioDetalle(value.IdAjuste, value.Fecha.ToString("dd/MM/yyyy"), value.Descripcion);
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

        public IList<LlaveDescripcion> ObtenerListadoTipoIdentificacion()
        {
            return TipoDeIdentificacion.ObtenerListado();
        }

        public IList<LlaveDescripcion> ObtenerListadoCatalogoReportes()
        {

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

        public IList<LlaveDescripcion> ObtenerListadoProvincias()
        {

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

        public IList<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
        {

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

        public IList<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {

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

        public int ObtenerTotalListaClasificacionProducto(string strDescripcion)
        {

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
                    log.Error("Error al obtener el listado de barrios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de barrios. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<ClasificacionProducto> ObtenerListadoClasificacionProducto(int numPagina, int cantRec, string strDescripcion)
        {

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
                    log.Error("Error al obtener el listado de clasificaciones de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de clasificaciones de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public ClasificacionProducto ObtenerClasificacionProducto(string strCodigo)
        {

            {
                try
                {
                    ClasificacionProducto clasificacionProducto = dbContext.ClasificacionProductoRepository.Where(x => x.Id == strCodigo).FirstOrDefault();
                    return clasificacionProducto;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la clasificación del producto: ", ex);
                    throw new Exception("Se produjo un error consultando la clasificación del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public void AgregarPuntoDeServicio(PuntoDeServicio puntoDeServicio)
        {

            {
                try
                {
                    dbContext.PuntoDeServicioRepository.Add(puntoDeServicio);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error agragando la información del punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void ActualizarPuntoDeServicio(PuntoDeServicio puntoDeServicio)
        {

            {
                try
                {
                    dbContext.NotificarModificacion(puntoDeServicio);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarPuntoDeServicio(int intIdPunto)
        {

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
                    log.Info("Validación al eliminar el punto de servicio: ", ex);
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
                    log.Error("Error al eliminar el punto de servicio: ", ex);
                    throw new Exception("Se produjo un error eliminando al punto de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public PuntoDeServicio ObtenerPuntoDeServicio(int intIdPunto)
        {

            {
                try
                {
                    PuntoDeServicio puntoDeServicio = dbContext.PuntoDeServicioRepository.Where(x => x.IdPunto == intIdPunto).FirstOrDefault();
                    return puntoDeServicio;
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la entidad PuntoDeServicio: ", ex);
                    throw new Exception("Se produjo un error consultando el punto  de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<LlaveDescripcion> ObtenerListadoPuntoDeServicio(int intIdEmpresa, int intIdSucursal, bool bolSoloActivo, string strDescripcion)
        {

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
                    log.Error("Error al obtener el listado de puntos de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de puntos de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        private string GenerarRegistroAutenticacion(int intRole)
        {

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
                string strTokenEncriptado = Encriptador.EncriptarDatos(strGuid);
                return strTokenEncriptado;
            }
        }

        public void ValidarRegistroAutenticacion(string strToken, int intRole)
        {
            try
            {

                {
                    string strTokenDesencriptado = Encriptador.DesencriptarDatos(strToken);
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

                {
                    DateTime detFechaMaxima = DateTime.UtcNow.AddHours(-12);
                    dbContext.RegistroAutenticacionRepository.RemoveRange(dbContext.RegistroAutenticacionRepository.Where(x => x.Fecha < detFechaMaxima));
                    dbContext.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al validar el registro de autenticación: ", ex);
            }
        }

        public decimal ObtenerTipoCambioVenta(string strServicioURL, string strSoapOperation, DateTime fechaConsulta)
        {
            try
            {
                TipoDeCambioDolar tipoDeCambio = null;
                string criteria = fechaConsulta.ToString("dd/MM/yyyy");
                tipoDeCambio = dbContext.TipoDeCambioDolarRepository.Find(criteria);
                if (tipoDeCambio == null)
                {
                    decimal decTipoDeCambio = ComprobanteElectronicoService.ObtenerTipoCambioVenta(strServicioURL, strSoapOperation, fechaConsulta);
                    tipoDeCambio = new TipoDeCambioDolar
                    {
                        FechaTipoCambio = fechaConsulta.ToString("dd/MM/yyyy"),
                        ValorTipoCambio = decTipoDeCambio
                    };
                    dbContext.TipoDeCambioDolarRepository.Add(tipoDeCambio);
                    dbContext.Commit();
                    return decTipoDeCambio;
                }
                else
                {
                    return tipoDeCambio.ValorTipoCambio;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el tipo de cambio de venta: ", ex);
                throw ex;
            }
        }
    }
}