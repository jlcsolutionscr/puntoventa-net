using LeandroSoftware.ServicioWeb.Contexto;
using LeandroSoftware.ServicioWeb.Servicios;
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web.Configuration;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class TokenValidationBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            throw new Exception("Behavior not supported on the consumer side!");
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            string strApplicationKey = WebConfigurationManager.AppSettings["ApplicationKey"].ToString();
            IUnityContainer unityContainer = new UnityContainer();
            string connString = WebConfigurationManager.ConnectionStrings["LeandroContext"].ConnectionString;
            unityContainer.RegisterType<LeandroContext>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterInstance("conectionString", connString, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDbContext, LeandroContext>(new InjectionConstructor(new ResolvedParameter<string>("conectionString")));
            unityContainer.RegisterType<IMantenimientoService, MantenimientoService>();
            var requiredHeaders = new Dictionary<string, string>();
            requiredHeaders.Add("Access-Control-Allow-Origin", "*");
            requiredHeaders.Add("Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS");
            requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type");
            TokenValidationInspector inspector = new TokenValidationInspector(requiredHeaders, unityContainer, strApplicationKey);
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}