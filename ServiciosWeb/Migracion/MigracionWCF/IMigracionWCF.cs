using LeandroSoftware.AccesoDatos.TiposDatos;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.Migracion.ServicioWCF
{
    [ServiceContract]
    public interface IMigracionWCF
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ejecutarconsulta")]
        string EjecutarConsulta(RequestDTO datos);
    }
}
