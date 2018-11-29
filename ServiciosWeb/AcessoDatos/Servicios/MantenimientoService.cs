using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using LeandroSoftware.Core;
using LeandroSoftware.Core.CommonTypes;
using LeandroSoftware.AccesoDatos.Dominio;
using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.Datos;
using log4net;
using Unity;

namespace LeandroSoftware.PuntoVenta.Servicios
{
    public interface IMantenimientoService
    {
        // Métodos para administrar las empresas
        Empresa AgregarEmpresa(Empresa empresa);
        void ActualizarEmpresa(Empresa empresa);
        Empresa ObtenerEmpresa(int intIdEmpresa);
        IEnumerable<Empresa> ObtenerListaEmpresas();
        Modulo ObtenerModulo(int intIdModulo);
        CatalogoReporte ObtenerCatalogoReporte(int intIdReporte);
        // Métodos para administrar los usuarios del sistema
        Usuario AgregarUsuario(Usuario usuario, string key);
        void ActualizarUsuario(Usuario usuario, string key);
        Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave, string key);
        Usuario ValidarUsuario(int intIdEmpresa, string strCodigoUsuario, string strClave, string key);
        void EliminarUsuario(int intIdUsuario);
        Usuario ObtenerUsuario(int intIdUsuario, string key);
        IEnumerable<Usuario> ObtenerListaUsuarios(int intIdEmpresa, string strCodigo = "");
        // Métodos para administrar los vendedores del sistema
        Vendedor AgregarVendedor(Vendedor vendedor);
        void ActualizarVendedor(Vendedor vendedor);
        void EliminarVendedor(int intIdVendedor);
        Vendedor ObtenerVendedor(int intIdVendedor);
        IEnumerable<Vendedor> ObtenerListaVendedores(int intIdEmpresa, string strNombre = "");
        // Métodos para administrar los roles del sistema
        Role ObtenerRole(int intIdRole);
        IEnumerable<Role> ObtenerListaRoles();
        // Métodos para administrar las líneas de producto
        Linea AgregarLinea(Linea linea);
        void ActualizarLinea(Linea linea);
        void EliminarLinea(int intIdLinea);
        Linea ObtenerLinea(int intIdLinea);
        IEnumerable<Linea> ObtenerListaLineas(int intIdEmpresa, string strDescripcion = "");
        IEnumerable<Linea> ObtenerListaLineasDeProducto(int intIdEmpresa);
        IEnumerable<Linea> ObtenerListaLineasDeServicio(int intIdEmpresa);
        // Métodos para administrar los datos de particulares vinculados al sistema
        Particular AgregarParticular(Particular particular);
        void ActualizarParticular(Particular particular);
        void EliminarParticular(int intIdParticular);
        Particular ObtenerParticular(int intIdParticular);
        Particular ValidaIdentificacionParticular(string strIdentificacion);
        int ObtenerTotalListaParticulares(int intIdEmpresa, string strNombre = "");
        IEnumerable<Particular> ObtenerListaParticulares(int intIdEmpresa, int numPagina, int cantRec, string strNombre = "");
        // Métodos para administrar los productos
        IEnumerable<TipoProducto> ObtenerTiposProducto();
        IEnumerable<TipoUnidad> ObtenerTiposUnidad();
        Producto AgregarProducto(Producto producto);
        void ActualizarProducto(Producto producto);
        void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "", decimal decPorcentajeAumento = 0);
        void EliminarProducto(int intIdProducto);
        Producto ObtenerProducto(int intIdProducto);
        Producto ObtenerProductoPorCodigo(string strCodigo);
        int ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "");
        IEnumerable<Producto> ObtenerListaProductos(int intIdEmpresa, int numPagina, int cantRec, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "");
        int ObtenerTotalMovimientosPorProducto(int intIdProducto, DateTime datFechaInicial, DateTime datFechaFinal);
        IEnumerable<MovimientoProducto> ObtenerMovimientosPorProducto(int intIdProducto, int numPagina, int cantRec, DateTime datFechaInicial, DateTime datFechaFinal);
        // Métodos para obtener las condiciones de venta para facturación
        IEnumerable<CondicionVenta> ObtenerListaCondicionVenta();
        // Métodos para obtener las formas de pago por tipo de servicio
        IEnumerable<FormaPago> ObtenerListaFormaPagoFactura();
        IEnumerable<FormaPago> ObtenerListaFormaPagoCompra();
        IEnumerable<FormaPago> ObtenerListaFormaPagoEgreso();
        IEnumerable<FormaPago> ObtenerListaFormaPagoIngreso();
        IEnumerable<FormaPago> ObtenerListaFormaPagoMovimientoCxC();
        IEnumerable<FormaPago> ObtenerListaFormaPagoMovimientoCxP();
        // Métodos para administrar los parámetros de banco adquiriente
        BancoAdquiriente AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente);
        void EliminarBancoAdquiriente(int intIdBanco);
        BancoAdquiriente ObtenerBancoAdquiriente(int intIdBanco);
        IEnumerable<BancoAdquiriente> ObtenerListaBancoAdquiriente(int intIdEmpresa, string strDescripcion = "");
        // Métodos para administrar los parámetros de tipo de moneda
        TipoMoneda AgregarTipoMoneda(TipoMoneda tipoMoneda);
        void ActualizarTipoMoneda(TipoMoneda tipoMoneda);
        void EliminarTipoMoneda(int intIdTipoMoneda);
        TipoMoneda ObtenerTipoMoneda(int intIdTipoMoneda);
        IEnumerable<TipoMoneda> ObtenerListaTipoMoneda(string strDescripcion = "");
        AjusteInventario AgregarAjusteInventario(AjusteInventario ajusteInventario);
        void ActualizarAjusteInventario(AjusteInventario ajusteInventario);
        void AnularAjusteInventario(int intIdAjusteInventario, int intIdUsuario);
        AjusteInventario ObtenerAjusteInventario(int intIdAjusteInventario);
        int ObtenerTotalListaAjustes(int intIdEmpresa, int intIdAjusteInventario = 0, string strDescripcion = "");
        IEnumerable<AjusteInventario> ObtenerListaAjustes(int intIdEmpresa, int numPagina, int cantRec, int intIdAjusteInventario = 0, string strDescripcion = "");
        IEnumerable<TipoIdentificacion> ObtenerListaTipoIdentificacion();
        IEnumerable<Provincia> ObtenerListaProvincias();
        IEnumerable<Canton> ObtenerListaCantones(int intIdProvincia);
        IEnumerable<Distrito> ObtenerListaDistritos(int intIdProvincia, int intIdCanton);
        IEnumerable<Barrio> ObtenerListaBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito);
        IList<TipodePrecio> ObtenerListaTipodePrecio();
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

        public Empresa AgregarEmpresa(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    dbContext.EmpresaRepository.Add(empresa);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al agregar la empresa: ", ex);
                    throw new Exception("Se produjo un error agregando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
            return empresa;
        }

        public void ActualizarEmpresa(Empresa empresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
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
                    return dbContext.EmpresaRepository.Include("DetalleRegistro").Include("ModuloPorEmpresa").Include("ReportePorEmpresa").Include("Barrio.Distrito.Canton.Provincia").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la empresa: ", ex);
                    throw new Exception("Se produjo un error consultando la información de la empresa. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Empresa> ObtenerListaEmpresas()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.EmpresaRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de empresas: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de empresas. Por favor consulte con su proveedor.");
                }
            }
        }

        public Modulo ObtenerModulo(int intIdModulo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ModuloRepository.FirstOrDefault(x => x.IdModulo == intIdModulo);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener la información del módulo: ", ex);
                    throw new Exception("Se produjo un error consultando la parametrización de la empresa. Por favor consulte con su proveedor.");
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

        public Usuario AgregarUsuario(Usuario usuario, string key)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    Usuario usuarioExistente = dbContext.UsuarioRepository.Where(x => x.CodigoUsuario.ToUpper().Contains(usuario.CodigoUsuario.ToUpper())).FirstOrDefault();
                    if (usuarioExistente != null)
                        throw new BusinessException("El código de usuario que desea agregar ya existe.");
                    usuario.Clave = Utilitario.EncriptarDatos(key, usuario.ClaveSinEncriptar);
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
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario, string key)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    usuario.Clave = Utilitario.EncriptarDatos(key, usuario.ClaveSinEncriptar);
                    Usuario usuarioPersistente = dbContext.UsuarioRepository.Include("RolePorUsuario").FirstOrDefault(x => x.IdUsuario == usuario.IdUsuario);
                    dbContext.ApplyCurrentValues(usuarioPersistente, usuario);
                    dbContext.RolePorUsuarioRepository.RemoveRange(usuarioPersistente.RolePorUsuario);
                    dbContext.RolePorUsuarioRepository.AddRange(usuario.RolePorUsuario);
                    dbContext.Commit();
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al actualizar el usuario: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public Usuario ActualizarClaveUsuario(int intIdUsuario, string strClave, string key)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Find(intIdUsuario);
                    if (usuario == null)
                        throw new Exception("El usuario seleccionado para actualizar clave no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    usuario.Clave = Utilitario.EncriptarDatos(key, strClave);
                    usuario.ClaveSinEncriptar = strClave;
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

        public Usuario ValidarUsuario(int intIdEmpresa, string strCodigoUsuario, string strClave, string key)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdEmpresa == intIdEmpresa && x.CodigoUsuario == strCodigoUsuario);
                    if (usuario == null)
                        throw new BusinessException("El código de usuario ingresado no se encuentra registrado. Contacte a su proveedor.");
                    if (Utilitario.DesencriptarDatos(key, usuario.Clave) != strClave)
                        throw new BusinessException("Contraseña incorrecta. Verifique los credenciales suministrados.");
                    return usuario;
                }
                catch (BusinessException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    log.Error("Error al validar los credenciales del usuario: ", ex);
                    throw new Exception("Se produjo un error verificando los credenciales del usuario. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarUsuario(int intIdUsuario)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Find(intIdUsuario);
                    if (usuario == null)
                        throw new BusinessException("El usuario por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(usuario.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.RolePorUsuarioRepository.RemoveRange(usuario.RolePorUsuario);
                    dbContext.UsuarioRepository.Remove(usuario);
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

        public Usuario ObtenerUsuario(int intIdUsuario, string key)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Usuario usuario = dbContext.UsuarioRepository.Include("RolePorUsuario.Role").FirstOrDefault(x => x.IdUsuario == intIdUsuario);
                    if (usuario == null)
                        throw new BusinessException("El usuario por consultar no existe");
                    else
                        usuario.ClaveSinEncriptar = Utilitario.DesencriptarDatos(key, usuario.Clave);
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

        public IEnumerable<Usuario> ObtenerListaUsuarios(int intIdEmpresa, string strCodigo = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaUsuarios = dbContext.UsuarioRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdUsuario > 1);
                    if (!strCodigo.Equals(string.Empty))
                        listaUsuarios = listaUsuarios.Where(x => x.CodigoUsuario.Contains(strCodigo));
                    return listaUsuarios.OrderBy(x => x.IdUsuario).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de usuarios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de usuarios. Por favor consulte con su proveedor.");
                }
            }
        }

        public Vendedor AgregarVendedor(Vendedor vendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
            return vendedor;
        }

        public void ActualizarVendedor(Vendedor vendedor)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(vendedor.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public IEnumerable<Vendedor> ObtenerListaVendedores(int intIdEmpresa, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaVendedores = dbContext.VendedorRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listaVendedores = listaVendedores.Where(x => x.Nombre.Contains(strNombre));
                    return listaVendedores.OrderBy(x => x.IdVendedor).ToList();
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

        public IEnumerable<Role> ObtenerListaRoles()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.RoleRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de roles de usuario: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de roles de acceso. Por favor consulte con su proveedor.");
                }
            }
        }

        public Linea AgregarLinea(Linea linea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
            return linea;
        }

        public void ActualizarLinea(Linea linea)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(linea.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public IEnumerable<Linea> ObtenerListaLineas(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaLineas = dbContext.LineaRepository.Include("TipoProducto").Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strDescripcion.Equals(string.Empty))
                        listaLineas = listaLineas.Where(x => x.Descripcion.Contains(strDescripcion));
                    return listaLineas.OrderBy(x => x.IdTipoProducto).ThenBy(x => x.Descripcion).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de líneas general: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de líneas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Linea> ObtenerListaLineasDeProducto(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.LineaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdTipoProducto == StaticTipoProducto.Producto).OrderBy(x => x.Descripcion).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de líneas de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de líneas de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Linea> ObtenerListaLineasDeServicio(int intIdEmpresa)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.LineaRepository.Where(x => x.IdEmpresa == intIdEmpresa && x.IdTipoProducto == StaticTipoProducto.Servicio).OrderBy(x => x.Descripcion).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de líneas de servicio: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de líneas de servicio. Por favor consulte con su proveedor.");
                }
            }
        }

        public Particular AgregarParticular(Particular particular)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(particular.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ParticularRepository.Add(particular);
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
                    log.Error("Error al agregar el particular: ", ex);
                    throw new Exception("Se produjo un error agregando al particular. Por favor consulte con su proveedor.");
                }
            }
            return particular;
        }

        public void ActualizarParticular(Particular particular)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(particular.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.NotificarModificacion(particular);
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
                    log.Error("Error al actualizar el particular: ", ex);
                    throw new Exception("Se produjo un error actualizando la información del particular. Por favor consulte con su proveedor.");
                }
            }
        }

        public void EliminarParticular(int intIdParticular)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Particular particular = dbContext.ParticularRepository.Find(intIdParticular);
                    if (particular == null)
                        throw new BusinessException("El particular por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(particular.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    dbContext.ParticularRepository.Remove(particular);
                    dbContext.Commit();
                }
                catch (DbUpdateException ex)
                {
                    log.Info("Validación al agregar el parámetro contable: ", ex);
                    throw new BusinessException("No es posible eliminar el particular seleccionado. Posee registros relacionados en el sistema.");
                }
                catch (BusinessException ex)
                {
                    dbContext.RollBack();
                    throw ex;
                }
                catch (Exception ex)
                {
                    dbContext.RollBack();
                    log.Error("Error al eliminar el particular: ", ex);
                    throw new Exception("Se produjo un error eliminando al particular. Por favor consulte con su proveedor.");
                }
            }
        }

        public Particular ObtenerParticular(int intIdParticular)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ParticularRepository.Find(intIdParticular);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el particular: ", ex);
                    throw new Exception("Se produjo un error consultando la información del particular. Por favor consulte con su proveedor.");
                }
            }
        }

        public Particular ValidaIdentificacionParticular(string strIdentificacion)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ParticularRepository.Where(x => x.Identificacion == strIdentificacion).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el particular: ", ex);
                    throw new Exception("Se produjo un error consultando la información del particular. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaParticulares(int intIdEmpresa, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaParticulares = dbContext.ParticularRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listaParticulares = listaParticulares.Where(x => x.Nombre.Contains(strNombre));
                    return listaParticulares.Count();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el total del listado de particulars: ", ex);
                    throw new Exception("Se produjo un error consultando el total del listado de particulars. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Particular> ObtenerListaParticulares(int intIdEmpresa, int numPagina, int cantRec, string strNombre = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaParticulares = dbContext.ParticularRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (!strNombre.Equals(string.Empty))
                        listaParticulares = listaParticulares.Where(x => x.Nombre.Contains(strNombre));
                    if (cantRec == 0)
                        return listaParticulares.OrderBy(x => x.Nombre).ToList();
                    else
                        return listaParticulares.OrderBy(x => x.IdParticular).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de particulares: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de particulares. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoProducto> ObtenerTiposProducto()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoProductoRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de producto: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoUnidad> ObtenerTiposUnidad()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoUnidadRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el tipo de unidad: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de unidad de producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto AgregarProducto(Producto producto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    bool existe = dbContext.ProductoRepository.Where(x => x.Codigo == producto.Codigo).Count() > 0;
                    if (existe)
                        throw new BusinessException("La Código de producto ingresado ya está registrado.");
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
            return producto;
        }

        public void ActualizarProducto(Producto producto)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
                    bool existe = dbContext.ProductoRepository.Where(x => x.Codigo == producto.Codigo && x.IdProducto != producto.IdProducto).Count() > 0;
                    if (existe)
                        throw new BusinessException("La Código de producto ingresado ya está registrado. Debe ingresar otro código para este producto.");
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

        public void ActualizarPrecioVentaProductos(int intIdEmpresa, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "", decimal decPorcentajeAumento = 0)
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
                    if (producto == null)
                        throw new BusinessException("El producto por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(producto.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    return dbContext.ProductoRepository.Include("Proveedor").FirstOrDefault(x => x.IdProducto == intIdProducto);
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public Producto ObtenerProductoPorCodigo(string strCodigo)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProductoRepository.Where(x => x.Codigo.Equals(strCodigo)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el producto: ", ex);
                    throw new Exception("Se produjo un error consultando la información del producto. Por favor consulte con su proveedor.");
                }
            }
        }

        public int ObtenerTotalListaProductos(int intIdEmpresa, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
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

        public IEnumerable<Producto> ObtenerListaProductos(int intIdEmpresa, int numPagina, int cantRec, bool bolIncluyeServicios, int intIdLinea = 0, string strCodigo = "", string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaProductos = dbContext.ProductoRepository.Include("TipoProducto").Where(x => x.IdEmpresa == intIdEmpresa);
                    if (intIdLinea > 0)
                        listaProductos = listaProductos.Where(x => x.IdLinea == intIdLinea);
                    if (!strCodigo.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Codigo.Contains(strCodigo));
                    if (!strDescripcion.Equals(string.Empty))
                        listaProductos = listaProductos.Where(x => x.Descripcion.Contains(strDescripcion));
                    if (!bolIncluyeServicios)
                        listaProductos = listaProductos.Where(x => x.Tipo == StaticTipoProducto.Producto);
                    if (cantRec > 0)
                        return listaProductos.OrderBy(x => x.Tipo).ThenBy(x => x.Descripcion).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                    else
                        return listaProductos.OrderBy(x => x.Tipo).ThenBy(x => x.Descripcion).ToList();
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

        public IEnumerable<CondicionVenta> ObtenerListaCondicionVenta()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CondicionVentaRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de condiciones de venta para facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de condiciones de venta. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoFactura()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoCompra()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para compras: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoEgreso()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para egresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoIngreso()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para egresos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoMovimientoCxC()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque, StaticFormaPago.Tarjeta }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para movimientos de cuentas por cobrar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<FormaPago> ObtenerListaFormaPagoMovimientoCxP()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.FormaPagoRepository.Where(x => new[] { StaticFormaPago.Efectivo, StaticFormaPago.TransferenciaDepositoBancario, StaticFormaPago.Cheque }.Contains(x.IdFormaPago)).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de formas de pago para movimientos de cuentas por pagar: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de formas de pago. Por favor consulte con su proveedor.");
                }
            }
        }

        public BancoAdquiriente AgregarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
            return bancoAdquiriente;
        }

        public void ActualizarBancoAdquiriente(BancoAdquiriente bancoAdquiriente)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    Empresa empresa = dbContext.EmpresaRepository.Find(bancoAdquiriente.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (banco == null)
                        throw new BusinessException("El banco adquiriente por eliminar no existe.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(banco.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public IEnumerable<BancoAdquiriente> ObtenerListaBancoAdquiriente(int intIdEmpresa, string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    var listaBancos = dbContext.BancoAdquirienteRepository.Where(x => x.IdEmpresa == intIdEmpresa);
                    if (strDescripcion != "")
                        listaBancos = listaBancos.Where(x => x.Descripcion.Contains(strDescripcion));
                    return listaBancos.ToList();
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

        public IEnumerable<TipoMoneda> ObtenerListaTipoMoneda(string strDescripcion = "")
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    if (strDescripcion == "")
                        return dbContext.TipoMonedaRepository.ToList();
                    else
                        return dbContext.TipoMonedaRepository.Where(x => x.IdTipoMoneda > StaticValoresPorDefecto.MonedaDelSistema && x.Descripcion.Contains(strDescripcion)).ToList();
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
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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
                    if (ajusteInventario == null)
                        throw new Exception("El registro de ajuste de inventario por anular no existe.");
                    if (ajusteInventario.Nulo == true)
                        throw new BusinessException("El registro de ajuste de inventario ya ha sido anulado.");
                    Empresa empresa = dbContext.EmpresaRepository.Find(ajusteInventario.IdEmpresa);
                    if (empresa == null)
                        throw new Exception("La empresa asignada a la transacción no existe.");
                    if (empresa.CierreEnEjecucion)
                        throw new BusinessException("Se está ejecutando el cierre en este momento. No es posible registrar la transacción.");
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

        public int ObtenerTotalListaAjustes(int intIdEmpresa, int intIdAjusteInventario = 0, string strDescripcion = "")
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

        public IEnumerable<AjusteInventario> ObtenerListaAjustes(int intIdEmpresa, int numPagina, int cantRec, int intIdAjusteInventario = 0, string strDescripcion = "")
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
                    return listaAjusteInventario.OrderByDescending(x => x.IdAjuste).Skip((numPagina - 1) * cantRec).Take(cantRec).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de registros de facturación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de facturas. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<TipoIdentificacion> ObtenerListaTipoIdentificacion()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.TipoIdentificacionRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de tipos de identificación: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de tipos de identificación. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Provincia> ObtenerListaProvincias()
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.ProvinciaRepository.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de provincias: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de provincias. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Canton> ObtenerListaCantones(int intIdProvincia)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.CantonRepository.Where(x => x.IdProvincia == intIdProvincia).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de cantones: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de cantones. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Distrito> ObtenerListaDistritos(int intIdProvincia, int intIdCanton)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.DistritoRepository.Where(x => x.IdProvincia == intIdProvincia & x.IdCanton == intIdCanton).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de distritos: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de distritos. Por favor consulte con su proveedor.");
                }
            }
        }

        public IEnumerable<Barrio> ObtenerListaBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            using (IDbContext dbContext = localContainer.Resolve<IDbContext>())
            {
                try
                {
                    return dbContext.BarrioRepository.Where(x => x.IdProvincia == intIdProvincia & x.IdCanton == intIdCanton & x.IdDistrito == intIdDistrito).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Error al obtener el listado de barrios: ", ex);
                    throw new Exception("Se produjo un error consultando el listado de barrios. Por favor consulte con su proveedor.");
                }
            }
        }

        public IList<TipodePrecio> ObtenerListaTipodePrecio()
        {
            IList<TipodePrecio> listado = null;
            try
            {
                listado = new List<TipodePrecio>();
                var tipoPrecio = new TipodePrecio(1, "Precio 1");
                listado.Add(tipoPrecio);
                tipoPrecio = new TipodePrecio(2, "Precio 2");
                listado.Add(tipoPrecio);
                tipoPrecio = new TipodePrecio(3, "Precio 3");
                listado.Add(tipoPrecio);
                tipoPrecio = new TipodePrecio(4, "Precio 4");
                listado.Add(tipoPrecio);
                tipoPrecio = new TipodePrecio(5, "Precio 5");
                listado.Add(tipoPrecio);
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener el listado de tipos de precio: ", ex);
                throw new Exception("Se produjo un error consultando el listado de tipos de precio. Por favor consulte con su proveedor.");
            }
            return listado;
        }
    }
}