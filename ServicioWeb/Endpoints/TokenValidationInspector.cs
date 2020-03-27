using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.ServicioWeb.Servicios;
using System;
using System.Collections.Generic;
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
        Dictionary<string, string> requiredHeaders;

        public TokenValidationInspector(Dictionary<string, string> headers, IUnityContainer container)
        {
            unityContainer = container;
            requiredHeaders = headers ?? new Dictionary<string, string>();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            try
            {
                if (!request.Properties["Via"].ToString().Contains("AdministracionWCF.svc"))
                {
                    servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
                    bool modoMantenimiento = servicioMantenimiento.EnModoMantenimiento();
                    if (modoMantenimiento) throw new Exception("El sistema se encuentra en modo mantenimiento y no es posible procesar su solicitud.");
                }
                string strOperacion = request.Properties["HttpOperationName"].ToString();
                if (!new string[] { "", "ObtenerUltimaVersionMobileApp", "ObtenerUltimaVersionApp", "DescargarActualizacion", "LimpiarRegistrosInvalidos", "ProcesarDocumentosElectronicosPendientes", "ValidarCredencialesAdmin", "ValidarCredenciales", "ObtenerListadoEmpresasAdministrador", "ObtenerListadoEmpresasPorTerminal", "ObtenerListadoTerminalesDisponibles", "RegistrarTerminal" }.Contains(strOperacion))
                {
                    IncomingWebRequestContext incomingRequest = WebOperationContext.Current.IncomingRequest;
                    WebHeaderCollection headers = incomingRequest.Headers;
                    string strToken = headers["Authorization"];
                    if (strToken == null) throw new Exception("La sessión del usuario no es válida. Debe reiniciar su sesión.");
                    strToken = strToken.Substring(7);
                    servicioMantenimiento = unityContainer.Resolve<IMantenimientoService>();
                    servicioMantenimiento.ValidarRegistroAutenticacion(strToken, StaticRolePorUsuario.USUARIO_SISTEMA);
                }
                var httpRequest = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
                return new
                {
                    origin = httpRequest.Headers["Origin"],
                    handlePreflight = httpRequest.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase)
                };
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            try
            {
                var state = (dynamic)correlationState;
                if (state.handlePreflight)
                {
                    reply = Message.CreateMessage(MessageVersion.None, "PreflightReturn");

                    var httpResponse = new HttpResponseMessageProperty();
                    reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);

                    httpResponse.SuppressEntityBody = true;
                    httpResponse.StatusCode = HttpStatusCode.OK;
                }
                var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
                foreach (var item in requiredHeaders)
                {
                    httpHeader.Headers.Add(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}