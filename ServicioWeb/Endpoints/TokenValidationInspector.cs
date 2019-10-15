using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.ServicioWeb.Servicios;
using System;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using Unity;

namespace LeandroSoftware.ServicioWeb
{
    public class TokenValidationInspector : IDispatchMessageInspector
    {
        IUnityContainer unityContainer;
        IMantenimientoService servicioMantenimiento;
        string strApplicationKey;
        public TokenValidationInspector(IUnityContainer container, string key)
        {
            unityContainer = container;
            strApplicationKey = key;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            try
            {
                string strOperacion = request.Properties["HttpOperationName"].ToString();
                if (!new string[] { "ObtenerUltimaVersionApp", "DescargarActualizacion", "ProcesarDocumentosElectronicosPendientes", "ValidarCredenciales", "ObtenerListadoEmpresasAdministrador", "ObtenerListadoEmpresasPorTerminal", "ObtenerListadoTerminalesDisponibles", "RegistrarTerminal" }.Contains(strOperacion))
                {
                    IncomingWebRequestContext incomingRequest = WebOperationContext.Current.IncomingRequest;
                    WebHeaderCollection headers = incomingRequest.Headers;
                    string strToken = headers["Authorization"];
                    if (strToken == null) throw new Exception("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
                    strToken = strToken.Substring(7);
                    servicioMantenimiento.ValidarRegistroAutenticacion(strToken, StaticRolePorUsuario.USUARIO_SISTEMA, strApplicationKey);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}