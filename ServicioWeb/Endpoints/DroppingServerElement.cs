using LeandroSoftware.ServicioWeb.Interceptor;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    class DroppingServerElement : InterceptingElement
    {
        protected override ChannelMessageInterceptor CreateMessageInterceptor()
        {
            return new DroppingServerInterceptor();
        }
    }
}