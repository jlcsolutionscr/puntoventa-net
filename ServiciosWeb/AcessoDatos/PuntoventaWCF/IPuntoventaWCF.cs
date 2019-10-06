using LeandroSoftware.Core.CommonTypes;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
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

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "actualizararchivoaplicacion")]
        void ActualizarArchivoAplicacion(Stream fileStream);

        [OperationContract]
        [WebGet(UriTemplate = "descargaractualizacion")]
        Stream DescargarActualizacion();
    }
}
