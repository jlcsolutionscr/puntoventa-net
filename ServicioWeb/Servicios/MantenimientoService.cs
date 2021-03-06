﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Utilitario;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ServicioWeb.Contexto;
using log4net;
using Unity;
using System.Globalization;

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
        Usuario ValidarCredenciales(string strUsuario, string strClave, string id);
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
        List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa);
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
        void AgregarUsuarioPorEmpresa(int intIdUsuario, int intIdEmpresa);
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
        IList<LlaveDescripcion> ObtenerListadoTipoProducto();
        IList<LlaveDescripcion> ObtenerListadoTipoExoneracion();
        IList<LlaveDescripcion> ObtenerListadoTipoImpuesto();
        ParametroImpuesto ObtenerParametroImpuesto(int intIdImpuesto);
        void AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea, string strCodigo, string strDescripcion, decimal decPorcentajeAumento);
        void EliminarProducto(int intIdProducto);
        Producto ObtenerProducto(int intIdProducto, int intIdSucursal);
        Producto ObtenerProductoTransitorio(int intIdEmpresa);
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
        // Métodos para administrar los parámetros de tipo de moneda
        TipoMoneda AgregarTipoMoneda(TipoMoneda tipoMoneda);
        void ActualizarTipoMoneda(TipoMoneda tipoMoneda);
        void EliminarTipoMoneda(int intIdTipoMoneda);
        TipoMoneda ObtenerTipoMoneda(int intIdTipoMoneda);
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
        void ValidarRegistroAutenticacion(string strToken, int intRole);
        void EliminarRegistroAutenticacionInvalidos();
    }

    public class MantenimientoService : IMantenimientoService
    {
        private static IUnityContainer localContainer;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static CultureInfo provider = CultureInfo.InvariantCulture;
        private static string strFormat = "dd/MM/yyyy HH:mm:ss";
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

        public IList<EquipoRegistrado> ObtenerListadoTerminalesDisponibles(string strUsuario, string strClave, string strIdentificacion, int intTipoDispositivo)
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
                    if (strUsuario.ToUpper() != "ADMIN" && usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "ADMIN")
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
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
        }

        public IList<LlaveDescripcion> ObtenerListadoEmpresasAdministrador()
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

        public IList<LlaveDescripcion> ObtenerListadoSucursales(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    log.Error("Error al validar la lista de empresas por valor de registro: ", ex);
                    throw new Exception("Error al validar la lista de empresas por valor de registro. . .");
                }
            }
        }
        
        public IList<LlaveDescripcion> ObtenerListadoTerminales(int intIdEmpresa, int intIdSucursal)
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

        public IList<LlaveDescripcion> ObtenerListadoEmpresasPorTerminal(string strDispositivoId)
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

        public bool EnModoMantenimiento()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    if (strUsuario.ToUpper() != "ADMIN" && usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    Usuario usuario = null;
                    if (strUsuario.ToUpper() == "ADMIN")
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
                    Usuario usuario = null;
                    UsuarioPorEmpresa usuarioEmpresa = null;
                    if (strUsuario.ToUpper() == "ADMIN")
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa);
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        if (strUsuario.ToUpper() != "CONTADOR")
                        {
                            if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                            if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                            if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
                        }
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == empresa.IdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                        if (usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
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
                    int tipoRole = strUsuario.ToUpper() == "ADMIN" ? StaticRolePorUsuario.ADMINISTRADOR : StaticRolePorUsuario.USUARIO_SISTEMA;
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
                    if (strUsuario.ToUpper() == "ADMIN")
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == 1);
                    }
                    else
                    {
                        usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                        if (usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                        usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
                    }
                    if (usuario.Clave != strClave) throw new BusinessException("Los credenciales suministrados no son válidos. Verifique los credenciales suministrados.");
                    Empresa empresa = dbContext.EmpresaRepository.Include("ReportePorEmpresa.CatalogoReporte").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                    if (!empresa.PermiteFacturar) throw new BusinessException("La empresa que envía la transacción no se encuentra activa en el sistema de facturación electrónica. Por favor, pongase en contacto con su proveedor del servicio.");
                    if (empresa.FechaVence < DateTime.Today) throw new BusinessException("La vigencia del plan de facturación ha expirado. Por favor, pongase en contacto con su proveedor de servicio.");
                    if (empresa.TipoContrato == 2 && empresa.CantidadDisponible == 0) throw new BusinessException("El disponible de documentos electrónicos fue agotado. Por favor, pongase en contacto con su proveedor del servicio.");
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = null;
                    UsuarioPorEmpresa usuarioEmpresa = null;
                    usuarioEmpresa = dbContext.UsuarioPorEmpresaRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Usuario.CodigoUsuario == strUsuario.ToUpper());
                    if (usuarioEmpresa == null) throw new BusinessException("Usuario no registrado en la empresa suministrada. Por favor verifique la información suministrada.");
                    usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == usuarioEmpresa.IdUsuario);
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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

        public Empresa ObtenerEmpresa(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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

        public void ActualizarEmpresa(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    try
                    {
                        Utilitario.ValidaFormatoIdentificacion(empresa.IdTipoIdentificacion, empresa.Identificacion);
                        Utilitario.ValidaFormatoEmail(empresa.CorreoNotificacion);
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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

        public List<LlaveDescripcion> ObtenerListadoRolePorEmpresa(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaRoles = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.RolePorEmpresaRepository.Include("Role").Where(x => x.IdEmpresa == intIdEmpresa && x.IdRole > 2);
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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

        public void EliminarRegistrosPorEmpresa(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
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
                    if (usuario.CodigoUsuario == "ADMIN" || usuario.CodigoUsuario == "CONTADOR") throw new BusinessException("El código de usuario ingresado no se encuentra disponible. Por favor modifique la información suministrada.");
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

        public IList<LlaveDescripcion> ObtenerListadoUsuarios(int intIdEmpresa, string strCodigo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaUsuario = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.UsuarioPorEmpresaRepository.Include("Usuario").Where(x => x.IdEmpresa == intIdEmpresa && x.IdUsuario > 2);
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

        public IList<LlaveDescripcion> ObtenerListadoVendedores(int intIdEmpresa, string strNombre)
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

        public IList<LlaveDescripcion> ObtenerListadoRoles()
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

        public IList<LlaveDescripcion> ObtenerListadoLineas(int intIdEmpresa, string strDescripcion)
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

        public IList<LlaveDescripcion> ObtenerListadoTipoProducto()
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

        public IList<LlaveDescripcion> ObtenerListadoTipoExoneracion()
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

        public IList<LlaveDescripcion> ObtenerListadoTipoImpuesto()
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
                    bool existe = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.Codigo == producto.Codigo).Count() > 0;
                    if (existe) throw new BusinessException("El código de producto ingresado ya está registrado en la empresa.");
                    if (producto.Tipo == 4)
                    {
                        bool transitorio = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.Tipo == 4).Count() > 0;
                        if (transitorio) throw new BusinessException("Ya existe un producto de tipo 'Transitorio' registrado en la empresa.");
                    }
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
                    bool existe = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto != producto.IdProducto && x.Codigo == producto.Codigo).Count() > 0;
                    if (existe) throw new BusinessException("El código del producto ingresado ya está registrado en la empresa.");
                    if (producto.Tipo == 4)
                    {
                        bool transitorio = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto != producto.IdProducto && x.Tipo == 4).Count() > 0;
                        if (transitorio) throw new BusinessException("Ya existe un producto de tipo 'Transitorio' registrado en la empresa.");
                    }
                    if (producto.Imagen == null)
                    {
                        byte[] imagen = dbContext.ProductoRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == producto.IdProducto).FirstOrDefault().Imagen;
                        producto.Imagen = imagen;
                    }
                    producto.ParametroImpuesto = null;
                    producto.Proveedor = null;
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("ParametroImpuesto").Include("Proveedor").FirstOrDefault(x => x.IdProducto == intIdProducto);
                    if (producto != null)
                    {
                        var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == producto.IdEmpresa && x.IdProducto == intIdProducto && x.IdSucursal == intIdSucursal).FirstOrDefault();
                        decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                        producto.Proveedor.Producto = null;
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

        public Producto ObtenerProductoTransitorio(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProductoRepository.FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.Tipo == 4);
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("ParametroImpuesto").Where(x => x.IdEmpresa == intIdEmpresa && x.Codigo.Equals(strCodigo)).FirstOrDefault();
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Producto producto = dbContext.ProductoRepository.Include("ParametroImpuesto").Where(x => x.IdEmpresa == intIdEmpresa && x.CodigoProveedor.Equals(strCodigo)).FirstOrDefault();
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    List<ReporteInventario> listaReporte = new List<ReporteInventario>();
                    var listaProductos = dbContext.ProductoRepository.Where(x => x.IdEmpresa == intIdEmpresa);
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaProducto = new List<ProductoDetalle>();
                try
                {
                    List<ProductoDetalle> listaReporte = new List<ProductoDetalle>();
                    var listaProductos = dbContext.ProductoRepository.Include("ParametroImpuesto").Where(x => x.IdEmpresa == intIdEmpresa);
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
                        var listado = listaProductos.Join(dbContext.ExistenciaPorSucursalRepository, x => x.IdProducto, y => y.IdProducto, (x, y) => new { x, y }).Where(x => x.y.IdEmpresa == intIdEmpresa && x.y.IdSucursal == intIdSucursal && x.y.Cantidad > 0).Join(dbContext.ParametroImpuestoRepository, x => x.x.IdImpuesto, y => y.IdImpuesto, (x, y) => new { x, y }).OrderBy(x => x.x.x.Codigo).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                        foreach (var value in listado)
                        {
                            decimal decUtilidad = value.x.x.PrecioCosto > 0 ? ((value.x.x.PrecioVenta1 / (1 + (value.x.x.ParametroImpuesto.TasaImpuesto / 100))) * 100 / value.x.x.PrecioCosto) - 100 : value.x.x.PrecioVenta1 > 0 ? 100 : 0;
                            ProductoDetalle item = new ProductoDetalle(value.x.x.IdProducto, value.x.x.Codigo, value.x.x.CodigoProveedor, value.x.x.Descripcion, value.x.y.Cantidad, value.x.x.PrecioCosto, value.x.x.PrecioVenta1, value.x.x.Observacion, decUtilidad, value.x.x.Activo);
                            listaProducto.Add(item);
                        }
                    }
                    else
                    {
                        var listado = listaProductos.OrderBy(x => x.Codigo).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                        foreach (var value in listado)
                        {
                            var existencias = dbContext.ExistenciaPorSucursalRepository.AsNoTracking().Where(x => x.IdEmpresa == intIdEmpresa && x.IdSucursal == intIdSucursal && x.IdProducto == value.IdProducto).FirstOrDefault();
                            decimal decCantidad = existencias != null ? existencias.Cantidad : 0;
                            decimal decUtilidad = value.PrecioCosto > 0 ? ((value.PrecioVenta1 / (1 + (value.ParametroImpuesto.TasaImpuesto / 100))) * 100 / value.PrecioCosto) - 100 : value.PrecioVenta1 > 0 ? 100 : 0;
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                var listaCondicionVenta = new List<LlaveDescripcion>();
                try
                {
                    var listado = dbContext.CondicionVentaRepository.Where(x => new[] { 1, 2 }.Contains(x.IdCondicionVenta));
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

        public IList<LlaveDescripcion> ObtenerListadoFormaPagoCliente()
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

        public IList<LlaveDescripcion> ObtenerListadoFormaPagoEmpresa()
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

        public IList<LlaveDescripcion> ObtenerListadoBancoAdquiriente(int intIdEmpresa, string strDescripcion)
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

        public IList<LlaveDescripcion> ObtenerListadoTipoMoneda()
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

        public string AgregarAjusteInventario(AjusteInventario ajusteInventario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    AjusteInventario ajuste = dbContext.AjusteInventarioRepository.Include("DetalleAjusteInventario.Producto.TipoProducto").FirstOrDefault(x => x.IdAjuste == intIdAjusteInventario);
                    foreach (var detalle in ajuste.DetalleAjusteInventario)
                        detalle.AjusteInventario = null;
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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

        public IList<LlaveDescripcion> ObtenerListadoCatalogoReportes()
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

        public IList<LlaveDescripcion> ObtenerListadoProvincias()
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

        public IList<LlaveDescripcion> ObtenerListadoCantones(int intIdProvincia)
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

        public IList<LlaveDescripcion> ObtenerListadoDistritos(int intIdProvincia, int intIdCanton)
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

        public IList<LlaveDescripcion> ObtenerListadoBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
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

        public int ObtenerTotalListaClasificacionProducto(string strDescripcion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
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
                    dbContext.RegistroAutenticacionRepository.RemoveRange(dbContext.RegistroAutenticacionRepository.Where(x => x.Fecha < detFechaMaxima));
                    dbContext.Commit();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error al validar el registro de autenticación: ", ex);
            }
        }
    }
}