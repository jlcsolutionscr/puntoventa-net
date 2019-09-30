using LeandroSoftware.FacturaElectronicaHacienda.TiposDatos;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace LeandroSoftware.FacturaElectronicaHacienda.ServicioWCF
{
    [ServiceContract]
    public interface IFacturaElectronicaWCF
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
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "enviardocumentoelectronico")]
        void EnviarDocumentoElectronico(DatosDocumentoElectronicoDTO datos);
        [OperationContract]
        [WebGet(UriTemplate = "consultardocumentoelectronico?empresa={empresa}&clave={clave}&consecutivo={consecutivo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DatosDocumentoElectronicoDTO ConsultarDocumentoElectronico(string empresa, string clave, string consecutivo);
        [OperationContract]
        [WebGet(UriTemplate = "consultarlistadodocumentos?empresa={empresa}&estado={estado}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<DatosDocumentoElectronicoDTO> ConsultarListadoDocumentosElectronicos(string empresa, string estado);
        [OperationContract]
        [WebGet(UriTemplate = "enviarnotificacion?empresa={empresa}&clave={clave}&consecutivo={consecutivo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void EnviarNotificacion(string empresa, string clave, string consecutivo);
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "recibirrespuestahacienda")]
        void RecibirRespuestaHacienda(RespuestaHaciendaDTO mensaje);
        [WebGet(UriTemplate = "consultarpersonaporidentificacion?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PadronDTO ConsultarPersonaPorIdentificacion(string id);
    }
}
