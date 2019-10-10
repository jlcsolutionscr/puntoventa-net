using LeandroSoftware.Core.CommonTypes;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IPuntoventaWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ejecutar")]
        void Ejecutar(RequestDTO datos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ejecutarconsulta")]
        ResponseDTO EjecutarConsulta(RequestDTO datos);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "obtenerultimaversionapp")]
        string ObtenerUltimaVersionApp();
    }
}