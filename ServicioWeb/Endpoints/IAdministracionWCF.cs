using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IAdministracionWCF
    {
        [OperationContract]
        [WebGet(UriTemplate = "validarcredenciales?usuario={usuario}&clave={clave}", ResponseFormat = WebMessageFormat.Json)]
        string ValidarCredenciales(string usuario, string clave);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoempresa", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoEmpresa();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerempresa?idempresa={idempresa}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerEmpresa(int idempresa);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "actualizarempresa")]
        void ActualizarEmpresa(string strEmpresa);

        [OperationContract]
        [WebGet(UriTemplate = "procesarpendientes")]
        void ProcesarDocumentosPendientes();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerultimaversionapp", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerUltimaVersionApp();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "actualizararchivoaplicacion")]
        void ActualizarArchivoAplicacion(Stream fileStream);

        [OperationContract]
        [WebGet(UriTemplate = "descargaractualizacion")]
        Stream DescargarActualizacion();
    }
}
