using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IAdministracionWCF
    {
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
        [WebGet(UriTemplate = "obtenerlogotipoempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerLogotipoEmpresa(int idempresa);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoreporteporempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoReportePorEmpresa(int idempresa);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoroleporempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoRolePorEmpresa(int idempresa);

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
        [WebGet(UriTemplate = "obtenerlistadoroles", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoRoles();

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
        [WebGet(UriTemplate = "obtenerdatosreporte?tipo={tipo}&idempresa={idempresa}&idsucursal={idsucursal}&fechainicial={fechainicial}&fechafinal={fechafinal}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerDatosReporte(int tipo, int idempresa, int idsucursal, string fechainicial, string fechafinal);

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
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarlistadoreportes")]
        void ActualizarListadoReporte(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarlistadoroles")]
        void ActualizarListadoRoles(string strDatos);

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
        [WebGet(UriTemplate = "eliminarregistrosporempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarRegistrosPorEmpresa(int idempresa);

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
        [WebGet(UriTemplate = "obtenerlistadoparametros", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoParametros();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarparametrosistema")]
        void ActualizarParametroSistema(string strDatos);
    }
}
