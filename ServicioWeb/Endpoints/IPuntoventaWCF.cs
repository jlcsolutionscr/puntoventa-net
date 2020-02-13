using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    [ServiceContract]
    public interface IPuntoventaWCF
    {
        [OperationContract]
        [WebGet(UriTemplate = "obtenerultimaversionapp", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerUltimaVersionApp();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerultimaversionmobileapp", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerUltimaVersionMobileApp();

        [OperationContract]
        [WebGet(UriTemplate = "descargaractualizacion")]
        Stream DescargarActualizacion();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoempresasadmin", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoEmpresasAdministrador();

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoempresas?dispositivo={dispositivo}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoEmpresasPorTerminal(string dispositivo);

        [OperationContract]
        [WebGet(UriTemplate = "validarcredenciales?usuario={usuario}&clave={clave}&idempresa={id}&dispositivo={dispositivo}", ResponseFormat = WebMessageFormat.Json)]
        string ValidarCredenciales(string usuario, string clave, int id, string dispositivo);

        [OperationContract]
        [WebGet(UriTemplate = "obtenerlistadoterminalesdisponibles?usuario={usuario}&clave={clave}&id={id}&tipodispositivo={tipo}", ResponseFormat = WebMessageFormat.Json)]
        string ObtenerListadoTerminalesDisponibles(string usuario, string clave, string id, int tipo);

        [OperationContract]
        [WebGet(UriTemplate = "registrarterminal?usuario={usuario}&clave={clave}&id={id}&sucursal={sucursal}&terminal={terminal}&tipodispositivo={tipo}&dispositivo={dispositivo}", ResponseFormat = WebMessageFormat.Json)]
        void RegistrarTerminal(string usuario, string clave, string id, int sucursal, int terminal, int tipo, string dispositivo);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ejecutar")]
        void Ejecutar(string strDatos);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "ejecutarconsulta")]
        string EjecutarConsulta(string strDatos);
    }
}