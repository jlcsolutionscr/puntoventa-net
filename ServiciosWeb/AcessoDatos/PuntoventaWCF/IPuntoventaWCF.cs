using LeandroSoftware.AccesoDatos.TiposDatos;
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
    }
}
