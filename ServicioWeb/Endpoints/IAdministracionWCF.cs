using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IAdministracionWCF
    {
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void Options();

        [OperationContract]
        [WebGet(UriTemplate = "validarcredencialesadmin?usuario={usuario}&clave={clave}", ResponseFormat = WebMessageFormat.Json)]
        string ValidarCredencialesAdmin(string usuario, string clave);

        [OperationContract]
        [WebGet(UriTemplate = "validarcredenciales?usuario={usuario}&clave={clave}&id={id}", ResponseFormat = WebMessageFormat.Json)]
        string ValidarCredenciales(string usuario, string clave, string id);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoempresa", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoEmpresa();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadosucursales?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoSucursales(int idempresa);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoterminales?idempresa={idempresa}&idsucursal={idsucursal}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoTerminales(int idempresa, int idsucursal);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerEmpresa(int idempresa);

        [OperationContract]
        [WebGet(UriTemplate = "obtenersucursalporempresa?idempresa={idempresa}&idsucursal={idsucursal}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerSucursalPorEmpresa(int idempresa, int idsucursal);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerterminalporsucursal?idempresa={idempresa}&idsucursal={idsucursal}&idterminal={idterminal}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerTerminalPorSucursal(int idempresa, int idsucursal, int idterminal);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadotipoidentificacion", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoTipoIdentificacion();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadocatalogoreportes", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoCatalogoReportes();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoprovincias", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoProvincias();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadocantones?idprovincia={idprovincia}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoCantones(int idprovincia);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadodistritos?idprovincia={idprovincia}&idcanton={idcanton}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoDistritos(int idprovincia, int idcanton);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadobarrios?idprovincia={idprovincia}&idcanton={idcanton}&iddistrito={iddistrito}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoBarrios(int idprovincia, int idcanton, int iddistrito);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "agregarempresa")]
        string AgregarEmpresa(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarempresa")]
        void ActualizarEmpresa(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarlogoempresa")]
        void ActualizarLogoEmpresa(string strDatos);

        [OperationContract]
        [WebGet(UriTemplate = "removerlogoempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        void RemoverLogoEmpresa(int idempresa);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarcertificadoempresa")]
        void ActualizarCertificadoEmpresa(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "agregarsucursalporempresa")]
        void AgregarSucursalPorEmpresa(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarsucursalporempresa")]
        void ActualizarSucursalPorEmpresa(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "agregarterminalporsucursal")]
        void AgregarTerminalPorSucursal(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarterminalporsucursal")]
        void ActualizarTerminalPorSucursal(string strDatos);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadodocumentospendientes", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoDocumentosElectronicosPendientes();

        [OperationContract]
        [WebGet(UriTemplate = "procesarpendientes")]
        void ProcesarDocumentosElectronicosPendientes();

        [OperationContract]
        [WebGet(UriTemplate = "limpiarregistrosinvalidos")]
        void LimpiarRegistrosInvalidos();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerultimaversionapp", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerUltimaVersionApp();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "actualizararchivoaplicacion")]
        void ActualizarArchivoAplicacion(Stream fileStream);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarversionapp")]
        void ActualizarVersionApp(string strDatos);
    }
}
