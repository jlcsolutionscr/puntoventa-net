using LeandroSoftware.ServicioWeb.Interceptor;
using System;
using System.ServiceModel.Channels;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class DroppingServerInterceptor : ChannelMessageInterceptor
    {
        int messagesSinceLastReport = 0;
        readonly int reportPeriod = 5;

        public DroppingServerInterceptor() { }

        public override void OnReceive(ref Message msg)
        {
            if (msg.Headers.FindHeader("ByPass", "urn:InterceptorNamespace") > 0)
            {
                if (++messagesSinceLastReport == this.reportPeriod)
                {
                    Console.WriteLine(reportPeriod + " wind speed reports have been received.");
                }
                return;
            }
            // Drop incoming Message if the Message does not have the special header
            msg = null;
        }

        public override ChannelMessageInterceptor Clone()
        {
            return new DroppingServerInterceptor();
        }
    }
}