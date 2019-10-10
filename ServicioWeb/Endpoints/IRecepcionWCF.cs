using LeandroSoftware.Core.CommonTypes;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IRecepcionWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "recibirrespuestahacienda")]
        void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje);
    }
}
