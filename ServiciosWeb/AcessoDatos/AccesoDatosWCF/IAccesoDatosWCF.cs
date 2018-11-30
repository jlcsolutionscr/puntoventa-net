using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.AccesoDatos.ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAccesoDatosWCF
    {

        [OperationContract]
        [WebGet(UriTemplate = "consultarlistadoempresas", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<EmpresaDTO> ConsultarListadoEmpresas();
        [OperationContract]
        [WebGet(UriTemplate = "consultarempresa?empresa={empresa}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        EmpresaDTO ConsultarEmpresa(string empresa);
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "registrarempresa")]
        string RegistrarEmpresa(EmpresaDTO empresa);
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "registrardocumentoelectronico")]
        void RegistrarDocumentoElectronico(DatosDocumentoElectronicoDTO datos);
    }
}
